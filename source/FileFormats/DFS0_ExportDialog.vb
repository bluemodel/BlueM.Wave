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
Imports System.Windows.Forms
Imports DHI.Generic.MikeZero

Namespace Fileformats
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
            eumItem.eumIEvaporationfraction,
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
            eumItem.eumISeepage,
            eumItem.eumISeepagefraction,
            eumItem.eumISpecificRunoff,
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

End Namespace