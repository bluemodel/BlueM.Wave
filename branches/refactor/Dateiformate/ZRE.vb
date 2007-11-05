Imports System.IO

Public Class ZRE
    Inherits Dateiformat

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.AnzKopfzeilen = 4

        'Sofort Spalten auslesen (bei ZRE kein ImportDialog!)
        Call Me.SpaltenAuslesen()

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'Reihentitel steht in 2. Zeile:
        For i = 0 To 1
            Zeile = StrRead.ReadLine.ToString()
        Next

        'Spalten �bernehmen
        Me.XSpalte = "Datum_Zeit"
        ReDim Me.YSpalten(0)
        Me.YSpalten(0) = Zeile.Substring(0, 15)

        Me.SpaltenSel = Me.YSpalten

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

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrRead.ReadLine.ToString()
            AnzZeil += 1
        Loop Until StrRead.Peek() = -1

        'Zeitreihe redimensionieren
        ReDim Me.Zeitreihen(0)
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0))
        Me.Zeitreihen(0).Length = AnzZeil - Me.AnzKopfzeilen

        'Zur�ck zum Dateianfang und lesen
        FiStr.Seek(0, SeekOrigin.Begin)

        For j = 0 To AnzZeil - 1
            Zeile = StrRead.ReadLine.ToString()
            If (j >= Me.AnzKopfzeilen) Then
                'Datum
                Me.Zeitreihen(0).XWerte(j - Me.AnzKopfzeilen) = New System.DateTime(Zeile.Substring(0, 4), Zeile.Substring(4, 2), Zeile.Substring(6, 2), Zeile.Substring(9, 2), Zeile.Substring(12, 2), 0, New System.Globalization.GregorianCalendar())
                'Wert
                Me.Zeitreihen(0).YWerte(j - Me.AnzKopfzeilen) = Convert.ToDouble(Zeile.Substring(15, 14))
            End If
        Next

        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class
