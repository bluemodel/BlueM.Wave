Imports System.IO

Public Class WEL

    'oft verwendete Zeichen (quasi Konstanten)
    '-----------------------------------------
    Private semikolon As Zeichen = New Zeichen(";")
    Private komma As Zeichen = New Zeichen(",")
    Private punkt As Zeichen = New Zeichen(".")
    Private leerzeichen As Zeichen = New Zeichen(" ")
    Private tab As Zeichen = New Zeichen(Chr(9))

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _file As String                             'Pfad zur Datei
    Private _trennzeichen As Zeichen = semikolon        'Spaltentrennzeichen (standardm��ig Semikolon)
    Private _dezimaltrennzeichen As Zeichen = punkt     'Dezimaltrennzeichen (standardm��ig Punkt)
    Private _XSpalte As String                          'X-Spalte
    Private _Yspalten() As String                       'Array der vorhandenen Y-Spaltennamen
    Private _spaltenSel() As String                     'Array der ausgew�hlten Y-Spaltennamen

    Public Zeitreihen() As Zeitreihe

    Private Const WELHeaderLen As Integer = 3           'Die ersten 3 Zeilen der WEL-Datei geh�ren zum Header


#End Region

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

    Public Property Trennzeichen() As Zeichen
        Get
            Return _trennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _trennzeichen = value
            Me.ComboBox_Trennzeichen.SelectedItem = _trennzeichen
        End Set
    End Property

    Public Property Dezimaltrennzeichen() As Zeichen
        Get
            Return _dezimaltrennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _dezimaltrennzeichen = value
            Me.ComboBox_Dezimaltrennzeichen.SelectedItem = _dezimaltrennzeichen
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

#End Region 'Properties

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, ByVal ParamArray spaltenSel() As String)

        ' Dieser Aufruf ist f�r den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' F�gen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        'Dateinamen setzen
        Me.File = FileName

        'Spalten auslesen
        Call Me.SpaltenAuslesen()

        'Spaltenauswahl
        If (spaltenSel.Length = 0) Then
            'Wenn keine Spaltenauswahl gegeben ist, WEL-Dialog anzeigen
            If (Not Me.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Exit Sub
            End If
        Else
            'Ansonsten Spaltenauswahl �bergeben und WEL direkt einlesen
            Me.SpaltenSel = spaltenSel
            Call Me.Read_WEL()
        End If

    End Sub

    'Form laden
    '**********
    Private Sub WEL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Combobox Trennzeichen initialisieren
        Me.ComboBox_Trennzeichen.BeginUpdate()
        Me.ComboBox_Trennzeichen.Items.Add(Me.semikolon)
        Me.ComboBox_Trennzeichen.Items.Add(Me.komma)
        Me.ComboBox_Trennzeichen.Items.Add(Me.punkt)
        Me.ComboBox_Trennzeichen.Items.Add(Me.leerzeichen)
        Me.ComboBox_Trennzeichen.Items.Add(Me.tab)
        Me.ComboBox_Trennzeichen.EndUpdate()

        Me.ComboBox_Trennzeichen.SelectedItem = Me.Trennzeichen

        'Combobox Dezimaltrennzeichen initialisieren
        Me.ComboBox_Dezimaltrennzeichen.BeginUpdate()
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.punkt)
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.komma)
        Me.ComboBox_Dezimaltrennzeichen.EndUpdate()

        Me.ComboBox_Dezimaltrennzeichen.SelectedItem = Me.Dezimaltrennzeichen

        'Datei als Vorschau anzeigen
        Me.RichTextBox_Vorschau.LoadFile(Me.File, RichTextBoxStreamType.PlainText)

    End Sub


    'Spalten auslesen
    '****************
    Private Sub SpaltenAuslesen()

        Dim i As Integer

        'Datei �ffnen
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        '2. Zeile (Spalten�berschriften) auslesen
        Dim Zeile As String = ""
        For i = 1 To 2
            Zeile = StrRead.ReadLine.ToString
        Next

        'Spaltennamen auslesen
        '---------------------
        Dim alleSpalten() As String
        alleSpalten = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
        'Leerzeichen entfernen
        For i = 0 To alleSpalten.GetUpperBound(0)
            alleSpalten(i) = alleSpalten(i).Trim()
        Next
        'X-Spalte �bernehmen
        Me.XSpalte = alleSpalten(0)
        'Y-Spalten �bernehmen
        Redim Me.YSpalten(alleSpalten.GetUpperBound(0) - 1)
        Array.Copy(alleSpalten, 1, Me.YSpalten, 0, alleSpalten.Length - 1)

        'Anzeige aktualisieren
        '---------------------
        'XSpalte
        Me.TextBox_XSpalte.Text = Me.XSpalte
        'YSpalten
        Me.ListBox_YSpalten.Items.Clear()
        Me.ListBox_YSpalten.Items.AddRange(Me.YSpalten)

    End Sub

    'WEL-Datei einlesen
    '******************
    Public Function Read_WEL() As Zeitreihe()

        Dim AnzZeil As Integer = 0
        Dim i, j, n As Integer
        Dim Zeile As String
        Dim Werte() As String = {}

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrRead.ReadLine.ToString
            AnzZeil += 1
        Loop Until StrRead.Peek() = -1

        'Anzahl Zeitreihen bestimmen
        ReDim Me.Zeitreihen(Me.SpaltenSel.GetUpperBound(0))

        'Zeitreihen redimensionieren
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i) = New Zeitreihe(SpaltenSel(i))
            Me.Zeitreihen(i).Length = AnzZeil - WELHeaderLen
        Next

        'temor�res Array f�r XWerte
        Dim tmpXWerte(AnzZeil - WELHeaderLen - 1) As DateTime

        'Auf Anfang setzen und einlesen
        '------------------------------
        FiStr.Seek(0, SeekOrigin.Begin)

        For i = 0 To AnzZeil - 1
            Werte = StrRead.ReadLine.ToString.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            If (i >= WELHeaderLen) Then
                'Erste Spalte: Datum_Zeit
                tmpXWerte(i - WELHeaderLen) = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(0).Substring(11, 2), Werte(0).Substring(14, 2), 0, New System.Globalization.GregorianCalendar())
                'Restliche Spalten: Werte
                n = 0
                For j = 0 To Me.YSpalten.GetUpperBound(0)
                    If (isSelected(Me.YSpalten(j))) Then
                        Me.Zeitreihen(n).YWerte(i - WELHeaderLen) = Convert.ToDouble(Werte(j + 1))
                        n += 1
                    End If
                Next
            End If
        Next

        StrRead.Close()
        FiStr.Close()

        'XWerte an alle Zeitreihen �bergeben
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i).XWerte = tmpXWerte
        Next

        Return Me.Zeitreihen

    End Function

    '�berpr�fung, ob eine Spalte ausgew�hlt ist
    '******************************************
    Private Function isSelected(ByVal spalte As String) As Boolean

        isSelected = False
        Dim i As Integer

        For i = 0 To Me.SpaltenSel.GetUpperBound(0)
            If (Me.SpaltenSel(i) = spalte) Then
                Return True
            End If
        Next

    End Function

    'OK Button gedr�ckt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Einstellungen �bernehmen
        '------------------------
        'Trenn- und Dezimaltrennzeichen
        Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
        Me.Dezimaltrennzeichen = Me.ComboBox_Dezimaltrennzeichen.SelectedItem

        'Ausgew�hlte Spalten
        Dim i As Integer
        If (Me.ListBox_YSpalten.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Y-Spalte ausw�hlen!", MsgBoxStyle.Exclamation, "Fehler")
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            ReDim Me.SpaltenSel(Me.ListBox_YSpalten.SelectedItems.Count - 1)
            For i = 0 To Me.ListBox_YSpalten.SelectedItems.Count - 1
                Me.SpaltenSel(i) = Me.ListBox_YSpalten.SelectedItems(i)
            Next
        End If

        'WEL-Datei einlesen
        Call Me.Read_WEL()

    End Sub

    'Wenn Trennzeichen ge�ndert wird, Spalten neu auslesen
    '*****************************************************
    Private Sub ComboBox_Trennzeichen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Trennzeichen.SelectedIndexChanged

        Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
        Call Me.SpaltenAuslesen()

    End Sub

#End Region 'Methoden

End Class