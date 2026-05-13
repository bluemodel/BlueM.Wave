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
Friend Class ExportDiag

    Public Sub New(ByRef timeseries As TimeSeriesCollection)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'add formats to combobox
        With Me.ComboBox_Format.Items
            .Add(TimeSeriesFile.FileTypes.CSV)
            .Add(TimeSeriesFile.FileTypes.BIN)
            .Add(TimeSeriesFile.FileTypes.DFS0)
            .Add(TimeSeriesFile.FileTypes.FEWS_PI)
            .Add(TimeSeriesFile.FileTypes.HYBNAT_BCS)
            .Add(TimeSeriesFile.FileTypes.HYSTEM_REG)
            .Add(TimeSeriesFile.FileTypes.SMUSI_REG)
            .Add(TimeSeriesFile.FileTypes.SWMM_INTERFACE)
            .Add(TimeSeriesFile.FileTypes.SWMM_TIMESERIES)
            .Add(TimeSeriesFile.FileTypes.UVF)
            .Add(TimeSeriesFile.FileTypes.ZRE)
            .Add(TimeSeriesFile.FileTypes.ZRXP)
        End With
        Me.ComboBox_Format.SelectedIndex = 0

        'Add series to listbox
        For Each zre As TimeSeries In timeseries.Values
            Me.ListBox_Series.Items.Add(zre)
        Next

    End Sub

    ''' <summary>
    ''' OK button pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_OK_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click

        'validate inputs
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MessageBox.Show("Please select at least one series!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.DialogResult = DialogResult.None
        End If

    End Sub

    ''' <summary>
    ''' Button 'select all' pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_SelectAll_Click(sender As System.Object, e As System.EventArgs) Handles Button_SelectAll.Click

        Dim i As Long
        For i = 0 To Me.ListBox_Series.Items.Count - 1
            Me.ListBox_Series.SetSelected(i, True)
        Next

    End Sub

End Class