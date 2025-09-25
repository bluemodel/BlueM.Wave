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
    ''' Class for the GINA binary WEL format
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GBL
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Column definitions with name and unit pairs for GBL format
        ''' </summary>
        Private Shared ReadOnly ColumnDefinitions As (Name As String, Unit As String)() = {
            ("Qzu", "l/s"),
            ("Qab", "l/s"),
            ("Pges_zu", "mg/l"),
            ("Pges_ab", "mg/l"),
            ("AFS_zu", "mg/l"),
            ("AFS_ab", "mg/l"),
            ("AFS63_zu", "mg/l"),
            ("AFS63_ab", "mg/l"),
            ("NH4-N_zu", "mg/l"),
            ("NH4-N_ab", "mg/l"),
            ("CSB_zu", "mg/l"),
            ("CSB_ab", "mg/l"),
            ("T_zu", "°C"),
            ("T_ab", "°C"),
            ("pH_zu", "-"),
            ("pH_ab", "-"),
            ("O2_zu", "mg/l"),
            ("O2_ab", "mg/l"),
            ("NH3_zu", "mg/l"),
            ("k2", "-"),
            ("tf", "min")
        }

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.Dateformat = Helpers.CurrentDateFormat 'irrelevant because binary
            Me.iLineData = 0
            Me.UseUnits = True

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Reads series info for fixed GBL format structure
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Me.TimeSeriesInfos.Clear()

            ' GBL format has fixed structure: 8 bytes date + 21 * 4 bytes data columns

            ' Create 21 time series info objects for the fixed columns
            For i As Integer = 0 To ColumnDefinitions.Length - 1
                Dim sInfo As New TimeSeriesInfo()
                sInfo.Name = ColumnDefinitions(i).Name
                sInfo.Unit = ColumnDefinitions(i).Unit
                sInfo.Index = i
                Me.TimeSeriesInfos.Add(sInfo)
            Next

            Log.AddLogEntry(Log.levels.debug, $"GBL format: Created {Me.TimeSeriesInfos.Count} fixed time series definitions")

        End Sub

        ''' <summary>
        ''' Reads the file with fixed GBL format structure
        ''' </summary>
        Public Overrides Sub readFile()

            Dim rdate As Double
            Dim timestamp As DateTime
            Dim value As Single
            Dim errorcount As Integer

            'instantiate time series
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                Dim ts As New TimeSeries(sInfo.Name)
                ts.Unit = sInfo.Unit
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            'get a sorted list of selected indices
            Dim selectedIndices As New List(Of Integer)
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                selectedIndices.Add(sInfo.Index)
            Next
            selectedIndices.Sort()

            Using reader As New IO.BinaryReader(IO.File.OpenRead(File), Text.ASCIIEncoding.ASCII)
                'read data records
                errorcount = 0
                Do Until reader.BaseStream.Position >= reader.BaseStream.Length
                    Try
                        'read date (8 bytes)
                        rdate = reader.ReadDouble()
                        'convert real date to DateTime
                        timestamp = BIN.rDateToDate(rdate)

                        'loop over all columns (each 4 bytes = Single)
                        For i As Integer = 0 To GBL.ColumnDefinitions.Length - 1
                            If selectedIndices.Contains(i) Then
                                'read value
                                value = reader.ReadSingle()
                                'convert error values to NaN
                                If Math.Abs(value - BIN.ErrorValue) < 0.0001 Then
                                    value = Double.NaN
                                    errorcount += 1
                                End If
                                'add node to time series
                                Me.TimeSeries(i).AddNode(timestamp, value)
                            Else
                                'skip value
                                reader.ReadBytes(4)
                            End If
                        Next

                    Catch ex As EndOfStreamException
                        Exit Do
                    End Try
                Loop
            End Using

            'Log 
            If Me.TimeSeries.Count > 0 Then
                Call Log.AddLogEntry(Log.levels.info, $"Read {Me.TimeSeries.Count} time series with {Me.TimeSeries.First.Value.Length} nodes each.")
            End If
            If errorcount > 0 Then
                Log.AddLogEntry(Log.levels.warning, $"The file contained {errorcount} error values ({BIN.ErrorValue}), which were converted to NaN!")
            End If

        End Sub

        ''' <summary>
        ''' Checks whether a file is a GINA binary file
        ''' </summary>
        ''' <param name="file">path to the file to check</param>
        ''' <returns>True if verification was successful</returns>
        ''' <remarks>Adapted from Fortran routine FILE_GETRECL (formerly ZRE_GETRECL)</remarks>
        Public Shared Function verifyFormat(file As String) As Boolean
            ' Für GBL-Format: Prüfung auf Dateiendung und Dateigröße (Vielfaches von 92)
            If Not file.ToLower().EndsWith(".gbl") Then Return False

            Dim fileInfo As New IO.FileInfo(file)
            ' Prüfen, ob Dateigröße ein Vielfaches von 92 ist
            Return (fileInfo.Length Mod 92 = 0) AndAlso (fileInfo.Length > 0)
        End Function

    End Class

End Namespace