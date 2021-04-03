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

Friend Class TimeSeriesValuesDialog

    Private isInitializing As Boolean
    Private dataset As DataSet

    ''' <summary>
    ''' The starting index of records to display in the datagridview
    ''' </summary>
    ''' <returns></returns>
    Private Property startIndex As Integer
        Get
            Return NumericUpDown_StartRecord.Value - 1
        End Get
        Set(value As Integer)
            NumericUpDown_StartRecord.Value = value + 1
        End Set
    End Property

    ''' <summary>
    ''' The maximum number of rows to display in the datagridview at a time
    ''' </summary>
    Private Const maxRows As Integer = 100

    Public Event Button_ExportValues_Clicked(sender As Object, e As EventArgs)
    Public Event SelectedRowsChanged(timestamps As List(Of DateTime))

    Public Sub New()

        Me.isInitializing = True
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.isInitializing = False

        Me.dataset = New DataSet()
        Dim table As New DataTable("data")
        Me.dataset.Tables.Add(table)

        'Adjust the column widths based on the displayed values.
        Me.DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

    End Sub

    ''' <summary>
    ''' Updates the form with a new List of TimeSeries
    ''' </summary>
    ''' <param name="seriesList">the new List of TimeSeries</param>
    Public Overloads Sub Update(ByRef seriesList As List(Of TimeSeries))

        Dim table As DataTable = Me.dataset.Tables("data")

        table.Clear()
        table.Columns.Clear()
        table.Columns.Add("Timestamp", GetType(DateTime))
        For Each ts As TimeSeries In seriesList
            table.Columns.Add(ts.Title, GetType(Double))
        Next

        'collect unique timestamps
        Dim unique_timestamps As New HashSet(Of DateTime)
        For Each ts As TimeSeries In seriesList
            unique_timestamps.UnionWith(New HashSet(Of DateTime)(ts.Dates))
        Next
        'sort timestamps
        Dim timestamps As List(Of DateTime) = unique_timestamps.ToList()
        timestamps.Sort()

        'add a row for each timestamp
        table.BeginLoadData()
        Dim cellvalues() As Object
        For Each t As DateTime In timestamps

            ReDim cellvalues(seriesList.Count)

            'first value is timestamp
            cellvalues(0) = t

            'add a value for each series
            Dim icol As Integer = 1
            For Each ts As TimeSeries In seriesList
                If ts.Dates.Contains(t) Then
                    cellvalues(icol) = ts.Nodes(t)
                Else
                    cellvalues(icol) = Nothing
                End If
                icol += 1
            Next

            table.Rows.Add(cellvalues)
        Next
        table.EndLoadData()

        'Reset DatagridView
        Me.DataGridView1.Rows.Clear()
        Me.DataGridView1.Columns.Clear()
        Dim colindex As Integer = 0
        For Each col As DataColumn In table.Columns
            Me.DataGridView1.Columns.Add(col.ColumnName, col.ColumnName)
            If colindex = 0 Then
                'freeze timestamp column
                Me.DataGridView1.Columns(colindex).Frozen = True
            End If
            colindex += 1
        Next

        'set max startIndex
        NumericUpDown_StartRecord.Maximum = table.Rows.Count

        'set first date as initial value for DateTimePicker
        DateTimePicker_JumpDate.Value = timestamps.First

        If Me.Visible Then
            populateRows()
        End If

    End Sub

    ''' <summary>
    ''' Loads records when the form becomes visible
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimeSeriesValuesDialog_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible() Then
            'load first rows
            startIndex = 0
            populateRows()
        End If
    End Sub

    ''' <summary>
    ''' Populates the datagridview with data starting from the currently set startIndex
    ''' </summary>
    Private Sub populateRows()

        Dim rows() As DataGridViewRow
        Dim j, numRows As Integer
        Dim table As DataTable = Me.dataset.Tables("data")

        numRows = Math.Min(maxRows, table.Rows.Count - startIndex)

        'Add new rows to datagridview
        Me.DataGridView1.SuspendLayout()
        Me.DataGridView1.Rows.Clear()
        If numRows > 0 Then
            ReDim rows(numRows - 1)
            For i As Integer = 0 To numRows - 1
                j = startIndex + i
                rows(i) = New DataGridViewRow()
                rows(i).CreateCells(DataGridView1, table.Rows(j).ItemArray)
            Next
            Me.DataGridView1.Rows.AddRange(rows)
        End If
        Me.DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        Me.DataGridView1.ResumeLayout()

        'Update label
        Me.Label_DisplayCount.Text = String.Format("Displaying records {0} to {1} of {2}", startIndex + 1, startIndex + numRows, table.Rows.Count)

    End Sub

    ''' <summary>
    ''' Shows the first page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_first_Click(sender As Object, e As EventArgs) Handles Button_first.Click
        startIndex = 0
        populateRows()
    End Sub

    ''' <summary>
    ''' Shows the previous page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_previous_Click(sender As Object, e As EventArgs) Handles Button_previous.Click, Button_first.Click
        startIndex = Math.Max(0, startIndex - maxRows)
        populateRows()
    End Sub

    ''' <summary>
    ''' Shows the next page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_next_Click(sender As Object, e As EventArgs) Handles Button_next.Click
        startIndex = Math.Min(Me.dataset.Tables("data").Rows.Count - 1, startIndex + maxRows)
        populateRows()
    End Sub

    ''' <summary>
    ''' Shows the last page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_last_Click(sender As Object, e As EventArgs) Handles Button_last.Click
        startIndex = Math.Max(0, Me.dataset.Tables("data").Rows.Count - maxRows)
        populateRows()
    End Sub

    ''' <summary>
    ''' Updates the datagridview after the starting index for showing data was changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub NumericUpDown_StartIndex_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_StartRecord.ValueChanged
        If Not Me.isInitializing Then
            populateRows()
        End If
    End Sub

    Private Sub ToolStripButton_ExportValues_Click(sender As Object, e As EventArgs) Handles ToolStripButton_ExportValues.Click
        RaiseEvent Button_ExportValues_Clicked(sender, e)
    End Sub

    ''' <summary>
    ''' Handles selection of datagridview changed
    ''' If showing markers is activated, selected rows are shown as markers in the main chart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If ToolStripButton_showMarkers.Checked Then
            'collect timestamps of currently selected rows
            Dim selectedRows As DataGridViewSelectedRowCollection = DataGridView1.SelectedRows()
            Dim timestamps As New List(Of DateTime)
            For Each row As DataGridViewRow In selectedRows
                timestamps.Add(row.Cells(0).Value)
            Next
            RaiseEvent SelectedRowsChanged(timestamps)
        End If
    End Sub

    ''' <summary>
    ''' Handles showMarkers button activated/deactivated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_showMarkers_CheckedChanged(sender As Object, e As EventArgs) Handles ToolStripButton_showMarkers.CheckedChanged
        If ToolStripButton_showMarkers.Checked Then
            'show markers for current selection
            DataGridView1_SelectionChanged(sender, e)
        Else
            'clear any existing markers
            Dim timestamps As New List(Of DateTime)
            RaiseEvent SelectedRowsChanged(timestamps)
        End If
    End Sub

    ''' <summary>
    ''' Handles DateTimePicker_JumpDate value changed
    ''' Searches for the selected date in the dataset and sets that as the start record for the datagridview display
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DateTimePicker_JumpDate_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker_JumpDate.ValueChanged
        Dim selectedDate As DateTime = DateTimePicker_JumpDate.Value
        Dim table As DataTable = Me.dataset.Tables("data")
        'use last record as default (will be used if the selected date is later than the last date of dataset)
        startIndex = table.Rows.Count - 1
        'search for selected date in dataset and set startIndex accordingly
        Dim rowIndex As Integer = 0
        For Each row As DataRow In table.Rows
            If row.ItemArray(0) = selectedDate Then
                startIndex = rowIndex
                Exit For
            ElseIf row.ItemArray(0) > selectedDate Then
                startIndex = rowIndex - 1
                Exit For
            End If
            rowIndex += 1
        Next
        'populate datagridview
        populateRows()
    End Sub

    Private Sub TimeSeriesValuesDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'prevent the form from closing and hide it instead
        e.Cancel = True
        Call Me.Hide()
    End Sub

End Class
