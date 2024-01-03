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
''' Form to display an analysis result chart
''' </summary>
Friend Class AnalysisResultChart

    Private formsPlot As ScottPlot.FormsPlot

    Public Sub New(formsPlot As ScottPlot.FormsPlot)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.formsPlot = formsPlot

        'add plot to form
        Me.formsPlot.Dock = DockStyle.Fill
        Me.Panel1.Controls.Add(Me.formsPlot)

    End Sub

    ''' <summary>
    ''' Copy chart to clipboard as PNG image
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_Copy_Click(sender As Object, e As EventArgs) Handles ToolStripButton_Copy.Click
        Clipboard.SetImage(Me.formsPlot.Plot.Render())
    End Sub

    ''' <summary>
    ''' Show the chart editor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_EditChart_Click(sender As Object, e As EventArgs) Handles ToolStripButton_EditChart.Click
        'TODO: TChart
        MsgBox("Not yet implemented!", MsgBoxStyle.Critical)
    End Sub

    Private Sub AnalysisResultChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.formsPlot.Refresh()
    End Sub

End Class