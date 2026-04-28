<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class MonthlyStatisticsDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MonthlyStatisticsDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        ComboBox_MonthType = New ComboBox()
        Label1 = New Label()
        Label2 = New Label()
        ComboBox_startMonth = New ComboBox()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(142, 88)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(228, 88)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' ComboBox_MonthType
        ' 
        ComboBox_MonthType.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_MonthType.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_MonthType.Items.AddRange(New Object() {"current month", "previous month"})
        ComboBox_MonthType.Location = New Point(190, 14)
        ComboBox_MonthType.Name = "ComboBox_MonthType"
        ComboBox_MonthType.Size = New Size(118, 23)
        ComboBox_MonthType.TabIndex = 8
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(15, 17)
        Label1.Name = "Label1"
        Label1.Size = New Size(153, 15)
        Label1.TabIndex = 10
        Label1.Text = "Series values correspond to "
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(15, 48)
        Label2.Name = "Label2"
        Label2.Size = New Size(116, 15)
        Label2.TabIndex = 11
        Label2.Text = "Start month in chart:"
        ' 
        ' ComboBox_startMonth
        ' 
        ComboBox_startMonth.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_startMonth.FormattingEnabled = True
        ComboBox_startMonth.Location = New Point(190, 45)
        ComboBox_startMonth.Name = "ComboBox_startMonth"
        ComboBox_startMonth.Size = New Size(118, 23)
        ComboBox_startMonth.TabIndex = 12
        ' 
        ' MonthlyStatisticsDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(320, 127)
        Controls.Add(OK_Button)
        Controls.Add(Cancel_Button)
        Controls.Add(ComboBox_startMonth)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(ComboBox_MonthType)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(336, 132)
        Name = "MonthlyStatisticsDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Monthly Statistics"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents ComboBox_MonthType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox_startMonth As ComboBox
End Class
