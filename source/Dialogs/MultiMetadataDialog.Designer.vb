<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MultiMetadataDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MultiMetadataDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        DataGridView1 = New DataGridView()
        NameColumn = New DataGridViewTextBoxColumn()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(378, 308)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(464, 308)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {NameColumn})
        DataGridView1.Location = New Point(15, 14)
        DataGridView1.Margin = New Padding(4, 3, 4, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 10
        DataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect
        DataGridView1.Size = New Size(525, 279)
        DataGridView1.TabIndex = 1
        ' 
        ' NameColumn
        ' 
        NameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        NameColumn.DefaultCellStyle = DataGridViewCellStyle1
        NameColumn.Frozen = True
        NameColumn.HeaderText = "Series"
        NameColumn.Name = "NameColumn"
        NameColumn.ReadOnly = True
        NameColumn.Width = 62
        ' 
        ' MultiMetadataDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(554, 347)
        Controls.Add(Cancel_Button)
        Controls.Add(DataGridView1)
        Controls.Add(OK_Button)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(312, 386)
        Name = "MultiMetadataDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Edit metadata"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
End Class
