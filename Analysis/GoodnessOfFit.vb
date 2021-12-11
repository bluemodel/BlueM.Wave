'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
'
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
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
        Public volumeerror As Double
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
    Private Shared Function calculateGOF(ByVal ts_o As TimeSeries, ByVal ts_s As TimeSeries) As GoF

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

        'synchronize
        TimeSeries.Synchronize(ts_o, ts_s)

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
        gof.volumeerror = 100 * (gof.volume_simulated - gof.volume_observed) / gof.volume_observed

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
        'Formula: Reff,ln = 1 - (SUM[(ln(x_i)-ln(y_i))^2]) / (SUM[(ln(x_i)-ln(x_avg))^2])
        'Reference: Merkblatt BWK M7, Okt. 2008
        Dim ln_avg_gemessen As Double
        Dim sum_ln_diff_squared As Double = 0.0
        Dim sum_ln_diff_avg_squared As Double = 0.0

        ln_avg_gemessen = Math.Log(avg_obs)

        For i = 0 To gof.nValues - 1
            sum_ln_diff_squared += (Math.Log(ts_o.Values(i)) - Math.Log(ts_s.Values(i))) ^ 2
            sum_ln_diff_avg_squared += (Math.Log(ts_o.Values(i)) - ln_avg_gemessen) ^ 2
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
        Dim dialog As New GoodnessOfFit_Dialog(Me.mZeitreihen(0).Title, Me.mZeitreihen(1).Title)
        diagresult = dialog.ShowDialog()
        If (diagresult <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'assign time series
        If (dialog.getNrGemesseneReihe = 1) Then
            Me.ts_obs = Me.mZeitreihen(0)
            Me.ts_sim = Me.mZeitreihen(1)
        Else
            Me.ts_obs = Me.mZeitreihen(1)
            Me.ts_sim = Me.mZeitreihen(0)
        End If

        'remove NaN values
        Me.ts_obs = Me.ts_obs.removeNaNValues()
        Me.ts_sim = Me.ts_sim.removeNaNValues()

        'synchronize
        TimeSeries.Synchronize(Me.ts_obs, Me.ts_sim)

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

        Dim i As Integer
        Dim shortText As String
        Dim _gof As GoF
        Const formatstring As String = "F4"

        'Text:
        '-----
        'shortText is displayed in the diagram. Displays the GoF parameters for the entire series
        _gof = Me.GoFResults("Entire series")
        shortText = "Entire series (" & _gof.startDate.ToString(Helpers.DefaultDateFormat) & " - " & _gof.endDate.ToString(Helpers.DefaultDateFormat) & "):" & eol
        shortText &= "Volume observed: Vobs = " & _gof.volume_observed.ToString(formatstring) & eol _
                     & "Volume simulated: Vsim = " & _gof.volume_simulated.ToString(formatstring) & eol _
                     & "Volume error: m = " & _gof.volumeerror.ToString(formatstring) & " %" & eol _
                     & "Sum of squared errors: F² = " & _gof.sum_squarederrors.ToString(formatstring) & eol _
                     & "Nash-Sutcliffe efficiency: E = " & _gof.nash_sutcliffe.ToString(formatstring) & eol _
                     & "Logarithmic Nash-Sutcliffe efficiency: E,ln = " & _gof.ln_nash_sutcliffe.ToString(formatstring) & eol _
                     & "Kling-Gupta efficiency: KGE = " & _gof.kge.ToString(formatstring) & eol _
                     & "Coefficient of correlation: r = " & _gof.coeff_correlation.ToString(formatstring) & eol _
                     & "Coefficient of determination: r² = " & _gof.coeff_determination.ToString(formatstring) & eol _
                     & "Hydrologic deviation: DEV = " & _gof.hydrodev.ToString(formatstring)

        'mResultText is written to the log. Contains all results.
        Me.mResultText = "Goodness of Fit analysis:" & eol & eol &
                         $"Observed time series: {Me.ts_obs.Title}" & eol &
                         $"Simulated time series: {Me.ts_sim.Title}" & eol & eol
        'output results in CSV format
        Me.mResultText &= "Results:" & eol
        Me.mResultText &= String.Join(Helpers.CurrentListSeparator, "Description", "Start", "End", "Length", "Volume observed", "Volume simulated", "Volume error [%]", "Sum of squared errors", "Nash-Sutcliffe efficiency", "Logarithmic Nash-Sutcliffe efficiency", "Coefficient of correlation", "Coefficient of determination", "Hydrologic deviation") & eol
        Me.mResultText &= String.Join(Helpers.CurrentListSeparator, "desc", "t0", "t1", "n", "Vobs", "Vsim", "m", "F²", "E", "E,ln", "KGE", "r", "r²", "DEV") & eol
        For Each GOFResult As KeyValuePair(Of String, GoF) In Me.GoFResults
            With GOFResult.Value
                Me.mResultText &= String.Join(Helpers.CurrentListSeparator,
                    GOFResult.Key,
                    .startDate.ToString(Helpers.DefaultDateFormat),
                    .endDate.ToString(Helpers.DefaultDateFormat),
                    .nValues.ToString(),
                    .volume_observed.ToString(formatstring),
                    .volume_simulated.ToString(formatstring),
                    .volumeerror.ToString(formatstring) & "%",
                    .sum_squarederrors.ToString(formatstring),
                    .nash_sutcliffe.ToString(formatstring),
                    .ln_nash_sutcliffe.ToString(formatstring),
                    .kge.ToString(formatstring),
                    .coeff_correlation.ToString(formatstring),
                    .coeff_determination.ToString(formatstring),
                    .hydrodev.ToString(formatstring)) & eol
            End With
        Next

        'Diagramm:
        '---------
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Goodness of Fit"

        'Text in Diagramm einfügen
        Dim annot As New Steema.TeeChart.Tools.Annotation(Me.mResultChart)
        annot.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom
        annot.Text = shortText

        'Linien instanzieren
        Dim line_gemessen As New Steema.TeeChart.Styles.Line(Me.mResultChart)
        Dim line_simuliert As New Steema.TeeChart.Styles.Line(Me.mResultChart)
        Dim line_fehlerquadrate As New Steema.TeeChart.Styles.Line(Me.mResultChart)

        'linke Achse
        Me.mResultChart.Axes.Left.Title.Caption = "Time series value"

        'rechte Achse
        line_fehlerquadrate.CustomVertAxis = Me.mResultChart.Axes.Right
        Me.mResultChart.Axes.Right.Title.Caption = "Squared error"
        Me.mResultChart.Axes.Right.Grid.Visible = False
        Me.mResultChart.Axes.Right.Inverted = True

        'Namen vergeben
        line_gemessen.Title = Me.ts_obs.Title
        line_simuliert.Title = Me.ts_sim.Title
        line_fehlerquadrate.Title = "Squared error"

        'Reihen formatieren
        line_fehlerquadrate.Color = Color.LightGray

        'X-Werte als Zeitdaten einstellen
        line_gemessen.XValues.DateTime = True
        line_simuliert.XValues.DateTime = True
        line_fehlerquadrate.XValues.DateTime = True

        'Werte zu Serien hinzufügen
        For i = 0 To Me.ts_obs.Length - 1
            line_gemessen.Add(Me.ts_obs.Dates(i), Me.ts_obs.Values(i))
        Next
        For i = 0 To Me.ts_sim.Length - 1
            line_simuliert.Add(Me.ts_sim.Dates(i), Me.ts_sim.Values(i))
        Next
        For i = 0 To Me.ts_obs.Length - 1
            line_fehlerquadrate.Add(Me.ts_obs.Dates(i), (Me.ts_sim.Values(i) - Me.ts_obs.Values(i)) ^ 2)
        Next

    End Sub

End Class
