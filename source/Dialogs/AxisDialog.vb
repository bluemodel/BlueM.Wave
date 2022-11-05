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
Friend Class AxisDialog

    ''' <summary>
    ''' Is raised when an axis unit is changed by the user
    ''' </summary>
    Friend Event AxisUnitChanged()

    ''' <summary>
    ''' Is raised when an axis is deleted by the user
    ''' </summary>
    ''' <param name="axisname"></param>
    Friend Event AxisDeleted(axisname As String)

    Public Overloads Sub Update(ByRef axisList As List(Of AxisWrapper))
        Me.AxisWrapperBindingSource.DataSource = axisList
    End Sub

    ''' <summary>
    ''' Handles changed cell values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If Me.DataGridView1.Columns(e.ColumnIndex).Name = "UnitColumn" Then
            RaiseEvent AxisUnitChanged()
        End If

    End Sub

    ''' <summary>
    ''' Handles user trying to delete a row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow

        Dim axisname As String

        'cancel row deletion because datagridview will be refreshed from outside
        'otherwise, two rows end up being deleted
        e.Cancel = True

        axisname = CType(e.Row.DataBoundItem, AxisWrapper).Name
        If axisname = "Left" Or axisname = "Right" Then
            MsgBox("Left and Right axes cannot be deleted.", MsgBoxStyle.Information)
            Return
        Else
            If MsgBox($"Delete axis {axisname}?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RaiseEvent AxisDeleted(axisname)
            End If
        End If
    End Sub

    Private Sub PropertiesDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'prevent the form from closing and hide it instead
        e.Cancel = True
        Call Me.Hide()
    End Sub

End Class
