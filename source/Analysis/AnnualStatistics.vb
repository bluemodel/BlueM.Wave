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

    Private stats As Dictionary(Of String, struct_stat)
    Private generateBoundingBoxes As Boolean
    Private createPlottingPosition As Boolean

    Public Overloads Shared Function Description() As String
        Return "Calculates annual statistics (min, max, avg, vol) based on hydrological years."
    End Function

    Public Overrides ReadOnly Property hasResultChart() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return True
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

    Public Sub New(ByRef series As List(Of TimeSeries))
        MyBase.New(series)
        'Check: expects exactly one series
        If (series.Count <> 1) Then
            Throw New Exception("The Annual Statistics analysis requires the selection of exactly 1 time series!")
        End If
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
        Dim series As TimeSeries

        Dim dialog As New AnnualStatistics_Dialog()
        Dim dialogResult As DialogResult = dialog.ShowDialog()
        If dialogResult <> DialogResult.OK Then
            Throw New Exception("User abort!")
        End If

        Dim startMonth As Integer = CType(dialog.ComboBox_startMonth.SelectedItem, Month).number
        Me.generateBoundingBoxes = dialog.CheckBox_boundingbox.Checked
        Me.createPlottingPosition = dialog.Checkbox_plottingPosition.Checked

        'stats for entire series
        Me.stats.Add("Entire series", calculateStats(Me.InputTimeSeries(0)))

        'stats for hydrological years
        hyoseries = Me.InputTimeSeries(0).SplitHydroYears(startMonth)
        For Each kvp As KeyValuePair(Of Integer, TimeSeries) In hyoseries
            year = kvp.Key
            series = kvp.Value
            Me.stats.Add(year.ToString, calculateStats(series))
        Next
    End Sub


    Public Overrides Sub PrepareResults()
        Dim stat As struct_stat

        Const formatstring As String = "F4"

        Me.ResultText = "Annual statistics:" & eol & eol &
                         $"Time series: {Me.InputTimeSeries(0).Title}" & eol & eol
        'output results in CSV format
        Me.ResultText &= "Results:" & eol
        Me.ResultText &= String.Join(Helpers.CurrentListSeparator, "Description", "Start", "End", "Length", "Min", "Max", "Avg", "Volume") & eol
        For Each kvp As KeyValuePair(Of String, struct_stat) In Me.stats
            stat = kvp.Value
            Me.ResultText &= String.Join(Helpers.CurrentListSeparator,
                kvp.Key,
                stat.startDate.ToString(Helpers.CurrentDateFormat),
                stat.endDate.ToString(Helpers.CurrentDateFormat),
                stat.len.ToString(),
                stat.min.ToString(formatstring),
                stat.max.ToString(formatstring),
                stat.avg.ToString(formatstring),
                stat.vol.ToString(formatstring)) & eol
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
                    stat = kvp.Value
                    timeseries_max.AddNode(stat.startDate, stat.max)
                    timeseries_avg.AddNode(stat.startDate, stat.avg)
                    timeseries_min.AddNode(stat.startDate, stat.min)
                End If
            Next

            'Bundle output timeseries
            MyBase.ResultSeries = New List(Of TimeSeries) From {timeseries_max, timeseries_avg, timeseries_min}
        End If


        'Plotting position
        If (Me.createPlottingPosition) Then
            'Chart
            Me.ResultChart = New Steema.TeeChart.Chart()
            Call Helpers.FormatChart(Me.ResultChart)
            Me.ResultChart.Walls.Back.Visible = True

            'Legend
            Me.ResultChart.Legend.CheckBoxes = False
            Me.ResultChart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Top

            'x-Axis
            Me.ResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
            Me.ResultChart.Axes.Bottom.Title.Caption = "Annual recurrence"
            Me.ResultChart.Axes.Bottom.Labels.Angle = 90
            Me.ResultChart.Axes.Bottom.Logarithmic = True
            Me.ResultChart.Axes.Bottom.LogarithmicBase = 10
            Me.ResultChart.Axes.Bottom.Automatic = False
            Me.ResultChart.Axes.Bottom.Minimum = 1
            Me.ResultChart.Axes.Bottom.Maximum = 100
            Me.ResultChart.Axes.Bottom.Increment = 1
            Me.ResultChart.Axes.Bottom.Grid.DrawEvery = 1

            'y-Axis
            Me.ResultChart.Axes.Left.AutomaticMaximum = True
            Me.ResultChart.Axes.Left.AutomaticMinimum = False
            Me.ResultChart.Axes.Left.Minimum = 0
            Me.ResultChart.Axes.Left.MaximumRound = True
            Me.ResultChart.Axes.Left.Grid.DrawEvery = 1
            Me.ResultChart.Axes.Left.Title.Caption = Me.InputTimeSeries(0).Unit

            'Scatter Plott
            Dim scatterPlot As New Steema.TeeChart.Styles.Points(Me.ResultChart)
            scatterPlot.Title = $"Plotting Position ({Me.InputTimeSeries(0).Title})"

            'Get max values
            Dim annualMaxValues As New List(Of Double)()
            For Each kvp As KeyValuePair(Of String, struct_stat) In Me.stats
                If IsNumeric(kvp.Key) Then
                    stat = kvp.Value
                    annualMaxValues.Add(stat.max)
                End If
            Next

            'Sort max values
            annualMaxValues.Sort()
            annualMaxValues.Reverse()

            'Plotting position
            Dim n As Integer = annualMaxValues.Count
            Dim m As Integer = 0
            Dim Pue, T As Double
            For Each annualMax As Double In annualMaxValues
                m += 1
                Pue = m / (n + 1)
                T = 1 / Pue
                scatterPlot.Add(T, annualMax)
            Next
        Else
            Me.ResultChart = New Steema.TeeChart.Chart()  'Workaround!
            'TODO: hasResultChart() depends on CheckBox_plottingPosition
        End If

    End Sub

End Class
