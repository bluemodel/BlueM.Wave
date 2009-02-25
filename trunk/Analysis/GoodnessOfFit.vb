''' <summary>
''' Goodness Of Fit: Berechnet diverse Gütekriterien für die Anpassung
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:GoodnessOfFit</remarks>
Public Class GoodnessOfFit
    Inherits Analysis

    Private fehlerquadrate(), sum_fehlerquadrate, nash_sutcliffe, volumenfehler As Double
    Private zre_gemessen, zre_simuliert As Zeitreihe

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

        Dim i As Integer
        Dim fehler(), sum_fehler, avg_gemessen, sum_qmittelwertabweichung, sum_simuliert, sum_gemessen, values(,) As Double
        Dim diagresult As DialogResult

        'Preprocessing
        '-------------

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
        '------------

        'Fehler, Fehlerquadrate und Summen
        ReDim Me.fehlerquadrate(values.GetUpperBound(0))
        ReDim fehler(values.GetUpperBound(0))
        sum_fehler = 0
        Me.sum_fehlerquadrate = 0
        sum_gemessen = 0
        sum_simuliert = 0

        For i = 0 To values.GetUpperBound(0)
            fehler(i) = values(i, 1) - values(i, 0) 'simuliert - gemessen
            sum_fehler += fehler(i)
            sum_gemessen += values(i, 0)
            sum_simuliert += values(i, 1)
            Me.fehlerquadrate(i) = fehler(i) ^ 2
            Me.sum_fehlerquadrate += Me.fehlerquadrate(i)
        Next

        'Volumenfehler [%]
        Me.volumenfehler = 100 * (sum_simuliert - sum_gemessen) / sum_gemessen

        'Mittelwert der gemessenen Zeitreihe
        avg_gemessen = sum_gemessen / values.GetUpperBound(0)

        'quadratische Abweichung der gemessenen Werte vom Mittelwert
        sum_qmittelwertabweichung = 0
        For i = 0 To values.GetUpperBound(0)
            sum_qmittelwertabweichung += (values(i, 0) - avg_gemessen) ^ 2
        Next

        'Nash-Sutcliffe - Koeffizient
        Me.nash_sutcliffe = 1 - Me.sum_fehlerquadrate / sum_qmittelwertabweichung

    End Sub

    Public Overrides Sub PrepareResults()

        Dim i As Integer

        'Text:
        '-----
        Me.mResultText = "Goodness of Fit Analyse:" & eol _
                        & eol _
                        & "Gemessene Zeitreihe: " & Me.zre_gemessen.Title & eol _
                        & "Simulierte Zeitreihe: " & Me.zre_simuliert.Title & eol _
                        & eol _
                        & "Die Analyse basiert auf " & Me.zre_gemessen.Length & " gemeinsamen Stützstellen zwischen " & Me.zre_gemessen.Anfangsdatum.ToString(Datumsformat) & " und " & Me.zre_gemessen.Enddatum.ToString(Datumsformat) & eol _
                        & eol _
                        & "Volumenfehler: " & Me.volumenfehler.ToString() & " %" & eol _
                        & "Summe der Fehlerquadrate: " & Me.sum_fehlerquadrate.ToString() & eol _
                        & "Nash-Sutcliffe Koeffizient: " & Me.nash_sutcliffe.ToString()

        'Werte:
        '------
        ReDim Me.mResultValues(2)
        Me.mResultValues(0) = Me.volumenfehler
        Me.mResultValues(1) = Me.sum_fehlerquadrate
        Me.mResultValues(2) = Me.nash_sutcliffe

        'Diagramm:
        '---------
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Goodness of Fit"

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
        For i = 0 to Me.zre_gemessen.Length - 1
            line_gemessen.Add(Me.zre_gemessen.XWerte(i), Me.zre_gemessen.YWerte(i))
        Next
        For i = 0 to Me.zre_simuliert.Length - 1
            line_simuliert.Add(Me.zre_simuliert.XWerte(i), Me.zre_simuliert.YWerte(i))
        Next
        For i = 0 To Me.zre_simuliert.Length - 1
            line_fehlerquadrate.Add(Me.zre_simuliert.XWerte(i), Me.fehlerquadrate(i))
        Next

    End Sub

End Class
