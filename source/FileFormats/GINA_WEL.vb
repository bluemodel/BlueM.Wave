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
Imports System.Linq

Namespace Fileformats

    ''' <summary>
    ''' Klasse für das GINA WEL-Dateiformat
    ''' </summary>
    ''' <remarks>Format siehe https://wiki.bluemodel.org/index.php/GINA-WEL-Format</remarks>
    Public Class GINA_WEL
        Inherits TimeSeriesFile

        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

#Region "Methoden"

        'Constructor
        Public Sub New(FileName As String)

            MyBase.New(FileName)

            ' Presettings
            Me.Dateformat = DateFormats("default")
            Me.DecimalSeparator = Constants.period
            Me.IsColumnSeparated = True
            Me.Separator = Constants.semicolon
            Me.UseUnits = True

            ' Index of header rows
            Me.iLineHeadings = 5
            Me.iLineUnits = 6
            Me.iLineData = 7

            Call Me.readSeriesInfo()

        End Sub

        ' get columns
        Public Overrides Sub readSeriesInfo()

            Dim i As Integer
            Dim Zeile As String = ""
            Dim ZeileSpalten As String = ""
            Dim ZeileEinheiten As String = ""
            Dim SeriesName As String = ""
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            ' open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            ' find line with column headings and units
            For i = 1 To Me.iLineHeadings + 1
                Zeile = StrReadSync.ReadLine.ToString()
                If (i = Me.iLineHeadings) Then ZeileSpalten = Zeile
                If (i = Me.iLineUnits) Then ZeileEinheiten = Zeile
            Next

            ' close file
            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            ' get column names and units
            Dim anzSpalten As Integer
            Dim Namen() As String
            Dim Einheiten() As String

            ' split line with column headings
            Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar})
            Einheiten = ZeileEinheiten.Split(New Char() {Me.Separator.ToChar})
            anzSpalten = Namen.Length
            If Namen.Length <> Einheiten.Length Then
                MsgBox("Number of column names <> number of units!")
            End If

            ' put headers and units into the Me.Spalten-array (starts with index 0, --> [anzSpalten -1])
            For i = 1 To (anzSpalten - 1) ' first column is timestamp
                sInfo = New TimeSeriesInfo()
                sInfo.Name = Namen(i).Trim()
                sInfo.Index = i
                If Einheiten(i).Trim = "cbm/s" Then
                    Einheiten(i) = "m3/s"
                End If
                sInfo.Unit = Einheiten(i).Trim()
                Me.TimeSeriesInfos.Add(sInfo)
            Next

        End Sub

        ' read file
        Public Overrides Sub readFile()

            Dim i As Integer
            Dim Zeile As String
            Dim ok As Boolean
            Dim datum As DateTime
            Dim Werte()
            Dim ts As TimeSeries

            ' open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            ' initialize a time series for every selected series
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                If Me.UseUnits Then
                    ts.Unit = sInfo.Unit
                End If
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            ' read over header lines
            For i = 0 To Me.nLinesHeader - 1
                StrReadSync.ReadLine()
            Next

            ' read date lines
            Do
                Zeile = StrReadSync.ReadLine.ToString()

                ' remove Whitespaces
                Zeile = Trim(Zeile)

                ' data columns are separated by ";"

                ' split data line into columns and trim
                Werte = Zeile.Split(New Char() {Me.Separator.ToChar}).Select(Function(s) s.Trim()).ToArray()

                ' first column ist date time, add date time to times series
                ok = DateTime.TryParseExact(Werte(Me.DateTimeColumnIndex), DateFormats("default"), Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                If (Not ok) Then
                    ok = DateTime.TryParseExact(Werte(Me.DateTimeColumnIndex), DateFormats("default"), Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                    If Not ok Then
                        Throw New Exception($"Kann das Datumsformat '{Werte(Me.DateTimeColumnIndex)}' nicht erkennen!{eol}Sollte in der Form '{DateFormats("default")} vorliegen!")
                    End If
                End If

                ' remaining columns are data, add to time series
                For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                    Me.TimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
                Next

            Loop Until StrReadSync.Peek() = -1

            ' close file
            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub


        ''' <summary>
        ''' Checks if the file is a GISMO result file (either *.CSV or *.ASC)
        ''' </summary>
        ''' <param name="file">file path</param>
        ''' <returns>True if the file is a GISMO result file</returns>
        Public Shared Function verifyFormat(file As String) As Boolean

            ' open file
            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim lines As New List(Of String)

            ' read four lines
            For i As Integer = 1 To 4
                lines.Add(StrRead.ReadLine().Trim())
            Next

            ' close file
            StrRead.Close()
            FiStr.Close()

            ' check first line
            If Not (lines(0).Contains("Gew.Typ")) Then
                Return False
            End If

            ' check second line
            If Not (lines(1).Contains("Grundlast")) Then
                Return False
            End If

            ' check third line
            If Not (lines(2).Contains("AEO")) Then
                Return False
            End If

            ' check fourth line
            If Not lines(3).Contains("AEOpnat") Then
                Return False
            End If

            Return True

        End Function

#End Region 'Methoden

    End Class

End Namespace