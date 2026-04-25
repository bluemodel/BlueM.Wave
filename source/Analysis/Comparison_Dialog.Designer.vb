<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Comparison_Dialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Comparison_Dialog))
        RadioButton_Reihe1 = New RadioButton()
        RadioButton_Reihe2 = New RadioButton()
        Label1 = New Label()
        Button_OK = New Button()
        SuspendLayout()
        ' 
        ' RadioButton_Reihe1
        ' 
        RadioButton_Reihe1.AutoSize = True
        RadioButton_Reihe1.Checked = True
        RadioButton_Reihe1.Location = New Point(14, 39)
        RadioButton_Reihe1.Name = "RadioButton_Reihe1"
        RadioButton_Reihe1.Size = New Size(97, 19)
        RadioButton_Reihe1.TabIndex = 0
        RadioButton_Reihe1.TabStop = True
        RadioButton_Reihe1.Text = "RadioButton1"
        ' 
        ' RadioButton_Reihe2
        ' 
        RadioButton_Reihe2.AutoSize = True
        RadioButton_Reihe2.Location = New Point(14, 66)
        RadioButton_Reihe2.Name = "RadioButton_Reihe2"
        RadioButton_Reihe2.Size = New Size(97, 19)
        RadioButton_Reihe2.TabIndex = 1
        RadioButton_Reihe2.Text = "RadioButton2"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(284, 15)
        Label1.TabIndex = 2
        Label1.Text = "Select the series that should be plotted on the x-axis:"
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(295, 106)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 3
        Button_OK.Text = "OK"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' Comparison_Dialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(387, 145)
        Controls.Add(Button_OK)
        Controls.Add(Label1)
        Controls.Add(RadioButton_Reihe1)
        Controls.Add(RadioButton_Reihe2)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Comparison_Dialog"
        StartPosition = FormStartPosition.CenterParent
        Text = "Comparison"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents RadioButton_Reihe1 As System.Windows.Forms.RadioButton
    Private WithEvents RadioButton_Reihe2 As System.Windows.Forms.RadioButton
End Class
