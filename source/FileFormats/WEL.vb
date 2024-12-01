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
Imports System.IO
Imports System.Text.RegularExpressions

Namespace Fileformats

    ''' <summary>
    ''' Klasse für das WEL-Dateiformat
    ''' </summary>
    ''' <remarks>Format siehe https://wiki.bluemodel.org/index.php/WEL-Format</remarks>
    Public Class WEL
        Inherits TimeSeriesFile

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
            Set(value As Integer)
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
            Set(value As Integer)
                _DateTimeLength = value
            End Set
        End Property

#End Region 'Properties

#Region "Methoden"

        ''' <summary>
        ''' Konstruktor
        ''' </summary>
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.iLineInfo = 1
            Me.iLineHeadings = 2
            Me.UseUnits = True
            Me.iLineUnits = 3
            Me.iLineData = 4
            Me.IsColumnSeparated = True
            Me.ColumnOffset = 1
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
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Spaltenüberschriften auslesen
            For i = 1 To Me.iLineUnits
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
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = Namen(i).Trim()
                    sInfo.Unit = Einheiten(i).Trim()
                    sInfo.Index = i
                    Me.TimeSeriesInfos.Add(sInfo)
                Next
            Else
                'Spalten mit fester Breite
                anzSpalten = Math.Ceiling((ZeileSpalten.Length - Me.DateTimeLength) / Me.ColumnWidth) + 1
                For i = 1 To anzSpalten - 1 'first column is timestamp
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = ZeileSpalten.Substring(Me.DateTimeLength + (i - 1) * Me.ColumnWidth, Me.ColumnWidth).Trim()
                    If ZeileEinheiten.Length < Me.DateTimeLength + (i - 1) * Me.ColumnWidth + Me.ColumnWidth Then
                        Log.AddLogEntry(levels.warning, $"Unable to read unit for series {sInfo.Name}!")
                        sInfo.Unit = "-"
                    Else
                        sInfo.Unit = ZeileEinheiten.Substring(Me.DateTimeLength + (i - 1) * Me.ColumnWidth, Me.ColumnWidth).Trim()
                    End If
                    sInfo.Index = i
                    Me.TimeSeriesInfos.Add(sInfo)
                Next
            End If

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
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihen instanzieren
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
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
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.TimeSeries.Add(sInfo.Index, ts)
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
                If Zeile.Trim().Length = 0 Then
                    'skip emtpy lines
                    Continue Do
                End If

                If (Me.IsColumnSeparated) Then
                    'Zeichengetrennt
                    '---------------
                    Werte = Zeile.Split(New Char() {Me.Separator.ToChar}, StringSplitOptions.RemoveEmptyEntries)
                    'Erste Spalte: Datum_Zeit
                    timestamp = Werte(0).Trim()
                    ok = DateTime.TryParseExact(timestamp, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        Throw New Exception($"Unable to parse the timestamp '{timestamp}' using the given format '{Me.Dateformat}'!")
                    End If
                    'Restliche Spalten: Werte
                    'Alle ausgewählten Reihen durchlaufen
                    For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                        Me.TimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
                    Next
                Else
                    'Spalten mit fester Breite
                    '-------------------------
                    'Erste Spalte: Datum_Zeit
                    timestamp = Zeile.Substring(ColumnOffset, Me.ColumnWidth).Trim()
                    ok = DateTime.TryParseExact(timestamp, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        Throw New Exception($"Unable to parse the timestamp '{timestamp}' using the given format '{Me.Dateformat}'!")
                    End If
                    'Restliche Spalten: Werte
                    'Alle ausgewählten Reihen durchlaufen
                    For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                        Me.TimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Zeile.Substring((sInfo.Index * Me.ColumnWidth) + ColumnOffset, Math.Min(Me.ColumnWidth, Zeile.Substring((sInfo.Index * Me.ColumnWidth) + ColumnOffset).Length))))
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
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim FiStr As FileStream
            Dim StrRead As StreamReader
            Dim Zeile As String

            'check the file format
            FiStr = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            StrRead = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)

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
                            Dim fs As New IO.FileStream(file, FileMode.CreateNew)
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

#End Region 'Methoden

    End Class

End Namespace