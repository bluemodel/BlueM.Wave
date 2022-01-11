'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
'
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
'
Imports DHI.Generic.MikeZero
Imports DHI.Generic.MikeZero.DFS
''' <summary>
''' Class for importing the DHI MIKE Dfs0 format
''' 
''' Requires the DHI.DFS Nuget package: https://www.nuget.org/packages/DHI.DFS/
''' 
''' Docs:
''' * https://docs.mikepoweredbydhi.com/core_libraries/dfs/dfs_api/
''' * https://docs.mikepoweredbydhi.com/core_libraries/dfs/dfs-file-formats/#dfs0-file
''' 
''' Code is loosely based on the following examples: https://github.com/DHI/MIKECore-Examples/blob/master/Examples/CSharp/ExamplesDfs0.cs
''' </summary>
''' <remarks>Only works in x64</remarks>
Public Class DFS0
    Inherits FileFormatBase

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

        Dim sInfo As SeriesInfo

        Me.SeriesList.Clear()

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
            sInfo = New SeriesInfo()
            sInfo.Name = dynamicItemInfo.Name
            sInfo.Unit = dynamicItemInfo.Quantity.UnitAbbreviation
            sInfo.Index = item_index
            Me.SeriesList.Add(sInfo)
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
        For Each sInfo As SeriesInfo In Me.SelectedSeries
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
            Me.FileTimeSeries.Add(sInfo.Index, ts)
        Next

        'Open the file as a generic dfs file
        Dim dfs0File As DFS.IDfsFile = DFS.DfsFileFactory.DfsGenericOpen(Me.File)

        'Read interpretation and metadata
        Dim dynamicItemInfo As DFS.IDfsSimpleDynamicItemInfo
        For Each sInfo As SeriesInfo In Me.SelectedSeries
            dynamicItemInfo = dfs0File.ItemInfo(sInfo.Index)
            Me.FileTimeSeries(sInfo.Index).Interpretation = DataValueTypeToInterpretation(dynamicItemInfo.ValueType)
            Me.FileTimeSeries(sInfo.Index).Metadata.Add("Quantity", dynamicItemInfo.Quantity.ItemDescription)
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
        For t_index As Integer = 0 To steps - 1
            For Each sInfo As SeriesInfo In Me.SelectedSeries
                data = dfs0File.ReadItemTimeStep(sInfo.Index + 1, t_index) 'expects itemNumber (itemIndex + 1)!
                datum = data.TimeAsDateTime(timeAxis)
                If DeleteValues.ContainsKey(DataTypes(sInfo.Index)) AndAlso data.Data(0) = DeleteValues(DataTypes(sInfo.Index)) Then
                    value = Double.NaN
                Else
                    value = Convert.ToDouble(data.Data(0))
                End If
                Me.FileTimeSeries(sInfo.Index).AddNode(datum, value)
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
                Interpretation = TimeSeries.InterpretationEnum.Cumulative
            Case DFS.DataValueType.Instantaneous
                Interpretation = TimeSeries.InterpretationEnum.Instantaneous
            Case DFS.DataValueType.MeanStepBackward
                Interpretation = TimeSeries.InterpretationEnum.BlockLeft
            Case DFS.DataValueType.MeanStepForward
                Interpretation = TimeSeries.InterpretationEnum.BlockRight
            Case DFS.DataValueType.StepAccumulated
                Interpretation = TimeSeries.InterpretationEnum.CumulativePerTimestep
            Case Else
                Interpretation = TimeSeries.InterpretationEnum.Undefined
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
            Case TimeSeries.InterpretationEnum.Cumulative
                ValueType = DFS.DataValueType.Accumulated
            Case TimeSeries.InterpretationEnum.Instantaneous
                ValueType = DFS.DataValueType.Instantaneous
            Case TimeSeries.InterpretationEnum.BlockLeft
                ValueType = DFS.DataValueType.MeanStepBackward
            Case TimeSeries.InterpretationEnum.BlockRight
                ValueType = DFS.DataValueType.MeanStepForward
            Case TimeSeries.InterpretationEnum.CumulativePerTimestep
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
            Case "m3/s", "m�/s", "m^3/s", "m^3s^-1", "meter^3/sec"
                eumUnit = eumUnit.eumUm3PerSec
            Case "m", "masl", "m.a.s.l.", "m�nn", "mnn", "meter"
                eumUnit = eumUnit.eumUmeter
            Case "m/s", "ms^-1", "meter/sec"
                eumUnit = eumUnit.eumUmeterPerSec
            Case "mm", "millimeter"
                eumUnit = eumUnit.eumUmillimeter
            Case "mm/h", "mm/hour"
                eumUnit = eumUnit.eumUmillimeterPerHour
            Case "mm/d", "mm/day"
                eumUnit = eumUnit.eumUmillimeterPerDay
            Case "mm/m", "mm/month"
                eumUnit = eumUnit.eumUmillimeterPerMonth
            Case "mm/y", "mm/year"
                eumUnit = eumUnit.eumUmillimeterPerYear
            Case "�c", "degree celsius", "degree c"
                eumUnit = eumUnit.eumUdegreeCelsius
            Case "()"
                eumUnit = eumUnit.eumUOnePerOne
            Case "%", "percent"
                eumUnit = eumUnit.eumUPerCent
            Case "m�", "m3", "cbm"
                eumUnit = eumUnit.eumUm3
            Case "mm�", "mm3", "million m�", "million m3"
                eumUnit = eumUnit.eumUTenTo6m3
            Case "m�", "m2", "sqm"
                eumUnit = eumUnit.eumUm2
            Case "tj"
                eumUnit = eumUnit.eumUteraJoule
            Case "w"
                eumUnit = eumUnit.eumUwatt
            Case "liter/sec/km^2", "l/s/km^2"
                eumUnit = eumUnit.eumUliterPerSecPerKm2
            Case Else
                Log.AddLogEntry(levels.warning, $"Unable to convert unit {unit} to a DHI EUM unit. Setting the unit to undefined.")
                eumUnit = eumUnit.eumUUnitUndefined
        End Select
        Return eumUnit
    End Function


    ''' <summary>
    ''' Writes one or more timeseries to a DFS0 file
    ''' </summary>
    ''' <param name="tsList">list of TimeSeries</param>
    ''' <param name="path">path to file</param>
    ''' <remarks></remarks>
    Public Shared Sub Write_File(ByRef tsList As List(Of TimeSeries), path As String)

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
                    'fill non-existant nodes with NaN
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
            'TODO: item type can not be automatically derived and is always set to undefined
            item.Set(ts.Title, eumQuantity.Create(eumItem.eumIItemUndefined, DFS0.UnitToEUMUnit(ts.Unit)), DFS.DfsSimpleType.Double)
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
