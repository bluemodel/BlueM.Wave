<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GoodnessOfFit_Dialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GoodnessOfFit_Dialog))
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.CheckBox_Annual = New System.Windows.Forms.CheckBox()
        Me.ComboBox_startMonth = New System.Windows.Forms.ComboBox()
        Me.Label_startMonth = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(9, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton1.TabIndex = 1
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "RadioButton1"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(9, 43)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton2.TabIndex = 2
        Me.RadioButton2.Text = "RadioButton2"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(245, 173)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 3
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'CheckBox_Annual
        '
        Me.CheckBox_Annual.AutoSize = True
        Me.CheckBox_Annual.Location = New System.Drawing.Point(7, 19)
        Me.CheckBox_Annual.Name = "CheckBox_Annual"
        Me.CheckBox_Annual.Size = New System.Drawing.Size(274, 17)
        Me.CheckBox_Annual.TabIndex = 4
        Me.CheckBox_Annual.Text = "Calculate parameters for individual hydrological years"
        Me.CheckBox_Annual.UseVisualStyleBackColor = True
        '
        'ComboBox_startMonth
        '
        Me.ComboBox_startMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_startMonth.Enabled = False
        Me.ComboBox_startMonth.FormattingEnabled = True
        Me.ComboBox_startMonth.Location = New System.Drawing.Point(138, 41)
        Me.ComboBox_startMonth.Name = "ComboBox_startMonth"
        Me.ComboBox_startMonth.Size = New System.Drawing.Size(102, 21)
        Me.ComboBox_startMonth.TabIndex = 14
        '
        'Label_startMonth
        '
        Me.Label_startMonth.AutoSize = True
        Me.Label_startMonth.Enabled = False
        Me.Label_startMonth.Location = New System.Drawing.Point(6, 44)
        Me.Label_startMonth.Name = "Label_startMonth"
        Me.Label_startMonth.Size = New System.Drawing.Size(126, 13)
        Me.Label_startMonth.TabIndex = 13
        Me.Label_startMonth.Text = "Start of hydrological year:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBox_Annual)
        Me.GroupBox1.Controls.Add(Me.ComboBox_startMonth)
        Me.GroupBox1.Controls.Add(Me.Label_startMonth)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 88)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(308, 73)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Hydrological years"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(308, 70)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Series with ""observed"" values"
        '
        'GoodnessOfFit_Dialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 208)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button_OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GoodnessOfFit_Dialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Goodness of Fit"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents CheckBox_Annual As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox_startMonth As ComboBox
    Friend WithEvents Label_startMonth As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
End Class
