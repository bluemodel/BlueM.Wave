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
        Me.Button_Close = New System.Windows.Forms.Button()
        Me.NumericUpDown_DefaultLineWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label_defaultLineWidth = New System.Windows.Forms.Label()
        Me.Label_loggingLevel = New System.Windows.Forms.Label()
        Me.GroupBox_Chart = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown_DefaultFontSize = New System.Windows.Forms.NumericUpDown()
        Me.Label_DefaultFontSize = New System.Windows.Forms.Label()
        Me.GroupBox_Logging = New System.Windows.Forms.GroupBox()
        Me.CheckBox_logShowDebugMessages = New System.Windows.Forms.CheckBox()
        Me.GroupBox_Interface = New System.Windows.Forms.GroupBox()
        Me.Label_showOverview = New System.Windows.Forms.Label()
        Me.CheckBox_showOverviewOnStartup = New System.Windows.Forms.CheckBox()
        CType(Me.NumericUpDown_DefaultLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Chart.SuspendLayout()
        CType(Me.NumericUpDown_DefaultFontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Logging.SuspendLayout()
        Me.GroupBox_Interface.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button_Close
        '
        Me.Button_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Close.Location = New System.Drawing.Point(155, 276)
        Me.Button_Close.Name = "Button_Close"
        Me.Button_Close.Size = New System.Drawing.Size(67, 23)
        Me.Button_Close.TabIndex = 2
        Me.Button_Close.Text = "Close"
        '
        'NumericUpDown_DefaultLineWidth
        '
        Me.NumericUpDown_DefaultLineWidth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_DefaultLineWidth.Location = New System.Drawing.Point(133, 45)
        Me.NumericUpDown_DefaultLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDown_DefaultLineWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_DefaultLineWidth.Name = "NumericUpDown_DefaultLineWidth"
        Me.NumericUpDown_DefaultLineWidth.Size = New System.Drawing.Size(71, 20)
        Me.NumericUpDown_DefaultLineWidth.TabIndex = 3
        Me.NumericUpDown_DefaultLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label_defaultLineWidth
        '
        Me.Label_defaultLineWidth.AutoSize = True
        Me.Label_defaultLineWidth.Location = New System.Drawing.Point(6, 47)
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
        Me.Label_loggingLevel.Size = New System.Drawing.Size(120, 13)
        Me.Label_loggingLevel.TabIndex = 0
        Me.Label_loggingLevel.Text = "Show debug messages:"
        '
        'GroupBox_Chart
        '
        Me.GroupBox_Chart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Chart.Controls.Add(Me.NumericUpDown_DefaultFontSize)
        Me.GroupBox_Chart.Controls.Add(Me.Label_DefaultFontSize)
        Me.GroupBox_Chart.Controls.Add(Me.Label_defaultLineWidth)
        Me.GroupBox_Chart.Controls.Add(Me.NumericUpDown_DefaultLineWidth)
        Me.GroupBox_Chart.Location = New System.Drawing.Point(12, 70)
        Me.GroupBox_Chart.Name = "GroupBox_Chart"
        Me.GroupBox_Chart.Size = New System.Drawing.Size(210, 78)
        Me.GroupBox_Chart.TabIndex = 0
        Me.GroupBox_Chart.TabStop = False
        Me.GroupBox_Chart.Text = "Chart"
        '
        'NumericUpDown_DefaultFontSize
        '
        Me.NumericUpDown_DefaultFontSize.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_DefaultFontSize.Location = New System.Drawing.Point(133, 19)
        Me.NumericUpDown_DefaultFontSize.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_DefaultFontSize.Minimum = New Decimal(New Integer() {6, 0, 0, 0})
        Me.NumericUpDown_DefaultFontSize.Name = "NumericUpDown_DefaultFontSize"
        Me.NumericUpDown_DefaultFontSize.Size = New System.Drawing.Size(71, 20)
        Me.NumericUpDown_DefaultFontSize.TabIndex = 1
        Me.NumericUpDown_DefaultFontSize.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label_DefaultFontSize
        '
        Me.Label_DefaultFontSize.AutoSize = True
        Me.Label_DefaultFontSize.Location = New System.Drawing.Point(6, 21)
        Me.Label_DefaultFontSize.Name = "Label_DefaultFontSize"
        Me.Label_DefaultFontSize.Size = New System.Drawing.Size(86, 13)
        Me.Label_DefaultFontSize.TabIndex = 0
        Me.Label_DefaultFontSize.Text = "Default font size:"
        '
        'GroupBox_Logging
        '
        Me.GroupBox_Logging.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Logging.Controls.Add(Me.Label_loggingLevel)
        Me.GroupBox_Logging.Controls.Add(Me.CheckBox_logShowDebugMessages)
        Me.GroupBox_Logging.Location = New System.Drawing.Point(12, 154)
        Me.GroupBox_Logging.Name = "GroupBox_Logging"
        Me.GroupBox_Logging.Size = New System.Drawing.Size(210, 52)
        Me.GroupBox_Logging.TabIndex = 1
        Me.GroupBox_Logging.TabStop = False
        Me.GroupBox_Logging.Text = "Logging"
        '
        'CheckBox_logShowDebugMessages
        '
        Me.CheckBox_logShowDebugMessages.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_logShowDebugMessages.AutoSize = True
        Me.CheckBox_logShowDebugMessages.Location = New System.Drawing.Point(189, 22)
        Me.CheckBox_logShowDebugMessages.Name = "CheckBox_logShowDebugMessages"
        Me.CheckBox_logShowDebugMessages.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_logShowDebugMessages.TabIndex = 1
        Me.CheckBox_logShowDebugMessages.UseVisualStyleBackColor = True
        '
        'GroupBox_Interface
        '
        Me.GroupBox_Interface.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Interface.Controls.Add(Me.Label_showOverview)
        Me.GroupBox_Interface.Controls.Add(Me.CheckBox_showOverviewOnStartup)
        Me.GroupBox_Interface.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_Interface.Name = "GroupBox_Interface"
        Me.GroupBox_Interface.Size = New System.Drawing.Size(209, 52)
        Me.GroupBox_Interface.TabIndex = 3
        Me.GroupBox_Interface.TabStop = False
        Me.GroupBox_Interface.Text = "Interface"
        '
        'Label_showOverview
        '
        Me.Label_showOverview.AutoSize = True
        Me.Label_showOverview.Location = New System.Drawing.Point(8, 20)
        Me.Label_showOverview.Name = "Label_showOverview"
        Me.Label_showOverview.Size = New System.Drawing.Size(160, 13)
        Me.Label_showOverview.TabIndex = 1
        Me.Label_showOverview.Text = "Show overview chart on startup:"
        '
        'CheckBox_showOverviewOnStartup
        '
        Me.CheckBox_showOverviewOnStartup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_showOverviewOnStartup.AutoSize = True
        Me.CheckBox_showOverviewOnStartup.Location = New System.Drawing.Point(187, 20)
        Me.CheckBox_showOverviewOnStartup.Name = "CheckBox_showOverviewOnStartup"
        Me.CheckBox_showOverviewOnStartup.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_showOverviewOnStartup.TabIndex = 0
        Me.CheckBox_showOverviewOnStartup.UseVisualStyleBackColor = True
        '
        'SettingsDialog
        '
        Me.AcceptButton = Me.Button_Close
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 311)
        Me.Controls.Add(Me.GroupBox_Interface)
        Me.Controls.Add(Me.Button_Close)
        Me.Controls.Add(Me.GroupBox_Logging)
        Me.Controls.Add(Me.GroupBox_Chart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(250, 350)
        Me.Name = "SettingsDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.TopMost = True
        CType(Me.NumericUpDown_DefaultLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Chart.ResumeLayout(False)
        Me.GroupBox_Chart.PerformLayout()
        CType(Me.NumericUpDown_DefaultFontSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Logging.ResumeLayout(False)
        Me.GroupBox_Logging.PerformLayout()
        Me.GroupBox_Interface.ResumeLayout(False)
        Me.GroupBox_Interface.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_Close As System.Windows.Forms.Button
    Friend WithEvents NumericUpDown_DefaultLineWidth As NumericUpDown
    Friend WithEvents Label_defaultLineWidth As Label
    Friend WithEvents Label_loggingLevel As Label
    Friend WithEvents GroupBox_Chart As GroupBox
    Friend WithEvents GroupBox_Logging As GroupBox
    Friend WithEvents CheckBox_logShowDebugMessages As CheckBox
    Friend WithEvents NumericUpDown_DefaultFontSize As NumericUpDown
    Friend WithEvents Label_DefaultFontSize As Label
    Friend WithEvents GroupBox_Interface As GroupBox
    Friend WithEvents Label_showOverview As Label
    Friend WithEvents CheckBox_showOverviewOnStartup As CheckBox
End Class
