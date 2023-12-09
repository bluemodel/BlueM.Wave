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

Imports System.Windows.Forms.AxHost
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window
Imports BlueM.Wave.AnnualStatistics
''' <summary>
''' Write description!
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:TestAnalysis</remarks>
Friend Class AnnualRecurrenceProbability
    Inherits Analysis

    Private maxima As Dictionary(Of String, Double)
    Private plottingPosition As New Dictionary(Of Double, Double)()

    ''' <summary>
    ''' Returns the description of the analysis function
    ''' </summary>
    Public Overloads Shared Function Description() As String
        Return "Write description!"
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
            Return False
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
            Throw New Exception("The TestAnalysis requires the selection of exactly one time series!")
        End If

    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim hyoseries As Dictionary(Of Integer, TimeSeries)
        Dim year As Integer
        Dim series As TimeSeries

        Dim dialog As New AnnualRecurrenceProbability_Dialog()
        Dim dialogResult As DialogResult = dialog.ShowDialog()
        If dialogResult <> DialogResult.OK Then
            Throw New Exception("User abort!")
        End If

        Dim startMonth As Integer = CType(dialog.ComboBox_startMonth.SelectedItem, Month).number

        'maxima for hydrological years
        maxima = New Dictionary(Of String, Double)
        hyoseries = InputTimeSeries(0).SplitHydroYears(startMonth)
        For Each kvp As KeyValuePair(Of Integer, TimeSeries) In hyoseries
            year = kvp.Key
            series = kvp.Value
            maxima.Add(year.ToString, series.Maximum)
        Next

        'Sort annual max values descending
        Dim maxValues As List(Of Double) = maxima.Values.ToList
        maxValues.Sort()
        maxValues.Reverse()

        'Plotting position
        Dim n As Integer = maxValues.Count
        Dim m As Integer = 0
        Dim Pue, T As Double
        For Each annualMax As Double In maxValues
            m += 1
            Pue = m / (n + 1)
            T = 1 / Pue
            plottingPosition.Add(T, annualMax)
        Next

    End Sub

    ''' <summary>
    ''' Prepare the results of the analysis
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Create result table
        ResultTable = New DataTable($"Annual maxima: {Me.InputTimeSeries(0).Title}")
        ResultTable.Columns.Add("Period", GetType(String))
        ResultTable.Columns.Add("Max", GetType(Double))
        ResultTable.Columns.Add("P", GetType(Double))

        'Add rows to result table
        For Each kvp As KeyValuePair(Of String, Double) In maxima
            Dim period As String = kvp.Key
            Dim maximum As Double = kvp.Value
            ResultTable.Rows.Add(
                period,
                maximum
            )
        Next

        'Chart
        ResultChart = New Steema.TeeChart.Chart()
        Call Helpers.FormatChart(ResultChart)
        ResultChart.Walls.Back.Visible = True

        'Legend
        ResultChart.Legend.CheckBoxes = False
        ResultChart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Top

        'x-Axis
        ResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        ResultChart.Axes.Bottom.Title.Caption = "Annual recurrence"
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
        ResultChart.Axes.Left.Title.Caption = Me.InputTimeSeries(0).Unit

        'Scatter Plott
        Dim scatterPlot As New Steema.TeeChart.Styles.Points(ResultChart)
        scatterPlot.Title = $"Plotting Position ({InputTimeSeries(0).Title})"
        For Each kvp As KeyValuePair(Of Double, Double) In plottingPosition
            scatterPlot.Add(kvp.Key, kvp.Value)
        Next

    End Sub

End Class