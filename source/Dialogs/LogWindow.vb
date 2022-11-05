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

Friend Class LogWindow

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.AddLogEntry(Log.levels.info, "Logging level is set to " & Log.level.ToString())
    End Sub

    Friend Sub AddLogEntry(level As Log.levels, msg As String)

        Dim start, length As Integer

        start = Me.TextBox_Log.TextLength

        'format log entry
        msg = $"* {DateTime.Now.ToString(Helpers.CurrentDateFormat)} {level.ToString.ToUpper()}: {msg}" & eol

        length = msg.Length

        'append text
        Me.TextBox_Log.AppendText(msg)

        'select new entry
        Me.TextBox_Log.Select(start, length)

        Dim currentFont As Font = Me.TextBox_Log.Font

        Dim boldFont As New Font(
            currentFont.FontFamily,
            currentFont.Size,
            FontStyle.Bold)

        'apply formatting
        Select Case level
            Case Log.levels.debug
                Me.TextBox_Log.SelectionColor = Color.DarkGreen
                Me.TextBox_Log.SelectionFont = boldFont
            Case Log.levels.warning
                Me.TextBox_Log.SelectionColor = Color.DarkOrange
                Me.TextBox_Log.SelectionFont = boldFont
            Case Log.levels.error
                Me.TextBox_Log.SelectionColor = Color.Red
                Me.TextBox_Log.SelectionFont = boldFont
            Case Else
                Me.TextBox_Log.SelectionColor = Color.Black
                Me.TextBox_Log.SelectionFont = currentFont
        End Select

        'unselect and reset formatting
        Me.TextBox_Log.Select(Me.TextBox_Log.Text.Length, 0)
        Me.TextBox_Log.SelectionColor = Color.Black
        Me.TextBox_Log.SelectionFont = currentFont

        Call Application.DoEvents()
    End Sub

    Friend Sub ClearLog()
        Me.TextBox_Log.Text = ""
        Call Application.DoEvents()
    End Sub

    'Form schließen
    '**************
    Private Sub LogWindow_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        'verhindern, dass das Formular komplett gelöscht wird
        e.Cancel = True

        'Formular verstecken
        Call Me.Hide()

    End Sub

    Private Sub ToolStripButton_New_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton_Clear.Click
        Call Log.ClearLog()
    End Sub

    Private Sub ToolStripButton_Save_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton_Save.Click
        If (Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Me.TextBox_Log.SaveFile(Me.SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub

End Class
