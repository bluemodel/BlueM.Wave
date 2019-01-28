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

    ''' <summary>
    ''' The LogWindow shared among all Wave instances
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared MyLogWindow As LogWindow

    'Log
    Private WithEvents myLog As Log

    'Eigenschaften
    '#############

    ''' <summary>
    ''' Flag for preventing unintended feedback loops in the UI. Default is False, can be temporarily set to True.
    ''' </summary>
    ''' <remarks></remarks>
    Private isInitializing As Boolean

    'Collection of imported files and the selected series of each file
    Private ImportedFiles As Dictionary(Of String, List(Of String)) '{filename: [seriesName, ...], ...}

    'Interne Zeitreihen-Collection
    Private Zeitreihen As Dictionary(Of String, TimeSeries)

    'Dateifilter
    Private Const FileFilter_TEN As String = "TeeChart files (*.ten)|*.ten"
    Private Const FileFilter_XML As String = "Theme files (*.xml)|*.xml"

    'Chart-Zeugs
    Private WithEvents colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private selectionMade As Boolean 'Flag zeigt an, ob bereits ein Auswahlbereich ausgewählt wurde
    Private MyAxes1, MyAxes2 As Dictionary(Of String, Steema.TeeChart.Axis)
    Private WithEvents ChartListBox1 As Steema.TeeChart.ChartListBox

    'Cursors
    Friend cursor_pan As Cursor
    Friend cursor_pan_hold As Cursor
    Friend cursor_zoom As Cursor

    Private Const HelpURL As String = "http://wiki.bluemodel.org/index.php/Wave"

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
        Me.ImportedFiles = New Dictionary(Of String, List(Of String))()
        Me.Zeitreihen = New Dictionary(Of String, TimeSeries)()
        Me.MyAxes1 = New Dictionary(Of String, Steema.TeeChart.Axis)
        Me.MyAxes2 = New Dictionary(Of String, Steema.TeeChart.Axis)

        'Charts einrichten
        '-----------------
        Me.ChartListBox1 = New Steema.TeeChart.ChartListBox()
        Call Me.Init_Charts()

        'Logfenster nur beim ersten Mal instanzieren
        '-------------------------------------------
        If (IsNothing(MyLogWindow)) Then
            MyLogWindow = New LogWindow()
        End If

        'Log (Singleton) Instanz holen
        Me.myLog = Log.getInstance()

        'Navigation initialisieren
        Me.ComboBox_NavIncrement.SelectedItem = "Days"

        'Instantiate cursors
        Me.cursor_pan = New Cursor(Me.GetType(), "cursor_pan.cur")
        Me.cursor_pan_hold = New Cursor(Me.GetType(), "cursor_pan_hold.cur")
        Me.cursor_zoom = New Cursor(Me.GetType(), "cursor_zoom.cur")

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
        Call Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Stores information about an imported file and adds the filename to the recently used file menu
    ''' </summary>
    ''' <param name="fileObj"></param>
    ''' <remarks></remarks>
    Private Sub storeFileInfo(ByRef fileObj As FileFormatBase)

        'store filename and selected series names
        If Me.ImportedFiles.ContainsKey(fileObj.File) Then
            Me.ImportedFiles(fileObj.File).AddRange(fileObj.SelectedSeries)
        Else
            Me.ImportedFiles.Add(fileObj.File, fileObj.SelectedSeries)
        End If

        'add filename to Recently Used Files menu
        'remove if already present
        Dim i As Integer = 0
        For Each _item As ToolStripItem In Me.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems
            If _item.Text = fileObj.File Then
                Me.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.RemoveAt(i)
                Exit For
            End If
            i += 1
        Next
        'add to top of list
        Dim item As New ToolStripMenuItem(fileObj.File)
        Me.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.Insert(0, item)

    End Sub

    ''' <summary>
    ''' Recently Used File menu item clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub openRecentlyUsedFile(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles ToolStripMenuItem_RecentlyUsedFiles.DropDownItemClicked
        Dim filename As String
        filename = e.ClickedItem.Text
        Call Me.Import_File(filename)
    End Sub

    ''' <summary>
    ''' Process Drag and Drop of files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Wave_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then

            Dim files() As String

            'get array of file paths
            files = e.Data.GetData(DataFormats.FileDrop)

            'Invoke the file import process asynchronously
            Me.BeginInvoke(New ImportDelegate(AddressOf Import_Files), New Object() {files})
        End If

    End Sub

    ''' <summary>
    ''' Used for processing drag and drop of files asynchronously
    ''' </summary>
    ''' <param name="files">array of file paths</param>
    ''' <remarks></remarks>
    Delegate Sub ImportDelegate(ByVal files() As String)

    ''' <summary>
    ''' Processes the Wave.DragEnter event. Sets DragEventArgs.Effect to Copy if the dragged object consist of files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Wave_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ''' <summary>
    ''' Process KeyDown events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Wave_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Dim dlgres As DialogResult

        Try
            If e.Control And e.KeyCode = Keys.V Then
                'Ctrl+V pressed
                If Clipboard.ContainsText(TextDataFormat.Text) Then

                    Dim clipboardtext As String
                    clipboardtext = Clipboard.GetText(TextDataFormat.Text)

                    If clipboardtext.Contains("SydroTyp=SydroErgZre") Or _
                       clipboardtext.Contains("SydroTyp=SydroBinZre") Then
                        'it's a clipboard entry from TALSIM!

                        'ask the user for confirmation
                        dlgres = MessageBox.Show("TALSIM clipboard content detected!" & eol & "Load series in Wave?", "Load from clipboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If Not dlgres = Windows.Forms.DialogResult.Yes Then
                            Exit Sub
                        End If
                        Me.Cursor = Cursors.WaitCursor
                        Call Me.loadFromClipboard_TALSIM(clipboardtext)
                        Me.Cursor = Cursors.Default
                    End If
                End If
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Log.AddLogEntry("ERROR: " & ex.Message)
            MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
        End Try
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
        Me.TChart2.Panel.Brush.Color = Color.FromArgb(239, 239, 239)
        Me.TChart2.Walls.Back.Color = Color.FromArgb(239, 239, 239)
        Me.TChart2.Header.Visible = False
        Me.TChart2.Legend.Visible = False

        'Übersicht darf nicht gescrolled oder gezoomt werden
        Me.TChart2.Zoom.Allow = False
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Hauptdiagramm darf nur horizontal gescrolled oder gezoomt werden
        Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
        Me.TChart1.Zoom.History = True
        Me.TChart1.Zoom.Animated = True
        Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal

        'Achsen
        Me.TChart1.Axes.Bottom.Automatic = False
        Me.TChart1.Axes.Bottom.Labels.Angle = 90
        Me.TChart1.Axes.Bottom.Labels.DateTimeFormat = "dd.MM.yy HH:mm"
        Me.TChart1.Axes.Right.Title.Angle = 90
        Me.TChart2.Axes.Bottom.Automatic = False

        'Assign ChartListBox to TChart1
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
            For Each zre As TimeSeries In Me.Zeitreihen.Values
                If (zre.StartDate < Xmin) Then Xmin = zre.StartDate
                If (zre.EndDate > Xmax) Then Xmax = zre.EndDate
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
            'save the current zoom snapshot
            Call Me.saveZoomSnapshot()
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
        Dim title_removed As String
        Dim s As Steema.TeeChart.Styles.Series

        title_removed = ""

        'Alle internen Zeitreihen durchlaufen und prüfen, ob es sie noch gibt
        For Each zre As TimeSeries In Me.Zeitreihen.Values
            found = False
            For Each s In Me.ChartListBox1.Items
                If (s.Title = zre.Title) Then
                    found = True 'diese Serie gibt es noch
                    Exit For
                End If
            Next
            If (Not found) Then
                title_removed = zre.Title 'diese Serie gibt es nicht mehr
                Exit For
            End If
        Next

        If (title_removed <> "") Then
            'Aus der internen Collection löschen
            Me.Zeitreihen.Remove(title_removed)

            'Aus der Übersicht löschen
            For i As Integer = Me.TChart2.Series.Count - 1 To 0 Step -1
                If (Me.TChart2.Series.Item(i).Title = title_removed) Then
                    Me.TChart2.Series.RemoveAt(i)
                    Me.TChart2.Refresh()
                    Exit For
                End If
            Next

            'Aus der Liste der importierten Dateien löschen
            For Each file As String In Me.ImportedFiles.Keys
                If Me.ImportedFiles(file).Contains(title_removed) Then
                    Me.ImportedFiles(file).Remove(title_removed)
                    'Ganze Datei vergessen, falls es die einzige Serie war
                    If Me.ImportedFiles(file).Count = 0 Then
                        Me.ImportedFiles.Remove(file)
                        Exit For
                    End If
                End If
            Next
        End If

    End Sub

    ''' <summary>
    ''' Add the current zoom to the zoom history
    ''' </summary>
    Private Sub saveZoomSnapshot()
        Dim snapshot As New Steema.TeeChart.ZoomSnapshot()
        snapshot.AxesMinMax = New Double() {Me.TChart1.Axes.Left.Minimum, _
                                            Me.TChart1.Axes.Left.Maximum, _
                                            Me.TChart1.Axes.Top.Minimum, _
                                            Me.TChart1.Axes.Top.Maximum, _
                                            Me.TChart1.Axes.Right.Minimum, _
                                            Me.TChart1.Axes.Right.Maximum, _
                                            Me.TChart1.Axes.Bottom.Minimum, _
                                            Me.TChart1.Axes.Bottom.Maximum}
        Me.TChart1.Zoom.HistorySteps.Add(snapshot)
        'TODO: perhaps remove some HistorySteps if there are too many?
    End Sub

#End Region 'Chart behavior'

#Region "UI"

#Region "Toolbar"

    'Neu
    '***
    Private Sub Neu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_New.Click

        Dim res As MsgBoxResult

        'Warnen, wenn bereits Serien vorhanden
        '-------------------------------------
        If (Me.TChart1.Series.Count() > 0) Then
            res = MsgBox("All existing series will be deleted!" & eol & "Continue?", MsgBoxStyle.OkCancel)
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
        Call Wave.MyLogWindow.Hide()

    End Sub

    'Serie(n) importieren
    '********************
    Private Sub Importieren_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ImportSeries.Click
        Me.OpenFileDialog1.Title = "Import time series"
        Me.OpenFileDialog1.Filter = FileFormatBase.FileFilter
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Import_File(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'TEN-Datei laden
    '****************
    Private Sub TENLaden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_LoadTEN.Click
        Me.OpenFileDialog1.Title = "Load TEN file"
        Me.OpenFileDialog1.Filter = FileFilter_TEN
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Load_TEN(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'Theme laden
    '***********
    Private Sub ThemeLaden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_LoadTheme.Click
        Me.OpenFileDialog1.Title = "Load theme"
        Me.OpenFileDialog1.Filter = FileFilter_XML
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Load_Theme(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    ''' <summary>
    ''' SaveProjectFile button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveProjectFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_SaveProjectFile.Click

        Dim dlgres As DialogResult
        Dim projectfile As String

        'Prepare SaveFileDialog
        Me.SaveFileDialog1.Title = "Save project file"
        Me.SaveFileDialog1.Filter = "Wave project files (*.wvp)|*wvp"
        Me.SaveFileDialog1.DefaultExt = "wvp"
        Me.SaveFileDialog1.OverwritePrompt = True

        'Show dialog
        dlgres = Me.SaveFileDialog1.ShowDialog()

        If dlgres = Windows.Forms.DialogResult.OK Then

            projectfile = Me.SaveFileDialog1.FileName

            'write the project file
            Dim fs As New FileStream(projectfile, FileMode.Create, FileAccess.Write)
            Dim strwrite As New StreamWriter(fs, System.Text.Encoding.GetEncoding("iso8859-1"))

            strwrite.WriteLine("# Wave project file")

            For Each filename As String In Me.ImportedFiles.Keys
                'TODO: write relative paths to the project file?
                strwrite.WriteLine("file=" & filename)
                For Each series As String In Me.ImportedFiles(filename)
                    'TODO: if a series was renamed, write the new title to the project file
                    strwrite.WriteLine("    series=" & series)
                Next
            Next

            strwrite.Close()
            fs.Close()

            Log.AddLogEntry("Wave project file " & projectfile & " saved.")

        End If

    End Sub

    'Teechart Export
    '***************
    Private Sub SaveChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_SaveChart.Click
        Call Me.TChart1.Export.ShowExportDialog()
    End Sub

    'Zeitreihen Export
    '*****************
    Private Sub ExportZeitreihe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ExportSeries.Click
        Call ExportZeitreihe()
    End Sub

    'Serie eingeben
    '**************
    Private Sub Eingeben_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_EnterSeries.Click
        Dim SeriesEditor As New SeriesEditorDialog()
        If (SeriesEditor.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Import_Series(SeriesEditor.Zeitreihe)
        End If
    End Sub

    ''' <summary>
    ''' Rename series button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Rename_Series_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_RenameSeries.Click

        'abort if no series loaded
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("No time series available for renaming!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Dim zre As TimeSeries
        Dim zres As New Dictionary(Of String, TimeSeries)
        Dim titles As New List(Of String)
        Dim title_old, title_new As String
        Dim renameDlg As RenameSeriesDialog
        Dim result As DialogResult

        'get titles for the dialog
        For Each title As String In Me.Zeitreihen.Keys
            titles.Add(title)
        Next
        'show the dialog
        renameDlg = New RenameSeriesDialog(titles)
        result = renameDlg.ShowDialog()
        'process results
        If result = Windows.Forms.DialogResult.OK Then
            'check for changes
            For Each kvp As KeyValuePair(Of String, String) In renameDlg.titles
                title_old = kvp.Key
                title_new = kvp.Value
                If title_new <> title_old Then
                    'make a copy of the series with the new title
                    zre = Me.Zeitreihen(title_old)
                    zre.Title = title_new
                    zres.Add(title_new, zre)
                    'update title in charts
                    For Each series As Steema.TeeChart.Styles.Series In Me.TChart1.Series
                        If series.Title = title_old Then
                            series.Title = title_new
                        End If
                    Next
                    For Each series As Steema.TeeChart.Styles.Series In Me.TChart2.Series
                        If series.Title = title_old Then
                            series.Title = title_new
                        End If
                    Next
                Else
                    'keep the series with title unchanged
                    zres.Add(title_old, Me.Zeitreihen(title_old))
                End If
            Next
            'update zre dict
            Me.Zeitreihen.Clear()
            Me.Zeitreihen = zres
        End If
    End Sub

    'Zeitreihe zuschneiden
    '*********************
    Private Sub Zuschneiden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Cut.Click

        Dim title As String
        Dim titles As List(Of String)

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("No time series available for cutting!", MsgBoxStyle.Exclamation, "Wave")
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
            For Each zre As TimeSeries In cutter.zreCut.Values
                Me.Import_Series(zre)
            Next

        End If

    End Sub

    ''' <summary>
    ''' Merge time series button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_Merge_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles ToolStripButton_Merge.Click

        Dim dlg As MergeSeriesDialog
        Dim dlgResult As DialogResult
        Dim seriesList As List(Of String)
        Dim seriesMerged, seriesToMerge As TimeSeries
        Dim mergedSeriesTitle As String

        'Abort if no series are loaded
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("No time series available for merging!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try

            dlg = New MergeSeriesDialog(Me.Zeitreihen)
            dlgResult = dlg.ShowDialog()

            Me.Cursor = Cursors.WaitCursor

            If dlgResult = Windows.Forms.DialogResult.OK Then

                seriesList = dlg.selectedSeries
                mergedSeriesTitle = dlg.mergedSeriesTitle

                'Clone the series with the highest priority
                seriesMerged = Me.Zeitreihen(seriesList(0)).Clone

                'Append the remaining series in order
                For i As Integer = 1 To seriesList.Count - 1
                    seriesToMerge = Me.Zeitreihen(seriesList(i))
                    seriesMerged.Append(seriesToMerge)
                Next

                'Assign title
                seriesMerged.Title = mergedSeriesTitle

                Log.AddLogEntry("Series successfully merged!")

                Me.Import_Series(seriesMerged)

            End If

        Catch ex As Exception
            Log.AddLogEntry("Error during merge: " & ex.Message)
            MsgBox("Error during merge: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try

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

        Dim exportDlg As ExportDiag
        Dim dlgResult As DialogResult
        Dim filename As String
        Dim zres As List(Of TimeSeries)

        'Abort if no time series loaded
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("No time series available for export!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        'Show Export dialog
        exportDlg = New ExportDiag(Me.Zeitreihen)
        dlgResult = exportDlg.ShowDialog()

        If dlgResult <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        'get selected series
        zres = New List(Of TimeSeries)
        For Each item As Object In exportDlg.ListBox_Series.SelectedItems
            zres.Add(CType(item, TimeSeries))
        Next

        'process metadata
        'TODO: assumes a single timeseries is being exported
        Dim keys As List(Of String)
        Dim metadata_old As Dictionary(Of String, String)
        Select Case exportDlg.ComboBox_Format.SelectedItem
            Case FileFormatBase.FileFormats.UVF
                keys = UVF.MetadataKeys
            Case FileFormatBase.FileFormats.ZRXP
                keys = ZRXP.MetadataKeys
            Case Else
                keys = FileFormatBase.MetadataKeys
        End Select
        If keys.Count > 0 Then
            'create a copy of the existing metadata
            metadata_old = New Dictionary(Of String, String)
            For Each kvp As KeyValuePair(Of String, String) In zres(0).Metadata
                metadata_old.Add(kvp.Key, kvp.Value)
            Next
            'assign new metadata keys to series
            zres(0).Metadata.Clear()
            For Each key As String In keys
                If metadata_old.ContainsKey(key) Then
                    'copy old metadata value with the same key
                    zres(0).Metadata.Add(key, metadata_old(key))
                Else
                    'add a new key with an empty value
                    zres(0).Metadata.Add(key, "")
                End If
            Next
            'set default metadata
            Select Case exportDlg.ComboBox_Format.SelectedItem
                Case FileFormatBase.FileFormats.UVF
                    UVF.setDefaultMetadata(zres(0))
                Case FileFormatBase.FileFormats.ZRXP
                    ZRXP.setDefaultMetadata(zres(0))
                Case Else
                    FileFormatBase.setDefaultMetadata(zres(0))
            End Select
            'show dialog for editing metadata
            Dim dlg As New MetadataDialog(zres(0).Metadata)
            dlgResult = dlg.ShowDialog()
            If Not dlgResult = Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            'update metadata in series
            zres(0).Metadata = dlg.Metadata
        End If

        'Prepare Save dialog
        Me.SaveFileDialog1.Title = "Save as..."
        Me.SaveFileDialog1.AddExtension = True
        Me.SaveFileDialog1.OverwritePrompt = True
        Select Case exportDlg.ComboBox_Format.SelectedItem
            Case FileFormatBase.FileFormats.ASC
                Me.SaveFileDialog1.DefaultExt = "asc"
                Me.SaveFileDialog1.Filter = "ASC files (*.asc)|*.asc"
            Case FileFormatBase.FileFormats.BIN
                Me.SaveFileDialog1.DefaultExt = "bin"
                Me.SaveFileDialog1.Filter = "SYDRO binary files (*.bin)|*.bin"
            Case FileFormatBase.FileFormats.CSV
                Me.SaveFileDialog1.DefaultExt = "csv"
                Me.SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv"
            Case FileFormatBase.FileFormats.WEL
                Me.SaveFileDialog1.DefaultExt = "wel"
                Me.SaveFileDialog1.Filter = "WEL files (*.wel)|*.wel"
            Case FileFormatBase.FileFormats.ZRE
                Me.SaveFileDialog1.DefaultExt = "zre"
                Me.SaveFileDialog1.Filter = "ZRE files (*.zre)|*.zre"
            Case FileFormatBase.FileFormats.REG_HYSTEM
                Me.SaveFileDialog1.DefaultExt = "reg"
                Me.SaveFileDialog1.Filter = "HYSTEM REG files (*.reg)|*.reg"
            Case FileFormatBase.FileFormats.REG_SMUSI
                Me.SaveFileDialog1.DefaultExt = "reg"
                Me.SaveFileDialog1.Filter = "SMUSI REG files (*.reg)|*.reg"
            Case FileFormatBase.FileFormats.DAT_SWMM_MASS, FileFormatBase.FileFormats.DAT_SWMM_TIME
                Me.SaveFileDialog1.DefaultExt = "dat"
                Me.SaveFileDialog1.Filter = "SWMM DAT files (*.dat)|*.dat"
            Case FileFormatBase.FileFormats.TXT_SWMM
                Me.SaveFileDialog1.DefaultExt = "txt"
                Me.SaveFileDialog1.Filter = "SWMM Interface files (*.txt)|*.txt"
            Case FileFormatBase.FileFormats.UVF
                Me.SaveFileDialog1.DefaultExt = "uvf"
                Me.SaveFileDialog1.Filter = "UVF files (*.uvf)|*.uvf"
        End Select
        Me.SaveFileDialog1.Filter &= "|All files (*.*)|*.*"
        Me.SaveFileDialog1.FilterIndex = 1

        'Show Save dialog
        dlgResult = Me.SaveFileDialog1.ShowDialog()
        If dlgResult <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        filename = Me.SaveFileDialog1.FileName

        'Export series
        Log.AddLogEntry("Exporting time series to file " & Me.SaveFileDialog1.FileName & "...")

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try

            Select Case exportDlg.ComboBox_Format.SelectedItem

                Case FileFormatBase.FileFormats.ZRE
                    Call ZRE.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.REG_HYSTEM
                    Call HystemExtran_REG.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.REG_SMUSI
                    Call REG_SMUSI.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.DAT_SWMM_MASS
                    Call SWMM_DAT_MASS.Write_File(zres(0), filename, 5) 'TODO: Zeitschritt ist noch nicht dynamisch definiert

                Case FileFormatBase.FileFormats.DAT_SWMM_TIME
                    Call SWMM_DAT_TIME.Write_File(zres(0), filename, 5) 'TODO: Zeitschritt ist noch nicht dynamisch definiert

                Case FileFormatBase.FileFormats.TXT_SWMM
                    Call SWMM_TXT.Write_File(zres, filename)

                Case FileFormatBase.FileFormats.CSV
                    Call CSV.Write_File(zres, filename)

                Case FileFormatBase.FileFormats.BIN
                    Call BIN.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.UVF
                    Call UVF.Write_File(zres(0), filename)

                Case Else
                    MsgBox("Not yet implemented!", MsgBoxStyle.Exclamation, "Wave")
            End Select

            MsgBox("Time series exported successfully!", MsgBoxStyle.Information, "Wave")
            Log.AddLogEntry("Time series exported successfully!")

        Catch ex As Exception
            Log.AddLogEntry("Error during export: " & ex.Message)
            MsgBox("Error during export: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    'Analysieren
    '***********
    Private Sub Analyse(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Analysis.Click

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("No time series available for analysis!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Dim oAnalysisDialog As New AnalysisDialog(Me.Zeitreihen)

        'Analysisdialog anzeigen
        '-----------------------
        If (oAnalysisDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            Try
                'Wait-Cursor
                Me.Cursor = Cursors.WaitCursor

                Call Log.AddLogEntry("Starting analysis " & oAnalysisDialog.selectedAnalysisFunction.ToString() & " ...")

                'Analyse instanzieren
                Dim oAnalysis As Analysis
                oAnalysis = AnalysisFactory.CreateAnalysis(oAnalysisDialog.selectedAnalysisFunction, oAnalysisDialog.selectedZeitreihen)

                Call Log.AddLogEntry("... executing analysis ...")

                'Analyse ausführen
                Call oAnalysis.ProcessAnalysis()

                Call Log.AddLogEntry("... preparing analysis result ...")

                'Ergebnisse aufbereiten
                Call oAnalysis.PrepareResults()

                Call Log.AddLogEntry("Analysis complete")

                'Default-Cursor
                Me.Cursor = Cursors.Default

                'Ergebnisse anzeigen:
                '--------------------
                'Ergebnisdiagramm anzeigen
                If (oAnalysis.hasResultChart) Then
                    Dim Wave2 As New Wave()
                    Wave2.Text = "Analysis result"
                    Wave2.Übersicht_Toggle(False)
                    Wave2.navigationToggle(False)
                    Wave2.TChart1.Chart = oAnalysis.getResultChart()
                    Call Wave2.Show()
                End If

                'Ergebnistext in Log schreiben und anzeigen
                If (oAnalysis.hasResultText) Then
                    Call Log.AddLogEntry(oAnalysis.getResultText)
                    Call Wave.MyLogWindow.Show()
                    Call Wave.MyLogWindow.BringToFront()
                End If

                'Ergebniswerte in Log schreiben
                If (oAnalysis.hasResultValues) Then
                    Call Log.AddLogEntry("Analysis results:")
                    For Each kvp As KeyValuePair(Of String, Double) In oAnalysis.getResultValues
                        Call Log.AddLogEntry(kvp.Key + ": " + Str(kvp.Value))
                    Next
                    Call Wave.MyLogWindow.Show()
                    Call Wave.MyLogWindow.BringToFront()
                End If

                'Display result series in main diagram
                If oAnalysis.hasResultSeries Then
                    For Each ts As TimeSeries In oAnalysis.getResultSeries.Values
                        Call Me.Import_Series(ts, True)
                    Next
                End If

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                'Logeintrag
                Call Log.AddLogEntry("Analysis failed:" & eol & ex.Message)
                'Alert
                MsgBox("Analysis failed:" & eol & ex.Message, MsgBoxStyle.Critical)
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Properties button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_Properties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Properties.Click
        Dim propDialog As New PropertiesDialog(Me.Zeitreihen)
        propDialog.Show()
    End Sub

    ''' <summary>
    ''' Change timestep button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_ChangeTimestep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ChangeTimestep.Click

        'BUG 704: Abort if running as 64bit
        If Helpers.is64BitProcess() Then
            MsgBox("Unable to load SydroZreNet.dll required for changing the timestep in a 64bit process, please use the x86-version of Wave.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Call Me.ChangeTimeStep()

    End Sub

    ''' <summary>
    ''' Show NaN values button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_ShowNaNValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ShowNaNValues.Click

        Dim processSeries As Boolean
        Dim bandStart, bandEnd As DateTime
        Dim band As Steema.TeeChart.Tools.ColorBand
        Dim isNaN, nanFound As Boolean

        If ToolStripButton_ShowNaNValues.Checked Then
            'Switch visualization of NaN values on
            'Show color bands for NaN values in the currently active series
            nanFound = False
            For Each ts As TimeSeries In Me.Zeitreihen.Values
                processSeries = False
                'check if time series is currently active
                For Each series As Steema.TeeChart.Styles.Series In Me.TChart1.Series
                    If series.Title = ts.Title Then
                        If series.Active Then
                            'process this series
                            processSeries = True
                        End If
                        Exit For
                    End If
                Next
                If processSeries Then
                    'find beginning and end of nan values
                    isNaN = False
                    For i As Integer = 0 To ts.Length - 1
                        If Not isNaN Then
                            If Double.IsNaN(ts.Values(i)) Then
                                'beginning of nan values
                                isNaN = True
                                nanFound = True
                                If i = 0 Then
                                    bandStart = ts.Dates(i)
                                Else
                                    bandStart = ts.Dates(i - 1)
                                End If
                            End If
                        Else
                            If Not Double.IsNaN(ts.Values(i)) Or i = ts.Length - 1 Then
                                'end of nan values
                                bandEnd = ts.Dates(i)
                                isNaN = False

                                'add a color band
                                band = New Steema.TeeChart.Tools.ColorBand()
                                Me.TChart1.Tools.Add(band)
                                band.Axis = Me.TChart1.Axes.Bottom
                                band.Start = bandStart.ToOADate()
                                band.End = bandEnd.ToOADate()
                                band.Pen.Color = Color.Red
                                band.Brush.Color = Color.Coral
                                band.Brush.Transparency = 50
                                band.ResizeEnd = False
                                band.ResizeStart = False
                                band.EndLinePen.Visible = False
                                band.StartLinePen.Visible = False
                            End If
                        End If
                    Next
                End If
            Next
            If Not nanFound Then
                MsgBox("No NaN values found in the currently active series!", MsgBoxStyle.Information)
                Me.ToolStripButton_ShowNaNValues.Checked = False
            End If
        Else
            'Switch visualization of NaN values off
            'Remove all tools of type ColorBand from TChart1
            'TODO: Any user-defined ColorBands unfortunately get removed as well!
            Dim colorbands As New List(Of Steema.TeeChart.Tools.ColorBand)
            For Each tool As Steema.TeeChart.Tools.Tool In Me.TChart1.Tools
                If tool.GetType Is GetType(Steema.TeeChart.Tools.ColorBand) Then
                    colorbands.Add(tool)
                End If
            Next
            For Each colorband As Steema.TeeChart.Tools.ColorBand In colorbands
                Me.TChart1.Tools.Remove(colorband)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Remove error values button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_ConvertErrorValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ConvertErrorValues.Click
        'Abort if no time series available!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("No time series available!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Dim dlg As New ConvertErrorValuesDialog(Me.Zeitreihen)
        Dim dlgresult As DialogResult = dlg.ShowDialog()

        If dlgresult = Windows.Forms.DialogResult.OK Then
            'import cleaned series
            For Each zre As TimeSeries In dlg.tsConverted.Values
                Me.Import_Series(zre, True)
            Next
        End If
    End Sub

    'Drucken
    '*******
    Private Sub Drucken_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Print.Click
        Call Me.TChart1.Printer.Preview()
    End Sub

    'Kopieren (als PNG)
    '******************
    Private Sub Kopieren_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Copy.Click
        Call Me.TChart1.Export.Image.PNG.CopyToClipboard()
    End Sub

    'Log anzeigen
    '************
    Private Sub ShowLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel_Log.Click

        'LogWindow anzeigen
        Call Wave.MyLogWindow.Show()
        Wave.MyLogWindow.WindowState = FormWindowState.Normal
        Call Wave.MyLogWindow.BringToFront()

    End Sub

    'Übersicht an/aus
    '****************
    Private Sub Übersicht_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ToggleOverview.Click

        Call Übersicht_Toggle(ToolStripButton_ToggleOverview.Checked)

    End Sub

    Private Sub Übersicht_Toggle(ByVal showÜbersicht As Boolean)
        If (showÜbersicht) Then
            Me.SplitContainer1.Panel1Collapsed = False
            Me.ToolStripButton_ToggleOverview.Checked = True
        Else
            Me.SplitContainer1.Panel1Collapsed = True
            Me.ToolStripButton_ToggleOverview.Checked = False
        End If
    End Sub

    ''' <summary>
    ''' Show Navigation button clicked
    ''' </summary>
    Private Sub ShowNavigation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ToggleNavigation.Click
        Call Me.navigationToggle(Me.ToolStripButton_ToggleNavigation.Checked)
    End Sub

    ''' <summary>
    ''' Zoom button clicked
    ''' </summary>
    Private Sub ToolStripButton_Zoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Zoom.Click
        If Me.ToolStripButton_Zoom.Checked Then
            'enable zooming
            Me.TChart1.Cursor = Me.cursor_zoom
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
            Me.TChart1.Cursor = Me.cursor_pan
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
        'Me.TChart1.Zoom.Undo() 'This (sometimes) reverts to the full zoom without using history
        If Me.TChart1.Zoom.HistorySteps.Count > 0 Then
            Dim snapshot As Steema.TeeChart.ZoomSnapshot
            snapshot = Me.TChart1.Zoom.HistorySteps.Last()
            Me.TChart1.Axes.Bottom.Minimum = snapshot.AxesMinMax(6)
            Me.TChart1.Axes.Bottom.Maximum = snapshot.AxesMinMax(7)
            Me.TChart1.Zoom.HistorySteps.RemoveAt(Me.TChart1.Zoom.HistorySteps.Count - 1)
            'Update everything
            Call Me.updateColorband()
            Call Me.updateNavigation()
        Else
            'no history present, reset the charts
            Me.selectionMade = False
            Call Me.UpdateCharts()
        End If
    End Sub

    ''' <summary>
    ''' ZoomToSeriesDropDownButton dropdown is opening
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>creates the dropdown items from the currently loaded series</remarks>
    Private Sub ToolStripDropDownButtonZoomToSeries_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton_ZoomToSeries.DropDownOpening
        Dim items As ToolStripItem()
        Dim i As Integer

        'clear existing items
        Me.ToolStripDropDownButton_ZoomToSeries.DropDown.Items.Clear()
        'create new items
        ReDim items(Me.Zeitreihen.Count - 1)
        i = 0
        For Each title As String In Me.Zeitreihen.Keys
            items(i) = New ToolStripMenuItem(title, Nothing, New EventHandler(AddressOf ZoomToSeries_Click))
            i += 1
        Next
        'add items to dropdown
        Me.ToolStripDropDownButton_ZoomToSeries.DropDown.Items.AddRange(items)
    End Sub

    ''' <summary>
    ''' ZoomToSeries entry clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ZoomToSeries_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim title As String
        title = sender.ToString()
        Call Me.ZoomToSeries(title)
    End Sub

    ''' <summary>
    ''' Zoom to a time series
    ''' </summary>
    ''' <param name="title">Title of the time series</param>
    ''' <remarks></remarks>
    Private Sub ZoomToSeries(ByVal title As String)
        Dim startdate, enddate As DateTime

        If Me.Zeitreihen.ContainsKey(title) Then
            startdate = Me.Zeitreihen(title).StartDate
            enddate = Me.Zeitreihen(title).EndDate
            'save the current zoom snapshot
            Call Me.saveZoomSnapshot()
            'zoom
            Me.TChart1.Axes.Bottom.Minimum = startdate.ToOADate()
            Me.TChart1.Axes.Bottom.Maximum = enddate.ToOADate()
            'update navigation
            Call Me.updateNavigation()
            'update colorband
            colorBand1.Start = Me.TChart1.Axes.Bottom.Minimum
            colorBand1.End = Me.TChart1.Axes.Bottom.Maximum

            Me.selectionMade = True
        Else
            'Series not found! Do nothing?
        End If
    End Sub

    ''' <summary>
    ''' Zoom All button clicked
    ''' </summary>
    Private Sub ToolStripButton_ZoomAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ZoomAll.Click
        'save the current zoom snapshot
        Call Me.saveZoomSnapshot()
        'reset the charts
        Me.selectionMade = False
        Call Me.UpdateCharts()
        'update the navigation
        Call Me.updateNavigation()
    End Sub

    ''' <summary>
    ''' Löscht alle vorhandenen Serien und liest alle importierten Zeitreihen neu ein
    ''' </summary>
    Private Sub RefreshFromFile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ReloadFromFiles.Click

        Dim fileObj As FileFormatBase
        Dim Dateiliste As String
        Dim Answer As MsgBoxResult

        'Wenn keine Dateien vorhanden, abbrechen
        If (Me.ImportedFiles.Count = 0) Then
            MsgBox("There are no known files that could be reloaded!", MsgBoxStyle.Information, "Wave")
            Exit Sub
        End If

        'Dateiliste in Textform generieren
        Dateiliste = ""
        For Each filename As String In Me.ImportedFiles.Keys
            Dateiliste &= filename & eol
        Next

        'Dialog anzeigen
        Answer = MsgBox("Delete all series and reload from the following files?" & eol & Dateiliste, MsgBoxStyle.OkCancel, "Wave")

        If (Answer = MsgBoxResult.Ok) Then

            'Alle Serien löschen
            Me.TChart1.Series.RemoveAllSeries()
            Me.TChart2.Series.RemoveAllSeries()

            'Collection zurücksetzen
            Me.Zeitreihen.Clear()

            'Alle Dateien durchlaufen
            For Each filename As String In Me.ImportedFiles.Keys

                Log.AddLogEntry("Reading file " & filename & " ...")

                'get an instance of the file
                fileObj = FileFactory.getFileInstance(filename)
                'select series for importing
                For Each series As String In Me.ImportedFiles(filename)
                    fileObj.selectSeries(series)
                Next
                'load the file
                Call fileObj.Read_File()
                'import the series
                For Each ts As TimeSeries In fileObj.TimeSeries
                    Call Me.Import_Series(ts)
                Next
                Log.AddLogEntry("File '" & filename & "' imported successfully!")
            Next

        End If

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
    ''' Toggle visibility of the navigation
    ''' </summary>
    ''' <param name="showNavigation">if True, the navigation is shown, otherwise it is hidden</param>
    Private Sub navigationToggle(ByVal showNavigation As Boolean)
        If showNavigation Then
            Me.TableLayoutPanel1.RowStyles(0).Height = 38
            Me.TableLayoutPanel1.RowStyles(2).Height = 36
            Me.ToolStripButton_ToggleNavigation.Checked = True
        Else
            Me.TableLayoutPanel1.RowStyles(0).Height = 0
            Me.TableLayoutPanel1.RowStyles(2).Height = 0
            Me.ToolStripButton_ToggleNavigation.Checked = False
        End If
    End Sub

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
            'save the current zoom snapshot
            Call Me.saveZoomSnapshot()
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

        'save the current zoom snapshot
        Call Me.saveZoomSnapshot()

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

        'fields do not correspond to the chart, therefore reset the fields
        Me.ComboBox_DisplayRangeUnit.SelectedItem = ""
        Me.NumericUpDown_DisplayRangeMultiplier.Value = 1

    End Sub

    ''' <summary>
    ''' Navigate forward/back
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_NavForwardBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_NavBack.Click, Button_NavForward.Click

        Dim multiplier As Integer
        Dim xMinOld, xMinNew, xMaxOld, xMaxNew As DateTime

        'get the previous min and max dates
        xMinOld = Date.FromOADate(Me.TChart1.Axes.Bottom.Minimum)
        xMaxOld = Date.FromOADate(Me.TChart1.Axes.Bottom.Maximum)

        multiplier = Me.NumericUpDown_NavMultiplier.Value
        'when navigating backwards, negate the multiplier
        If CType(sender, Button).Name = "Button_NavBack" Then
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
                Exit Sub
        End Select

        'save the current zoom snapshot
        Call Me.saveZoomSnapshot()

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
    ''' Mouse down event on TChart1: change cursor as needed and save zoom snapshot before panning
    ''' </summary>
    Private Sub TChart1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseDown
        If Me.ToolStripButton_NormalMode.Checked Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.TChart1.Cursor = Me.cursor_zoom
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
                'save current zoom snapshot before scrolling
                Call Me.saveZoomSnapshot()
                Me.TChart1.Cursor = Me.cursor_pan_hold
            End If
        ElseIf Me.ToolStripButton_Pan.Checked Then
            'save current zoom snapshot before scrolling
            Call Me.saveZoomSnapshot()
            Me.TChart1.Cursor = Me.cursor_pan_hold
        End If
    End Sub

    ''' <summary>
    ''' Mouse up event on TChart1: change cursor as needed
    ''' </summary>
    Private Sub TChart1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseUp
        If Me.ToolStripButton_NormalMode.Checked Then
            Me.TChart1.Cursor = Cursors.Default
        ElseIf Me.ToolStripButton_Pan.Checked Then
            Me.TChart1.Cursor = Me.cursor_pan
        End If
    End Sub

#End Region 'Cursor

#End Region 'UI

#Region "Funktionalität"

    'Zeitreihe intern hinzufügen
    '***************************
    Private Sub AddZeitreihe(ByRef zre As TimeSeries)

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
    ''' Load a Wave project file
    ''' </summary>
    ''' <param name="projectfile">Path to the Wave project file</param>
    ''' <remarks></remarks>
    Private Sub Load_WVP(ByVal projectfile As String)

        Dim fstr As FileStream
        Dim strRead As StreamReader
        Dim line, parts(), path, series, title, value As String
        Dim found As Boolean
        Dim seriesList As Dictionary(Of String, String)
        Dim seriesNotFound As List(Of String)
        Dim fileobj As FileFormatBase

        'fileDict = {filename1:{series1:title1, series2:title2, ...}, ...}
        Dim fileDict As New Dictionary(Of String, Dictionary(Of String, String))
        'settingsDict = {filename1:{setting1:value1, setting2:value2, ...}, ...}
        Dim settingsDict As New Dictionary(Of String, Dictionary(Of String, String))

        Try

            Log.AddLogEntry("Loading Wave project file " & projectfile & " ...")

            'read project file

            'file format (all whitespace is optional):
            '
            'file=path\to\file1
            ' series=seriesname1
            ' series=series2: "optional title"
            'file=path\to\file2
            ' series=series3
            ' series=series4
            '
            fstr = New FileStream(projectfile, FileMode.Open)
            strRead = New StreamReader(fstr, True)

            path = ""

            line = strRead.ReadLine()
            While Not IsNothing(line)

                line = line.Trim() 'get rid of whitespace

                If line.StartsWith("#") Then
                    'skip comments
                    line = strRead.ReadLine()
                    Continue While
                End If

                If line.ToLower().StartsWith("file=") Then
                    'file
                    path = line.Split("=".ToCharArray(), 2)(1).Trim()
                    If Not IO.Path.IsPathRooted(path) Then
                        'it's a relative path: construct the full path relative to the project file
                        path = IO.Path.GetFullPath(IO.Path.Combine(IO.Path.GetDirectoryName(projectfile), path))
                    End If
                    If Not fileDict.ContainsKey(path) Then
                        fileDict.Add(path, New Dictionary(Of String, String))
                    End If

                ElseIf line.ToLower().StartsWith("series=") Then
                    'series
                    line = line.Split("=".ToCharArray(), 2)(1).Trim()
                    If line.Contains(":") Then
                        'series with title
                        series = line.Split(":".ToCharArray(), 2)(0).Trim()
                        title = line.Split(":".ToCharArray(), 2)(1).Replace("""", "").Trim()
                    Else
                        'series without title
                        series = line.Trim()
                        title = ""
                    End If
                    'add series to file
                    If fileDict.ContainsKey(path) Then
                        If Not fileDict(path).ContainsKey(series) Then
                            fileDict(path).Add(series, title)
                        Else
                            Log.AddLogEntry("WARNING: Series " & series & " is specified twice, the second mention will be ignored!")
                        End If
                    Else
                        Log.AddLogEntry("WARNING: Series " & series & " is not associated with a file and will be ignored!")
                    End If

                ElseIf line.Contains("=") Then
                    'settings
                    parts = line.Trim().Split("=".ToCharArray(), 2)
                    'add setting to file
                    If fileDict.ContainsKey(path) Then
                        If Not settingsDict.ContainsKey(path) Then
                            settingsDict.Add(path, New Dictionary(Of String, String))
                        End If
                        If Not settingsDict(path).ContainsKey(parts(0)) Then
                            settingsDict(path).Add(parts(0), parts(1))
                        Else
                            Log.AddLogEntry("WARNING: Setting " & parts(0) & " is specified twice, the second mention will be ignored!")
                        End If
                    Else
                        Log.AddLogEntry("WARNING: Setting " & parts(0) & " is not associated with a file and will be ignored!")
                    End If

                Else
                    'ignore any other lines
                End If
                line = strRead.ReadLine()
            End While

            strRead.Close()
            fstr.Close()

            'loop over file list
            For Each file As String In fileDict.Keys

                Log.AddLogEntry("Reading file " & file & " ...")

                'get an instance of the file
                fileobj = FileFactory.getFileInstance(file)

                'apply custom import settings
                If settingsDict.ContainsKey(file) Then
                    For Each setting As String In settingsDict(file).Keys
                        value = settingsDict(file)(setting)
                        Try
                            Select Case setting.ToLower()
                                Case "iscolumnseparated"
                                    fileobj.IsColumnSeparated = If(value.ToLower() = "true", True, False)
                                Case "separator"
                                    fileobj.Separator = New Character(value)
                                Case "dateformat"
                                    fileobj.Dateformat = value
                                Case "decimalseparator"
                                    fileobj.DecimalSeparator = New Character(value)
                                Case "ilineheadings"
                                    fileobj.iLineHeadings = Convert.ToInt32(value)
                                Case "ilineunits"
                                    fileobj.iLineUnits = Convert.ToInt32(value)
                                Case "ilinedata"
                                    fileobj.iLineData = Convert.ToInt32(value)
                                Case "useunits"
                                    fileobj.UseUnits = If(value.ToLower() = "true", True, False)
                                Case "columnwidth"
                                    fileobj.ColumnWidth = Convert.ToInt32(value)
                                Case "datetimecolumnindex"
                                    fileobj.DateTimeColumnIndex = Convert.ToInt32(value)
                                Case Else
                                    Log.AddLogEntry("WARNING: Setting '" & setting & "' was not recognized and was ignored!")
                            End Select
                        Catch ex As Exception
                            Log.AddLogEntry("WARNING: Setting '" & setting & "' with value '" & value & "' could not be parsed and was ignored!")
                        End Try
                    Next
                    'reread columns with new settings
                    fileobj.ReadColumns()
                End If

                'get the list of series to be imported
                seriesList = fileDict(file)

                'select series for importing
                If seriesList.Count = 0 Then
                    'read all series contained in the file
                    Call fileobj.selectAllColumns()
                Else
                    'loop over series names
                    seriesNotFound = New List(Of String)
                    For Each series In seriesList.Keys
                        found = fileobj.selectSeries(series)
                        If Not found Then
                            seriesNotFound.Add(series)
                        End If
                    Next
                    'remove series that were not found from the dictionary
                    For Each series In seriesNotFound
                        seriesList.Remove(series)
                    Next
                    'if no series remain to be imported, abort reading the file altogether
                    If seriesList.Count = 0 Then
                        Log.AddLogEntry("ERROR: No series left to import, skipping file!")
                        Continue For
                    End If

                End If

                'read the file
                fileobj.Read_File()

                'Log
                Call Log.AddLogEntry("File '" & file & "' imported successfully!")

                'import the series
                Call Log.AddLogEntry("Loading series in chart...")
                For Each ts As TimeSeries In fileobj.TimeSeries
                    'change title if specified in the project file
                    If seriesList.Count > 0 Then
                        If seriesList(ts.Title) <> "" Then
                            ts.Title = seriesList(ts.Title)
                        End If
                    End If
                    Call Me.Import_Series(ts)
                Next

                'store file information
                Call Me.storeFileInfo(fileobj)
            Next

            'Log
            Call Log.AddLogEntry("Project file '" & projectfile & "' loaded successfully!")

            Call Me.UpdateCharts()

        Catch ex As Exception
            MsgBox("Error while loading project file:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry("ERROR: Error while loading project file:" & eol & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Lädt eine native Teechart-Datei (*.TEN)
    ''' </summary>
    ''' <param name="FileName">Pfad zur TEN-Datei</param>
    ''' <remarks></remarks>
    Private Sub Load_TEN(ByVal FileName As String)

        Dim result As DialogResult
        Dim i As Integer
        Dim reihe As TimeSeries
        Dim XMin, XMax As Double

        Try

            'Log
            Call Log.AddLogEntry("Loading file '" & FileName & "' ...")

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
                result = MsgBox("Also import time series?", MsgBoxStyle.YesNo)

                Select Case result

                    Case Windows.Forms.DialogResult.Yes
                        'Reihen aus TEN-Datei sollen importiert werden

                        'Alle Reihen durchlaufen
                        For Each series As Steema.TeeChart.Styles.Series In TChart1.Series

                            'Nur Zeitreihen behandeln
                            If (series.GetHorizAxis.IsDateTime) Then

                                'Zeitreihe aus dem importierten Diagramm nach intern übertragen
                                Log.AddLogEntry("Importing series '" & series.Title & "' from TEN file...")
                                reihe = New TimeSeries(series.Title)
                                For i = 0 To series.Count - 1
                                    reihe.AddNode(Date.FromOADate(series.XValues(i)), series.YValues(i))
                                Next
                                'Get the series' unit from the axis title
                                'TODO: the axis title might not be the actual unit
                                Select Case series.VertAxis
                                    Case Steema.TeeChart.Styles.VerticalAxis.Left
                                        reihe.Unit = Me.TChart1.Axes.Left.Title.Text
                                    Case Steema.TeeChart.Styles.VerticalAxis.Right
                                        reihe.Unit = Me.TChart1.Axes.Right.Title.Text
                                    Case Steema.TeeChart.Styles.VerticalAxis.Custom
                                        reihe.Unit = series.CustomVertAxis.Title.Text
                                End Select
                                'Store the series
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
            'Update navigation
            Call Me.updateNavigation()

            'Re-assign the chart to the chartlistbox (Bug 701)
            Me.ChartListBox1.Chart = Me.TChart1

            'Log
            Call Log.AddLogEntry("TEN file '" & FileName & "' loaded successfully!")

        Catch ex As Exception
            MsgBox("Error while loading:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry("Error while loading:" & eol & ex.Message)
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
            Call Log.AddLogEntry("Loading theme '" & FileName & "' ...")

            'Theme laden
            Call TChart1.Import.Theme.Load(FileName)

            'Log
            Call Log.AddLogEntry("Theme '" & FileName & "' loaded successfully!")

        Catch ex As Exception
            MsgBox("Error while loading:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry("Error while loading:" & eol & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Import series from multiple files
    ''' </summary>
    ''' <param name="files">array of file paths</param>
    ''' <remarks></remarks>
    Public Sub Import_Files(ByVal files() As String)
        For Each file As String In files
            Call Me.Import_File(file)
        Next
    End Sub

    ''' <summary>
    ''' Zeitreihe(n) aus einer Datei importieren
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    Public Sub Import_File(ByVal file As String)

        Dim Datei As FileFormatBase
        Dim i As Integer
        Dim ok As Boolean

        'Sonderfälle abfangen:
        '---------------------
        Select Case Path.GetExtension(file).ToUpper()

            Case FileFactory.FileExtTEN
                '.TEN-Datei
                Call Me.Load_TEN(file)

            Case FileFactory.FileExtWVP
                'Wave project file
                Call Me.Load_WVP(file)

            Case Else

                'Normalfall:
                '-----------

                Try
                    'Log
                    Call Log.AddLogEntry("Importing file '" & file & "' ...")

                    'Datei-Instanz erzeugen
                    Datei = FileFactory.getFileInstance(file)

                    If (Datei.UseImportDialog) Then
                        'Falls Importdialog erforderlich, diesen anzeigen
                        ok = Me.showImportDialog(Datei)
                        Call Application.DoEvents()
                    Else
                        'Ansonsten alle Spalten auswählen
                        Call Datei.selectAllColumns()
                        ok = True
                    End If

                    If (ok) Then

                        Me.Cursor = Cursors.WaitCursor

                        'Datei einlesen
                        Call Datei.Read_File()

                        'Log
                        Call Log.AddLogEntry("File '" & file & "' imported successfully!")

                        'Datei abspeichern
                        Call Me.storeFileInfo(Datei)

                        'Log
                        Call Log.AddLogEntry("Loading series in chart...")

                        'Alle eingelesenen Zeitreihen der Datei durchlaufen
                        For i = 0 To Datei.TimeSeries.GetUpperBound(0)
                            'Serie importieren
                            Call Me.Import_Series(Datei.TimeSeries(i))
                        Next

                        'Log
                        Call Log.AddLogEntry("Successfully loaded series in chart!")

                    Else
                        'Import abgebrochen
                        Log.AddLogEntry("Import cancelled!")

                    End If

                Catch ex As Exception
                    MsgBox("Error during import:" & eol & ex.Message, MsgBoxStyle.Critical)
                    Call Log.AddLogEntry("Error during import: " & ex.Message)

                Finally
                    Me.Cursor = Cursors.Default
                End Try

        End Select

    End Sub

    ''' <summary>
    ''' Load a timeseries from a file using information from a TALSIM clipboard entry
    ''' </summary>
    ''' <param name="clipboardtext">text content of the clipboard</param>
    ''' <remarks></remarks>
    Private Sub loadFromClipboard_TALSIM(ByVal clipboardtext As String)

        'Examples:

        '[SETTINGS]
        'Count = 1
        '[Zeitreihe1]
        'SydroTyp=SydroErgZre
        'ZRFormat=4
        'ID=362
        'Extension=.WEL
        'Kennung=S000
        'KennungLang={S000} {1AB, HYO} Ablauf_1
        'Zustand=1AB
        'Datei=D:\Talsim-NG\customers\WVER\projectData\felix\dataBase\Felix_data\00000362.WEL
        'GeaendertAm=
        'Modell=TALSIM
        'Herkunft=simuliert
        'Interpretation=2
        'SimVariante=Test Langzeit/HWMerkmal_HWGK_v02
        'Simulation=Test Langzeit
        'Einheit=m3/s
        'EndZeitreihe

        '[SETTINGS]
        'Count=2
        '[Zeitreihe1]
        'SydroTyp=SydroBinZre
        'ZRFormat=99
        'ID=1041
        'Extension=.BIN
        'Kennung=Sce10, E038, C38 (TA_Tekeze TK 04B)
        'KennungLang=Sce10, E038, C38 (TA_Tekeze TK 04B), m3/s
        'Datei=C:\Talsim-NG\customers\Nile\projectData\hubert\dataBase\hubert_zre\00001041.BIN
        'Einheit=m3/s
        'Modell=TALSIM
        'Interpretation=1
        'EndZeitreihe
        '[Zeitreihe2]
        'SydroTyp=SydroBinZre
        'ZRFormat=99
        'ID=1042
        'Extension=.BIN
        'Kennung=Sce10, E039, C39 (TA_TK5)
        'KennungLang=Sce10, E039, C39 (TA_TK5), m3/s
        'Datei=C:\Talsim-NG\customers\Nile\projectData\hubert\dataBase\hubert_zre\00001042.BIN
        'Einheit=m3/s
        'Modell=TALSIM
        'Interpretation=1
        'EndZeitreihe

        'parse clipboard contents
        Dim i_series As Integer
        Dim parts() As String
        Dim zreblock As Boolean
        Dim data As New List(Of Dictionary(Of String, String))
        Dim file, name As String
        Dim fileobj As FileFormatBase
        Dim ts As TimeSeries

        zreblock = False
        For Each line As String In clipboardtext.Split(eol)
            line = line.Trim()

            If line.StartsWith("[Zeitreihe") Then
                i_series = line.Substring(10, 1)
                data.Add(New Dictionary(Of String, String))
                zreblock = True
            End If

            If zreblock Then
                If line.Contains("=") Then
                    parts = line.Split("=")
                    data(i_series - 1).Add(parts(0), parts(1))
                ElseIf line = "EndZeitreihe" Then
                    zreblock = False
                    Continue For
                End If
            End If

        Next

        'initiate loading of series
        For Each params As Dictionary(Of String, String) In data

            file = params("Datei")

            Select Case params("ZRFormat")
                Case "4" 'WEL file

                    'build series name
                    name = params("Kennung").PadRight(4, " ") & "_" & params("Zustand")

                    'read file
                    Log.AddLogEntry("Loading file " & file & " ...")
                    fileobj = FileFactory.getFileInstance(file)

                    'read series from file
                    ts = fileobj.getTimeSeries(name)

                    'import series
                    Call Me.Import_Series(ts)

                    'store file information
                    Call Me.storeFileInfo(fileobj)

                Case "99" 'BIN file

                    name = params("Kennung")

                    'read file
                    Log.AddLogEntry("Loading file " & file & " ...")
                    fileobj = FileFactory.getFileInstance(file)

                    'read series from file
                    fileobj.Read_File()
                    ts = fileobj.TimeSeries(0)

                    'add metadata
                    ts.Title = name
                    ts.Unit = params("Einheit")

                    'import series
                    Call Me.Import_Series(ts)

                    'store file information
                    Call Me.storeFileInfo(fileobj)

            End Select

        Next

    End Sub

    ''' <summary>
    ''' Zeigt den Importdialog an und liest im Anschluss die Datei mit den eingegebenen Einstellungen ein
    ''' </summary>
    ''' <param name="Datei">Instanz der Datei, die importiert werden soll</param>
    Private Function showImportDialog(ByRef Datei As FileFormatBase) As Boolean

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
    Public Sub Import_Series(ByVal zre As TimeSeries, Optional ByVal Display As Boolean = True)
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
    Private Sub Display_Series(ByVal zre As TimeSeries)

        Dim AxisNo As Integer

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
        For Each node As KeyValuePair(Of DateTime, Double) In zre.NodesClean
            Line1.Add(node.Key, node.Value)
            Line2.Add(node.Key, node.Value)
        Next

        'Y-Achsenzuordnung
        AxisNo = getAxisNo(zre.Unit)

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
                Line1.CustomVertAxis = Me.MyAxes1(zre.Unit)
                Line2.CustomVertAxis = Me.MyAxes2(zre.Unit)
        End Select

        'Charts aktualisieren
        Call Me.UpdateCharts()
        'Update navigation
        Call Me.updateNavigation()

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
    Friend Shared Sub formatChart(ByRef chart As Steema.TeeChart.Chart)

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

    ''' <summary>
    ''' Starts the workflow for changing the time step of a series
    ''' </summary>
    ''' <remarks>requires SydroZre</remarks>
    Private Sub ChangeTimeStep()

        'Abort if no time series available!
        If (Me.Zeitreihen.Count < 1) Then
            MsgBox("No time series available!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Try
            Dim dlg As New ChangeTimestepDialog(Me.Zeitreihen)

            'show the ChangeTimeStepDialog
            If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then

                Dim zre, zre_new As TimeSeries
                Dim dates() As DateTime
                Dim double_dates() As Double
                Dim values() As Single
                Dim interpretation As Sydro.SydroZre.Fortran.InterpretationEnum
                Dim timesteptype As Sydro.SydroZre.Fortran.TimeStepTypeEnum
                Dim timestepinterval, iResp, i As Integer
                Dim startdate, enddate As DateTime
                Dim file_tmp, msg As String

                'read settings from dialog
                zre = Me.Zeitreihen(dlg.ComboBox_Timeseries.SelectedItem)
                interpretation = dlg.ComboBox_Interpretation.SelectedItem
                timesteptype = dlg.ComboBox_TimestepType.SelectedItem
                timestepinterval = dlg.NumericUpDown_TimestepInterval.Value
                startdate = dlg.DateTimePicker_Start.Value
                enddate = dlg.DateTimePicker_End.Value

                'export time series to temporary bin file
                file_tmp = Path.Combine(Path.GetTempPath, "tmp.bin")
                BIN.Write_File(zre, file_tmp)

                Using fortran As New Sydro.SydroZre.Fortran()

                    ReDim double_dates(0)
                    ReDim values(0)

                    'call the Fortran routine to get new dates and values
                    iResp = fortran.getZreDTConstValues(file_tmp, startdate, enddate, interpretation, timesteptype, timestepinterval, double_dates, values)
                    msg = fortran.ErrorMsg

                    'convert double_dates to dates
                    dates = fortran.DoubleToDate(double_dates)

                    If iResp <= 0 Then
                        If iResp = -301 Then
                            'err_wrongdimension: can be ignored
                            Log.AddLogEntry("SydroZre error " & iResp & ": " & msg)
                        Else
                            Throw New Exception("SydroZre error " & iResp & ": " & msg)
                        End If
                    End If

                End Using

                'store as a new time series
                zre_new = New TimeSeries(zre.Title & " (" & timestepinterval & " " & [Enum].GetName(GetType(Sydro.SydroZre.Fortran.TimeStepTypeEnum), timesteptype) & ")")
                zre_new.Unit = zre.Unit
                For i = 0 To dates.Length - 1
                    zre_new.AddNode(dates(i), values(i))
                Next

                Call Me.Import_Series(zre_new)

                Log.AddLogEntry("Time step changed successfully.")

            End If

        Catch ex As Exception
            Log.AddLogEntry("Error while changing the timestep: " & ex.Message)
            MsgBox("Error while changing the timestep: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

#End Region 'Funktionalität

End Class
