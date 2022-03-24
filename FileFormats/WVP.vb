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
Imports System.Text.RegularExpressions

''' <summary>
''' Class for importing a wave project file
''' https://wiki.bluemodel.org/index.php/Wave_project_file
''' </summary>
Public Class WVP

    Private projectfile As String

    'fileDict = {filename1:{series1:title1, series2:title2, ...}, ...}
    Private fileDict As Dictionary(Of String, Dictionary(Of String, String))

    'settingsDict = {filename1:{setting1:value1, setting2:value2, ...}, ...}
    Private settingsDict As Dictionary(Of String, Dictionary(Of String, String))

    Public Sub New(file As String)

        Me.projectfile = file

        fileDict = New Dictionary(Of String, Dictionary(Of String, String))
        settingsDict = New Dictionary(Of String, Dictionary(Of String, String))

        Call Me.Parse()

    End Sub

    ''' <summary>
    ''' Parses the project file
    ''' </summary>
    Private Sub Parse()

        Dim fstr As IO.FileStream
        Dim strRead As IO.StreamReader
        Dim line, parts(), file, series, title As String

        Log.AddLogEntry(Log.levels.info, $"Parsing Wave project file {projectfile} ...")

        'read project file

        'file format (all whitespace is optional):
        '
        'file=path\to\file1
        ' series=seriesname1
        ' series=series2: "optional title"
        'file=path\to\file2
        ' series=series3
        ' series=series4
        '
        fstr = New IO.FileStream(projectfile, IO.FileMode.Open)
        strRead = New IO.StreamReader(fstr, detectEncodingFromByteOrderMarks:=True)

        file = ""

        line = strRead.ReadLine()
        While Not IsNothing(line)

            line = line.Trim() 'get rid of whitespace

            If line.StartsWith("#") Then
                'skip comments
                line = strRead.ReadLine()
                Continue While
            End If

            If line.ToLower().StartsWith("file=") Then
                'file
                file = line.Split("=".ToCharArray(), 2)(1).Trim()
                If Not IO.Path.IsPathRooted(file) Then
                    'it's a relative path: construct the full path relative to the project file
                    file = IO.Path.GetFullPath(IO.Path.Combine(IO.Path.GetDirectoryName(projectfile), file))
                End If
                If Not fileDict.ContainsKey(file) Then
                    fileDict.Add(file, New Dictionary(Of String, String))
                End If

            ElseIf line.ToLower().StartsWith("series=") Then
                'series
                line = line.Split("=".ToCharArray(), 2)(1).Trim()
                'series name may be enclosed in quotes and be followed by an optional title, which may also be enclosed in quotes
                'examples:
                'series
                'series:title
                '"se:ries":title
                '"se:ries":"title"
                Dim pattern As String
                If line.StartsWith("""") Then
                    'series name is enclosed in quotes
                    pattern = "^""([^""]+)""(:(.+))?$"
                Else
                    'no quotes around series name
                    pattern = "^([^:]+)(:(.+))?$"
                End If
                Dim m As Match = Regex.Match(line, pattern)
                If m.Success Then
                    series = m.Groups(1).Value.Trim()
                    If m.Groups(2).Success Then
                        title = m.Groups(3).Value.Replace("""", "").Trim() 'remove quotes around title here
                    Else
                        title = ""
                    End If
                    'add series to file
                    If fileDict.ContainsKey(file) Then
                        If Not fileDict(file).ContainsKey(series) Then
                            fileDict(file).Add(series, title)
                        Else
                            Log.AddLogEntry(Log.levels.warning, $"Series {series} is specified twice, the second mention will be ignored!")
                        End If
                    Else
                        Log.AddLogEntry(Log.levels.warning, $"Series {series} is not associated with a file and will be ignored!")
                    End If
                Else
                    Log.AddLogEntry(Log.levels.warning, $"Unable to parse series definition 'series={line}', this series will be ignored!")
                End If

            ElseIf line.Contains("=") Then
                'settings
                parts = line.Trim().Split("=".ToCharArray(), 2)
                'add setting to file
                If fileDict.ContainsKey(file) Then
                    If Not settingsDict.ContainsKey(file) Then
                        settingsDict.Add(file, New Dictionary(Of String, String))
                    End If
                    If Not settingsDict(file).ContainsKey(parts(0)) Then
                        settingsDict(file).Add(parts(0), parts(1))
                    Else
                        Log.AddLogEntry(Log.levels.warning, $"Setting {parts(0)} is specified twice, the second mention will be ignored!")
                    End If
                Else
                    Log.AddLogEntry(Log.levels.warning, $"Setting {parts(0)} is not associated with a file and will be ignored!")
                End If

            Else
                'ignore any other lines
            End If
            line = strRead.ReadLine()
        End While

        strRead.Close()
        fstr.Close()

    End Sub

    ''' <summary>
    ''' Reads all timeseries from all files as specified in the project file
    ''' </summary>
    ''' <returns>the list of time series</returns>
    Public Function Process() As List(Of TimeSeries)

        Dim found As Boolean
        Dim value As String
        Dim seriesList As Dictionary(Of String, String)
        Dim seriesNotFound As List(Of String)
        Dim fileInstance As FileFormatBase

        Dim tsList As New List(Of TimeSeries)

        'loop over file list
        For Each file As String In fileDict.Keys

            Log.AddLogEntry(Log.levels.info, $"Reading file {file} ...")

            'get an instance of the file
            fileInstance = FileFactory.getFileInstance(file)

            'apply custom import settings
            If settingsDict.ContainsKey(file) Then
                For Each setting As String In settingsDict(file).Keys
                    value = settingsDict(file)(setting)
                    Try
                        Select Case setting.ToLower()
                            Case "iscolumnseparated"
                                fileInstance.IsColumnSeparated = If(value.ToLower() = "true", True, False)
                            Case "separator"
                                fileInstance.Separator = New Character(value)
                            Case "dateformat"
                                fileInstance.Dateformat = value
                            Case "decimalseparator"
                                fileInstance.DecimalSeparator = New Character(value)
                            Case "ilineheadings"
                                fileInstance.iLineHeadings = Convert.ToInt32(value)
                            Case "ilineunits"
                                fileInstance.iLineUnits = Convert.ToInt32(value)
                            Case "ilinedata"
                                fileInstance.iLineData = Convert.ToInt32(value)
                            Case "useunits"
                                fileInstance.UseUnits = If(value.ToLower() = "true", True, False)
                            Case "columnwidth"
                                fileInstance.ColumnWidth = Convert.ToInt32(value)
                            Case "datetimecolumnindex"
                                fileInstance.DateTimeColumnIndex = Convert.ToInt32(value)
                            Case Else
                                Log.AddLogEntry(Log.levels.warning, $"Setting '{setting}' was not recognized and was ignored!")
                        End Select
                    Catch ex As Exception
                        Log.AddLogEntry(Log.levels.warning, $"Setting '{setting}' with value '{value}' could not be parsed and was ignored!")
                    End Try
                Next
                'reread columns with new settings
                fileInstance.readSeriesInfo()
            End If

            'get the list of series to be imported
            seriesList = fileDict(file)

            'select series for importing
            If seriesList.Count = 0 Then
                'read all series contained in the file
                Call fileInstance.selectAllSeries()
            Else
                'loop over series names
                seriesNotFound = New List(Of String)
                For Each series In seriesList.Keys
                    found = fileInstance.selectSeries(series)
                    If Not found Then
                        seriesNotFound.Add(series)
                    End If
                Next
                'remove series that were not found from the dictionary
                For Each series In seriesNotFound
                    seriesList.Remove(series)
                Next
                'if no series remain to be imported, abort reading the file altogether
                If seriesList.Count = 0 Then
                    Log.AddLogEntry(Log.levels.error, "No series left to import, skipping file!")
                    Continue For
                End If

            End If

            'read the file
            fileInstance.readFile()

            'Log
            Call Log.AddLogEntry(Log.levels.info, $"File '{file}' imported successfully!")

            'store the series
            For Each ts As TimeSeries In fileInstance.FileTimeSeries.Values
                'change title if specified in the project file
                If seriesList.Count > 0 Then
                    If seriesList(ts.Title) <> "" Then
                        ts.Title = seriesList(ts.Title)
                    End If
                End If
                tsList.Add(ts)
            Next
        Next

        Return tsList

    End Function

End Class
