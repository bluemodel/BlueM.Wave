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
Imports System.Text.RegularExpressions

Namespace Fileformats

    ''' <summary>
    ''' Class for importing a wave project file
    ''' https://wiki.bluemodel.org/index.php/Wave_project_file
    ''' </summary>
    Public Class WVP

        Private projectfile As String

        ''' <summary>
        ''' Structure for storing series options
        ''' </summary>
        Private Structure seriesOption
            Dim title As String
            Dim unit As String
            Dim color As String
            Dim linestyle As String
            Dim linewidth As String
            Dim interpretation As String
            Dim showpoints As String
        End Structure

        'fileDict = {filename1:{series1:options1, series2:options2, ...}, ...}
        Private fileDict As Dictionary(Of String, Dictionary(Of String, seriesOption))

        'settingsDict = {filename1:{setting1:value1, setting2:value2, ...}, ...}
        Private settingsDict As Dictionary(Of String, Dictionary(Of String, String))

        Public Sub New(file As String)

            Me.projectfile = file

            fileDict = New Dictionary(Of String, Dictionary(Of String, seriesOption))
            settingsDict = New Dictionary(Of String, Dictionary(Of String, String))

            Call Me.Parse()

        End Sub

        ''' <summary>
        ''' Parses the project file
        ''' </summary>
        Private Sub Parse()

            Dim fstr As IO.FileStream
            Dim strRead As IO.StreamReader
            Dim line, parts(), file, seriesName As String

            Log.AddLogEntry(Log.levels.info, $"Parsing Wave project file {projectfile} ...")

            'read project file

            'file format (all whitespace is optional):
            '
            'file=path\to\file1
            ' series=seriesname1
            ' series=series2: "optional title"
            ' series=series3: title=custom title, color=Red, linestyle=Dash, unit=mm, interpretation=BlockRight
            'file=path\to\file2
            ' series=series4
            ' series=series5
            '
            fstr = New IO.FileStream(projectfile, IO.FileMode.Open)
            strRead = New IO.StreamReader(fstr, Text.Encoding.UTF8)

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
                        fileDict.Add(file, New Dictionary(Of String, seriesOption))
                    End If

                ElseIf line.ToLower().StartsWith("series=") Then
                    'series
                    line = line.Split("=".ToCharArray(), 2).Last.Trim()
                    'series name may be enclosed in quotes and be followed by an optional title, which may also be enclosed in quotes
                    'examples:
                    'series
                    'series:title
                    '"se:ries":title
                    '"se:ries":"title"
                    'series:title="title", unit=m³/s, color=Red, linestyle=Dash, linewidth=3, interpretation=BlockRight
                    Dim pattern As String
                    If line.StartsWith("""") Then
                        'series name is enclosed in quotes
                        pattern = "^""(?<name>[^""]+)""(?<options>:(?<optionstring>.+))?$"
                    Else
                        'no quotes around series name
                        pattern = "^(?<name>[^:]+)(?<options>:(?<optionstring>.+))?$"
                    End If
                    Dim m As Match = Regex.Match(line, pattern)
                    If m.Success Then
                        seriesName = m.Groups("name").Value.Trim()
                        'check for additional series options
                        'by default, series options are nothing
                        Dim seriesOptions As New seriesOption With {
                            .title = Nothing,
                            .unit = Nothing,
                            .color = Nothing,
                            .linestyle = Nothing,
                            .linewidth = Nothing,
                            .interpretation = Nothing
                        }
                        If m.Groups("options").Success Then
                            'parse series options
                            Dim optionString As String = m.Groups("optionstring").Value
                            If Not optionString.Contains("=") Then
                                'title only
                                seriesOptions.title = optionString.Replace("""", "").Trim() 'remove any quotes around title
                            Else
                                'keyword options, comma-separated, values may be quoted
                                Dim matches As MatchCollection = Regex.Matches(optionString, "(?<key>[^"",=]+)\s?=\s?(?<value>(?<q>"").+?(?<-q>"")|[^"",=]+),?")
                                For Each m In matches
                                    Dim keyword As String = m.Groups("key").Value.ToLower().Trim()
                                    Dim value As String = m.Groups("value").Value.Replace("""", "").Trim() 'values are case-sensitive!
                                    Select Case keyword
                                        Case "title"
                                            seriesOptions.title = value
                                        Case "unit"
                                            seriesOptions.unit = value
                                        Case "color"
                                            seriesOptions.color = value
                                        Case "linestyle"
                                            seriesOptions.linestyle = value
                                        Case "linewidth"
                                            seriesOptions.linewidth = value
                                        Case "interpretation"
                                            seriesOptions.interpretation = value
                                        Case "showpoints"
                                            seriesOptions.showpoints = value
                                        Case Else
                                            Log.AddLogEntry(levels.warning, $"Series import option keyword {keyword} not recognized!")
                                    End Select
                                Next
                            End If
                        End If
                        'add series to fileDict
                        If fileDict.ContainsKey(file) Then
                            If Not fileDict(file).ContainsKey(seriesName) Then
                                fileDict(file).Add(seriesName, seriesOptions)
                            Else
                                Log.AddLogEntry(Log.levels.warning, $"Series {seriesName} is specified more than once, only the first mention will be processed!")
                            End If
                        Else
                            Log.AddLogEntry(Log.levels.warning, $"Series {seriesName} is not associated with a file and will be ignored!")
                        End If
                    Else
                        Log.AddLogEntry(Log.levels.warning, $"Unable to parse series definition 'series={line}', this series will be ignored!")
                    End If

                ElseIf line.Contains("=") Then
                    'file import settings
                    Dim key, value As String
                    parts = line.Trim().Split("=".ToCharArray(), 2)
                    key = parts(0).Trim()
                    value = parts(1).Trim()
                    'add setting to fileDict
                    If fileDict.ContainsKey(file) Then
                        If Not settingsDict.ContainsKey(file) Then
                            settingsDict.Add(file, New Dictionary(Of String, String))
                        End If
                        If Not settingsDict(file).ContainsKey(key) Then
                            settingsDict(file).Add(key, value)
                        Else
                            Log.AddLogEntry(Log.levels.warning, $"Setting {key} is specified more than once, only the first mention will be processed!")
                        End If
                    Else
                        Log.AddLogEntry(Log.levels.warning, $"Setting {key} is not associated with a file and will be ignored!")
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
            Dim series As Dictionary(Of String, seriesOption)
            Dim seriesNotFound As List(Of String)
            Dim fileInstance As TimeSeriesFile

            Dim tsList As New List(Of TimeSeries)

            'loop over file list
            For Each file As String In fileDict.Keys

                Log.AddLogEntry(Log.levels.info, $"Reading file {file} ...")

                'get an instance of the file
                fileInstance = TimeSeriesFile.getInstance(file)

                'apply custom file import settings
                If settingsDict.ContainsKey(file) Then
                    For Each key As String In settingsDict(file).Keys
                        value = settingsDict(file)(key)
                        Try
                            Select Case key.ToLower()
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
                                    Log.AddLogEntry(Log.levels.warning, $"Setting '{key}' was not recognized and was ignored!")
                            End Select
                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.warning, $"Setting '{key}' with value '{value}' could not be parsed and was ignored!")
                        End Try
                    Next
                    'reread columns with new settings
                    fileInstance.readSeriesInfo()
                End If

                'get the series to be imported
                series = fileDict(file)

                'select series for importing
                If series.Count = 0 Then
                    'read all series contained in the file
                    Call fileInstance.selectAllSeries()
                Else
                    'loop over series names
                    seriesNotFound = New List(Of String)
                    For Each seriesName As String In series.Keys
                        found = fileInstance.selectSeries(seriesName)
                        If Not found Then
                            seriesNotFound.Add(seriesName)
                        End If
                    Next
                    'remove series that were not found from the dictionary
                    For Each seriesName As String In seriesNotFound
                        series.Remove(seriesName)
                    Next
                    'if no series remain to be imported, abort reading the file altogether
                    If series.Count = 0 Then
                        Log.AddLogEntry(Log.levels.error, "No series left to import, skipping file!")
                        Continue For
                    End If

                End If

                'read the file
                fileInstance.readFile()

                'Log
                Call Log.AddLogEntry(Log.levels.info, $"File '{file}' imported successfully!")

                'store the series
                For Each ts As TimeSeries In fileInstance.TimeSeries.Values
                    'set series options
                    If series.ContainsKey(ts.Title) Then
                        Dim seriesOptions As seriesOption = series(ts.Title)
                        'options affecting the time series itself
                        If Not IsNothing(seriesOptions.title) Then
                            ts.Title = seriesOptions.title
                        End If
                        If Not IsNothing(seriesOptions.unit) Then
                            ts.Unit = seriesOptions.unit
                        End If
                        If Not IsNothing(seriesOptions.interpretation) Then
                            If Not [Enum].IsDefined(GetType(TimeSeries.InterpretationEnum), seriesOptions.interpretation) Then
                                Log.AddLogEntry(levels.warning, $"Interpretation {seriesOptions.interpretation} is not recognized!")
                            Else
                                ts.Interpretation = [Enum].Parse(GetType(TimeSeries.InterpretationEnum), seriesOptions.interpretation)
                            End If
                        End If
                        'display options
                        If Not IsNothing(seriesOptions.color) Then
                            ts.DisplayOptions.SetColor(seriesOptions.color)
                        End If
                        If Not IsNothing(seriesOptions.linestyle) Then
                            ts.DisplayOptions.SetLineStyle(seriesOptions.linestyle)
                        End If
                        If Not IsNothing(seriesOptions.linewidth) Then
                            ts.DisplayOptions.SetLineWidth(seriesOptions.linewidth)
                        End If
                        If Not IsNothing(seriesOptions.showpoints) Then
                            ts.DisplayOptions.SetShowPoints(seriesOptions.showpoints)
                        End If
                    End If
                    tsList.Add(ts)
                Next
            Next

            Return tsList

        End Function

        ''' <summary>
        ''' Write a Wave project file
        ''' </summary>
        ''' <remarks>Only Timeseries with `.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport` will be saved</remarks>
        ''' <param name="tsList">List of Timeseries to save</param>
        ''' <param name="file">Path to the wvp file to write</param>
        ''' <param name="saveTitle">Whether to save titles</param>
        ''' <param name="saveUnit">Whether to save units</param>
        ''' <param name="saveInterpretation">Whether to save interpretations</param>
        ''' <param name="saveColor">Whether to save line colors</param>
        ''' <param name="saveLineStyle">Whether to save line styles</param>
        ''' <param name="saveLineWidth">Whether to save line widths</param>
        ''' <param name="savePointsVisibility">Whether to save points visibility</param>
        Public Shared Sub Write_File(ByRef tsList As List(Of TimeSeries), file As String,
                                     Optional saveTitle As Boolean = False,
                                     Optional saveUnit As Boolean = False,
                                     Optional saveInterpretation As Boolean = False,
                                     Optional saveColor As Boolean = False,
                                     Optional saveLineStyle As Boolean = False,
                                     Optional saveLineWidth As Boolean = False,
                                     Optional savePointsVisibility As Boolean = False
            )

            'check whether there are any series with a file datasource at all
            Dim haveFileDatasources As Boolean = False
            For Each ts As TimeSeries In tsList
                If ts.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport Then
                    haveFileDatasources = True
                    Exit For
                End If
            Next
            If Not haveFileDatasources Then
                Dim msg As String = $"None of the series originate from a file import! No project file was saved! Save the chart with data or export the time series to preserve them!"
                Log.AddLogEntry(Log.levels.error, msg)
                Throw New Exception(msg)
            End If

            'keep a list of series that could not be saved
            Dim unsavedSeries As New List(Of String)

            'write the project file
            Dim fs As New IO.FileStream(file, IO.FileMode.Create, IO.FileAccess.Write)
            Dim strwrite As New IO.StreamWriter(fs, Text.Encoding.UTF8)

            strwrite.WriteLine("# Wave project file")

            'loop over series and save to file
            Dim filePath As String = ""
            For Each ts As TimeSeries In tsList
                If Not ts.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport Then
                    unsavedSeries.Add(ts.Title)
                    Log.AddLogEntry(Log.levels.warning, $"Series '{ts.Title}' with datasource {ts.DataSource} does not originate from a file import and could not be saved to the project file!")
                Else
                    If ts.DataSource.FilePath <> filePath Then
                        'write file path
                        'TODO: write relative paths to the project file?
                        filePath = ts.DataSource.FilePath
                        strwrite.WriteLine("file=" & filePath)
                    End If
                    'write series name
                    Dim line As String
                    Dim seriesName As String = ts.DataSource.Title
                    If seriesName.Contains(":") Then
                        'enclose series names containing ":" in quotes
                        seriesName = $"""{seriesName}"""
                    End If
                    line = $"    series={seriesName}"

                    'options
                    Dim options As New List(Of String)
                    'series options
                    If saveTitle Then
                        options.Add($"title=""{ts.Title}""")
                    End If
                    If saveUnit Then
                        options.Add($"unit=""{ts.Unit}""")
                    End If
                    If saveInterpretation Then
                        options.Add($"interpretation={ts.Interpretation}")
                    End If
                    'display options
                    If saveColor Then
                        options.Add($"color={ts.DisplayOptions.Color.Name}")
                    End If
                    If saveLineWidth Then
                        options.Add($"linewidth={ts.DisplayOptions.LineWidth}")
                    End If
                    If saveLineStyle Then
                        options.Add($"linestyle={ts.DisplayOptions.LineStyle}")
                    End If
                    If savePointsVisibility Then
                        options.Add($"showpoints={ts.DisplayOptions.ShowPoints}")
                    End If

                    If options.Count > 0 Then
                        line &= ": " & String.Join(", ", options)
                    End If
                    strwrite.WriteLine(line)
                End If
            Next

            strwrite.Close()
            fs.Close()

            If unsavedSeries.Count = 0 Then
                Log.AddLogEntry(Log.levels.info, $"Wave project file {file} saved.")
            Else
                Dim msg As String = $"Wave project file {file} saved. {unsavedSeries.Count} series could not be saved! Save the chart with data or export the time series to preserve them!"
                Log.AddLogEntry(Log.levels.warning, msg)
                Throw New Exception(msg)
            End If

        End Sub

    End Class

End Namespace