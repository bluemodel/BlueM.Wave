'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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

Public Class AxisDialog

    ''' <summary>
    ''' Is raised when an axis unit is changed by the user
    ''' </summary>
    Friend Event AxisUnitChanged()

    ''' <summary>
    ''' Is raised when an axis is deleted by the user
    ''' </summary>
    ''' <param name="axisname"></param>
    Friend Event AxisDeleted(ByVal axisname As String)

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
            If MsgBox("Delete axis " & axisname & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
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
