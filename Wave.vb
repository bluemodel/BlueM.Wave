'*******************************************************************************
'*******************************************************************************
'**** Klasse Wave                                                           ****
'****                                                                       ****
'**** Tool zur Darstellung und Analyse von Zeitreihen in TeeChart           ****
'****                                                                       ****
'**** Autoren: Michael Bach, Felix Froehlich,                               ****
'****          Dirk Muschalla, Christoph Hübner                             ****
'****                                                                       ****
'**** Fachgebiet Ingenieurhydrologie und Wasserbewirtschaftung              ****
'**** TU Darmstadt                                                          ****
'*******************************************************************************
'*******************************************************************************
Imports System.Text.RegularExpressions
Imports System.IO

''' <summary>
''' Hauptformular
''' </summary>
Public Class Wave

    'Eigenschaften
    '#############

    'Log-Fenster
    Private myLogWindow As LogWindow

    'Log
    Private WithEvents myLog As Log

    'Collection von importierten Dateien
    Private ImportedFiles As List(Of Dateiformat)

    'Interne Zeitreihen-Collection
    Private Zeitreihen As Dictionary(Of String, Zeitreihe)

    'Dateifilter
    Private Const FileFilter_TEN As String = "TeeChart-Dateien (*.ten)|*.ten"
    Private Const FileFilter_Import As String = _
            "Alle Dateien (*.*)|*.*|" & _
            "Text-Dateien (*.txt)|*.txt|" & _
            "CSV-Dateien (*.csv)|*.csv|" & _
            "ZRE-Dateien (*.zre)|*.zre|" & _
            "WEL-Dateien (*.wel, *.kwl)|*.wel;*.kwl|" & _
            "RVA-Dateien (*.rva)|*.rva|" & _
            "SMUSI-Dateien (*.asc)|*.asc|" & _
            "SIMBA-Dateien (*.smb)|*.smb|" & _
            "Hystem-Dateien (*.dat)|*.dat"

    'Chart-Zeugs
    Private WithEvents colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private selectionMade As Boolean 'Flag zeigt an, ob bereits ein Auswahlbereich ausgewählt wurde
    Private MyAxes1, MyAxes2 As Dictionary(Of String, Steema.TeeChart.Axis)
    Private WithEvents ChartListBox1 As Steema.TeeChart.ChartListBox

    'Methoden
    '########

#Region "Form behavior"

    'Konstruktor
    '***********
    Public Sub New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        'Kollektionen einrichten
        '-----------------------
        Me.ImportedFiles = New List(Of Dateiformat)()
        Me.Zeitreihen = New Dictionary(Of String, Zeitreihe)()
        Me.MyAxes1 = New Dictionary(Of String, Steema.TeeChart.Axis)
        Me.MyAxes2 = New Dictionary(Of String, Steema.TeeChart.Axis)

        'Charts einrichten
        '-----------------
        Me.ChartListBox1 = New Steema.TeeChart.ChartListBox()
        Call Me.Init_Charts()

        'Logfenster nur beim ersten Mal instanzieren
        '-------------------------------------------
        If (IsNothing(myLogWindow)) Then
            myLogWindow = New LogWindow()
        End If

        'Log (Singleton) Instanz holen
        Me.myLog = Log.getInstance()

    End Sub

    'Form wird geladen
    '*****************
    Private Sub Wave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'nix zu tun
    End Sub

    ''' <summary>
    ''' Wenn sich der Log verändert hat, Statustext aktualisieren
    ''' </summary>
    Private Sub LogChanged() Handles myLog.LogChanged
        'Status Info aktualisieren
        Me.ToolStripStatusLabel_Log.Text = Log.LastMessage
    End Sub

    'Drag & Drop von Dateien verarbeiten
    '***********************************
    Private Sub Wave_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then

            Dim dateien() As String

            'Dateien einem Array zuweisen
            dateien = e.Data.GetData(DataFormats.FileDrop)

            'Die einzelnen Dateien importieren
            For Each datei As String In dateien
                Call Me.Import_File(datei)
            Next

        End If

    End Sub

    'Drag & Drop von Dateien zulassen
    '********************************
    Private Sub Wave_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

#End Region 'Form behavior

#Region "Chart behavior"

    'Charts neu einrichten
    '*********************
    Private Sub Init_Charts()

        'Charts zurücksetzen
        Me.TChart1.Clear()
        Call Wave.formatChart(Me.TChart1.Chart)

        Me.TChart2.Clear()
        Call Wave.formatChart(Me.TChart2.Chart)
        Me.TChart2.Header.Visible = False

        'Übersicht darf nicht gescrolled oder gezoomt werden
        Me.TChart2.Zoom.Allow = False
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Hauptdiagramm darf nur horizontal gescrolled oder gezoomt werden
        Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
        Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal

        'Achsen
        Me.TChart1.Axes.Bottom.Automatic = False
        Me.TChart1.Axes.Bottom.Labels.Angle = 90
        Me.TChart1.Axes.Bottom.Labels.DateTimeFormat = "dd.MM.yy HH:mm"
        Me.TChart1.Axes.Right.Title.Angle = 90
        Me.TChart2.Axes.Bottom.Automatic = False

        'ChartListBox
        Me.ChartListBox1.Chart = Me.TChart1

        'ColorBand einrichten
        Me.selectionMade = False
        Call Me.Init_ColorBand()

    End Sub

    'ColorBand einrichten
    '********************
    Private Sub Init_ColorBand()
        colorBand1 = New Steema.TeeChart.Tools.ColorBand()
        Me.TChart2.Tools.Add(colorBand1)
        colorBand1.Axis = Me.TChart2.Axes.Bottom
        colorBand1.Brush.Color = Color.Coral
        colorBand1.Brush.Transparency = 50
        colorBand1.ResizeEnd = True
        colorBand1.ResizeStart = True
        colorBand1.EndLinePen.Visible = False
        colorBand1.StartLinePen.Visible = False
    End Sub

    'Charts aktualisieren
    '********************
    Private Sub UpdateCharts()

        Dim i, AnzSerien As Integer

        AnzSerien = Me.TChart1.Series.Count()

        'kleinsten und größten X-Wert bestimmen
        Dim Xmin, Xmax, tmpXmin, tmpXmax As DateTime
        Xmin = Date.FromOADate(Me.TChart1.Series(0).MinXValue)
        Xmax = Date.FromOADate(Me.TChart1.Series(0).MaxXValue)
        For i = 1 To AnzSerien - 1
            tmpXmin = Date.FromOADate(Me.TChart1.Series(i).MinXValue)
            tmpXmax = Date.FromOADate(Me.TChart1.Series(i).MaxXValue)
            If (tmpXmin < Xmin) Then Xmin = tmpXmin
            If (tmpXmax > Xmax) Then Xmax = tmpXmax
        Next

        'Übersicht neu skalieren
        Me.TChart2.Axes.Bottom.Minimum = Xmin.ToOADate()
        Me.TChart2.Axes.Bottom.Maximum = Xmax.ToOADate()

        If (Not Me.selectionMade) Then
            'Alles auswählen
            colorBand1.Start = Me.TChart2.Axes.Bottom.Minimum
            colorBand1.End = Me.TChart2.Axes.Bottom.Maximum
        End If

        'Hauptdiagramm neu skalieren
        Me.TChart1.Axes.Bottom.Minimum = colorBand1.Start
        Me.TChart1.Axes.Bottom.Maximum = colorBand1.End

    End Sub

    'TChart1 Scrolled, Zoomed, ZoomUndone
    '************************************
    Private Sub TChart1_Scrolled(ByVal sender As Object, ByVal e As System.EventArgs) Handles TChart1.Scroll, TChart1.Zoomed, TChart1.UndoneZoom
        Me.colorBand1.Start = Me.TChart1.Axes.Bottom.Minimum
        Me.colorBand1.End = Me.TChart1.Axes.Bottom.Maximum
        Me.selectionMade = True
    End Sub

    'ColorBand Resized
    '*****************
    Private Sub TChart2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart2.MouseUp
        Me.TChart1.Axes.Bottom.Minimum = Me.colorBand1.Start
        Me.TChart1.Axes.Bottom.Maximum = Me.colorBand1.End
        Me.selectionMade = True
    End Sub

    'TChart2 DoubleClick
    '*******************
    Private Sub TChart2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TChart2.DoubleClick
        Me.TChart2.ShowEditor()
    End Sub

    ''' <summary>
    ''' Eine im TeeChart-Editor gelöschte Serie intern löschen
    ''' </summary>
    ''' <remarks>
    ''' Wird für jede gelöschte Serie ein Mal aufgerufen.
    ''' Funktioniert nur unter der Annahme, dass alle Serien unterschiedliche Titel haben.
    ''' </remarks>
    Private Sub DeleteSeriesInternally(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChartListBox1.RemovedSeries

        Dim found As Boolean
        Dim title As String
        Dim s As Steema.TeeChart.Styles.Series

        title = ""

        'Alle internen Zeitreihen durchlaufen und prüfen, ob es sie noch gibt
        For Each zre As Zeitreihe In Me.Zeitreihen.Values
            found = False
            For Each s In Me.ChartListBox1.Items
                If (s.Title = zre.Title) Then
                    found = True 'diese Serie gibt es noch
                    Exit For
                End If
            Next
            If (Not found) Then
                title = zre.Title 'diese Serie gibt es nicht mehr
                Exit For
            End If
        Next

        If (title <> "") Then
            'Aus der internen Collection löschen
            Me.Zeitreihen.Remove(title)

            'Aus der Übersicht löschen
            For i As Integer = Me.TChart2.Series.Count - 1 To 0 Step -1
                If (Me.TChart2.Series.Item(i).Title = title) Then
                    Me.TChart2.Series.RemoveAt(i)
                    Me.TChart2.Refresh()
                    Exit For
                End If
            Next
        End If

    End Sub

#End Region 'Chart behavior'

#Region "UI"

    'Neu
    '***
    Private Sub Neu(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Neu.Click

        Dim res As MsgBoxResult

        'Warnen, wenn bereits Serien vorhanden
        '-------------------------------------
        If (Me.TChart1.Series.Count() > 0) Then
            res = MsgBox("Alle vorhandenen Serien werden gelöscht!" & eol & "Fortfahren?", MsgBoxStyle.OkCancel)
            If (Not res = Windows.Forms.DialogResult.OK) Then Exit Sub
        End If

        'Charts zurücksetzen
        Call Me.Init_Charts()

        'Collections zurücksetzen
        Me.ImportedFiles.Clear()
        Me.Zeitreihen.Clear()
        Me.MyAxes1.Clear()
        Me.MyAxes2.Clear()

        'Log zurücksetzen
        Call Log.ClearLog()
        Call Me.myLogWindow.Hide()

    End Sub

    'TEN-Datei öffnen
    '****************
    Private Sub Öffnen(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Öffnen.Click
        Me.OpenFileDialog1.Title = "TEN-Datei öffnen"
        Me.OpenFileDialog1.Filter = FileFilter_TEN
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Open_TEN(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'Serie(n) importieren
    '********************
    Private Sub Importieren(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Import.Click
        Me.OpenFileDialog1.Title = "Serie(n) importieren"
        Me.OpenFileDialog1.Filter = FileFilter_Import
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Import_File(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'Serie eingeben
    '**************
    Private Sub Eingeben(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_EnterSeries.Click
        Dim SeriesEditor As New SeriesEditorDialog()
        If (SeriesEditor.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Me.AddZeitreihe(SeriesEditor.Zeitreihe)
            Call Me.Display_Series(SeriesEditor.Zeitreihe)
        End If
    End Sub

    'Zeitreihe zuschneiden
    '*********************
    Private Sub Zuschneiden(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Cut.Click

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("Es sind keine Zeitreihen zum Zuschneiden verfügbar!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Dialog vorbereiten
        Dim cutter As New CutDialog(Me.Zeitreihen)

        'Dialog anzeigen
        If (cutter.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            'Neue Reihe speichern und anzeigen
            Me.AddZeitreihe(cutter.zreCut)
            Me.Display_Series(cutter.zreCut)
        End If

    End Sub

    'Edit Chart
    '**********
    Private Sub EditChart(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_EditChart.Click, TChart1.DoubleClick
        Call Me.TChart1.ShowEditor()
    End Sub

    'Speichern
    '*********
    Private Sub Speichern(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Speichern.Click
        Call Me.TChart1.Export.ShowExportDialog()
    End Sub

    'Exportieren
    '***********
    Private Sub Exportieren(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Export.Click

        Dim ExportDiag As New ExportDiag()
        Dim Reihe As Zeitreihe


        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("Es sind keine Zeitreihen für den Export verfügbar!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        'Exportdialog vorbereiten
        '------------------------
        'Liste der Formate
        ExportDiag.ComboBox_Format.DataSource = System.Enum.GetValues(GetType(Konstanten.Dateiformate))
        'Zeitreihen in Listbox eintragen
        For Each Reihe In Me.Zeitreihen.Values
            ExportDiag.ListBox_Zeitreihen.Items.Add(Reihe)
        Next

        'Exportdialog anzeigen
        '---------------------
        If (ExportDiag.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            'Speichern-Dialog vorbereiten
            '----------------------------
            Me.SaveFileDialog1.Title = "Speichern unter..."
            Me.SaveFileDialog1.AddExtension = True
            Select Case ExportDiag.ComboBox_Format.SelectedItem
                Case Dateiformate.ASC
                    Me.SaveFileDialog1.DefaultExt = "asc"
                    Me.SaveFileDialog1.Filter = "ASC-Dateien (*.asc)|*.asc"
                Case Dateiformate.CSV
                    Me.SaveFileDialog1.DefaultExt = "csv"
                    Me.SaveFileDialog1.Filter = "CSV-Dateien (*.csv)|*.csv"
                Case Dateiformate.WEL
                    Me.SaveFileDialog1.DefaultExt = "wel"
                    Me.SaveFileDialog1.Filter = "WEL-Dateien (*.wel)|*.wel"
                Case Dateiformate.ZRE
                    Me.SaveFileDialog1.DefaultExt = "zre"
                    Me.SaveFileDialog1.Filter = "ZRE-Dateien (*.zre)|*.zre"
                Case Dateiformate.REG
                    Me.SaveFileDialog1.DefaultExt = "reg"
                    Me.SaveFileDialog1.Filter = "REG-Dateien (*.reg)|*.reg"
            End Select
            Me.SaveFileDialog1.Filter &= "|Alle Dateien (*.*)|*.*"
            Me.SaveFileDialog1.FilterIndex = 1

            'Speichern-Dialog anzeigen
            '-------------------------
            If (Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                'Reihen exportieren
                Select Case ExportDiag.ComboBox_Format.SelectedItem
                    Case Dateiformate.ZRE
                        For Each item As Object In ExportDiag.ListBox_Zeitreihen.SelectedItems
                            Reihe = CType(item, Zeitreihe)
                            Call ZRE.Write_File(Reihe, Me.SaveFileDialog1.FileName)
                        Next
                    Case Dateiformate.REG
                        For Each item As Object In ExportDiag.ListBox_Zeitreihen.SelectedItems
                            Reihe = CType(item, Zeitreihe)
                            Call REG.Write_File(Reihe, Me.SaveFileDialog1.FileName)
                        Next
                    Case Else
                        MsgBox("Noch nicht implementiert!", MsgBoxStyle.Exclamation, "Wave")
                End Select

                MsgBox("Zeitreihe erfolgreich exportiert!", MsgBoxStyle.Information, "Wave")
            End If
        End If
    End Sub

    'Analysieren
    '***********
    Private Sub Analyse(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Analysis.Click

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("Es sind keine Zeitreihen für die Analyse verfügbar!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Dim oAnalysisDialog As New AnalysisDialog(Me.Zeitreihen)

        'Analysisdialog anzeigen
        '-----------------------
        If (oAnalysisDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            Try
                'Wait-Cursor
                Me.Cursor = Cursors.WaitCursor

                Call Log.AddLogEntry("Starte Analyse " & oAnalysisDialog.selectedAnalysisFunction.ToString() & " ...")

                'Analyse instanzieren
                Dim oAnalysis As Analysis
                oAnalysis = AnalysisFactory.CreateAnalysis(oAnalysisDialog.selectedAnalysisFunction, oAnalysisDialog.selectedZeitreihen)

                Call Log.AddLogEntry("... Analyse ausführen ...")

                'Analyse ausführen
                Call oAnalysis.ProcessAnalysis()

                Call Log.AddLogEntry("... Analyseergebnis aufbereiten ...")

                'Ergebnisse aufbereiten
                Call oAnalysis.PrepareResults()

                Call Log.AddLogEntry("Analyse abgeschlossen")

                'Default-Cursor
                Me.Cursor = Cursors.Default

                'Ergebnisse anzeigen:
                '--------------------
                'Ergebnisdiagramm anzeigen
                If (oAnalysis.hasResultChart) Then
                    Dim Wave2 As New Wave()
                    Wave2.Text = "Analyse-Ergebnis"
                    Wave2.Übersicht_Toggle(False)
                    Wave2.TChart1.Chart = oAnalysis.getResultChart()
                    Call Wave2.Show()
                End If

                'Ergebnistext in Log schreiben und anzeigen
                If (oAnalysis.hasResultText) Then
                    Call Log.AddLogEntry(oAnalysis.getResultText)
                    Call Me.myLogWindow.Show()
                    Call Me.myLogWindow.BringToFront()
                End If

                'Ergebniswerte anzeigen
                If (oAnalysis.hasResultValues) Then
                    'TODO: Ergebniswerte anzeigen
                End If

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                'Logeintrag
                Call Log.AddLogEntry("Analyse fehlgeschlagen:" & eol & ex.Message)
                'Alert
                MsgBox("Analyse fehlgeschlagen:" & eol & ex.Message, MsgBoxStyle.Critical)
            End Try

        End If

    End Sub

    'Drucken
    '*******
    Private Sub Drucken(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Drucken.Click
        Call Me.TChart1.Printer.Preview()
    End Sub

    'Kopieren (als PNG)
    '******************
    Private Sub Kopieren(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Kopieren.Click
        Call Me.TChart1.Export.Image.PNG.CopyToClipboard()
    End Sub

    'Log anzeigen
    '************
    Private Sub ShowLog(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel_Log.Click

        'LogWindow anzeigen
        Call Me.myLogWindow.Show()
        Me.myLogWindow.WindowState = FormWindowState.Normal
        Call Me.myLogWindow.BringToFront()

    End Sub

    'Hilfe
    '*****
    Private Sub Info(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Info.Click
        Dim AboutDialog As New AboutDialog()
        Call AboutDialog.ShowDialog()
    End Sub

    'Übersicht an/aus
    '****************
    Private Sub Übersicht_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Übersicht.Click

        Call Übersicht_Toggle(ToolStripButton_Übersicht.Checked)

    End Sub

    Private Sub Übersicht_Toggle(ByVal showÜbersicht As Boolean)

        If (showÜbersicht) Then
            Me.SplitContainer1.Panel1Collapsed = False
            Me.ToolStripButton_Übersicht.Checked = True
        Else
            Me.SplitContainer1.Panel1Collapsed = True
            Me.ToolStripButton_Übersicht.Checked = False
        End If

    End Sub

    ''' <summary>
    ''' Löscht alle vorhandenen Serien und liest alle importierten Zeitreihen neu ein
    ''' </summary>
    Private Sub ReRead_Files(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ReRead.Click

        Dim Datei As Dateiformat
        Dim Dateiliste As String
        Dim Answer As MsgBoxResult

        'Wenn keine Dateien vorhanden, abbrechen
        If (Me.ImportedFiles.Count = 0) Then
            MsgBox("Es sind keine Dateien bekannt, die neu eingelesen werden könnten!", MsgBoxStyle.Information, "Dateien neu einlesen")
            Exit Sub
        End If

        'Dateiliste in Textform generieren
        Dateiliste = ""
        For Each Datei In Me.ImportedFiles
            Dateiliste &= Datei.File & eol
        Next

        'Dialog anzeigen
        Answer = MsgBox("Alle Serien löschen und folgende Dateien neu einlesen?" & eol & Dateiliste, MsgBoxStyle.OkCancel, "Dateien neu einlesen")

        If (Answer = MsgBoxResult.Ok) Then

            'Alle Serien löschen
            Me.TChart1.Series.RemoveAllSeries()
            Me.TChart2.Series.RemoveAllSeries()

            'Collection zurücksetzen
            Me.Zeitreihen.Clear()

            'Alle Dateien durchlaufen
            For Each Datei In Me.ImportedFiles
                'Jede Datei neu einlesen
                Call Datei.Read_File()
                'Alle Zeitreihen der Datei durchlaufen
                For Each zre As Zeitreihe In Datei.Zeitreihen
                    'Jede Zeitreihe abspeichern und anzeigen
                    Call Me.AddZeitreihe(zre)
                    Call Me.Display_Series(zre)
                Next
            Next

        End If

    End Sub

#End Region 'UI

#Region "Funktionalität"

    'Zeitreihe intern hinzufügen
    '***************************
    Private Sub AddZeitreihe(ByRef zre As Zeitreihe)

        Dim n As Integer = 1

        'Umbenennen, falls Titel schon vergeben
        'Format: "Titel (n)"
        Do While (Me.Zeitreihen.ContainsKey(zre.Title))

            Dim pattern As String = "(?<name>.*)\s\(\d+\)$"
            Dim match As Match = Regex.Match(zre.Title, pattern)

            If (match.Success) Then
                n += 1
                zre.Title = Regex.Replace(zre.Title, pattern, "${name} (" & n.ToString() & ")")
            Else
                zre.Title &= " (1)"
            End If
        Loop

        Me.Zeitreihen.Add(zre.Title, zre)

    End Sub

    'TEN-Datei importieren
    '*********************
    Private Sub Open_TEN(ByVal FileName As String)

        Dim res As DialogResult
        Dim i As Integer
        Dim reihe As Zeitreihe

        'Warnen, wenn bereits Serien vorhanden (Chart wird komplett überschrieben!)
        '--------------------------------------------------------------------------
        If (Me.TChart1.Series.Count() > 0) Then
            res = MsgBox("Die vorhandenen Serien werden überschrieben!" & eol & "Fortfahren?", MsgBoxStyle.OkCancel)
            If (Not res = Windows.Forms.DialogResult.OK) Then Exit Sub
        End If

        Try

            'Log
            Call Log.AddLogEntry("Öffne Datei '" & FileName & "' ...")

            'TEN-Datei importieren
            '---------------------
            Call TChart1.Import.Template.Load(FileName)
            Call TChart2.Import.Template.Load(FileName)

            'Zeitreihen-Objekte aus TChart importieren
            '-----------------------------------------
            'Alte Zeitreihen löschen
            Me.Zeitreihen.Clear()

            'Alle Reihen durchlaufen
            For Each series As Steema.TeeChart.Styles.Series In TChart1.Series

                'Nur Zeitreihen importieren!
                If (series.GetHorizAxis.IsDateTime) Then

                    reihe = New Zeitreihe(series.Title)

                    For i = 0 To reihe.Length - 1
                        reihe.AddNode(Date.FromOADate(series.XValues(i)), series.YValues(i))
                    Next

                    'Zeitreihe abspeichern
                    Call Me.AddZeitreihe(reihe)
                End If
            Next

            'Log
            Call Log.AddLogEntry("... Datei '" & FileName & "' erfolgreich geöffnet!")

        Catch ex As Exception
            MsgBox("Fehler beim Öffnen:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry("... Fehler beim Öffnen:" & eol & ex.Message)
        End Try

        'Übersicht anpassen
        '------------------
        TChart2.Header.Visible = False

        'ColorBand neu einrichten (geht bei TEN-Import verloren)
        '-------------------------------------------------------
        Call Me.Init_ColorBand()

        'Charts aktualisieren
        '--------------------
        Me.selectionMade = False
        Call Me.UpdateCharts()

    End Sub

    ''' <summary>
    ''' Zeitreihe(n) aus einer Datei importieren
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    Public Sub Import_File(ByVal file As String)

        Dim Datei As Dateiformat
        Dim i As Integer
        Dim ok As Boolean

        'Sonderfälle abfangen:
        '---------------------
        Select Case Path.GetExtension(file).ToUpper()

            Case Dateifactory.FileExtTEN
                '.TEN-Datei
                Call Me.Open_TEN(file)

            Case Dateifactory.FileExtRVA
                '.RVA-Datei
                Call Me.Import_RVA(file)

            Case Else

                'Normalfall:
                '-----------

                Try
                    'Log
                    Call Log.AddLogEntry("Importiere Datei '" & file & "' ...")

                    'Datei-Instanz erzeugen
                    Datei = Dateifactory.getDateiInstanz(file)

                    If (Datei.UseImportDialog) Then
                        'Falls Importdialog erforderlich, diesen anzeigen
                        ok = Me.showImportDialog(Datei)
                        Call Application.DoEvents()
                    Else
                        'Ansonsten alle Spalten auswählen
                        Call Datei.selectAllSpalten()
                        ok = True
                    End If

                    If (ok) Then

                        Cursor = Cursors.WaitCursor

                        'Datei einlesen
                        Call Datei.Read_File()

                        'Log
                        Call Log.AddLogEntry("Datei '" & file & "' erfolgreich importiert!")
                        Application.DoEvents()

                        'Datei abspeichern
                        Me.ImportedFiles.Add(Datei)

                        'Log
                        Call Log.AddLogEntry("Zeitreihen in Diagramm laden...")
                        Application.DoEvents()

                        'Alle eingelesenen Zeitreihen der Datei durchlaufen
                        For i = 0 To Datei.Zeitreihen.GetUpperBound(0)
                            'Serie abspeichen
                            Me.AddZeitreihe(Datei.Zeitreihen(i))
                            'Serie anzeigen
                            Call Me.Display_Series(Datei.Zeitreihen(i))
                        Next

                        'Log
                        Call Log.AddLogEntry("Zeitreihen erfolgreich in Diagramm geladen!")

                    Else
                        'Import abgebrochen
                        Log.AddLogEntry("Import abgebrochen!")

                    End If

                Catch ex As Exception
                    MsgBox("Fehler beim Import:" & eol & ex.Message, MsgBoxStyle.Critical)
                    Call Log.AddLogEntry("Fehler beim Import: " & ex.Message)

                Finally
                    Cursor = Cursors.Default
                End Try

        End Select

    End Sub

    Public Sub Select_ASC(ByVal Workdir As String)

        Dim FileName As String

        OpenFileDialog1.Title = "ASC-Datei auswählen"
        OpenFileDialog1.Filter = "SMUSI-Dateien (*.asc)|*.asc"
        OpenFileDialog1.InitialDirectory = Workdir

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FileName = OpenFileDialog1.FileName
            If Not (FileName Is Nothing) Then
                Call Me.Import_File(FileName)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Eine RVA-Datei importieren
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <remarks>Sonderfall, weil keine Zeitreihen</remarks>
    Private Sub Import_RVA(ByVal file As String)

        Dim RVADatei As RVA

        Try
            'Log
            Call Log.AddLogEntry("Importiere Datei '" & file & "' ...")

            'Datei-Instanz erzeugen
            RVADatei = Dateifactory.getDateiInstanz(file)

            'Einlesen
            Call RVADatei.Read_File()

            'Log
            Call Log.AddLogEntry("... Datei '" & file & "' erfolgreich importiert!")

            'Datei abspeichern
            Me.ImportedFiles.Add(RVADatei)

            'Chart vorbereiten
            Call Me.PrepareChart_RVA()

            'Serie zeichnen
            Call Me.Display_RVA(RVADatei.RVAValues, True)

        Catch ex As Exception
            MsgBox("Fehler beim Import:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry("... Fehler beim Import:" & eol & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Zeigt den Importdialog an und liest im Anschluss die Datei mit den eingegebenen Einstellungn ein
    ''' </summary>
    ''' <param name="Datei">Instanz der Datei, die importiert werden soll</param>
    Private Function showImportDialog(ByRef Datei As Dateiformat) As Boolean

        Datei.ImportDiag = New ImportDiag(Datei)

        Dim DiagResult As DialogResult

        'Dialog anzeigen
        DiagResult = Datei.ImportDiag.ShowDialog(Me)

        If (DiagResult = Windows.Forms.DialogResult.OK) Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Eine Zeitreihe im Diagramm anzeigen
    ''' </summary>
    ''' <param name="zre">Die anzuzeigende Zeitreihe</param>
    Public Sub Display_Series(ByVal zre As Zeitreihe)

        Dim AxisNo As Integer

        'NaN und Infinity-Stützstellen entfernen
        zre = zre.getCleanZRE()

        'Serie zu Hauptdiagramm und zu Übersichtsdiagramm hinzufügen

        'Linien instanzieren
        Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        Dim Line2 As New Steema.TeeChart.Styles.Line(Me.TChart2.Chart)

        'X-Werte als Zeitdaten einstellen
        Line1.XValues.DateTime = True
        Line2.XValues.DateTime = True

        'Namen vergeben
        Line1.Title = zre.Title
        Line2.Title = zre.Title

        'Punkte zur Serie hinzufügen
        For Each node As KeyValuePair(Of DateTime, Double) In zre.Nodes
            Line1.Add(node.Key, node.Value)
            Line2.Add(node.Key, node.Value)
        Next

        'Achsenzuordnung
        AxisNo = getAxisNo(zre.Einheit)

        'Reihe der Achse zuordnen
        '(Unterscheidung zwischen Standard- und Custom-Achsen notwendig)
        Select Case AxisNo
            Case 1
                'Linke Achse
                Line1.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left
                Line2.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left
            Case 2
                'Rechte Achse
                Line1.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
                Line2.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
            Case Else
                'Custom Achse
                Line1.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom
                Line1.CustomVertAxis = Me.MyAxes1(zre.Einheit)
                Line2.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom
                Line2.CustomVertAxis = Me.MyAxes2(zre.Einheit)
        End Select

        'Charts aktualisieren
        Call Me.UpdateCharts()

    End Sub

    'RVA-Ergebnis in Chart anzeigen
    '******************************
    Public Sub Display_RVA(ByVal RVAResult As RVA.Struct_RVAValues, Optional ByVal showAll As Boolean = False)

        Dim i, j As Integer
        Dim barLow, barMiddle, barHigh As Steema.TeeChart.Styles.Bar

        'Chart formatieren
        Call PrepareChart_RVA()

        'Säulen (HA-Werte)
        '-----------------
        barMiddle = New Steema.TeeChart.Styles.Bar(Me.TChart1.Chart)
        barMiddle.Marks.Visible = False
        barMiddle.Title = "HA Middle"
        If (RVAResult.Title <> "") Then barMiddle.Title &= " (" & RVAResult.Title & ")"

        barLow = New Steema.TeeChart.Styles.Bar(Me.TChart1.Chart)
        barLow.Marks.Visible = False
        barLow.Title = "HA Low"
        If (RVAResult.Title <> "") Then barLow.Title &= " (" & RVAResult.Title & ")"

        barHigh = New Steema.TeeChart.Styles.Bar(Me.TChart1.Chart)
        barHigh.Marks.Visible = False
        barHigh.Title = "HA High"
        If (RVAResult.Title <> "") Then barHigh.Title &= " (" & RVAResult.Title & ")"

        'Werte eintragen
        '---------------
        With RVAResult

            'Schleife über Parametergruppen
            For i = 0 To .IHAParamGroups.GetUpperBound(0)
                ''Gruppenname schreiben (Mit Wert 0)
                'bar.Add(0, .IHAParamGroups(i).GName)
                'Schleife über Parameter
                For j = 0 To .IHAParamGroups(i).IHAParams.GetUpperBound(0)
                    'Parameter eintragen
                    barMiddle.Add(.IHAParamGroups(i).IHAParams(j).HAMiddle, .IHAParamGroups(i).IHAParams(j).PName)
                    barLow.Add(.IHAParamGroups(i).IHAParams(j).HALow, .IHAParamGroups(i).IHAParams(j).PName)
                    barHigh.Add(.IHAParamGroups(i).IHAParams(j).HAHigh, .IHAParamGroups(i).IHAParams(j).PName)
                Next
            Next

        End With

        'Wenn showAll = False dann nur HAMiddle-Serie anzeigen
        If (Not showAll) Then
            barLow.Active = False
            barHigh.Active = False
        End If

    End Sub

    ''' <summary>
    ''' Gibt die Achsen-Nummer für eine bestimmte Einheit zurück
    ''' Wenn noch nicht vorhanden, wird eine neue Achse erstellt
    ''' </summary>
    ''' <param name="einheit">Die Einheit</param>
    ''' <returns>Der 1-basierte Index der zu verwendenden Achse in der MyAxes-Dictionary</returns>
    Private Function getAxisNo(ByVal einheit As String) As Integer

        Dim AxisNo As Integer

        If (Me.MyAxes1.ContainsKey(einheit)) Then

            'Nummer der vorhandenen, zu verwendenden Achse bestimmen
            AxisNo = 0
            For Each usedunit As String In Me.MyAxes1.Keys
                AxisNo += 1
                If (usedunit = einheit) Then
                    Exit For
                End If
            Next

        Else

            'Neue Einheit => neue Achse
            AxisNo = Me.MyAxes1.Count + 1

            Select Case AxisNo

                Case 1
                    'Erste Einheit links
                    Me.TChart1.Chart.Axes.Left.Title.Caption = einheit
                    Me.MyAxes1.Add(einheit, Me.TChart1.Chart.Axes.Left)
                    Me.MyAxes2.Add(einheit, Me.TChart2.Chart.Axes.Left)

                Case 2
                    'Zweite Einheit rechts
                    Me.TChart1.Chart.Axes.Right.Title.Caption = einheit
                    Me.MyAxes1.Add(einheit, Me.TChart1.Chart.Axes.Right)
                    Me.MyAxes2.Add(einheit, Me.TChart2.Chart.Axes.Right)

                Case Else
                    'Ab der dritten Einheit Custom Achsen erstellen
                    Dim customaxis1, customaxis2 As Steema.TeeChart.Axis
                    customaxis1 = Steema.TeeChart.Axes.CreateNewAxis(Me.TChart1.Chart)
                    customaxis2 = Steema.TeeChart.Axes.CreateNewAxis(Me.TChart2.Chart)
                    customaxis1.Visible = True
                    customaxis2.Visible = True
                    'Jede zweite Achse rechts anzeigen
                    If ((AxisNo) Mod 2 = 0) Then
                        customaxis1.OtherSide = True
                        customaxis2.OtherSide = True
                    End If
                    'Abstand berechnen
                    customaxis1.RelativePosition = Math.Ceiling((AxisNo - 2) / 2) * 8
                    customaxis2.RelativePosition = Math.Ceiling((AxisNo - 2) / 2) * 8

                    customaxis1.Title.Caption = einheit
                    customaxis1.Title.Angle = 90

                    Me.MyAxes1.Add(einheit, customaxis1)
                    Me.MyAxes2.Add(einheit, customaxis2)

            End Select

        End If

        Return AxisNo

    End Function

    ''' <summary>
    ''' Führt Standardformatierung eines TCharts aus
    ''' </summary>
    ''' <param name="chart"></param>
    Public Shared Sub formatChart(ByRef chart As Steema.TeeChart.Chart)

        chart.Aspect.View3D = False
        'chart.BackColor = Color.White
        chart.Panel.Gradient.Visible = False
        chart.Panel.Brush.Color = Color.White
        chart.Walls.Back.Transparent = False
        chart.Walls.Back.Gradient.Visible = False
        chart.Walls.Back.Color = Color.White

        'Header
        chart.Header.Text = ""

        'Legende
        chart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        chart.Legend.FontSeriesColor = True
        chart.Legend.CheckBoxes = True

    End Sub

    'Chart für RVA-Anzeige formatieren
    '*********************************
    Public Sub PrepareChart_RVA()

        'Übersicht ausschalten
        Call Me.Übersicht_Toggle(False)

        'Titel
        Me.TChart1.Header.Text = "RVA Analysis"

        'Achsen formatieren
        Me.TChart1.Axes.Bottom.Automatic = True
        Me.TChart1.Axes.Bottom.Labels.Angle = 90
        Me.TChart1.Axes.Bottom.Title.Caption = "IHA Parameter"
        Me.TChart1.Axes.Bottom.MinorTicks.Visible = False

        Me.TChart1.Axes.Left.Automatic = False
        Me.TChart1.Axes.Left.Minimum = -1.1
        Me.TChart1.Axes.Left.Maximum = 2
        Me.TChart1.Axes.Left.Labels.ValueFormat = "#,##0.0##"
        Me.TChart1.Axes.Left.Title.Caption = "Hydrologic Alteration"

        'Legende
        Me.TChart1.Legend.CheckBoxes = True

        'Markstips
        Dim markstip As New Steema.TeeChart.Tools.MarksTip()
        markstip.Style = Steema.TeeChart.Styles.MarksStyles.Value
        Me.TChart1.Tools.Add(markstip)

    End Sub

#End Region 'Funktionalität

End Class
