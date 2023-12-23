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
Friend Class PropertiesWindow
    Implements IView

    Private _controller As PropertiesController

    Public Sub SetController(controller As Controller) Implements IView.SetController
        _controller = controller
    End Sub

    ''' <summary>
    ''' Is raised when a property is changed by the user
    ''' </summary>
    ''' <param name="id">Id of the time series whose properties were changed</param>
    Friend Event SeriesPropertyChanged(id As Integer)

    ''' <summary>
    ''' Is raised when the user deletes rows/series
    ''' </summary>
    ''' <param name="ids">List of ids of the time series that were deleted</param>
    Friend Event SeriesDeleted(ids As List(Of Integer))

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
    ''' Handles data binding complete
    ''' Adds cell error texts for NaN volumes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete

        'Add cell error texts for NaN volume
        Dim colIndex_Volume As Integer
        For Each column As DataGridViewColumn In Me.DataGridView1.Columns
            If column.Name = "Volume" Then
                colIndex_Volume = column.Index
                Exit For
            End If
        Next
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            If Double.IsNaN(row.Cells(colIndex_Volume).Value) Then
                row.Cells(colIndex_Volume).ErrorText = "Interpretation and/or unit do not allow volume calculation!"
            End If
        Next

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
    ''' Handles cell validating events by performing additional checks
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating

        'Check that series titles are unique
        If Me.DataGridView1.Columns(e.ColumnIndex).HeaderCell.Value = "Title" Then
            For Each row As DataGridViewRow In Me.DataGridView1.Rows
                If row.Index = e.RowIndex Then
                    Continue For
                End If
                If e.FormattedValue = row.Cells(e.ColumnIndex).Value Then
                    MsgBox("Title must be unique!", MsgBoxStyle.Critical)
                    e.Cancel = True
                    Return
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

        RaiseEvent SeriesPropertyChanged(id)

    End Sub

    ''' <summary>
    ''' Handles the user deleting rows/series using the keyboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        Call DeleteSeries()
        'cancel row deletion because datagridview will be refreshed from outside
        'otherwise, two rows end up being deleted
        e.Cancel = True
    End Sub

    ''' <summary>
    ''' Handles the user clicking the delete button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_Delete_Click(sender As Object, e As EventArgs) Handles ToolStripButton_Delete.Click
        Call DeleteSeries()
    End Sub

    Private Sub PropertiesDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'prevent the form from closing and hide it instead
        e.Cancel = True
        Call Me.Hide()
    End Sub

    ''' <summary>
    ''' Handles the datagridview selection changed
    ''' Enables/disables buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        'Check if any rows are selected
        If Me.DataGridView1.SelectedRows.Count = 0 Then
            Me.ToolStripButton_Delete.Enabled = False
        Else
            Me.ToolStripButton_Delete.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Delete all selected rows / series
    ''' </summary>
    Private Sub DeleteSeries()
        'collect titles and ids of all selected rows
        Dim titles As New List(Of String)
        Dim ids As New List(Of Integer)
        For Each row As DataGridViewRow In Me.DataGridView1.SelectedRows
            Dim ts As TimeSeries = CType(row.DataBoundItem, TimeSeries)
            If Not IsNothing(ts) Then
                titles.Add(ts.Title)
                ids.Add(ts.Id)
            End If
        Next
        If ids.Count > 0 Then
            'ask for user confirmation
            Dim result As MsgBoxResult = MsgBox($"Delete {ids.Count} series?{eol}{String.Join(eol, titles)}", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation)
            If result = MsgBoxResult.Ok Then
                RaiseEvent SeriesDeleted(ids)
            End If
        End If
    End Sub

    Private Overloads Sub Close() Implements IView.Close
        Throw New NotImplementedException()
    End Sub

End Class
