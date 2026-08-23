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
Imports System.Text.RegularExpressions

Namespace Fileformats

    ''' <summary>
    ''' Class for parsing the SIMBA file format (*.SMB)
    ''' </summary>
    ''' <remarks>Format see https://wiki.bluemodel.org/index.php/SMB-Format</remarks>
    Public Class SMB
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Flag indicating whether the file has a header line containing the start date and series title
        ''' </summary>
        Private hasHeader As Boolean

        ''' <summary>
        ''' Start date of the time series, either read from the header or provided by the user
        ''' </summary>
        Private startDate As DateTime

        ''' <summary>
        ''' No import dialog needed for SMB files, as there is only one time series per file
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog As Boolean = False

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'defaults
            Me.LineNumberData = 2
            Me.UseUnits = True

            Call Me.ReadSeriesInfo()

            If (ReadAllNow) Then
                Call Me.SelectAllSeries()
                Call Me.ReadFile()
            End If

        End Sub

        Public Overrides Sub ReadSeriesInfo()

            Dim line, title As String
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            Dim fiStr As New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim strRead As New StreamReader(fiStr, Me.Encoding)
            Dim strReadSync As TextReader = TextReader.Synchronized(strRead)

            sInfo = New TimeSeriesInfo()

            'check whether first line is header containing start date and series title
            line = strReadSync.ReadLine()
            Dim m As Match = Regex.Match(line, "^(\d{12})(.+)$")
            If m.Success Then
                Me.hasHeader = True
                'parse start date
                Dim success As Boolean = DateTime.TryParseExact(m.Groups(1).Value, "ddMMyyyyHHmm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, Me.startDate)
                If Not success Then
                    Throw New Exception($"Could not parse start date from header of file {Me.File}.")
                End If
                'get title from line
                title = m.Groups(2).Value.Trim()
            Else
                Me.hasHeader = False
                'ask user for reference start date
                Dim dlg As New ReferenceDateDialog()
                dlg.ShowDialog()
                Me.startDate = dlg.ReferenceDate
                'use filename as series title
                title = IO.Path.GetFileName(Me.File)
            End If

            sInfo.Name = title
            'we assume SMB files always contain precipitation, thus set the unit to mm
            sInfo.Unit = "mm"
            sInfo.Index = 0

            strReadSync.Close()
            strRead.Close()
            fiStr.Close()

            'store series info
            Me.TimeSeriesInfos.Add(sInfo)

        End Sub

        Public Overrides Sub ReadFile()

            Dim line As String
            Dim hasHeader As Boolean = False
            Dim minutes As Integer
            Dim value As Double
            Dim timestamp As DateTime
            Dim sInfo As TimeSeriesInfo
            Dim ts As TimeSeries

            Dim fiStr As New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim strRead As New StreamReader(fiStr, Me.Encoding)
            Dim strReadSync As TextReader = TextReader.Synchronized(strRead)

            'instantiate timeseries (only one)
            sInfo = Me.TimeSeriesInfos(0)
            ts = New TimeSeries(sInfo.Name) With {
                .Unit = sInfo.Unit,
                .DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
            }

            'read file
            Dim iLine As Integer = 0
            Do
                iLine += 1
                line = strReadSync.ReadLine()
                If Me.hasHeader And iLine = 1 Then
                    'skip first line if header is present
                    Continue Do
                End If
                If line.Length > 0 Then
                    'each line consists of minutes and value, separated by two spaces
                    Dim parts As String() = line.Split("  ", StringSplitOptions.None)
                    If parts.Length = 2 Then
                        minutes = Integer.Parse(parts(0).Trim())
                        value = StringToDouble(parts(1).Trim())
                        timestamp = Me.startDate.AddMinutes(minutes)
                        ts.AddNode(timestamp, value)
                    End If
                End If
            Loop Until strReadSync.Peek() = -1

            strReadSync.Close()
            strRead.Close()
            fiStr.Close()

            'store time series
            Me.TimeSeries.Add(sInfo.Index, ts)

        End Sub

    End Class

End Namespace