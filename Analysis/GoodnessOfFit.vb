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
''' Goodness Of Fit: Berechnet diverse Gütekriterien für die Anpassung
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:GoodnessOfFit</remarks>
Public Class GoodnessOfFit
    Inherits Analysis

    Private zre_gemessen, zre_simuliert As Zeitreihe
    Private fehlerquadrate() As Double
    Private sum_fehlerquadrate As Double
    Private nash_sutcliffe As Double
    Private volumenfehler As Double
    Private bestimmtheitsmass As Double
    Private korrelationskoeffizient As Double
    Private hydrodev As Double

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
            Return True
        End Get
    End Property

    Public Sub New(ByRef zeitreihen As List(Of Zeitreihe))

        Call MyBase.New(zeitreihen)

        'Prüfung: Anzahl erwarteter Zeitreihen ist 2
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("Für GoodnessOfFit-Analyse müssen genau 2 Zeitreihen ausgewählt werden!")
        End If

    End Sub

    Public Overrides Sub ProcessAnalysis()

        Dim i, n As Integer
        Dim values(,) As Double
        Dim fehler() As Double
        Dim sum_fehler As Double
        Dim avg_gemessen As Double
        Dim sum_qmittelwertabweichung As Double
        Dim sum_simuliert As Double
        Dim sum_gemessen As Double
        Dim kovar As Double
        Dim std_simuliert As Double
        Dim std_gemessen As Double
        Dim avg_simuliert As Double
        Dim max_gemessen As Double
        Dim zaehler As Double
        Dim diagresult As DialogResult

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

        'Auf gemeinsame Stützstellen beschränken
        values = AnalysisHelper.getConcurrentValues(Me.zre_gemessen, Me.zre_simuliert)

        'Berechnungen
        'values(i, 0) -> gemessen
        'values(i, 1) -> simuliert
        '=========================

        'Anzahl Werte
        n = values.GetLength(0)

        'Kennwerte berechnen
        ReDim Me.fehlerquadrate(values.GetUpperBound(0))
        ReDim fehler(values.GetUpperBound(0))
        sum_fehler = 0
        Me.sum_fehlerquadrate = 0
        sum_gemessen = 0
        sum_simuliert = 0

        For i = 0 To n - 1
            fehler(i) = values(i, 1) - values(i, 0) 'simuliert - gemessen
            sum_fehler += fehler(i)
            sum_gemessen += values(i, 0)
            sum_simuliert += values(i, 1)
            Me.fehlerquadrate(i) = fehler(i) ^ 2
            Me.sum_fehlerquadrate += Me.fehlerquadrate(i)
        Next

        'Mittelwerte
        avg_gemessen = sum_gemessen / n
        avg_simuliert = sum_simuliert / n

        'Volumenfehler [%]
        Me.volumenfehler = 100 * (sum_simuliert - sum_gemessen) / sum_gemessen

        'Nash-Sutcliffe - Koeffizient
        '----------------------------
        'quadratische Abweichung der gemessenen Werte vom Mittelwert
        sum_qmittelwertabweichung = 0
        For i = 0 To n - 1
            sum_qmittelwertabweichung += (values(i, 0) - avg_gemessen) ^ 2
        Next

        Me.nash_sutcliffe = 1 - Me.sum_fehlerquadrate / sum_qmittelwertabweichung

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
        Me.korrelationskoeffizient = kovar / (std_simuliert * std_gemessen)
        'Bestimmtheitsmaß
        Me.bestimmtheitsmass = Me.korrelationskoeffizient ^ 2

        'Hydrologische Deviation
        '-----------------------
        max_gemessen = zre_gemessen.getWert("MaxWert")

        zaehler = 0
        For i = 0 To n - 1
            zaehler += Math.Abs(values(i, 0) - values(i, 1)) * values(i, 0)
        Next
        Me.hydrodev = 200 * zaehler / (n * max_gemessen ^ 2)

    End Sub

    Public Overrides Sub PrepareResults()

        Dim i As Integer
        Dim shortText As String
        Const formatstring As String = "F4"

        'Text:
        '-----
        shortText = "Volumenfehler: m = " & Me.volumenfehler.ToString(formatstring) & " %" & eol _
                     & "Summe der Fehlerquadrate: F² = " & Me.sum_fehlerquadrate.ToString(formatstring) & eol _
                     & "Nash-Sutcliffe Effizienz: E = " & Me.nash_sutcliffe.ToString(formatstring) & eol _
                     & "Korrelationskoeffizient: r = " & Me.korrelationskoeffizient.ToString(formatstring) & eol _
                     & "Bestimmtheitsmaß: r² = " & Me.bestimmtheitsmass.ToString(formatstring) & eol _
                     & "Hydrologische Deviation: DEV = " & Me.hydrodev.ToString(formatstring)

        Me.mResultText = "Goodness of Fit Analyse:" & eol _
                         & eol _
                         & "Gemessene Zeitreihe: " & Me.zre_gemessen.Title & eol _
                         & "Simulierte Zeitreihe: " & Me.zre_simuliert.Title & eol _
                         & eol _
                         & "Die Analyse basiert auf " & Me.zre_gemessen.Length & " gemeinsamen Stützstellen zwischen " & Me.zre_gemessen.Anfangsdatum.ToString(Datumsformate("default")) & " und " & Me.zre_gemessen.Enddatum.ToString(Datumsformate("default")) & eol _
                         & eol _
                         & shortText

        'Werte:
        '------
        Me.mResultValues.Add("Volumenfehler", Me.volumenfehler)
        Me.mResultValues.Add("Summe der Fehlerquadrate", Me.sum_fehlerquadrate)
        Me.mResultValues.Add("Nash-Sutcliffe Effizienz", Me.nash_sutcliffe)
        Me.mResultValues.Add("Korrelationskoeffizient", Me.korrelationskoeffizient)
        Me.mResultValues.Add("Bestimmtheitsmaß", Me.bestimmtheitsmass)
        Me.mResultValues.Add("Hydrologische Deviation", Me.hydrodev)

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
        Me.mResultChart.Axes.Left.Title.Caption = "Zeitreihenwerte"

        'rechte Achse
        line_fehlerquadrate.CustomVertAxis = Me.mResultChart.Axes.Right
        Me.mResultChart.Axes.Right.Title.Caption = "Fehlerquadrate"
        Me.mResultChart.Axes.Right.Grid.Visible = False
        Me.mResultChart.Axes.Right.Inverted = True

        'Namen vergeben
        line_gemessen.Title = Me.zre_gemessen.Title
        line_simuliert.Title = Me.zre_simuliert.Title
        line_fehlerquadrate.Title = "Fehlerquadrate"

        'Reihen formatieren
        line_fehlerquadrate.Color = Color.LightGray

        'X-Werte als Zeitdaten einstellen
        line_gemessen.XValues.DateTime = True
        line_simuliert.XValues.DateTime = True
        line_fehlerquadrate.XValues.DateTime = True

        'Werte zu Serien hinzufügen
        For i = 0 To Me.zre_gemessen.Length - 1
            line_gemessen.Add(Me.zre_gemessen.XWerte(i), Me.zre_gemessen.YWerte(i))
        Next
        For i = 0 To Me.zre_simuliert.Length - 1
            line_simuliert.Add(Me.zre_simuliert.XWerte(i), Me.zre_simuliert.YWerte(i))
        Next
        For i = 0 To Me.zre_simuliert.Length - 1
            line_fehlerquadrate.Add(Me.zre_simuliert.XWerte(i), Me.fehlerquadrate(i))
        Next

    End Sub

End Class
