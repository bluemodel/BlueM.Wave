Imports System.IO
Imports System.Globalization

''' <summary>
''' Klasse f�r ASC-Dateiformat (SMUSI)
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
        Me.XSpalte = 0

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Call Me.selectAllSpalten()
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

        'Datei �ffnen
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

        'Spalten�berschriften
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
        Dim anzSpalten As Integer
        Dim Namen() As String
        Dim Einheiten() As String

        Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
        Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)

        'Bei ASC hat die Datumsspalte (manchmal) keine Einheit
        If (Einheiten.Length = Namen.Length - 1) Then
            'Einheit f�r Datumsspalte erg�nzen
            Array.Reverse(Einheiten)
            ReDim Preserve Einheiten(Einheiten.Length)
            Array.Reverse(Einheiten)
            Einheiten(0) = "-"
        End If

        anzSpalten = Namen.Length

        ReDim Me.Spalten(anzSpalten - 1)
        For i = 0 To anzSpalten - 1
            Me.Spalten(i).Name = Namen(i).Trim()
            Me.Spalten(i).Einheit = Einheiten(i).Trim()
            Me.Spalten(i).Index = i
        Next

    End Sub

    'ASC-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i As Integer
        Dim Zeile As String
        Dim Werte() As String
        Dim datum, datumLast As DateTime
        Dim Ereignisende As Boolean
        Dim dt As TimeSpan

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        dt = New TimeSpan(0, 5, 0)

        'Anzahl Zeitreihen
        ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)

        'Zeitreihen instanzieren
        For i = 0 To Me.SpaltenSel.Length - 1
            Me.Zeitreihen(i) = New Zeitreihe(Me.SpaltenSel(i).Name)
        Next

        'Einheiten?
        If (Me.UseEinheiten) Then
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i).Einheit = Me.SpaltenSel(i).Einheit
            Next
        End If

        'Einlesen
        '--------

        'Header
        For i = 1 To Me.nZeilenHeader + 1
            StrReadSync.ReadLine()
        Next

        'Daten
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

                    'Mit St�tzstellen vom Wert 0 L�cke zwischen Ereignissen abschliessen
                    datumLast = Me.Zeitreihen(0).Enddatum
                    If (datum.Subtract(datumLast) > dt) Then 'nur wenn L�cke gr��er als dt ist

                        For i = 0 To Me.SpaltenSel.Length - 1
                            'Eine Null nach dem letzten Datum
                            Me.Zeitreihen(i).AddNode(datumLast.Add(dt), 0.0)
                            If (datum.Subtract(dt) > datumLast.Add(dt)) Then 'nur wenn L�cke damit noch nicht geschlossen ist
                                'Eine Null vor dem aktuellen Datum
                                Me.Zeitreihen(i).AddNode(datum.Subtract(dt), 0.0)
                            End If
                        Next

                        'Log
                        Call Log.AddLogEntry("... Die L�cke zwischen " & datumLast.ToString(Konstanten.Datumsformat) & " und " & datum.ToString(Konstanten.Datumsformat) & " wurde mit 0-Werten abgeschlossen.")
                    End If
                    Ereignisende = False 'zur�cksetzen

                End If

                'eingelesene St�tzstellen hinzuf�gen
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index + 1))) '+1 weil Datum auch ein Leerzeichen enth�lt
                Next

            Else
                'Falls eine leere Zeile eingelesen wurde
                Ereignisende = True

            End If
        Loop

        'Datei schlie�en
        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class