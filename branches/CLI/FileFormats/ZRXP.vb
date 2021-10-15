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
Imports System.Globalization

''' <summary>
''' Class for the ZRXP file format
''' Format description: https://www.kisters.de/fileadmin/user_upload/Wasser/Downloads/ZRXP3.0_DE.pdf
''' </summary>
Public Class ZRXP
    Inherits FileFormatBase

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
    Public Sub New(ByVal file As String, Optional ByVal ReadAllNow As Boolean = False)

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
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

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
                If line.StartsWith("#ZRXP") Then isZRXP = True
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
        Dim line, data(), keys(), value As String
        Dim sInfo As SeriesInfo

        Me.SeriesList.Clear()

        'copy metadata keys to array
        ReDim keys(Me.FileMetadata.Keys.Count - 1)
        Me.FileMetadata.Keys.CopyTo(keys, 0)

        'read header
        Try
            'open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

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
            sInfo = New SeriesInfo
            sInfo.Name = $"{Me.FileMetadata("SNAME")}.{Me.FileMetadata("CNAME")}"
            sInfo.Unit = Me.FileMetadata("CUNIT")
            sInfo.Index = 0
            Me.SeriesList.Add(sInfo)

        Catch ex As Exception
            MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try

    End Sub

    ''' <summary>
    ''' reads the file
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub readFile()

        Dim line, parts() As String
        Dim datestring, valuestring As String
        Dim errorcount As Integer
        Dim timestamp As DateTime
        Dim ok As Boolean
        Dim value As Double
        Dim sInfo As SeriesInfo
        Dim ts As TimeSeries

        Try
            'instantiate time series
            sInfo = Me.SeriesList(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

            'store metadata
            ts.Metadata = Me.FileMetadata

            'open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'read file
            errorcount = 0
            Do
                line = StrReadSync.ReadLine.ToString()
                'ignore lines starting with "#" and empty lines
                If line.StartsWith("#") Or line.Trim().Length = 0 Then
                    Continue Do
                End If
                'split line
                parts = line.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                'parse date
                datestring = parts(0)
                If datestring.Length < 14 Then
                    'fill missing values with 0
                    datestring = datestring.PadRight(14, "0")
                End If
                ok = DateTime.TryParseExact(datestring, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, timestamp)
                If (Not ok) Then
                    Throw New Exception($"Unable to parse the date '{datestring}' using the expected date format '{Me.Dateformat}'!")
                End If
                'parse value
                valuestring = parts(1)
                value = Helpers.StringToDouble(valuestring)
                If value = Helpers.StringToDouble(Me.FileMetadata("RINVAL")) Then
                    'convert error value to NaN
                    value = Double.NaN
                    errorcount += 1
                End If

                'store node
                ts.AddNode(timestamp, value)

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If errorcount > 0 Then
                Log.AddLogEntry(Log.levels.warning, $"The file contained {errorcount} error values ({Me.FileMetadata("RINVAL")}), which were converted to NaN!")
            End If

            'store time series
            Me.FileTimeSeries.Add(sInfo.Index, ts)

        Catch ex As Exception
            MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try

    End Sub

    ''' <summary>
    ''' Returns a list of ZRXP-specific metadata keys
    ''' </summary>
    Public Overloads Shared ReadOnly Property MetadataKeys() As List(Of String)
        Get
            Dim keys As New List(Of String)
            keys.Add("ZRXPVERSION")
            keys.Add("ZRXPMODE")
            keys.Add("ZRXPCREATOR")
            keys.Add("TZ")
            keys.Add("SANR")
            keys.Add("SNAME")
            keys.Add("SWATER")
            keys.Add("CNR")
            keys.Add("CNAME")
            keys.Add("CTYPE")
            keys.Add("CMW")
            keys.Add("RTIMELVL")
            keys.Add("CUNIT")
            keys.Add("RINVAL")
            keys.Add("RNR")
            keys.Add("RTYPE")
            keys.Add("RORPR")
            keys.Add("LAYOUT")
            Return keys
        End Get
    End Property

    ''' <summary>
    ''' Sets default metadata values for a time series corresponding to the ZRXP file format
    ''' </summary>
    Public Overloads Shared Sub setDefaultMetadata(ByVal ts As TimeSeries)
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
    Public Shared Sub Write_File(ByRef ts As TimeSeries, ByVal file As String)

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
