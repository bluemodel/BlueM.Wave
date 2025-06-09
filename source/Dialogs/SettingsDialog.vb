Imports System.Windows.Forms

Public Class SettingsDialog

    Private Sub SettingsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'set current setting values in the form
        Me.NumericUpDown_DefaultLineWidth.Value = My.Settings.defaultLineWidth
        Me.CheckBox_logShowDebugMessages.Checked = (My.Settings.loggingLevel = Log.levels.debug.ToString())

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        'store the settings
        My.Settings.defaultLineWidth = CInt(Me.NumericUpDown_DefaultLineWidth.Value)
        My.Settings.loggingLevel = IIf(Me.CheckBox_logShowDebugMessages.Checked, Log.levels.debug.ToString(), Log.levels.info.ToString())

        'save them
        My.Settings.Save()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
