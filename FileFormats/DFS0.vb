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
''' Code is loosely based on the following example: https://github.com/DHI/MIKECore-Examples/blob/master/Examples/CSharp/ExamplesDfs0.cs
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
        Dim dfs0File As IDfsFile = DfsFileFactory.DfsGenericOpen(Me.File)

        'Header information is contained in the IDfsFileInfo
        Dim FileInfo As IDfsFileInfo = dfs0File.FileInfo
        If Not FileInfo.TimeAxis.IsCalendar Then
            'TODO: if the file uses a TemporalTimeAxis, we could ask the user for a date offset, like we do for HYDRO_AS-2D
            Throw New Exception("Dfs0 file does not have a calendar time axis, unable to open!")
        End If

        'Loop over all items in the file
        Dim dynamicItemInfo As IDfsSimpleDynamicItemInfo
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
        Dim DataTypes As New Dictionary(Of Integer, DfsSimpleType)
        'dictionary for temporarily storing DeleteValues (to be converted to NaN)
        Dim DeleteValues As New Dictionary(Of DfsSimpleType, Object)

        'Instantiate Timeseries
        Dim ts As TimeSeries
        For Each sInfo As SeriesInfo In Me.SelectedSeries
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
            Me.FileTimeSeries.Add(sInfo.Index, ts)
        Next

        'Open the file as a generic dfs file
        Dim dfs0File As IDfsFile = DfsFileFactory.DfsGenericOpen(Me.File)

        'Read interpretation and metadata
        Dim dynamicItemInfo As IDfsSimpleDynamicItemInfo
        For Each sInfo As SeriesInfo In Me.SelectedSeries
            dynamicItemInfo = dfs0File.ItemInfo(sInfo.Index)
            Me.FileTimeSeries(sInfo.Index).Interpretation = DataValueTypeToInterpretation(dynamicItemInfo.ValueType)
            Me.FileTimeSeries(sInfo.Index).Metadata.Add("Quantity", dynamicItemInfo.Quantity.ItemDescription)
            DataTypes.Add(sInfo.Index, dynamicItemInfo.DataType)
        Next

        'Header information is contained in the IDfsFileInfo
        Dim FileInfo As IDfsFileInfo = dfs0File.FileInfo
        Dim timeAxis As IDfsTemporalAxis = FileInfo.TimeAxis
        Dim steps As Integer = timeAxis.NumberOfTimeSteps

        'collect DeleteValues
        DeleteValues.Add(DfsSimpleType.Double, FileInfo.DeleteValueDouble)
        DeleteValues.Add(DfsSimpleType.Float, FileInfo.DeleteValueFloat)
        DeleteValues.Add(DfsSimpleType.Int, FileInfo.DeleteValueInt)
        DeleteValues.Add(DfsSimpleType.UInt, FileInfo.DeleteValueUnsignedInt)
        DeleteValues.Add(DfsSimpleType.Byte, FileInfo.DeleteValueByte)

        'This iterates through all timesteps and selected items in the file
        'For performance reasons it is important to iterate over time steps first and items second.
        Dim data As IDfsItemData
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
    ''' <returns>the corresponding interpretation</returns>
    Private Shared Function DataValueTypeToInterpretation(ValueType As DataValueType) As TimeSeries.InterpretationEnum
        Dim Interpretation As TimeSeries.InterpretationEnum
        Select Case ValueType
            Case DataValueType.Accumulated
                Interpretation = TimeSeries.InterpretationEnum.Cumulative
            Case DataValueType.Instantaneous
                Interpretation = TimeSeries.InterpretationEnum.Instantaneous
            Case DataValueType.MeanStepBackward
                Interpretation = TimeSeries.InterpretationEnum.BlockLeft
            Case DataValueType.MeanStepForward
                Interpretation = TimeSeries.InterpretationEnum.BlockRight
            Case DataValueType.StepAccumulated
                Interpretation = TimeSeries.InterpretationEnum.CumulativePerTimestep
            Case Else
                Interpretation = TimeSeries.InterpretationEnum.Undefined
        End Select
        Return Interpretation
    End Function

End Class
