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
        Public vol As Double
    End Structure

    Private timeseries As TimeSeries
    Private startOfHydrologicalYear As Integer
    Private stats As Dictionary(Of String, struct_stat)
    Private generateBoundingBoxes As Boolean

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Calculates annual statistics (min, max, avg, vol) based on hydrological years."
        End Get
    End Property

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

    Public Sub New()
        MyBase.New()
        MyBase.parameters.Add("timeseries",
            New AnalysisParameter("Input time series", AnalysisParameter.ParameterTypeEnum.Timeseries, AnalysisParameter.ParameterAmountEnum.Single)
        )
        MyBase.parameters.Add("startOfHydrologicalYear",
            New AnalysisParameter("Start of hydrological year", AnalysisParameter.ParameterTypeEnum.Integer, AnalysisParameter.ParameterAmountEnum.Single, def:=11, min:=1, max:=12)
        )
        MyBase.parameters.Add("generateBoundingBoxes",
            New AnalysisParameter("Generate annual bounding boxes (max, avg, min)", AnalysisParameter.ParameterTypeEnum.Boolean, AnalysisParameter.ParameterAmountEnum.Single, def:=False)
        )
        'initialize data structures
        stats = New Dictionary(Of String, struct_stat)
    End Sub

    Private Function calculateStats(ByRef series As TimeSeries) As struct_stat
        Dim stats As struct_stat
        stats.startDate = series.StartDate
        stats.endDate = series.EndDate
        stats.len = series.Length
        stats.min = series.Minimum
        stats.max = series.Maximum
        stats.avg = series.Average
        stats.vol = series.Volume
        Return stats
    End Function

    Public Overrides Sub ProcessAnalysis()
        Dim hyoseries As Dictionary(Of Integer, TimeSeries)
        Dim year As Integer
        Dim ts As TimeSeries

        'get input parameters
        Me.timeseries = MyBase.parameters("timeseries").Value
        Me.startOfHydrologicalYear = MyBase.parameters("startOfHydrologicalYear").Value
		Me.generateBoundingBoxes = MyBase.parameters("generateBoundingBoxes").Value

        'stats for entire series
        Me.stats.Add("Entire series", calculateStats(Me.timeseries))

        'stats for hydrological years
        hyoseries = Me.timeseries.SplitHydroYears(Me.startOfHydrologicalYear)
        For Each kvp As KeyValuePair(Of Integer, TimeSeries) In hyoseries
            year = kvp.Key
            ts = kvp.Value
            Me.stats.Add(year.ToString, calculateStats(ts))
        Next
    End Sub


    Public Overrides Sub PrepareResults()

        'result table
        Me.ResultTable = New DataTable($"Annual statistics: {Me.timeseries.Title}")

        Me.ResultTable.Columns.Add("Period", GetType(String))
        Me.ResultTable.Columns.Add("Start", GetType(DateTime))
        Me.ResultTable.Columns.Add("End", GetType(DateTime))
        Me.ResultTable.Columns.Add("Length", GetType(Integer))
        Me.ResultTable.Columns.Add("Min", GetType(Double))
        Me.ResultTable.Columns.Add("Max", GetType(Double))
        Me.ResultTable.Columns.Add("Avg", GetType(Double))
        Me.ResultTable.Columns.Add("Volume", GetType(Double))

        For Each kvp As KeyValuePair(Of String, struct_stat) In Me.stats
            Dim period As String = kvp.Key
            Dim stat As struct_stat = kvp.Value
            Me.ResultTable.Rows.Add(
                period,
                stat.startDate,
                stat.endDate,
                stat.len,
                stat.min,
                stat.max,
                stat.avg,
                stat.vol
            )
        Next

        'Generate timeseries with max/avg/min values if checkbox is checked and at least two years are present
        If (Me.generateBoundingBoxes And Me.stats.Count > 2) Then
            'Prepare output timeseries
            Dim basename As String = Me.InputTimeSeries(0).Title
            Dim timeseries_max As TimeSeries = New TimeSeries(basename & " (annual maximum)")
            Dim timeseries_avg As TimeSeries = New TimeSeries(basename & " (annual average)")
            Dim timeseries_min As TimeSeries = New TimeSeries(basename & " (annual minimum)")

            'Set interpretation mode
            timeseries_max.Interpretation = TimeSeries.InterpretationEnum.BlockRight
            timeseries_avg.Interpretation = TimeSeries.InterpretationEnum.BlockRight
            timeseries_min.Interpretation = TimeSeries.InterpretationEnum.BlockRight

            'Transfer unit from input timeseries
            Dim unit As String = Me.InputTimeSeries(0).Unit
            timeseries_max.Unit = unit
            timeseries_avg.Unit = unit
            timeseries_min.Unit = unit

            'Set data source origin
            timeseries_max.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
            timeseries_avg.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
            timeseries_min.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)

            'Fill timeseries with values of max, avg, min
            For Each kvp As KeyValuePair(Of String, struct_stat) In Me.stats
                If IsNumeric(kvp.Key) Then
                    Dim stat As struct_stat = kvp.Value
                    timeseries_max.AddNode(stat.startDate, stat.max)
                    timeseries_avg.AddNode(stat.startDate, stat.avg)
                    timeseries_min.AddNode(stat.startDate, stat.min)
                End If
            Next

            'Bundle output timeseries
            MyBase.ResultSeries = New List(Of TimeSeries) From {timeseries_max, timeseries_avg, timeseries_min}
        End If

    End Sub

End Class
