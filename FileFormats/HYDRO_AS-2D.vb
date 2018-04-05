﻿'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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
    Public Sub New(ByVal file As String)

        Call MyBase.New(file)

        Me.iLineData = 9
        Me.iLineHeadings = 4
        Me.UseUnits = False
        Me.IsColumnSeparated = True
        Me.Separator = New BlueM.Wave.Character(" ")

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

        Call Me.ReadColumns()

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um eine Ergebnisdatei von HYDRO_AS-2D handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns>Boolean</returns>
    ''' <remarks>Prüfung erfolgt anhand des Dateinamens (Q_Strg.dat, Pegel.dat oder BW_TMP.dat)</remarks>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

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
    Public Overrides Sub ReadColumns()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Select Case Path.GetFileName(Me.File).ToLower()

                Case "q_strg.dat", "pegel.dat"

                    'Zeile mit Spaltenüberschriften lesen
                    For i = 1 To Me.iLineHeadings
                        Zeile = StrReadSync.ReadLine.ToString
                        If (i = Me.iLineHeadings) Then ZeileSpalten = Zeile
                    Next

                    'Spaltennamen auslesen
                    Dim anzSpalten As Integer
                    Dim Namen() As String

                    Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar}, System.StringSplitOptions.RemoveEmptyEntries)
                    anzSpalten = Namen.Length + 1 'X-Spalte künstlich dazuzählen, da ohne Namen

                    'Spalten abspeichern
                    ReDim Me.Columns(anzSpalten - 1)

                    'X-Spalte
                    Me.Columns(0).Name = "Zeit"
                    Me.Columns(0).Einheit = "h"
                    Me.Columns(0).Index = 0

                    Me.DateTimeColumnIndex = 0

                    'Y-Spalten
                    For i = 0 To Namen.Length - 1
                        Me.Columns(i + 1).Name = Namen(i).Trim()
                        Me.Columns(i + 1).Einheit = Me._einheit
                        Me.Columns(i + 1).Index = i + 1
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
                        names.Add(parts(0) & "-" & parts(1))
                        Zeile = StrReadSync.ReadLine.ToString
                    Loop Until Zeile.StartsWith(" ---")

                    'Zeitreihen-Namen abspeichern
                    ReDim Me.Columns(names.Count) '1 mehr wegen X-Spalte

                    'X-Spalte
                    Me.Columns(0).Name = "Zeit"
                    Me.Columns(0).Einheit = "s"
                    Me.Columns(0).Index = 0

                    Me.DateTimeColumnIndex = 0

                    'Y-Spalten
                    For i = 0 To names.Count - 1
                        Me.Columns(i + 1).Name = names(i).Trim()
                        Me.Columns(i + 1).Einheit = Me._einheit
                        Me.Columns(i + 1).Index = i + 1 'hier irrelevant
                    Next

            End Select

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try


    End Sub

    ''' <summary>
    ''' Liest die Datei ein
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Read_File()

        Dim i, j As Integer
        Dim Zeile As String = ""
        Dim datum As DateTime
        Dim Werte() As String

        Try
            'Anzahl Zeitreihen bestimmen
            ReDim Me.TimeSeries(Me.SelectedColumns.Length - 1)

            'Zeitreihen instanzieren
            For i = 0 To Me.SelectedColumns.Length - 1
                Me.TimeSeries(i) = New TimeSeries(Me.SelectedColumns(i).Name)
                Me.TimeSeries(i).Unit = Me.SelectedColumns(i).Einheit
            Next

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Einlesen
            '--------

            Select Path.GetFileName(Me.File).ToLower()

                Case "q_strg.dat", "pegel.dat"

                    'Header
                    For i = 0 To Me.nLinesHeader - 1
                        StrReadSync.ReadLine()
                    Next

                    'Daten
                    Do
                        Zeile = StrReadSync.ReadLine.ToString()
                        Werte = Zeile.Split(New Char() {Me.Separator.ToChar}, System.StringSplitOptions.RemoveEmptyEntries)
                        'Simulationszeit [h] wird zu Datum nach 01.01.2000 00:00:00 konvertiert
                        datum = New DateTime(2000, 1, 1, 0, 0, 0) + New TimeSpan(0, 0, Werte(0) * 3600)
                        For j = 0 To Me.SelectedColumns.Length - 1
                            Me.TimeSeries(j).AddNode(datum, Helpers.StringToDouble(Werte(Me.SelectedColumns(j).Index)))
                        Next

                    Loop Until StrReadSync.Peek() = -1

                Case "bw_tmp.dat"

                    Dim zeit, value As Double
                    Dim parts(), name As String
                    Dim series As New Dictionary(Of String, Dictionary(Of DateTime, Double))

                    Do
                        Zeile = StrReadSync.ReadLine.ToString()
                        'Headerzeilen
                        If Zeile.StartsWith(" ---") Then Continue Do
                        If Zeile.StartsWith("  Abflu") Then
                            'Zeit lesen
                            zeit = Convert.ToDouble(Zeile.Split(New Char() {"="}, StringSplitOptions.RemoveEmptyEntries)(1).Replace("[Sek]", "").Trim())
                            'Simulationszeit [s] wird zu Datum nach 01.01.2000 00:00:00 konvertiert
                            datum = New DateTime(2000, 1, 1, 0, 0, 0) + New TimeSpan(0, 0, zeit)
                            Continue Do
                        End If
                        If Zeile.StartsWith("  IJBW_Seg-1") Then Continue Do
                        'Datenzeilen (alle Daten unabhängig von Auswahl einlesen)
                        parts = Zeile.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        name = parts(0) & "-" & parts(1)
                        value = Convert.ToDouble(parts(2))
                        If Not series.ContainsKey(name) Then
                            series.Add(name, New Dictionary(Of DateTime, Double))
                        End If
                        series(name).Add(datum, value)

                    Loop Until StrReadSync.Peek() = -1

                    'Nur ausgewählte Reihen abspeichern
                    For i = 0 To Me.SelectedColumns.Length - 1
                        For Each entry As KeyValuePair(Of DateTime, Double) In series(Me.SelectedColumns(i).Name)
                            Me.TimeSeries(i).AddNode(entry.Key, entry.Value)
                        Next
                    Next

            End Select

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub


End Class
