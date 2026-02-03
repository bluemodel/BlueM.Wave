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
Imports PureHDF
Imports PureHDF.VOL.Native

Namespace Fileformats

    ''' <summary>
    ''' Class for importing HDF5 files with timeseries data
    ''' 
    ''' Expected structure:
    ''' /timeseries/
    '''   /<group> (e.g., HNW, SN1, SN2)
    '''     /time (string dataset with timestamps) - required
    '''     /<element_name> (2D dataset with Real values) - at least one required
    ''' 
    ''' Requires the PureHDF NuGet package: https://www.nuget.org/packages/PureHDF/
    ''' </summary>
    Public Class HDF5
        Inherits TimeSeriesFile

        ''' <summary>
        ''' The name of the child group containing the data (e.g., HNW, SN1, SN2)
        ''' </summary>
        Private _dataGroupName As String

        ''' <summary>
        ''' Flag indicating whether to show the import dialog
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="FileName">Path to the HDF5 file</param>
        ''' <param name="ReadAllNow">If True, read all series immediately</param>
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            Me.UseUnits = False

            Call Me.readSeriesInfo()

            If ReadAllNow Then
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Reads series info from the HDF5 file
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Dim sInfo As TimeSeriesInfo
            Dim index As Integer = 0

            Me.TimeSeriesInfos.Clear()

            Try
                Using h5File As NativeFile = PureHDF.H5File.OpenRead(Me.File)

                    'Navigate to the timeseries group
                    If Not h5File.LinkExists("timeseries") Then
                        Throw New Exception("HDF5 file does not contain a 'timeseries' group!")
                    End If

                    Dim timeseriesGroup As NativeGroup = h5File.Group("timeseries")

                    'Find the first child group (HNW, SN1, SN2, etc.)
                    Dim dataGroup As NativeGroup = Nothing
                    Dim hasTimeDataset As Boolean = False

                    For Each child As NativeObject In timeseriesGroup.Children()
                        If TypeOf child Is NativeGroup Then
                            Dim candidateGroup As NativeGroup = CType(child, NativeGroup)
                            'Check if this group contains a 'time' dataset
                            If candidateGroup.LinkExists("time") Then
                                dataGroup = candidateGroup
                                _dataGroupName = child.Name
                                hasTimeDataset = True
                                Exit For
                            End If
                        End If
                    Next

                    If dataGroup Is Nothing Then
                        Throw New Exception("No data group found under 'timeseries' group!")
                    End If

                    If Not hasTimeDataset Then
                        Throw New Exception($"Data group '{_dataGroupName}' does not contain a required 'time' dataset!")
                    End If

                    Log.AddLogEntry(Log.levels.info, $"Found data group: {_dataGroupName}")

                    'Read dataset names (excluding 'time' which contains timestamps)
                    For Each child As NativeObject In dataGroup.Children()
                        If child.Name.ToLower() <> "time" Then
                            'Check if it's a dataset
                            If TypeOf child Is NativeDataset Then
                                sInfo = New TimeSeriesInfo()
                                sInfo.Name = child.Name
                                sInfo.Unit = "-"
                                sInfo.Index = index
                                Me.TimeSeriesInfos.Add(sInfo)
                                index += 1
                            End If
                        End If
                    Next

                    If Me.TimeSeriesInfos.Count = 0 Then
                        Throw New Exception($"Data group '{_dataGroupName}' does not contain any element datasets!")
                    End If

                End Using

                Log.AddLogEntry(Log.levels.info, $"Found {Me.TimeSeriesInfos.Count} series in HDF5 file.")

            Catch ex As Exception
                Log.AddLogEntry(Log.levels.error, $"Error reading HDF5 file: {ex.Message}")
                Throw
            End Try

        End Sub

        ''' <summary>
        ''' Reads the selected series from the HDF5 file
        ''' </summary>
        Public Overrides Sub readFile()

            If Me.SelectedSeries.Count = 0 Then
                Throw New Exception("No series selected for import!")
            End If

            Try
                Using h5File As NativeFile = PureHDF.H5File.OpenRead(Me.File)

                    'Navigate to the data group
                    Dim dataGroup As NativeGroup = h5File.Group($"timeseries/{_dataGroupName}")

                    'Read timestamps from the 'time' dataset
                    Dim timeDataset As NativeDataset = dataGroup.Dataset("time")
                    Dim timeStrings As String() = timeDataset.Read(Of String())()

                    'DEBUG: Write timestamps to CSV
                    Dim debugFolder As String = IO.Path.GetDirectoryName(Me.File)
                    Dim timestampsCsvPath As String = IO.Path.Combine(debugFolder, "debug_timestamps.csv")
                    Using writer As New IO.StreamWriter(timestampsCsvPath)
                        writer.WriteLine("Index;Timestamp")
                        For i As Integer = 0 To Math.Min(timeStrings.Length - 1, 100) 'First 100 entries
                            writer.WriteLine($"{i};{timeStrings(i)}")
                        Next
                    End Using
                    Log.AddLogEntry(Log.levels.info, $"DEBUG: Written timestamps to {timestampsCsvPath}")

                    'Parse timestamps
                    Dim timestamps As New List(Of DateTime)
                    For Each timeStr As String In timeStrings
                        Dim timestamp As DateTime
                        If DateTime.TryParse(timeStr, timestamp) Then
                            timestamps.Add(timestamp)
                        Else
                            'Try parsing with common formats
                            If DateTime.TryParseExact(timeStr, {"yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy HH:mm", "yyyy-MM-ddTHH:mm:ss"},
                                                      Globalization.CultureInfo.InvariantCulture,
                                                      Globalization.DateTimeStyles.None, timestamp) Then
                                timestamps.Add(timestamp)
                            Else
                                Throw New Exception($"Unable to parse timestamp: {timeStr}")
                            End If
                        End If
                    Next

                    Log.AddLogEntry(Log.levels.info, $"Read {timestamps.Count} timestamps from 'time' dataset.")

                    'Read each selected series
                    For Each sInfo As TimeSeriesInfo In Me.SelectedSeries

                        Try
                            Dim dataset As NativeDataset = dataGroup.Dataset(sInfo.Name)
                            Dim dimensions = dataset.Space.Dimensions
                            Dim dataType = dataset.Type

                            Log.AddLogEntry(Log.levels.info, $"DEBUG: Dataset '{sInfo.Name}' - Dimensions: {String.Join("x", dimensions)}, Type Class: {dataType.Class}, Size: {dataType.Size} bytes")

                            'DEBUG: Write raw dataset info to CSV
                            Dim dataCsvPath As String = IO.Path.Combine(debugFolder, $"debug_data_{sInfo.Name}.csv")

                            If dimensions.Length = 1 Then
                                '1D dataset - one value per timestamp
                                Dim values As Single() = dataset.Read(Of Single())()

                                Using writer As New IO.StreamWriter(dataCsvPath)
                                    writer.WriteLine("Index;Value")
                                    For i As Integer = 0 To Math.Min(values.Length - 1, 100)
                                        writer.WriteLine($"{i};{values(i)}")
                                    Next
                                End Using
                                Log.AddLogEntry(Log.levels.info, $"DEBUG: Written 1D data to {dataCsvPath}")

                                Dim ts As New TimeSeries(sInfo.Name)
                                ts.Unit = sInfo.Unit
                                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

                                For i As Integer = 0 To Math.Min(timestamps.Count, values.Length) - 1
                                    ts.AddNode(timestamps(i), CDbl(values(i)))
                                Next

                                Me.TimeSeries.Add(sInfo.Index, ts)
                                Log.AddLogEntry(Log.levels.info, $"Read series '{sInfo.Name}' with {ts.Length} nodes.")

                            ElseIf dimensions.Length = 2 Then
                                '2D dataset - Fortran stores as (numColumns, numTimesteps)
                                'dimensions(0) = number of data columns (e.g., Qzu, Qab, tf = 3)
                                'dimensions(1) = number of timestamps (e.g., 58609)
                                Dim numDataCols As Integer = CInt(dimensions(0))
                                Dim numTimesteps As Integer = CInt(dimensions(1))

                                Log.AddLogEntry(Log.levels.info, $"DEBUG: 2D dataset with {numDataCols} data columns and {numTimesteps} timesteps")

                                'Read as flat 1D array
                                Dim flatValues As Single() = dataset.Read(Of Single())()
                                Log.AddLogEntry(Log.levels.info, $"DEBUG: Read {flatValues.Length} values (expected {numDataCols * numTimesteps})")

                                'Write debug CSV - rows are timesteps, columns are data series
                                Using writer As New IO.StreamWriter(dataCsvPath)
                                    Dim header As String = "TimeIndex"
                                    For c As Integer = 0 To numDataCols - 1
                                        header &= $";Col{c}"
                                    Next
                                    writer.WriteLine(header)
                                    For t As Integer = 0 To Math.Min(numTimesteps - 1, 100)
                                        Dim line As String = t.ToString()
                                        For c As Integer = 0 To numDataCols - 1
                                            'Fortran column-major: data for column c, timestep t is at index c * numTimesteps + t
                                            Dim flatIndex As Integer = c * numTimesteps + t
                                            If flatIndex < flatValues.Length Then
                                                line &= $";{flatValues(flatIndex)}"
                                            End If
                                        Next
                                        writer.WriteLine(line)
                                    Next
                                End Using
                                Log.AddLogEntry(Log.levels.info, $"DEBUG: Written 2D data to {dataCsvPath}")

                                'Create a separate TimeSeries for each data column
                                For col As Integer = 0 To numDataCols - 1
                                    Dim seriesName As String = If(numDataCols > 1, $"{sInfo.Name}_Col{col}", sInfo.Name)
                                    Dim ts As New TimeSeries(seriesName)
                                    ts.Unit = sInfo.Unit
                                    ts.DataSource = New TimeSeriesDataSource(Me.File, seriesName)

                                    For t As Integer = 0 To Math.Min(timestamps.Count, numTimesteps) - 1
                                        'Fortran column-major order: column c, timestep t is at index c * numTimesteps + t
                                        Dim flatIndex As Integer = col * numTimesteps + t
                                        If flatIndex < flatValues.Length Then
                                            ts.AddNode(timestamps(t), CDbl(flatValues(flatIndex)))
                                        End If
                                    Next

                                    'Use unique index for each column series
                                    Dim uniqueIndex As Integer = sInfo.Index * 100 + col
                                    Me.TimeSeries.Add(uniqueIndex, ts)
                                    Log.AddLogEntry(Log.levels.info, $"Read series '{seriesName}' with {ts.Length} nodes.")
                                Next

                            Else
                                Log.AddLogEntry(Log.levels.warning, $"Dataset '{sInfo.Name}' has unsupported dimensionality: {dimensions.Length}")
                            End If


                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.error, $"Error reading dataset '{sInfo.Name}': {ex.Message}")
                            If ex.InnerException IsNot Nothing Then
                                Log.AddLogEntry(Log.levels.error, $"Inner exception: {ex.InnerException.Message}")
                            End If
                            Log.AddLogEntry(Log.levels.error, $"Stack trace: {ex.StackTrace}")
                        End Try

                    Next

                End Using

            Catch ex As Exception
                Log.AddLogEntry(Log.levels.error, $"Error reading HDF5 file: {ex.Message}")
                Throw
            End Try

        End Sub

        ''' <summary>
        ''' Verifies whether the file is a valid HDF5 file with the expected structure
        ''' </summary>
        ''' <param name="file">Path to the file</param>
        ''' <returns>True if the file is a valid HDF5 file with timeseries structure containing time and element datasets</returns>
        Public Shared Function verifyFormat(file As String) As Boolean
            Try
                Using h5File As NativeFile = PureHDF.H5File.OpenRead(file)
                    'Check if 'timeseries' group exists
                    If Not h5File.LinkExists("timeseries") Then
                        Return False
                    End If

                    Dim timeseriesGroup As NativeGroup = h5File.Group("timeseries")

                    'Check if there's at least one child group with a 'time' dataset
                    For Each child As NativeObject In timeseriesGroup.Children()
                        If TypeOf child Is NativeGroup Then
                            Dim dataGroup As NativeGroup = CType(child, NativeGroup)
                            If dataGroup.LinkExists("time") Then
                                'Check if there's at least one other dataset (element)
                                For Each dataChild As NativeObject In dataGroup.Children()
                                    If TypeOf dataChild Is NativeDataset AndAlso dataChild.Name.ToLower() <> "time" Then
                                        Return True
                                    End If
                                Next
                            End If
                        End If
                    Next


                    Return False
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class

End Namespace

