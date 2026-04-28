<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AccumulateDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AccumulateDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        MaskedTextBox_Start = New MaskedTextBox()
        Label4 = New Label()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(235, 66)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(321, 66)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' MaskedTextBox_Start
        ' 
        MaskedTextBox_Start.Culture = New System.Globalization.CultureInfo("")
        MaskedTextBox_Start.Location = New Point(86, 7)
        MaskedTextBox_Start.Name = "MaskedTextBox_Start"
        MaskedTextBox_Start.Size = New Size(116, 23)
        MaskedTextBox_Start.TabIndex = 6
        MaskedTextBox_Start.ValidatingType = GetType(Date)
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(14, 10)
        Label4.Name = "Label4"
        Label4.Size = New Size(60, 15)
        Label4.TabIndex = 5
        Label4.Text = "Start date:"
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label1.ForeColor = SystemColors.ControlDarkDark
        Label1.Location = New Point(14, 45)
        Label1.Name = "Label1"
        Label1.Size = New Size(189, 52)
        Label1.TabIndex = 7
        Label1.Text = "The time series will be cut to this start date before calculating cumulative values"
        ' 
        ' AccumulateDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(413, 105)
        Controls.Add(OK_Button)
        Controls.Add(Label1)
        Controls.Add(Cancel_Button)
        Controls.Add(Label4)
        Controls.Add(MaskedTextBox_Start)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(429, 144)
        Name = "AccumulateDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Accumulate"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents MaskedTextBox_Start As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As Label
End Class
