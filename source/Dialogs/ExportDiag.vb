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
        Me.ComboBox_Format.Items.AddRange({
            TimeSeriesFile.FileTypes.CSV,
            TimeSeriesFile.FileTypes.BIN,
            TimeSeriesFile.FileTypes.DFS0,
            TimeSeriesFile.FileTypes.FEWS_PI,
            TimeSeriesFile.FileTypes.HYBNAT_BCS,
            TimeSeriesFile.FileTypes.HYSTEM_REG,
            TimeSeriesFile.FileTypes.SMUSI_REG,
            TimeSeriesFile.FileTypes.SWMM_INTERFACE,
            TimeSeriesFile.FileTypes.SWMM_TIMESERIES,
            TimeSeriesFile.FileTypes.UVF,
            TimeSeriesFile.FileTypes.ZRE,
            TimeSeriesFile.FileTypes.ZRXP
        })
        Me.ComboBox_Format.SelectedIndex = 0

        'Add series to listbox
        Me.ListBox_Series.Items.AddRange(timeseries.Values.ToArray())

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