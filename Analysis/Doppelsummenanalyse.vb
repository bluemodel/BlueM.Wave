Public Class Doppelsummenanalyse
    Inherits Analysis

    Dim summe1(), summe2() As Double
    Dim datume() As Datetime

    'Konstruktor
    '***********
    Public Sub New(ByRef zeitreihen As Collection)

        Call MyBase.New(zeitreihen)

        'Prüfung: genau 2 Zeitreihen erlaubt
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("Für die Doppelsummenanalyse müssen genau 2 Zeitreihen ausgewählt werden!")
        End If

        Me.hasResultChart = True

    End Sub

    Public Overrides Sub ProcessAnalysis()

        Dim i, j, n As Integer
        Dim zre1, zre2 As Zeitreihe
        Dim found As Boolean

        zre1 = Me._zeitreihen.Item(1)
        zre2 = Me._zeitreihen.Item(2)

        'zunächst die Zeitreihen zuschneiden
        Call zre1.Cut(zre2)
        Call zre2.Cut(zre1)

        ReDim summe1(0)
        ReDim summe2(0)
        ReDim datume(0)

        'Ersten gemeinsamen Wert finden
        found = False
        j = 0
        For i = 0 To zre1.Length - 1
            'Ende von zre2 abfangen
            If (j > zre2.Length - 1) Then
                Exit For
            End If
            'NaN-Werte in zre1 überspringen
            If (zre1.YWerte(i) <> NaN) Then
                'Korrepondierenden Wert in zre2 suchen
                Do
                    'Ende von zre2 abfangen
                    If (j > zre2.Length - 1) Then
                        Exit Do
                    End If
                    If (zre2.XWerte(j) = zre1.XWerte(i)) Then
                        'NaN-Werte in zre2 überspringen
                        If (zre2.YWerte(j) <> NaN) Then
                            'Übereinstimmung gefunden!
                            summe1(0) = zre1.YWerte(i)
                            summe2(0) = zre2.YWerte(j)
                            datume(0) = zre1.XWerte(i)
                            found = True
                        End If
                    End If
                    j += 1
                Loop Until (zre2.XWerte(j) > zre1.XWerte(i))
            End If
            If (found) Then Exit For
        Next

        If (Not found) Then Throw New Exception("Es konnte keine gemeinsame Stützstelle gefunden werden!")

        'Weitere gemeinsame Werte aufsummieren
        n = 1
        Do Until (i > (zre1.Length - 1) Or j > (zre2.Length - 1))
            i += 1
            'NaN-Werte in zre1 überspringen
            If (zre1.YWerte(i) <> NaN) Then
                'Korrepondierenden Wert in zre2 suchen
                Do
                    If (zre2.XWerte(j) = zre1.XWerte(i)) Then
                        'NaN-Werte in zre2 überspringen
                        If (zre2.YWerte(j) <> NaN) Then
                            'Übereinstimmung gefunden!
                            ReDim Preserve summe1(n)
                            ReDim Preserve summe2(n)
                            summe1(n) = zre1.YWerte(i) + summe1(n - 1)
                            summe2(n) = zre2.YWerte(j) + summe2(n - 1)
                            ReDim Preserve datume(n)
                            datume(n) = zre1.XWerte(i)
                            n += 1
                        End If
                    End If
                    j += 1
                    'Ende von zre2 abfangen
                    If (j > zre2.Length - 1) Then
                        Exit Do
                    End If
                Loop Until (zre2.XWerte(j) > zre1.XWerte(i))
            End If
        Loop

        Me.isProcessed = True

    End Sub

    Public Overrides ReadOnly Property Result() As Double()
        Get
            If (Not isProcessed) Then Throw New Exception("Ergebnis ist noch nicht berechnet!")

            Return New Double() {}

        End Get
    End Property

    Public Overrides Function ResultText() As String

        If (Not isProcessed) Then Throw New Exception("Ergebnis ist noch nicht berechnet!")

        Return "Siehe Diagramm"

    End Function

    'Ergebnisdiagramm
    '****************
    Public Overrides Function ResultChart() As Steema.TeeChart.Chart

        If (Not isProcessed) Then Throw New Exception("Ergebnis ist noch nicht berechnet!")

        Dim i As Integer
        Dim doppelsumme As Steema.TeeChart.Styles.Line

        'Chart
        '-----
        Me._chart = New Steema.TeeChart.Chart()
        Me._chart.Aspect.View3D = False
        Me._chart.Header.Text = "Doppelsummenanalyse (" & Me._zeitreihen(1).Title & " / " & Me._zeitreihen(2).Title & ")"
        Me._chart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        Me._chart.Legend.Visible = False

        'Achsen
        '------
        Me._chart.Axes.Bottom.Title.Caption = "Summe " & Me._zeitreihen(1).Title
        Me._chart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me._chart.Axes.Left.Title.Caption = "Summe " & Me._zeitreihen(2).Title
        Me._chart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        doppelsumme = New Steema.TeeChart.Styles.Line(Me._chart)
        doppelsumme.Title = "Doppelsumme " & Me._zeitreihen(1).Title & " - " & Me._zeitreihen(2).Title
        doppelsumme.Pointer.Visible = True
        doppelsumme.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        doppelsumme.Pointer.HorizSize = 2
        doppelsumme.Pointer.VertSize = 2
        doppelsumme.Cursor = Cursors.Help

        'Werte eintragen
        For i = 0 To summe1.Length - 1
            doppelsumme.Add(summe1(i), summe2(i), datume(i).ToString(Konstanten.Datumsformat))
        Next

        'Markstips
        '---------
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me._chart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        markstips.Style = Steema.TeeChart.Styles.MarksStyles.Label

        Return Me._chart

    End Function

End Class
