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

#Region "allgemeine Eigenschaften"
    protected SpaltenOffset As Integer = 0          'Anzahl Zeichen bevor die erste Spalte anfängt (nur bei Spalten mit fester Breite)
#end region

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
    Private _XSpalte As Integer = 0
    Private _Spalten() As SpaltenInfo
    Private _spaltenSel() As SpaltenInfo

    Public Structure SpaltenInfo
        Public Name As String
        Public Einheit As String
        Public Index As Integer
        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Structure

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
    ''' Index der X-Spalte
    ''' </summary>
    Public Property XSpalte() As Integer
        Get
            Return _XSpalte
        End Get
        Set(ByVal value As Integer)
            _XSpalte = value
        End Set
    End Property

    ''' <summary>
    ''' Array aller in der Datei vorhandenen Spalten
    ''' </summary>
	''' <remarks>inklusive X-Spalte!</remarks>
    Public Property Spalten() As SpaltenInfo()
        Get
            Return _Spalten
        End Get
        Set(ByVal value As SpaltenInfo())
            _Spalten = value
        End Set
    End Property

    ''' <summary>
    ''' Array der ausgewählten Spalten
    ''' </summary>
    Public Property SpaltenSel() As SpaltenInfo()
        Get
            Return _spaltenSel
        End Get
        Set(ByVal value As SpaltenInfo())
            _spaltenSel = value
        End Set
    End Property

    ''' <summary>
    ''' Alle vorhandenen Spalten für den Import auswählen
    ''' </summary>
    ''' <remarks>Die X-Spalte wird nicht mit ausgewählt</remarks>
    Public Sub selectAllSpalten()

        Dim i, n As Integer

        ReDim Me.SpaltenSel(Me.Spalten.Length - 2) 'X-Spalte weglassen

        n = 0
        For i = 0 To Me.Spalten.Length - 1
            If (Me.Spalten(i).Index <> Me.XSpalte) Then
                Me.SpaltenSel(n) = Me.Spalten(i)
                n += 1
            End If
        Next

    End Sub

    ''' <summary>
    ''' Aus der Datei eine Zeitreihe anhand ihres Titels holen
    ''' </summary>
    ''' <param name="title">Titel der zu holenden Zeitreihe</param>
    ''' <returns>Die Zeitreihe</returns>
    ''' <remarks>falls noch nicht eingelesen, wird dies nachgeholt. Schmeisst eine Exception, wenn die Zeitreihe nicht gefunden werden kann.</remarks>
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
            For Each spalte As SpaltenInfo In Me.Spalten
                If (spalte.Name = title) Then
                    'gewünschte Zeitreihe zur Spaltenauswahl hinzufügen
                    ReDim Preserve Me.SpaltenSel(Me.SpaltenSel.GetUpperBound(0) + 1)
                    Me.SpaltenSel(Me.SpaltenSel.GetUpperBound(0)) = spalte
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
    ''' Aus der Datei eine Zeitreihe anhand ihres Index holen
    ''' </summary>
    ''' <param name="index">0-basierter Index der Zeitreihe (0 => 1. Zeitreihenspalte)</param>
    ''' <returns>Die Zeitreihe</returns>
    ''' <remarks>falls noch nicht eingelesen, wird dies nachgeholt. Schmeisst eine Exception, wenn die Zeitreihe nicht gefunden werden kann.</remarks>
    Public ReadOnly Property getReihe(ByVal index As Integer) As Zeitreihe
        Get
            If (index <= Me.Zeitreihen.GetUpperBound(0)) Then
                'Zeitreihe zurückgeben
                Return Me.Zeitreihen(index)
            End If

            'Zeitreihe ist noch nicht eingelesen!
            For Each spalte As SpaltenInfo In Me.Spalten
                If (spalte.Index <> Me.XSpalte And spalte.Index - 1 = index) Then
                    'gewünschte Zeitreihe zur Spaltenauswahl hinzufügen
                    ReDim Preserve Me.SpaltenSel(Me.SpaltenSel.GetUpperBound(0) + 1)
                    Me.SpaltenSel(Me.SpaltenSel.GetUpperBound(0)) = spalte
                    'Zeitreihen erneut einlesen
                    Call Me.Read_File()
                    'Zeitreihe zurückgeben
                    Return Me.getReihe(index)
                End If
            Next
            'Zeitreihe nicht vorhanden!
            Throw New Exception("Zeitreihe '" & index.ToString() & "' in Datei '" & System.IO.Path.GetFileName(Me.File) & "' nicht gefunden!")
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
        ReDim Me.SpaltenSel(-1)

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

#End Region 'Methoden

End Class
