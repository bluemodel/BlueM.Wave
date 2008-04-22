Imports System.IO

Public Class ZRE
    Inherits Dateiformat

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileDaten = 5
        Me.UseEinheiten = True

        Call Me.SpaltenAuslesen()

        If (ReadNow) Then
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

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Reihentitel steht in 2. Zeile:
            For i = 0 To 1
                Zeile = StrReadSync.ReadLine.ToString()
            Next

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

            'Spalten übernehmen
            Me.XSpalte = "Datum_Zeit"

            ReDim Me.YSpalten(0)
            Me.YSpalten(0) = Zeile.Substring(0, 15).Trim()

            'Einheit übernehmen
            ReDim Me.Einheiten(0)
            Me.Einheiten(0) = Zeile.Substring(15).Trim()

            Me.SpaltenSel = Me.YSpalten

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'ZRE-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim AnzZeil As Integer = 0
        Dim j As Integer = 0
        Dim Zeile As String

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum As DateTime


        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrReadSync.ReadLine.ToString()
            AnzZeil += 1
        Loop Until StrReadSync.Peek() = -1

        'Zeitreihe redimensionieren
        ReDim Me.Zeitreihen(0)
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0))
        Me.Zeitreihen(0).Einheit = Me.Einheiten(0)
        Me.Zeitreihen(0).Length = AnzZeil - Me.nZeilenHeader

        'Zurück zum Dateianfang und lesen
        FiStr.Seek(0, SeekOrigin.Begin)

        For j = 0 To AnzZeil - 1
            Zeile = StrReadSync.ReadLine.ToString()
            If (j >= Me.nZeilenHeader) Then
                'Datum
                Jahr = Zeile.Substring(0, 4)
                Monat = Zeile.Substring(4, 2)
                Tag = Zeile.Substring(6, 2)
                Stunde = Zeile.Substring(9, 2)
                Minute = Zeile.Substring(12, 2)
                Datum = New System.DateTime(Jahr, Monat, Tag, 0, 0, 0, New System.Globalization.GregorianCalendar())
                ' Wenn Uhrzeit als 24:00 gegeben ist, wird der Tag um eins hochgezählt und die Zeit auf 00:00 gesetzt
                If (Stunde = 24) Then
                    Stunde = 0
                    Datum = Datum.AddDays(1)
                End If
                Datum = Datum.AddHours(Stunde)
                Datum = Datum.AddMinutes(Minute)

                Me.Zeitreihen(0).XWerte(j - Me.nZeilenHeader) = Datum
                'Wert
                Me.Zeitreihen(0).YWerte(j - Me.nZeilenHeader) = Convert.ToDouble(Zeile.Substring(15), FortranProvider)
            End If
        Next

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    'ZRE-Datei exportieren
    '******************
    Public Shared Sub Writefile(ByVal Reihe As Zeitreihe, ByVal File As String)

        Dim strwrite As StreamWriter

        strwrite = New StreamWriter(File)

        strwrite.WriteLine("*ZRE")
        Dim Titel As String
        If Reihe.Title.Length > 15 Then
            Titel = Reihe.Title.Substring(0, 15)
        Else
            Titel = Reihe.Title.PadRight(15)
        End If
        strwrite.WriteLine(Titel & Reihe.Einheit)
        strwrite.WriteLine("0                      0.        0.        0.")
        strwrite.WriteLine(Reihe.XWerte(0).ToString("yyyyMMdd HH:mm") & " " & Reihe.XWerte(Reihe.Length - 1).ToString("yyyyMMdd HH:mm"))
        Dim i As Integer
        For i = 0 To Reihe.Length - 1
            strwrite.Write(Reihe.XWerte(i).ToString("yyyyMMdd HH:mm") & " " & Reihe.YWerte(i).ToString(FortranProvider))
            If i < Reihe.Length - 1 Then
                strwrite.WriteLine()
            End If

        Next
        strwrite.Close()
    End Sub



#End Region 'Methoden

End Class
