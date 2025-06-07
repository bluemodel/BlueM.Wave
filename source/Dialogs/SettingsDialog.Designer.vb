<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.NumericUpDown_DefaultLineWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label_defaultLineWidth = New System.Windows.Forms.Label()
        Me.Label_loggingLevel = New System.Windows.Forms.Label()
        Me.ComboBox_loggingLevel = New System.Windows.Forms.ComboBox()
        Me.GroupBox_Chart = New System.Windows.Forms.GroupBox()
        Me.GroupBox_Logging = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.NumericUpDown_DefaultLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Chart.SuspendLayout()
        Me.GroupBox_Logging.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(76, 270)
        Me.TableLayoutPanel1.MinimumSize = New System.Drawing.Size(150, 30)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(150, 30)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(79, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'NumericUpDown_DefaultLineWidth
        '
        Me.NumericUpDown_DefaultLineWidth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_DefaultLineWidth.Location = New System.Drawing.Point(133, 19)
        Me.NumericUpDown_DefaultLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDown_DefaultLineWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_DefaultLineWidth.Name = "NumericUpDown_DefaultLineWidth"
        Me.NumericUpDown_DefaultLineWidth.Size = New System.Drawing.Size(71, 20)
        Me.NumericUpDown_DefaultLineWidth.TabIndex = 1
        Me.NumericUpDown_DefaultLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label_defaultLineWidth
        '
        Me.Label_defaultLineWidth.AutoSize = True
        Me.Label_defaultLineWidth.Location = New System.Drawing.Point(6, 21)
        Me.Label_defaultLineWidth.Name = "Label_defaultLineWidth"
        Me.Label_defaultLineWidth.Size = New System.Drawing.Size(121, 13)
        Me.Label_defaultLineWidth.TabIndex = 2
        Me.Label_defaultLineWidth.Text = "Default series line width:"
        '
        'Label_loggingLevel
        '
        Me.Label_loggingLevel.AutoSize = True
        Me.Label_loggingLevel.Location = New System.Drawing.Point(6, 22)
        Me.Label_loggingLevel.Name = "Label_loggingLevel"
        Me.Label_loggingLevel.Size = New System.Drawing.Size(73, 13)
        Me.Label_loggingLevel.TabIndex = 3
        Me.Label_loggingLevel.Text = "Logging level:"
        '
        'ComboBox_loggingLevel
        '
        Me.ComboBox_loggingLevel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_loggingLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_loggingLevel.FormattingEnabled = True
        Me.ComboBox_loggingLevel.Location = New System.Drawing.Point(133, 19)
        Me.ComboBox_loggingLevel.Name = "ComboBox_loggingLevel"
        Me.ComboBox_loggingLevel.Size = New System.Drawing.Size(71, 21)
        Me.ComboBox_loggingLevel.TabIndex = 4
        '
        'GroupBox_Chart
        '
        Me.GroupBox_Chart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Chart.Controls.Add(Me.Label_defaultLineWidth)
        Me.GroupBox_Chart.Controls.Add(Me.NumericUpDown_DefaultLineWidth)
        Me.GroupBox_Chart.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_Chart.Name = "GroupBox_Chart"
        Me.GroupBox_Chart.Size = New System.Drawing.Size(210, 54)
        Me.GroupBox_Chart.TabIndex = 5
        Me.GroupBox_Chart.TabStop = False
        Me.GroupBox_Chart.Text = "Chart"
        '
        'GroupBox_Logging
        '
        Me.GroupBox_Logging.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Logging.Controls.Add(Me.ComboBox_loggingLevel)
        Me.GroupBox_Logging.Controls.Add(Me.Label_loggingLevel)
        Me.GroupBox_Logging.Location = New System.Drawing.Point(12, 73)
        Me.GroupBox_Logging.Name = "GroupBox_Logging"
        Me.GroupBox_Logging.Size = New System.Drawing.Size(210, 52)
        Me.GroupBox_Logging.TabIndex = 6
        Me.GroupBox_Logging.TabStop = False
        Me.GroupBox_Logging.Text = "Logging"
        '
        'SettingsDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(234, 311)
        Me.Controls.Add(Me.GroupBox_Logging)
        Me.Controls.Add(Me.GroupBox_Chart)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(250, 350)
        Me.Name = "SettingsDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.NumericUpDown_DefaultLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Chart.ResumeLayout(False)
        Me.GroupBox_Chart.PerformLayout()
        Me.GroupBox_Logging.ResumeLayout(False)
        Me.GroupBox_Logging.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents NumericUpDown_DefaultLineWidth As NumericUpDown
    Friend WithEvents Label_defaultLineWidth As Label
    Friend WithEvents Label_loggingLevel As Label
    Friend WithEvents ComboBox_loggingLevel As ComboBox
    Friend WithEvents GroupBox_Chart As GroupBox
    Friend WithEvents GroupBox_Logging As GroupBox
End Class
