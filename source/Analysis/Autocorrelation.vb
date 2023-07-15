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

    Public Overrides ReadOnly Property Description() As String
        Get
            Return "Autocorrelation analysis for analyzing time series periodicity"
        End Get
    End Property

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
    Public Sub New()
        MyBase.New()
        'define input parameters
        MyBase.parameters.Add("timeseries",
            New AnalysisParameter("Input time series", AnalysisParameter.ParameterTypeEnum.Timeseries, AnalysisParameter.ParameterAmountEnum.Single)
        )
        MyBase.parameters.Add("lagSize",
            New AnalysisParameter("Number of time steps", AnalysisParameter.ParameterTypeEnum.Integer, AnalysisParameter.ParameterAmountEnum.Single, def:=5, min:=1, max:=10)
        )
        MyBase.parameters.Add("numberOfOffsets",
            New AnalysisParameter("Number of offsets", AnalysisParameter.ParameterTypeEnum.Integer, AnalysisParameter.ParameterAmountEnum.Single, def:=300, min:=1, max:=2000)
        )
    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        'get input parameters
        Me.ts_in = MyBase.parameters("timeseries").Value
        Me.lagSize = MyBase.parameters("lagSize").Value
        Me.lagCount = MyBase.parameters("numberOfOffsets").Value

        'Check max possible lag
        Dim maxlag As Integer = Me.ts_in.Length / Me.lagSize - 1
        If Me.lagSize * Me.lagCount > Me.ts_in.Length Then
            Throw New Exception(
                $"The selected time series is too short or the largest lag is too long! " &
                $"Please select at most {maxlag} offsets with the currently set number of time steps!")
        End If

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

        'Ergebnisdiagramm
        Me.ResultChart = New Steema.TeeChart.Chart()
        Call Helpers.FormatChart(Me.ResultChart)
        Me.ResultChart.Header.Text = "Autocorrelation for " & ts_in.Title

        'X-Achse
        Me.ResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.ResultChart.Axes.Bottom.Title.Caption = "Offset (Lag) [number of time steps]"

        'Y-Achse
        Me.ResultChart.Axes.Left.Title.Caption = "Autocorrelation coefficient (-1 < ra < 1)"
        Me.ResultChart.Axes.Left.Automatic = False
        Me.ResultChart.Axes.Left.Minimum = -1
        Me.ResultChart.Axes.Left.Maximum = 1

        'Linie instanzieren und benennen
        Dim line_ra As New Steema.TeeChart.Styles.Bar(Me.ResultChart)
        line_ra.Title = "Autocorrelogram"
        Dim line_raMax As New Steema.TeeChart.Styles.Points(Me.ResultChart)
        line_raMax.Title = "Peaks (guessed)"

        'Linie befüllen
        For i = 0 To Me.lagCount
            Dim mark As String = $"Lag {i * lagSize}, ra = {raList.Item(i)}"
            line_ra.Add(i * lagSize, raList.Item(i), mark)
        Next

        For i = 0 To Me.periodeList.Count - 1
            Dim mark As String = $"Lag {periodeList.Item(i)}, ra = {raMaxlist.Item(i)}"
            line_raMax.Add(periodeList.Item(i), raMaxlist.Item(i), mark)
        Next

        'Marks nicht anzeigen
        line_ra.Marks.Visible = False

        'Markstips bei Mausaktion anzeigen
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.ResultChart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move

        'Textfeld in Diagramm einfügen und Position bestimmen
        Dim annot As New Steema.TeeChart.Tools.Annotation(Me.ResultChart)
        annot.Text =
            $"Time series was offset {lagCount} times by {lagSize} time steps." & eol &
            $"Assumed periodicity: {periode_avg}"
        annot.Position = Steema.TeeChart.Tools.AnnotationPositions.RightTop

        'Announce finish
        MyBase.AnalysisProgressFinish()
    End Sub

End Class