<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeTimestepDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangeTimestepDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox_InputInterpretation = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_TimestepType = New System.Windows.Forms.ComboBox()
        Me.NumericUpDown_TimestepInterval = New System.Windows.Forms.NumericUpDown()
        Me.MaskedTextBox_Start = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBox_IgnoreNaN = New System.Windows.Forms.CheckBox()
        Me.Label_IgnoreNaN = New System.Windows.Forms.Label()
        Me.ComboBox_OutputInterpretation = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.NumericUpDown_TimestepInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(246, 133)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Input interpretation:"
        '
        'ComboBox_InputInterpretation
        '
        Me.ComboBox_InputInterpretation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_InputInterpretation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_InputInterpretation.FormattingEnabled = True
        Me.ComboBox_InputInterpretation.Location = New System.Drawing.Point(124, 6)
        Me.ComboBox_InputInterpretation.Name = "ComboBox_InputInterpretation"
        Me.ComboBox_InputInterpretation.Size = New System.Drawing.Size(268, 21)
        Me.ComboBox_InputInterpretation.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Timestep interval:"
        '
        'ComboBox_TimestepType
        '
        Me.ComboBox_TimestepType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_TimestepType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_TimestepType.FormattingEnabled = True
        Me.ComboBox_TimestepType.Location = New System.Drawing.Point(184, 60)
        Me.ComboBox_TimestepType.Name = "ComboBox_TimestepType"
        Me.ComboBox_TimestepType.Size = New System.Drawing.Size(208, 21)
        Me.ComboBox_TimestepType.TabIndex = 4
        '
        'NumericUpDown_TimestepInterval
        '
        Me.NumericUpDown_TimestepInterval.Location = New System.Drawing.Point(124, 60)
        Me.NumericUpDown_TimestepInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_TimestepInterval.Name = "NumericUpDown_TimestepInterval"
        Me.NumericUpDown_TimestepInterval.Size = New System.Drawing.Size(54, 20)
        Me.NumericUpDown_TimestepInterval.TabIndex = 3
        Me.NumericUpDown_TimestepInterval.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'MaskedTextBox_Start
        '
        Me.MaskedTextBox_Start.Culture = New System.Globalization.CultureInfo("")
        Me.MaskedTextBox_Start.Location = New System.Drawing.Point(124, 87)
        Me.MaskedTextBox_Start.Name = "MaskedTextBox_Start"
        Me.MaskedTextBox_Start.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox_Start.TabIndex = 6
        Me.MaskedTextBox_Start.ValidatingType = GetType(Date)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Start:"
        '
        'CheckBox_IgnoreNaN
        '
        Me.CheckBox_IgnoreNaN.AutoSize = True
        Me.CheckBox_IgnoreNaN.Location = New System.Drawing.Point(124, 114)
        Me.CheckBox_IgnoreNaN.Name = "CheckBox_IgnoreNaN"
        Me.CheckBox_IgnoreNaN.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_IgnoreNaN.TabIndex = 8
        Me.CheckBox_IgnoreNaN.UseVisualStyleBackColor = True
        '
        'Label_IgnoreNaN
        '
        Me.Label_IgnoreNaN.AutoSize = True
        Me.Label_IgnoreNaN.Location = New System.Drawing.Point(12, 115)
        Me.Label_IgnoreNaN.Name = "Label_IgnoreNaN"
        Me.Label_IgnoreNaN.Size = New System.Drawing.Size(65, 13)
        Me.Label_IgnoreNaN.TabIndex = 7
        Me.Label_IgnoreNaN.Text = "Ignore NaN:"
        '
        'ComboBox_OutputInterpretation
        '
        Me.ComboBox_OutputInterpretation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_OutputInterpretation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_OutputInterpretation.FormattingEnabled = True
        Me.ComboBox_OutputInterpretation.Location = New System.Drawing.Point(124, 33)
        Me.ComboBox_OutputInterpretation.Name = "ComboBox_OutputInterpretation"
        Me.ComboBox_OutputInterpretation.Size = New System.Drawing.Size(268, 21)
        Me.ComboBox_OutputInterpretation.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Output interpretation:"
        '
        'ChangeTimestepDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(404, 174)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBox_InputInterpretation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox_OutputInterpretation)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NumericUpDown_TimestepInterval)
        Me.Controls.Add(Me.ComboBox_TimestepType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.MaskedTextBox_Start)
        Me.Controls.Add(Me.Label_IgnoreNaN)
        Me.Controls.Add(Me.CheckBox_IgnoreNaN)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(420, 170)
        Me.Name = "ChangeTimestepDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change Timestep"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.NumericUpDown_TimestepInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_InputInterpretation As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_TimestepType As System.Windows.Forms.ComboBox
    Friend WithEvents NumericUpDown_TimestepInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents MaskedTextBox_Start As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_IgnoreNaN As CheckBox
    Friend WithEvents Label_IgnoreNaN As Label
    Friend WithEvents ComboBox_OutputInterpretation As ComboBox
    Friend WithEvents Label1 As Label
End Class
