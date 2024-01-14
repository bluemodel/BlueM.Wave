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
''' <summary>
''' Class for storing time series display options
''' </summary>
Public Class TimeSeriesDisplayOptions

    Private _color As Color
    Private _linestyle As Drawing2D.DashStyle
    Private _linewidth As Integer
    Private _showPoints As Boolean

    ''' <summary>
    ''' Color
    ''' </summary>
    ''' <remarks>Default color is empty. When empty, color is automatically assigned by the chart</remarks>
    ''' <returns></returns>
    Public Property Color As Color
        Get
            Return _color
        End Get
        Set(value As Color)
            _color = value
        End Set
    End Property

    ''' <summary>
    ''' Line style
    ''' </summary>
    ''' <returns></returns>
    Public Property LineStyle As ScottPlot.LineStyle
        Get
            Return _linestyle
        End Get
        Set(value As ScottPlot.LineStyle)
            _linestyle = value
        End Set
    End Property

    ''' <summary>
    ''' Line width
    ''' </summary>
    ''' <returns></returns>
    Public Property LineWidth As Integer
        Get
            Return _linewidth
        End Get
        Set(value As Integer)
            _linewidth = value
        End Set
    End Property

    ''' <summary>
    ''' Flag to show series points
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowPoints As Boolean
        Get
            Return _showPoints
        End Get
        Set(value As Boolean)
            _showPoints = value
        End Set
    End Property

    ''' <summary>
    ''' Creates a new TimeSeriesDisplayOption instance with default properties
    ''' </summary>
    ''' <remarks>Default Color is empty in order to allow automatic color assignment by the chart</remarks>
    Public Sub New()
        Me.Color = Color.Empty
        Me.LineStyle = ScottPlot.LineStyle.Solid
        Me.LineWidth = 2
        Me.ShowPoints = False
    End Sub

    ''' <summary>
    ''' Returns a copy of the instance
    ''' </summary>
    ''' <returns></returns>
    Public Function Copy() As TimeSeriesDisplayOptions
        Dim displayOptions As New TimeSeriesDisplayOptions() With {
            .Color = Me.Color,
            .LineStyle = Me.LineStyle,
            .LineWidth = Me.LineWidth,
            .ShowPoints = Me.ShowPoints
        }
        Return displayOptions
    End Function

    ''' <summary>
    ''' Sets the color from a color string
    ''' </summary>
    ''' <remarks>
    ''' Must be either the name of a known color: https://learn.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor
    ''' or a hexadecimal representation of ARGB values
    ''' </remarks>
    ''' <param name="colorString">the color string</param>
    Public Sub SetColor(colorString As String)
        If [Enum].IsDefined(GetType(KnownColor), colorString) Then
            Me.Color = Drawing.Color.FromName(colorString)
        Else
            Try
                'try to convert a hexadecimal representation of ARGB values
                Dim argb As Integer = Int32.Parse(colorString, Globalization.NumberStyles.HexNumber)
                Me.Color = Color.FromArgb(argb)
            Catch e As FormatException
                Log.AddLogEntry(levels.warning, $"Color '{colorString}' is not recognized!")
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Sets the line style from a string
    ''' </summary>
    ''' <remarks>Recognized line styles: https://learn.microsoft.com/en-us/dotnet/api/system.drawing.drawing2d.dashstyle</remarks>
    ''' <param name="lineStyle">the line style as string</param>
    Public Sub SetLineStyle(lineStyle As String)
        If Not [Enum].IsDefined(GetType(ScottPlot.LineStyle), lineStyle) Then
            Log.AddLogEntry(levels.warning, $"Line style '{lineStyle}' is not recognized!")
        Else
            Me.LineStyle = [Enum].Parse(GetType(ScottPlot.LineStyle), lineStyle)
        End If
    End Sub

    ''' <summary>
    ''' Sets the line width from a string
    ''' </summary>
    ''' <remarks>string must be convertible to an integer</remarks>
    ''' <param name="lineWidth">line width as string</param>
    Public Sub SetLineWidth(lineWidth As String)
        Dim lineWidthInt As Integer
        If Not Integer.TryParse(lineWidth, lineWidthInt) Then
            Log.AddLogEntry(levels.warning, $"Line width '{lineWidth}' can not be converted to integer!")
        Else
            Me.LineWidth = lineWidthInt
        End If
    End Sub

    ''' <summary>
    ''' Sets the ShowPoints property from a string
    ''' </summary>
    ''' <remarks>string must be convertible to a boolean</remarks>
    ''' <param name="showPoints">whether to show points</param>
    Public Sub SetShowPoints(showPoints As String)
        Select Case showPoints.ToLower()
            Case "true", "y", "1"
                Me.ShowPoints = True
            Case Else
                Me.ShowPoints = False
        End Select
    End Sub

End Class
