Imports System.Windows.Forms

Public Class SettingsDialog

    Private Sub SettingsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'initialize form
        Me.ComboBox_loggingLevel.Items.AddRange([Enum].GetNames(GetType(Log.levels)))

        'set current setting values in the form
        Me.NumericUpDown_DefaultLineWidth.Value = My.Settings.defaultLineWidth
        Me.ComboBox_loggingLevel.SelectedItem = My.Settings.loggingLevel.ToString()

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        'store the settings
        My.Settings.defaultLineWidth = CInt(Me.NumericUpDown_DefaultLineWidth.Value)
        My.Settings.loggingLevel = Me.ComboBox_loggingLevel.SelectedItem.ToString()

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
