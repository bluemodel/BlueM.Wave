''' <summary>
''' Basisklasse für Analysefunktionen
''' </summary>
Public MustInherit Class Analysis

    'Allgemeine Eigenschaften
    Protected _zeitreihen As Collection
    Protected _result() As Double
    Protected _chart As Steema.TeeChart.Chart
    Protected isProcessed As Boolean

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion ein Ergebnisdiagramm erzeugt
    ''' </summary>
    Public hasResultChart As Boolean

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">Collection von Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As Collection)
        Me._zeitreihen = zeitreihen
    End Sub

    ''' <summary>
    ''' Analyseergebnis in Form eines Arrays von Double
    ''' </summary>
    ''' <remarks>Je nach Analyse</remarks>
    Public MustOverride ReadOnly Property Result() As Double()

    ''' <summary>
    ''' Analyseergebnis in Form von Text
    ''' </summary>
    ''' <remarks>Je nach Analyse</remarks>
    Public MustOverride Function ResultText() As String

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public MustOverride Sub ProcessAnalysis()

    ''' <summary>
    ''' Analyseergebnis in Form eines Diagramms
    ''' </summary>
    ''' <remarks>Je nach Analyse</remarks>
    Public Overridable Function ResultChart() As Steema.TeeChart.Chart
        Return New Steema.TeeChart.Chart()
    End Function

End Class
