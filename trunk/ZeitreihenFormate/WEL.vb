Imports System.IO

''' <summary>
''' Klasse für das WEL-Dateiformat
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/WEL-Format</remarks>
Public Class WEL
    Inherits Dateiformat

#Region "Eigenschaften"

    'Eigenschaften
    '#############


#End Region

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"
    
    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        SpaltenOffset = 1
        
        'Voreinstellungen
        Me.iZeileUeberschriften = 2
        Me.UseEinheiten = True
        Me.iZeileEinheiten = 3
        Me.iZeileDaten = 4
        Me.Zeichengetrennt = True
        Me.Trennzeichen = Me.semikolon
        Me.Dezimaltrennzeichen = Me.punkt

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If

        
    End Sub

    ''' <summary>
    ''' Spalten auslesen
    ''' </summary>
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Spaltenüberschriften auslesen
            For i = 1 To Me.iZeileDaten
                Zeile = StrReadSync.ReadLine.ToString()
                If (i = Me.iZeileUeberschriften) Then ZeileSpalten = Zeile
                If (i = Me.iZeileEinheiten) Then ZeileEinheiten = Zeile
            Next

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'Spalteninformationen auslesen
            '-----------------------------
            Dim anzSpalten As Integer
            Dim Namen() As String
            Dim Einheiten() As String

            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                anzSpalten = Namen.Length
                ReDim Me.Spalten(anzSpalten - 1)
                For i = 0 To anzSpalten - 1
                    Me.Spalten(i).Name = Namen(i).Trim()
                    Me.Spalten(i).Einheit = Einheiten(i).Trim()
                    Me.Spalten(i).Index = i
                Next
            Else
                'Spalten mit fester Breite
                anzSpalten = Math.Ceiling(ZeileSpalten.Length / Me.Spaltenbreite)
                ReDim Me.Spalten(anzSpalten - 1)
                For i = 0 To anzSpalten - 1
                    Me.Spalten(i).Name = ZeileSpalten.Substring((i * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, ZeileSpalten.Substring((i * Me.Spaltenbreite) + SpaltenOffset).Length)).Trim()
                    Me.Spalten(i).Einheit = ZeileEinheiten.Substring((i * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, ZeileSpalten.Substring((i * Me.Spaltenbreite) + SpaltenOffset).Length)).Trim()
                    Me.Spalten(i).Index = i
                Next
            End If

        Catch ex As Exception
            MsgBox("Konnte die Datei '" & Path.GetFileName(Me.File) & "' nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    ''' <summary>
    ''' WEL-Datei einlesen
    ''' </summary>
    Public Overrides Sub Read_File()

        Dim iZeile, i As Integer
        Dim Zeile As String
        Dim Werte() As String
        Dim datum As DateTime

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Anzahl Zeitreihen bestimmen
        ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)

        'Zeitreihen instanzieren
        For i = 0 To Me.SpaltenSel.Length - 1
            Me.Zeitreihen(i) = New Zeitreihe(Me.SpaltenSel(i).Name)
        Next

        'Einheiten?
        If (Me.UseEinheiten) Then
            'Alle ausgewählten Spalten durchlaufen
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i).Einheit = Me.SpaltenSel(i).Einheit
            Next
        End If

        'Einlesen
        '--------

        'Header
        For iZeile = 1 To Me.nZeilenHeader
            Zeile = StrReadSync.ReadLine.ToString()
        Next

        'Daten
        Do
            Zeile = StrReadSync.ReadLine.ToString()

            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                '---------------
                Werte = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                'Erste Spalte: Datum_Zeit
                datum = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(0).Substring(11, 2), Werte(0).Substring(14, 2), 0, New System.Globalization.GregorianCalendar())
                'Restliche Spalten: Werte
                'Alle ausgewählten Spalten durchlaufen
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index)))
                Next
            Else
                'Spalten mit fester Breite
                '-------------------------
                'Erste Spalte: Datum_Zeit
                datum = New System.DateTime(Zeile.Substring(6 + SpaltenOffset, 4), Zeile.Substring(3 + SpaltenOffset, 2), Zeile.Substring(0 + SpaltenOffset, 2), Zeile.Substring(11 + SpaltenOffset, 2), Zeile.Substring(14 + SpaltenOffset, 2), 0, New System.Globalization.GregorianCalendar())
                'Restliche Spalten: Werte
                'Alle ausgewählten Spalten durchlaufen
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).AddNode(datum, StringToDouble(Zeile.Substring((Me.SpaltenSel(i).Index * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, Zeile.Substring((Me.SpaltenSel(i).Index * Me.Spaltenbreite) + SpaltenOffset).Length))))
                Next
            End If

        Loop Until StrReadSync.Peek() = -1

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um eine WEL-Datei für BlueM handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        '2 Zeilen einlesen
        Zeile = StrRead.ReadLine.ToString()
        Zeile = Trim(zeile)
        
        StrRead.Close()
        FiStr.Close()

        If (Zeile.StartsWith("*WEL")) Then
            'Es ist eine WEL-Reihe für BlueM
            Return True
        Else
            Return False
        End If

    End Function

#End Region 'Methoden

End Class