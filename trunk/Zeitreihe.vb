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
            _XWerte = value
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
            _YWerte = value
        End Set
    End Property

    ''' <summary>
    ''' Die Länge (Anzahl Stützstellen) der Zeitreihe
    ''' </summary>
    ''' <remarks>Wird die Länge neu gesetzt, bleiben vorhandene Werte erhalten</remarks>
    Public Property Length() As Integer
        Get
            Return Me.XWerte.GetLength(0)
        End Get
        Set(ByVal value As Integer)
            ReDim Preserve Me.XWerte(value - 1)
            ReDim Preserve Me.YWerte(value - 1)
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
        Me._title = "[nicht gesetzt]"
        Me.Length = 0
    End Sub

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="title">Titel der Zeireihe</param>
    Public Sub New(ByVal title As String)
        Me._title = title
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
    ''' <param name="WertTyp">MaxWert, MinWert, Average, AnfWert, EndWert</param>
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

            Case Else
                Throw New Exception("Der Werttyp '" & WertTyp & "' wird nicht unterstützt!")

        End Select

        Return Wert

    End Function

#End Region 'Methoden

End Class
