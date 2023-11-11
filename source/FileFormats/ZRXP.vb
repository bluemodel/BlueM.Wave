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

Namespace Fileformats

    ''' <summary>
    ''' Class for the ZRXP file format
    ''' Format description: https://www.kisters.de/fileadmin/user_upload/Wasser/Downloads/ZRXP3.0_DE.pdf
    ''' </summary>
    Public Class ZRXP
        Inherits TimeSeriesFile

        ''' <summary>
        ''' ZRXP layouts
        ''' </summary>
        Private Enum LayoutEnum
            ''' <summary>
            ''' A single time series consisting of timestamp and value (possibly followed by remarks, which are ignored by Wave)
            ''' LAYOUT(timestamp,value) or LAYOUT(timestamp,value,remark)
            ''' </summary>
            [Single]
            ''' <summary>
            ''' An ensemble forecast consisting of multiple time series with different initialization dates and member numbers.
            ''' LAYOUT(timestamp,forecast,member,value,)
            ''' </summary>
            EnsembleForecast
        End Enum

        ''' <summary>
        ''' Layout of this ZRXP file
        ''' </summary>
        Private layout As LayoutEnum

        ''' <summary>
        ''' Number of error values encountered while reading the file
        ''' </summary>
        Private errorcount As Integer

        ''' <summary>
        ''' Specifies whether to use the file import dialog
        ''' </summary>
        ''' <value></value>
        ''' <returns>False</returns>
        ''' <remarks></remarks>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Instantiates a new ZRXP object
        ''' </summary>
        ''' <param name="file">path to the file</param>
        ''' <remarks></remarks>
        Public Sub New(file As String, Optional ReadAllNow As Boolean = False)

            Call MyBase.New(file)

            'settings
            Me.Dateformat = Helpers.DateFormats("ZRXP")
            Me.UseUnits = True

            'set default metadata keys
            Me.FileMetadata.AddKeys(ZRXP.MetadataKeys)

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'read immediately
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Checks whether a file conforms with the ZRXP-format
        ''' </summary>
        ''' <param name="file">path to file</param>
        ''' <returns>Boolean</returns>
        ''' <remarks>Checks whether the first or second line starts with the string "#ZRXP"</remarks>
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim line As String
            Dim isZRXP As Boolean = False

            Try
                'open file
                Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                line = StrReadSync.ReadLine.ToString()
                If line.StartsWith("#ZRXP") Then
                    isZRXP = True
                Else
                    'try the second line
                    line = StrReadSync.ReadLine.ToString()
                    If line.StartsWith("#ZRXP") Then
                        isZRXP = True
                    End If
                End If

                StrReadSync.Close()
                StrRead.Close()
                FiStr.Close()

                Return isZRXP

            Catch ex As Exception
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Reads the metadata from the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readSeriesInfo()

            Dim i As Integer
            Dim line, data(), value As String
            Dim keys As List(Of String)
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'copy metadata keys to list
            keys = Me.FileMetadata.Keys.ToList()

            'open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'read header
            i = 0
            Do
                line = StrReadSync.ReadLine.ToString()
                i += 1
                If line.StartsWith("##") Then Continue Do ' ignore lines starting with ##
                If line.StartsWith("#") Then
                    line = line.Substring(1) ' remove "#" from the beginning of the line
                    If line.Contains("|*|") Then
                        data = line.Split("|*|")
                    ElseIf line.Contains(";*;") Then
                        data = line.Split(";*;")
                    Else
                        Throw New Exception("The file does not contain header lines in the expected format!")
                    End If
                    'process header data and store as metadata
                    For Each block As String In data
                        For Each key As String In keys
                            If block.StartsWith(key) Then
                                value = block.Substring(key.Length)
                                Me.FileMetadata(key) = value
                            End If
                        Next
                    Next
                Else
                    'end of header reached
                    Me.iLineData = i
                    Exit Do
                End If
            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'store series info
            sInfo = New TimeSeriesInfo()
            sInfo.Name = $"{Me.FileMetadata("SNAME")}.{Me.FileMetadata("CNAME")}"
            sInfo.Unit = Me.FileMetadata("CUNIT")
            sInfo.Index = 0
            Me.TimeSeriesInfos.Add(sInfo)

            'store layout info
            If Me.FileMetadata("LAYOUT").StartsWith("(timestamp,value") Then
                'single time series
                Me.layout = LayoutEnum.Single

            ElseIf Me.FileMetadata("LAYOUT").StartsWith("(timestamp,forecast,member,value") Then
                'forecast ensemble time series
                Me.layout = LayoutEnum.EnsembleForecast
                'Even though we have multiple time series, we only use a single TimeSeriesInfo instance
            Else
                'unsupported layout
                Throw New Exception($"ZRXP file has an unsupported layout " & Me.FileMetadata("LAYOUT") & "!")
            End If

        End Sub

        ''' <summary>
        ''' reads the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readFile()

            Dim line, parts() As String
            Dim datestring, valuestring As String
            Dim timestamp As DateTime
            Dim value As Double
            Dim sInfo As TimeSeriesInfo
            Dim ts As TimeSeries

            'instantiate time series
            sInfo = Me.TimeSeriesInfos(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

            'store metadata
            ts.Metadata = Me.FileMetadata

            'for ensemble forecasts, use a nested dictionary of time series {initDate: {member: TimeSeries, ...}, ...}
            Dim ts_ensemble As New Dictionary(Of DateTime, Dictionary(Of Integer, TimeSeries))

            'open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'read file
            Me.errorcount = 0
            Do
                line = StrReadSync.ReadLine.ToString()
                'ignore header lines starting with "#" and empty lines
                If line.StartsWith("#") Or line.Trim().Length = 0 Then
                    Continue Do
                End If
                'split line
                parts = line.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)

                Select Case Me.layout
                    Case LayoutEnum.Single
                        'single time series

                        'parse date
                        datestring = parts(0)
                        timestamp = parseDateString(datestring)
                        'parse value
                        valuestring = parts(1)
                        value = parseValueString(valuestring)

                        'store node
                        ts.AddNode(timestamp, value)

                    Case LayoutEnum.EnsembleForecast
                        'forecast ensemble time series

                        'parse initialization date ("timestamp")
                        Dim initDateString As String = parts(0)
                        Dim initDate As DateTime = parseDateString(initDateString)

                        'parse forecast valid date ("forecast")
                        Dim forecastdate As DateTime = parseDateString(parts(1))

                        'parse member number
                        Dim member As Integer = Integer.Parse(parts(2))

                        'parse value
                        valuestring = parts(3)
                        value = parseValueString(valuestring)

                        'add a time series for each new initialization date and member
                        If Not ts_ensemble.ContainsKey(initDate) Then
                            ts_ensemble.Add(initDate, New Dictionary(Of Integer, TimeSeries))
                        End If
                        If Not ts_ensemble(initDate).ContainsKey(member) Then
                            'instantiate new Timeseries
                            Dim ts_member As TimeSeries
                            'copy and then adapt properties
                            ts_member = ts.Clone()
                            ts_member.Title &= $" ({initDateString}, {member})"
                            ts_member.Metadata.Add("timestamp", initDateString)
                            ts_member.Metadata.Add("member", member)
                            ts_ensemble(initDate).Add(member, ts_member)
                        End If

                        'store node
                        ts_ensemble(initDate)(member).AddNode(forecastdate, value)

                End Select

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If Me.errorcount > 0 Then
                Log.AddLogEntry(Log.levels.warning, $"The file contained {Me.errorcount} error values ({Me.FileMetadata("RINVAL")}), which were converted to NaN!")
            End If

            'store time series
            Select Case Me.layout
                Case LayoutEnum.Single
                    Me.TimeSeries.Add(sInfo.Index, ts)

                Case LayoutEnum.EnsembleForecast
                    Dim index As Integer = 0
                    For Each initDate As DateTime In ts_ensemble.Keys
                        For Each member As Integer In ts_ensemble(initDate).Keys
                            Me.TimeSeries.Add(index, ts_ensemble(initDate)(member))
                            index += 1
                        Next
                    Next
            End Select

        End Sub

        ''' <summary>
        ''' Parses a string representing a timestamp to a DateTime instance
        ''' </summary>
        ''' <param name="datestring"></param>
        ''' <returns></returns>
        Private Function parseDateString(datestring As String) As DateTime

            Dim timestamp As DateTime
            Dim ok As Boolean

            If datestring.Length < 14 Then
                'fill missing values with 0
                datestring = datestring.PadRight(14, "0")
            End If
            ok = DateTime.TryParseExact(datestring, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, timestamp)
            If (Not ok) Then
                'WORKAROUND: check whether the hour is 24 and if so, parse manually
                If datestring.Substring(8, 2) = "24" Then
                    Dim year As Integer = Integer.Parse(datestring.Substring(0, 4))
                    Dim month As Integer = Integer.Parse(datestring.Substring(4, 2))
                    Dim day As Integer = Integer.Parse(datestring.Substring(6, 2))
                    Dim hour As Integer = Integer.Parse(datestring.Substring(8, 2))
                    Dim minute As Integer = Integer.Parse(datestring.Substring(10, 2))
                    Dim second As Integer = Integer.Parse(datestring.Substring(12, 2))
                    timestamp = New DateTime(year, month, day, 0, minute, second) + New TimeSpan(days:=1, 0, 0, 0)
                    Log.AddLogEntry(levels.debug, $"Non-standard timestamp '{datestring}' parsed manually to {timestamp:G}")
                Else
                    Throw New Exception($"Unable to parse the date '{datestring}' using the expected date format '{Me.Dateformat}'!")
                End If
            End If
            Return timestamp
        End Function

        ''' <summary>
        ''' Parses a string representing a value to a Double while considering error values
        ''' </summary>
        ''' <param name="valueString"></param>
        ''' <returns></returns>
        Private Function parseValueString(valueString As String) As Double

            Dim value As Double

            value = Helpers.StringToDouble(valueString)
            If value = Helpers.StringToDouble(Me.FileMetadata("RINVAL")) Then
                'convert error value to NaN
                value = Double.NaN
                Me.errorcount += 1
            End If
            Return value
        End Function

        ''' <summary>
        ''' Returns a list of ZRXP-specific metadata keys
        ''' </summary>
        Public Overloads Shared ReadOnly Property MetadataKeys() As List(Of String)
            Get
                Dim keys As New List(Of String) From {
                    "ZRXPVERSION",
                    "ZRXPMODE",
                    "ZRXPCREATOR",
                    "REXCHANGE",
                    "TZ",
                    "SANR",
                    "SNAME",
                    "SWATER",
                    "CNR",
                    "CNAME",
                    "CTYPE",
                    "CMW",
                    "RTIMELVL",
                    "CUNIT",
                    "RINVAL",
                    "RNR",
                    "RTYPE",
                    "RORPR",
                    "LAYOUT"
                }
                Return keys
            End Get
        End Property

        ''' <summary>
        ''' Sets default metadata values for a time series corresponding to the ZRXP file format
        ''' </summary>
        Public Overloads Shared Sub setDefaultMetadata(ts As TimeSeries)
            'Make sure all required keys exist
            ts.Metadata.AddKeys(ZRXP.MetadataKeys)
            'Set default values
            ts.Metadata("ZRXPVERSION") = "3014.03"
            ts.Metadata("ZRXPCREATOR") = "BlueM.Wave"
            ts.Metadata("LAYOUT") = "(timestamp,value)"
            If ts.Metadata("ZRXPMODE") = "" Then ts.Metadata("ZRXPMODE") = "Standard"
            If ts.Metadata("TZ") = "" Then ts.Metadata("TZ") = "CET"
            If ts.Metadata("SNAME") = "" Then ts.Metadata("SNAME") = ts.Title
            If ts.Metadata("SANR") = "" Then ts.Metadata("SANR") = "0"
            If ts.Metadata("CUNIT") = "" Then ts.Metadata("CUNIT") = ts.Unit
            If ts.Metadata("RINVAL") = "" Then ts.Metadata("RINVAL") = "-777.0"
        End Sub

        ''' <summary>
        ''' Exports a time series to a file in the ZRXP format
        ''' </summary>
        ''' <param name="ts">the time series to export</param>
        ''' <param name="file">path to the file</param>
        ''' <remarks></remarks>
        Public Shared Sub Write_File(ByRef ts As TimeSeries, file As String)

            Dim strwrite As StreamWriter

            'ensure that all required metadata keys are present
            ts.Metadata.AddKeys(ZRXP.MetadataKeys)

            strwrite = New StreamWriter(file, False, Helpers.DefaultEncoding)

            '1st line: ZRXP version number, mode, creation tool and timezone
            strwrite.WriteLine($"#ZRXPVERSION{ts.Metadata("ZRXPVERSION")}|*|ZRXPMODE{ts.Metadata("ZRXPMODE")}|*|ZRXPCREATOR{ts.Metadata("ZRXPCREATOR")}|*|TZ{ts.Metadata("TZ")}|*|")

            'next lines: remaining metadata, omitting empty keywords, wrapped
            Dim excludeKeys As New List(Of String)(New String() {"ZRXPVERSION", "ZRXPMODE", "ZRXPCREATOR", "TZ"})
            Dim query As IEnumerable(Of KeyValuePair(Of String, String)) = ts.Metadata.Where(Function(kvp As KeyValuePair(Of String, String)) kvp.Value <> "" And Not excludeKeys.Contains(kvp.Key))
            Dim text As String = ""
            Dim line As String = "#"
            Dim sep As String = "|*|"
            Dim field As String
            Dim maxlength As Integer = 85
            Dim fields As New List(Of String)
            For Each kvp As KeyValuePair(Of String, String) In query
                field = kvp.Key & kvp.Value
                If line.Length + field.Length + sep.Length > maxlength Then
                    'start a new line
                    text &= line & eol
                    line = "#"
                End If
                line &= field & sep
            Next
            text &= line
            strwrite.WriteLine(text)

            'data lines
            Dim timestamp, value As String
            For Each node As KeyValuePair(Of DateTime, Double) In ts.Nodes
                timestamp = node.Key.ToString(Helpers.DateFormats("ZRXP"))
                If Double.IsNaN(node.Value) Then
                    value = ts.Metadata("RINVAL")
                Else
                    value = node.Value.ToString(Helpers.DefaultNumberFormat)
                End If
                strwrite.WriteLine(timestamp & " " & value)
            Next

            strwrite.Close()

        End Sub
    End Class

End Namespace