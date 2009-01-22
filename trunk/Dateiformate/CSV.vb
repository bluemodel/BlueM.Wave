Imports System.IO

''' <summary>
''' Klasse für generisches Textformat
''' </summary>
Public Class CSV
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
    Public Sub New(ByVal FileName As String)
        MyBase.New(FileName)
        DatumsspalteSetzen = true
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
                If (i = Me.iZeileUeberschriften) Then ZeileSpalten = Zeile
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
                Einheiten(i) = Einheiten(i).Trim()
                Namen(i) = Namen(i).Trim()
                If (Namen(i).Length = 0) Then Namen(i) = "{" & i.ToString() & "}"
            Next

            'X-Spalte übernehmen
            Me.XSpalte = Namen(me.Datumsspalte)

            'Y-Spalten übernehmen
            ReDim Me.YSpalten(Namen.GetUpperBound(0) - 1)
            ReDim Me.Einheiten(Namen.GetUpperBound(0) - 1)
            Array.Copy(Namen, 1, Me.YSpalten, 0, Namen.Length - 1)
            Array.Copy(Einheiten, 1, Me.Einheiten, 0, Namen.Length - 1)

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
        Dim ok As Boolean
        Dim datum As DateTime
        Dim Werte() As String

        Try

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Anzahl Zeitreihen bestimmen
            ReDim Me.Zeitreihen(Me.SpaltenSel.GetUpperBound(0))

            'Zeitreihen redimensionieren
            For i = 0 To Me.Zeitreihen.GetUpperBound(0)
                Me.Zeitreihen(i) = New Zeitreihe(SpaltenSel(i))
            Next

            'Einheiten übergeben
            If (Me.UseEinheiten) Then
                n = 0
                For j = 0 To Me.YSpalten.GetUpperBound(0)
                    If (isSelected(Me.YSpalten(j))) Then
                        Me.Zeitreihen(n).Einheit = Me.Einheiten(j)
                        n += 1
                    End If
                Next
            End If

            'Kopfzeilen überspringen
            For i = 0 To Me.nZeilenHeader - 1
                StrReadSync.ReadLine()
            Next

            'Einlesen
            '--------
            Do
                Zeile = StrReadSync.ReadLine.ToString()

                If (Me.Zeichengetrennt) Then

                    'Zeichengetrennt
                    '---------------
                    Werte = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)

                    If (Werte.Length > 0) Then
                        'Erste Spalte: Datum_Zeit
                        ok = DateTime.TryParseExact(Werte(Me.Datumsspalte), DatumsformatCSV, Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                        If (Not ok) Then
                            Throw New Exception("Kann das Datumsformat '" & Werte(Me.Datumsspalte) & "' nicht erkennen! " & eol & "Sollte in der Form '" & DatumsformatCSV & "' vorliegen!")
                        End If
                        'Restliche Spalten: Werte
                        n = 0
                        For j = 0 To Me.YSpalten.GetUpperBound(0)
                            If (isSelected(Me.YSpalten(j))) Then
                                Me.Zeitreihen(n).AddNode(datum, StringToDouble(Werte(j + 1)))
                                n += 1
                            End If
                        Next
                    End If

                Else
                    'Spalten mit fester Breite
                    '-------------------------
                    'Erste Spalte: Datum_Zeit
                    datum = New System.DateTime(Zeile.Substring(Me.Datumsspalte + 6 + SpaltenOffset, 4), Zeile.Substring(Me.Datumsspalte + 3 + SpaltenOffset, 2), Zeile.Substring(Me.Datumsspalte + 0 + SpaltenOffset, 2), Zeile.Substring(Me.Datumsspalte + 11 + SpaltenOffset, 2), Zeile.Substring(Me.Datumsspalte + 14 + SpaltenOffset, 2), 0, New System.Globalization.GregorianCalendar())
                    'Restliche Spalten: Werte
                    n = 0
                    For j = 0 To Me.YSpalten.GetUpperBound(0)
                        If (isSelected(Me.YSpalten(j))) Then
                            Me.Zeitreihen(n).AddNode(datum, StringToDouble(Zeile.Substring((j + 1) * Me.Spaltenbreite, Math.Min(Me.Spaltenbreite, Zeile.Substring((j + 1) * Me.Spaltenbreite).Length))))
                            n += 1
                        End If
                    Next
                End If

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

#End Region 'Methoden

End Class