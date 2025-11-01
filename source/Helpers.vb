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
Imports System.Globalization

Public Module Helpers

    ''' <summary>
    ''' Default NumberFormat
    ''' </summary>
    ''' <returns>NumberFormatInfo instance with decimal separator "." and no NumberGroupSeparator</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DefaultNumberFormat() As NumberFormatInfo
        Get
            'NumberFormatInfo einrichten
            DefaultNumberFormat = New NumberFormatInfo()
            DefaultNumberFormat.NumberDecimalSeparator = "."
            DefaultNumberFormat.NumberGroupSeparator = ""
            DefaultNumberFormat.NumberGroupSizes = New Integer() {3}
        End Get
    End Property

    ''' <summary>
    ''' Default Encoding (as set by the operating system, usually ISO-8859-1)
    ''' </summary>
    Public ReadOnly Property DefaultEncoding As Text.Encoding
        Get
            Return Text.Encoding.Default
        End Get
    End Property

    ''' <summary>
    ''' Returns the number format as set by the operating system
    ''' </summary>
    Public ReadOnly Property CurrentNumberFormat() As NumberFormatInfo
        Get
            Return Globalization.CultureInfo.CurrentCulture.NumberFormat
        End Get
    End Property

    ''' <summary>
    ''' Returns the current date time format as set by the operating system
    ''' </summary>
    Public ReadOnly Property CurrentDateFormat As String
        Get
            Dim format As DateTimeFormatInfo = Globalization.CultureInfo.CurrentCulture.DateTimeFormat
            Return format.ShortDatePattern & " " & format.ShortTimePattern
        End Get
    End Property

    ''' <summary>
    ''' Returns the list separator as set by the operating system (e.g. "," or ";")
    ''' </summary>
    Public ReadOnly Property CurrentListSeparator As String
        Get
            Return Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
        End Get
    End Property

    ''' <summary>
    ''' Date formats
    ''' </summary>
    ''' <returns>Dictionary of available DateFormats</returns>
    ''' <remarks>see https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings </remarks>
    Public ReadOnly Property DateFormats() As Dictionary(Of String, String)
        Get
            Dim dict As New Dictionary(Of String, String)
            dict.Add("current", Helpers.CurrentDateFormat)
            dict.Add("default", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO1", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO2", "yyyyMMdd HH:mm")
            dict.Add("HYSTEMEXTRAN", "ddMMyyyyHHmmss")
            dict.Add("SMUSI", "dd MM yyyy   HH")
            dict.Add("SWMM", "MM/dd/yyyy HH:mm:ss")
            dict.Add("UVF", "yyyyMMddHHmm") 'eigentlich nur zweistellige Jahreszahl, aber das Jahrhundert wird beim Einlesen trotzdem bestimmt
            dict.Add("WEL", "dd.MM.yyyy HH:mm")
            dict.Add("ZRE", "yyyyMMdd HH:mm")
            dict.Add("ZRXP", "yyyyMMddHHmmss")
            dict.Add("ISO", "yyyy-MM-dd HH:mm:ss")
            Return dict
        End Get
    End Property

    ''' <summary>
    ''' Converts a string to a double
    ''' </summary>
    ''' <param name="str">string to be converted</param>
    ''' <param name="format">optional NumberFormatInfo object to use for the conversion. If not provided, the DefaultNumberInfo is used.</param>
    ''' <returns>Double value, set to NaN if the string was not parseable. NaN and +-Infinity in the input string are recognized and converted to the correspoing Double value.</returns>
    ''' <remarks></remarks>
    Public Function StringToDouble(str As String, Optional format As NumberFormatInfo = Nothing) As Double

        Dim value As Double
        Dim success As Boolean

        If format Is Nothing Then
            'use default number format
            format = DefaultNumberFormat
        End If

        success = Double.TryParse(str, NumberStyles.Any, format, value)

        If (Not success) Then
            'string could not be parsed
            value = Double.NaN
        End If

        Return value

    End Function

    ''' <summary>
    ''' Returns a specified color palette
    ''' </summary>
    ''' <param name="name">Available color palettes are "Material", "Distinct", "Color Wheel" and "Random". Defaults to "Material".</param>
    ''' <returns>A color palette</returns>
    Public Function getColorPalette(Optional name As String = "Material") As Color()
        Dim colorPalette As Color()
        Select Case name
            Case "Material"
                'Material Design Palette
                'https://hexcolor.co/material-design-colors
                colorPalette = {
                    Color.FromArgb(3, 169, 244),
                    Color.FromArgb(244, 67, 54),
                    Color.FromArgb(156, 39, 176),
                    Color.FromArgb(63, 81, 181),
                    Color.FromArgb(0, 188, 212),
                    Color.FromArgb(0, 150, 136),
                    Color.FromArgb(76, 175, 80),
                    Color.FromArgb(205, 220, 57),
                    Color.FromArgb(255, 235, 59),
                    Color.FromArgb(255, 152, 0),
                    Color.FromArgb(121, 85, 72),
                    Color.FromArgb(158, 158, 158),
                    Color.FromArgb(96, 125, 139),
                    Color.FromArgb(0, 0, 0),
                    Color.FromArgb(62, 69, 81)
                }
            Case "Distinct"
                '20 Simple, Distinct Colors
                'https://sashamaps.net/docs/resources/20-colors/
                colorPalette = {
                    Color.FromArgb(230, 25, 75),
                    Color.FromArgb(60, 180, 75),
                    Color.FromArgb(0, 130, 200),
                    Color.FromArgb(245, 130, 48),
                    Color.FromArgb(145, 30, 180),
                    Color.FromArgb(70, 240, 240),
                    Color.FromArgb(240, 50, 230),
                    Color.FromArgb(0, 128, 128),
                    Color.FromArgb(170, 110, 40),
                    Color.FromArgb(128, 0, 0),
                    Color.FromArgb(128, 128, 0),
                    Color.FromArgb(0, 0, 128),
                    Color.FromArgb(128, 128, 128)
                }
            Case "Color Wheel"
                '12 Color Wheel
                'http://www.changingminds.org/explanations/perception/visual/12-wheel.htm
                colorPalette = {
                    Color.FromArgb(255, 0, 0),
                    Color.FromArgb(0, 0, 255),
                    Color.FromArgb(0, 255, 0),
                    Color.FromArgb(255, 0, 255),
                    Color.FromArgb(0, 255, 255),
                    Color.FromArgb(255, 255, 0),
                    Color.FromArgb(255, 127, 10),
                    Color.FromArgb(255, 0, 127),
                    Color.FromArgb(127, 0, 255),
                    Color.FromArgb(127, 0, 255),
                    Color.FromArgb(0, 127, 255),
                    Color.FromArgb(0, 255, 127),
                    Color.FromArgb(127, 255, 0),
                    Color.FromArgb(255, 127, 10)
                }
            Case "Random"
                Dim nColors As Integer = 20
                ReDim colorPalette(nColors - 1)
                Dim random As New Random()
                For i As Integer = 0 To nColors - 1
                    colorPalette(i) = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))
                Next
            Case Else
                colorPalette = getColorPalette()
        End Select
        Return colorPalette
    End Function

    ''' <summary>
    ''' Returns a list of calendar months from January to December
    ''' </summary>
    ''' <returns></returns>
    Public Function CalendarMonths() As List(Of Month)
        Dim months As New List(Of Month) From {
            New Month(1, "January"),
            New Month(2, "February"),
            New Month(3, "March"),
            New Month(4, "April"),
            New Month(5, "May"),
            New Month(6, "June"),
            New Month(7, "July"),
            New Month(8, "August"),
            New Month(9, "September"),
            New Month(10, "October"),
            New Month(11, "November"),
            New Month(12, "December")
        }
        Return months
    End Function

    ''' <summary>
    ''' Formats a chart with default settings
    ''' </summary>
    ''' <param name="chart"></param>
    Friend Sub ChartSetDefaultFormat(ByRef chart As Steema.TeeChart.Chart)

        'set default color palette
        chart.ColorPalette = Helpers.getColorPalette()

        chart.Aspect.View3D = False
        'chart.BackColor = Color.White
        chart.Panel.Gradient.Visible = False
        chart.Panel.Brush.Color = Color.White
        chart.Walls.Back.Transparent = False
        chart.Walls.Back.Gradient.Visible = False
        chart.Walls.Back.Color = Color.White

        'Header
        chart.Header.Font.Name = "GenericSansSerif"
        chart.Header.Font.Color = Color.Black
        chart.Header.Text = ""

        'Legende
        chart.Legend.Font.Name = "GenericSansSerif"
        chart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        chart.Legend.FontSeriesColor = True
        chart.Legend.CheckBoxes = True

        'Achsen
        chart.Axes.DrawBehind = False

        chart.Axes.Left.Title.Font.Name = "GenericSansSerif"
        chart.Axes.Left.Title.Font.Color = Color.Black

        chart.Axes.Left.Labels.Font.Name = "GenericSansSerif"
        chart.Axes.Left.Labels.Font.Color = Color.Black

        chart.Axes.Left.AxisPen.Visible = True

        chart.Axes.Left.Grid.Visible = True
        chart.Axes.Left.Grid.Style = Drawing2D.DashStyle.Dash

        chart.Axes.Right.Title.Font.Name = "GenericSansSerif"
        chart.Axes.Right.Title.Font.Color = Color.Black

        chart.Axes.Right.Labels.Font.Name = "GenericSansSerif"
        chart.Axes.Right.Labels.Font.Color = Color.Black

        chart.Axes.Right.AxisPen.Visible = True

        chart.Axes.Right.Grid.Visible = False
        chart.Axes.Right.Grid.Style = Drawing2D.DashStyle.Dash

        chart.Axes.Bottom.Title.Font.Name = "GenericSansSerif"
        chart.Axes.Bottom.Title.Font.Color = Color.Black

        chart.Axes.Bottom.Labels.Font.Name = "GenericSansSerif"
        chart.Axes.Bottom.Labels.Font.Color = Color.Black

        chart.Axes.Bottom.Automatic = True

        chart.Axes.Bottom.AxisPen.Visible = True

        chart.Axes.Bottom.Grid.Visible = True
        chart.Axes.Bottom.Grid.Style = Drawing2D.DashStyle.Dash

        'set font size with user setting
        Call ChartSetFontSize(chart, My.Settings.defaultFontSize)

    End Sub

    ''' <summary>
    ''' Sets the font size of all chart elements
    ''' </summary>
    ''' <param name="chart"></param>
    ''' <param name="size"></param>
    Friend Sub ChartSetFontSize(ByRef chart As Steema.TeeChart.Chart, size As Integer)
        'Set font size for all chart elements
        chart.Header.Font.Size = size + 2
        chart.Legend.Font.Size = size
        chart.Axes.Left.Title.Font.Size = size
        chart.Axes.Left.Labels.Font.Size = size
        chart.Axes.Right.Title.Font.Size = size
        chart.Axes.Right.Labels.Font.Size = size
        chart.Axes.Bottom.Title.Font.Size = size
        chart.Axes.Bottom.Labels.Font.Size = size
        chart.Axes.Top.Title.Font.Size = size
        chart.Axes.Top.Labels.Font.Size = size
        chart.Axes.Depth.Title.Font.Size = size
        chart.Axes.Depth.Labels.Font.Size = size
        chart.Axes.DepthTop.Title.Font.Size = size
        chart.Axes.DepthTop.Labels.Font.Size = size
        For Each axis As Steema.TeeChart.Axis In chart.Axes.Custom
            axis.Labels.Font.Size = size
            axis.Title.Font.Size = size
        Next
    End Sub

    '''<summary>
    '''Creates a relative path from one file or folder to another.
    '''</summary>
    '''<param name="fromPath">Contains the directory that defines the start of the relative path.</param>
    '''<param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
    '''<returns>The relative path from the start directory to the end path.</returns>
    '''<exception cref="ArgumentNullException"><paramref name="fromPath"/> or <paramref name="toPath"/> is <c>null</c>.</exception>
    '''<exception cref="UriFormatException"></exception>
    '''<exception cref="InvalidOperationException"></exception>
    '''<remarks>
    '''This function is a replacement for IO.Path.GetRelativePath() which is only available in later .NET versions.
    '''Based on C# code from https://stackoverflow.com/a/32113484
    '''</remarks>
    Public Function GetRelativePath(fromPath As String, toPath As String) As String

        If String.IsNullOrEmpty(fromPath) Then
            Throw New ArgumentNullException("fromPath")
        End If
        If String.IsNullOrEmpty(toPath) Then
            Throw New ArgumentNullException("toPath")
        End If

        Dim fromUri As Uri = New Uri(AppendDirectorySeparatorChar(fromPath))
        Dim toUri As Uri = New Uri(AppendDirectorySeparatorChar(toPath))

        If fromUri.Scheme <> toUri.Scheme Then
            Return toPath
        End If

        Dim relativeUri As Uri = fromUri.MakeRelativeUri(toUri)
        Dim relativePath As String = Uri.UnescapeDataString(relativeUri.ToString())

        If String.Equals(toUri.Scheme, Uri.UriSchemeFile, StringComparison.OrdinalIgnoreCase) Then
            relativePath = relativePath.Replace(IO.Path.AltDirectorySeparatorChar, IO.Path.DirectorySeparatorChar)
        End If

        Return relativePath
    End Function

    Private Function AppendDirectorySeparatorChar(path As String) As String
        'Append a slash only if the path is a directory and does not have a slash.
        If Not IO.Path.HasExtension(path) And Not path.EndsWith(IO.Path.DirectorySeparatorChar.ToString()) Then
            Return path + IO.Path.DirectorySeparatorChar
        End If

        Return path
    End Function

End Module
