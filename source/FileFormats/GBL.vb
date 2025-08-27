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
        ''' Possible data types of values
        ''' </summary>
        Private Enum DataTypes As Integer
            [Integer] = 1
            [Single] = 2
            [Double] = 3
            [Boolean] = 4
        End Enum

        ''' <summary>
        ''' Lengths in bytes for different data types
        ''' </summary>
        Private Shared ReadOnly DataTypeLengths As New Dictionary(Of DataTypes, Integer) From {
        {DataTypes.Integer, 4},
        {DataTypes.Single, 4},
        {DataTypes.Double, 8},
        {DataTypes.Boolean, 1}
    }

        ''' <summary>
        ''' The data type of values in this file instance
        ''' </summary>
        Private DataType As DataTypes

        ''' <summary>
        ''' The length in bytes of the values in this file instance
        ''' </summary>
        ''' <returns></returns>
        Private ReadOnly Property DataTypeLength As Integer
            Get
                Return GBL.DataTypeLengths(Me.DataType)
            End Get
        End Property

        Private Shared ReadOnly ColumnNames As String() = {
            "Qzu", "Qab", "Pges_zu", "Pges_ab",
            "AFS_zu", "AFS_ab", "AFS63_zu", "AFS63_ab", "NH4-N_zu", "NH4-N_ab",
            "CSB_zu", "CSB_ab", "T_zu", "T_ab", "pH_zu",
            "pH_ab", "O2_zu", "O2_ab", "NH3_zu", "k2", "tf"
        }

        Private Shared ReadOnly ColumnUnits As String() = {
            "l/s", "l/s", "mg/l", "mg/l",
            "mg/l", "mg/l", "mg/l", "mg/l", "mg/l", "mg/l",
            "mg/l", "mg/l", "°C", "°C", "-",
            "-", "mg/l", "mg/l", "mg/l", "-", "min"
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
            Me.DataType = DataTypes.Single ' Fixed data type for GBL format

            ' Create 21 time series info objects for the fixed columns
            For i As Integer = 0 To 20 ' 21 columns (0-20)
                Dim sInfo As New TimeSeriesInfo()
                sInfo.Name = ColumnNames(i)
                sInfo.Unit = ColumnUnits(i)
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
                'skip first record (header) - 92 bytes für Fortran direct access
                'reader.ReadBytes(92)

                'read data records
                errorcount = 0
                Do Until reader.BaseStream.Position >= reader.BaseStream.Length
                    Try
                        'read date (8 bytes)
                        rdate = reader.ReadDouble()
                        'convert real date to DateTime
                        timestamp = BIN.rDateToDate(rdate)

                        'read all 21 values (each 4 bytes = Single)
                        Dim allValues(20) As Single
                        For i As Integer = 0 To 20
                            allValues(i) = reader.ReadSingle()
                        Next

                        'add selected values to time series
                        For Each index As Integer In selectedIndices
                            value = allValues(index)

                            'convert error values to NaN
                            If Math.Abs(value - BIN.ErrorValue) < 0.0001 Then
                                value = Double.NaN
                                errorcount += 1
                            End If

                            'add node to time series
                            Me.TimeSeries(index).AddNode(timestamp, value)
                        Next

                        'skip padding bytes to complete 92-byte record if needed
                        Dim bytesRead As Integer = 8 + (21 * 4) ' 8 + 84 = 92
                        If bytesRead < 92 Then
                            reader.ReadBytes(92 - bytesRead)
                        End If

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