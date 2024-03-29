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

Namespace Fileformats

    ''' <summary>
    ''' Class for importing GISMO result files (*.CSV,*.ASC)
    ''' For information about GISMO refer to http://www.sydro.de/
    ''' For file format info refer to https://wiki.bluemodel.org/index.php/WEL-Format_%28GISMO%29
    ''' </summary>
    Public Class GISMO_WEL
        Inherits TimeSeriesFile
        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

#Region "Methods"

        ' Constructor
        Public Sub New(FileName As String)

            MyBase.New(FileName)

            ' Presettings
            Me.Dateformat = DateFormats("GISMO1")
            Me.DecimalSeparator = Constants.period
            Me.UseUnits = True

            ' which lines contain heading, units, first data line
            Me.iLineHeadings = 15
            Me.iLineUnits = 16
            Me.iLineData = 17

            If Me.IsCSV() Then
                ' is it a CSV file? GISMO uses ";" to separate values if CSV mode is choosen
                Me.IsColumnSeparated = True
                Me.Separator = Constants.semicolon
            Else
                ' if not, the space " " is used as separator
                Me.IsColumnSeparated = False
                Me.Separator = space
            End If

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

            ' get element name to add to time series name
            Zeile = StrReadSync.ReadLine.ToString
            SeriesName = Zeile.Substring(13, 16)

            ' find line with data headers and units
            For i = 2 To Math.Max(Me.iLineData, Me.iLineHeadings + 1)
                Zeile = StrReadSync.ReadLine.ToString
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

            ' first space needs to be removed
            ZeileSpalten = ZeileSpalten.Substring(1, ZeileSpalten.Length - 1)
            ZeileEinheiten = ZeileEinheiten.Substring(1, ZeileEinheiten.Length - 1)

            If (Me.IsColumnSeparated) Then
                ' data columns are separated by ";"
                ' split string at every ";"
                Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar})
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Separator.ToChar})
                anzSpalten = Namen.Length
                If Namen.Length <> Einheiten.Length Then
                    MsgBox("Number of column names <> number of units!")
                End If

            Else
                ' data columns are separated by spaces
                ' converge multiple spaces to one
                ZeileSpalten = System.Text.RegularExpressions.Regex.Replace(ZeileSpalten, "\s{2,}", Me.Separator.ToChar)
                ZeileEinheiten = System.Text.RegularExpressions.Regex.Replace(ZeileEinheiten, "\s{2,}", Me.Separator.ToChar)
                ' remove leading and trailing spaces
                ZeileSpalten = Trim(ZeileSpalten)
                ZeileEinheiten = Trim(ZeileEinheiten)

                ' special for GISMO Aussengebiet-result-files
                ' wave only wants one column for datetime 
                ' --> Replace "Datum Zeit" with "Datum-Zeit" in ZeileSpalten and "- -" with " - " in Zeile Einheiten
                ' if it is there
                Dim Replacepostion As Integer
                Replacepostion = ZeileSpalten.IndexOf("Datum Zeit")
                If Replacepostion <> -1 Then
                    Mid(ZeileSpalten, Replacepostion + 1, 10) = "Datum_Zeit"
                    Mid(ZeileEinheiten, Replacepostion + 1, 3) = " - "
                    ZeileEinheiten = Trim(System.Text.RegularExpressions.Regex.Replace(ZeileEinheiten, "\s{2,}", Me.Separator.ToChar))
                End If

                ' data columns are separated by " "
                ' split string at every " "
                Namen = ZeileSpalten.Split(New Char() {Me.Separator.ToChar})
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Separator.ToChar})
                anzSpalten = Namen.Length
                If Namen.Length <> Einheiten.Length Then
                    MsgBox("Number of column names <> number of units!")
                End If
            End If

            ' put headers and units into the Me.Spalten-array (starts with index 0, --> [anzSpalten -1])
            For i = 1 To (anzSpalten - 1) ' first column is timestamp
                sInfo = New TimeSeriesInfo()
                sInfo.Name = $"{SeriesName.Trim}_{Namen(i).Trim()}"
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
            Dim Werte(), Werte_temp() As String
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

                ' first empty space "" needs to be removed (otherwise date time format is not understood)
                'Zeile = Zeile.Substring(1, Zeile.Length - 1)
                Zeile = Trim(Zeile)


                If (Me.IsColumnSeparated) Then
                    ' data columns are separated by ";"

                    ' split data line into columns
                    Werte = Zeile.Split(New Char() {Me.Separator.ToChar})

                    ' first column ist date time, add date time to times series
                    ok = DateTime.TryParseExact(Werte(Me.DateTimeColumnIndex), DateFormats("GISMO1"), Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        ok = DateTime.TryParseExact(Werte(Me.DateTimeColumnIndex), DateFormats("GISMO2"), Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                        If Not ok Then
                            Throw New Exception($"Kann das Datumsformat '{Werte(Me.DateTimeColumnIndex)}' nicht erkennen!{eol}Sollte in der Form '{DateFormats("GISMO1")} oder {DateFormats("GISMO2")}' vorliegen!")
                        End If
                    End If

                    ' remaining columns are data, add to time series
                    For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                        Me.TimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
                    Next

                Else
                    ' data columns are separated by spaces
                    ' converge multiple spaces to one
                    Zeile = System.Text.RegularExpressions.Regex.Replace(Zeile, "\s{2,}", Me.Separator.ToChar)

                    ' the date time columns need to be moved to one column
                    Werte_temp = Zeile.Split(New Char() {Me.Separator.ToChar})
                    ReDim Werte(Werte_temp.Length - 2)
                    For i = 0 To Werte.Length - 1
                        Werte(i) = Werte_temp(i + 1)
                    Next
                    Werte(0) = Werte_temp(0) & " " & Werte_temp(1)

                    ' first column (now) is date time, add to time series
                    ok = DateTime.TryParseExact(Werte(Me.DateTimeColumnIndex), DateFormats("GISMO1"), Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        ok = DateTime.TryParseExact(Werte(Me.DateTimeColumnIndex), DateFormats("GISMO2"), Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                        If Not ok Then
                            Throw New Exception($"Kann das Datumsformat '{Werte(Me.DateTimeColumnIndex)}' nicht erkennen! {eol}Sollte in der Form '{DateFormats("GISMO1")} oder {DateFormats("GISMO2")}' vorliegen!")
                        End If
                    End If

                    ' remaining columns are data, add to time series
                    For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                        Me.TimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
                    Next

                End If

            Loop Until StrReadSync.Peek() = -1

            ' close file
            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Checks whether the GISMO result file is CSV (semicolon-separated) or ASC (space-separated)
        ''' </summary>
        ''' <returns>True if the file is CSV (semicolon-separated)</returns>
        Public Function IsCSV() As Boolean

            ' open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim line As String

            ' read first line
            line = StrRead.ReadLine().Trim()

            ' close file
            StrRead.Close()
            FiStr.Close()

            If line.Contains("*WEL.CSV") Then
                ' it's in CSV format
                ' separator is a ";)
                Return True

            ElseIf line.Contains("*WEL.ASC") Then
                ' it's in WEL format
                Return False

            Else
                Throw New Exception("Unable to determine GISMO result file variant!")
            End If

        End Function


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

            ' read three lines
            For i As Integer = 1 To 3
                lines.Add(StrRead.ReadLine().Trim())
            Next

            ' close file
            StrRead.Close()
            FiStr.Close()

            'check first line
            If Not (lines(0).Contains("*WEL.CSV") Or lines(0).Contains("*WEL.ASC")) Then
                Return False
            End If

            ' check third line, must contain the word "GISMO"
            If Not lines(2).Contains("GISMO") Then
                Return False
            End If

            Return True

        End Function


#End Region 'Methods

    End Class

End Namespace