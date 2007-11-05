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
    Private _anzKopfzeilen As Integer = 1               'Anzahl Kopfzeilen
    Private _spaltenbreite As Integer = 16              'Breite einer Spalte (standardm‰ﬂig 16)
    Private _XSpalte As String = ""                     'X-Spalte
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
            'Dialoganzeige aktualisieren
            If (Not IsNothing(Me.ImportDiag)) Then
                Me.ImportDiag.ComboBox_Trennzeichen.SelectedItem = _trennzeichen
            End If
        End Set
    End Property

    Public Property Dezimaltrennzeichen() As Zeichen
        Get
            Return _dezimaltrennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _dezimaltrennzeichen = value
            'Dialoganzeige aktualisieren
            If (Not IsNothing(Me.ImportDiag)) Then
                Me.ImportDiag.ComboBox_Dezimaltrennzeichen.SelectedItem = _dezimaltrennzeichen
            End If
        End Set
    End Property

    Public Property AnzKopfzeilen() As Integer
        Get
            Return _anzKopfzeilen
        End Get
        Set(ByVal value As Integer)
            _anzKopfzeilen = value
            'Dialoganzeige aktualisieren
            If (Not IsNothing(Me.ImportDiag)) Then
                Me.ImportDiag.TextBox_AnzKopfzeilen.Text = _anzKopfzeilen.ToString()
            End If
        End Set
    End Property

    Public Property Spaltenbreite() As Integer
        Get
            Return _spaltenbreite
        End Get
        Set(ByVal value As Integer)
            _spaltenbreite = value
            'Dialoganzeige aktualisieren
            If (Not IsNothing(Me.ImportDiag)) Then
                Me.ImportDiag.TextBox_Spaltenbreite.Text = _spaltenbreite.ToString()
            End If
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
