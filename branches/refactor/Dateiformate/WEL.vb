Imports System.IO

Public Class WEL
    Inherits Dateiformat

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private Const SpaltenOffset As Integer = 1          'Anzahl Zeichen bevor die erste Spalte anfängt (nur bei Spalten mit fester Breite)

#End Region


#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.AnzKopfzeilen = 3
        Me.Zeichengetrennt = True
        Me.Trennzeichen = Me.semikolon
        Me.Dezimaltrennzeichen = Me.punkt

    End Sub


    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer

        'Datei öffnen
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'Spaltenüberschriften auslesen
        Dim Zeile As String = ""
        For i = 1 To Me.AnzKopfzeilen - 1
            Zeile = StrRead.ReadLine.ToString
        Next

        StrRead.Close()
        FiStr.Close()

        'Spaltennamen auslesen
        '---------------------
        Dim alleSpalten() As String

        If (Me.Zeichengetrennt) Then
            'Zeichengetrennt
            alleSpalten = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
        Else
            'Spalten mit fester Breite
            Dim anzSpalten As Integer = Math.Ceiling(Zeile.Length / Me.Spaltenbreite)
            ReDim alleSpalten(anzSpalten - 1)
            For i = 0 To anzSpalten - 1
                alleSpalten(i) = Zeile.Substring((i * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, Zeile.Substring((i * Me.Spaltenbreite) + SpaltenOffset).Length))
            Next
        End If

        'Leerzeichen entfernen
        For i = 0 To alleSpalten.GetUpperBound(0)
            alleSpalten(i) = alleSpalten(i).Trim()
        Next

        'X-Spalte übernehmen
        Me.XSpalte = alleSpalten(0)
        'Y-Spalten übernehmen
        ReDim Me.YSpalten(alleSpalten.GetUpperBound(0) - 1)
        Array.Copy(alleSpalten, 1, Me.YSpalten, 0, alleSpalten.Length - 1)

    End Sub

    'WEL-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim AnzZeil As Integer = 0
        Dim i, j, n As Integer
        Dim Zeile As String
        Dim Werte() As String = {}

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrRead.ReadLine.ToString
            AnzZeil += 1
        Loop Until StrRead.Peek() = -1

        'Anzahl Zeitreihen bestimmen
        ReDim Me.Zeitreihen(Me.SpaltenSel.GetUpperBound(0))

        'Zeitreihen redimensionieren
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i) = New Zeitreihe(SpaltenSel(i))
            Me.Zeitreihen(i).Length = AnzZeil - Me.AnzKopfzeilen
        Next

        'temoräres Array für XWerte
        Dim tmpXWerte(AnzZeil - Me.AnzKopfzeilen - 1) As DateTime

        'Auf Anfang setzen und einlesen
        '------------------------------
        FiStr.Seek(0, SeekOrigin.Begin)

        For i = 0 To AnzZeil - 1
            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                Werte = StrRead.ReadLine.ToString.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                If (i >= Me.AnzKopfzeilen) Then
                    'Erste Spalte: Datum_Zeit
                    tmpXWerte(i - Me.AnzKopfzeilen) = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(0).Substring(11, 2), Werte(0).Substring(14, 2), 0, New System.Globalization.GregorianCalendar())
                    'Restliche Spalten: Werte
                    n = 0
                    For j = 0 To Me.YSpalten.GetUpperBound(0)
                        If (isSelected(Me.YSpalten(j))) Then
                            Me.Zeitreihen(n).YWerte(i - Me.AnzKopfzeilen) = Convert.ToDouble(Werte(j + 1))
                            n += 1
                        End If
                    Next
                End If
            Else
                'Spalten mit fester Breite
                Zeile = StrRead.ReadLine.ToString()
                If (i >= Me.AnzKopfzeilen) Then
                    'Erste Spalte: Datum_Zeit
                    tmpXWerte(i - Me.AnzKopfzeilen) = New System.DateTime(Zeile.Substring(6 + SpaltenOffset, 4), Zeile.Substring(3 + SpaltenOffset, 2), Zeile.Substring(0 + SpaltenOffset, 2), Zeile.Substring(11 + SpaltenOffset, 2), Zeile.Substring(14 + SpaltenOffset, 2), 0, New System.Globalization.GregorianCalendar())
                    'Restliche Spalten: Werte
                    n = 0
                    For j = 0 To Me.YSpalten.GetUpperBound(0)
                        If (isSelected(Me.YSpalten(j))) Then
                            Me.Zeitreihen(n).YWerte(i - Me.AnzKopfzeilen) = Convert.ToDouble(Zeile.Substring(((j + 1) * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, Zeile.Substring(((j + 1) * Me.Spaltenbreite) + SpaltenOffset).Length)))
                            n += 1
                        End If
                    Next
                End If
            End If

        Next

        StrRead.Close()
        FiStr.Close()

        'XWerte an alle Zeitreihen übergeben
        For i = 0 To Me.Zeitreihen.GetUpperBound(0)
            Me.Zeitreihen(i).XWerte = tmpXWerte
        Next

    End Sub

#End Region 'Methoden

End Class