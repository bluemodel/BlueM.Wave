'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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

Friend Class PropertiesDialog

    ''' <summary>
    ''' Is raised when a property is changed by the user
    ''' </summary>
    ''' <param name="id">Id of the time series whose properties were changed</param>
    Friend Event PropertyChanged(id As Integer)

    ''' <summary>
    ''' Is raised when the user deletes a row/series
    ''' </summary>
    ''' <param name="id">Id of the time series that was deleted</param>
    Friend Event SeriesDeleted(id As Integer)

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Interpretation.DataSource = System.Enum.GetValues(GetType(TimeSeries.InterpretationEnum))
    End Sub

    ''' <summary>
    ''' Updates the form with a new List of TimeSeries
    ''' </summary>
    ''' <param name="seriesList">the new List of TimeSeries</param>
    Public Overloads Sub Update(ByRef seriesList As List(Of TimeSeries))
        Me.TimeSeriesBindingSource.DataSource = seriesList
        MyBase.Update()
    End Sub

    ''' <summary>
    ''' Handles the Show statistics button being pressed
    ''' Shows/hides the columns with statistics
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub showStatisticis_Click(sender As Object, e As EventArgs) Handles ToolStripButton_showStatistics.Click
        Dim colindex As Integer = 6 'index of the first column with statistics
        If ToolStripButton_showStatistics.Checked Then
            For Each col As DataGridViewColumn In DataGridView1.Columns()
                If col.Index >= colindex Then
                    col.Visible = True
                End If
            Next
        Else
            For Each col As DataGridViewColumn In DataGridView1.Columns()
                If col.Index >= colindex Then
                    col.Visible = False
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Commit edits as soon as they occur, but only if they occur in the Interpretation column
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        If DataGridView1.IsCurrentCellDirty And DataGridView1.CurrentCell.OwningColumn.Name = "Interpretation" Then
            DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
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

        Dim idColumnIndex As Integer
        Dim id As Integer

        'determine column index of "Id" column
        For Each column As DataGridViewColumn In Me.DataGridView1.Columns
            If column.Name = "Id" Then
                idColumnIndex = column.Index
                Exit For
            End If
        Next

        'get id of changed series from Datagridview
        id = DataGridView1.Rows(e.RowIndex).Cells(idColumnIndex).Value

        RaiseEvent PropertyChanged(id)

    End Sub

    ''' <summary>
    ''' Handles the user deleting a row/series
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        Dim title As String = CType(e.Row.DataBoundItem, TimeSeries).Title
        Dim id As Integer = CType(e.Row.DataBoundItem, TimeSeries).Id
        If MsgBox("Delete series " & title & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            RaiseEvent SeriesDeleted(id)
        End If
        'cancel row deletion because datagridview will be refreshed from outside
        'otherwise, two rows end up being deleted
        e.Cancel = True
    End Sub

    Private Sub PropertiesDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'prevent the form from closing and hide it instead
        e.Cancel = True
        Call Me.Hide()
    End Sub

End Class
