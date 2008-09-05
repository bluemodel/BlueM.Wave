Imports System.Windows.Forms

Public Class LogWindow

#Region "Form behavior"

    'Form schließen
    '**************
    Private Sub Log_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        'verhindern, dass das Formular komplett gelöscht wird
        e.Cancel = True

        'Formular verstecken
        Call Me.Hide()

    End Sub

    Private Sub ToolStripButton_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_New.Click
        Call Me.ClearLog()
    End Sub

    Private Sub ToolStripButton_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Save.Click
        If (Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Me.TextBox_Log.SaveFile(Me.SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub LogWindow_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.TextBox_Log.Width = Me.ClientSize.Width - 20
        Me.TextBox_Log.Height = Me.ClientSize.Height - 10 - 25
    End Sub

#End Region 'Form behavior

    'Log-Eintrag hinzufügen
    '**********************
    Public Sub AddLogEntry(ByVal msg As String)

        Me.TextBox_Log.Text &= "* " & DateTime.Now.ToString(Konstanten.Datumsformat) & ": " & msg & eol

    End Sub

    'Log zurücksetzen
    '****************
    Public Sub ClearLog()

        Me.TextBox_Log.Clear()

    End Sub

End Class
