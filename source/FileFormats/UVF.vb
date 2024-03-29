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
Imports System.Globalization
Imports System.Text.RegularExpressions

Namespace Fileformats

    ''' <summary>
    ''' Klasse für UVF Dateiformat
    ''' Formatbeschreibung: http://aquaplan.de/public_papers/imex/sectionUVF.html
    ''' </summary>
    Public Class UVF
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Error value
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ErrorValue As Double = -777.0

        ''' <summary>
        ''' Ob der Importdialog genutzt werden soll
        ''' </summary>
        ''' <value></value>
        ''' <returns>False</returns>
        ''' <remarks></remarks>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Instanziert ein neues UVF Objekt
        ''' </summary>
        ''' <param name="file">Pfad zur Datei</param>
        ''' <remarks></remarks>
        Public Sub New(file As String, Optional ReadAllNow As Boolean = False)

            Call MyBase.New(file)

            'Voreinstellungen
            Me.Dateformat = Helpers.DateFormats("UVF")
            Me.UseUnits = True

            'set default metadata keys
            Me.FileMetadata.AddKeys(UVF.MetadataKeys)

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Prüft, ob es sich um eine UVF-Datei handelt
        ''' </summary>
        ''' <param name="file">Pfad zur Datei</param>
        ''' <returns>Boolean</returns>
        ''' <remarks>Prüfung erfolgt anhand der Zeile *Z</remarks>
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim i As Integer
            Dim Zeile As String
            Dim headerFound As Boolean = False

            Try
                'Datei öffnen
                Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                Do
                    Zeile = StrReadSync.ReadLine.ToString()
                    i += 1
                    If Zeile.StartsWith("$") Then Continue Do ' Kommentarzeile
                    If Zeile.ToLower.StartsWith("*z") Then    ' Hier fängt der Header an
                        headerFound = True
                        Exit Do
                    End If

                Loop Until StrReadSync.Peek() = -1

                StrReadSync.Close()
                StrRead.Close()
                FiStr.Close()

                If Not headerFound Then
                    Throw New Exception("The file does not contain a header line starting with '*Z'!")
                End If

                Return True

            Catch ex As Exception
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Liest die Metadaten der in der Datei enthaltenen Zeitreihe aus
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readSeriesInfo()

            Dim i As Integer
            Dim Zeile As String
            Dim sInfo As TimeSeriesInfo
            Dim headerFound As Boolean = False

            Me.TimeSeriesInfos.Clear()

            'Header einlesen
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Do
                Zeile = StrReadSync.ReadLine.ToString()
                i += 1
                If Zeile.StartsWith("$") Then
                    'Kommentarzeile
                    'TODO: store comments as metadata
                    Continue Do
                ElseIf Zeile.ToLower.StartsWith("*z") Then    ' Hier fängt der Header an
                    headerFound = True
                    iLineHeadings = i + 1
                    iLineUnits = i + 1
                    iLineData = i + 4
                    Continue Do
                End If
                If i = iLineHeadings Then
                    'Zeitreihenname einlesen
                    Me.FileMetadata("name") = Zeile.Substring(0, 15).Trim()
                    'Einheit einlesen
                    Me.FileMetadata("unit") = Zeile.Substring(15, 15).Trim()
                    'DefArt oder Anfangsjahrhundert einlesen, falls vorhanden
                    If Zeile.Length > 30 Then
                        If Zeile.Substring(30, 1) = "I" Or
                           Zeile.Substring(30, 1) = "K" Or
                           Zeile.Substring(30, 1) = "M" Then
                            Me.FileMetadata("defArt") = Zeile.Substring(30, 1)
                        ElseIf Regex.IsMatch(Zeile.Substring(30, 4), "\d\d\d\d") Then
                            'Anfangsjahrhundert ist angegeben
                            Me.FileMetadata("century") = Zeile.Substring(30, 4)
                        End If
                    End If
                    'Anfangsjahrhundert auf 1900 setzen, falls nicht angegeben
                    If Me.FileMetadata("century") = "" Then
                        Me.FileMetadata("century") = "1900"
                        Log.AddLogEntry(Log.levels.warning, "Starting century is not specified in file header, assuming 1900.")
                    End If
                    Continue Do
                End If
                If i = iLineHeadings + 1 Then
                    'Ort und Lage einlesen
                    Try
                        Me.FileMetadata("location") = Zeile.Substring(0, Math.Min(Zeile.Length, 15)).Trim()
                        Me.FileMetadata("coord_X") = Zeile.Substring(15, 10).Trim()
                        Me.FileMetadata("coord_Y") = Zeile.Substring(25, 10).Trim()
                        Me.FileMetadata("coord_Z") = Zeile.Substring(35).Trim()
                        Exit Do
                    Catch ex As Exception
                        'do nothing
                    End Try
                End If

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'store series info

            sInfo = New TimeSeriesInfo()
            sInfo.Name = Me.FileMetadata("name")
            If Me.FileMetadata("location") <> "" Then
                'append location to series title
                sInfo.Name &= " - " & Me.FileMetadata("location")
            End If
            sInfo.Unit = Me.FileMetadata("unit")
            sInfo.Index = 0
            Me.TimeSeriesInfos.Add(sInfo)

            If Not headerFound Then
                Throw New Exception("The file does not contain a header line starting with '*Z'!")
            End If

        End Sub

        ''' <summary>
        ''' Liest die Datei ein
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readFile()

            Dim i, year, year_prev, century As Integer
            Dim Zeile As String
            Dim datumstring, datumstringExt As String
            Dim datum As DateTime
            Dim ok As Boolean
            Dim wert As Double
            Dim errorcount As Integer
            Dim sInfo As TimeSeriesInfo
            Dim ts As TimeSeries

            'Zeitreihe instanzieren
            sInfo = Me.TimeSeriesInfos(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

            'store metadata
            ts.Metadata = Me.FileMetadata

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Einlesen
            '--------

            'Header
            For i = 0 To Me.nLinesHeader - 1
                StrReadSync.ReadLine()
            Next

            'Daten
            century = Integer.Parse(Me.FileMetadata("century"))
            year_prev = Integer.Parse(century.ToString().Substring(2)) 'Aus Anfangsjahrhundert
            errorcount = 0
            Do
                Zeile = StrReadSync.ReadLine.ToString()
                'Datum lesen
                datumstring = Zeile.Substring(0, 10)
                year = Integer.Parse(datumstring.Substring(0, 2))
                'Jahrhundert bestimmen
                If year - year_prev < 0 Then
                    'neues Jahrhundert
                    century += 100
                End If
                year_prev = year
                'Jahrhundert voranstellen
                datumstringExt = century.ToString().Substring(0, 2) & datumstring
                'parse it
                ok = DateTime.TryParseExact(datumstringExt, Me.Dateformat, Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, datum)
                If (Not ok) Then
                    Throw New Exception($"Unable to parse the date '{datumstring}' using the given date format '{Me.Dateformat}'!")
                End If
                'Wert lesen
                wert = Helpers.StringToDouble(Zeile.Substring(10))
                If wert = UVF.ErrorValue Then
                    'convert error value to NaN
                    wert = Double.NaN
                    errorcount += 1
                End If
                'Stützstelle abspeichern
                ts.AddNode(datum, wert)

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If errorcount > 0 Then
                Log.AddLogEntry(Log.levels.warning, $"The file contained {errorcount} error values ({UVF.ErrorValue}), which were converted to NaN!")
            End If

            'store time series
            Me.TimeSeries.Add(sInfo.Index, ts)

        End Sub

        ''' <summary>
        ''' Returns a list of UVF-specific metadata keys
        ''' </summary>
        Public Overloads Shared ReadOnly Property MetadataKeys() As List(Of String)
            Get
                Dim keys As New List(Of String)
                keys.Add("name")
                keys.Add("unit")
                keys.Add("defArt")
                keys.Add("century")
                keys.Add("location")
                keys.Add("coord_X")
                keys.Add("coord_Y")
                keys.Add("coord_Z")
                Return keys
            End Get
        End Property

        ''' <summary>
        ''' Sets default metadata values for a time series corresponding to the UVF file format
        ''' </summary>
        Public Overloads Shared Sub setDefaultMetadata(ts As TimeSeries)
            'Make sure all required keys exist
            ts.Metadata.AddKeys(UVF.MetadataKeys)
            'Set default values
            ts.Metadata("century") = Math.Floor(ts.StartDate.Year / 100) * 100
            If ts.Metadata("name") = "" Then ts.Metadata("name") = ts.Title
            If ts.Metadata("unit") = "" Then ts.Metadata("unit") = ts.Unit
            If ts.Metadata("location") = "" Then ts.Metadata("location") = "unknown"
            If ts.Metadata("coord_X") = "" Then ts.Metadata("coord_X") = "0"
            If ts.Metadata("coord_Y") = "" Then ts.Metadata("coord_Y") = "0"
            If ts.Metadata("coord_Z") = "" Then ts.Metadata("coord_Z") = "0"
        End Sub

        ''' <summary>
        ''' Exports a time series to a file in the UVF format
        ''' </summary>
        ''' <param name="ts">the time series to export</param>
        ''' <param name="file">path to the file</param>
        ''' <remarks></remarks>
        Public Shared Sub Write_File(ByRef ts As TimeSeries, file As String)

            'Format specification:
            'http://aquaplan.de/public_papers/imex/sectionUVF.html

            Dim strwrite As StreamWriter
            Dim name, unit, century, timestamp, value As String
            Dim i As Integer

            'ensure that all required metadata keys are present
            ts.Metadata.AddKeys(UVF.MetadataKeys)

            strwrite = New StreamWriter(file, False, Helpers.DefaultEncoding)

            '1st line
            strwrite.WriteLine("*Z")
            '2nd line: name, unit and centuries
            name = ts.Metadata("name").PadRight(15).Substring(0, 15)
            unit = ts.Metadata("unit").PadRight(15).Substring(0, 15)
            century = Math.Floor(ts.StartDate.Year / 100) * 100 & " " & Math.Floor(ts.EndDate.Year / 100) * 100
            strwrite.WriteLine(name & unit & century)
            '3rd line: location
            strwrite.WriteLine(ts.Metadata("location").PadRight(15).Substring(0, 15) &
                           ts.Metadata("coord_X").PadRight(10).Substring(0, 10) &
                           ts.Metadata("coord_Y").PadRight(10).Substring(0, 10) &
                           ts.Metadata("coord_Z").PadRight(10).Substring(0, 10))
            '4th line: start and end date (without the first two digits)
            strwrite.WriteLine(ts.StartDate.ToString(DateFormats("UVF")).Substring(2) & ts.EndDate.ToString(DateFormats("UVF")).Substring(2))
            'from 5th line onwards: values
            For i = 0 To ts.Length - 1
                timestamp = ts.Dates(i).ToString(DateFormats("UVF")).Substring(2) 'without the first two digits
                'TODO: values < 1 have a leading zero and are technically one character too long!
                value = String.Format(Helpers.DefaultNumberFormat, "{0,9:g8}", ts.Values(i))
                strwrite.WriteLine($"{timestamp} {value}")
            Next
            strwrite.Close()

        End Sub

    End Class

End Namespace