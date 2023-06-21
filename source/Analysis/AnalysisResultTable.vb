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
''' <summary>
''' Window for displaying an analysis result table
''' </summary>
Friend Class AnalysisResultTable

    ''' <summary>
    ''' The DataTable to display
    ''' </summary>
    Private Data As DataTable

    ''' <summary>
    ''' Instantiates a new AnalysisResultTable window
    ''' </summary>
    ''' <param name="data">the DataTable to display</param>
    Public Sub New(data As DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Data = data

    End Sub

    Private Sub AnalysisResultTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.GroupBox1.Text = Me.Data.TableName
        Me.DataGridView1.AutoGenerateColumns = True
        Me.DataGridView1.DataSource = Me.Data
        'freeze the first column
        Me.DataGridView1.Columns(0).Frozen = True
    End Sub

    Private Sub CopyToolStripButton_Click(sender As Object, e As EventArgs) Handles CopyToolStripButton.Click
        Try
            Me.DataGridView1.SelectAll()
            Clipboard.SetDataObject(Me.DataGridView1.GetClipboardContent())

        Catch ex As System.Runtime.InteropServices.ExternalException
            MsgBox("The Clipboard could not be accessed. Please try again.", MsgBoxStyle.Exclamation)
        End Try

    End Sub
End Class