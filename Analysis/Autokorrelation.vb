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
''' Autokorrelation analysiert Zeitreihen auf Periodizität
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:Autokorrelation</remarks>

Friend Class Autokorrelation
    Inherits Analysis

    Private zeitreiheEin As TimeSeries  'Eingangszeitreihe
    Private zeitreiheLag As TimeSeries  'Lagzeitreihe
    Private groesseLags As Integer      'Größe der Lags
    Private anzahlLags As Integer       'Anzahl der Lags
    Private values(,) As Double         'Array für gemeinsame Werte der Zeitreihen
    Private raList As List(Of Double)       'Liste für Autokorrelationskoeffizienten
    Private raMaxlist As List(Of Double)    'Liste für maximale Autokorrelationskoeffizienten
    Private periodeList As List(Of Double)  'Liste für Perioden
    Private periode_avg As Double           'Durchschinttliche Periode

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
            Throw New Exception("Für eine Autokorrelation muss genau 1 Zeitreihen ausgewählt werden!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        'Parameter-Dialog anzeigen
        Dim dialog As New Autokorrelation_Dialog()
        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'Parameter der Lags aus Dialog abfragen
        Me.groesseLags = dialog.groesseLagsErmitteln
        Me.anzahlLags = dialog.anzahlLagsErmitteln

        'Maximal zulässige Verschiebung
        Me.zeitreiheEin = Me.mZeitreihen(0).removeNaNValues()
        Dim maxlag As Integer = Me.zeitreiheEin.Length / groesseLags - 1
        If groesseLags * anzahlLags > Me.zeitreiheEin.Length Then
            Throw New Exception("Die ausgewählte Zeitreihe ist zu kurz oder die größte Zeitverschiebung ist zu lang! Bitte wählen sie bei derzeit ausgewählter Größe der Zeitverschiebungen eine Anzahl von maximal " & maxlag & " Verschiebungen!")
        End If

        'ProgressBar Dialog anzeigen und Progressbar initialisieren
        Dim progress_dialog As New Progress_Dialog
        progress_dialog.Show()
        progress_dialog.ProgressBar1.Value = 0
        progress_dialog.ProgressBar1.Minimum = 0
        progress_dialog.ProgressBar1.Maximum = anzahlLags + 1

        'Instanzieren einer neuen Liste für Autokorrelationskoeffizienten
        raList = New List(Of Double)
        raMaxlist = New List(Of Double)
        periodeList = New List(Of Double)

        'Berechnungsschleife über mehrere Verschiebungen
        For j As Integer = 0 To anzahlLags

            'Verschiebung berechnen
            Dim verschiebung As Integer
            verschiebung = j * groesseLags

            'Eingangszeitreihe säubern und leere Lagzeitreihe erstellen
            Me.zeitreiheEin = Me.mZeitreihen(0).removeNaNValues()
            Me.zeitreiheLag = New TimeSeries()

            'Schleife zum Verschieben der Eingangszeitreihe und Speichern als Lagzeitreihe
            Dim i As Integer
            For i = verschiebung To zeitreiheEin.Length - 1
                Me.zeitreiheLag.AddNode(Me.zeitreiheEin.Dates(i), Me.zeitreiheEin.Values(i - verschiebung))
            Next

            'Nur gemeinsamme Stützstellen verwenden
            TimeSeries.Synchronize(Me.zeitreiheEin, Me.zeitreiheLag)
            ReDim values(Me.zeitreiheEin.Length - 1, 1)
            For i = 0 To Me.zeitreiheEin.Length - 1
                values(i, 0) = Me.zeitreiheEin.Values(i)
                values(i, 1) = Me.zeitreiheLag.Values(i)
            Next

            'Berechnungen
            'values(i, 0) -> eingang
            'values(i, 1) -> verschoben
            '=============================

            'Deklarieren und Initialisieren der Berechnungsvariablen
            Dim n As Integer = 0        'Anzahl der Werte
            Dim sum_ein As Double = 0   'Wertesumme der Eingangszeitreihe
            Dim sum_lag As Double = 0   'Wertesumme der Lagzeitreihe
            Dim avg_ein As Double = 0   'Mittelwert der Eingangszeitreihe
            Dim avg_lag As Double = 0   'Mittelwert der Lagzeitreihe
            Dim std_ein As Double = 0   'Standardabweichung der Einganszeitreihe
            Dim std_lag As Double = 0   'Standardabweichung der Lagzeitreihe
            Dim kovar As Double = 0     'Kovarianz
            Dim ra As Double = 0        'Autokorrelationskoeffizient

            'Anzahl der Werte
            n = values.GetLength(0)

            'Summen der Werte
            For i = 0 To n - 1
                sum_ein += values(i, 0)
                sum_lag += values(i, 1)
            Next

            'Mittelwerte
            avg_ein = sum_ein / n
            avg_lag = sum_ein / n

            'Standarabweichungen und Kovarianz
            For i = 0 To n - 1
                std_ein += (values(i, 1) - avg_ein) ^ 2
                std_lag += (values(i, 0) - avg_lag) ^ 2
                kovar += (values(i, 1) - avg_ein) * (values(i, 0) - avg_lag)
            Next

            std_ein = Math.Sqrt(1 / (n - 1) * std_ein)
            std_lag = Math.Sqrt(1 / (n - 1) * std_lag)
            kovar = 1 / (n - 1) * kovar

            'Autokorrelationskoeffizient berechnen und in Liste speichern
            ra = kovar / (std_ein * std_lag)
            raList.Add(ra)

            'ProgressBar erhöhen
            progress_dialog.ProgressBar1.Value += 1
        Next

        'Bestimmung der Scheitelpunkte
        For i As Integer = 2 To raList.Count - 1
            If raList.Item(i - 1) > raList.Item(i - 2) And raList.Item(i - 1) > raList.Item(i) And raList.Item(i - 1) > 0 Then
                raMaxlist.Add(raList.Item(i - 1))
                periodeList.Add((i - 1) * groesseLags)
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

        'ProgressBar Dialog schließen
        progress_dialog.Close()
    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Korrelationskoeffizienten als Ausgabetext
        Dim i As Integer
        Dim raAuswertung As String = ""
        For i = 0 To anzahlLags
            raAuswertung &= "Lag " & i * groesseLags & ": ra = " & raList.Item(i) & eol
        Next

        'Scheitelpunkte der Korrelationskoeffizienten als Ausgabetext
        Dim raMaxAuswertung As String = ""
        For i = 0 To raMaxlist.Count - 1
            raMaxAuswertung &= "Lag " & periodeList.Item(i) & ": " & raMaxlist.Item(i) & eol
        Next

        'Ergebnistext
        Me.mResultText = "Die ausgewählte Zeitreihe wurde " & anzahlLags & " mal um je " & groesseLags & " Zeiteinheiten verschoben." & eol & raAuswertung & eol & "Bei einer vermuteten Periode = " & periode_avg & eol & raMaxAuswertung

        'Ergebnisdiagramm
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Helpers.FormatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Autokorrelation von " & zeitreiheEin.Title

        'X-Achse
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Bottom.Title.Caption = "Zeitverschiebung (Lag)"

        'Y-Achse
        Me.mResultChart.Axes.Left.Title.Caption = "Autokorrelationskoeffizient (-1 < ra < 1)"
        Me.mResultChart.Axes.Left.Automatic = False
        Me.mResultChart.Axes.Left.Minimum = -1
        Me.mResultChart.Axes.Left.Maximum = 1

        'Linie instanzieren und benennen
        Dim line_ra As New Steema.TeeChart.Styles.Bar(Me.mResultChart)
        line_ra.Title = "Autokorrelogramm"
        Dim line_raMax As New Steema.TeeChart.Styles.Points(Me.mResultChart)
        line_raMax.Title = "Scheitelpunkte (vermutet)"

        'Linie befüllen
        For i = 0 To Me.anzahlLags
            Dim mark As String = "Lag " & i * groesseLags & ", ra = " & raList.Item(i)
            line_ra.Add(i * groesseLags, raList.Item(i), mark)
        Next

        For i = 0 To Me.periodeList.Count - 1
            Dim mark As String = "Lag " & periodeList.Item(i) & ", ra = " & raMaxlist.Item(i)
            line_raMax.Add(periodeList.Item(i), raMaxlist.Item(i), mark)
        Next

        'Marks nicht anzeigen
        line_ra.Marks.Visible = False

        'Markstips bei Mausaktion anzeigen
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.mResultChart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move

        'Textfeld in Diagramm einfügen und Position bestimmen
        Dim annot As New Steema.TeeChart.Tools.Annotation(Me.mResultChart)
        annot.Text = "Die ausgewählte Zeitreihe wurde " & anzahlLags & " mal um je " & groesseLags & " Zeiteinheiten verschoben." & eol & "Bei einer vermuteten Periode von p = " & periode_avg
        annot.Position = Steema.TeeChart.Tools.AnnotationPositions.RightTop

    End Sub

End Class