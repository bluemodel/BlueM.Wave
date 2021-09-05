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

Friend Class MergeSeriesDialog

    ''' <summary>
    ''' Returns the list of selected series IDs, ordered by priority (highest first)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property selectedSeries() As List(Of Integer)
        Get
            Dim ids As New List(Of Integer)
            For Each ts As TimeSeries In Me.ListBox_SelectedSeries.Items
                ids.Add(ts.Id)
            Next
            Return ids
        End Get
    End Property

    ''' <summary>
    ''' Returns the title of the merged series
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property mergedSeriesTitle() As String
        Get
            Return Me.TextBox_MergedSeriesTitle.Text.Trim()
        End Get
    End Property

    Public Sub New(ByRef series As List(Of TimeSeries))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'populate list of available series
        For Each ts As TimeSeries In series
            Me.CheckedListBox_AvailableSeries.Items.Add(ts)
        Next

    End Sub

    ''' <summary>
    ''' Series selection changed (checkbox state changed)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SeriesSelection_Changed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox_AvailableSeries.ItemCheck

        Dim ts As TimeSeries

        ts = Me.CheckedListBox_AvailableSeries.Items(e.Index)

        If e.NewValue = CheckState.Checked Then
            'Add series to priorities list box
            Me.ListBox_SelectedSeries.Items.Add(ts)
        Else
            'Remove series from priorities list box
            Me.ListBox_SelectedSeries.Items.Remove(ts)
        End If

    End Sub

    ''' <summary>
    ''' Raise priority button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Up.Click

        Dim index As Integer
        Dim ts As TimeSeries

        index = Me.ListBox_SelectedSeries.SelectedIndex
        ts = Me.ListBox_SelectedSeries.SelectedItem

        If index <> -1 And index > 0 Then
            Me.ListBox_SelectedSeries.Items.RemoveAt(index)
            Me.ListBox_SelectedSeries.Items.Insert(index - 1, ts)

            Me.ListBox_SelectedSeries.SetSelected(index - 1, True)
        End If

    End Sub

    ''' <summary>
    ''' Lower priority button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Down.Click

        Dim index As Integer
        Dim ts As TimeSeries

        index = Me.ListBox_SelectedSeries.SelectedIndex
        ts = Me.ListBox_SelectedSeries.SelectedItem

        If index <> -1 And index < Me.ListBox_SelectedSeries.Items.Count - 1 Then
            Me.ListBox_SelectedSeries.Items.RemoveAt(index)
            Me.ListBox_SelectedSeries.Items.Insert(index + 1, ts)

            Me.ListBox_SelectedSeries.SetSelected(index + 1, True)
        End If

    End Sub

    ''' <summary>
    ''' Select All clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SelectAll.Click
        For i As Integer = 0 To Me.CheckedListBox_AvailableSeries.Items.Count - 1
            Me.CheckedListBox_AvailableSeries.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Check for valid inputs
        If Me.selectedSeries.Count < 2 Then
            MsgBox("Please select at least two series to merge!", MsgBoxStyle.Exclamation)
            Return
        End If
        If Me.mergedSeriesTitle = "" Then
            MsgBox("Please enter a title for the merged series!", MsgBoxStyle.Exclamation)
            Me.TextBox_MergedSeriesTitle.Focus()
            Return
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
