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

        Private anzSpalten As Integer
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
            subcatchment = 0
            node = 1
            link = 2
            system = 3
            pollutant = 4
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
        Private Structure SeriesInfo
            ''' <summary>
            ''' element type
            ''' </summary>
            Dim iType As Type
            ''' <summary>
            ''' element index (0-based)
            ''' </summary>
            Dim iIndex As Integer
            ''' <summary>
            ''' variable index (see https://github.com/USEPA/Stormwater-Management-Model/blob/master/src/outfile/include/swmm_output_enums.h)
            ''' </summary>
            Dim vIndex As Integer
        End Structure

        ''' <summary>
        ''' Array containing all series infos
        ''' </summary>
        Private SeriesInfos() As SeriesInfo

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
            Dim indexSpalten As Integer
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

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

            'Spaltenüberschriften
            anzSpalten = nSubcatch * nSubcatchVars _
                   + nNodes * nNodesVars _
                   + nLinks * nLinksVars _
                   + nSysvars

            ReDim SeriesInfos(anzSpalten - 1)

            indexSpalten = 0
            'loop over subcatchments
            iType = Type.subcatchment
            For i = 0 To nSubcatch - 1
                'Flows
                For j = 0 To nSubcatchVars - nPolluts - 1
                    index = indexSpalten + i * nSubcatchVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"subcatchment {oSWMM.subcatchments(i)} {oSWMM.SUBCATCHVAR(j)}"
                    sInfo.Objekt = oSWMM.subcatchments(i)
                    sInfo.Unit = Units(iType, j, FlowUnit)
                    sInfo.Type = "FLOW"
                    sInfo.ObjType = "Subcatchment"
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    SeriesInfos(index).iType = iType
                    SeriesInfos(index).iIndex = i
                    SeriesInfos(index).vIndex = j
                Next
                'Pollutants
                For j = nSubcatchVars - nPolluts To nSubcatchVars - 1
                    index = indexSpalten + i * nSubcatchVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"subcatchment {oSWMM.subcatchments(i)} {oSWMM.pollutants(j - nSubcatchVars + nPolluts)}"
                    sInfo.Objekt = oSWMM.subcatchments(i)
                    sInfo.Unit = Units(iType, j, FlowUnit)
                    'Type aus String (z.B. für "S101 CSB" wird "CSB" ausgelesen)
                    sInfo.Type = oSWMM.pollutants(j - nSubcatchVars + nPolluts)
                    sInfo.ObjType = "Subcatchment"
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    SeriesInfos(index).iType = iType
                    SeriesInfos(index).iIndex = i
                    SeriesInfos(index).vIndex = j
                Next
            Next
            indexSpalten += nSubcatch * nSubcatchVars
            'loop over nodes
            iType = Type.node
            For i = 0 To nNodes - 1
                'Flows
                For j = 0 To nNodesVars - nPolluts - 1
                    index = indexSpalten + i * nNodesVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"node {oSWMM.nodes(i)} {oSWMM.NODEVAR(j)}"
                    sInfo.Objekt = oSWMM.nodes(i)
                    sInfo.Unit = Units(iType, j, FlowUnit)
                    sInfo.Type = "FLOW"
                    sInfo.ObjType = "Node"
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    SeriesInfos(index).iType = iType
                    SeriesInfos(index).iIndex = i
                    SeriesInfos(index).vIndex = j
                Next
                'Pollutants
                For j = nNodesVars - nPolluts To nNodesVars - 1
                    index = indexSpalten + i * nNodesVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"node {oSWMM.nodes(i)} {oSWMM.pollutants(j - nNodesVars + nPolluts)}"
                    sInfo.Objekt = oSWMM.nodes(i)
                    sInfo.Unit = Units(iType, j, FlowUnit)
                    'Type aus String (z.B. für "S101 CSB" wird "CSB" ausgelesen)
                    sInfo.Type = oSWMM.pollutants(j - nNodesVars + nPolluts)
                    sInfo.ObjType = "Node"
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    SeriesInfos(index).iType = iType
                    SeriesInfos(index).iIndex = i
                    SeriesInfos(index).vIndex = j
                Next
            Next
            indexSpalten += nNodes * nNodesVars
            'loop over links
            iType = Type.link
            For i = 0 To nLinks - 1
                'Flows
                For j = 0 To nLinksVars - nPolluts - 1
                    index = indexSpalten + i * nLinksVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"link {oSWMM.links(i)} {oSWMM.LINKVAR(j)}"
                    sInfo.Objekt = oSWMM.links(i)
                    sInfo.Unit = Units(iType, j, FlowUnit)
                    sInfo.Type = "FLOW"
                    sInfo.ObjType = "Link"
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    SeriesInfos(index).iType = iType
                    SeriesInfos(index).iIndex = i
                    SeriesInfos(index).vIndex = j
                Next
                'Pollutants
                For j = nLinksVars - nPolluts To nLinksVars - 1
                    index = indexSpalten + i * nLinksVars + j
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"link {oSWMM.links(i)} {oSWMM.pollutants(j - nLinksVars + nPolluts)}"
                    sInfo.Objekt = oSWMM.links(i)
                    sInfo.Unit = Units(iType, j, FlowUnit)
                    'Type aus String (z.B. für "S101 CSB" wird "CSB" ausgelesen)
                    sInfo.Type = oSWMM.pollutants(j - nLinksVars + nPolluts)
                    sInfo.ObjType = "Link"
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)
                    SeriesInfos(index).iType = iType
                    SeriesInfos(index).iIndex = i
                    SeriesInfos(index).vIndex = j
                Next
            Next
            indexSpalten += nLinks * nLinksVars
            'loop over system variables
            iType = Type.system
            For i = 0 To nSysvars - 1
                index = indexSpalten + i
                sInfo = New TimeSeriesInfo()
                sInfo.Name = $"system {oSWMM.SYSVAR(i)}"
                sInfo.Unit = Units(iType, j, FlowUnit)
                sInfo.Index = index
                Me.TimeSeriesInfos.Add(sInfo)
                SeriesInfos(index).iType = iType
                SeriesInfos(index).iIndex = 0
                SeriesInfos(index).vIndex = i
            Next

        End Sub

        Public Overrides Sub readFile()
            Dim j, period As Integer
            Dim value As Double
            Dim index As Integer
            Dim anzahlZeitreihen As Integer
            Dim datum As Double
            Dim ts As TimeSeries

            'Anzahl Zeitreihen
            anzahlZeitreihen = Me.SelectedSeries.Count

            'Indexarray
            'ReDim index(anzahlZeitreihen)

            'Zeitreihen instanzieren
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                'Einheiten?
                If (Me.UseUnits) Then
                    ts.Unit = sInfo.Unit
                End If
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                'Objektname und Typ (für SWMM-Txt-Export)
                ts.Objekt = sInfo.Objekt
                ts.Type = sInfo.Type
                For j = 0 To anzSpalten - 1
                    If (sInfo.Name = Me.TimeSeriesInfos(j).Name) And (sInfo.ObjType = Me.TimeSeriesInfos(j).ObjType) Then
                        index = j
                        For period = 0 To oSWMM.NPeriods - 1
                            oSWMM.GetSwmmDate(period, datum)
                            oSWMM.GetSwmmResult(SeriesInfos(index).iType, SeriesInfos(index).iIndex, SeriesInfos(index).vIndex, period, value)
                            ts.AddNode(DateTime.FromOADate(datum), value)
                        Next
                    End If
                Next

                'store time series
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

        End Sub


        Private Function Units(iType As Type, vIndex As Integer, FlowUnit As FlowUnits) As String
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

            Units = "-"
            Select Case FlowUnit
                Case FlowUnits.CMS
                    Select Case iType
                        Case Type.subcatchment
                            Select Case vIndex
                                Case 0
                                    Units = "mm/hr"
                                Case 1
                                    Units = "mm"
                                Case 2
                                    Units = "mm/hr"
                                Case 3
                                    Units = "m³s­¹"
                                Case 4
                                    Units = "m³s­¹"
                                Case 5
                                    Units = "m"
                                Case Else
                                    Units = "MGL"
                            End Select
                        Case Type.node
                            Select Case vIndex
                                Case 0
                                    Units = "m"
                                Case 1
                                    Units = "m"
                                Case 2
                                    Units = "m³"
                                Case 3
                                    Units = "m³s­¹"
                                Case 4
                                    Units = "m³s­¹"
                                Case 5
                                    Units = "m³s­¹"
                                Case Else
                                    Units = "MGL"
                            End Select
                        Case Type.link
                            Select Case vIndex
                                Case 0
                                    Units = "m³s­¹"
                                Case 1
                                    Units = "m"
                                Case 2
                                    Units = "ms­¹"
                                Case 3
                                    Units = "-"
                                Case 4
                                    Units = "-"
                                Case Else
                                    Units = "MGL"
                            End Select
                        Case Type.system
                            Select Case vIndex
                                Case 0
                                    Units = "C°"
                                Case 1
                                    Units = "mm/hr"
                                Case 2
                                    Units = "mm"
                                Case 3
                                    Units = "mm/hr"
                                Case 4
                                    Units = "m³s­¹"
                                Case 5
                                    Units = "m³s­¹"
                                Case 6
                                    Units = "m³s­¹"
                                Case 7
                                    Units = "m³s­¹"
                                Case 8
                                    Units = "m³s­¹"
                                Case 9
                                    Units = "m³s­¹"
                                Case 10
                                    Units = "m³s­¹"
                                Case 11
                                    Units = "m³s­¹"
                                Case 12
                                    Units = "m³"
                                Case 13
                                    Units = "mm/day"
                            End Select
                        Case Else
                            Log.AddLogEntry(levels.warning, $"Unable to determine unit for element type {iType}!")
                    End Select
                Case FlowUnits.LPS
                    Select Case iType
                        Case Type.subcatchment
                            Select Case vIndex
                                Case 0
                                    Units = "mm/hr"
                                Case 1
                                    Units = "mm"
                                Case 2
                                    Units = "mm/hr"
                                Case 3
                                    Units = "LPS"
                                Case 4
                                    Units = "LPS"
                                Case 5
                                    Units = "m"
                                Case Else
                                    Units = "MGL"
                            End Select
                        Case Type.node
                            Select Case vIndex
                                Case 0
                                    Units = "m"
                                Case 1
                                    Units = "m"
                                Case 2
                                    Units = "m³"
                                Case 3
                                    Units = "LPS"
                                Case 4
                                    Units = "LPS"
                                Case 5
                                    Units = "LPS"
                                Case Else
                                    Units = "MGL"
                            End Select
                        Case Type.link
                            Select Case vIndex
                                Case 0
                                    Units = "LPS"
                                Case 1
                                    Units = "m"
                                Case 2
                                    Units = "LPS"
                                Case 3
                                    Units = "-"
                                Case 4
                                    Units = "-"
                                Case Else
                                    Units = "MGL"
                            End Select
                        Case Type.system
                            Select Case vIndex
                                Case 0
                                    Units = "C°"
                                Case 1
                                    Units = "mm/hr"
                                Case 2
                                    Units = "mm"
                                Case 3
                                    Units = "mm/hr"
                                Case 4
                                    Units = "LPS"
                                Case 5
                                    Units = "LPS"
                                Case 6
                                    Units = "LPS"
                                Case 7
                                    Units = "LPS"
                                Case 8
                                    Units = "LPS"
                                Case 9
                                    Units = "LPS"
                                Case 10
                                    Units = "LPS"
                                Case 11
                                    Units = "LPS"
                                Case 12
                                    Units = "m³"
                                Case 13
                                    Units = "mm/day"
                            End Select
                        Case Else
                            Log.AddLogEntry(levels.warning, $"Unable to determine unit for element type {iType}!")
                    End Select
                Case Else
                    Log.AddLogEntry(levels.warning, $"Unable to determine unit for flow unit {FlowUnit}!")
            End Select
            Return Units
        End Function
    End Class

End Namespace