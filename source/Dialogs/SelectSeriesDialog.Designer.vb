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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectSeriesDialog))
        Me.GroupBox_Selection = New System.Windows.Forms.GroupBox()
        Me.PictureBox_SearchHelp = New System.Windows.Forms.PictureBox()
        Me.Label_Selected = New System.Windows.Forms.Label()
        Me.Label_Search = New System.Windows.Forms.Label()
        Me.TextBox_Search = New System.Windows.Forms.TextBox()
        Me.Label_Series = New System.Windows.Forms.Label()
        Me.ListBox_Series = New System.Windows.Forms.ListBox()
        Me.Button_SelectAll = New System.Windows.Forms.Button()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label_FileName = New System.Windows.Forms.Label()
        Me.Label_FileType = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TextBox_TitleSuffix = New System.Windows.Forms.TextBox()
        Me.Label_TitleSuffix = New System.Windows.Forms.Label()
        Me.GroupBox_Selection.SuspendLayout()
        CType(Me.PictureBox_SearchHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox_Selection
        '
        Me.GroupBox_Selection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Selection.Controls.Add(Me.PictureBox_SearchHelp)
        Me.GroupBox_Selection.Controls.Add(Me.Label_Selected)
        Me.GroupBox_Selection.Controls.Add(Me.Label_Search)
        Me.GroupBox_Selection.Controls.Add(Me.TextBox_Search)
        Me.GroupBox_Selection.Controls.Add(Me.Label_Series)
        Me.GroupBox_Selection.Controls.Add(Me.ListBox_Series)
        Me.GroupBox_Selection.Controls.Add(Me.Button_SelectAll)
        Me.GroupBox_Selection.Location = New System.Drawing.Point(12, 53)
        Me.GroupBox_Selection.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox_Selection.Name = "GroupBox_Selection"
        Me.GroupBox_Selection.Size = New System.Drawing.Size(219, 297)
        Me.GroupBox_Selection.TabIndex = 5
        Me.GroupBox_Selection.TabStop = False
        Me.GroupBox_Selection.Text = "Series selection"
        '
        'PictureBox_SearchHelp
        '
        Me.PictureBox_SearchHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox_SearchHelp.Image = Global.BlueM.Wave.My.Resources.Resources.help
        Me.PictureBox_SearchHelp.Location = New System.Drawing.Point(197, 19)
        Me.PictureBox_SearchHelp.Name = "PictureBox_SearchHelp"
        Me.PictureBox_SearchHelp.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox_SearchHelp.TabIndex = 7
        Me.PictureBox_SearchHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox_SearchHelp, "Wildcards: * matches any number of characters, ? matches a single character")
        '
        'Label_Selected
        '
        Me.Label_Selected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Selected.Location = New System.Drawing.Point(138, 267)
        Me.Label_Selected.Name = "Label_Selected"
        Me.Label_Selected.Size = New System.Drawing.Size(75, 13)
        Me.Label_Selected.TabIndex = 5
        Me.Label_Selected.Text = "0 selected"
        Me.Label_Selected.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label_Search
        '
        Me.Label_Search.AutoSize = True
        Me.Label_Search.Location = New System.Drawing.Point(12, 21)
        Me.Label_Search.Name = "Label_Search"
        Me.Label_Search.Size = New System.Drawing.Size(44, 13)
        Me.Label_Search.TabIndex = 0
        Me.Label_Search.Text = "Search:"
        '
        'TextBox_Search
        '
        Me.TextBox_Search.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Search.Location = New System.Drawing.Point(59, 18)
        Me.TextBox_Search.Name = "TextBox_Search"
        Me.TextBox_Search.Size = New System.Drawing.Size(132, 20)
        Me.TextBox_Search.TabIndex = 1
        '
        'Label_Series
        '
        Me.Label_Series.AutoSize = True
        Me.Label_Series.Location = New System.Drawing.Point(12, 46)
        Me.Label_Series.Name = "Label_Series"
        Me.Label_Series.Size = New System.Drawing.Size(83, 13)
        Me.Label_Series.TabIndex = 2
        Me.Label_Series.Text = "Available series:"
        '
        'ListBox_Series
        '
        Me.ListBox_Series.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Series.FormattingEnabled = True
        Me.ListBox_Series.Location = New System.Drawing.Point(15, 64)
        Me.ListBox_Series.Name = "ListBox_Series"
        Me.ListBox_Series.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_Series.Size = New System.Drawing.Size(198, 186)
        Me.ListBox_Series.TabIndex = 3
        '
        'Button_SelectAll
        '
        Me.Button_SelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectAll.Location = New System.Drawing.Point(15, 262)
        Me.Button_SelectAll.Name = "Button_SelectAll"
        Me.Button_SelectAll.Size = New System.Drawing.Size(73, 23)
        Me.Button_SelectAll.TabIndex = 4
        Me.Button_SelectAll.Text = "Select all"
        Me.Button_SelectAll.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(75, 385)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 8
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(156, 385)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 0
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "File name: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "File type: "
        '
        'Label_FileName
        '
        Me.Label_FileName.AutoSize = True
        Me.Label_FileName.Location = New System.Drawing.Point(68, 13)
        Me.Label_FileName.Name = "Label_FileName"
        Me.Label_FileName.Size = New System.Drawing.Size(52, 13)
        Me.Label_FileName.TabIndex = 1
        Me.Label_FileName.Text = "File name"
        '
        'Label_FileType
        '
        Me.Label_FileType.AutoSize = True
        Me.Label_FileType.Location = New System.Drawing.Point(68, 30)
        Me.Label_FileType.Name = "Label_FileType"
        Me.Label_FileType.Size = New System.Drawing.Size(46, 13)
        Me.Label_FileType.TabIndex = 3
        Me.Label_FileType.Text = "File type"
        '
        'TextBox_TitleSuffix
        '
        Me.TextBox_TitleSuffix.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_TitleSuffix.Location = New System.Drawing.Point(75, 356)
        Me.TextBox_TitleSuffix.Name = "TextBox_TitleSuffix"
        Me.TextBox_TitleSuffix.Size = New System.Drawing.Size(156, 20)
        Me.TextBox_TitleSuffix.TabIndex = 7
        '
        'Label_TitleSuffix
        '
        Me.Label_TitleSuffix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_TitleSuffix.AutoSize = True
        Me.Label_TitleSuffix.Location = New System.Drawing.Point(12, 359)
        Me.Label_TitleSuffix.Name = "Label_TitleSuffix"
        Me.Label_TitleSuffix.Size = New System.Drawing.Size(57, 13)
        Me.Label_TitleSuffix.TabIndex = 6
        Me.Label_TitleSuffix.Text = "Title suffix:"
        '
        'SelectSeriesDialog
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(239, 420)
        Me.Controls.Add(Me.Label_TitleSuffix)
        Me.Controls.Add(Me.TextBox_TitleSuffix)
        Me.Controls.Add(Me.Label_FileType)
        Me.Controls.Add(Me.Label_FileName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox_Selection)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(220, 320)
        Me.Name = "SelectSeriesDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import series from file"
        Me.TopMost = True
        Me.GroupBox_Selection.ResumeLayout(False)
        Me.GroupBox_Selection.PerformLayout()
        CType(Me.PictureBox_SearchHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
End Class
