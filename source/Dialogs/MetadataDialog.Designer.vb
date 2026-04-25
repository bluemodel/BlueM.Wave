<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MetadataDialog
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MetadataDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        DataGridView1 = New DataGridView()
        NameColumn = New DataGridViewTextBoxColumn()
        ValueColumn = New DataGridViewTextBoxColumn()
        Label1 = New Label()
        LabelSeries = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(132, 321)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(218, 321)
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
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {NameColumn, ValueColumn})
        DataGridView1.Location = New Point(15, 43)
        DataGridView1.Margin = New Padding(4, 3, 4, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersVisible = False
        DataGridView1.Size = New Size(279, 263)
        DataGridView1.TabIndex = 1
        ' 
        ' NameColumn
        ' 
        NameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        NameColumn.DefaultCellStyle = DataGridViewCellStyle1
        NameColumn.HeaderText = "Name"
        NameColumn.Name = "NameColumn"
        NameColumn.ReadOnly = True
        NameColumn.Width = 64
        ' 
        ' ValueColumn
        ' 
        ValueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        ValueColumn.HeaderText = "Value"
        ValueColumn.Name = "ValueColumn"
        ValueColumn.Width = 60
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(15, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(43, 15)
        Label1.TabIndex = 2
        Label1.Text = "Series: "
        ' 
        ' LabelSeries
        ' 
        LabelSeries.AutoSize = True
        LabelSeries.Location = New Point(64, 15)
        LabelSeries.Name = "LabelSeries"
        LabelSeries.Size = New Size(36, 15)
        LabelSeries.TabIndex = 3
        LabelSeries.Text = "series"
        ' 
        ' MetadataDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(308, 360)
        Controls.Add(OK_Button)
        Controls.Add(LabelSeries)
        Controls.Add(Cancel_Button)
        Controls.Add(Label1)
        Controls.Add(DataGridView1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(312, 386)
        Name = "MetadataDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Edit metadata"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents NameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents LabelSeries As Label
End Class
