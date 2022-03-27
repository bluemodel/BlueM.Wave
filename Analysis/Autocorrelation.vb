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

        'Parameter-Dialog anzeigen
        Dim dialog As New Autocorrelation_Dialog()
        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'Parameter der Lags aus Dialog abfragen
        Me.lagSize = dialog.groesseLagsErmitteln
        Me.lagCount = dialog.anzahlLagsErmitteln

        Me.ts_in = Me.mZeitreihen(0).Clone()

        'Maximal zulässige Verschiebung
        Dim maxlag As Integer = Me.ts_in.Length / lagSize - 1
        If lagSize * lagCount > Me.ts_in.Length Then
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
        Me.mResultText =
            $"The selected time series was offset {lagCount} times by {lagSize} time steps." & eol &
            raAuswertung & eol &
            $"Assumed periodicity: {periode_avg} time steps" & eol &
            raMaxAuswertung

        'Ergebnisdiagramm
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Helpers.FormatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Autocorrelation for " & ts_in.Title

        'X-Achse
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Bottom.Title.Caption = "Offset (Lag) [number of time steps]"

        'Y-Achse
        Me.mResultChart.Axes.Left.Title.Caption = "Autocorrelation coefficient (-1 < ra < 1)"
        Me.mResultChart.Axes.Left.Automatic = False
        Me.mResultChart.Axes.Left.Minimum = -1
        Me.mResultChart.Axes.Left.Maximum = 1

        'Linie instanzieren und benennen
        Dim line_ra As New Steema.TeeChart.Styles.Bar(Me.mResultChart)
        line_ra.Title = "Autocorrelogram"
        Dim line_raMax As New Steema.TeeChart.Styles.Points(Me.mResultChart)
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
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.mResultChart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move

        'Textfeld in Diagramm einfügen und Position bestimmen
        Dim annot As New Steema.TeeChart.Tools.Annotation(Me.mResultChart)
        annot.Text =
            $"Time series was offset {lagCount} times by {lagSize} time steps." & eol &
            $"Assumed periodicity: {periode_avg}"
        annot.Position = Steema.TeeChart.Tools.AnnotationPositions.RightTop

        'Announce finish
        MyBase.AnalysisProgressFinish()
    End Sub

End Class