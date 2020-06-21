<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PropertiesDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PropertiesDialog))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Interpretation = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.UnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MetadataText = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LengthDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EndDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinimumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaximumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AverageDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VolumeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeSeriesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimeSeriesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.DataGridView1.CausesValidation = False
        Me.DataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.TitleDataGridViewTextBoxColumn, Me.Interpretation, Me.UnitDataGridViewTextBoxColumn, Me.MetadataText, Me.LengthDataGridViewTextBoxColumn, Me.StartDateDataGridViewTextBoxColumn, Me.EndDateDataGridViewTextBoxColumn, Me.MinimumDataGridViewTextBoxColumn, Me.MaximumDataGridViewTextBoxColumn, Me.AverageDataGridViewTextBoxColumn, Me.FirstValueDataGridViewTextBoxColumn, Me.LastValueDataGridViewTextBoxColumn, Me.SumDataGridViewTextBoxColumn, Me.VolumeDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.TimeSeriesBindingSource
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DataGridView1.Location = New System.Drawing.Point(13, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView1.Size = New System.Drawing.Size(439, 238)
        Me.DataGridView1.TabIndex = 1
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.Frozen = True
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        Me.Id.Width = 41
        '
        'TitleDataGridViewTextBoxColumn
        '
        Me.TitleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.TitleDataGridViewTextBoxColumn.DataPropertyName = "Title"
        Me.TitleDataGridViewTextBoxColumn.Frozen = True
        Me.TitleDataGridViewTextBoxColumn.HeaderText = "Title"
        Me.TitleDataGridViewTextBoxColumn.Name = "TitleDataGridViewTextBoxColumn"
        Me.TitleDataGridViewTextBoxColumn.Width = 52
        '
        'Interpretation
        '
        Me.Interpretation.DataPropertyName = "Interpretation"
        Me.Interpretation.HeaderText = "Interpretation"
        Me.Interpretation.Name = "Interpretation"
        Me.Interpretation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Interpretation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Interpretation.Width = 94
        '
        'UnitDataGridViewTextBoxColumn
        '
        Me.UnitDataGridViewTextBoxColumn.DataPropertyName = "Unit"
        Me.UnitDataGridViewTextBoxColumn.HeaderText = "Unit"
        Me.UnitDataGridViewTextBoxColumn.Name = "UnitDataGridViewTextBoxColumn"
        Me.UnitDataGridViewTextBoxColumn.Width = 51
        '
        'MetadataText
        '
        Me.MetadataText.DataPropertyName = "MetadataText"
        Me.MetadataText.HeaderText = "Metadata"
        Me.MetadataText.Name = "MetadataText"
        Me.MetadataText.ReadOnly = True
        Me.MetadataText.Width = 77
        '
        'LengthDataGridViewTextBoxColumn
        '
        Me.LengthDataGridViewTextBoxColumn.DataPropertyName = "Length"
        Me.LengthDataGridViewTextBoxColumn.HeaderText = "Length"
        Me.LengthDataGridViewTextBoxColumn.Name = "LengthDataGridViewTextBoxColumn"
        Me.LengthDataGridViewTextBoxColumn.ReadOnly = True
        Me.LengthDataGridViewTextBoxColumn.Width = 65
        '
        'StartDateDataGridViewTextBoxColumn
        '
        Me.StartDateDataGridViewTextBoxColumn.DataPropertyName = "StartDate"
        Me.StartDateDataGridViewTextBoxColumn.HeaderText = "StartDate"
        Me.StartDateDataGridViewTextBoxColumn.Name = "StartDateDataGridViewTextBoxColumn"
        Me.StartDateDataGridViewTextBoxColumn.ReadOnly = True
        Me.StartDateDataGridViewTextBoxColumn.Width = 77
        '
        'EndDateDataGridViewTextBoxColumn
        '
        Me.EndDateDataGridViewTextBoxColumn.DataPropertyName = "EndDate"
        Me.EndDateDataGridViewTextBoxColumn.HeaderText = "EndDate"
        Me.EndDateDataGridViewTextBoxColumn.Name = "EndDateDataGridViewTextBoxColumn"
        Me.EndDateDataGridViewTextBoxColumn.ReadOnly = True
        Me.EndDateDataGridViewTextBoxColumn.Width = 74
        '
        'MinimumDataGridViewTextBoxColumn
        '
        Me.MinimumDataGridViewTextBoxColumn.DataPropertyName = "Minimum"
        Me.MinimumDataGridViewTextBoxColumn.HeaderText = "Minimum"
        Me.MinimumDataGridViewTextBoxColumn.Name = "MinimumDataGridViewTextBoxColumn"
        Me.MinimumDataGridViewTextBoxColumn.ReadOnly = True
        Me.MinimumDataGridViewTextBoxColumn.Width = 73
        '
        'MaximumDataGridViewTextBoxColumn
        '
        Me.MaximumDataGridViewTextBoxColumn.DataPropertyName = "Maximum"
        Me.MaximumDataGridViewTextBoxColumn.HeaderText = "Maximum"
        Me.MaximumDataGridViewTextBoxColumn.Name = "MaximumDataGridViewTextBoxColumn"
        Me.MaximumDataGridViewTextBoxColumn.ReadOnly = True
        Me.MaximumDataGridViewTextBoxColumn.Width = 76
        '
        'AverageDataGridViewTextBoxColumn
        '
        Me.AverageDataGridViewTextBoxColumn.DataPropertyName = "Average"
        Me.AverageDataGridViewTextBoxColumn.HeaderText = "Average"
        Me.AverageDataGridViewTextBoxColumn.Name = "AverageDataGridViewTextBoxColumn"
        Me.AverageDataGridViewTextBoxColumn.ReadOnly = True
        Me.AverageDataGridViewTextBoxColumn.Width = 72
        '
        'FirstValueDataGridViewTextBoxColumn
        '
        Me.FirstValueDataGridViewTextBoxColumn.DataPropertyName = "FirstValue"
        Me.FirstValueDataGridViewTextBoxColumn.HeaderText = "FirstValue"
        Me.FirstValueDataGridViewTextBoxColumn.Name = "FirstValueDataGridViewTextBoxColumn"
        Me.FirstValueDataGridViewTextBoxColumn.ReadOnly = True
        Me.FirstValueDataGridViewTextBoxColumn.Width = 78
        '
        'LastValueDataGridViewTextBoxColumn
        '
        Me.LastValueDataGridViewTextBoxColumn.DataPropertyName = "LastValue"
        Me.LastValueDataGridViewTextBoxColumn.HeaderText = "LastValue"
        Me.LastValueDataGridViewTextBoxColumn.Name = "LastValueDataGridViewTextBoxColumn"
        Me.LastValueDataGridViewTextBoxColumn.ReadOnly = True
        Me.LastValueDataGridViewTextBoxColumn.Width = 79
        '
        'SumDataGridViewTextBoxColumn
        '
        Me.SumDataGridViewTextBoxColumn.DataPropertyName = "Sum"
        Me.SumDataGridViewTextBoxColumn.HeaderText = "Sum"
        Me.SumDataGridViewTextBoxColumn.Name = "SumDataGridViewTextBoxColumn"
        Me.SumDataGridViewTextBoxColumn.ReadOnly = True
        Me.SumDataGridViewTextBoxColumn.Width = 53
        '
        'VolumeDataGridViewTextBoxColumn
        '
        Me.VolumeDataGridViewTextBoxColumn.DataPropertyName = "Volume"
        Me.VolumeDataGridViewTextBoxColumn.HeaderText = "Volume"
        Me.VolumeDataGridViewTextBoxColumn.Name = "VolumeDataGridViewTextBoxColumn"
        Me.VolumeDataGridViewTextBoxColumn.ReadOnly = True
        Me.VolumeDataGridViewTextBoxColumn.Width = 67
        '
        'TimeSeriesBindingSource
        '
        Me.TimeSeriesBindingSource.DataSource = GetType(BlueM.Wave.TimeSeries)
        '
        'PropertiesDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 262)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 200)
        Me.Name = "PropertiesDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Time Series Properties"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimeSeriesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TimeSeriesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents TitleDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Interpretation As DataGridViewComboBoxColumn
    Friend WithEvents UnitDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MetadataText As DataGridViewTextBoxColumn
    Friend WithEvents LengthDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents StartDateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EndDateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MinimumDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MaximumDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents AverageDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents FirstValueDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LastValueDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SumDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents VolumeDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
