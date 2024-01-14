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
''' Autocorrelation for analyzing time series periodicity
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:Autocorrelation</remarks>

Friend Class Autocorrelation
    Inherits Analysis

    Private ts_in As TimeSeries             'Eingangszeitreihe
    Private lagSize As Integer              'Größe der Lags
    Private lagCount As Integer             'Anzahl der Lags
    Private raList As List(Of Double)       'Liste für Autokorrelationskoeffizienten
    Private raMaxlist As List(Of Double)    'Liste für maximale Autokorrelationskoeffizienten
    Private periodeList As List(Of Double)  'Liste für Perioden
    Private dtList As List(Of TimeSpan)     'list of time step differences
    Private periode_avg As Double           'Durchschinttliche Periode

    Public Overloads Shared Function Description() As String
        Return "Autocorrelation analysis for analyzing time series periodicity"
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

        'Konstruktor der Basisklasse aufrufen!
        Call MyBase.New(zeitreihen)

        'Prüfung: Anzahl erwarteter Zeitreihen
        If (zeitreihen.Count <> 1) Then
            Throw New Exception("The Autocorrelation analysis requires the selection of exactly 1 time series!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Me.ts_in = Me.InputTimeSeries(0)

        'Parameter-Dialog anzeigen
        Dim dialog As New Autocorrelation_Dialog(Me.ts_in)
        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'Parameter der Lags aus Dialog abfragen
        Me.lagSize = dialog.lagSize
        Me.lagCount = dialog.lagCount

        'Announce progress start
        MyBase.AnalysisProgressStart(lagCount)

        'Instanzieren einer neuen Liste für Autokorrelationskoeffizienten
        raList = New List(Of Double)
        raMaxlist = New List(Of Double)
        periodeList = New List(Of Double)
        dtList = New List(Of TimeSpan)

        'Berechnungsschleife über mehrere Verschiebungen
        Dim values As New List(Of Double)(Me.ts_in.Values)

        For j As Integer = 0 To lagCount

            'Verschiebung berechnen
            Dim offset As Integer
            offset = j * lagSize

            Dim x_values, y_values As Double()

            y_values = values.Skip(offset).ToArray
            x_values = values.Take(y_values.Length).ToArray()

            'calculate dt
            If j > 1 Then
                dtList.Add(Me.ts_in.Dates(offset) - Me.ts_in.Dates((j - 1) * lagSize))
            End If

            'Calculate correlation coefficient and store in list
            Dim ra As Double
            ra = MathNet.Numerics.GoodnessOfFit.R(y_values, x_values)
            raList.Add(ra)

            'Announce progress
            MyBase.AnalysisProgressUpdate(j)
        Next

        If dtList.Max <> dtList.Min Then
            Log.AddLogEntry(levels.warning, "Non-equidistant timesteps encountered. Autocorrelation analysis may be unreliable!")
        End If

        'Bestimmung der Scheitelpunkte
        For i As Integer = 2 To raList.Count - 1
            If raList.Item(i - 1) > raList.Item(i - 2) And raList.Item(i - 1) > raList.Item(i) And raList.Item(i - 1) > 0 Then
                raMaxlist.Add(raList.Item(i - 1))
                periodeList.Add((i - 1) * lagSize)
            End If
        Next

        'Bestimmung der Periode aus Scheitelpunkten
        Dim y As Integer = 0
        Dim z As Integer = 0
        For i As Integer = 1 To raMaxlist.Count
            y += periodeList(i - 1)
            z += i
        Next
        periode_avg = y / z

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Korrelationskoeffizienten als Ausgabetext
        Dim i As Integer
        Dim raAuswertung As String = ""
        For i = 0 To lagCount
            raAuswertung &= $"Lag {i * lagSize}: ra = {raList.Item(i)}" & eol
        Next

        'Scheitelpunkte der Korrelationskoeffizienten als Ausgabetext
        Dim raMaxAuswertung As String = ""
        For i = 0 To raMaxlist.Count - 1
            raMaxAuswertung &= $"Lag {periodeList.Item(i)}: {raMaxlist.Item(i)}" & eol
        Next

        'Ergebnistext
        Me.ResultText =
            $"The selected time series was offset {lagCount} times by {lagSize} time steps." & eol &
            raAuswertung & eol &
            $"Assumed periodicity: {periode_avg} time steps" & eol &
            raMaxAuswertung

        'Result chart
        Me.ResultChart = New ScottPlot.Plot()
        Call Helpers.FormatChart(Me.ResultChart)

        Me.ResultChart.Title($"Autocorrelation for {ts_in.Title}")

        'X axis
        Me.ResultChart.XLabel("Offset (Lag) [number of time steps]")
        Me.ResultChart.XAxis.DateTimeFormat(False)

        'Y axis
        Me.ResultChart.YLabel("Autocorrelation coefficient (-1 < ra < 1)")
        Me.ResultChart.YAxis.Dims.SetAxis(-1, 1)

        'series
        Dim Xs, Ys As List(Of Double)
        Dim labels As List(Of String)

        'add bar plot
        Xs = New List(Of Double)
        Ys = New List(Of Double)
        labels = New List(Of String)
        For i = 0 To Me.lagCount
            Xs.Add(i * lagSize)
            Ys.Add(raList.Item(i))
            labels.Add($"Lag {i * lagSize}, ra = {Math.Round(raList.Item(i), 3)}")
        Next
        Dim bars As ScottPlot.Plottable.BarPlot
        bars = Me.ResultChart.AddBar(values:=Ys.ToArray(), positions:=Xs.ToArray())
        bars.Label = "Autocorrelogram"
        bars.BarWidth = lagSize
        bars.PositionOffset = -0.5 * lagSize 'center bars
        bars.FillColorNegative = bars.FillColor 'use same color for positive and negative values
        'TODO: TChart
        'show labels on hover
        'Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.ResultChart)
        'markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move

        'add point series
        Xs = New List(Of Double)
        Ys = New List(Of Double)
        labels = New List(Of String)
        For i = 0 To Me.periodeList.Count - 1
            Xs.Add(periodeList.Item(i))
            Ys.Add(raMaxlist.Item(i))
            labels.Add($"Lag {periodeList.Item(i)}, ra = {Math.Round(raMaxlist.Item(i), 3)}")
        Next
        Dim points As ScottPlot.Plottable.ScatterPlot
        points = Me.ResultChart.AddScatterPoints(Xs.ToArray(), Ys.ToArray(), label:="Peaks (guessed)")
        points.MarkerShape = ScottPlot.MarkerShape.filledSquare
        points.MarkerSize = 10
        points.DataPointLabels = labels.ToArray()

        'add annotation
        Dim annot As New ScottPlot.Plottable.Annotation()
        annot.Label =
            $"Time series was offset {lagCount} times by {lagSize} time steps." & eol &
            $"Assumed periodicity: {periode_avg}"
        annot.Alignment = ScottPlot.Alignment.LowerRight
        annot.BackgroundColor = Color.White
        Me.ResultChart.Add(annot)

        'Announce finish
        MyBase.AnalysisProgressFinish()
    End Sub

End Class