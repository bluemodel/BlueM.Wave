Imports System.IO
Imports System.Globalization

''' <summary>
''' Klasse für ASC-Dateiformat (SMUSI)
''' </summary>
Public Class ASC
    Inherits Dateiformat


#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileUeberschriften = 2
        Me.UseEinheiten = False
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

        Try
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
            Me.XSpalte = Namen(0)

            'Y-Spalten übernehmen
            ReDim Me.YSpalten(Namen.GetUpperBound(0) - 1)
            ReDim Me.Einheiten(Namen.GetUpperBound(0) - 1)
            Array.Copy(Namen, 1, Me.YSpalten, 0, Namen.Length - 1)
            Array.Copy(Einheiten, 1, Me.Einheiten, 0, Namen.Length - 1)

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'ASC-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

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
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        dt = New TimeSpan(0, 5, 0)

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrReadSync.ReadLine.ToString
            AnzZeil += 1
            If Trim(Zeile).Length = 0 Then
                AnzZeilLeer += 1
            End If
        Loop Until StrReadSync.Peek() = -1

        'Anzahl Zeitreihen bestimmen und Array entsprechend dimensionieren
        ReDim Me.Zeitreihen(Me.SpaltenSel.GetUpperBound(0))

        'Zeitreihen mit vorläufiger Zeilennanzahl Dimensionieren
        If AnzZeilLeer > 1 Then
            AnzWerte = AnzZeil - Me.nZeilenHeader + AnzZeilLeer
        Else
            AnzWerte = AnzZeil - Me.nZeilenHeader
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
        For i = 1 To Me.nZeilenHeader + 1
            StrReadSync.ReadLine()
        Next
        'Einlesen
        i = 0
        Ereignisende = True
        Do While Not StrReadSync.Peek() = -1
            'Komplette Zeile einlesen
            Werte = StrReadSync.ReadLine.ToString.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
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
                            Me.Zeitreihen(n).Einheit = Me.Einheiten(j)
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
                                Me.Zeitreihen(n).Einheit = Me.Einheiten(j)
                                Me.Zeitreihen(n).YWerte(i) = 0.0
                                Me.Zeitreihen(n).YWerte(i + 1) = StringToDouble(Werte(j + 2))
                                n += 1
                            End If
                        Next
                        i += 1
                    Else
                        'Restliche Spalten: Werte
                        n = 0
                        For j = 0 To Me.YSpalten.GetUpperBound(0)
                            If (isSelected(Me.YSpalten(j))) Then
                                Me.Zeitreihen(n).Einheit = Me.Einheiten(j)
                                Me.Zeitreihen(n).YWerte(i) = StringToDouble(Werte(j + 2))
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
                            Me.Zeitreihen(n).Einheit = Me.Einheiten(j)
                            Me.Zeitreihen(n).YWerte(i) = StringToDouble(Werte(j + 2))
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
        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

        'XWerte an alle Zeitreihen übergeben
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i).XWerte = tmpXWerte
        Next

    End Sub

#End Region 'Methoden

End Class