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
''' TODO: unlike the main chart, this chart uses the inbuilt TeeChart zoom and pan behaviour, which may be unexpected for users
''' </summary>
Friend Class AnalysisResultChart

    ''' <summary>
    ''' Copy chart to clipboard as PNG image
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_Copy_Click(sender As Object, e As EventArgs) Handles ToolStripButton_Copy.Click
        Call Me.TChart1.Export.Image.PNG.CopyToClipboard()
    End Sub

    ''' <summary>
    ''' Show the chart editor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton_EditChart_Click(sender As Object, e As EventArgs) Handles ToolStripButton_EditChart.Click, TChart1.DoubleClick
        TChart1.ShowEditor()
    End Sub

End Class