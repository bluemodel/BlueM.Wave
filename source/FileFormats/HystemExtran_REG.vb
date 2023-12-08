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
    ''' Klasse für das Hystem-Extran REG-Dateiformat
    ''' </summary>
    ''' <remarks>Format siehe https://wiki.bluemodel.org/index.php/Hystem-Extran_REG-Format</remarks>
    Public Class HystemExtran_REG
        Inherits TimeSeriesFile

        Const LenString As Integer = 5   'Länge des Strings eines Wertes in der reg/dat-Datei
        Const fehlWert As String = "-9999" 'Fehlwert / Ausfall

#Region "Eigenschaften"

        'Eigenschaften
        '#############

        Private _Zeitintervall As Integer
        Private _DezFaktor As Integer

#End Region

#Region "Properties"

        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return False
            End Get
        End Property

        Private Property Zeitintervall() As Integer
            Get
                Return _Zeitintervall
            End Get
            Set(value As Integer)
                _Zeitintervall = value
            End Set
        End Property

        Private Property DezFaktor() As Integer
            Get
                Return _DezFaktor
            End Get
            Set(value As Integer)
                _DezFaktor = value
            End Set
        End Property

        Private Shared ReadOnly Property WerteProZeile(dt As Integer) As Integer
            Get
                Select Case dt  'siehe KN-Anwenderhandbuch S.384
                    Case 1, 5, 10, 15, 20, 30, 60, 120
                        Return 12
                    Case 2, 3
                        Return 10
                    Case 720
                        Return 2
                    Case 1440
                        Return 10 'TODO: Eigentlich 10 Werte für die ersten zwei Zeilen eines Monats, dann variabel für den Rest des Monats
                    Case Else
                        Throw New Exception($"Number of entries per line for time step of {dt} minutes is undefined!")
                End Select
            End Get
        End Property

#End Region

#Region "Methoden"

        'Methoden
        '########

        'Konstruktor
        '***********
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.iLineData = 6
            Me.UseUnits = True
            Me.Dateformat = Helpers.DateFormats("HYSTEMEXTRAN")

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        'Spalten auslesen
        '****************
        Public Overrides Sub readSeriesInfo()

            Dim Zeile As String
            Dim title As String
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            sInfo = New TimeSeriesInfo()
            sInfo.Index = 0

            'Reihentitel steht in 1. Zeile, ist aber optional:
            Zeile = StrReadSync.ReadLine.ToString()
            title = Zeile.Substring(20, 30).Trim()
            If title.Length = 0 Then title = Path.GetFileName(Me.File)
            sInfo.Name = title

            'Einheit steht in 2. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            sInfo.Unit = Zeile.Substring(68, 10).Trim()

            'store series info
            Me.TimeSeriesInfos.Add(sInfo)

            'read additional info
            Me.DezFaktor = Zeile.Substring(25, 5)

            'Zeitintervall auslesen
            Me.Zeitintervall = Convert.ToSingle(Zeile.Substring(20, 5).Trim)

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        'REG-Datei einlesen
        '******************
        Public Overrides Sub readFile()

            Dim i, j As Integer
            Dim Zeile, wertString As String
            Dim kennzeichnung As String
            Dim Datum, Zeilendatum As DateTime
            Dim wert As Double
            Dim sInfo As TimeSeriesInfo
            Dim ts As TimeSeries

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihe instanzieren (nur eine)
            sInfo = Me.TimeSeriesInfos(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

            'Einlesen
            '--------
            j = 0

            Do
                j += 1
                Zeile = StrReadSync.ReadLine.ToString()

                If Zeile.Substring(19, 1) = "E" Then Exit Do

                If (j > Me.nLinesHeader And Zeile.Length > 0) Then

                    'Kennzeichnung lesen
                    kennzeichnung = Zeile.Substring(19, 1)

                    'Parse date of line
                    Dim dateString As String = Zeile.Substring(5, 14)
                    Dim success As Boolean = DateTime.TryParseExact(dateString.Replace(" ", 0), Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, Zeilendatum)
                    If Not success Then
                        Throw New Exception($"Unable to parse the date '{dateString}'!")
                    End If

                    'Datum und Wert zur Zeitreihe hinzufügen
                    '---------------------------------------
                    Select Case kennzeichnung
                        Case " ", "S" 'normale Datenzeile oder Datenzeile mit Ausfällen
                            'alle bis auf den letzten Wert einlesen
                            'beim letzten Wert besteht die Möglichkeit, dass nicht alle Zeichen belegt sind
                            For i = 0 To HystemExtran_REG.WerteProZeile(Me.Zeitintervall) - 1
                                Datum = Zeilendatum.AddMinutes(i * Me.Zeitintervall)
                                wertString = Zeile.Substring(20 + LenString * i, LenString)
                                If wertString = fehlWert Then
                                    wert = Double.NaN
                                Else
                                    wert = StringToDouble(wertString) * 10 ^ (DezFaktor)
                                End If
                                ts.AddNode(Datum, wert)
                            Next
                        Case "K" 'Konstantsatz
                            ' nur ein Wert für die gesamte Zeile
                            wertString = Zeile.Substring(20)
                            If wertString = fehlWert Then
                                wert = Double.NaN
                            Else
                                wert = StringToDouble(wertString) * 10 ^ (DezFaktor)
                            End If
                            For i = 0 To HystemExtran_REG.WerteProZeile(Me.Zeitintervall) - 1
                                Datum = Zeilendatum.AddMinutes(i * Me.Zeitintervall)
                                ts.AddNode(Datum, wert)
                            Next
                        Case "A" 'Ausfallsatz, keine Daten
                            wert = Double.NaN
                            For i = 0 To HystemExtran_REG.WerteProZeile(Me.Zeitintervall) - 1
                                Datum = Zeilendatum.AddMinutes(i * Me.Zeitintervall)
                                ts.AddNode(Datum, wert)
                            Next
                        Case "N" 'Nullsatz, keine Daten
                            Continue Do
                        Case Else
                            Log.AddLogEntry(Log.levels.warning, $"Unrecognized character {kennzeichnung} in line {j} column 20!")
                    End Select

                End If
            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'store time series
            Me.TimeSeries.Add(sInfo.Index, ts)

        End Sub

        ''' <summary>
        ''' Exportiert eine Zeitreihe als REG-Datei
        ''' </summary>
        ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
        ''' <param name="File">Pfad zur anzulegenden Datei</param>
        Public Shared Sub Write_File(Reihe As TimeSeries, File As String)

            Dim dt As Integer
            Dim KontiReihe As TimeSeries
            Const iDim As Integer = -3        'Dezimalfaktor wird erstmal global auf -3 gesetzt

            'Zeitintervall aus ersten und zweiten Zeitschritt der Reihe ermitteln
            dt = DateDiff(DateInterval.Minute, Reihe.Dates(0), Reihe.Dates(1))

            'Äquidistante Zeitreihe erzeugen
            KontiReihe = Reihe.ChangeTimestep(BlueM.Wave.TimeSeries.TimeStepTypeEnum.Minute, dt, Reihe.StartDate, BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight)

            Dim strwrite As StreamWriter
            Dim iZeile, j, n As Integer
            Dim WerteproZeile As Integer = HystemExtran_REG.WerteProZeile(dt)
            strwrite = New StreamWriter(File)
            Dim IntWert As Long

            '1. Zeile
            Dim title As String = Reihe.Title
            If title.Length > 30 Then
                title = title.Substring(0, 30)
            End If
            strwrite.WriteLine($"TUD   0 0   0 1 0 0 {title.PadRight(30)}       0        0           0")

            '2. Zeile: 
            'Standard
            strwrite.Write("TUD   0 0   0 2 0 0 ")
            'Zeitintervall
            strwrite.Write(dt.ToString.PadLeft(5))
            'Dimension der Zehnerprotenz
            strwrite.Write(iDim.ToString.PadLeft(5))
            'Anfangsdatum
            strwrite.Write(KontiReihe.StartDate.ToString(Helpers.DateFormats("HYSTEMEXTRAN")))
            'Enddatum
            strwrite.Write(KontiReihe.EndDate.ToString(Helpers.DateFormats("HYSTEMEXTRAN")))
            'Anzahl der Kommentarzeilen nach Zeile 2, wird = 3 gesetzt
            strwrite.Write("    3")
            'Art der Daten, N = Niederschlag, Q = Abfluss
            strwrite.Write("N    ")
            'Einheit
            strwrite.WriteLine(Reihe.Unit.PadRight(10))

            '3. Zeile: 
            strwrite.WriteLine("TUD   0 0   0 3 0 0 Beginn         Kommentarzeile 1                        Ende")

            '4. Zeile: Anfangs- und Enddatum
            strwrite.WriteLine("TUD   0 0   0 4 0 0 Beginn         Kommentarzeile 2                        Ende")

            '5. Zeile: Werte
            strwrite.WriteLine("TUD   0 0   0 5 0 0 Beginn         Kommentarzeile 3                        Ende")

            n = 0   'n = Anzahl der Zeitreihenwerte
            For iZeile = 0 To (KontiReihe.Length / WerteproZeile) - 1
                strwrite.Write("TUD  ")
                strwrite.Write(KontiReihe.Dates(n).ToString(Helpers.DateFormats("HYSTEMEXTRAN")) & " ")
                For j = 1 To WerteproZeile
                    If n > KontiReihe.Length - 1 Then
                        'falls keine Werte mehr vorhanden Zeile mit Fehlwerten auffüllen
                        IntWert = fehlWert
                    ElseIf Double.IsNaN(KontiReihe.Values(n)) Then
                        IntWert = fehlWert
                    Else
                        IntWert = KontiReihe.Values(n) * 10 ^ (-iDim)
                    End If
                    strwrite.Write(IntWert.ToString.PadLeft(5))
                    n = n + 1
                Next
                strwrite.WriteLine()
            Next
            strwrite.WriteLine("TUD   0 09999 0 0 0E")
            strwrite.Close()

        End Sub

        ''' <summary>
        ''' Prüft, ob es sich um eine HystemExtran-REG-Datei handelt
        ''' </summary>
        ''' <param name="file">Pfad zur Datei</param>
        ''' <returns></returns>
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim Zeile As String = ""

            '1 Zeile einlesen
            Zeile = StrRead.ReadLine.ToString()

            StrRead.Close()
            FiStr.Close()

            If Zeile.Length >= 13 Then
                If (Zeile.Substring(5, 8) = " 0 0   0") Then
                    'Es ist eine Hystem-Regenreihe!
                    Return True
                End If
            End If

            Return False

        End Function

#End Region 'Methoden

    End Class

End Namespace