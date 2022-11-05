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
    Private Sub SeriesSelection_Changed(sender As System.Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox_AvailableSeries.ItemCheck

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
    Private Sub Button_Up_Click(sender As System.Object, e As System.EventArgs) Handles Button_Up.Click

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
    Private Sub Button_Down_Click(sender As System.Object, e As System.EventArgs) Handles Button_Down.Click

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
    Private Sub Button_SelectAll_Click(sender As System.Object, e As System.EventArgs) Handles Button_SelectAll.Click
        For i As Integer = 0 To Me.CheckedListBox_AvailableSeries.Items.Count - 1
            Me.CheckedListBox_AvailableSeries.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click
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

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
