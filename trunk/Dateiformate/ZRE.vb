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

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Reihentitel steht in 2. Zeile:
        For i = 0 To 1
            Zeile = StrReadSync.ReadLine.ToString()
        Next

        'Spalten übernehmen
        Me.XSpalte = "Datum_Zeit"

        ReDim Me.YSpalten(0)
        Me.YSpalten(0) = Zeile.Substring(0, 15).Trim()

        'Einheit anhängen
        If (Me.UseEinheiten) Then
            Me.YSpalten(0) &= " [" & Zeile.Substring(15, 5).Trim() & "]"
        End If

        Me.SpaltenSel = Me.YSpalten

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

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

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrReadSync.ReadLine.ToString()
            AnzZeil += 1
        Loop Until StrReadSync.Peek() = -1

        'Zeitreihe redimensionieren
        ReDim Me.Zeitreihen(0)
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0))
        Me.Zeitreihen(0).Length = AnzZeil - Me.nZeilenHeader

        'Zurück zum Dateianfang und lesen
        FiStr.Seek(0, SeekOrigin.Begin)

        For j = 0 To AnzZeil - 1
            Zeile = StrReadSync.ReadLine.ToString()
            If (j >= Me.nZeilenHeader) Then
                'Datum
                Me.Zeitreihen(0).XWerte(j - Me.nZeilenHeader) = New System.DateTime(Zeile.Substring(0, 4), Zeile.Substring(4, 2), Zeile.Substring(6, 2), Zeile.Substring(9, 2), Zeile.Substring(12, 2), 0, New System.Globalization.GregorianCalendar())
                'Wert
                Me.Zeitreihen(0).YWerte(j - Me.nZeilenHeader) = Convert.ToDouble(Zeile.Substring(15, 14), FortranProvider)
            End If
        Next

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class
