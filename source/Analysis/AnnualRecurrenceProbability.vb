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
''' Calculates the annual recurrence probility and return period of annual maxima using plotting position method
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:AnnualRecurrenceProbabilty</remarks>
Friend Class AnnualRecurrenceProbability
    Inherits Analysis

    ''' <summary>
    ''' Class representing an annual event and its data
    ''' </summary>
    Private Class AnnualEvent
        Public year As Integer
        Public maxDate As DateTime
        Public maxValue As Double
        Public rank As Integer
        Public pExceedance As Double
        Public returnPeriod As Double
    End Class

    ''' <summary>
    ''' List of annual events
    ''' </summary>
    Private events As List(Of AnnualEvent)

    ''' <summary>
    ''' Returns the description of the analysis function
    ''' </summary>
    Public Overloads Shared Function Description() As String
        Return "Calculates the annual recurrence probility and return period of annual maxima using plotting position method"
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function produces a result text
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function produces result values
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function produces a result chart
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
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result table
    ''' that should be shown in a separate window
    ''' </summary>
    Public Overrides ReadOnly Property hasResultTable() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Class constructor
    ''' </summary>
    ''' <param name="timeseries">list of time series to be analyzed</param>
    Public Sub New(ByRef timeseries As List(Of TimeSeries))

        'Call constructor of base class
        Call MyBase.New(timeseries)

        'Check expected count of time series
        If (timeseries.Count <> 1) Then
            Throw New Exception("The AnnualRecurrenceProbabilty requires the selection of exactly one time series!")
        End If

    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim ts As TimeSeries = Me.InputTimeSeries(0)

        Dim dialog As New AnnualRecurrenceProbability_Dialog()
        Dim dialogResult As DialogResult = dialog.ShowDialog()
        If dialogResult <> DialogResult.OK Then
            Throw New Exception("User abort!")
        End If

        Dim startMonth As Integer = CType(dialog.ComboBox_startMonth.SelectedItem, Month).number

        'get annual max values as events
        Me.events = New List(Of AnnualEvent)

        'determine first hydrological year
        Dim year As Integer
        If ts.StartDate.Month >= startMonth Then
            year = ts.StartDate.Year
        Else
            year = ts.StartDate.Year - 1
        End If

        'loop over years
        Do
            'start of hydrological year
            Dim startDate As New DateTime(year, startMonth, 1)
            If startDate < ts.StartDate Then
                Log.AddLogEntry(levels.warning, $"Hydrological year {year} begins before start of time series!")
            End If
            If startDate > ts.EndDate Then
                'end of time series
                Exit Do
            End If
            'end of hydrological year
            Dim endDate As DateTime = New DateTime(year + 1, startMonth, 1) - New TimeSpan(0, 0, 1)
            If endDate > ts.EndDate Then
                Log.AddLogEntry(levels.warning, $"Hydrological year {year} ends after end of time series!")
            End If
            'get node with max value in year
            Dim maxNode As KeyValuePair(Of DateTime, Double) = ts.MaximumNode(startDate, endDate)
            'store as new event
            If Double.IsNaN(maxNode.Value) Then
                Log.AddLogEntry(levels.warning, $"Hydrological year {year} contains no usable data!")
            Else
                Dim ev As New AnnualEvent() With {
                    .year = year,
                    .maxDate = maxNode.Key,
                    .maxValue = maxNode.Value
                }
                Me.events.Add(ev)
            End If
            'next year
            year += 1
        Loop

        'Sort events by max value descending
        Me.events.Sort(Function(ev1 As AnnualEvent, ev2 As AnnualEvent)
                           Return ev1.maxValue.CompareTo(ev2.maxValue)
                       End Function)
        Me.events.Reverse()

        'Plotting position
        Dim n As Integer = Me.events.Count
        Dim m As Integer = 0
        Dim Pue, T As Double

        For i As Integer = 0 To n - 1

            m = i + 1

            'if two or more events have the same max value, they get the same rank
            If i > 0 Then
                If Me.events(i).maxValue = Me.events(i - 1).maxValue Then
                    m = Me.events(i - 1).rank
                End If
            End If

            Pue = m / (n + 1)
            T = 1 / Pue

            Me.events(i).rank = m
            Me.events(i).pExceedance = Pue
            Me.events(i).returnPeriod = T

        Next

    End Sub

    ''' <summary>
    ''' Prepare the results of the analysis
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Create result table
        ResultTable = New DataTable($"Annual maxima: {Me.InputTimeSeries(0).Title}")
        ResultTable.Columns.Add("Year", GetType(Integer))
        ResultTable.Columns.Add("Date", GetType(DateTime))
        ResultTable.Columns.Add($"Maximum [{Me.InputTimeSeries(0).Unit}]", GetType(Double))
        ResultTable.Columns.Add($"Rank", GetType(Integer))
        ResultTable.Columns.Add("Probability of exceedance [-]", GetType(Double))
        ResultTable.Columns.Add("Return period [years]", GetType(Double))

        'Add rows to result table
        For Each ev As AnnualEvent In events
            ResultTable.Rows.Add(
                ev.year,
                ev.maxDate,
                ev.maxValue,
                ev.rank,
                ev.pExceedance,
                ev.returnPeriod
            )
        Next

        'result chart
        ResultChart = New ScottPlot.Plot()
        Call Helpers.FormatChart(ResultChart)

        'legend
        ResultChart.Legend.Location = ScottPlot.Alignment.LowerRight

        'X axis
        ResultChart.XLabel("Return period [years]")
        ResultChart.XAxis.DateTimeFormat(False)
        ResultChart.XAxis.TickLabelStyle(rotation:=90)
        'logarithmic labels and ticks
        ResultChart.XAxis.TickLabelFormat(Function(x As Double) Math.Pow(10, x).ToString("N0"))
        ResultChart.XAxis.MinorLogScale(True)
        ResultChart.XAxis.MajorGrid(True, Color.FromArgb(80, Color.Black))
        ResultChart.XAxis.MinorGrid(True, Color.FromArgb(20, Color.Black))

        'Y axis
        ResultChart.YLabel(Me.InputTimeSeries(0).Unit)

        'point series
        Dim Xs As New List(Of Double)
        Dim Ys As New List(Of Double)
        Dim labels As New List(Of String)
        For Each ev As AnnualEvent In Me.events
            Xs.Add(Math.Log10(ev.returnPeriod)) 'plot the log value
            Ys.Add(ev.maxValue)
            labels.Add(ev.year.ToString())
        Next
        Dim points As ScottPlot.Plottable.ScatterPlot
        points = ResultChart.AddScatterPoints(Xs.ToArray(), Ys.ToArray())
        points.Label = $"Plotting Position ({InputTimeSeries(0).Title})"
        points.MarkerSize = 10
        points.MarkerShape = ScottPlot.MarkerShape.filledSquare
        'add data point labels
        points.DataPointLabels = labels.ToArray()

        'set Y axis limits
        ResultChart.YAxis.Dims.SetAxis(0, Ys.Max() * 1.1)

        'result series (annual maxima)
        Dim ts As New TimeSeries(Me.InputTimeSeries(0).Title + " (annual maxima)")
        ts.Unit = Me.InputTimeSeries(0).Unit
        ts.Interpretation = TimeSeries.InterpretationEnum.Instantaneous
        ts.DisplayOptions.ShowPoints = True
        For Each ev As AnnualEvent In Me.events
            ts.AddNode(ev.maxDate, ev.maxValue)
        Next
        Me.ResultSeries = New List(Of TimeSeries) From {ts}

    End Sub

End Class