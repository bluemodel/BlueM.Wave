Imports System.IO

''' <summary>
''' Klasse für das Simba-Dateiformat (*.SMB)
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/SMB-Format</remarks>
Public Class SMB
    Inherits Dateiformat

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
        Me.iZeileDaten = 2
        Me.UseEinheiten = True

        Call Me.SpaltenAuslesen()

        'ZRE-Dateien immer direkt einlesen
        Me.SpaltenSel = Me.YSpalten
        Call Me.Read_File()

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim Zeile As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Reihentitel steht in 1. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            ReDim Me.YSpalten(0)
            Me.YSpalten(0) = Zeile.Substring(15).Trim()

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

            'Spalten übernehmen
            Me.XSpalte = "Datum_Zeit"
            
            'Annahme, dass SMB-Dateien Regenreihen sind, daher Einheit mm fest verdrahtet
            'Einheit übernehmen
            ReDim Me.Einheiten(0)
            Me.Einheiten(0) = "mm"

            Me.SpaltenSel = Me.YSpalten

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'SMB-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i, j, n, AnzZeilen As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum As DateTime
        Dim Anfangsdatum As DateTime
        Dim tmpWert As String

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
        ReDim Me.Zeitreihen(0) 'bei ZRE gibt es nur eine Zeitreihe
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0))
        Me.Zeitreihen(0).Einheit = Me.Einheiten(0)
        Me.Zeitreihen(0).Length = AnzZeilen - Me.nZeilenHeader

        'Datei wieder auf Anfang setzen und einlesen
        FiStr.Seek(0, SeekOrigin.Begin)

        j = 1
        n = 0
         
        'Anfangsdatum einlesen
        Zeile = StrReadSync.ReadLine.ToString()
        Tag = Zeile.Substring(0, 2)
        Monat = Zeile.Substring(2, 2)
        Jahr = Zeile.Substring(4, 4)
        Stunde = Zeile.Substring(8, 2)
        Minute = Zeile.Substring(10, 2)
        
        Anfangsdatum = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())
        
        Do
            Zeile = StrReadSync.ReadLine.ToString()
            j += 1
            If (j > Me.nZeilenHeader And Zeile.Length > 0) Then

                'Datum erkennen
                '--------------
                For i = 0 To Zeile.Length
                  tmpWert = Zeile.Substring(i, 2)
                  If tmpWert = "  " Then
                     Minute = Zeile.Substring(0, i)
                     Exit For
                  End If
                Next
                'Minute = Zeile.Substring(0, 3)
                Datum = Anfangsdatum.AddMinutes(Minute)
                'Debug.Print(Datum)

                'Datum und Wert zur Zeitreihe hinzufügen
                '---------------------------------------
                Me.Zeitreihen(0).XWerte(n) = Datum
                Me.Zeitreihen(0).YWerte(n) = StringToDouble(Zeile.Substring(i + 2))
                n += 1

            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class
