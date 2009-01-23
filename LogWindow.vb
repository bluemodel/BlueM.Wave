Imports System.Windows.Forms

Public Class LogWindow

#Region "Form behavior"

    Private WithEvents myLog As Log

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.myLog = Log.getInstance()

    End Sub

    Private Sub LogChanged() Handles myLog.LogChanged
        'Textbox mit Logtext aktualisieren
        Me.TextBox_Log.Text = Log.Text
    End Sub

    'Form schließen
    '**************
    Private Sub LogWindow_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        'verhindern, dass das Formular komplett gelöscht wird
        e.Cancel = True

        'Formular verstecken
        Call Me.Hide()

    End Sub

    Private Sub ToolStripButton_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_New.Click
        Call Log.ClearLog()
    End Sub

    Private Sub ToolStripButton_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Save.Click
        If (Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Me.TextBox_Log.SaveFile(Me.SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub

#End Region 'Form behavior

End Class
