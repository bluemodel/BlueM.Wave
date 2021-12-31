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
''' Main chart window
''' </summary>
Friend Class MainWindow
    Implements IView

    'Eigenschaften
    '#############

    Private _controller As WaveController

    ''' <summary>
    ''' Flag for preventing unintended feedback loops in the UI. Default is False, can be temporarily set to True.
    ''' </summary>
    ''' <remarks></remarks>
    Friend isInitializing As Boolean

    'ColorBand that is shown while zooming in main chart
    Friend colorBandZoom As Steema.TeeChart.Tools.ColorBand

    'ColorBand representing current view extent of main chart in OverviewChart
    Friend colorBandOverview As Steema.TeeChart.Tools.ColorBand

    'Cursors
    Friend cursor_pan As Cursor
    Friend cursor_zoom As Cursor

#Region "Properties"

    Public Sub SetController(controller As Controller) Implements IView.SetController
        _controller = controller
    End Sub

    ''' <summary>
    ''' Checks whether the option to auto-adjust the Y-axes to the current viewport is activated
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property AutoAdjustYAxes() As Boolean
        Get
            Return Me.ToolStripButton_AutoAdjustYAxes.Checked
        End Get
    End Property

    Friend Property ChartMinX As DateTime
        Get
            Try
                Return DateTime.FromOADate(Me.TChart1.Axes.Bottom.Minimum)
            Catch ex As ArgumentException
                Return Constants.minOADate
            End Try
        End Get
        Set(value As DateTime)
            If value < Constants.minOADate Then
                value = Constants.minOADate
            End If
            Me.TChart1.Axes.Bottom.Minimum = value.ToOADate()
        End Set
    End Property

    Friend Property ChartMaxX As DateTime
        Get
            Try
                Return DateTime.FromOADate(Me.TChart1.Axes.Bottom.Maximum)
            Catch ex As ArgumentException
                Return Constants.maxOADate
            End Try
        End Get
        Set(value As DateTime)
            If value > Constants.maxOADate Then
                value = Constants.maxOADate
            End If
            Me.TChart1.Axes.Bottom.Maximum = value.ToOADate()
        End Set
    End Property

#End Region 'Properties

#Region "Form behavior"

    'Konstruktor
    '***********
    Public Sub New()

        Me.isInitializing = True

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        'Charts einrichten
        '-----------------
        Call Me.Init_Charts()

        'Navigation initialisieren
        Me.ComboBox_NavIncrement.SelectedItem = "Days"

        'set CurrentCulture for MaskedTextBoxes
        Me.MaskedTextBox_NavStart.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_NavStart.FormatProvider = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_NavEnd.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_NavEnd.FormatProvider = Globalization.CultureInfo.CurrentCulture
        'set current date as initial values
        Me.MaskedTextBox_NavStart.Text = DateTime.Now
        Me.MaskedTextBox_NavEnd.Text = DateTime.Now

        'Instantiate cursors
        Me.cursor_pan = New Cursor(Me.GetType(), "cursor_pan.cur")
        Me.cursor_zoom = New Cursor(Me.GetType(), "cursor_zoom.cur")

        Me.isInitializing = False

    End Sub

    'Edit Chart
    '**********
    Private Sub EditChart_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton_EditChart.Click, TChart1.DoubleClick
        Call Steema.TeeChart.Editor.Show(Me.TChart1)
    End Sub

    ''' <summary>
    ''' Color palette menu item clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripMenuItemColorPalette_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_ColorPaletteMaterial.Click, ToolStripMenuItem_ColorPaletteDistinct.Click, ToolStripMenuItem_ColorPaletteWheel.Click, ToolStripMenuItem_ColorPaletteRandom.Click
        Dim colorPaletteName As String = CType(sender, ToolStripMenuItem).Text
        SetChartColorPalette(Helpers.getColorPalette(colorPaletteName))
    End Sub

#End Region 'Form behavior

#Region "Chart behavior"

    'Charts neu einrichten
    '*********************
    Friend Sub Init_Charts()

        'Charts zurücksetzen
        Me.TChart1.Clear()
        Call Helpers.FormatChart(Me.TChart1.Chart)

        Me.TChart2.Clear()
        Call Helpers.FormatChart(Me.TChart2.Chart)
        Me.TChart2.Panel.Brush.Color = Color.FromArgb(239, 239, 239)
        Me.TChart2.Walls.Back.Color = Color.FromArgb(239, 239, 239)
        Me.TChart2.Header.Visible = False
        Me.TChart2.Legend.Visible = False

        'Disable TeeChart builtin zooming and panning functionality
        Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.None
        Me.TChart1.Zoom.History = False
        Me.TChart1.Zoom.Animated = True
        Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.None

        Me.TChart2.Zoom.Direction = Steema.TeeChart.ZoomDirections.None
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Achsen
        Me.TChart1.Axes.Bottom.Automatic = False
        Me.TChart1.Axes.Bottom.Labels.Angle = 90
        Me.TChart1.Axes.Bottom.Labels.DateTimeFormat = "dd.MM.yy HH:mm"
        Me.TChart1.Axes.Right.Title.Angle = 90

        Me.TChart2.Axes.Left.Labels.Font.Color = Color.FromArgb(100, 100, 100)
        Me.TChart2.Axes.Left.Labels.Font.Size = 8
        Me.TChart2.Axes.Bottom.Labels.Font.Color = Color.FromArgb(100, 100, 100)
        Me.TChart2.Axes.Bottom.Labels.Font.Size = 8
        Me.TChart2.Axes.Bottom.Automatic = False
        Me.TChart2.Axes.Bottom.Labels.DateTimeFormat = "dd.MM.yyyy"

        'ColorBand einrichten
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

    '''' <summary>
    '''' Handles TChart1 events Scrolled, Zoomed, UndoneZoom
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub TChart1_ZoomChanged(sender As Object, e As System.EventArgs) Handles TChart1.Scroll, TChart1.Zoomed, TChart1.UndoneZoom
    '    If (Me.ChartMinX <> Me.ChartMaxX) Then
    '        Call Me.ViewportChanged()
    '        Me.selectionMade = True
    '    End If
    'End Sub

#End Region 'Chart behavior'

#Region "UI"

#Region "Toolbar"

    Private Sub Neu_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton_New.Click

    End Sub


    'Übersicht an/aus
    '****************
    Private Sub ToolStripButton_ToggleOverview_Clicked(sender As System.Object, e As System.EventArgs) Handles ToolStripButton_ToggleOverview.Click

        Call ToggleOverviewPanel(ToolStripButton_ToggleOverview.Checked)

    End Sub

    ''' <summary>
    ''' Toggles the overview panel's visibility
    ''' </summary>
    ''' <param name="show"></param>
    Private Sub ToggleOverviewPanel(show As Boolean)
        If (show) Then
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
    Private Sub ShowNavigation_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton_ToggleNavigation.Click
        Call Me.ToggleNavigation(Me.ToolStripButton_ToggleNavigation.Checked)
    End Sub



#End Region 'Toolbar

#Region "Navigation"

    ''' <summary>
    ''' Toggle visibility of the navigation
    ''' </summary>
    ''' <param name="show">if True, the navigation is shown, otherwise it is hidden</param>
    Private Sub ToggleNavigation(show As Boolean)
        If show Then
            Me.TableLayoutPanel1.RowStyles(0).Height = 38
            Me.TableLayoutPanel1.RowStyles(2).Height = 36
            Me.ToolStripButton_ToggleNavigation.Checked = True
        Else
            Me.TableLayoutPanel1.RowStyles(0).Height = 0
            Me.TableLayoutPanel1.RowStyles(2).Height = 0
            Me.ToolStripButton_ToggleNavigation.Checked = False
        End If
    End Sub

#End Region 'Navigation

#Region "Cursor"

#End Region 'Cursor

#End Region 'UI

#Region "Funktionalität"

    ''' <summary>
    ''' Sets a color palette in the charts and changes the colors of any existing series accordingly
    ''' </summary>
    ''' <param name="colorPalette">The color palette to apply</param>
    Private Sub SetChartColorPalette(colorPalette As Color())

        'set colorpalette in charts
        Me.TChart1.Chart.ColorPalette = colorPalette
        Me.TChart2.Chart.ColorPalette = colorPalette

        'change colors of existing series
        Dim counter As Integer = 0
        For Each series As Steema.TeeChart.Styles.Series In Me.TChart1.Series
            If counter >= colorPalette.Length Then
                'loop color palette
                counter = 0
            End If
            series.Color = colorPalette(counter)
            'apply same color to series in overview chart
            For Each series2 As Steema.TeeChart.Styles.Series In Me.TChart2.Series
                If series2.Tag = series.Tag Then
                    series2.Color = colorPalette(counter)
                    Exit For
                End If
            Next
            counter += 1
        Next
    End Sub

    Private Sub IView_Close() Implements IView.Close
        'FIXME: Throw New NotImplementedException()
    End Sub

#End Region 'Funktionalität

End Class
