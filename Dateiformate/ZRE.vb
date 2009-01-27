Imports System.IO

''' <summary>
''' Klasse für das ZRE-Dateiformat
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/ZRE-Format</remarks>
Public Class ZRE
    Inherits Dateiformat

    Const DatumsformatZRE As String = "yyyyMMdd HH:mm"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

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
        Call Me.selectAllSpalten()
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

            'Reihentitel steht in 2. Zeile:
            For i = 0 To 1
                Zeile = StrReadSync.ReadLine.ToString()
            Next

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

            ReDim Me.Spalten(1)
            Me.Spalten(0).Name = "Datum_Zeit"
            Me.Spalten(1).Name = Zeile.Substring(0, 15).Trim()
            Me.Spalten(1).Einheit = Zeile.Substring(15).Trim()
            Me.Spalten(1).Index = 1

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'ZRE-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim j As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum As DateTime

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren (nur eine)
        ReDim Me.Zeitreihen(0)
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0).Name)
        Me.Zeitreihen(0).Einheit = Me.SpaltenSel(0).Einheit

        'Einlesen
        '--------
        j = 0
        Do
            j += 1
            Zeile = StrReadSync.ReadLine.ToString()
            If (j > Me.nZeilenHeader And Zeile.Length > 0) Then

                'Datum erkennen
                '--------------
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

                'Datum und Wert zur Zeitreihe hinzufügen
                '---------------------------------------
                Me.Zeitreihen(0).AddNode(Datum, StringToDouble(Zeile.Substring(15)))

            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als ZRE-Datei
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
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
