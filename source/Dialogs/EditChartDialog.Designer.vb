<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EditChartDialog
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditChartDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.AxisWrapperBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AxisIndexDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AutoMinDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MinDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AutoMaxDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MaxDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxisWrapperBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 274)
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
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AxisIndexDataGridViewTextBoxColumn, Me.UnitDataGridViewTextBoxColumn, Me.AutoMinDataGridViewCheckBoxColumn, Me.MinDataGridViewTextBoxColumn, Me.AutoMaxDataGridViewCheckBoxColumn, Me.MaxDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.AxisWrapperBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(13, 13)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(410, 150)
        Me.DataGridView1.TabIndex = 1
        '
        'AxisWrapperBindingSource
        '
        Me.AxisWrapperBindingSource.DataSource = GetType(BlueM.Wave.AxisWrapper)
        '
        'AxisIndexDataGridViewTextBoxColumn
        '
        Me.AxisIndexDataGridViewTextBoxColumn.DataPropertyName = "AxisIndex"
        Me.AxisIndexDataGridViewTextBoxColumn.Frozen = True
        Me.AxisIndexDataGridViewTextBoxColumn.HeaderText = "AxisIndex"
        Me.AxisIndexDataGridViewTextBoxColumn.Name = "AxisIndexDataGridViewTextBoxColumn"
        Me.AxisIndexDataGridViewTextBoxColumn.ReadOnly = True
        Me.AxisIndexDataGridViewTextBoxColumn.Visible = False
        Me.AxisIndexDataGridViewTextBoxColumn.Width = 77
        '
        'UnitDataGridViewTextBoxColumn
        '
        Me.UnitDataGridViewTextBoxColumn.DataPropertyName = "Unit"
        Me.UnitDataGridViewTextBoxColumn.Frozen = True
        Me.UnitDataGridViewTextBoxColumn.HeaderText = "Unit"
        Me.UnitDataGridViewTextBoxColumn.Name = "UnitDataGridViewTextBoxColumn"
        Me.UnitDataGridViewTextBoxColumn.Width = 51
        '
        'AutoMinDataGridViewCheckBoxColumn
        '
        Me.AutoMinDataGridViewCheckBoxColumn.DataPropertyName = "AutoMin"
        Me.AutoMinDataGridViewCheckBoxColumn.HeaderText = "AutoMin"
        Me.AutoMinDataGridViewCheckBoxColumn.Name = "AutoMinDataGridViewCheckBoxColumn"
        Me.AutoMinDataGridViewCheckBoxColumn.Width = 52
        '
        'MinDataGridViewTextBoxColumn
        '
        Me.MinDataGridViewTextBoxColumn.DataPropertyName = "Min"
        Me.MinDataGridViewTextBoxColumn.HeaderText = "Min"
        Me.MinDataGridViewTextBoxColumn.Name = "MinDataGridViewTextBoxColumn"
        Me.MinDataGridViewTextBoxColumn.Width = 49
        '
        'AutoMaxDataGridViewCheckBoxColumn
        '
        Me.AutoMaxDataGridViewCheckBoxColumn.DataPropertyName = "AutoMax"
        Me.AutoMaxDataGridViewCheckBoxColumn.HeaderText = "AutoMax"
        Me.AutoMaxDataGridViewCheckBoxColumn.Name = "AutoMaxDataGridViewCheckBoxColumn"
        Me.AutoMaxDataGridViewCheckBoxColumn.Width = 55
        '
        'MaxDataGridViewTextBoxColumn
        '
        Me.MaxDataGridViewTextBoxColumn.DataPropertyName = "Max"
        Me.MaxDataGridViewTextBoxColumn.HeaderText = "Max"
        Me.MaxDataGridViewTextBoxColumn.Name = "MaxDataGridViewTextBoxColumn"
        Me.MaxDataGridViewTextBoxColumn.Width = 52
        '
        'EditChartDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditChartDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Chart"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxisWrapperBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents AxisIndexDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents UnitDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents AutoMinDataGridViewCheckBoxColumn As DataGridViewCheckBoxColumn
    Friend WithEvents MinDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents AutoMaxDataGridViewCheckBoxColumn As DataGridViewCheckBoxColumn
    Friend WithEvents MaxDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents AxisWrapperBindingSource As BindingSource
End Class
