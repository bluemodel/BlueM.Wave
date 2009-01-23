Imports System.IO
Imports System.Globalization

''' <summary>
''' Klasse für ASC-Dateiformat (SMUSI)
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/ASC-Format</remarks>
Public Class ASC
    Inherits Dateiformat

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
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileUeberschriften = 2
        Me.UseEinheiten = True
        Me.iZeileEinheiten = 3
        Me.iZeileDaten = 4
        Me.Zeichengetrennt = True
        Me.Trennzeichen = Me.leerzeichen
        Me.Dezimaltrennzeichen = Me.punkt

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Me.SpaltenSel = Me.YSpalten
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""

        'Datei öffnen
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

        'Spaltenüberschriften
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

        Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
        Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)

        'Sicherstellen, dass es so viele Einheiten wie Spalten gibt:
        ReDim Preserve Einheiten(Namen.GetUpperBound(0))
        For i = 0 To Einheiten.GetUpperBound(0)
            If (IsNothing(Einheiten(i))) Then Einheiten(i) = "-"
        Next

        'Leerzeichen entfernen
        For i = 0 To Namen.GetUpperBound(0)
            Einheiten(i) = Einheiten(i).Trim()
            Namen(i) = Namen(i).Trim()
        Next

        'X-Spalte übernehmen
        Dim iDatumspalte As Integer = 0
        Me.XSpalte = Namen(iDatumspalte)

        'Y-Spalten übernehmen
        ReDim Me.YSpalten(Namen.GetUpperBound(0) - 1)
        ReDim Me.Einheiten(Namen.GetUpperBound(0) - 1)
        Array.Copy(Namen, 1, Me.YSpalten, 0, Namen.Length - 1)
        Array.Copy(Einheiten, 1, Me.Einheiten, 0, Namen.Length - 1)

    End Sub

    'ASC-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i, j, n As Integer
        Dim Zeile As String
        Dim Werte() As String
        Dim datum, datumLast As DateTime
        Dim Ereignisende As Boolean
        Dim dt As TimeSpan

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        dt = New TimeSpan(0, 5, 0)

        'Zeitreihen instanzieren
        ReDim Me.Zeitreihen(Me.SpaltenSel.GetUpperBound(0))
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i) = New Zeitreihe(SpaltenSel(i))
        Next

        'Einheiten
        If (Me.UseEinheiten) Then
            n = 0
            For j = 0 To Me.YSpalten.GetUpperBound(0)
                If (isSelected(Me.YSpalten(j))) Then
                    Me.Zeitreihen(n).Einheit = Me.Einheiten(j)
                    n += 1
                End If
            Next
        End If

        'Einlesen
        '--------

        'Kopfzeilen
        For i = 1 To Me.nZeilenHeader + 1
            StrReadSync.ReadLine()
        Next

        Ereignisende = False
        Do While Not StrReadSync.Peek() = -1

            'Zeile einlesen
            Zeile = StrReadSync.ReadLine()

            '* am Anfang ignorieren
            If (Zeile.StartsWith("*")) Then Zeile = Zeile.Substring(1)

            Werte = Zeile.ToString.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)

            If Werte.Length > 0 Then

                'Eine Zeile mit Werten wird eingelesen
                'Ersten zwei Spalten: Datum_Zeit
                'Dim ok As Boolean
                'ok = DateTime.TryParseExact(Werte(0) & " " & Werte(1), "dd.MM.yyyy HH:mm", Konstanten.Zahlenformat, DateTimeStyles.None, datum)
                datum = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(1).Substring(0, 2), Werte(1).Substring(3, 2), 0, New System.Globalization.GregorianCalendar())

                'Wenn vorher eine leere Zeile eingelesen wurde
                If (Ereignisende) Then

                    'Mit Stützstellen vom Wert 0 Lücke zwischen Ereignissen abschliessen
                    datumLast = Me.Zeitreihen(0).Enddatum
                    If (datum.Subtract(datumLast) > dt) Then 'nur wenn Lücke größer als dt ist
                        n = 0
                        For j = 0 To Me.YSpalten.GetUpperBound(0)
                            If (isSelected(Me.YSpalten(j))) Then
                                'Eine Null nach dem letzten Datum
                                Me.Zeitreihen(n).AddNode(datumLast.Add(dt), 0.0)
                                If (datum.Subtract(dt) > datumLast.Add(dt)) Then 'nur wenn Lücke damit noch nicht geschlossen ist
                                    'Eine Null vor dem aktuellen Datum
                                    Me.Zeitreihen(n).AddNode(datum.Subtract(dt), 0.0)
                                End If
                                n += 1
                            End If
                        Next
                        'Log
                        Call Log.AddLogEntry("... Die Lücke zwischen " & datumLast.ToString(Konstanten.Datumsformat) & " und " & datum.ToString(Konstanten.Datumsformat) & " wurde mit 0-Werten abgeschlossen.")
                    End If
                    Ereignisende = False 'zurücksetzen

                End If

                'eingelesene Stützstelle hinzufügen
                n = 0
                For j = 0 To Me.YSpalten.GetUpperBound(0)
                    If (isSelected(Me.YSpalten(j))) Then
                        Me.Zeitreihen(n).AddNode(datum, StringToDouble(Werte(j + 2)))
                        n += 1
                    End If
                Next



            Else
                'Falls eine leere Zeile eingelesen wurde
                Ereignisende = True

            End If
        Loop

        'Datei schließen
        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class