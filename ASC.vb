Imports System.IO
Imports System.Globalization

Public Class ASC


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
    Private _trennzeichen As Zeichen = leerzeichen      'Spaltentrennzeichen (standardmäßig Leerzeichen)
    Private _XSpalte As String                          'X-Spalte
    Private _Yspalten() As String                       'Array der vorhandenen Y-Spaltennamen
    Private _spaltenSel() As String                     'Array der ausgewählten Y-Spaltennamen

    Public Zeitreihen() As Zeitreihe

    Private Const WELHeaderLen As Integer = 4           'Die ersten 3 Zeilen der WEL-Datei gehören zum Header
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

    Public Property Trennzeichen() As Zeichen
        Get
            Return _trennzeichen
        End Get
        Set(ByVal value As Zeichen)
            _trennzeichen = value
        End Set
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
            Call Me.Read_ASC()
        End If

    End Sub

    'Form laden
    '**********
    Private Sub ASC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Standardeinstellungen setzen
        Me.Trennzeichen = Me.leerzeichen

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

        alleSpalten = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)

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

    'ASC-Datei einlesen
    '******************
    Public Function Read_ASC() As Zeitreihe()

        Dim AnzZeil As Integer = 0
        Dim AnzZeilLeer As Integer
        Dim AnzWerte As Integer
        Dim i, j, n As Integer
        Dim Zeile As String
        Dim Werte() As String = {}
        Dim Ereignisende As Boolean
        Dim dt As TimeSpan

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'SMUSI gibt immer Punkt als Dezimalseperator aus, convert.todouble(String, IFormatProvider) erlaubt eeine
        'von den Ländereinstellungen unabhängige Konvertierung
        Dim provider As NumberFormatInfo = New NumberFormatInfo()

        provider.NumberDecimalSeparator = "."
        provider.NumberGroupSeparator = ","
        provider.NumberGroupSizes = New Integer() {3}

        dt = New TimeSpan(0, 5, 0)

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrRead.ReadLine.ToString
            AnzZeil += 1
            If Trim(Zeile).Length = 0 Then
                AnzZeilLeer += 1
            End If
        Loop Until StrRead.Peek() = -1

        'Anzahl Zeitreihen bestimmen und Array entsprechend dimensionieren
        ReDim Me.Zeitreihen(Me.SpaltenSel.GetUpperBound(0))

        'Zeitreihen mit vorläufiger Zeilennanzahl Dimensionieren
        If AnzZeilLeer > 1 Then
            AnzWerte = AnzZeil - WELHeaderLen + AnzZeilLeer
        Else
            AnzWerte = AnzZeil - WELHeaderLen
        End If
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i) = New Zeitreihe(SpaltenSel(i))
            Me.Zeitreihen(i).Length = AnzWerte
        Next

        'temoräres Array für XWerte
        Dim tmpXWerte(AnzWerte - 1) As DateTime
        'Auf Anfang setzen, Kopfzeile überspringen und einlesen
        '------------------------------
        'Auf Anfang setzten
        FiStr.Seek(0, SeekOrigin.Begin)
        'Kopfzeilen Überspringen
        For i = 1 To WELHeaderLen
            StrRead.ReadLine()
        Next
        'Einlesen
        i = 0
        Ereignisende = True
        Do While Not StrRead.EndOfStream
            'Komplette Zeile einlesen
            Werte = StrRead.ReadLine.ToString.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            'Falls eine leere Zeile eingelesen wurde
            If Werte.Length = 0 Then
                'Leere Zeile befindet sich am Ende eines Ereignisses
                If Ereignisende Then
                    n = 0
                    'Letztes Datum um fünf Minuten hochzählen
                    tmpXWerte(i) = tmpXWerte(i - 1).AddMinutes(5)
                    'Alle Werte auf 0.0 setzen
                    For j = 0 To Me.YSpalten.GetUpperBound(0)
                        If (isSelected(Me.YSpalten(j))) Then
                            Me.Zeitreihen(n).YWerte(i) = 0.0
                            n += 1
                        End If
                    Next
                End If
                '
                Ereignisende = False
            Else
                'Eine Zeile mit Werten wird eingelesen
                'Erste Spalte: Datum_Zeit
                tmpXWerte(i) = New System.DateTime(Werte(0).Substring(7, 4), Werte(0).Substring(4, 2), Werte(0).Substring(1, 2), Werte(1).Substring(0, 2), Werte(1).Substring(3, 2), 0, New System.Globalization.GregorianCalendar())
                If Not Ereignisende Then
                    If Not (tmpXWerte(i - 1).AddMinutes(5) = tmpXWerte(i)) Then
                        tmpXWerte(i + 1) = tmpXWerte(i)
                        tmpXWerte(i) = tmpXWerte(i + 1).Subtract(dt)
                        n = 0
                        For j = 0 To Me.YSpalten.GetUpperBound(0)
                            If (isSelected(Me.YSpalten(j))) Then
                                Me.Zeitreihen(n).YWerte(i) = 0.0
                                Me.Zeitreihen(n).YWerte(i + 1) = Convert.ToDouble(Werte(j + 2), provider)
                                n += 1
                            End If
                        Next
                        i += 1
                    Else
                        'Restliche Spalten: Werte
                        n = 0
                        For j = 0 To Me.YSpalten.GetUpperBound(0)
                            If (isSelected(Me.YSpalten(j))) Then
                                Me.Zeitreihen(n).YWerte(i) = Convert.ToDouble(Werte(j + 2), provider)
                                n += 1
                            End If
                        Next
                    End If
                    Ereignisende = True
                Else
                    'Restliche Spalten: Werte
                    n = 0
                    For j = 0 To Me.YSpalten.GetUpperBound(0)
                        If (isSelected(Me.YSpalten(j))) Then
                            Me.Zeitreihen(n).YWerte(i) = Convert.ToDouble(Werte(j + 2), provider)
                            n += 1
                        End If
                    Next
                End If
            End If
            i += 1
        Loop
        AnzZeil = i
        ReDim Preserve tmpXWerte(AnzZeil - 1)
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i).Length = AnzZeil
        Next
        'Datei schließen
        StrRead.Close()
        FiStr.Close()

        'XWerte an alle Zeitreihen übergeben
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i).XWerte = tmpXWerte
        Next

        Return Me.Zeitreihen

    End Function

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
        Call Me.Read_ASC()

    End Sub

    ''Wenn Spaltenart geändert wird, Spalten neu auslesen
    ''***************************************************
    'Private Sub RadioButton_Spalten_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton_Spaltenbreite.CheckedChanged

    '    If (Me.RadioButton_Zeichengetrennt.Checked) Then
    '        Me.Zeichengetrennt = True
    '    Else
    '        Me.Zeichengetrennt = False
    '    End If

    '    Call Me.SpaltenAuslesen()

    'End Sub

    'Wenn Trennzeichen geändert wird, Spalten neu auslesen
    '*****************************************************
    'Private Sub ComboBox_Trennzeichen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Trennzeichen.SelectedIndexChanged

    '    Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
    '    Call Me.SpaltenAuslesen()

    'End Sub

    'Wenn Spaltenbreite geändert wird, Spalten neu auslesen
    '******************************************************
    'Private Sub TextBox_Spaltenbreite_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Spaltenbreite.TextChanged

    '    If (Convert.ToInt16(Me.TextBox_Spaltenbreite.Text) < 1) Then
    '        MsgBox("Bitte eine Zahl größer 0 für die Spaltenbreite angeben!", MsgBoxStyle.Exclamation, "Fehler")
    '        Me.TextBox_Spaltenbreite.Focus()
    '        Exit Sub
    '    End If

    '    Me.Spaltenbreite = Convert.ToInt16(Me.TextBox_Spaltenbreite.Text)

    '    Call Me.SpaltenAuslesen()

    'End Sub

#End Region 'Methoden


End Class