'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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
    ''' The Log instance shared among all Wave instances
    ''' </summary>
    Private WithEvents logInstance As Log

    'Dialogs
    Private WithEvents propDialog As PropertiesDialog
    Private WithEvents axisDialog As AxisDialog

    'Eigenschaften
    '#############

    ''' <summary>
    ''' Flag for preventing unintended feedback loops in the UI. Default is False, can be temporarily set to True.
    ''' </summary>
    ''' <remarks></remarks>
    Private isInitializing As Boolean

    ''' <summary>
    ''' Internal collection of time series {id: TimeSeries, ...}
    ''' </summary>
    Private TimeSeriesDict As Dictionary(Of Integer, TimeSeries)

    'Dateifilter
    Private Const FileFilter_TEN As String = "TeeChart files (*.ten)|*.ten"
    Private Const FileFilter_XML As String = "Theme files (*.xml)|*.xml"

    'Chart-Zeugs
    Private selectionMade As Boolean 'Flag zeigt an, ob bereits ein Auswahlbereich ausgewählt wurde
    Private WithEvents ChartListBox1 As Steema.TeeChart.ChartListBox

    'ColorBand that is shown while zooming in main chart
    Private colorBandZoom As Steema.TeeChart.Tools.ColorBand
    'State and variables for zoom in main chart
    Private ChartMouseZoomDragging As Boolean = False
    Private ChartMouseDragStartX As Double

    ''' <summary>
    ''' History of view extents [(xmin, xmax), ...]
    ''' </summary>
    Private ZoomHistory As List(Of Tuple(Of Double, Double))

    ''' <summary>
    ''' Index of current view extent in zoom history (may not be saved yet)
    ''' </summary>
    Private ZoomHistoryIndex As Integer

    'ColorBand representing current view extent of main chart in OverviewChart
    Private colorBandOverview As Steema.TeeChart.Tools.ColorBand
    'State and variables for zoom/pan in OverviewChart
    Private OverviewChartMouseDragging As Boolean = False
    Private OverviewChartMouseDragStartX As Double
    Private OverviewChartMouseDragOffset As Double

    'Cursors
    Friend cursor_pan As Cursor
    Friend cursor_zoom As Cursor

    Private Const urlHelp As String = "https://wiki.bluemodel.org/index.php/Wave"
    Private Const urlUpdateCheck As String = "https://downloads.bluemodel.org/BlueM.Wave/.version-latest"
    Private Const urlDownload As String = "https://downloads.bluemodel.org/?dir=BlueM.Wave"

#Region "Properties"

    ''' <summary>
    ''' Checks whether the option to auto-adjust the Y-axes to the current viewport is activated
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property AutoAdjustYAxes() As Boolean
        Get
            Return Me.ToolStripButton_AutoAdjustYAxes.Checked
        End Get
    End Property

#End Region 'Properties

#Region "Form behavior"

    'Konstruktor
    '***********
    Public Sub New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        'Kollektionen einrichten
        '-----------------------
        Me.TimeSeriesDict = New Dictionary(Of Integer, TimeSeries)()

        'Charts einrichten
        '-----------------
        Me.ChartListBox1 = New Steema.TeeChart.ChartListBox()
        Call Me.Init_Charts()

        'Dialogs
        If IsNothing(propDialog) Then
            propDialog = New PropertiesDialog()
        End If
        If IsNothing(axisDialog) Then
            axisDialog = New AxisDialog()
        End If

        'Zoom history
        Me.ZoomHistory = New List(Of Tuple(Of Double, Double))
        Me.ZoomHistoryIndex = 0

        'Log (Singleton) Instanz holen
        Me.logInstance = Log.getInstance()

        'Navigation initialisieren
        Me.ComboBox_NavIncrement.SelectedItem = "Days"

        'Instantiate cursors
        Me.cursor_pan = New Cursor(Me.GetType(), "cursor_pan.cur")
        Me.cursor_zoom = New Cursor(Me.GetType(), "cursor_zoom.cur")

    End Sub

    'Form wird geladen
    '*****************
    Private Async Sub Wave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Check for update
        Try
            Dim updateAvailable As Boolean
            updateAvailable = Await CheckForUpdate()
            If updateAvailable Then
                Me.ToolStripButton_UpdateNotification.Visible = True
            End If
        Catch ex As Exception
            'do nothing if check for update fails at startup
        End Try
    End Sub

    ''' <summary>
    ''' Checks for a newer version on the server
    ''' </summary>
    ''' <returns>True if a newer version is available</returns>
    Private Async Function CheckForUpdate() As Threading.Tasks.Task(Of Boolean)


        'get current version (only consider major, minor and build numbers, omitting the auto-generated revision number)
        Dim v As Version = Reflection.Assembly.GetExecutingAssembly.GetName().Version()
        Dim currentVersion As New Version(String.Format("{0}.{1}.{2}",
                                                        v.Major,
                                                        v.Minor,
                                                        v.Build))

        'retrieve latest version number from server
        Dim client As New Net.Http.HttpClient()
        Dim s As String = Await client.GetStringAsync(urlUpdateCheck)
        Dim latestVersion As New Version(s)
#If Not DEBUG Then
        'TODO: Logging is not thread-safe and causes an exception in debug mode!
        Log.AddLogEntry(Log.levels.debug, "CheckUpdate: Latest version on server: " & latestVersion.ToString())
#End If

        'compare versions
        If currentVersion < latestVersion Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Wenn sich der Log verändert hat, Statustext aktualisieren
    ''' </summary>
    Private Sub updateStatusLabel(level As Log.levels, msg As String) Handles logInstance.LogMsgAdded
        'remove any linebreak at the beginning
        If msg.StartsWith(eol) Then
            msg = msg.Substring(eol.Length)
        End If
        'if it is a multiline message use only the first line
        If msg.Contains(eol) Then
            msg = msg.Substring(0, msg.IndexOf(eol))
        End If
        'set text
        Me.ToolStripStatusLabel_Log.Text = msg.Trim()
        'set color according to level
        Select Case level
            Case Log.levels.debug
                Me.ToolStripStatusLabel_Log.LinkColor = Color.DarkGreen
            Case Log.levels.warning
                Me.ToolStripStatusLabel_Log.LinkColor = Color.DarkOrange
            Case Log.levels.error
                Me.ToolStripStatusLabel_Log.LinkColor = Color.Red
            Case Else
                Me.ToolStripStatusLabel_Log.LinkColor = SystemColors.ControlDarkDark
        End Select
        'Update counters
        If level = Log.levels.warning Then
            Me.ToolStripStatusLabel_Warnings.Text = Int(Me.ToolStripStatusLabel_Warnings.Text) + 1
            Me.ToolStripStatusLabel_Warnings.Image = My.Resources.warning
        ElseIf level = Log.levels.error Then
            Me.ToolStripStatusLabel_Errors.Text = Int(Me.ToolStripStatusLabel_Errors.Text) + 1
            Me.ToolStripStatusLabel_Errors.Image = My.Resources.cancel
        End If
        Call Application.DoEvents()
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

        If e.Control And e.KeyCode = Keys.V Then
            'Ctrl+V pressed
            Call Me.Import_Clipboard()
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
        Me.TChart2.Panel.Brush.Color = Color.FromArgb(239, 239, 239)
        Me.TChart2.Walls.Back.Color = Color.FromArgb(239, 239, 239)
        Me.TChart2.Header.Visible = False
        Me.TChart2.Legend.Visible = False

        'Übersicht darf nicht gescrolled oder gezoomt werden
        Me.TChart2.Zoom.Allow = False
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Hauptdiagramm darf nur horizontal gescrolled und nicht gezoomt werden
        'Zoom ist selbst implementiert
        Me.TChart1.Zoom.Allow = False
        Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
        Me.TChart1.Zoom.History = False
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
        Call Me.Init_ColorBands()

    End Sub

    ''' <summary>
    ''' Initialize color bands
    ''' </summary>
    Private Sub Init_ColorBands()

        colorBandOverview = New Steema.TeeChart.Tools.ColorBand()
        Me.TChart2.Tools.Add(colorBandOverview)
        colorBandOverview.Axis = Me.TChart2.Axes.Bottom
        colorBandOverview.Brush.Color = Color.Coral
        colorBandOverview.Brush.Transparency = 50
        colorBandOverview.ResizeEnd = False
        colorBandOverview.ResizeStart = False
        colorBandOverview.EndLinePen.Visible = False
        colorBandOverview.StartLinePen.Visible = False

        colorBandZoom = New Steema.TeeChart.Tools.ColorBand()
        Me.TChart1.Tools.Add(colorBandZoom)
        colorBandZoom.Axis = Me.TChart1.Axes.Bottom
        colorBandZoom.Color = Color.Black
        colorBandZoom.Pen.Color = Color.Black
        colorBandZoom.Pen.Style = Drawing2D.DashStyle.Dash
        colorBandZoom.Brush.Visible = False
        colorBandZoom.ResizeEnd = False
        colorBandZoom.ResizeStart = False
        colorBandZoom.EndLinePen.Visible = True
        colorBandZoom.StartLinePen.Visible = True
    End Sub

    ''' <summary>
    ''' Updates the extent of the x-axes of the charts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateChartExtents()

        Dim Xmin, Xmax As DateTime

        If (Me.TimeSeriesDict.Count = 0) Then
            'just refresh
            Me.TChart1.Refresh()
            Me.TChart2.Refresh()

        Else
            'Update X-Axis and colorBand

            'Min- und Max-Datum bestimmen
            Xmin = DateTime.MaxValue
            Xmax = DateTime.MinValue
            For Each zre As TimeSeries In Me.TimeSeriesDict.Values
                If zre.Length > 0 Then
                    If (zre.StartDate < Xmin) Then Xmin = zre.StartDate
                    If (zre.EndDate > Xmax) Then Xmax = zre.EndDate
                End If
            Next

            'Übersicht neu skalieren
            Me.TChart2.Axes.Bottom.Minimum = Xmin.ToOADate()
            Me.TChart2.Axes.Bottom.Maximum = Xmax.ToOADate()

            If (Not Me.selectionMade) Then
                'Wenn noch nicht gezoomed wurde, Gesamtzeitraum auswählen
                Me.TChart1.Axes.Bottom.Minimum = Xmin.ToOADate()
                Me.TChart1.Axes.Bottom.Maximum = Xmax.ToOADate()
            End If
            'Extent auf Colorband übertragen
            colorBandOverview.Start = Me.TChart1.Axes.Bottom.Minimum
            colorBandOverview.End = Me.TChart1.Axes.Bottom.Maximum

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
            Call Me.viewportChanged()
            Me.selectionMade = True
        End If
    End Sub

    ''' <summary>
    ''' Updates the colorband to correspond to the currently displayed timespan of the main chart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub updateOverviewZoomExtent()
        Me.colorBandOverview.Start = Me.TChart1.Axes.Bottom.Minimum
        Me.colorBandOverview.End = Me.TChart1.Axes.Bottom.Maximum
    End Sub

    ''' <summary>
    ''' Updates the navigation, the overview, and if activated auto-adjusts the Y-axes
    ''' to the currently displayed viewport
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub viewportChanged()

        'Update navigation
        Call Me.updateNavigation()

        'Update overview
        Call Me.updateOverviewZoomExtent()

        'Auto-adjust Y-axes to current viewport
        If Me.AutoAdjustYAxes Then

            Dim startdate, enddate As DateTime
            Dim title As String
            Dim seriesMin, seriesMax, Ymin, Ymax As Double
            Dim axisType As Steema.TeeChart.Styles.VerticalAxis
            Dim axis As Steema.TeeChart.Axis

            'get start and end date of current viewport
            startdate = DateTime.FromOADate(Me.TChart1.Axes.Bottom.Minimum)
            enddate = DateTime.FromOADate(Me.TChart1.Axes.Bottom.Maximum)

            'define axes to process
            Dim axes As New Dictionary(Of Steema.TeeChart.Styles.VerticalAxis, Steema.TeeChart.Axis)
            axes.Add(Steema.TeeChart.Styles.VerticalAxis.Left, Me.TChart1.Axes.Left)
            axes.Add(Steema.TeeChart.Styles.VerticalAxis.Right, Me.TChart1.Axes.Right)
            'TODO: auto-adjustment of custom axes

            'loop over Y-axes
            For Each kvp As KeyValuePair(Of Steema.TeeChart.Styles.VerticalAxis, Steema.TeeChart.Axis) In axes
                axisType = kvp.Key
                axis = kvp.Value

                'loop over series
                Ymin = Double.MaxValue
                Ymax = Double.MinValue
                For Each ts As TimeSeries In Me.TimeSeriesDict.Values
                    title = ts.Title

                    'only process active series on the current axis
                    If Me.TChart1.Series.WithTitle(title).Active _
                        And Me.TChart1.Series.WithTitle(title).VertAxis = axisType Then

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
    ''' Handles OverviewChart MouseDown event
    ''' Starts the zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseDown(sender As Object, e As MouseEventArgs) Handles TChart2.MouseDown

        Dim startValue As Double

        If Me.TChart2.Series.Count > 0 Then

            If e.Button = MouseButtons.Left Then
                'start zoom process

                Me.TChart2.Cursor = cursor_zoom

                Me.OverviewChartMouseDragging = True
                Me.OverviewChartMouseDragStartX = e.X

                startValue = Me.TChart2.Series(0).XScreenToValue(Me.OverviewChartMouseDragStartX)

                Me.colorBandOverview.Start = startValue
                Me.colorBandOverview.End = startValue

                Log.AddLogEntry(Log.levels.debug, "Zoom start at " & Date.FromOADate(startValue))

            ElseIf e.Button = MouseButtons.Right Then
                'start panning process

                Me.TChart2.Cursor = cursor_pan

                Me.OverviewChartMouseDragging = True
                Me.OverviewChartMouseDragStartX = e.X
                Me.OverviewChartMouseDragOffset = e.X - TChart2.Series(0).ValuePointToScreenPoint(Me.colorBandOverview.Start, 0).X

            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles OverviewChart MouseMove event
    ''' Animates any started zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseMove(sender As Object, e As MouseEventArgs) Handles TChart2.MouseMove
        If Me.OverviewChartMouseDragging Then
            If e.Button = MouseButtons.Left Then
                'move the end of the colorband
                Me.colorBandOverview.End = TChart2.Series(0).XScreenToValue(e.X)
            ElseIf e.Button = MouseButtons.Right Then
                'move the whole color band while maintaining its width
                Dim width As Double = Me.colorBandOverview.End - Me.colorBandOverview.Start
                Me.colorBandOverview.Start = TChart2.Series(0).XScreenToValue(e.X - Me.OverviewChartMouseDragOffset)
                Me.colorBandOverview.End = Me.colorBandOverview.Start + width
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles OverviewChart MouseUp event
    ''' Completes any started zoom or pan process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OverviewChart_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart2.MouseUp

        Me.TChart2.Cursor = Cursors.Default

        If Me.OverviewChartMouseDragging Then

            Me.OverviewChartMouseDragging = False

            If e.Button = MouseButtons.Left Then
                'complete the zoom process

                'determine start and end of zoom
                If e.X <> Me.OverviewChartMouseDragStartX Then
                    Dim startValue, endValue As Double
                    If e.X > Me.OverviewChartMouseDragStartX Then
                        startValue = TChart2.Series(0).XScreenToValue(Me.OverviewChartMouseDragStartX)
                        endValue = TChart2.Series(0).XScreenToValue(e.X)
                    Else
                        startValue = TChart2.Series(0).XScreenToValue(e.X)
                        endValue = TChart2.Series(0).XScreenToValue(Me.OverviewChartMouseDragStartX)
                    End If
                    Log.AddLogEntry(Log.levels.debug, "Zoom end at " & Date.FromOADate(endValue))

                    'adjust colorband
                    Me.colorBandOverview.Start = startValue
                    Me.colorBandOverview.End = endValue

                    'save the current zoom snapshot
                    Call Me.saveZoomSnapshot()

                    'set the new viewport on the main chart
                    Me.TChart1.Axes.Bottom.Minimum = Me.colorBandOverview.Start
                    Me.TChart1.Axes.Bottom.Maximum = Me.colorBandOverview.End

                    Me.selectionMade = True
                    Call Me.viewportChanged()
                End If

            ElseIf e.Button = MouseButtons.Right Then
                'complete the pan process

                'save the current zoom snapshot
                Call Me.saveZoomSnapshot()

                'set the new viewport on the main chart
                Me.TChart1.Axes.Bottom.Minimum = Me.colorBandOverview.Start
                Me.TChart1.Axes.Bottom.Maximum = Me.colorBandOverview.End

                Me.selectionMade = True
                Call Me.viewportChanged()
            End If
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
    ''' </remarks>
    Private Sub TChart1_SeriesRemoved(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChartListBox1.RemovedSeries

        Dim title As String
        Dim id_removed As Integer
        Dim s As Steema.TeeChart.Styles.Series
        Dim ids_remaining As New List(Of Integer)

        'collect ids of series currently still in the chart
        For Each s In Me.ChartListBox1.Items
            ids_remaining.Add(s.Tag)
        Next

        id_removed = -1

        'find the time series id missing in the chart
        For Each id As Integer In Me.TimeSeriesDict.Keys
            If Not ids_remaining.Contains(id) Then
                id_removed = id
            End If
        Next

        If id_removed <> -1 Then

            title = Me.TimeSeriesDict(id_removed).Title

            'remove from internal dictionary
            Me.TimeSeriesDict.Remove(id_removed)

            'remove from overview chart
            For i As Integer = Me.TChart2.Series.Count - 1 To 0 Step -1
                If (Me.TChart2.Series.Item(i).Tag = id_removed) Then
                    Me.TChart2.Series.RemoveAt(i)
                    Me.TChart2.Refresh()
                    Exit For
                End If
            Next

        End If

        'Update dialogs
        Me.propDialog.Update(Me.TimeSeriesDict.Values.ToList)
    End Sub

    ''' <summary>
    ''' Add the current zoom extent to the zoom history
    ''' </summary>
    Private Sub saveZoomSnapshot()

        If ZoomHistoryIndex < (ZoomHistory.Count - 1) Then
            'if we are branching off from an old index, just remove the zoom history after the current index
            ZoomHistory.RemoveRange(ZoomHistoryIndex + 1, ZoomHistory.Count - (ZoomHistoryIndex + 1))
            Log.AddLogEntry(Log.levels.debug, String.Format("Removed zoom history after index " & ZoomHistoryIndex))
        Else
            'add new snapshot
            ZoomHistory.Add(New Tuple(Of Double, Double)(Me.TChart1.Axes.Bottom.Minimum, Me.TChart1.Axes.Bottom.Maximum))
            Log.AddLogEntry(Log.levels.debug, String.Format("Saved zoom snapshot {0}: {1}, {2}", ZoomHistoryIndex, Date.FromOADate(Me.TChart1.Axes.Bottom.Minimum), Date.FromOADate(Me.TChart1.Axes.Bottom.Maximum)))
        End If
        ZoomHistoryIndex += 1

        Me.ToolStripButton_ZoomPrevious.Enabled = (ZoomHistoryIndex - 1 < ZoomHistory.Count And ZoomHistoryIndex > 0)
        Me.ToolStripButton_ZoomNext.Enabled = (ZoomHistoryIndex + 1 < ZoomHistory.Count)

    End Sub

    ''' <summary>
    ''' Handles time series properties changed in the PropertiesDialog
    ''' </summary>
    ''' <param name="id">Id of the time series whose properties have changed</param>
    ''' <remarks>Handles changed interpretation, title and unit</remarks>
    Private Sub TimeSeriesPropertiesChanged(id As Integer) Handles propDialog.PropertyChanged

        'find series in chart
        For Each series As Steema.TeeChart.Styles.Series In Me.TChart1.Series
            If series.Tag = id Then
                'set line display according to interpretation
                If TypeOf series Is Steema.TeeChart.Styles.Line Then
                    Dim seriesline As Steema.TeeChart.Styles.Line = series
                    Select Case Me.TimeSeriesDict(id).Interpretation
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
                series.Title = Me.TimeSeriesDict(id).Title

                'assign to axis according to unit
                assignSeriesToAxis(series, Me.TimeSeriesDict(id).Unit)

                'TODO: apply the same changes in the overview chart
                Exit For
            End If
        Next

    End Sub

    ''' <summary>
    ''' Handles axis unit changed in the AxisDialog
    ''' </summary>
    ''' <remarks>Reassigns all series to their appropriate axis</remarks>
    Private Sub AxisUnitChanged() Handles axisDialog.AxisUnitChanged

        For Each series As Steema.TeeChart.Styles.Series In Me.TChart1.Series
            assignSeriesToAxis(series, Me.TimeSeriesDict(series.Tag).Unit)
        Next

        'deactivate unused custom axes
        Dim unitUsed As Boolean
        For Each axis As Steema.TeeChart.Axis In Me.TChart1.Axes.Custom
            unitUsed = False
            For Each ts As TimeSeries In Me.TimeSeriesDict.Values
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

        'Reset Zoom history
        Me.ZoomHistory.Clear()
        Me.ZoomHistoryIndex = 0
        Me.ToolStripButton_ZoomPrevious.Enabled = False
        Me.ToolStripButton_ZoomNext.Enabled = False

        'Collections zurücksetzen
        Me.TimeSeriesDict.Clear()

        'Log zurücksetzen
        Call Log.ClearLog()
        Call Log.HideLogWindow()

        'Reset counters
        Me.ToolStripStatusLabel_Errors.Text = 0
        Me.ToolStripStatusLabel_Warnings.Text = 0
        Me.ToolStripStatusLabel_Errors.Image = My.Resources.cancel_inactive
        Me.ToolStripStatusLabel_Warnings.Image = My.Resources.warning_inactive

        'Update dialogs
        Call Me.updateAxisDialog()
        Call propDialog.Update(Me.TimeSeriesDict.Values.ToList)

        'Update window title
        Me.Text = "BlueM.Wave"

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

    Private Sub PasteFromClipboardCtrlVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteFromClipboardCtrlVToolStripMenuItem.Click
        Call Me.Import_Clipboard()
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

            'collect datasources
            Dim datasources As New Dictionary(Of String, List(Of String)) '{file: [title, ...], ...}
            Dim file, title As String
            For Each ts As TimeSeries In Me.TimeSeriesDict.Values
                If ts.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport Then
                    file = ts.DataSource.FilePath
                    title = ts.DataSource.Title
                    If Not datasources.ContainsKey(file) Then
                        datasources.Add(file, New List(Of String))
                    End If
                    datasources(file).Add(title)
                Else
                    Log.AddLogEntry(Log.levels.warning, String.Format("Series '{0}' does not originate from a file import and could not be saved to the project file!", ts.Title))
                End If
            Next

            'write the project file
            Dim fs As New FileStream(projectfile, FileMode.Create, FileAccess.Write)
            Dim strwrite As New StreamWriter(fs, Helpers.DefaultEncoding)

            strwrite.WriteLine("# Wave project file")

            For Each file In datasources.Keys
                'TODO: write relative paths to the project file?
                strwrite.WriteLine("file=" & file)
                For Each title In datasources(file)
                    'TODO: if a series was renamed, write the new title to the project file
                    strwrite.WriteLine("    series=" & title)
                Next
            Next

            strwrite.Close()
            fs.Close()

            Log.AddLogEntry(Log.levels.info, "Wave project file " & projectfile & " saved.")

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

    'Zeitreihe zuschneiden
    '*********************
    Private Sub Zuschneiden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Cut.Click

        Dim id As String
        Dim ids As List(Of Integer)

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for cutting!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        'Dialog instanzieren
        Dim cutter As New CutDialog(Me.TimeSeriesDict.Values.ToList, DateTime.FromOADate(Me.TChart1.Chart.Axes.Bottom.Minimum), DateTime.FromOADate(Me.TChart1.Chart.Axes.Bottom.Maximum))

        'Dialog anzeigen
        If (cutter.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            If (cutter.CheckBox_keepUncutSeries.Checked = False) Then
                'Alte Zeitreihe(n) löschen
                If (cutter.ComboBox_ZeitreiheCut.SelectedItem.ToString = CutDialog.labelAlle) Then
                    ids = New List(Of Integer)
                    For Each id In Me.TimeSeriesDict.Keys
                        ids.Add(id)
                    Next
                    For Each id In ids
                        Call Me.DeleteTimeSeries(id)
                    Next
                Else
                    id = CType(cutter.ComboBox_ZeitreiheCut.SelectedItem, TimeSeries).Id
                    Call Me.DeleteTimeSeries(id)
                End If
            End If

            'Neue Reihe(n) importieren
            For Each zre As TimeSeries In cutter.zreCut
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
    Private Sub ToolStripButton_Merge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Merge.Click

        Dim dlg As MergeSeriesDialog
        Dim dlgResult As DialogResult
        Dim ids As List(Of Integer)
        Dim seriesMerged, seriesToMerge As TimeSeries
        Dim mergedSeriesTitle As String

        'Abort if no series are loaded
        If (Me.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for merging!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try

            dlg = New MergeSeriesDialog(Me.TimeSeriesDict.Values.ToList)
            dlgResult = dlg.ShowDialog()

            Me.Cursor = Cursors.WaitCursor

            If dlgResult = Windows.Forms.DialogResult.OK Then

                ids = dlg.selectedSeries
                mergedSeriesTitle = dlg.mergedSeriesTitle

                'Clone the series with the highest priority
                seriesMerged = Me.TimeSeriesDict(ids(0)).Clone

                'Append the remaining series in order
                For i As Integer = 1 To ids.Count - 1
                    seriesToMerge = Me.TimeSeriesDict(ids(i))
                    seriesMerged.Append(seriesToMerge)
                Next

                'Assign title
                seriesMerged.Title = mergedSeriesTitle

                Log.AddLogEntry(Log.levels.info, "Series successfully merged!")

                Me.Import_Series(seriesMerged)

            End If

        Catch ex As Exception
            Log.AddLogEntry(Log.levels.error, "Error during merge: " & ex.Message)
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
    ''' Show AxisDialog button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_AxisDialog_Click(sender As Object, e As EventArgs) Handles ToolStripButton_AxisDialog.Click
        Call Me.updateAxisDialog()
        Me.axisDialog.Show()
        Me.axisDialog.BringToFront()
    End Sub

    ''' <summary>
    ''' Update AxisDialog
    ''' </summary>
    Private Sub updateAxisDialog()
        'Wrap Left, Right and Custom axes
        Dim axisList As New List(Of AxisWrapper)
        axisList.Add(New AxisWrapper("Left", Me.TChart1.Axes.Left))
        axisList.Add(New AxisWrapper("Right", Me.TChart1.Axes.Right))
        For i As Integer = 0 To Me.TChart1.Axes.Custom.Count - 1
            axisList.Add(New AxisWrapper("Custom " & i, Me.TChart1.Axes.Custom(i)))
        Next

        Me.axisDialog.Update(axisList)
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
        If (Me.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for export!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        'Show Export dialog
        exportDlg = New ExportDiag(Me.TimeSeriesDict)
        dlgResult = exportDlg.ShowDialog()

        If dlgResult <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        'get copies of the selected series
        zres = New List(Of TimeSeries)
        For Each ts As TimeSeries In exportDlg.ListBox_Series.SelectedItems
            zres.Add(ts.Clone())
        Next

        'prepare metadata according to file format
        Dim keys As List(Of String)
        Dim metadata_old As Metadata
        For Each ts As TimeSeries In zres
            'get a list of metadata keys
            Select Case exportDlg.ComboBox_Format.SelectedItem
                Case FileFormatBase.FileFormats.UVF
                    keys = UVF.MetadataKeys
                Case FileFormatBase.FileFormats.ZRXP
                    keys = ZRXP.MetadataKeys
                Case Else
                    keys = FileFormatBase.MetadataKeys 'empty list
            End Select
            If keys.Count > 0 Then
                'create a copy of the existing metadata
                metadata_old = ts.Metadata
                'create new metadata keys
                ts.Metadata = New Metadata()
                For Each key As String In keys
                    If metadata_old.Keys.Contains(key) Then
                        'copy old metadata value with the same key
                        ts.Metadata.Add(key, metadata_old(key))
                    Else
                        'add a new key with an empty value
                        ts.Metadata.Add(key, "")
                    End If
                Next
                'set default metadata values
                Select Case exportDlg.ComboBox_Format.SelectedItem
                    Case FileFormatBase.FileFormats.UVF
                        UVF.setDefaultMetadata(ts)
                    Case FileFormatBase.FileFormats.ZRXP
                        ZRXP.setDefaultMetadata(ts)
                    Case Else
                        FileFormatBase.setDefaultMetadata(ts)
                End Select
                'show dialog for editing metadata
                Dim dlg As New MetadataDialog(ts.Metadata)
                dlgResult = dlg.ShowDialog()
                If Not dlgResult = Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
                'update metadata of series
                ts.Metadata = dlg.Metadata
            End If
        Next

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
            Case FileFormatBase.FileFormats.ZRXP
                Me.SaveFileDialog1.DefaultExt = "zrx"
                Me.SaveFileDialog1.Filter = "ZRXP files (*.zrx)|*.zrx"
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
        Log.AddLogEntry(Log.levels.info, "Exporting time series to file " & Me.SaveFileDialog1.FileName & "...")

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

                Case FileFormatBase.FileFormats.ZRXP
                    Call ZRXP.Write_File(zres(0), filename)

                Case Else
                    MsgBox("Not yet implemented!", MsgBoxStyle.Exclamation, "Wave")
            End Select

            MsgBox("Time series exported successfully!", MsgBoxStyle.Information, "Wave")
            Log.AddLogEntry(Log.levels.info, "Time series exported successfully!")

        Catch ex As Exception
            Log.AddLogEntry(Log.levels.error, "Error during export: " & ex.Message)
            MsgBox("Error during export: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    'Analysieren
    '***********
    Private Sub Analyse(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Analysis.Click

        'Wenn keine Zeitreihen vorhanden, abbrechen!
        If (Me.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for analysis!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Dim oAnalysisDialog As New AnalysisDialog(Me.TimeSeriesDict.Values.ToList)

        'Analysisdialog anzeigen
        '-----------------------
        If (oAnalysisDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            Try
                'Wait-Cursor
                Me.Cursor = Cursors.WaitCursor

                Call Log.AddLogEntry(Log.levels.info, "Starting analysis " & oAnalysisDialog.selectedAnalysisFunction.ToString() & " ...")

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
                Me.Cursor = Cursors.Default

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
                        Call Me.Import_Series(ts, True)
                    Next
                End If

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                'Logeintrag
                Call Log.AddLogEntry(Log.levels.error, "Analysis failed:" & eol & ex.Message)
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
        propDialog.Update(Me.TimeSeriesDict.Values.ToList)
        propDialog.Show()
        propDialog.BringToFront()
    End Sub

    ''' <summary>
    ''' Show NaN values button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_ShowNaNValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ShowNaNValues.Click

        Dim processSeries As Boolean
        Dim nanStart, nanEnd, bandStart, bandEnd As DateTime
        Dim band As Steema.TeeChart.Tools.ColorBand
        Dim color As Drawing.Color
        Dim isNaNPeriod, nanFound, nanFoundInSeries As Boolean

        'set default color
        color = Color.Red

        If ToolStripButton_ShowNaNValues.Checked Then
            'Switch visualization of NaN values on
            'Show color bands for NaN values in the currently active series
            nanFound = False
            For Each ts As TimeSeries In Me.TimeSeriesDict.Values
                processSeries = False
                'check if time series is currently active
                For Each series As Steema.TeeChart.Styles.Series In Me.TChart1.Series
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
                    Log.AddLogEntry(Log.levels.info, "Finding NaN values for series " & ts.Title & "...")
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
                            End If
                        Else
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
                                Me.TChart1.Tools.Add(band)
                                band.Axis = Me.TChart1.Axes.Bottom
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

                                'write to log
                                Log.AddLogEntry(Log.levels.info, "Series contains NaN values from " & nanStart.ToString(Helpers.DateFormats("default")) & " to " & nanEnd.ToString(Helpers.DateFormats("default")))
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
    ''' Convert error values button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_ConvertErrorValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ConvertErrorValues.Click
        'Abort if no time series available!
        If (Me.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Dim dlg As New ConvertErrorValuesDialog(Me.TimeSeriesDict.Values.ToList)
        Dim dlgresult As DialogResult = dlg.ShowDialog()

        If dlgresult = Windows.Forms.DialogResult.OK Then
            'import cleaned series
            For Each zre As TimeSeries In dlg.tsConverted
                Me.Import_Series(zre, True)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Remove error values button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_RemoveNaNValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_RemoveNaNValues.Click

        Dim dlgResult As DialogResult
        Dim keys As List(Of Integer)
        Dim ts As TimeSeries

        dlgResult = MsgBox("Delete all nodes with NaN values from all series?", MsgBoxStyle.OkCancel)
        If dlgResult = Windows.Forms.DialogResult.OK Then
            keys = Me.TimeSeriesDict.Keys.ToList()
            For Each key As Integer In keys
                ts = Me.TimeSeriesDict(key)
                ts = ts.removeNaNValues()
                Me.TimeSeriesDict(key) = ts
                'TODO: remove NaN values from series in chart
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
    Private Sub ShowLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel_Log.Click, ToolStripStatusLabel_Errors.Click, ToolStripStatusLabel_Warnings.Click
        'LogWindow anzeigen
        Call Log.ShowLogWindow()
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
    ''' Zoom in button clicked
    ''' decrease current zoom extent by 25%
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_ZoomIn_Click(sender As Object, e As EventArgs) Handles ToolStripButton_ZoomIn.Click

        'save the current zoom snapshot
        Call Me.saveZoomSnapshot()

        'zoom
        Dim displayRange As Double = Me.TChart1.Axes.Bottom.Maximum - Me.TChart1.Axes.Bottom.Minimum
        Me.TChart1.Axes.Bottom.Minimum = Me.TChart1.Axes.Bottom.Minimum + displayRange * 0.125
        Me.TChart1.Axes.Bottom.Maximum = Me.TChart1.Axes.Bottom.Maximum - displayRange * 0.125

        Me.selectionMade = True
        Call Me.viewportChanged()

    End Sub

    ''' <summary>
    ''' Zoom out button clicked
    ''' increase current zoom extent by 25%
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_ZoomOut_Click(sender As Object, e As EventArgs) Handles ToolStripButton_ZoomOut.Click

        'save the current zoom snapshot
        Call Me.saveZoomSnapshot()

        'zoom
        Dim displayRange As Double = Me.TChart1.Axes.Bottom.Maximum - Me.TChart1.Axes.Bottom.Minimum
        Me.TChart1.Axes.Bottom.Minimum = Me.TChart1.Axes.Bottom.Minimum - displayRange * 0.125
        Me.TChart1.Axes.Bottom.Maximum = Me.TChart1.Axes.Bottom.Maximum + displayRange * 0.125

        Me.selectionMade = True
        Call Me.viewportChanged()

    End Sub

    ''' <summary>
    ''' Zoom previous button clicked
    ''' </summary>
    Private Sub ToolStripButton_ZoomPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_ZoomPrevious.Click

        If ZoomHistoryIndex >= 1 And Me.ZoomHistory.Count >= (ZoomHistoryIndex - 1) Then
            Dim prevIndex As Integer = ZoomHistoryIndex - 1
            If ZoomHistoryIndex >= ZoomHistory.Count Then
                'save the current zoom snapshot first
                Call Me.saveZoomSnapshot()
            End If
            Dim extent As Tuple(Of Double, Double) = ZoomHistory(prevIndex)
            Me.TChart1.Axes.Bottom.Minimum = extent.Item1
            Me.TChart1.Axes.Bottom.Maximum = extent.Item2
            ZoomHistoryIndex = prevIndex
            Call Me.viewportChanged()
            Log.AddLogEntry(Log.levels.debug, "Zoomed to history index " & prevIndex)
        Else
            Log.AddLogEntry(Log.levels.debug, "No zoom history before index " & ZoomHistoryIndex & " available!")
        End If

        Me.ToolStripButton_ZoomPrevious.Enabled = (ZoomHistoryIndex - 1 < ZoomHistory.Count And ZoomHistoryIndex > 0)
        Me.ToolStripButton_ZoomNext.Enabled = (ZoomHistoryIndex + 1 < ZoomHistory.Count)

    End Sub

    ''' <summary>
    ''' Zoom next button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_ZoomNext_Click(sender As Object, e As EventArgs) Handles ToolStripButton_ZoomNext.Click

        If Me.ZoomHistory.Count > (ZoomHistoryIndex + 1) Then
            Dim extent As Tuple(Of Double, Double) = ZoomHistory(ZoomHistoryIndex + 1)
            Me.TChart1.Axes.Bottom.Minimum = extent.Item1
            Me.TChart1.Axes.Bottom.Maximum = extent.Item2
            ZoomHistoryIndex += 1
            Call Me.viewportChanged()
            Log.AddLogEntry(Log.levels.debug, "Zoomed to history index " & ZoomHistoryIndex)
        Else
            Log.AddLogEntry(Log.levels.debug, "No zoom history after index " & ZoomHistoryIndex & " available!")
        End If

        Me.ToolStripButton_ZoomPrevious.Enabled = (ZoomHistoryIndex - 1 < ZoomHistory.Count And ZoomHistoryIndex > 0)
        Me.ToolStripButton_ZoomNext.Enabled = (ZoomHistoryIndex + 1 < ZoomHistory.Count)

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
        ReDim items(Me.TimeSeriesDict.Count - 1)
        i = 0
        For Each ts As TimeSeries In Me.TimeSeriesDict.Values
            'store the time series Id in the Name property
            items(i) = New ToolStripMenuItem(ts.Title, Nothing, New EventHandler(AddressOf ZoomToSeries_Click), ts.Id.ToString())
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
    ''' <remarks></remarks>
    Private Sub ZoomToSeries(ByVal id As Integer)
        Dim startdate, enddate As DateTime

        If Me.TimeSeriesDict.ContainsKey(id) Then
            startdate = Me.TimeSeriesDict(id).StartDate
            enddate = Me.TimeSeriesDict(id).EndDate
            'save the current zoom snapshot
            Call Me.saveZoomSnapshot()
            'zoom
            Me.TChart1.Axes.Bottom.Minimum = startdate.ToOADate()
            Me.TChart1.Axes.Bottom.Maximum = enddate.ToOADate()
            Me.selectionMade = True
            Call Me.viewportChanged()
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
        Call Me.UpdateChartExtents()

        Call Me.viewportChanged()
    End Sub

    ''' <summary>
    ''' Auto-adjust Y-axes to current viewport button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton_AutoAdjustYAxis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton_AutoAdjustYAxes.CheckedChanged
        If Me.ToolStripButton_AutoAdjustYAxes.Checked Then
            Call Me.viewportChanged()
        Else
            'Reset the Y axes to automatic
            Me.TChart1.Axes.Left.Automatic = True
            Me.TChart1.Axes.Right.Automatic = True
            Me.TChart1.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' Löscht alle vorhandenen Serien und liest alle importierten Zeitreihen neu ein
    ''' </summary>
    Private Sub RefreshFromFile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_ReloadFromFiles.Click

        'TODO: keep time series that cannot be reloaded from file!

        'collect datasources
        Dim datasources As New Dictionary(Of String, List(Of String)) '{file: [title, ...], ...}
        Dim seriesNotFromFiles As New List(Of String)
        Dim file, title As String
        For Each ts As TimeSeries In Me.TimeSeriesDict.Values
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
            MsgBox("There are no known files that could be reloaded!", MsgBoxStyle.Information, "Wave")
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
            Me.TChart1.Series.RemoveAllSeries()
            Me.TChart2.Series.RemoveAllSeries()
            Me.TChart1.Refresh()
            Me.TChart2.Refresh()

            'Collection zurücksetzen
            Me.TimeSeriesDict.Clear()

            'Alle Dateien durchlaufen
            Dim success As Boolean
            For Each file In datasources.Keys

                Log.AddLogEntry(Log.levels.info, "Reading file " & file & " ...")

                success = True

                If IO.Path.GetExtension(file).ToUpper() = FileFactory.FileExtTEN Then
                    'TODO: this currently loads all series from the TEN, instead of only the currently loaded ones
                    Call Load_TEN(file)
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
                        Call Me.Import_Series(ts)
                    Next
                End If
                If success Then
                    Log.AddLogEntry(Log.levels.info, "File '" & file & "' imported successfully!")
                Else
                    Log.AddLogEntry(Log.levels.error, "Error while importing file '" & file & "'!")
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' Check for update menu item or update notification button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub CheckForUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdateToolStripMenuItem.Click, ToolStripButton_UpdateNotification.Click
        Try
            Dim updateAvailable As Boolean = Await CheckForUpdate()
            If updateAvailable Then
                Me.ToolStripButton_UpdateNotification.Visible = True
                Dim resp As MsgBoxResult = MsgBox("A new version is available!" & eol & "Click OK to go to downloads.bluemodel.org to get it.", MsgBoxStyle.OkCancel)
                If resp = MsgBoxResult.Ok Then
                    Process.Start(urlDownload)
                End If
            Else
                Me.ToolStripButton_UpdateNotification.Visible = False
                MsgBox("You are already up to date!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Error while checking for update:" & eol & ex.Message, MsgBoxStyle.Critical)
        End Try
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
        Process.Start(urlHelp)
    End Sub

    ''' <summary>
    ''' Release Notes click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripMenuItem_ReleaseNotes_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ReleaseNotes.Click
        Dim filepath As String
        filepath = IO.Path.Combine(Application.StartupPath, "BlueM.Wave_Release-notes.txt")
        Try
            System.Diagnostics.Process.Start(filepath)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
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
        If xMin < DateTimePicker.MinimumDateTime Then xMin = DateTimePicker.MinimumDateTime
        If xMin > DateTimePicker.MaximumDateTime Then xMin = DateTimePicker.MaximumDateTime
        If xMax < DateTimePicker.MinimumDateTime Then xMax = DateTimePicker.MinimumDateTime
        If xMax > DateTimePicker.MaximumDateTime Then xMax = DateTimePicker.MaximumDateTime
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
            Call Me.viewportChanged()
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

        Call Me.viewportChanged()

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

        Call Me.viewportChanged()

        Me.selectionMade = True

    End Sub

#End Region 'Navigation

#Region "Cursor"

    ''' <summary>
    ''' Handles main chart MouseDown event
    ''' Start a zooming or panning process, save zoom snapshot
    ''' </summary>
    Private Sub TChart1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Left Then
            'start zoom process
            If Me.TChart1.Series.Count > 0 Then

                Dim startValue As Double
                startValue = Me.TChart1.Series(0).XScreenToValue(e.X)

                If startValue < Me.TChart1.Axes.Bottom.Minimum Or
                    startValue > Me.TChart1.Axes.Bottom.Maximum Then
                    'click outside of chart, don't start zoom process
                    Exit Sub
                End If

                Me.TChart1.Cursor = Me.cursor_zoom
                Call Me.saveZoomSnapshot()

                Me.ChartMouseZoomDragging = True
                Me.ChartMouseDragStartX = e.X

                Me.colorBandZoom.Chart = Me.TChart1.Chart
                Me.colorBandZoom.Active = True
                Me.colorBandZoom.Start = startValue
                Me.colorBandZoom.End = startValue

                Log.AddLogEntry(Log.levels.debug, "Zoom start at " & Date.FromOADate(startValue))
            End If

        ElseIf e.Button = MouseButtons.Right Then
            'start pan process
            Call Me.saveZoomSnapshot()
            Me.TChart1.Cursor = Me.cursor_pan
        End If

    End Sub

    ''' <summary>
    ''' Handles main chart MouseMove event
    ''' Animates any started zoom
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TChart1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseMove
        If Me.ChartMouseZoomDragging Then
            Dim endValue As Double
            endValue = TChart1.Series(0).XScreenToValue(e.X)
            Me.colorBandZoom.End = endValue
        End If
    End Sub

    ''' <summary>
    ''' Handles main chart MouseUp event
    ''' Complete any started zoom process, update cursor
    ''' </summary>
    Private Sub TChart1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseUp
        If Me.ChartMouseZoomDragging Then
            'complete the zoom process
            Me.ChartMouseZoomDragging = False
            'determine start and end of zoom
            If e.X <> Me.ChartMouseDragStartX Then
                Dim startValue, endValue As Double
                If e.X > Me.ChartMouseDragStartX Then
                    startValue = TChart1.Series(0).XScreenToValue(Me.ChartMouseDragStartX)
                    endValue = TChart1.Series(0).XScreenToValue(e.X)
                Else
                    startValue = TChart1.Series(0).XScreenToValue(e.X)
                    endValue = TChart1.Series(0).XScreenToValue(Me.ChartMouseDragStartX)
                End If
                Log.AddLogEntry(Log.levels.debug, "Zoom end at " & Date.FromOADate(endValue))

                'adjust colorband
                Me.colorBandZoom.Active = False

                'save the current zoom snapshot
                Call Me.saveZoomSnapshot()

                'set the new viewport 
                Me.TChart1.Axes.Bottom.Minimum = startValue
                Me.TChart1.Axes.Bottom.Maximum = endValue
                Me.selectionMade = True
                Call Me.viewportChanged()
            End If
        End If
        Me.TChart1.Cursor = Cursors.Default
    End Sub

#End Region 'Cursor

#End Region 'UI

#Region "Funktionalität"

    'Zeitreihe intern hinzufügen
    '***************************
    Private Sub AddZeitreihe(ByRef zre As TimeSeries)

        Dim duplicateFound As Boolean
        Dim pattern As String = "(?<name>.*)\s\(\d+\)$"
        Dim match As Match
        Dim n As Integer = 1

        'Umbenennen, falls Titel schon vergeben
        'Format: "Titel (n)"
        Do While True
            duplicateFound = False
            For Each ts As TimeSeries In Me.TimeSeriesDict.Values
                If zre.Title = ts.Title Then
                    duplicateFound = True
                End If
            Next

            If duplicateFound Then
                match = Regex.Match(zre.Title, pattern)
                If (match.Success) Then
                    n += 1
                    zre.Title = Regex.Replace(zre.Title, pattern, "${name} (" & n.ToString() & ")")
                Else
                    zre.Title &= " (1)"
                End If
            Else
                Exit Do
            End If
        Loop

        Me.TimeSeriesDict.Add(zre.Id, zre)

        'add datasource filename to Recently Used Files menu
        If zre.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport Then
            'remove if already present
            Dim i As Integer = 0
            For Each _item As ToolStripItem In Me.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems
                If _item.Text = zre.DataSource.FilePath Then
                    Me.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.RemoveAt(i)
                    Exit For
                End If
                i += 1
            Next
            'add to top of list
            Dim item As New ToolStripMenuItem(zre.DataSource.FilePath)
            Me.ToolStripMenuItem_RecentlyUsedFiles.DropDownItems.Insert(0, item)
        End If

        'Update dialogs
        Me.propDialog.Update(Me.TimeSeriesDict.Values.ToList)
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

            Log.AddLogEntry(Log.levels.info, "Loading Wave project file " & projectfile & " ...")

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
            strRead = New StreamReader(fstr, detectEncodingFromByteOrderMarks:=True)

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
                    'series name may be enclosed in quotes and be followed by an optional title, which may also be enclosed in quotes
                    'examples:
                    'series
                    'series:title
                    '"se:ries":title
                    '"se:ries":"title"
                    Dim pattern As String
                    If line.StartsWith("""") Then
                        'series name is enclosed in quotes
                        pattern = "^""([^""]+)""(:(.+))?$"
                    Else
                        'no quotes around series name
                        pattern = "^([^:]+)(:(.+))?$"
                    End If
                    Dim m As Match = Regex.Match(line, pattern)
                    If m.Success Then
                        series = m.Groups(1).Value.Trim()
                        If m.Groups(2).Success Then
                            title = m.Groups(3).Value.Replace("""", "").Trim() 'remove quotes around title here
                        Else
                            title = ""
                        End If
                        'add series to file
                        If fileDict.ContainsKey(path) Then
                            If Not fileDict(path).ContainsKey(series) Then
                                fileDict(path).Add(series, title)
                            Else
                                Log.AddLogEntry(Log.levels.warning, "Series " & series & " is specified twice, the second mention will be ignored!")
                            End If
                        Else
                            Log.AddLogEntry(Log.levels.warning, "Series " & series & " is not associated with a file and will be ignored!")
                        End If
                    Else
                        Log.AddLogEntry(Log.levels.warning, "Unable to parse series definition 'series=" & line & "', this series will be ignored!")
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
                            Log.AddLogEntry(Log.levels.warning, "Setting " & parts(0) & " is specified twice, the second mention will be ignored!")
                        End If
                    Else
                        Log.AddLogEntry(Log.levels.warning, "Setting " & parts(0) & " is not associated with a file and will be ignored!")
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

                Log.AddLogEntry(Log.levels.info, "Reading file " & file & " ...")

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
                                    Log.AddLogEntry(Log.levels.warning, "Setting '" & setting & "' was not recognized and was ignored!")
                            End Select
                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.warning, "Setting '" & setting & "' with value '" & value & "' could not be parsed and was ignored!")
                        End Try
                    Next
                    'reread columns with new settings
                    fileobj.readSeriesInfo()
                End If

                'get the list of series to be imported
                seriesList = fileDict(file)

                'select series for importing
                If seriesList.Count = 0 Then
                    'read all series contained in the file
                    Call fileobj.selectAllSeries()
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
                        Log.AddLogEntry(Log.levels.error, "No series left to import, skipping file!")
                        Continue For
                    End If

                End If

                'read the file
                fileobj.readFile()

                'Log
                Call Log.AddLogEntry(Log.levels.info, "File '" & file & "' imported successfully!")

                'import the series
                Call Log.AddLogEntry(Log.levels.info, "Loading series in chart...")
                For Each ts As TimeSeries In fileobj.FileTimeSeries.Values
                    'change title if specified in the project file
                    If seriesList.Count > 0 Then
                        If seriesList(ts.Title) <> "" Then
                            ts.Title = seriesList(ts.Title)
                        End If
                    End If
                    Call Me.Import_Series(ts)
                Next
            Next

            'Log
            Call Log.AddLogEntry(Log.levels.info, "Project file '" & projectfile & "' loaded successfully!")

            'Update window title
            Me.Text = "BlueM.Wave - " & projectfile

            Call Me.UpdateChartExtents()

        Catch ex As Exception
            MsgBox("Error while loading project file:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry(Log.levels.error, "Error while loading project file:" & eol & ex.Message)
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
            Call Log.AddLogEntry(Log.levels.info, "Loading file '" & FileName & "' ...")

            'Bereits vorhandene Reihen merken
            Dim existingIds = New List(Of Integer)
            For Each id As Integer In Me.TimeSeriesDict.Keys
                existingIds.Add(id)
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
            'change type of series to FastLine
            For i = 0 To Me.TChart2.Series.Count - 1
                Steema.TeeChart.Styles.Series.ChangeType(Me.TChart2.Series(i), GetType(Steema.TeeChart.Styles.FastLine))
            Next

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
                                Log.AddLogEntry(Log.levels.info, "Importing series '" & series.Title & "' from TEN file...")
                                reihe = New TimeSeries(series.Title)
                                For i = 0 To series.Count - 1
                                    reihe.AddNode(Date.FromOADate(series.XValues(i)), series.YValues(i))
                                Next
                                'Determine total number of NaN-values and write to log
                                If reihe.Nodes.Count > reihe.NodesClean.Count Then
                                    Log.AddLogEntry(Log.levels.warning, String.Format("Series '{0}' contains {1} NaN values!", reihe.Title, reihe.Nodes.Count - reihe.NodesClean.Count))
                                End If
                                'Get the series' unit from the axis title
                                Dim axistitle As String = ""
                                Select Case series.VertAxis
                                    Case Steema.TeeChart.Styles.VerticalAxis.Left
                                        axistitle = Me.TChart1.Axes.Left.Title.Text
                                    Case Steema.TeeChart.Styles.VerticalAxis.Right
                                        axistitle = Me.TChart1.Axes.Right.Title.Text
                                    Case Steema.TeeChart.Styles.VerticalAxis.Custom
                                        axistitle = series.CustomVertAxis.Title.Text
                                End Select
                                reihe.Unit = AxisWrapper.parseUnit(axistitle)

                                'Save datasource
                                reihe.DataSource = New TimeSeriesDataSource(FileName, series.Title)

                                'Store the series internally
                                Call Me.AddZeitreihe(reihe)

                                'update the title in case it was changed during storage
                                series.Title = reihe.Title

                                'Store the time series id in the Tag property
                                series.Tag = reihe.Id
                                'Store id as Tag in Chart2 as well
                                For Each series2 As Steema.TeeChart.Styles.Series In TChart2.Series
                                    If series2.Title = series.Title Then
                                        series2.Tag = reihe.Id
                                        Exit For
                                    End If
                                Next

                            End If
                        Next

                        'Update window title
                        Me.Text = "BlueM.Wave - " & FileName

                    Case Windows.Forms.DialogResult.No
                        'Reihen aus TEN-Datei sollen nicht importiert werden

                        'Alle Reihen aus den Diagrammen löschen (wurden bei TEN-Import automatisch mit eingeladen)
                        Me.TChart1.Series.RemoveAllSeries()
                        Me.TChart2.Series.RemoveAllSeries()

                End Select

            End If

            'extract units from axis titles and store as tags
            Me.TChart1.Axes.Left.Tag = AxisWrapper.parseUnit(Me.TChart1.Axes.Left.TitleOrName)
            Me.TChart1.Axes.Right.Tag = AxisWrapper.parseUnit(Me.TChart1.Axes.Right.TitleOrName)
            For Each axis As Steema.TeeChart.Axis In Me.TChart1.Axes.Custom
                axis.Tag = AxisWrapper.parseUnit(axis.TitleOrName)
            Next

            'Die vor dem Laden bereits vorhandenen Zeitreihen wieder zu den Diagrammen hinzufügen (durch TEN-Import verloren)
            For Each id As Integer In existingIds
                Call Me.Display_Series(Me.TimeSeriesDict(id))
            Next

            'Vorherigen Zoom wiederherstellen
            If (Me.selectionMade) Then
                Me.TChart1.Axes.Bottom.Minimum = XMin
                Me.TChart1.Axes.Bottom.Maximum = XMax
            End If

            'ColorBands neu einrichten (durch TEN-Import verloren)
            Call Me.Init_ColorBands()

            'Reset zoom and pan settings
            Me.TChart1.Zoom.Allow = False
            Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal
            Me.TChart1.Panning.MouseButton = MouseButtons.Right
            Me.TChart2.Zoom.Allow = False
            Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

            'Charts aktualisieren
            Call Me.UpdateChartExtents()

            Call Me.viewportChanged()

            'Re-assign the chart to the chartlistbox (Bug 701)
            Me.ChartListBox1.Chart = Me.TChart1

            'Log
            Call Log.AddLogEntry(Log.levels.info, "TEN file '" & FileName & "' loaded successfully!")

        Catch ex As Exception
            MsgBox("Error while loading:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry(Log.levels.error, "Error while loading:" & eol & ex.Message)
        End Try

        'Update AxisDialog
        Call Me.updateAxisDialog()
    End Sub

    ''' <summary>
    ''' Lädt ein TeeChart Theme (XML-Datei)
    ''' </summary>
    ''' <param name="FileName">Pfad zur XML-Datei</param>
    ''' <remarks></remarks>
    Private Sub Load_Theme(ByVal FileName As String)

        Try

            'Log
            Call Log.AddLogEntry(Log.levels.info, "Loading theme '" & FileName & "' ...")

            'Theme laden
            Call TChart1.Import.Theme.Load(FileName)

            'Log
            Call Log.AddLogEntry(Log.levels.info, "Theme '" & FileName & "' loaded successfully!")

        Catch ex As Exception
            MsgBox("Error while loading:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry(Log.levels.error, "Error while loading:" & eol & ex.Message)
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
                    Call Log.AddLogEntry(Log.levels.info, "Importing file '" & file & "' ...")

                    'Datei-Instanz erzeugen
                    Datei = FileFactory.getFileInstance(file)

                    If (Datei.UseImportDialog) Then
                        'Falls Importdialog erforderlich, diesen anzeigen
                        ok = Me.showImportDialog(Datei)
                        Call Application.DoEvents()
                    Else
                        'Ansonsten alle Spalten auswählen
                        Call Datei.selectAllSeries()
                        ok = True
                    End If

                    If (ok) Then

                        Me.Cursor = Cursors.WaitCursor

                        'Datei einlesen
                        Call Datei.readFile()

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, "File '" & file & "' imported successfully!")

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, "Loading series in chart...")

                        'Import all time series into the chart
                        For Each ts As TimeSeries In Datei.FileTimeSeries.Values
                            Call Me.Import_Series(ts)
                        Next

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, "Successfully loaded series in chart!")

                        'Update window title
                        Me.Text = "BlueM.Wave - " & file

                    Else
                        'Import abgebrochen
                        Log.AddLogEntry(Log.levels.error, "Import cancelled!")

                    End If

                Catch ex As Exception
                    MsgBox("Error during import:" & eol & ex.Message, MsgBoxStyle.Critical)
                    Call Log.AddLogEntry(Log.levels.error, "Error during import: " & ex.Message)

                Finally
                    Me.Cursor = Cursors.Default
                End Try

        End Select

    End Sub

    ''' <summary>
    ''' Attempts import of clipboard content
    ''' Detects TALSIM clipboard content or plain text
    ''' </summary>
    Private Sub Import_Clipboard()

        Dim dlgres As DialogResult

        Try
            'Check data format
            If Clipboard.ContainsText(TextDataFormat.Text) Then

                Dim clipboardtext As String
                clipboardtext = Clipboard.GetText(TextDataFormat.Text)

                If clipboardtext.Contains("SydroTyp=SydroErgZre") Or
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
                Else
                    'ask the user whether to attempt plain text import
                    dlgres = MessageBox.Show("Attempt to load clipboard text content in Wave as CSV data?", "Load from clipboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If Not dlgres = Windows.Forms.DialogResult.Yes Then
                        Exit Sub
                    End If
                    'save as temp text file and then load file
                    Dim tmpfile As String = IO.Path.GetTempFileName()
                    Using writer As New StreamWriter(tmpfile, False, Helpers.DefaultEncoding)
                        writer.Write(clipboardtext)
                    End Using
                    Call Me.Import_File(tmpfile)
                    'delete temp file after import
                    IO.File.Delete(tmpfile)
                End If
            Else
                MessageBox.Show("No usable clipboard content detected!", "Load from clipboard", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Log.AddLogEntry(Log.levels.error, ex.Message)
            MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
        End Try

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
        Dim m As Match
        Dim i_series As Integer
        Dim parts() As String
        Dim zreblock As Boolean
        Dim data As New List(Of Dictionary(Of String, String)) '[{zreparams1},{zreparams2},...]
        Dim file, name As String
        Dim fileobj As FileFormatBase
        Dim ts As TimeSeries

        zreblock = False
        For Each line As String In clipboardtext.Split(eol)
            line = line.Trim()

            m = Regex.Match(line, "\[Zeitreihe(\d+)\]")
            If m.Success Then
                i_series = m.Groups(1).Value
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
                    If params("Kennung") = "ZPG" Then
                        'handle control groups
                        name = "KGRP_" & params("Zustand")
                    Else
                        name = params("Kennung").PadRight(4, " ") & "_" & params("Zustand")
                    End If

                    'read file
                    Log.AddLogEntry(Log.levels.info, "Loading file " & file & " ...")
                    fileobj = FileFactory.getFileInstance(file)

                    'read series from file
                    ts = fileobj.getTimeSeries(name)

                    'set interpretation
                    ts.Interpretation = params("Interpretation")

                    'import series
                    Call Me.Import_Series(ts)

                Case "99" 'BIN file

                    name = params("Kennung")

                    'read file
                    Log.AddLogEntry(Log.levels.info, "Loading file " & file & " ...")
                    fileobj = FileFactory.getFileInstance(file)

                    'read series from file
                    fileobj.readFile()
                    ts = fileobj.FileTimeSeries.First.Value

                    'add metadata
                    ts.Title = name
                    ts.Unit = params("Einheit")

                    'set interpretation
                    ts.Interpretation = params("Interpretation")

                    'import series
                    Call Me.Import_Series(ts)

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

        'Cut timeseries if necessary
        If zre.StartDate < Constants.minOADate Then
            Log.AddLogEntry(Log.levels.warning, String.Format("Unable to display timeseries before {0}, cutting!", Constants.minOADate))
            zre.Cut(Constants.minOADate, zre.EndDate)
        End If
        If zre.EndDate > Constants.maxOADate Then
            Log.AddLogEntry(Log.levels.warning, String.Format("Unable to display timeseries after {0}, cutting!", Constants.maxOADate))
            zre.Cut(zre.StartDate, Constants.maxOADate)
        End If

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

        'Serie zu Diagramm hinzufügen

        'Linien instanzieren
        Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        Dim Line2 As New Steema.TeeChart.Styles.FastLine(Me.TChart2.Chart)

        'Do not paint NaN values
        Line1.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.DoNotPaint
        Line2.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.DoNotPaint

        'X-Werte als Zeitdaten einstellen
        Line1.XValues.DateTime = True
        Line2.XValues.DateTime = True

        'Store id as Tag property
        Line1.Tag = zre.Id
        Line2.Tag = zre.Id

        'Namen vergeben
        Line1.Title = zre.Title
        Line2.Title = zre.Title

        'Stützstellen zur Serie hinzufügen
        Line1.BeginUpdate()
        Line2.BeginUpdate()
        Dim i As Integer = 0
        For Each node As KeyValuePair(Of DateTime, Double) In zre.Nodes
            Try
                Line1.Add(node.Key, node.Value)
                Line2.Add(node.Key, node.Value)
            Catch ex As OverflowException
                Log.AddLogEntry(Log.levels.error, String.Format("Unable to display date {0} in chart!", node.Key))
            End Try
            'Set NaN values to Null
            If Double.IsNaN(node.Value) Then
                Line1.SetNull(i)
                Line2.SetNull(i)
            End If
            i += 1
        Next
        Line1.EndUpdate()
        Line2.EndUpdate()

        'Determine total number of NaN-values and write to log
        If zre.Nodes.Count > zre.NodesClean.Count Then
            Log.AddLogEntry(Log.levels.warning, String.Format("Series '{0}' contains {1} NaN values!", zre.Title, zre.Nodes.Count - zre.NodesClean.Count))
        End If

        'Y-Achsenzuordnung
        assignSeriesToAxis(Line1, zre.Unit)

        'Interpretation
        Select Case zre.Interpretation
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

        Call Me.viewportChanged()
    End Sub

    ''' <summary>
    ''' Delete a TimeSeries
    ''' </summary>
    ''' <param name="id">TimeSeries Id</param>
    Private Sub DeleteTimeSeries(ByVal id As Integer) Handles propDialog.SeriesDeleted

        'Remove from main diagram
        For i As Integer = Me.TChart1.Series.Count - 1 To 0 Step -1
            If (Me.TChart1.Series.Item(i).Tag = id) Then
                Me.TChart1.Series.RemoveAt(i)
                Me.TChart1.Refresh()
                Exit For
            End If
        Next

        'Remove from overview chart
        For i As Integer = Me.TChart2.Series.Count - 1 To 0 Step -1
            If (Me.TChart2.Series.Item(i).Tag = id) Then
                Me.TChart2.Series.RemoveAt(i)
                Me.TChart2.Refresh()
                Exit For
            End If
        Next

        'Delete internally
        Me.TimeSeriesDict.Remove(id)

        'Update dialogs
        Me.propDialog.Update(Me.TimeSeriesDict.Values.ToList)

    End Sub

    ''' <summary>
    ''' Assigns a series to the appropriate axis depending on its unit
    ''' If no axis exists for the given unit, a new axis is created
    ''' </summary>
    ''' <param name="unit">The unit</param>
    Private Sub assignSeriesToAxis(ByRef series As Steema.TeeChart.Styles.Series, ByVal unit As String)

        If IsNothing(Me.TChart1.Axes.Left.Tag) Then
            'use left axis for the first time
            Me.TChart1.Axes.Left.Title.Text = unit
            Me.TChart1.Axes.Left.Tag = unit
            Me.TChart1.Axes.Left.Visible = True
            Me.TChart1.Axes.Left.Automatic = True
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left

        ElseIf Me.TChart1.Axes.Left.Tag = unit Then
            'reuse left axis
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Left

        ElseIf IsNothing(Me.TChart1.Axes.Right.Tag) Then
            'use right axis for the first time
            Me.TChart1.Axes.Right.Title.Text = unit
            Me.TChart1.Axes.Right.Tag = unit
            Me.TChart1.Axes.Right.Visible = True
            Me.TChart1.Axes.Right.Automatic = True
            Me.TChart1.Axes.Right.Grid.Visible = False
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right

        ElseIf Me.TChart1.Axes.Right.Tag = unit Then
            'reuse right axis
            series.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
        Else
            'check for reusable custom axes
            Dim axisFound As Boolean = False
            For Each axis As Steema.TeeChart.Axis In Me.TChart1.Axes.Custom
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
                Dim number As Integer = Me.TChart1.Axes.Custom.Count + 1
                axis = Steema.TeeChart.Axes.CreateNewAxis(Me.TChart1.Chart)
                axis.Title.Text = unit
                axis.Title.Angle = 90
                axis.Tag = unit
                axis.Visible = True
                axis.Automatic = True
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
                Call Me.updateAxisDialog()
            End If
        End If

    End Sub

    ''' <summary>
    ''' Handles axis deleted in the AxisDialog
    ''' </summary>
    ''' <param name="axisname"></param>
    Private Sub axisDeleted(ByVal axisname As String) Handles axisDialog.AxisDeleted
        Dim axisnumber As Integer
        Dim m As Match = Regex.Match(axisname, "Custom (\d+)")
        If m.Success Then
            axisnumber = Integer.Parse(m.Groups(1).Value)
            'Delete axis from chart
            Me.TChart1.Axes.Custom(axisnumber).Dispose()
            Me.TChart1.Refresh()
            'update axis dialog
            Call Me.updateAxisDialog()
        End If
    End Sub

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

#End Region 'Funktionalität

End Class
