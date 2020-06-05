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
''' <summary>
''' Dialog for selecting an analysis function and time series
''' </summary>
Friend Class AnalysisDialog

    Public Sub New(ByVal seriesDict As Dictionary(Of String, TimeSeries))

        Call InitializeComponent()

        'Populate combobox with analysis functions
        Me.ComboBox_Analysis.DataSource = System.Enum.GetValues(GetType(AnalysisFactory.AnalysisFunctions))

        'Populate listbox with time series
        For Each series As TimeSeries In seriesDict.Values
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
                seriesList.Add(CType(item, TimeSeries))
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
        url = "http://wiki.bluemodel.org/index.php/Wave:" & Me.selectedAnalysisFunction.ToString("g")
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
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click
        'Eingabekontrolle
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MsgBox("Please select at least one series!", MsgBoxStyle.Exclamation, "Wave")
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub

End Class