Public Class Zeitreihe

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _title As String
    Private _XWerte() As DateTime
    Private _YWerte() As Double
    Private _length As Integer

#End Region 'Eigenschaften

#Region "Properties"

    'Properties
    '##########

    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property XWerte() As DateTime()
        Get
            Return _XWerte
        End Get
        Set(ByVal value As DateTime())
            _XWerte = value
        End Set
    End Property

    Public Property YWerte() As Double()
        Get
            Return _YWerte
        End Get
        Set(ByVal value As Double())
            _YWerte = value
        End Set
    End Property

    Public Property Length() As Integer
        Get
            Return _length
        End Get
        Set(ByVal value As Integer)
            _length = value
            ReDim Me.XWerte(Me.Length - 1)
            ReDim Me.YWerte(Me.Length - 1)
        End Set
    End Property

#End Region 'Properties

#Region "Methoden"

    'Konstruktor
    '***********
    Public Sub New(ByVal title As String)
        Me.Title = title
    End Sub

#End Region 'Methoden

End Class
