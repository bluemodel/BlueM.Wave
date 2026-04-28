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
        Button_Close = New Button()
        NumericUpDown_DefaultLineWidth = New NumericUpDown()
        Label_defaultLineWidth = New Label()
        Label_loggingLevel = New Label()
        GroupBox_Chart = New GroupBox()
        NumericUpDown_DefaultFontSize = New NumericUpDown()
        Label_DefaultFontSize = New Label()
        GroupBox_Logging = New GroupBox()
        CheckBox_logShowDebugMessages = New CheckBox()
        GroupBox_Interface = New GroupBox()
        Label_showOverview = New Label()
        CheckBox_showOverviewOnStartup = New CheckBox()
        CType(NumericUpDown_DefaultLineWidth, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox_Chart.SuspendLayout()
        CType(NumericUpDown_DefaultFontSize, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox_Logging.SuspendLayout()
        GroupBox_Interface.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button_Close
        ' 
        Button_Close.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Close.Location = New Point(181, 320)
        Button_Close.Name = "Button_Close"
        Button_Close.Size = New Size(80, 27)
        Button_Close.TabIndex = 2
        Button_Close.Text = "Close"
        ' 
        ' NumericUpDown_DefaultLineWidth
        ' 
        NumericUpDown_DefaultLineWidth.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        NumericUpDown_DefaultLineWidth.Location = New Point(154, 52)
        NumericUpDown_DefaultLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        NumericUpDown_DefaultLineWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_DefaultLineWidth.Name = "NumericUpDown_DefaultLineWidth"
        NumericUpDown_DefaultLineWidth.Size = New Size(87, 23)
        NumericUpDown_DefaultLineWidth.TabIndex = 3
        NumericUpDown_DefaultLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label_defaultLineWidth
        ' 
        Label_defaultLineWidth.AutoSize = True
        Label_defaultLineWidth.Location = New Point(6, 54)
        Label_defaultLineWidth.Name = "Label_defaultLineWidth"
        Label_defaultLineWidth.Size = New Size(135, 15)
        Label_defaultLineWidth.TabIndex = 2
        Label_defaultLineWidth.Text = "Default series line width:"
        ' 
        ' Label_loggingLevel
        ' 
        Label_loggingLevel.AutoSize = True
        Label_loggingLevel.Location = New Point(6, 25)
        Label_loggingLevel.Name = "Label_loggingLevel"
        Label_loggingLevel.Size = New Size(130, 15)
        Label_loggingLevel.TabIndex = 0
        Label_loggingLevel.Text = "Show debug messages:"
        ' 
        ' GroupBox_Chart
        ' 
        GroupBox_Chart.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Chart.Controls.Add(NumericUpDown_DefaultFontSize)
        GroupBox_Chart.Controls.Add(Label_DefaultFontSize)
        GroupBox_Chart.Controls.Add(Label_defaultLineWidth)
        GroupBox_Chart.Controls.Add(NumericUpDown_DefaultLineWidth)
        GroupBox_Chart.Location = New Point(14, 81)
        GroupBox_Chart.Name = "GroupBox_Chart"
        GroupBox_Chart.Size = New Size(247, 90)
        GroupBox_Chart.TabIndex = 0
        GroupBox_Chart.TabStop = False
        GroupBox_Chart.Text = "Chart"
        ' 
        ' NumericUpDown_DefaultFontSize
        ' 
        NumericUpDown_DefaultFontSize.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        NumericUpDown_DefaultFontSize.Location = New Point(154, 22)
        NumericUpDown_DefaultFontSize.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        NumericUpDown_DefaultFontSize.Minimum = New Decimal(New Integer() {6, 0, 0, 0})
        NumericUpDown_DefaultFontSize.Name = "NumericUpDown_DefaultFontSize"
        NumericUpDown_DefaultFontSize.Size = New Size(87, 23)
        NumericUpDown_DefaultFontSize.TabIndex = 1
        NumericUpDown_DefaultFontSize.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' Label_DefaultFontSize
        ' 
        Label_DefaultFontSize.AutoSize = True
        Label_DefaultFontSize.Location = New Point(6, 24)
        Label_DefaultFontSize.Name = "Label_DefaultFontSize"
        Label_DefaultFontSize.Size = New Size(95, 15)
        Label_DefaultFontSize.TabIndex = 0
        Label_DefaultFontSize.Text = "Default font size:"
        ' 
        ' GroupBox_Logging
        ' 
        GroupBox_Logging.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Logging.Controls.Add(Label_loggingLevel)
        GroupBox_Logging.Controls.Add(CheckBox_logShowDebugMessages)
        GroupBox_Logging.Location = New Point(14, 178)
        GroupBox_Logging.Name = "GroupBox_Logging"
        GroupBox_Logging.Size = New Size(247, 60)
        GroupBox_Logging.TabIndex = 1
        GroupBox_Logging.TabStop = False
        GroupBox_Logging.Text = "Logging"
        ' 
        ' CheckBox_logShowDebugMessages
        ' 
        CheckBox_logShowDebugMessages.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        CheckBox_logShowDebugMessages.AutoSize = True
        CheckBox_logShowDebugMessages.Location = New Point(226, 25)
        CheckBox_logShowDebugMessages.Name = "CheckBox_logShowDebugMessages"
        CheckBox_logShowDebugMessages.Size = New Size(15, 14)
        CheckBox_logShowDebugMessages.TabIndex = 1
        CheckBox_logShowDebugMessages.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_Interface
        ' 
        GroupBox_Interface.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Interface.Controls.Add(Label_showOverview)
        GroupBox_Interface.Controls.Add(CheckBox_showOverviewOnStartup)
        GroupBox_Interface.Location = New Point(14, 14)
        GroupBox_Interface.Name = "GroupBox_Interface"
        GroupBox_Interface.Size = New Size(246, 60)
        GroupBox_Interface.TabIndex = 3
        GroupBox_Interface.TabStop = False
        GroupBox_Interface.Text = "Interface"
        ' 
        ' Label_showOverview
        ' 
        Label_showOverview.AutoSize = True
        Label_showOverview.Location = New Point(8, 23)
        Label_showOverview.Name = "Label_showOverview"
        Label_showOverview.Size = New Size(176, 15)
        Label_showOverview.TabIndex = 1
        Label_showOverview.Text = "Show overview chart on startup:"
        ' 
        ' CheckBox_showOverviewOnStartup
        ' 
        CheckBox_showOverviewOnStartup.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        CheckBox_showOverviewOnStartup.AutoSize = True
        CheckBox_showOverviewOnStartup.Location = New Point(224, 23)
        CheckBox_showOverviewOnStartup.Name = "CheckBox_showOverviewOnStartup"
        CheckBox_showOverviewOnStartup.Size = New Size(15, 14)
        CheckBox_showOverviewOnStartup.TabIndex = 0
        CheckBox_showOverviewOnStartup.UseVisualStyleBackColor = True
        ' 
        ' SettingsDialog
        ' 
        AcceptButton = Button_Close
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(273, 359)
        Controls.Add(GroupBox_Interface)
        Controls.Add(Button_Close)
        Controls.Add(GroupBox_Logging)
        Controls.Add(GroupBox_Chart)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(289, 398)
        Name = "SettingsDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Settings"
        TopMost = True
        CType(NumericUpDown_DefaultLineWidth, ComponentModel.ISupportInitialize).EndInit()
        GroupBox_Chart.ResumeLayout(False)
        GroupBox_Chart.PerformLayout()
        CType(NumericUpDown_DefaultFontSize, ComponentModel.ISupportInitialize).EndInit()
        GroupBox_Logging.ResumeLayout(False)
        GroupBox_Logging.PerformLayout()
        GroupBox_Interface.ResumeLayout(False)
        GroupBox_Interface.PerformLayout()
        ResumeLayout(False)

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
