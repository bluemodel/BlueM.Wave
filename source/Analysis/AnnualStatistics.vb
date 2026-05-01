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
Imports System.Data

''' <summary>
''' Calculates annual statistics (min, max, avg, vol) based on hydrological years
''' </summary>
''' <remarks></remarks>
Friend Class AnnualStatistics
    Inherits Analysis

    Public Structure struct_stat
        Public startDate As Date
        Public endDate As Date
        Public len As Long
        Public min As Double
        Public max As Double
        Public avg As Double
        Public sum As Double
        Public vol As Double
    End Structure

    ''' <summary>
    ''' Nested dictionary with key of series title and values as dictionary with key of period (entire series or year) and value of struct_stat with the calculated statistics for the period
    ''' </summary>
    Private stats As Dictionary(Of String, Dictionary(Of String, struct_stat))
    Private generateBoundingBoxes As Boolean

    Public Overloads Shared Function Description() As String
        Return "Calculates annual statistics (min, max, avg, vol) based on hydrological years."
    End Function

    Public Overrides ReadOnly Property hasResultChart() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has result series
    ''' that should be added to the main diagram
    ''' </summary>
    Public Overrides ReadOnly Property hasResultSeries() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property hasResultTable() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Sub New(ByRef series As List(Of TimeSeries))
        MyBase.New(series)
        stats = New Dictionary(Of String, Dictionary(Of String, struct_stat))()
    End Sub

    Private Function calculateStats(ByRef series As TimeSeries) As struct_stat
        Dim stats As struct_stat
        stats.startDate = series.StartDate
        stats.endDate = series.EndDate
        stats.len = series.Length
        stats.min = series.Minimum
        stats.max = series.Maximum
        stats.avg = series.Average
        stats.sum = series.Sum
        stats.vol = series.Volume
        Return stats
    End Function

    Public Overrides Sub ProcessAnalysis()

        Dim hyoseries As Dictionary(Of Integer, TimeSeries)
        Dim year As Integer
        Dim series As TimeSeries

        Dim dlg As New AnnualStatistics_Dialog()
        If dlg.ShowDialog() <> DialogResult.OK Then
            Throw New AnalysisCancelledException("Analysis cancelled")
        End If

        Dim startMonth As Integer = CType(dlg.ComboBox_startMonth.SelectedItem, Month).number
        Me.generateBoundingBoxes = dlg.CheckBox_boundingbox.Checked

        For Each ts As TimeSeries In Me.InputTimeSeries

            Dim tsStats As New Dictionary(Of String, struct_stat)()

            'stats for entire series
            tsStats.Add("Entire series", calculateStats(ts))

            'stats for hydrological years
            hyoseries = ts.SplitHydroYears(startMonth)
            For Each kvp As KeyValuePair(Of Integer, TimeSeries) In hyoseries
                year = kvp.Key
                series = kvp.Value
                tsStats.Add(year.ToString, calculateStats(series))
            Next

            Me.stats.Add(ts.Title, tsStats)

        Next

    End Sub


    Public Overrides Sub PrepareResults()

        'result table
        Me.ResultTable = New DataTable($"Annual statistics: {String.Join(", ", Me.InputTimeSeries.Select(Function(ts1) ts1.Title))}")

        Me.ResultTable.Columns.Add("Series", GetType(String))
        Me.ResultTable.Columns.Add("Period", GetType(String))
        Me.ResultTable.Columns.Add("Start", GetType(DateTime))
        Me.ResultTable.Columns.Add("End", GetType(DateTime))
        Me.ResultTable.Columns.Add("Length", GetType(Integer))
        Me.ResultTable.Columns.Add("Min", GetType(Double))
        Me.ResultTable.Columns.Add("Max", GetType(Double))
        Me.ResultTable.Columns.Add("Avg", GetType(Double))
        Me.ResultTable.Columns.Add("Sum", GetType(Double))
        Me.ResultTable.Columns.Add("Volume", GetType(Double))

        For Each ts As TimeSeries In Me.InputTimeSeries
            Dim tsStats As Dictionary(Of String, struct_stat) = Me.stats(ts.Title)
            For Each kvp As KeyValuePair(Of String, struct_stat) In tsStats
                Dim period As String = kvp.Key
                Dim stat As struct_stat = kvp.Value
                Me.ResultTable.Rows.Add(
                    ts.Title,
                    period,
                    stat.startDate,
                    stat.endDate,
                    stat.len,
                    stat.min,
                    stat.max,
                    stat.avg,
                    stat.sum,
                    stat.vol
                )
            Next
        Next

        'Generate timeseries with max/avg/min values if checkbox is checked and at least two years are present
        If Me.generateBoundingBoxes Then
            MyBase.ResultSeries = New List(Of TimeSeries)
            For Each ts As TimeSeries In Me.InputTimeSeries
                Dim tsStats As Dictionary(Of String, struct_stat) = Me.stats(ts.Title)
                If tsStats.Count > 2 Then
                    'Prepare output timeseries
                    Dim basename As String = ts.Title
                    Dim timeseries_max As New TimeSeries(basename & " (annual maximum)")
                    Dim timeseries_avg As New TimeSeries(basename & " (annual average)")
                    Dim timeseries_min As New TimeSeries(basename & " (annual minimum)")

                    'Set interpretation mode
                    timeseries_max.Interpretation = TimeSeries.InterpretationEnum.BlockRight
                    timeseries_avg.Interpretation = TimeSeries.InterpretationEnum.BlockRight
                    timeseries_min.Interpretation = TimeSeries.InterpretationEnum.BlockRight

                    'Transfer unit from input timeseries
                    Dim unit As String = ts.Unit
                    timeseries_max.Unit = unit
                    timeseries_avg.Unit = unit
                    timeseries_min.Unit = unit

                    'Set data source origin
                    timeseries_max.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
                    timeseries_avg.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
                    timeseries_min.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)

                    'Fill timeseries with values of max, avg, min
                    For Each kvp As KeyValuePair(Of String, struct_stat) In tsStats
                        If IsNumeric(kvp.Key) Then
                            Dim stat As struct_stat = kvp.Value
                            timeseries_max.AddNode(stat.startDate, stat.max)
                            timeseries_avg.AddNode(stat.startDate, stat.avg)
                            timeseries_min.AddNode(stat.startDate, stat.min)
                        End If
                    Next

                    'store output timeseries
                    MyBase.ResultSeries.Add(timeseries_max)
                    MyBase.ResultSeries.Add(timeseries_avg)
                    MyBase.ResultSeries.Add(timeseries_min)

                End If
            Next
        End If

    End Sub

End Class
