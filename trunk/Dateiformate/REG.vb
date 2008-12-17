Imports System.IO

''' <summary>
''' Klasse für das REG-Dateiformat (Hystem-Extran-Regendaten)
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/DAT-Format</remarks>
Public Class REG
    Inherits Dateiformat

    Const DatumsformatREG As String = "ddMMyyyyHHmmss"
    Const AnzZeichen As Integer = 5   'Anzahl der Zeichen für ein Wert in der reg/dat-Datei

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private Zeitintervall As Integer
    Private AnzZeilenWerte As Integer
    Private DezFaktor As Integer

#End Region

#Region "Properties"


#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileDaten = 6
        Me.UseEinheiten = True

        Call Me.SpaltenAuslesen()

        'REG-Dateien immer direkt einlesen
        Me.SpaltenSel = Me.YSpalten

        Call Me.Read_File()

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

            'Einheit steht in 2. Zeile:
            For i = 0 To 1
                Zeile = StrReadSync.ReadLine.ToString()
            Next

            'Einheit übernehmen
            ReDim Me.Einheiten(0)
            Me.Einheiten(0) = Zeile.Substring(68, 2)
            Me.DezFaktor = Zeile.Substring(29, 1)


            'Zeitintervall auslesen
            Me.Zeitintervall = Convert.ToSingle(Zeile.Substring(23, 2).Trim)

            Select Case Me.Zeitintervall
               Case 5
                  Me.AnzZeilenWerte = 12
               Case Else
            End Select


            'Reihentitel steht in 3. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()

            ReDim Me.YSpalten(0)
            Me.YSpalten(0) = Zeile.Substring(20).Trim()

            'Spalten übernehmen
            Me.XSpalte = "Datum_Zeit"

            Me.SpaltenSel = Me.YSpalten


            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()


        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'REG-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i, j, n, AnzZeilen As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum, Zeilendatum As DateTime

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Anzahl der Zeilen feststellen
        AnzZeilen = 0
        Do
            Zeile = StrReadSync.ReadLine.ToString()
            If (Zeile.Length > 0) Then
                AnzZeilen += 1
            End If
        Loop Until StrReadSync.Peek() = -1

        'Zeitreihe redimensionieren
        ReDim Me.Zeitreihen(0) 'bei REG gibt es nur eine Zeitreihe
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0))
        Me.Zeitreihen(0).Einheit = Me.Einheiten(0)
        Me.Zeitreihen(0).Length = (AnzZeilen - Me.nZeilenHeader - 1) * AnzZeilenWerte

        'Datei wieder auf Anfang setzen und einlesen
        FiStr.Seek(0, SeekOrigin.Begin)

        j = 0
        n = 0

        Do
            Zeile = StrReadSync.ReadLine.ToString()
            If Zeile.Substring(5) = " 0 09999 0 0 0E" Then Exit Do
            j += 1
            If (j > Me.nZeilenHeader And Zeile.Length > 0) Then

                'Datum erkennen
                '--------------
                Jahr = Zeile.Substring(9, 4)
                Monat = Zeile.Substring(7, 2)
                Tag = Zeile.Substring(5, 2)
                Stunde = Zeile.Substring(13, 2)
                Minute = Zeile.Substring(15, 2)
                Zeilendatum = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())

                'Datum und Wert zur Zeitreihe hinzufügen
                '---------------------------------------
                'alle bis auf den letzten Wert einlesen
                'beim letzten Wert besteht die Möglichkeit, dass nicht alle Zeichen belegt sind
                For i = 0 To AnzZeilenWerte - 1
                   Datum = Zeilendatum.AddMinutes(i * Me.Zeitintervall)
                   Me.Zeitreihen(0).XWerte(n) = Datum
                   Me.Zeitreihen(0).YWerte(n) = StringToDouble(Zeile.Substring(20 + AnzZeichen * i, AnzZeichen)) * 10 ^ (-DezFaktor)
                   n += 1
                Next


            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als REG-Datei
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    Public Shared Sub Write_File(ByVal Reihe As Zeitreihe, ByVal File As String)

        Dim strwrite As StreamWriter
        Dim iZeile, j, n As Integer
        Const WerteproZeile As Integer = 12
        strwrite = New StreamWriter(File)

        '1. Zeile
        strwrite.WriteLine("TUD   0 0   0 1 0 0                                      0        0           0")
        '2. Zeile: 
        'todo: -3 == Dezimalfaktor, muss in Variable gepackt werden
        'todo: Anfangs- und Enddatum aus Zeitreihenobjekt auslesen
        strwrite.WriteLine("TUD   0 0   0 2 0 0     5   -311 42008 0 0 014 42008 0 0 0    3N    MM / IB   ")
        '3. Zeile: 
        strwrite.WriteLine("TUD   0 0   0 3 0 0 Niederschlag Ober-Ramstadt                                  ")
        '4. Zeile: Anfangs- und Enddatum
        strwrite.WriteLine("TUD   0 0   0 4 0 0 Dauer: xy min                                               ")
        '5. Zeile: Werte
        strwrite.WriteLine("TUD   0 0   0 5 0 0 Wiederkehrzeit: x.yz a                                      ")

        n = 0   'n = Anzahl der Zeitreihenwerte
        For iZeile = 0 To (Reihe.Length / WerteproZeile) - 1
            strwrite.Write(Reihe.XWerte(n).ToString(DatumsformatREG) & " ")
            For j = 1 To WerteproZeile
               'todo: * 1000 wegen Dezimalfaktor 3, muss in Variable gepackt werden
               strwrite.Write((Reihe.YWerte(n) * 1000).ToString(Zahlenformat).PadLeft(5))
               n = n + 1
            Next
            strwrite.WriteLine()
        Next
        strwrite.Close()

    End Sub

#End Region 'Methoden

End Class
