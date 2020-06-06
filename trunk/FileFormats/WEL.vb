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

''' <summary>
''' Klasse für das WEL-Dateiformat
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/WEL-Format</remarks>
Public Class WEL
    Inherits FileFormatBase

#Region "Eigenschaften"

    Private _iLineInfo As Integer = 1
    Private _DateTimeLength As Integer = 17

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Number of the line containing general information
    ''' </summary>
    Public Property iLineInfo() As Integer
        Get
            Return _iLineInfo
        End Get
        Set(ByVal value As Integer)
            _iLineInfo = value
        End Set
    End Property

    ''' <summary>
    ''' Length of date time stamp
    ''' </summary>
    Public Property DateTimeLength() As Integer
        Get
            Return _DateTimeLength
        End Get
        Set(ByVal value As Integer)
            _DateTimeLength = value
        End Set
    End Property

#End Region 'Properties

#Region "Methoden"
    
    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        SpaltenOffset = 1
        
        'Voreinstellungen
        Me.iLineInfo = 1
        Me.iLineHeadings = 2
        Me.UseUnits = True
        Me.iLineUnits = 3
        Me.iLineData = 4
        Me.IsColumnSeparated = True
        Me.Separator = Constants.semicolon
        Me.Dateformat = Helpers.DateFormats("WEL")
        Me.DecimalSeparator = Constants.period
        Me.DateTimeLength = 17

        Call Me.readSeriesInfo()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Call Me.selectAllSeries()
            Call Me.readFile()
        End If


    End Sub

    ''' <summary>
    ''' Spalten auslesen
    ''' </summary>
    Public Overrides Sub readSeriesInfo()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""
        Dim LineInfo As String = ""
        Dim sInfo As SeriesInfo

        Me.SeriesList.Clear()

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Spaltenüberschriften auslesen
            For i = 1 To Me.iLineData
                Zeile = StrReadSync.ReadLine.ToString()
                If (i = Me.iLineInfo) Then LineInfo = Zeile
                If (i = Me.iLineHeadings) Then ZeileSpalten = Zeile
                If (i = Me.iLineUnits) Then ZeileEinheiten = Zeile
            Next

            'Are columns separted by ";" or should fixed format be used?
            If ZeileSpalten.Contains(";") Then
                Me.IsColumnSeparated = True
            Else
                Me.IsColumnSeparated = False
            End If

            'Is it a WEL or EFL_WEL file
            If LineInfo.Contains("EFL-WEL") Then
                Me.ColumnWidth = 41
            Else
                'nothing to do, presets can be applied
            End If

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'Spalteninformationen auslesen
            '-----------------------------
            Dim anzSpalten As Integer
            Dim Namen() As String
            Dim Einheiten() As String

            If (Me.IsColumnSeparated) Then
                'Zeichengetrennt
                Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar}, StringSplitOptions.RemoveEmptyEntries)
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Separator.ToChar}, StringSplitOptions.RemoveEmptyEntries)
                anzSpalten = Namen.Length
                For i = 1 To anzSpalten - 1 'first column is timestamp
                    sInfo = New SeriesInfo()
                    sInfo.Name = Namen(i).Trim()
                    sInfo.Unit = Einheiten(i).Trim()
                    sInfo.Index = i
                    Me.SeriesList.Add(sInfo)
                Next
            Else
                'Spalten mit fester Breite
                anzSpalten = Math.Ceiling((ZeileSpalten.Length - Me.DateTimeLength) / Me.ColumnWidth) + 1
                For i = 1 To anzSpalten - 1 'first column is timestamp
                    sInfo = New SeriesInfo()
                    sInfo.Name = ZeileSpalten.Substring(Me.DateTimeLength + (i - 1) * Me.ColumnWidth, Me.ColumnWidth).Trim()
                    sInfo.Unit = ZeileEinheiten.Substring(Me.DateTimeLength + (i - 1) * Me.ColumnWidth, Me.ColumnWidth).Trim()
                    sInfo.Index = i
                    Me.SeriesList.Add(sInfo)
                Next
            End If

        Catch ex As Exception
            MsgBox("Konnte die Datei '" & Path.GetFileName(Me.File) & "' nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    ''' <summary>
    ''' WEL-Datei einlesen
    ''' </summary>
    Public Overrides Sub readFile()

        Dim iZeile As Integer
        Dim Zeile As String
        Dim Werte() As String
        Dim timestamp As String
        Dim datum As DateTime
        Dim ok As Boolean
        Dim ts As TimeSeries

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihen instanzieren
        For Each sInfo As SeriesInfo In Me.SelectedSeries
            ts = New TimeSeries(sInfo.Name)
            If Me.UseUnits Then
                ts.Unit = sInfo.Unit
            End If
            'Series in a WEL file are always interpreted as BlockRight, with the exception of volume
            If ts.Title.EndsWith("_VOL") Then
                ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.Instantaneous
            Else
                ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight
            End If
            Me.TimeSeriesList.Add(ts)
        Next

        'Einlesen
        '--------

        'Header
        For iZeile = 1 To Me.nLinesHeader
            Zeile = StrReadSync.ReadLine.ToString()
        Next

        'Daten
        Do
            Zeile = StrReadSync.ReadLine.ToString()

            If (Me.IsColumnSeparated) Then
                'Zeichengetrennt
                '---------------
                Werte = Zeile.Split(New Char() {Me.Separator.ToChar}, StringSplitOptions.RemoveEmptyEntries)
                'Erste Spalte: Datum_Zeit
                timestamp = Werte(0).Trim()
                ok = DateTime.TryParseExact(timestamp, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                If (Not ok) Then
                    Throw New Exception("Unable to parse the timestamp '" & timestamp & "' using the given format '" & Me.Dateformat & "'!")
                End If
                'Restliche Spalten: Werte
                'Alle ausgewählten Reihen durchlaufen
                For Each sInfo As SeriesInfo In Me.SelectedSeries
                    Me.TimeSeriesList(sInfo.Name).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
                Next
            Else
                'Spalten mit fester Breite
                '-------------------------
                'Erste Spalte: Datum_Zeit
                timestamp = Zeile.Substring(SpaltenOffset, Me.ColumnWidth).Trim()
                ok = DateTime.TryParseExact(timestamp, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                If (Not ok) Then
                    Throw New Exception("Unable to parse the timestamp '" & timestamp & "' using the given format '" & Me.Dateformat & "'!")
                End If
                'Restliche Spalten: Werte
                'Alle ausgewählten Reihen durchlaufen
                For Each sInfo As SeriesInfo In Me.SelectedSeries
                    Me.TimeSeriesList(sInfo.Name).AddNode(datum, StringToDouble(Zeile.Substring((sInfo.Index * Me.ColumnWidth) + SpaltenOffset, Math.Min(Me.ColumnWidth, Zeile.Substring((sInfo.Index * Me.ColumnWidth) + SpaltenOffset).Length))))
                Next
            End If

        Loop Until StrReadSync.Peek() = -1

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Checks whether a file is in the WEL format
    ''' </summary>
    ''' <param name="file">path to the file to check</param>
    ''' <returns>True if verification was successful</returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream
        Dim StrRead As StreamReader
        Dim Zeile As String

        'check the file format
        FiStr = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        StrRead = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))

        'read only the first line
        Zeile = StrRead.ReadLine.ToString()

        StrRead.Close()
        FiStr.Close()

        If (Zeile.StartsWith(" *WEL")) Then
            'It's a BlueM WEL file
            Return True
        ElseIf (Zeile.StartsWith("*EFL-WEL")) Then
            'It's a BlueM EFL WEL file
            Return True
        ElseIf (Zeile.StartsWith("*WEL")) Then
            'It's a TALSIM WEL file
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Attempts to extract a specified WEL file from a WLZIP file of the same name
    ''' </summary>
    ''' <param name="file">path to WEL file</param>
    ''' <returns>True if successful</returns>
    ''' <remarks>TALSIM specific</remarks>
    Public Shared Function extractFromWLZIP(ByVal file As String) As Boolean

        Dim file_wlzip As String
        Dim ze As Ionic.Zip.ZipEntry
        Dim filename, dir As String
        Dim fileFound As Boolean = False

        If file.ToUpper().EndsWith(".KTR.WEL") Then
            file_wlzip = file.Substring(0, file.Length - 8) & ".WLZIP"
        Else
            file_wlzip = file.Substring(0, file.Length - 4) & ".WLZIP"
        End If

        If IO.File.Exists(file_wlzip) Then

            Log.AddLogEntry("Looking for file in " & file_wlzip & " ...")
            dir = IO.Path.GetDirectoryName(file)
            filename = IO.Path.GetFileName(file)

            For Each ze In Ionic.Zip.ZipFile.Read(file_wlzip)
                If ze.FileName.ToLower() = filename.ToLower() Then
                    fileFound = True
                    Log.AddLogEntry("Extracting file from " & file_wlzip & " ...")
                    ze.Extract(dir, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                    Return True
                End If
            Next

            If Not fileFound Then
                Log.AddLogEntry("WARNING: File " & filename & " not found in " & file_wlzip & "!")
                Return False
            End If

        Else
            Return False
        End If

    End Function

#End Region 'Methoden

End Class