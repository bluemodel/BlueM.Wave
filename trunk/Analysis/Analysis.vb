Public MustInherit Class Analysis

    '*********************************
    'Basisklasse f�r Analysefunktionen
    '*********************************

    'Allgemeine Eigenschaften
    Protected _zeitreihen As Collection
    Protected _result() As Double
    Protected _chart As Steema.TeeChart.Chart
    Protected isProcessed As Boolean

    'Flag, der anzeigt, ob die Analysefunktion ein Ergebnisdiagramm erzeugt
    Public hasResultChart As Boolean

    'Konstruktor
    '***********
    Public Sub New(ByRef zeitreihen As Collection)
        Me._zeitreihen = zeitreihen
    End Sub

    'Ergebnis
    '********
    Public MustOverride ReadOnly Property Result() As Double()

    'Ergebnistext
    '************
    Public MustOverride Function ResultText() As String

    'Analyse durchf�hren
    '*******************
    Public MustOverride Sub ProcessAnalysis()

    'Optional: Ergebnischart
    '***********************
    Public Overridable Function ResultChart() As Steema.TeeChart.Chart
        Return New Steema.TeeChart.Chart()
    End Function

End Class
