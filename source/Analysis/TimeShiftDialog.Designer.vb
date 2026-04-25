<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TimeShiftDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TimeShiftDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        Label3 = New Label()
        ComboBox_TimestepType = New ComboBox()
        NumericUpDown_TimestepInterval = New NumericUpDown()
        Label_Help = New Label()
        CType(NumericUpDown_TimestepInterval, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(215, 94)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(301, 94)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(14, 53)
        Label3.Name = "Label3"
        Label3.Size = New Size(61, 15)
        Label3.TabIndex = 2
        Label3.Text = "Timespan:"
        ' 
        ' ComboBox_TimestepType
        ' 
        ComboBox_TimestepType.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_TimestepType.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_TimestepType.FormattingEnabled = True
        ComboBox_TimestepType.Location = New Point(159, 50)
        ComboBox_TimestepType.Name = "ComboBox_TimestepType"
        ComboBox_TimestepType.Size = New Size(222, 23)
        ComboBox_TimestepType.TabIndex = 4
        ' 
        ' NumericUpDown_TimestepInterval
        ' 
        NumericUpDown_TimestepInterval.Location = New Point(88, 51)
        NumericUpDown_TimestepInterval.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        NumericUpDown_TimestepInterval.Minimum = New Decimal(New Integer() {9999, 0, 0, Integer.MinValue})
        NumericUpDown_TimestepInterval.Name = "NumericUpDown_TimestepInterval"
        NumericUpDown_TimestepInterval.Size = New Size(63, 23)
        NumericUpDown_TimestepInterval.TabIndex = 3
        NumericUpDown_TimestepInterval.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label_Help
        ' 
        Label_Help.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label_Help.Location = New Point(14, 10)
        Label_Help.Name = "Label_Help"
        Label_Help.Size = New Size(362, 37)
        Label_Help.TabIndex = 5
        Label_Help.Text = "Shift all timestamps of the selected series by the given timespan." & vbCrLf & "Positive amounts shift to the future, negative to the past."
        ' 
        ' TimeShiftDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(393, 133)
        Controls.Add(OK_Button)
        Controls.Add(Label_Help)
        Controls.Add(Cancel_Button)
        Controls.Add(Label3)
        Controls.Add(NumericUpDown_TimestepInterval)
        Controls.Add(ComboBox_TimestepType)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(301, 144)
        Name = "TimeShiftDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Time Shift"
        CType(NumericUpDown_TimestepInterval, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_TimestepType As System.Windows.Forms.ComboBox
    Friend WithEvents NumericUpDown_TimestepInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label_Help As Label
End Class
