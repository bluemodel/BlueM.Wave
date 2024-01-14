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
Imports BlueM.Wave.AnnualStatistics
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

    ''' <summary>
    ''' Result of the analysis
    ''' </summary>
    Private result As Dictionary(Of Integer, MonthData)

    ''' <summary>
    ''' Flag indicating whether series values correspond to the previous month
    ''' </summary>
    ''' <remarks></remarks>
    Private isPreviousMonth As Boolean

    Private startMonth As Integer

    Public Overloads Shared Function Description() As String
        Return "Calculates the following statistical values for each month of the year: average, median, minimum, maximum, standard deviation " &
            "and displays them in a chart."
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result text
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
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

    Public Overrides ReadOnly Property hasResultTable() As Boolean
        Get
            Return True
        End Get
    End Property

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
        Me.startMonth = CType(dlg.ComboBox_startMonth.SelectedItem, Month).number

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

        reihe = Me.InputTimeSeries.Item(0).removeNaNValues()

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
                    .average = Double.NaN
                    .stddev = Double.NaN
                    .min = Double.NaN
                    .max = Double.NaN
                    .median = Double.NaN
                End If

            End With
        Next

    End Sub

    ''' <summary>
    ''' Prepare the analysis results
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub PrepareResults()

        'result table
        Me.ResultTable = New DataTable($"Monthly statistics: {Me.InputTimeSeries(0).Title}")

        Me.ResultTable.Columns.Add("Month", GetType(Integer))
        Me.ResultTable.Columns.Add("Name", GetType(String))
        Me.ResultTable.Columns.Add("ValueCount", GetType(Integer))
        Me.ResultTable.Columns.Add("Average", GetType(Double))
        Me.ResultTable.Columns.Add("Median", GetType(Double))
        Me.ResultTable.Columns.Add("Minimum", GetType(Double))
        Me.ResultTable.Columns.Add("Maximum", GetType(Double))
        Me.ResultTable.Columns.Add("Standard deviation", GetType(Double))

        For Each monthData As MonthData In Me.result.Values
            Me.ResultTable.Rows.Add(
                monthData.month.number,
                monthData.month.name,
                monthData.values.Count,
                monthData.average,
                monthData.median,
                monthData.min,
                monthData.max,
                monthData.stddev
            )
        Next

        'Result chart
        Me.ResultChart = New ScottPlot.Plot()
        Call Helpers.FormatChart(Me.ResultChart)
        Me.ResultChart.XAxis.DateTimeFormat(False)
        Me.ResultChart.Title($"Monthly statistics ({Me.InputTimeSeries(0).Title})")

        'sort month data by index for plotting
        Dim monthDatas As List(Of MonthData) = Me.result.Values.ToList()
        monthDatas.Sort(Function(m1 As MonthData, m2 As MonthData)
                            Return m1.index.CompareTo(m2.index)
                        End Function)

        'collect data
        Dim Xs As Double() = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}
        Dim xLabels As New List(Of String)
        Dim mins As New List(Of Double)
        Dim maxs As New List(Of Double)
        Dim avgs As New List(Of Double)
        Dim stddevbases As New List(Of Double)
        Dim stddevs As New List(Of Double)
        Dim medians As New List(Of Double)
        For Each monthdata As MonthData In monthDatas
            xLabels.Add(monthdata.month.name)
            avgs.Add(monthdata.average)
            medians.Add(monthdata.median)
            If monthdata.values.Count > 0 Then
                mins.Add(monthdata.min)
                maxs.Add(monthdata.max)
                stddevbases.Add(monthdata.stddev)
                stddevs.Add(monthdata.stddev)
            Else
                'series that cannot handle NaNs are displayed as 0
                mins.Add(0)
                maxs.Add(0)
                stddevbases.Add(0)
                stddevs.Add(0)
            End If
        Next

        'axes
        Me.ResultChart.XAxis.ManualTickPositions(Xs, xLabels.ToArray())
        Me.ResultChart.XAxis.AxisTicks.TickLabelRotation = 90
        Me.ResultChart.XAxis.AxisTicks.MinorGridVisible = False
        Me.ResultChart.YLabel(Me.InputTimeSeries(0).Unit)

        'series min-max
        Dim minmax As ScottPlot.Plottable.Polygon
        minmax = Me.ResultChart.AddFill(Xs, mins.ToArray(), maxs.ToArray())
        minmax.Label = "Min / Max"
        minmax.FillColor = Color.FromArgb(128, Color.LightGray.R, Color.LightGray.G, Color.LightGray.B)
        minmax.LineColor = Color.DarkGray
        minmax.LineWidth = 1

        'series standard deviation
        Dim stddev As ScottPlot.Plottable.ErrorBar
        stddev = Me.ResultChart.AddErrorBars(Xs, stddevbases.ToArray(), xErrors:=Nothing, yErrors:=stddevs.ToArray())
        stddev.Label = "Standard deviation"
        stddev.Color = Color.Red
        stddev.CapSize = 8

        'series average
        Dim avg As ScottPlot.Plottable.ScatterPlot
        avg = Me.ResultChart.AddScatter(Xs, avgs.ToArray())
        avg.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap
        avg.Label = "Average"
        avg.Color = Color.Blue
        avg.LineWidth = 2

        'series median
        Dim median As ScottPlot.Plottable.ScatterPlot
        median = Me.ResultChart.AddScatter(Xs, medians.ToArray())
        median.OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap
        median.Label = "Median"
        median.Color = Color.Green
        median.LineWidth = 1

    End Sub

End Class