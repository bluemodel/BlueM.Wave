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
Imports DHI.Generic.MikeZero
Imports DHI.Generic.MikeZero.DFS

Namespace Fileformats

    ''' <summary>
    ''' Class for importing the DHI MIKE Dfs0 format
    ''' 
    ''' Requires the DHI.DFS Nuget package: https://www.nuget.org/packages/DHI.DFS/
    ''' 
    ''' Docs:
    ''' * https://docs.mikepoweredbydhi.com/core_libraries/dfs/dfs_api/
    ''' * https://docs.mikepoweredbydhi.com/core_libraries/dfs/dfs-file-formats/#dfs0-file
    ''' * https://docs.mikepoweredbydhi.com/SDK_UserGuide/#eum-quantity
    ''' 
    ''' Code is loosely based on the following examples: https://github.com/DHI/MIKECore-Examples/blob/master/Examples/CSharp/ExamplesDfs0.cs
    ''' </summary>
    ''' <remarks>Only works in x64</remarks>
    Public Class DFS0
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Flag indicating whether to show the import dialog
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Check for 64bit environment
            If Not Environment.Is64BitProcess() Then
                Throw New Exception("Loading Dfs0 files is not supported in a 32bit environment. Please use the 64bit version of BlueM.Wave instead!")
            End If

            'Basic settings
            Me.UseUnits = True

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Reads series info
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            'Open the file as a generic dfs file
            Dim dfs0File As DFS.IDfsFile = DFS.DfsFileFactory.DfsGenericOpen(Me.File)

            'Header information is contained in the IDfsFileInfo
            Dim FileInfo As DFS.IDfsFileInfo = dfs0File.FileInfo
            If Not FileInfo.TimeAxis.IsCalendar Then
                'TODO: if the file uses a TemporalTimeAxis, we could ask the user for a date offset, like we do for HYDRO_AS-2D
                Throw New Exception("Dfs0 file does not have a calendar time axis, unable to open!")
            End If

            'Loop over all items in the file
            Dim dynamicItemInfo As DFS.IDfsSimpleDynamicItemInfo
            For item_index As Integer = 0 To dfs0File.ItemInfo.Count - 1
                dynamicItemInfo = dfs0File.ItemInfo(item_index)
                sInfo = New TimeSeriesInfo()
                sInfo.Name = dynamicItemInfo.Name
                sInfo.Unit = dynamicItemInfo.Quantity.UnitAbbreviation
                sInfo.Index = item_index
                Me.TimeSeriesInfos.Add(sInfo)
            Next

            dfs0File.Close()

        End Sub

        ''' <summary>
        ''' Reads the file
        ''' </summary>
        Public Overrides Sub readFile()

            'dictionary for temporarily storing datatypes 
            Dim DataTypes As New Dictionary(Of Integer, DFS.DfsSimpleType)
            'dictionary for temporarily storing DeleteValues (to be converted to NaN)
            Dim DeleteValues As New Dictionary(Of DFS.DfsSimpleType, Object)

            'Instantiate Timeseries
            Dim ts As TimeSeries
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                ts.Unit = sInfo.Unit
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            'Open the file as a generic dfs file
            Dim dfs0File As DFS.IDfsFile = DFS.DfsFileFactory.DfsGenericOpen(Me.File)

            'Read interpretation and metadata
            Dim dynamicItemInfo As DFS.IDfsSimpleDynamicItemInfo
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                dynamicItemInfo = dfs0File.ItemInfo(sInfo.Index)
                Me.TimeSeries(sInfo.Index).Interpretation = DataValueTypeToInterpretation(dynamicItemInfo.ValueType)
                Me.TimeSeries(sInfo.Index).Metadata.Add("eumItem", [Enum].GetName(GetType(eumItem), dynamicItemInfo.Quantity.Item))
                Me.TimeSeries(sInfo.Index).Metadata.Add("eumItemDescription", dynamicItemInfo.Quantity.ItemDescription)
                Me.TimeSeries(sInfo.Index).Metadata.Add("eumUnit", [Enum].GetName(GetType(eumUnit), dynamicItemInfo.Quantity.Unit))
                Me.TimeSeries(sInfo.Index).Metadata.Add("eumUnitAbbreviation", dynamicItemInfo.Quantity.UnitAbbreviation)
                DataTypes.Add(sInfo.Index, dynamicItemInfo.DataType)
            Next

            'Header information is contained in the IDfsFileInfo
            Dim FileInfo As DFS.IDfsFileInfo = dfs0File.FileInfo
            Dim timeAxis As DFS.IDfsTemporalAxis = FileInfo.TimeAxis
            Dim steps As Integer = timeAxis.NumberOfTimeSteps

            'collect DeleteValues
            DeleteValues.Add(DFS.DfsSimpleType.Double, FileInfo.DeleteValueDouble)
            DeleteValues.Add(DFS.DfsSimpleType.Float, FileInfo.DeleteValueFloat)
            DeleteValues.Add(DFS.DfsSimpleType.Int, FileInfo.DeleteValueInt)
            DeleteValues.Add(DFS.DfsSimpleType.UInt, FileInfo.DeleteValueUnsignedInt)
            DeleteValues.Add(DFS.DfsSimpleType.Byte, FileInfo.DeleteValueByte)

            'This iterates through all timesteps and selected items in the file
            'For performance reasons it is important to iterate over time steps first and items second.
            Dim data As DFS.IDfsItemData
            Dim datum As DateTime
            Dim value As Double

            'create a sorted list of item indices in order to significantly improve performance
            Dim itemIndices As New List(Of Integer)
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                itemIndices.Add(sInfo.Index)
            Next
            itemIndices.Sort()

            For t_index As Integer = 0 To steps - 1
                For Each itemIndex As Integer In itemIndices
                    data = dfs0File.ReadItemTimeStep(itemIndex + 1, t_index) 'expects itemNumber (itemIndex + 1)!
                    datum = data.TimeAsDateTime(timeAxis)
                    If DeleteValues.ContainsKey(DataTypes(itemIndex)) AndAlso data.Data(0) = DeleteValues(DataTypes(itemIndex)) Then
                        value = Double.NaN
                    Else
                        value = Convert.ToDouble(data.Data(0))
                    End If
                    Me.TimeSeries(itemIndex).AddNode(datum, value)
                Next
            Next

            dfs0File.Close()

        End Sub

        ''' <summary>
        ''' Converts a DHI.Generic.MikeZero.DFS.DataValueType to a TimeSeries.Interpretation
        ''' </summary>
        ''' <param name="ValueType">the DataValueType to convert</param>
        ''' <returns>the corresponding interpretation, defaults to Undefined</returns>
        Private Shared Function DataValueTypeToInterpretation(ValueType As DFS.DataValueType) As TimeSeries.InterpretationEnum
            Dim Interpretation As TimeSeries.InterpretationEnum
            Select Case ValueType
                Case DFS.DataValueType.Accumulated
                    Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.Cumulative
                Case DFS.DataValueType.Instantaneous
                    Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.Instantaneous
                Case DFS.DataValueType.MeanStepBackward
                    Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockLeft
                Case DFS.DataValueType.MeanStepForward
                    Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight
                Case DFS.DataValueType.StepAccumulated
                    Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.CumulativePerTimestep
                Case Else
                    Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.Undefined
            End Select
            Return Interpretation
        End Function

        ''' <summary>
        ''' Converts a TimeSeries.Interpretation to a DHI.Generic.MikeZero.DFS.DataValueType
        ''' </summary>
        ''' <param name="interpretation">the interpretation to convert</param>
        ''' <returns>the corresponding DataValueType, defaults to DataValueType.Instantaneous</returns>
        Public Shared Function InterpretationToDataValueType(interpretation As TimeSeries.InterpretationEnum) As DFS.DataValueType
            Dim ValueType As DFS.DataValueType
            Select Case interpretation
                Case BlueM.Wave.TimeSeries.InterpretationEnum.Cumulative
                    ValueType = DFS.DataValueType.Accumulated
                Case BlueM.Wave.TimeSeries.InterpretationEnum.Instantaneous
                    ValueType = DFS.DataValueType.Instantaneous
                Case BlueM.Wave.TimeSeries.InterpretationEnum.BlockLeft
                    ValueType = DFS.DataValueType.MeanStepBackward
                Case BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight
                    ValueType = DFS.DataValueType.MeanStepForward
                Case BlueM.Wave.TimeSeries.InterpretationEnum.CumulativePerTimestep
                    ValueType = DFS.DataValueType.StepAccumulated
                Case Else
                    ValueType = DFS.DataValueType.Instantaneous
            End Select
            Return ValueType
        End Function

        ''' <summary>
        ''' Converts a string unit to a DHI.Generic.MikeZero.eumUnit
        ''' </summary>
        ''' <param name="unit">unit</param>
        ''' <returns>the corresponding eumUnit, defaults to eumUUnitUndefined</returns>
        Public Shared Function UnitToEUMUnit(unit As String) As eumUnit
            Dim eumUnit As eumUnit
            Select Case unit.ToLower()
                Case "m3/s", "m³/s", "m^3/s", "m^3s^-1", "meter^3/sec"
                    eumUnit = eumUnit.eumUm3PerSec
                Case "m", "masl", "m.a.s.l.", "münn", "mnn", "meter"
                    eumUnit = eumUnit.eumUmeter
                Case "m/s", "ms^-1", "meter/sec"
                    eumUnit = eumUnit.eumUmeterPerSec
                Case "mm", "millimeter"
                    eumUnit = eumUnit.eumUmillimeter
                Case "mm/h", "mm/hour"
                    eumUnit = eumUnit.eumUmillimeterPerHour
                Case "mm/d", "mm/day"
                    eumUnit = eumUnit.eumUmillimeterPerDay
                Case "mm/m", "mm/month", "mm/mth"
                    eumUnit = eumUnit.eumUmillimeterPerMonth
                Case "mm/y", "mm/year", "mm/yr"
                    eumUnit = eumUnit.eumUmillimeterPerYear
                Case "°c", "degree celsius", "degree c", "deg c"
                    eumUnit = eumUnit.eumUdegreeCelsius
                Case "()"
                    eumUnit = eumUnit.eumUOnePerOne
                Case "%", "percent"
                    eumUnit = eumUnit.eumUPerCent
                Case "m³", "m3", "cbm"
                    eumUnit = eumUnit.eumUm3
                Case "mm³", "mm3", "million m³", "million m3", "10^6m^3"
                    eumUnit = eumUnit.eumUTenTo6m3
                Case "m²", "m2", "sqm"
                    eumUnit = eumUnit.eumUm2
                Case "tj"
                    eumUnit = eumUnit.eumUteraJoule
                Case "w"
                    eumUnit = eumUnit.eumUwatt
                Case "liter/sec/km^2", "l/s/km^2"
                    eumUnit = eumUnit.eumUliterPerSecPerKm2
                Case Else
                    Log.AddLogEntry(levels.debug, $"Unable to convert unit '{unit}' to a DHI EUM unit. Using undefined.")
                    eumUnit = eumUnit.eumUUnitUndefined
            End Select
            Return eumUnit
        End Function


        ''' <summary>
        ''' Writes one or more timeseries to a DFS0 file
        ''' Displays a dialog for setting EUM Item and Unit
        ''' </summary>
        ''' <param name="tsList">list of TimeSeries</param>
        ''' <param name="path">path to file</param>
        ''' <remarks></remarks>
        Public Shared Sub Write_File(ByRef tsList As List(Of TimeSeries), path As String)

            'show DFS0 export dialog in order to allow the user to specify EUM Items and Units
            Dim dlg As New DFS0_ExportDialog(tsList)
            Dim dlgresult As DialogResult = dlg.ShowDialog()
            If dlgresult <> DialogResult.OK Then
                Throw New Exception("Export to DFS0 cancelled by user!")
            End If
            Dim quantities As Dictionary(Of Integer, eumQuantity) = dlg.Quantities

            'collect timestamps from all time series
            Dim timestampSet As New HashSet(Of DateTime)
            For Each ts As TimeSeries In tsList
                timestampSet.UnionWith(New HashSet(Of DateTime)(ts.Dates))
            Next
            'sort timestamps
            Dim timestamps As List(Of DateTime) = timestampSet.ToList()
            timestamps.Sort()

            'prepare data
            Dim times As Double()   'array of timestamps as seconds after the start
            Dim values As Double(,) '2d-array of values, 1st dimension is time, 2nd dimension is series

            ReDim times(timestamps.Count - 1)
            ReDim values(timestamps.Count - 1, tsList.Count - 1)

            Dim t As DateTime
            For i As Integer = 0 To timestamps.Count - 1
                t = timestamps(i)
                For j As Integer = 0 To tsList.Count - 1
                    If tsList(j).Nodes.ContainsKey(t) Then
                        values(i, j) = tsList(j).Nodes(t)
                    Else
                        'fill non-existent nodes with NaN
                        values(i, j) = Double.NaN
                    End If
                Next
                times(i) = (timestamps(i) - timestamps.First).TotalSeconds
            Next

            Dim factory As New DFS.DfsFactory()
            Dim builder As DFS.DfsBuilder = DFS.DfsBuilder.Create(IO.Path.GetFileNameWithoutExtension(path), "BlueM.Wave", 1)

            'set up file header
            builder.SetDataType(0)
            builder.SetGeographicalProjection(factory.CreateProjectionUndefined())
            builder.SetTemporalAxis(factory.CreateTemporalNonEqCalendarAxis(eumUnit.eumUsec, timestamps.First))
            builder.SetItemStatisticsType(DFS.StatType.RegularStat)
            builder.DeleteValueDouble = Double.NaN

            'set up items
            For Each ts As TimeSeries In tsList
                Dim item As DFS.DfsDynamicItemBuilder = builder.CreateDynamicItemBuilder()
                item.Set(ts.Title, quantities(ts.Id), DFS.DfsSimpleType.Double)
                item.SetValueType(InterpretationToDataValueType(ts.Interpretation))
                item.SetAxis(factory.CreateAxisEqD0())
                builder.AddDynamicItem(item.GetDynamicItemInfo())
            Next

            'create file
            builder.CreateFile(path)
            Dim file As DFS.IDfsFile = builder.GetFile()

            'write data
            DFS.dfs0.Dfs0Util.WriteDfs0DataDouble(file, times, values)

            file.Close()

        End Sub

    End Class

End Namespace