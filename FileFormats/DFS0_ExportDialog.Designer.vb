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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DFS0_ExportDialog))
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.OK_Button = New System.Windows.Forms.Button()
            Me.Cancel_Button = New System.Windows.Forms.Button()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Title = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.EUMItemColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.EUMUnitColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
            Me.TableLayoutPanel1.SuspendLayout()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel1.ColumnCount = 2
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(334, 171)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 1
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
            Me.TableLayoutPanel1.TabIndex = 0
            '
            'OK_Button
            '
            Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OK_Button.Location = New System.Drawing.Point(3, 3)
            Me.OK_Button.Name = "OK_Button"
            Me.OK_Button.Size = New System.Drawing.Size(67, 23)
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            '
            'Cancel_Button
            '
            Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
            Me.Cancel_Button.Name = "Cancel_Button"
            Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            '
            'DataGridView1
            '
            Me.DataGridView1.AllowUserToAddRows = False
            Me.DataGridView1.AllowUserToDeleteRows = False
            Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Title, Me.Unit, Me.EUMItemColumn, Me.EUMUnitColumn})
            Me.DataGridView1.Location = New System.Drawing.Point(13, 29)
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.Size = New System.Drawing.Size(467, 131)
            Me.DataGridView1.TabIndex = 1
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(13, 13)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(213, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Set desired EUM Item and Unit in DFS0 file:"
            '
            'Id
            '
            Me.Id.HeaderText = "ID"
            Me.Id.Name = "Id"
            Me.Id.Visible = False
            '
            'Title
            '
            Me.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.Title.HeaderText = "Title"
            Me.Title.Name = "Title"
            Me.Title.ReadOnly = True
            Me.Title.Width = 52
            '
            'Unit
            '
            Me.Unit.HeaderText = "Unit"
            Me.Unit.Name = "Unit"
            Me.Unit.ReadOnly = True
            Me.Unit.Width = 60
            '
            'EUMItemColumn
            '
            Me.EUMItemColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.EUMItemColumn.DefaultCellStyle = DataGridViewCellStyle1
            Me.EUMItemColumn.HeaderText = "EUM Item"
            Me.EUMItemColumn.Name = "EUMItemColumn"
            Me.EUMItemColumn.Width = 60
            '
            'EUMUnitColumn
            '
            Me.EUMUnitColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.EUMUnitColumn.DefaultCellStyle = DataGridViewCellStyle2
            Me.EUMUnitColumn.HeaderText = "EUM Unit"
            Me.EUMUnitColumn.Name = "EUMUnitColumn"
            Me.EUMUnitColumn.Width = 59
            '
            'DFS0_ExportDialog
            '
            Me.AcceptButton = Me.OK_Button
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel_Button
            Me.ClientSize = New System.Drawing.Size(492, 212)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.DataGridView1)
            Me.Controls.Add(Me.TableLayoutPanel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "DFS0_ExportDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "DFS0 Export"
            Me.TableLayoutPanel1.ResumeLayout(False)
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
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