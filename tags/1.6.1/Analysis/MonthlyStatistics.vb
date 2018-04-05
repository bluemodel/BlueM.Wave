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
''' <summary>
'''Calculates the following statistical values for each month of the year:
''' Average
''' Median
''' Minimum / Maximum
''' Standard Deviation
''' </summary>
''' <remarks>http://wiki.bluemodel.org/index.php/Wave:MonthlyStatistics</remarks>
Public Class MonthlyStatistics
    Inherits Analysis

#Region "Data structures"

    ''' <summary>
    ''' Structure for storing the statistic values of a month
    ''' </summary>
    Private Structure monthType
        Dim name As String
        Dim index As Integer
        Dim values As List(Of Double)
        Dim average As Double
        Dim stddev As Double
        Dim min As Double
        Dim max As Double
        Dim median As Double
    End Structure

    ''' <summary>
    ''' Structure for storing results
    ''' </summary>
    Private Structure resultType
        Dim months() As monthType
    End Structure

#End Region 'Data structures

#Region "Members"

    ''' <summary>
    ''' Result of the analysis
    ''' </summary>
    Private result As resultType

#End Region 'Members

#Region "Properties"

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result text
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has result values
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result diagram
    ''' </summary>
    Public Overrides ReadOnly Property hasResultChart() As Boolean
        Get
            Return True
        End Get
    End Property

#End Region ' Properties

#Region "Methoden"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="series">list of series to analyse</param>
    Public Sub New(ByRef series As List(Of TimeSeries))

        Call MyBase.New(series)

        'Check: must be only one series
        If (series.Count <> 1) Then
            Throw New Exception("The Monthly analysis requires that exactly 1 time series is selected!")
        End If

        Dim i As Integer

        'Initialize result structure
        ReDim Me.result.months(11)
        Me.result.months(0).name = "January"
        Me.result.months(0).index = 3
        Me.result.months(1).name = "February"
        Me.result.months(1).index = 4
        Me.result.months(2).name = "March"
        Me.result.months(2).index = 5
        Me.result.months(3).name = "April"
        Me.result.months(3).index = 6
        Me.result.months(4).name = "May"
        Me.result.months(4).index = 7
        Me.result.months(5).name = "June"
        Me.result.months(5).index = 8
        Me.result.months(6).name = "July"
        Me.result.months(6).index = 9
        Me.result.months(7).name = "August"
        Me.result.months(7).index = 10
        Me.result.months(8).name = "September"
        Me.result.months(8).index = 11
        Me.result.months(9).name = "October"
        Me.result.months(9).index = 12
        Me.result.months(10).name = "November"
        Me.result.months(10).index = 1
        Me.result.months(11).name = "December"
        Me.result.months(11).index = 2

        For i = 0 To 11
            Me.result.months(i).values = New List(Of Double)
        Next

    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub ProcessAnalysis()

        Dim reihe As TimeSeries
        Dim i, j As Integer
        Dim N As Long
        Dim sum, sumofsquares As Double

        reihe = Me.mZeitreihen.Item(0).getCleanZRE()

        'Sort values into months
        For i = 0 To reihe.Length - 1
            Me.result.months(reihe.Dates(i).Month() - 1).values.Add(reihe.Values(i))
        Next

        'Analyse each month
        For i = 0 To 11
            With Me.result.months(i)
                sum = 0
                sumofsquares = 0
                N = .values.Count
                If (N > 0) Then
                    'calculate sums
                    For j = 0 To N - 1
                        sum += .values(j)
                        sumofsquares += .values(j) ^ 2
                    Next
                    'Average
                    .average = sum / N
                    'Standard deviation
                    .stddev = Math.Sqrt((N * sumofsquares - sum ^ 2) / (N * (N - 1)))
                    'Sort values
                    Call .values.Sort()
                    'Min and Max
                    .min = .values(0)
                    .max = .values(N - 1)
                    'Median
                    If (N Mod 2 = 0) Then
                        'even number: average of the two middle values
                        .median = (.values((N / 2) - 1) + .values(N / 2)) / 2
                    Else
                        'uneven number: middle value
                        .median = .values(((N + 1) / 2) - 1)
                    End If
                Else
                    MsgBox("The series does not contain any data for the month of " & .name & "!", MsgBoxStyle.Information)
                End If

            End With
        Next

    End Sub

    ''' <summary>
    ''' Prepare the analysis results
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub PrepareResults()

        'Result text
        '------------
        Me.mResultText = "Monthly statistics have been calculated." & eol
        Me.mResultText &= "Result data:" & eol
        'header line
        Me.mResultText &= "Name;average;median;min;max;stddev" &eol

        'data
        For Each month As monthType In Me.result.months
            Me.mResultText &= month.name & ";"
            Me.mResultText &= month.average & ";"
            Me.mResultText &= month.median & ";"
            Me.mResultText &= month.min & ";"
            Me.mResultText &= month.max & ";"
            Me.mResultText &= month.stddev & eol

        Next

        'Result chart
        '------------
        Dim i As Integer
        Dim mittelwert, median As Steema.TeeChart.Styles.Line
        Dim stdabw As Steema.TeeChart.Styles.Error
        Dim minmax As Steema.TeeChart.Styles.HighLow

        'Diagram
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Monthly statistics (" & Me.mZeitreihen(0).Title & ")"

        'Axes
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Text
        Me.mResultChart.Axes.Bottom.Labels.Angle = 90
        Me.mResultChart.Axes.Bottom.MinorTickCount = 0

        'Series

        'MinMax
        minmax = New Steema.TeeChart.Styles.HighLow(Me.mResultChart)
        minmax.Title = "Min / Max"
        minmax.Color = Color.DarkGray
        minmax.Pen.Color = Color.DarkGray
        minmax.HighBrush.Visible = True
        minmax.HighBrush.Color = Color.LightGray
        minmax.HighBrush.Transparency = 50
        For i = 0 To 11
            minmax.Add(Me.result.months(i).index, Me.result.months(i).max, Me.result.months(i).min, Me.result.months(i).name)
        Next

        'Standard deviation
        stdabw = New Steema.TeeChart.Styles.Error(Me.mResultChart)
        stdabw.Title = "Standard deviation"
        stdabw.Color = Color.Red
        stdabw.ErrorWidth = 50
        For i = 0 To 11
            stdabw.Add(Me.result.months(i).index, Me.result.months(i).average, Me.result.months(i).stddev, Me.result.months(i).name)
        Next

        'Average
        mittelwert = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        mittelwert.Title = "Average"
        mittelwert.Color = Color.Blue
        mittelwert.LinePen.Width = 2
        For i = 0 To 11
            mittelwert.Add(Me.result.months(i).index, Me.result.months(i).average, Me.result.months(i).name)
        Next

        'Median
        median = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        median.Title = "Median"
        median.Color = Color.Green
        For i = 0 To 11
            median.Add(Me.result.months(i).index, Me.result.months(i).median, Me.result.months(i).name)
        Next

    End Sub

#End Region 'Methods

End Class