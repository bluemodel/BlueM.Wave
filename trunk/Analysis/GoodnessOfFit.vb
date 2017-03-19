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
    Private fehlerquadrate() As Double
    Private sum_fehlerquadrate As Double
    Private nash_sutcliffe As Double
    Private volume_observed As Double
    Private volume_simulated As Double
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

    Public Sub New(ByRef zeitreihen As List(Of TimeSeries))

        Call MyBase.New(zeitreihen)

        'Prüfung: Anzahl erwarteter Zeitreihen ist 2
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("The Goodness of Fit analysis requires the selection of exactly 2 time series!")
        End If

    End Sub

    Public Overrides Sub ProcessAnalysis()

        Dim i, n As Integer
        Dim values(,) As Double
        Dim fehler() As Double
        Dim sum_fehler As Double
        Dim avg_gemessen As Double
        Dim sum_qmittelwertabweichung As Double
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

        'Cut series
        Me.zre_gemessen.Cut(Me.zre_simuliert)
        Me.zre_simuliert.Cut(Me.zre_gemessen)

        'Calculate volumes
        volume_simulated = Me.zre_simuliert.Volume
        volume_observed = Me.zre_gemessen.Volume

        'Mittelwerte
        avg_gemessen = Me.zre_gemessen.Average
        avg_simuliert = Me.zre_simuliert.Average

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

        'Fehler
        For i = 0 To n - 1
            fehler(i) = values(i, 1) - values(i, 0) 'simuliert - gemessen
            sum_fehler += fehler(i)
            Me.fehlerquadrate(i) = fehler(i) ^ 2
            Me.sum_fehlerquadrate += Me.fehlerquadrate(i)
        Next

        'Volumenfehler [%]
        Me.volumenfehler = 100 * (volume_simulated - volume_observed) / volume_observed

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
        max_gemessen = zre_gemessen.Maximum

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
        shortText = "Volume observed: Vobs = " & Me.volume_observed.ToString(formatstring) & eol _
                     & "Volume simulated: Vsim = " & Me.volume_simulated.ToString(formatstring) & eol _
                     & "Volume error: m = " & Me.volumenfehler.ToString(formatstring) & " %" & eol _
                     & "Sum of squared errors: F² = " & Me.sum_fehlerquadrate.ToString(formatstring) & eol _
                     & "Nash-Sutcliffe efficiency: E = " & Me.nash_sutcliffe.ToString(formatstring) & eol _
                     & "Coefficient of correlation: r = " & Me.korrelationskoeffizient.ToString(formatstring) & eol _
                     & "Coefficient of determination: r² = " & Me.bestimmtheitsmass.ToString(formatstring) & eol _
                     & "Hydrologic deviation: DEV = " & Me.hydrodev.ToString(formatstring)

        Me.mResultText = "Goodness of Fit analysis:" & eol _
                         & eol _
                         & "Observed time series: " & Me.zre_gemessen.Title & eol _
                         & "Simulated time series: " & Me.zre_simuliert.Title & eol _
                         & eol _
                         & "The analysis is based on " & Me.zre_gemessen.Length & " coincident data points between " & Me.zre_gemessen.StartDate.ToString(Datumsformate("default")) & " and " & Me.zre_gemessen.EndDate.ToString(Datumsformate("default")) & eol _
                         & eol

        'Werte:
        '------
        Me.mResultValues.Add("Volume observed", Me.volume_observed)
        Me.mResultValues.Add("Volume simulated", Me.volume_simulated)
        Me.mResultValues.Add("Volume error [%]", Me.volumenfehler)
        Me.mResultValues.Add("Sum of squared errors", Me.sum_fehlerquadrate)
        Me.mResultValues.Add("Nash-Sutcliffe efficiency", Me.nash_sutcliffe)
        Me.mResultValues.Add("Coefficient of correlation", Me.korrelationskoeffizient)
        Me.mResultValues.Add("Coefficient of determination", Me.bestimmtheitsmass)
        Me.mResultValues.Add("Hydrologic deviation", Me.hydrodev)

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
        For i = 0 To Me.zre_simuliert.Length - 1
            line_fehlerquadrate.Add(Me.zre_simuliert.Dates(i), Me.fehlerquadrate(i))
        Next

    End Sub

End Class
