Imports System.IO

Public Class WEL

#Region "Eigenschaften"

    'Eigenschaften
    '#############
    Private _file As String                             'Pfad zur Datei
    Private _trennzeichen As Zeichen                    'Spaltentrennzeichen
    Private _dezimaltrennzeichen As Zeichen             'Dezimaltrennzeichen
    Private _spalten() As String                        'Array der Spaltennamen

    Public XWerte() As DateTime
    Public Structure Struct_YWerte
        Public YWerte() As Double
    End Structure
    Public AllYWerte() As Struct_YWerte

    Private Const WELHeaderLen As Integer = 3           'Die ersten 3 Zeilen der WEL-Datei gehören zum Header

    Private semikolon As Zeichen = New Zeichen(";")
    Private komma As Zeichen = New Zeichen(",")
    Private punkt As Zeichen = New Zeichen(".")
    Private leerzeichen As Zeichen = New Zeichen(" ")
    Private tab As Zeichen = New Zeichen(Chr(9))

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

    Public Property Spalten() As String()
        Get
            Return _spalten
        End Get
        Set(ByVal value As String())
            _spalten = value
        End Set
    End Property

#End Region 'Properties

#Region "Methoden"

    'Methoden
    '########

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

        'Combobox Dezimaltrennzeichen initialisieren
        Me.ComboBox_Dezimaltrennzeichen.BeginUpdate()
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.komma)
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.punkt)
        Me.ComboBox_Dezimaltrennzeichen.EndUpdate()

        'Standard-Trennzeichen setzen
        Me.Trennzeichen = Me.semikolon

        'Standard-Dezimaltrennzeichen setzen
        Me.Dezimaltrennzeichen = Me.punkt

        'Datei als Vorschau anzeigen
        Me.RichTextBox_Vorschau.LoadFile(Me.File, RichTextBoxStreamType.PlainText)

        'Spalten auslesen
        SpaltenAuslesen()

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

        'Spaltennamen übergeben
        Me.Spalten() = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
        For i = 0 To Spalten.GetUpperBound(0)
            Spalten(i) = Spalten(i).Trim()
        Next

        'Listbox aktualisieren
        Me.ListBox_Spalten.Items.Clear()
        Me.ListBox_Spalten.Items.AddRange(Me.Spalten)

    End Sub

    'WEL-Datei einlesen
    '******************
    Public Sub Read_WEL()

        Dim AnzZeil As Integer = 0
        Dim i, j As Integer
        Dim Zeile As String
        Dim Werte() As String = {}

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrRead.ReadLine.ToString
            AnzZeil += 1
        Loop Until StrRead.Peek() = -1

        ReDim XWerte(AnzZeil - WELHeaderLen - 1)
        ReDim AllYWerte(Me.Spalten.GetUpperBound(0) - 1)
        For i = 0 To AllYWerte.GetUpperBound(0)
            ReDim AllYWerte(i).YWerte(AnzZeil - WELHeaderLen - 1)
        Next

        'Auf Anfang setzen und einlesen
        '------------------------------
        FiStr.Seek(0, SeekOrigin.Begin)

        For i = 0 To AnzZeil - 1
            Werte = StrRead.ReadLine.ToString.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            If (i >= WELHeaderLen) Then
                'Erste Spalte: Datum_Zeit
                XWerte(i - WELHeaderLen) = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(0).Substring(11, 2), Werte(0).Substring(14, 2), 0, New System.Globalization.GregorianCalendar())
                'Restliche Spalten: Werte
                For j = 1 To Me.Spalten.GetUpperBound(0)
                    AllYWerte(j - 1).YWerte(i - WELHeaderLen) = Convert.ToDouble(Werte(j))
                Next
            End If
        Next

        StrRead.Close()
        FiStr.Close()

    End Sub

    'OK Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Einstellungen übernehmen
        '------------------------
        'Trenn- und Dezimaltrennzeichen
        Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
        Me.Dezimaltrennzeichen = Me.ComboBox_Dezimaltrennzeichen.SelectedItem

        'TODO: Ausgewählte Spalten übernehmen

        'WEL-Datei einlesen
        Call Me.Read_WEL()

    End Sub

    'Wenn Trennzeichen geändert wird, Spalten neu auslesen
    '*****************************************************
    Private Sub ComboBox_Trennzeichen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Trennzeichen.SelectedIndexChanged
        Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
        Call Me.SpaltenAuslesen()
    End Sub

#End Region 'Methoden

End Class