'Copyright (c) 2011, ihwb, TU Darmstadt
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

Public Class LineareRegression
    Inherits Analysis

    Private zeitreiheKomplett As Zeitreihe  'vollständige Zeitreihe
    Private zeitreiheKomplett2 As Zeitreihe 'vollständige Zeitreihe Kopie
    Private zeitreiheLuecken As Zeitreihe   'lückenhafte Zeitreihe
    Private zeitreiheLuecken2 As Zeitreihe  'lückenhafte Zeitreihe ohne Null-Werte
    Private values(,) As Double             'Array der gemeinsamen Stützstellen
    Private a, b As Double                  'Regressionskoeffizienten
    Private r As Double                     'Korrelationskoeffizient
    Private zaehler As Integer = 0          'Zähler für gefüllte Lücken
    Private neueDatume As List(Of Date)     'Liste der neuen Datume
    Private neueWerte As List(Of Double)    'Liste der neuen Werte

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
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">zu analysierende Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As List(Of Zeitreihe))

        'Konstruktor der Basisklasse aufrufen!
        Call MyBase.New(zeitreihen)

        'Prüfung: Anzahl erwarteter Zeitreihen
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("Für eine lineare Regression müssen genau 2 Zeitreihen ausgewählt werden!")
        End If

        'Prüfung: Zeitreihen müssen die gleiche Einheit besitzen
        If (zeitreihen(0).Einheit <> zeitreihen(1).Einheit) Then
            Throw New Exception("Bitte nur Zeitreihen mit der gleichen Einheit auswählen!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        'Auswahl-Dialog öffnen
        Dim dialog As New LineareRegression_Dialog(Me.mZeitreihen(0).Title, Me.mZeitreihen(1).Title)
        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'Zeitreihen zuweisen (und säubern)
        If (dialog.getNrLueckenhafteReihe = 1) Then
            Me.zeitreiheLuecken = Me.mZeitreihen(0).getCleanZRE()
            Me.zeitreiheKomplett = Me.mZeitreihen(1).getCleanZRE()
            Me.zeitreiheKomplett2 = Me.mZeitreihen(1).getCleanZRE()
        Else
            Me.zeitreiheLuecken = Me.mZeitreihen(1).getCleanZRE()
            Me.zeitreiheKomplett = Me.mZeitreihen(0).getCleanZRE()
            Me.zeitreiheKomplett2 = Me.mZeitreihen(0).getCleanZRE()
        End If

        'Schleife zum Überspringen der Nullwerte und speichern in neuer Zeitreihe
        Me.zeitreiheLuecken2 = New Zeitreihe
        For i As Integer = 0 To zeitreiheLuecken.Length - 1
            If Me.zeitreiheLuecken.YWerte(i) <> 0 Then
                Me.zeitreiheLuecken2.AddNode(Me.zeitreiheLuecken.XWerte(i), Me.zeitreiheLuecken.YWerte(i))
            End If
        Next

        'Auf gemeinsame Stützstellen beschränken
        values = AnalysisHelper.getConcurrentValues(Me.zeitreiheLuecken2, Me.zeitreiheKomplett)

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
            If (zeitreiheLuecken2.Nodes.ContainsKey(zeitreiheKomplett2.XWerte(i))) Then
                'vorhandene Stützstellen bleiben erhalten --> nichts passiert
            Else
                'Werte als neue Stützstellen in Zeitreihe eintragen
                'yi = a + b * xi
                Me.zeitreiheLuecken2.AddNode(Me.zeitreiheKomplett2.XWerte(i), a + b * Me.zeitreiheKomplett2.YWerte(i))

                'Wertepaare speichern für Textausgabe
                neueDatume.Add(Me.zeitreiheKomplett2.XWerte(i))
                neueWerte.Add(a + b * Me.zeitreiheKomplett2.YWerte(i))

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

        'Ergebnisdiagramm
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)

        'Y-Achse
        Me.mResultChart.Axes.Left.Title.Caption = zeitreiheKomplett.Einheit

        'Text in Diagramm einfügen
        Dim annot As New Steema.TeeChart.Tools.Annotation(Me.mResultChart)
        annot.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom
        annot.Text = "Bezugslinie: yi = " & Me.a & " + " & Me.b & " * xi" & eol & "Korrelationskoeffizient: r = " & Me.r

        'Linien instanzieren und bennenen
        Dim line_komplett As New Steema.TeeChart.Styles.Line(Me.mResultChart)
        Dim line_luecken As New Steema.TeeChart.Styles.Line(Me.mResultChart)
        Dim line_gefuellt As New Steema.TeeChart.Styles.Line(Me.mResultChart)
        line_komplett.Title = zeitreiheKomplett2.Title & " (Bezugsreihe)"
        line_luecken.Title = zeitreiheLuecken.Title & " (Lücken)"
        line_gefuellt.Title = zeitreiheLuecken.Title & " (aufgefüllt)"

        'Linien befüllen
        For i As Integer = 0 To Me.zeitreiheKomplett2.Length - 1
            line_komplett.Add(Me.zeitreiheKomplett2.XWerte(i), Me.zeitreiheKomplett2.YWerte(i))
        Next

        For i As Integer = 0 To Me.zeitreiheLuecken.Length - 1
            line_luecken.Add(Me.zeitreiheLuecken.XWerte(i), Me.zeitreiheLuecken.YWerte(i))
        Next

        For i As Integer = 0 To Me.zeitreiheLuecken2.Length - 1
            line_gefuellt.Add(Me.zeitreiheLuecken2.XWerte(i), Me.zeitreiheLuecken2.YWerte(i))
        Next

    End Sub
End Class
