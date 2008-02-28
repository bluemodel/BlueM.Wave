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
            ReDim Preserve Me.XWerte(_length - 1)
            ReDim Preserve Me.YWerte(_length - 1)
        End Set
    End Property

#End Region 'Properties

#Region "Methoden"

    'Konstruktor
    '***********
    Public Sub New(ByVal title As String)
        Me._title = title
        Me._length = 0
    End Sub

    'Public Shared Operator =(ByVal zre1 As Zeitreihe, ByVal zre2 As Zeitreihe) As Zeitreihe
    '    zre1.Length = zre2.Length
    '    zre1.Title = zre2.Title
    '    zre1.XWerte = zre2.XWerte
    '    zre1.YWerte = zre2.YWerte
    '    return zre1
    'End Operator

    Public Overrides Function ToString() As String
        Return Me.Title
    End Function

    'Zeitreihe kopieren
    '******************
    Public Function copy() As Zeitreihe
        Dim target As New Zeitreihe(Me.Title)
        target.Length = Me.Length
        target.XWerte = Me.XWerte
        target.YWerte = Me.YWerte
        Return target
    End Function

    'Zeitreihe kürzen
    '****************
    Public Sub cut(ByVal start As DateTime, ByVal ende As DateTime)

        Dim j, k As Integer

        'Zeitreihe kopieren
        Dim tmpZeitreihe As New Zeitreihe("tmp")
        tmpZeitreihe = Me.copy()

        'Zeitschrittweite feststellen
        'BUG 218: Es wird von konstanten Zeitschritten ausgegangen
        Dim dt As TimeSpan = Me.XWerte(1) - Me.XWerte(0)

        'Wenn dt >= 1 Tag, Start und Ende auf Tage runden
        If (dt.TotalDays >= 1) Then
            start = start.Date
            ende = ende.Date
        End If

        'Neue Länge der Zeitreihe
        Dim length As Integer = ((ende - start).TotalSeconds / dt.TotalSeconds) + 1

        'Zeitreihe redimensionieren
        Me.Length = length

        'bis Start vorspulen
        j = 0
        Do Until (tmpZeitreihe.XWerte(j) >= start)
            j += 1
        Loop

        'bis Ende Werte in Zeitreihe zurückkopieren
        k = 0
        Do Until (tmpZeitreihe.XWerte(j) > ende)
            Me.XWerte(k) = tmpZeitreihe.XWerte(j)
            Me.YWerte(k) = tmpZeitreihe.YWerte(j)
            j += 1
            k += 1
            If (j > tmpZeitreihe.XWerte.GetUpperBound(0)) Then
                Exit Do
            End If
        Loop

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
