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
Imports System.Drawing.Text
Imports System.Windows.Forms

''' <summary>
''' Dialog for user settings
''' </summary>
Friend Class SettingsDialog

    Private isLoading As Boolean = True

    Private Sub SettingsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.isLoading = True

        'color modes
        Me.ComboBox_Colormode.Items.Add("Auto")
        Me.ComboBox_Colormode.Items.Add("Light")
        Me.ComboBox_Colormode.Items.Add("Dark")

        'get installed fonts
        Dim installedFontCollection As New InstalledFontCollection()
        Dim fontFamilies() As FontFamily = installedFontCollection.Families

        'add list of installed fonts to the combo box
        For Each fontFamily As FontFamily In fontFamilies
            Me.ComboBox_Font.Items.Add(fontFamily.Name)
        Next

        'set current setting values in the form
        Me.ComboBox_Colormode.SelectedItem = My.Settings.colorMode
        Me.CheckBox_showOverviewOnStartup.Checked = My.Settings.showOverviewOnStartup
        Me.ComboBox_Font.SelectedItem = My.Settings.defaultFont
        Me.NumericUpDown_DefaultFontSize.Value = My.Settings.defaultFontSize
        Me.NumericUpDown_DefaultLineWidth.Value = My.Settings.defaultLineWidth
        Me.CheckBox_logShowDebugMessages.Checked = (My.Settings.loggingLevel = Log.levels.debug.ToString())

        Me.isLoading = False

    End Sub

    Private Sub ComboBox_Colormode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Colormode.SelectedIndexChanged
        My.Settings.colorMode = Me.ComboBox_Colormode.SelectedItem
        My.Settings.Save()
    End Sub

    Private Sub CheckBox_showOverviewOnStartup_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_showOverviewOnStartup.CheckedChanged
        My.Settings.showOverviewOnStartup = Me.CheckBox_showOverviewOnStartup.Checked
        My.Settings.Save()
    End Sub

    Private Sub ComboBox_Font_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Font.SelectedIndexChanged
        If Not isLoading Then
            'update the default font
            Dim selectedFont As String = Me.ComboBox_Font.SelectedItem
            My.Settings.defaultFont = selectedFont
            My.Settings.Save()
        End If
    End Sub

    Private Sub NumericUpDown_DefaultFontSize_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_DefaultFontSize.ValueChanged
        If Not isLoading Then
            'update the default font size
            My.Settings.defaultFontSize = CInt(Me.NumericUpDown_DefaultFontSize.Value)
            My.Settings.Save()
        End If
    End Sub

    Private Sub NumericUpDown_DefaultLineWidth_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_DefaultLineWidth.ValueChanged
        If Not isLoading Then
            'update the default line width
            My.Settings.defaultLineWidth = CInt(Me.NumericUpDown_DefaultLineWidth.Value)
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox_logShowDebugMessages_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_logShowDebugMessages.CheckedChanged
        If Not isLoading Then
            'update the logging level
            My.Settings.loggingLevel = IIf(Me.CheckBox_logShowDebugMessages.Checked, Log.levels.debug.ToString(), Log.levels.info.ToString())
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button_ShowFontDialog_Click(sender As Object, e As EventArgs) Handles Button_ShowFontDialog.Click
        Dim dlgResult As DialogResult = Me.FontDialog1.ShowDialog()
        If dlgResult = DialogResult.OK Then
            Me.ComboBox_Font.SelectedItem = Me.FontDialog1.Font.Name
            Me.NumericUpDown_DefaultFontSize.Value = CInt(Me.FontDialog1.Font.Size)
        End If
    End Sub

    Private Sub Button_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Close.Click

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class
