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
Imports System.Windows.Forms
Imports DHI.Generic.MikeZero
''' <summary>
''' Dialog for setting EUM Item and Unit for writing to DFS0
''' </summary>
Friend Class DFS0_ExportDialog

    ''' <summary>
    ''' Columns in the DataGridView
    ''' </summary>
    Private Enum Columns
        id
        title
        unit
        eumItem
        eumUnit
    End Enum

    Private tableItems As DataTable
    Private tableUnits As DataTable

    Public Sub New(tsList As List(Of TimeSeries))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'initialize selectable items and units
        'TODO: this may also be doable with EUMWrapper.eumSetProductFilter() and EUMWrapper.eumGetItemTypeSeq() or similar
        Dim items As New List(Of eumItem) From {
            eumItem.eumIItemUndefined,
            eumItem.eumIBottomLevel,
            eumItem.eumICapillaryFlux,
            eumItem.eumIConveyanceLoss,
            eumItem.eumIDamcrestlevel,
            eumItem.eumIDischarge,
            eumItem.eumIenergy,
            eumItem.eumIEvaporation,
            eumItem.eumIEvaporationFlux,
            eumItem.eumIEvaporationRate,
            eumItem.eumIFlowRate,
            eumItem.eumIFraction,
            eumItem.eumIGroundWaterAbstractionFlux,
            eumItem.eumIGroundWaterDepth,
            eumItem.eumIGroundwaterRecharge,
            eumItem.eumIHeadDifference,
            eumItem.eumIInfiltration,
            eumItem.eumIInfiltrationFlux,
            eumItem.eumIIrrigationRate,
            eumItem.eumIPotentialEvapRate,
            eumItem.eumIPower,
            eumItem.eumIPrecipitation,
            eumItem.eumIRainfall,
            eumItem.eumIRainfallIntensity,
            eumItem.eumIRainfallRate,
            eumItem.eumIRecharge,
            eumItem.eumIReductionfraction,
            eumItem.eumIReductionlevel,
            eumItem.eumIRelMoistureCont,
            eumItem.eumIReturnFlowFraction,
            eumItem.eumISeepagefraction,
            eumItem.eumISurfaceArea,
            eumItem.eumISurfStorage_OF0,
            eumItem.eumISurfStorage_OF1,
            eumItem.eumITemperature,
            eumItem.eumIVolume,
            eumItem.eumIWaterDepth,
            eumItem.eumIWaterLevel,
            eumItem.eumIWaterVolume
        }

        Dim units As New List(Of eumUnit) From {
            eumUnit.eumUUnitUndefined,
            eumUnit.eumUdegreeCelsius,
            eumUnit.eumUPerCent,
            eumUnit.eumUOnePerOne,
            eumUnit.eumUliterPerSecPerKm2,
            eumUnit.eumUmeter,
            eumUnit.eumUmeterPerSec,
            eumUnit.eumUm2,
            eumUnit.eumUm3,
            eumUnit.eumUTenTo6m3,
            eumUnit.eumUm3PerSec,
            eumUnit.eumUmillimeter,
            eumUnit.eumUmillimeterPerHour,
            eumUnit.eumUmmPerDay,
            eumUnit.eumUmillimeterPerMonth,
            eumUnit.eumUmillimeterPerYear,
            eumUnit.eumUteraJoule,
            eumUnit.eumUwatt
        }

        'fill data tables for combobox columns
        tableItems = New DataTable("eumItems")
        tableItems.Columns.Add("eumItem", GetType(Integer)) 'must not be of type eumItem because that causes display issues!
        tableItems.Columns.Add("ItemDescription", GetType(String))
        For Each item As eumItem In items
            Dim description As String = Nothing
            EUMWrapper.eumGetItemTypeKey(item, description)
            tableItems.Rows.Add(item, description)
        Next

        tableUnits = New DataTable("eumUnits")
        tableUnits.Columns.Add("eumUnit", GetType(Integer)) 'must not be of type eumUnit because that causes display issues!
        tableUnits.Columns.Add("UnitAbbreviation", GetType(String))
        For Each unit As eumUnit In units
            Dim description As String = Nothing
            EUMWrapper.eumGetUnitAbbreviation(unit, description)
            tableUnits.Rows.Add(unit, description)
        Next

        'setup combobox columns
        Me.EUMItemColumn.DataSource = tableItems
        Me.EUMItemColumn.DataPropertyName = "eumItem"
        Me.EUMItemColumn.ValueMember = "eumItem"
        Me.EUMItemColumn.DisplayMember = "ItemDescription"
        Me.EUMItemColumn.ValueType = GetType(Integer)

        Me.EUMUnitColumn.DataSource = tableUnits
        Me.EUMUnitColumn.DataPropertyName = "eumUnit"
        Me.EUMUnitColumn.ValueMember = "eumUnit"
        Me.EUMUnitColumn.DisplayMember = "UnitAbbreviation"
        Me.EUMUnitColumn.ValueType = GetType(Integer)

        'add timeseries to datagridview
        For Each ts As TimeSeries In tsList
            'convert unit to eumUnit
            Dim unit As eumUnit = DFS0.UnitToEUMUnit(ts.Unit)
            Dim item As eumItem = eumItem.eumIItemUndefined
            'try to determine eumItem from metadata (only available if timeseries was read from DFS0)
            If ts.Metadata.ContainsKey("eumItem") Then
                Dim eumItemString As String = ts.Metadata("eumItem")
                Dim eumItem As eumItem = [Enum].Parse(GetType(eumItem), eumItemString)
                If items.Contains(eumItem) Then
                    item = eumItem
                Else
                    Log.AddLogEntry(levels.debug, $"Metadata eumItem '{eumItemString}' is not contained in list.")
                End If
            End If
            'cast item and unit cell values to Integer to prevent automatic conversion of enum members to string
            Me.DataGridView1.Rows.Add({ts.Id, ts.Title, ts.Unit, CType(item, Integer), CType(unit, Integer)})
        Next

    End Sub

    ''' <summary>
    ''' Commit edits as soon as they occur
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    ''' <summary>
    ''' Handles changed cell values
    ''' Checks whether EUM Item and Unit are compatible 
    ''' and if not, displays an error message and disables the OK button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        Dim item As eumItem = DataGridView1.Rows(e.RowIndex).Cells(Columns.eumItem).Value
        Dim unit As eumUnit = DataGridView1.Rows(e.RowIndex).Cells(Columns.eumUnit).Value

        'check whether EUM Item and Unit are compatible
        Dim quantity As New eumQuantity(item)
        Dim allowedUnits As eumUnit() = EUMWrapper.GetItemAllowedUnits(item) ' quantity.AllowedUnitsForItem
        If allowedUnits.Contains(unit) Then
            DataGridView1.Rows(e.RowIndex).ErrorText = Nothing
            Me.OK_Button.Enabled = True
        Else
            Dim allowedUnitsString As String = String.Join(", ", allowedUnits)
            DataGridView1.Rows(e.RowIndex).ErrorText = $"EUM Item and Unit are incompatible! Allowed units for item {item} are {allowedUnitsString}"
            Me.OK_Button.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' Returns a dictionary of timeseries IDs and eumQuantities from the DataGridView
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Quantities As Dictionary(Of Integer, eumQuantity)
        Get
            Dim _quantities As New Dictionary(Of Integer, eumQuantity)
            For Each row As DataGridViewRow In Me.DataGridView1.Rows
                Dim id As Integer = row.Cells(Columns.id).Value
                Dim item As eumItem = row.Cells(Columns.eumItem).Value
                Dim unit As eumUnit = row.Cells(Columns.eumUnit).Value
                Dim quantity As New eumQuantity(item, unit)
                _quantities.Add(id, quantity)
            Next
            Return _quantities
        End Get
    End Property


    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
