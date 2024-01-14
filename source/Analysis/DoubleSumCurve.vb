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
''' Plots the cumulative coincident values of two time series against each other and adds a 45° line to the resulting plot
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:Doppelsummenanalyse</remarks>
Friend Class DoubleSumCurve
    Inherits Analysis

    Private ts_1, ts_2 As TimeSeries
    Private summe1(), summe2() As Double

    Public Overloads Shared Function Description() As String
        Return "Plots the cumulative coincident values of two time series against each other and adds a 45° line to the resulting plot."
    End Function

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion einen Ergebnistext erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion Ergebniswerte erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion ein Ergebnisdiagramm erzeugt
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
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">zu analysierende Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As List(Of TimeSeries))

        Call MyBase.New(zeitreihen)

        'Prüfung: genau 2 Zeitreihen erlaubt
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("The Double Sum Curve analysis requires the selection of exactly 2 time series!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i, n As Integer

        'assign timeseries and remove NaN values
        Me.ts_1 = Me.InputTimeSeries(0).removeNaNValues()
        Me.ts_2 = Me.InputTimeSeries(1).removeNaNValues()

        'synchronize
        TimeSeries.Synchronize(Me.ts_1, Me.ts_2)

        n = Me.ts_1.Length

        ReDim Me.summe1(n - 1)
        ReDim Me.summe2(n - 1)

        'Erster Wert
        Me.summe1(0) = Me.ts_1.FirstValue
        Me.summe2(0) = Me.ts_2.FirstValue
        'Weitere Werte kumulativ aufsummieren
        For i = 1 To n - 1
            Me.summe1(i) = Me.summe1(i - 1) + Me.ts_1.Values(i)
            Me.summe2(i) = Me.summe2(i - 1) + Me.ts_2.Values(i)
        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Text:
        '-----
        Me.ResultText = $"The analysis is based on {Me.summe1.Length} coincident data points between {Me.ts_1.StartDate.ToString(Helpers.CurrentDateFormat)} and {Me.ts_1.EndDate.ToString(Helpers.CurrentDateFormat)}"

        'Result chart:
        Dim doppelsumme, gerade As ScottPlot.Plottable.ScatterPlot

        Me.ResultChart = New ScottPlot.Plot()
        Call Helpers.FormatChart(Me.ResultChart)
        Me.ResultChart.XAxis.DateTimeFormat(False)
        Me.ResultChart.Title($"Double Sum Curve ({Me.ts_1.Title} / {Me.ts_2.Title})")
        Me.ResultChart.Legend.IsVisible = False

        'axes
        Me.ResultChart.XLabel($"Sum {Me.ts_1.Title}")
        Me.ResultChart.YLabel($"Sum {Me.ts_2.Title}")

        'series
        doppelsumme = Me.ResultChart.AddScatter(summe1, summe2)
        doppelsumme.Label = $"Double Sum Curve {Me.ts_1.Title} - {Me.ts_2.Title}"
        doppelsumme.MarkerShape = ScottPlot.MarkerShape.filledCircle
        doppelsumme.MarkerSize = 4

        Dim max As Double = Math.Max(summe1.Max, summe2.Max)
        gerade = Me.ResultChart.AddScatterLines({0, max}, {0, max})
        gerade.Label = "45° line"
        gerade.LineColor = Color.DarkGray
        gerade.LineStyle = ScottPlot.LineStyle.Dash

    End Sub

End Class
