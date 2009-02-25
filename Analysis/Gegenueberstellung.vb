''' <summary>
''' Gegenüberstellung/Vergleich zweier Zeitreihen
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:Gegenueberstellung</remarks>
Public Class Gegenueberstellung
    Inherits Analysis

    Dim datume As IList(Of DateTime)
    Dim ergebnisreihe(,) As Double ' Ergebnis der Gegenueberstellung: y-Werte der Reihe(xnummer) werden x-Achsen-Werte, y-Werte der Reihe(ynummer) werden y-Achsen-Werte  
    Dim xnummer As Integer ' Nummer mit der auf mZeitreihen(i) zugegriffen wird, xnummer = Zeitreihe soll auf x-Achse
    Dim ynummer As Integer ' Nummer mit der auf mZeitreihen(i) zugegriffen wird, xnummer = Zeitreihe soll auf y-Achse

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

        ' Dialogaufruf zur Auswahl der x-Achse
        Dim dialog As New Gegenueberstellung_Dialog(Me.mZeitreihen(0).Title, Me.mZeitreihen(1).Title)

        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        ' Zuweisen der x-Achse
        Dim xachse As String
        xachse = dialog.xAchse
        If (xachse = Me.mZeitreihen(0).Title) Then
            xnummer = 0
            ynummer = 1
        Else
            xnummer = 1
            ynummer = 0
        End If

        'Reihen säubern und zuweisen
        reihe1 = Me.mZeitreihen(xnummer).getCleanZRE()
        reihe2 = Me.mZeitreihen(ynummer).getCleanZRE()

        'Nur gemeinsame Stützstellen nutzen
        values = AnalysisHelper.getConcurrentValues(reihe1, reihe2)

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
                        & "Die Analyse basiert auf " & Me.ergebnisreihe.Length & " gemeinsamen Stützstellen zwischen " & Me.datume(0).ToString(Datumsformat) & " und " & Me.datume(Me.datume.Count - 1).ToString(Datumsformat) & eol _
                        & eol

        'Diagramm:
        '---------
        Dim i, ende As Integer
        Dim gegenueberstellung_linie As Steema.TeeChart.Styles.Line

        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Gegenüberstellung (" & Me.mZeitreihen(xnummer).Title & " / " & Me.mZeitreihen(ynummer).Title & ")"
        Me.mResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Title.Caption = Me.mZeitreihen(xnummer).Title & " [" & Me.mZeitreihen(xnummer).Einheit & "]"
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Left.Title.Caption = Me.mZeitreihen(ynummer).Title & " [" & Me.mZeitreihen(ynummer).Einheit & "]"
        Me.mResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        gegenueberstellung_linie = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        gegenueberstellung_linie.Title = "Vergleich" & Me.mZeitreihen(xnummer).Title & " - " & Me.mZeitreihen(ynummer).Title
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