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

Friend Class MetadataDialog

    Public Metadata As Metadata

    ''' <summary>
    ''' Instantiates a new MetadataDialog
    ''' </summary>
    ''' <param name="ts">Timeseries whose metadata to display</param>
    ''' <param name="visibleKeys">List of metadata keys to display (others will not be visible, thus cannot be edited)</param>
    Public Sub New(ts As TimeSeries, visibleKeys As List(Of String))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.LabelSeries.Text = ts.Title

        Dim rowIndex As Integer

        'Display passed metadata in DataGridView
        For Each kvp As KeyValuePair(Of String, String) In ts.Metadata
            rowIndex = Me.DataGridView1.Rows.Add(kvp.Key, kvp.Value)
            'hide rows not contained in visibleKeys
            If Not visibleKeys.Contains(kvp.Key) Then
                Me.DataGridView1.Rows(rowIndex).Visible = False
            End If
        Next

    End Sub

    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click
        'Store metadata
        Me.Metadata = New Metadata()
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            Me.Metadata.Add(row.Cells(0).Value, row.Cells(1).Value)
        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
