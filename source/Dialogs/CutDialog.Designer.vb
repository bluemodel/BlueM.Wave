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
        components = New ComponentModel.Container()
        Dim Label_Start As Label
        Dim Label_End As Label
        Dim Label1 As Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CutDialog))
        Button_OK = New Button()
        Label_MinDateTime = New Label()
        Label_MaxDateTime = New Label()
        MaskedTextBox_cutEnd = New MaskedTextBox()
        MaskedTextBox_cutStart = New MaskedTextBox()
        ComboBox_RefSeries = New ComboBox()
        Button_Cancel = New Button()
        CheckBox_keepUncutSeries = New CheckBox()
        SplitContainer1 = New SplitContainer()
        GroupBox_Selection = New GroupBox()
        ListBox_Series = New ListBox()
        Button_SelectAll = New Button()
        GroupBox_Timeperiod = New GroupBox()
        GroupBox_Settings = New GroupBox()
        PictureBox_TitleSuffixHelp = New PictureBox()
        Label_TitleSuffix = New Label()
        TextBox_TitleSuffix = New TextBox()
        ToolTip1 = New ToolTip(components)
        Label_Start = New Label()
        Label_End = New Label()
        Label1 = New Label()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        GroupBox_Selection.SuspendLayout()
        GroupBox_Timeperiod.SuspendLayout()
        GroupBox_Settings.SuspendLayout()
        CType(PictureBox_TitleSuffixHelp, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label_Start
        ' 
        Label_Start.AutoSize = True
        Label_Start.Location = New Point(7, 47)
        Label_Start.Name = "Label_Start"
        Label_Start.Size = New Size(34, 15)
        Label_Start.TabIndex = 2
        Label_Start.Text = "Start:"
        ' 
        ' Label_End
        ' 
        Label_End.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label_End.AutoSize = True
        Label_End.Location = New Point(180, 47)
        Label_End.Name = "Label_End"
        Label_End.Size = New Size(30, 15)
        Label_End.TabIndex = 4
        Label_End.Text = "End:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(7, 104)
        Label1.Name = "Label1"
        Label1.Size = New Size(154, 15)
        Label1.TabIndex = 6
        Label1.Text = "Use time period from series:"
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.Location = New Point(352, 363)
        Button_OK.Margin = New Padding(4, 3, 4, 3)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 1
        Button_OK.Text = "OK"
        ' 
        ' Label_MinDateTime
        ' 
        Label_MinDateTime.AutoSize = True
        Label_MinDateTime.ForeColor = SystemColors.ControlDarkDark
        Label_MinDateTime.Location = New Point(7, 22)
        Label_MinDateTime.Name = "Label_MinDateTime"
        Label_MinDateTime.Size = New Size(39, 15)
        Label_MinDateTime.TabIndex = 0
        Label_MinDateTime.Text = "Min: -"
        ' 
        ' Label_MaxDateTime
        ' 
        Label_MaxDateTime.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label_MaxDateTime.ForeColor = SystemColors.ControlDarkDark
        Label_MaxDateTime.Location = New Point(136, 22)
        Label_MaxDateTime.Name = "Label_MaxDateTime"
        Label_MaxDateTime.Size = New Size(159, 15)
        Label_MaxDateTime.TabIndex = 1
        Label_MaxDateTime.Text = "Max: -"
        Label_MaxDateTime.TextAlign = ContentAlignment.TopRight
        ' 
        ' MaskedTextBox_cutEnd
        ' 
        MaskedTextBox_cutEnd.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        MaskedTextBox_cutEnd.Culture = New System.Globalization.CultureInfo("")
        MaskedTextBox_cutEnd.Location = New Point(184, 66)
        MaskedTextBox_cutEnd.Name = "MaskedTextBox_cutEnd"
        MaskedTextBox_cutEnd.Size = New Size(117, 23)
        MaskedTextBox_cutEnd.TabIndex = 5
        MaskedTextBox_cutEnd.ValidatingType = GetType(Date)
        ' 
        ' MaskedTextBox_cutStart
        ' 
        MaskedTextBox_cutStart.Culture = New System.Globalization.CultureInfo("")
        MaskedTextBox_cutStart.Location = New Point(8, 66)
        MaskedTextBox_cutStart.Name = "MaskedTextBox_cutStart"
        MaskedTextBox_cutStart.Size = New Size(116, 23)
        MaskedTextBox_cutStart.TabIndex = 3
        MaskedTextBox_cutStart.ValidatingType = GetType(Date)
        ' 
        ' ComboBox_RefSeries
        ' 
        ComboBox_RefSeries.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_RefSeries.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_RefSeries.FormattingEnabled = True
        ComboBox_RefSeries.Location = New Point(7, 122)
        ComboBox_RefSeries.Name = "ComboBox_RefSeries"
        ComboBox_RefSeries.Size = New Size(293, 23)
        ComboBox_RefSeries.TabIndex = 7
        ' 
        ' Button_Cancel
        ' 
        Button_Cancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Cancel.DialogResult = DialogResult.Cancel
        Button_Cancel.Location = New Point(440, 363)
        Button_Cancel.Margin = New Padding(4, 3, 4, 3)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New Size(80, 27)
        Button_Cancel.TabIndex = 2
        Button_Cancel.Text = "Cancel"
        Button_Cancel.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_keepUncutSeries
        ' 
        CheckBox_keepUncutSeries.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        CheckBox_keepUncutSeries.CheckAlign = ContentAlignment.MiddleRight
        CheckBox_keepUncutSeries.Location = New Point(7, 18)
        CheckBox_keepUncutSeries.Name = "CheckBox_keepUncutSeries"
        CheckBox_keepUncutSeries.Size = New Size(293, 23)
        CheckBox_keepUncutSeries.TabIndex = 0
        CheckBox_keepUncutSeries.Text = "Keep uncut series:"
        CheckBox_keepUncutSeries.UseVisualStyleBackColor = True
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        SplitContainer1.Location = New Point(14, 14)
        SplitContainer1.Margin = New Padding(4, 3, 4, 3)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(GroupBox_Selection)
        SplitContainer1.Panel1.Padding = New Padding(4, 3, 4, 3)
        SplitContainer1.Panel1MinSize = 180
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(GroupBox_Timeperiod)
        SplitContainer1.Panel2.Controls.Add(GroupBox_Settings)
        SplitContainer1.Panel2.Padding = New Padding(4, 3, 4, 3)
        SplitContainer1.Panel2MinSize = 300
        SplitContainer1.Size = New Size(506, 343)
        SplitContainer1.SplitterDistance = 180
        SplitContainer1.SplitterWidth = 5
        SplitContainer1.TabIndex = 0
        ' 
        ' GroupBox_Selection
        ' 
        GroupBox_Selection.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Selection.Controls.Add(ListBox_Series)
        GroupBox_Selection.Controls.Add(Button_SelectAll)
        GroupBox_Selection.Location = New Point(4, 3)
        GroupBox_Selection.Name = "GroupBox_Selection"
        GroupBox_Selection.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Selection.Size = New Size(173, 336)
        GroupBox_Selection.TabIndex = 0
        GroupBox_Selection.TabStop = False
        GroupBox_Selection.Text = "Series to cut"
        ' 
        ' ListBox_Series
        ' 
        ListBox_Series.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox_Series.FormattingEnabled = True
        ListBox_Series.Location = New Point(7, 22)
        ListBox_Series.Name = "ListBox_Series"
        ListBox_Series.SelectionMode = SelectionMode.MultiExtended
        ListBox_Series.Size = New Size(158, 274)
        ListBox_Series.TabIndex = 1
        ' 
        ' Button_SelectAll
        ' 
        Button_SelectAll.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectAll.Location = New Point(7, 303)
        Button_SelectAll.Name = "Button_SelectAll"
        Button_SelectAll.Size = New Size(80, 27)
        Button_SelectAll.TabIndex = 0
        Button_SelectAll.Text = "Select all"
        Button_SelectAll.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_Timeperiod
        ' 
        GroupBox_Timeperiod.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Timeperiod.Controls.Add(Label1)
        GroupBox_Timeperiod.Controls.Add(MaskedTextBox_cutEnd)
        GroupBox_Timeperiod.Controls.Add(Label_MinDateTime)
        GroupBox_Timeperiod.Controls.Add(ComboBox_RefSeries)
        GroupBox_Timeperiod.Controls.Add(Label_MaxDateTime)
        GroupBox_Timeperiod.Controls.Add(Label_End)
        GroupBox_Timeperiod.Controls.Add(Label_Start)
        GroupBox_Timeperiod.Controls.Add(MaskedTextBox_cutStart)
        GroupBox_Timeperiod.Location = New Point(6, 3)
        GroupBox_Timeperiod.Name = "GroupBox_Timeperiod"
        GroupBox_Timeperiod.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Timeperiod.Size = New Size(307, 153)
        GroupBox_Timeperiod.TabIndex = 0
        GroupBox_Timeperiod.TabStop = False
        GroupBox_Timeperiod.Text = "Time period to cut to"
        ' 
        ' GroupBox_Settings
        ' 
        GroupBox_Settings.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Settings.Controls.Add(PictureBox_TitleSuffixHelp)
        GroupBox_Settings.Controls.Add(Label_TitleSuffix)
        GroupBox_Settings.Controls.Add(TextBox_TitleSuffix)
        GroupBox_Settings.Controls.Add(CheckBox_keepUncutSeries)
        GroupBox_Settings.Location = New Point(6, 164)
        GroupBox_Settings.Name = "GroupBox_Settings"
        GroupBox_Settings.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Settings.Size = New Size(307, 175)
        GroupBox_Settings.TabIndex = 0
        GroupBox_Settings.TabStop = False
        GroupBox_Settings.Text = "Settings"
        ' 
        ' PictureBox_TitleSuffixHelp
        ' 
        PictureBox_TitleSuffixHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        PictureBox_TitleSuffixHelp.Image = CType(resources.GetObject("PictureBox_TitleSuffixHelp.Image"), Image)
        PictureBox_TitleSuffixHelp.Location = New Point(282, 44)
        PictureBox_TitleSuffixHelp.Name = "PictureBox_TitleSuffixHelp"
        PictureBox_TitleSuffixHelp.Size = New Size(19, 18)
        PictureBox_TitleSuffixHelp.TabIndex = 39
        PictureBox_TitleSuffixHelp.TabStop = False
        ToolTip1.SetToolTip(PictureBox_TitleSuffixHelp, "The title suffix will be appended to each cut series' title")
        ' 
        ' Label_TitleSuffix
        ' 
        Label_TitleSuffix.AutoSize = True
        Label_TitleSuffix.Location = New Point(7, 45)
        Label_TitleSuffix.Name = "Label_TitleSuffix"
        Label_TitleSuffix.Size = New Size(64, 15)
        Label_TitleSuffix.TabIndex = 37
        Label_TitleSuffix.Text = "Title suffix:"
        ' 
        ' TextBox_TitleSuffix
        ' 
        TextBox_TitleSuffix.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_TitleSuffix.Location = New Point(184, 42)
        TextBox_TitleSuffix.Name = "TextBox_TitleSuffix"
        TextBox_TitleSuffix.Size = New Size(90, 23)
        TextBox_TitleSuffix.TabIndex = 38
        TextBox_TitleSuffix.Text = " (cut)"
        ' 
        ' CutDialog
        ' 
        AcceptButton = Button_OK
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(534, 397)
        Controls.Add(SplitContainer1)
        Controls.Add(Button_OK)
        Controls.Add(Button_Cancel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(550, 398)
        Name = "CutDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Cut time series"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        GroupBox_Selection.ResumeLayout(False)
        GroupBox_Timeperiod.ResumeLayout(False)
        GroupBox_Timeperiod.PerformLayout()
        GroupBox_Settings.ResumeLayout(False)
        GroupBox_Settings.PerformLayout()
        CType(PictureBox_TitleSuffixHelp, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

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
