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
''' <summary>
''' Wave commandline interface
''' </summary>
Friend Class CLI
    Implements IDisposable

    Private disposed As Boolean = False

    Declare Function AttachConsole Lib "kernel32.dll" (dwProcessId As Int32) As Boolean
    Declare Function AllocConsole Lib "kernel32.dll" () As Boolean
    Declare Function FreeConsole Lib "kernel32.dll" () As Boolean

    Private Shared stdOutWriter As IO.StreamWriter

    Public Sub New()

        'Create a streamwriter for stdout in case output is piped
        Dim stdOut As IO.Stream = Console.OpenStandardOutput()
        stdOutWriter = New IO.StreamWriter(stdOut)
        stdOutWriter.AutoFlush = True

        'Attach to parent console
        If Not AttachConsole(-1) Then AllocConsole()

        'subscribe to log events
        AddHandler Log.LogMsgAdded, AddressOf ConsoleAddLog

    End Sub

    ''' <summary>
    ''' Runs the CLI
    ''' </summary>
    ''' <param name="args">the CLI arguments</param>
    ''' <returns>whether to show Wave (True) or not (False) after the CLI is finished</returns>
    Public Function Run(args As List(Of String), wave As Wave) As Boolean

        Dim showWave As Boolean = False

        Dim v As Version = Reflection.Assembly.GetExecutingAssembly.GetName().Version()
        Dim currentVersion As New Version($"{v.Major}.{v.Minor}.{v.Build}")

        ConsoleOutput("")
        ConsoleOutput($"BlueM.Wave v{currentVersion}")
        ConsoleOutput("Copyright (C) BlueM Dev Group")
        ConsoleOutput("This program comes with ABSOLUTELY NO WARRANTY")
        ConsoleOutput("This is free software, and you are welcome to redistribute it under certain conditions")
        ConsoleOutput("See COPYING and COPYING.LESSER for details")
        ConsoleOutput("")

        Try

            Select Case args(0).ToLower

                Case "-import"

                    Dim fileargs As List(Of String) = args.Skip(1).ToList

                    If fileargs.Count < 1 Then
                        Throw New ArgumentException("Too few arguments for -import!")
                    End If

                    showWave = True

                    'Import
                    Log.AddLogEntry(BlueM.Wave.Log.levels.info, $"Starting import of {fileargs.Count} files...")
                    For Each file_in As String In fileargs
                        Call wave.Import_File(file_in)
                    Next

                Case "-convert"

                    'default options
                    Dim interactive As Boolean = False
                    Dim outputformat As TimeSeriesFile.FileTypes = TimeSeriesFile.FileTypes.CSV

                    'parse options from commandline
                    Dim i As Integer = 1
                    Dim n_option_args As Integer = 0
                    Do While i < args.Count
                        If args(i).ToLower() = "-i" Then
                            'interactive mode
                            interactive = True
                            n_option_args += 1
                        ElseIf args(i).ToLower = "-of" Then
                            'parse outputformat
                            i += 1
                            Select Case args(i).ToUpper()
                                Case "CSV"
                                    outputformat = TimeSeriesFile.FileTypes.CSV
                                Case "BIN"
                                    outputformat = TimeSeriesFile.FileTypes.BIN
                                Case "DFS0"
                                    outputformat = TimeSeriesFile.FileTypes.DFS0
                                Case Else
                                    Throw New ArgumentException($"Unrecognized output format option -of {args(i)}!")
                            End Select
                            n_option_args += 2
                        End If
                        i += 1
                    Loop

                    Dim fileargs As List(Of String) = args.Skip(1 + n_option_args).ToList

                    If fileargs.Count < 2 Then
                        Throw New ArgumentException("Too few arguments for -convert!")
                    End If

                    showWave = False

                    Log.AddLogEntry(BlueM.Wave.Log.levels.info, $"Starting conversion ...")

                    Dim files_in As List(Of String) = fileargs.Take(fileargs.Count - 1).ToList
                    Dim path_out As String = fileargs.Last

                    'Import
                    Log.AddLogEntry(BlueM.Wave.Log.levels.info, $"Importing {files_in.Count} file(s)...")

                    Dim tsList As New List(Of TimeSeries)

                    Dim fileInstance As TimeSeriesFile
                    For Each file_in As String In files_in
                        Log.AddLogEntry(BlueM.Wave.Log.levels.info, $"Importing file {file_in}...")
                        Select Case IO.Path.GetExtension(file_in).ToUpper

                            Case TimeSeriesFile.FileExtensions.TEN
                                Throw New NotImplementedException("TEN files are currently not supported in the CLI!")

                            Case TimeSeriesFile.FileExtensions.WVP
                                Dim wvp As New Fileformats.WVP(file_in)
                                Dim wvpSeries As List(Of TimeSeries) = wvp.Process()
                                Log.AddLogEntry(Log.levels.info, $"Imported {wvpSeries.Count} time series")
                                tsList.AddRange(wvpSeries)

                            Case Else
                                fileInstance = TimeSeriesFile.getInstance(file_in)
                                Dim isOK As Boolean
                                If interactive And fileInstance.UseImportDialog Then
                                    isOK = wave.ShowImportDialog(fileInstance)
                                    If Not isOK Then
                                        Log.AddLogEntry(Log.levels.warning, $"Import of file {file_in} cancelled by user, skipping this file!")
                                        Continue For
                                    End If
                                Else
                                    fileInstance.selectAllSeries()
                                End If
                                fileInstance.readFile()
                                Log.AddLogEntry(Log.levels.info, $"Imported {fileInstance.TimeSeries.Count} time series")
                                For Each ts As TimeSeries In fileInstance.TimeSeries.Values
                                    tsList.Add(ts)
                                Next
                        End Select
                    Next

                    'Export
                    If tsList.Count = 0 Then
                        Throw New Exception("No time series to export!")
                    End If

                    Log.AddLogEntry(BlueM.Wave.Log.levels.info, $"Exporting to {path_out}...")

                    Select Case outputformat
                        Case TimeSeriesFile.FileTypes.CSV
                            If IO.File.Exists(path_out) Then
                                Log.AddLogEntry(BlueM.Wave.Log.levels.warning, "Overwriting existing file!")
                            End If
                            Fileformats.CSV.Write_File(tsList, path_out)
                        Case TimeSeriesFile.FileTypes.DFS0
                            If IO.File.Exists(path_out) Then
                                Log.AddLogEntry(BlueM.Wave.Log.levels.warning, "Overwriting existing file!")
                            End If
                            Fileformats.DFS0.Write_File(tsList, path_out)
                        Case TimeSeriesFile.FileTypes.BIN
                            'treat output path as a directory and export individual files, using the title as filename
                            IO.Directory.CreateDirectory(path_out)
                            Dim filename, filepath As String
                            Dim invalidFileNameCharsPattern As String = $"[{Text.RegularExpressions.Regex.Escape(String.Join("", IO.Path.GetInvalidFileNameChars))}]"
                            For Each ts As TimeSeries In tsList
                                'generate file name from cleaned title
                                filename = Text.RegularExpressions.Regex.Replace(ts.Title, invalidFileNameCharsPattern, "_") & TimeSeriesFile.FileExtensions.BIN
                                filepath = IO.Path.Combine(path_out, filename)
                                Log.AddLogEntry(BlueM.Wave.Log.levels.info, $"Exporting to {filepath}...")
                                If IO.File.Exists(filepath) Then
                                    Log.AddLogEntry(Log.levels.warning, "Overwriting existing file!")
                                End If
                                Fileformats.BIN.Write_File(ts, filepath)
                            Next
                    End Select
                    Log.AddLogEntry(BlueM.Wave.Log.levels.info, "Finished conversion!")


                Case "-help"

                    showWave = False
                    ConsoleOutputHelp()

                Case Else

                    showWave = False
                    Throw New ArgumentException("Unknown command!")

            End Select

        Catch e As ArgumentException
            Log.AddLogEntry(BlueM.Wave.Log.levels.error, e.Message)
            ConsoleOutputHelp()

        Catch e As Exception
            Log.AddLogEntry(BlueM.Wave.Log.levels.error, e.Message)

        End Try

        Return showWave

    End Function

    ''' <summary>
    ''' Outputs the CLI help
    ''' </summary>
    Private Shared Sub ConsoleOutputHelp()
        ConsoleOutput("Wave CLI commands:")
        ConsoleOutput("")
        ConsoleOutput("-help")
        ConsoleOutput("Show this help message")
        ConsoleOutput("")
        ConsoleOutput("-import file1[, file2[, ...]]")
        ConsoleOutput("Import one or multiple files and then show Wave")
        ConsoleOutput("")
        ConsoleOutput("-convert [-i] [-of <format>] file1[, file2[, ...]] path_out")
        ConsoleOutput("Import one or multiple files and export them to path_out, without showing Wave")
        ConsoleOutput("Options:")
        ConsoleOutput("   -i: interactive mode: will show the Import File Dialog for files containing multiple time series.")
        ConsoleOutput("   -of <format>: specifies the output format (BIN, CSV or DFS0). Default is CSV.")
        ConsoleOutput("")
        ConsoleOutput("See https://wiki.bluemodel.org/index.php/Wave:CLI for more details")
    End Sub

    ''' <summary>
    ''' Adds a log entry to the console output
    ''' </summary>
    ''' <param name="level">log level</param>
    ''' <param name="msg">log message</param>
    Private Shared Sub ConsoleAddLog(level As BlueM.Wave.Log.levels, msg As String)
        ConsoleOutput($"{level.ToString().ToUpper(),-10} {msg}")
    End Sub

    ''' <summary>
    ''' Writes a message to the console output
    ''' </summary>
    ''' <param name="msg">message</param>
    Private Shared Sub ConsoleOutput(msg As String)
        Console.WriteLine(msg)
        stdOutWriter.WriteLine(msg) ' this is for when the output is being piped
    End Sub

    ''' <summary>
    ''' Dispose implementation
    ''' </summary>
    ''' <param name="disposing"></param>
    ''' <remarks>see https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose </remarks>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposed Then
            If disposing Then
                'dispose managed state (managed objects)
                ConsoleOutput("Press <Enter> to continue.")

                'unsubscribe from log events
                RemoveHandler Log.LogMsgAdded, AddressOf ConsoleAddLog

                'close stdOut streamwriter
                stdOutWriter.Close()

                'detach from console
                FreeConsole()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposed = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
