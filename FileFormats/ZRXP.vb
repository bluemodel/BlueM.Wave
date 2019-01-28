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

        'instantiate metadata
        For Each key As String In ZRXP.MetadataKeys
            Me.Metadata.Add(key, "")
        Next

        Call Me.ReadColumns()

        If (ReadAllNow) Then
            'read immediately
            Call Me.selectAllColumns()
            Call Me.Read_File()
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
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
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
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Reads the metadata from the file
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub ReadColumns()

        Dim i As Integer
        Dim line, data(), keys(), value As String

        'copy metadata keys to array
        ReDim keys(Me.metadata.Count - 1)
        Me.metadata.Keys.CopyTo(keys, 0)

        'read header
        Try
            'open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
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
                                Me.Metadata(key) = value
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

            'Configure columns
            ReDim Me.Columns(1) ' Each ZRXP file contains only one time series

            Me.Columns(0).Index = 0
            Me.Columns(0).Name = "timestamp"
            Me.Columns(0).Einheit = ""

            Me.Columns(1).Index = 1
            Me.Columns(1).Name = Me.Metadata("SNAME") & "." & Me.Metadata("CNAME")
            Me.Columns(1).Einheit = Me.Metadata("CUNIT")

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    ''' <summary>
    ''' reads the file
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Read_File()

        Dim line, parts() As String
        Dim datestring, valuestring As String
        Dim errorcount As Integer
        Dim timestamp As DateTime
        Dim ok As Boolean
        Dim value As Double

        Try
            'instantiate time series
            ReDim Me.TimeSeries(0)
            Me.TimeSeries(0) = New TimeSeries(Me.Columns(1).Name)
            Me.TimeSeries(0).Unit = Me.Columns(1).Einheit

            'store metadata
            Me.TimeSeries(0).Metadata = Me.metadata

            'open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
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
                    Throw New Exception("Unable to parse the date '" & datestring & "' using the expected date format '" & Me.Dateformat & "'!")
                End If
                'parse value
                valuestring = parts(1)
                If valuestring = Me.Metadata("RINVAL") Then
                    'convert error value to NaN
                    value = Double.NaN
                    errorcount += 1
                Else
                    value = Helpers.StringToDouble(valuestring)
                End If
                'store node
                Me.TimeSeries(0).AddNode(timestamp, value)

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            Log.AddLogEntry("The file contained " & errorcount & " error values (" & Me.Metadata("RINVAL") & "), which were converted to NaN!")

        Catch ex As Exception
            MsgBox("Error while parsing file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
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
        For Each key As String In ZRXP.MetadataKeys
            If Not ts.Metadata.ContainsKey(key) Then
                ts.Metadata.Add(key, "")
            End If
        Next
        'Set default values
        If ts.Metadata("ZRXPVERSION") = "" Then ts.Metadata("ZRXPVERSION") = "3014.03"
        If ts.Metadata("ZRXPCREATOR") = "" Then ts.Metadata("ZRXPCREATOR") = "BlueM.Wave"
        If ts.Metadata("SNAME") = "" Then ts.Metadata("SNAME") = ts.Title
        If ts.Metadata("SANR") = "" Then ts.Metadata("SANR") = "0"
        If ts.Metadata("CUNIT") = "" Then ts.Metadata("CUNIT") = ts.Unit
        If ts.Metadata("RINVAL") = "" Then ts.Metadata("RINVAL") = "-777.0"
        If ts.Metadata("LAYOUT") = "" Then ts.Metadata("LAYOUT") = "(timestamp,value)"
    End Sub

End Class
