''' <summary>
''' Doppelsummenanalyse zweier Zeitreihen
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:Doppelsummenanalyse</remarks>
Public Class Doppelsummenanalyse
    Inherits Analysis

    Dim summe1(), summe2() As Double
    Dim datume As IList(Of DateTime)

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
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">zu analysierende Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As List(Of Zeitreihe))

        Call MyBase.New(zeitreihen)

        'Prüfung: genau 2 Zeitreihen erlaubt
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("Für die Doppelsummenanalyse müssen genau 2 Zeitreihen ausgewählt werden!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i As Integer
        Dim zre1, zre2 As Zeitreihe
        Dim values(,) As Double

        zre1 = Me.mZeitreihen(0)
        zre2 = Me.mZeitreihen(1)

        'nur gemeinsame Stützstellen nutzen
        values = AnalysisHelper.getConcurrentValues(zre1, zre2)

        ReDim Me.summe1(values.GetUpperBound(0))
        ReDim Me.summe2(values.GetUpperBound(0))

        'Erster Wert
        Me.summe1(0) = values(0, 0)
        Me.summe2(0) = values(0, 1)

        'Weitere Werte kumulativ aufsummieren
        For i = 1 To values.GetUpperBound(0)
            Me.summe1(i) = values(i, 0) + summe1(i - 1)
            Me.summe2(i) = values(i, 1) + summe2(i - 1)
        Next

        'Datume übernehmen (werden später für Punkte-Labels im Diagramm gebraucht)
        datume = zre1.XWerte

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Text:
        '-----
        Me.mResultText = "Doppelsummenanalyse:" & eol _
                        & eol _
                        & "Die Analyse basiert auf " & Me.summe1.Length & " gemeinsamen Stützstellen zwischen " & Me.datume(0).ToString(Datumsformat) & " und " & Me.datume(Me.datume.Count - 1).ToString(Datumsformat) & eol _
                        & eol

        'Diagramm:
        '---------
        Dim i As Integer
        Dim doppelsumme, gerade As Steema.TeeChart.Styles.Line

        Me.mResultChart = New Steema.TeeChart.Chart()
        Me.mResultChart.Aspect.View3D = False
        Me.mResultChart.Header.Text = "Doppelsummenanalyse (" & Me.mZeitreihen(0).Title & " / " & Me.mZeitreihen(1).Title & ")"
        Me.mResultChart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        Me.mResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Title.Caption = "Summe " & Me.mZeitreihen(0).Title
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Left.Title.Caption = "Summe " & Me.mZeitreihen(1).Title
        Me.mResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        doppelsumme = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        doppelsumme.Title = "Doppelsumme " & Me.mZeitreihen(0).Title & " - " & Me.mZeitreihen(1).Title
        doppelsumme.Pointer.Visible = True
        doppelsumme.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        doppelsumme.Pointer.HorizSize = 2
        doppelsumme.Pointer.VertSize = 2

        gerade = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        gerade.Title = "45° Gerade"
        gerade.Color = Color.DarkGray
        gerade.linepen.Style = Drawing2D.DashStyle.Dash

        'Werte eintragen
        For i = 0 To summe1.Length - 1
            doppelsumme.Add(summe1(i), summe2(i), datume(i).ToString(Konstanten.Datumsformat))
        Next

        gerade.Add(0, 0)
        Dim maxwert As Double = Math.Min(Me.mResultChart.Axes.Bottom.MaxXValue, Me.mResultChart.Axes.Left.MaxYValue)
        gerade.Add(maxwert, maxwert)

        'Markstips
        '---------
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.mResultChart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        markstips.Style = Steema.TeeChart.Styles.MarksStyles.Label
        markstips.Series = doppelsumme
        doppelsumme.Cursor = Cursors.Help

    End Sub

End Class
