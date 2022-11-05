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
Friend Class GoodnessOfFit_Dialog

    Public Sub New(zre1 As String, zre2 As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.RadioButton1.Text = zre1
        Me.RadioButton2.Text = zre2

        Me.ComboBox_startMonth.BeginUpdate()
        Me.ComboBox_startMonth.Items.AddRange(Helpers.CalendarMonths.ToArray)
        Me.ComboBox_startMonth.EndUpdate()

        Me.ComboBox_startMonth.SelectedIndex = 10 'November
    End Sub

    Public Function getNrGemesseneReihe() As Integer

        If (Me.RadioButton1.Checked) Then
            Return 1
        Else
            Return 2
        End If

    End Function

    Private Sub CheckBox_Annual_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Annual.CheckedChanged
        If CheckBox_Annual.Checked Then
            Me.Label_startMonth.Enabled = True
            Me.ComboBox_startMonth.Enabled = True
        Else
            Me.Label_startMonth.Enabled = False
            Me.ComboBox_startMonth.Enabled = False
        End If
    End Sub
End Class