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
''' Goodness Of Fit: Berechnet diverse Gütekriterien für die Anpassung
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:GoodnessOfFit</remarks>
Friend Class GoodnessOfFit
    Inherits Analysis

    Dim ts_obs, ts_sim As TimeSeries

    Private GoFResults As Dictionary(Of String, GoF)

    Public Overloads Shared Function Description() As String
        Return "Calculates multiple goodness of fit criteria for two time series. Only coincident data points where both values are not NaN are considered."
    End Function

    Private Structure GoF
        Public startDate As DateTime
        Public endDate As DateTime
        Public nValues As Long
        Public sum_squarederrors As Double
        Public nash_sutcliffe As Double
        Public ln_nash_sutcliffe As Double
        Public kge As Double
        Public volume_observed As Double
        Public volume_simulated As Double
        Public volume_error As Double
        Public ReadOnly Property abs_volume_error As Double
            Get
                Return Math.Abs(volume_error)
            End Get
        End Property
        Public coeff_determination As Double
        Public coeff_correlation As Double
        Public hydrodev As Double
    End Structure

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
            Return False
        End Get
    End Property

    Public Sub New(ByRef zeitreihen As List(Of TimeSeries))

        Call MyBase.New(zeitreihen)

        'Prüfung: Anzahl erwarteter Zeitreihen ist 2
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("The Goodness of Fit analysis requires the selection of exactly 2 time series!")
        End If

        'Instantiate result structure
        Me.GoFResults = New Dictionary(Of String, GoF)

    End Sub

    ''' <summary>
    ''' Calculate GoF parameters for two time series
    ''' </summary>
    ''' <param name="ts_o">timeseries with observed values</param>
    ''' <param name="ts_s">timeseries with simulated values</param>
    ''' <returns>GoF parameters</returns>
    ''' <remarks></remarks>
    Private Shared Function calculateGOF(ts_o As TimeSeries, ts_s As TimeSeries) As GoF

        Dim i As Integer
        Dim errors() As Double
        Dim squarederrors() As Double
        Dim sum_errors As Double
        Dim avg_obs As Double
        Dim sum_squarederrordeviation As Double
        Dim kovar As Double
        Dim std_sim As Double
        Dim std_obs As Double
        Dim avg_sim As Double
        Dim zaehler As Double

        Dim gof As New GoF()

        'check synchronousness
        If ts_o.Length <> ts_s.Length Or ts_o.StartDate <> ts_s.StartDate Or ts_o.EndDate <> ts_s.EndDate Then
            Throw New Exception("Simulated and observed time series are not synchronous!")
        End If

        'metadata
        gof.startDate = ts_o.StartDate
        gof.endDate = ts_o.EndDate
        gof.nValues = ts_o.Length

        'volumes
        gof.volume_observed = ts_o.Volume
        gof.volume_simulated = ts_s.Volume

        'averages
        avg_obs = ts_o.Average
        avg_sim = ts_s.Average

        'GoF parameters
        ReDim squarederrors(gof.nValues - 1)
        ReDim errors(gof.nValues - 1)

        sum_errors = 0
        gof.sum_squarederrors = 0

        'error & squared error
        For i = 0 To gof.nValues - 1
            errors(i) = ts_s.Values(i) - ts_o.Values(i)
            sum_errors += errors(i)
            squarederrors(i) = errors(i) ^ 2
            gof.sum_squarederrors += squarederrors(i)
        Next

        'volume error [%]
        gof.volume_error = 100 * (gof.volume_simulated - gof.volume_observed) / gof.volume_observed

        'Nash-Sutcliffe - Koeffizient
        '----------------------------
        'quadratische Abweichung der gemessenen Werte vom Mittelwert
        sum_squarederrordeviation = 0
        For i = 0 To gof.nValues - 1
            sum_squarederrordeviation += (ts_o.Values(i) - avg_obs) ^ 2
        Next

        gof.nash_sutcliffe = 1 - gof.sum_squarederrors / sum_squarederrordeviation

        'Logarithmic Nash-Sutcliffe coefficient
        '--------------------------------------
        'Formula: Reff,ln = 1 - (SUM[(ln(x_i + e)-ln(y_i + e))^2]) / (SUM[(ln(x_i + e)-avg(ln(x + e))^2])
        'Reference: Merkblatt BWK M7, Okt. 2008
        Dim epsilon, avg_ln_obs As Double
        'negligible constant to prevent Math.Log(0) = -Infinity
        'Pushpalatha et al. (2012) DOI:10.1016/j.jhydrol.2011.11.055
        epsilon = ts_o.Average / 100.0

        ' transform all values by adding epsilon and then logarithmisizing
        Dim values_ref As New List(Of Double)
        Dim values_sim As New List(Of Double)
        For i = 0 To ts_o.Length - 1
            values_ref.Add(Math.Log(ts_o.Values(i) + epsilon))
            values_sim.Add(Math.Log(ts_s.Values(i) + epsilon))
        Next

        avg_ln_obs = values_ref.Sum() / values_ref.Count

        Dim sum_ln_diff_squared As Double = 0.0
        Dim sum_ln_diff_avg_squared As Double = 0.0
        For i = 0 To values_ref.Count - 1
            sum_ln_diff_squared += (values_ref(i) - values_sim(i)) ^ 2
            sum_ln_diff_avg_squared += (values_ref(i) - avg_ln_obs) ^ 2
        Next

        gof.ln_nash_sutcliffe = 1 - sum_ln_diff_squared / sum_ln_diff_avg_squared

        'Korrelationskoeffizient
        '-----------------------
        'Formel: r = sxy / (sx * sy)
        'Standardabweichung: sx = SQRT[ 1 / (n-1) * SUMME[(x_i - x_avg)^2] ]
        'Kovarianz: kovar = sxy = 1 / (n-1) * SUMME[(x_i - x_avg) * (y_i - y_avg)]
        kovar = 0
        std_sim = 0
        std_obs = 0

        For i = 0 To gof.nValues - 1
            kovar += (ts_s.Values(i) - avg_sim) * (ts_o.Values(i) - avg_obs)
            std_sim += (ts_s.Values(i) - avg_sim) ^ 2
            std_obs += (ts_o.Values(i) - avg_obs) ^ 2
        Next

        std_sim = Math.Sqrt(1 / (gof.nValues - 1) * std_sim)
        std_obs = Math.Sqrt(1 / (gof.nValues - 1) * std_obs)
        kovar = 1 / (gof.nValues - 1) * kovar

        'Korrelationskoeffizient
        gof.coeff_correlation = kovar / (std_sim * std_obs)
        'Bestimmtheitsmaß
        gof.coeff_determination = gof.coeff_correlation ^ 2

        'Kling-Gupta Efficiency
        'https://permetrics.readthedocs.io/pages/regression/KGE.html
        '----------------------
        Dim biasratio As Double = avg_sim / avg_obs
        Dim variabilityratio As Double = (std_sim / avg_sim) / (std_obs / avg_obs)
        gof.kge = 1 - Math.Sqrt((gof.coeff_correlation - 1) ^ 2 + (biasratio - 1) ^ 2 + (variabilityratio - 1) ^ 2)

        'Hydrologische Deviation
        '-----------------------
        zaehler = 0
        For i = 0 To gof.nValues - 1
            zaehler += Math.Abs(ts_o.Values(i) - ts_s.Values(i)) * ts_o.Values(i)
        Next
        gof.hydrodev = 200 * zaehler / (gof.nValues * ts_o.Maximum ^ 2)

        Return gof

    End Function

    Public Overrides Sub ProcessAnalysis()

        Dim diagresult As DialogResult
        Dim series_o As New Dictionary(Of String, TimeSeries)
        Dim series_s As New Dictionary(Of String, TimeSeries)

        'Preprocessing
        '=============

        'Dialog anzeigen
        Dim dialog As New GoodnessOfFit_Dialog(Me.InputTimeSeries(0).Title, Me.InputTimeSeries(1).Title)
        diagresult = dialog.ShowDialog()
        If (diagresult <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'assign time series
        If (dialog.getNrGemesseneReihe = 1) Then
            Me.ts_obs = Me.InputTimeSeries(0)
            Me.ts_sim = Me.InputTimeSeries(1)
        Else
            Me.ts_obs = Me.InputTimeSeries(1)
            Me.ts_sim = Me.InputTimeSeries(0)
        End If

        'check for overlap
        If ts_obs.StartDate > ts_sim.EndDate Or ts_obs.EndDate < ts_sim.StartDate Then
            Throw New Exception("Series have no overlap!")
        End If

        'store original start and end dates
        Dim start_obs_original As DateTime = ts_obs.StartDate
        Dim end_obs_original As DateTime = ts_obs.EndDate
        Dim start_sim_original As DateTime = ts_sim.StartDate
        Dim end_sim_original As DateTime = ts_sim.EndDate

        'cut time series
        ts_obs.Cut(ts_sim)
        ts_sim.Cut(ts_obs)

        'emit warning if overlap period differs from original series extent
        If ts_obs.StartDate <> start_obs_original _
            Or ts_sim.StartDate <> start_sim_original _
            Or ts_obs.EndDate <> end_obs_original _
            Or ts_sim.EndDate <> end_sim_original Then
            Log.AddLogEntry(levels.warning, $"Reduced overlap period used for GoodnessOfFit analysis: {ts_obs.StartDate} - {ts_obs.EndDate}")
        End If

        'remove NaN values
        ts_obs = ts_obs.removeNaNValues()
        ts_sim = ts_sim.removeNaNValues()

        'store original number of non-NaN nodes
        Dim length_obs_original As Integer = ts_obs.Length
        Dim length_sim_original As Integer = ts_sim.Length

        'synchronize
        TimeSeries.Synchronize(ts_obs, ts_sim)

        'emit warning if number of nodes was reduced due to synchronizing
        If ts_obs.Length < length_obs_original Then
            Log.AddLogEntry(levels.warning, $"Series {ts_obs.Title}: only {ts_obs.Length} of {length_obs_original} nodes are coincident and can be used for GoodnessOfFit calculation")
        End If
        If ts_sim.Length < length_sim_original Then
            Log.AddLogEntry(levels.warning, $"Series {ts_sim.Title}: only {ts_sim.Length} of {length_sim_original} nodes are coincident and can be used for GoodnessOfFit calculation")
        End If

        'use entire series by default
        series_o.Add("Entire series", ts_obs)
        series_s.Add("Entire series", ts_sim)

        'calculate annual GoF parameters?
        If dialog.CheckBox_Annual.Checked Then
            Dim startMonth As Integer = CType(dialog.ComboBox_startMonth.SelectedItem, Month).number
            Dim splits As Dictionary(Of Integer, TimeSeries)
            'split observed series and store them
            splits = ts_obs.SplitHydroYears(startMonth)
            For Each kvp As KeyValuePair(Of Integer, TimeSeries) In splits
                series_o.Add(kvp.Key.ToString(), kvp.Value)
            Next
            'split simulated series and store them
            splits = ts_sim.SplitHydroYears(startMonth)
            For Each kvp As KeyValuePair(Of Integer, TimeSeries) In splits
                series_s.Add(kvp.Key.ToString(), kvp.Value)
            Next
        End If

        'Calculate GoF parameters for each series
        For Each key As String In series_o.Keys
            Me.GoFResults.Add(key, GoodnessOfFit.calculateGOF(series_o(key), series_s(key)))
        Next

    End Sub

    Public Overrides Sub PrepareResults()

        Dim shortText As String
        Dim _gof As GoF
        Const formatstring As String = "F4"

        'Text:
        '-----
        'shortText is displayed in the diagram. Displays the GoF indicator values for the entire series
        _gof = Me.GoFResults("Entire series")
        shortText = "Entire series (" & _gof.startDate.ToString(Helpers.CurrentDateFormat) & " - " & _gof.endDate.ToString(Helpers.CurrentDateFormat) & "):" & eol
        shortText &= "Volume observed: Vobs = " & _gof.volume_observed.ToString(formatstring) & eol _
                     & "Volume simulated: Vsim = " & _gof.volume_simulated.ToString(formatstring) & eol _
                     & "Volume error: m = " & _gof.volume_error.ToString(formatstring) & " %" & eol _
                     & "Sum of squared errors: F² = " & _gof.sum_squarederrors.ToString(formatstring) & eol _
                     & "Nash-Sutcliffe efficiency: E = " & _gof.nash_sutcliffe.ToString(formatstring) & eol _
                     & "Logarithmic Nash-Sutcliffe efficiency: E,ln = " & _gof.ln_nash_sutcliffe.ToString(formatstring) & eol _
                     & "Kling-Gupta efficiency: KGE = " & _gof.kge.ToString(formatstring) & eol _
                     & "Coefficient of correlation: r = " & _gof.coeff_correlation.ToString(formatstring) & eol _
                     & "Coefficient of determination: r² = " & _gof.coeff_determination.ToString(formatstring) & eol _
                     & "Hydrologic deviation: DEV = " & _gof.hydrodev.ToString(formatstring)

        'mResultText is written to the log. Contains all results.
        Me.ResultText = "GoodnessOfFit analysis results:" & eol & eol &
                         $"Observed time series: {Me.ts_obs.Title}" & eol &
                         $"Simulated time series: {Me.ts_sim.Title}" & eol & eol
        'output results in CSV format
        Dim headerItems1 = New List(Of String) From {
            "Description", "Start", "End", "Length", "Volume observed", "Volume simulated", "Volume error [%]", "Sum of squared errors", "Nash-Sutcliffe efficiency", "Logarithmic Nash-Sutcliffe efficiency", "Kling-Gupta efficiency", "Coefficient of correlation", "Coefficient of determination", "Hydrologic deviation"
        }
        Dim headerItems2 = New List(Of String) From {
            "desc", "t0", "t1", "n", "Vobs", "Vsim", "m", "F²", "E", "E,ln", "KGE", "r", "r²", "DEV"
        }
        Me.ResultText &= String.Join(Helpers.CurrentListSeparator, headerItems1.Select(Function(s) $"""{s}""")) & eol
        Me.ResultText &= String.Join(Helpers.CurrentListSeparator, headerItems2.Select(Function(s) $"""{s}""")) & eol
        For Each GOFResult As KeyValuePair(Of String, GoF) In Me.GoFResults
            With GOFResult.Value
                Me.ResultText &= String.Join(Helpers.CurrentListSeparator,
                    GOFResult.Key,
                    .startDate.ToString(Helpers.CurrentDateFormat),
                    .endDate.ToString(Helpers.CurrentDateFormat),
                    .nValues.ToString(),
                    .volume_observed.ToString(formatstring),
                    .volume_simulated.ToString(formatstring),
                    .volume_error.ToString(formatstring) & "%",
                    .sum_squarederrors.ToString(formatstring),
                    .nash_sutcliffe.ToString(formatstring),
                    .ln_nash_sutcliffe.ToString(formatstring),
                    .kge.ToString(formatstring),
                    .coeff_correlation.ToString(formatstring),
                    .coeff_determination.ToString(formatstring),
                    .hydrodev.ToString(formatstring)) & eol
            End With
        Next

        'result chart (radar plot):
        '--------------------------
        'TODO: E and E,ln can be < -1, what would the chart look like then?
        'TODO: m is currently plotted using its absolute value, but labelled with the actual value, while the axis title says "absolute", confusing?
        'TODO: some parameters (m, DEV, F²) are better when smaller, should their axis direction be switched?
        'TODO: F² becomes larger with longer time series, "Entire series" will always have a much larger value than the individual years, maybe omit this parameter from the plot?
        'TODO: the axis titles and grid disappear when the last series is unchecked by the user, why?

        Me.ResultChart = New Steema.TeeChart.Chart()
        Call Helpers.FormatChart(Me.ResultChart)
        Me.ResultChart.Header.Text = $"Goodness of Fit: {Me.ts_obs.Title} vs. {Me.ts_sim.Title}"

        'determine max absolute volume error for scaling
        Dim max_abs_volume_error As Double = 0
        For Each GOFResult As KeyValuePair(Of String, GoF) In Me.GoFResults
            Dim gof As GoF = GOFResult.Value
            max_abs_volume_error = Math.Max(max_abs_volume_error, gof.abs_volume_error)
        Next

        For Each GOFResult As KeyValuePair(Of String, GoF) In Me.GoFResults
            Dim title = GOFResult.Key
            Dim gof As GoF = GOFResult.Value

            Dim series As New Steema.TeeChart.Styles.Radar(Me.ResultChart.Chart)
            series.Title = title

            'this is important because otherwise the series nodes are automatically ordered by their x values (angles), potentially messing up the labelling
            series.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.None


            'in the chart, X (angle) represents the actual value, Y is the scaled (plotted) value, text is the axis label

            'scale the absolute volume error to range (-1, 1) and reverse it's sign
            Dim scaled_volume_error As Double = -1 * (gof.abs_volume_error / max_abs_volume_error * 2 + -1)
            series.Add(gof.volume_error, scaled_volume_error, "Volume error [%]")
            series.Add(gof.nash_sutcliffe, gof.nash_sutcliffe, "Nash-Sutcliffe efficiency")
            series.Add(gof.ln_nash_sutcliffe, gof.ln_nash_sutcliffe, "Logarithmic Nash-Sutcliffe efficiency")
            series.Add(gof.kge, gof.kge, "Kling-Gupta efficiency")
            series.Add(gof.coeff_correlation, gof.coeff_correlation, "Coefficient of correlation")

            series.Circled = True
            series.Pen.Width = 2
            series.Pen.Color = series.Pointer.Color
            'series.Color
            series.Brush.Visible = False
            series.Pointer.Visible = True
            series.CircleLabels = True
            'series.ClockWiseLabels = True
            series.Marks.Visible = True
            series.Marks.Style = Steema.TeeChart.Styles.MarksStyles.XValue 'label the real value
            series.Marks.FontSeriesColor = True
        Next

        'scale radar spokes between -1 and 1
        Me.ResultChart.Axes.Left.Automatic = False
        Me.ResultChart.Axes.Left.Minimum = -1
        Me.ResultChart.Axes.Left.Maximum = 1

        'grid is on chart's left axis
        Me.ResultChart.Axes.Left.Increment = 0.2
        'labels are on chart's right axis, hide them
        Me.ResultChart.Axes.Right.Labels.Visible = False

        'Text in Diagramm einfügen
        Dim annot As New Steema.TeeChart.Tools.Annotation(Me.ResultChart)
        annot.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom
        annot.Text = shortText

    End Sub

End Class
