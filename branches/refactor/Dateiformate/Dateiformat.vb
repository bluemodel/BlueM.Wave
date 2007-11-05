Public MustInherit Class Dateiformat

    'oft verwendete Zeichen (quasi Konstanten)
    '-----------------------------------------
    Public semikolon As Zeichen = New Zeichen(";")
    Public komma As Zeichen = New Zeichen(",")
    Public punkt As Zeichen = New Zeichen(".")
    Public leerzeichen As Zeichen = New Zeichen(" ")
    Public tab As Zeichen = New Zeichen(Chr(9))

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Public Zeitreihen() As Zeitreihe

    Private _file As String                             'Pfad zur Datei
    Private _zeichengetrennt As Boolean = True          'Zeichengetrennte (standardm‰ﬂig) oder Spalten mit fester Breite
    Private _trennzeichen As Zeichen = semikolon        'Spaltentrennzeichen (standardm‰ﬂig Semikolon)
    Private _dezimaltrennzeichen As Zeichen = punkt     'Dezimaltrennzeichen (standardm‰ﬂig Punkt)
    Private _iZeile‹berschriften As Integer = 1         'Zeile mit den Spalten¸berschriften
    Private _iZeileEinheiten As Integer = 2             'Zeile mit den Spalteneinheiten
    Private _iZeileDaten As Integer = 3                 'Erste Zeile mit Daten
    Private _useEinheiten As Boolean = True             'Einheiten auslesen?
    Private _spaltenbreite As Integer = 16              'Breite einer Spalte (standardm‰ﬂig 16)
    Private _XSpalte As String = ""                     'Name der X-Spalte
    Private _Yspalten() As String = {}                  'Array der vorhandenen Y-Spaltennamen
    Private _spaltenSel() As String = {}                'Array der ausgew‰hlten Y-Spaltennamen

    Public ImportDiag As ImportDiag

#End Region 'Eigenschaften

#Region "Properties"

    'Properties
    '##########

    Public Property File() As String
        Get
            Return _file
        End Get
        Set(ByVal value As String)
            _file = value
        End Set
    End Property

    Public Property Zeichengetrennt() As Boolean
        Get
            Return _zeichengetrennt
        End Get
        Set(ByVal value As Boolean)
            _zeichengetrennt = value
        End Set
    End Property

    Public Property Trennzeichen() As Zeichen
        Get
            Return _trennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _trennzeichen = value
        End Set
    End Property

    Public Property Dezimaltrennzeichen() As Zeichen
        Get
            Return _dezimaltrennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _dezimaltrennzeichen = value
        End Set
    End Property

    Public Property iZeile‹berschriften() As Integer
        Get
            Return _iZeile‹berschriften
        End Get
        Set(ByVal value As Integer)
            _iZeile‹berschriften = value
        End Set
    End Property

    Public Property iZeileEinheiten() As Integer
        Get
            Return _iZeileEinheiten
        End Get
        Set(ByVal value As Integer)
            _iZeileEinheiten = value
        End Set
    End Property

    Public Property iZeileDaten() As Integer
        Get
            Return _iZeileDaten
        End Get
        Set(ByVal value As Integer)
            _iZeileDaten = value
        End Set
    End Property

    Public ReadOnly Property nZeilenHeader() As Integer
        Get
            Return _iZeileDaten - 1
        End Get
    End Property

    Public Property UseEinheiten() As Boolean
        Get
            Return _useEinheiten
        End Get
        Set(ByVal value As Boolean)
            _useEinheiten = value
        End Set
    End Property

    Public Property Spaltenbreite() As Integer
        Get
            Return _spaltenbreite
        End Get
        Set(ByVal value As Integer)
            _spaltenbreite = value
        End Set
    End Property

    Public Property XSpalte() As String
        Get
            Return _XSpalte
        End Get
        Set(ByVal value As String)
            _XSpalte = value
        End Set
    End Property

    Public Property YSpalten() As String()
        Get
            Return _Yspalten
        End Get
        Set(ByVal value As String())
            _Yspalten = value
        End Set
    End Property

    Public Property SpaltenSel() As String()
        Get
            Return _spaltenSel
        End Get
        Set(ByVal value As String())
            _spaltenSel = value
        End Set
    End Property

    'Eine Zeitreihe anhand des Titels holen
    '**************************************
    Public ReadOnly Property getReihe(ByVal title As String) As Zeitreihe
        Get
            For i As Integer = 0 To Me.Zeitreihen.GetUpperBound(0)
                If (Me.Zeitreihen(i).Title = title) Then
                    Return Me.Zeitreihen(i)
                End If
            Next
            'Zeitreihe nicht vorhanden
            'TODO: Throw Exception?
            Return New Zeitreihe("unbekannt")
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)

        'Dateinamen setzen
        Me.File = FileName

    End Sub

    'Spalten auslesen
    '****************
    Public MustOverride Sub SpaltenAuslesen()

    'Datei einlesen
    '**************
    Public MustOverride Sub Read_File()

    '‹berpr¸fung, ob eine Spalte ausgew‰hlt ist
    '******************************************
    Protected Function isSelected(ByVal spalte As String) As Boolean

        isSelected = False
        Dim i As Integer

        For i = 0 To Me.SpaltenSel.GetUpperBound(0)
            If (Me.SpaltenSel(i) = spalte) Then
                Return True
            End If
        Next

    End Function

#End Region 'Methoden

End Class
