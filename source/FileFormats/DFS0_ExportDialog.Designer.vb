Namespace Fileformats
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class DFS0_ExportDialog
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
            Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DFS0_ExportDialog))
            OK_Button = New Button()
            Cancel_Button = New Button()
            DataGridView1 = New DataGridView()
            Id = New DataGridViewTextBoxColumn()
            Title = New DataGridViewTextBoxColumn()
            Unit = New DataGridViewTextBoxColumn()
            EUMItemColumn = New DataGridViewComboBoxColumn()
            EUMUnitColumn = New DataGridViewComboBoxColumn()
            Label1 = New Label()
            CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' OK_Button
            ' 
            OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            OK_Button.Location = New Point(396, 206)
            OK_Button.Name = "OK_Button"
            OK_Button.Size = New Size(80, 27)
            OK_Button.TabIndex = 0
            OK_Button.Text = "OK"
            ' 
            ' Cancel_Button
            ' 
            Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            Cancel_Button.DialogResult = DialogResult.Cancel
            Cancel_Button.Location = New Point(482, 206)
            Cancel_Button.Name = "Cancel_Button"
            Cancel_Button.Size = New Size(80, 27)
            Cancel_Button.TabIndex = 1
            Cancel_Button.Text = "Cancel"
            ' 
            ' DataGridView1
            ' 
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.AllowUserToDeleteRows = False
            DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridView1.Columns.AddRange(New DataGridViewColumn() {Id, Title, Unit, EUMItemColumn, EUMUnitColumn})
            DataGridView1.Location = New Point(15, 33)
            DataGridView1.Name = "DataGridView1"
            DataGridView1.Size = New Size(547, 151)
            DataGridView1.TabIndex = 1
            ' 
            ' Id
            ' 
            Id.HeaderText = "ID"
            Id.Name = "Id"
            Id.Visible = False
            ' 
            ' Title
            ' 
            Title.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            Title.HeaderText = "Title"
            Title.Name = "Title"
            Title.ReadOnly = True
            Title.Width = 54
            ' 
            ' Unit
            ' 
            Unit.HeaderText = "Unit"
            Unit.Name = "Unit"
            Unit.ReadOnly = True
            Unit.Width = 60
            ' 
            ' EUMItemColumn
            ' 
            EUMItemColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
            EUMItemColumn.DefaultCellStyle = DataGridViewCellStyle1
            EUMItemColumn.HeaderText = "EUM Item"
            EUMItemColumn.Name = "EUMItemColumn"
            EUMItemColumn.Width = 65
            ' 
            ' EUMUnitColumn
            ' 
            EUMUnitColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
            EUMUnitColumn.DefaultCellStyle = DataGridViewCellStyle2
            EUMUnitColumn.HeaderText = "EUM Unit"
            EUMUnitColumn.Name = "EUMUnitColumn"
            EUMUnitColumn.Width = 63
            ' 
            ' Label1
            ' 
            Label1.AutoSize = True
            Label1.Location = New Point(15, 15)
            Label1.Name = "Label1"
            Label1.Size = New Size(231, 15)
            Label1.TabIndex = 2
            Label1.Text = "Set desired EUM Item and Unit in DFS0 file:"
            ' 
            ' DFS0_ExportDialog
            ' 
            AcceptButton = OK_Button
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            CancelButton = Cancel_Button
            ClientSize = New Size(574, 245)
            Controls.Add(OK_Button)
            Controls.Add(Label1)
            Controls.Add(Cancel_Button)
            Controls.Add(DataGridView1)
            Icon = CType(resources.GetObject("$this.Icon"), Icon)
            Margin = New Padding(4, 3, 4, 3)
            MaximizeBox = False
            MinimizeBox = False
            Name = "DFS0_ExportDialog"
            ShowInTaskbar = False
            StartPosition = FormStartPosition.CenterParent
            Text = "DFS0 Export"
            CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()

        End Sub
        Friend WithEvents OK_Button As System.Windows.Forms.Button
        Friend WithEvents Cancel_Button As System.Windows.Forms.Button
        Friend WithEvents DataGridView1 As DataGridView
        Friend WithEvents Label1 As Label
        Friend WithEvents IdColumn As DataGridViewTextBoxColumn
        Friend WithEvents TitleColumn As DataGridViewTextBoxColumn
        Friend WithEvents UnitColumn As DataGridViewTextBoxColumn
        Friend WithEvents Id As DataGridViewTextBoxColumn
        Friend WithEvents Title As DataGridViewTextBoxColumn
        Friend WithEvents Unit As DataGridViewTextBoxColumn
        Friend WithEvents EUMItemColumn As DataGridViewComboBoxColumn
        Friend WithEvents EUMUnitColumn As DataGridViewComboBoxColumn
    End Class
End Namespace