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
''' Class to import of TALSIM result files (*.WEL)
''' For information about TALSIM refer to http://www.sydro.de/
''' For file format info refer to http://wiki.bluemodel.org/index.php/Wave
''' </summary>

Public Class WEL_TALSIM
    Inherits Dateiformat

#Region "Eigenschaften"

    'Eigenschaften
    '#############


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
        Me.iZeileUeberschriften = 2
        Me.UseEinheiten = True
        Me.iZeileEinheiten = 3
        Me.iZeileDaten = 4
        Me.Zeichengetrennt = True
        Me.Trennzeichen = Me.semikolon
        Me.Dezimaltrennzeichen = Me.punkt
        Me.DateTimeLength = 17

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If


    End Sub

    ''' <summary>
    ''' Spalten auslesen
    ''' </summary>
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""
        Dim LineInfo As String = ""

        Try
            'Datei �ffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Spalten�berschriften auslesen
            For i = 1 To Me.iZeileDaten
                Zeile = StrReadSync.ReadLine.ToString()
                If (i = Me.iLineInfo) Then LineInfo = Zeile
                If (i = Me.iZeileUeberschriften) Then ZeileSpalten = Zeile
                If (i = Me.iZeileEinheiten) Then ZeileEinheiten = Zeile
            Next

            'Are columns separted by ";" or should fixed format be used?
            If ZeileSpalten.Contains(";") Then
                Me.Zeichengetrennt = True
            Else
                Me.Zeichengetrennt = False
            End If

            'Is it a WEL or EFL_WEL file
            If LineInfo.Contains("EFL-WEL") Then
                Me.Spaltenbreite = 41
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

            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                anzSpalten = Namen.Length
                ReDim Me.Spalten(anzSpalten - 1)
                For i = 0 To anzSpalten - 1
                    Me.Spalten(i).Name = Namen(i).Trim()
                    Me.Spalten(i).Einheit = Einheiten(i).Trim()
                    Me.Spalten(i).Index = i
                Next
            Else
                'Spalten mit fester Breite
                anzSpalten = Math.Ceiling((ZeileSpalten.Length - Me.DateTimeLength) / Me.Spaltenbreite) + 1
                ReDim Me.Spalten(anzSpalten - 1)
                For i = 0 To anzSpalten - 1
                    If i = 0 Then
                        'DateTime need to be considered especially as it can be shorter than the standard "Spaltenbreite"
                        Me.Spalten(i).Name = ZeileSpalten.Substring(0, Me.DateTimeLength) '.Trim()
                        Me.Spalten(i).Einheit = ZeileEinheiten.Substring(0, Me.DateTimeLength) '.Trim()
                        Me.Spalten(i).Index = i
                    Else
                        Me.Spalten(i).Name = ZeileSpalten.Substring(Me.DateTimeLength + (i - 1) * Me.Spaltenbreite, Me.Spaltenbreite).Trim()
                        Me.Spalten(i).Einheit = ZeileEinheiten.Substring(Me.DateTimeLength + (i - 1) * Me.Spaltenbreite, Me.Spaltenbreite).Trim()
                        Me.Spalten(i).Index = i
                    End If
                Next
            End If

        Catch ex As Exception
            MsgBox("Konnte die Datei '" & Path.GetFileName(Me.File) & "' nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    ''' <summary>
    ''' WEL-Datei einlesen
    ''' </summary>
    Public Overrides Sub Read_File()

        Dim iZeile, i As Integer
        Dim Zeile As String
        Dim Werte() As String
        Dim datum As DateTime

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Anzahl Zeitreihen bestimmen
        ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)

        'Zeitreihen instanzieren
        For i = 0 To Me.SpaltenSel.Length - 1
            Me.Zeitreihen(i) = New TimeSeries(Me.SpaltenSel(i).Name)
        Next

        'Einheiten?
        If (Me.UseEinheiten) Then
            'Alle ausgew�hlten Spalten durchlaufen
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i).Unit = Me.SpaltenSel(i).Einheit
            Next
        End If

        'Einlesen
        '--------

        'Header
        For iZeile = 1 To Me.nZeilenHeader
            Zeile = StrReadSync.ReadLine.ToString()
        Next

        'Daten
        Do
            Zeile = StrReadSync.ReadLine.ToString()

            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                '---------------
                Werte = Zeile.Split(New Char() {Me.Trennzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                'Erste Spalte: Datum_Zeit
                datum = New System.DateTime(Werte(0).Substring(6, 4), Werte(0).Substring(3, 2), Werte(0).Substring(0, 2), Werte(0).Substring(11, 2), Werte(0).Substring(14, 2), 0, New System.Globalization.GregorianCalendar())
                'Restliche Spalten: Werte
                'Alle ausgew�hlten Spalten durchlaufen
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index)))
                Next
            Else
                'Spalten mit fester Breite
                '-------------------------
                'Erste Spalte: Datum_Zeit
                datum = New System.DateTime(Zeile.Substring(6 + SpaltenOffset, 4), Zeile.Substring(3 + SpaltenOffset, 2), Zeile.Substring(0 + SpaltenOffset, 2), Zeile.Substring(11 + SpaltenOffset, 2), Zeile.Substring(14 + SpaltenOffset, 2), 0, New System.Globalization.GregorianCalendar())
                'Restliche Spalten: Werte
                'Alle ausgew�hlten Spalten durchlaufen
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).AddNode(datum, StringToDouble(Zeile.Substring((Me.SpaltenSel(i).Index * Me.Spaltenbreite) + SpaltenOffset, Math.Min(Me.Spaltenbreite, Zeile.Substring((Me.SpaltenSel(i).Index * Me.Spaltenbreite) + SpaltenOffset).Length))))
                Next
            End If

        Loop Until StrReadSync.Peek() = -1

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Pr�ft, ob es sich um eine WEL-Datei f�r BlueM handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        Zeile = StrRead.ReadLine.ToString()
        StrRead.Close()
        FiStr.Close()

        If (Zeile.StartsWith("*WEL")) Then
            'It's a TALSIM WEL file
            Return True
        Else
            Return False
        End If

    End Function

#End Region 'Methoden

End Class