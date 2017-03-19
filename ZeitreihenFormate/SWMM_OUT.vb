'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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
Imports System.IO
Imports System.Globalization

''' <summary>
''' Klasse für SWMM out-Dateiformat (Binärer SWMM Ergebnisfile)
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/ASC-Format</remarks>
''' 
Public Class SWMM_OUT
    Inherits Dateiformat
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
    Private FlowUnits As Integer

    Structure SWMM_Binary_file_Definition
        'iType = type of object whose value is being sought
        '(0 = subcatchment, 1 = node, 2 = link, 3 = system)
        'iIndex = index of item being sought (starting from 0)
        'vIndex = index of variable being sought (see Interfacing Guide)
        Dim iType As Integer
        Dim iIndex As Integer
        Dim vIndex As Integer
    End Structure
    Dim SWMMBinaryFileIndex() As SWMM_Binary_file_Definition


    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal Series As String = "")

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileUeberschriften = 1
        Me.UseEinheiten = True
        Me.iZeileEinheiten = 0
        Me.iZeileDaten = 0
        Me.Zeichengetrennt = False
        Me.Trennzeichen = Me.leerzeichen
        Me.Dezimaltrennzeichen = Me.punkt
        Me.XSpalte = 0

        oSWMM = New modelEAU.SWMM.DllAdapter.SWMM_iface()

        Call Me.SpaltenAuslesen()

        If Series <> "" Then
            Read_File(Series)
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim i, j, index As Integer
        Dim indexSpalten As Integer


        oSWMM.OpenSwmmOutFile(Me.File)

        nSubcatch = oSWMM.Nsubcatch
        nNodes = oSWMM.Nnodes
        nLinks = oSWMM.Nlinks
        nPolluts = oSWMM.Npolluts
        nSysvars = oSWMM.nSYSVARS
        nSubcatchVars = oSWMM.nSUBCATCHVARS
        nNodesVars = oSWMM.nNODEVARS
        nLinksVars = oSWMM.nLINKVARS
        FlowUnits = oSWMM.FlowUnits

        'Spaltenüberschriften
        anzSpalten = nSubcatch * nSubcatchVars _
                   + nNodes * nNodesVars _
                   + nLinks * nLinksVars _
                   + nSysvars _
                   + 1

        ReDim Me.Spalten(anzSpalten - 1)
        ReDim SWMMBinaryFileIndex(anzSpalten - 1)

        Me.Spalten(0).Name = "Date"
        Me.Spalten(0).Index = 0
        Me.Spalten(0).Einheit = "-"
        indexSpalten = 1
        For i = 0 To nSubcatch - 1
            'Flows
            For j = 0 To nSubcatchVars - nPolluts - 1
                index = indexSpalten + i * nSubcatchVars + j
                Me.Spalten(index).Name = oSWMM.subcatchments(i) & " " & oSWMM.SUBCATCHVAR(j)
                Me.Spalten(index).Objekt = oSWMM.subcatchments(i)
                Me.Spalten(index).Einheit = Units(0, j, FlowUnits)
                Me.Spalten(index).Type = "FLOW"
                Me.Spalten(index).ObjType = "Subcatchment"
                Me.Spalten(index).Index = index
                SWMMBinaryFileIndex(index).iType = 0
                SWMMBinaryFileIndex(index).iIndex = i
                SWMMBinaryFileIndex(index).vIndex = j
            Next
            'Pollutants
            For j = nSubcatchVars - nPolluts To nSubcatchVars - 1
                index = indexSpalten + i * nSubcatchVars + j
                Me.Spalten(index).Name = oSWMM.subcatchments(i) & " " & oSWMM.pollutants(j - nSubcatchVars + nPolluts)
                Me.Spalten(index).Objekt = oSWMM.subcatchments(i)
                Me.Spalten(index).Einheit = Units(0, j, FlowUnits)
                'Type aus String (z.B. für "S101 CSB" wird "CSB" ausgelesen)
                Me.Spalten(index).Type = oSWMM.pollutants(j - nSubcatchVars + nPolluts)
                Me.Spalten(index).ObjType = "Subcatchment"
                Me.Spalten(index).Index = index
                SWMMBinaryFileIndex(index).iType = 0
                SWMMBinaryFileIndex(index).iIndex = i
                SWMMBinaryFileIndex(index).vIndex = j
            Next
        Next
        indexSpalten += nSubcatch * nSubcatchVars
        For i = 0 To nNodes - 1
            'Flows
            For j = 0 To nNodesVars - nPolluts - 1
                index = indexSpalten + i * nNodesVars + j
                Me.Spalten(index).Name = oSWMM.nodes(i) & " " & oSWMM.NODEVAR(j)
                Me.Spalten(index).Objekt = oSWMM.nodes(i)
                Me.Spalten(index).Einheit = Units(1, j, FlowUnits)
                Me.Spalten(index).Type = "FLOW"
                Me.Spalten(index).ObjType = "Node"
                Me.Spalten(index).Index = index
                SWMMBinaryFileIndex(index).iType = 1
                SWMMBinaryFileIndex(index).iIndex = i
                SWMMBinaryFileIndex(index).vIndex = j
            Next
            'Pollutants
            For j = nNodesVars - nPolluts To nNodesVars - 1
                index = indexSpalten + i * nNodesVars + j
                Me.Spalten(index).Name = oSWMM.nodes(i) & " " & oSWMM.pollutants(j - nNodesVars + nPolluts)
                Me.Spalten(index).Objekt = oSWMM.nodes(i)
                Me.Spalten(index).Einheit = Units(1, j, FlowUnits)
                'Type aus String (z.B. für "S101 CSB" wird "CSB" ausgelesen)
                Me.Spalten(index).Type = oSWMM.pollutants(j - nNodesVars + nPolluts)
                Me.Spalten(index).ObjType = "Node"
                Me.Spalten(index).Index = index
                SWMMBinaryFileIndex(index).iType = 1
                SWMMBinaryFileIndex(index).iIndex = i
                SWMMBinaryFileIndex(index).vIndex = j
            Next
        Next
        indexSpalten += nNodes * nNodesVars
        For i = 0 To nLinks - 1
            'Flows
            For j = 0 To nLinksVars - nPolluts - 1
                index = indexSpalten + i * nLinksVars + j
                Me.Spalten(index).Name = oSWMM.links(i) & " " & oSWMM.LINKVAR(j)
                Me.Spalten(index).Objekt = oSWMM.links(i)
                Me.Spalten(index).Einheit = Units(2, j, FlowUnits)
                Me.Spalten(index).Type = "FLOW"
                Me.Spalten(index).ObjType = "Link"
                Me.Spalten(index).Index = index
                SWMMBinaryFileIndex(index).iType = 2
                SWMMBinaryFileIndex(index).iIndex = i
                SWMMBinaryFileIndex(index).vIndex = j
            Next
            'Pollutants
            For j = nLinksVars - nPolluts To nLinksVars - 1
                index = indexSpalten + i * nLinksVars + j
                Me.Spalten(index).Name = oSWMM.links(i) & " " & oSWMM.pollutants(j - nLinksVars + nPolluts)
                Me.Spalten(index).Objekt = oSWMM.links(i)
                Me.Spalten(index).Einheit = Units(2, j, FlowUnits)
                'Type aus String (z.B. für "S101 CSB" wird "CSB" ausgelesen)
                Me.Spalten(index).Type = oSWMM.pollutants(j - nLinksVars + nPolluts)
                Me.Spalten(index).ObjType = "Link"
                Me.Spalten(index).Index = index
                SWMMBinaryFileIndex(index).iType = 2
                SWMMBinaryFileIndex(index).iIndex = i
                SWMMBinaryFileIndex(index).vIndex = j
            Next
        Next
        indexSpalten += nLinks * nLinksVars
        For i = 0 To nSysvars - 1
            index = indexSpalten + i
            Me.Spalten(index).Name = oSWMM.SYSVAR(i)
            Me.Spalten(index).Einheit = Units(3, j, FlowUnits)
            Me.Spalten(index).Index = index
            SWMMBinaryFileIndex(index).iType = 3
            SWMMBinaryFileIndex(index).iIndex = 0
            SWMMBinaryFileIndex(index).vIndex = i
        Next

    End Sub

#End Region


    Public Overrides Sub Read_File()
        Dim i, j, period As Integer
        Dim value As Double
        Dim index As Integer
        Dim anzahlZeitreihen As Integer
        Dim datum As Double

        'Anzahl Zeitreihen
        anzahlZeitreihen = Me.SpaltenSel.Length
        ReDim Me.Zeitreihen(anzahlZeitreihen - 1)
        'Indexarray
        'ReDim index(anzahlZeitreihen)
        'Zeitreihen instanzieren
        For i = 0 To anzahlZeitreihen - 1
            Me.Zeitreihen(i) = New TimeSeries(Me.SpaltenSel(i).Name)
            'Einheiten?
            If (Me.UseEinheiten) Then
                Me.Zeitreihen(i).Unit = Me.SpaltenSel(i).Einheit
            End If
            'Objektname und Typ (für SWMM-Txt-Export)
            Me.Zeitreihen(i).Objekt = Me.SpaltenSel(i).Objekt
            Me.Zeitreihen(i).Type = Me.SpaltenSel(i).Type
            For j = 0 To anzSpalten - 1
                If (Me.SpaltenSel(i).Name = Me.Spalten(j).Name) And (Me.SpaltenSel(i).ObjType = Me.Spalten(j).ObjType) Then
                    index = j
                    For period = 0 To oSWMM.NPeriods - 1
                        oSWMM.GetSwmmDate(period, datum)
                        oSWMM.GetSwmmResult(SWMMBinaryFileIndex(index).iType, SWMMBinaryFileIndex(index).iIndex, SWMMBinaryFileIndex(index).vIndex, period, value)
                        Me.Zeitreihen(i).AddNode(Date.FromOADate(datum), value)
                    Next
                End If
            Next
        Next
    End Sub

    Private Overloads Sub Read_File(ByVal Series As String)
        Dim i, j, period As Integer
        Dim value As Double
        Dim index As Integer
        Dim anzahlZeitreihen As Integer
        Dim datum As Double

        'Anzahl Zeitreihen
        anzahlZeitreihen = 1
        ReDim Me.Zeitreihen(anzahlZeitreihen - 1)
        'Indexarray
        'ReDim index(anzahlZeitreihen)
        'Zeitreihen instanzieren
        Me.Zeitreihen(i) = New TimeSeries(series)
        For j = 0 To anzSpalten - 1
            If Series = Me.Spalten(j).Name Then
                index = j
                For period = 0 To oSWMM.NPeriods - 1
                    oSWMM.GetSwmmDate(period, datum)
                    oSWMM.GetSwmmResult(SWMMBinaryFileIndex(index).iType, SWMMBinaryFileIndex(index).iIndex, SWMMBinaryFileIndex(index).vIndex, period, value)
                    Me.Zeitreihen(i).AddNode(Date.FromOADate(datum), value)
                Next
            End If
        Next
    End Sub

    Private Function Units(ByVal iType As Integer, ByVal vIndex As Integer, ByVal FlowUnits As Integer) As String
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

        'FlowUnits:
        'CMS = 3
        'LPS = 4
        Units = "-"
        Select Case FlowUnits
            Case 3    'CMS
                Select Case iType
                    Case 0
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
                    Case 1
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
                    Case 2
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
                    Case 3
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
                End Select
            Case 4  'LPS
                Select Case iType
                    Case 0
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
                    Case 1
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
                    Case 2
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
                    Case 3
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
                    End Select
                Case Else
                Units = "-"
        End Select
        Return Units
    End Function
End Class

