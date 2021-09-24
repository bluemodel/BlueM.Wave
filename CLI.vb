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
''' <summary>
''' Wave commandline interface
''' </summary>
Public Class CLI

    Declare Function AttachConsole Lib "kernel32.dll" (ByVal dwProcessId As Int32) As Boolean
    Declare Function AllocConsole Lib "kernel32.dll" () As Boolean
    Declare Function FreeConsole Lib "kernel32.dll" () As Boolean

    Private Shared stdOutWriter As IO.StreamWriter
    Private Shared iLogMsg As Integer = 0

    Shared Sub New()

        'Create a streamwriter for stdout in case output is piped
        Dim stdOut As IO.Stream = Console.OpenStandardOutput()
        stdOutWriter = New IO.StreamWriter(stdOut)
        stdOutWriter.AutoFlush = True

        'Attach to parent console
        If Not AttachConsole(-1) Then AllocConsole()

        'Initialise the central log instance
        Wave.logInstance = BlueM.Wave.Log.getInstance()

    End Sub

    ''' <summary>
    ''' Runs the CLI
    ''' </summary>
    ''' <param name="args">the CLI arguments</param>
    ''' <returns>whether to show Wave (True) or not (False) after the CLI is finished</returns>
    Public Shared Function Run(args As List(Of String)) As Boolean

        Dim showWave As Boolean = False

        ConsoleOutput("")

        Try

            Select Case args(0).ToLower

                Case "-import"

                    If args.Count < 2 Then
                        Throw New Exception("Too few arguments for -import!")
                    End If

                    showWave = True

                    'Import
                    ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Attempting to import {args.Count - 1} files...")
                    For i As Integer = 1 To args.Count - 1
                        Call Wave.Import_File(args(i))
                        ConsoleOutputLog()
                    Next

                Case "-convert"

                    If args.Count < 3 Then
                        Throw New Exception("Too few arguments for -convert!")
                    End If

                    showWave = False

                    ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Starting conversion to CSV ...")

                    Dim n_files_in As Integer = args.Count - 2
                    Dim files_in As List(Of String) = args.Skip(1).Take(n_files_in).ToList()
                    Dim file_out As String = args.Last

                    'Import
                    'TODO: Import_File() also adds imported time series to the chart, which we technically do not need here.
                    'But this method is currently the only one that also supports WVP files, which we want to support here.
                    ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Importing {n_files_in} file(s)...")

                    For Each file_in As String In files_in
                        Wave.Import_File(file_in)
                        ConsoleOutputLog()
                    Next

                    'Export
                    Dim tsList As List(Of TimeSeries) = Wave.TimeSeriesDict.Values.ToList
                    If tsList.Count = 0 Then
                        Throw New Exception("No time series to export!")
                    End If

                    ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Exporting as CSV to {file_out}...")
                    If IO.File.Exists(file_out) Then
                        ConsoleAddLog(Log.levels.warning, "Overwriting existing file!")
                    End If

                    CSV.Write_File(tsList, file_out)
                    ConsoleOutputLog()

                    ConsoleAddLog(BlueM.Wave.Log.levels.info, "Finished conversion to CSV!")

                Case "-help"

                    showWave = False
                    ConsoleOutputHelp()

                Case Else

                    showWave = False
                    ConsoleAddLog(BlueM.Wave.Log.levels.error, "Unknown command!")
                    ConsoleOutputHelp()

            End Select

        Catch e As Exception
            ConsoleAddLog(BlueM.Wave.Log.levels.error, e.Message)

        Finally
            ConsoleOutput("Press <Enter> to continue.")
            FreeConsole()
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
        ConsoleOutput("-convert file1[, file2[, ...]] file_out")
        ConsoleOutput("Import one or multiple files and export them as CSV to file_out, without showing Wave")
        ConsoleOutput("")
        ConsoleOutput("See https://wiki.bluemodel.org/index.php/Wave:CLI for more details")
    End Sub

    ''' <summary>
    ''' Adds any new log messages from the central log instance to the console output
    ''' </summary>
    Private Shared Sub ConsoleOutputLog()
        For i As Integer = iLogMsg To BlueM.Wave.Log.logMessages.Count - 1
            ConsoleAddLog(BlueM.Wave.Log.logMessages(i).Key, BlueM.Wave.Log.logMessages(i).Value)
        Next
        iLogMsg = BlueM.Wave.Log.logMessages.Count - 1
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

End Class
