<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConvertErrorValuesDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConvertErrorValuesDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        ComboBox_Series = New ComboBox()
        Label_Series = New Label()
        TextBox_errorvalues = New TextBox()
        Label_ErrorValues = New Label()
        Label_note = New Label()
        Label_Help = New Label()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(169, 90)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(255, 90)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' ComboBox_Series
        ' 
        ComboBox_Series.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_Series.FormattingEnabled = True
        ComboBox_Series.Location = New Point(99, 12)
        ComboBox_Series.Name = "ComboBox_Series"
        ComboBox_Series.Size = New Size(236, 23)
        ComboBox_Series.TabIndex = 1
        ' 
        ' Label_Series
        ' 
        Label_Series.AutoSize = True
        Label_Series.Location = New Point(15, 15)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New Size(40, 15)
        Label_Series.TabIndex = 2
        Label_Series.Text = "Series:"
        ' 
        ' TextBox_errorvalues
        ' 
        TextBox_errorvalues.Location = New Point(99, 44)
        TextBox_errorvalues.Name = "TextBox_errorvalues"
        TextBox_errorvalues.Size = New Size(116, 23)
        TextBox_errorvalues.TabIndex = 3
        TextBox_errorvalues.Text = "-777,-9999.999"
        ' 
        ' Label_ErrorValues
        ' 
        Label_ErrorValues.AutoSize = True
        Label_ErrorValues.Location = New Point(15, 47)
        Label_ErrorValues.Name = "Label_ErrorValues"
        Label_ErrorValues.Size = New Size(71, 15)
        Label_ErrorValues.TabIndex = 4
        Label_ErrorValues.Text = "Error values:"
        ' 
        ' Label_note
        ' 
        Label_note.AutoSize = True
        Label_note.Location = New Point(223, 47)
        Label_note.Name = "Label_note"
        Label_note.Size = New Size(112, 15)
        Label_note.TabIndex = 5
        Label_note.Text = "(comma-separated)"
        ' 
        ' Label_Help
        ' 
        Label_Help.ForeColor = SystemColors.ControlDarkDark
        Label_Help.Location = New Point(15, 85)
        Label_Help.Name = "Label_Help"
        Label_Help.Size = New Size(141, 37)
        Label_Help.TabIndex = 6
        Label_Help.Text = "Converts error values to NaN"
        ' 
        ' ConvertErrorValuesDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(348, 129)
        Controls.Add(OK_Button)
        Controls.Add(Label_Help)
        Controls.Add(Cancel_Button)
        Controls.Add(Label_note)
        Controls.Add(Label_ErrorValues)
        Controls.Add(TextBox_errorvalues)
        Controls.Add(Label_Series)
        Controls.Add(ComboBox_Series)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "ConvertErrorValuesDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Convert error values"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Series As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Series As System.Windows.Forms.Label
    Friend WithEvents TextBox_errorvalues As System.Windows.Forms.TextBox
    Friend WithEvents Label_ErrorValues As System.Windows.Forms.Label
    Friend WithEvents Label_note As System.Windows.Forms.Label
    Friend WithEvents Label_Help As System.Windows.Forms.Label

End Class
