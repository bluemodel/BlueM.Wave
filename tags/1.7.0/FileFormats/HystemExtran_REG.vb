'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
'
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
'
Imports System.IO

''' <summary>
''' Klasse für das Hystem-Extran REG-Dateiformat
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/Hystem-Extran_REG-Format</remarks>
Public Class HystemExtran_REG
    Inherits FileFormatBase

    Const DatumsformatHystemExtran As String = "ddMMyyyyHHmmss"
    Const LenString As Integer = 5   'Länge des Strings eines Wertes in der reg/dat-Datei
    Const iDim As Integer = 3        'Dezimalfaktor wird erstmal global auf 3 gesetzt

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _Zeitintervall As Integer
    Private _WerteProZeile As Integer
    Private _DezFaktor As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Property Zeitintervall() As Integer
        Get
            Return _Zeitintervall
        End Get
        Set(ByVal value As Integer)
            _Zeitintervall = value
        End Set
    End Property

    Public Property DezFaktor() As Integer
        Get
            Return _DezFaktor
        End Get
        Set(ByVal value As Integer)
            _DezFaktor = value
        End Set
    End Property

    Public Property WerteProZeile(ByVal dt As Integer) As Integer
        Get
            Select Case dt  'siehe KN-Anwenderhandbuch S.384
                Case 1, 5, 10, 15, 20, 30, 60, 120
                    Return 12
                Case 2, 3
                    Return 10
                Case 720
                    Return 2
            End Select
        End Get
        Set(ByVal value As Integer)
            _WerteProZeile = value
        End Set
    End Property

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iLineData = 6
        Me.UseUnits = True

        Call Me.ReadColumns()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllColumns()
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub ReadColumns()

        Dim Zeile As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Es gibt immer 2 Spalten!
            ReDim Me.Columns(1)

            '1. Spalte (X)
            Me.Columns(0).Name = "Datum_Zeit"
            Me.Columns(0).Index = 0

            '2. Spalte (Y)
            Me.Columns(1).Index = 1

            'Reihentitel steht in 1. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            Me.Columns(1).Name = Zeile.Substring(20, 30).Trim()

            'Einheit steht in 2. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            Me.Columns(1).Einheit = Zeile.Substring(68, 2)

            Me.DezFaktor = Zeile.Substring(29, 1)

            'Zeitintervall auslesen
            Me.Zeitintervall = Convert.ToSingle(Zeile.Substring(23, 2).Trim)

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

        Dim i, j As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum, Zeilendatum As DateTime

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren
        ReDim Me.TimeSeries(0) 'bei REG gibt es nur eine Zeitreihe
        Me.TimeSeries(0) = New TimeSeries(Me.SelectedColumns(0).Name)
        Me.TimeSeries(0).Unit = Me.SelectedColumns(0).Einheit

        'Einlesen
        '--------
        j = 0

        Do
            j += 1
            Zeile = StrReadSync.ReadLine.ToString()

            If Zeile.Substring(19, 1) = "E" Then Exit Do

            If (j > Me.nLinesHeader And Zeile.Length > 0) Then

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
                For i = 0 To Me.WerteProZeile(Me.Zeitintervall) - 1
                    Datum = Zeilendatum.AddMinutes(i * Me.Zeitintervall)
                    Me.TimeSeries(0).AddNode(Datum, StringToDouble(Zeile.Substring(20 + LenString * i, LenString)) * 10 ^ (-DezFaktor))
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
    Public Shared Sub Write_File(ByVal Reihe As TimeSeries, ByVal File As String)

        Dim dt As Integer
        Dim KontiReihe As TimeSeries

        'Zeitintervall aus ersten und zweiten Zeitschritt der Reihe ermitteln
        dt = DateDiff(DateInterval.Minute, Reihe.Dates(0), Reihe.Dates(1))

        'Äquidistante Zeitreihe erzeugen
        KontiReihe = Reihe.getKontiZRE(dt)

        Dim strwrite As StreamWriter
        Dim iZeile, j, n As Integer
        Const WerteproZeile As Integer = 12
        strwrite = New StreamWriter(File)
        Dim IntWert As Long

        '1. Zeile
        strwrite.WriteLine("TUD   0 0   0 1 0 0 Messstelle / Station                 0        0           0")

        '2. Zeile: 
        'Standard
        strwrite.Write("TUD   0 0   0 2 0 0 ")
        'Zeitintervall
        strwrite.Write(dt.ToString.PadLeft(5))
        'Dimension der Zehnerprotenz
        strwrite.Write((iDim * (-1)).ToString.PadLeft(5))
        'Anfangsdatum
        strwrite.Write(KontiReihe.StartDate.ToString(DatumsformatHystemExtran))
        'Enddatum
        strwrite.Write(KontiReihe.EndDate.ToString(DatumsformatHystemExtran))
        'Anzahl der Kommentarzeilen nach Zeile 2, wird = 3 gesetzt
        strwrite.Write("    3")
        'Art der Daten, N = Niederschlag, Q = Abfluss
        strwrite.Write("N    ")
        'Einheit
        strwrite.WriteLine("MM / IB   ")

        '3. Zeile: 
        strwrite.WriteLine("TUD   0 0   0 3 0 0 Beginn         Kommentarzeile 1                        Ende")

        '4. Zeile: Anfangs- und Enddatum
        strwrite.WriteLine("TUD   0 0   0 4 0 0 Beginn         Kommentarzeile 2                        Ende")

        '5. Zeile: Werte
        strwrite.WriteLine("TUD   0 0   0 5 0 0 Beginn         Kommentarzeile 3                        Ende")

        n = 0   'n = Anzahl der Zeitreihenwerte
        For iZeile = 0 To (KontiReihe.Length / WerteproZeile) - 1
            strwrite.Write("TUD  ")
            strwrite.Write(KontiReihe.Dates(n).ToString(DatumsformatHystemExtran) & " ")
            For j = 1 To WerteproZeile
                IntWert = KontiReihe.Values(n) * 10 ^ (iDim)
                strwrite.Write(IntWert.ToString.PadLeft(5))
                n = n + 1
            Next
            strwrite.WriteLine()
        Next
        strwrite.WriteLine("TUD   0 09999 0 0 0E")
        strwrite.Close()

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um eine HystemExtran-REG-Datei handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        '1 Zeile einlesen
        Zeile = StrRead.ReadLine.ToString()

        StrRead.Close()
        FiStr.Close()

        If Zeile.Length >= 8 Then
            If (Zeile.Substring(5, 8) = " 0 0   0") Then
                'Es ist eine Hystem-Regenreihe!
                Return True
            End If
        End If

        Return False

    End Function

#End Region 'Methoden

End Class
