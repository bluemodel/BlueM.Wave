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

Imports System.IO

''' <summary>
''' Hauptformular
''' </summary>
Public Class Wave

    'Eigenschaften
    '#############

    'Log-Fenster
    Friend Shared Log As LogWindow

    Private WithEvents colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private selectionMade As Boolean                                    'Flag zeigt an, ob bereits ein Auswahlbereich ausgewählt wurde
    Private Zeitreihen As Collection

    Private Const FileFilter_TEN As String = "TeeChart-Dateien (*.ten)|*.ten"
    Private Const FileFilter_Import As String = _
            "Alle Dateien (*.*)|*.*|" & _
            "Text-Dateien (*.txt)|*.txt|" & _
            "CSV-Dateien (*.csv)|*.csv|" & _
            "ZRE-Dateien (*.zre)|*.zre|" & _
            "WEL-Dateien (*.wel, *.kwl)|*.wel;*.kwl|" & _
            "RVA-Dateien (*.rva)|*.rva|" & _
            "SMUSI-Dateien (*.asc)|*.asc"
    Public UsedUnits As New Collection
    Public CustomAxis1L As New Steema.TeeChart.Axis
    Public CustomAxis1R As New Steema.TeeChart.Axis
    Public CustomAxis2L As New Steema.TeeChart.Axis
    Public CustomAxis2R As New Steema.TeeChart.Axis


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
        Me.Zeitreihen = New Collection()

        'Charts einrichten
        '-----------------
        Call Me.Init_Charts()

        'Logfenster nur beim ersten Mal instanzieren
        '-------------------------------------------
        If (IsNothing(Log)) Then
            Log = New LogWindow()
        End If

    End Sub

    'Form wird geladen
    '*****************
    Private Sub Wave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'nix zu tun

    End Sub

    'Drag & Drop von Dateien verarbeiten
    '***********************************
    Private Sub Wave_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then

            Dim dateien() As String
            Dim i As Integer

            'Dateien einem Array zuweisen
            dateien = e.Data.GetData(DataFormats.FileDrop)

            'Die einzelnen Dateien importieren
            For i = 0 To dateien.GetUpperBound(0)
                Call Import_File(dateien(i))
            Next
        End If

    End Sub

    'Drag & Drop von Dateien zulassen
    '********************************
    Private Sub Wave_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    'Form Resize
    '***********
    Private Sub Wave_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Call Me.ResizeCharts()
    End Sub

    'Splitter Resize
    '***************
    Private Sub SplitContainer1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        Call Me.ResizeCharts()
    End Sub

#End Region 'Form behavior

#Region "Chart behavior"

    'Charts neu einrichten
    '*********************
    Private Sub Init_Charts()

        'Charts zurücksetzen
        Me.TChart1.Clear()
        Me.TChart1.Aspect.View3D = False
        Me.TChart2.Clear()
        Me.TChart2.Aspect.View3D = False
        Me.TChart2.Header.Visible = False

        'Übersicht darf nicht gescrolled oder gezoomt werden
        Me.TChart2.Zoom.Allow = False
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Hauptdiagramm darf nur horizontal gescrolled oder gezoomt werden
        Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal

        'Achsen
        Me.TChart1.Axes.Bottom.Automatic = False
        Me.TChart1.Axes.Bottom.Labels.Angle = 90
        Me.TChart1.Axes.Bottom.Labels.DateTimeFormat = "dd.MM.yy hh:mm"
        Me.TChart1.Axes.Right.Title.Angle = 90
        Me.TChart2.Axes.Bottom.Automatic = False
        Me.TChart2.Axes.Right.Title.Angle = 90

        'Legenden
        Me.TChart1.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        Me.TChart1.Legend.CheckBoxes = True
        Me.TChart1.Legend.FontSeriesColor = True
        Me.TChart2.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        Me.TChart2.Legend.CheckBoxes = True
        Me.TChart2.Legend.FontSeriesColor = True

        'ColorBand einrichten
        Me.selectionMade = False
        Call Me.Init_ColorBand()

        'Zusätzliche Achsen einrichten und den Charts zuweisen
        CustomAxis1L.OtherSide = False
        CustomAxis1L.Visible = False
        CustomAxis1L.RelativePosition = 8
        CustomAxis1L.Title.Angle = 90
        Me.TChart1.Chart.Axes.Custom.Add(CustomAxis1L)

        CustomAxis1R.OtherSide = True
        CustomAxis1R.Visible = False
        CustomAxis1R.RelativePosition = 8
        CustomAxis1R.Title.Angle = 90
        Me.TChart1.Chart.Axes.Custom.Add(CustomAxis1R)

        CustomAxis2L.OtherSide = False
        CustomAxis2L.Visible = False
        CustomAxis2L.RelativePosition = 8
        CustomAxis2L.Title.Angle = 90
        Me.TChart2.Chart.Axes.Custom.Add(CustomAxis2L)

        CustomAxis2R.OtherSide = True
        CustomAxis2R.Visible = False
        CustomAxis2R.RelativePosition = 8
        CustomAxis2R.Title.Angle = 90
        Me.TChart2.Chart.Axes.Custom.Add(CustomAxis2R)

    End Sub

    'Größe von Charts anpassen
    '*************************
    Private Sub ResizeCharts()
        Me.TChart2.Width = Me.SplitContainer1.Panel1.Width - 5
        Me.TChart2.Height = Me.SplitContainer1.Panel1.Height - 5
        Me.TChart1.Width = Me.SplitContainer1.Panel2.Width - 5
        Me.TChart1.Height = Me.SplitContainer1.Panel2.Height - 5 - 22
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

        'Collection zurücksetzen
        Me.Zeitreihen.Clear()

        'Messages zurücksetzen
        Call Log.ClearLog()
        Call Log.Hide()

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
            Me.Zeitreihen.Add(SeriesEditor.Zeitreihe)
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
            Me.Zeitreihen.Add(cutter.zreCut)
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
        For Each Reihe In Me.Zeitreihen
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

                Call Wave.Log.AddLogEntry("Starte Analyse " & oAnalysisDialog.selectedAnalysisFunction.ToString() & " ...")

                'Analyse instanzieren
                Dim oAnalysis As Analysis
                oAnalysis = AnalysisFactory.CreateAnalysis(oAnalysisDialog.selectedAnalysisFunction, oAnalysisDialog.selectedZeitreihen)

                'Analyse ausführen
                Call oAnalysis.ProcessAnalysis()

                Call Wave.Log.AddLogEntry("... Analyseergebnis aufbereiten ...")

                'Ergebnisse aufbereiten
                Call oAnalysis.PrepareResults()

                Call Wave.Log.AddLogEntry("... Analyse abgeschlossen")

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
                    Call Wave.Log.AddLogEntry(oAnalysis.getResultText)
                    Call Wave.Log.Show()
                End If

                'Ergebniswerte anzeigen
                If (oAnalysis.hasResultValues) Then
                    'TODO: Ergebniswerte anzeigen
                End If

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                'Logeintrag
                Call Wave.Log.AddLogEntry("Analyse fehlgeschlagen:" & eol & ex.Message)
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

        'MessageDialog anzeigen
        Call Log.Show()
        Log.WindowState = FormWindowState.Normal
        Call Log.BringToFront()

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

        Call Me.ResizeCharts()

    End Sub

#End Region 'UI

#Region "Funktionalität"

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
                reihe.Length = series.XValues.Count

                For i = 0 To reihe.Length - 1
                    reihe.XWerte(i) = Date.FromOADate(series.XValues(i))
                    reihe.YWerte(i) = series.YValues(i)
                Next

                Me.Zeitreihen.Add(reihe)
            End If
        Next

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

        'Kontrolle
        If (Not System.IO.File.Exists(file)) Then
            MsgBox("Datei '" & file & "' nicht gefunden!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        'Log
        Call Wave.Log.AddLogEntry("Importiere Datei '" & file & "' ...")

        Try

            Select Case Path.GetExtension(file).ToUpper()

                Case ".ZRE"
                    Call Me.Import_ZRE(file)

                Case ".WEL", ".KWL"
                    Call Me.Import_WEL(file)

                Case ".ASC"
                    Call Me.Import_ASC(file)

                Case ".RVA"
                    Call Me.Import_RVA(file)

                Case ".TEN"
                    Call Me.Open_TEN(file)

                Case Else
                    Call Me.Import_CSV(file)

            End Select

            'Logeintrag
            Call Wave.Log.AddLogEntry("... Import abgeschlossen.")

        Catch ex As Exception
            MsgBox("Fehler beim Import:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Wave.Log.AddLogEntry("... Fehler beim Import:" & eol & ex.Message)
        End Try

    End Sub

    'ZRE-Datei importieren
    '*********************
    Private Sub Import_ZRE(ByVal FileName As String)

        'ZRE-Objekt instanzieren
        Dim ZRE As New ZRE(FileName)

        'Sofort Spalten auslesen (bei ZRE kein ImportDialog!)
        Call ZRE.SpaltenAuslesen()

        'ZRE einlesen
        Call ZRE.Read_File()

        'Serie abspeichern
        Me.Zeitreihen.Add(ZRE.Zeitreihen(0))

        'Serie zeichnen
        '--------------
        Call Me.Display_Series(ZRE.Zeitreihen(0))

    End Sub

    'WEL-Datei importieren
    '*********************
    Private Sub Import_WEL(ByVal FileName As String)

        Dim i As Integer

        'WEL-Objekt instanzieren
        Dim WEL As New WEL(FileName)

        'Import-Dialog anzeigen
        Call Me.showImportDialog(WEL)

        'Serien zeichnen und abspeichern
        '-------------------------------
        If (Not IsNothing(WEL.Zeitreihen)) Then

            For i = 0 To WEL.Zeitreihen.GetUpperBound(0)
                Call Me.Display_Series(WEL.Zeitreihen(i))
                'Serie abspeichen
                Me.Zeitreihen.Add(WEL.Zeitreihen(i))
            Next

        End If

    End Sub

    'ASC-Datei importieren (SMUSI)
    '*****************************
    Private Sub Import_ASC(ByVal FileName As String)

        Dim i As Integer

        'ASC-Objekt instanzieren
        Dim ASC As New ASC(FileName)

        'Import-Dialog anzeigen
        Call Me.showImportDialog(ASC)

        'Serien zeichnen
        '---------------
        If (Not IsNothing(ASC.Zeitreihen)) Then

            For i = 0 To ASC.Zeitreihen.GetUpperBound(0)
                Call Me.Display_Series(ASC.Zeitreihen(i))
                'Serie abspeichern
                Me.Zeitreihen.Add(ASC.Zeitreihen(i))
            Next

        End If

    End Sub

    Public Sub Select_ASC(ByVal Workdir As String)

        Dim FileName As String

        OpenFileDialog1.Title = "ASC-Datei auswählen"
        OpenFileDialog1.Filter = "SMUSI-Dateien (*.asc)|*.asc"
        OpenFileDialog1.InitialDirectory = Workdir

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FileName = OpenFileDialog1.FileName
            If Not (FileName Is Nothing) Then
                Call Import_ASC(FileName)
            End If
        End If

    End Sub

    'RVA-Datei importieren
    '*********************
    Private Sub Import_RVA(ByVal FileName As String)

        'RVA-Objekt instanzieren
        Dim RVA As New RVA(FileName)

        'Chart vorbereiten
        Call Me.PrepareChart_RVA()

        'Serie zeichnen
        Call Me.Display_RVA(RVA.RVAValues, True)

    End Sub

    'CSV-Datei importieren
    '*********************
    Public Sub Import_CSV(ByVal FileName As String)

        Dim i As Integer

        'CSV-Objekt instanzieren
        Dim CSV As New CSV(FileName)

        'Import-Dialog anzeigen
        Call Me.showImportDialog(CSV)

        'Serien zeichnen
        '---------------
        If (Not IsNothing(CSV.Zeitreihen)) Then

            For i = 0 To CSV.Zeitreihen.GetUpperBound(0)
                Call Me.Display_Series(CSV.Zeitreihen(i))
                ' Serie abspeichern
                Me.Zeitreihen.Add(CSV.Zeitreihen(i))
            Next

        End If

    End Sub

    'Import-Formular anzeigen
    '*********************
    Private Sub showImportDialog(ByRef _datei As Dateiformat)

        _datei.ImportDiag = New ImportDiag(_datei)

        Dim DiagResult As DialogResult

        'Dialog anzeigen
        DiagResult = _datei.ImportDiag.ShowDialog()

        If (DiagResult = Windows.Forms.DialogResult.OK) Then
            'Datei einlesen
            Call _datei.Read_File()
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Eine Zeitreihe im Diagramm anzeigen
    ''' </summary>
    ''' <param name="zre">Die anzuzeigende Zeitreihe</param>
    Public Sub Display_Series(ByVal zre As Zeitreihe)

        'Falls alle Serien im Chart gelöscht wurden
        If Not UsedUnits.Count = 0 And TChart1.Chart.Series.Count = 0 Then
            UsedUnits.Clear()
            Me.TChart1.Chart.Axes.Left.Title.Text = ""
            Me.TChart2.Chart.Axes.Left.Title.Text = ""
            Me.TChart1.Chart.Axes.Right.Title.Text = ""
            Me.TChart2.Chart.Axes.Right.Title.Text = ""
            CustomAxis1L.Title.Text = ""
            CustomAxis2L.Title.Text = ""
            CustomAxis1R.Title.Text = ""
            CustomAxis2R.Title.Text = ""
            CustomAxis1L.Visible = False
            CustomAxis2L.Visible = False
            CustomAxis1R.Visible = False
            CustomAxis2R.Visible = False
        End If

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

        'Neue Serie hinzufügen
        Line1.Add(zre.XWerte, zre.YWerte)
        Line2.Add(zre.XWerte, zre.YWerte)

        'Verwendete Einheiten werden gespeichert um sie später wieder zuzuordnen
        If Not UsedUnits.Contains(zre.Einheit) Then
            UsedUnits.Add(UsedUnits.Count + 1, zre.Einheit)

            Select Case UsedUnits.Count
                Case 1
                    Me.TChart1.Chart.Axes.Left.Title.Text = zre.Einheit
                    Me.TChart2.Chart.Axes.Left.Title.Text = zre.Einheit
                Case 2
                    Me.TChart1.Chart.Axes.Right.Title.Text = zre.Einheit
                    Me.TChart2.Chart.Axes.Right.Title.Text = zre.Einheit
                Case 3
                    CustomAxis1L.Title.Text = zre.Einheit
                    CustomAxis1L.Visible = True
                    CustomAxis2L.Title.Text = zre.Einheit
                    CustomAxis2L.Visible = True
                Case 4
                    CustomAxis1R.Title.Text = zre.Einheit
                    CustomAxis1R.Visible = True
                    CustomAxis2R.Title.Text = zre.Einheit
                    CustomAxis2R.Visible = True
                Case 5
                    CustomAxis1R.Title.Text = ""
                    CustomAxis2R.Title.Text = ""
            End Select
        End If

        'Lines werden auf die beiden Achsen verteilt
        Select Case UsedUnits(zre.Einheit)
            Case 1
                Line1.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left
                Line2.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left
            Case 2
                Line1.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
                Line2.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
            Case 3
                Line1.CustomVertAxis = CustomAxis1L
                Line2.CustomVertAxis = CustomAxis2L
            Case Else
                Line1.CustomVertAxis = CustomAxis1R
                Line2.CustomVertAxis = CustomAxis2R
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
