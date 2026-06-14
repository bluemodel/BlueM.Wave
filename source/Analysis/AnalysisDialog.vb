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
''' Dialog for selecting an analysis function and time series
''' </summary>
Friend Class AnalysisDialog

    Public Sub New(seriesList As List(Of TimeSeries))

        Call InitializeComponent()

        'Populate combobox with analysis functions
        Me.ComboBox_Analysis.DataSource = [Enum].GetValues(Of AnalysisFactory.AnalysisFunctions)()

        'Populate listbox with time series
        Me.ListBox_Series.Items.AddRange(seriesList.ToArray())

    End Sub

    ''' <summary>
    ''' Selected analysis function
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property SelectedAnalysisFunction As AnalysisFactory.AnalysisFunctions
        Get
            Return Me.ComboBox_Analysis.SelectedItem
        End Get
    End Property

    ''' <summary>
    ''' List of selected time series
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property SelectedTimeseries As List(Of TimeSeries)
        Get
            Dim seriesList As New List(Of TimeSeries)()
            For Each item As Object In Me.ListBox_Series.SelectedItems
                seriesList.Add(CType(item, TimeSeries).Clone())
            Next
            Return seriesList
        End Get
    End Property

    ''' <summary>
    ''' Selected analysis function changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ComboBox_Analysis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Analysis.SelectedIndexChanged
        Dim url As String
        'update the description and wiki link
        Me.Label_AnalaysisDescription.Text = AnalysisFactory.GetAnalysisDescription(Me.SelectedAnalysisFunction)
        url = "https://wiki.bluemodel.org/index.php/Wave:" & Me.SelectedAnalysisFunction.ToString("g")
        Me.LinkLabel_Helplink.Text = url
        Me.LinkLabel_Helplink.Links.Clear()
        Me.LinkLabel_Helplink.Links.Add(0, Me.LinkLabel_Helplink.Text.Length, url)
    End Sub

    ''' <summary>
    ''' Link clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LinkLabel_Helplink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Helplink.LinkClicked
        Helpers.OpenUrl(e.Link.LinkData)
    End Sub

    ''' <summary>
    ''' OK button pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_OK_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click
        'Eingabekontrolle
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MessageBox.Show("Please select at least one series!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.DialogResult = DialogResult.None
        End If
    End Sub

End Class