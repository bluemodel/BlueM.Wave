''' <summary>
''' Basisklasse für Analysefunktionen
''' </summary>
Public MustInherit Class Analysis

#Region "Eigenschaften"

    ''' <summary>
    ''' Die zu analysierenden Zeitreihen
    ''' </summary>
    Protected mZeitreihen As List(Of Zeitreihe)

    ''' <summary>
    ''' Ergebnistext
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Protected mResultText As String

    ''' <summary>
    ''' Ergebniswerte
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Protected mResultValues() As Double

    ''' <summary>
    ''' Ergebnisdiagramm
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Protected mResultChart As Steema.TeeChart.Chart

#End Region 'Eigenschaften

#Region "Properties"

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion einen Ergebnistext erzeugt
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultText() As Boolean

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion Ergebniswerte erzeugt
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultValues() As Boolean

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion ein Ergebnisdiagramm erzeugt
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultChart() As Boolean

    ''' <summary>
    ''' Analyseergebnis in Form von Text
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultText() As String
        Get
            Return Me.mResultText
        End Get
    End Property

    ''' <summary>
    ''' Analyseergebnis in Form eines Arrays von Double
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultValues() As Double()
        Get
            Return Me.mResultValues
        End Get
    End Property

    ''' <summary>
    ''' Analyseergebnis in Form eines Diagramms
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultChart() As Steema.TeeChart.Chart
        Get
            Return Me.mResultChart
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">Collection von Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As List(Of Zeitreihe))
        Me.mZeitreihen = zeitreihen
    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public MustOverride Sub ProcessAnalysis()

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public MustOverride Sub PrepareResults()

#End Region 'Methoden

End Class
