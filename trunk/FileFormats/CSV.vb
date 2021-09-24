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
Imports System.Globalization

''' <summary>
''' Klasse f�r generisches Textformat
''' </summary>
Public Class CSV
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
    Public Sub New(ByVal FileName As String)
        MyBase.New(FileName)
        'TODO: use operating system defaults for initial settings, requires refactoring of import dialog
        'Me.Dateformat = Helpers.CurrentDateFormat.ShortDatePattern & " " & Helpers.CurrentDateFormat.LongTimePattern
        'Me.Separator = New Character(Helpers.CurrentListSeparator)
        'Me.DecimalSeparator = New Character(Helpers.CurrentNumberFormat.NumberDecimalSeparator)
        Call Me.readSeriesInfo()
    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub readSeriesInfo()

        Dim i As Integer
        Dim sInfo As SeriesInfo
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""

        Me.SeriesList.Clear()

        Try
            'Datei �ffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Spalten�berschriften auslesen
            For i = 1 To Math.Max(Me.iLineData, Me.iLineHeadings + 1)
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

            ReDim Namen(0)
            ReDim Einheiten(0)

            If (Me.IsColumnSeparated) Then
                'Zeichengetrennt
                Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar})
                If Me.UseUnits Then
                    Einheiten = ZeileEinheiten.Split(New Char() {Me.Separator.ToChar})
                End If
                anzSpalten = Namen.Length
            Else
                'Spalten mit fester Breite
                anzSpalten = Math.Ceiling(ZeileSpalten.Length / Me.ColumnWidth)
                ReDim Namen(anzSpalten - 1)
                ReDim Einheiten(anzSpalten - 1)
                For i = 0 To anzSpalten - 1
                    Namen(i) = ZeileSpalten.Substring(i * Me.ColumnWidth, Math.Min(Me.ColumnWidth, ZeileSpalten.Substring(i * Me.ColumnWidth).Length)).Trim()
                    If Me.UseUnits Then
                        Einheiten(i) = ZeileEinheiten.Substring(i * Me.ColumnWidth, Math.Min(Me.ColumnWidth, ZeileSpalten.Substring(i * Me.ColumnWidth).Length)).Trim()
                    End If
                Next
            End If

            'remove quotes around names and units
            For i = 0 To anzSpalten - 1
                If Namen(i).StartsWith("""") And Namen(i).EndsWith("""") Then
                    Namen(i) = Namen(i).Replace("""", "")
                End If
                If Me.UseUnits Then
                    If Einheiten(i).StartsWith("""") And Einheiten(i).EndsWith("""") Then
                        Einheiten(i) = Einheiten(i).Replace("""", "")
                    End If
                End If
            Next

            'store series info
            For i = 0 To anzSpalten - 1
                If i <> Me.DateTimeColumnIndex Then
                    sInfo = New SeriesInfo()
                    sInfo.Index = i
                    sInfo.Name = Namen(i)
                    If Me.UseUnits Then
                        sInfo.Unit = Einheiten(i)
                    End If
                    Me.SeriesList.Add(sInfo)
                End If
            Next

            'TODO: gegebenes Datumsformat an dieser Stelle testen

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    'CSV-Datei einlesen
    '******************
    Public Overrides Sub readFile()

        Dim i As Integer
        Dim Zeile As String
        Dim ok As Boolean
        Dim numberformat As NumberFormatInfo
        Dim datum As DateTime
        Dim Werte() As String
        Dim ts As TimeSeries

        Try

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihen instanzieren
            For Each sInfo As SeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                If Me.UseUnits Then
                    ts.Unit = sInfo.Unit
                End If
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.FileTimeSeries.Add(sInfo.Index, ts)
            Next

            'Use default number format by default
            numberformat = Helpers.DefaultNumberFormat.Clone()
            If Me.DecimalSeparator.ToChar = Chr(44) Then
                'change decimal separator to comma
                numberformat.NumberDecimalSeparator = ","
            End If

            'Einlesen
            '--------

            'Header
            For i = 0 To Me.nLinesHeader - 1
                StrReadSync.ReadLine()
            Next

            'Daten
            Do
                Zeile = StrReadSync.ReadLine.ToString()

                If (Me.IsColumnSeparated) Then

                    'Zeichengetrennt
                    '---------------
                    Werte = Zeile.Split(New Char() {Me.Separator.ToChar})

                    If (Werte.Length > 0 And Zeile.Trim.Length > 1) Then
                        'Erste Spalte: Datum_Zeit
                        ok = DateTime.TryParseExact(Werte(Me.DateTimeColumnIndex).Trim(), Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                        If (Not ok) Then
                            Throw New Exception("Could not parse the date '" & Werte(Me.DateTimeColumnIndex) & "' using the given date format '" & Me.Dateformat & "'! Please check the date format!")
                        End If
                        'Restliche Spalten: Werte
                        For Each sInfo As SeriesInfo In Me.SelectedSeries
                            Me.FileTimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index), numberformat))
                        Next
                    End If

                Else
                    'Spalten mit fester Breite
                    '-------------------------
                    'Erste Spalte: Datum_Zeit
                    ok = DateTime.TryParseExact(Zeile.Substring(0, Me.ColumnWidth), Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        Throw New Exception("Could not parse the date '" & Zeile.Substring(0, Me.ColumnWidth) & "' using the given date format '" & Me.Dateformat & "'! Please check the date format!")
                    End If
                    'Restliche Spalten: Werte
                    For Each sInfo As SeriesInfo In Me.SelectedSeries
                        Me.FileTimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Zeile.Substring(sInfo.Index * Me.ColumnWidth + SpaltenOffset, Math.Min(Me.ColumnWidth, Zeile.Substring(sInfo.Index * Me.ColumnWidth + SpaltenOffset).Length)), numberformat))
                    Next
                End If

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    ''' <summary>
    ''' Write one or multiple series to a CSV file
    ''' </summary>
    ''' <param name="tsList">time series to write to file</param>
    ''' <param name="file">path to the csv file</param>
    ''' <param name="cInfo">
    '''     optional CultureInfo object for setting the date time format, number format and the list separator.
    '''     if not set, these settings are taken from CurrentCulture
    ''' </param>
    ''' <remarks></remarks>
    Public Shared Sub Write_File(ByRef tsList As List(Of TimeSeries), ByVal file As String, Optional cInfo As CultureInfo = Nothing)

        Dim strwrite As StreamWriter
        Dim t As DateTime
        Dim value As String
        Dim line As String

        Dim separator As String
        Dim dateFormat As DateTimeFormatInfo
        Dim numberFormat As NumberFormatInfo

        'If no culturInfo is given, use current culture
        If IsNothing(cInfo) Then
            cInfo = Globalization.CultureInfo.CurrentCulture
        End If

        separator = cInfo.TextInfo.ListSeparator
        dateFormat = cInfo.DateTimeFormat
        numberFormat = cInfo.NumberFormat

        Const quote As String = """"

        'collect unique timestamps
        Dim unique_timestamps As New HashSet(Of DateTime)
        For Each ts As TimeSeries In tsList
            unique_timestamps.UnionWith(New HashSet(Of DateTime)(ts.Dates))
        Next
        'sort timestamps
        Dim timestamps As List(Of DateTime) = unique_timestamps.ToList()
        timestamps.Sort()

        'write the file
        strwrite = New StreamWriter(file, False, Helpers.DefaultEncoding)

        '1st line: titles
        line = "datetime"
        For Each zre As TimeSeries In tsList
            line &= separator & quote & zre.Title & quote
        Next
        strwrite.WriteLine(line)
        '2nd line: units
        line = "-"
        For Each zre As TimeSeries In tsList
            line &= separator & quote & zre.Unit & quote
        Next
        strwrite.WriteLine(line)
        '3rd row onwards: data
        For Each t In timestamps
            line = t.ToString(dateFormat)
            For Each ts As TimeSeries In tsList
                If ts.Dates.Contains(t) Then
                    If Double.IsNaN(ts.Nodes(t)) Then
                        value = "NaN"
                    Else
                        value = ts.Nodes(t).ToString(numberFormat)
                    End If
                Else
                    value = "" 'empty value
                End If
                line &= separator & value
            Next
            strwrite.WriteLine(line)
        Next

        strwrite.Close()

    End Sub

#End Region 'Methoden

End Class