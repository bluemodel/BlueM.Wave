''' <summary>
''' Basisklasse für importierbare Dateiformate
''' </summary>
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

    Private _file As String
    Private _zeichengetrennt As Boolean = True
    Private _trennzeichen As Zeichen = semikolon
    Private _dezimaltrennzeichen As Zeichen = punkt
    Private _iZeileUeberschriften As Integer = 1
    Private _iZeileEinheiten As Integer = 2
    Private _iZeileDaten As Integer = 3
    Private _useEinheiten As Boolean = True
    Private _spaltenbreite As Integer = 16
    Private _XSpalte As String = ""
    Private _Yspalten() As String = {}
    Private _spaltenSel() As String = {}
    Private _Einheiten() As String = {}

    ''' <summary>
    ''' Array der in der Datei enhaltenen Zeitreihen
    ''' </summary>
    Public Zeitreihen() As Zeitreihe

    ''' <summary>
    ''' Der ImportDialog
    ''' </summary>
    Public ImportDiag As ImportDiag

#End Region 'Eigenschaften

#Region "Properties"

    'Properties
    '##########
    ''' <summary>
    ''' Pfad zur Datei
    ''' </summary>
    Public Property File() As String
        Get
            Return _file
        End Get
        Set(ByVal value As String)
            _file = value
        End Set
    End Property

    ''' <summary>
    ''' True: Zeichengetrennt (standardmäßig), False: Spalten mit fester Breite
    ''' </summary>
    Public Property Zeichengetrennt() As Boolean
        Get
            Return _zeichengetrennt
        End Get
        Set(ByVal value As Boolean)
            _zeichengetrennt = value
        End Set
    End Property

    ''' <summary>
    ''' Spaltentrennzeichen (standardmäßig Semikolon)
    ''' </summary>
    Public Property Trennzeichen() As Zeichen
        Get
            Return _trennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _trennzeichen = value
        End Set
    End Property

    ''' <summary>
    ''' Dezimaltrennzeichen (standardmäßig Punkt)
    ''' </summary>
    Public Property Dezimaltrennzeichen() As Zeichen
        Get
            Return _dezimaltrennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _dezimaltrennzeichen = value
        End Set
    End Property

    ''' <summary>
    ''' Nummer der Zeile mit den Spaltenüberschriften
    ''' </summary>
    Public Property iZeileUeberschriften() As Integer
        Get
            Return _iZeileUeberschriften
        End Get
        Set(ByVal value As Integer)
            _iZeileUeberschriften = value
        End Set
    End Property

    ''' <summary>
    ''' Nummer der Zeile mit den Spalteneinheiten
    ''' </summary>
    Public Property iZeileEinheiten() As Integer
        Get
            Return _iZeileEinheiten
        End Get
        Set(ByVal value As Integer)
            _iZeileEinheiten = value
        End Set
    End Property

    ''' <summary>
    ''' Nummer der ersten Zeile, die Daten enthält
    ''' </summary>
    Public Property iZeileDaten() As Integer
        Get
            Return _iZeileDaten
        End Get
        Set(ByVal value As Integer)
            _iZeileDaten = value
        End Set
    End Property

    ''' <summary>
    ''' Anzahl Header-Zeilen
    ''' </summary>
    Public ReadOnly Property nZeilenHeader() As Integer
        Get
            Return _iZeileDaten - 1
        End Get
    End Property

    ''' <summary>
    ''' Einheiten auslesen?
    ''' </summary>
    Public Property UseEinheiten() As Boolean
        Get
            Return _useEinheiten
        End Get
        Set(ByVal value As Boolean)
            _useEinheiten = value
        End Set
    End Property

    ''' <summary>
    ''' Breite einer Spalte (standardmäßig 16)
    ''' </summary>
    ''' <remarks>nur bei Spalten fester Breite</remarks>
    Public Property Spaltenbreite() As Integer
        Get
            Return _spaltenbreite
        End Get
        Set(ByVal value As Integer)
            _spaltenbreite = value
        End Set
    End Property

    ''' <summary>
    ''' Name der X-Spalte
    ''' </summary>
    Public Property XSpalte() As String
        Get
            Return _XSpalte
        End Get
        Set(ByVal value As String)
            _XSpalte = value
        End Set
    End Property

    ''' <summary>
    ''' Array der vorhandenen Y-Spaltennamen
    ''' </summary>
    Public Property YSpalten() As String()
        Get
            Return _Yspalten
        End Get
        Set(ByVal value As String())
            _Yspalten = value
        End Set
    End Property

    ''' <summary>
    ''' Array der ausgewählten Y-Spaltennamen
    ''' </summary>
    Public Property SpaltenSel() As String()
        Get
            Return _spaltenSel
        End Get
        Set(ByVal value As String())
            _spaltenSel = value
        End Set
    End Property

    ''' <summary>
    ''' Array der Einheiten
    ''' </summary>
    Public Property Einheiten() As String()
        Get
            Return _Einheiten
        End Get
        Set(ByVal value As String())
            _Einheiten = value
        End Set
    End Property

    ''' <summary>
    ''' Aus der Datei eine Zeitreihe anhand ihres Titels holen
    ''' </summary>
    ''' <param name="title">Titel der zu holenden Zeitreihe</param>
    ''' <returns>Die Zeitreihe</returns>
    ''' <remarks>falls noch nicht eingelesen, wird dies nachgeholt. Schmeiss eine Exception, wenn die Zeitreihe nicht gefunden werdn kann.</remarks>
    Public ReadOnly Property getReihe(ByVal title As String) As Zeitreihe
        Get
            For i As Integer = 0 To Me.Zeitreihen.GetUpperBound(0)
                If (Me.Zeitreihen(i).Title = title) Then
                    'Zeitreihe zurückgeben
                    Return Me.Zeitreihen(i)
                End If
            Next
            'Zeitreihe ist noch nicht eingelesen!
            'Vielleicht ist die Zeitreihe trotzdem in der Datei vorhanden?
            For Each spalte As String In Me.YSpalten
                If (spalte = title) Then
                    'gewünschte Zeitreihe zur Spaltenauswahl hinzufügen
                    ReDim Preserve Me.SpaltenSel(Me.SpaltenSel.GetUpperBound(0) + 1)
                    Me.SpaltenSel(Me.SpaltenSel.GetUpperBound(0)) = title
                    'Zeitreihen erneut einlesen
                    Call Me.Read_File()
                    'Zeitreihe zurückgeben
                    Return Me.getReihe(title)
                End If
            Next
            'Zeitreihe nicht vorhanden!
            Throw New Exception("Zeitreihe '" & title & "' in Datei '" & System.IO.Path.GetFileName(Me.File) & "' nicht gefunden!")
        End Get
    End Property

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public MustOverride ReadOnly Property UseImportDialog() As Boolean

#End Region 'Properties

#Region "Methoden"

    'Methoden
    '########

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="FileName">Kompletter Pfad zur einzulesenden Datei</param>
    Public Sub New(ByVal FileName As String)

        'Objektstruktur initialisieren
        ReDim Me.Zeitreihen(-1)

        'Dateinamen setzen
        Me.File = FileName

    End Sub

    ''' <summary>
    ''' Liest die Spaltennamen aus der Datei
    ''' </summary>
    Public MustOverride Sub SpaltenAuslesen()

    ''' <summary>
    ''' Liest die ausgewählten Spalten (siehe SpaltenSel) ein und legt sie im Array Zeitreihen ab.
    ''' </summary>
    Public MustOverride Sub Read_File()

    ''' <summary>
    ''' Prüft, ob eine Spalte ausgewählt ist
    ''' </summary>
    ''' <param name="spalte">Name der zu prüfenden Spalte</param>
    ''' <returns>True wenn die Spalte ausgewählt wurde, ansonsten False</returns>
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
