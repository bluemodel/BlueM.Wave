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

    ''' <summary>
    ''' Color
    ''' </summary>
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
    Public Property LineStyle As Drawing2D.DashStyle
        Get
            Return _linestyle
        End Get
        Set(value As Drawing2D.DashStyle)
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
    ''' Creates a new TimeSeriesDisplayOption instance with empty properties
    ''' </summary>
    Public Sub New()
        Me.Color = Nothing
        Me.LineStyle = Nothing
        Me.LineWidth = Nothing
    End Sub

    ''' <summary>
    ''' Sets the color from a color name
    ''' </summary>
    ''' <remarks>Recognized colors: https://learn.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor</remarks>
    ''' <param name="colorName">the color name</param>
    Public Sub SetColor(colorName As String)
        If Not [Enum].IsDefined(GetType(KnownColor), colorName) Then
            Log.AddLogEntry(levels.warning, $"Color '{colorName}' is not recognized!")
            Me.Color = Nothing
        Else
            Me.Color = Drawing.Color.FromName(colorName)
        End If
    End Sub

    ''' <summary>
    ''' Sets the line style from a string
    ''' </summary>
    ''' <remarks>Recognized line styles: https://learn.microsoft.com/en-us/dotnet/api/system.drawing.drawing2d.dashstyle</remarks>
    ''' <param name="lineStyle">the line style as string</param>
    Public Sub SetLineStyle(lineStyle As String)
        If Not [Enum].IsDefined(GetType(Drawing2D.DashStyle), lineStyle) Then
            Log.AddLogEntry(levels.warning, $"Line style '{lineStyle}' is not recognized!")
            Me.LineStyle = Nothing
        Else
            Me.LineStyle = [Enum].Parse(GetType(Drawing2D.DashStyle), lineStyle)
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
            Me.LineWidth = Nothing
        Else
            Me.LineWidth = lineWidthInt
        End If
    End Sub

End Class
