﻿'Copyright (c) BlueM Dev Group
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

                    Dim fileargs As List(Of String) = args.Skip(1).ToList

                    If fileargs.Count < 1 Then
                        Throw New Exception("Too few arguments for -import!")
                    End If

                    showWave = True

                    'Import
                    ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Starting import of {fileargs.Count} files...")
                    For Each file_in As String In fileargs
                        Call Wave.Import_File(file_in)
                        ConsoleOutputLog()
                    Next

                Case "-convert"

                    Dim fileargs As List(Of String) = args.Skip(1).ToList

                    Dim interactive As Boolean = False
                    If args(1).ToLower = "-i" Then
                        interactive = True
                        fileargs = fileargs.Skip(1).ToList
                    End If

                    If fileargs.Count < 2 Then
                        Throw New Exception("Too few arguments for -convert!")
                    End If

                    showWave = False

                    ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Starting conversion to CSV ...")

                    Dim files_in As List(Of String) = fileargs.Take(fileargs.Count - 1).ToList
                    Dim file_out As String = fileargs.Last

                    'Import
                    ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Importing {fileargs.Count} file(s)...")

                    Dim tsList As New List(Of TimeSeries)

                    Dim fileInstance As FileFormatBase
                    For Each file_in As String In files_in
                        ConsoleAddLog(BlueM.Wave.Log.levels.info, $"Importing file {file_in}...")
                        Select Case IO.Path.GetExtension(file_in).ToUpper

                            Case FileExtTEN
                                Throw New NotImplementedException("TEN files are currently not supported in the CLI!")

                            Case FileExtWVP
                                Dim wvp As New WVP(file_in)
                                Dim wvpSeries As List(Of TimeSeries) = wvp.Process()
                                ConsoleOutputLog()
                                ConsoleAddLog(Log.levels.info, $"Imported {wvpSeries.Count} time series")
                                tsList.AddRange(wvpSeries)

                            Case Else
                                fileInstance = FileFactory.getFileInstance(file_in)
                                Dim isOK As Boolean
                                If interactive And fileInstance.UseImportDialog Then
                                    isOK = Wave.showImportDialog(fileInstance)
                                    If Not isOK Then
                                        ConsoleAddLog(Log.levels.warning, $"Import of file {file_in} cancelled by user, skipping this file!")
                                        Continue For
                                    End If
                                Else
                                    fileInstance.selectAllSeries()
                                End If
                                fileInstance.readFile()
                                ConsoleOutputLog()
                                ConsoleAddLog(Log.levels.info, $"Imported {fileInstance.FileTimeSeries.Count} time series")
                                For Each ts As TimeSeries In fileInstance.FileTimeSeries.Values
                                    tsList.Add(ts)
                                Next
                        End Select
                        'Wave.Import_File(file_in, display:=False)
                    Next

                    'Export
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
        ConsoleOutput("-convert [-i] file1[, file2[, ...]] file_out")
        ConsoleOutput("Import one or multiple files and export them as CSV to file_out, without showing Wave")
        ConsoleOutput("The option -i (interactive) will show the Import File Dialog for files containing multiple time series.")
        ConsoleOutput("Without the option -i (non-interactive), no dialog will be shown and all series from all files will be imported.")
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
        iLogMsg = BlueM.Wave.Log.logMessages.Count
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
