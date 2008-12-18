Public Class Zeitreihe

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _title As String
    Private _XWerte() As DateTime
    Private _YWerte() As Double
    Private _Einheit As String
   

#End Region 'Eigenschaften

#Region "Properties"

    'Properties
    '##########

    ''' <summary>
    ''' Titel der Zeitreihe
    ''' </summary>
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    ''' <summary>
    ''' Die X-Werte der Zeitreihe als Array von DateTime
    ''' </summary>
    Public Property XWerte() As DateTime()
        Get
            Return _XWerte
        End Get
        Set(ByVal value As DateTime())
            ReDim Me._XWerte(value.Length - 1)
            Call Array.Copy(value, Me._XWerte, value.Length)
        End Set
    End Property

    ''' <summary>
    ''' Die Y-Werte der Zeitreihe als Array von Double
    ''' </summary>
    ''' <value></value>
    Public Property YWerte() As Double()
        Get
            Return _YWerte
        End Get
        Set(ByVal value As Double())
            ReDim Me._YWerte(value.Length - 1)
            Call Array.Copy(value, Me._YWerte, value.Length)
        End Set
    End Property

    ''' <summary>
    ''' Die Länge (Anzahl Stützstellen) der Zeitreihe
    ''' </summary>
    ''' <value>Die neu zu setzenden Länge. Ist die neue Länge kürzer als die aktuelle Länge, wird von hinten abgeschnitten, andernfalls wird mit leeren Werten aufgefüllt</value>
    Public Property Length() As Integer
        Get
            Return Me._XWerte.GetLength(0)
        End Get
        Set(ByVal value As Integer)
            ReDim Preserve Me._XWerte(value - 1)
            ReDim Preserve Me._YWerte(value - 1)
        End Set
    End Property

    ''' <summary>
    ''' Einheit der Zeitreihe
    ''' </summary>
    Public Property Einheit() As String
        Get
            Return _Einheit
        End Get
        Set(ByVal value As String)
            _Einheit = value
        End Set
    End Property

    ''' <summary>
    ''' Anfangsdatum
    ''' </summary>
   Public ReadOnly Property Anfangsdatum() As DateTime
      Get
         Return Me.XWerte(0)
      End Get

   End Property

    ''' <summary>
    ''' Enddatum
    ''' </summary>
   Public ReadOnly Property Enddatum() As DateTime
      Get
         Return Me.XWerte(Me.XWerte.GetUpperBound(0))
      End Get

   End Property

#End Region 'Properties

#Region "Methoden"

   'Methoden
   '########

   ''' <summary>
   ''' Default Konstruktor
   ''' </summary>
   Public Sub New()
      Me._title = "-"
      Me._Einheit = "-"
      Me.Length = 0
   End Sub

   ''' <summary>
   ''' Konstruktor
   ''' </summary>
   ''' <param name="title">Titel der Zeireihe</param>
   Public Sub New(ByVal title As String)
      Me._title = title
      Me._Einheit = "-"
      Me.Length = 0
   End Sub

   ''' <summary>
   ''' Gibt den Zeitreihen-Titel zurück
   ''' </summary>
   Public Overrides Function ToString() As String
      Return Me.Title
   End Function

   ''' <summary>
   ''' Zeitreihe kopieren
   ''' </summary>
   Public Function Clone() As Zeitreihe
      Dim target As New Zeitreihe(Me.Title)
      target.Einheit = Me.Einheit
      target.Length = Me.Length
      target.XWerte = Me.XWerte
      target.YWerte = Me.YWerte
      Return target
   End Function

   ''' <summary>
   ''' Zeitreihe zuschneiden
   ''' </summary>
   ''' <param name="start">Startdatum</param>
   ''' <param name="ende">Enddatum</param>
   ''' <remarks>Wenn Anfangs- und/oder Enddatum nicht genau als Stützstellen vorliegen, wird an den nächstaußeren Stützstellen abgeschnitten</remarks>
   Public Overloads Sub Cut(ByVal start As DateTime, ByVal ende As DateTime)

      Dim j, k, n As Integer

      'Ende finden
      For k = Me.Length - 1 To 0 Step -1
         If (Me.XWerte(k) <= ende) Then
            'Wenn Ende nicht genau getroffen, 
            'dann eine weitere Stützstelle hinten mitnehmen
            If (Me.XWerte(k) < ende And k < Me.Length - 1) Then
               k += 1
            End If
            Exit For
         End If
      Next

      'Ende abschneiden
      ReDim Preserve Me.XWerte(k)
      ReDim Preserve Me.YWerte(k)

      'Anfang finden
      For j = 0 To Me.Length - 1
         If (Me.XWerte(j) >= start) Then
            'Wenn Anfang nicht genau getroffen,
            'dann eine weitere Stützstelle vorne mitnehmen
            If (Me.XWerte(j) > start And j > 0) Then
               j -= 1
            End If
            Exit For
         End If
      Next

      'Neue Länge
      n = k - j + 1

      'Umdrehen, Anfang abschneiden und wieder umdrehen
      Call Array.Reverse(Me.XWerte)
      Call Array.Reverse(Me.YWerte)

      ReDim Preserve Me.XWerte(n - 1)
      ReDim Preserve Me.YWerte(n - 1)

      Call Array.Reverse(Me.XWerte)
      Call Array.Reverse(Me.YWerte)

   End Sub

   ''' <summary>
   ''' Schneidet die Zeitreihe auf den Zeitraum einer 2. Zeitreihe zu
   ''' </summary>
   ''' <param name="zre2">die 2. Zeitreihe</param>
   ''' <remarks></remarks>
   Public Overloads Sub Cut(ByVal zre2 As Zeitreihe)

      If (Me.Anfangsdatum < zre2.Anfangsdatum Or Me.Enddatum > zre2.Enddatum) Then
         Call Me.Cut(zre2.Anfangsdatum, zre2.Enddatum)
      End If

   End Sub

   ''' <summary>
   ''' Einen Wert aus einer Zeitreihe berechnen
   ''' </summary>
   ''' <param name="WertTyp">MaxWert, MinWert, Average, AnfWert, EndWert, Summe</param>
   ''' <returns>der berechnete Wert</returns>
   Public Function getWert(ByVal WertTyp As String) As Double

      Dim i As Integer
      Dim Wert As Double

      Select Case WertTyp

         Case "MaxWert"
            Wert = 0
            For i = 0 To Me.Length - 1
               If (Me.YWerte(i) > Wert) Then
                  Wert = Me.YWerte(i)
               End If
            Next

         Case "MinWert"
            Wert = Double.MaxValue
            For i = 0 To Me.Length - 1
               If (Me.YWerte(i) < Wert) Then
                  Wert = Me.YWerte(i)
               End If
            Next

         Case "Average"
            Wert = 0
            For i = 0 To Me.Length - 1
               Wert += Me.YWerte(i)
            Next
            Wert = Wert / Me.Length

         Case "AnfWert"
            Wert = Me.YWerte(0)

         Case "EndWert"
            Wert = Me.YWerte(Me.Length - 1)

         Case "Summe"
            Wert = 0
            For i = 0 To Me.Length - 1
               Wert += Me.YWerte(i)
            Next

         Case Else
            Throw New Exception("Der Werttyp '" & WertTyp & "' wird nicht unterstützt!")

      End Select

      Return Wert

   End Function

   ''' <summary>
   ''' Erstellt eine neue äquidistante Zeitreihe
   ''' </summary>
   ''' <param name="Soll_dT">Sollzeitschritt</param>      
   Public Function MakeKontiZeitreihe(ByVal Soll_dT As Integer) As KontiZeitreihe
      Dim i As Integer
      Dim j As Integer
      Dim intloop As Integer
      Dim Ist_dT As Integer
      Dim AnzZusWerte As Integer
      Dim SumZusWerte As Long

      Dim OutZR As New KontiZeitreihe("Konti_" & Me.Title)
      OutZR.Einheit = Me.Einheit

      ReDim Preserve OutZR.XWerte(Me.Length - 1)
      ReDim Preserve OutZR.YWerte(Me.Length - 1)

      j = 0
      SumZusWerte = 0
      For i = 0 To Me.Length - 2
         AnzZusWerte = 0
         Ist_dT = DateDiff(DateInterval.Minute, Me.XWerte(i), Me.XWerte(i + 1))
         If Ist_dT - Soll_dT > 0 Then
            AnzZusWerte = (Ist_dT / Soll_dT) - 1
            SumZusWerte = SumZusWerte + AnzZusWerte
            ReDim Preserve OutZR.XWerte(Me.Length - 1 + SumZusWerte)
            ReDim Preserve OutZR.YWerte(Me.Length - 1 + SumZusWerte)
            OutZR.XWerte(j) = Me.XWerte(i)
            OutZR.YWerte(j) = Me.YWerte(i)
            j = j + 1
            For intloop = 1 To AnzZusWerte
               OutZR.XWerte(j) = XWerte(i).AddMinutes(intloop * Soll_dT)
               OutZR.YWerte(j) = 0
               j = j + 1
            Next
         Else
            OutZR.XWerte(j) = Me.XWerte(i)
            OutZR.YWerte(j) = Me.YWerte(i)
            j = j + 1
         End If
      Next

      'letzten Wert schreiben
      OutZR.XWerte(j) = Me.XWerte(i)
      OutZR.YWerte(j) = Me.YWerte(i)
      
      Return OutZR

   End Function

#End Region 'Methoden

End Class
