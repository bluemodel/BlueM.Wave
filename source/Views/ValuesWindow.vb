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

    Private isInitializing As Boolean
    Private tsList As List(Of TimeSeries)
    Private dataset As DataSet
    Private dataview As DataView
    Private databinding As BindingSource

    Private _controller As ValuesController

    Public Sub SetController(controller As Controller) Implements IView.SetController
        _controller = controller
    End Sub

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
    ''' The maximum number of rows to display in the datagridview at a time
    ''' </summary>
    Private Const maxRows As Integer = 100

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
        'Me.DataGridView1.DataBindings(0).DataSourceUpdateMode = DataSourceUpdateMode.Never
        'Me.DataGridView1.DataBindings.Add(New Binding(binding)

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
            Call Me.loadDataTable()
        End If

    End Sub

    ''' <summary>
    ''' Loads records when the form becomes visible
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimeSeriesValuesDialog_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible Then

            'load data in the datatable
            Call Me.loadDataTable()

            'load first rows
            startIndex = 0
            populateRows()
        End If
    End Sub

    ''' <summary>
    ''' Loads the current time series list into the DataTable
    ''' </summary>
    Private Sub loadDataTable()

        Dim table As DataTable = Me.dataset.Tables("data")

        Me.DataGridView1.SuspendLayout()
        Me.databinding.SuspendBinding()
        Me.dataview.RowStateFilter = DataViewRowState.None
        Me.dataview.RowFilter = String.Empty

        table.Clear()
        table.Columns.Clear()
        table.Columns.Add("index", GetType(Long))
        table.Columns.Add("Timestamp", GetType(DateTime))
        Dim nColumns As Integer = Me.tsList.Count + nHeaderColumns
        For Each ts As TimeSeries In Me.tsList
            table.Columns.Add(ts.Title, GetType(Double))
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
        table.BeginLoadData()
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

            table.Rows.Add(cellvalues)

            index += 1
        Next
        table.EndLoadData()

        Me.dataview.RowFilter = "index >= 0 and index < 100"
        Me.dataview.RowStateFilter = DataViewRowState.CurrentRows
        Me.databinding.ResumeBinding()
        Me.DataGridView1.ResumeLayout()

        'set max startIndex
        NumericUpDown_StartRecord.Maximum = Me.dataset.Tables("data").Rows.Count

        'set first date as initial value for jump date
        If Me.dataset.Tables("data").Rows.Count > 0 Then
            Dim firstDate As DateTime = Me.dataset.Tables("data").Rows(0)(1)
            MaskedTextBox_JumpDate.Text = firstDate.ToString()
        End If

        Me.DataGridView1.Columns(colIndex).Visible = False
        Me.DataGridView1.Columns(colIndex).Frozen = True
        Me.DataGridView1.Columns(colDateTime).Frozen = True

    End Sub

    ''' <summary>
    ''' Populates the datagridview with data starting from the currently set startIndex
    ''' </summary>
    Private Sub populateRows()

        Me.Cursor = Cursors.WaitCursor
        Me.DataGridView1.SuspendLayout()

        Dim numRows, endIndex As Integer
        Dim table As DataTable = Me.dataset.Tables("data")

        numRows = Math.Min(maxRows, table.Rows.Count - startIndex)
        endIndex = startIndex + numRows

        'update filter
        dataview.RowFilter = $"index >= {startIndex} and index < {endIndex}"

        'Update label
        Dim startRecord As Integer
        If table.Rows.Count = 0 Then
            startRecord = 0
        Else
            startRecord = startIndex + 1
        End If
        Me.Label_DisplayCount.Text = $"Displaying records {startRecord} to {startRecord + numRows - 1} of {table.Rows.Count}"

        Me.DataGridView1.ResumeLayout()
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' Shows the first page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_first_Click(sender As Object, e As EventArgs) Handles Button_first.Click
        startIndex = 0
    End Sub

    ''' <summary>
    ''' Shows the previous page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_previous_Click(sender As Object, e As EventArgs) Handles Button_previous.Click
        startIndex = Math.Max(0, startIndex - maxRows)
    End Sub

    ''' <summary>
    ''' Shows the next page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_next_Click(sender As Object, e As EventArgs) Handles Button_next.Click
        startIndex = Math.Min(Me.dataset.Tables("data").Rows.Count - 1, startIndex + maxRows)
    End Sub

    ''' <summary>
    ''' Shows the last page of data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_last_Click(sender As Object, e As EventArgs) Handles Button_last.Click
        startIndex = Math.Max(0, Me.dataset.Tables("data").Rows.Count - maxRows)
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
    Private Sub MaskedTextBox_JumpDate_ValueChanged(sender As Object, e As EventArgs) Handles MaskedTextBox_JumpDate.Validated, Button_Jump.Click
        Me.Cursor = Cursors.WaitCursor
        Dim selectedDate As DateTime = CType(Me.MaskedTextBox_JumpDate.Text, DateTime)
        Dim table As DataTable = Me.dataset.Tables("data")
        'use last record as default (will be used if the selected date is later than the last date of dataset)
        startIndex = table.Rows.Count - 1
        'search for selected date in dataset and set startIndex accordingly
        Dim rowIndex As Integer = 0
        For Each row As DataRow In table.Rows
            If row.ItemArray(colDateTime) = selectedDate Then
                startIndex = rowIndex
                Exit For
            ElseIf row.ItemArray(colDateTime) > selectedDate Then
                startIndex = rowIndex - 1
                Exit For
            End If
            rowIndex += 1
        Next
        'populate datagridview
        populateRows()
        Me.Cursor = Cursors.Default
    End Sub

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
