Imports System.IO

''' <summary>
''' Klasse für generisches Textformat
''' </summary>
Public Class GISMO_WEL
    Inherits Dateiformat

    Public Const DatumsformatCSV As String = "dd.MM.yyyy HH:mm"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, ByVal Zeichengetrennt As Boolean)
        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileUeberschriften = 13
        Me.UseEinheiten = True
        Me.iZeileEinheiten = 14
        Me.iZeileDaten = 17
        Me.Zeichengetrennt = Zeichengetrennt
        Me.Trennzeichen = Me.semikolon
        Me.Dezimaltrennzeichen = Me.punkt
        Me.Spaltenbreite = 9

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Spaltenüberschriften auslesen
            For i = 1 To math.Max(Me.iZeileDaten, Me.iZeileUeberschriften + 1)
                Zeile = StrReadSync.ReadLine.ToString
                If (i = Me.iZeileUeberschriften) Then ZeileSpalten = Zeile
                If (i = Me.iZeileEinheiten) Then ZeileEinheiten = Zeile
            Next

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'Spaltennamen auslesen
            '---------------------
            Dim anzSpalten As Integer
            Dim Namen() As String
            Dim Einheiten() As String

            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character})
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character})
                anzSpalten = Namen.Length
            Else
                'Spalten mit fester Breite
                Throw New Exception("Import of GISMO-ASCII files (*.asc) with fixed width not yet implemented (due to a GISMO bug)!")
            End If

            'Spalten abspeichern
            ReDim Me.Spalten(anzSpalten - 2)
            For i = 0 To Namen.Length - 2
                Me.Spalten(i).Name = Namen(i + 1).Trim()
                Me.Spalten(i).Index = i
            Next

            For i = 0 To Einheiten.Length - 2
                Me.Spalten(i).Einheit = Einheiten(i + 1).Trim()
            Next

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'GISMO-CSV-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i As Integer
        Dim Zeile As String
        Dim ok As Boolean
        Dim datum As DateTime
        Dim Werte(), Werte_temp() As String

        Try

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Anzahl Zeitreihen bestimmen
            ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)
            'Zeitreihen instanzieren
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i) = New Zeitreihe(Me.SpaltenSel(i).Name)
            Next

            'Einheiten übergeben
            If (Me.UseEinheiten) Then
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).Einheit = Me.SpaltenSel(i).Einheit
                Next
            End If

            'Einlesen
            '--------

            'Header
            For i = 0 To Me.nZeilenHeader - 1
                StrReadSync.ReadLine()
            Next

            'Daten
            Do
                Zeile = StrReadSync.ReadLine.ToString()

                If (Me.Zeichengetrennt) Then

                    'Zeichengetrennt
                    '---------------
                    Werte_temp = Zeile.Split(New Char() {Me.Trennzeichen.Character})

                    If (Werte_temp.Length > 0 And Zeile.Trim.Length > 1) Then
                        ' First two characters of line "*;" need to be excluded as they are no relevant data
                        ' Probably a GISMO-export bug
                        ReDim Werte(Werte_temp.Length - 2)
                        For i = 0 To (Werte_temp.Length - 2)
                            Werte(i) = Werte_temp(i + 1)
                        Next

                        'Erste Spalte: Datum_Zeit
                        ok = DateTime.TryParseExact(Werte(Me.XSpalte), DatumsformatCSV, Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                        If (Not ok) Then
                            Throw New Exception("Kann das Datumsformat '" & Werte(Me.XSpalte) & "' nicht erkennen! " & eol & "Sollte in der Form '" & DatumsformatCSV & "' vorliegen!")
                        End If
                        'Restliche Spalten: Werte
                        For i = 0 To Me.SpaltenSel.Length - 1
                            Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index)))
                        Next
                    End If

                Else
                    'Spalten mit fester Breite
                    '-------------------------
                    Throw New Exception("Import of GISMO-ASCII files (*.asc) with fixed width not yet implemented (due to a GISMO bug)!")
                End If

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub


    ''' <summary>
    ''' Checks, if the file is a GISMO-WEL file
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""


        '1. read lien
        Zeile = StrRead.ReadLine.ToString()
        Zeile = Trim(zeile)

        If (Zeile.StartsWith("*CSV")) Then
            'It's in csv format
        ElseIf (Zeile.StartsWith("*WEL")) Then
            'it's in asc format
        End If

        Zeile = StrRead.ReadLine.ToString()
        Zeile = Trim(Zeile)
        Dim SearchForThis As String = "GISMO"
        If Zeile.IndexOf(SearchForThis) > 0 Then
            '"GISMO" is within the second line --> it's a GISMO-file
            Return True
        Else
            Return False
        End If

        StrRead.Close()
        FiStr.Close()

    End Function


#End Region 'Methoden

End Class