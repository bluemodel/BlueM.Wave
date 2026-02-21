<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CutDialog
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
        Me.components = New System.ComponentModel.Container()
        Dim Label_Start As System.Windows.Forms.Label
        Dim Label_End As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CutDialog))
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Label_MinDateTime = New System.Windows.Forms.Label()
        Me.Label_MaxDateTime = New System.Windows.Forms.Label()
        Me.MaskedTextBox_cutEnd = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox_cutStart = New System.Windows.Forms.MaskedTextBox()
        Me.ComboBox_RefSeries = New System.Windows.Forms.ComboBox()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.CheckBox_keepUncutSeries = New System.Windows.Forms.CheckBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox_Selection = New System.Windows.Forms.GroupBox()
        Me.ListBox_Series = New System.Windows.Forms.ListBox()
        Me.Button_SelectAll = New System.Windows.Forms.Button()
        Me.GroupBox_Timeperiod = New System.Windows.Forms.GroupBox()
        Me.GroupBox_Settings = New System.Windows.Forms.GroupBox()
        Me.Label_TitleSuffix = New System.Windows.Forms.Label()
        Me.TextBox_TitleSuffix = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox_TitleSuffixHelp = New System.Windows.Forms.PictureBox()
        Label_Start = New System.Windows.Forms.Label()
        Label_End = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox_Selection.SuspendLayout()
        Me.GroupBox_Timeperiod.SuspendLayout()
        Me.GroupBox_Settings.SuspendLayout()
        CType(Me.PictureBox_TitleSuffixHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_Start
        '
        Label_Start.AutoSize = True
        Label_Start.Location = New System.Drawing.Point(6, 41)
        Label_Start.Name = "Label_Start"
        Label_Start.Size = New System.Drawing.Size(32, 13)
        Label_Start.TabIndex = 2
        Label_Start.Text = "Start:"
        '
        'Label_End
        '
        Label_End.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label_End.AutoSize = True
        Label_End.Location = New System.Drawing.Point(302, 41)
        Label_End.Name = "Label_End"
        Label_End.Size = New System.Drawing.Size(29, 13)
        Label_End.TabIndex = 4
        Label_End.Text = "End:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(6, 90)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(136, 13)
        Label1.TabIndex = 6
        Label1.Text = "Use time period from series:"
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.Location = New System.Drawing.Point(470, 315)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(67, 23)
        Me.Button_OK.TabIndex = 1
        Me.Button_OK.Text = "OK"
        '
        'Label_MinDateTime
        '
        Me.Label_MinDateTime.AutoSize = True
        Me.Label_MinDateTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_MinDateTime.Location = New System.Drawing.Point(6, 19)
        Me.Label_MinDateTime.Name = "Label_MinDateTime"
        Me.Label_MinDateTime.Size = New System.Drawing.Size(33, 13)
        Me.Label_MinDateTime.TabIndex = 0
        Me.Label_MinDateTime.Text = "Min: -"
        '
        'Label_MaxDateTime
        '
        Me.Label_MaxDateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_MaxDateTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_MaxDateTime.Location = New System.Drawing.Point(264, 19)
        Me.Label_MaxDateTime.Name = "Label_MaxDateTime"
        Me.Label_MaxDateTime.Size = New System.Drawing.Size(136, 13)
        Me.Label_MaxDateTime.TabIndex = 1
        Me.Label_MaxDateTime.Text = "Max: -"
        Me.Label_MaxDateTime.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'MaskedTextBox_cutEnd
        '
        Me.MaskedTextBox_cutEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MaskedTextBox_cutEnd.Culture = New System.Globalization.CultureInfo("")
        Me.MaskedTextBox_cutEnd.Location = New System.Drawing.Point(305, 57)
        Me.MaskedTextBox_cutEnd.Name = "MaskedTextBox_cutEnd"
        Me.MaskedTextBox_cutEnd.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox_cutEnd.TabIndex = 5
        Me.MaskedTextBox_cutEnd.ValidatingType = GetType(Date)
        '
        'MaskedTextBox_cutStart
        '
        Me.MaskedTextBox_cutStart.Culture = New System.Globalization.CultureInfo("")
        Me.MaskedTextBox_cutStart.Location = New System.Drawing.Point(7, 57)
        Me.MaskedTextBox_cutStart.Name = "MaskedTextBox_cutStart"
        Me.MaskedTextBox_cutStart.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox_cutStart.TabIndex = 3
        Me.MaskedTextBox_cutStart.ValidatingType = GetType(Date)
        '
        'ComboBox_RefSeries
        '
        Me.ComboBox_RefSeries.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_RefSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_RefSeries.FormattingEnabled = True
        Me.ComboBox_RefSeries.Location = New System.Drawing.Point(6, 106)
        Me.ComboBox_RefSeries.Name = "ComboBox_RefSeries"
        Me.ComboBox_RefSeries.Size = New System.Drawing.Size(399, 21)
        Me.ComboBox_RefSeries.TabIndex = 7
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(543, 315)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 2
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'CheckBox_keepUncutSeries
        '
        Me.CheckBox_keepUncutSeries.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_keepUncutSeries.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox_keepUncutSeries.Location = New System.Drawing.Point(6, 16)
        Me.CheckBox_keepUncutSeries.Name = "CheckBox_keepUncutSeries"
        Me.CheckBox_keepUncutSeries.Size = New System.Drawing.Size(399, 20)
        Me.CheckBox_keepUncutSeries.TabIndex = 0
        Me.CheckBox_keepUncutSeries.Text = "Keep uncut series:"
        Me.CheckBox_keepUncutSeries.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 12)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox_Selection)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Panel1MinSize = 180
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox_Timeperiod)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox_Settings)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Panel2MinSize = 300
        Me.SplitContainer1.Size = New System.Drawing.Size(606, 297)
        Me.SplitContainer1.SplitterDistance = 180
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox_Selection
        '
        Me.GroupBox_Selection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Selection.Controls.Add(Me.ListBox_Series)
        Me.GroupBox_Selection.Controls.Add(Me.Button_SelectAll)
        Me.GroupBox_Selection.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Selection.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox_Selection.Name = "GroupBox_Selection"
        Me.GroupBox_Selection.Size = New System.Drawing.Size(174, 291)
        Me.GroupBox_Selection.TabIndex = 0
        Me.GroupBox_Selection.TabStop = False
        Me.GroupBox_Selection.Text = "Series to cut"
        '
        'ListBox_Series
        '
        Me.ListBox_Series.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Series.FormattingEnabled = True
        Me.ListBox_Series.Location = New System.Drawing.Point(6, 19)
        Me.ListBox_Series.Name = "ListBox_Series"
        Me.ListBox_Series.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_Series.Size = New System.Drawing.Size(162, 238)
        Me.ListBox_Series.TabIndex = 1
        '
        'Button_SelectAll
        '
        Me.Button_SelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectAll.Location = New System.Drawing.Point(6, 263)
        Me.Button_SelectAll.Name = "Button_SelectAll"
        Me.Button_SelectAll.Size = New System.Drawing.Size(73, 23)
        Me.Button_SelectAll.TabIndex = 0
        Me.Button_SelectAll.Text = "Select all"
        Me.Button_SelectAll.UseVisualStyleBackColor = True
        '
        'GroupBox_Timeperiod
        '
        Me.GroupBox_Timeperiod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Timeperiod.Controls.Add(Label1)
        Me.GroupBox_Timeperiod.Controls.Add(Me.MaskedTextBox_cutEnd)
        Me.GroupBox_Timeperiod.Controls.Add(Me.Label_MinDateTime)
        Me.GroupBox_Timeperiod.Controls.Add(Me.ComboBox_RefSeries)
        Me.GroupBox_Timeperiod.Controls.Add(Me.Label_MaxDateTime)
        Me.GroupBox_Timeperiod.Controls.Add(Label_End)
        Me.GroupBox_Timeperiod.Controls.Add(Label_Start)
        Me.GroupBox_Timeperiod.Controls.Add(Me.MaskedTextBox_cutStart)
        Me.GroupBox_Timeperiod.Location = New System.Drawing.Point(5, 3)
        Me.GroupBox_Timeperiod.Name = "GroupBox_Timeperiod"
        Me.GroupBox_Timeperiod.Size = New System.Drawing.Size(411, 133)
        Me.GroupBox_Timeperiod.TabIndex = 0
        Me.GroupBox_Timeperiod.TabStop = False
        Me.GroupBox_Timeperiod.Text = "Time period to cut to"
        '
        'GroupBox_Settings
        '
        Me.GroupBox_Settings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Settings.Controls.Add(Me.PictureBox_TitleSuffixHelp)
        Me.GroupBox_Settings.Controls.Add(Me.Label_TitleSuffix)
        Me.GroupBox_Settings.Controls.Add(Me.TextBox_TitleSuffix)
        Me.GroupBox_Settings.Controls.Add(Me.CheckBox_keepUncutSeries)
        Me.GroupBox_Settings.Location = New System.Drawing.Point(5, 142)
        Me.GroupBox_Settings.Name = "GroupBox_Settings"
        Me.GroupBox_Settings.Size = New System.Drawing.Size(411, 152)
        Me.GroupBox_Settings.TabIndex = 0
        Me.GroupBox_Settings.TabStop = False
        Me.GroupBox_Settings.Text = "Settings"
        '
        'Label_TitleSuffix
        '
        Me.Label_TitleSuffix.AutoSize = True
        Me.Label_TitleSuffix.Location = New System.Drawing.Point(6, 39)
        Me.Label_TitleSuffix.Name = "Label_TitleSuffix"
        Me.Label_TitleSuffix.Size = New System.Drawing.Size(57, 13)
        Me.Label_TitleSuffix.TabIndex = 37
        Me.Label_TitleSuffix.Text = "Title suffix:"
        '
        'TextBox_TitleSuffix
        '
        Me.TextBox_TitleSuffix.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_TitleSuffix.Location = New System.Drawing.Point(108, 36)
        Me.TextBox_TitleSuffix.Name = "TextBox_TitleSuffix"
        Me.TextBox_TitleSuffix.Size = New System.Drawing.Size(275, 20)
        Me.TextBox_TitleSuffix.TabIndex = 38
        Me.TextBox_TitleSuffix.Text = " (cut)"
        '
        'PictureBox_TitleSuffixHelp
        '
        Me.PictureBox_TitleSuffixHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox_TitleSuffixHelp.Image = Global.BlueM.Wave.My.Resources.Resources.help
        Me.PictureBox_TitleSuffixHelp.Location = New System.Drawing.Point(389, 38)
        Me.PictureBox_TitleSuffixHelp.Name = "PictureBox_TitleSuffixHelp"
        Me.PictureBox_TitleSuffixHelp.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox_TitleSuffixHelp.TabIndex = 39
        Me.PictureBox_TitleSuffixHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox_TitleSuffixHelp, "The title suffix will be appended to each cut series' title")
        '
        'CutDialog
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 344)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 350)
        Me.Name = "CutDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cut time series"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox_Selection.ResumeLayout(False)
        Me.GroupBox_Timeperiod.ResumeLayout(False)
        Me.GroupBox_Timeperiod.PerformLayout()
        Me.GroupBox_Settings.ResumeLayout(False)
        Me.GroupBox_Settings.PerformLayout()
        CType(Me.PictureBox_TitleSuffixHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Label_MinDateTime As System.Windows.Forms.Label
    Private WithEvents Label_MaxDateTime As System.Windows.Forms.Label
    Private WithEvents ComboBox_RefSeries As System.Windows.Forms.ComboBox
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents MaskedTextBox_cutStart As MaskedTextBox
    Friend WithEvents MaskedTextBox_cutEnd As MaskedTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents GroupBox_Selection As GroupBox
    Friend WithEvents Button_SelectAll As Button
    Friend WithEvents GroupBox_Timeperiod As GroupBox
    Friend WithEvents GroupBox_Settings As GroupBox
    Private WithEvents CheckBox_keepUncutSeries As CheckBox
    Private WithEvents ListBox_Series As ListBox
    Friend WithEvents Label_TitleSuffix As Label
    Friend WithEvents TextBox_TitleSuffix As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents PictureBox_TitleSuffixHelp As PictureBox
End Class
