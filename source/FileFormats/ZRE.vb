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
    ''' Klasse für das ZRE-Dateiformat
    ''' </summary>
    ''' <remarks>Format siehe https://wiki.bluemodel.org/index.php/ZRE-Format</remarks>
    Public Class ZRE
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
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Reihentitel steht in 2. Zeile:
            For i = 0 To 1
                Zeile = StrReadSync.ReadLine.ToString()
            Next

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'store series info
            sInfo = New TimeSeriesInfo
            sInfo.Name = Zeile.Substring(0, 15).Trim()
            sInfo.Unit = Zeile.Substring(15).Trim()
            sInfo.Index = 0
            Me.TimeSeriesInfos.Add(sInfo)

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
            Dim sInfo As TimeSeriesInfo

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihe instanzieren (nur eine)
            sInfo = Me.TimeSeriesInfos(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

            'Einlesen
            '--------
            j = 0
            Do
                j += 1
                Zeile = StrReadSync.ReadLine.ToString()
                If (j > Me.nLinesHeader And Zeile.Trim.Length > 0) Then

                    'Datum erkennen
                    timestamp = Zeile.Substring(0, 14)
                    ok = DateTime.TryParseExact(timestamp, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, Datum)
                    If (Not ok) Then
                        Throw New Exception($"Unable to parse the timestamp '{timestamp}' using the expected format '{Me.Dateformat}'!")
                    End If

                    'Datum und Wert zur Zeitreihe hinzufügen
                    '---------------------------------------
                    ts.AddNode(Datum, StringToDouble(Zeile.Substring(15)))

                End If
            Loop Until StrReadSync.Peek() = -1

            'store time series
            Me.TimeSeries.Add(sInfo.Index, ts)

            'Datei schliessen
            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Exportiert eine Zeitreihe als ZRE-Datei
        ''' </summary>
        ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
        ''' <param name="File">Pfad zur anzulegenden Datei</param>
        Public Shared Sub Write_File(Reihe As TimeSeries, File As String)

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

End Namespace