Public Class Monatsauswertung
    Inherits Analysis

    '************************************************************************************
    'Analysiert eine Zeitreihe auf Monatsbasis:
    'Für jeden Monat werden Mittelwert, Median, Standardabweichung, Min und Max berechnet
    '************************************************************************************

    Private Structure struct_Ergebnis
        Dim Monatswerte() As struct_Monatswerte
    End Structure

    Private Structure struct_Monatswerte
        Dim monatsname As String
        Dim monatsort As Integer
        Dim werte As List(Of Double)
        Dim mittelwert As Double
        Dim stdabw As Double
        Dim min As Double
        Dim max As Double
        Dim median As Double
    End Structure

    Private mErgebnis As struct_Ergebnis

    'Konstruktor
    '***********
    Public Sub New(ByRef zeitreihen As Collection)

        Call MyBase.New(zeitreihen)

        Dim i As Integer

        Me.hasResultChart = True

        'Ergebnisstruktur instanzieren
        ReDim Me.mErgebnis.Monatswerte(11)
        Me.mErgebnis.Monatswerte(0).monatsname = "Januar"
        Me.mErgebnis.Monatswerte(0).monatsort = 3
        Me.mErgebnis.Monatswerte(1).monatsname = "Februar"
        Me.mErgebnis.Monatswerte(1).monatsort = 4
        Me.mErgebnis.Monatswerte(2).monatsname = "März"
        Me.mErgebnis.Monatswerte(2).monatsort = 5
        Me.mErgebnis.Monatswerte(3).monatsname = "April"
        Me.mErgebnis.Monatswerte(3).monatsort = 6
        Me.mErgebnis.Monatswerte(4).monatsname = "Mai"
        Me.mErgebnis.Monatswerte(4).monatsort = 7
        Me.mErgebnis.Monatswerte(5).monatsname = "Juni"
        Me.mErgebnis.Monatswerte(5).monatsort = 8
        Me.mErgebnis.Monatswerte(6).monatsname = "Juli"
        Me.mErgebnis.Monatswerte(6).monatsort = 9
        Me.mErgebnis.Monatswerte(7).monatsname = "August"
        Me.mErgebnis.Monatswerte(7).monatsort = 10
        Me.mErgebnis.Monatswerte(8).monatsname = "September"
        Me.mErgebnis.Monatswerte(8).monatsort = 11
        Me.mErgebnis.Monatswerte(9).monatsname = "Oktober"
        Me.mErgebnis.Monatswerte(9).monatsort = 12
        Me.mErgebnis.Monatswerte(10).monatsname = "November"
        Me.mErgebnis.Monatswerte(10).monatsort = 1
        Me.mErgebnis.Monatswerte(11).monatsname = "Dezember"
        Me.mErgebnis.Monatswerte(11).monatsort = 2

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

        reihe = Me._zeitreihen.Item(1)

        'Werte in Monate einsortieren
        For i = 0 To reihe.Length - 1
            If (Not reihe.YWerte(i) = Konstanten.NaN) Then 
                Me.mErgebnis.Monatswerte(reihe.XWerte(i).Month() - 1).werte.Add(reihe.YWerte(i))
            End If
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
                    MsgBox("Für den Monat " & .monatsname & " liegen keine Werte vor!", MsgBoxStyle.Exclamation, "Warnung")
                End If

            End With
        Next

        Me.isProcessed = True

    End Sub

    'Ergebnis
    '********
    Public Overrides ReadOnly Property Result() As Double()
        Get
            If (Not isProcessed) Then Throw New Exception("Ergebnis ist noch nicht berechnet!")

            'Es werden die Mittelwerte jedes Monats zurückgegeben
            'Result(0): Mittelwert für Januar,
            'Result(1): Mittelwert für Februar, etc.

            Dim i As Integer

            ReDim Me._result(11)

            For i = 0 To 11
                Me._result(i) = Me.mErgebnis.Monatswerte(i).mittelwert
            Next

            Return Me._result

        End Get
    End Property

    'Ergebnistext
    '************
    Public Overrides Function ResultText() As String

        If (Not isProcessed) Then Throw New Exception("Ergebnis ist noch nicht berechnet!")

        'TODO: Ergebnistext generieren
        Return "siehe Chart"

    End Function

    'Ergebnisdiagramm
    '****************
    Public Overrides Function ResultChart() As Steema.TeeChart.Chart

        If (Not isProcessed) Then Throw New Exception("Ergebnis ist noch nicht berechnet!")

        Dim i As Integer
        Dim mittelwert, median As Steema.TeeChart.Styles.Line
        Dim stdabw As Steema.TeeChart.Styles.Error
        Dim minmax As Steema.TeeChart.Styles.HighLow

        'Chart
        '-----
        Me._chart = New Steema.TeeChart.Chart()
        Me._chart.Aspect.View3D = False
        Me._chart.Header.Text = "Monatsauswertung (" & Me._zeitreihen(1).Title & ")"

        'Achsen
        '------
        Me._chart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Text
        Me._chart.Axes.Bottom.Labels.Angle = 90
        Me._chart.Axes.Bottom.MinorTickCount = 0

        'Reihen
        '------

        'MinMax
        minmax = New Steema.TeeChart.Styles.HighLow(Me._chart)
        minmax.Title = "Min / Max"
        minmax.Color = Color.DarkGray
        minmax.Pen.Color = Color.DarkGray
        minmax.HighBrush.Visible = True
        minmax.HighBrush.Color = Color.LightGray
        minmax.HighBrush.Transparency = 50
        For i = 0 To 11
            minmax.Add(Me.mErgebnis.Monatswerte(i).monatsort, Me.mErgebnis.Monatswerte(i).max, Me.mErgebnis.Monatswerte(i).min, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

        'Standardabweichung
        stdabw = New Steema.TeeChart.Styles.Error(Me._chart)
        stdabw.Title = "Standardabweichung"
        stdabw.Color = Color.Red
        stdabw.ErrorWidth = 50
        For i = 0 To 11
            stdabw.Add(Me.mErgebnis.Monatswerte(i).monatsort, Me.mErgebnis.Monatswerte(i).mittelwert, Me.mErgebnis.Monatswerte(i).stdabw, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

        'Mittelwert
        mittelwert = New Steema.TeeChart.Styles.Line(Me._chart)
        mittelwert.Title = "Mittelwert"
        mittelwert.Color = Color.Blue
        mittelwert.LinePen.Width = 2
        For i = 0 To 11
            mittelwert.Add(Me.mErgebnis.Monatswerte(i).monatsort, Me.mErgebnis.Monatswerte(i).mittelwert, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

        'Median
        median = New Steema.TeeChart.Styles.Line(Me._chart)
        median.Title = "Median"
        median.Color = Color.Green
        For i = 0 To 11
            median.Add(Me.mErgebnis.Monatswerte(i).monatsort, Me.mErgebnis.Monatswerte(i).median, Me.mErgebnis.Monatswerte(i).monatsname)
        Next

        Return Me._chart

    End Function

End Class
