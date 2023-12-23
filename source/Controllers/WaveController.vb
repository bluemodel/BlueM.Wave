'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
'
Imports System.Text.RegularExpressions

Friend Class WaveController
    Inherits Controller

    Implements Steema.TeeChart.ITeeEventListener

    Private Overloads ReadOnly Property View As MainWindow
        Get
            Return _view
        End Get
    End Property

    Public Overrides Sub ShowView()
        If IsNothing(_view) Then
            _view = New MainWindow()
        End If
        View.Show()
    End Sub

    ''' <summary>
    ''' Returns the current version of Wave
    ''' </summary>
    ''' <remarks>only considers major, minor and build numbers, omitting the auto-generated revision number</remarks>
    ''' <returns>the current version number</returns>
    Private ReadOnly Property CurrentVersion As Version
        Get
            Dim v As Version = Reflection.Assembly.GetExecutingAssembly.GetName().Version()
            Return New Version($"{v.Major}.{v.Minor}.{v.Build}")
        End Get
    End Property

    ''' <summary>
    ''' Dictionary of series in the chart, key corresponds to TimeSeries ID
    ''' </summary>
    Friend ChartSeries As Dictionary(Of Integer, ScottPlot.Plottable.IPlottable)

    Private selectionMade As Boolean 'Flag zeigt an, ob bereits ein Auswahlbereich ausgewählt wurde

    ''' <summary>
    ''' History of view extents [(xmin, xmax), ...]
    ''' </summary>
    Private ZoomHistory As List(Of (xmin As Double, xmax As Double))

    ''' <summary>
    ''' Index of current view extent in zoom history (may not be saved yet)
    ''' </summary>
    Private ZoomHistoryIndex As Integer

    'State and variables for zoom and pan in main chart
    Private ChartMouseZoomDragging As Boolean = False
    Private ChartMouseDragStartX As Double
    Private ChartMousePanning As Boolean = False
    Private ChartMousePanDisplayRange As Double

    'State and variables for zoom/pan in OverviewChart
    Private OverviewChartMouseDragging As Boolean = False
    Private OverviewChartMouseDragStartX As Double
    Private OverviewChartMouseDragOffset As Double

    Private WithEvents _axisDialog As AxisDialog

    'Events handled by the AppInstance
    Friend Event Properties_Clicked()
    Friend Event TimeseriesValues_Clicked()

    Public Sub New(view As IView, ByRef model As Wave)

        Call MyBase.New(view, model)

        Me.View.SetController(Me)

        'Initialize chart series container
        Me.ChartSeries = New Dictionary(Of Integer, ScottPlot.Plottable.IPlottable)

        'Initialize zoom history
        Me.ZoomHistory = New List(Of (xmin As Double, xmax As Double))
        Me.ZoomHistoryIndex = 0

        _axisDialog = New AxisDialog()

        'Subscribe to view events

        AddHandler Me.View.Load, AddressOf View_Load

        'toolbar buttons
        AddHandler Me.View.ToolStripButton_New.Click, AddressOf New_Click
        AddHandler Me.View.ToolStripButton_Copy.Click, AddressOf Copy_Click
        AddHandler Me.View.ToolStripButton_Print.Click, AddressOf Print_Click
        AddHandler Me.View.ToolStripMenuItem_ImportSeries.Click, AddressOf ImportSeries_Click
        AddHandler Me.View.ToolStripMenuItem_LoadTEN.Click, AddressOf LoadTEN_Click
        AddHandler Me.View.ToolStripMenuItem_ReloadFromFiles.Click, AddressOf RefreshFromFiles_Click
        AddHandler Me.View.ToolStripMenuItem_RecentlyUsedFiles.DropDownItemClicked, AddressOf openRecentlyUsedFile
        AddHandler Me.View.ToolStripMenuItem_PasteFromClipboard.Click, AddressOf PasteFromClipboard_Click
        AddHandler Me.View.ToolStripMenuItem_SaveProjectFile.Click, AddressOf SaveProjectFile_Click
        AddHandler Me.View.ToolStripMenuItem_SaveChart.Click, AddressOf SaveChart_Click
        AddHandler Me.View.ToolStripMenuItem_ExportSeries.Click, AddressOf ExportZeitreihe_Click
        AddHandler Me.View.ToolStripMenuItem_EnterSeries.Click, AddressOf Eingeben_Click
        AddHandler Me.View.ToolStripButton_Cut.Click, AddressOf Zuschneiden_Click
        AddHandler Me.View.ToolStripButton_Merge.Click, AddressOf Merge_Click
        AddHandler Me.View.ToolStripButton_Analysis.Click, AddressOf Analysis_Click
        AddHandler Me.View.ToolStripButton_AxisDialog.Click, AddressOf AxisDialog_Click
        AddHandler Me.View.ToolStripButton_EditChart.Click, AddressOf EditChart_Click
        AddHandler Me.View.ToolStripMenuItem_ColorPaletteMaterial.Click, AddressOf ColorPalette_Click
        AddHandler Me.View.ToolStripMenuItem_ColorPaletteDistinct.Click, AddressOf ColorPalette_Click
        AddHandler Me.View.ToolStripMenuItem_ColorPaletteWheel.Click, AddressOf ColorPalette_Click
        AddHandler Me.View.ToolStripMenuItem_ColorPaletteRandom.Click, AddressOf ColorPalette_Click
        AddHandler Me.View.ToolStripButton_Properties.Click, AddressOf Properties_Click
        AddHandler Me.View.ToolStripButton_TimeseriesValues.Click, AddressOf TimeseriesValues_Click
        AddHandler Me.View.ToolStripButton_ToggleOverview.Click, AddressOf ToggleOverview_Clicked
        AddHandler Me.View.ToolStripButton_ToggleNavigation.Click, AddressOf ToggleNavigation_Click
        AddHandler Me.View.ToolStripButton_ShowNaNValues.Click, AddressOf ShowNaNValues_Click
        AddHandler Me.View.ToolStripButton_ConvertErrorValues.Click, AddressOf ConvertErrorValues_Click
        AddHandler Me.View.ToolStripButton_RemoveNaNValues.Click, AddressOf RemoveNaNValues_Click
        AddHandler Me.View.ToolStripButton_AutoAdjustYAxes.CheckedChanged, AddressOf AutoAdjustYAxis_CheckedChanged
        AddHandler Me.View.ToolStripButton_ZoomIn.Click, AddressOf ZoomIn_Click
        AddHandler Me.View.ToolStripButton_ZoomOut.Click, AddressOf ZoomOut_Click
        AddHandler Me.View.ToolStripButton_ZoomPrevious.Click, AddressOf ZoomPrevious_Click
        AddHandler Me.View.ToolStripButton_ZoomNext.Click, AddressOf ZoomNext_Click
        AddHandler Me.View.ToolStripButton_ZoomAll.Click, AddressOf ZoomAll_Click
        AddHandler Me.View.ToolStripDropDownButton_ZoomToSeries.DropDownOpening, AddressOf ZoomToSeries_DropDown
        AddHandler Me.View.ToolStripMenuItem_Help.Click, AddressOf Help_Click
        AddHandler Me.View.ToolStripMenuItem_CheckForUpdate.Click, AddressOf CheckForUpdate_Click
        AddHandler Me.View.ToolStripButton_UpdateNotification.Click, AddressOf CheckForUpdate_Click
        AddHandler Me.View.ToolStripMenuItem_Changelog.Click, AddressOf Changelog_Click
        AddHandler Me.View.ToolStripMenuItem_About.Click, AddressOf About_Click

        'keyboard events
        AddHandler Me.View.KeyDown, AddressOf KeyDown

        'mouse events
        AddHandler Me.View.MainPlot.MouseDown, AddressOf Chart_MouseDown
        AddHandler Me.View.MainPlot.MouseMove, AddressOf Chart_MouseMove
        AddHandler Me.View.MainPlot.MouseUp, AddressOf Chart_MouseUp
        AddHandler Me.View.MainPlot.DoubleClick, AddressOf EditChart_Click

        AddHandler Me.View.OverviewPlot.MouseDown, AddressOf OverviewChart_MouseDown
        AddHandler Me.View.OverviewPlot.MouseMove, AddressOf OverviewChart_MouseMove
        AddHandler Me.View.OverviewPlot.MouseUp, AddressOf OverviewChart_MouseUp
        AddHandler Me.View.OverviewPlot.DoubleClick, AddressOf OverviewChart_DoubleClick

        AddHandler Me.View.MainPlot.MouseWheel, AddressOf Chart_MouseWheel
        AddHandler Me.View.OverviewPlot.MouseWheel, AddressOf OverviewChart_MouseWheel

        'drag drop events
        AddHandler Me.View.DragEnter, AddressOf Wave_DragEnter
        AddHandler Me.View.DragDrop, AddressOf Wave_DragDrop

        'TOC events
        AddHandler Me.View.CheckedListBox_Series.SelectedIndexChanged, AddressOf TOC_SelectionChanged
        AddHandler Me.View.ToolStripButton_Delete.Click, AddressOf ToolStripButton_Delete_Click
        AddHandler Me.View.ToolStripButton_MoveUp.Click, AddressOf ToolStripButton_MoveUp_Click
        AddHandler Me.View.ToolStripButton_MoveDown.Click, AddressOf ToolStripButton_MoveDown_Click
        AddHandler Me.View.ToolStripMenuItem_ActivateAllSeries.Click, AddressOf ActivateAllSeries_Click
        AddHandler Me.View.ToolStripMenuItem_DeactivateAllSeries.Click, AddressOf DeactivateAllSeries_Click
        AddHandler Me.View.CheckedListBox_Series.ItemCheck, AddressOf SeriesActiveChanged

        'navigation events
        AddHandler Me.View.MaskedTextBox_NavStart.KeyDown, AddressOf navigationKeyDown
        AddHandler Me.View.MaskedTextBox_NavEnd.KeyDown, AddressOf navigationKeyDown
        AddHandler Me.View.MaskedTextBox_NavStart.TypeValidationCompleted, AddressOf navigationTypeValidationCompleted
        AddHandler Me.View.MaskedTextBox_NavEnd.TypeValidationCompleted, AddressOf navigationTypeValidationCompleted
        AddHandler Me.View.MaskedTextBox_NavStart.Validated, AddressOf navigationValidated
        AddHandler Me.View.MaskedTextBox_NavEnd.Validated, AddressOf navigationValidated

        AddHandler Me.View.ComboBox_DisplayRangeUnit.SelectedIndexChanged, AddressOf displayRangeChanged
        AddHandler Me.View.NumericUpDown_DisplayRangeMultiplier.ValueChanged, AddressOf displayRangeChanged
        AddHandler Me.View.Button_NavStart.Click, AddressOf navigationStartEnd_Click
        AddHandler Me.View.Button_NavBack.Click, AddressOf navigationBackwardForward_Click
        AddHandler Me.View.Button_NavForward.Click, AddressOf navigationBackwardForward_Click
        AddHandler Me.View.Button_NavEnd.Click, AddressOf navigationStartEnd_Click

        'status strip events
        AddHandler Me.View.ToolStripStatusLabel_Log.Click, AddressOf ShowLog_Click
        AddHandler Me.View.ToolStripStatusLabel_Errors.Click, AddressOf ShowLog_Click
        AddHandler Me.View.ToolStripStatusLabel_Warnings.Click, AddressOf ShowLog_Click

        'axis dialog events
        AddHandler _axisDialog.AxisDeleted, AddressOf axisDeleted
        AddHandler _axisDialog.AxisUnitChanged, AddressOf AxisUnitChanged

        'model events
        AddHandler _model.FileImported, AddressOf FileImported
        AddHandler _model.SeriesAdded, AddressOf SeriesAdded
        AddHandler _model.SeriesPropertiesChanged, AddressOf SeriesPropertiesChanged
        AddHandler _model.SeriesRemoved, AddressOf SeriesRemoved
        AddHandler _model.SeriesCleared, AddressOf SeriesCleared
        AddHandler _model.SeriesReordered, AddressOf SeriesReordered
        AddHandler _model.HighlightTimestamps, AddressOf showMarkers
        AddHandler _model.TENFileLoading, AddressOf Load_TEN
        AddHandler _model.IsBusyChanged, AddressOf ShowBusy

        'add any already existing time series
        For Each ts As TimeSeries In _model.TimeSeries.Values
            Call SeriesAdded(ts)
        Next

        'Log events
        AddHandler Log.LogMsgAdded, AddressOf LogMsgAdded

        'Handlers for events from Analyses are added dynamically

        'add any already existing log messages
        For Each msg As KeyValuePair(Of levels, String) In Log.logMessages
            Call LogMsgAdded(msg.Key, msg.Value)
        Next

    End Sub

    Private Sub SeriesCleared()
        'nothing to do
    End Sub

    ''' <summary>
    ''' View is loading
    ''' Checks for update and displays a notification
    ''' </summary>
    Private Async Sub View_Load(sender As System.Object, e As System.EventArgs)
        'Check for update
        Try
            Dim latestVersion As Version = Await GetLatestReleaseVersion()
            If CurrentVersion < latestVersion Then
                'show update available notification
                View.ToolStripButton_UpdateNotification.Visible = True
            End If
        Catch ex As Exception
            'do nothing if check for update fails at startup
        End Try
    End Sub

    ''' <summary>
    ''' Handles TeeChart Events
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub TeeEvent(e As Steema.TeeChart.TeeEvent) Implements Steema.TeeChart.ITeeEventListener.TeeEvent
        'TODO: TChart
        'Try
        '    If TypeOf e Is Steema.TeeChart.Styles.SeriesEvent Then
        '        Dim seriesEvent As Steema.TeeChart.Styles.SeriesEvent = CType(e, Steema.TeeChart.Styles.SeriesEvent)
        '        Select Case seriesEvent.Event
        '            Case Steema.TeeChart.Styles.SeriesEventStyle.ChangeActive
        '                'series visibility has been changed. check whether custom axes should be made invisible

        '                'collect units of all active series
        '                Dim activeUnits As New HashSet(Of String)
        '                For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
        '                    If series.Active Then
        '                        activeUnits.Add(series.GetVertAxis.Tag)
        '                    End If
        '                Next
        '                'set visibility of custom axes accordingly
        '                For Each axis As Steema.TeeChart.Axis In View.MainPlot.Axes.Custom
        '                    If activeUnits.Contains(axis.Tag) Then
        '                        axis.Visible = True
        '                    Else
        '                        axis.Visible = False
        '                    End If
        '                Next

        '            Case Steema.TeeChart.Styles.SeriesEventStyle.ChangeTitle
        '                'series title changed, update title in the model
        '                Dim id As Integer = seriesEvent.Series.Tag
        '                If _model.TimeSeries.ContainsId(id) Then
        '                    If _model.TimeSeries(id).Title <> seriesEvent.Series.Title Then
        '                        _model.TimeSeries(id).Title = seriesEvent.Series.Title
        '                        _model.SeriesPropertiesChangedHandler(id)
        '                    End If
        '                End If

        '            Case Steema.TeeChart.Styles.SeriesEventStyle.Remove
        '                'series removed, delete series from model
        '                Dim id As Integer = seriesEvent.Series.Tag
        '                If _model.TimeSeries.ContainsId(id) Then
        '                    'temporarily disable event handling to prevent multiple deletions
        '                    RemoveHandler _model.SeriesRemoved, AddressOf SeriesRemoved
        '                    _model.RemoveTimeSeries(id)
        '                    AddHandler _model.SeriesRemoved, AddressOf SeriesRemoved
        '                End If

        '        End Select
        '    End If
        'Catch ex As Exception
        '    Log.AddLogEntry(Log.levels.debug, ex.Message)
        'End Try
    End Sub

#Region "user events"

    'Neu
    '***
    Private Sub New_Click(sender As System.Object, e As System.EventArgs)

        Dim res As MsgBoxResult

        'Warnen, wenn bereits Serien vorhanden
        '-------------------------------------
        If (View.MainPlot.Plot.GetPlottables().Count() > 0) Then
            res = MsgBox($"All existing series will be deleted!{eol}Continue?", MsgBoxStyle.OkCancel)
            If (Not res = Windows.Forms.DialogResult.OK) Then Exit Sub
        End If

        'Clear chart series
        Me.ChartSeries.Clear()

        'Charts zurücksetzen
        Call View.InitializeCharts()

        'Reset Zoom history
        Me.ZoomHistory.Clear()
        Me.ZoomHistoryIndex = 0
        View.ToolStripButton_ZoomPrevious.Enabled = False
        View.ToolStripButton_ZoomNext.Enabled = False

        'remove time series from model
        _model.RemoveAllTimeSeries()

        'Log zurücksetzen
        Call Log.ClearLog()
        Call Log.HideLogWindow()

        'Reset counters
        View.ToolStripStatusLabel_Errors.Text = 0
        View.ToolStripStatusLabel_Warnings.Text = 0
        View.ToolStripStatusLabel_Errors.Image = My.Resources.cancel_inactive
        View.ToolStripStatusLabel_Warnings.Image = My.Resources.warning_inactive

        'Update axis dialog
        Me.UpdateAxisDialog()

        Me.selectionMade = False

        'Update window title
        View.Text = "BlueM.Wave"

    End Sub

    'Serie(n) importieren
    '********************
    Private Sub ImportSeries_Click(sender As System.Object, e As System.EventArgs)
        View.OpenFileDialog1.Title = "Import time series"
        View.OpenFileDialog1.Filter = TimeSeriesFile.FileFilter
        If (View.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call _model.Import_Files(View.OpenFileDialog1.FileNames)
        End If
    End Sub

    ''' <summary>
    ''' Recently Used File menu item clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub openRecentlyUsedFile(sender As Object, e As ToolStripItemClickedEventArgs)
        Dim filename As String
        filename = e.ClickedItem.Text
        Call _model.Import_File(filename)
    End Sub

    'TEN-Datei laden
    '****************
    Private Sub LoadTEN_Click(sender As System.Object, e As System.EventArgs)
        View.OpenFileDialog1.Title = "Load TEN file"
        View.OpenFileDialog1.Filter = "TeeChart files (*.ten)|*.ten"
        If (View.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Load_TEN(View.OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub PasteFromClipboard_Click(sender As Object, e As EventArgs)
        Call _model.Import_Clipboard()
    End Sub

    ''' <summary>
    ''' SaveProjectFile button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveProjectFile_Click(sender As System.Object, e As System.EventArgs)

        If _model.TimeSeries.Count = 0 Then
            MsgBox("No time series to save!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            'show save project file dialog
            Dim dlgres As DialogResult
            Dim dlg As New SaveProjectFileDialog()
            dlgres = dlg.ShowDialog()

            If dlgres = Windows.Forms.DialogResult.OK Then
                'collect display options from chart and store them in timeseries
                Dim tsList As New List(Of TimeSeries)
                For Each ts As TimeSeries In _model.TimeSeries.ToList()
                    'TODO: TChart
                    'For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
                    '    If series.Tag = ts.Id Then
                    '        If TypeOf series Is Steema.TeeChart.Styles.Line Then
                    '            Dim line As Steema.TeeChart.Styles.Line = CType(series, Steema.TeeChart.Styles.Line)
                    '            ts.DisplayOptions.Color = line.Color
                    '            ts.DisplayOptions.LineStyle = line.LinePen.Style
                    '            ts.DisplayOptions.LineWidth = line.LinePen.Width
                    '            ts.DisplayOptions.ShowPoints = line.Pointer.Visible
                    '        End If
                    '        Exit For
                    '    End If
                    'Next
                    tsList.Add(ts)
                Next
                Call Fileformats.WVP.Write_File(tsList, dlg.FileName,
                                                saveRelativePaths:=dlg.SaveRelativePaths,
                                                saveTitle:=dlg.SaveTitle,
                                                saveUnit:=dlg.SaveUnit,
                                                saveInterpretation:=dlg.SaveInterpretation,
                                                saveColor:=dlg.SaveColor,
                                                saveLineStyle:=dlg.SaveLineStyle,
                                                saveLineWidth:=dlg.SaveLineWidth,
                                                savePointsVisibility:=dlg.SavePointsVisibility
                )
                MsgBox($"Wave project file {dlg.FileName} saved.", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    'Teechart Export
    '***************
    Private Sub SaveChart_Click(sender As System.Object, e As System.EventArgs)
        'TODO: TChart
        'Dim fmt As New Steema.TeeChart.Export.TemplateExport(View.MainPlot.MainPlot)
        'Call View.MainPlot.Export.ShowExportDialog(fmt)
    End Sub

    'Zeitreihen Export
    '*****************
    Private Sub ExportZeitreihe_Click(sender As System.Object, e As System.EventArgs)
        Call _model.ExportTimeseries()
    End Sub

    'Serie eingeben
    '**************
    Private Sub Eingeben_Click(sender As System.Object, e As System.EventArgs)
        Dim SeriesEditor As New SeriesEditorDialog()
        If (SeriesEditor.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call _model.Import_Series(SeriesEditor.Zeitreihe)
        End If
    End Sub

    'Zeitreihe zuschneiden
    '*********************
    Private Sub Zuschneiden_Click(sender As System.Object, e As System.EventArgs)

        Dim id As String
        Dim ids As List(Of Integer)

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (_model.TimeSeries.Count < 1) Then
            MsgBox("No time series available for cutting!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Dialog instanzieren
        Dim cutter As New CutDialog(_model.TimeSeries.ToList, View.ChartMinX, View.ChartMaxX)

        'Dialog anzeigen
        If (cutter.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            If (cutter.CheckBox_keepUncutSeries.Checked = False) Then
                'Alte Zeitreihe(n) löschen
                If (cutter.ComboBox_ZeitreiheCut.SelectedItem.ToString = CutDialog.labelAlle) Then
                    ids = New List(Of Integer)
                    For Each id In _model.TimeSeries.Ids
                        ids.Add(id)
                    Next
                    For Each id In ids
                        Call _model.RemoveTimeSeries(id)
                    Next
                Else
                    id = CType(cutter.ComboBox_ZeitreiheCut.SelectedItem, TimeSeries).Id
                    Call _model.RemoveTimeSeries(id)
                End If
            End If

            'Neue Reihe(n) importieren
            For Each zre As TimeSeries In cutter.zreCut
                _model.Import_Series(zre)
            Next

        End If

    End Sub

    ''' <summary>
    ''' Merge time series button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Merge_Click(sender As System.Object, e As System.EventArgs)

        Dim dlg As MergeSeriesDialog
        Dim dlgResult As DialogResult
        Dim ids As List(Of Integer)
        Dim seriesMerged, seriesToMerge As TimeSeries
        Dim mergedSeriesTitle As String

        'Abort if no series are loaded
        If (_model.TimeSeries.Count < 1) Then
            MsgBox("No time series available for merging!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try

            dlg = New MergeSeriesDialog(_model.TimeSeries.ToList)
            dlgResult = dlg.ShowDialog()

            View.Cursor = Cursors.WaitCursor

            If dlgResult = Windows.Forms.DialogResult.OK Then

                ids = dlg.selectedSeries
                mergedSeriesTitle = dlg.mergedSeriesTitle

                'Clone the series with the highest priority
                seriesMerged = _model.TimeSeries(ids(0)).Clone

                'Append the remaining series in order
                For i As Integer = 1 To ids.Count - 1
                    seriesToMerge = _model.TimeSeries(ids(i))
                    seriesMerged.Append(seriesToMerge)
                Next

                'Assign title
                seriesMerged.Title = mergedSeriesTitle

                Log.AddLogEntry(Log.levels.info, "Series successfully merged!")

                _model.Import_Series(seriesMerged)

            End If

        Catch ex As Exception
            Log.AddLogEntry(Log.levels.error, "Error during merge: " & ex.Message)
            MsgBox("Error during merge: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            View.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Edit Chart button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub EditChart_Click(sender As System.Object, e As System.EventArgs)
        'TODO: TChart
        'Call Steema.TeeChart.Editor.Show(View.MainPlot)
    End Sub

    ''' <summary>
    ''' Color palette menu item clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ColorPalette_Click(sender As Object, e As EventArgs)
        Dim colorPaletteName As String = CType(sender, ToolStripMenuItem).Text
        SetChartColorPalette(Helpers.getColorPalette(colorPaletteName))
    End Sub
    ''' <summary>
    ''' Show AxisDialog button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AxisDialog_Click(sender As Object, e As EventArgs)
        Call UpdateAxisDialog()
        _axisDialog.Show()
        _axisDialog.BringToFront()
    End Sub

    ''' <summary>
    ''' Properties button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Properties_Click(sender As System.Object, e As System.EventArgs)
        RaiseEvent Properties_Clicked()
    End Sub

    ''' <summary>
    ''' Timeseries Values button clicked
    ''' </summary>
    Private Sub TimeseriesValues_Click(sender As Object, e As EventArgs)
        RaiseEvent TimeseriesValues_Clicked()
    End Sub

    ''' <summary>
    ''' Toggle overview button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToggleOverview_Clicked(sender As System.Object, e As System.EventArgs)
        If View.ToolStripButton_ToggleOverview.Checked Then
            View.SplitContainer1.Panel1Collapsed = False
            View.ToolStripButton_ToggleOverview.Checked = True
        Else
            View.SplitContainer1.Panel1Collapsed = True
            View.ToolStripButton_ToggleOverview.Checked = False
        End If
    End Sub

    ''' <summary>
    ''' Show Navigation button clicked
    ''' </summary>
    Private Sub ToggleNavigation_Click(sender As System.Object, e As System.EventArgs)
        If View.ToolStripButton_ToggleNavigation.Checked Then
            View.TableLayoutPanel1.RowStyles(0).Height = 38
            View.TableLayoutPanel1.RowStyles(2).Height = 36
            View.ToolStripButton_ToggleNavigation.Checked = True
        Else
            View.TableLayoutPanel1.RowStyles(0).Height = 0
            View.TableLayoutPanel1.RowStyles(2).Height = 0
            View.ToolStripButton_ToggleNavigation.Checked = False
        End If
    End Sub

    ''' <summary>
    ''' Show NaN values button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ShowNaNValues_Click(sender As System.Object, e As System.EventArgs)

        'TODO: TChart
        'Dim processSeries As Boolean
        'Dim nanStart, nanEnd, bandStart, bandEnd As DateTime
        'Dim band As Steema.TeeChart.Tools.ColorBand
        'Dim color As Drawing.Color
        'Dim isNaNPeriod, nanFound, nanFoundInSeries As Boolean

        ''set default color
        'color = Color.Red

        'If View.ToolStripButton_ShowNaNValues.Checked Then
        '    'Switch visualization of NaN values on
        '    'Show color bands for NaN values in the currently active series
        '    nanFound = False
        '    For Each ts As TimeSeries In _model.TimeSeries.Values
        '        processSeries = False
        '        'check if time series is currently active
        '        For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
        '            If series.Title = ts.Title Then
        '                If series.Active Then
        '                    'process this series
        '                    processSeries = True
        '                    color = series.Color
        '                End If
        '                Exit For
        '            End If
        '        Next
        '        If processSeries Then
        '            'log
        '            Log.AddLogEntry(Log.levels.info, $"Finding NaN values for series {ts.Title}...")
        '            'find beginning and end of nan values
        '            nanFoundInSeries = False
        '            isNaNPeriod = False
        '            For i As Integer = 0 To ts.Length - 1
        '                If Not isNaNPeriod Then
        '                    'test for start of NaN values
        '                    If Double.IsNaN(ts.Values(i)) Then
        '                        isNaNPeriod = True
        '                        nanFoundInSeries = True
        '                        nanFound = True
        '                        If i = 0 Then
        '                            bandStart = ts.Dates(i)
        '                        Else
        '                            bandStart = ts.Dates(i - 1)
        '                        End If
        '                        nanStart = ts.Dates(i)

        '                        If i < ts.Length - 1 Then
        '                            Continue For
        '                        End If
        '                    End If
        '                End If
        '                If isNaNPeriod Then
        '                    'test for end of NaN values
        '                    If Not Double.IsNaN(ts.Values(i)) Then
        '                        bandEnd = ts.Dates(i)
        '                        nanEnd = ts.Dates(i - 1)
        '                        isNaNPeriod = False

        '                    ElseIf i = ts.Length - 1 Then
        '                        'force end if end of time series reached
        '                        bandEnd = ts.Dates(i)
        '                        nanEnd = ts.Dates(i)
        '                        isNaNPeriod = False

        '                    End If

        '                    If Not isNaNPeriod Then
        '                        'end of NaN period reached, add a color band
        '                        band = New Steema.TeeChart.Tools.ColorBand()
        '                        View.MainPlot.Tools.Add(band)
        '                        band.Axis = View.MainPlot.Axes.Bottom
        '                        band.Start = bandStart.ToOADate()
        '                        band.End = bandEnd.ToOADate()
        '                        band.Pen.Visible = False
        '                        band.Pen.Color = color
        '                        band.Brush.Color = ControlPaint.Light(color)
        '                        band.Brush.Transparency = 50
        '                        band.ResizeEnd = False
        '                        band.ResizeStart = False
        '                        band.EndLinePen.Visible = False
        '                        band.StartLinePen.Visible = False
        '                        band.Tag = "NaN"

        '                        'write to log
        '                        Log.AddLogEntry(Log.levels.info, $"Series contains NaN values from {nanStart.ToString(Helpers.CurrentDateFormat)} to {nanEnd.ToString(Helpers.CurrentDateFormat)}")
        '                    End If
        '                End If
        '            Next
        '            If Not nanFoundInSeries Then
        '                Log.AddLogEntry(Log.levels.info, "Series does not contain any NaN values")
        '            End If
        '        End If
        '    Next
        '    If Not nanFound Then
        '        MsgBox("No NaN values found in the currently active series!", MsgBoxStyle.Information)
        '        View.ToolStripButton_ShowNaNValues.Checked = False
        '    End If
        'Else
        '    'Switch visualization of NaN values off
        '    'Remove all tools of type ColorBand with Tag "NaN" from MainPlot
        '    Dim nanbands As New List(Of Steema.TeeChart.Tools.ColorBand)
        '    For Each tool As Steema.TeeChart.Tools.Tool In View.MainPlot.Tools
        '        If tool.GetType Is GetType(Steema.TeeChart.Tools.ColorBand) And tool.Tag = "NaN" Then
        '            nanbands.Add(tool)
        '        End If
        '    Next
        '    For Each nanband As Steema.TeeChart.Tools.ColorBand In nanbands
        '        View.MainPlot.Tools.Remove(nanband)
        '    Next
        'End If
    End Sub

    ''' <summary>
    ''' Convert error values button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConvertErrorValues_Click(sender As System.Object, e As System.EventArgs)
        'Abort if no time series available!
        If (_model.TimeSeries.Count < 1) Then
            MsgBox("No time series available!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim dlg As New ConvertErrorValuesDialog(_model.TimeSeries.ToList)
        Dim dlgresult As DialogResult = dlg.ShowDialog()

        If dlgresult = Windows.Forms.DialogResult.OK Then
            'import cleaned series
            For Each zre As TimeSeries In dlg.tsConverted
                _model.Import_Series(zre)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Remove error values button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveNaNValues_Click(sender As System.Object, e As System.EventArgs)

        'TODO: TChart
        'Dim dlgResult As DialogResult
        'Dim ids As List(Of Integer)
        'Dim ts As TimeSeries

        'dlgResult = MsgBox("Delete all nodes with NaN values from all series?", MsgBoxStyle.OkCancel)
        'If dlgResult = Windows.Forms.DialogResult.OK Then
        '    ids = _model.TimeSeries.Ids.ToList()
        '    'loop over time series
        '    For Each id As Integer In ids
        '        'remove NaN values
        '        ts = _model.TimeSeries(id)
        '        ts = ts.removeNaNValues()
        '        _model.TimeSeries(id) = ts
        '        'replace values of series in chart
        '        For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
        '            If series.Tag = id Then
        '                series.BeginUpdate()
        '                series.Clear()
        '                For Each kvp As KeyValuePair(Of DateTime, Double) In ts.Nodes
        '                    series.Add(kvp.Key, kvp.Value)
        '                Next
        '                series.EndUpdate()
        '                Exit For
        '            End If
        '        Next
        '    Next
        'End If
    End Sub

    'Drucken
    '*******
    Private Sub Print_Click(sender As System.Object, e As System.EventArgs)
        'TODO: TChart
        'Call View.MainPlot.Printer.Preview()
    End Sub

    'Kopieren (als PNG)
    '******************
    Private Sub Copy_Click(sender As System.Object, e As System.EventArgs)
        'TODO: TChart
        'Call View.MainPlot.Export.Image.PNG.CopyToClipboard()
    End Sub

    'Analysieren
    '***********
    Private Sub Analysis_Click(sender As System.Object, e As System.EventArgs)

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (_model.TimeSeries.Count < 1) Then
            MsgBox("No time series available for analysis!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim oAnalysisDialog As New AnalysisDialog(_model.TimeSeries.ToList)

        'Analysisdialog anzeigen
        '-----------------------
        If (oAnalysisDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            Try
                'Wait-Cursor
                View.Cursor = Cursors.WaitCursor

                Call Log.AddLogEntry(Log.levels.info, $"Starting analysis {oAnalysisDialog.selectedAnalysisFunction} ...")

                'Analyse instanzieren
                Dim oAnalysis As Analysis
                oAnalysis = AnalysisFactory.CreateAnalysis(oAnalysisDialog.selectedAnalysisFunction, oAnalysisDialog.selectedTimeseries)

                'Add handlers for progress events
                AddHandler oAnalysis.AnalysisStarted, AddressOf ProgressReset
                AddHandler oAnalysis.AnalysisUpdated, AddressOf ProgressUpdate
                AddHandler oAnalysis.AnalysisFinished, AddressOf ProgressFinish

                Call Log.AddLogEntry(Log.levels.info, "... executing analysis ...")

                'Analyse ausführen
                Call oAnalysis.ProcessAnalysis()

                Call Log.AddLogEntry(Log.levels.info, "... preparing analysis result ...")

                'Ergebnisse aufbereiten
                Call oAnalysis.PrepareResults()

                Call Log.AddLogEntry(Log.levels.info, "Analysis complete")

                'Ergebnisse anzeigen:
                '--------------------
                'Ergebnisdiagramm anzeigen
                If (oAnalysis.hasResultChart) Then
                    Dim resultChart As New AnalysisResultChart()
                    resultChart.Text &= " - " & oAnalysisDialog.selectedAnalysisFunction.ToString()
                    'TODO: TChart
                    'resultChart.ResultChart.Controls.Add(oAnalysis.getResultChart())
                    Call resultChart.Show()
                End If

                'show result table
                If (oAnalysis.hasResultTable) Then
                    Dim resultTable As New AnalysisResultTable(oAnalysis.getResultTable())
                    resultTable.Text &= " - " & oAnalysisDialog.selectedAnalysisFunction.ToString()
                    Call resultTable.Show()
                End If

                'Ergebnistext in Log schreiben und anzeigen
                If (oAnalysis.hasResultText) Then
                    Call Log.AddLogEntry(Log.levels.info, oAnalysis.getResultText)
                    Call Log.ShowLogWindow()
                End If

                'Ergebniswerte in Log schreiben
                If (oAnalysis.hasResultValues) Then
                    Call Log.AddLogEntry(Log.levels.info, "Analysis results:")
                    For Each kvp As KeyValuePair(Of String, Double) In oAnalysis.getResultValues
                        Call Log.AddLogEntry(Log.levels.info, kvp.Key + ": " + Str(kvp.Value))
                    Next
                    Call Log.ShowLogWindow()
                End If

                'Display result series in main diagram
                If oAnalysis.hasResultSeries Then
                    For Each ts As TimeSeries In oAnalysis.getResultSeries
                        Call _model.Import_Series(ts)
                    Next
                End If

            Catch ex As Exception
                'Logeintrag
                Call Log.AddLogEntry(Log.levels.error, "Analysis failed: " & ex.Message)
                'Alert
                MsgBox("Analysis failed:" & eol & ex.Message, MsgBoxStyle.Critical)

            Finally
                View.Cursor = Cursors.Default

                ProgressFinish()
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Zoom in button clicked
    ''' decrease current zoom extent by 25%
    ''' </summary>
    Private Sub ZoomIn_Click(sender As Object, e As EventArgs)

        'save the current zoom snapshot
        Call Me.SaveZoomSnapshot()

        'zoom
        Dim displayRange As TimeSpan = View.ChartMaxX - View.ChartMinX
        View.ChartMinX = View.ChartMinX + New TimeSpan(0, 0, seconds:=displayRange.TotalSeconds * 0.125)
        View.ChartMaxX = View.ChartMaxX - New TimeSpan(0, 0, seconds:=displayRange.TotalSeconds * 0.125)

        Me.selectionMade = True
        Call Me.ViewportChanged()

    End Sub

    ''' <summary>
    ''' Zoom out button clicked
    ''' increase current zoom extent by 25%
    ''' </summary>
    Private Sub ZoomOut_Click(sender As Object, e As EventArgs)

        'save the current zoom snapshot
        Call Me.SaveZoomSnapshot()

        'zoom
        Dim displayRange As TimeSpan = View.ChartMaxX - View.ChartMinX
        View.ChartMinX = View.ChartMinX - New TimeSpan(0, 0, seconds:=displayRange.TotalSeconds * 0.125)
        View.ChartMaxX = View.ChartMaxX + New TimeSpan(0, 0, seconds:=displayRange.TotalSeconds * 0.125)

        Me.selectionMade = True
        Call Me.ViewportChanged()

    End Sub

    ''' <summary>
    ''' Zoom previous button clicked
    ''' </summary>
    Private Sub ZoomPrevious_Click(sender As System.Object, e As System.EventArgs)

        If ZoomHistoryIndex >= 1 And Me.ZoomHistory.Count >= (ZoomHistoryIndex - 1) Then
            Dim prevIndex As Integer = ZoomHistoryIndex - 1
            If ZoomHistoryIndex >= ZoomHistory.Count Then
                'save the current zoom snapshot first
                Call Me.SaveZoomSnapshot()
            End If
            Dim extent As (xmin As Double, xmax As Double) = ZoomHistory(prevIndex)
            View.ChartMinX = DateTime.FromOADate(extent.xmin)
            View.ChartMaxX = DateTime.FromOADate(extent.xmax)
            ZoomHistoryIndex = prevIndex
            Call Me.ViewportChanged()
            Log.AddLogEntry(Log.levels.debug, "Zoomed to history index " & prevIndex)
        Else
            Log.AddLogEntry(Log.levels.debug, $"No zoom history before index {ZoomHistoryIndex} available!")
        End If

        View.ToolStripButton_ZoomPrevious.Enabled = (ZoomHistoryIndex - 1 < ZoomHistory.Count And ZoomHistoryIndex > 0)
        View.ToolStripButton_ZoomNext.Enabled = (ZoomHistoryIndex + 1 < ZoomHistory.Count)

    End Sub

    ''' <summary>
    ''' Zoom next button clicked
    ''' </summary>
    Private Sub ZoomNext_Click(sender As Object, e As EventArgs)

        If Me.ZoomHistory.Count > (ZoomHistoryIndex + 1) Then
            Dim extent As (xmin As Double, xmax As Double) = ZoomHistory(ZoomHistoryIndex + 1)
            View.ChartMinX = DateTime.FromOADate(extent.xmin)
            View.ChartMaxX = DateTime.FromOADate(extent.xmax)
            ZoomHistoryIndex += 1
            Call Me.ViewportChanged()
            Log.AddLogEntry(Log.levels.debug, "Zoomed to history index " & ZoomHistoryIndex)
        Else
            Log.AddLogEntry(Log.levels.debug, $"No zoom history after index {ZoomHistoryIndex} available!")
        End If

        View.ToolStripButton_ZoomPrevious.Enabled = (ZoomHistoryIndex - 1 < ZoomHistory.Count And ZoomHistoryIndex > 0)
        View.ToolStripButton_ZoomNext.Enabled = (ZoomHistoryIndex + 1 < ZoomHistory.Count)

    End Sub

    ''' <summary>
    ''' ZoomToSeriesDropDownButton dropdown is opening
    ''' </summary>
    ''' <remarks>creates the dropdown items from the currently loaded series</remarks>
    Private Sub ZoomToSeries_DropDown(sender As System.Object, e As System.EventArgs)

        Dim items As ToolStripItem()
        Dim i As Integer

        'clear existing items
        View.ToolStripDropDownButton_ZoomToSeries.DropDown.Items.Clear()
        'create new items
        ReDim items(_model.TimeSeries.Count - 1)
        i = 0
        For Each ts As TimeSeries In _model.TimeSeries.Values
            'store the time series Id in the Name property
            items(i) = New ToolStripMenuItem(ts.Title, Nothing, New EventHandler(AddressOf ZoomToSeries_Click), ts.Id.ToString())
            i += 1
        Next
        'add items to dropdown
        View.ToolStripDropDownButton_ZoomToSeries.DropDown.Items.AddRange(items)
    End Sub

    ''' <summary>
    ''' ZoomToSeries entry clicked
    ''' </summary>
    Private Sub ZoomToSeries_Click(sender As Object, e As System.EventArgs)
        Dim id As Integer
        'try to parse the time series Id from ToolstripMenuItem.Name
        If Integer.TryParse(CType(sender, ToolStripMenuItem).Name, id) Then
            Call Me.ZoomToSeries(id)
        End If
    End Sub

    ''' <summary>
    ''' Zoom to a time series
    ''' </summary>
    ''' <param name="id">ID of the time series</param>
    Private Sub ZoomToSeries(id As Integer)
        Dim startdate, enddate As DateTime

        If _model.TimeSeries.ContainsId(id) Then
            startdate = _model.TimeSeries(id).StartDate
            enddate = _model.TimeSeries(id).EndDate
            'save the current zoom snapshot
            Call Me.SaveZoomSnapshot()
            'zoom
            View.ChartMinX = startdate
            View.ChartMaxX = enddate
            Me.selectionMade = True
            Call Me.ViewportChanged()
        Else
            'Series not found! Do nothing?
        End If
    End Sub

    ''' <summary>
    ''' Zoom All button clicked
    ''' </summary>
    Private Sub ZoomAll_Click(sender As System.Object, e As System.EventArgs)

        'save the current zoom snapshot
        Call Me.SaveZoomSnapshot()

        'reset the charts
        Me.selectionMade = False
        Call Me.UpdateChartExtents()

        Call Me.ViewportChanged()
    End Sub

    ''' <summary>
    ''' Auto-adjust Y-axes to current viewport button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AutoAdjustYAxis_CheckedChanged(sender As Object, e As System.EventArgs)
        If View.ToolStripButton_AutoAdjustYAxes.Checked Then
            Call Me.ViewportChanged()
        Else
            'Reset the Y axes to automatic
            Dim axes As List(Of ScottPlot.Renderable.Axis)
            axes = View.MainPlot.Plot.GetAxesMatching(axisIndex:=Nothing, isVertical:=True)
            For Each axis As ScottPlot.Renderable.Axis In axes
                axis.Dims.ResetLimits()
            Next
            View.MainPlot.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' Löscht alle vorhandenen Serien und liest alle importierten Zeitreihen neu ein
    ''' </summary>
    Private Sub RefreshFromFiles_Click(sender As System.Object, e As System.EventArgs)

        'TODO: keep time series that cannot be reloaded from file!

        'collect datasources
        Dim datasources As New Dictionary(Of String, List(Of String)) '{file: [title, ...], ...}
        Dim seriesNotFromFiles As New List(Of String)
        Dim file, title As String
        For Each ts As TimeSeries In _model.TimeSeries.Values
            If ts.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport Then
                file = ts.DataSource.FilePath
                title = ts.DataSource.Title
                If Not datasources.ContainsKey(file) Then
                    datasources.Add(file, New List(Of String))
                End If
                datasources(file).Add(title)
            Else
                seriesNotFromFiles.Add(ts.Title)
            End If
        Next

        'Wenn keine Dateien vorhanden, abbrechen
        If (datasources.Count = 0) Then
            MsgBox("There are no known files that could be reloaded!", MsgBoxStyle.Information)
            Exit Sub
        End If


        'Compose message
        Dim msg As String = ""
        If seriesNotFromFiles.Count > 0 Then
            msg &= "The following series do not originate from a file import and will be lost!" & eol
            For Each s As String In seriesNotFromFiles
                msg &= "* " & s & eol
            Next
            msg &= eol
        End If
        msg &= "Delete all series and reload from the following files?" & eol
        For Each s As String In datasources.Keys
            msg &= "* " & s & eol
        Next

        'Ask for user confirmation
        If MsgBox(msg, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then

            Log.AddLogEntry(Log.levels.info, "Reloading all series from their original datasources...")

            'Alle Serien löschen
            View.MainPlot.Plot.Clear()
            View.OverviewPlot.Plot.Clear()
            'View.MainPlot.Refresh()
            'View.OverviewPlot.Refresh()

            'Collection zurücksetzen
            _model.TimeSeries.Clear()

            'Alle Dateien durchlaufen
            Dim success As Boolean
            For Each file In datasources.Keys

                Log.AddLogEntry(Log.levels.info, $"Reading file {file} ...")

                success = True

                If IO.Path.GetExtension(file).ToUpper() = TimeSeriesFile.FileExtensions.TEN Then
                    'TODO: this currently loads all series from the TEN, instead of only the currently loaded ones
                    Call Load_TEN(file)
                Else
                    'get an instance of the file
                    Dim fileInstance As TimeSeriesFile = TimeSeriesFile.getInstance(file)
                    'select series for importing
                    For Each title In datasources(file)
                        success = success And fileInstance.selectSeries(title)
                    Next
                    'load the file
                    Call fileInstance.readFile()
                    'import the series
                    For Each ts As TimeSeries In fileInstance.TimeSeries.Values
                        Call _model.Import_Series(ts)
                    Next
                End If
                If success Then
                    Log.AddLogEntry(Log.levels.info, $"File '{file}' imported successfully!")
                Else
                    Log.AddLogEntry(Log.levels.error, $"Error while importing file '{file}'!")
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' Handles the user clicking the delete button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_Delete_Click(sender As Object, e As EventArgs)
        Call DeleteSelectedSeries()
    End Sub

    ''' <summary>
    ''' Handles the user clicking the move up button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_MoveUp_Click(sender As Object, e As EventArgs)
        Call ReorderSelectedSeries(Direction.Up)
    End Sub

    ''' <summary>
    ''' Handles the user clicking the move down button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_MoveDown_Click(sender As Object, e As EventArgs)
        Call ReorderSelectedSeries(Direction.Down)
    End Sub

    ''' <summary>
    ''' Activate all series button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ActivateAllSeries_Click(sender As Object, e As EventArgs)
        For i As Integer = 0 To View.CheckedListBox_Series.Items.Count - 1
            View.CheckedListBox_Series.SetItemChecked(i, True)
        Next
    End Sub

    ''' <summary>
    ''' Deactivate all series button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DeactivateAllSeries_Click(sender As Object, e As EventArgs)
        For i As Integer = 0 To View.CheckedListBox_Series.Items.Count - 1
            View.CheckedListBox_Series.SetItemChecked(i, False)
        Next
    End Sub

    ''' <summary>
    ''' Reorder selected series
    ''' </summary>
    ''' <param name="direction">Direction in which to move selected series</param>
    Private Sub ReorderSelectedSeries(direction As Direction)

        'TODO: CheckedListBox does not allow multiselection, so only one series will ever be selected

        Dim indices As New List(Of Integer)
        Dim ids As New List(Of Integer)
        For Each index As Integer In Me.View.CheckedListBox_Series.SelectedIndices
            If (direction = Direction.Up And index = 0) Or
               (direction = Direction.Down And index = Me.View.CheckedListBox_Series.Items.Count - 1) Then
                'if first row is selected, moving up does nothing
                'if last row is selected, moving down does nothing
                Exit Sub
            End If
            indices.Add(index)
            ids.Add(CType(Me.View.CheckedListBox_Series.Items(index), TimeSeries).Id)
        Next
        'reorder each series in the model
        For Each id As Integer In ids
            Model.SeriesReorder(id, direction)
        Next
        'reselect previously selected items in their new positions
        Me.View.CheckedListBox_Series.ClearSelected()
        Dim offset As Integer
        If direction = Direction.Up Then
            offset = -1
        Else
            offset = 1
        End If
        For Each index As Integer In indices
            Me.View.CheckedListBox_Series.SetSelected(index + offset, True)
        Next
    End Sub

    ''' <summary>
    ''' Deletes the series currently selected in the TOC
    ''' </summary>
    Private Sub DeleteSelectedSeries()

        'TODO: CheckedListBox does not allow multiselection, so only one series will ever be selected

        'collect titles and ids of all selected items
        Dim titles As New List(Of String)
        Dim ids As New List(Of Integer)
        For Each ts As TimeSeries In Me.View.CheckedListBox_Series.SelectedItems
            titles.Add(ts.Title)
            ids.Add(ts.Id)
        Next
        If ids.Count > 0 Then
            'ask for user confirmation
            Dim result As MsgBoxResult = MsgBox($"Delete {ids.Count} series?{eol}{String.Join(eol, titles)}", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation)
            If result = MsgBoxResult.Ok Then
                For Each id As Integer In ids
                    Model.RemoveTimeSeries(id)
                Next
            End If
        End If

    End Sub

    ''' <summary>
    ''' Handles the TOC selection changed
    ''' Enables/disables buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TOC_SelectionChanged(sender As Object, e As EventArgs)
        'Check if any items are selected
        If Me.View.CheckedListBox_Series.SelectedIndices.Count = 0 Then
            Me.View.ToolStripButton_Delete.Enabled = False
            Me.View.ToolStripButton_MoveUp.Enabled = False
            Me.View.ToolStripButton_MoveDown.Enabled = False
        Else
            Me.View.ToolStripButton_Delete.Enabled = True
            Me.View.ToolStripButton_MoveUp.Enabled = True
            Me.View.ToolStripButton_MoveDown.Enabled = True
            'check if first row is selected
            If Me.View.CheckedListBox_Series.GetSelected(0) Then
                Me.View.ToolStripButton_MoveUp.Enabled = False
            End If
            'check if last row is selected
            If Me.View.CheckedListBox_Series.GetSelected(Me.View.CheckedListBox_Series.Items.Count - 1) Then
                Me.View.ToolStripButton_MoveDown.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Check for update menu item or update notification button clicked
    ''' </summary>
    Private Async Sub CheckForUpdate_Click(sender As Object, e As EventArgs)
        Try
            'get latest release version
            Dim latestVersion As Version = Await GetLatestReleaseVersion()

            'compare versions
            If CurrentVersion < latestVersion Then
                'Update is available
                View.ToolStripButton_UpdateNotification.Visible = True
                Dim resp As MsgBoxResult = MsgBox($"A new version {latestVersion} is available!{eol}Click OK to go to the download page.", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation)
                If resp = MsgBoxResult.Ok Then
                    Process.Start(urlDownload)
                End If
            Else
                'No update available
                View.ToolStripButton_UpdateNotification.Visible = False
                MsgBox($"Currently used version {CurrentVersion} is up to date!", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox("Error while checking for update:" & eol & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the version of the latest release from the GitHub API
    ''' </summary>
    ''' <returns>Version of the latest release</returns>
    Private Async Function GetLatestReleaseVersion() As Threading.Tasks.Task(Of Version)

        'retrieve version number of latest release from GitHub API
        Dim client As New Net.Http.HttpClient()
        'GitHub API requires a user agent request header
        client.DefaultRequestHeaders.UserAgent.Clear()
        client.DefaultRequestHeaders.UserAgent.Add(New Net.Http.Headers.ProductInfoHeaderValue(New Net.Http.Headers.ProductHeaderValue("BlueM.Wave")))
        Dim json As String = Await client.GetStringAsync(urlUpdateCheck)
        Dim response As Dictionary(Of String, Object) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(json)
        Dim latestVersion As New Version(response("tag_name"))
#If Not DEBUG Then
        'TODO: Logging is not thread-safe and causes an exception in debug mode!
        Log.AddLogEntry(Log.levels.debug, "CheckUpdate: Latest release version: " & latestVersion.ToString())
#End If

        Return latestVersion

    End Function

    ''' <summary>
    ''' About Click
    ''' </summary>
    Private Sub About_Click(sender As System.Object, e As System.EventArgs)
        Dim about As New AboutBox()
        Call about.ShowDialog(Me.View)
    End Sub

    ''' <summary>
    ''' Hilfe Click (URL zum Wiki)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Help_Click(sender As System.Object, e As System.EventArgs)
        Process.Start(urlHelp)
    End Sub

    ''' <summary>
    ''' Changelog click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Changelog_Click(sender As Object, e As EventArgs)
        Dim filepath As String
        filepath = IO.Path.Combine(Application.StartupPath, "CHANGELOG.md")
        Try
            System.Diagnostics.Process.Start("notepad", filepath)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ''' <summary>
    ''' Handles KeyDown in navigation textboxes
    ''' Resets the color and, if Escape is pressed, resets the value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub navigationKeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        CType(sender, MaskedTextBox).ForeColor = Control.DefaultForeColor
        If e.KeyCode = Keys.Escape Then
            'reset navigation to correspond to chart
            Call Me.UpdateNavigation()
        End If
    End Sub

    ''' <summary>
    ''' Handles ValidationCompleted of navigation textboxes.
    ''' Checks whether input is valid DateTime and whether end date is after start date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub navigationTypeValidationCompleted(sender As System.Object, e As TypeValidationEventArgs)
        If Not e.IsValidInput Then
            e.Cancel = True
            CType(sender, MaskedTextBox).ForeColor = Color.Red
        Else
            If CType(View.MaskedTextBox_NavStart.Text(), DateTime) >= CType(View.MaskedTextBox_NavEnd.Text(), DateTime) Then
                If CType(sender, MaskedTextBox).Name = "MaskedTextBox_NavStart" Then
                    'if the start date was set to a value after the end date,
                    'move the end date using the currently displayed timespan
                    Dim displayrange As TimeSpan
                    displayrange = View.ChartMaxX - View.ChartMinX
                    View.MaskedTextBox_NavEnd.Text = CType(View.MaskedTextBox_NavStart.Text(), DateTime) + displayrange
                Else
                    'setting the end date to a value before the start date is not allowed
                    e.Cancel = True
                    CType(sender, MaskedTextBox).ForeColor = Color.Red
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles successful validation of the navigation text boxes. Updates the chart accordingly.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub navigationValidated(sender As System.Object, e As System.EventArgs)
        If Not View.isInitializing Then
            'save the current zoom snapshot
            Call Me.SaveZoomSnapshot()
            'Adjust the display range of the main chart
            View.ChartMinX = CType(View.MaskedTextBox_NavStart.Text(), DateTime)
            View.ChartMaxX = CType(View.MaskedTextBox_NavEnd.Text(), DateTime)
            Call Me.ViewportChanged()
            Me.selectionMade = True
        End If
    End Sub

    ''' <summary>
    ''' The Display range has been changed - update the chart accordingly
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayRangeChanged()

        Dim xMin, xMax As DateTime
        Dim multiplier As Integer

        'get min x axis from chart
        xMin = View.ChartMinX

        'Calculate new max value for x axis
        multiplier = View.NumericUpDown_DisplayRangeMultiplier.Value
        Select Case View.ComboBox_DisplayRangeUnit.SelectedItem
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
        Call Me.SaveZoomSnapshot()

        'Set new max value for x axis
        View.ChartMaxX = xMax

        Call Me.ViewportChanged()

        Me.selectionMade = True

    End Sub

    ''' <summary>
    ''' Navigate forward/back
    ''' </summary>
    Private Sub navigationBackwardForward_Click(sender As System.Object, e As System.EventArgs)

        Dim multiplier As Integer
        Dim xMinOld, xMinNew, xMaxOld, xMaxNew As DateTime

        'get the previous min and max dates
        xMinOld = View.ChartMinX
        xMaxOld = View.ChartMaxX

        multiplier = View.NumericUpDown_NavMultiplier.Value
        'when navigating backwards, negate the multiplier
        If CType(sender, Button).Name = "Button_NavBack" Then
            multiplier *= -1
        End If

        Select Case View.ComboBox_NavIncrement.SelectedItem
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
        Call Me.SaveZoomSnapshot()

        'update chart
        View.ChartMinX = xMinNew
        View.ChartMaxX = xMaxNew

        Call Me.ViewportChanged()

        Me.selectionMade = True

    End Sub

    ''' <summary>
    ''' Navigate to start/end
    ''' </summary>
    Private Sub navigationStartEnd_Click(sender As System.Object, e As System.EventArgs)

        Dim xMinNew, xMaxNew As Double
        Dim xDiff As Double

        'collect start and end dates of all currently active series
        Dim startdates As New List(Of Double)
        Dim enddates As New List(Of Double)
        'TODO: TChart
        'For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
        '    If Not series.Active Then
        '        Continue For
        '    End If
        '    startdates.Add(series.MinXValue)
        '    enddates.Add(series.MaxXValue)
        'Next

        If startdates.Count = 0 Or enddates.Count = 0 Then
            'Do nothing
            Exit Sub
        End If

        'calculate current viewport extent in OADate units
        xDiff = (View.ChartMaxX - View.ChartMinX).TotalDays

        Select Case CType(sender, Button).Name
            Case "Button_NavStart"
                xMinNew = startdates.Min()
                xMaxNew = xMinNew + xDiff
            Case "Button_NavEnd"
                xMaxNew = enddates.Max()
                xMinNew = xMaxNew - xDiff
            Case Else
                Throw New Exception($"Unknown button pressed!")
        End Select

        'save the current zoom snapshot
        Call Me.SaveZoomSnapshot()

        'update chart
        View.ChartMinX = DateTime.FromOADate(xMinNew)
        View.ChartMaxX = DateTime.FromOADate(xMaxNew)

        Call Me.ViewportChanged()

        Me.selectionMade = True

    End Sub

    'Log anzeigen
    '************
    Private Sub ShowLog_Click(sender As System.Object, e As System.EventArgs)
        'LogWindow anzeigen
        Call Log.ShowLogWindow()
    End Sub

    ''' <summary>
    ''' Handles KeyDown events
    ''' Initiates clipboard import if Ctrl+V was pressed
    ''' </summary>
    Private Sub KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)

        If e.Control And e.KeyCode = Keys.V Then
            'Ctrl+V pressed
            Call _model.Import_Clipboard()
        End If
    End Sub

    ''' <summary>
    ''' Handles main chart MouseDown event
    ''' Start a zooming or panning process, save zoom snapshot
    ''' </summary>
    Private Sub Chart_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Left Then
            'start zoom process
            If View.MainPlot.Plot.GetPlottables().Length > 0 Then

                Dim startValue As Double
                startValue = View.MainPlot.Plot.GetCoordinateX(e.X)

                If startValue < View.ChartMinX.ToOADate() Or
                    startValue > View.ChartMaxX.ToOADate() Then
                    'click outside of chart, don't start zoom process
                    Exit Sub
                End If

                View.MainPlot.Cursor = View.cursor_zoom
                Call Me.SaveZoomSnapshot()

                Me.ChartMouseZoomDragging = True
                Me.ChartMouseDragStartX = e.X

                View.ZoomRectangle.X1 = startValue
                View.ZoomRectangle.X2 = startValue
                View.ZoomRectangle.IsVisible = True

                View.MainPlot.Refresh()

                Log.AddLogEntry(Log.levels.debug, "Zoom start at " & DateTime.FromOADate(startValue))
            End If

        ElseIf e.Button = MouseButtons.Right Then
            'start pan process
            Me.ChartMousePanning = True
            Me.ChartMouseDragStartX = e.X
            Me.ChartMousePanDisplayRange = View.MainPlot.Plot.XAxis.Dims.Span
            Call Me.SaveZoomSnapshot()
            View.MainPlot.Cursor = View.cursor_pan
        End If

    End Sub

    ''' <summary>
    ''' Handles main chart MouseMove event
    ''' Animates any started zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Chart_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)

        If Me.ChartMouseZoomDragging Then
            Dim endValue As Double
            endValue = View.MainPlot.Plot.GetCoordinateX(e.X)
            View.ZoomRectangle.X2 = endValue
            View.MainPlot.Refresh()

        ElseIf Me.ChartMousePanning Then
            Dim xMin, xMax As Double
            Dim panDistance As Double = View.MainPlot.Plot.GetCoordinateX(Me.ChartMouseDragStartX) - View.MainPlot.Plot.GetCoordinateX(e.X)
            xMin = View.MainPlot.Plot.XAxis.Dims.Min + panDistance
            xMax = View.MainPlot.Plot.XAxis.Dims.Max + panDistance
            'prevent panning beyond displayable range (#68)
            If xMin < Constants.minOADate.ToOADate() Then
                xMin = Constants.minOADate.ToOADate()
                xMax = xMin + Me.ChartMousePanDisplayRange
            End If
            If xMax > Constants.maxOADate.ToOADate() Then
                xMax = Constants.maxOADate.ToOADate()
                xMin = xMax - Me.ChartMousePanDisplayRange
            End If
            'set the new viewport 
            View.ChartMinX = DateTime.FromOADate(xMin)
            View.ChartMaxX = DateTime.FromOADate(xMax)
            Me.selectionMade = True
            'update drag start point
            Me.ChartMouseDragStartX = e.X
        End If
    End Sub

    ''' <summary>
    ''' Handles main chart MouseUp event
    ''' Complete any started zoom or pan process, update cursor
    ''' </summary>
    Private Sub Chart_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        If Me.ChartMouseZoomDragging Then
            'complete the zoom process
            Me.ChartMouseZoomDragging = False
            'only zoom if at least 5 pixels difference to start of drag operation
            If Math.Abs(e.X - Me.ChartMouseDragStartX) > 5 Then
                'determine start and end dates of zoom
                Dim mouseValue, startValue, endValue As Double
                'prevent zooming beyond the displayable date range (#68)
                mouseValue = View.MainPlot.Plot.GetCoordinateX(e.X)
                mouseValue = Math.Max(mouseValue, Constants.minOADate.ToOADate)
                mouseValue = Math.Min(mouseValue, Constants.maxOADate.ToOADate)
                'set start and end depending on zoom direction
                If e.X > Me.ChartMouseDragStartX Then
                    startValue = View.MainPlot.Plot.GetCoordinateX(Me.ChartMouseDragStartX)
                    endValue = mouseValue
                Else
                    startValue = mouseValue
                    endValue = View.MainPlot.Plot.GetCoordinateX(Me.ChartMouseDragStartX)
                End If
                Log.AddLogEntry(Log.levels.debug, "Zoom end at " & DateTime.FromOADate(endValue))

                'save the current zoom snapshot
                Call Me.SaveZoomSnapshot()

                'set the new viewport 
                View.ChartMinX = DateTime.FromOADate(startValue)
                View.ChartMaxX = DateTime.FromOADate(endValue)
                Me.selectionMade = True
                Call Me.ViewportChanged()
            End If
            'hide zoom rectangle
            View.ZoomRectangle.IsVisible = False

            View.MainPlot.Refresh()

        ElseIf Me.ChartMousePanning Then
            'complete the pan process
            Call Me.ViewportChanged()
            Me.ChartMousePanning = False
        End If
        View.MainPlot.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Handles main chart MouseWheel events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Chart_MouseWheel(sender As Object, e As MouseEventArgs)

        Try
            ' Update the drawing based upon the mouse wheel scrolling.
            ' "The UI should scroll when the accumulated delta is plus or minus 120.
            '  The UI should scroll the number of logical lines returned by the
            '  SystemInformation.MouseWheelScrollLines property for every delta value reached."
            'https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.mousewheel?f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Windows.Forms.Control.MouseWheel)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.8)%3Bk(DevLang-VB)%26rd%3Dtrue&view=windowsdesktop-7.0#remarks
            'TODO: scale mousewheel zoom with numberOfTextLinesToMove
            Dim numberOfTextLinesToMove As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)

            'zoom while centering on mouse
            Dim currentExtent As TimeSpan = View.ChartMaxX - View.ChartMinX
            Dim newExtent As Long
            If numberOfTextLinesToMove > 0 Then
                'zoom in 25%
                newExtent = currentExtent.Ticks * 0.75
            Else
                'zoom out 25%
                newExtent = currentExtent.Ticks * 1.25
            End If

            'determine mouse position and its left-right ratio in relation to x axis
            Dim centerOADate As Double = View.MainPlot.Plot.GetCoordinateX(e.X)
            Dim centerDate As DateTime = DateTime.FromOADate(centerOADate)
            Dim leftRatio As Double = (centerDate - View.ChartMinX).Ticks / currentExtent.Ticks
            Dim rightRatio As Double = 1 - leftRatio

            'save the current zoom snapshot
            Call Me.SaveZoomSnapshot()

            'set new viewport
            View.ChartMinX = centerDate - New TimeSpan(ticks:=newExtent * leftRatio)
            View.ChartMaxX = centerDate + New TimeSpan(ticks:=newExtent * rightRatio)

            Me.selectionMade = True
            Call Me.ViewportChanged()

        Catch ex As ArgumentOutOfRangeException
            'can happen when zooming out too far, TimeSpan becomes too big or DateTime is not representable
            Log.AddLogEntry(levels.debug, $"Exception in Chart_MouseWheel: {ex}")
        End Try

    End Sub

    ''' <summary>
    ''' Handles OverviewChart MouseDown event
    ''' Starts the zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseDown(sender As Object, e As MouseEventArgs)

        If View.OverviewPlot.Plot.GetPlottables().Count > 0 Then

            If e.Button = MouseButtons.Left Then
                'start zoom process

                View.OverviewPlot.Cursor = View.cursor_zoom

                Me.OverviewChartMouseDragging = True
                Me.OverviewChartMouseDragStartX = e.X

                'set start and end value of colorband to mouse position
                Dim xMouse As Double
                xMouse = View.OverviewPlot.Plot.GetCoordinateX(Me.OverviewChartMouseDragStartX)

                'prevent zoom starting beyond displayable date range (#68)
                xMouse = Math.Max(xMouse, Constants.minOADate.ToOADate)
                xMouse = Math.Min(xMouse, Constants.maxOADate.ToOADate)

                View.ViewExtentRectangle.X1 = xMouse
                View.ViewExtentRectangle.X2 = xMouse
                View.OverviewPlot.Refresh()

                Log.AddLogEntry(Log.levels.debug, "Zoom start at " & DateTime.FromOADate(xMouse))

            ElseIf e.Button = MouseButtons.Right Then
                'start panning process

                View.OverviewPlot.Cursor = View.cursor_pan

                Me.OverviewChartMouseDragging = True
                Me.OverviewChartMouseDragStartX = e.X
                Me.OverviewChartMouseDragOffset = e.X - View.OverviewPlot.Plot.GetPixelX(View.ViewExtentRectangle.X1)

            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles OverviewChart MouseMove event
    ''' Animates any started zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseMove(sender As Object, e As MouseEventArgs)

        If Me.OverviewChartMouseDragging Then

            If e.Button = MouseButtons.Left Then
                'move the end of the colorband to the mouse pointer
                Dim xMouse As Double = View.OverviewPlot.Plot.GetCoordinateX(e.X)

                'restrict to displayable date range (#68)
                xMouse = Math.Max(xMouse, Constants.minOADate.ToOADate)
                xMouse = Math.Min(xMouse, Constants.maxOADate.ToOADate)

                View.ViewExtentRectangle.X2 = xMouse
                View.OverviewPlot.Refresh()

            ElseIf e.Button = MouseButtons.Right Then
                'move the whole color band while maintaining its width
                Dim width As Double = View.ViewExtentRectangle.X2 - View.ViewExtentRectangle.X1

                Dim startValue, endValue As Double
                startValue = View.OverviewPlot.Plot.GetCoordinateX(e.X - Me.OverviewChartMouseDragOffset)
                endValue = startValue + width

                'restrict to displayable date range (#68)
                If startValue < Constants.minOADate.ToOADate Then
                    startValue = Constants.minOADate.ToOADate
                    endValue = startValue + width
                End If
                If endValue > Constants.maxOADate.ToOADate Then
                    endValue = Constants.maxOADate.ToOADate
                    startValue = endValue - width
                End If

                View.ViewExtentRectangle.X1 = startValue
                View.ViewExtentRectangle.X2 = endValue
                View.OverviewPlot.Refresh()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles OverviewChart MouseUp event
    ''' Completes any started zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)

        View.OverviewPlot.Cursor = Cursors.Default

        If Me.OverviewChartMouseDragging Then

            Me.OverviewChartMouseDragging = False

            If e.Button = MouseButtons.Left Then
                'complete the zoom process

                'determine start and end of zoom
                If e.X <> Me.OverviewChartMouseDragStartX Then
                    'set start and end depending on zoom direction
                    Dim startValue, endValue As Double
                    If e.X > Me.OverviewChartMouseDragStartX Then
                        startValue = View.OverviewPlot.Plot.GetCoordinateX(Me.OverviewChartMouseDragStartX)
                        endValue = View.OverviewPlot.Plot.GetCoordinateX(e.X)
                    Else
                        startValue = View.OverviewPlot.Plot.GetCoordinateX(e.X)
                        endValue = View.OverviewPlot.Plot.GetCoordinateX(Me.OverviewChartMouseDragStartX)
                    End If
                    'restrict to displayable date range (#68)
                    startValue = Math.Max(startValue, Constants.minOADate.ToOADate)
                    endValue = Math.Min(endValue, Constants.maxOADate.ToOADate)

                    Log.AddLogEntry(Log.levels.debug, "Zoom end at " & DateTime.FromOADate(endValue))

                    'adjust colorband
                    View.ViewExtentRectangle.X1 = startValue
                    View.ViewExtentRectangle.X2 = endValue
                    View.OverviewPlot.Refresh()

                    'save the current zoom snapshot
                    Call Me.SaveZoomSnapshot()

                    'set the new viewport on the main chart
                    View.ChartMinX = DateTime.FromOADate(View.ViewExtentRectangle.X1)
                    View.ChartMaxX = DateTime.FromOADate(View.ViewExtentRectangle.X2)

                    Me.selectionMade = True
                    Call Me.ViewportChanged()
                End If

            ElseIf e.Button = MouseButtons.Right Then
                'complete the pan process

                'save the current zoom snapshot
                Call Me.SaveZoomSnapshot()

                'set the new viewport on the main chart
                View.ChartMinX = DateTime.FromOADate(View.ViewExtentRectangle.X1)
                View.ChartMaxX = DateTime.FromOADate(View.ViewExtentRectangle.X2)

                Me.selectionMade = True
                Call Me.ViewportChanged()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles overview chart MouseWheel events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseWheel(sender As Object, e As MouseEventArgs)

        'TODO: TChart
        Try
            ' Update the drawing based upon the mouse wheel scrolling.
            ' "The UI should scroll when the accumulated delta is plus or minus 120.
            '  The UI should scroll the number of logical lines returned by the
            '  SystemInformation.MouseWheelScrollLines property for every delta value reached."
            'https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.mousewheel?f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Windows.Forms.Control.MouseWheel)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.8)%3Bk(DevLang-VB)%26rd%3Dtrue&view=windowsdesktop-7.0#remarks
            'TODO: scale mousewheel zoom with numberOfTextLinesToMove
            Dim numberOfTextLinesToMove As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)

            If numberOfTextLinesToMove = 0 Then
                Exit Sub
            End If

            'zoom while centering on mouse
            Dim newStart As Double
            Dim newEnd As Double
            Dim mouseOADate As Double = View.OverviewPlot.Plot.GetCoordinateX(e.X)
            Dim currentExtent As Double = View.ViewExtentRectangle.X2 - View.ViewExtentRectangle.X1

            If mouseOADate >= View.ViewExtentRectangle.X1 And mouseOADate <= View.ViewExtentRectangle.X2 Then
                'zoom by 25% if mouse is inside color band
                Dim newExtent As Double
                If numberOfTextLinesToMove > 0 Then
                    newExtent = currentExtent * 0.75
                Else
                    newExtent = currentExtent * 1.25
                End If

                'set new viewport, centering on mouse
                newStart = mouseOADate - (newExtent / 2)
                newEnd = mouseOADate + (newExtent / 2)

            Else
                'pan by 25% of current extent if mouse is outside of color band
                Dim delta As Double = currentExtent * 0.25
                If mouseOADate > View.ViewExtentRectangle.X2 Then
                    'pan right
                    newStart = View.ViewExtentRectangle.X1 + delta
                    newEnd = View.ViewExtentRectangle.X2 + delta
                Else
                    'pan left
                    newStart = View.ViewExtentRectangle.X1 - delta
                    newEnd = View.ViewExtentRectangle.X2 - delta
                End If
            End If

            'set the new colorband
            View.ViewExtentRectangle.X1 = newStart
            View.ViewExtentRectangle.X2 = newEnd
            View.OverviewPlot.Refresh()

            'save the current zoom snapshot
            Call Me.SaveZoomSnapshot()

            'set the new viewport on the main chart
            View.ChartMinX = DateTime.FromOADate(View.ViewExtentRectangle.X1)
            View.ChartMaxX = DateTime.FromOADate(View.ViewExtentRectangle.X2)

            Me.selectionMade = True
            Call Me.ViewportChanged()

        Catch ex As ArgumentException
            'can happen when zooming out too far, invalid OADate
            Log.AddLogEntry(levels.debug, $"Exception in OverviewChart_MouseWheel: {ex}")
        End Try

    End Sub

    'TChart2 DoubleClick
    '*******************
    Private Sub OverviewChart_DoubleClick(sender As System.Object, e As System.EventArgs)
        'TODO: TChart
        'Call Steema.TeeChart.Editor.Show(View.OverviewPlot)
    End Sub

    ''' <summary>
    ''' Add the current zoom extent to the zoom history
    ''' </summary>
    Private Sub SaveZoomSnapshot()

        If ZoomHistoryIndex < (ZoomHistory.Count - 1) Then
            'if we are branching off from an old index, just remove the zoom history after the current index
            ZoomHistory.RemoveRange(ZoomHistoryIndex + 1, ZoomHistory.Count - (ZoomHistoryIndex + 1))
            Log.AddLogEntry(Log.levels.debug, $"Removed zoom history after index {ZoomHistoryIndex}")
        Else
            'add new snapshot
            ZoomHistory.Add((View.ChartMinX.ToOADate(), View.ChartMaxX.ToOADate()))
            Log.AddLogEntry(Log.levels.debug, $"Saved zoom snapshot {ZoomHistoryIndex}: {View.ChartMinX}, {View.ChartMaxX}")
        End If
        ZoomHistoryIndex += 1

        View.ToolStripButton_ZoomPrevious.Enabled = (ZoomHistoryIndex - 1 < ZoomHistory.Count And ZoomHistoryIndex > 0)
        View.ToolStripButton_ZoomNext.Enabled = (ZoomHistoryIndex + 1 < ZoomHistory.Count)

    End Sub

#End Region 'user events

    ''' <summary>
    ''' Updates the navigation, the overview, and if activated auto-adjusts the Y-axes
    ''' to the currently displayed viewport
    ''' </summary>
    Private Sub ViewportChanged()

        'Update navigation
        Call Me.UpdateNavigation()

        'Update overview
        Call Me.UpdateOverviewZoomExtent()

        'Auto-adjust Y-axes to current viewport
        If View.AutoAdjustYAxes Then

            'TODO: TChart
            'Dim startdate, enddate As DateTime
            'Dim title As String
            'Dim seriesMin, seriesMax, Ymin, Ymax As Double
            'Dim axisType As Steema.TeeChart.Styles.VerticalAxis
            'Dim axis As Steema.TeeChart.Axis

            ''get start and end date of current viewport
            'startdate = View.ChartMinX
            'enddate = View.ChartMaxX

            ''define axes to process
            'Dim axes As New List(Of (axisType As Steema.TeeChart.Styles.VerticalAxis, axis As Steema.TeeChart.Axis))
            'axes.Add((Steema.TeeChart.Styles.VerticalAxis.Left, View.MainPlot.Axes.Left))
            'axes.Add((Steema.TeeChart.Styles.VerticalAxis.Right, View.MainPlot.Axes.Right))
            'For Each axis In View.MainPlot.Axes.Custom
            '    axes.Add((Steema.TeeChart.Styles.VerticalAxis.Custom, axis))
            'Next

            ''loop over Y-axes
            'For Each t As (axisType As Steema.TeeChart.Styles.VerticalAxis, axis As Steema.TeeChart.Axis) In axes
            '    axisType = t.axisType
            '    axis = t.axis

            '    'loop over series
            '    Ymin = Double.MaxValue
            '    Ymax = Double.MinValue
            '    For Each ts As TimeSeries In _model.TimeSeries.Values
            '        title = ts.Title

            '        'only process active series on the current axis
            '        If View.MainPlot.Series.WithTitle(title).Active And View.MainPlot.Series.WithTitle(title).VertAxis = axisType Then

            '            If axisType = Steema.TeeChart.Styles.VerticalAxis.Custom And ts.Unit <> axis.Tag Then
            '                'series is on a different custom axis, skip it
            '                Continue For
            '            End If

            '            'get series min and max for current viewport
            '            seriesMin = ts.Minimum(startdate, enddate)
            '            If seriesMin < Ymin Then
            '                Ymin = seriesMin
            '            End If
            '            seriesMax = ts.Maximum(startdate, enddate)
            '            If seriesMax > Ymax Then
            '                Ymax = seriesMax
            '            End If
            '        End If
            '    Next

            '    'set new Y axis bounds
            '    If Ymin < Double.MaxValue Then
            '        axis.AutomaticMinimum = False
            '        axis.Minimum = Ymin
            '    End If
            '    If Ymax > Double.MinValue Then
            '        axis.AutomaticMaximum = False
            '        axis.Maximum = Ymax
            '    End If
            'Next
        End If
    End Sub

    ''' <summary>
    ''' Update the navigation based on the currently displayed timespan of the main chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateNavigation()

        Dim xMin, xMax As DateTime

        View.isInitializing = True 'need this to prevent a feedback loop

        'read dates from chart
        xMin = View.ChartMinX
        xMax = View.ChartMaxX

        'set MaskedTextBoxes
        View.MaskedTextBox_NavStart.Text = xMin.ToString()
        View.MaskedTextBox_NavEnd.Text = xMax.ToString()

        'reset color in case of previous error
        View.MaskedTextBox_NavStart.ForeColor = Control.DefaultForeColor
        View.MaskedTextBox_NavEnd.ForeColor = Control.DefaultForeColor

        'update the display range
        Call Me.UpdateDisplayRange()

        View.isInitializing = False
    End Sub

    ''' <summary>
    ''' Update the display range input fields to correspond to the chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateDisplayRange()

        Dim xMin, xMax As DateTime
        Dim multiplier As Integer

        xMin = View.ChartMinX
        xMax = View.ChartMaxX

        'check whether the selected display range corresponds to the chart
        multiplier = View.NumericUpDown_DisplayRangeMultiplier.Value
        Select Case View.ComboBox_DisplayRangeUnit.SelectedItem
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
        View.ComboBox_DisplayRangeUnit.SelectedItem = ""
        View.NumericUpDown_DisplayRangeMultiplier.Value = 1

    End Sub

    ''' <summary>
    ''' Updates the extent of the x-axes of the charts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateChartExtents()

        Dim Xmin, Xmax As DateTime

        If (_model.TimeSeries.Count = 0) Then
            'just refresh
            View.MainPlot.Refresh()
            View.OverviewPlot.Refresh()

        Else
            'Update X-Axis and colorBand

            'Determine maximum extent of chart
            Dim startdates As New List(Of DateTime)
            Dim enddates As New List(Of DateTime)
            For Each zre As TimeSeries In _model.TimeSeries.Values
                If zre.Length > 0 Then
                    startdates.Add(zre.StartDate)
                    enddates.Add(zre.EndDate)
                End If
            Next
            Xmin = startdates.Min()
            Xmax = enddates.Max()

            'respect min and max displayable dates (#68)
            If Xmax > Constants.maxOADate Then
                Xmax = Constants.maxOADate
            End If
            If Xmin < Constants.minOADate Then
                Xmin = Constants.minOADate
            End If

            'Übersicht neu skalieren
            View.OverviewPlot.Plot.XAxis.Dims.SetAxis(min:=Xmin.ToOADate(), max:=Xmax.ToOADate())

            If (Not Me.selectionMade) Then
                'Wenn noch nicht gezoomed wurde, Gesamtzeitraum auswählen
                View.ChartMinX = Xmin
                View.ChartMaxX = Xmax
            End If
            'Extent auf Colorband übertragen
            Call Me.UpdateOverviewZoomExtent()

            'TODO: Update Y-Axis extents
            'View.OverviewPlot.Plot.YAxis.Dims.

            View.MainPlot.Refresh()
            View.OverviewPlot.Refresh()
        End If

    End Sub

    ''' <summary>
    ''' Updates the colorband to correspond to the currently displayed timespan of the main chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateOverviewZoomExtent()
        Dim limits As ScottPlot.AxisLimits = View.OverviewPlot.Plot.GetAxisLimits()
        View.ViewExtentRectangle.X1 = View.ChartMinX.ToOADate()
        View.ViewExtentRectangle.X2 = View.ChartMaxX.ToOADate()
        View.OverviewPlot.Refresh()
    End Sub

    ''' <summary>
    ''' Handles a new series being added to the model
    ''' Adds the series to the charts
    ''' Also adds the datasource to the MRU file list if the time series has a file datasource
    ''' </summary>
    ''' <param name="ts">time series to display</param>
    Private Sub SeriesAdded(ts As TimeSeries)

        'Check for extreme dates not supported by TChart
        'and if necessary create a copy with removed nodes for display purposes (#68)
        If ts.StartDate < Constants.minOADate Then
            ts = ts.Clone(preserveId:=True)
            Dim t_too_early = New List(Of DateTime)
            For Each t As DateTime In ts.Dates
                If t < Constants.minOADate Then
                    t_too_early.Add(t)
                Else
                    Exit For
                End If
            Next
            For Each t As DateTime In t_too_early
                ts.Nodes.Remove(t)
            Next
            Log.AddLogEntry(Log.levels.warning, $"Unable to display {t_too_early.Count} nodes between {t_too_early.First().ToString(Helpers.CurrentDateFormat)} and {t_too_early.Last().ToString(Helpers.CurrentDateFormat)}!")
        End If
        If ts.EndDate > Constants.maxOADate Then
            ts = ts.Clone(preserveId:=True)
            Dim t_too_late As New List(Of DateTime)
            For Each t As DateTime In ts.Dates.Reverse()
                If t > Constants.maxOADate Then
                    t_too_late.Add(t)
                Else
                    Exit For
                End If
            Next
            For Each t As DateTime In t_too_late
                ts.Nodes.Remove(t)
            Next
            Log.AddLogEntry(Log.levels.warning, $"Unable to display {t_too_late.Count} nodes between {t_too_late.Last().ToString(Helpers.CurrentDateFormat)} and {t_too_late.First().ToString(Helpers.CurrentDateFormat)}!")
        End If

        'Serie zu Diagramm hinzufügen

        'Linien instanzieren
        Dim Line1 As ScottPlot.Plottable.ScatterPlot
        Line1 = View.MainPlot.Plot.AddScatter(ts.Dates.Select(Function(t As DateTime) t.ToOADate()).ToArray(), ts.Values.ToArray())

        'Do not paint NaN values
        Line1.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap

        'Namen vergeben
        Line1.Label = ts.Title

        'set display options
        If Not ts.DisplayOptions.Color.IsEmpty Then
            Line1.Color = ts.DisplayOptions.Color
        End If
        Line1.LineStyle = ts.DisplayOptions.LineStyle
        Line1.LineWidth = ts.DisplayOptions.LineWidth
        If Not ts.DisplayOptions.ShowPoints Then
            Line1.MarkerShape = ScottPlot.MarkerShape.none
        End If

        'Y-Achsenzuordnung
        assignSeriesToAxis(Line1, ts.Unit)

        'Interpretation
        Select Case ts.Interpretation
            Case TimeSeries.InterpretationEnum.BlockRight
                Line1.StepDisplay = True
                Line1.StepDisplayRight = True
            Case TimeSeries.InterpretationEnum.BlockLeft,
                TimeSeries.InterpretationEnum.CumulativePerTimestep
                Line1.StepDisplay = True
                Line1.StepDisplayRight = False
        End Select

        'Store chart series
        Me.ChartSeries.Add(ts.Id, Line1)

        'Add series to overview chart
        Call Me.AddSeriesToOverview(ts)

        'Charts aktualisieren
        Call Me.UpdateChartExtents()

        Call Me.ViewportChanged()

        View.MainPlot.Refresh()

        'Determine total number of NaN-values and write to log
        If ts.NaNCount > 0 Then
            Log.AddLogEntry(Log.levels.warning, $"Series '{ts.Title}' contains {ts.NaNCount} NaN values!")
        End If

        'add series to TOC
        View.CheckedListBox_Series.Items.Add(ts, isChecked:=True)

    End Sub

    ''' <summary>
    ''' Adds a time series to the overview chart
    ''' </summary>
    ''' <param name="ts">the TimeSeries to add</param>
    Private Sub AddSeriesToOverview(ts As TimeSeries)

        'Linien instanzieren
        Dim Line2 As ScottPlot.Plottable.ScatterPlot
        Line2 = View.OverviewPlot.Plot.AddScatterLines(ts.Dates.Select(Function(t As DateTime) t.ToOADate()).ToArray(), ts.Values.ToArray())

        'Do not paint NaN values
        Line2.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap

        'TODO: TChart
        ''Store id as Tag property
        'Line2.Tag = ts.Id

        'Namen vergeben
        Line2.Label = ts.Title

        'set display options
        If Not ts.DisplayOptions.Color.IsEmpty Then
            Line2.Color = ts.DisplayOptions.Color
        End If
        Line2.LineStyle = ts.DisplayOptions.LineStyle
        Line2.LineWidth = ts.DisplayOptions.LineWidth

        'Interpretation
        Select Case ts.Interpretation
            Case TimeSeries.InterpretationEnum.BlockRight
                Line2.StepDisplay = True
                Line2.StepDisplayRight = True
            Case TimeSeries.InterpretationEnum.BlockLeft,
                TimeSeries.InterpretationEnum.CumulativePerTimestep
                Line2.StepDisplay = True
                Line2.StepDisplayRight = False
        End Select

        View.OverviewPlot.Refresh()

    End Sub

    ''' <summary>
    ''' Handles file imported in the model event
    ''' </summary>
    ''' <param name="file"></param>
    Private Sub FileImported(file As String)
        'add filename to Recently Used Files menu
        'remove if already present
        Dim i As Integer = 0
        For Each _item As ToolStripItem In View.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems
            If _item.Text = file Then
                View.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.RemoveAt(i)
                Exit For
            End If
            i += 1
        Next
        'add to top of list
        Dim item As New ToolStripMenuItem(file)
        View.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.Insert(0, item)

        'add filename to window title
        View.Text = $"BlueM.Wave - {file}"
    End Sub

    ''' <summary>
    ''' Assigns a series to the appropriate axis depending on its unit
    ''' If no axis exists for the given unit, a new axis is created
    ''' </summary>
    ''' <param name="series">The series</param>
    ''' <param name="unit">The unit</param>
    Private Sub assignSeriesToAxis(ByRef series As ScottPlot.Plottable.IPlottable, unit As String)

        'TODO: for now, assign series to axes by matching the axis label to the unit

        'determine min and max values with some padding
        Dim serieslimits As ScottPlot.AxisLimits = series.GetAxisLimits()
        Dim span As Double = serieslimits.YMax - serieslimits.YMin
        Dim padding As Double = 0.05 * span
        Dim min As Double = serieslimits.YMin - padding
        Dim max As Double = serieslimits.YMax + padding

        'check for reusable axes
        Dim axes As IEnumerable(Of ScottPlot.Renderable.Axis)
        axes = View.MainPlot.Plot.GetAxesMatching(axisIndex:=Nothing, isVertical:=True)

        Dim axisFound As Boolean = False
        For Each axis As ScottPlot.Renderable.Axis In axes
            If axis.AxisLabel.Label = "" Then
                'this axis hasn't been used yet
                axis.IsVisible = True
                axis.Ticks(enable:=True)
                axis.AxisTicks.IsVisible = True
                axis.AxisLabel.IsVisible = True
                axis.AxisLabel.Label = unit

                'set axis bounds
                axis.Dims.SetAxis(min, max)
                axis.SetBoundary(min, max)
                axis.SetInnerBoundary(min, max)

                'assign series to new axis
                series.YAxisIndex = axis.AxisIndex
                axisFound = True
                Exit For
            ElseIf axis.AxisLabel.Label = unit Then
                'suitable existing axis found

                'update axis bounds
                Dim axisMin As Double = axis.Dims.Min
                Dim axisMax As Double = axis.Dims.Max
                Dim newMin As Double = Math.Min(axisMin, min)
                Dim newMax As Double = Math.Max(axisMax, max)

                axis.Dims.SetAxis(newMin, newMax)
                axis.SetBoundary(newMin, newMax)
                axis.SetInnerBoundary(newMin, newMax)

                'assign series to new axis
                series.YAxisIndex = axis.AxisIndex
                axisFound = True
                Exit For
            End If
        Next
        If Not axisFound Then
            'create a new custom axis
            Dim axis As ScottPlot.Renderable.Axis
            Dim number As Integer = axes.Count + 1

            'Place every second axis on the right
            If number Mod 2 = 0 Then
                axis = View.MainPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Right)
            Else
                axis = View.MainPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Left)
            End If

            axis.AxisLabel.Label = unit

            'TODO: TChart
            'axis.Tag = unit
            axis.IsVisible = True

            'Calculate position
            'axis.SetOffset(Math.Ceiling((number) / 2) * 8)

            'set axis bounds
            axis.Dims.SetAxis(min, max)
            axis.SetBoundary(min, max)
            axis.SetInnerBoundary(min, max)

            'assign series to new axis
            series.YAxisIndex = axis.AxisIndex

            'update axis dialog
            Call Me.UpdateAxisDialog()
        End If

    End Sub

    ''' <summary>
    ''' Handles a series being activated/deactivated in the TOC
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SeriesActiveChanged(sender As Object, e As ItemCheckEventArgs)

        Dim id As Integer = View.CheckedListBox_Series.Items(e.Index).Id

        If Me.ChartSeries.ContainsKey(id) Then
            Me.ChartSeries(id).IsVisible = (e.NewValue = CheckState.Checked)
        End If
        Me.View.MainPlot.Refresh()
    End Sub

    ''' <summary>
    ''' Shows markers at the given timestamps in the chart
    ''' </summary>
    ''' <param name="timestamps">List of timestamps for which to show markers</param>
    Private Sub showMarkers(timestamps As List(Of DateTime))

        'TODO: TChart
        ''Remove any existing marker series
        'For i As Integer = View.MainPlot.Series.Count - 1 To 0 Step -1
        '    Try
        '        If CType(View.MainPlot.Series(i).Tag, String) = "_markers" Then
        '            View.MainPlot.Series.RemoveAt(i)
        '        End If
        '    Catch ex As Exception
        '        Log.AddLogEntry(Log.levels.debug, ex.Message)
        '    End Try
        'Next
        'View.MainPlot.Refresh()

        'If timestamps.Count = 0 Then
        '    Exit Sub
        'End If

        ''loop over series and create a marker series for each
        'For i As Integer = 0 To View.MainPlot.Series.Count - 1
        '    Try
        '        Dim series As Steema.TeeChart.Styles.Series = View.MainPlot.Series(i)
        '        If Not series.Active Then
        '            'do not display markers for inactive series
        '            Continue For
        '        End If
        '        'collect all non-NaN values to display as markers
        '        Dim markerValues As New Dictionary(Of DateTime, Double)
        '        For Each t As DateTime In timestamps
        '            Dim index As Integer = series.XValues.IndexOf(t.ToOADate)
        '            If index <> -1 Then
        '                If Not series.IsNull(index) Then
        '                    markerValues.Add(t, series.YValues(index))
        '                End If
        '            End If
        '        Next
        '        If markerValues.Count > 0 Then
        '            'create a new point series for markers
        '            Dim markers As New Steema.TeeChart.Styles.Points(View.MainPlot.Chart)
        '            markers.Legend.Visible = False
        '            markers.Title = $"{series.Title} (selection)"
        '            markers.Tag = "_markers"
        '            markers.VertAxis = series.VertAxis
        '            If series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom Then
        '                markers.CustomVertAxis = series.CustomVertAxis
        '            End If
        '            markers.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        '            markers.Pointer.Brush.Visible = False
        '            markers.Color = series.Color
        '            markers.Pointer.Color = series.Color
        '            markers.Pointer.Pen.Color = series.Color
        '            markers.Pointer.Pen.Width = 2
        '            markers.Marks.Visible = True
        '            markers.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value
        '            'markers.Marks.OnTop = True 'causes crash when markers are panned out of view on the left
        '            markers.Marks.Callout.Visible = False
        '            markers.Marks.FontSeriesColor = True
        '            markers.Marks.Arrow.Visible = False
        '            markers.Marks.ArrowLength = 5
        '            markers.Marks.Pen.Color = series.Color
        '            'add data points
        '            For Each t As DateTime In markerValues.Keys
        '                markers.Add(t, markerValues(t))
        '            Next
        '        End If
        '    Catch ex As Exception
        '        Log.AddLogEntry(Log.levels.debug, ex.Message)
        '    End Try
        'Next

    End Sub

    ''' <summary>
    ''' Wenn sich der Log verändert hat, Statustext aktualisieren
    ''' </summary>
    Friend Sub LogMsgAdded(level As Log.levels, msg As String)
        'remove any linebreak at the beginning
        If msg.StartsWith(eol) Then
            msg = msg.Substring(eol.Length)
        End If
        'if it is a multiline message use only the first line
        If msg.Contains(eol) Then
            msg = msg.Substring(0, msg.IndexOf(eol))
        End If
        'set text
        View.ToolStripStatusLabel_Log.Text = msg.Trim()
        'set color according to level
        Select Case level
            Case Log.levels.debug
                View.ToolStripStatusLabel_Log.LinkColor = Color.DarkGreen
            Case Log.levels.warning
                View.ToolStripStatusLabel_Log.LinkColor = Color.DarkOrange
            Case Log.levels.error
                View.ToolStripStatusLabel_Log.LinkColor = Color.Red
            Case Else
                View.ToolStripStatusLabel_Log.LinkColor = SystemColors.ControlDarkDark
        End Select
        'Update counters
        If level = Log.levels.warning Then
            View.ToolStripStatusLabel_Warnings.Text = Int(View.ToolStripStatusLabel_Warnings.Text) + 1
            View.ToolStripStatusLabel_Warnings.Image = My.Resources.warning
        ElseIf level = Log.levels.error Then
            View.ToolStripStatusLabel_Errors.Text = Int(View.ToolStripStatusLabel_Errors.Text) + 1
            View.ToolStripStatusLabel_Errors.Image = My.Resources.cancel
        End If
        Call Application.DoEvents()
    End Sub

    Private Sub SeriesPropertiesChanged(id As Integer)
        'TODO: TChart
        ''find series in chart
        'For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
        '    If series.Tag = id Then
        '        'set line display according to interpretation
        '        If TypeOf series Is Steema.TeeChart.Styles.Line Then
        '            Dim seriesline As Steema.TeeChart.Styles.Line = series
        '            Select Case _model.TimeSeries(id).Interpretation
        '                Case TimeSeries.InterpretationEnum.Instantaneous,
        '                     TimeSeries.InterpretationEnum.Undefined
        '                    seriesline.Stairs = False
        '                    seriesline.InvertedStairs = False
        '                Case TimeSeries.InterpretationEnum.BlockRight
        '                    seriesline.Stairs = True
        '                    seriesline.InvertedStairs = False
        '                Case TimeSeries.InterpretationEnum.BlockLeft,
        '                     TimeSeries.InterpretationEnum.CumulativePerTimestep
        '                    seriesline.Stairs = True
        '                    seriesline.InvertedStairs = True
        '            End Select
        '        End If

        '        'update title in chart
        '        series.Title = _model.TimeSeries(id).Title

        '        'assign to axis according to unit
        '        assignSeriesToAxis(series, _model.TimeSeries(id).Unit)

        '        'TODO: apply the same changes in the overview chart?
        '        Exit For
        '    End If
        'Next

    End Sub

    ''' <summary>
    ''' Handles the case where a TimeSeries was removed from the model
    ''' </summary>
    ''' <param name="id"></param>
    Private Sub SeriesRemoved(id As Integer)

        'Remove series from main chart
        If Me.ChartSeries.ContainsKey(id) Then
            Me.View.MainPlot.Plot.Remove(Me.ChartSeries(id))
            View.MainPlot.Refresh()
        End If

        'Remove series from TOC
        Dim indexFound As Boolean = False
        Dim index As Integer = 0
        For Each ts As TimeSeries In Me.View.CheckedListBox_Series.Items
            If ts.Id = id Then
                indexFound = True
                Exit For
            End If
            index += 1
        Next
        If indexFound Then
            Me.View.CheckedListBox_Series.Items.RemoveAt(index)
        End If

        'remove series from internal storage
        Me.ChartSeries.Remove(id)

    End Sub

    ''' <summary>
    ''' Handles the case where a TimeSeries was reordered on the model side
    ''' </summary>
    ''' <param name="id">Id of the TimeSeries whose order was changed</param>
    ''' <param name="direction">Direction in which the series was moved</param>
    Private Sub SeriesReordered(id As Integer, direction As Direction)

        Dim offset As Integer
        If direction = Direction.Up Then
            offset = -1
        Else
            offset = 1
        End If

        'update series order in chart
        'find the index of the series in the chart by comparing titles
        'TODO: find a better way to detemine a series' index in the chart
        Dim index As Integer = 0
        For Each plottable As ScottPlot.Plottable.IPlottable In Me.View.MainPlot.Plot.GetPlottables()
            Dim series As ScottPlot.Plottable.ScatterPlot = TryCast(plottable, ScottPlot.Plottable.ScatterPlot)
            If Not IsNothing(series) Then
                If series.Label = Me.Model.TimeSeries(id).Title Then
                    Me.View.MainPlot.Plot.Move(index, index + offset)
                    Me.View.MainPlot.Refresh()
                    Exit For
                End If
            End If
            index += 1
        Next

        'update series order in TOC
        'find the index of the series in the TOC by comparing titles
        Dim indexFound As Boolean = False
        index = 0
        For Each ts As TimeSeries In Me.View.CheckedListBox_Series.Items
            If ts.Title = Me.Model.TimeSeries(id).Title Then
                indexFound = True
                Exit For
            End If
            index += 1
        Next
        If indexFound Then
            Dim isChecked As Boolean = Me.View.CheckedListBox_Series.GetItemChecked(index)
            Me.View.CheckedListBox_Series.Items.RemoveAt(index)
            Me.View.CheckedListBox_Series.Items.Insert(index + offset, Me.Model.TimeSeries(id))
            Me.View.CheckedListBox_Series.SetItemChecked(index + offset, isChecked)
        End If

    End Sub

    ''' <summary>
    ''' Handles axis deleted in the AxisDialog
    ''' </summary>
    ''' <param name="axisname"></param>
    Private Sub axisDeleted(axisname As String)
        'TODO: TChart
        'Dim axisnumber As Integer
        'Dim m As Match = Regex.Match(axisname, "Custom (\d+)")
        'If m.Success Then
        '    axisnumber = Integer.Parse(m.Groups(1).Value)
        '    'Delete axis from chart
        '    View.MainPlot.Axes.Custom.RemoveAt(axisnumber)
        '    View.MainPlot.Refresh()
        '    'update axis dialog
        '    Call Me.UpdateAxisDialog()
        'End If
    End Sub

    ''' <summary>
    ''' Handles axis unit changed in the AxisDialog
    ''' </summary>
    ''' <remarks>Reassigns all series to their appropriate axis</remarks>
    Private Sub AxisUnitChanged()

        'TODO: TChart
        'For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
        '    assignSeriesToAxis(series, _model.TimeSeries(series.Tag).Unit)
        'Next

        ''deactivate unused custom axes
        'Dim unitUsed As Boolean
        'For Each axis As Steema.TeeChart.Axis In View.MainPlot.Axes.Custom
        '    unitUsed = False
        '    For Each ts As TimeSeries In _model.TimeSeries.Values
        '        If ts.Unit = axis.Tag Then
        '            unitUsed = True
        '            Exit For
        '        End If
        '    Next
        '    If Not unitUsed Then
        '        axis.Visible = False
        '    End If
        'Next

    End Sub

    ''' <summary>
    ''' Update AxisDialog
    ''' </summary>
    Private Sub UpdateAxisDialog()
        'TODO: TChart
        ''Wrap Left, Right and Custom axes
        'Dim axisList As New List(Of AxisWrapper)
        'axisList.Add(New AxisWrapper("Left", View.MainPlot.Axes.Left))
        'axisList.Add(New AxisWrapper("Right", View.MainPlot.Axes.Right))
        'For i As Integer = 0 To View.MainPlot.Axes.Custom.Count - 1
        '    axisList.Add(New AxisWrapper("Custom " & i, View.MainPlot.Axes.Custom(i)))
        'Next

        '_axisDialog.Update(axisList)
    End Sub

    ''' <summary>
    ''' Process Drag and Drop of files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Wave_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs)

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then

            Dim files() As String

            'get array of file paths
            files = e.Data.GetData(DataFormats.FileDrop)

            'Invoke the file import process asynchronously
            View.BeginInvoke(New ImportDelegate(AddressOf _model.Import_Files), New String()() {files})
        End If

    End Sub

    ''' <summary>
    ''' Used for processing drag and drop of files asynchronously
    ''' </summary>
    ''' <param name="files">An enumerable of file paths</param>
    ''' <remarks></remarks>
    Private Delegate Sub ImportDelegate(files As IEnumerable(Of String))

    ''' <summary>
    ''' Processes the Wave.DragEnter event. Sets DragEventArgs.Effect to Copy if the dragged object consists of files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Wave_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs)
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ''' <summary>
    ''' Sets a color palette in the charts and changes the colors of any existing series accordingly
    ''' </summary>
    ''' <param name="colorPalette">The color palette to apply</param>
    Private Sub SetChartColorPalette(colorPalette As Color())

        'TODO: TChart
        ''set colorpalette in charts
        'View.MainPlot.Chart.ColorPalette = colorPalette
        'View.TChart2.Chart.ColorPalette = colorPalette

        ''change colors of existing series
        'Dim counter As Integer = 0
        'For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series
        '    If counter >= colorPalette.Length Then
        '        'loop color palette
        '        counter = 0
        '    End If
        '    series.Color = colorPalette(counter)
        '    'apply same color to series in overview chart
        '    For Each series2 As Steema.TeeChart.Styles.Series In View.TChart2.Series
        '        If series2.Tag = series.Tag Then
        '            series2.Color = colorPalette(counter)
        '            Exit For
        '        End If
        '    Next
        '    counter += 1
        'Next
    End Sub

    ''' <summary>
    ''' Resets the progress bar to start
    ''' </summary>
    ''' <param name="n_steps">Number of total steps expected</param>
    Private Sub ProgressReset(n_steps As Integer)
        View.ProgressBar1.Value = 0
        View.ProgressBar1.Maximum = n_steps
        View.ProgressBar1.Enabled = True
    End Sub

    ''' <summary>
    ''' Updates the progress bar
    ''' </summary>
    ''' <param name="value">Value to set the progress bar to</param>
    Private Sub ProgressUpdate(value As Integer)
        View.ProgressBar1.Value = Math.Min(value, View.ProgressBar1.Maximum)
        Call Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Sets the progress bar to finished
    ''' </summary>
    Private Sub ProgressFinish()
        View.ProgressBar1.Value = 0
        View.ProgressBar1.Enabled = False
    End Sub

    ''' <summary>
    ''' Loads a native Teechart file (*.TEN)
    ''' Unlike other filetypes, this file type has to be loaded by the controller
    ''' </summary>
    ''' <param name="FileName">Path to TEN file</param>
    Private Sub Load_TEN(FileName As String)

        'TODO: TChart
        'Dim result As DialogResult
        'Dim i As Integer
        'Dim reihe As TimeSeries
        'Dim XMin, XMax As DateTime

        'Try

        '    'Log
        '    Call Log.AddLogEntry(Log.levels.info, $"Loading file '{FileName}' ...")

        '    'Bereits vorhandene Reihen merken
        '    Dim existingIds = New List(Of Integer)
        '    For Each id As Integer In _model.TimeSeries.Ids
        '        existingIds.Add(id)
        '    Next

        '    'Zoom der X-Achse merken
        '    XMin = View.ChartMinX
        '    XMax = View.ChartMaxX
        '    If (XMin <> XMax) Then
        '        Me.selectionMade = True
        '    Else
        '        Me.selectionMade = False
        '    End If

        '    'Load TEN file in main chart, this completely replaces the chart!
        '    Call View.MainPlot.Import.Template.Load(FileName)
        '    'Clear overview chart
        '    Call View.TChart2.Series.Clear()

        '    'Abfrage für Reihenimport
        '    If (View.MainPlot.Series.Count() > 0) Then
        '        result = MsgBox("Also import time series?", MsgBoxStyle.YesNo)

        '        Select Case result

        '            Case Windows.Forms.DialogResult.Yes
        '                'Reihen aus TEN-Datei sollen importiert werden

        '                Dim nSeries As Integer = 0

        '                'Alle Reihen durchlaufen
        '                For Each series As Steema.TeeChart.Styles.Series In View.MainPlot.Series

        '                    'Nur Zeitreihen behandeln
        '                    If (series.XValues.DateTime) Then

        '                        'Zeitreihe aus dem importierten Diagramm nach intern übertragen
        '                        Log.AddLogEntry(Log.levels.info, $"Importing series '{series.Title}' from TEN file...")
        '                        reihe = New TimeSeries(series.Title)
        '                        For i = 0 To series.Count - 1
        '                            reihe.AddNode(DateTime.FromOADate(series.XValues(i)), series.YValues(i))
        '                        Next
        '                        'Determine total number of NaN-values and write to log
        '                        If reihe.NaNCount > 0 Then
        '                            Log.AddLogEntry(Log.levels.warning, $"Series '{reihe.Title}' contains {reihe.NaNCount} NaN values!")
        '                        End If
        '                        'Get the series' unit from the axis title
        '                        Dim axistitle As String = ""
        '                        Select Case series.VertAxis
        '                            Case Steema.TeeChart.Styles.VerticalAxis.Left
        '                                axistitle = View.MainPlot.Axes.Left.Title.Text
        '                            Case Steema.TeeChart.Styles.VerticalAxis.Right
        '                                axistitle = View.MainPlot.Axes.Right.Title.Text
        '                            Case Steema.TeeChart.Styles.VerticalAxis.Custom
        '                                axistitle = series.CustomVertAxis.Title.Text
        '                        End Select
        '                        reihe.Unit = AxisWrapper.parseUnit(axistitle)

        '                        'Save datasource
        '                        reihe.DataSource = New TimeSeriesDataSource(FileName, series.Title)

        '                        'Add the series to the model while temporarily disabling event handling
        '                        RemoveHandler _model.SeriesAdded, AddressOf SeriesAdded
        '                        Call _model.Import_Series(reihe)
        '                        AddHandler _model.SeriesAdded, AddressOf SeriesAdded

        '                        'after import in the model, the title might have been changed,
        '                        'then we need to edit the title in the chart as well
        '                        If reihe.Title <> series.Title Then
        '                            series.Title = reihe.Title
        '                        End If

        '                        'Store the time series id in the Tag property
        '                        series.Tag = reihe.Id

        '                        'Add series to overview
        '                        Call Me.AddSeriesToOverview(reihe)

        '                        nSeries += 1

        '                    End If
        '                Next

        '                Log.AddLogEntry(levels.info, $"Imported {nSeries} time series from TEN file.")

        '                'Update window title
        '                View.Text = "BlueM.Wave - " & FileName

        '            Case Windows.Forms.DialogResult.No
        '                'Reihen aus TEN-Datei sollen nicht importiert werden

        '                'Alle Reihen aus den Diagrammen löschen (wurden bei TEN-Import automatisch mit eingeladen)
        '                View.MainPlot.Series.RemoveAllSeries()
        '                View.TChart2.Series.RemoveAllSeries()

        '        End Select

        '    End If

        '    'extract units from axis titles and store as tags
        '    View.MainPlot.Axes.Left.Tag = AxisWrapper.parseUnit(View.MainPlot.Axes.Left.TitleOrName)
        '    View.MainPlot.Axes.Right.Tag = AxisWrapper.parseUnit(View.MainPlot.Axes.Right.TitleOrName)
        '    For Each axis As Steema.TeeChart.Axis In View.MainPlot.Axes.Custom
        '        axis.Tag = AxisWrapper.parseUnit(axis.TitleOrName)
        '    Next

        '    'Die vor dem Laden bereits vorhandenen Zeitreihen wieder zu den Diagrammen hinzufügen (durch TEN-Import verloren)
        '    For Each id As Integer In existingIds
        '        Call Me.SeriesAdded(_model.TimeSeries(id))
        '    Next

        '    'Vorherigen Zoom wiederherstellen
        '    If (Me.selectionMade) Then
        '        View.ChartMinX = XMin
        '        View.ChartMaxX = XMax
        '    End If

        '    'ColorBands neu einrichten (durch TEN-Import verloren)
        '    Call View.Init_ColorBands()

        '    'Reset zoom and pan settings
        '    View.MainPlot.Zoom.Direction = Steema.TeeChart.ZoomDirections.None
        '    View.MainPlot.Panning.Allow = Steema.TeeChart.ScrollModes.None
        '    View.MainPlot.Panning.MouseButton = MouseButtons.Right

        '    'Charts aktualisieren
        '    Call Me.UpdateChartExtents()

        '    Call Me.ViewportChanged()

        '    'Log
        '    Call Log.AddLogEntry(Log.levels.info, $"TEN file '{FileName}' loaded successfully!")

        '    Call FileImported(FileName)

        'Catch ex As Exception
        '    MsgBox("Error while loading:" & eol & ex.Message, MsgBoxStyle.Critical)
        '    Call Log.AddLogEntry(Log.levels.error, "Error while loading:" & eol & ex.Message)
        'End Try

    End Sub

    Private Sub ShowBusy(isBusy As Boolean)
        If isBusy Then
            View.Cursor = Cursors.WaitCursor
        Else
            View.Cursor = Cursors.Default
        End If
    End Sub

End Class
