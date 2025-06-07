Imports System.Windows.Forms

Public Class SettingsDialog

    Private Sub SettingsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'set values in the form
        Me.NumericUpDown_DefaultLineWidth.Value = My.Settings.defaultLineWidth

    End Sub


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        'store the settings
        My.Settings.defaultLineWidth = CInt(Me.NumericUpDown_DefaultLineWidth.Value)

        My.Settings.Save()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
