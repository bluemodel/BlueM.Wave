'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
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
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            Try
                'Datei öffnen
                Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                sInfo = New TimeSeriesInfo()

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
                Me.TimeSeriesInfos.Add(sInfo)

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
            Dim sInfo As TimeSeriesInfo
            Dim ts As TimeSeries

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihe instanzieren (bei SMB gibt es nur eine Zeitreihe)
            sInfo = Me.TimeSeriesInfos(0)
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
            Me.TimeSeries.Add(sInfo.Index, ts)

        End Sub

#End Region 'Methoden

    End Class

End Namespace