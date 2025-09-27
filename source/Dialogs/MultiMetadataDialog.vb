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

Friend Class MultiMetadataDialog

    ''' <summary>
    ''' Instantiates a new MultiMetadataDialog
    ''' </summary>
    ''' <param name="tsList">List of Timeseries whose metadata to display</param>
    ''' <param name="visibleKeys">List of metadata keys to display (others will be lost)</param>
    Public Sub New(tsList As List(Of TimeSeries), visibleKeys As List(Of String))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim rowIndex As Integer

        'add columns to DataGridView
        For Each key As String In visibleKeys
            Me.DataGridView1.Columns.Add(key, key)
        Next

        'add time series' metadata values to DataGridView
        For Each ts As TimeSeries In tsList
            Dim cellvalues As New List(Of String) From {ts.Title}
            For Each key As String In ts.Metadata.Keys
                If visibleKeys.Contains(key) Then
                    cellvalues.Add(ts.Metadata(key))
                End If
            Next
            rowIndex = Me.DataGridView1.Rows.Add(cellvalues.ToArray())
        Next

    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            PasteClipBoard()
            e.Handled = True
        End If
    End Sub

    Private Sub PasteClipBoard()
        Dim grid As DataGridView = Me.DataGridView1
        Dim rowSplitter As Char() = {vbCr, vbLf}
        Dim columnSplitter As Char() = {vbTab}
        Dim dataInClipboard As IDataObject = Clipboard.GetDataObject()
        Dim stringInClipboard As String = CStr(dataInClipboard.GetData(DataFormats.Text))
        Dim rowsInClipboard As String() = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries)

        If grid.RowCount <> rowsInClipboard.Length Then
            MsgBox($"Number of rows in clipboard ({rowsInClipboard.Length}) does not match number of rows in grid ({grid.Rows.Count}). Paste cancelled.", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim clipboardColumnCount As Integer = rowsInClipboard(0).Split(columnSplitter).Length
        If grid.ColumnCount <> clipboardColumnCount Then
            MsgBox($"Number of columns in clipboard ({clipboardColumnCount}) does not match number of columns in grid ({grid.ColumnCount}). Paste cancelled.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        For iRow As Integer = 0 To rowsInClipboard.Length - 1
            Dim valuesInRow As String() = rowsInClipboard(iRow).Split(columnSplitter)
            'check series titles match in first column
            If valuesInRow(0) <> grid.Rows(iRow).Cells(0).Value Then
                MsgBox($"Series title in row {iRow + 1} in clipboard ('{valuesInRow(0)}') does not match title in grid ('{grid.Rows(iRow).Cells(0).Value}'). Paste cancelled.", MsgBoxStyle.Critical)
                Exit Sub
            End If
            'paste values in other columns
            For iCol As Integer = 1 To valuesInRow.Length - 1
                Dim cell As DataGridViewCell = grid.Rows(iRow).Cells(iCol)
                cell.Value = valuesInRow(iCol)
            Next
        Next
    End Sub


    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Friend Function getMetadata(title As String) As Metadata
        Dim md As New Metadata()
        Dim row As DataGridViewRow = Me.DataGridView1.Rows.Cast(Of DataGridViewRow)().Where(Function(r) r.Cells(0).Value = title).FirstOrDefault()
        If row IsNot Nothing Then
            For Each cell As DataGridViewCell In row.Cells
                'skip first column with series titles
                If cell.ColumnIndex > 0 Then
                    md.Add(Me.DataGridView1.Columns(cell.ColumnIndex).Name, cell.Value)
                End If
            Next
        End If
        Return md

    End Function

End Class
