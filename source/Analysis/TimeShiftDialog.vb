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

Friend Class TimeShiftDialog

    Public Sub New()

        Call InitializeComponent()

        'fill comboboxes
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Year)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Month)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Week)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Day)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Hour)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Minute)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Second)
        Me.ComboBox_TimestepType.SelectedIndex = 3 'default: Day

    End Sub

    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click

        If Me.NumericUpDown_TimestepInterval.Value = 0 Then
            MessageBox.Show("Please select a non-zero timespan.", "Time Shift", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.DialogResult = DialogResult.None
            Exit Sub
        End If
        If Me.ComboBox_TimestepType.SelectedItem = TimeSeries.TimeStepTypeEnum.Year Then
            Dim dlgresult As DialogResult = MessageBox.Show("Shifting by years can cause the loss of data due to leap days! Are you sure?", "Time Shift", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If dlgresult = DialogResult.No Then
                Me.DialogResult = DialogResult.None
                Exit Sub
            End If
        ElseIf Me.ComboBox_TimestepType.SelectedItem = TimeSeries.TimeStepTypeEnum.Month Then
            Dim dlgresult As DialogResult = MessageBox.Show("Shifting by months can cause the loss of data due to varying month lengths! Are you sure?", "Time Shift", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If dlgresult = DialogResult.No Then
                Me.DialogResult = DialogResult.None
                Exit Sub
            End If
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
