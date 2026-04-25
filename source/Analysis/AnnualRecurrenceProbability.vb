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
    ''' Dictionary of annual events, key is timeseries title, value is list of annual events for this time series
    ''' </summary>
    Private events As Dictionary(Of String, List(Of AnnualEvent))

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

    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim tsList As List(Of TimeSeries) = Me.InputTimeSeries

        Dim dlg As New AnnualRecurrenceProbability_Dialog()
        If dlg.ShowDialog() <> DialogResult.OK Then
            Throw New AnalysisCancelledException("Analysis cancelled")
        End If

        Dim startMonth As Integer = CType(dlg.ComboBox_startMonth.SelectedItem, Month).number

        Me.events = New Dictionary(Of String, List(Of AnnualEvent))()

        For Each ts As TimeSeries In tsList

            'get annual max values as events
            Dim tsEvents As New List(Of AnnualEvent)()

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
                    tsEvents.Add(ev)
                End If
                'next year
                year += 1
            Loop

            'Sort events by max value descending
            tsEvents.Sort(Function(ev1 As AnnualEvent, ev2 As AnnualEvent)
                              Return ev1.maxValue.CompareTo(ev2.maxValue)
                          End Function)
            tsEvents.Reverse()

            'Plotting position
            Dim n As Integer = tsEvents.Count
            Dim m As Integer = 0
            Dim Pue, T As Double

            For i As Integer = 0 To n - 1

                m = i + 1

                'if two or more events have the same max value, they get the same rank
                If i > 0 Then
                    If tsEvents(i).maxValue = tsEvents(i - 1).maxValue Then
                        m = tsEvents(i - 1).rank
                    End If
                End If

                Pue = m / (n + 1)
                T = 1 / Pue

                tsEvents(i).rank = m
                tsEvents(i).pExceedance = Pue
                tsEvents(i).returnPeriod = T

            Next

            'store events for this time series in dictionary
            Me.events(ts.Title) = tsEvents

        Next

    End Sub

    ''' <summary>
    ''' Prepare the results of the analysis
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Create result table
        ResultTable = New DataTable($"Annual maxima: {String.Join(", ", Me.InputTimeSeries.Select(Function(ts1) ts1.Title))}")
        ResultTable.Columns.Add("Series", GetType(String))
        ResultTable.Columns.Add("Year", GetType(Integer))
        ResultTable.Columns.Add("Date", GetType(DateTime))
        ResultTable.Columns.Add($"Maximum [{String.Join(", ", Me.InputTimeSeries.Select(Function(ts1) ts1.Unit).Distinct())}]", GetType(Double))
        ResultTable.Columns.Add($"Rank", GetType(Integer))
        ResultTable.Columns.Add("Probability of exceedance [-]", GetType(Double))
        ResultTable.Columns.Add("Return period [years]", GetType(Double))

        'Add rows to result table
        For Each ts As TimeSeries In Me.InputTimeSeries
            Dim tsEvents As List(Of AnnualEvent) = Me.events(ts.Title)
            For Each ev As AnnualEvent In tsEvents
                ResultTable.Rows.Add(
                    ts.Title,
                    ev.year,
                    ev.maxDate,
                    ev.maxValue,
                    ev.rank,
                    ev.pExceedance,
                    ev.returnPeriod
                )
            Next
        Next

        'Chart
        ResultChart = New Steema.TeeChart.Chart()
        Call Helpers.ChartSetDefaultFormat(ResultChart)
        ResultChart.Walls.Back.Visible = True
        ResultChart.Header.Visible = True
        ResultChart.Header.Text = $"Annual maxima (plotting position)"

        'Legend
        ResultChart.Legend.ActiveStyle = Steema.TeeChart.LegendActiveStyles.None
        ResultChart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Top

        'x-Axis
        ResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        ResultChart.Axes.Bottom.Title.Caption = "Return period [years]"
        ResultChart.Axes.Bottom.Labels.Angle = 90
        ResultChart.Axes.Bottom.Logarithmic = True
        ResultChart.Axes.Bottom.LogarithmicBase = 10
        ResultChart.Axes.Bottom.Automatic = False
        ResultChart.Axes.Bottom.Minimum = 1
        ResultChart.Axes.Bottom.Maximum = 100
        ResultChart.Axes.Bottom.Increment = 1
        ResultChart.Axes.Bottom.Grid.DrawEvery = 1

        'y-Axis
        ResultChart.Axes.Left.AutomaticMaximum = True
        ResultChart.Axes.Left.AutomaticMinimum = False
        ResultChart.Axes.Left.Minimum = 0
        ResultChart.Axes.Left.MaximumRound = True
        ResultChart.Axes.Left.Grid.DrawEvery = 1
        ResultChart.Axes.Left.Title.Caption = String.Join(", ", Me.InputTimeSeries.Select(Function(ts1) ts1.Unit).Distinct())

        'point series
        For Each ts As TimeSeries In Me.InputTimeSeries
            Dim tsEvents As List(Of AnnualEvent) = Me.events(ts.Title)
            Dim points As New Steema.TeeChart.Styles.Points(ResultChart)
            points.Title = ts.Title
            For Each ev As AnnualEvent In tsEvents
                points.Add(ev.returnPeriod, ev.maxValue, ev.year.ToString())
            Next
            'prepare year label as mark, but hide it by default
            points.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Label
            points.Marks.Visible = False
        Next

        'result series (annual maxima)
        Me.ResultSeries = New List(Of TimeSeries)()
        For Each ts As TimeSeries In Me.InputTimeSeries
            Dim tsEvents As List(Of AnnualEvent) = Me.events(ts.Title)
            Dim ts_events As New TimeSeries($"{ts.Title} (annual maxima)")
            ts_events.Unit = ts.Unit
            ts_events.Interpretation = TimeSeries.InterpretationEnum.Instantaneous
            ts_events.DisplayOptions.ShowPoints = True
            For Each ev As AnnualEvent In tsEvents
                ts_events.AddNode(ev.maxDate, ev.maxValue)
            Next
            Me.ResultSeries.Add(ts_events)
        Next

    End Sub

End Class