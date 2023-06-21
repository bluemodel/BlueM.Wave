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
        Me.ComboBox_Analysis.DataSource = System.Enum.GetValues(GetType(AnalysisFactory.AnalysisFunctions))

        'Populate listbox with time series
        For Each series As TimeSeries In seriesList
            Me.ListBox_Series.Items.Add(series)
        Next

    End Sub

    ''' <summary>
    ''' Selected analysis function
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property selectedAnalysisFunction() As AnalysisFactory.AnalysisFunctions
        Get
            Return Me.ComboBox_Analysis.SelectedItem
        End Get
    End Property

    ''' <summary>
    ''' List of selected time series
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property selectedTimeseries() As List(Of TimeSeries)
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
        Me.Label_AnalaysisDescription.Text = AnalysisFactory.getAnalysisDescription(Me.selectedAnalysisFunction)
        url = "https://wiki.bluemodel.org/index.php/Wave:" & Me.selectedAnalysisFunction.ToString("g")
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
        System.Diagnostics.Process.Start(e.Link.LinkData)
    End Sub

    ''' <summary>
    ''' OK button pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_OK_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click
        'Eingabekontrolle
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MsgBox("Please select at least one series!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub

End Class