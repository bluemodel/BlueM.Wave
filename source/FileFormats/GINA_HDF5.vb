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
    ''' Class for importing GINA HDF5 files with timeseries data
    ''' 
    ''' Expected structure:
    ''' /timeseries/
    '''   /[group] (e.g., HNW, SN1, SN2)
    '''     /time (string dataset with timestamps) - required
    '''     /[element_name] (2D dataset with Real values) - at least one required
    ''' 
    ''' Requires the PureHDF NuGet package: https://www.nuget.org/packages/PureHDF/
    ''' </summary>
    Public Class GINA_HDF5
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
        ''' Reads series info from the GINA HDF5 file
        ''' Creates a separate entry for each column of each dataset (e.g., T_Dru0161_Qzu, T_Dru0161_Qab)
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Dim sInfo As TimeSeriesInfo
            Dim index As Integer = 0

            Me.TimeSeriesInfos.Clear()

            Try
                Using h5File As NativeFile = PureHDF.H5File.OpenRead(Me.File)

                    'Navigate to the timeseries group
                    If Not h5File.LinkExists("timeseries") Then
                        Throw New Exception("GINA HDF5 file does not contain a 'timeseries' group!")
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

                    'Read column names and units from group attributes if available
                    Dim columnNames As String() = Nothing
                    Dim columnUnits As String() = Nothing

                    If dataGroup.AttributeExists("Spalten_Namen") Then
                        Try
                            Dim namesAttr = dataGroup.Attribute("Spalten_Namen")
                            columnNames = namesAttr.Read(Of String())()
                            Log.AddLogEntry(Log.levels.info, $"Read column names from attribute: {String.Join(", ", columnNames)}")
                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.warning, $"Could not read Spalten_Namen attribute: {ex.Message}")
                        End Try
                    End If

                    If dataGroup.AttributeExists("Spalten_Einheiten") Then
                        Try
                            Dim unitsAttr = dataGroup.Attribute("Spalten_Einheiten")
                            columnUnits = unitsAttr.Read(Of String())()
                            Log.AddLogEntry(Log.levels.info, $"Read column units from attribute: {String.Join(", ", columnUnits)}")
                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.warning, $"Could not read Spalten_Einheiten attribute: {ex.Message}")
                        End Try
                    End If

                    'Read dataset names and enumerate columns for each
                    For Each child As NativeObject In dataGroup.Children()
                        If child.Name.ToLower() <> "time" Then
                            'Check if it's a dataset
                            If TypeOf child Is NativeDataset Then
                                Dim dataset As NativeDataset = CType(child, NativeDataset)
                                Dim dimensions = dataset.Space.Dimensions
                                Dim datasetName As String = child.Name

                                If dimensions.Length = 1 Then
                                    '1D dataset - single series
                                    sInfo = New TimeSeriesInfo()
                                    sInfo.Name = datasetName
                                    sInfo.Unit = "-"
                                    sInfo.Index = index
                                    Me.TimeSeriesInfos.Add(sInfo)
                                    index += 1

                                ElseIf dimensions.Length = 2 Then
                                    '2D dataset - create entry for each column
                                    Dim numDataCols As Integer = CInt(dimensions(0))

                                    For col As Integer = 0 To numDataCols - 1
                                        'Use column name from attribute if available
                                        Dim colName As String
                                        If columnNames IsNot Nothing AndAlso col < columnNames.Length Then
                                            colName = columnNames(col).Trim()
                                        Else
                                            colName = $"Col{col}"
                                        End If

                                        'Use column unit from attribute if available
                                        Dim colUnit As String
                                        If columnUnits IsNot Nothing AndAlso col < columnUnits.Length Then
                                            colUnit = columnUnits(col).Trim()
                                        Else
                                            colUnit = "-"
                                        End If

                                        sInfo = New TimeSeriesInfo()
                                        sInfo.Name = $"{datasetName}_{colName}"
                                        sInfo.Unit = colUnit
                                        sInfo.Index = index
                                        Me.TimeSeriesInfos.Add(sInfo)
                                        index += 1
                                    Next
                                End If
                            End If
                        End If
                    Next

                    If Me.TimeSeriesInfos.Count = 0 Then
                        Throw New Exception($"Data group '{_dataGroupName}' does not contain any element datasets!")
                    End If

                End Using

                Log.AddLogEntry(Log.levels.info, $"Found {Me.TimeSeriesInfos.Count} series in GINA HDF5 file.")

            Catch ex As Exception
                Log.AddLogEntry(Log.levels.error, $"Error reading GINA HDF5 file: {ex.Message}")
                Throw
            End Try


        End Sub

        ''' <summary>
        ''' Reads the selected series from the GINA HDF5 file
        ''' </summary>
        Public Overrides Sub readFile()

            If Me.SelectedSeries.Count = 0 Then
                Throw New Exception("No series selected for import!")
            End If

            Try
                Using h5File As NativeFile = PureHDF.H5File.OpenRead(Me.File)

                    'Navigate to the data group
                    Dim dataGroup As NativeGroup = h5File.Group($"timeseries/{_dataGroupName}")

                    'Read column names and units from group attributes if available
                    Dim columnNames As String() = Nothing
                    Dim columnUnits As String() = Nothing

                    If dataGroup.AttributeExists("Spalten_Namen") Then
                        Try
                            Dim namesAttr = dataGroup.Attribute("Spalten_Namen")
                            columnNames = namesAttr.Read(Of String())()
                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.warning, $"Could not read Spalten_Namen attribute: {ex.Message}")
                        End Try
                    End If

                    If dataGroup.AttributeExists("Spalten_Einheiten") Then
                        Try
                            Dim unitsAttr = dataGroup.Attribute("Spalten_Einheiten")
                            columnUnits = unitsAttr.Read(Of String())()
                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.warning, $"Could not read Spalten_Einheiten attribute: {ex.Message}")
                        End Try
                    End If

                    'Read timestamps from the 'time' dataset
                    Dim timeDataset As NativeDataset = dataGroup.Dataset("time")
                    Dim timeStrings As String() = timeDataset.Read(Of String())()

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

                    'Group selected series by dataset name (series name format: "DatasetName_ColumnName")
                    Dim datasetColumns As New Dictionary(Of String, List(Of Tuple(Of Integer, String, String, Integer)))  'DatasetName -> List of (colIndex, colName, unit, seriesIndex)

                    For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                        'Parse series name to extract dataset name and column name
                        'The column name may contain underscores (e.g., "Q_zu"), so we need to match against known column names
                        Dim foundMatch As Boolean = False

                        If columnNames IsNot Nothing Then
                            'Try to find a matching column name suffix
                            For colIndex As Integer = 0 To columnNames.Length - 1
                                Dim colName As String = columnNames(colIndex).Trim()
                                Dim suffix As String = "_" & colName

                                If sInfo.Name.EndsWith(suffix) Then
                                    Dim datasetName As String = sInfo.Name.Substring(0, sInfo.Name.Length - suffix.Length)

                                    If Not datasetColumns.ContainsKey(datasetName) Then
                                        datasetColumns.Add(datasetName, New List(Of Tuple(Of Integer, String, String, Integer)))
                                    End If
                                    datasetColumns(datasetName).Add(Tuple.Create(colIndex, colName, sInfo.Unit, sInfo.Index))
                                    foundMatch = True
                                    Exit For
                                End If
                            Next
                        End If

                        If Not foundMatch Then
                            'Fallback: try splitting by last underscore for Col# format or 1D datasets
                            Dim lastUnderscore As Integer = sInfo.Name.LastIndexOf("_"c)
                            If lastUnderscore > 0 Then
                                Dim datasetName As String = sInfo.Name.Substring(0, lastUnderscore)
                                Dim colName As String = sInfo.Name.Substring(lastUnderscore + 1)

                                'Try parsing as Col# format
                                Dim colIndex As Integer = -1
                                If colName.StartsWith("Col") Then
                                    Integer.TryParse(colName.Substring(3), colIndex)
                                End If

                                If colIndex >= 0 Then
                                    If Not datasetColumns.ContainsKey(datasetName) Then
                                        datasetColumns.Add(datasetName, New List(Of Tuple(Of Integer, String, String, Integer)))
                                    End If
                                    datasetColumns(datasetName).Add(Tuple.Create(colIndex, colName, sInfo.Unit, sInfo.Index))
                                Else
                                    Log.AddLogEntry(Log.levels.warning, $"Could not determine column index for series '{sInfo.Name}'")
                                End If
                            Else
                                '1D dataset (no column suffix) - treat as single series
                                If Not datasetColumns.ContainsKey(sInfo.Name) Then
                                    datasetColumns.Add(sInfo.Name, New List(Of Tuple(Of Integer, String, String, Integer)))
                                End If
                                datasetColumns(sInfo.Name).Add(Tuple.Create(-1, "", sInfo.Unit, sInfo.Index))
                            End If
                        End If
                    Next

                    'Read each dataset and extract only the requested columns
                    For Each kvp In datasetColumns
                        Dim datasetName As String = kvp.Key
                        Dim columnsToRead As List(Of Tuple(Of Integer, String, String, Integer)) = kvp.Value

                        Try
                            Dim dataset As NativeDataset = dataGroup.Dataset(datasetName)
                            Dim dimensions = dataset.Space.Dimensions

                            If dimensions.Length = 1 Then
                                '1D dataset - single series
                                Dim values As Single() = dataset.Read(Of Single())()

                                Dim colInfo = columnsToRead(0)
                                Dim ts As New TimeSeries(datasetName)
                                ts.Unit = colInfo.Item3
                                ts.DataSource = New TimeSeriesDataSource(Me.File, datasetName)

                                For i As Integer = 0 To Math.Min(timestamps.Count, values.Length) - 1
                                    ts.AddNode(timestamps(i), CDbl(values(i)))
                                Next

                                Me.TimeSeries.Add(colInfo.Item4, ts)
                                Log.AddLogEntry(Log.levels.info, $"Read series '{datasetName}' with {ts.Length} nodes.")

                            ElseIf dimensions.Length = 2 Then
                                '2D dataset - read entire dataset, then extract selected columns
                                Dim numDataCols As Integer = CInt(dimensions(0))
                                Dim numTimesteps As Integer = CInt(dimensions(1))

                                Dim flatValues As Single() = dataset.Read(Of Single())()
                                Log.AddLogEntry(Log.levels.info, $"Read dataset '{datasetName}': {flatValues.Length} values ({numDataCols} columns x {numTimesteps} timesteps)")

                                'Extract only the selected columns
                                For Each colInfo In columnsToRead
                                    Dim colIndex As Integer = colInfo.Item1
                                    Dim colName As String = colInfo.Item2
                                    Dim colUnit As String = colInfo.Item3
                                    Dim seriesIndex As Integer = colInfo.Item4

                                    If colIndex >= 0 AndAlso colIndex < numDataCols Then
                                        Dim seriesName As String = $"{datasetName}_{colName}"
                                        Dim ts As New TimeSeries(seriesName)
                                        ts.Unit = colUnit
                                        ts.DataSource = New TimeSeriesDataSource(Me.File, seriesName)

                                        For t As Integer = 0 To Math.Min(timestamps.Count, numTimesteps) - 1
                                            'Fortran column-major order: column c, timestep t is at index c * numTimesteps + t
                                            Dim flatIndex As Integer = colIndex * numTimesteps + t
                                            If flatIndex < flatValues.Length Then
                                                ts.AddNode(timestamps(t), CDbl(flatValues(flatIndex)))
                                            End If
                                        Next

                                        Me.TimeSeries.Add(seriesIndex, ts)
                                        Log.AddLogEntry(Log.levels.info, $"Read series '{seriesName}' (unit: {colUnit}) with {ts.Length} nodes.")
                                    Else
                                        Log.AddLogEntry(Log.levels.warning, $"Column index {colIndex} out of range for dataset '{datasetName}' (has {numDataCols} columns)")
                                    End If
                                Next
                            End If

                        Catch ex As Exception
                            Log.AddLogEntry(Log.levels.error, $"Error reading dataset '{datasetName}': {ex.Message}")
                            If ex.InnerException IsNot Nothing Then
                                Log.AddLogEntry(Log.levels.error, $"Inner exception: {ex.InnerException.Message}")
                            End If
                        End Try
                    Next

                End Using

            Catch ex As Exception
                Log.AddLogEntry(Log.levels.error, $"Error reading GINA HDF5 file: {ex.Message}")
                Throw
            End Try

        End Sub

        ''' <summary>
        ''' Verifies whether the file is a valid GINA HDF5 file with the expected structure
        ''' </summary>
        ''' <param name="file">Path to the file</param>
        ''' <returns>True if the file is a valid GINA HDF5 file with timeseries structure containing time and element datasets</returns>
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

