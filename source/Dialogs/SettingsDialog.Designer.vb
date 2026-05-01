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
        Button_ShowFontDialog = New Button()
        ComboBox_Font = New ComboBox()
        Label_DeafultFont = New Label()
        NumericUpDown_DefaultFontSize = New NumericUpDown()
        Label_DefaultFontSize = New Label()
        GroupBox_Logging = New GroupBox()
        CheckBox_logShowDebugMessages = New CheckBox()
        GroupBox_Interface = New GroupBox()
        Label_showOverview = New Label()
        CheckBox_showOverviewOnStartup = New CheckBox()
        FontDialog1 = New FontDialog()
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
        Button_Close.Location = New Point(242, 320)
        Button_Close.Name = "Button_Close"
        Button_Close.Size = New Size(80, 27)
        Button_Close.TabIndex = 3
        Button_Close.Text = "Close"
        ' 
        ' NumericUpDown_DefaultLineWidth
        ' 
        NumericUpDown_DefaultLineWidth.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        NumericUpDown_DefaultLineWidth.Location = New Point(147, 75)
        NumericUpDown_DefaultLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        NumericUpDown_DefaultLineWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_DefaultLineWidth.Name = "NumericUpDown_DefaultLineWidth"
        NumericUpDown_DefaultLineWidth.Size = New Size(155, 23)
        NumericUpDown_DefaultLineWidth.TabIndex = 6
        NumericUpDown_DefaultLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label_defaultLineWidth
        ' 
        Label_defaultLineWidth.AutoSize = True
        Label_defaultLineWidth.Location = New Point(6, 77)
        Label_defaultLineWidth.Name = "Label_defaultLineWidth"
        Label_defaultLineWidth.Size = New Size(135, 15)
        Label_defaultLineWidth.TabIndex = 5
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
        GroupBox_Chart.Controls.Add(Button_ShowFontDialog)
        GroupBox_Chart.Controls.Add(ComboBox_Font)
        GroupBox_Chart.Controls.Add(Label_DeafultFont)
        GroupBox_Chart.Controls.Add(NumericUpDown_DefaultFontSize)
        GroupBox_Chart.Controls.Add(Label_DefaultFontSize)
        GroupBox_Chart.Controls.Add(Label_defaultLineWidth)
        GroupBox_Chart.Controls.Add(NumericUpDown_DefaultLineWidth)
        GroupBox_Chart.Location = New Point(14, 81)
        GroupBox_Chart.Name = "GroupBox_Chart"
        GroupBox_Chart.Size = New Size(308, 110)
        GroupBox_Chart.TabIndex = 1
        GroupBox_Chart.TabStop = False
        GroupBox_Chart.Text = "Chart"
        ' 
        ' Button_ShowFontDialog
        ' 
        Button_ShowFontDialog.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_ShowFontDialog.Location = New Point(277, 16)
        Button_ShowFontDialog.Name = "Button_ShowFontDialog"
        Button_ShowFontDialog.Size = New Size(25, 23)
        Button_ShowFontDialog.TabIndex = 2
        Button_ShowFontDialog.Text = "..."
        Button_ShowFontDialog.UseVisualStyleBackColor = True
        ' 
        ' ComboBox_Font
        ' 
        ComboBox_Font.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_Font.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_Font.FormattingEnabled = True
        ComboBox_Font.Location = New Point(147, 16)
        ComboBox_Font.Name = "ComboBox_Font"
        ComboBox_Font.Size = New Size(124, 23)
        ComboBox_Font.TabIndex = 1
        ' 
        ' Label_DeafultFont
        ' 
        Label_DeafultFont.AutoSize = True
        Label_DeafultFont.Location = New Point(6, 19)
        Label_DeafultFont.Name = "Label_DeafultFont"
        Label_DeafultFont.Size = New Size(73, 15)
        Label_DeafultFont.TabIndex = 0
        Label_DeafultFont.Text = "Default font:"
        ' 
        ' NumericUpDown_DefaultFontSize
        ' 
        NumericUpDown_DefaultFontSize.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        NumericUpDown_DefaultFontSize.Location = New Point(147, 45)
        NumericUpDown_DefaultFontSize.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        NumericUpDown_DefaultFontSize.Minimum = New Decimal(New Integer() {6, 0, 0, 0})
        NumericUpDown_DefaultFontSize.Name = "NumericUpDown_DefaultFontSize"
        NumericUpDown_DefaultFontSize.Size = New Size(155, 23)
        NumericUpDown_DefaultFontSize.TabIndex = 4
        NumericUpDown_DefaultFontSize.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' Label_DefaultFontSize
        ' 
        Label_DefaultFontSize.AutoSize = True
        Label_DefaultFontSize.Location = New Point(6, 47)
        Label_DefaultFontSize.Name = "Label_DefaultFontSize"
        Label_DefaultFontSize.Size = New Size(95, 15)
        Label_DefaultFontSize.TabIndex = 3
        Label_DefaultFontSize.Text = "Default font size:"
        ' 
        ' GroupBox_Logging
        ' 
        GroupBox_Logging.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Logging.Controls.Add(Label_loggingLevel)
        GroupBox_Logging.Controls.Add(CheckBox_logShowDebugMessages)
        GroupBox_Logging.Location = New Point(14, 197)
        GroupBox_Logging.Name = "GroupBox_Logging"
        GroupBox_Logging.Size = New Size(308, 60)
        GroupBox_Logging.TabIndex = 2
        GroupBox_Logging.TabStop = False
        GroupBox_Logging.Text = "Logging"
        ' 
        ' CheckBox_logShowDebugMessages
        ' 
        CheckBox_logShowDebugMessages.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        CheckBox_logShowDebugMessages.AutoSize = True
        CheckBox_logShowDebugMessages.Location = New Point(287, 25)
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
        GroupBox_Interface.Size = New Size(307, 60)
        GroupBox_Interface.TabIndex = 0
        GroupBox_Interface.TabStop = False
        GroupBox_Interface.Text = "Interface"
        ' 
        ' Label_showOverview
        ' 
        Label_showOverview.AutoSize = True
        Label_showOverview.Location = New Point(6, 23)
        Label_showOverview.Name = "Label_showOverview"
        Label_showOverview.Size = New Size(176, 15)
        Label_showOverview.TabIndex = 0
        Label_showOverview.Text = "Show overview chart on startup:"
        ' 
        ' CheckBox_showOverviewOnStartup
        ' 
        CheckBox_showOverviewOnStartup.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        CheckBox_showOverviewOnStartup.AutoSize = True
        CheckBox_showOverviewOnStartup.Location = New Point(285, 23)
        CheckBox_showOverviewOnStartup.Name = "CheckBox_showOverviewOnStartup"
        CheckBox_showOverviewOnStartup.Size = New Size(15, 14)
        CheckBox_showOverviewOnStartup.TabIndex = 1
        CheckBox_showOverviewOnStartup.UseVisualStyleBackColor = True
        ' 
        ' FontDialog1
        ' 
        FontDialog1.AllowVerticalFonts = False
        FontDialog1.FixedPitchOnly = True
        FontDialog1.ShowEffects = False
        ' 
        ' SettingsDialog
        ' 
        AcceptButton = Button_Close
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(334, 359)
        Controls.Add(GroupBox_Interface)
        Controls.Add(Button_Close)
        Controls.Add(GroupBox_Logging)
        Controls.Add(GroupBox_Chart)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(350, 398)
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
    Friend WithEvents ComboBox_Font As ComboBox
    Friend WithEvents Label_DeafultFont As Label
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents Button_ShowFontDialog As Button
End Class
