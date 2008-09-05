Imports System.IO

Public Class ZRE
    Inherits Dateiformat

    Const DatumsformatZRE As String = "yyyyMMdd HH:mm"

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileDaten = 5
        Me.UseEinheiten = True

        Call Me.SpaltenAuslesen()

        'ZRE-Dateien immer direkt einlesen
        Me.SpaltenSel = Me.YSpalten
        Call Me.Read_File()

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""

        Try
            'Datei �ffnen
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

            'Spalten �bernehmen
            Me.XSpalte = "Datum_Zeit"

            ReDim Me.YSpalten(0)
            Me.YSpalten(0) = Zeile.Substring(0, 15).Trim()

            'Einheit �bernehmen
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

        Dim j, n As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum As DateTime

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe redimensionieren
        ReDim Me.Zeitreihen(0) 'bei ZRE gibt es nur eine Zeitreihe
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0))
        Me.Zeitreihen(0).Einheit = Me.Einheiten(0)

        j = 0
        n = 0

        Do
            Zeile = StrReadSync.ReadLine.ToString()
            j += 1
            If (j > Me.nZeilenHeader And Zeile.Length > 0) Then

                'Datum erkennen
                '--------------
                Jahr = Zeile.Substring(0, 4)
                Monat = Zeile.Substring(4, 2)
                Tag = Zeile.Substring(6, 2)
                Stunde = Zeile.Substring(9, 2)
                Minute = Zeile.Substring(12, 2)
                Datum = New System.DateTime(Jahr, Monat, Tag, 0, 0, 0, New System.Globalization.GregorianCalendar())
                ' Wenn Uhrzeit als 24:00 gegeben ist, wird der Tag um eins hochgez�hlt und die Zeit auf 00:00 gesetzt
                If (Stunde = 24) Then
                    Stunde = 0
                    Datum = Datum.AddDays(1)
                End If
                Datum = Datum.AddHours(Stunde)
                Datum = Datum.AddMinutes(Minute)

                'Datum und Wert zur Zeitreihe hinzuf�gen
                '---------------------------------------
                Me.Zeitreihen(0).Length = n + 1
                Me.Zeitreihen(0).XWerte(n) = Datum
                Me.Zeitreihen(0).YWerte(n) = StringToDouble(Zeile.Substring(15))
                n += 1

            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    'ZRE-Datei exportieren
    '******************
    Public Shared Sub Write_File(ByVal Reihe As Zeitreihe, ByVal File As String)

        Dim strwrite As StreamWriter
        Dim i As Integer

        strwrite = New StreamWriter(File)

        '1. Zeile
        strwrite.WriteLine("*ZRE")
        '2. Zeile: Titel und Einheit
        strwrite.WriteLine(Reihe.Title.PadRight(15).Substring(0, 15) & Reihe.Einheit)
        '3. Zeile: Parameter
        strwrite.WriteLine("0                      0.        0.        0.")
        '4. Zeile: Anfangs- und Enddatum
        strwrite.WriteLine(Reihe.XWerte(0).ToString(DatumsformatZRE) & " " & Reihe.XWerte(Reihe.Length - 1).ToString(DatumsformatZRE))
        'ab 5. Zeile: Werte
        For i = 0 To Reihe.Length - 1
            strwrite.Write(Reihe.XWerte(i).ToString(DatumsformatZRE) & " " & Reihe.YWerte(i).ToString(Zahlenformat).PadLeft(14))
            If (i < Reihe.Length - 1) Then 'kein Zeilenumbruch nach der letzten Zeile!
                strwrite.WriteLine()
            End If
        Next
        strwrite.Close()

    End Sub



#End Region 'Methoden

End Class
