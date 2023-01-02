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
    ''' Klasse für das WEL-Dateiformat von Hystem-Extran
    ''' Bei WEL-Dateien in Hystem handelt es sich immer um Zuflussdaten
    ''' Format ist festgeschrieben im HystemExtran-Anwenderhandbuch
    ''' </summary>
    ''' <remarks>Format siehe https://wiki.bluemodel.org/index.php/WEL-Format</remarks>
    Public Class HystemExtran_WEL
        Inherits TimeSeriesFile



#Region "Eigenschaften"

        Const maxSpalten_dT As Integer = 8
        Const HExt_welEinheit As String = "m3/s"
        Private AnzSpalten_dT() As Integer

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
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.iLineHeadings = 5
            Me.UseUnits = True
            Me.IsColumnSeparated = False
            Me.ColumnWidth = 10
            Me.ColumnOffset = 0
            Me.DecimalSeparator = Constants.period

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Datei komplett einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If


        End Sub

        ''' <summary>
        ''' Spaltenköpfe auslesen
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Dim i, j As Integer
            Dim Zeile As String = ""
            Dim ZeileSpalten As String = ""
            Dim ZeileEinheiten As String = ""
            Dim iZeileAnzSpalten As Integer = 4
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Zeile mit der Anzahl der Zeireihen finden
            For i = 1 To Me.iLineHeadings - 1
                Zeile = StrReadSync.ReadLine.ToString()
                If (i = iZeileAnzSpalten) Then ZeileSpalten = Zeile
            Next

            'Anzahl der Zeitreihen auslesen
            Dim anzSpalten As Integer
            anzSpalten = Convert.ToSingle(Right(ZeileSpalten, 5))

            'Anzahl der Zeilen und Spalten pro Zeitschritt ermitteln
            Me.nLinesPerTimestamp = Math.Ceiling(anzSpalten / maxSpalten_dT)
            ReDim AnzSpalten_dT(Me.nLinesPerTimestamp - 1)
            If Me.nLinesPerTimestamp = 1 Then
                AnzSpalten_dT(Me.nLinesPerTimestamp - 1) = anzSpalten
            ElseIf Me.nLinesPerTimestamp > 1 Then
                For i = 0 To Me.nLinesPerTimestamp - 2
                    AnzSpalten_dT(i) = maxSpalten_dT
                Next
                AnzSpalten_dT(Me.nLinesPerTimestamp - 1) = anzSpalten - (maxSpalten_dT * (Me.nLinesPerTimestamp - 1))
            End If

            'Spaltenköpfe (Zuflussknoten) und Indizes einlesen
            Dim index As Integer
            index = 1
            For i = 0 To Me.nLinesPerTimestamp - 1
                Zeile = StrReadSync.ReadLine.ToString()
                For j = 0 To AnzSpalten_dT(i) - 1
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = Zeile.Substring((j * Me.ColumnWidth) + ColumnOffset, Me.ColumnWidth).Trim()
                    sInfo.Unit = HExt_welEinheit
                    sInfo.Index = index
                    sInfo.Objekt = Trim(sInfo.Name)
                    sInfo.Type = "FLOW"
                    Me.TimeSeriesInfos.Add(sInfo)
                    index = index + 1
                Next
            Next

            'iZeileDaten kann erst jetzt gesetzt werden, wenn AnzZeilen_dT bekannt ist
            Me.iLineData = iLineHeadings + Me.nLinesPerTimestamp

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Zeitreihen einlesen
        ''' </summary>
        Public Overrides Sub readFile()

            Dim iZeile, i As Integer
            Dim Zeile As String
            Dim WerteString As String
            Dim datum As DateTime
            Dim ts As TimeSeries

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihen instanzieren
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                If Me.UseUnits Then
                    ts.Unit = sInfo.Unit
                    ts.Objekt = sInfo.Objekt
                    ts.Type = sInfo.Type
                End If
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            'Einlesen
            '--------

            'Header
            For iZeile = 1 To Me.iLineData - 1
                Zeile = StrReadSync.ReadLine.ToString()
            Next

            'Daten
            Do
                Zeile = StrReadSync.ReadLine.ToString()
                If Zeile.Substring(0, 5) = "*****" Then
                    Exit Do
                End If
                'Erste Zeile: Datum_Zeit
                datum = New System.DateTime(Zeile.Substring(6 + ColumnOffset, 4), Zeile.Substring(4 + ColumnOffset, 2), Zeile.Substring(2 + ColumnOffset, 2), Zeile.Substring(14 + ColumnOffset, 2), Zeile.Substring(16 + ColumnOffset, 2), 0, New System.Globalization.GregorianCalendar())
                'Restliche Zeilen pro Zeitschritt: Werte
                'Alle ausgewählten Spalten durchlaufen
                'Alle Abflusswerte einlesen
                WerteString = ""
                For i = 0 To Me.nLinesPerTimestamp - 1
                    WerteString = WerteString + StrReadSync.ReadLine.ToString()
                Next
                For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                    Me.TimeSeries(sInfo.Index).AddNode(datum, StringToDouble(WerteString.Substring(((sInfo.Index - 1) * Me.ColumnWidth) + ColumnOffset, Me.ColumnWidth)))
                Next

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Prüft, ob es sich um eine WEL-Datei für Hystem-Extran handelt
        ''' </summary>
        ''' <param name="file">Pfad zur Datei</param>
        ''' <returns></returns>
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim Zeile As String = ""

            '3 Zeilen einlesen
            Zeile = StrRead.ReadLine.ToString()
            Zeile = StrRead.ReadLine.ToString()
            Zeile = StrRead.ReadLine.ToString()

            StrRead.Close()
            FiStr.Close()

            Try
                If (Zeile.Length = 40) And (Left(Zeile, 2) = "  ") And (Zeile.Substring(10, 4) = "    ") Then
                    'Es ist eine Extran-Regenreihe!
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                'höchstwahrscheinlich keine Extran-Regenreihe
                Return False
            End Try

        End Function
#End Region 'Methoden

    End Class

End Namespace