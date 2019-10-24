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
Imports System.Text.RegularExpressions

''' <summary>
''' Class for PRMS OUT file format
''' </summary>
''' <remarks>
''' Supports annual summary, monthly summary and DPOUT files
''' Assumes that headers are fixed (files always contain the same variables)
''' </remarks>
Public Class PRMS
    Inherits FileFormatBase

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    Private Enum FileType As Integer
        monthly = 1
        annual = 2
        dpout = 3
        statvar = 4
    End Enum

    Private FileFormat As FileType

#Region "Methoden"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal FileName As String)

        MyBase.New(FileName)

        Dim i As Integer
        Dim line As String
        Dim lines As Dictionary(Of Integer, String)

        'Open the file to determine the file format
        Dim FiStr As FileStream = New FileStream(File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        i = 0
        lines = New Dictionary(Of Integer, String)
        Do
            line = StrReadSync.ReadLine.ToString()
            i += 1
            lines.Add(i, line)
        Loop Until i = 3

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

        If lines(1).Trim() = "ANNUAL SUMMARY" And _
            lines(2).Trim() = "YEAR   OBS. PREC.   INT.LOSS   NET PREC.   POT.ET     NET ET   SNOWMELT    SIMRO   OBSRO     ERR.*100    CUMS    CUMS2" Then
            Me.FileFormat = FileType.annual
            Me.iLineData = 3
        ElseIf lines(1).Trim() = "MONTHLY SUMMARY" And _
            lines(2).Trim() = "MO   YEAR  O-PPT  N-PPT  INLOS    P-ET   A-ET   SMELT   GW-FL   RS-FL    SRO SIMRO[mm] OBSRO[mm] ERR*100" Then
            Me.FileFormat = FileType.monthly
            Me.iLineData = 3
        ElseIf lines(1).Trim().Contains("PRED DISCH    (m3/s)") Then
            Me.FileFormat = FileType.dpout
            Me.iLineData = 12
        ElseIf Path.GetExtension(FileName).ToLower = ".dat" Then
            Dim nSeries As Integer
            If Integer.TryParse(lines(1).Trim(), nSeries) Then
                Me.FileFormat = FileType.statvar
                Me.iLineData = nSeries + 2
            End If
        Else
            Throw New Exception("Unexpected file format for a PRMS OUT file!")
        End If

        Call Me.ReadColumns()

    End Sub

    Public Overrides Sub ReadColumns()

        Dim i, nSeries As Integer
        Dim line As String
        Dim parts As String()
        Dim lines As Dictionary(Of Integer, String)

        Try
            'Open the file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            lines = New Dictionary(Of Integer, String)

            'Read header data
            For i = 1 To Me.iLineData
                line = StrReadSync.ReadLine.ToString
                lines.Add(i, line)
            Next

            'process header
            Select Case Me.FileFormat

                Case FileType.annual
                    parts = lines(2).Split(New String() {"  "}, StringSplitOptions.RemoveEmptyEntries)
                    nSeries = parts.Count() - 1
                    ReDim Me.Columns(parts.Count() - 1)
                    For i = 0 To parts.Count() - 1
                        Me.Columns(i) = New ColumnInfo()
                        Me.Columns(i).Index = i
                        Me.Columns(i).Name = parts(i).Trim()
                        Me.Columns(i).Einheit = "-"
                    Next
                    Me.DateTimeColumnIndex = 0

                Case FileType.monthly
                    parts = lines(2).Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                    nSeries = parts.Count() - 2
                    ReDim Me.Columns(parts.Count() - 1)
                    For i = 0 To parts.Count() - 1
                        Me.Columns(i) = New ColumnInfo()
                        Me.Columns(i).Index = i
                        Me.Columns(i).Name = parts(i)
                        Me.Columns(i).Einheit = "-"
                    Next
                    Me.DateTimeColumnIndex = 0 'technically 0 and 1

                Case FileType.dpout
                    nSeries = 10
                    parts = lines(Me.iLineData).Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                    ReDim Me.Columns(parts.Count() - 1)
                    For i = 0 To parts.Count() - 1
                        Me.Columns(i) = New ColumnInfo()
                        Me.Columns(i).Index = i
                        If i = 0 Then
                            Me.Columns(i).Name = "YEAR"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 1 Then
                            Me.Columns(i).Name = "MONTH"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 2 Then
                            Me.Columns(i).Name = "DAY"
                            Me.Columns(i).Einheit = "-"
                        Else
                            Dim m As Match
                            m = Regex.Match(lines(i - 2).Trim(), ".{3} (.+)\s+\((.+)\).+")
                            Me.Columns(i).Name = m.Groups(1).Value.Trim()
                            Me.Columns(i).Einheit = m.Groups(2).Value.Trim()
                        End If
                    Next
                    Me.DateTimeColumnIndex = 0 ' technically 0, 1 and 2

                Case FileType.statvar
                    'first header line contains the number of series
                    Integer.TryParse(lines(1), nSeries)
                    parts = lines(Me.iLineData).Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                    ReDim Me.Columns(parts.Count() - 1)
                    For i = 0 To parts.Count() - 1
                        Me.Columns(i) = New ColumnInfo()
                        Me.Columns(i).Index = i
                        If i = 0 Then
                            Me.Columns(i).Name = "NUMBER"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 1 Then
                            Me.Columns(i).Name = "YEAR"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 2 Then
                            Me.Columns(i).Name = "MONTH"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 3 Then
                            Me.Columns(i).Name = "DAY"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 4 Then
                            Me.Columns(i).Name = "HOUR"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 5 Then
                            Me.Columns(i).Name = "MINUTES"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i = 6 Then
                            Me.Columns(i).Name = "SECONDS"
                            Me.Columns(i).Einheit = "-"
                        ElseIf i > 6 Then
                        	'these are the data columns
                            Me.Columns(i).Name = lines(i - 5).Trim()
                            Me.Columns(i).Einheit = "-"
                        End If
                    Next
                    Me.DateTimeColumnIndex = 0 ' technically 1 to 6
            End Select

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    ''' <summary>
    ''' Reads the file
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Read_File()

        Dim i, nSeries, nDateCols As Integer
        Dim line As String
        Dim parts() As String
        Dim value As Double
        Dim timestamp As DateTime

        Try

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Select Case Me.FileFormat
                Case FileType.annual
                    nDateCols = 1
                Case FileType.monthly
                    nDateCols = 2
                Case FileType.dpout
                    nDateCols = 3
                Case FileType.statvar
                    nDateCols = 7 'technically only 5, the first column is a line number and the 7th one is always 0 (milliseconds?)
            End Select

            'Instantiate time series
            nSeries = Me.Columns.Count() - nDateCols
            ReDim Me.TimeSeries(nSeries - 1)
            For i = 0 To nSeries - 1
                Me.TimeSeries(i) = New TimeSeries()
                Me.TimeSeries(i).Title = Me.Columns(i + nDateCols).Name
                Me.TimeSeries(i).Unit = Me.Columns(i + nDateCols).Einheit
            Next

            'Skip header
            For i = 1 To Me.iLineData - 1
                StrReadSync.ReadLine()
            Next

            'Read data
            Do
                line = StrReadSync.ReadLine.ToString()

                parts = line.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)

                'Parse date
                Select Case Me.FileFormat
                    Case FileType.annual
                        timestamp = New Date(parts(0), 1, 1)
                    Case FileType.monthly
                        timestamp = New Date(parts(1), parts(0), 1)
                    Case FileType.dpout
                        timestamp = New Date(parts(0), parts(1), parts(2))
                    Case FileType.statvar
                        timestamp = New DateTime(parts(1), parts(2), parts(3), parts(4), parts(5), parts(6))
                End Select
                'Parse values and store nodes
                For i = 0 To nSeries - 1
                    value = Helpers.StringToDouble(parts(i + nDateCols))
                    Me.TimeSeries(i).AddNode(timestamp, value)
                Next

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    ''' <summary>
    ''' Checks whether a file is in the PRMS OUT format
    ''' Recognizes annual summary, monthly summary and DPOUT files
    ''' </summary>
    ''' <param name="file"></param>
    ''' <returns></returns>
    ''' <remarks>Assumes that headers are fixed (always the same variables)</remarks>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim i As Integer
        Dim line As String
        Dim lines As Dictionary(Of Integer, String)
        Dim headerFound As Boolean = False

        Try
            'Open the file
            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            i = 0
            lines = New Dictionary(Of Integer, String)
            Do
                i += 1
                line = StrReadSync.ReadLine.ToString()
                lines.Add(i, line)
            Loop Until i = 3

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If lines(1).Trim() = "ANNUAL SUMMARY" And _
                lines(2).Trim() = "YEAR   OBS. PREC.   INT.LOSS   NET PREC.   POT.ET     NET ET   SNOWMELT    SIMRO   OBSRO     ERR.*100    CUMS    CUMS2" Then
                Return True
            ElseIf lines(1).Trim() = "MONTHLY SUMMARY" And _
                lines(2).Trim() = "MO   YEAR  O-PPT  N-PPT  INLOS    P-ET   A-ET   SMELT   GW-FL   RS-FL    SRO SIMRO[mm] OBSRO[mm] ERR*100" Then
                Return True
            ElseIf lines(1).Trim().Contains("PRED DISCH    (m3/s)") Then
                Return True
            ElseIf Path.GetExtension(file).ToLower = ".dat" Then
                'first line should contain only an integer
                Dim nLinesHeader As Integer
                If Integer.TryParse(lines(1).Trim(), nLinesHeader) Then
                    Return True
                End If
            End If

            Return False

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try


    End Function


#End Region 'Methoden

End Class