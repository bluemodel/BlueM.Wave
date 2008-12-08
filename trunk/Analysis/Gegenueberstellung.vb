''' <summary>
''' Gegenüberstellung/Vergleich zweier Zeitreihen
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:Gegenueberstellung</remarks>
Public Class Gegenueberstellung
    Inherits Analysis

    Dim ergebnisreihe(,) As Double
    Dim datume() As DateTime

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
    Public Sub New(ByRef zeitreihen As Collection)

        Call MyBase.New(zeitreihen)

        'Prüfung: genau 2 Zeitreihen erlaubt
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("Für die Gegenüberstellung müssen genau 2 Zeitreihen ausgewählt werden!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i As Integer
        Dim reihe1, reihe2 As Zeitreihe
        Dim values(,) As Double

        reihe1 = Me.mZeitreihen(1)
        reihe2 = Me.mZeitreihen(2)

        'nur gemeinsame Stützstellen nutzen
        values = AnalysisHelper.getConcurrentValues(reihe1, reihe2)
        If (values.GetUpperBound(0) + 1 < reihe1.Length) Then  ' + Weil value-Array von 0 zählt
            MessageBox.Show("ACHTUNG: Es wurden Werte entfernt!")
        End If

        ' Ergebnisreihe allokieren
        ReDim Me.ergebnisreihe(values.GetUpperBound(0), 1)

        ' x- und y-Werte der Ergebnisreihe zuweisen
        For i = 0 To values.GetUpperBound(0)
            ergebnisreihe(i, 0) = values(i, 0)
            ergebnisreihe(i, 1) = values(i, 1)
        Next

        'Datume übernehmen (werden später für Punkte-Labels im Diagramm gebraucht)
        datume = reihe1.XWerte

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Text:
        '-----
        Me.mResultText = "Gegenüberstellung:" & eol _
                        & eol _
                        & "Die Analyse basiert auf " & Me.ergebnisreihe.Length & " gemeinsamen Stützstellen zwischen " & Me.datume(0).ToString(Datumsformat) & " und " & Me.datume(Me.datume.Length - 1).ToString(Datumsformat) & eol _
                        & eol

        'Diagramm:
        '---------
        Dim i, ende As Integer
        Dim gegenueberstellung_linie As Steema.TeeChart.Styles.Line

        Me.mResultChart = New Steema.TeeChart.Chart()
        Me.mResultChart.Aspect.View3D = False
        Me.mResultChart.Header.Text = "Gegenüberstellung (" & Me.mZeitreihen(1).Title & " / " & Me.mZeitreihen(2).Title & ")"
        Me.mResultChart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        Me.mResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Title.Caption = Me.mZeitreihen(1).Title & " [" & Me.mZeitreihen(1).Einheit & "]"
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Left.Title.Caption = Me.mZeitreihen(2).Title & " [" & Me.mZeitreihen(2).Einheit & "]"
        Me.mResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        gegenueberstellung_linie = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        gegenueberstellung_linie.Title = "Vergleich" & Me.mZeitreihen(1).Title & " - " & Me.mZeitreihen(2).Title
        gegenueberstellung_linie.Pointer.Visible = True
        gegenueberstellung_linie.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        gegenueberstellung_linie.Pointer.HorizSize = 2
        gegenueberstellung_linie.Pointer.VertSize = 2

        'Werte eintragen
        ende = (ergebnisreihe.Length / 2 - 2)
        For i = 0 To ende
            gegenueberstellung_linie.Add(ergebnisreihe(i, 0), ergebnisreihe(i, 1), datume(i).ToString(Konstanten.Datumsformat))
        Next

        'Markstips
        '---------
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.mResultChart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        markstips.Style = Steema.TeeChart.Styles.MarksStyles.Label
        markstips.Series = gegenueberstellung_linie
        gegenueberstellung_linie.Cursor = Cursors.Help

    End Sub

End Class