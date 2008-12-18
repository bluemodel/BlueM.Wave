Public Class KontiZeitreihe
   Inherits Zeitreihe

#Region "Eigenschaften"

   'Eigenschaften
   '#############

   Private _Anfangsdatum As DateTime
   Private _Enddatum As DateTime
   Private _Zeitintervall As Integer

#End Region


#Region "Properties"

   'Properties
   '##########

   '''' <summary>
   '''' Anfangsdatum
   '''' </summary>
   'Public Overloads Property Anfangsdatum() As DateTime
   '   Get
   '      Return Me.XWerte(0)
   '   End Get
   '   Set(ByVal Datum As DateTime)
   '      _Anfangsdatum = Datum
   '   End Set
   'End Property

   '''' <summary>
   '''' Enddatum
   '''' </summary>
   'Public Overloads Property Enddatum() As DateTime
   '   Get
   '      Return Me.XWerte(Me.XWerte.GetUpperBound(0))
   '   End Get
   '   Set(ByVal Datum As DateTime)
   '      _Enddatum = Datum
   '   End Set
   'End Property


   Public Property Zeitintervall() As Integer
      Get
         Return _Zeitintervall
      End Get
      Set(ByVal value As Integer)
         _Zeitintervall = value
      End Set
   End Property

#End Region

#Region "Methoden"

   'Methoden
   '########

   'Konstruktor
   '***********
   Public Sub New(ByVal title As String)
      MyBase.New(title)
   End Sub

#End Region

End Class
