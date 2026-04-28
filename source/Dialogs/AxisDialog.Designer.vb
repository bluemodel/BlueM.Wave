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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxisDialog))
        DataGridView1 = New DataGridView()
        NameColumn = New DataGridViewTextBoxColumn()
        TitleColumn = New DataGridViewTextBoxColumn()
        UnitColumn = New DataGridViewTextBoxColumn()
        AxisWrapperBindingSource = New BindingSource(components)
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(AxisWrapperBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {NameColumn, TitleColumn, UnitColumn})
        DataGridView1.DataSource = AxisWrapperBindingSource
        DataGridView1.Location = New Point(12, 12)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(483, 233)
        DataGridView1.TabIndex = 3
        ' 
        ' NameColumn
        ' 
        NameColumn.DataPropertyName = "Name"
        NameColumn.HeaderText = "Name"
        NameColumn.Name = "NameColumn"
        NameColumn.ReadOnly = True
        NameColumn.Width = 64
        ' 
        ' TitleColumn
        ' 
        TitleColumn.DataPropertyName = "Title"
        TitleColumn.HeaderText = "Title"
        TitleColumn.Name = "TitleColumn"
        TitleColumn.Width = 54
        ' 
        ' UnitColumn
        ' 
        UnitColumn.DataPropertyName = "Unit"
        UnitColumn.HeaderText = "Unit"
        UnitColumn.Name = "UnitColumn"
        UnitColumn.Width = 54
        ' 
        ' AxisWrapperBindingSource
        ' 
        AxisWrapperBindingSource.DataSource = GetType(AxisWrapper)
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.DataPropertyName = "Tag"
        DataGridViewTextBoxColumn1.HeaderText = "Tag"
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.Width = 51
        ' 
        ' AxisDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(507, 257)
        Controls.Add(DataGridView1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AxisDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Manage Axes"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(AxisWrapperBindingSource, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents AxisWrapperBindingSource As BindingSource
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents NameColumn As DataGridViewTextBoxColumn
    Friend WithEvents TitleColumn As DataGridViewTextBoxColumn
    Friend WithEvents UnitColumn As DataGridViewTextBoxColumn
End Class
