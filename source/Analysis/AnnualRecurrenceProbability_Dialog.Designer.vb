<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AnnualRecurrenceProbability_Dialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnnualRecurrenceProbability_Dialog))
        Button_OK = New Button()
        ComboBox_startMonth = New ComboBox()
        Label_startMonth = New Label()
        SuspendLayout()
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(234, 44)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 3
        Button_OK.Text = "OK"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' ComboBox_startMonth
        ' 
        ComboBox_startMonth.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_startMonth.FormattingEnabled = True
        ComboBox_startMonth.Location = New Point(171, 7)
        ComboBox_startMonth.Name = "ComboBox_startMonth"
        ComboBox_startMonth.Size = New Size(143, 23)
        ComboBox_startMonth.TabIndex = 14
        ' 
        ' Label_startMonth
        ' 
        Label_startMonth.AutoSize = True
        Label_startMonth.Location = New Point(14, 10)
        Label_startMonth.Name = "Label_startMonth"
        Label_startMonth.Size = New Size(142, 15)
        Label_startMonth.TabIndex = 13
        Label_startMonth.Text = "Start of hydrological year:"
        ' 
        ' AnnualRecurrenceProbability_Dialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(326, 83)
        Controls.Add(ComboBox_startMonth)
        Controls.Add(Label_startMonth)
        Controls.Add(Button_OK)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AnnualRecurrenceProbability_Dialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "AnnualRecurrenceProbability"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_startMonth As ComboBox
    Friend WithEvents Label_startMonth As Label
End Class
