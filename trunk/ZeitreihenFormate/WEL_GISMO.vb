'---------------------------------------------------------------------
'BlueM.Wave ist a tool for time series management and analysis
'Copyright (C) 2015  BlueM Dev Team, http://bluemodel.org/

'This file is part of BlueM.Wave

'BlueM.Wave is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'BlueM.Wave is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <http://www.gnu.org/licenses/>.
'---------------------------------------------------------------------
'
Imports System.IO

''' <summary>
''' Class to import of GIMSO result files (*.CSV,*.ASC)
''' For information about GISMO refer to http://www.sydro.de/
''' For file format info refer to http://wiki.bluemodel.org/index.php/Wave
''' </summary>
Public Class WEL_GISMO
    Inherits Dateiformat
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
    Public Sub New(ByVal FileName As String, ByVal IsSSV As Boolean)

        MyBase.New(FileName)

        ' Presettings
        Me.Datumsformat = Datumsformate("GISMO1")
        Me.Dezimaltrennzeichen = Me.punkt
        Me.UseEinheiten = True

        ' which lines contain heading, units, first data line
        Me.iZeileUeberschriften = 15
        Me.iZeileEinheiten = 16
        Me.iZeileDaten = 17

        If (IsSSV) Then
            ' is it a semiicolon separated file (SSV)? GISMO uses ";" to separate values if CSV mode is choosen
            Me.Zeichengetrennt = True
            Me.Trennzeichen = Me.semikolon
        Else
            ' if not, the space " " is used as separator
            Me.Zeichengetrennt = False
            Me.Trennzeichen = leerzeichen
        End If

        Call Me.SpaltenAuslesen()

    End Sub

    ' get columns
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""
        Dim SeriesName As String = ""

        Try
            ' open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            ' get element name to add to time series name
            Zeile = StrReadSync.ReadLine.ToString
            SeriesName = Zeile.Substring(13, 16)

            ' find line with data headers and units
            For i = 2 To Math.Max(Me.iZeileDaten, Me.iZeileUeberschriften + 1)
                Zeile = StrReadSync.ReadLine.ToString
                If (i = Me.iZeileUeberschriften) Then ZeileSpalten = Zeile
                If (i = Me.iZeileEinheiten) Then ZeileEinheiten = Zeile
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

            If (Me.Zeichengetrennt) Then
                ' data columns are separated by ";"
                ' split string at every ";"
                Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character})
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character})
                anzSpalten = Namen.Length
                If Namen.Length <> Einheiten.Length Then
                    MsgBox("Number of column names <> number of units!")
                End If

            Else
                ' data columns are separated by spaces
                ' converge multiple spaces to one
                ZeileSpalten = System.Text.RegularExpressions.Regex.Replace(ZeileSpalten, "\s{2,}", Me.Trennzeichen.Character)
                ZeileEinheiten = System.Text.RegularExpressions.Regex.Replace(ZeileEinheiten, "\s{2,}", Me.Trennzeichen.Character)
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
                    ZeileEinheiten = Trim(System.Text.RegularExpressions.Regex.Replace(ZeileEinheiten, "\s{2,}", Me.Trennzeichen.Character))
                End If

                ' data columns are separated by " "
                ' split string at every " "
                Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character})
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character})
                anzSpalten = Namen.Length
                If Namen.Length <> Einheiten.Length Then
                    MsgBox("Number of column names <> number of units!")
                End If
            End If

            ' put headers and units into the Me.Spalten-array (starts with index 0, --> [anzSpalten -1])
            ReDim Me.Spalten(anzSpalten - 1)
            For i = 0 To (anzSpalten - 1)
                Me.Spalten(i).Name = SeriesName.Trim & "_" & Namen(i).Trim()
                Me.Spalten(i).Index = i
                If Einheiten(i).Trim = "cbm/s" Then
                    Einheiten(i) = "m3/s"
                End If
                Me.Spalten(i).Einheit = Einheiten(i).Trim()
            Next

        Catch ex As Exception
            ' catch errors
            MsgBox("Could not read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    ' read file
    Public Overrides Sub Read_File()

        Dim i As Integer
        Dim Zeile As String
        Dim ok As Boolean
        Dim datum As DateTime
        Dim Werte(), Werte_temp() As String

        Try
            ' open file
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            ' get number of selected colums (time series) to be read
            ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)

            ' intialize a time series for every selected column (time series)
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i) = New TimeSeries(Me.SpaltenSel(i).Name)
            Next

            ' assign units to time series (for all selected)
            If (Me.UseEinheiten) Then
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).Unit = Me.SpaltenSel(i).Einheit
                Next
            End If

            ' read over header lines
            For i = 0 To Me.nZeilenHeader - 1
                StrReadSync.ReadLine()
            Next

            ' read date lines
            Do
                Zeile = StrReadSync.ReadLine.ToString()

                ' first empty space "" needs to be removed (otherwise date time format is not understood)
                'Zeile = Zeile.Substring(1, Zeile.Length - 1)
                Zeile = Trim(Zeile)


                If (Me.Zeichengetrennt) Then
                    ' data columns are separated by ";"

                    ' split data line into columns
                    Werte = Zeile.Split(New Char() {Me.Trennzeichen.Character})

                    ' first column ist date time, add date time to times series
                    ok = DateTime.TryParseExact(Werte(Me.XSpalte), Datumsformate("GISMO1"), Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        ok = DateTime.TryParseExact(Werte(Me.XSpalte), Datumsformate("GISMO2"), Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                        If Not ok Then
                            Throw New Exception("Kann das Datumsformat '" & Werte(Me.XSpalte) & "' nicht erkennen! " & eol & "Sollte in der Form '" & Datumsformate("GISMO1") & " oder " & Datumsformate("GISMO2") & "' vorliegen!")
                        End If
                    End If

                    ' remaining columns are data, add to time series
                    For i = 0 To Me.SpaltenSel.Length - 1
                        Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index)))
                    Next

                Else
                    ' data columns are separated by spaces
                    ' converge multiple spaces to one
                    Zeile = System.Text.RegularExpressions.Regex.Replace(Zeile, "\s{2,}", Me.Trennzeichen.Character)

                    ' the date time columns need to be moved to one column
                    Werte_temp = Zeile.Split(New Char() {Me.Trennzeichen.Character})
                    ReDim Werte(Werte_temp.Length - 2)
                    For i = 0 To Werte.Length - 1
                        Werte(i) = Werte_temp(i + 1)
                    Next
                    Werte(0) = Werte_temp(0) & " " & Werte_temp(1)

                    ' first column (now) is date time, add to time series
                    ok = DateTime.TryParseExact(Werte(Me.XSpalte), Datumsformate("GISMO1"), Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        ok = DateTime.TryParseExact(Werte(Me.XSpalte), Datumsformate("GISMO2"), Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                        If Not ok Then
                            Throw New Exception("Kann das Datumsformat '" & Werte(Me.XSpalte) & "' nicht erkennen! " & eol & "Sollte in der Form '" & Datumsformate("GISMO1") & " oder " & Datumsformate("GISMO2") & "' vorliegen!")
                        End If
                    End If

                    ' remaining columns are data, add to time series
                    For i = 0 To Me.SpaltenSel.Length - 1
                        Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index)))
                    Next

                End If

            Loop Until StrReadSync.Peek() = -1

            ' close file
            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            'catch errors
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub


    ''' <summary>
    ''' Checks, if the file is a GISMO result file (either *.CSV or *.ASC)
    ''' </summary>
    ''' <param name="file">file path</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String, Optional ByRef IsSSV As Boolean = False) As Boolean

        ' open file
        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
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
                Return True
            Else
                Return False
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
                Return True
            Else
                Return False
            End If

        End If

        ' close file
        StrRead.Close()
        FiStr.Close()

    End Function


#End Region 'Methods

End Class