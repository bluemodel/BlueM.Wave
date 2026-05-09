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
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PropertiesWindow))
        DataGridView1 = New DataGridView()
        Id = New DataGridViewTextBoxColumn()
        TitleDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        Interpretation = New DataGridViewComboBoxColumn()
        UnitDataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        MetadataText = New DataGridViewTextBoxColumn()
        DataSource = New DataGridViewTextBoxColumn()
        Length = New DataGridViewTextBoxColumn()
        StartDate = New DataGridViewTextBoxColumn()
        EndDate = New DataGridViewTextBoxColumn()
        Average = New DataGridViewTextBoxColumn()
        FirstValue = New DataGridViewTextBoxColumn()
        LastValue = New DataGridViewTextBoxColumn()
        Minimum = New DataGridViewTextBoxColumn()
        Maximum = New DataGridViewTextBoxColumn()
        Sum = New DataGridViewTextBoxColumn()
        Volume = New DataGridViewTextBoxColumn()
        TimeSeriesBindingSource = New BindingSource(components)
        ToolStrip1 = New ToolStrip()
        ToolStripButton_showStatistics = New ToolStripButton()
        ToolStripButton_MoveUp = New ToolStripButton()
        ToolStripButton_MoveDown = New ToolStripButton()
        ToolStripButton_Delete = New ToolStripButton()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(TimeSeriesBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.CausesValidation = False
        DataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Id, TitleDataGridViewTextBoxColumn, Interpretation, UnitDataGridViewTextBoxColumn, MetadataText, DataSource, Length, StartDate, EndDate, Average, FirstValue, LastValue, Minimum, Maximum, Sum, Volume})
        DataGridView1.DataSource = TimeSeriesBindingSource
        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.Location = New Point(15, 32)
        DataGridView1.Margin = New Padding(4, 3, 4, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridView1.Size = New Size(512, 256)
        DataGridView1.TabIndex = 1
        ' 
        ' Id
        ' 
        Id.DataPropertyName = "Id"
        Id.Frozen = True
        Id.HeaderText = "Id"
        Id.Name = "Id"
        Id.ReadOnly = True
        Id.Visible = False
        Id.Width = 41
        ' 
        ' TitleDataGridViewTextBoxColumn
        ' 
        TitleDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        TitleDataGridViewTextBoxColumn.DataPropertyName = "Title"
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        TitleDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        TitleDataGridViewTextBoxColumn.Frozen = True
        TitleDataGridViewTextBoxColumn.HeaderText = "Title"
        TitleDataGridViewTextBoxColumn.Name = "TitleDataGridViewTextBoxColumn"
        TitleDataGridViewTextBoxColumn.Width = 54
        ' 
        ' Interpretation
        ' 
        Interpretation.DataPropertyName = "Interpretation"
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        Interpretation.DefaultCellStyle = DataGridViewCellStyle2
        Interpretation.DisplayStyleForCurrentCellOnly = True
        Interpretation.HeaderText = "Interpretation"
        Interpretation.Name = "Interpretation"
        Interpretation.Resizable = DataGridViewTriState.True
        Interpretation.SortMode = DataGridViewColumnSortMode.Automatic
        Interpretation.Width = 94
        ' 
        ' UnitDataGridViewTextBoxColumn
        ' 
        UnitDataGridViewTextBoxColumn.DataPropertyName = "Unit"
        DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        UnitDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        UnitDataGridViewTextBoxColumn.HeaderText = "Unit"
        UnitDataGridViewTextBoxColumn.Name = "UnitDataGridViewTextBoxColumn"
        UnitDataGridViewTextBoxColumn.Width = 51
        ' 
        ' MetadataText
        ' 
        MetadataText.DataPropertyName = "MetadataText"
        MetadataText.HeaderText = "Metadata"
        MetadataText.Name = "MetadataText"
        MetadataText.ReadOnly = True
        ' 
        ' DataSource
        ' 
        DataSource.DataPropertyName = "DataSource"
        DataSource.HeaderText = "DataSource"
        DataSource.Name = "DataSource"
        DataSource.ReadOnly = True
        ' 
        ' Length
        ' 
        Length.DataPropertyName = "Length"
        Length.HeaderText = "Length"
        Length.Name = "Length"
        Length.ReadOnly = True
        Length.Visible = False
        ' 
        ' StartDate
        ' 
        StartDate.DataPropertyName = "StartDate"
        StartDate.HeaderText = "StartDate"
        StartDate.Name = "StartDate"
        StartDate.ReadOnly = True
        StartDate.Visible = False
        ' 
        ' EndDate
        ' 
        EndDate.DataPropertyName = "EndDate"
        EndDate.HeaderText = "EndDate"
        EndDate.Name = "EndDate"
        EndDate.ReadOnly = True
        EndDate.Visible = False
        ' 
        ' Average
        ' 
        Average.DataPropertyName = "Average"
        Average.HeaderText = "Average"
        Average.Name = "Average"
        Average.ReadOnly = True
        Average.Visible = False
        ' 
        ' FirstValue
        ' 
        FirstValue.DataPropertyName = "FirstValue"
        FirstValue.HeaderText = "FirstValue"
        FirstValue.Name = "FirstValue"
        FirstValue.ReadOnly = True
        FirstValue.Visible = False
        ' 
        ' LastValue
        ' 
        LastValue.DataPropertyName = "LastValue"
        LastValue.HeaderText = "LastValue"
        LastValue.Name = "LastValue"
        LastValue.ReadOnly = True
        LastValue.Visible = False
        ' 
        ' Minimum
        ' 
        Minimum.DataPropertyName = "Minimum"
        Minimum.HeaderText = "Minimum"
        Minimum.Name = "Minimum"
        Minimum.ReadOnly = True
        Minimum.Visible = False
        ' 
        ' Maximum
        ' 
        Maximum.DataPropertyName = "Maximum"
        Maximum.HeaderText = "Maximum"
        Maximum.Name = "Maximum"
        Maximum.ReadOnly = True
        Maximum.Visible = False
        ' 
        ' Sum
        ' 
        Sum.DataPropertyName = "Sum"
        Sum.HeaderText = "Sum"
        Sum.Name = "Sum"
        Sum.ReadOnly = True
        Sum.Visible = False
        ' 
        ' Volume
        ' 
        Volume.DataPropertyName = "Volume"
        Volume.HeaderText = "Volume"
        Volume.Name = "Volume"
        Volume.ReadOnly = True
        Volume.Visible = False
        ' 
        ' TimeSeriesBindingSource
        ' 
        TimeSeriesBindingSource.DataSource = GetType(TimeSeries)
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton_showStatistics, ToolStripButton_MoveUp, ToolStripButton_MoveDown, ToolStripButton_Delete})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(541, 25)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton_showStatistics
        ' 
        ToolStripButton_showStatistics.Alignment = ToolStripItemAlignment.Right
        ToolStripButton_showStatistics.CheckOnClick = True
        ToolStripButton_showStatistics.Image = CType(resources.GetObject("ToolStripButton_showStatistics.Image"), Image)
        ToolStripButton_showStatistics.ImageTransparentColor = Color.Magenta
        ToolStripButton_showStatistics.Name = "ToolStripButton_showStatistics"
        ToolStripButton_showStatistics.Size = New Size(104, 22)
        ToolStripButton_showStatistics.Text = "Show statistics"
        ToolStripButton_showStatistics.ToolTipText = "Show statistics"
        ' 
        ' ToolStripButton_MoveUp
        ' 
        ToolStripButton_MoveUp.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_MoveUp.Enabled = False
        ToolStripButton_MoveUp.Image = CType(resources.GetObject("ToolStripButton_MoveUp.Image"), Image)
        ToolStripButton_MoveUp.ImageTransparentColor = Color.Magenta
        ToolStripButton_MoveUp.Name = "ToolStripButton_MoveUp"
        ToolStripButton_MoveUp.Size = New Size(23, 22)
        ToolStripButton_MoveUp.Text = "Move selected time series up"
        ToolStripButton_MoveUp.ToolTipText = "Move selected time series up"
        ' 
        ' ToolStripButton_MoveDown
        ' 
        ToolStripButton_MoveDown.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_MoveDown.Enabled = False
        ToolStripButton_MoveDown.Image = CType(resources.GetObject("ToolStripButton_MoveDown.Image"), Image)
        ToolStripButton_MoveDown.ImageTransparentColor = Color.Magenta
        ToolStripButton_MoveDown.Name = "ToolStripButton_MoveDown"
        ToolStripButton_MoveDown.Size = New Size(23, 22)
        ToolStripButton_MoveDown.Text = "Move selected time series down"
        ' 
        ' ToolStripButton_Delete
        ' 
        ToolStripButton_Delete.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Delete.Enabled = False
        ToolStripButton_Delete.Image = CType(resources.GetObject("ToolStripButton_Delete.Image"), Image)
        ToolStripButton_Delete.ImageTransparentColor = Color.Magenta
        ToolStripButton_Delete.Name = "ToolStripButton_Delete"
        ToolStripButton_Delete.Size = New Size(23, 22)
        ToolStripButton_Delete.Text = "ToolStripButton_Delete"
        ToolStripButton_Delete.ToolTipText = "Delete selected time series"
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.DataPropertyName = "DataSource"
        DataGridViewTextBoxColumn1.HeaderText = "DataSource"
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.ReadOnly = True
        ' 
        ' PropertiesWindow
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(541, 302)
        Controls.Add(ToolStrip1)
        Controls.Add(DataGridView1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimumSize = New Size(347, 225)
        Name = "PropertiesWindow"
        StartPosition = FormStartPosition.CenterParent
        Text = "Time Series Properties"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(TimeSeriesBindingSource, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TimeSeriesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton_showStatistics As ToolStripButton
    Friend WithEvents ToolStripButton_Delete As ToolStripButton
    Friend WithEvents ToolStripButton_MoveUp As ToolStripButton
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
    Friend WithEvents ToolStripButton_MoveDown As ToolStripButton
End Class
