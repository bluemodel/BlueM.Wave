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

        ''' <summary>
        ''' Column indices for 20-byte format (Qzu, Qab, tf)
        ''' </summary>
        Private Shared ReadOnly Format2ColumnIndices As Integer() = {0, 1, 20}

        ''' <summary>
        ''' Format type read from header: 1 for 92-byte records, 2 for 20-byte records
        ''' </summary>
        Private RecordFormat As Integer

        ''' <summary>
        ''' Number of data columns based on format type
        ''' </summary>
        Private ColumnCount As Integer

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
        ''' Reads series info for GBL format structure based on header
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Me.TimeSeriesInfos.Clear()

            ' Read header to determine format
            Using reader As New IO.BinaryReader(IO.File.OpenRead(File), Text.ASCIIEncoding.ASCII)
                ' Read header marker (first 8 bytes)
                Dim headerMarker As Double = reader.ReadDouble()

                ' Validate header
                If Math.Abs(headerMarker - (-999.0)) > 0.0001 Then
                    Throw New Exception($"Invalid GBL file header. Expected marker -999.0, found {headerMarker}")
                End If

                ' Read format marker (next 4 bytes)
                Dim formatMarker As Single = reader.ReadSingle()

                ' Determine format based on marker
                If Math.Abs(formatMarker - (-1.0F)) < 0.0001F Then
                    ' Format 1: 92-byte records (21 columns)
                    Me.RecordFormat = 1
                    Me.ColumnCount = 21
                    ' Skip remaining bytes of header (80 bytes: 20 more singles = 20 * 4 bytes)
                    reader.ReadBytes(80)
                    Log.AddLogEntry(Log.levels.debug, "GBL format: Detected format 1 (92-byte records, 21 columns)")
                ElseIf Math.Abs(formatMarker - (-2.0F)) < 0.0001F Then
                    ' Format 2: 20-byte records (3 columns: Qzu, Qab, tf)
                    Me.RecordFormat = 2
                    Me.ColumnCount = 3
                    ' Skip remaining bytes of header (8 bytes: 2 more singles = 2 * 4 bytes)
                    reader.ReadBytes(8)
                    Log.AddLogEntry(Log.levels.debug, "GBL format: Detected format 2 (20-byte records, 3 columns: Qzu, Qab, tf)")
                Else
                    Throw New Exception($"Invalid GBL format marker. Expected -1.0 or -2.0, found {formatMarker}")
                End If
            End Using

            ' Create time series info objects based on detected format
            If Me.RecordFormat = 1 Then
                ' Format 1: All 21 columns
                For i As Integer = 0 To Me.ColumnCount - 1
                    Dim sInfo As New TimeSeriesInfo()
                    sInfo.Name = ColumnDefinitions(i).Name
                    sInfo.Unit = ColumnDefinitions(i).Unit
                    sInfo.Index = i
                    Me.TimeSeriesInfos.Add(sInfo)
                Next
            Else
                ' Format 2: Only Qzu (index 0), Qab (index 1), and tf (index 20)
                For i As Integer = 0 To Me.ColumnCount - 1
                    Dim columnIndex As Integer = Format2ColumnIndices(i)
                    Dim sInfo As New TimeSeriesInfo()
                    sInfo.Name = ColumnDefinitions(columnIndex).Name
                    sInfo.Unit = ColumnDefinitions(columnIndex).Unit
                    sInfo.Index = i ' Use sequential index for format 2
                    Me.TimeSeriesInfos.Add(sInfo)
                Next
            End If

            Log.AddLogEntry(Log.levels.debug, $"GBL format: Created {Me.TimeSeriesInfos.Count} time series definitions")

        End Sub

        ''' <summary>
        ''' Reads the file with GBL format structure
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
                ' Skip header record (full record: 92 or 20 bytes depending on format)
                Dim headerSize As Integer = If(Me.RecordFormat = 1, 92, 20)
                reader.ReadBytes(headerSize)

                'read data records
                errorcount = 0
                Do Until reader.BaseStream.Position >= reader.BaseStream.Length
                    Try
                        'read date (8 bytes)
                        rdate = reader.ReadDouble()
                        'convert real date to DateTime
                        timestamp = BIN.rDateToDate(rdate)

                        'loop over columns based on format (each 4 bytes = Single)
                        For i As Integer = 0 To Me.ColumnCount - 1
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
            ' Check file extension
            If Not file.ToLower().EndsWith(".gbl") Then Return False

            Dim fileInfo As New IO.FileInfo(file)
            If fileInfo.Length < 20 Then Return False ' At least a 20-byte header must be present

            Try
                ' Read and validate header
                Using reader As New IO.BinaryReader(IO.File.OpenRead(file), Text.ASCIIEncoding.ASCII)
                    Dim headerMarker As Double = reader.ReadDouble()
                    Dim formatMarker As Single = reader.ReadSingle()

                    ' Validate header marker
                    If Math.Abs(headerMarker - (-999.0)) > 0.0001 Then Return False

                    ' Determine expected record size based on format marker
                    Dim recordSize As Integer
                    If Math.Abs(formatMarker - (-1.0F)) < 0.0001F Then
                        recordSize = 92 ' 8 bytes date + 21 * 4 bytes data
                    ElseIf Math.Abs(formatMarker - (-2.0F)) < 0.0001F Then
                        recordSize = 20 ' 8 bytes date + 3 * 4 bytes data
                    Else
                        Return False ' Invalid format marker
                    End If

                    ' Validate file size: must be a multiple of recordSize (header + data records)
                    ' File size = n * recordSize where n >= 1 (at least header present)
                    Return (fileInfo.Length Mod recordSize = 0) AndAlso (fileInfo.Length >= recordSize)
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class

End Namespace