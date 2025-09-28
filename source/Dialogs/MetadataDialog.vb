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

    ''' <summary>
    ''' Instantiates a new MetadataDialog
    ''' </summary>
    ''' <param name="ts">Timeseries whose metadata to display</param>
    ''' <param name="fileformat">File format for which to edit metadata</param>
    ''' <param name="visibleKeys">List of metadata keys to display (others will not be visible, thus cannot be edited)</param>
    Public Sub New(ts As TimeSeries, fileformat As TimeSeriesFile.FileTypes, visibleKeys As List(Of String))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Text = $"Edit metadata for file format {fileformat}"
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
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' Returns the metadata from the DataGridView
    ''' </summary>
    ''' <returns>Metadata instance</returns>
    Friend Function getMetadata() As Metadata
        Dim md As New Metadata()
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            md.Add(row.Cells(0).Value, row.Cells(1).Value)
        Next
        Return md
    End Function

End Class
