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
    Private _zeichengetrennt As Boolean = True          'Zeichengetrennte (standardmäßig) oder Spalten mit fester Breite
    Private _trennzeichen As Zeichen = semikolon        'Spaltentrennzeichen (standardmäßig Semikolon)
    Private _spaltenbreite As Integer = 16              'Breite einer Spalte (standardmäßig 17)
    Private _XSpalte As String                          'X-Spalte
    Private _Yspalten() As String                       'Array der vorhandenen Y-Spaltennamen
    Private _spaltenSel() As String                     'Array der ausgewählten Y-Spaltennamen

    Public Zeitreihen() As Zeitreihe

    Private Const WELHeaderLen As Integer = 3           'Die ersten 3 Zeilen der WEL-Datei gehören zum Header
    Private Const SpaltenOffset As Integer = 1          'Anzahl Zeichen bevor die erste Spalte anfängt (nur bei Spalten mit fester Breite)


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

    Public Property Zeichengetrennt() As Boolean
        Get
            Return _zeichengetrennt
        End Get
        Set(ByVal value As Boolean)
            _zeichengetrennt = value
            If (_zeichengetrennt) Then
                Me.RadioButton_Zeichengetrennt.Checked = True
                Me.ComboBox_Trennzeichen.Enabled = True
                Me.TextBox_Spaltenbreite.Enabled = False
            Else
                Me.RadioButton_Zeichengetrennt.Checked = False
                Me.ComboBox_Trennzeichen.Enabled = False
                Me.TextBox_Spaltenbreite.Enabled = True
            End If

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

    Public Property Spaltenbreite() As Integer
        Get
            Return _spaltenbreite
        End Get
        Set(ByVal value As Integer)
            _spaltenbreite = value
            Me.TextBox_Spaltenbreite.Text = _spaltenbreite.ToString()
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
    Public Sub New(ByVal FileName As String, ByVal ParamArray spaltenSel() As String)

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

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
            'Ansonsten Spaltenauswahl übergeben und WEL direkt einlesen
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

        'Standardeinstellungen setzen
        Me.Zeichengetrennt = True
        Me.Trennzeichen = Me.semikolon
        Me.Spaltenbreite = 16

        'Datei als Vorschau anzeigen
        Me.Label_Datei.Text += " " & Path.GetFileName(Me.File)
        Me.RichTextBox_Vorschau.LoadFile(Me.File, RichTextBoxStreamType.PlainText)

    End Sub


    'Spalten auslesen
    '****************
    Private Sub SpaltenAuslesen()

        Dim i As Integer

        'Datei öffnen
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        '2. Zeile (Spaltenüberschriften) auslesen
        Dim Zeile As String = ""
        For i = 1 To 2
            Zeile = StrRead.ReadLine.ToString
        Next

        'Spaltennamen auslesen
        '---------------------
        Dim alleSpalten() As String

        If (Me.Zeichengetrennt) Then
            'Zeichengetrennt
            alleSpalten = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
        Else
            'Spalten mit fester Breite
            Dim anzSpalten As Integer = Math.Ceiling(Zeile.Length / Me.Spaltenbreite)
            ReDim alleSpalten(anzSpalten - 1)
            For i = 0 To anzSpalten - 1
                alleSpalten(i) = Zeile.Substring((i * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, Zeile.Substring((i * Me.Spaltenbreite) + SpaltenOffset).Length))
            Next
        End If

        'Leerzeichen entfernen
        For i = 0 To alleSpalten.GetUpperBound(0)
            alleSpalten(i) = alleSpalten(i).Trim()
        Next
        'X-Spalte übernehmen
        Me.XSpalte = alleSpalten(0)
        'Y-Spalten übernehmen
        ReDim Me.YSpalten(alleSpalten.GetUpperBound(0) - 1)
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
    Private Sub Read_WEL()

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

        'temoräres Array für XWerte
        Dim tmpXWerte(AnzZeil - WELHeaderLen - 1) As DateTime

        'Auf Anfang setzen und einlesen
        '------------------------------
        FiStr.Seek(0, SeekOrigin.Begin)

        For i = 0 To AnzZeil - 1
            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
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
            Else
                'Spalten mit fester Breite
                Zeile = StrRead.ReadLine.ToString()
                If (i >= WELHeaderLen) Then
                    'Erste Spalte: Datum_Zeit
                    tmpXWerte(i - WELHeaderLen) = New System.DateTime(Zeile.Substring(6 + SpaltenOffset, 4), Zeile.Substring(3 + SpaltenOffset, 2), Zeile.Substring(0 + SpaltenOffset, 2), Zeile.Substring(11 + SpaltenOffset, 2), Zeile.Substring(14 + SpaltenOffset, 2), 0, New System.Globalization.GregorianCalendar())
                    'Restliche Spalten: Werte
                    n = 0
                    For j = 0 To Me.YSpalten.GetUpperBound(0)
                        If (isSelected(Me.YSpalten(j))) Then
                            Me.Zeitreihen(n).YWerte(i - WELHeaderLen) = Convert.ToDouble(Zeile.Substring(((j + 1) * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, Zeile.Substring(((j + 1) * Me.Spaltenbreite) + SpaltenOffset).Length)))
                            n += 1
                        End If
                    Next
                End If
            End If

        Next

        StrRead.Close()
        FiStr.Close()

        'XWerte an alle Zeitreihen übergeben
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i).XWerte = tmpXWerte
        Next

    End Sub

    'Überprüfung, ob eine Spalte ausgewählt ist
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

    'OK Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Einstellungen übernehmen
        '------------------------
        'Trennzeichen
        Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem

        'Ausgewählte Spalten
        Dim i As Integer
        If (Me.ListBox_YSpalten.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Y-Spalte auswählen!", MsgBoxStyle.Exclamation, "Fehler")
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

    'Wenn Spaltenart geändert wird, Spalten neu auslesen
    '***************************************************
    Private Sub RadioButton_Spalten_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton_Spaltenbreite.CheckedChanged

        If (Me.RadioButton_Zeichengetrennt.Checked) Then
            Me.Zeichengetrennt = True
        Else
            Me.Zeichengetrennt = False
        End If

        Call Me.SpaltenAuslesen()

    End Sub

    'Wenn Trennzeichen geändert wird, Spalten neu auslesen
    '*****************************************************
    Private Sub ComboBox_Trennzeichen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Trennzeichen.SelectedIndexChanged

        Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
        Call Me.SpaltenAuslesen()

    End Sub

    'Wenn Spaltenbreite geändert wird, Spalten neu auslesen
    '******************************************************
    Private Sub TextBox_Spaltenbreite_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Spaltenbreite.TextChanged

        If (Convert.ToInt16(Me.TextBox_Spaltenbreite.Text) < 1) Then
            MsgBox("Bitte eine Zahl größer 0 für die Spaltenbreite angeben!", MsgBoxStyle.Exclamation, "Fehler")
            Me.TextBox_Spaltenbreite.Focus()
            Exit Sub
        End If

        Me.Spaltenbreite = Convert.ToInt16(Me.TextBox_Spaltenbreite.Text)

        Call Me.SpaltenAuslesen()

    End Sub

#End Region 'Methoden

End Class