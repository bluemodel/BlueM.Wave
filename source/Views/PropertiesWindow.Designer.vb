<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PropertiesWindow
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PropertiesWindow))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Interpretation = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.MetadataText = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EndDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Average = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Minimum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Maximum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Volume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeSeriesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStripButton_showStatistics = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_Delete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimeSeriesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
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
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.TitleDataGridViewTextBoxColumn, Me.Interpretation, Me.UnitDataGridViewTextBoxColumn, Me.MetadataText, Me.DataSource, Me.Length, Me.StartDate, Me.EndDate, Me.Average, Me.FirstValue, Me.LastValue, Me.Minimum, Me.Maximum, Me.Sum, Me.Volume})
        Me.DataGridView1.DataSource = Me.TimeSeriesBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(13, 28)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView1.Size = New System.Drawing.Size(439, 222)
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
        'Interpretation
        '
        Me.Interpretation.DataPropertyName = "Interpretation"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Interpretation.DefaultCellStyle = DataGridViewCellStyle2
        Me.Interpretation.DisplayStyleForCurrentCellOnly = True
        Me.Interpretation.HeaderText = "Interpretation"
        Me.Interpretation.Name = "Interpretation"
        Me.Interpretation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Interpretation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Interpretation.Width = 94
        '
        'MetadataText
        '
        Me.MetadataText.DataPropertyName = "MetadataText"
        Me.MetadataText.HeaderText = "Metadata"
        Me.MetadataText.Name = "MetadataText"
        Me.MetadataText.ReadOnly = True
        '
        'Length
        '
        Me.Length.DataPropertyName = "Length"
        Me.Length.HeaderText = "Length"
        Me.Length.Name = "Length"
        Me.Length.ReadOnly = True
        Me.Length.Visible = False
        '
        'StartDate
        '
        Me.StartDate.DataPropertyName = "StartDate"
        Me.StartDate.HeaderText = "StartDate"
        Me.StartDate.Name = "StartDate"
        Me.StartDate.ReadOnly = True
        Me.StartDate.Visible = False
        '
        'EndDate
        '
        Me.EndDate.DataPropertyName = "EndDate"
        Me.EndDate.HeaderText = "EndDate"
        Me.EndDate.Name = "EndDate"
        Me.EndDate.ReadOnly = True
        Me.EndDate.Visible = False
        '
        'Average
        '
        Me.Average.DataPropertyName = "Average"
        Me.Average.HeaderText = "Average"
        Me.Average.Name = "Average"
        Me.Average.ReadOnly = True
        Me.Average.Visible = False
        '
        'FirstValue
        '
        Me.FirstValue.DataPropertyName = "FirstValue"
        Me.FirstValue.HeaderText = "FirstValue"
        Me.FirstValue.Name = "FirstValue"
        Me.FirstValue.ReadOnly = True
        Me.FirstValue.Visible = False
        '
        'LastValue
        '
        Me.LastValue.DataPropertyName = "LastValue"
        Me.LastValue.HeaderText = "LastValue"
        Me.LastValue.Name = "LastValue"
        Me.LastValue.ReadOnly = True
        Me.LastValue.Visible = False
        '
        'Minimum
        '
        Me.Minimum.DataPropertyName = "Minimum"
        Me.Minimum.HeaderText = "Minimum"
        Me.Minimum.Name = "Minimum"
        Me.Minimum.ReadOnly = True
        Me.Minimum.Visible = False
        '
        'Maximum
        '
        Me.Maximum.DataPropertyName = "Maximum"
        Me.Maximum.HeaderText = "Maximum"
        Me.Maximum.Name = "Maximum"
        Me.Maximum.ReadOnly = True
        Me.Maximum.Visible = False
        '
        'Sum
        '
        Me.Sum.DataPropertyName = "Sum"
        Me.Sum.HeaderText = "Sum"
        Me.Sum.Name = "Sum"
        Me.Sum.ReadOnly = True
        Me.Sum.Visible = False
        '
        'Volume
        '
        Me.Volume.DataPropertyName = "Volume"
        Me.Volume.HeaderText = "Volume"
        Me.Volume.Name = "Volume"
        Me.Volume.ReadOnly = True
        Me.Volume.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "DataSource"
        Me.DataGridViewTextBoxColumn1.HeaderText = "DataSource"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'TitleDataGridViewTextBoxColumn
        '
        Me.TitleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.TitleDataGridViewTextBoxColumn.DataPropertyName = "Title"
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TitleDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.TitleDataGridViewTextBoxColumn.Frozen = True
        Me.TitleDataGridViewTextBoxColumn.HeaderText = "Title"
        Me.TitleDataGridViewTextBoxColumn.Name = "TitleDataGridViewTextBoxColumn"
        Me.TitleDataGridViewTextBoxColumn.Width = 52
        '
        'UnitDataGridViewTextBoxColumn
        '
        Me.UnitDataGridViewTextBoxColumn.DataPropertyName = "Unit"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.UnitDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.UnitDataGridViewTextBoxColumn.HeaderText = "Unit"
        Me.UnitDataGridViewTextBoxColumn.Name = "UnitDataGridViewTextBoxColumn"
        Me.UnitDataGridViewTextBoxColumn.Width = 51
        '
        'DataSource
        '
        Me.DataSource.DataPropertyName = "DataSource"
        Me.DataSource.HeaderText = "DataSource"
        Me.DataSource.Name = "DataSource"
        Me.DataSource.ReadOnly = True
        '
        'TimeSeriesBindingSource
        '
        Me.TimeSeriesBindingSource.DataSource = GetType(BlueM.Wave.TimeSeries)
        '
        'ToolStripButton_showStatistics
        '
        Me.ToolStripButton_showStatistics.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton_showStatistics.CheckOnClick = True
        Me.ToolStripButton_showStatistics.Image = Global.BlueM.Wave.My.Resources.Resources.table_gear
        Me.ToolStripButton_showStatistics.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_showStatistics.Name = "ToolStripButton_showStatistics"
        Me.ToolStripButton_showStatistics.Size = New System.Drawing.Size(104, 22)
        Me.ToolStripButton_showStatistics.Text = "Show statistics"
        Me.ToolStripButton_showStatistics.ToolTipText = "Show statistics"
        '
        'ToolStripButton_Delete
        '
        Me.ToolStripButton_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Delete.Enabled = False
        Me.ToolStripButton_Delete.Image = Global.BlueM.Wave.My.Resources.Resources.chart_line_delete
        Me.ToolStripButton_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Delete.Name = "ToolStripButton_Delete"
        Me.ToolStripButton_Delete.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Delete.Text = "ToolStripButton_Delete"
        Me.ToolStripButton_Delete.ToolTipText = "Delete selected time series"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_showStatistics, Me.ToolStripButton_Delete})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(464, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'PropertiesWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 262)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 200)
        Me.Name = "PropertiesWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Time Series Properties"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimeSeriesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TimeSeriesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents TitleDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Interpretation As DataGridViewComboBoxColumn
    Friend WithEvents UnitDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents MetadataText As DataGridViewTextBoxColumn
    Friend WithEvents DataSource As DataGridViewTextBoxColumn
    Friend WithEvents Length As DataGridViewTextBoxColumn
    Friend WithEvents StartDate As DataGridViewTextBoxColumn
    Friend WithEvents EndDate As DataGridViewTextBoxColumn
    Friend WithEvents Average As DataGridViewTextBoxColumn
    Friend WithEvents FirstValue As DataGridViewTextBoxColumn
    Friend WithEvents LastValue As DataGridViewTextBoxColumn
    Friend WithEvents Minimum As DataGridViewTextBoxColumn
    Friend WithEvents Maximum As DataGridViewTextBoxColumn
    Friend WithEvents Sum As DataGridViewTextBoxColumn
    Friend WithEvents Volume As DataGridViewTextBoxColumn
    Friend WithEvents ToolStripButton_showStatistics As ToolStripButton
    Friend WithEvents ToolStripButton_Delete As ToolStripButton
    Friend WithEvents ToolStrip1 As ToolStrip
End Class
