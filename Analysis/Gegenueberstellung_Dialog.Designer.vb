<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Gegenueberstellung_Dialog
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
        Me.RadioButton_Reihe1 = New System.Windows.Forms.RadioButton
        Me.RadioButton_Reihe2 = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'RadioButton_Reihe1
        '
        Me.RadioButton_Reihe1.AutoSize = True
        Me.RadioButton_Reihe1.Location = New System.Drawing.Point(30, 49)
        Me.RadioButton_Reihe1.Name = "RadioButton_Reihe1"
        Me.RadioButton_Reihe1.Size = New System.Drawing.Size(84, 17)
        Me.RadioButton_Reihe1.TabIndex = 0
        Me.RadioButton_Reihe1.TabStop = True
        Me.RadioButton_Reihe1.Text = "RadioButton"
        Me.RadioButton_Reihe1.UseVisualStyleBackColor = True
        '
        'RadioButton_Reihe2
        '
        Me.RadioButton_Reihe2.AutoSize = True
        Me.RadioButton_Reihe2.Location = New System.Drawing.Point(30, 72)
        Me.RadioButton_Reihe2.Name = "RadioButton_Reihe2"
        Me.RadioButton_Reihe2.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton_Reihe2.TabIndex = 1
        Me.RadioButton_Reihe2.TabStop = True
        Me.RadioButton_Reihe2.Text = "RadioButton1"
        Me.RadioButton_Reihe2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'Gegenueberstellung_Dialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 165)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadioButton_Reihe2)
        Me.Controls.Add(Me.RadioButton_Reihe1)
        Me.Name = "Gegenueberstellung_Dialog"
        Me.Text = "Gegenueberstellung_Dialog"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButton_Reihe1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Reihe2 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
