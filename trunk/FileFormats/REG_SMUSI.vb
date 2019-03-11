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
''' Klasse für das SMUSI REG-Dateiformat (SMUSI-Regendateien)
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/SMUSI_REG-Format</remarks>
Public Class REG_SMUSI
    Inherits FileFormatBase

    Const WerteproZeile As Integer = 12
    Const LenWert As Integer = 5
    Const LenZeilenanfang As Integer = 20
    Const dt_min As Integer = 5

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Zeitintervall von SMUSI-Regenreihen
    ''' </summary>
    ''' <remarks>5 Minuten</remarks>
    Private ReadOnly Property Zeitintervall() As TimeSpan
        Get
            Return New TimeSpan(0, REG_SMUSI.dt_min, 0)
        End Get
    End Property

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.Dateformat = DateFormats("SMUSI")
        Me.iLineData = 4
        Me.UseUnits = True

        Call Me.ReadColumns()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllColumns()
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub ReadColumns()

        Dim Zeile, title As String
        Dim i As Integer

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Es gibt immer 2 Spalten (Datum + Werte)!
            ReDim Me.Columns(1)

            '1. Spalte (X)
            Me.Columns(0).Name = "Datum_Zeit"
            Me.Columns(0).Index = 0

            '2. Spalte (Y)
            Me.Columns(1).Index = 1

            'Reihentitel aus 1. Zeile nehmen.
            'Wenn Komma enthalten ist, nur den Teil vor dem Komma verwenden
            Zeile = StrReadSync.ReadLine()
            If Zeile.Contains(",") Then
                title = Zeile.Split(",")(0)
            Else
                title = Zeile
            End If
            Me.Columns(1).Name = title

            'Einheit ist immer mm
            Me.Columns(1).Einheit = "mm"

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'SMUSI_REG-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i, j As Integer
        Dim leerzeile As Boolean
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim DatumCurrent, DatumZeile, DatumTmp As DateTime
        Dim Wert As Double

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren
        ReDim Me.TimeSeries(0) 'bei REG gibt es nur eine Zeitreihe
        Me.TimeSeries(0) = New TimeSeries(Me.SelectedColumns(0).Name)
        Me.TimeSeries(0).Unit = Me.SelectedColumns(0).Einheit

        'Einlesen
        '--------
        j = 0
        leerzeile = False

        Do
            j += 1
            Zeile = StrReadSync.ReadLine.ToString()

            If (j > Me.nLinesHeader) Then

                If (Zeile.Trim.Length < 1) Then
                    'Leere Zeile
                    '-----------
                    leerzeile = True
                Else
                    'Zeile mit Werten
                    '----------------
                    'Zeilendatum erkennen
                    'TODO: Me.Datumsformat verwenden
                    Jahr = Zeile.Substring(11, 4)
                    Monat = Zeile.Substring(8, 2)
                    Tag = Zeile.Substring(5, 2)
                    Stunde = Zeile.Substring(18, 2)
                    Minute = 0
                    DatumZeile = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())

                    If (leerzeile) Then
                        'Bei vorheriger leeren Zeile: 0-Stelle 5 min nach letztem Datum einfügen
                        DatumTmp = DatumCurrent.Add(Me.Zeitintervall)
                        If (Not DatumTmp = DatumZeile) Then
                            Me.TimeSeries(0).AddNode(DatumTmp, 0)
                        End If
                    End If

                    '12 x Datum und Wert zur Zeitreihe hinzufügen
                    '---------------------------------------
                    For i = 0 To REG_SMUSI.WerteproZeile - 1
                        DatumCurrent = DatumZeile.AddMinutes(i * REG_SMUSI.dt_min)
                        Wert = StringToDouble(Zeile.Substring(LenZeilenanfang + LenWert * i, LenWert)) / 1000
                        Me.TimeSeries(0).AddNode(DatumCurrent, Wert)
                    Next

                    If (leerzeile) Then
                        'Bei vorheriger leeren Zeile: 0-Stelle 5 min vor Zeilendatum einfügen
                        DatumTmp = DatumZeile.Subtract(Me.Zeitintervall)
                        If (Not TimeSeries(0).Nodes.ContainsKey(DatumTmp)) Then
                            Me.TimeSeries(0).AddNode(DatumTmp, 0)
                        End If
                    End If

                    leerzeile = False
                End If
            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als SMUSI-REG-Datei
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    ''' <remarks>Zeitreihe muss äquidistant mit 5 min Zeitschritt vorliegen!</remarks>
    Public Shared Sub Write_File(ByVal Reihe As TimeSeries, ByVal File As String)

        Dim t1, t2 As DateTime
        Dim dt As Integer
        Dim j, n As Integer
        Dim export_start, export_end As DateTime
        Dim IntWert As Long
        Dim Summe As Integer
        Dim Spanne As TimeSpan
        Dim nJahre As Integer
        Dim hn_A_Mittel As Integer
        Dim strwrite As StreamWriter
        Dim FiStr As FileStream
        Dim StrRead As StreamReader
        Dim alles As String

        'Zeitreihe muss äquidistant mit 5 min Zeitschritt vorliegen!
        t1 = Reihe.StartDate
        For Each t2 In Reihe.Dates.Skip(1)
            dt = (t2 - t1).Minutes
            If dt <> 5 Then
                Throw New Exception(String.Format("Unable to export to SMUSI REG format. " & _
                                                  "Time series must be equidistant with a time step of 5 minutes." & eol & _
                                                  "Timestep between {0} and {1} is not 5 minutes!", t1, t2))
            End If
            t1 = t2
        Next

        'Anfangsdatum und Enddatum der zu exportierenden Zeitreihe bestimmen

        'ExportStartDatum
        'Start muss zur vollen Stunde sein
        export_start = Reihe.StartDate
        If export_start.Minute > 0 Then
            export_start = export_start.AddMinutes(60 - export_start.Minute)
        End If

        'ExportEndDatum
        'Ende muss um XX:55 sein
        export_end = Reihe.EndDate
        If export_end.Minute > 55 Then
            export_end = export_end.AddMinutes(55 - export_end.Minute)
        ElseIf export_end.Minute < 55 Then
            export_end = export_end.AddHours(-1)
            export_end = export_end.AddMinutes(55 - export_end.Minute)
        End If

        'Zeitreihe zuschneiden
        Reihe.Cut(export_start, export_end)

        'Wertezeilen schreiben
        strwrite = New StreamWriter(File)
        Summe = 0 'Summe für die spätere Berechnung der Jahresniederschlagshöhe
        n = 0
        Do While n < Reihe.Length
            strwrite.Write(Reihe.Title.Substring(0, 4) & " ")
            strwrite.Write(Reihe.Dates(n).ToString(DateFormats("SMUSI")))
            For j = 1 To WerteproZeile
                IntWert = Reihe.Values(n) * 1000
                Summe = Summe + IntWert
                strwrite.Write(IntWert.ToString.PadLeft(5))
                n = n + 1
            Next
            strwrite.WriteLine()
        Loop
        strwrite.Close()

        'Header anpassen (Summe und Betrachungszeitraum)
        FiStr = New FileStream(File, FileMode.Open, IO.FileAccess.Read)
        StrRead = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'Mittlere Jahresniederschlagshöhe berechnen
        Spanne = Reihe.EndDate - Reihe.StartDate
        nJahre = Math.Max(Spanne.TotalDays / 365, 1)
        hn_A_Mittel = Summe / 1000 * 1 / nJahre

        'Komplette Datei einlesen
        alles = StrRead.ReadToEnd()
        StrRead.Close()
        FiStr.Close()

        'Header schreiben
        strwrite = New StreamWriter(File)
        strwrite.WriteLine(Reihe.Title)
        strwrite.WriteLine(String.Format("hN = {0} mm/a", hn_A_Mittel))
        strwrite.WriteLine("================================================================================")

        'Rest anhängen
        strwrite.Write(alles)
        strwrite.Close()

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um eine SMUSI-REG-Datei handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        '2 Zeilen einlesen
        Zeile = StrRead.ReadLine.ToString()
        Zeile = StrRead.ReadLine.ToString()

        StrRead.Close()
        FiStr.Close()

        If (Zeile.Substring(0, 4) = "hN =") Then
            'Es ist eine SMUSI-Regenreihe!
            Return True
        Else
            Return False
        End If

    End Function

#End Region 'Methoden

End Class
