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
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.CSV)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.BIN)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.DFS0)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.HYSTEM_REG)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.SMUSI_REG)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.SWMM_DAT_MASS)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.SWMM_DAT_TIME)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.SWMM_INTERFACE)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.UVF)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.ZRE)
        Me.ComboBox_Format.Items.Add(TimeSeriesFile.FileTypes.ZRXP)
        Me.ComboBox_Format.SelectedIndex = 0

        'Add series to listbox
        For Each zre As TimeSeries In timeseries.Values
            Me.ListBox_Series.Items.Add(zre)
        Next

    End Sub

    ''' <summary>
    ''' Selected format changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox_Format_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox_Format.SelectedIndexChanged

        Select Case ComboBox_Format.SelectedItem

            Case TimeSeriesFile.FileTypes.ZRE,
                 TimeSeriesFile.FileTypes.HYSTEM_REG,
                 TimeSeriesFile.FileTypes.SMUSI_REG,
                 TimeSeriesFile.FileTypes.SWMM_DAT_MASS,
                 TimeSeriesFile.FileTypes.SWMM_DAT_TIME,
                 TimeSeriesFile.FileTypes.BIN,
                 TimeSeriesFile.FileTypes.UVF,
                 TimeSeriesFile.FileTypes.ZRXP
                'Allow selection of a single series
                Me.ListBox_Series.SelectionMode = SelectionMode.One
                Me.Button_SelectAll.Enabled = False

            Case TimeSeriesFile.FileTypes.SWMM_INTERFACE,
                 TimeSeriesFile.FileTypes.CSV,
                 TimeSeriesFile.FileTypes.DFS0
                'Allow selection of multiple series
                Me.ListBox_Series.SelectionMode = SelectionMode.MultiExtended
                Me.Button_SelectAll.Enabled = True

            Case Else
                'not yet implemented
                MsgBox("Not yet implemented!", MsgBoxStyle.Exclamation)
                ComboBox_Format.SelectedIndex = 0
        End Select

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
            MsgBox("Please select at least one series!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
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