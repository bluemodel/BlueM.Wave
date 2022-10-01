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
    ''' Class for importing GISMO result files (*.CSV,*.ASC)
    ''' For information about GISMO refer to http://www.sydro.de/
    ''' For file format info refer to https://wiki.bluemodel.org/index.php/WEL-Format_%28GISMO%29
    ''' </summary>
    Public Class WEL_GISMO
        Inherits FileFormatBase
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
        Public Sub New(FileName As String, IsSSV As Boolean)

            MyBase.New(FileName)

            ' Presettings
            Me.Dateformat = DateFormats("GISMO1")
            Me.DecimalSeparator = Constants.period
            Me.UseUnits = True

            ' which lines contain heading, units, first data line
            Me.iLineHeadings = 15
            Me.iLineUnits = 16
            Me.iLineData = 17

            If (IsSSV) Then
                ' is it a semiicolon separated file (SSV)? GISMO uses ";" to separate values if CSV mode is choosen
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
            Dim sInfo As SeriesInfo

            Me.SeriesList.Clear()

            Try
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
                    sInfo = New SeriesInfo()
                    sInfo.Name = $"{SeriesName.Trim}_{Namen(i).Trim()}"
                    sInfo.Index = i
                    If Einheiten(i).Trim = "cbm/s" Then
                        Einheiten(i) = "m3/s"
                    End If
                    sInfo.Unit = Einheiten(i).Trim()
                    Me.SeriesList.Add(sInfo)
                Next

            Catch ex As Exception
                ' catch errors
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
            End Try

        End Sub

        ' read file
        Public Overrides Sub readFile()

            Dim i As Integer
            Dim Zeile As String
            Dim ok As Boolean
            Dim datum As DateTime
            Dim Werte(), Werte_temp() As String
            Dim ts As TimeSeries

            Try
                ' open file
                Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                ' initialize a time series for every selected series
                For Each sInfo As SeriesInfo In Me.SelectedSeries
                    ts = New TimeSeries(sInfo.Name)
                    If Me.UseUnits Then
                        ts.Unit = sInfo.Unit
                    End If
                    ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                    Me.FileTimeSeries.Add(sInfo.Index, ts)
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
                        For Each sInfo As SeriesInfo In Me.SelectedSeries
                            Me.FileTimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
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
                        For Each sInfo As SeriesInfo In Me.SelectedSeries
                            Me.FileTimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
                        Next

                    End If

                Loop Until StrReadSync.Peek() = -1

                ' close file
                StrReadSync.Close()
                StrRead.Close()
                FiStr.Close()

            Catch ex As Exception
                'catch errors
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
            End Try

        End Sub


        ''' <summary>
        ''' Checks, if the file is a GISMO result file (either *.CSV or *.ASC)
        ''' </summary>
        ''' <param name="file">file path</param>
        ''' <param name="IsSSV">Boolean indicates wther the file is semicolon-separated</param>
        ''' <returns>True if the file is a GISMO result file</returns>
        Public Shared Function verifyFormat(file As String, Optional ByRef IsSSV As Boolean = False) As Boolean

            Dim isGISMO As Boolean = False

            ' open file
            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim Zeile As String = ""

            ' read first line
            Zeile = StrRead.ReadLine.ToString()
            Zeile = Trim(Zeile)

            If (Zeile.Contains("*WEL.CSV")) Then
                ' it's in CSV format
                ' separator is a ";)
                IsSSV = True

                ' read third line
                Zeile = StrRead.ReadLine.ToString()
                Zeile = StrRead.ReadLine.ToString()
                Zeile = Trim(Zeile)
                ' check if it contains the word "GISMO"
                If Zeile.Contains("GISMO") Then
                    isGISMO = True
                End If

            ElseIf (Zeile.Contains("*WEL.ASC")) Then
                ' it's in WEL format
                IsSSV = False

                ' read third line
                Zeile = StrRead.ReadLine.ToString()
                Zeile = StrRead.ReadLine.ToString()
                Zeile = Trim(Zeile)
                ' check if it contains the word "GISMO"
                If Zeile.Contains("GISMO") Then
                    isGISMO = True
                End If

            End If

            ' close file
            StrRead.Close()
            FiStr.Close()

            Return isGISMO

        End Function


#End Region 'Methods

    End Class

End Namespace