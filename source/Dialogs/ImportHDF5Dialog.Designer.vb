<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportHDF5Dialog
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportHDF5Dialog))
        Me.Label_FileName = New System.Windows.Forms.Label()
        Me.Label_Elements = New System.Windows.Forms.Label()
        Me.ListBox_Elements = New System.Windows.Forms.ListBox()
        Me.Button_SelectAllElements = New System.Windows.Forms.Button()
        Me.Button_SelectNoneElements = New System.Windows.Forms.Button()
        Me.Label_Variables = New System.Windows.Forms.Label()
        Me.CheckedListBox_Variables = New System.Windows.Forms.CheckedListBox()
        Me.Button_SelectAllVariables = New System.Windows.Forms.Button()
        Me.Button_SelectNoneVariables = New System.Windows.Forms.Button()
        Me.Label_TitleSuffix = New System.Windows.Forms.Label()
        Me.TextBox_TitleSuffix = New System.Windows.Forms.TextBox()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label_FileName
        '
        Me.Label_FileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_FileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label_FileName.Location = New System.Drawing.Point(10, 10)
        Me.Label_FileName.Name = "Label_FileName"
        Me.Label_FileName.Size = New System.Drawing.Size(760, 20)
        Me.Label_FileName.TabIndex = 0
        '
        'Label_Elements
        '
        Me.Label_Elements.Location = New System.Drawing.Point(10, 40)
        Me.Label_Elements.Name = "Label_Elements"
        Me.Label_Elements.Size = New System.Drawing.Size(350, 20)
        Me.Label_Elements.TabIndex = 1
        Me.Label_Elements.Text = "Elements:"
        '
        'ListBox_Elements
        '
        Me.ListBox_Elements.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Elements.Location = New System.Drawing.Point(10, 65)
        Me.ListBox_Elements.Name = "ListBox_Elements"
        Me.ListBox_Elements.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_Elements.Size = New System.Drawing.Size(350, 368)
        Me.ListBox_Elements.TabIndex = 2
        '
        'Button_SelectAllElements
        '
        Me.Button_SelectAllElements.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectAllElements.Location = New System.Drawing.Point(10, 450)
        Me.Button_SelectAllElements.Name = "Button_SelectAllElements"
        Me.Button_SelectAllElements.Size = New System.Drawing.Size(80, 23)
        Me.Button_SelectAllElements.TabIndex = 3
        Me.Button_SelectAllElements.Text = "Select All"
        '
        'Button_SelectNoneElements
        '
        Me.Button_SelectNoneElements.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectNoneElements.Location = New System.Drawing.Point(95, 450)
        Me.Button_SelectNoneElements.Name = "Button_SelectNoneElements"
        Me.Button_SelectNoneElements.Size = New System.Drawing.Size(80, 23)
        Me.Button_SelectNoneElements.TabIndex = 4
        Me.Button_SelectNoneElements.Text = "Select None"
        '
        'Label_Variables
        '
        Me.Label_Variables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Variables.Location = New System.Drawing.Point(380, 40)
        Me.Label_Variables.Name = "Label_Variables"
        Me.Label_Variables.Size = New System.Drawing.Size(390, 20)
        Me.Label_Variables.TabIndex = 5
        Me.Label_Variables.Text = "Variables:"
        '
        'CheckedListBox_Variables
        '
        Me.CheckedListBox_Variables.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBox_Variables.CheckOnClick = True
        Me.CheckedListBox_Variables.Location = New System.Drawing.Point(380, 65)
        Me.CheckedListBox_Variables.Name = "CheckedListBox_Variables"
        Me.CheckedListBox_Variables.Size = New System.Drawing.Size(390, 364)
        Me.CheckedListBox_Variables.TabIndex = 6
        '
        'Button_SelectAllVariables
        '
        Me.Button_SelectAllVariables.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectAllVariables.Location = New System.Drawing.Point(380, 450)
        Me.Button_SelectAllVariables.Name = "Button_SelectAllVariables"
        Me.Button_SelectAllVariables.Size = New System.Drawing.Size(80, 23)
        Me.Button_SelectAllVariables.TabIndex = 7
        Me.Button_SelectAllVariables.Text = "Select All"
        '
        'Button_SelectNoneVariables
        '
        Me.Button_SelectNoneVariables.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectNoneVariables.Location = New System.Drawing.Point(465, 450)
        Me.Button_SelectNoneVariables.Name = "Button_SelectNoneVariables"
        Me.Button_SelectNoneVariables.Size = New System.Drawing.Size(80, 23)
        Me.Button_SelectNoneVariables.TabIndex = 8
        Me.Button_SelectNoneVariables.Text = "Select None"
        '
        'Label_TitleSuffix
        '
        Me.Label_TitleSuffix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_TitleSuffix.Location = New System.Drawing.Point(10, 485)
        Me.Label_TitleSuffix.Name = "Label_TitleSuffix"
        Me.Label_TitleSuffix.Size = New System.Drawing.Size(80, 20)
        Me.Label_TitleSuffix.TabIndex = 9
        Me.Label_TitleSuffix.Text = "Title Suffix:"
        '
        'TextBox_TitleSuffix
        '
        Me.TextBox_TitleSuffix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_TitleSuffix.Location = New System.Drawing.Point(95, 482)
        Me.TextBox_TitleSuffix.Name = "TextBox_TitleSuffix"
        Me.TextBox_TitleSuffix.Size = New System.Drawing.Size(300, 20)
        Me.TextBox_TitleSuffix.TabIndex = 10
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(612, 526)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 11
        Me.Button_OK.Text = "OK"
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(697, 526)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 12
        Me.Button_Cancel.Text = "Cancel"
        '
        'ImportHDF5Dialog
        '
        Me.AcceptButton = Me.Button_OK
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.Label_FileName)
        Me.Controls.Add(Me.Label_Elements)
        Me.Controls.Add(Me.ListBox_Elements)
        Me.Controls.Add(Me.Button_SelectAllElements)
        Me.Controls.Add(Me.Button_SelectNoneElements)
        Me.Controls.Add(Me.Label_Variables)
        Me.Controls.Add(Me.CheckedListBox_Variables)
        Me.Controls.Add(Me.Button_SelectAllVariables)
        Me.Controls.Add(Me.Button_SelectNoneVariables)
        Me.Controls.Add(Me.Label_TitleSuffix)
        Me.Controls.Add(Me.TextBox_TitleSuffix)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "ImportHDF5Dialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import series from HDF5 file"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents ListBox_Elements As ListBox
    Private WithEvents CheckedListBox_Variables As CheckedListBox
    Private Label_Elements As Label
    Private Label_Variables As Label
    Private Label_FileName As Label
    Private WithEvents Button_OK As Button
    Private Button_Cancel As Button
    Private WithEvents Button_SelectAllElements As Button
    Private WithEvents Button_SelectNoneElements As Button
    Private WithEvents Button_SelectAllVariables As Button
    Private WithEvents Button_SelectNoneVariables As Button
    Private TextBox_TitleSuffix As TextBox
    Private Label_TitleSuffix As Label

End Class
