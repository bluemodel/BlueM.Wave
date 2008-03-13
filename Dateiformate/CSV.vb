Imports System.IO

Public Class CSV
    Inherits Dateiformat

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)
        MyBase.New(FileName)
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
            For i = 1 To Me.iZeileDaten
                Zeile = StrReadSync.ReadLine.ToString
                If (i = Me.iZeileÜberschriften) Then ZeileSpalten = Zeile
                If (i = Me.iZeileEinheiten) Then ZeileEinheiten = Zeile
            Next

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'Spaltennamen auslesen
            '---------------------
            Dim Namen() As String
            Dim Einheiten() As String

            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            Else
                'Spalten mit fester Breite
                Dim anzSpalten As Integer = Math.Ceiling(ZeileSpalten.Length / Me.Spaltenbreite)
                ReDim Namen(anzSpalten - 1)
                ReDim Einheiten(anzSpalten - 1)
                For i = 0 To anzSpalten - 1
                    Namen(i) = ZeileSpalten.Substring(i * Me.Spaltenbreite, Math.Min(Me.Spaltenbreite, ZeileSpalten.Substring(i * Me.Spaltenbreite).Length))
                    Einheiten(i) = ZeileEinheiten.Substring(i * Me.Spaltenbreite, Math.Min(Me.Spaltenbreite, ZeileSpalten.Substring(i * Me.Spaltenbreite).Length))
                Next
            End If

            'Sicherstellen, dass es so viele Einheiten wie Spalten gibt:
            ReDim Preserve Einheiten(Namen.GetUpperBound(0))
            For i = 0 To Einheiten.GetUpperBound(0)
                If (IsNothing(Einheiten(i))) Then Einheiten(i) = "-"
            Next

            'Leerzeichen entfernen
            For i = 0 To Namen.GetUpperBound(0)
                Namen(i) = Namen(i).Trim()
                If (Namen(i).Length = 0) Then Namen(i) = "{" & i.ToString() & "}"
                'Einheiten anhängen
                If (Me.UseEinheiten) Then
                    Namen(i) &= " [" & Einheiten(i).Trim() & "]"
                End If
            Next

            'X-Spalte übernehmen
            Me.XSpalte = Namen(0)

            'Y-Spalten übernehmen
            ReDim Me.YSpalten(Namen.GetUpperBound(0) - 1)
            Array.Copy(Namen, 1, Me.YSpalten, 0, Namen.Length - 1)

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'CSV-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim AnzZeil As Integer = 0
        Dim i, j, n As Integer
        Dim Zeile As String
        Dim Werte() As String = {}

        Try

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Anzahl der Zeilen feststellen
            Do
                Zeile = StrReadSync.ReadLine.ToString
                AnzZeil += 1
            Loop Until StrReadSync.Peek() = -1

            'Anzahl Zeitreihen bestimmen
            ReDim Me.Zeitreihen(Me.SpaltenSel.GetUpperBound(0))

            'Zeitreihen redimensionieren
            For i = 0 To Me.Zeitreihen.GetUpperBound(0)
                Me.Zeitreihen(i) = New Zeitreihe(SpaltenSel(i))
                Me.Zeitreihen(i).Length = AnzZeil - Me.nZeilenHeader
            Next

            'temoräres Array für XWerte
            Dim tmpXWerte(AnzZeil - Me.nZeilenHeader - 1) As DateTime

            'Auf Anfang setzen und einlesen
            '------------------------------
            FiStr.Seek(0, SeekOrigin.Begin)

            For i = 0 To AnzZeil - 1
                If (Me.Zeichengetrennt) Then
                    'Zeichengetrennt
                    Werte = StrReadSync.ReadLine.ToString.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                    If (i >= Me.nZeilenHeader) Then
                        'Erste Spalte: Datum_Zeit
                        tmpXWerte(i - Me.nZeilenHeader) = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(0).Substring(11, 2), Werte(0).Substring(14, 2), 0, New System.Globalization.GregorianCalendar())
                        'Restliche Spalten: Werte
                        n = 0
                        For j = 0 To Me.YSpalten.GetUpperBound(0)
                            If (isSelected(Me.YSpalten(j))) Then
                                Me.Zeitreihen(n).YWerte(i - Me.nZeilenHeader) = Convert.ToDouble(Werte(j + 1))
                                n += 1
                            End If
                        Next
                    End If
                Else
                    'Spalten mit fester Breite
                    Zeile = StrReadSync.ReadLine.ToString()
                    If (i >= Me.nZeilenHeader) Then
                        'Erste Spalte: Datum_Zeit
                        tmpXWerte(i - Me.nZeilenHeader) = New System.DateTime(Zeile.Substring(6, 4), Zeile.Substring(3, 2), Zeile.Substring(0, 2), Zeile.Substring(11, 2), Zeile.Substring(14, 2), 0, New System.Globalization.GregorianCalendar())
                        'Restliche Spalten: Werte
                        n = 0
                        For j = 0 To Me.YSpalten.GetUpperBound(0)
                            If (isSelected(Me.YSpalten(j))) Then
                                Me.Zeitreihen(n).YWerte(i - Me.nZeilenHeader) = Convert.ToDouble(Zeile.Substring((j + 1) * Me.Spaltenbreite, Math.Min(Me.Spaltenbreite, Zeile.Substring((j + 1) * Me.Spaltenbreite).Length)))
                                n += 1
                            End If
                        Next
                    End If
                End If

            Next

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'XWerte an alle Zeitreihen übergeben
            For i = 0 To Me.Zeitreihen.GetUpperBound(0)
                Me.Zeitreihen(i).XWerte = tmpXWerte
            Next

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

#End Region 'Methoden

End Class