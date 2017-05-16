'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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
''' <remarks>http://wiki.bluemodel.org/index.php/Wave:GoodnessOfFit</remarks>
Public Class GoodnessOfFit
    Inherits Analysis

    Private zre_gemessen, zre_simuliert As TimeSeries

    Private GoFResults As Dictionary(Of String, GoF)

    Private Structure GoF
        Public startDate As DateTime
        Public endDate As DateTime
        Public nValues As Long
        Public sum_fehlerquadrate As Double
        Public nash_sutcliffe As Double
        Public ln_nash_sutcliffe As Double
        Public volume_observed As Double
        Public volume_simulated As Double
        Public volumenfehler As Double
        Public bestimmtheitsmass As Double
        Public korrelationskoeffizient As Double
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
    ''' <remarks>the time series must already be cut to the same timespan</remarks>
    Private Function calculateGOF(ByVal ts_o As TimeSeries, ByVal ts_s As TimeSeries) As GoF

        Dim i, n As Integer
        Dim values(,) As Double
        Dim fehler() As Double
        Dim fehlerquadrate() As Double
        Dim sum_fehler As Double
        Dim avg_gemessen As Double
        Dim sum_qmittelwertabweichung As Double
        Dim kovar As Double
        Dim std_simuliert As Double
        Dim std_gemessen As Double
        Dim avg_simuliert As Double
        Dim max_gemessen As Double
        Dim zaehler As Double

        Dim _gof As New GoF()

        'Calculate volumes
        _gof.volume_simulated = ts_s.Volume
        _gof.volume_observed = ts_o.Volume

        'Mittelwerte
        avg_gemessen = ts_o.Average
        avg_simuliert = ts_s.Average

        'Auf gemeinsame Stützstellen beschränken
        values = AnalysisHelper.getConcurrentValues(ts_o, ts_s)

        'Berechnungen
        'values(i, 0) -> gemessen
        'values(i, 1) -> simuliert
        '=========================

        _gof.startDate = ts_o.StartDate
        _gof.endDate = ts_o.EndDate
        _gof.nValues = ts_o.Length

        'Anzahl Werte
        n = values.GetLength(0)

        'Kennwerte berechnen
        ReDim fehlerquadrate(values.GetUpperBound(0))
        ReDim fehler(values.GetUpperBound(0))

        sum_fehler = 0
        _gof.sum_fehlerquadrate = 0

        'Fehler
        For i = 0 To n - 1
            fehler(i) = values(i, 1) - values(i, 0) 'simuliert - gemessen
            sum_fehler += fehler(i)
            fehlerquadrate(i) = fehler(i) ^ 2
            _gof.sum_fehlerquadrate += fehlerquadrate(i)
        Next

        'Volumenfehler [%]
        _gof.volumenfehler = 100 * (_gof.volume_simulated - _gof.volume_observed) / _gof.volume_observed

        'Nash-Sutcliffe - Koeffizient
        '----------------------------
        'quadratische Abweichung der gemessenen Werte vom Mittelwert
        sum_qmittelwertabweichung = 0
        For i = 0 To n - 1
            sum_qmittelwertabweichung += (values(i, 0) - avg_gemessen) ^ 2
        Next

        _gof.nash_sutcliffe = 1 - _gof.sum_fehlerquadrate / sum_qmittelwertabweichung

        'Logarithmic Nash-Sutcliffe coefficient
        '--------------------------------------
        'Formula: Reff,ln = 1 - (SUM[(ln(x_i)-ln(y_i))^2]) / (SUM[(ln(x_i)-ln(x_avg))^2])
        'Reference: Merkblatt BWK M7, Okt. 2008
        Dim ln_avg_gemessen As Double
        Dim sum_ln_diff_squared As Double = 0.0
        Dim sum_ln_diff_avg_squared As Double = 0.0

        ln_avg_gemessen = Math.Log(avg_gemessen)

        For i = 0 To n - 1
            sum_ln_diff_squared += (Math.Log(values(i, 0)) - Math.Log(values(i, 1))) ^ 2
            sum_ln_diff_avg_squared += (Math.Log(values(i, 0)) - ln_avg_gemessen) ^ 2
        Next

        _gof.ln_nash_sutcliffe = 1 - sum_ln_diff_squared / sum_ln_diff_avg_squared

        'Korrelationskoeffizient
        '-----------------------
        'Formel: r = sxy / (sx * sy)
        'Standardabweichung: sx = SQRT[ 1 / (n-1) * SUMME[(x_i - x_avg)^2] ]
        'Kovarianz: kovar = sxy = 1 / (n-1) * SUMME[(x_i - x_avg) * (y_i - y_avg)]
        kovar = 0
        std_simuliert = 0
        std_gemessen = 0

        For i = 0 To n - 1
            kovar += (values(i, 1) - avg_simuliert) * (values(i, 0) - avg_gemessen)
            std_simuliert += (values(i, 1) - avg_simuliert) ^ 2
            std_gemessen += (values(i, 0) - avg_gemessen) ^ 2
        Next

        std_simuliert = Math.Sqrt(1 / (n - 1) * std_simuliert)
        std_gemessen = Math.Sqrt(1 / (n - 1) * std_gemessen)
        kovar = 1 / (n - 1) * kovar

        'Korrelationskoeffizient
        _gof.korrelationskoeffizient = kovar / (std_simuliert * std_gemessen)
        'Bestimmtheitsmaß
        _gof.bestimmtheitsmass = _gof.korrelationskoeffizient ^ 2

        'Hydrologische Deviation
        '-----------------------
        max_gemessen = zre_gemessen.Maximum

        zaehler = 0
        For i = 0 To n - 1
            zaehler += Math.Abs(values(i, 0) - values(i, 1)) * values(i, 0)
        Next
        _gof.hydrodev = 200 * zaehler / (n * max_gemessen ^ 2)

        Return _gof

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

        'Zeitreihen zuweisen (und säubern)
        If (dialog.getNrGemesseneReihe = 1) Then
            Me.zre_gemessen = Me.mZeitreihen(0).getCleanZRE()
            Me.zre_simuliert = Me.mZeitreihen(1).getCleanZRE()
        Else
            Me.zre_gemessen = Me.mZeitreihen(1).getCleanZRE()
            Me.zre_simuliert = Me.mZeitreihen(0).getCleanZRE()
        End If

        'Cut series
        Me.zre_gemessen.Cut(Me.zre_simuliert)
        Me.zre_simuliert.Cut(Me.zre_gemessen)

        'use entire series by default
        series_o.Add("Entire series", Me.zre_gemessen)
        series_s.Add("Entire series", Me.zre_simuliert)

        'do yearly analysis?
        If dialog.CheckBox_Annual.Checked Then
            'cut observed and simulated time series into separate series
            Dim ts_o, ts_s As TimeSeries
            Dim year, year_start, year_end As Integer
            year_start = zre_gemessen.StartDate.Year
            year_end = zre_gemessen.EndDate.Year
            'first year (may be incomplete)
            ts_o = Me.zre_gemessen.Clone()
            ts_o.Cut(Me.zre_gemessen.StartDate, New DateTime(year_start, 12, 31, 23, 59, 59))
            series_o.Add(year_start, ts_o)
            ts_s = Me.zre_simuliert.Clone()
            ts_s.Cut(Me.zre_simuliert.StartDate, New DateTime(year_start, 12, 31, 23, 59, 59))
            series_s.Add(year_start, ts_s)
            'following years
            For year = year_start + 1 To year_end - 1
                ts_o = Me.zre_gemessen.Clone()
                ts_o.Cut(New DateTime(year, 1, 1, 0, 0, 0), New DateTime(year, 12, 31, 23, 59, 59))
                series_o.Add(year, ts_o)
                ts_s = Me.zre_simuliert.Clone()
                ts_s.Cut(New DateTime(year, 1, 1, 0, 0, 0), New DateTime(year, 12, 31, 23, 59, 59))
                series_s.Add(year, ts_s)
            Next
            'last year (may be incomplete)
            ts_o = Me.zre_gemessen.Clone()
            ts_o.Cut(New DateTime(year_end, 1, 1, 0, 0, 0), Me.zre_gemessen.EndDate)
            series_o.Add(year_end, ts_o)
            ts_s = Me.zre_simuliert.Clone()
            ts_s.Cut(New DateTime(year_end, 1, 1, 0, 0, 0), Me.zre_simuliert.EndDate)
            series_s.Add(year_end, ts_s)
        End If

        'Calculate GoF parameters for all series
        For Each key As String In series_o.Keys
            Me.GoFResults.Add(key, Me.calculateGOF(series_o(key), series_s(key)))
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
        shortText = "Entire series (" & _gof.startDate.ToString(Datumsformate("default")) & " - " & _gof.endDate.ToString(Datumsformate("default")) & "):" & eol
        shortText &= "Volume observed: Vobs = " & _gof.volume_observed.ToString(formatstring) & eol _
                     & "Volume simulated: Vsim = " & _gof.volume_simulated.ToString(formatstring) & eol _
                     & "Volume error: m = " & _gof.volumenfehler.ToString(formatstring) & " %" & eol _
                     & "Sum of squared errors: F² = " & _gof.sum_fehlerquadrate.ToString(formatstring) & eol _
                     & "Nash-Sutcliffe efficiency: E = " & _gof.nash_sutcliffe.ToString(formatstring) & eol _
                     & "Logarithmic Nash-Sutcliffe efficiency: E,ln = " & _gof.ln_nash_sutcliffe.ToString(formatstring) & eol _
                     & "Coefficient of correlation: r = " & _gof.korrelationskoeffizient.ToString(formatstring) & eol _
                     & "Coefficient of determination: r² = " & _gof.bestimmtheitsmass.ToString(formatstring) & eol _
                     & "Hydrologic deviation: DEV = " & _gof.hydrodev.ToString(formatstring)

        'mResultText is written to the log. Contains all results.
        Me.mResultText = "Goodness of Fit analysis:" & eol _
                         & eol _
                         & "Observed time series: " & Me.zre_gemessen.Title & eol _
                         & "Simulated time series: " & Me.zre_simuliert.Title & eol _
                         & eol
        'output results in CSV format
        Me.mResultText &= "Results:" & eol
        Me.mResultText &= "Description;Start;End;Length;Volume observed;Volume simulated;Volume error [%];Sum of squared errors;Nash-Sutcliffe efficiency;Logarithmic Nash-Sutcliffe efficiency;Coefficient of correlation;Coefficient of determination;Hydrologic deviation" & eol
        Me.mResultText &= "desc;t0;t1;n;Vobs;Vsim;m;F²;E;E,ln;r;r²;DEV" & eol
        For Each GOFResult As KeyValuePair(Of String, GoF) In Me.GoFResults
            _gof = GOFResult.Value
            Dim values() As String
            ReDim values(12)
            values(0) = GOFResult.Key
            values(1) = _gof.startDate.ToString(Datumsformate("default"))
            values(2) = _gof.endDate.ToString(Datumsformate("default"))
            values(3) = _gof.nValues.ToString()
            values(4) = _gof.volume_observed.ToString(formatstring)
            values(5) = _gof.volume_simulated.ToString(formatstring)
            values(6) = _gof.volumenfehler.ToString(formatstring) & "%"
            values(7) = _gof.sum_fehlerquadrate.ToString(formatstring)
            values(8) = _gof.nash_sutcliffe.ToString(formatstring)
            values(9) = _gof.ln_nash_sutcliffe.ToString(formatstring)
            values(10) = _gof.korrelationskoeffizient.ToString(formatstring)
            values(11) = _gof.bestimmtheitsmass.ToString(formatstring)
            values(12) = _gof.hydrodev.ToString(formatstring)
            Me.mResultText &= Join(values, ";") & eol
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
        line_gemessen.Title = Me.zre_gemessen.Title
        line_simuliert.Title = Me.zre_simuliert.Title
        line_fehlerquadrate.Title = "Squared error"

        'Reihen formatieren
        line_fehlerquadrate.Color = Color.LightGray

        'X-Werte als Zeitdaten einstellen
        line_gemessen.XValues.DateTime = True
        line_simuliert.XValues.DateTime = True
        line_fehlerquadrate.XValues.DateTime = True

        'Werte zu Serien hinzufügen
        For i = 0 To Me.zre_gemessen.Length - 1
            line_gemessen.Add(Me.zre_gemessen.Dates(i), Me.zre_gemessen.Values(i))
        Next
        For i = 0 To Me.zre_simuliert.Length - 1
            line_simuliert.Add(Me.zre_simuliert.Dates(i), Me.zre_simuliert.Values(i))
        Next
        Dim cvalues(,) As Double
        cvalues = AnalysisHelper.getConcurrentValues(Me.zre_gemessen, Me.zre_simuliert)
        For i = 0 To cvalues.GetUpperBound(0) - 1
            line_fehlerquadrate.Add(Me.zre_simuliert.Dates(i), (cvalues(i, 1) - cvalues(i, 0)) ^ 2)
        Next

    End Sub

End Class
