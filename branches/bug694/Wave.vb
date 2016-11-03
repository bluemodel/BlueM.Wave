'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
'
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
'
Imports System.Text.RegularExpressions
Imports System.IO

''' <summary>
''' Hauptformular
''' </summary>
Public Class Wave

    'Eigenschaften
    '#############

    ''' <summary>
    ''' Flag for preventing unintended feedback loops in the UI. Default is False, can be temporarily set to True.
    ''' </summary>
    ''' <remarks></remarks>
    Private isInitializing As Boolean

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
    Private Const FileFilter_XML As String = "Theme-Dateien (*.xml)|*.xml"
    Private Const FileFilter_Import As String = _
            "Alle Dateien (*.*)|*.*|" & _
            "Text-Dateien (*.txt)|*.txt|" & _
            "CSV-Dateien (*.csv)|*.csv|" & _
            "ZRE-Dateien (*.zre)|*.zre|" & _
            "WEL-Dateien (*.wel, *.kwl)|*.wel;*.kwl|" & _
            "UVF-Dateien (*.uvf)|*.uvf|" & _
            "SMUSI-Dateien (*.asc)|*.asc|" & _
            "SIMBA-Dateien (*.smb)|*.smb|" & _
            "Hystem-Dateien (*.dat)|*.dat|" & _
            "DWD-Temperatur-Feuchte (*.dtl)|*.dtl|" & _
            "SWMM-Dateien (*.out)|*.out|" & _
            "netCDF Daten (*.nc)|*.nc|" & _
            "HYDRO_AS-2D Dateien (*.dat)|*.dat|" & _
            "SYDRO Binärformat (*.bin)|*.bin"

    'Chart-Zeugs
    Private WithEvents colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private selectionMade As Boolean 'Flag zeigt an, ob bereits ein Auswahlbereich ausgewählt wurde
    Private MyAxes1, MyAxes2 As Dictionary(Of String, Steema.TeeChart.Axis)
    Private WithEvents ChartListBox1 As Steema.TeeChart.ChartListBox

    Private Const HelpURL As String = "http://wiki.bluemodel.org/index.php/Wave"

    'Methoden
    '########

#Region "Form behavior"

    'Konstruktor
    '***********
    Public Sub New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        Me.isInitializing = False

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

        'Navigation initialisieren
        Me.ComboBox_NavIncrement.SelectedItem = "Days"

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
        Me.TChart2.Panel.Brush.Color = Color.FromArgb(239,239,239)
        Me.TChart2.Walls.Back.Color = Color.FromArgb(239, 239, 239)
        Me.TChart2.Header.Visible = False
        Me.TChart2.Legend.Visible = False

        'Übersicht darf nicht gescrolled oder gezoomt werden
        Me.TChart2.Zoom.Allow = False
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Hauptdiagramm darf nur horizontal gescrolled oder gezoomt werden
        Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
        Me.TChart1.Zoom.History = True
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

        Dim Xmin, Xmax As DateTime

        If (Me.Zeitreihen.Count = 0) Then
            'just refresh
            Me.TChart1.Refresh()
            Me.TChart2.Refresh()

        Else
            'Update Axes and colorBand

            'Min- und Max-Datum bestimmen
            Xmin = DateTime.MaxValue
            Xmax = DateTime.MinValue
            For Each zre As Zeitreihe In Me.Zeitreihen.Values
                If (zre.Anfangsdatum < Xmin) Then Xmin = zre.Anfangsdatum
                If (zre.Enddatum > Xmax) Then Xmax = zre.Enddatum
            Next

            'Übersicht neu skalieren
            Me.TChart2.Axes.Bottom.Minimum = Xmin.ToOADate()
            Me.TChart2.Axes.Bottom.Maximum = Xmax.ToOADate()

            If (Not Me.selectionMade) Then
                'Wenn noch nicht gezoomed wurde, Gesamtzeitraum auswählen
                colorBand1.Start = Xmin.ToOADate()
                colorBand1.End = Xmax.ToOADate()
                Me.TChart1.Axes.Bottom.Minimum = Xmin.ToOADate()
                Me.TChart1.Axes.Bottom.Maximum = Xmax.ToOADate()
            Else
                'Ansonsten Zoom auf Colorband übertragen
                colorBand1.Start = Me.TChart1.Axes.Bottom.Minimum
                colorBand1.End = Me.TChart1.Axes.Bottom.Maximum
            End If

            'Update navigation
            Call Me.updateNavigation()

        End If

    End Sub

    ''' <summary>
    ''' Handles TChart1 events Scrolled, Zoomed, UndoneZoom
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TChart1_ZoomChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TChart1.Scroll, TChart1.Zoomed, TChart1.UndoneZoom
        If (Me.TChart1.Axes.Bottom.Minimum <> Me.TChart1.Axes.Bottom.Maximum) Then
            'Update everything
            Call Me.updateColorband()
            Call Me.updateNavigation()
            Me.selectionMade = True
        End If
    End Sub

    ''' <summary>
    ''' Updates the colorband to correspond to the currently displayed timespan of the main chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub updateColorband()
        Me.colorBand1.Start = Me.TChart1.Axes.Bottom.Minimum
        Me.colorBand1.End = Me.TChart1.Axes.Bottom.Maximum
    End Sub

    'ColorBand Resized
    '*****************
    Private Sub TChart2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart2.MouseUp
        If (Me.colorBand1.Start > Me.colorBand1.End) Then
            'invalid selection - reset the colorband to the timespan of the main chart
            Me.colorBand1.Start = Me.TChart1.Axes.Bottom.Minimum
            Me.colorBand1.End = Me.TChart1.Axes.Bottom.Maximum
        Else
            'TODO: Add the previous zoom to the zoom history: Me.TChart1.Zoom.HistorySteps.Add(XXX)
            'set new min/max values for the bottom axis of the main chart
            Me.TChart1.Axes.Bottom.Minimum = Me.colorBand1.Start
            Me.TChart1.Axes.Bottom.Maximum = Me.colorBand1.End
            'Update navigation
            Call Me.updateNavigation()
            Me.selectionMade = True
        End If
    End Sub

    'TChart2 DoubleClick
    '*******************
    Private Sub TChart2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TChart2.DoubleClick
        Call Steema.TeeChart.Editor.Show(Me.TChart2)
    End Sub

    ''' <summary>
    ''' Eine im TeeChart-Editor gelöschte Serie intern löschen
    ''' </summary>
    ''' <remarks>
    ''' Wird für jede gelöschte Serie ein Mal aufgerufen.
    ''' Funktioniert nur unter der Annahme, dass alle Serien unterschiedliche Titel haben.
    ''' </remarks>
    Private Sub TChart1_SeriesRemoved(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChartListBox1.RemovedSeries

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

#Region "Toolbar"

    'Neu
    '***
    Private Sub Neu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Neu.Click

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

    'Dropdown für Öffnen Button
    '**************************
    Private Sub MenuDropDown_Oeffnen(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton_Oeffnen.ButtonClick
        Me.ToolStripSplitButton_Oeffnen.ShowDropDown()
    End Sub

    'Serie(n) importieren
    '********************
    Private Sub Importieren_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ZeitreihenImportieren.Click
        Me.OpenFileDialog1.Title = "Serie(n) importieren"
        Me.OpenFileDialog1.Filter = FileFilter_Import
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Import_File(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'TEN-Datei laden
    '****************
    Private Sub TENLaden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_TENLaden.Click
        Me.OpenFileDialog1.Title = "TEN-Datei laden"
        Me.OpenFileDialog1.Filter = FileFilter_TEN
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Load_TEN(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'Theme laden
    '***********
    Private Sub ThemeLaden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ThemeLaden.Click
        Me.OpenFileDialog1.Title = "Theme laden"
        Me.OpenFileDialog1.Filter = FileFilter_XML
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Load_Theme(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'Dropdown für Speichern Button
    '*****************************
    Private Sub MenuDropDown_Speichern(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton_Speichern.Click
        Me.ToolStripSplitButton_Speichern.ShowDropDown()
    End Sub

    'Teechart Export
    '***************
    Private Sub ExportDiagramm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ExportDiagramm.Click
        Call Me.TChart1.Export.ShowExportDialog()
    End Sub

    'Zeitreihen Export
    '*****************
    Private Sub ExportZeitreihe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ExportZeitreihe.Click
        Call ExportZeitreihe()
    End Sub

    ''' <summary>
    ''' Convert a file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Just a shortcut for importing and then exporting series</remarks>
    Private Sub Convert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton_Convert.Click
        Call Me.Importieren_Click(sender, e)
        Call Me.ExportZeitreihe()
    End Sub

    'Serie eingeben
    '**************
    Private Sub Eingeben_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ZeitreiheEingeben.Click
        Dim SeriesEditor As New SeriesEditorDialog()
        If (SeriesEditor.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Import_Series(SeriesEditor.Zeitreihe)
        End If
    End Sub

    'Zeitreihe zuschneiden
    '*********************
    Private Sub Zuschneiden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Cut.Click

        Dim title As String
        Dim titles As List(Of String)

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("Es sind keine Zeitreihen zum Zuschneiden verfügbar!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Dialog instanzieren
        Dim cutter As New CutDialog(Me.Zeitreihen)

        'Dialog anzeigen
        If (cutter.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            If (cutter.CheckBox_keepUncutSeries.Checked = False) Then
                'Alte Zeitreihe(n) löschen
                If (cutter.ComboBox_ZeitreiheCut.SelectedItem.ToString = CutDialog.labelAlle) Then
                    titles = New List(Of String)
                    For Each title In Me.Zeitreihen.Keys
                        titles.Add(title)
                    Next
                    For Each title In titles
                        Call Me.DeleteZeitreihe(title)
                    Next
                Else
                    title = cutter.ComboBox_ZeitreiheCut.SelectedItem.ToString
                    Call Me.DeleteZeitreihe(title)
                End If
            End If

            'Neue Reihe(n) importieren
            For Each zre As Zeitreihe In cutter.zreCut.Values
                Me.Import_Series(zre)
            Next

        End If

    End Sub

    'Edit Chart
    '**********
    Private Sub EditChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_EditChart.Click, TChart1.DoubleClick
        Call Steema.TeeChart.Editor.Show(Me.TChart1)
    End Sub

    ''' <summary>
    ''' Zeitreihe(n) exportieren
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExportZeitreihe()
        Dim ExportDiag As New ExportDiag()
        Dim Reihe As Zeitreihe
        Dim MultiReihe() As Zeitreihe
        Dim iReihe As Long


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
                Case Dateiformate.REG_HYSTEM
                    Me.SaveFileDialog1.DefaultExt = "reg"
                    Me.SaveFileDialog1.Filter = "HYSTEM-REG-Dateien (*.reg)|*.reg"
                Case Dateiformate.REG_SMUSI
                    Me.SaveFileDialog1.DefaultExt = "reg"
                    Me.SaveFileDialog1.Filter = "SMUSI-REG-Dateien (*.reg)|*.reg"
                Case Dateiformate.DAT_SWMM_MASS, Dateiformate.DAT_SWMM_TIME
                    Me.SaveFileDialog1.DefaultExt = "dat"
                    Me.SaveFileDialog1.Filter = "SWMM-DAT-Dateien (*.dat)|*.dat"
                Case Dateiformate.TXT
                    Me.SaveFileDialog1.DefaultExt = "txt"
                    Me.SaveFileDialog1.Filter = "SWMM-Interface-Dateien (*.txt)|*.txt"

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
                    Case Dateiformate.REG_HYSTEM
                        For Each item As Object In ExportDiag.ListBox_Zeitreihen.SelectedItems
                            Reihe = CType(item, Zeitreihe)
                            Call HystemExtran_REG.Write_File(Reihe, Me.SaveFileDialog1.FileName)
                        Next
                    Case Dateiformate.REG_SMUSI
                        For Each item As Object In ExportDiag.ListBox_Zeitreihen.SelectedItems
                            Reihe = CType(item, Zeitreihe)
                            Call REG_SMUSI.Write_File(Reihe, Me.SaveFileDialog1.FileName)
                        Next
                    Case Dateiformate.DAT_SWMM_MASS
                        For Each item As Object In ExportDiag.ListBox_Zeitreihen.SelectedItems
                            Reihe = CType(item, Zeitreihe)
                            Call SWMM_DAT_MASS.Write_File(Reihe, Me.SaveFileDialog1.FileName, 5) 'Zeitschritt ist noch nicht dynamisch definiert
                        Next
                    Case Dateiformate.DAT_SWMM_TIME
                        For Each item As Object In ExportDiag.ListBox_Zeitreihen.SelectedItems
                            Reihe = CType(item, Zeitreihe)
                            Call SWMM_DAT_TIME.Write_File(Reihe, Me.SaveFileDialog1.FileName, 5) 'Zeitschritt ist noch nicht dynamisch definiert
                        Next
                    Case Dateiformate.TXT
                        ReDim MultiReihe(ExportDiag.ListBox_Zeitreihen.SelectedItems.Count - 1)
                        iReihe = 0
                        For Each item As Object In ExportDiag.ListBox_Zeitreihen.SelectedItems
                            MultiReihe(iReihe) = CType(item, Zeitreihe)
                            iReihe = iReihe + 1
                        Next
                        Call SWMM_TXT.Write_File(MultiReihe, Me.SaveFileDialog1.FileName)
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
                    'TODO: Ergebniswerte anzeigen?
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
    Private Sub Drucken_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Drucken.Click
        Call Me.TChart1.Printer.Preview()
    End Sub

    'Kopieren (als PNG)
    '******************
    Private Sub Kopieren_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Kopieren.Click
        Call Me.TChart1.Export.Image.PNG.CopyToClipboard()
    End Sub

    'Log anzeigen
    '************
    Private Sub ShowLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel_Log.Click

        'LogWindow anzeigen
        Call Me.myLogWindow.Show()
        Me.myLogWindow.WindowState = FormWindowState.Normal
        Call Me.myLogWindow.BringToFront()

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
    ''' Zoom button clicked
    ''' </summary>
    Private Sub ToolStripButton_Zoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Zoom.Click
        If Me.ToolStripButton_Zoom.Checked Then
            'enable zooming
            Me.TChart1.Cursor = Cursors.Cross
            Me.TChart1.Zoom.Allow = True
            Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
            Me.TChart1.Zoom.History = True 'number of steps is 8, don't know how to change that
            Me.TChart1.Zoom.MouseButton = Windows.Forms.MouseButtons.Left
            'uncheck the other buttons
            Me.ToolStripButton_NormalMode.Checked = False
            Me.ToolStripButton_Pan.Checked = False
            'disable panning
            Me.TChart1.Panning.Allow = False
        Else
            'revert to normal mode
            Me.TChart1.Cursor = Cursors.Default
            Me.TChart1.Zoom.Allow = True
            Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
            Me.TChart1.Zoom.MouseButton = Windows.Forms.MouseButtons.Left
            Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal
            Me.TChart1.Panning.MouseButton = Windows.Forms.MouseButtons.Right
        End If
    End Sub

    ''' <summary>
    ''' Pan button clicked
    ''' </summary>
    Private Sub ToolStripButton_Pan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Pan.Click
        If Me.ToolStripButton_Pan.Checked Then
            'enable panning
            Me.TChart1.Cursor = Cursors.SizeWE
            Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal
            Me.TChart1.Panning.MouseButton = Windows.Forms.MouseButtons.Left
            'uncheck the other buttons
            Me.ToolStripButton_NormalMode.Checked = False
            Me.ToolStripButton_Zoom.Checked = False
            'disable zooming
            Me.TChart1.Zoom.Allow = False
        Else
            'revert to normal mode
            Me.TChart1.Cursor = Cursors.Default
            Me.TChart1.Zoom.Allow = True
            Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
            Me.TChart1.Zoom.MouseButton = Windows.Forms.MouseButtons.Left
            Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal
            Me.TChart1.Panning.MouseButton = Windows.Forms.MouseButtons.Right
        End If
    End Sub

    ''' <summary>
    ''' Normal mode button clicked
    ''' </summary>
    Private Sub ToolStripButton_Normal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_NormalMode.Click
        If Me.ToolStripButton_NormalMode.Checked Then
            'revert to normal mode
            Me.TChart1.Cursor = Cursors.Default
            Me.TChart1.Zoom.Allow = True
            Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
            Me.TChart1.Zoom.MouseButton = Windows.Forms.MouseButtons.Left
            Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal
            Me.TChart1.Panning.MouseButton = Windows.Forms.MouseButtons.Right
            'uncheck the other buttons
            Me.ToolStripButton_Zoom.Checked = False
            Me.ToolStripButton_Pan.Checked = False
        Else
            If Not Me.ToolStripButton_Zoom.Checked And Not Me.ToolStripButton_Pan.Checked Then
                'keep the button checked as no other mode is activated
                Me.ToolStripButton_NormalMode.Checked = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Zoom previous button clicked
    ''' </summary>
    Private Sub ToolStripButton_ZoomPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ZoomPrevious.Click
        Me.TChart1.Zoom.Undo()
    End Sub

    ''' <summary>
    ''' Zoom All button clicked
    ''' </summary>
    Private Sub ToolStripButton_ZoomAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ZoomAll.Click
        Me.selectionMade = False
        Call Me.UpdateCharts()
    End Sub

    ''' <summary>
    ''' Löscht alle vorhandenen Serien und liest alle importierten Zeitreihen neu ein
    ''' </summary>
    Private Sub RefreshFromFile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_Refresh.Click

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
                    'Jede Zeitreihe importieren
                    Call Me.Import_Series(zre)
                Next
            Next

        End If

    End Sub

    ''' <summary>
    ''' Info Click
    ''' </summary>
    Private Sub MenuDropdown_Hilfe(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton_Help.ButtonClick
        'keine Funktionalität, nur Dropdown
        Me.ToolStripSplitButton_Help.ShowDropDown()
    End Sub

    ''' <summary>
    ''' About Click
    ''' </summary>
    Private Sub About(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim about As New AboutBox()
        Call about.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Hilfe Click (URL zum Wiki)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Hilfe(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HilfeToolStripMenuItem.Click
        Process.Start(HelpURL)
    End Sub

#End Region 'Toolbar

#Region "Navigation"

    ''' <summary>
    ''' Update the navigation based on the currently displayed timespan of the main chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub updateNavigation()

        Dim xMin, xMax As DateTime

        Me.isInitializing = True 'need this to prevent a feedback loop

        'read dates from chart
        xMin = Date.FromOADate(Me.TChart1.Axes.Bottom.Minimum)
        xMax = Date.FromOADate(Me.TChart1.Axes.Bottom.Maximum)

        'set DateTimePickers
        Me.DateTimePicker_NavStart.Value = xMin
        Me.DateTimePicker_NavEnd.Value = xMax

        'update the display range
        Call Me.updateDisplayRange()

        Me.isInitializing = False
    End Sub

    ''' <summary>
    ''' Called when the value of one of the navigation DateTimePickers is changed. Ensures that validation is successful before continuing.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub navigationChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_NavStart.ValueChanged, DateTimePicker_NavEnd.ValueChanged
        If Not Me.navigationValidating(sender, New System.ComponentModel.CancelEventArgs()) Then
            'reset navigation to correspond to chart
            Call Me.updateNavigation()
        Else
            'validation was successful
            Call Me.navigationValidated(sender, e)
        End If
    End Sub

    ''' <summary>
    ''' Validates the navigation DateTimePickers. Checks that start is before end.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <returns>True if validation is successful</returns>
    ''' <remarks></remarks>
    Private Function navigationValidating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) As Boolean Handles DateTimePicker_NavStart.Validating, DateTimePicker_NavEnd.Validating
        If Me.DateTimePicker_NavStart.Value >= Me.DateTimePicker_NavEnd.Value Then
            If CType(sender, DateTimePicker).Name = "DateTimePicker_NavStart" Then
                'if the start date was set to a value after the end date,
                'move the end date using the currently displayed timespan
                Dim displayrange As TimeSpan
                displayrange = DateTime.FromOADate(Me.TChart1.Axes.Bottom.Maximum) - DateTime.FromOADate(Me.TChart1.Axes.Bottom.Minimum)
                DateTimePicker_NavEnd.Value = DateTimePicker_NavStart.Value + displayrange
            Else
                'setting the end date to a value before the start date is not allowed
                e.Cancel = True
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Is called upon successful validation of the navigation DateTimePickers. Updates the chart.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub navigationValidated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_NavStart.Validated, DateTimePicker_NavEnd.Validated
        If Not Me.isInitializing Then
            'Adjust the display range of the main chart
            Me.TChart1.Axes.Bottom.Minimum = Me.DateTimePicker_NavStart.Value.ToOADate()
            Me.TChart1.Axes.Bottom.Maximum = Me.DateTimePicker_NavEnd.Value.ToOADate()
            'Update everything
            Call Me.updateColorband()
            Call Me.updateDisplayRange()
            Me.selectionMade = True
        End If
    End Sub

    ''' <summary>
    ''' The Display range has been changed - update the chart accordingly
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayRangeChanged() Handles ComboBox_DisplayRangeUnit.SelectedIndexChanged, NumericUpDown_DisplayRangeMultiplier.ValueChanged

        Dim xMin, xMax As DateTime
        Dim multiplier As Integer

        'get min x axis from chart
        xMin = Date.FromOADate(Me.TChart1.Axes.Bottom.Minimum)

        'Calculate new max value for x axis
        multiplier = NumericUpDown_DisplayRangeMultiplier.Value
        Select Case ComboBox_DisplayRangeUnit.SelectedItem
            Case "Centuries"
                xMax = xMin.AddYears(multiplier * 100)
            Case "Decades"
                xMax = xMin.AddYears(multiplier * 10)
            Case "Years"
                xMax = xMin.AddYears(multiplier)
            Case "Months"
                xMax = xMin.AddMonths(multiplier)
            Case "Weeks"
                xMax = xMin.AddDays(multiplier * 7)
            Case "Days"
                xMax = xMin.AddDays(multiplier)
            Case "Hours"
                xMax = xMin.AddHours(multiplier)
            Case "Minutes"
                xMax = xMin.AddMinutes(multiplier)
            Case "Seconds"
                xMax = xMin.AddSeconds(multiplier)
            Case Else
                'do nothing and abort
                Exit Sub
        End Select

        'Set new max value for x axis
        Me.TChart1.Axes.Bottom.Maximum = xMax.ToOADate()

        'Update everything
        Call Me.updateNavigation()
        Call Me.updateColorband()

        Me.selectionMade = True

    End Sub

    ''' <summary>
    ''' Update the display range input fields to correspond to the chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub updateDisplayRange()

        Dim xMin, xMax As DateTime
        Dim multiplier As Integer

        xMin = Date.FromOADate(Me.TChart1.Axes.Bottom.Minimum)
        xMax = Date.FromOADate(Me.TChart1.Axes.Bottom.Maximum)

        'check whether the selected display range corresponds to the chart
        multiplier = Me.NumericUpDown_DisplayRangeMultiplier.Value
        Select Case ComboBox_DisplayRangeUnit.SelectedItem
            Case "Centuries"
                If xMin.AddYears(multiplier * 100) = xMax Then
                    Exit Sub
                End If
            Case "Decades"
                If xMin.AddYears(multiplier * 10) = xMax Then
                    Exit Sub
                End If
            Case "Years"
                If xMin.AddYears(multiplier) = xMax Then
                    Exit Sub
                End If
            Case "Months"
                If xMin.AddMonths(multiplier) = xMax Then
                    Exit Sub
                End If
            Case "Weeks"
                If xMin.AddDays(multiplier * 7) = xMax Then
                    Exit Sub
                End If
            Case "Days"
                If xMin.AddDays(multiplier) = xMax Then
                    Exit Sub
                End If
            Case "Hours"
                If xMin.AddHours(multiplier) = xMax Then
                    Exit Sub
                End If
            Case "Minutes"
                If xMin.AddMinutes(multiplier) = xMax Then
                    Exit Sub
                End If
            Case "Seconds"
                If xMin.AddSeconds(multiplier) = xMax Then
                    Exit Sub
                End If
            Case Else
                Exit Sub
        End Select

        'reset the display range input fields
        Me.NumericUpDown_DisplayRangeMultiplier.Value = 1
        Me.ComboBox_DisplayRangeUnit.SelectedItem = ""

    End Sub

    ''' <summary>
    ''' Navigate to the left/right
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_NavLeftRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_NavLeft.Click, Button_NavRight.Click

        Dim multiplier As Integer
        Dim xMinOld, xMinNew, xMaxOld, xMaxNew As DateTime

        'get the previous min and max dates
        xMinOld = Date.FromOADate(Me.TChart1.Axes.Bottom.Minimum)
        xMaxOld = Date.FromOADate(Me.TChart1.Axes.Bottom.Maximum)

        multiplier = Me.NumericUpDown_NavMultiplier.Value
        'when navigating to the left, negate the multiplier
        If CType(sender, Button).Name = "Button_NavLeft" Then
            multiplier *= -1
        End If

        Select Case Me.ComboBox_NavIncrement.SelectedItem
            Case "Centuries"
                xMinNew = xMinOld.AddYears(multiplier * 100)
                xMaxNew = xMaxOld.AddYears(multiplier * 100)
            Case "Decades"
                xMinNew = xMinOld.AddYears(multiplier * 10)
                xMaxNew = xMaxOld.AddYears(multiplier * 10)
            Case "Years"
                xMinNew = xMinOld.AddYears(multiplier)
                xMaxNew = xMaxOld.AddYears(multiplier)
            Case "Months"
                xMinNew = xMinOld.AddMonths(multiplier)
                xMaxNew = xMaxOld.AddMonths(multiplier)
            Case "Weeks"
                xMinNew = xMinOld.AddDays(multiplier * 7)
                xMaxNew = xMaxOld.AddDays(multiplier * 7)
            Case "Days"
                xMinNew = xMinOld.AddDays(multiplier)
                xMaxNew = xMaxOld.AddDays(multiplier)
            Case "Hours"
                xMinNew = xMinOld.AddHours(multiplier)
                xMaxNew = xMaxOld.AddHours(multiplier)
            Case "Minutes"
                xMinNew = xMinOld.AddMinutes(multiplier)
                xMaxNew = xMaxOld.AddMinutes(multiplier)
            Case "Seconds"
                xMinNew = xMinOld.AddSeconds(multiplier)
                xMaxNew = xMaxOld.AddSeconds(multiplier)
            Case Else
                xMinNew = xMinOld
                xMaxNew = xMaxOld
        End Select

        'update chart
        Me.TChart1.Axes.Bottom.Minimum = xMinNew.ToOADate()
        Me.TChart1.Axes.Bottom.Maximum = xMaxNew.ToOADate()

        'update everything else
        Call Me.updateNavigation()
        Call Me.updateColorband()

        Me.selectionMade = True

    End Sub

#End Region 'Navigation

#Region "Cursor"

    ''' <summary>
    ''' Mouse down event on TChart1: in normal mode, change the cursor to indicate zooming / panning
    ''' </summary>
    Private Sub TChart1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseDown
        If Me.ToolStripButton_NormalMode.Checked Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.TChart1.Cursor = Cursors.Cross
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
                Me.TChart1.Cursor = Cursors.SizeWE
            End If
        End If
    End Sub

    ''' <summary>
    ''' Mouse up event on TChart1: in normal mode, restore default cursor
    ''' </summary>
    Private Sub TChart1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseUp
        If Me.ToolStripButton_NormalMode.Checked Then
            Me.TChart1.Cursor = Cursors.Default
        End If
    End Sub

#End Region 'Cursor

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

    ''' <summary>
    ''' Lädt eine native Teechart-Datei (*.TEN)
    ''' </summary>
    ''' <param name="FileName">Pfad zur TEN-Datei</param>
    ''' <remarks></remarks>
    Private Sub Load_TEN(ByVal FileName As String)

        Dim result As DialogResult
        Dim i As Integer
        Dim reihe As Zeitreihe
        Dim XMin, XMax As Double

        Try

            'Log
            Call Log.AddLogEntry("Lade Datei '" & FileName & "' ...")

            'Bereits vorhandene Reihen merken
            Dim existingseries = New List(Of String)
            For Each zretitle As String In Me.Zeitreihen.Keys
                existingseries.Add(zretitle)
            Next

            'Zoom der X-Achse merken
            XMin = Me.TChart1.Axes.Bottom.Minimum
            XMax = Me.TChart1.Axes.Bottom.Maximum
            If (XMin <> XMax) Then
                Me.selectionMade = True
            Else
                Me.selectionMade = False
            End If

            'TEN-Datei importieren
            'Diagramme werden hiermit komplett ersetzt!
            Call TChart1.Import.Template.Load(FileName)
            Call TChart2.Import.Template.Load(FileName)

            'Übersichtsdiagramm wieder als solches formatieren
            'TODO: als Funktion auslagern und auch bei Init_Charts() verwenden
            Call Wave.formatChart(Me.TChart2.Chart)
            Me.TChart2.Panel.Brush.Color = Color.FromArgb(239, 239, 239)
            Me.TChart2.Walls.Back.Color = Color.FromArgb(239, 239, 239)
            Me.TChart2.Header.Visible = False
            Me.TChart2.Legend.Visible = False
            Me.TChart2.Axes.Left.Title.Visible = False
            Me.TChart2.Axes.Right.Title.Visible = False
            For i = 0 To Me.TChart2.Axes.Custom.Count - 1
                Me.TChart2.Axes.Custom(i).Title.Visible = False
            Next
            Me.TChart2.Axes.Bottom.Labels.DateTimeFormat = "dd.MM.yy"
            Me.TChart2.Axes.Bottom.Labels.Angle = 0
            Me.TChart2.Zoom.Allow = False
            Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

            'Abfrage für Reihenimport
            If (Me.TChart1.Series.Count() > 0) Then
                result = MsgBox("Zeitreihen auch importieren?", MsgBoxStyle.YesNo)

                Select Case result

                    Case Windows.Forms.DialogResult.Yes
                        'Reihen aus TEN-Datei sollen importiert werden

                        'Alle Reihen durchlaufen
                        For Each series As Steema.TeeChart.Styles.Series In TChart1.Series

                            'Nur Zeitreihen behandeln
                            If (series.GetHorizAxis.IsDateTime) Then

                                'Zeitreihe aus dem importierten Diagramm nach intern übertragen
                                Log.AddLogEntry("Importiere Zeitreihe '" & series.Title & "' aus TEN-Datei...")
                                reihe = New Zeitreihe(series.Title)
                                For i = 0 To series.Count - 1
                                    reihe.AddNode(Date.FromOADate(series.XValues(i)), series.YValues(i))
                                Next
                                Call Me.AddZeitreihe(reihe)

                            End If
                        Next


                    Case Windows.Forms.DialogResult.No
                        'Reihen aus TEN-Datei sollen nicht importiert werden

                        'Alle Reihen aus den Diagrammen löschen (wurden bei TEN-Import automatisch mit eingeladen)
                        Me.TChart1.Series.RemoveAllSeries()
                        Me.TChart2.Series.RemoveAllSeries()

                End Select

            End If

            'Achsen neu aus Diagramm einlesen
            Me.MyAxes1.Clear()
            Me.MyAxes2.Clear()

            'Linke Achse
            Me.MyAxes1.Add(Me.TChart1.Chart.Axes.Left.TitleOrName, Me.TChart1.Chart.Axes.Left)
            Me.MyAxes2.Add(Me.TChart2.Chart.Axes.Left.TitleOrName, Me.TChart2.Chart.Axes.Left)
            'Rechte Achse
            If (Me.TChart1.Chart.Axes.Right.Visible) Then
                Me.MyAxes1.Add(Me.TChart1.Chart.Axes.Right.TitleOrName, Me.TChart1.Chart.Axes.Right)
                Me.MyAxes2.Add(Me.TChart2.Chart.Axes.Right.TitleOrName, Me.TChart2.Chart.Axes.Right)
            End If
            'Custom Achsen
            For i = 0 To Me.TChart1.Chart.Axes.Custom.Count - 1
                Me.MyAxes1.Add(Me.TChart1.Chart.Axes.Custom(i).TitleOrName, Me.TChart1.Chart.Axes.Custom(i))
                Me.MyAxes2.Add(Me.TChart2.Chart.Axes.Custom(i).TitleOrName, Me.TChart2.Chart.Axes.Custom(i))
            Next

            'Die vor dem Laden bereits vorhandenen Zeitreihen wieder zu den Diagrammen hinzufügen (durch TEN-Import verloren)
            For Each title As String In existingseries
                Call Me.Display_Series(Me.Zeitreihen(title))
            Next

            'Vorherigen Zoom wiederherstellen
            If (Me.selectionMade) Then
                Me.TChart1.Axes.Bottom.Minimum = XMin
                Me.TChart1.Axes.Bottom.Maximum = XMax
            End If

            'ColorBand neu einrichten (durch TEN-Import verloren)
            Call Me.Init_ColorBand()

            'Charts aktualisieren
            Call Me.UpdateCharts()

            'Log
            Call Log.AddLogEntry("... Datei '" & FileName & "' erfolgreich geladen!")

        Catch ex As Exception
            MsgBox("Fehler beim Laden:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry("... Fehler beim Laden:" & eol & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Lädt ein TeeChart Theme (XML-Datei)
    ''' </summary>
    ''' <param name="FileName">Pfad zur XML-Datei</param>
    ''' <remarks></remarks>
    Private Sub Load_Theme(ByVal FileName As String)

        Try

            'Log
            Call Log.AddLogEntry("Lade Theme '" & FileName & "' ...")

            'Theme laden
            Call TChart1.Import.Theme.Load(FileName)

            'Log
            Call Log.AddLogEntry("... Theme '" & FileName & "' erfolgreich geladen!")

        Catch ex As Exception
            MsgBox("Fehler beim Laden:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry("... Fehler beim Laden:" & eol & ex.Message)
        End Try

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
                Call Me.Load_TEN(file)

            Case Dateifactory.FileExtnetCDF
                '.netCDF Datei

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
                            'Serie importieren
                            Call Me.Import_Series(Datei.Zeitreihen(i))
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

    ''' <summary>
    ''' Eine netCDF-Datei importieren
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <remarks>Sonderfall, weil komplexes Dateiformat</remarks>
    Private Sub Import_netCDF(ByVal file As String)

        'Dim dlg As ImportFromNetCDF
        'Dim ZR_netCDF As Zeitreihe_netCDF

        'Try
        '    'Log
        '    Call Log.AddLogEntry("Importiere Datei '" & file & "' ...")

        '    ZR_netCDF = New Zeitreihe_netCDF()

        '    dlg = New ImportFromNetCDF
        '    If (dlg.ShowDialog(Me) = DialogResult.OK) Then
        '        'Importiere
        '        'TODO
        '    End If

        '    'Log
        '    Call Log.AddLogEntry("... Datei '" & file & "' erfolgreich importiert!")


        '    ''Datei-Instanz erzeugen
        '    'ZR_netCDF = Dateifactory.getDateiInstanz(file)

        '    ''Einlesen
        '    'Call RVADatei.Read_File()


        '    ''Datei abspeichern
        '    'Me.ImportedFiles.Add(RVADatei)

        '    ''Chart vorbereiten
        '    'Call Me.PrepareChart_RVA()

        '    ''Serie zeichnen
        '    'Call Me.Display_RVA(RVADatei.RVAValues, True)

        'Catch ex As Exception
        '    MsgBox("Fehler beim Import:" & eol & ex.Message, MsgBoxStyle.Critical)
        '    Call Log.AddLogEntry("... Fehler beim Import:" & eol & ex.Message)
        'End Try

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
    ''' Import a time series
    ''' </summary>
    ''' <param name="zre">the time series</param>
    ''' <remarks>saves and then display the time series</remarks>
    Public Sub Import_Series(ByVal zre As Zeitreihe, Optional ByVal Display As Boolean = True)
        'Serie abspeichen
        Me.AddZeitreihe(zre)
        If Display Then
            'Serie in Diagrammen anzeigen
            Call Me.Display_Series(zre)
        End If
    End Sub

    ''' <summary>
    ''' Eine Zeitreihe in den Diagrammen anzeigen
    ''' </summary>
    ''' <param name="zre">Die anzuzeigende Zeitreihe</param>
    Private Sub Display_Series(ByVal zre As Zeitreihe)

        Dim AxisNo As Integer

        'NaN und Infinity-Stützstellen entfernen
        zre = zre.getCleanZRE()

        'Serie zu Diagramm hinzufügen

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

        'Y-Achsenzuordnung
        AxisNo = getAxisNo(zre.Einheit)

        'Reihe der Y-Achse zuordnen
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
                Line2.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom
                Line1.CustomVertAxis = Me.MyAxes1(zre.Einheit)
                Line2.CustomVertAxis = Me.MyAxes2(zre.Einheit)
        End Select

        'Charts aktualisieren
        Call Me.UpdateCharts()

    End Sub

    ''' <summary>
    ''' Löscht eine Zeitreihe
    ''' </summary>
    ''' <param name="title">Titel der Zeitreihe</param>
    ''' <remarks>Annahme, dass alle Zeitreihentitel eindeutig sind</remarks>
    Private Sub DeleteZeitreihe(ByVal title As String)

        'Aus Diagramm entfernen
        For i As Integer = Me.TChart1.Series.Count - 1 To 0 Step -1
            If (Me.TChart1.Series.Item(i).Title = title) Then
                Me.TChart1.Series.RemoveAt(i)
                Me.TChart1.Refresh()
                Exit For
            End If
        Next

        'Aus Übersicht entfernen
        For i As Integer = Me.TChart2.Series.Count - 1 To 0 Step -1
            If (Me.TChart2.Series.Item(i).Title = title) Then
                Me.TChart2.Series.RemoveAt(i)
                Me.TChart2.Refresh()
                Exit For
            End If
        Next

        'Intern entfernen
        Me.Zeitreihen.Remove(title)

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

        'Achsen
        chart.Axes.Bottom.Automatic = True

    End Sub

#End Region 'Funktionalität

End Class
