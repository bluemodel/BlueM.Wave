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
Friend Class SaveProjectFileDialog

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Friend ReadOnly Property FileName As String
        Get
            Return TextBox_File.Text
        End Get
    End Property

    Friend ReadOnly Property SaveRelativePaths As Boolean
        Get
            Return CheckBox_RelativePaths.Checked
        End Get
    End Property

    Friend ReadOnly Property SaveTitle As Boolean
        Get
            Return CheckBox_Title.Checked
        End Get
    End Property

    Friend ReadOnly Property SaveUnit As Boolean
        Get
            Return CheckBox_Unit.Checked
        End Get
    End Property

    Friend ReadOnly Property SaveInterpretation As Boolean
        Get
            Return CheckBox_Interpretation.Checked
        End Get
    End Property

    Friend ReadOnly Property SaveColor As Boolean
        Get
            Return CheckBox_Color.Checked
        End Get
    End Property

    Friend ReadOnly Property SaveLineStyle As Boolean
        Get
            Return CheckBox_LineStyle.Checked
        End Get
    End Property

    Friend ReadOnly Property SaveLineWidth As Boolean
        Get
            Return CheckBox_LineWidth.Checked
        End Get
    End Property

    Friend ReadOnly Property SavePointsVisibility As Boolean
        Get
            Return CheckBox_PointsVisibility.Checked
        End Get
    End Property

    ''' <summary>
    ''' OK button pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_OK_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click

        'validate inputs
        If Me.TextBox_File.Text.Length = 0 Then
            MsgBox("Please specify a file name!", MsgBoxStyle.Exclamation)
            Me.DialogResult = DialogResult.None
        End If

    End Sub

    Private Sub Button_Browse_Click(sender As Object, e As EventArgs) Handles Button_Browse.Click

        Dim dlgres As DialogResult

        'Prepare SaveFileDialog
        SaveFileDialog1.Title = "Save project file"
        SaveFileDialog1.Filter = "Wave project files (*.wvp)|*wvp"
        SaveFileDialog1.DefaultExt = "wvp"
        SaveFileDialog1.OverwritePrompt = True

        'Show dialog
        dlgres = SaveFileDialog1.ShowDialog()

        If dlgres = DialogResult.OK Then
            Me.TextBox_File.Text = SaveFileDialog1.FileName
        End If

    End Sub
End Class