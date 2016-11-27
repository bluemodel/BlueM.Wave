<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Comparison_Dialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.RadioButton_Reihe1 = New System.Windows.Forms.RadioButton
        Me.RadioButton_Reihe2 = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button_OK = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'RadioButton_Reihe1
        '
        Me.RadioButton_Reihe1.AutoSize = True
        Me.RadioButton_Reihe1.Checked = True
        Me.RadioButton_Reihe1.Location = New System.Drawing.Point(12, 34)
        Me.RadioButton_Reihe1.Name = "RadioButton_Reihe1"
        Me.RadioButton_Reihe1.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton_Reihe1.TabIndex = 0
        Me.RadioButton_Reihe1.TabStop = True
        Me.RadioButton_Reihe1.Text = "RadioButton1"
        '
        'RadioButton_Reihe2
        '
        Me.RadioButton_Reihe2.AutoSize = True
        Me.RadioButton_Reihe2.Location = New System.Drawing.Point(12, 57)
        Me.RadioButton_Reihe2.Name = "RadioButton_Reihe2"
        Me.RadioButton_Reihe2.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton_Reihe2.TabIndex = 1
        Me.RadioButton_Reihe2.Text = "RadioButton2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(255, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select the series that should be plotted on the x-axis:"
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(245, 91)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 3
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Comparison_Dialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 126)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadioButton_Reihe1)
        Me.Controls.Add(Me.RadioButton_Reihe2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Comparison_Dialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Comparison"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents RadioButton_Reihe1 As System.Windows.Forms.RadioButton
    Private WithEvents RadioButton_Reihe2 As System.Windows.Forms.RadioButton
End Class
