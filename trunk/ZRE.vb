Imports System.IO

Public Class ZRE

    Private _DateiPfad As String

    Private Const ZREHEaderLen As Integer = 4       'Die ersten 4 Zeilen der ZRE-Datei gehören zum Header

    Public XWerte() As DateTime
    Public YWerte() As Double

    'Konstruktor
    '***********
    Public Sub New(ByVal DateiPfad As String)
        Me._DateiPfad = DateiPfad
        Call Me.Read_ZRE()
    End Sub


    'Eine ZRE-Datei einlesen
    '***********************
    Private Sub Read_ZRE()

        Dim AnzZeil As Integer = 0
        Dim j As Integer = 0
        Dim Zeile As String

        Dim FiStr As FileStream = New FileStream(Me._DateiPfad, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'Anzahl der Zeilen feststellen
        Do
            Zeile = StrRead.ReadLine.ToString()
            AnzZeil += 1
        Loop Until StrRead.Peek() = -1

        ReDim Me.XWerte(AnzZeil - ZREHEaderLen - 1)
        ReDim Me.YWerte(AnzZeil - ZREHEaderLen - 1)

        'Zurück zum Dateianfang und lesen
        FiStr.Seek(0, SeekOrigin.Begin)

        For j = 0 To AnzZeil - 1
            Zeile = StrRead.ReadLine.ToString()
            If (j >= ZREHEaderLen) Then
                'Datum
                Me.XWerte(j - ZREHEaderLen) = New System.DateTime(Zeile.Substring(0, 4), Zeile.Substring(4, 2), Zeile.Substring(6, 2), Zeile.Substring(9, 2), Zeile.Substring(12, 2), 0, New System.Globalization.GregorianCalendar())
                'Wert
                Me.YWerte(j - ZREHEaderLen) = Convert.ToDouble(Zeile.Substring(15, 14))
            End If
        Next

        StrRead.Close()
        FiStr.Close()

    End Sub

End Class
