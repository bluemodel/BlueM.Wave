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
''' <summary>
'''Calculates the following statistical values for each month of the year:
''' Average
''' Median
''' Minimum / Maximum
''' Standard Deviation
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:MonthlyStatistics</remarks>
Friend Class MonthlyStatistics
    Inherits Analysis

#Region "Data structures"

    ''' <summary>
    ''' Class for storing the statistic values of a month
    ''' </summary>
    Private Class MonthData
        Public month As Month
        Public index As Integer
        Public values As List(Of Double)
        Public average As Double
        Public stddev As Double
        Public min As Double
        Public max As Double
        Public median As Double
    End Class


#End Region 'Data structures

#Region "Members"

    ''' <summary>
    ''' Result of the analysis
    ''' </summary>
    Private result As Dictionary(Of Integer, MonthData)

    ''' <summary>
    ''' Flag indicating whether series values correspond to the previous month
    ''' </summary>
    ''' <remarks></remarks>
    Private isPreviousMonth As Boolean

#End Region 'Members

#Region "Properties"

    Public Overloads Shared Function Description() As String
        Return "Calculates the following statistical values for each month of the year: average, median, minimum, maximum, standard deviation " &
            "and displays them in a chart."
    End Function

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

    ''' <summary>
    ''' Flag indicating whether the analysis function has result series
    ''' that should be added to the main diagram
    ''' </summary>
    Public Overrides ReadOnly Property hasResultSeries() As Boolean
        Get
            Return False
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

        Dim dlg As New MonthlyStatisticsDialog()
        Dim dlg_result As DialogResult = dlg.ShowDialog()

        If Not dlg_result = DialogResult.OK Then
            Throw New Exception("User abort")
        End If

        Me.isPreviousMonth = (dlg.ComboBox_MonthType.SelectedItem = "previous month")
        Dim startMonth As Integer = CType(dlg.ComboBox_startMonth.SelectedItem, Month).number

        'Initialize result data structure
        Me.result = New Dictionary(Of Integer, MonthData)
        For Each month As Month In Helpers.CalendarMonths
            Me.result.Add(month.number, New MonthData() With {
                .month = month,
                .values = New List(Of Double)
                })
            'set index according to selected start month
            If month.number >= startMonth Then
                Me.result(month.number).index = month.number - startMonth
            Else
                Me.result(month.number).index = month.number - startMonth + 12
            End If
        Next

    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub ProcessAnalysis()

        Dim reihe As TimeSeries
        Dim i, j, month As Integer
        Dim N As Long
        Dim sum, sumofsquares As Double

        reihe = Me.mZeitreihen.Item(0).removeNaNValues()

        'Sort values into months
        For i = 0 To reihe.Length - 1
            month = reihe.Dates(i).Month
            'assign to previous month if necessary
            If Me.isPreviousMonth Then
                If (month - 1) >= 1 Then
                    month = month - 1
                Else
                    month = 12
                End If
            End If
            Me.result(month).values.Add(reihe.Values(i))
        Next

        'Analyse each month
        For i = 1 To 12
            With Me.result(i)
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
                    Log.AddLogEntry(Log.levels.warning, $"The series does not contain any data for the month of { .month}!")
                    'TODO: ideally we would set all result values to Double.NaN here but this inexplicably causes the result chart to crash!
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
        Me.mResultText &= String.Join(Helpers.CurrentListSeparator, "Month", "Name", "ValueCount", "Average", "Median", "Min", "Max", "Stddev") & eol

        'data
        For Each monthData As MonthData In Me.result.Values
            If monthData.values.Count > 0 Then
                Me.mResultText &= String.Join(Helpers.CurrentListSeparator,
                    monthData.month.number,
                    monthData.month.name,
                    monthData.values.Count,
                    monthData.average,
                    monthData.median,
                    monthData.min,
                    monthData.max,
                    monthData.stddev) & eol
            Else
                Me.mResultText &= String.Join(Helpers.CurrentListSeparator,
                    monthData.month.number,
                    monthData.month.name,
                    monthData.values.Count,
                    Double.NaN,
                    Double.NaN,
                    Double.NaN,
                    Double.NaN,
                    Double.NaN) & eol
            End If
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
        Me.mResultChart.Header.Text = $"Monthly statistics ({Me.mZeitreihen(0).Title})"

        'Axes
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Text
        Me.mResultChart.Axes.Bottom.Labels.Angle = 90
        Me.mResultChart.Axes.Bottom.MinorTickCount = 0
        Me.mResultChart.Axes.Left.Title.Text = Me.mZeitreihen(0).Unit

        'Series

        'MinMax
        minmax = New Steema.TeeChart.Styles.HighLow(Me.mResultChart)
        minmax.DefaultNullValue = Double.NaN
        minmax.Title = "Min / Max"
        minmax.Color = Color.DarkGray
        minmax.Pen.Color = Color.DarkGray
        minmax.HighBrush.Visible = True
        minmax.HighBrush.Color = Color.LightGray
        minmax.HighBrush.Transparency = 50
        For i = 1 To 12
            minmax.Add(Me.result(i).index, Me.result(i).max, Me.result(i).min, Me.result(i).month.name)
        Next

        'Standard deviation
        stdabw = New Steema.TeeChart.Styles.Error(Me.mResultChart)
        stdabw.DefaultNullValue = Double.NaN
        stdabw.Title = "Standard deviation"
        stdabw.Color = Color.Red
        stdabw.ErrorWidth = 50
        For i = 1 To 12
            'Skip months with no or NaN data
            If Me.result(i).values.Count > 0 And Not Double.IsNaN(Me.result(i).stddev) Then
                stdabw.Add(Me.result(i).index, Me.result(i).average, Me.result(i).stddev, Me.result(i).month.name)
            End If
        Next

        'Average
        mittelwert = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        mittelwert.TreatNaNAsNull = True
        mittelwert.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.DoNotPaint
        mittelwert.Title = "Average"
        mittelwert.Color = Color.Blue
        mittelwert.LinePen.Width = 2
        For i = 1 To 12
            mittelwert.Add(Me.result(i).index, Me.result(i).average, Me.result(i).month.name)
        Next

        'Median
        median = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        median.TreatNaNAsNull = True
        median.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.DoNotPaint
        median.Title = "Median"
        median.Color = Color.Green
        For i = 1 To 12
            median.Add(Me.result(i).index, Me.result(i).median, Me.result(i).month.name)
        Next

    End Sub

#End Region 'Methods

End Class