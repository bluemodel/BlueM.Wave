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
    ''' Default DateFormat
    ''' </summary>
    ''' <returns>DateFormats("default")</returns>
    ''' <remarks>dd.MM.yyyy HH:mm</remarks>
    Public ReadOnly Property DefaultDateFormat() As String
        Get
            Return DateFormats("default")
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
    ''' Returns the date format as set by the operating system
    ''' </summary>
    Public ReadOnly Property CurrentDateFormat As DateTimeFormatInfo
        Get
            Return Globalization.CultureInfo.CurrentCulture.DateTimeFormat
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
            dict.Add("default", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO1", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO2", "yyyyMMdd HH:mm")
            dict.Add("SMUSI", "dd MM yyyy   HH")
            dict.Add("SWMM", "MM/dd/yyyy HH:mm:ss")
            dict.Add("UVF", "yyyyMMddHHmm") 'eigentlich nur zweistellige Jahreszahl, aber das Jahrhundert wird beim Einlesen trotzdem bestimmt
            dict.Add("WEL", "dd.MM.yyyy HH:mm")
            dict.Add("ZRE", "yyyyMMdd HH:mm")
            dict.Add("ZRXP", "yyyyMMddHHmmss")
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
    Public Function getColorPalette(Optional name As String = "Material")
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
    ''' Führt Standardformatierung eines TCharts aus
    ''' </summary>
    ''' <param name="chart"></param>
    Friend Sub FormatChart(ByRef chart As Steema.TeeChart.Chart)

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
        chart.Header.Font.Size = 12
        chart.Header.Text = ""

        'Legende
        chart.Legend.Font.Name = "GenericSansSerif"
        chart.Legend.Font.Size = 10
        chart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        chart.Legend.FontSeriesColor = True
        chart.Legend.CheckBoxes = True

        'Achsen
        chart.Axes.DrawBehind = False

        chart.Axes.Left.Title.Font.Name = "GenericSansSerif"
        chart.Axes.Left.Title.Font.Color = Color.Black
        chart.Axes.Left.Title.Font.Size = 10

        chart.Axes.Left.Labels.Font.Name = "GenericSansSerif"
        chart.Axes.Left.Labels.Font.Color = Color.Black
        chart.Axes.Left.Labels.Font.Size = 10

        chart.Axes.Left.AxisPen.Visible = True

        chart.Axes.Left.Grid.Visible = True
        chart.Axes.Left.Grid.Style = Drawing2D.DashStyle.Dash

        chart.Axes.Right.Title.Font.Name = "GenericSansSerif"
        chart.Axes.Right.Title.Font.Color = Color.Black
        chart.Axes.Right.Title.Font.Size = 10

        chart.Axes.Right.Labels.Font.Name = "GenericSansSerif"
        chart.Axes.Right.Labels.Font.Color = Color.Black
        chart.Axes.Right.Labels.Font.Size = 10

        chart.Axes.Right.AxisPen.Visible = True

        chart.Axes.Right.Grid.Visible = False
        chart.Axes.Right.Grid.Style = Drawing2D.DashStyle.Dash

        chart.Axes.Bottom.Title.Font.Name = "GenericSansSerif"
        chart.Axes.Bottom.Title.Font.Color = Color.Black
        chart.Axes.Bottom.Title.Font.Size = 10

        chart.Axes.Bottom.Labels.Font.Name = "GenericSansSerif"
        chart.Axes.Bottom.Labels.Font.Color = Color.Black
        chart.Axes.Bottom.Labels.Font.Size = 10

        chart.Axes.Bottom.Automatic = True

        chart.Axes.Bottom.AxisPen.Visible = True

        chart.Axes.Bottom.Grid.Visible = True
        chart.Axes.Bottom.Grid.Style = Drawing2D.DashStyle.Dash

    End Sub

End Module
