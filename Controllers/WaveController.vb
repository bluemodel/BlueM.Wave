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
        View.ShowDialog()
    End Sub

    Private selectionMade As Boolean 'Flag zeigt an, ob bereits ein Auswahlbereich ausgewählt wurde

    ''' <summary>
    ''' History of view extents [(xmin, xmax), ...]
    ''' </summary>
    Private ZoomHistory As List(Of Tuple(Of Double, Double))

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

    Public Sub New(view As IView, ByRef model As Wave)

        Call MyBase.New(view, model)

        Me.View.SetController(Me)

        'Initialize zoom history
        Me.ZoomHistory = New List(Of Tuple(Of Double, Double))
        Me.ZoomHistoryIndex = 0

        _axisDialog = New AxisDialog()

        'Subscribe to view events

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
        AddHandler Me.View.ToolStripMenuItem_ReleaseNotes.Click, AddressOf ReleaseNotes_Click
        AddHandler Me.View.ToolStripMenuItem_About.Click, AddressOf About_Click

        'keyboard events
        AddHandler Me.View.KeyDown, AddressOf KeyDown

        'mouse events
        AddHandler Me.View.TChart1.MouseDown, AddressOf TChart1_MouseDown
        AddHandler Me.View.TChart1.MouseMove, AddressOf TChart1_MouseMove
        AddHandler Me.View.TChart1.MouseUp, AddressOf TChart1_MouseUp
        AddHandler Me.View.TChart1.DoubleClick, AddressOf EditChart_Click

        AddHandler Me.View.TChart2.MouseDown, AddressOf OverviewChart_MouseDown
        AddHandler Me.View.TChart2.MouseMove, AddressOf OverviewChart_MouseMove
        AddHandler Me.View.TChart2.MouseUp, AddressOf OverviewChart_MouseUp
        AddHandler Me.View.TChart2.DoubleClick, AddressOf TChart2_DoubleClick

        'drag drop events
        AddHandler Me.View.DragEnter, AddressOf Wave_DragEnter
        AddHandler Me.View.DragDrop, AddressOf Wave_DragDrop

        'navigation events
        AddHandler Me.View.MaskedTextBox_NavStart.KeyDown, AddressOf navigationKeyDown
        AddHandler Me.View.MaskedTextBox_NavEnd.KeyDown, AddressOf navigationKeyDown
        AddHandler Me.View.MaskedTextBox_NavStart.TypeValidationCompleted, AddressOf navigationTypeValidationCompleted
        AddHandler Me.View.MaskedTextBox_NavEnd.TypeValidationCompleted, AddressOf navigationTypeValidationCompleted
        AddHandler Me.View.MaskedTextBox_NavStart.Validated, AddressOf navigationValidated
        AddHandler Me.View.MaskedTextBox_NavEnd.Validated, AddressOf navigationValidated

        AddHandler Me.View.ComboBox_DisplayRangeUnit.SelectedIndexChanged, AddressOf displayRangeChanged
        AddHandler Me.View.NumericUpDown_DisplayRangeMultiplier.ValueChanged, AddressOf displayRangeChanged
        AddHandler Me.View.Button_NavBack.Click, AddressOf navigation_Click
        AddHandler Me.View.Button_NavForward.Click, AddressOf navigation_Click

        'status strip events
        AddHandler Me.View.ToolStripStatusLabel_Log.Click, AddressOf ShowLog_Click
        AddHandler Me.View.ToolStripStatusLabel_Errors.Click, AddressOf ShowLog_Click
        AddHandler Me.View.ToolStripStatusLabel_Warnings.Click, AddressOf ShowLog_Click

        'axis dialog events
        AddHandler _axisDialog.AxisDeleted, AddressOf axisDeleted
        AddHandler _axisDialog.AxisUnitChanged, AddressOf AxisUnitChanged

        'add chart event listener
        Me.View.TChart1.Chart.Listeners.Add(Me)

        'model events
        AddHandler _model.SeriesAdded, AddressOf SeriesAdded
        AddHandler _model.SeriesPropertiesChanged, AddressOf SeriesPropertiesChanged
        AddHandler _model.SeriesRemoved, AddressOf SeriesRemoved
        AddHandler _model.SeriesCleared, AddressOf SeriesCleared
        AddHandler _model.HighlightTimestamps, AddressOf showMarkers

        'Log events
        AddHandler Log.LogMsgAdded, AddressOf LogMsgAdded

    End Sub

    Private Sub SeriesCleared()
        'nothing to do
    End Sub

    '''' <summary>
    '''' View is loading
    '''' FIXME: Checks for update
    '''' TODO: should this be done in the model?
    '''' </summary>
    'Private Async Sub Wave_Load(sender As System.Object, e As System.EventArgs) Handles View.Load
    '    'Check for update
    '    Try
    '        Dim updateAvailable As Boolean
    '        updateAvailable = Await _model.CheckForUpdate()
    '        If updateAvailable Then
    '            View.ToolStripButton_UpdateNotification.Visible = True
    '        End If
    '    Catch ex As Exception
    '        'do nothing if check for update fails at startup
    '    End Try
    'End Sub

    ''' <summary>
    ''' Handles TeeChart Events
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub TeeEvent(e As Steema.TeeChart.TeeEvent) Implements Steema.TeeChart.ITeeEventListener.TeeEvent
        Try
            If TypeOf e Is Steema.TeeChart.Styles.SeriesEvent Then
                Dim seriesEvent As Steema.TeeChart.Styles.SeriesEvent = CType(e, Steema.TeeChart.Styles.SeriesEvent)
                Select Case seriesEvent.Event
                    Case Steema.TeeChart.Styles.SeriesEventStyle.ChangeActive
                        'series visibility has been changed. check whether custom axes should be made invisible

                        'collect units of all active series
                        Dim activeUnits As New HashSet(Of String)
                        For Each series As Steema.TeeChart.Styles.Series In View.TChart1.Series
                            If series.Active Then
                                activeUnits.Add(series.GetVertAxis.Tag)
                            End If
                        Next
                        'set visibility of custom axes accordingly
                        For Each axis As Steema.TeeChart.Axis In View.TChart1.Axes.Custom
                            If activeUnits.Contains(axis.Tag) Then
                                axis.Visible = True
                            Else
                                axis.Visible = False
                            End If
                        Next

                    Case Steema.TeeChart.Styles.SeriesEventStyle.ChangeTitle
                        'series title changed, update title in the model
                        Dim id As Integer = seriesEvent.Series.Tag
                        If _model.TimeSeriesDict.ContainsKey(id) Then
                            If _model.TimeSeriesDict(id).Title <> seriesEvent.Series.Title Then
                                _model.TimeSeriesDict(id).Title = seriesEvent.Series.Title
                                _model.SeriesPropertiesChangedHandler(id)
                            End If
                        End If

                    Case Steema.TeeChart.Styles.SeriesEventStyle.Remove
                        'series removed, delete series from model
                        Dim id As Integer = seriesEvent.Series.Tag
                        If _model.TimeSeriesDict.ContainsKey(id) Then
                            'FIXME: this causes a feedback loop and deletes two series!
                            _model.RemoveTimeSeries(id)
                        End If

                End Select
            End If
        Catch ex As Exception
            Log.AddLogEntry(Log.levels.debug, ex.Message)
        End Try
    End Sub

#Region "user events"

    'Neu
    '***
    Private Sub New_Click(sender As System.Object, e As System.EventArgs)

        Dim res As MsgBoxResult

        'Warnen, wenn bereits Serien vorhanden
        '-------------------------------------
        If (View.TChart1.Series.Count() > 0) Then
            res = MsgBox($"All existing series will be deleted!{eol}Continue?", MsgBoxStyle.OkCancel)
            If (Not res = Windows.Forms.DialogResult.OK) Then Exit Sub
        End If

        'Charts zurücksetzen
        Call View.Init_Charts()

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
        View.OpenFileDialog1.Filter = FileFormatBase.FileFilter
        'TODO: allow selection of multiple files
        If (View.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call _model.Import_File(View.OpenFileDialog1.FileName)
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
            Call _model.Load_TEN(View.OpenFileDialog1.FileName)
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

        Dim dlgres As DialogResult

        'Prepare SaveFileDialog
        View.SaveFileDialog1.Title = "Save project file"
        View.SaveFileDialog1.Filter = "Wave project files (*.wvp)|*wvp"
        View.SaveFileDialog1.DefaultExt = "wvp"
        View.SaveFileDialog1.OverwritePrompt = True

        'Show dialog
        dlgres = View.SaveFileDialog1.ShowDialog()

        If dlgres = Windows.Forms.DialogResult.OK Then
            Call _model.SaveProjectFile(View.SaveFileDialog1.FileName)
        End If

    End Sub

    'Teechart Export
    '***************
    Private Sub SaveChart_Click(sender As System.Object, e As System.EventArgs)
        Call View.TChart1.Export.ShowExportDialog()
    End Sub

    'Zeitreihen Export
    '*****************
    Private Sub ExportZeitreihe_Click(sender As System.Object, e As System.EventArgs)
        Call _model.ExportZeitreihe()
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
        If (_model.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for cutting!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Dialog instanzieren
        Dim cutter As New CutDialog(_model.TimeSeriesDict.Values.ToList, View.ChartMinX, View.ChartMaxX)

        'Dialog anzeigen
        If (cutter.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            If (cutter.CheckBox_keepUncutSeries.Checked = False) Then
                'Alte Zeitreihe(n) löschen
                If (cutter.ComboBox_ZeitreiheCut.SelectedItem.ToString = CutDialog.labelAlle) Then
                    ids = New List(Of Integer)
                    For Each id In _model.TimeSeriesDict.Keys
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
        If (_model.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for merging!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try

            dlg = New MergeSeriesDialog(_model.TimeSeriesDict.Values.ToList)
            dlgResult = dlg.ShowDialog()

            View.Cursor = Cursors.WaitCursor

            If dlgResult = Windows.Forms.DialogResult.OK Then

                ids = dlg.selectedSeries
                mergedSeriesTitle = dlg.mergedSeriesTitle

                'Clone the series with the highest priority
                seriesMerged = _model.TimeSeriesDict(ids(0)).Clone

                'Append the remaining series in order
                For i As Integer = 1 To ids.Count - 1
                    seriesToMerge = _model.TimeSeriesDict(ids(i))
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
        Call Steema.TeeChart.Editor.Show(View.TChart1)
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
        App.showPropDialog()
    End Sub

    ''' <summary>
    ''' Timeseries Values button clicked
    ''' </summary>
    Private Sub TimeseriesValues_Click(sender As Object, e As EventArgs)
        App.showValuesDialog()
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

        Dim processSeries As Boolean
        Dim nanStart, nanEnd, bandStart, bandEnd As DateTime
        Dim band As Steema.TeeChart.Tools.ColorBand
        Dim color As Drawing.Color
        Dim isNaNPeriod, nanFound, nanFoundInSeries As Boolean

        'set default color
        color = Color.Red

        If View.ToolStripButton_ShowNaNValues.Checked Then
            'Switch visualization of NaN values on
            'Show color bands for NaN values in the currently active series
            nanFound = False
            For Each ts As TimeSeries In _model.TimeSeriesDict.Values
                processSeries = False
                'check if time series is currently active
                For Each series As Steema.TeeChart.Styles.Series In View.TChart1.Series
                    If series.Title = ts.Title Then
                        If series.Active Then
                            'process this series
                            processSeries = True
                            color = series.Color
                        End If
                        Exit For
                    End If
                Next
                If processSeries Then
                    'log
                    Log.AddLogEntry(Log.levels.info, $"Finding NaN values for series {ts.Title}...")
                    'find beginning and end of nan values
                    nanFoundInSeries = False
                    isNaNPeriod = False
                    For i As Integer = 0 To ts.Length - 1
                        If Not isNaNPeriod Then
                            'test for start of NaN values
                            If Double.IsNaN(ts.Values(i)) Then
                                isNaNPeriod = True
                                nanFoundInSeries = True
                                nanFound = True
                                If i = 0 Then
                                    bandStart = ts.Dates(i)
                                Else
                                    bandStart = ts.Dates(i - 1)
                                End If
                                nanStart = ts.Dates(i)

                                If i < ts.Length - 1 Then
                                    Continue For
                                End If
                            End If
                        End If
                        If isNaNPeriod Then
                            'test for end of NaN values
                            If Not Double.IsNaN(ts.Values(i)) Then
                                bandEnd = ts.Dates(i)
                                nanEnd = ts.Dates(i - 1)
                                isNaNPeriod = False

                            ElseIf i = ts.Length - 1 Then
                                'force end if end of time series reached
                                bandEnd = ts.Dates(i)
                                nanEnd = ts.Dates(i)
                                isNaNPeriod = False

                            End If

                            If Not isNaNPeriod Then
                                'end of NaN period reached, add a color band
                                band = New Steema.TeeChart.Tools.ColorBand()
                                View.TChart1.Tools.Add(band)
                                band.Axis = View.TChart1.Axes.Bottom
                                band.Start = bandStart.ToOADate()
                                band.End = bandEnd.ToOADate()
                                band.Pen.Visible = False
                                band.Pen.Color = color
                                band.Brush.Color = ControlPaint.Light(color)
                                band.Brush.Transparency = 50
                                band.ResizeEnd = False
                                band.ResizeStart = False
                                band.EndLinePen.Visible = False
                                band.StartLinePen.Visible = False
                                band.Tag = "NaN"

                                'write to log
                                Log.AddLogEntry(Log.levels.info, $"Series contains NaN values from {nanStart.ToString(Helpers.DefaultDateFormat)} to {nanEnd.ToString(Helpers.DefaultDateFormat)}")
                            End If
                        End If
                    Next
                    If Not nanFoundInSeries Then
                        Log.AddLogEntry(Log.levels.info, "Series does not contain any NaN values")
                    End If
                End If
            Next
            If Not nanFound Then
                MsgBox("No NaN values found in the currently active series!", MsgBoxStyle.Information)
                View.ToolStripButton_ShowNaNValues.Checked = False
            End If
        Else
            'Switch visualization of NaN values off
            'Remove all tools of type ColorBand with Tag "NaN" from TChart1
            Dim nanbands As New List(Of Steema.TeeChart.Tools.ColorBand)
            For Each tool As Steema.TeeChart.Tools.Tool In View.TChart1.Tools
                If tool.GetType Is GetType(Steema.TeeChart.Tools.ColorBand) And tool.Tag = "NaN" Then
                    nanbands.Add(tool)
                End If
            Next
            For Each nanband As Steema.TeeChart.Tools.ColorBand In nanbands
                View.TChart1.Tools.Remove(nanband)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Convert error values button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConvertErrorValues_Click(sender As System.Object, e As System.EventArgs)
        'Abort if no time series available!
        If (_model.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim dlg As New ConvertErrorValuesDialog(_model.TimeSeriesDict.Values.ToList)
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

        Dim dlgResult As DialogResult
        Dim ids As List(Of Integer)
        Dim ts As TimeSeries

        dlgResult = MsgBox("Delete all nodes with NaN values from all series?", MsgBoxStyle.OkCancel)
        If dlgResult = Windows.Forms.DialogResult.OK Then
            ids = _model.TimeSeriesDict.Keys.ToList()
            'loop over time series
            For Each id As Integer In ids
                'remove NaN values
                ts = _model.TimeSeriesDict(id)
                ts = ts.removeNaNValues()
                _model.TimeSeriesDict(id) = ts
                'replace values of series in chart
                For Each series As Steema.TeeChart.Styles.Series In View.TChart1.Series
                    If series.Tag = id Then
                        series.BeginUpdate()
                        series.Clear()
                        For Each kvp As KeyValuePair(Of DateTime, Double) In ts.Nodes
                            series.Add(kvp.Key, kvp.Value)
                        Next
                        series.EndUpdate()
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub

    'Drucken
    '*******
    Private Sub Print_Click(sender As System.Object, e As System.EventArgs)
        Call View.TChart1.Printer.Preview()
    End Sub

    'Kopieren (als PNG)
    '******************
    Private Sub Copy_Click(sender As System.Object, e As System.EventArgs)
        Call View.TChart1.Export.Image.PNG.CopyToClipboard()
    End Sub

    'Analysieren
    '***********
    Private Sub Analysis_Click(sender As System.Object, e As System.EventArgs)

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (_model.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for analysis!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim oAnalysisDialog As New AnalysisDialog(_model.TimeSeriesDict.Values.ToList)

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

                Call Log.AddLogEntry(Log.levels.info, "... executing analysis ...")

                'Analyse ausführen
                Call oAnalysis.ProcessAnalysis()

                Call Log.AddLogEntry(Log.levels.info, "... preparing analysis result ...")

                'Ergebnisse aufbereiten
                Call oAnalysis.PrepareResults()

                Call Log.AddLogEntry(Log.levels.info, "Analysis complete")

                'Default-Cursor
                View.Cursor = Cursors.Default

                'Ergebnisse anzeigen:
                '--------------------
                'Ergebnisdiagramm anzeigen
                If (oAnalysis.hasResultChart) Then
                    Dim resultChart As New AnalysisChart()
                    resultChart.Text &= " - " & oAnalysisDialog.selectedAnalysisFunction.ToString()
                    resultChart.TChart1.Chart = oAnalysis.getResultChart()
                    Call resultChart.Show()
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
                View.Cursor = Cursors.Default
                'Logeintrag
                Call Log.AddLogEntry(Log.levels.error, "Analysis failed:" & eol & ex.Message)
                'Alert
                MsgBox("Analysis failed:" & eol & ex.Message, MsgBoxStyle.Critical)
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
            Dim extent As Tuple(Of Double, Double) = ZoomHistory(prevIndex)
            View.ChartMinX = DateTime.FromOADate(extent.Item1)
            View.ChartMaxX = DateTime.FromOADate(extent.Item2)
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
            Dim extent As Tuple(Of Double, Double) = ZoomHistory(ZoomHistoryIndex + 1)
            View.ChartMinX = DateTime.FromOADate(extent.Item1)
            View.ChartMaxX = DateTime.FromOADate(extent.Item2)
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
        ReDim items(_model.TimeSeriesDict.Count - 1)
        i = 0
        For Each ts As TimeSeries In _model.TimeSeriesDict.Values
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

        If _model.TimeSeriesDict.ContainsKey(id) Then
            startdate = _model.TimeSeriesDict(id).StartDate
            enddate = _model.TimeSeriesDict(id).EndDate
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
            View.TChart1.Axes.Left.Automatic = True
            View.TChart1.Axes.Right.Automatic = True
            For Each axis As Steema.TeeChart.Axis In View.TChart1.Axes.Custom
                axis.Automatic = True
            Next
            View.TChart1.Refresh()
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
        For Each ts As TimeSeries In _model.TimeSeriesDict.Values
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
            View.TChart1.Series.RemoveAllSeries()
            View.TChart2.Series.RemoveAllSeries()
            View.TChart1.Refresh()
            View.TChart2.Refresh()

            'Collection zurücksetzen
            _model.TimeSeriesDict.Clear()

            'Alle Dateien durchlaufen
            Dim success As Boolean
            For Each file In datasources.Keys

                Log.AddLogEntry(Log.levels.info, $"Reading file {file} ...")

                success = True

                If IO.Path.GetExtension(file).ToUpper() = FileFactory.FileExtTEN Then
                    'TODO: this currently loads all series from the TEN, instead of only the currently loaded ones
                    Call _model.Load_TEN(file)
                Else
                    'get an instance of the file
                    Dim fileObj As FileFormatBase = FileFactory.getFileInstance(file)
                    'select series for importing
                    For Each title In datasources(file)
                        success = success And fileObj.selectSeries(title)
                    Next
                    'load the file
                    Call fileObj.readFile()
                    'import the series
                    For Each ts As TimeSeries In fileObj.FileTimeSeries.Values
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
    ''' Check for update menu item or update notification button clicked
    ''' </summary>
    Private Async Sub CheckForUpdate_Click(sender As Object, e As EventArgs)
        Try
            Dim updateAvailable As Boolean = Await _model.CheckForUpdate()
            If updateAvailable Then
                View.ToolStripButton_UpdateNotification.Visible = True
                Dim resp As MsgBoxResult = MsgBox($"A new version is available!{eol}Click OK to go to downloads.bluemodel.org to get it.", MsgBoxStyle.OkCancel)
                If resp = MsgBoxResult.Ok Then
                    Process.Start(urlDownload)
                End If
            Else
                View.ToolStripButton_UpdateNotification.Visible = False
                MsgBox("You are already up to date!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Error while checking for update:" & eol & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ''' <summary>
    ''' About Click
    ''' </summary>
    Private Sub About_Click(sender As System.Object, e As System.EventArgs)
        Dim about As New AboutBox()
        Call about.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Hilfe Click (URL zum Wiki)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Help_Click(sender As System.Object, e As System.EventArgs)
        Process.Start(urlHelp)
    End Sub

    ''' <summary>
    ''' Release Notes click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReleaseNotes_Click(sender As Object, e As EventArgs)
        Dim filepath As String
        filepath = IO.Path.Combine(Application.StartupPath, "BlueM.Wave_Release-notes.txt")
        Try
            System.Diagnostics.Process.Start(filepath)
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
    Private Sub navigation_Click(sender As System.Object, e As System.EventArgs)

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
    Private Sub TChart1_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Left Then
            'start zoom process
            If View.TChart1.Series.Count > 0 Then

                Dim startValue As Double
                startValue = View.TChart1.Series(0).XScreenToValue(e.X)

                If startValue < View.ChartMinX.ToOADate() Or
                    startValue > View.ChartMaxX.ToOADate() Then
                    'click outside of chart, don't start zoom process
                    Exit Sub
                End If

                View.TChart1.Cursor = View.cursor_zoom
                Call Me.SaveZoomSnapshot()

                Me.ChartMouseZoomDragging = True
                Me.ChartMouseDragStartX = e.X

                View.colorBandZoom.Chart = View.TChart1.Chart
                View.colorBandZoom.Active = True
                View.colorBandZoom.Start = startValue
                View.colorBandZoom.End = startValue

                Log.AddLogEntry(Log.levels.debug, "Zoom start at " & DateTime.FromOADate(startValue))
            End If

        ElseIf e.Button = MouseButtons.Right Then
            'start pan process
            Me.ChartMousePanning = True
            Me.ChartMouseDragStartX = e.X
            Me.ChartMousePanDisplayRange = View.TChart1.Axes.Bottom.Maximum - View.TChart1.Axes.Bottom.Minimum
            Call Me.SaveZoomSnapshot()
            View.TChart1.Cursor = View.cursor_pan
        End If

    End Sub

    ''' <summary>
    ''' Handles main chart MouseMove event
    ''' Animates any started zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TChart1_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)

        If Me.ChartMouseZoomDragging Then
            Dim endValue As Double
            endValue = View.TChart1.Series(0).XScreenToValue(e.X)
            View.colorBandZoom.End = endValue

        ElseIf Me.ChartMousePanning Then
            Dim xMin, xMax As Double
            Dim panDistance As Double = View.TChart1.Series(0).XScreenToValue(Me.ChartMouseDragStartX) - View.TChart1.Series(0).XScreenToValue(e.X)
            xMin = View.TChart1.Axes.Bottom.Minimum + panDistance
            xMax = View.TChart1.Axes.Bottom.Maximum + panDistance
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
            Call Me.ViewportChanged()
            'update drag start point
            Me.ChartMouseDragStartX = e.X
        End If
    End Sub

    ''' <summary>
    ''' Handles main chart MouseUp event
    ''' Complete any started zoom or pan process, update cursor
    ''' </summary>
    Private Sub TChart1_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        If Me.ChartMouseZoomDragging Then
            'complete the zoom process
            Me.ChartMouseZoomDragging = False
            'only zoom if at least 5 pixels difference to start of drag operation
            If Math.Abs(e.X - Me.ChartMouseDragStartX) > 5 Then
                'determine start and end dates of zoom
                Dim mouseValue, startValue, endValue As Double
                'prevent zooming beyond the displayable date range (#68)
                mouseValue = View.TChart1.Series(0).XScreenToValue(e.X)
                mouseValue = Math.Max(mouseValue, Constants.minOADate.ToOADate)
                mouseValue = Math.Min(mouseValue, Constants.maxOADate.ToOADate)
                'set start and end depending on zoom direction
                If e.X > Me.ChartMouseDragStartX Then
                    startValue = View.TChart1.Series(0).XScreenToValue(Me.ChartMouseDragStartX)
                    endValue = mouseValue
                Else
                    startValue = mouseValue
                    endValue = View.TChart1.Series(0).XScreenToValue(Me.ChartMouseDragStartX)
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
            'hide colorband
            View.colorBandZoom.Active = False
        ElseIf Me.ChartMousePanning Then
            'complete the pan process
            Me.ChartMousePanning = False
        End If
        View.TChart1.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Handles OverviewChart MouseDown event
    ''' Starts the zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseDown(sender As Object, e As MouseEventArgs)

        If View.TChart2.Series.Count > 0 Then

            If e.Button = MouseButtons.Left Then
                'start zoom process

                View.TChart2.Cursor = View.cursor_zoom

                Me.OverviewChartMouseDragging = True
                Me.OverviewChartMouseDragStartX = e.X

                'set start and end value of colorband to mouse position
                Dim xMouse As Double
                xMouse = View.TChart2.Series(0).XScreenToValue(Me.OverviewChartMouseDragStartX)

                'prevent zoom starting beyond displayable date range (#68)
                xMouse = Math.Max(xMouse, Constants.minOADate.ToOADate)
                xMouse = Math.Min(xMouse, Constants.maxOADate.ToOADate)

                View.colorBandOverview.Start = xMouse
                View.colorBandOverview.End = xMouse

                Log.AddLogEntry(Log.levels.debug, "Zoom start at " & DateTime.FromOADate(xMouse))

            ElseIf e.Button = MouseButtons.Right Then
                'start panning process

                View.TChart2.Cursor = View.cursor_pan

                Me.OverviewChartMouseDragging = True
                Me.OverviewChartMouseDragStartX = e.X
                Me.OverviewChartMouseDragOffset = e.X - View.TChart2.Series(0).ValuePointToScreenPoint(View.colorBandOverview.Start, 0).X

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
                Dim xMouse As Double = View.TChart2.Series(0).XScreenToValue(e.X)

                'restrict to displayable date range (#68)
                xMouse = Math.Max(xMouse, Constants.minOADate.ToOADate)
                xMouse = Math.Min(xMouse, Constants.maxOADate.ToOADate)

                View.colorBandOverview.End = xMouse

            ElseIf e.Button = MouseButtons.Right Then
                'move the whole color band while maintaining its width
                Dim width As Double = View.colorBandOverview.End - View.colorBandOverview.Start

                Dim startValue, endValue As Double
                startValue = View.TChart2.Series(0).XScreenToValue(e.X - Me.OverviewChartMouseDragOffset)
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

                View.colorBandOverview.Start = startValue
                View.colorBandOverview.End = endValue
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

        View.TChart2.Cursor = Cursors.Default

        If Me.OverviewChartMouseDragging Then

            Me.OverviewChartMouseDragging = False

            If e.Button = MouseButtons.Left Then
                'complete the zoom process

                'determine start and end of zoom
                If e.X <> Me.OverviewChartMouseDragStartX Then
                    'set start and end depending on zoom direction
                    Dim startValue, endValue As Double
                    If e.X > Me.OverviewChartMouseDragStartX Then
                        startValue = View.TChart2.Series(0).XScreenToValue(Me.OverviewChartMouseDragStartX)
                        endValue = View.TChart2.Series(0).XScreenToValue(e.X)
                    Else
                        startValue = View.TChart2.Series(0).XScreenToValue(e.X)
                        endValue = View.TChart2.Series(0).XScreenToValue(Me.OverviewChartMouseDragStartX)
                    End If
                    'restrict to displayable date range (#68)
                    startValue = Math.Max(startValue, Constants.minOADate.ToOADate)
                    endValue = Math.Min(endValue, Constants.maxOADate.ToOADate)

                    Log.AddLogEntry(Log.levels.debug, "Zoom end at " & DateTime.FromOADate(endValue))

                    'adjust colorband
                    View.colorBandOverview.Start = startValue
                    View.colorBandOverview.End = endValue

                    'save the current zoom snapshot
                    Call Me.SaveZoomSnapshot()

                    'set the new viewport on the main chart
                    View.ChartMinX = DateTime.FromOADate(View.colorBandOverview.Start)
                    View.ChartMaxX = DateTime.FromOADate(View.colorBandOverview.End)

                    Me.selectionMade = True
                    Call Me.ViewportChanged()
                End If

            ElseIf e.Button = MouseButtons.Right Then
                'complete the pan process

                'save the current zoom snapshot
                Call Me.SaveZoomSnapshot()

                'set the new viewport on the main chart
                View.ChartMinX = DateTime.FromOADate(View.colorBandOverview.Start)
                View.ChartMaxX = DateTime.FromOADate(View.colorBandOverview.End)

                Me.selectionMade = True
                Call Me.ViewportChanged()
            End If
        End If
    End Sub

    'TChart2 DoubleClick
    '*******************
    Private Sub TChart2_DoubleClick(sender As System.Object, e As System.EventArgs)
        Call Steema.TeeChart.Editor.Show(View.TChart2)
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
            ZoomHistory.Add(New Tuple(Of Double, Double)(View.ChartMinX.ToOADate(), View.ChartMaxX.ToOADate()))
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

            Dim startdate, enddate As DateTime
            Dim title As String
            Dim seriesMin, seriesMax, Ymin, Ymax As Double
            Dim axisType As Steema.TeeChart.Styles.VerticalAxis
            Dim axis As Steema.TeeChart.Axis

            'get start and end date of current viewport
            startdate = View.ChartMinX
            enddate = View.ChartMaxX

            'define axes to process
            Dim axes As New List(Of Tuple(Of Steema.TeeChart.Styles.VerticalAxis, Steema.TeeChart.Axis))
            axes.Add(New Tuple(Of Steema.TeeChart.Styles.VerticalAxis, Steema.TeeChart.Axis)(Steema.TeeChart.Styles.VerticalAxis.Left, View.TChart1.Axes.Left))
            axes.Add(New Tuple(Of Steema.TeeChart.Styles.VerticalAxis, Steema.TeeChart.Axis)(Steema.TeeChart.Styles.VerticalAxis.Right, View.TChart1.Axes.Right))
            For Each axis In View.TChart1.Axes.Custom
                axes.Add(New Tuple(Of Steema.TeeChart.Styles.VerticalAxis, Steema.TeeChart.Axis)(Steema.TeeChart.Styles.VerticalAxis.Custom, axis))
            Next

            'loop over Y-axes
            For Each t As Tuple(Of Steema.TeeChart.Styles.VerticalAxis, Steema.TeeChart.Axis) In axes
                axisType = t.Item1
                axis = t.Item2

                'loop over series
                Ymin = Double.MaxValue
                Ymax = Double.MinValue
                For Each ts As TimeSeries In _model.TimeSeriesDict.Values
                    title = ts.Title

                    'only process active series on the current axis
                    If View.TChart1.Series.WithTitle(title).Active And View.TChart1.Series.WithTitle(title).VertAxis = axisType Then

                        If axisType = Steema.TeeChart.Styles.VerticalAxis.Custom And ts.Unit <> axis.Tag Then
                            'series is on a different custom axis, skip it
                            Continue For
                        End If

                        'get series min and max for current viewport
                        seriesMin = ts.Minimum(startdate, enddate)
                        If seriesMin < Ymin Then
                            Ymin = seriesMin
                        End If
                        seriesMax = ts.Maximum(startdate, enddate)
                        If seriesMax > Ymax Then
                            Ymax = seriesMax
                        End If
                    End If
                Next

                'set new Y axis bounds
                If Ymin < Double.MaxValue Then
                    axis.AutomaticMinimum = False
                    axis.Minimum = Ymin
                End If
                If Ymax > Double.MinValue Then
                    axis.AutomaticMaximum = False
                    axis.Maximum = Ymax
                End If
            Next
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

        If (_model.TimeSeriesDict.Count = 0) Then
            'just refresh
            View.TChart1.Refresh()
            View.TChart2.Refresh()

        Else
            'Update X-Axis and colorBand

            'Determine maximum extent of chart
            Dim startdates As New List(Of DateTime)
            Dim enddates As New List(Of DateTime)
            For Each zre As TimeSeries In _model.TimeSeriesDict.Values
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
            View.TChart2.Axes.Bottom.Minimum = Xmin.ToOADate()
            View.TChart2.Axes.Bottom.Maximum = Xmax.ToOADate()

            If (Not Me.selectionMade) Then
                'Wenn noch nicht gezoomed wurde, Gesamtzeitraum auswählen
                View.ChartMinX = Xmin
                View.ChartMaxX = Xmax
            End If
            'Extent auf Colorband übertragen
            Call Me.UpdateOverviewZoomExtent()

        End If

    End Sub

    ''' <summary>
    ''' Updates the colorband to correspond to the currently displayed timespan of the main chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateOverviewZoomExtent()
        View.colorBandOverview.Start = View.ChartMinX.ToOADate()
        View.colorBandOverview.End = View.ChartMaxX.ToOADate()
    End Sub

    ''' <summary>
    ''' Handles a new series being added to the model
    ''' Adds the series to the charts
    ''' Also adds the datasource to the MRU file list if the time series has a file datasource
    ''' </summary>
    ''' <param name="ts">Die anzuzeigende Zeitreihe</param>
    Private Sub SeriesAdded(ts As TimeSeries)

        'Remove nodes if necessary (#68)
        If ts.StartDate < Constants.minOADate Then
            ts = ts.Clone()
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
            Log.AddLogEntry(Log.levels.warning, $"Unable to display {t_too_early.Count} nodes between {t_too_early.First().ToString(Helpers.DefaultDateFormat)} and {t_too_early.Last().ToString(Helpers.DefaultDateFormat)}!")
        End If
        If ts.EndDate > Constants.maxOADate Then
            ts = ts.Clone()
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
            Log.AddLogEntry(Log.levels.warning, $"Unable to display {t_too_late.Count} nodes between {t_too_late.Last().ToString(Helpers.DefaultDateFormat)} and {t_too_late.First().ToString(Helpers.DefaultDateFormat)}!")
        End If

        'Serie zu Diagramm hinzufügen

        'Linien instanzieren
        Dim Line1 As New Steema.TeeChart.Styles.Line(View.TChart1.Chart)
        Dim Line2 As New Steema.TeeChart.Styles.FastLine(View.TChart2.Chart)

        'Do not paint NaN values
        Line1.TreatNaNAsNull = True
        Line2.TreatNaNAsNull = True
        Line1.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.DoNotPaint
        Line2.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.DoNotPaint

        'X-Werte als Zeitdaten einstellen
        Line1.XValues.DateTime = True
        Line2.XValues.DateTime = True

        'Store id as Tag property
        Line1.Tag = ts.Id
        Line2.Tag = ts.Id

        'Namen vergeben
        Line1.Title = ts.Title
        Line2.Title = ts.Title

        'Set line width to 2
        Line1.LinePen.Width = 2
        Line2.LinePen.Width = 2

        'Stützstellen zur Serie hinzufügen
        'Main chart
        Line1.BeginUpdate()
        Line1.Add(ts.Dates.ToArray(), ts.Values.ToArray())
        Line1.EndUpdate()

        'Overview chart
        Line2.BeginUpdate()
        If Double.IsNaN(ts.FirstValue) Then
            'TeeChart throws an OverflowException when attemtping to display a FastLine that begins with a NaN value as a step function!
            'To avoid this we generally do not add NaN values at the beginning of the time series to the FastLine (#67)
            Dim isNaN As Boolean = True
            For Each node As KeyValuePair(Of DateTime, Double) In ts.Nodes
                If isNaN Then
                    isNaN = isNaN And Double.IsNaN(node.Value)
                End If
                If Not isNaN Then
                    Line2.Add(node.Key, node.Value)
                End If
            Next
        Else
            Line2.Add(ts.Dates.ToArray(), ts.Values.ToArray())
        End If
        Line2.EndUpdate()

        'Determine total number of NaN-values and write to log
        If ts.NaNCount > 0 Then
            Log.AddLogEntry(Log.levels.warning, $"Series '{ts.Title}' contains {ts.NaNCount} NaN values!")
        End If

        'Y-Achsenzuordnung
        assignSeriesToAxis(Line1, ts.Unit)

        'Interpretation
        Select Case ts.Interpretation
            Case TimeSeries.InterpretationEnum.BlockRight
                Line1.Stairs = True
                Line2.Stairs = True
            Case TimeSeries.InterpretationEnum.BlockLeft,
                TimeSeries.InterpretationEnum.CumulativePerTimestep
                Line1.Stairs = True
                Line2.Stairs = True
                Line1.InvertedStairs = True
                Line2.InvertedStairs = True
        End Select

        'Charts aktualisieren
        Call Me.UpdateChartExtents()

        Call Me.ViewportChanged()

        'add datasource filename to Recently Used Files menu
        If ts.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport Then
            'remove if already present
            Dim i As Integer = 0
            For Each _item As ToolStripItem In View.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems
                If _item.Text = ts.DataSource.FilePath Then
                    View.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.RemoveAt(i)
                    Exit For
                End If
                i += 1
            Next
            'add to top of list
            Dim item As New ToolStripMenuItem(ts.DataSource.FilePath)
            View.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.Insert(0, item)
        End If

    End Sub

    ''' <summary>
    ''' Assigns a series to the appropriate axis depending on its unit
    ''' If no axis exists for the given unit, a new axis is created
    ''' </summary>
    ''' <param name="unit">The unit</param>
    Private Sub assignSeriesToAxis(ByRef series As Steema.TeeChart.Styles.Series, unit As String)

        If IsNothing(View.TChart1.Axes.Left.Tag) Then
            'use left axis for the first time
            View.TChart1.Axes.Left.Title.Text = unit
            View.TChart1.Axes.Left.Tag = unit
            View.TChart1.Axes.Left.Visible = True
            View.TChart1.Axes.Left.Automatic = True
            View.TChart1.Axes.Left.MaximumOffset = 5
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left

        ElseIf View.TChart1.Axes.Left.Tag = unit Then
            'reuse left axis
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left

        ElseIf IsNothing(View.TChart1.Axes.Right.Tag) Then
            'use right axis for the first time
            View.TChart1.Axes.Right.Title.Text = unit
            View.TChart1.Axes.Right.Tag = unit
            View.TChart1.Axes.Right.Visible = True
            View.TChart1.Axes.Right.Automatic = True
            View.TChart1.Axes.Right.MaximumOffset = 5
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right

        ElseIf View.TChart1.Axes.Right.Tag = unit Then
            'reuse right axis
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
        Else
            'check for reusable custom axes
            Dim axisFound As Boolean = False
            For Each axis As Steema.TeeChart.Axis In View.TChart1.Axes.Custom
                If axis.Tag = unit Then
                    series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom
                    series.CustomVertAxis = axis
                    axisFound = True
                    Exit For
                End If
            Next
            If Not axisFound Then
                'create a new custom axis
                Dim axis As Steema.TeeChart.Axis
                Dim number As Integer = View.TChart1.Axes.Custom.Count + 1
                axis = New Steema.TeeChart.Axis(View.TChart1.Chart)
                View.TChart1.Axes.Custom.Add(axis)
                axis.Labels.Font.Name = "GenericSansSerif"
                axis.Labels.Font.Color = Color.Black
                axis.Labels.Font.Size = 10
                axis.Title.Font.Name = "GenericSansSerif"
                axis.Title.Font.Color = Color.Black
                axis.Title.Font.Size = 10
                axis.Title.Text = unit
                axis.Title.Angle = 90
                axis.Tag = unit
                axis.Visible = True
                axis.Automatic = True
                axis.MaximumOffset = 5
                axis.AxisPen.Visible = True
                axis.Grid.Visible = False
                'Place every second axis on the right
                If number Mod 2 = 0 Then
                    axis.OtherSide = True
                End If
                'Calculate position
                axis.RelativePosition = Math.Ceiling((number) / 2) * 8
                'assign series to new axis
                series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom
                series.CustomVertAxis = axis

                'update axis dialog
                Call Me.UpdateAxisDialog()
            End If
        End If

    End Sub

    ''' <summary>
    ''' Shows markers at the given timestamps in the chart
    ''' </summary>
    ''' <param name="timestamps">List of timestamps for which to show markers</param>
    Private Sub showMarkers(timestamps As List(Of DateTime))

        'Remove any existing marker series
        For i As Integer = View.TChart1.Series.Count - 1 To 0 Step -1
            Try
                If CType(View.TChart1.Series(i).Tag, String) = "_markers" Then
                    View.TChart1.Series.RemoveAt(i)
                End If
            Catch ex As Exception
                Log.AddLogEntry(Log.levels.debug, ex.Message)
            End Try
        Next
        View.TChart1.Refresh()

        If timestamps.Count = 0 Then
            Exit Sub
        End If

        'loop over series and create a marker series for each
        For i As Integer = 0 To View.TChart1.Series.Count - 1
            Try
                Dim series As Steema.TeeChart.Styles.Series = View.TChart1.Series(i)
                If Not series.Active Then
                    'do not display markers for inactive series
                    Continue For
                End If
                'collect all non-NaN values to display as markers
                Dim markerValues As New Dictionary(Of DateTime, Double)
                For Each t As DateTime In timestamps
                    Dim index As Integer = series.XValues.IndexOf(t.ToOADate)
                    If index <> -1 Then
                        If Not series.IsNull(index) Then
                            markerValues.Add(t, series.YValues(index))
                        End If
                    End If
                Next
                If markerValues.Count > 0 Then
                    'create a new point series for markers
                    Dim markers As New Steema.TeeChart.Styles.Points(View.TChart1.Chart)
                    markers.Legend.Visible = False
                    markers.Title = $"{series.Title} (selection)"
                    markers.Tag = "_markers"
                    markers.VertAxis = series.VertAxis
                    If series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Custom Then
                        markers.CustomVertAxis = series.CustomVertAxis
                    End If
                    markers.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
                    markers.Pointer.Brush.Visible = False
                    markers.Color = series.Color
                    markers.Pointer.Color = series.Color
                    markers.Pointer.Pen.Color = series.Color
                    markers.Pointer.Pen.Width = 2
                    markers.Marks.Visible = True
                    markers.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value
                    'markers.Marks.OnTop = True 'causes crash when markers are panned out of view on the left
                    markers.Marks.Callout.Visible = True
                    markers.Marks.FontSeriesColor = True
                    markers.Marks.Arrow.Visible = False
                    markers.Marks.Callout.Visible = False
                    markers.Marks.Callout.InflateMargins = False
                    markers.Marks.ArrowLength = 5
                    'add data points
                    For Each t As DateTime In markerValues.Keys
                        markers.Add(t, markerValues(t))
                    Next
                End If
            Catch ex As Exception
                Log.AddLogEntry(Log.levels.debug, ex.Message)
            End Try
        Next

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

    Private Sub SeriesPropertiesChanged(id)
        'find series in chart
        For Each series As Steema.TeeChart.Styles.Series In View.TChart1.Series
            If series.Tag = id Then
                'set line display according to interpretation
                If TypeOf series Is Steema.TeeChart.Styles.Line Then
                    Dim seriesline As Steema.TeeChart.Styles.Line = series
                    Select Case _model.TimeSeriesDict(id).Interpretation
                        Case TimeSeries.InterpretationEnum.Instantaneous,
                             TimeSeries.InterpretationEnum.Undefined
                            seriesline.Stairs = False
                            seriesline.InvertedStairs = False
                        Case TimeSeries.InterpretationEnum.BlockRight
                            seriesline.Stairs = True
                            seriesline.InvertedStairs = False
                        Case TimeSeries.InterpretationEnum.BlockLeft,
                             TimeSeries.InterpretationEnum.CumulativePerTimestep
                            seriesline.Stairs = True
                            seriesline.InvertedStairs = True
                    End Select
                End If

                'update title in chart
                series.Title = _model.TimeSeriesDict(id).Title

                'assign to axis according to unit
                assignSeriesToAxis(series, _model.TimeSeriesDict(id).Unit)

                'TODO: apply the same changes in the overview chart?
                Exit For
            End If
        Next

    End Sub

    Private Sub SeriesRemoved(id As Integer)

        'Remove series from main chart
        'FIXME: the event may have originated from the chartlistbox, in which case the series is going to be removed by TChart, removing it here causes a second removal!
        For i As Integer = View.TChart1.Series.Count - 1 To 0 Step -1
            'FIXME: some series may be marker series without an id!
            If (View.TChart1.Series.Item(i).Tag = id) Then
                View.TChart1.Series.RemoveAt(i)
                View.TChart1.Refresh()
                Exit For
            End If
        Next

        'Remove series from overview chart
        For i As Integer = View.TChart2.Series.Count - 1 To 0 Step -1
            If (View.TChart2.Series.Item(i).Tag = id) Then
                View.TChart2.Series.RemoveAt(i)
                View.TChart2.Refresh()
                Exit For
            End If
        Next

        'update properties dialog
        'FIXME: _propDialog.Update(_model.TimeSeriesDict.Values.ToList)
    End Sub

    ''' <summary>
    ''' Handles axis deleted in the AxisDialog
    ''' </summary>
    ''' <param name="axisname"></param>
    Private Sub axisDeleted(axisname As String)
        Dim axisnumber As Integer
        Dim m As Match = Regex.Match(axisname, "Custom (\d+)")
        If m.Success Then
            axisnumber = Integer.Parse(m.Groups(1).Value)
            'Delete axis from chart
            View.TChart1.Axes.Custom.RemoveAt(axisnumber)
            View.TChart1.Refresh()
            'update axis dialog
            Call Me.UpdateAxisDialog()
        End If
    End Sub

    ''' <summary>
    ''' Handles axis unit changed in the AxisDialog
    ''' </summary>
    ''' <remarks>Reassigns all series to their appropriate axis</remarks>
    Private Sub AxisUnitChanged()

        For Each series As Steema.TeeChart.Styles.Series In View.TChart1.Series
            assignSeriesToAxis(series, _model.TimeSeriesDict(series.Tag).Unit)
        Next

        'deactivate unused custom axes
        Dim unitUsed As Boolean
        For Each axis As Steema.TeeChart.Axis In View.TChart1.Axes.Custom
            unitUsed = False
            For Each ts As TimeSeries In _model.TimeSeriesDict.Values
                If ts.Unit = axis.Tag Then
                    unitUsed = True
                    Exit For
                End If
            Next
            If Not unitUsed Then
                axis.Visible = False
            End If
        Next

    End Sub

    ''' <summary>
    ''' Update AxisDialog
    ''' </summary>
    Private Sub UpdateAxisDialog()
        'Wrap Left, Right and Custom axes
        Dim axisList As New List(Of AxisWrapper)
        axisList.Add(New AxisWrapper("Left", View.TChart1.Axes.Left))
        axisList.Add(New AxisWrapper("Right", View.TChart1.Axes.Right))
        For i As Integer = 0 To View.TChart1.Axes.Custom.Count - 1
            axisList.Add(New AxisWrapper("Custom " & i, View.TChart1.Axes.Custom(i)))
        Next

        _axisDialog.Update(axisList)
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
    ''' <param name="files">array of file paths</param>
    ''' <remarks></remarks>
    Private Delegate Sub ImportDelegate(files() As String)

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

        'set colorpalette in charts
        View.TChart1.Chart.ColorPalette = colorPalette
        View.TChart2.Chart.ColorPalette = colorPalette

        'change colors of existing series
        Dim counter As Integer = 0
        For Each series As Steema.TeeChart.Styles.Series In View.TChart1.Series
            If counter >= colorPalette.Length Then
                'loop color palette
                counter = 0
            End If
            series.Color = colorPalette(counter)
            'apply same color to series in overview chart
            For Each series2 As Steema.TeeChart.Styles.Series In View.TChart2.Series
                If series2.Tag = series.Tag Then
                    series2.Color = colorPalette(counter)
                    Exit For
                End If
            Next
            counter += 1
        Next
    End Sub

End Class
