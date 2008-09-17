''' <summary>
''' Goodness Of Fit: Summe der Fehlerquadrate und Nash-Sutcliffe berechnen
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:GoodnessOfFit</remarks>
Public Class GoodnessOfFit
    Inherits Analysis

    Private fehlerquadrate(), nash_sutcliffe, sum_fehlerquadrate As Double
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

    Public Sub New(ByRef zeitreihen As Collection)

        Call MyBase.New(zeitreihen)

        'Prüfung: Anzahl erwarteter Zeitreihen ist 2
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("Für GoodnessOfFit-Analyse müssen genau 2 Zeitreihen ausgewählt werden!")
        End If

    End Sub

    Public Overrides Sub ProcessAnalysis()

        Dim i As Integer
        Dim mittelwert, sum_qmittelwertabweichung, values(,) As Double

        Me.zre_gemessen = Me.mZeitreihen(1)
        Me.zre_simuliert = Me.mZeitreihen(2)

        'Auf gemeinsame Stützstellen beschränken
        values = AnalysisHelper.getConcurrentValues(Me.zre_gemessen, Me.zre_simuliert)

        'Summe der Fehlerquadrate
        ReDim Me.fehlerquadrate(values.GetUpperBound(0))
        Me.sum_fehlerquadrate = 0
        For i = 0 To values.GetUpperBound(0)
            Me.fehlerquadrate(i) = (values(i, 0) - values(i, 1)) ^ 2
            Me.sum_fehlerquadrate += Me.fehlerquadrate(i)
        Next

        'Mittelwert der gemessenen Zeitreihe
        mittelwert = Me.zre_gemessen.getWert("Average")

        'quadratische Abweichung der gemessenen Werte vom Mittelwert
        sum_qmittelwertabweichung = 0
        For i = 0 To values.GetUpperBound(0)
            sum_qmittelwertabweichung += (values(i, 0) - mittelwert) ^ 2
        Next

        'Nash-Sutcliffe - Koeffizient
        Me.nash_sutcliffe = 1 - Me.sum_fehlerquadrate / sum_qmittelwertabweichung

    End Sub

    Public Overrides Sub PrepareResults()

        'Text:
        '-----
        Me.mResultText = "Goodness of Fit Analyse:" & eol _
                        & eol _
                        & "Gemessene Zeitreihe: " & Me.zre_gemessen.Title & eol _
                        & "Simulierte Zeitreihe: " & Me.zre_simuliert.Title & eol _
                        & eol _
                        & "Die Analyse basiert auf " & Me.zre_gemessen.Length & " gemeinsamen Stützstellen zwischen " & Me.zre_gemessen.Anfangsdatum.ToString(Datumsformat) & " und " & Me.zre_gemessen.Enddatum.ToString(Datumsformat) & eol _
                        & eol _
                        & "Summe der Fehlerquadrate: " & Me.sum_fehlerquadrate.ToString() & eol _
                        & "Nash-Sutcliffe Koeffizient: " & Me.nash_sutcliffe.ToString()

        'Werte:
        '------
        ReDim Me.mResultValues(1)
        Me.mResultValues(0) = Me.sum_fehlerquadrate
        Me.mResultValues(1) = Me.nash_sutcliffe

        'Diagramm:
        '---------
        Me.mResultChart = New Steema.TeeChart.Chart()
        Me.mResultChart.Aspect.View3D = False
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

        'Namen vergeben
        line_gemessen.Title = Me.zre_gemessen.Title
        line_simuliert.Title = Me.zre_simuliert.Title
        line_fehlerquadrate.Title = "Fehlerquadrate"

        'X-Werte als Zeitdaten einstellen
        line_gemessen.XValues.DateTime = True
        line_simuliert.XValues.DateTime = True
        line_fehlerquadrate.XValues.DateTime = True

        'Werte hinzufügen
        line_gemessen.Add(Me.zre_gemessen.XWerte, Me.zre_gemessen.YWerte)
        line_simuliert.Add(Me.zre_simuliert.XWerte, Me.zre_simuliert.YWerte)
        line_fehlerquadrate.Add(Me.zre_gemessen.XWerte, Me.fehlerquadrate)

    End Sub

End Class
