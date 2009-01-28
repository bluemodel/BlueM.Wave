''' <summary>
''' Statistische Auswertung (Wahrscheinlichkeiten Unterschreitungswahrscheinlichkeiten)
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:Statistik</remarks>
Public Class Statistik
    Inherits Analysis

    Private Const AnzKlassen As Integer = 100
    Dim klassengrösse As Double
    Dim klassen As Double()

    Private Structure ErgebnisWerte
        Dim Titel As String
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
    Public Sub New(ByRef zeitreihen As List(Of Zeitreihe))

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

        'Zeitreihen säubern
        For Each zre As Zeitreihe In Me.mZeitreihen
            zre = zre.getCleanZRE()
        Next

        'Min und max aller Zeitreihen bestimmen
        '--------------------------------------
        min = Double.MaxValue
        max = Double.MinValue
        For Each zre As Zeitreihe In Me.mZeitreihen

            'Min und Max
            min = Math.Min(min, zre.getWert("MinWert"))
            max = Math.Max(max, zre.getWert("MaxWert"))

        Next

        'Klassenaufteilung
        '-----------------
        ReDim Me.klassen(AnzKlassen - 1)
        Me.klassengrösse = (max - min) / (AnzKlassen)
        For i = 0 To AnzKlassen - 1
            Me.klassen(i) = min + i * Me.klassengrösse
        Next

        'Zeitreihen analysieren
        '----------------------
        n = 0
        For Each zre As Zeitreihe In Me.mZeitreihen

            With Me.Ergebnisse(n)

                'Titel
                .Titel = zre.Title

                'Häufigkeiten bestimmen
                '----------------------
                ReDim .häufigkeiten(AnzKlassen - 1)
                'Zeitreihe durchlaufen
                For i = 0 To zre.Length - 1
                    'Klassen durchlaufen
                    For j = 0 To AnzKlassen - 1
                        If (zre.YWerte(i) >= Me.klassen(j) And zre.YWerte(i) < (Me.klassen(j) + Me.klassengrösse)) Then
                            'Klasse gefunden: Häufigkeit hochzählen
                            .häufigkeiten(j) += 1
                            Exit For
                        End If
                    Next
                Next

                'Gesamtmenge bestimmen
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
        Me.mResultText = "Statistik wurde berechnet:" & eol
        Me.mResultText &= "Folgende Klassenaufteilung wurde verwendet:" & eol & eol
        For i As Integer = 0 To AnzKlassen - 1
            Me.mResultText &= "Klasse " & i + 1 & ": " & Me.klassen(i) & " - " & (Me.klassen(i) + Me.klassengrösse) & eol
        Next
        Me.mResultText &= eol

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
        Me.mResultChart.Axes.Bottom.Title.Caption = "Wert [" & Me.mZeitreihen(0).Einheit & "]"

        'Serien
        For Each erg As ErgebnisWerte In Me.Ergebnisse

            Dim serieP As New Steema.TeeChart.Styles.Bar(Me.mResultChart)
            serieP.Title = erg.Titel & " (P(x))"
            serieP.Marks.Visible = False

            For i As Integer = 0 To AnzKlassen - 1
                serieP.Add(Me.klassen(i) + Me.klassengrösse / 2, erg.wahrscheinlichkeiten(i), "Klasse " & (i + 1).ToString & ": " & erg.wahrscheinlichkeiten(i).ToString("F2") & "%")
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
                seriePU.Add(Me.klassen(i) + Me.klassengrösse / 2, erg.PU(i), "Klasse " & (i + 1).ToString & ": " & erg.PU(i).ToString("F2") & "%")
            Next

        Next

        'Markstips
        Dim markstip As New Steema.TeeChart.Tools.MarksTip()
        markstip.Style = Steema.TeeChart.Styles.MarksStyles.Label
        'markstip.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        Me.mResultChart.Tools.Add(markstip)

    End Sub

End Class