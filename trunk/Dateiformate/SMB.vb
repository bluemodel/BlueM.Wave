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
        Call Me.selectAllSpalten()
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

            'Es gibt immer 2 Spalten
            ReDim Me.Spalten(1)

            '1. Spalte (X)
            Me.Spalten(0).Name = "Datum_Zeit"
            Me.Spalten(0).Index = 0

            '2. Spalte (Y)

            'Reihentitel steht in 1. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            Me.Spalten(1).Name = Zeile.Substring(15).Trim()
            'Annahme, dass SMB-Dateien Regenreihen sind, daher Einheit mm fest verdrahtet
            Me.Spalten(1).Einheit = "mm"
            Me.Spalten(1).Index = 1

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'SMB-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i, j As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum As DateTime
        Dim Anfangsdatum As DateTime
        Dim tmpWert As String

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren
        ReDim Me.Zeitreihen(0) 'bei SMB gibt es nur eine Zeitreihe
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0).Name)
        Me.Zeitreihen(0).Einheit = Me.SpaltenSel(0).Einheit

        j = 1

        'Anfangsdatum einlesen
        Zeile = StrReadSync.ReadLine.ToString()
        Tag = Zeile.Substring(0, 2)
        Monat = Zeile.Substring(2, 2)
        Jahr = Zeile.Substring(4, 4)
        Stunde = Zeile.Substring(8, 2)
        Minute = Zeile.Substring(10, 2)

        Anfangsdatum = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())

        'Einlesen
        '--------
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

                'Datum und Wert zur Zeitreihe hinzufügen
                '---------------------------------------
                Me.Zeitreihen(0).AddNode(Datum, StringToDouble(Zeile.Substring(i + 2)))

            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class
