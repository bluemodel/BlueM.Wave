'Copyright (c) BlueM Dev Group
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
Imports System.Globalization

''' <summary>
''' Klasse f�r ASC-Dateiformat (SMUSI)
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/ASC-Format</remarks>
Public Class ASC
    Inherits FileFormatBase

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iLineHeadings = 2
        Me.UseUnits = True
        Me.iLineUnits = 3
        Me.iLineData = 4
        Me.IsColumnSeparated = True
        Me.Separator = Constants.space
        Me.DecimalSeparator = Constants.period
        Me.DateTimeColumnIndex = 0

        Call Me.ReadColumns()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Call Me.selectAllColumns()
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub ReadColumns()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""

        'Datei �ffnen
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

        'Spalten�berschriften
        For i = 1 To Me.iLineData
            Zeile = StrReadSync.ReadLine.ToString
            If (i = Me.iLineHeadings) Then ZeileSpalten = Zeile
            If (i = Me.iLineUnits) Then ZeileEinheiten = Zeile
        Next
        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

        'Spaltennamen auslesen
        '---------------------
        Dim anzSpalten As Integer
        Dim Namen() As String
        Dim Einheiten() As String

        Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar}, StringSplitOptions.RemoveEmptyEntries)
        Einheiten = ZeileEinheiten.Split(New Char() {Me.Separator.ToChar}, StringSplitOptions.RemoveEmptyEntries)

        'Bei ASC hat die Datumsspalte (manchmal) keine Einheit
        If (Einheiten.Length = Namen.Length - 1) Then
            'Einheit f�r Datumsspalte erg�nzen
            Array.Reverse(Einheiten)
            ReDim Preserve Einheiten(Einheiten.Length)
            Array.Reverse(Einheiten)
            Einheiten(0) = "-"
        End If

        anzSpalten = Namen.Length

        ReDim Me.Columns(anzSpalten - 1)
        For i = 0 To anzSpalten - 1
            Me.Columns(i).Name = Namen(i).Trim()
            Me.Columns(i).Einheit = Einheiten(i).Trim()
            Me.Columns(i).Index = i
        Next

    End Sub

    'ASC-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i As Integer
        Dim Zeile As String
        Dim Werte() As String
        Dim datum, datumLast As DateTime
        Dim Ereignisende As Boolean
        Dim dt As TimeSpan

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        dt = New TimeSpan(0, 5, 0)

        'Anzahl Zeitreihen
        ReDim Me.TimeSeries(Me.SelectedColumns.Length - 1)

        'Zeitreihen instanzieren
        For i = 0 To Me.SelectedColumns.Length - 1
            Me.TimeSeries(i) = New TimeSeries(Me.SelectedColumns(i).Name)
        Next

        'Einheiten?
        If (Me.UseUnits) Then
            For i = 0 To Me.SelectedColumns.Length - 1
                Me.TimeSeries(i).Unit = Me.SelectedColumns(i).Einheit
            Next
        End If

        'Einlesen
        '--------

        'Header
        For i = 1 To Me.nLinesHeader + 1
            StrReadSync.ReadLine()
        Next

        'Daten
        Ereignisende = False
        Do While Not StrReadSync.Peek() = -1

            'Zeile einlesen
            Zeile = StrReadSync.ReadLine()

            '* am Anfang ignorieren
            If (Zeile.StartsWith("*")) Then Zeile = Zeile.Substring(1)

            Werte = Zeile.ToString.Split(New Char() {Me.Separator.ToChar}, StringSplitOptions.RemoveEmptyEntries)

            If Werte.Length > 0 Then

                'Eine Zeile mit Werten wird eingelesen
                'Ersten zwei Spalten: Datum_Zeit
                'Dim ok As Boolean
                'ok = DateTime.TryParseExact(Werte(0) & " " & Werte(1), "dd.MM.yyyy HH:mm", Konstanten.Zahlenformat, DateTimeStyles.None, datum)
                datum = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(1).Substring(0, 2), Werte(1).Substring(3, 2), 0, New System.Globalization.GregorianCalendar())

                'Wenn vorher eine leere Zeile eingelesen wurde
                If (Ereignisende) Then

                    'Mit St�tzstellen vom Wert 0 L�cke zwischen Ereignissen abschliessen
                    datumLast = Me.TimeSeries(0).EndDate
                    If (datum.Subtract(datumLast) > dt) Then 'nur wenn L�cke gr��er als dt ist

                        For i = 0 To Me.SelectedColumns.Length - 1
                            'Eine Null nach dem letzten Datum
                            Me.TimeSeries(i).AddNode(datumLast.Add(dt), 0.0)
                            If (datum.Subtract(dt) > datumLast.Add(dt)) Then 'nur wenn L�cke damit noch nicht geschlossen ist
                                'Eine Null vor dem aktuellen Datum
                                Me.TimeSeries(i).AddNode(datum.Subtract(dt), 0.0)
                            End If
                        Next

                        'Log
                        Call Log.AddLogEntry("... Die L�cke zwischen " & datumLast.ToString(Helpers.DefaultDateFormat) & " und " & datum.ToString(Helpers.DefaultDateFormat) & " wurde mit 0-Werten abgeschlossen.")
                    End If
                    Ereignisende = False 'zur�cksetzen

                End If

                'eingelesene St�tzstellen hinzuf�gen
                For i = 0 To Me.SelectedColumns.Length - 1
                    Me.TimeSeries(i).AddNode(datum, StringToDouble(Werte(Me.SelectedColumns(i).Index + 1))) '+1 weil Datum auch ein Leerzeichen enth�lt
                Next

            Else
                'Falls eine leere Zeile eingelesen wurde
                Ereignisende = True

            End If
        Loop

        'Datei schlie�en
        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class