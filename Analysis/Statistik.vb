''' <summary>
''' Statistische Auswertung (Wahrscheinlichkeiten Unterschreitungswahrscheinlichkeiten)
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:Statistik</remarks>
Public Class Statistik
    Inherits Analysis

    Private Const AnzKlassen As Integer = 20

    Private Structure ErgebnisWerte
        Dim Titel As String
        Dim klassen As Double()
        Dim klassengrösse As Double
        Dim häufigkeiten, summenhäufigkeiten As Integer()
        Dim menge As Integer
        Dim wahrscheinlichkeiten As Double()
        Dim PU As Double()
    End Structure

    Private Ergebnisse As ErgebnisWerte()

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

        'Konstruktor der Basisklasse aufrufen!
        Call MyBase.New(zeitreihen)

        Dim einheit As String

        'Prüfung: Zeitreihen müssen die gleiche Einheit besitzen
        If (zeitreihen.Count > 1) Then
            einheit = zeitreihen(1).Einheit
            For Each zre As Zeitreihe In zeitreihen
                If (zre.Einheit <> einheit) Then
                    Throw New Exception("Bitte nur Zeitreihen mit der gleichen Einheit auswählen!")
                End If
            Next
        End If

        ReDim Me.Ergebnisse(zeitreihen.Count - 1)

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i, j, n As Integer
        Dim min, max As Double

        'Alle Zeitreihen durchlaufen
        n = 0
        For Each zre As Zeitreihe In Me.mZeitreihen

            'Min und Max bestimmen
            min = zre.getWert("MinWert")
            max = zre.getWert("MaxWert")

            With Me.Ergebnisse(n)

                'Titel
                .Titel = zre.Title

                'Klassenaufteilung
                ReDim .klassen(AnzKlassen - 1)
                .klassengrösse = (max - min) / (AnzKlassen)
                For i = 0 To AnzKlassen - 1
                    .klassen(i) = min + i * .klassengrösse
                Next

                'Häufigkeiten bestimmen
                '----------------------
                ReDim .häufigkeiten(AnzKlassen - 1)
                'Zeitreihe durchlaufen
                For i = 0 To zre.Length - 1
                    'Klassen durchlaufen
                    For j = 0 To AnzKlassen - 1
                        'NaN-Werte überspringen
                        If (zre.YWerte(i) <> Konstanten.NaN) Then
                            If (zre.YWerte(i) >= .klassen(j) And zre.YWerte(i) < (.klassen(j) + .klassengrösse)) Then
                                'Klasse gefunden
                                .häufigkeiten(j) += 1
                                Exit For
                            End If
                        End If
                    Next
                Next

                'Häufigkeiten zusammenzählen
                .menge = 0
                For i = 0 To AnzKlassen - 1
                    .menge += .häufigkeiten(i)
                Next

                'Wahrscheinlichkeiten
                ReDim .wahrscheinlichkeiten(AnzKlassen - 1)
                For i = 0 To AnzKlassen - 1
                    .wahrscheinlichkeiten(i) = .häufigkeiten(i) / .menge * 100 '%
                Next

                'Summenhäufigkeiten
                ReDim .summenhäufigkeiten(AnzKlassen - 1)
                .summenhäufigkeiten(0) = .häufigkeiten(0)
                For i = 1 To AnzKlassen - 1
                    .summenhäufigkeiten(i) = .summenhäufigkeiten(i - 1) + .häufigkeiten(i)
                Next

                'Unterschreitungswahrscheinlichkeiten
                ReDim .PU(AnzKlassen - 1)
                For i = 0 To AnzKlassen - 1
                    .PU(i) = .summenhäufigkeiten(i) / .menge * 100 '%
                Next
            End With
            n += 1
        Next


    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Ergebnistext
        '------------
        Me.mResultText = "Statistik wurde berechnet:" & eol & eol
        Me.mResultText &= "Folgende Klassenaufteilung wurde verwendet:" & eol & eol
        For Each erg As ErgebnisWerte In Me.Ergebnisse
            Me.mResultText &= erg.Titel & ":" & eol
            For i As Integer = 0 To AnzKlassen - 1
                Me.mResultText &= "Klasse " & i + 1 & ": " & erg.klassen(i) & " - " & (erg.klassen(i) + erg.klassengrösse) & eol
            Next
            Me.mResultText &= eol
        Next

        'Ergebniswerte
        '-------------
        'TODO: Ergebniswerte zurückgeben?

        'Ergebnisdiagramm
        '----------------

        'Diagramm formatieren
        Me.mResultChart = New Steema.TeeChart.Chart()
        Me.mResultChart.Aspect.View3D = False
        Me.mResultChart.Header.Text = "Statistische Auswertung"

        'Legende
        Me.mResultChart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series

        'Achsen
        Me.mResultChart.Axes.Left.Title.Caption = "Wahrscheinlichkeit [%]"
        Me.mResultChart.Axes.Left.Automatic = False
        Me.mResultChart.Axes.Left.Minimum = 0
        Me.mResultChart.Axes.Left.AutomaticMaximum = True
        Me.mResultChart.Axes.Left.MaximumOffset = 2

        Me.mResultChart.Axes.Right.Visible = True
        Me.mResultChart.Axes.Right.Title.Caption = "Unterschreitungswahrscheinlichkeit [%]"
        Me.mResultChart.Axes.Right.Title.Angle = 90
        Me.mResultChart.Axes.Right.Automatic = False
        Me.mResultChart.Axes.Right.Minimum = 0
        Me.mResultChart.Axes.Right.Maximum = 100

        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Bottom.Title.Caption = "Wert [" & Me.mZeitreihen(1).Einheit & "]"

        'Serien
        For Each erg As ErgebnisWerte In Me.Ergebnisse

            Dim serieP As New Steema.TeeChart.Styles.Bar(Me.mResultChart)
            serieP.Title = erg.Titel & " (P(x))"
            serieP.Marks.Visible = False

            For i As Integer = 0 To AnzKlassen - 1
                serieP.Add(erg.klassen(i) + erg.klassengrösse / 2, erg.wahrscheinlichkeiten(i), erg.klassen(i).ToString)
            Next

            Dim seriePU As New Steema.TeeChart.Styles.Line(Me.mResultChart)
            seriePU.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
            seriePU.Title = erg.Titel & " (PU(x))"
            seriePU.LinePen.Width = 2
            seriePU.Pointer.Visible = True
            seriePU.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
            seriePU.Pointer.HorizSize = 2
            seriePU.Pointer.VertSize = 2

            For i As Integer = 0 To AnzKlassen - 1
                seriePU.Add(erg.klassen(i) + erg.klassengrösse / 2, erg.PU(i), erg.klassen(i).ToString)
            Next

        Next

        'Markstips
        Dim markstip As New Steema.TeeChart.Tools.MarksTip()
        markstip.Style = Steema.TeeChart.Styles.MarksStyles.Label
        'markstip.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        Me.mResultChart.Tools.Add(markstip)

    End Sub

End Class