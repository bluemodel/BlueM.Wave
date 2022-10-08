'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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

Namespace Fileformats

    ''' <summary>
    ''' Klasse für das Simba-Dateiformat (*.SMB)
    ''' </summary>
    ''' <remarks>Format siehe https://wiki.bluemodel.org/index.php/SMB-Format</remarks>
    Public Class SMB
        Inherits TimeSeriesFile

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
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.iLineData = 2
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

            Dim Zeile As String = ""
            Dim sInfo As SeriesInfo

            Me.SeriesList.Clear()

            Try
                'Datei öffnen
                Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                sInfo = New SeriesInfo()

                'Reihentitel steht in 1. Zeile:
                Zeile = StrReadSync.ReadLine.ToString()
                sInfo.Name = Zeile.Substring(15).Trim()
                'Annahme, dass SMB-Dateien Regenreihen sind, daher Einheit mm fest verdrahtet
                sInfo.Unit = "mm"
                sInfo.Index = 0

                StrReadSync.Close()
                StrRead.Close()
                FiStr.Close()

                'store series info
                Me.SeriesList.Add(sInfo)

            Catch ex As Exception
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
            End Try

        End Sub

        'SMB-Datei einlesen
        '******************
        Public Overrides Sub readFile()

            Dim i, j As Integer
            Dim Zeile As String
            Dim Stunde, Minute, Tag, Monat, Jahr As Integer
            Dim Datum As DateTime
            Dim Anfangsdatum As DateTime
            Dim tmpWert As String
            Dim sInfo As SeriesInfo
            Dim ts As TimeSeries

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihe instanzieren (bei SMB gibt es nur eine Zeitreihe)
            sInfo = Me.SeriesList(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

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
                If (j > Me.nLinesHeader And Zeile.Length > 0) Then

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
                    ts.AddNode(Datum, StringToDouble(Zeile.Substring(i + 2)))

                End If
            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'store time series
            Me.FileTimeSeries.Add(sInfo.Index, ts)

        End Sub

#End Region 'Methoden

    End Class

End Namespace