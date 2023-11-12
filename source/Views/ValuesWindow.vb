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
Friend Class ValuesWindow
    Implements IView

    Const colIndex As Integer = 0
    Const colDateTime As Integer = 1
    Const nHeaderColumns As Integer = 2

    ''' <summary>
    ''' The maximum number of rows to display in the datagridview at a time
    ''' </summary>
    Private Const maxRows As Integer = 100

    Private isInitializing As Boolean
    Private tsList As List(Of TimeSeries)
    Private dataset As DataSet
    Private dataview As DataView
    Private databinding As BindingSource
    Private IsJumpDateSet As Boolean = False

    Private _controller As ValuesController

    Public Sub SetController(controller As Controller) Implements IView.SetController
        _controller = controller
    End Sub

    ''' <summary>
    ''' The datatable containing all the data
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property dataTable As DataTable
        Get
            Return Me.dataset.Tables("data")
        End Get
    End Property

    ''' <summary>
    ''' The number of rows currently contained in the datatable
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property nRows As Integer
        Get
            Return Me.dataTable.Rows.Count
        End Get
    End Property

    ''' <summary>
    ''' The starting index of records to display in the datagridview
    ''' </summary>
    ''' <returns></returns>
    Private Property startIndex As Integer
        Get
            Return Math.Max(NumericUpDown_StartRecord.Value - 1, 0)
        End Get
        Set(value As Integer)
            value += 1 'convert from index to record number
            value = Math.Min(value, NumericUpDown_StartRecord.Maximum)
            value = Math.Max(value, NumericUpDown_StartRecord.Minimum)
            NumericUpDown_StartRecord.Value = value
        End Set
    End Property

    ''' <summary>
    ''' Is raised when the selected rows changes
    ''' </summary>
    ''' <param name="timestamps">List of selected timestamps</param>
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

        'create the dataview and bind it to the DataGridView
        Me.dataview = New DataView(table)
        Me.databinding = New BindingSource With {
            .DataSource = Me.dataview
        }
        Me.DataGridView1.DataSource = Me.databinding

        'set CurrentCulture for MaskedTextBox
        Me.MaskedTextBox_JumpDate.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_JumpDate.FormatProvider = Globalization.CultureInfo.CurrentCulture

        'Adjust the column widths based on the displayed values.
        Me.DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

    End Sub

    ''' <summary>
    ''' Updates the form with a new List of TimeSeries
    ''' </summary>
    ''' <param name="seriesList">the new List of TimeSeries</param>
    Public Overloads Sub Update(ByRef seriesList As List(Of TimeSeries))

        Me.tsList = seriesList

        If Me.Visible Then
            Call Me.UpdateDataTable()
        End If

    End Sub

    ''' <summary>
    ''' Loads records when the form becomes visible
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimeSeriesValuesDialog_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible Then
            'update the datatable
            Call Me.UpdateDataTable()
            'reset start index and jump date
            Me.startIndex = 0
            Me.IsJumpDateSet = False
            If Me.nRows > 0 Then
                Dim firstDate As DateTime = Me.dataTable.Rows(0)(colDateTime)
                MaskedTextBox_JumpDate.Text = firstDate.ToString()
            End If
            Call Me.updateDataViewFilter()
        End If
    End Sub

    ''' <summary>
    ''' Updates the DataTable using the current list of time series
    ''' </summary>
    Private Sub UpdateDataTable()

        Me.DataGridView1.SuspendLayout()
        Me.databinding.SuspendBinding()
        Me.dataview.RowStateFilter = DataViewRowState.None
        Me.dataview.RowFilter = String.Empty

        Me.dataTable.Clear()
        Me.dataTable.Columns.Clear()
        Me.dataTable.Columns.Add("index", GetType(Long))
        Me.dataTable.Columns.Add("Timestamp", GetType(DateTime))
        Dim nColumns As Integer = Me.tsList.Count + nHeaderColumns
        For Each ts As TimeSeries In Me.tsList
            Me.dataTable.Columns.Add(ts.Title, GetType(Double))
        Next

        'collect unique timestamps
        Dim unique_timestamps As New HashSet(Of DateTime)
        For Each ts As TimeSeries In Me.tsList
            unique_timestamps.UnionWith(New HashSet(Of DateTime)(ts.Dates))
        Next

        'sort timestamps
        Dim timestamps As List(Of DateTime) = unique_timestamps.ToList()
        timestamps.Sort()

        'add a row for each timestamp
        Me.dataTable.BeginLoadData()
        Dim index As Long = 0
        Dim cellvalues() As Object
        For Each t As DateTime In timestamps

            ReDim cellvalues(nColumns - 1)

            'index column
            cellvalues(colIndex) = index

            'timestamp column
            cellvalues(colDateTime) = t

            'add a value for each series
            Dim icol As Integer = nHeaderColumns
            For Each ts As TimeSeries In Me.tsList
                If ts.Dates.Contains(t) Then
                    cellvalues(icol) = ts.Nodes(t)
                Else
                    cellvalues(icol) = Nothing
                End If
                icol += 1
            Next

            Me.dataTable.Rows.Add(cellvalues)

            index += 1
        Next
        Me.dataTable.EndLoadData()

        Me.dataview.RowFilter = $"index >= 0 and index < {maxRows}"
        Me.dataview.RowStateFilter = DataViewRowState.CurrentRows
        Me.databinding.ResumeBinding()
        Me.DataGridView1.ResumeLayout()

        'set max startIndex
        NumericUpDown_StartRecord.Maximum = Me.nRows

        'determine start index
        Dim jumpDate As DateTime
        Dim isDate As Boolean = DateTime.TryParse(Me.MaskedTextBox_JumpDate.Text, jumpDate)
        If isDate And Me.IsJumpDateSet Then
            'set start index to correspond to currently set jump date
            Me.startIndex = Me.getStartIndexForDate(jumpDate)
            Call Me.updateDataViewFilter()
        Else
            'set start index to 0 and jump date to first date
            Me.startIndex = 0
            If Me.nRows > 0 Then
                Dim firstDate As DateTime = Me.dataTable.Rows(0)(colDateTime)
                MaskedTextBox_JumpDate.Text = firstDate.ToString()
            End If
        End If

        Me.DataGridView1.Columns(colIndex).Visible = False
        Me.DataGridView1.Columns(colIndex).Frozen = True
        Me.DataGridView1.Columns(colDateTime).Frozen = True

    End Sub

    ''' <summary>
    ''' Updates the DataViewFilter to show rows starting from the currently set startIndex
    ''' </summary>
    Private Sub updateDataViewFilter()

        Me.Cursor = Cursors.WaitCursor
        Me.DataGridView1.SuspendLayout()

        Dim numRows, endIndex As Integer

        numRows = Math.Min(maxRows, Me.nRows - Me.startIndex)
        endIndex = Me.startIndex + numRows

        'update filter
        dataview.RowFilter = $"index >= {Me.startIndex} and index < {endIndex}"

        'Update label
        Dim startRecord As Integer
        If Me.nRows = 0 Then
            startRecord = 0
        Else
            startRecord = Me.startIndex + 1
        End If
        Me.Label_DisplayCount.Text = $"Displaying records {startRecord} to {startRecord + numRows - 1} of {Me.nRows}"

        Me.DataGridView1.ResumeLayout()
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' Shows the first page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_first_Click(sender As Object, e As EventArgs) Handles Button_first.Click
        Me.startIndex = 0
    End Sub

    ''' <summary>
    ''' Shows the previous page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_previous_Click(sender As Object, e As EventArgs) Handles Button_previous.Click
        Me.startIndex = Math.Max(0, Me.startIndex - maxRows)
    End Sub

    ''' <summary>
    ''' Shows the next page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_next_Click(sender As Object, e As EventArgs) Handles Button_next.Click
        Me.startIndex = Math.Min(Me.nRows - 1, Me.startIndex + maxRows)
    End Sub

    ''' <summary>
    ''' Shows the last page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_last_Click(sender As Object, e As EventArgs) Handles Button_last.Click
        Me.startIndex = Math.Max(0, Me.nRows - maxRows)
    End Sub

    ''' <summary>
    ''' Updates the datagridview after the starting index for showing data was changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub NumericUpDown_StartIndex_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_StartRecord.ValueChanged
        If Not Me.isInitializing Then
            Call Me.updateDataViewFilter()
        End If
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
                timestamps.Add(row.Cells(colDateTime).Value)
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
    ''' Handles KeyDown in jump date textbox
    ''' Resets the color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBox_JumpDate_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MaskedTextBox_JumpDate.KeyDown
        CType(sender, MaskedTextBox).ForeColor = DefaultForeColor
        'If e.KeyCode = Keys.Escape Then
        '    'set original date
        'End If
    End Sub

    ''' <summary>
    ''' Handles TypeValidationCompleted of jump date textbox
    ''' Checks whether input is valid DateTime
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBox_JumpDate_TypeValidationCompleted(sender As System.Object, e As TypeValidationEventArgs) Handles MaskedTextBox_JumpDate.TypeValidationCompleted
        If Not e.IsValidInput Then
            e.Cancel = True
            CType(sender, MaskedTextBox).ForeColor = Color.Red
        End If
    End Sub

    ''' <summary>
    ''' Handles jump date value changed
    ''' Searches for the selected date in the dataset and sets that as the start record for the datagridview display
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBox_JumpDate_ValueChanged(sender As Object, e As EventArgs) Handles Button_Jump.Click
        Me.Cursor = Cursors.WaitCursor
        Dim jumpDate As DateTime = CType(Me.MaskedTextBox_JumpDate.Text, DateTime)
        Me.IsJumpDateSet = True
        'update data view filter
        Me.startIndex = Me.getStartIndexForDate(jumpDate)
        Call Me.updateDataViewFilter()
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Returns the start index corresponding to the given date
    ''' </summary>
    ''' <param name="timestamp">Date</param>
    ''' <returns>Index</returns>
    Private Function getStartIndexForDate(timestamp As DateTime) As Integer
        Dim index As Integer
        'use last record as default (will be used if the timestamp is later than the last timestamp of the dataset)
        index = Me.nRows - 1
        'search for selected date in dataset and set startIndex accordingly
        Dim rowIndex As Integer = 0
        For Each row As DataRow In Me.dataTable.Rows
            If row.ItemArray(colDateTime) = timestamp Then
                index = rowIndex
                Exit For
            ElseIf row.ItemArray(colDateTime) > timestamp Then
                index = rowIndex - 1
                Exit For
            End If
            rowIndex += 1
        Next
        Return index
    End Function

    Private Sub TimeSeriesValuesDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'stop highlighting
        RaiseEvent SelectedRowsChanged(New List(Of DateTime))
        'prevent the form from closing and hide it instead
        e.Cancel = True
        Call Me.Hide()
    End Sub

    Private Overloads Sub Close() Implements IView.Close
        Throw New NotImplementedException()
    End Sub
End Class
