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

                        If Not IO.File.Exists(file) Then
                            'A WEL/WBL file may be zipped within a WLZIP file, so try extracting it from there
                            If Fileformats.WEL.extractFromWLZIP(file) Then
                                'TODO #136: we should ideally clean up the extracted file later
                            End If
                        End If

                        'convert interpretation value to string
                        Dim interpretationValue As Integer = Integer.Parse(params("Interpretation"))
                        Dim interpretationString As String = [Enum].GetName(GetType(TimeSeries.InterpretationEnum), interpretationValue)

                        Dim fileRef As New FileReference() With {
                            .path = file,
                            .series = New Dictionary(Of String, SeriesOptions)() From {
                                {name, New SeriesOptions() With {.interpretation = interpretationString}}
                            }
                        }
                        FileReferences.Add(fileRef)

                    Case "99" 'BIN file

                        If Not params.ContainsKey("Einheit") Then
                            Throw New Exception("Missing required entry 'Einheit' in clipboard content!")
                        End If

                        name = params("Kennung")

                        'convert interpretation value to string
                        Dim interpretationValue As Integer = Integer.Parse(params("Interpretation"))
                        Dim interpretationString As String = [Enum].GetName(GetType(TimeSeries.InterpretationEnum), interpretationValue)

                        Dim fileRef As New FileReference() With {
                            .path = file,
                            .series = New Dictionary(Of String, SeriesOptions)() From {
                                {
                                    IO.Path.GetFileName(file), New SeriesOptions() With {
                                        .title = name,
                                        .unit = params("Einheit"),
                                        .interpretation = interpretationString
                                    }
                                }
                            }
                        }
                        FileReferences.Add(fileRef)

                    Case Else
                        Throw New Exception($"Unsupported value {params("ZRFormat")} for ZRFormat!")

                End Select

            Next

        End Sub

    End Class

End Namespace