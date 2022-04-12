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

    Friend analysis As Analysis
    Private isInitializing As Boolean
    Private seriesList As List(Of TimeSeries)

    Public Sub New(seriesList As List(Of TimeSeries))

        Call InitializeComponent()

        Me.isInitializing = True

        'Populate combobox with analysis functions
        Me.ListBox_AnalysisFunctions.DataSource = System.Enum.GetValues(GetType(AnalysisFactory.AnalysisFunctions))
        Me.ListBox_AnalysisFunctions.SelectedIndex = -1

        'store list of time series
        Me.seriesList = seriesList

        Me.isInitializing = False

    End Sub

    ''' <summary>
    ''' Selected analysis function
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property selectedAnalysisFunction() As AnalysisFactory.AnalysisFunctions
        Get

            Return Me.ListBox_AnalysisFunctions.SelectedItem
        End Get
    End Property

    ''' <summary>
    ''' Selected analysis function changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ComboBox_Analysis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox_AnalysisFunctions.SelectedIndexChanged

        If Me.isInitializing Then
            Return
        End If

        Me.analysis = AnalysisFactory.CreateAnalysis(Me.selectedAnalysisFunction)

        Dim url As String
        'update the description and wiki link
        Me.Label_AnalaysisDescription.Text = AnalysisFactory.getAnalysisDescription(Me.selectedAnalysisFunction)
        url = "https://wiki.bluemodel.org/index.php/Wave:" & Me.selectedAnalysisFunction.ToString("g")
        Me.LinkLabel_Helplink.Text = url
        Me.LinkLabel_Helplink.Links.Clear()
        Me.LinkLabel_Helplink.Links.Add(0, Me.LinkLabel_Helplink.Text.Length, url)

        'prepare second wizard page
        Me.Label_AnalysisFunction.Text = Me.selectedAnalysisFunction.ToString("g")
        Me.TableLayoutPanel1.Controls.Clear()
        Me.TableLayoutPanel1.RowCount = analysis.parameters.Count
        Me.TableLayoutPanel1.Visible = True
        Dim row As Integer = 0
        For Each param As AnalysisParameter In analysis.parameters.Values

            'label
            Dim label As New Label()
            label.Text = param.Description & ":"
            label.AutoSize = True
            Me.TableLayoutPanel1.Controls.Add(label)
            Me.TableLayoutPanel1.SetCellPosition(label, New TableLayoutPanelCellPosition(0, row))

            'input control
            Dim control As New Control()
            Select Case param.ParameterType

                Case AnalysisParameter.ParameterTypeEnum.Timeseries
                    Dim combobox As New ComboBox()
                    combobox.DropDownStyle = ComboBoxStyle.DropDownList
                    For Each ts As TimeSeries In Me.seriesList
                        combobox.Items.Add(ts)
                    Next
                    combobox.SelectedIndex = 0
                    param.Value = combobox.SelectedItem
                    combobox.DataBindings.Add(New Binding("SelectedItem", param, "Value"))
                    control = combobox

                Case AnalysisParameter.ParameterTypeEnum.Integer
                    Dim numericupdown As New NumericUpDown()
                    numericupdown.Minimum = param.MinValue
                    numericupdown.Maximum = param.MaxValue
                    param.Value = param.DefaultValue
                    numericupdown.DataBindings.Add(New Binding("Value", param, "Value"))
                    control = numericupdown
            End Select

            Me.TableLayoutPanel1.Controls.Add(control)
            Me.TableLayoutPanel1.SetCellPosition(control, New TableLayoutPanelCellPosition(1, row))

            row += 1
        Next
        'Me.TableLayoutPanel1.Refresh()
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
    Private Sub Button_Next_Click(sender As System.Object, e As System.EventArgs) Handles Button_Next.Click
        Me.WizardPages1.SelectedIndex += 1
    End Sub

    Private Sub Button_Previous_Click(sender As Object, e As EventArgs) Handles Button_Previous.Click
        Me.WizardPages1.SelectedIndex -= 1
    End Sub

    Private Sub WizardPages1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WizardPages1.SelectedIndexChanged

        If Me.WizardPages1.SelectedIndex > 0 Then
            Me.Button_Previous.Enabled = True
        Else
            Me.Button_Previous.Enabled = False
        End If

        If Me.WizardPages1.SelectedIndex < Me.WizardPages1.TabCount - 1 Then
            Me.Button_Next.Enabled = True
        Else
            Me.Button_Next.Enabled = False
        End If

        If Me.WizardPages1.SelectedIndex = Me.WizardPages1.TabCount - 1 Then
            Me.Button_Execute.Enabled = True
        Else
            Me.Button_Execute.Enabled = False
        End If
    End Sub

End Class