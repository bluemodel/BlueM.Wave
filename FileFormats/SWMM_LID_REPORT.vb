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
    ''' Class for reading the SWMM5 LID Report format (*.txt)
    ''' The format is described in the SWMM format description 
    ''' </summary>
    ''' <remarks>See https://wiki.bluemodel.org/index.php/SWMM_file_formats </remarks>
    Public Class SWMM_LID_REPORT
        Inherits TimeSeriesFile

        Public Overrides ReadOnly Property UseImportDialog As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New(file As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(file)

            'default settings
            Me.iLineHeadings = 2
            Me.UseUnits = True
            Me.IsColumnSeparated = True
            Me.Separator = Constants.tab
            Me.DecimalSeparator = Constants.period
            Me.iLineHeadings = 6 ' and 7!
            Me.UseUnits = True
            Me.iLineUnits = 8
            Me.iLineData = 10
            Me.Dateformat = "MM/dd/yyyy HH:mm:ss"
            Me.DateTimeColumnIndex = 0

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Datei komplett einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If


        End Sub

        ''' <summary>
        ''' Reads series information from the file
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Dim iLine As Integer = 0
            Dim line As String
            Dim sInfo As TimeSeriesInfo
            Dim titles1, titles2, titles, units As New List(Of String)

            Dim FiStr As New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            Do Until StrReadSync.Peek() = -1
                iLine += 1
                line = StrReadSync.ReadLine.ToString().Trim()

                If iLine = Me.iLineHeadings Then
                    titles1 = line.Split(Me.Separator.ToChar).ToList
                ElseIf iLine = Me.iLineHeadings + 1 Then 'titles span two lines!
                    titles2 = line.Split(Me.Separator.ToChar).ToList
                ElseIf iLine = Me.iLineUnits Then
                    units = line.Split(Me.Separator.ToChar).ToList
                ElseIf iLine = Me.iLineData Then
                    Exit Do
                End If
            Loop

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'merge titles
            For i As Integer = 0 To titles1.Count - 1
                titles.Add(titles1(i).Trim() + " " + titles2(i).Trim())
            Next

            'insert missing title at beginning
            If titles.Count < units.Count Then
                titles.Insert(0, "datetime")
            End If

            'store series info
            Me.TimeSeriesInfos.Clear()
            For i As Integer = 0 To titles.Count - 1
                If i <> Me.DateTimeColumnIndex Then
                    sInfo = New TimeSeriesInfo()
                    sInfo.Index = i
                    sInfo.Name = titles(i)
                    sInfo.Unit = units(i).Trim()
                    Me.TimeSeriesInfos.Add(sInfo)
                End If
            Next

        End Sub

        ''' <summary>
        ''' Reads the file
        ''' </summary>
        Public Overrides Sub readFile()

            Dim iLine As Integer = 0
            Dim line As String
            Dim parts As List(Of String)
            Dim ok As Boolean
            Dim timestamp As DateTime
            Dim ts As TimeSeries

            'instantiate time series
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                If Me.UseUnits Then
                    ts.Unit = sInfo.Unit
                End If
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            'read the file
            Dim FiStr As New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            Do Until StrReadSync.Peek() = -1
                iLine += 1
                line = StrReadSync.ReadLine.ToString().Trim()

                If iLine < Me.iLineData Or line.Trim().Length = 0 Then
                    Continue Do
                End If

                parts = line.Split(Me.Separator.ToChar).ToList

                ok = DateTime.TryParseExact(parts(Me.DateTimeColumnIndex).Trim(), Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, timestamp)
                If Not ok Then
                    Throw New Exception($"Could Not parse the timestamp '{parts(Me.DateTimeColumnIndex).Trim()}' using the given date format '{Me.Dateformat}'! Please check the date format!")
                End If
                For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                    Me.TimeSeries(sInfo.Index).AddNode(timestamp, StringToDouble(parts(sInfo.Index), Helpers.DefaultNumberFormat))
                Next
            Loop

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Checks whether the file is a SWMM LID Report File
        ''' </summary>
        ''' <param name="file">path to file</param>
        ''' <returns></returns>
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim line As String

            Dim FiStr As New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)

            'read first line
            line = StrRead.ReadLine.ToString().Trim()

            StrRead.Close()
            FiStr.Close()

            If line.StartsWith("SWMM5 LID Report File") Then
                Return True
            Else
                Return False
            End If

        End Function

    End Class

End Namespace