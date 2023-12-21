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
Imports System.IO

''' <summary>
''' Main chart window
''' </summary>
Friend Class MainWindow
    Implements IView

    Private _controller As WaveController

    ''' <summary>
    ''' Flag for preventing unintended feedback loops in the UI. Default is False, can be temporarily set to True.
    ''' </summary>
    ''' <remarks></remarks>
    Friend isInitializing As Boolean

    'ColorBand that is shown while zooming in main chart
    'TODO: TChart
    'Friend colorBandZoom As Steema.TeeChart.Tools.ColorBand

    'ColorBand representing current view extent of main chart in OverviewChart
    'TODO: TChart
    'Friend colorBandOverview As Steema.TeeChart.Tools.ColorBand

    'Cursors
    Friend cursor_pan As Cursor
    Friend cursor_zoom As Cursor

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
                Return DateTime.FromOADate(Me.MainPlot.Plot.XAxis.Dims.Min)
            Catch ex As ArgumentException
                Return Constants.minOADate
            End Try
        End Get
        Set(value As DateTime)
            If value < Constants.minOADate Then
                value = Constants.minOADate
            End If
            Me.MainPlot.Plot.XAxis.Dims.SetAxis(min:=value.ToOADate(), max:=Nothing)
            Me.MainPlot.Refresh()
        End Set
    End Property

    Friend Property ChartMaxX As DateTime
        Get
            Try
                Return DateTime.FromOADate(Me.MainPlot.Plot.XAxis.Dims.Max)
            Catch ex As ArgumentException
                Return Constants.maxOADate
            End Try
        End Get
        Set(value As DateTime)
            If value > Constants.maxOADate Then
                value = Constants.maxOADate
            End If
            Me.MainPlot.Plot.XAxis.Dims.SetAxis(min:=Nothing, max:=value.ToOADate())
            Me.MainPlot.Refresh()
        End Set
    End Property

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


    'Charts neu einrichten
    '*********************
    Friend Sub Init_Charts()

        'initialize main plot
        Me.MainPlot.Plot.Clear()
        Call Helpers.FormatChart(Me.MainPlot.Plot)
        Me.MainPlot.Refresh()

        'initialize overview plot
        Me.OverviewPlot.Plot.Clear()
        Call Helpers.FormatChart(Me.OverviewPlot.Plot)
        Me.OverviewPlot.Plot.Legend.IsVisible = False
        Me.OverviewPlot.Configuration.Pan = False
        Me.OverviewPlot.Configuration.Zoom = False
        Me.OverviewPlot.Refresh()

        'TODO: TChart
        'Me.TChart2.Panel.Brush.Color = Color.FromArgb(239, 239, 239)
        'Me.TChart2.Walls.Back.Color = Color.FromArgb(239, 239, 239)
        'Me.TChart2.Header.Visible = False
        'Me.TChart2.Legend.Visible = False

        ''Disable TeeChart builtin zooming and panning functionality
        'Me.MainPlot.Zoom.Direction = Steema.TeeChart.ZoomDirections.None
        'Me.MainPlot.Zoom.History = False
        'Me.MainPlot.Zoom.Animated = True
        'Me.MainPlot.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Me.TChart2.Zoom.Direction = Steema.TeeChart.ZoomDirections.None
        'Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        ''Achsen
        'Me.MainPlot.Axes.Bottom.Automatic = False
        'Me.MainPlot.Axes.Bottom.Labels.Angle = 90
        'Me.MainPlot.Axes.Bottom.Labels.DateTimeFormat = Helpers.CurrentDateFormat
        'Me.MainPlot.Axes.Right.Title.Angle = 90

        'Me.TChart2.Axes.Left.Labels.Font.Color = Color.FromArgb(100, 100, 100)
        'Me.TChart2.Axes.Left.Labels.Font.Size = 8
        'Me.TChart2.Axes.Bottom.Labels.Font.Color = Color.FromArgb(100, 100, 100)
        'Me.TChart2.Axes.Bottom.Labels.Font.Size = 8
        'Me.TChart2.Axes.Bottom.Automatic = False
        'Me.TChart2.Axes.Bottom.Labels.DateTimeFormat = Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern 'date only without time

        'ColorBand einrichten
        Call Me.Init_ColorBands()

    End Sub

    ''' <summary>
    ''' Initialize color bands
    ''' </summary>
    Friend Sub Init_ColorBands()

        'TODO: TChart
        'colorBandOverview = New Steema.TeeChart.Tools.ColorBand()
        'Me.OverviewPlot.Tools.Add(colorBandOverview)
        'colorBandOverview.Axis = Me.OverviewPlot.Axes.Bottom
        'colorBandOverview.Brush.Color = Color.Coral
        'colorBandOverview.Brush.Transparency = 50
        'colorBandOverview.ResizeEnd = False
        'colorBandOverview.ResizeStart = False
        'colorBandOverview.EndLinePen.Visible = False
        'colorBandOverview.StartLinePen.Visible = False

        'colorBandZoom = New Steema.TeeChart.Tools.ColorBand()
        'Me.MainPlot.Tools.Add(colorBandZoom)
        'colorBandZoom.Axis = Me.MainPlot.Axes.Bottom
        'colorBandZoom.Color = Color.Black
        'colorBandZoom.Pen.Color = Color.Black
        'colorBandZoom.Pen.Style = Drawing2D.DashStyle.Dash
        'colorBandZoom.Brush.Visible = False
        'colorBandZoom.ResizeEnd = False
        'colorBandZoom.ResizeStart = False
        'colorBandZoom.EndLinePen.Visible = True
        'colorBandZoom.StartLinePen.Visible = True
    End Sub

    Private Overloads Sub Close() Implements IView.Close
        Throw New NotImplementedException()
    End Sub

End Class
