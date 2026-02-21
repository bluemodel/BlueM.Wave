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

''' <summary>
''' Dialog for cutting time series
''' </summary>
''' <remarks></remarks>
Friend Class CutDialog

    Private IsInitializing As Boolean
    Private earliestStart, latestEnd As DateTime
    Private cutStart, cutEnd As DateTime

    ''' <summary>
    ''' List of cut time series resulting from the dialog. Each selected time series is cut to the specified start and end and added to this list.
    ''' </summary>
    Friend CutSeries As List(Of TimeSeries)

    ''' <summary>
    ''' Whether to keep uncut versions of the time series in the project. If false, the cut time series will replace the original time series in the project. If true, the cut time series will be added to the project and the original time series will be kept unchanged.
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property KeepUncutSeries As Boolean
        Get
            Return Me.CheckBox_keepUncutSeries.Checked
        End Get
    End Property

    ''' <summary>
    ''' List of time series selected to be cut. These are the time series that are selected in the listbox in the dialog. The cut time series resulting from the dialog are stored in the CutSeries property.
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property SelectedSeries As List(Of TimeSeries)
        Get
            Dim tsList As New List(Of TimeSeries)
            For Each ts As TimeSeries In Me.ListBox_Series.SelectedItems
                tsList.Add(ts)
            Next
            Return tsList
        End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="tsList">List of time series to be displayed in the dialog</param>
    ''' <param name="initialStart">Initial start date for the cut</param>
    ''' <param name="initialEnd">Initial end date for the cut</param>
    Public Sub New(ByRef tsList As List(Of TimeSeries), initialStart As DateTime, initialEnd As DateTime)

        Me.IsInitializing = True

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.CutSeries = New List(Of TimeSeries)

        Me.cutStart = initialStart
        Me.cutEnd = initialEnd

        'set CurrentCulture for MaskedTextBoxes
        Me.MaskedTextBox_cutStart.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_cutStart.FormatProvider = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_cutEnd.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_cutEnd.FormatProvider = Globalization.CultureInfo.CurrentCulture

        'populate controls
        For Each ts As TimeSeries In tsList
            Me.ListBox_Series.Items.Add(ts)
            Me.ComboBox_RefSeries.Items.Add(ts)
        Next

        'Update MaskedTextboxes
        Call Me.UpdateCutExtentControls()

        Me.IsInitializing = False

        If Me.ListBox_Series.Items.Count = 1 Then
            'if only one time series is available, select it by default
            Me.ListBox_Series.SetSelected(0, True)
        End If

    End Sub

    ''' <summary>
    ''' Handles selection of time series to be cut
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ListBox_Series_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox_Series.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.earliestStart = DateTime.MaxValue
        Me.latestEnd = DateTime.MinValue

        'determine earliest start and latest end of selected time series
        For Each ts As TimeSeries In Me.ListBox_Series.SelectedItems
            If (ts.StartDate < Me.earliestStart) Then Me.earliestStart = ts.StartDate
            If (ts.EndDate > Me.latestEnd) Then Me.latestEnd = ts.EndDate
        Next

        'update min and max labels
        Me.Label_MinDateTime.Text = "Min: " & Me.earliestStart.ToString(Helpers.CurrentDateFormat)
        Me.Label_MaxDateTime.Text = "Max: " & Me.latestEnd.ToString(Helpers.CurrentDateFormat)

        'update controls
        Call Me.UpdateCutExtentControls()

    End Sub

    ''' <summary>
    ''' Handles KeyDown in start and end date textboxes
    ''' Resets the color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBoxKeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MaskedTextBox_cutStart.KeyDown, MaskedTextBox_cutEnd.KeyDown
        CType(sender, MaskedTextBox).ForeColor = DefaultForeColor
    End Sub

    ''' <summary>
    ''' Handles ValidationCompleted of start and end dates
    ''' Checks whether input is valid DateTime
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBoxValidationCompleted(sender As System.Object, e As TypeValidationEventArgs) Handles MaskedTextBox_cutStart.TypeValidationCompleted, MaskedTextBox_cutEnd.TypeValidationCompleted
        If Not e.IsValidInput Then
            e.Cancel = True
            CType(sender, MaskedTextBox).ForeColor = Color.Red
        End If
    End Sub

    ''' <summary>
    ''' Handles cut start date changed in form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBox_cutStart_Validated(sender As System.Object, e As System.EventArgs) Handles MaskedTextBox_cutStart.Validated

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.cutStart = Me.MaskedTextBox_cutStart.Text
        Me.ComboBox_RefSeries.SelectedIndex = -1 'reset reference series selection, as the cut extent may no longer match the reference series

    End Sub

    ''' <summary>
    ''' Handles cut end date changed in form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBox_cutEnd_Validated(sender As System.Object, e As System.EventArgs) Handles MaskedTextBox_cutEnd.Validated

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.cutEnd = Me.MaskedTextBox_cutEnd.Text
        Me.ComboBox_RefSeries.SelectedIndex = -1 'reset reference series selection, as the cut extent may no longer match the reference series

    End Sub

    ''' <summary>
    ''' Updates the MaskedTextBoxes displaying the currently set cut start and end dates
    ''' </summary>
    Private Sub UpdateCutExtentControls()

        Me.IsInitializing = True 'avoid chain reaction of events

        Me.MaskedTextBox_cutStart.Text = Me.cutStart
        Me.MaskedTextBox_cutEnd.Text = Me.cutEnd

        Me.IsInitializing = False

    End Sub

    ''' <summary>
    ''' Handles selection of a reference time series for cutting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ComboBox_RefSeries_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox_RefSeries.SelectedIndexChanged

        If Me.IsInitializing Or ComboBox_RefSeries.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim tsRef As TimeSeries = Me.ComboBox_RefSeries.SelectedItem

        'check whether selected time series overlap with reference time series
        If tsRef.EndDate < Me.earliestStart Or tsRef.StartDate > Me.latestEnd Then
            MsgBox("The selected series do not overlap!", MsgBoxStyle.Exclamation)
            'cancel
            Exit Sub
        End If

        'set new start and end
        Me.cutStart = tsRef.StartDate
        Me.cutEnd = tsRef.EndDate

        'update controls
        Call Me.UpdateCutExtentControls()

    End Sub

    ''' <summary>
    ''' Select all button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_SelectAll_Click(sender As Object, e As EventArgs) Handles Button_SelectAll.Click
        Me.IsInitializing = True
        Me.ListBox_Series.BeginUpdate()
        For i As Integer = 0 To Me.ListBox_Series.Items.Count - 1
            Me.ListBox_Series.SetSelected(i, True)
        Next
        Me.ListBox_Series.EndUpdate()
        Me.IsInitializing = False
        Call ListBox_Series_SelectedIndexChanged(ListBox_Series, New EventArgs())
    End Sub

    ''' <summary>
    ''' OK button clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click

        'validation
        If Me.ListBox_Series.SelectedIndices.Count = 0 Then
            MsgBox("Please select at least one time series to be cut!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        If Me.cutStart >= Me.cutEnd Then
            MsgBox("The end must be later than the start!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        'cut selected time series
        For Each ts As TimeSeries In Me.ListBox_Series.SelectedItems
            Dim ts_cut As TimeSeries = ts.Clone()
            Call ts_cut.Cut(Me.cutStart, Me.cutEnd)
            Me.CutSeries.Add(ts_cut)
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Button_Cancel_Click(sender As System.Object, e As System.EventArgs) Handles Button_Cancel.Click
        Call Me.Close()
    End Sub

End Class
