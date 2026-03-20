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

Namespace Parsers

    ''' <summary>
    ''' Class for parsing Talsim clipboard content
    ''' </summary>
    Public Class TalsimClipboard
        Inherits Parser

        Public Sub New(clipboardtext As String)
            MyBase.New(inputtext:=clipboardtext)
        End Sub

        Public Overloads Shared Function verifyFormat(clipboardtext As String) As Boolean
            'check if content contains expected header
            If clipboardtext.Contains("SydroTyp=SydroErgZre") Or
               clipboardtext.Contains("SydroTyp=SydroBinZre") Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Parses the clipboard content
        ''' </summary>
        Protected Overrides Sub Parse()

            'Examples:

            '[SETTINGS]
            'Count = 1
            '[Zeitreihe1]
            'SydroTyp=SydroErgZre
            'ZRFormat=4
            'ID=362
            'Extension=.WEL
            'Kennung=S000
            'KennungLang={S000} {1AB, HYO} Ablauf_1
            'Zustand=1AB
            'Datei=D:\Talsim-NG\customers\WVER\projectData\felix\dataBase\Felix_data\00000362.WEL
            'GeaendertAm=
            'Modell=TALSIM
            'Herkunft=simuliert
            'Interpretation=2
            'SimVariante=Test Langzeit/HWMerkmal_HWGK_v02
            'Simulation=Test Langzeit
            'Einheit=m3/s
            'EndZeitreihe

            '[SETTINGS]
            'Count=2
            '[Zeitreihe1]
            'SydroTyp=SydroBinZre
            'ZRFormat=99
            'ID=1041
            'Extension=.BIN
            'Kennung=Sce10, E038, C38 (TA_Tekeze TK 04B)
            'KennungLang=Sce10, E038, C38 (TA_Tekeze TK 04B), m3/s
            'Datei=C:\Talsim-NG\customers\Nile\projectData\hubert\dataBase\hubert_zre\00001041.BIN
            'Einheit=m3/s
            'Modell=TALSIM
            'Interpretation=1
            'EndZeitreihe
            '[Zeitreihe2]
            'SydroTyp=SydroBinZre
            'ZRFormat=99
            'ID=1042
            'Extension=.BIN
            'Kennung=Sce10, E039, C39 (TA_TK5)
            'KennungLang=Sce10, E039, C39 (TA_TK5), m3/s
            'Datei=C:\Talsim-NG\customers\Nile\projectData\hubert\dataBase\hubert_zre\00001042.BIN
            'Einheit=m3/s
            'Modell=TALSIM
            'Interpretation=1
            'EndZeitreihe

            'parse clipboard contents
            Dim m As Match
            Dim i_series As Integer
            Dim parts() As String
            Dim zreblock As Boolean
            Dim data As New List(Of Dictionary(Of String, String)) '[{zreparams1},{zreparams2},...]
            Dim file, name As String

            zreblock = False
            For Each line As String In InputText.Split(eol)

                line = line.Trim()

                m = Regex.Match(line, "\[Zeitreihe(\d+)\]")
                If m.Success Then
                    i_series = m.Groups(1).Value
                    data.Add(New Dictionary(Of String, String))
                    zreblock = True
                End If

                If zreblock Then
                    If line.Contains("=") Then
                        parts = line.Split("=")
                        data(i_series - 1).Add(parts(0), parts(1))
                    ElseIf line = "EndZeitreihe" Then
                        zreblock = False
                        Continue For
                    End If
                End If

            Next

            If data.Count = 0 Then
                Throw New Exception("No series could be parsed from TALSIM clipboard content!")
            End If

            'initiate parsing of series
            For Each params As Dictionary(Of String, String) In data

                ' check all required parameters are present
                Dim expectedKeys As New List(Of String) From {
                    "Datei",
                    "ZRFormat",
                    "Kennung",
                    "Interpretation"
                }
                For Each key As String In expectedKeys
                    If Not params.ContainsKey(key) Then
                        Throw New Exception($"Missing required entry '{key}' in clipboard content!")
                    End If
                Next

                file = params("Datei")

                Select Case params("ZRFormat")
                    Case "4" 'WEL file

                        If Not params.ContainsKey("Zustand") Then
                            Throw New Exception("Missing required entry 'Zustand' in clipboard content!")
                        End If

                        'build series name
                        If params("Kennung") = "ZPG" Then
                            'handle control groups
                            name = "KGRP_" & params("Zustand")
                        Else
                            name = params("Kennung").PadRight(4, " ") & "_" & params("Zustand")
                        End If

                        Dim options As New SeriesOptions() With {
                            .title = name,
                            .interpretation = Helpers.ParseInterpretation(Integer.Parse(params("Interpretation")))
                        }
                        If params.ContainsKey("Einheit") Then
                            options.unit = params("Einheit")
                        End If

                        Dim fileRef As New FileReference() With {
                            .path = file,
                            .series = New Dictionary(Of String, SeriesOptions)() From {
                                {name, options}
                            }
                        }
                        FileReferences.Add(fileRef)

                    Case "99" 'BIN file

                        If Not params.ContainsKey("Einheit") Then
                            Throw New Exception("Missing required entry 'Einheit' in clipboard content!")
                        End If

                        name = params("Kennung")

                        Dim options As New SeriesOptions() With {
                            .title = name,
                            .unit = params("Einheit"),
                            .interpretation = Helpers.ParseInterpretation(Integer.Parse(params("Interpretation")))
                        }

                        Dim fileRef As New FileReference() With {
                            .path = file,
                            .series = New Dictionary(Of String, SeriesOptions)() From {
                                {
                                    IO.Path.GetFileName(file), options
                                }
                            }
                        }
                        FileReferences.Add(fileRef)

                    Case Else
                        Throw New Exception($"Unsupported value {params("ZRFormat")} for ZRFormat!")

                End Select

            Next

        End Sub

        ''' <summary>
        ''' Wraps the base Process() method to check for WEL/WBL files that do not exist
        ''' and attempt to extract them from WLZIP files if possible before processing,
        ''' and then deletes any extracted files after processing
        ''' </summary>
        ''' <returns>A list of processed time series</returns>
        Public Overloads Function Process() As List(Of TimeSeries)

            'check for WEL/WBL that do not exist and try to extract them from WLZIP files if possible
            Dim extractedFiles As New List(Of String)
            For Each fileRef As FileReference In FileReferences
                If Not IO.File.Exists(fileRef.path) Then
                    Dim fileExt As String = IO.Path.GetExtension(fileRef.path).ToUpper()
                    If fileExt = TimeSeriesFile.FileExtensions.WEL Or fileExt = TimeSeriesFile.FileExtensions.WBL Then
                        If TalsimClipboard.extractFromWLZIP(fileRef.path) Then
                            'remember the file so that we can delete it later
                            extractedFiles.Add(fileRef.path)
                        End If
                    End If
                End If
            Next

            'process the files as usual
            Dim tsList As List(Of TimeSeries) = MyBase.Process()

            'delete any extracted files
            For Each file As String In extractedFiles
                Try
                    IO.File.Delete(file)
                Catch ex As Exception
                    Log.AddLogEntry(Log.levels.warning, $"Could not delete extracted file {file}: {ex.Message}")
                End Try
            Next

            Return tsList

        End Function

        ''' <summary>
        ''' Attempts to extract a specified WEL file from a WLZIP file of the same name
        ''' </summary>
        ''' <param name="file">path to WEL file</param>
        ''' <returns>True if successful</returns>
        ''' <remarks>TALSIM specific</remarks>
        Public Shared Function extractFromWLZIP(file As String) As Boolean

            Dim file_wlzip As String
            Dim filename As String
            Dim zipEntryFound As Boolean = False
            Dim success As Boolean = False

            'determine WLZIP filename for files ending with .WEL (may also be e.g. .KTR.WEL, .CHLO.WEL, etc.)
            Dim m As Match = Regex.Match(file, "^(.+?)(\.[a-z]+)?\.WEL$", RegexOptions.IgnoreCase)
            If m.Success Then
                file_wlzip = $"{m.Groups(1)}.WLZIP"

                If IO.File.Exists(file_wlzip) Then

                    Log.AddLogEntry(Log.levels.info, $"Looking for file in {file_wlzip} ...")
                    filename = IO.Path.GetFileName(file)

                    Dim zip As IO.Compression.ZipArchive = IO.Compression.ZipFile.OpenRead(file_wlzip)

                    For Each entry As IO.Compression.ZipArchiveEntry In zip.Entries
                        If entry.Name.ToLower() = filename.ToLower() Then
                            zipEntryFound = True
                            'extract file from zip archive
                            Log.AddLogEntry(Log.levels.info, $"Extracting file from {file_wlzip} ...")
                            Dim fs As New IO.FileStream(file, IO.FileMode.CreateNew)
                            entry.Open().CopyTo(fs)
                            fs.Flush()
                            fs.Close()
                            success = True
                            Exit For
                        End If
                    Next

                    If Not zipEntryFound Then
                        Log.AddLogEntry(Log.levels.error, $"File {filename} not found in {file_wlzip}!")
                    End If

                End If
            End If

            Return success

        End Function

    End Class

End Namespace