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
Imports System.Globalization
Imports System.Net

Namespace Fileformats

    ''' <summary>
    ''' Class for reading JAMS/J2000/J2K timeseries results
    ''' </summary>
    ''' <remarks>see https://wiki.bluemodel.org/index.php/JAMS_result_files</remarks>
    Public Class JAMS
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Presets
            Me.IsColumnSeparated = True
            Me.Separator = Constants.tab
            Me.DecimalSeparator = Constants.period
            Me.iLineHeadings = 6
            Me.UseUnits = False
            Me.iLineData = 10
            Me.Dateformat = "yyyy-MM-dd HH:mm"
            Me.DateTimeColumnIndex = 0

            'Determine iLineHeader and iLineData by reading the file contents and checking for tokens
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Dim iLine As Integer = 0
            Do While StrReadSync.Peek <> -1
                iLine += 1
                Dim line As String = StrReadSync.ReadLine()
                If line.StartsWith("@attributes") Then
                    Me.iLineHeadings = iLine + 1
                ElseIf line.StartsWith("@data") Then
                    Me.iLineData = iLine + 1
                    Exit Do
                End If
            Loop

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'Read series information
            Call Me.readSeriesInfo()

            If ReadAllNow Then
                Me.selectAllSeries()
                Me.readFile()
            End If
        End Sub

        Public Overrides Sub readSeriesInfo()

            Dim iLine, index As Integer
            Dim line As String
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'Open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            iLine = 0
            Do While StrReadSync.Peek <> -1

                iLine += 1
                line = StrReadSync.ReadLine()

                If iLine = Me.iLineHeadings Then
                    Dim parts() As String = line.Split(Me.Separator.ToChar())
                    index = 0
                    For Each part As String In parts
                        If part.Trim() <> "" And index <> Me.DateTimeColumnIndex Then
                            sInfo = New TimeSeriesInfo()
                            sInfo.Name = part.Trim()
                            sInfo.Index = index
                            Me.TimeSeriesInfos.Add(sInfo)
                        End If
                        index += 1
                    Next
                    Exit Do
                End If

            Loop

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        Public Overrides Sub readFile()

            Dim iLine As Integer
            Dim line As String
            Dim numberformat As NumberFormatInfo
            Dim isData, success As Boolean
            Dim timestamp As DateTime
            Dim value As Double
            Dim parts(), valuestring As String
            Dim ts As TimeSeries

            'instantiate time series
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            'Number format
            numberformat = Helpers.DefaultNumberFormat.Clone()
            If Me.DecimalSeparator.ToChar = Chr(44) Then
                'change decimal separator to comma
                numberformat.NumberDecimalSeparator = ","
            End If

            'read file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            iLine = 0
            isData = False
            Do While StrReadSync.Peek <> -1

                iLine += 1
                line = StrReadSync.ReadLine()

                'data is only between @start and @end
                If line.StartsWith("@start") Then
                    isData = True
                ElseIf line.StartsWith("@end") Then
                    isData = False
                End If

                If line.StartsWith("@") Then
                    Continue Do
                End If

                If iLine >= Me.iLineData And isData Then
                    parts = line.Split(Me.Separator.ToChar())
                    'parse timestamp
                    success = DateTime.TryParseExact(parts(Me.DateTimeColumnIndex).Trim(), Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, timestamp)
                    If Not success Then
                        Throw New Exception($"Could Not parse the date '{parts(Me.DateTimeColumnIndex)}' using the given date format '{Me.Dateformat}'! Please check the date format!")
                    End If
                    'loop over selected series
                    For Each sinfo As TimeSeriesInfo In Me.SelectedSeries
                        'parse value
                        valuestring = parts(sinfo.Index).Trim()
                        'replace "Infinity" with NaN
                        If valuestring = "Infinity" Then
                            value = Double.NaN
                        Else
                            value = StringToDouble(parts(sinfo.Index), numberformat)
                        End If
                        'add node to timeseries
                        Me.TimeSeries(sinfo.Index).AddNode(timestamp, value)
                    Next
                End If

            Loop

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Checks whether a file is a readable JAMS timeseries result file
        ''' </summary>
        ''' <param name="file">path to file</param>
        ''' <returns></returns>
        Public Shared Function verifyFormat(file As String) As Boolean
            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim line As String
            Dim verificationResult As Boolean

            verificationResult = False

            'check first line
            line = StrRead.ReadLine.ToString()
            If line.StartsWith("@context") Then
                verificationResult = True
            End If
            'check second line
            line = StrRead.ReadLine.ToString()
            If line.StartsWith("jams.components.core.TemporalContext") Then
                verificationResult = verificationResult And True
            End If

            StrRead.Close()
            FiStr.Close()

            Return verificationResult

        End Function

    End Class

End Namespace