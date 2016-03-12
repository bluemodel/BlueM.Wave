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
Imports IHWB.Wave.BlueMOrbDataSet
Imports IHWB.Wave.BlueMOrbDataSetTableAdapters

Public Class ZeitreihenDatabase
    Inherits ZeitreihenExtern

    Private ds As BlueMOrbDataSet
    Private tSelectedResults As SelectedResultsTableAdapter


    Private mSelectedResultIDs As New List(Of Long)


    Public Sub AddZRE(ByVal SIMResultID As Int32)
        mSelectedResultIDs.Add(SIMResultID)
    End Sub


    Public Overrides Sub selectAll_Zeitreihen()
        'Macht keinen Sinn
    End Sub

    Public Overrides ReadOnly Property Typ() As ZeitreihenExtern.ZREQuellenTyp
        Get
            Return ZREQuellenTyp.ZRDatabase
        End Get
    End Property

    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Zeitreihe_Einlesen()
        ReDim Zeitreihen(mSelectedResultIDs.Count - 1)
        Dim index As Integer
        ds = New BlueMOrbDataSet()
        tSelectedResults = New SelectedResultsTableAdapter()
        tSelectedResults.ClearBeforeFill = true

        For Each SimresultID As Long In mSelectedResultIDs
            einzelne_Zeitreihe_Einlesen(Zeitreihen(index), SimresultID)
            index = index + 1
        Next
    End Sub

    Private Sub einzelne_Zeitreihe_Einlesen(ByRef ZRE As Zeitreihe, ByVal SimResultID As Long)
        'Tableadapter aus DB holen
        ds.EnforceConstraints = False
        tSelectedResults.FillBySimResultID(ds.SelectedResults, SimResultID)

        'ZRE setzen
        ZRE = New Zeitreihe()
        If (ds.SelectedResults.Count = 0) Then
            'Hier gibt es nicht mehr zu tun :(
            Return
        End If
        'Titel und Einheit aus dem Erstem Element
        ZRE.Title = ds.SelectedResults(0).ELEMENTSET_NAME + "-" + ds.SelectedResults(0).DIMENSION_NAME
        ZRE.Einheit = ds.SelectedResults(0).DIMENSION_EINHEIT

        For Each row As BlueMOrbDataSet.SelectedResultsRow In ds.SelectedResults
            ZRE.AddNode(row.RESULT_TIME, row.RESULT_VALUE)
        Next

    End Sub



End Class
