Imports System.IO
Imports System.Globalization

Public Class ASC
    Inherits Dateiformat


#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileÜberschriften = 2
        Me.UseEinheiten = False
        Me.iZeileEinheiten = 3
        Me.iZeileDaten = 4
        Me.Zeichengetrennt = True
        Me.Trennzeichen = Me.leerzeichen
        Me.Dezimaltrennzeichen = Me.punkt

        If (ReadNow) Then
            'Datei komplett einlesen
            Call Me.SpaltenAuslesen()
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

        'Spaltenüberschriften
        For i = 1 To Me.iZeileDaten
            Zeile = StrRead.ReadLine.ToString
            If (i = Me.iZeileÜberschriften) Then ZeileSpalten = Zeile
            If (i = Me.iZeileEinheiten) Then ZeileEinheiten = Zeile
        Next

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
            Namen(i) = Namen(i).Trim()
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
        For i = 1 To Me.nZeilenHeader
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
                tmpXWerte(i) = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(1).Substring(0, 2), Werte(1).Substring(3, 2), 0, New System.Globalization.GregorianCalendar())
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

    End Sub

#End Region 'Methoden

End Class