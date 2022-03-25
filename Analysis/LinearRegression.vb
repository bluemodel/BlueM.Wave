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
''' Lineare Regression füllt Lücken einer Zeitreihe in Abhängigkeit einer anderen Zeitreihe 
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:LineareRegression</remarks>

Friend Class LinearRegression
    Inherits Analysis

    Private zeitreiheKomplett As TimeSeries  'vollständige Zeitreihe
    Private zeitreiheKomplett2 As TimeSeries 'vollständige Zeitreihe Kopie
    Private zeitreiheLuecken As TimeSeries   'lückenhafte Zeitreihe
    Private zeitreiheLuecken2 As TimeSeries  'lückenhafte Zeitreihe ohne Null-Werte
    Private values(,) As Double              'Array der gemeinsamen Stützstellen
    Private a, b As Double                   'Regressionskoeffizienten
    Private r As Double                      'Korrelationskoeffizient
    Private zaehler As Integer = 0           'Zähler für gefüllte Lücken
    Private neueDatume As List(Of Date)      'Liste der neuen Datume
    Private neueWerte As List(Of Double)     'Liste der neuen Werte

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
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion ein Ergebnisdiagramm erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultChart() As Boolean
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
            Return True
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
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("Für eine lineare Regression müssen genau 2 Zeitreihen ausgewählt werden!")
        End If

        'Prüfung: Zeitreihen müssen die gleiche Einheit besitzen
        If (zeitreihen(0).Unit <> zeitreihen(1).Unit) Then
            Throw New Exception("Bitte nur Zeitreihen mit der gleichen Einheit auswählen!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        'Auswahl-Dialog öffnen
        Dim dialog As New LinearRegression_Dialog(Me.mZeitreihen(0).Title, Me.mZeitreihen(1).Title)
        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'Zeitreihen zuweisen (und säubern)
        If (dialog.getNrLueckenhafteReihe = 1) Then
            Me.zeitreiheLuecken = Me.mZeitreihen(0).removeNaNValues()
            Me.zeitreiheKomplett = Me.mZeitreihen(1).removeNaNValues()
            Me.zeitreiheKomplett2 = Me.mZeitreihen(1).removeNaNValues()
        Else
            Me.zeitreiheLuecken = Me.mZeitreihen(1).removeNaNValues()
            Me.zeitreiheKomplett = Me.mZeitreihen(0).removeNaNValues()
            Me.zeitreiheKomplett2 = Me.mZeitreihen(0).removeNaNValues()
        End If

        'Create a copy of the time series with gaps
        Me.zeitreiheLuecken2 = New TimeSeries With {
            .Title = Me.zeitreiheLuecken.Title & " (filled)",
            .Unit = Me.zeitreiheLuecken.Unit,
            .Interpretation = Me.zeitreiheLuecken.Interpretation,
            .Metadata = Me.zeitreiheLuecken.Metadata,
            .DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
        }
        'Copy non-zero nodes
        For i As Integer = 0 To zeitreiheLuecken.Length - 1
            If Me.zeitreiheLuecken.Values(i) <> 0 Then
                Me.zeitreiheLuecken2.AddNode(Me.zeitreiheLuecken.Dates(i), Me.zeitreiheLuecken.Values(i))
            End If
        Next

        'Gemeinsame Stützstellen bestimmen
        Dim ts_gaps As TimeSeries = Me.zeitreiheLuecken2.Clone()
        Dim ts_ref As TimeSeries = Me.zeitreiheKomplett.Clone()
        TimeSeries.Synchronize(ts_gaps, ts_ref)
        ReDim values(ts_gaps.Length - 1, 1)
        For i = 0 To ts_gaps.Length - 1
            values(i, 0) = ts_gaps.Values(i)
            values(i, 1) = ts_ref.Values(i)
        Next

        'Berechnungen
        'values(i, 0) -> luecke
        'values(i, 1) -> komplett
        '===========================

        Dim n As Integer                'Anzahl Werte
        Dim sum_lue, sum_kom As Double  'Summen der Werte
        Dim avg_lue, avg_kom As Double  'Mittelwerte
        Dim std_lue, std_kom As Double  'Standardabweichungen
        Dim kovar As Double             'Kovarianz

        'Anzahl der Werte
        n = values.GetLength(0)

        'Summen der Werte
        For i As Integer = 0 To n - 1
            sum_lue += values(i, 0)
            sum_kom += values(i, 1)
        Next

        'Mittelwerte
        avg_lue = sum_lue / n
        avg_kom = sum_kom / n

        'Standarabweichungen und Kovarianz
        For i As Integer = 0 To n - 1
            std_lue += (values(i, 1) - avg_lue) ^ 2
            std_kom += (values(i, 0) - avg_kom) ^ 2
            kovar += (values(i, 1) - avg_lue) * (values(i, 0) - avg_kom)
        Next

        std_lue = Math.Sqrt(1 / (n - 1) * std_lue)
        std_kom = Math.Sqrt(1 / (n - 1) * std_kom)
        kovar = 1 / (n - 1) * kovar

        'Regressionskoeffizienten
        b = kovar / (std_kom ^ 2)
        a = avg_lue - b * avg_kom

        'Korrelationskoeffizient
        r = kovar / (std_lue * std_kom)

        'Fehlermeldung bei zu niedriger Korrelation
        If (r < 0.7) Then
            Throw New Exception("Der Korrelationskoeffizient ist zu klein! (r < 0,7)" & eol & "Es besteht kein kausaler Zusammenhang zwischen den ausgewählten Zeitreihen! Eine Auffüllung der Lücken ist nicht möglich!")
        End If

        'Listen instanzieren für neue Wertepaare
        neueDatume = New List(Of Date)
        neueWerte = New List(Of Double)

        'Fehlende Werte berechnen und auffüllen
        For i As Integer = 0 To Me.zeitreiheKomplett2.Length - 1
            If (zeitreiheLuecken2.Nodes.ContainsKey(zeitreiheKomplett2.Dates(i))) Then
                'vorhandene Stützstellen bleiben erhalten --> nichts passiert
            Else
                'Werte als neue Stützstellen in Zeitreihe eintragen
                'yi = a + b * xi
                Me.zeitreiheLuecken2.AddNode(Me.zeitreiheKomplett2.Dates(i), a + b * Me.zeitreiheKomplett2.Values(i))

                'Wertepaare speichern für Textausgabe
                neueDatume.Add(Me.zeitreiheKomplett2.Dates(i))
                neueWerte.Add(a + b * Me.zeitreiheKomplett2.Values(i))

                'Zähler der gefüllten Lücken erhöhen
                zaehler += 1
            End If
        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Aufgefüllte Werte als Text
        Dim neueWerteText As String = ""
        For i As Integer = 0 To neueDatume.Count - 1
            neueWerteText &= neueDatume(i) & ":   " & neueWerte(i) & eol
        Next

        'Ergebnistext
        Me.mResultText = "Die Zeitreihe '" & Me.zeitreiheLuecken.Title & "' wurde um " & Me.zaehler & " Werte vervollständigt." & eol & "Bezugslinie: yi = " & Me.a & " + " & Me.b & " * xi" & eol & "Korrelationskoeffizient: r = " & Me.r & eol & "aufgefüllte Wertepaare:" & eol & neueWerteText

        'Ergebniswerte:
        Me.mResultValues.Add("Korrelationskoeffizient", Me.r)

        'Result series
        Me.mResultSeries = New List(Of TimeSeries) From {Me.zeitreiheLuecken2}

    End Sub
End Class
