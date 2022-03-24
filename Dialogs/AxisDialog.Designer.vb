<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AxisDialog
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxisDialog))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AxisWrapperBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxisWrapperBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameColumn, Me.TitleColumn, Me.UnitColumn})
        Me.DataGridView1.DataSource = Me.AxisWrapperBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(15, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(405, 199)
        Me.DataGridView1.TabIndex = 3
        '
        'NameColumn
        '
        Me.NameColumn.DataPropertyName = "Name"
        Me.NameColumn.HeaderText = "Name"
        Me.NameColumn.Name = "NameColumn"
        Me.NameColumn.ReadOnly = True
        Me.NameColumn.Width = 60
        '
        'TitleColumn
        '
        Me.TitleColumn.DataPropertyName = "Title"
        Me.TitleColumn.HeaderText = "Title"
        Me.TitleColumn.Name = "TitleColumn"
        Me.TitleColumn.Width = 52
        '
        'UnitColumn
        '
        Me.UnitColumn.DataPropertyName = "Unit"
        Me.UnitColumn.HeaderText = "Unit"
        Me.UnitColumn.Name = "UnitColumn"
        Me.UnitColumn.Width = 51
        '
        'AxisWrapperBindingSource
        '
        Me.AxisWrapperBindingSource.DataSource = GetType(BlueM.Wave.AxisWrapper)
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Tag"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Tag"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 51
        '
        'AxisDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 223)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AxisDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Axes"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxisWrapperBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents AxisWrapperBindingSource As BindingSource
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
    Friend WithEvents TitleColumn As DataGridViewTextBoxColumn
    Friend WithEvents UnitColumn As DataGridViewTextBoxColumn
End Class
