''' <summary>
''' Berechnet monatliche Statistiken
''' </summary>
''' <remarks>http://130.83.196.154/BlueM/wiki/index.php/Wave:Monatsauswertung</remarks>
Public Class Monatsauswertung
    Inherits Analysis

#Region "Datenstrukturen"

    ''' <summary>
    ''' Struktur zum Speichern der Werte eines Monats
    ''' </summary>
    Private Structure struct_Monatswerte
        Dim monatsname As String
        Dim monatsindex As Integer
        Dim werte As List(Of Double)
        Dim mittelwert As Double
        Dim stdabw As Double
        Dim min As Double
        Dim max As Double
        Dim median As Double
    End Structure

    ''' <summary>
    ''' Struktur zum Speichern der Werte mehrerer Monate
    ''' </summary>
    Private Structure struct_Ergebnis
        Dim Monatswerte() As struct_Monatswerte
    End Structure

#End Region 'Datenstrukturen

#Region "Eigenschaften"

    ''' <summary>
    ''' internes Ergebnis
    ''' </summary>
    Private mErgebnis As struct_Ergebnis

#End Region 'Eigenschaften

#Region "Properties"

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion einen Ergebnistext erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
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

#End Region ' Properties

#Region "Methoden"

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">zu analysierende Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As List(Of Zeitreihe))

        Call MyBase.New(zeitreihen)

        'Prüfung: Genau 1 Zeitreihe erlaubt
        If (zeitreihen.Count <> 1) Then
            Throw New Exception("Bei der Monatsauswertung muss genau 1 Zeitreihe ausgewählt werden!")
        End If

        Dim i As Integer

        'Ergebnisstruktur instanzieren
        ReDim Me.mErgebnis.Monatswerte(11)
        Me.mErgebnis.Monatswerte(0).monatsname = "Januar"
        Me.mErgebnis.Monatswerte(0).monatsindex = 3
        Me.mErgebnis.Monatswerte(1).monatsname = "Februar"
        Me.mErgebnis.Monatswerte(1).monatsindex = 4
        Me.mErgebnis.Monatswerte(2).monatsname = "März"
        Me.mErgebnis.Monatswerte(2).monatsindex = 5
        Me.mErgebnis.Monatswerte(3).monatsname = "April"
        Me.mErgebnis.Monatswerte(3).monatsindex = 6
        Me.mErgebnis.Monatswerte(4).monatsname = "Mai"
        Me.mErgebnis.Monatswerte(4).monatsindex = 7
        Me.mErgebnis.Monatswerte(5).monatsname = "Juni"
        Me.mErgebnis.Monatswerte(5).monatsindex = 8
        Me.mErgebnis.Monatswerte(6).monatsname = "Juli"
        Me.mErgebnis.Monatswerte(6).monatsindex = 9
        Me.mErgebnis.Monatswerte(7).monatsname = "August"
        Me.mErgebnis.Monatswerte(7).monatsindex = 10
        Me.mErgebnis.Monatswerte(8).monatsname = "September"
        Me.mErgebnis.Monatswerte(8).monatsindex = 11
        Me.mErgebnis.Monatswerte(9).monatsname = "Oktober"
        Me.mErgebnis.Monatswerte(9).monatsindex = 12
        Me.mErgebnis.Monatswerte(10).monatsname = "November"
        Me.mErgebnis.Monatswerte(10).monatsindex = 1
        Me.mErgebnis.Monatswerte(11).monatsname = "Dezember"
        Me.mErgebnis.Monatswerte(11).monatsindex = 2

        For i = 0 To 11
            Me.mErgebnis.Monatswerte(i).werte = New List(Of Double)
        Next

    End Sub

    'Analyse ausführen
    '*****************
    Public Overrides Sub ProcessAnalysis()

        Dim reihe As Zeitreihe
        Dim i, j, N As Integer
        Dim summe, summequadrate As Double

        reihe = Me.mZeitreihen.Item(0).getCleanZRE()

        'Werte in Monate einsortieren
        For i = 0 To reihe.Length - 1
            Me.mErgebnis.Monatswerte(reihe.XWerte(i).Month() - 1).werte.Add(reihe.YWerte(i))
        Next

        'Monate analysieren
        For i = 0 To 11
            With Me.mErgebnis.Monatswerte(i)
                summe = 0
                summequadrate = 0
                N = .werte.Count
                If (N > 0) Then
                    'Summen berechnen
                    For j = 0 To N - 1
                        summe += .werte(j)
                        summequadrate += .werte(j) ^ 2
                    Next
                    'Mittelwert
                    .mittelwert = summe / N
                    'Standardabweichung
                    .stdabw = Math.Sqrt((N * summequadrate - summe ^ 2) / (N * (N - 1)))
                    'Werte sortieren
                    Call .werte.Sort()
                    'Min und Max
                    .min = .werte(0)
                    .max = .werte(N - 1)
                    'Median
                    If (N Mod 2 = 0) Then
                        'gerade Anzahl Werte: Mittelwert aus den 2 mittleren Werten
                        .median = (.werte((N / 2) - 1) + .werte(N / 2)) / 2
                    Else
                        'ungerade Anzahl Werte: mittlerer Wert
                        .median = .werte(((N + 1) / 2) - 1)
                    End If
                Else
                    MsgBox("Für den Monat " & .monatsname & " liegen keine Werte vor!", MsgBoxStyle.Exclamation)
                End If

            End With
        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    ''' <remarks>Hier nur Ergebnisdiagramm</remarks>
    Public Overrides Sub PrepareResults()

        Dim i As Integer
        Dim mittelwert, median As Steema.TeeChart.Styles.Line
        Dim stdabw As Steema.TeeChart.Styles.Error
        Dim minmax As Steema.TeeChart.Styles.HighLow

        'Diagramm
        '--------
        Me.mResultChart = New Steema.TeeChart.Chart()
        Me.mResultChart.Aspect.View3D = False
        Me.mResultChart.Header.Text = "Monatsauswertung (" & Me.mZeitreihen(0).Title & ")"

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Text
        Me.mResultChart.Axes.Bottom.Labels.Angle = 90
        Me.mResultChart.Axes.Bottom.MinorTickCount = 0

        'Reihen
        '------

        'MinMax
        minmax = New Steema.TeeChart.Styles.HighLow(Me.mResultChart)
        minmax.Title = "Min / Max"
        minmax.Color = Color.DarkGray
        minmax.Pen.Color = Color.DarkGray
        minmax.HighBrush.Visible = True
        minmax.HighBrush.Color = Color.LightGray
        minmax.HighBrush.Transparency = 50
        For i = 0 To 11
            minmax.Add(Me.mErgebnis.Monatswerte(i).monatsindex, Me.mErgebnis.Monatswerte(i).max, Me.mErgebnis.Monatswerte(i).min, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

        'Standardabweichung
        stdabw = New Steema.TeeChart.Styles.Error(Me.mResultChart)
        stdabw.Title = "Standardabweichung"
        stdabw.Color = Color.Red
        stdabw.ErrorWidth = 50
        For i = 0 To 11
            stdabw.Add(Me.mErgebnis.Monatswerte(i).monatsindex, Me.mErgebnis.Monatswerte(i).mittelwert, Me.mErgebnis.Monatswerte(i).stdabw, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

        'Mittelwert
        mittelwert = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        mittelwert.Title = "Mittelwert"
        mittelwert.Color = Color.Blue
        mittelwert.LinePen.Width = 2
        For i = 0 To 11
            mittelwert.Add(Me.mErgebnis.Monatswerte(i).monatsindex, Me.mErgebnis.Monatswerte(i).mittelwert, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

        'Median
        median = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        median.Title = "Median"
        median.Color = Color.Green
        For i = 0 To 11
            median.Add(Me.mErgebnis.Monatswerte(i).monatsindex, Me.mErgebnis.Monatswerte(i).median, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

    End Sub

#End Region 'Methoden

End Class