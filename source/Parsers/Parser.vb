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
Namespace Parsers

    ''' <summary>
    ''' Base class for parsing file import instructions
    ''' </summary>
    Public MustInherit Class Parser

        ''' <summary>
        ''' A reference to a file to be imported with all information needed for importing series from that file
        ''' </summary>
        Protected Class FileReference

            ''' <summary>
            ''' The path to the file to be imported
            ''' </summary>
            Public path As String

            ''' <summary>
            ''' The series to be imported from the file with their options, where the key is the name of the series as it appears in the file and the value is a SeriesOptions object containing options for importing that series
            ''' An empty dictionary means that all series contained in the file should be imported with default options
            ''' </summary>
            Public series As Dictionary(Of String, SeriesOptions)

            ''' <summary>
            ''' File import settings as key value pairs, e.g. separator, date format, etc.
            ''' An empty dictionary means that default settings should be used for importing the file.
            ''' </summary>
            Public settings As Dictionary(Of String, String)

            Public Sub New()
                Me.series = New Dictionary(Of String, SeriesOptions)
                Me.settings = New Dictionary(Of String, String)
            End Sub

        End Class

        ''' <summary>
        ''' Holds options for importing a series, e.g. display options, custom title and unit, etc.
        ''' </summary>
        Protected Class SeriesOptions
            Public title As String
            Public unit As String
            Public interpretation As String
            Public displayOptions As TimeSeriesDisplayOptions
            Public Sub New()
                Me.displayOptions = New TimeSeriesDisplayOptions()
            End Sub
        End Class

        ''' <summary>
        ''' The path to the input file
        ''' </summary>
        Protected InputFile As String

        ''' <summary>
        ''' The input text to be parsed
        ''' </summary>
        Protected InputText As String

        Protected FileReferences As New List(Of FileReference)

        ''' <summary>
        ''' Instantiates a new Parser instance with either a file or text input and calls the Parse method to parse the input
        ''' </summary>
        ''' <param name="inputfile">The path to the input file</param>
        ''' <param name="inputtext">The input text</param>
        Public Sub New(Optional inputfile As String = Nothing, Optional inputtext As String = Nothing)
            If IsNothing(inputfile) And IsNothing(inputtext) Then
                Throw New ArgumentException("Either a file or text input must be provided!")
            ElseIf Not IsNothing(inputfile) And Not IsNothing(inputtext) Then
                Throw New ArgumentException("Only one of file or text input can be provided!")
            End If
            Me.InputFile = inputfile
            If Not IsNothing(inputfile) Then
                Me.InputText = IO.File.ReadAllText(inputfile, Text.Encoding.UTF8)
            Else
                Me.InputText = inputtext
            End If
            Call Me.Parse()
        End Sub

        Public Shared Function verifyFormat() As Boolean
            'default implementation always returns true, can be overridden in subclasses to implement specific format verification
            Return True
        End Function

        ''' <summary>
        ''' Parses the input and stores the results in the FileReferences list
        ''' </summary>
        Protected MustOverride Sub Parse()

        ''' <summary>
        ''' Processes the FileReferences list by reading the specified series from the files and returning them as a list
        ''' </summary>
        ''' <returns>a list of time series</returns>
        Public Function Process() As List(Of TimeSeries)

            Dim tsList As New List(Of TimeSeries)

            'loop over file list
            For Each fileRef As FileReference In FileReferences

                Log.AddLogEntry(Log.levels.info, $"Reading file {fileRef.path} ...")

                'get an instance of the file
                Dim fileInstance As TimeSeriesFile = TimeSeriesFile.getInstance(fileRef.path)

                'apply custom file import settings
                If fileRef.settings.Count > 0 Then
                    For Each key As String In fileRef.settings.Keys
                        Dim value As String = fileRef.settings(key)
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

                'select series for importing
                If fileRef.series.Count = 0 Then
                    'read all series contained in the file
                    Call fileInstance.selectAllSeries()
                Else
                    'loop over series names
                    Dim seriesNames As List(Of String) = fileRef.series.Keys.ToList()
                    Dim seriesNotFound As New List(Of String)
                    For Each seriesName As String In seriesNames
                        Dim found As Boolean = fileInstance.selectSeries(seriesName)
                        If Not found Then
                            seriesNotFound.Add(seriesName)
                            Log.AddLogEntry(Log.levels.error, $"Series {seriesName} not found in file, skipping series!")
                        End If
                    Next
                    'remove series that were not found from the list
                    For Each seriesName As String In seriesNotFound
                        seriesNames.Remove(seriesName)
                    Next
                    'if no series remain to be imported, abort reading the file altogether
                    If seriesNames.Count = 0 Then
                        Log.AddLogEntry(Log.levels.error, "No series left to import, skipping file!")
                        Continue For
                    End If

                End If

                'read the file
                fileInstance.readFile()

                'Log
                Call Log.AddLogEntry(Log.levels.info, $"File '{fileRef.path}' imported successfully!")

                'store the series
                For Each ts As TimeSeries In fileInstance.TimeSeries.Values
                    'set series options
                    If fileRef.series.ContainsKey(ts.Title) Then
                        Dim options As SeriesOptions = fileRef.series(ts.Title)
                        'options affecting the time series itself
                        If Not IsNothing(options.title) Then
                            ts.Title = options.title
                        End If
                        If Not IsNothing(options.unit) Then
                            ts.Unit = options.unit
                        End If
                        If Not IsNothing(options.interpretation) Then
                            If Not [Enum].IsDefined(GetType(TimeSeries.InterpretationEnum), options.interpretation) Then
                                Log.AddLogEntry(levels.warning, $"Interpretation {options.interpretation} is not recognized!")
                            Else
                                ts.Interpretation = [Enum].Parse(GetType(TimeSeries.InterpretationEnum), options.interpretation)
                            End If
                        End If
                        'display options
                        ts.DisplayOptions = options.displayOptions
                    End If
                    'store the time series in the list
                    tsList.Add(ts)
                Next
            Next

            Return tsList

        End Function

    End Class

End Namespace