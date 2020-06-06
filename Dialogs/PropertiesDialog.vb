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

Friend Class PropertiesDialog

    Private isInitializing As Boolean

    ''' <summary>
    ''' Is raised when a property is changed by the user
    ''' </summary>
    ''' <param name="ts_title">Title of the time series whose properties were changed</param>
    Friend Event PropertyChanged(ts_title As String)

    Public Sub New(ByRef series As Dictionary(Of String, TimeSeries))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Me.isInitializing = True

        Me.Interpretation.DataSource = System.Enum.GetValues(GetType(TimeSeries.InterpretationEnum))

        'add the time series to the binding source
        Me.TimeSeriesBindingSource.Clear()
        For Each ts As TimeSeries In series.Values
            Me.TimeSeriesBindingSource.Add(ts)
        Next

        Me.isInitializing = False
    End Sub

    ''' <summary>
    ''' Commit edits as soon as they occur
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        If DataGridView1.IsCurrentCellDirty Then
            DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    ''' <summary>
    ''' Handles changed cell values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        If Me.isInitializing Or e.RowIndex = -1 Then
            Exit Sub
        End If

        Dim titleColumnIndex As Integer
        Dim ts_title As String

        'determine column index of "Title" column
        For Each column As DataGridViewColumn In Me.DataGridView1.Columns
            If column.Name = "Title" Then
                titleColumnIndex = column.Index
                Exit For
            End If
        Next

        'get title of changed series from Datagridview
        ts_title = DataGridView1.Rows(e.RowIndex).Cells(titleColumnIndex).Value

        RaiseEvent PropertyChanged(ts_title)

    End Sub
End Class
