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
Imports System.Globalization
Imports System.IO

Namespace Fileformats

    ''' <summary>
    ''' Class for SWMM time series (DAT) file format
    ''' </summary>
    ''' <remarks>See https://wiki.bluemodel.org/index.php/SWMM_file_formats</remarks>
    Public Class SWMM_TIMESERIES
        Inherits TimeSeriesFile

        Const SWMM_DATEFORMAT As String = "MM/dd/yyyy HH:mm"

        ''' <summary>
        ''' Date time format to force "/" as date separator
        ''' </summary>
        ''' <returns></returns>
        Private Shared ReadOnly Property dtfi As DateTimeFormatInfo
            Get
                Return CultureInfo.GetCultureInfo("en-US").DateTimeFormat
            End Get
        End Property

        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.iLineData = 2
            Me.UseUnits = False
            Me.Dateformat = SWMM_DATEFORMAT

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        Public Overrides Sub readSeriesInfo()

            Dim line As String
            Dim title As String = Nothing
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'Open the file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'find a commented header line
            Do
                line = StrReadSync.ReadLine()
                If line.StartsWith(";") Then
                    'use the first header line as time series title
                    title = line.Substring(1).Trim()
                    Exit Do
                End If
            Loop Until StrReadSync.Peek = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If title Is Nothing Then
                'use file name as title
                title = IO.Path.GetFileName(Me.File)
            End If

            'store series info
            sInfo = New TimeSeriesInfo()
            sInfo.Name = title
            sInfo.Index = 0
            Me.TimeSeriesInfos.Add(sInfo)

        End Sub

        Public Overrides Sub readFile()

            Dim line As String
            Dim dateString, valueString As String
            Dim timestamp As DateTime
            Dim value As Double
            Dim sinfo As TimeSeriesInfo
            Dim ts As TimeSeries

            Me.TimeSeries.Clear()

            'instantiate a single timeseries
            sinfo = Me.TimeSeriesInfos.First
            ts = New TimeSeries()
            ts.Title = sinfo.Name
            ts.DataSource = New TimeSeriesDataSource(Me.File, ts.Title)

            'Open the file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Do
                line = StrReadSync.ReadLine()
                If line.StartsWith(";") Or line.Trim().Length = 0 Then
                    'skip comment/header and empty lines
                    Continue Do
                End If

                'parse date
                dateString = line.Substring(0, Me.Dateformat.Length)
                If Not DateTime.TryParseExact(dateString, Me.Dateformat, SWMM_TIMESERIES.dtfi, Globalization.DateTimeStyles.None, timestamp) Then
                    Throw New Exception($"Unable to parse the date {dateString} using the format {Me.Dateformat}!")
                End If
                'parse value
                valueString = line.Substring(Me.Dateformat.Length).Trim()
                value = Helpers.StringToDouble(valueString)

                'add node to timeseries
                ts.AddNode(timestamp, value)

            Loop Until StrReadSync.Peek = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'store timeseries
            Me.TimeSeries.Add(1, ts)

        End Sub

        ''' <summary>
        ''' Checks whether a file is in SWMM time series (DAT) file format
        ''' </summary>
        ''' <param name="file">path to file</param>
        ''' <returns></returns>
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim line As String
            Dim dateString, valueString As String
            Dim timestamp As DateTime
            Dim value As Double
            Dim verificationResult As Boolean

            verificationResult = False

            'Open the file
            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Helpers.DefaultEncoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'try to read the first data line
            Try
                Do
                    line = StrReadSync.ReadLine()
                    If line.StartsWith(";") Or line.Trim().Length = 0 Then
                        'skip comment/header and empty lines
                        Continue Do
                    End If

                    'try to parse the date
                    dateString = line.Substring(0, SWMM_DATEFORMAT.Length)
                    If Not DateTime.TryParseExact(dateString, SWMM_DATEFORMAT, SWMM_TIMESERIES.dtfi, Globalization.DateTimeStyles.None, timestamp) Then
                        Throw New Exception($"Unable to parse the date {dateString} using the format {SWMM_DATEFORMAT}!")
                    End If
                    'try to parse the value
                    valueString = line.Substring(SWMM_DATEFORMAT.Length).Trim()
                    value = Helpers.StringToDouble(valueString)

                    verificationResult = True

                    Exit Do

                Loop Until StrReadSync.Peek = -1

            Catch ex As Exception
                verificationResult = False
            End Try

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            Return verificationResult

        End Function

        ''' <summary>
        ''' Writes a time series to file in SWMM time series (DAT) file format
        ''' </summary>
        ''' <param name="ts">time series to export</param>
        ''' <param name="path">path to file to write</param>
        Public Shared Sub Write_File(ts As TimeSeries, path As String)

            Dim strwrite As New StreamWriter(path)

            'write a header line containing title and unit
            strwrite.WriteLine($";{ts.Title} [{ts.Unit}]")
            'write data
            For Each kvp As KeyValuePair(Of DateTime, Double) In ts.Nodes
                Dim timestamp As DateTime = kvp.Key
                Dim value As Double = kvp.Value
                strwrite.WriteLine($"{timestamp.ToString(SWMM_DATEFORMAT, SWMM_TIMESERIES.dtfi)} {value.ToString(Helpers.DefaultNumberFormat)}")
            Next
            strwrite.Close()

        End Sub

    End Class

End Namespace