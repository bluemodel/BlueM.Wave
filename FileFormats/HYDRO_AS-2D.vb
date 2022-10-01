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
    ''' Klasse für Q_Strg.dat, Pegel.dat und BW_TMP.dat von HYDRO_AS-2D
    ''' </summary>
    Public Class HYDRO_AS_2D
        Inherits FileFormatBase

        ''' <summary>
        ''' Die Einheit der Zeitreihen
        ''' </summary>
        ''' <remarks>Ist für jede Art von Datei fest vorgegeben</remarks>
        Private _einheit As String

        ''' <summary>
        ''' HYDRO_AS-2D Version 
        ''' </summary>
        ''' <remarks>is 2 by default, in version 5 the Q_Strg.dat format has changed</remarks>
        Private _HYDROAS_version As Integer

        ''' <summary>
        ''' Referenzdatum für den Beginn der Simulation
        ''' </summary>
        ''' <remarks>default: 01.01.2000 00:00:00</remarks>
        Public refDate As DateTime

        ''' <summary>
        ''' Ob der Importdialog genutzt werden soll
        ''' </summary>
        ''' <value></value>
        ''' <returns>True</returns>
        ''' <remarks></remarks>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Instanziert eine neue HYDRO-AS_2D-Ergebnisdatei
        ''' </summary>
        ''' <param name="file">Pfad zur Datei</param>
        ''' <remarks></remarks>
        Public Sub New(file As String)

            Call MyBase.New(file)

            'assume HYDRO_AS-2D version 2 by default
            Me._HYDROAS_version = 2

            'default settings apply to HYDRO_AS-2D version 2 
            Me.iLineData = 9
            Me.iLineHeadings = 4
            Me.UseUnits = False
            Me.IsColumnSeparated = True
            Me.Separator = New BlueM.Wave.Character(" ")
            Me.DateTimeColumnIndex = 0

            'Einheiten anhand des Dateinamens festlegen
            Select Case Path.GetFileName(file).ToLower
                Case "q_strg.dat"
                    Me._einheit = "m³/s"
                Case "pegel.dat"
                    Me._einheit = "müNN"
                Case "bw_tmp.dat"
                    Me._einheit = "m³/s"
                Case Else
                    Me._einheit = "-"
            End Select

            'Default Referenzdatum für den Beginn der Simulation
            Me.refDate = New DateTime(2000, 1, 1, 0, 0, 0)

            Call Me.readSeriesInfo()

        End Sub

        ''' <summary>
        ''' Prüft, ob es sich um eine Ergebnisdatei von HYDRO_AS-2D handelt
        ''' </summary>
        ''' <param name="file">Pfad zur Datei</param>
        ''' <returns>Boolean</returns>
        ''' <remarks>Prüfung erfolgt anhand des Dateinamens (Q_Strg.dat, Pegel.dat oder BW_TMP.dat)</remarks>
        Public Shared Function verifyFormat(file As String) As Boolean

            'TODO: Prüfung etwas robuster machen

            Dim filename As String = Path.GetFileName(file).ToLower()
            If (filename = "q_strg.dat" Or filename = "pegel.dat" Or filename = "bw_tmp.dat") Then
                Return True
            Else
                Return False
            End If

        End Function

        ''' <summary>
        ''' Liest die Anzahl der Spalten und ihre Namen aus
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readSeriesInfo()

            Dim i, l As Integer
            Dim sInfo As SeriesInfo
            Dim Zeile As String = ""
            Dim ZeileSpalten As String = ""

            Me.SeriesList.Clear()

            Try
                'Datei öffnen
                Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                'Determine HYDRO_AS-2D version for Q_Strg.dat
                If Path.GetFileName(Me.File).ToLower() = "q_strg.dat" Then
                    'If the 5th line starts with "Name:", then it is HYDROAS-2D version 5 format
                    For i = 1 To 5
                        Zeile = StrReadSync.ReadLine.ToString()
                    Next
                    If Zeile.Trim.StartsWith("Name:") Then
                        Me._HYDROAS_version = 5
                        'use names instead of node numbers as series titles
                        Me.iLineHeadings = 5
                    End If
                    'return to beginning of file
                    FiStr.Seek(0, SeekOrigin.Begin)
                    StrRead = New StreamReader(FiStr, Me.Encoding)
                    StrReadSync = TextReader.Synchronized(StrRead)
                End If

                Select Case Path.GetFileName(Me.File).ToLower()

                    Case "q_strg.dat", "pegel.dat"

                        'Zeile mit Spaltenüberschriften lesen
                        For i = 1 To Me.iLineHeadings
                            Zeile = StrReadSync.ReadLine.ToString
                            If (i = Me.iLineHeadings) Then ZeileSpalten = Zeile
                        Next

                        'Spaltennamen auslesen
                        Dim anzSpalten As Integer
                        Dim Namen(0) As String

                        Select Case Me._HYDROAS_version
                            Case 2
                                'space separated names
                                Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar}, System.StringSplitOptions.RemoveEmptyEntries)
                                anzSpalten = Namen.Length + 1 'X-Spalte künstlich dazuzählen, da ohne Namen
                            Case 5
                                'names in columns of equal width
                                ZeileSpalten = ZeileSpalten.Substring(14) 'start from column 14
                                'split into equal lengths
                                l = 13
                                anzSpalten = ZeileSpalten.Length / l
                                ReDim Namen(anzSpalten - 1)
                                For i = 0 To anzSpalten - 1
                                    Namen(i) = ZeileSpalten.Substring(i * l, l).Trim()
                                Next
                                anzSpalten += 1 'X-Spalte künstlich dazuzählen, da ohne Namen
                        End Select

                        'store series info

                        For i = 0 To Namen.Length - 1
                            sInfo = New SeriesInfo
                            sInfo.Name = Namen(i).Trim()
                            sInfo.Unit = Me._einheit
                            sInfo.Index = i + 1
                            Me.SeriesList.Add(sInfo)
                        Next

                    Case "bw_tmp.dat"

                        Dim parts As String()
                        Dim names As New Collections.Generic.List(Of String)

                        'Zur ersten Datenzeile springen
                        For i = 1 To 4
                            Zeile = StrReadSync.ReadLine.ToString
                        Next

                        'Zeitreihen-Namen sind Kombination aus Spalte IJBW_Seg-1 und IJBW_Seg-2
                        Do
                            parts = Zeile.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                            names.Add($"{parts(0)}-{parts(1)}")
                            Zeile = StrReadSync.ReadLine.ToString().Trim()
                        Loop Until Zeile.StartsWith("---")

                        'store series info

                        For i = 0 To names.Count - 1
                            sInfo = New SeriesInfo
                            sInfo.Name = names(i).Trim()
                            sInfo.Unit = Me._einheit
                            sInfo.Index = i + 1 ' hier eigentlich irrelevant
                            Me.SeriesList.Add(sInfo)
                        Next

                End Select

                StrReadSync.Close()
                StrRead.Close()
                FiStr.Close()

            Catch ex As Exception
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
            End Try


        End Sub

        ''' <summary>
        ''' Liest die Datei ein
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readFile()

            Dim i As Integer
            Dim Zeile As String = ""
            Dim datum As DateTime
            Dim Werte() As String
            Dim ts As TimeSeries

            'show dialog for setting the reference date
            Dim dlg As New HYDRO_AS_2D_Diag()
            dlg.ShowDialog()
            'store refDate
            Me.refDate = dlg.DateTimePicker_refDate.Value

            Try
                'Instantiate time series
                For Each sInfo As SeriesInfo In Me.SelectedSeries
                    ts = New TimeSeries(sInfo.Name)
                    ts.Unit = sInfo.Unit
                    ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                    Me.FileTimeSeries.Add(sInfo.Index, ts)
                Next

                'Datei öffnen
                Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                'Einlesen
                '--------

                Select Case Path.GetFileName(Me.File).ToLower()

                    Case "q_strg.dat", "pegel.dat"

                        'Header
                        For i = 0 To Me.nLinesHeader - 1
                            StrReadSync.ReadLine()
                        Next

                        'Daten
                        Do
                            Zeile = StrReadSync.ReadLine.ToString()
                            Werte = Zeile.Split(New Char() {Me.Separator.ToChar}, System.StringSplitOptions.RemoveEmptyEntries)
                            'Simulationszeit [h] wird zu Datum nach dem Referenzdatum (default: 01.01.2000 00:00:00) konvertiert
                            datum = Me.refDate + New TimeSpan(0, 0, Helpers.StringToDouble(Werte(0)) * 3600)
                            For Each sInfo As SeriesInfo In Me.SelectedSeries
                                Me.FileTimeSeries(sInfo.Index).AddNode(datum, Helpers.StringToDouble(Werte(sInfo.Index)))
                            Next

                        Loop Until StrReadSync.Peek() = -1

                    Case "bw_tmp.dat"

                        Dim zeit, value As Double
                        Dim parts(), name As String

                        Do
                            Zeile = StrReadSync.ReadLine.ToString().Trim()
                            If Zeile.Length = 0 Then Continue Do
                            'Headerzeilen
                            If Zeile.StartsWith("---") Then Continue Do
                            If Zeile.StartsWith("Abflu") Then
                                'Zeit lesen
                                zeit = Helpers.StringToDouble(Zeile.Split(New Char() {"="}, StringSplitOptions.RemoveEmptyEntries)(1).Replace("[Sek]", "").Trim())
                                'Simulationszeit [s] wird zu Datum nach 01.01.2000 00:00:00 konvertiert
                                datum = Me.refDate + New TimeSpan(0, 0, zeit)
                                Continue Do
                            End If
                            If Zeile.StartsWith("IJBW_Seg-1") Then Continue Do
                            'Datenzeilen
                            parts = Zeile.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                            name = $"{parts(0)}-{parts(1)}"
                            value = Helpers.StringToDouble(parts(2))
                            'nur ausgewählte Reihen abspeichern
                            For Each sInfo As SeriesInfo In Me.SelectedSeries
                                If sInfo.Name = name Then
                                    Me.FileTimeSeries(sInfo.Index).AddNode(datum, value)
                                    Exit For
                                End If
                            Next

                        Loop Until StrReadSync.Peek() = -1

                End Select

                StrReadSync.Close()
                StrRead.Close()
                FiStr.Close()

            Catch ex As Exception
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
            End Try

        End Sub


    End Class

End Namespace