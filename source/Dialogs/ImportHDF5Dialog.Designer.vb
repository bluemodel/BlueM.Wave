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
        Label_FileName = New Label()
        Label_Elements = New Label()
        ListBox_Elements = New ListBox()
        Button_SelectAllElements = New Button()
        Button_SelectNoneElements = New Button()
        Label_Variables = New Label()
        CheckedListBox_Variables = New CheckedListBox()
        Button_SelectAllVariables = New Button()
        Button_SelectNoneVariables = New Button()
        Label_TitleSuffix = New Label()
        TextBox_TitleSuffix = New TextBox()
        Button_OK = New Button()
        Button_Cancel = New Button()
        SuspendLayout()
        ' 
        ' Label_FileName
        ' 
        Label_FileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label_FileName.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold)
        Label_FileName.Location = New Point(10, 10)
        Label_FileName.Name = "Label_FileName"
        Label_FileName.Size = New Size(760, 20)
        Label_FileName.TabIndex = 0
        Label_FileName.Text = "Filename"
        ' 
        ' Label_Elements
        ' 
        Label_Elements.Location = New Point(10, 40)
        Label_Elements.Name = "Label_Elements"
        Label_Elements.Size = New Size(350, 20)
        Label_Elements.TabIndex = 1
        Label_Elements.Text = "Elements:"
        ' 
        ' ListBox_Elements
        ' 
        ListBox_Elements.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        ListBox_Elements.Location = New Point(10, 65)
        ListBox_Elements.Name = "ListBox_Elements"
        ListBox_Elements.SelectionMode = SelectionMode.MultiExtended
        ListBox_Elements.Size = New Size(350, 364)
        ListBox_Elements.TabIndex = 2
        ' 
        ' Button_SelectAllElements
        ' 
        Button_SelectAllElements.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectAllElements.Location = New Point(12, 447)
        Button_SelectAllElements.Name = "Button_SelectAllElements"
        Button_SelectAllElements.Size = New Size(80, 27)
        Button_SelectAllElements.TabIndex = 3
        Button_SelectAllElements.Text = "Select All"
        ' 
        ' Button_SelectNoneElements
        ' 
        Button_SelectNoneElements.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectNoneElements.Location = New Point(98, 447)
        Button_SelectNoneElements.Name = "Button_SelectNoneElements"
        Button_SelectNoneElements.Size = New Size(80, 27)
        Button_SelectNoneElements.TabIndex = 4
        Button_SelectNoneElements.Text = "Select None"
        ' 
        ' Label_Variables
        ' 
        Label_Variables.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label_Variables.Location = New Point(380, 40)
        Label_Variables.Name = "Label_Variables"
        Label_Variables.Size = New Size(390, 20)
        Label_Variables.TabIndex = 5
        Label_Variables.Text = "Variables:"
        ' 
        ' CheckedListBox_Variables
        ' 
        CheckedListBox_Variables.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        CheckedListBox_Variables.CheckOnClick = True
        CheckedListBox_Variables.Location = New Point(380, 65)
        CheckedListBox_Variables.Name = "CheckedListBox_Variables"
        CheckedListBox_Variables.Size = New Size(390, 364)
        CheckedListBox_Variables.TabIndex = 6
        ' 
        ' Button_SelectAllVariables
        ' 
        Button_SelectAllVariables.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectAllVariables.Location = New Point(380, 447)
        Button_SelectAllVariables.Name = "Button_SelectAllVariables"
        Button_SelectAllVariables.Size = New Size(80, 27)
        Button_SelectAllVariables.TabIndex = 7
        Button_SelectAllVariables.Text = "Select All"
        ' 
        ' Button_SelectNoneVariables
        ' 
        Button_SelectNoneVariables.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectNoneVariables.Location = New Point(466, 447)
        Button_SelectNoneVariables.Name = "Button_SelectNoneVariables"
        Button_SelectNoneVariables.Size = New Size(80, 27)
        Button_SelectNoneVariables.TabIndex = 8
        Button_SelectNoneVariables.Text = "Select None"
        ' 
        ' Label_TitleSuffix
        ' 
        Label_TitleSuffix.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label_TitleSuffix.Location = New Point(10, 485)
        Label_TitleSuffix.Name = "Label_TitleSuffix"
        Label_TitleSuffix.Size = New Size(80, 20)
        Label_TitleSuffix.TabIndex = 9
        Label_TitleSuffix.Text = "Title Suffix:"
        ' 
        ' TextBox_TitleSuffix
        ' 
        TextBox_TitleSuffix.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TextBox_TitleSuffix.Location = New Point(95, 482)
        TextBox_TitleSuffix.Name = "TextBox_TitleSuffix"
        TextBox_TitleSuffix.Size = New Size(300, 23)
        TextBox_TitleSuffix.TabIndex = 10
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(604, 526)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 11
        Button_OK.Text = "OK"
        ' 
        ' Button_Cancel
        ' 
        Button_Cancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Cancel.DialogResult = DialogResult.Cancel
        Button_Cancel.Location = New Point(690, 526)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New Size(80, 27)
        Button_Cancel.TabIndex = 12
        Button_Cancel.Text = "Cancel"
        ' 
        ' ImportHDF5Dialog
        ' 
        AcceptButton = Button_OK
        CancelButton = Button_Cancel
        ClientSize = New Size(784, 561)
        Controls.Add(Label_FileName)
        Controls.Add(Label_Elements)
        Controls.Add(ListBox_Elements)
        Controls.Add(Button_SelectAllElements)
        Controls.Add(Button_SelectNoneElements)
        Controls.Add(Label_Variables)
        Controls.Add(CheckedListBox_Variables)
        Controls.Add(Button_SelectAllVariables)
        Controls.Add(Button_SelectNoneVariables)
        Controls.Add(Label_TitleSuffix)
        Controls.Add(TextBox_TitleSuffix)
        Controls.Add(Button_OK)
        Controls.Add(Button_Cancel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(600, 400)
        Name = "ImportHDF5Dialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Import series from HDF5 file"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()

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
