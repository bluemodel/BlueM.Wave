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

Namespace Fileformats

    ''' <summary>
    ''' Class for reading the SWMM binary output format
    ''' </summary>
    ''' <remarks>See https://wiki.bluemodel.org/index.php/SWMM_file_formats </remarks>
    ''' 
    Public Class SWMM_OUT
        Inherits TimeSeriesFile
        Protected oSWMM As modelEAU.SWMM.DllAdapter.SWMM_iface

        Private nSubcatch As Integer
        Private nNodes As Integer
        Private nLinks As Integer
        Private nPolluts As Integer
        Private nSubcatchVars As Integer
        Private nNodesVars As Integer
        Private nLinksVars As Integer
        Private nSysvars As Integer
        Private FlowUnit As FlowUnits

        ''' <summary>
        ''' Element types
        ''' </summary>
        ''' <remarks>see https://github.com/USEPA/Stormwater-Management-Model/blob/master/src/outfile/include/swmm_output_enums.h#L36</remarks>
        Private Enum Type As Integer
            Subcatchment = 0
            Node = 1
            Link = 2
            System = 3
            Pollutant = 4
        End Enum

        ''' <summary>
        ''' Flow units
        ''' </summary>
        ''' <remarks>see https://github.com/USEPA/Stormwater-Management-Model/blob/master/src/outfile/include/swmm_output_enums.h#L20</remarks>
        Private Enum FlowUnits As Integer
            CFS = 0
            GPM = 1
            MGD = 2
            CMS = 3
            LPS = 4
            MLD = 5
        End Enum

        ''' <summary>
        ''' Structure for storing SWMM series information
        ''' </summary>
        Private Structure SWMMSeriesInfo
            ''' <summary>
            ''' element type
            ''' </summary>
            Dim iType As Type
            ''' <summary>
            ''' element index (0-based)
            ''' </summary>
            Dim iIndex As Integer
            ''' <summary>
            ''' element name
            ''' </summary>
            Dim Name As String
            ''' <summary>
            ''' variable name
            ''' </summary>
            Dim Variable As String
            ''' <summary>
            ''' variable index (see https://github.com/USEPA/Stormwater-Management-Model/blob/master/src/outfile/include/swmm_output_enums.h)
            ''' </summary>
            Dim vIndex As Integer
        End Structure

        ''' <summary>
        ''' Dictionary containing all SWMM series infos
        ''' Key is series index
        ''' </summary>
        Private swmmInfos As Dictionary(Of Integer, SWMMSeriesInfo)

        ''' <summary>
        ''' Returns a list of SWMM binary output file specific metadata keys
        ''' </summary>
        Public Overloads Shared ReadOnly Property MetadataKeys() As List(Of String)
            Get
                Dim keys As New List(Of String) From {
                    "Type",
                    "Name",
                    "Variable"
                }
                Return keys
            End Get
        End Property

        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.iLineHeadings = 1
            Me.UseUnits = True
            Me.iLineUnits = 0
            Me.iLineData = 0
            Me.IsColumnSeparated = False
            Me.Separator = Constants.space
            Me.DecimalSeparator = Constants.period
            Me.DateTimeColumnIndex = 0

            oSWMM = New modelEAU.SWMM.DllAdapter.SWMM_iface()

            Call Me.readSeriesInfo()

            If ReadAllNow Then
                Me.selectAllSeries()
                Me.readFile()
            End If

        End Sub

        Public Overrides Sub readSeriesInfo()

            Dim i, j, index As Integer
            Dim iType As Type
            Dim indexOffset As Integer
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()
            Me.swmmInfos = New Dictionary(Of Integer, SWMMSeriesInfo)

            oSWMM.OpenSwmmOutFile(Me.File)

            nSubcatch = oSWMM.Nsubcatch
            nNodes = oSWMM.Nnodes
            nLinks = oSWMM.Nlinks
            nPolluts = oSWMM.Npolluts
            nSysvars = oSWMM.nSYSVARS
            nSubcatchVars = oSWMM.nSUBCATCHVARS
            nNodesVars = oSWMM.nNODEVARS
            nLinksVars = oSWMM.nLINKVARS
            FlowUnit = oSWMM.FlowUnits

            'loop over subcatchments
            indexOffset = 0
            iType = Type.Subcatchment
            For i = 0 To nSubcatch - 1
                'Flows
                For j = 0 To nSubcatchVars - nPolluts - 1
                    index = indexOffset + i * nSubcatchVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"Subcatchment {oSWMM.subcatchments(i)} {oSWMM.SUBCATCHVAR(j)}"
                    sInfo.Unit = getUnit(iType, j, FlowUnit)
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    'store SWMM info
                    Dim swmmInfo As New SWMMSeriesInfo()
                    swmmInfo.iType = iType
                    swmmInfo.Name = oSWMM.subcatchments(i)
                    swmmInfo.Variable = "FLOW"
                    swmmInfo.iIndex = i
                    swmmInfo.vIndex = j
                    Me.swmmInfos.Add(index, swmmInfo)
                Next
                'Pollutants
                For j = nSubcatchVars - nPolluts To nSubcatchVars - 1
                    index = indexOffset + i * nSubcatchVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"Subcatchment {oSWMM.subcatchments(i)} {oSWMM.pollutants(j - nSubcatchVars + nPolluts)}"
                    sInfo.Unit = getUnit(iType, j, FlowUnit)
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    'store SWMM info
                    Dim swmmInfo As New SWMMSeriesInfo()
                    swmmInfo.iType = iType
                    swmmInfo.Name = oSWMM.subcatchments(i)
                    swmmInfo.Variable = oSWMM.pollutants(j - nSubcatchVars + nPolluts)
                    swmmInfo.iIndex = i
                    swmmInfo.vIndex = j
                    Me.swmmInfos.Add(index, swmmInfo)
                Next
            Next
            'loop over nodes
            indexOffset += nSubcatch * nSubcatchVars
            iType = Type.Node
            For i = 0 To nNodes - 1
                'Flows
                For j = 0 To nNodesVars - nPolluts - 1
                    index = indexOffset + i * nNodesVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"Node {oSWMM.nodes(i)} {oSWMM.NODEVAR(j)}"
                    sInfo.Unit = getUnit(iType, j, FlowUnit)
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    'store SWMM info
                    Dim swmmInfo As New SWMMSeriesInfo()
                    swmmInfo.iType = iType
                    swmmInfo.Name = oSWMM.nodes(i)
                    swmmInfo.Variable = "FLOW"
                    swmmInfo.iIndex = i
                    swmmInfo.vIndex = j
                    Me.swmmInfos.Add(index, swmmInfo)
                Next
                'Pollutants
                For j = nNodesVars - nPolluts To nNodesVars - 1
                    index = indexOffset + i * nNodesVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"Node {oSWMM.nodes(i)} {oSWMM.pollutants(j - nNodesVars + nPolluts)}"
                    sInfo.Unit = getUnit(iType, j, FlowUnit)
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    'store SWMM info
                    Dim swmmInfo As New SWMMSeriesInfo()
                    swmmInfo.iType = iType
                    swmmInfo.Name = oSWMM.nodes(i)
                    swmmInfo.Variable = oSWMM.pollutants(j - nNodesVars + nPolluts)
                    swmmInfo.iIndex = i
                    swmmInfo.vIndex = j
                    Me.swmmInfos.Add(index, swmmInfo)
                Next
            Next
            'loop over links
            indexOffset += nNodes * nNodesVars
            iType = Type.Link
            For i = 0 To nLinks - 1
                'Flows
                For j = 0 To nLinksVars - nPolluts - 1
                    index = indexOffset + i * nLinksVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"Link {oSWMM.links(i)} {oSWMM.LINKVAR(j)}"
                    sInfo.Unit = getUnit(iType, j, FlowUnit)
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    'store SWMM info
                    Dim swmmInfo As New SWMMSeriesInfo()
                    swmmInfo.iType = iType
                    swmmInfo.Name = oSWMM.links(i)
                    swmmInfo.Variable = "FLOW"
                    swmmInfo.iIndex = i
                    swmmInfo.vIndex = j
                    Me.swmmInfos.Add(index, swmmInfo)
                Next
                'Pollutants
                For j = nLinksVars - nPolluts To nLinksVars - 1
                    index = indexOffset + i * nLinksVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"Link {oSWMM.links(i)} {oSWMM.pollutants(j - nLinksVars + nPolluts)}"
                    sInfo.Unit = getUnit(iType, j, FlowUnit)
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    'store SWMM info
                    Dim swmmInfo As New SWMMSeriesInfo()
                    swmmInfo.iType = iType
                    swmmInfo.Name = oSWMM.links(i)
                    swmmInfo.Variable = oSWMM.pollutants(j - nLinksVars + nPolluts)
                    swmmInfo.iIndex = i
                    swmmInfo.vIndex = j
                    Me.swmmInfos.Add(index, swmmInfo)
                Next
            Next
            'loop over system variables
            indexOffset += nLinks * nLinksVars
            iType = Type.System
            For i = 0 To nSysvars - 1
                index = indexOffset + i
                sInfo = New TimeSeriesInfo()
                sInfo.Name = $"System {oSWMM.SYSVAR(i)}"
                sInfo.Unit = getUnit(iType, i, FlowUnit)
                sInfo.Index = index
                Me.TimeSeriesInfos.Add(sInfo)
                'store SWMM info
                Dim swmmInfo As New SWMMSeriesInfo()
                swmmInfo.iType = iType
                swmmInfo.Name = "System"
                swmmInfo.Variable = oSWMM.SYSVAR(i)
                swmmInfo.iIndex = 0
                swmmInfo.vIndex = i
                Me.swmmInfos.Add(index, swmmInfo)
            Next

        End Sub

        Public Overrides Sub readFile()

            Dim period As Integer
            Dim value As Double
            Dim index As Integer
            Dim datum As Double
            Dim ts As TimeSeries

            'loop over selected series
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries

                index = sInfo.Index

                'instantiate time series
                ts = New TimeSeries(sInfo.Name)
                ts.Unit = sInfo.Unit
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

                'add metadata
                ts.Metadata("Type") = [Enum].GetName(GetType(Type), swmmInfos(index).iType)
                ts.Metadata("Name") = swmmInfos(index).Name
                ts.Metadata("Variable") = swmmInfos(index).Variable

                'read data and add nodes to time series
                For period = 0 To oSWMM.NPeriods - 1
                    oSWMM.GetSwmmDate(period, datum)
                    oSWMM.GetSwmmResult(swmmInfos(index).iType, swmmInfos(index).iIndex, swmmInfos(index).vIndex, period, value)
                    ts.AddNode(DateTime.FromOADate(datum), value)
                Next

                'store time series
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

        End Sub

        ''' <summary>
        ''' Sets default metadata values for a time series corresponding to the SWMM binary output file format
        ''' </summary>
        Public Overloads Shared Sub setDefaultMetadata(ts As TimeSeries)
            'Make sure all required keys exist
            ts.Metadata.AddKeys(SWMM_OUT.MetadataKeys)
            'Set default values
            If ts.Metadata("Type") = "" Then ts.Metadata("Type") = "Node"
            If ts.Metadata("Name") = "" Then ts.Metadata("Name") = ts.Title
            If ts.Metadata("Variable") = "" Then ts.Metadata("Variable") = "FLOW"
        End Sub

        ''' <summary>
        ''' Returns the unit for a given element type, variable and flow unit
        ''' </summary>
        ''' <param name="iType">element type</param>
        ''' <param name="vIndex">variable index</param>
        ''' <param name="FlowUnit">flow unit</param>
        ''' <returns>unit as string</returns>
        ''' <remarks>see https://github.com/USEPA/Stormwater-Management-Model/blob/master/src/outfile/include/swmm_output_enums.h</remarks>
        Private Function getUnit(iType As Type, vIndex As Integer, FlowUnit As FlowUnits) As String

            '_SUBCATCHVAR (iType = 0)
            '                {"Rainfall",     //0 for rainfall (in/hr or mm/hr)
            '                 "Snow Depth",   //1 for snow depth (in or mm)
            '                 "Losses",       //2 for evaporation + infiltration losses (in/hr or mm/hr) 
            '                 "Runoff",       //3 for runoff rate (flow units)
            '                 "GW Flow",      //4 for groundwater outflow rate (flow units)
            '                 "GW Elev"};     //5 for groundwater water table elevation (ft or m)
            '_NODEVAR (iType = 1)
            '                {"Depth",        //0 for depth of water above invert (ft or m)
            '                "Head",          //1 for hydraulic head (ft or m)
            '                "Volume",        //2 for volume of stored + ponded water (ft3 or m3)
            '                "Lateral Inflow",//3 for lateral inflow (flow units) 
            '                "Total Inflow",  //4 for total inflow (lateral + upstream) (flow units)
            '                "Flooding"};     //5 for flow lost to flooding (flow units)
            '_LINKVAR (iType = 2)
            '                {"Flow",         //0 for flow rate (flow units)
            '                 "Depth",        //1 for flow depth (ft or m)
            '                 "Velocity",     //2 for flow velocity (ft/s or m/s)
            '                 "Froude No",    //3 for Froude number 
            '                 "Capacity"};    //4 for capacity (fraction of conduit filled)
            '_SYSVAR = (iType = 3)
            '                {"Temperature",  //0 for air temperature (deg. F or deg. C),  
            '                 "Rainfall",     //1 for rainfall (in/hr or mm/hr),  
            '                 "Snow",         //2 for snow depth (in or mm),  
            '                 "Losses Evap.+Inf.",  //3 for evaporation + infiltration loss rate (in/hr or mm/hr),  
            '                 "Runoff",       //4 for runoff flow (flow units),  
            '                 "DW Inflow",    //5 for dry weather inflow (flow units),  
            '                 "GW Inflow",    //6 for groundwater inflow (flow units),  
            '                 "RDII Inflow",  //7 for RDII inflow (flow units),  
            '                 "Direct Inflow",//8 for user supplied direct inflow (flow units),  
            '                 "Total Inflow", //9 for total lateral inflow (sum of variables 4 to 8) (flow units),  
            '                 "Flooding",     //10 for flow lost to flooding (flow units),  
            '                 "Outfalls",     //11 for flow leaving through outfalls (flow units),  
            '                 "Stored Volume",//12 for volume of stored water (ft3 or m3),  
            '                 "Rate Evapo"};   //13 for evaporation rate (in/day or mm/day) 

            Dim unit As String = "-"

            Dim flowUnitString As String
            Select Case FlowUnit
                Case FlowUnits.CMS
                    flowUnitString = "CMS"
                Case FlowUnits.LPS
                    flowUnitString = "LPS"
                Case Else
                    Log.AddLogEntry(levels.warning, $"Unable to determine unit for flow unit {FlowUnit}!")
                    flowUnitString = "-"
            End Select

            Select Case iType
                Case Type.Subcatchment
                    Select Case vIndex
                        Case 0
                            unit = "mm/hr"
                        Case 1
                            unit = "mm"
                        Case 2
                            unit = "mm/hr"
                        Case 3
                            unit = flowUnitString
                        Case 4
                            unit = flowUnitString
                        Case 5
                            unit = "m"
                        Case Else
                            unit = "MGL"
                    End Select
                Case Type.Node
                    Select Case vIndex
                        Case 0
                            unit = "m"
                        Case 1
                            unit = "m"
                        Case 2
                            unit = "m³"
                        Case 3
                            unit = flowUnitString
                        Case 4
                            unit = flowUnitString
                        Case 5
                            unit = flowUnitString
                        Case Else
                            unit = "MGL"
                    End Select
                Case Type.Link
                    Select Case vIndex
                        Case 0
                            unit = flowUnitString
                        Case 1
                            unit = "m"
                        Case 2
                            unit = "ms­¹"
                        Case 3
                            unit = "-"
                        Case 4
                            unit = "-"
                        Case Else
                            unit = "MGL"
                    End Select
                Case Type.System
                    Select Case vIndex
                        Case 0
                            unit = "C°"
                        Case 1
                            unit = "mm/hr"
                        Case 2
                            unit = "mm"
                        Case 3
                            unit = "mm/hr"
                        Case 4
                            unit = flowUnitString
                        Case 5
                            unit = flowUnitString
                        Case 6
                            unit = flowUnitString
                        Case 7
                            unit = flowUnitString
                        Case 8
                            unit = flowUnitString
                        Case 9
                            unit = flowUnitString
                        Case 10
                            unit = flowUnitString
                        Case 11
                            unit = flowUnitString
                        Case 12
                            unit = "m³"
                        Case 13
                            unit = "mm/day"
                        Case 14
                            unit = "mm/day"
                    End Select
                Case Else
                    Log.AddLogEntry(levels.warning, $"Unable to determine unit for element type {iType}!")
            End Select

            Return unit

        End Function

    End Class

End Namespace