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
            Return Me.XWerte.GetLength(0)
        End Get
        Set(ByVal value As Integer)
            ReDim Preserve Me.XWerte(value - 1)
            ReDim Preserve Me.YWerte(value - 1)
        End Set
    End Property

    Public Property Einheit() As String
        Get
            Return _Einheit
        End Get
        Set(ByVal value As String)
            _Einheit = value
        End Set
    End Property

    Public ReadOnly Property Anfangsdatum() As DateTime
        Get
            Return Me.XWerte(0)
        End Get
    End Property

    Public ReadOnly Property Enddatum() As DateTime
        Get
            Return Me.XWerte(Me.XWerte.GetUpperBound(0))
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"

    'Konstruktor
    '***********
    Public Sub New()
        Me._title = "[nicht gesetzt]"
        Me.Length = 0
    End Sub

    'Konstruktor
    '***********
    Public Sub New(ByVal title As String)
        Me._title = title
        Me.Length = 0
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Title
    End Function

    'Zeitreihe kopieren
    '******************
    Public Function Clone() As Zeitreihe
        Dim target As New Zeitreihe(Me.Title)
        target.Length = Me.Length
        target.XWerte = Me.XWerte
        target.YWerte = Me.YWerte
        Return target
    End Function

    'Zeitreihe kürzen
    '****************
    Public Sub Cut(ByVal start As DateTime, ByVal ende As DateTime)

        Dim j, k, n As Integer

        'Ende finden
        For k = Me.Length - 1 To 0 Step -1
            If (Me.XWerte(k) <= ende) Then
                If (Me.XWerte(k) < ende) Then
                    'Wenn Ende nicht genau getroffen, eine Stützstelle vor
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
                If (Me.XWerte(j) > start) Then
                    'Wenn Anfang nicht genau getroffen, eine Stützstelle zurück
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

    'Einen Wert aus einer Zeitreihe berechnen
    'WertTyp: MaxWert, MinWert, Average, AnfWert, EndWert
    '****************************************************
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
