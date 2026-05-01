<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SelectSeriesDialog
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectSeriesDialog))
        GroupBox_Selection = New GroupBox()
        PictureBox_SearchHelp = New PictureBox()
        Label_Selected = New Label()
        Label_Search = New Label()
        TextBox_Search = New TextBox()
        Label_Series = New Label()
        ListBox_Series = New ListBox()
        Button_SelectAll = New Button()
        Button_OK = New Button()
        Button_Cancel = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label_FileName = New Label()
        Label_FileType = New Label()
        ToolTip1 = New ToolTip(components)
        PictureBox_TitleSuffixHelp = New PictureBox()
        TextBox_TitleSuffix = New TextBox()
        Label_TitleSuffix = New Label()
        GroupBox_Selection.SuspendLayout()
        CType(PictureBox_SearchHelp, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox_TitleSuffixHelp, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' GroupBox_Selection
        ' 
        GroupBox_Selection.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Selection.Controls.Add(PictureBox_SearchHelp)
        GroupBox_Selection.Controls.Add(Label_Selected)
        GroupBox_Selection.Controls.Add(Label_Search)
        GroupBox_Selection.Controls.Add(TextBox_Search)
        GroupBox_Selection.Controls.Add(Label_Series)
        GroupBox_Selection.Controls.Add(ListBox_Series)
        GroupBox_Selection.Controls.Add(Button_SelectAll)
        GroupBox_Selection.Location = New Point(14, 61)
        GroupBox_Selection.Name = "GroupBox_Selection"
        GroupBox_Selection.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Selection.Size = New Size(255, 343)
        GroupBox_Selection.TabIndex = 4
        GroupBox_Selection.TabStop = False
        GroupBox_Selection.Text = "Series selection"
        ' 
        ' PictureBox_SearchHelp
        ' 
        PictureBox_SearchHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        PictureBox_SearchHelp.Image = CType(resources.GetObject("PictureBox_SearchHelp.Image"), Image)
        PictureBox_SearchHelp.Location = New Point(230, 22)
        PictureBox_SearchHelp.Name = "PictureBox_SearchHelp"
        PictureBox_SearchHelp.Size = New Size(19, 18)
        PictureBox_SearchHelp.TabIndex = 7
        PictureBox_SearchHelp.TabStop = False
        ToolTip1.SetToolTip(PictureBox_SearchHelp, "Wildcards: * matches any number of characters, ? matches a single character")
        ' 
        ' Label_Selected
        ' 
        Label_Selected.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label_Selected.Location = New Point(161, 308)
        Label_Selected.Name = "Label_Selected"
        Label_Selected.Size = New Size(88, 15)
        Label_Selected.TabIndex = 5
        Label_Selected.Text = "0 selected"
        Label_Selected.TextAlign = ContentAlignment.TopRight
        ' 
        ' Label_Search
        ' 
        Label_Search.AutoSize = True
        Label_Search.Location = New Point(14, 24)
        Label_Search.Name = "Label_Search"
        Label_Search.Size = New Size(45, 15)
        Label_Search.TabIndex = 0
        Label_Search.Text = "Search:"
        ' 
        ' TextBox_Search
        ' 
        TextBox_Search.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_Search.Location = New Point(69, 21)
        TextBox_Search.Name = "TextBox_Search"
        TextBox_Search.Size = New Size(153, 23)
        TextBox_Search.TabIndex = 1
        ' 
        ' Label_Series
        ' 
        Label_Series.AutoSize = True
        Label_Series.Location = New Point(14, 53)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New Size(90, 15)
        Label_Series.TabIndex = 2
        Label_Series.Text = "Available series:"
        ' 
        ' ListBox_Series
        ' 
        ListBox_Series.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox_Series.FormattingEnabled = True
        ListBox_Series.Location = New Point(18, 74)
        ListBox_Series.Name = "ListBox_Series"
        ListBox_Series.SelectionMode = SelectionMode.MultiExtended
        ListBox_Series.Size = New Size(230, 214)
        ListBox_Series.TabIndex = 3
        ' 
        ' Button_SelectAll
        ' 
        Button_SelectAll.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectAll.Location = New Point(18, 302)
        Button_SelectAll.Name = "Button_SelectAll"
        Button_SelectAll.Size = New Size(85, 27)
        Button_SelectAll.TabIndex = 4
        Button_SelectAll.Text = "Select all"
        Button_SelectAll.UseVisualStyleBackColor = True
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(103, 446)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 7
        Button_OK.Text = "OK"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' Button_Cancel
        ' 
        Button_Cancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Cancel.DialogResult = DialogResult.Cancel
        Button_Cancel.Location = New Point(189, 446)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New Size(80, 27)
        Button_Cancel.TabIndex = 8
        Button_Cancel.Text = "Cancel"
        Button_Cancel.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(64, 15)
        Label1.TabIndex = 0
        Label1.Text = "File name: "
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(14, 35)
        Label2.Name = "Label2"
        Label2.Size = New Size(57, 15)
        Label2.TabIndex = 2
        Label2.Text = "File type: "
        ' 
        ' Label_FileName
        ' 
        Label_FileName.AutoSize = True
        Label_FileName.Location = New Point(79, 15)
        Label_FileName.Name = "Label_FileName"
        Label_FileName.Size = New Size(58, 15)
        Label_FileName.TabIndex = 1
        Label_FileName.Text = "File name"
        ' 
        ' Label_FileType
        ' 
        Label_FileType.AutoSize = True
        Label_FileType.Location = New Point(79, 35)
        Label_FileType.Name = "Label_FileType"
        Label_FileType.Size = New Size(51, 15)
        Label_FileType.TabIndex = 3
        Label_FileType.Text = "File type"
        ' 
        ' PictureBox_TitleSuffixHelp
        ' 
        PictureBox_TitleSuffixHelp.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        PictureBox_TitleSuffixHelp.Image = CType(resources.GetObject("PictureBox_TitleSuffixHelp.Image"), Image)
        PictureBox_TitleSuffixHelp.Location = New Point(251, 414)
        PictureBox_TitleSuffixHelp.Name = "PictureBox_TitleSuffixHelp"
        PictureBox_TitleSuffixHelp.Size = New Size(19, 18)
        PictureBox_TitleSuffixHelp.TabIndex = 8
        PictureBox_TitleSuffixHelp.TabStop = False
        ToolTip1.SetToolTip(PictureBox_TitleSuffixHelp, "The title suffix will be appended to each series' title during import")
        ' 
        ' TextBox_TitleSuffix
        ' 
        TextBox_TitleSuffix.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_TitleSuffix.Location = New Point(88, 411)
        TextBox_TitleSuffix.Name = "TextBox_TitleSuffix"
        TextBox_TitleSuffix.Size = New Size(156, 23)
        TextBox_TitleSuffix.TabIndex = 6
        ' 
        ' Label_TitleSuffix
        ' 
        Label_TitleSuffix.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label_TitleSuffix.AutoSize = True
        Label_TitleSuffix.Location = New Point(14, 414)
        Label_TitleSuffix.Name = "Label_TitleSuffix"
        Label_TitleSuffix.Size = New Size(64, 15)
        Label_TitleSuffix.TabIndex = 5
        Label_TitleSuffix.Text = "Title suffix:"
        ' 
        ' SelectSeriesDialog
        ' 
        AcceptButton = Button_OK
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Button_Cancel
        ClientSize = New Size(279, 485)
        Controls.Add(PictureBox_TitleSuffixHelp)
        Controls.Add(Label_TitleSuffix)
        Controls.Add(TextBox_TitleSuffix)
        Controls.Add(Label_FileType)
        Controls.Add(Label_FileName)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(GroupBox_Selection)
        Controls.Add(Button_OK)
        Controls.Add(Button_Cancel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(254, 363)
        Name = "SelectSeriesDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Import series from file"
        TopMost = True
        GroupBox_Selection.ResumeLayout(False)
        GroupBox_Selection.PerformLayout()
        CType(PictureBox_SearchHelp, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox_TitleSuffixHelp, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents ListBox_Series As System.Windows.Forms.ListBox
    Private WithEvents Label_Search As System.Windows.Forms.Label
    Private WithEvents TextBox_Search As System.Windows.Forms.TextBox
    Private WithEvents Label_Series As System.Windows.Forms.Label
    Friend WithEvents Button_SelectAll As System.Windows.Forms.Button
    Friend WithEvents GroupBox_Selection As System.Windows.Forms.GroupBox
    Friend WithEvents Label_Selected As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label_FileName As Label
    Friend WithEvents Label_FileType As Label
    Friend WithEvents PictureBox_SearchHelp As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TextBox_TitleSuffix As TextBox
    Friend WithEvents Label_TitleSuffix As Label
    Friend WithEvents PictureBox_TitleSuffixHelp As PictureBox
End Class
