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
''' Klasse für das ZRE-Dateiformat
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/ZRE-Format</remarks>
Public Class ZRE
    Inherits FileFormatBase

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
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.Dateformat = DateFormats("ZRE")
        Me.iLineData = 5
        Me.UseUnits = True

        Call Me.readSeriesInfo()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllSeries()
            Call Me.readFile()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub readSeriesInfo()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim sInfo As SeriesInfo

        Me.SeriesList.Clear()

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Reihentitel steht in 2. Zeile:
            For i = 0 To 1
                Zeile = StrReadSync.ReadLine.ToString()
            Next

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

            'store series info
            sInfo = New SeriesInfo
            sInfo.Name = Zeile.Substring(0, 15).Trim()
            sInfo.Unit = Zeile.Substring(15).Trim()
            sInfo.Index = 0
            Me.SeriesList.Add(sInfo)

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'ZRE-Datei einlesen
    '******************
    Public Overrides Sub readFile()

        Dim j As Integer
        Dim Zeile As String
        Dim timestamp As String
        Dim ok As Boolean
        Dim Datum As DateTime
        Dim ts As TimeSeries
        Dim sInfo As SeriesInfo

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren (nur eine)
        sInfo = Me.SeriesList(0)
        ts = New TimeSeries(sInfo.Name)
        ts.Unit = sInfo.Unit
        ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

        'Einlesen
        '--------
        Try
            j = 0
            Do
                j += 1
                Zeile = StrReadSync.ReadLine.ToString()
                If (j > Me.nLinesHeader And Zeile.Trim.Length > 0) Then

                    'Datum erkennen
                    timestamp = Zeile.Substring(0, 14)
                    ok = DateTime.TryParseExact(timestamp, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        Throw New Exception("Unable to parse the timestamp '" & timestamp & "' using the given format '" & Me.Dateformat & "'!")
                    End If

                    'Datum und Wert zur Zeitreihe hinzufügen
                    '---------------------------------------
                    ts.AddNode(Datum, StringToDouble(Zeile.Substring(15)))

                End If
            Loop Until StrReadSync.Peek() = -1

            'store time series
            Me.FileTimeSeries.Add(sInfo.Index, ts)

        Catch ex As Exception
            'Fehler weiterschmeissen
            Throw ex

        Finally
            'Datei schliessen
            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

        End Try

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als ZRE-Datei
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    Public Shared Sub Write_File(ByVal Reihe As TimeSeries, ByVal File As String)

        Dim strwrite As StreamWriter
        Dim i As Integer

        strwrite = New StreamWriter(File, False, Helpers.DefaultEncoding)

        '1. Zeile
        strwrite.WriteLine("*ZRE")
        '2. Zeile: Titel und Einheit
        strwrite.WriteLine(Reihe.Title.PadRight(15).Substring(0, 15) & Reihe.Unit)
        '3. Zeile: Parameter
        strwrite.WriteLine("0                      0.        0.        0.")
        '4. Zeile: Anfangs- und Enddatum
        strwrite.WriteLine(Reihe.Dates(0).ToString(DateFormats("ZRE")) & " " & Reihe.Dates(Reihe.Length - 1).ToString(DateFormats("ZRE")))
        'ab 5. Zeile: Werte
        For i = 0 To Reihe.Length - 1
            strwrite.Write(Reihe.Dates(i).ToString(DateFormats("ZRE")) & " " & Reihe.Values(i).ToString(DefaultNumberFormat).PadLeft(14))
            If (i < Reihe.Length - 1) Then 'kein Zeilenumbruch nach der letzten Zeile!
                strwrite.WriteLine()
            End If
        Next
        strwrite.Close()

    End Sub

#End Region 'Methoden

End Class
