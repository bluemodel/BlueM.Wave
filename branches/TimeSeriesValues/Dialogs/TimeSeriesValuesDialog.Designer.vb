<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TimeSeriesValuesDialog
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TimeSeriesValuesDialog))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TimeseriesValuesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton_ExportValues = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_showMarkers = New System.Windows.Forms.ToolStripButton()
        Me.Label_DisplayCount = New System.Windows.Forms.Label()
        Me.Button_next = New System.Windows.Forms.Button()
        Me.Button_previous = New System.Windows.Forms.Button()
        Me.NumericUpDown_StartRecord = New System.Windows.Forms.NumericUpDown()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button_first = New System.Windows.Forms.Button()
        Me.Button_last = New System.Windows.Forms.Button()
        Me.DateTimePicker_JumpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimeseriesValuesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.NumericUpDown_StartRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.CausesValidation = False
        Me.DataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(13, 53)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.GrayText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView1.Size = New System.Drawing.Size(359, 369)
        Me.DataGridView1.TabIndex = 3
        '
        'TimeseriesValuesBindingSource
        '
        Me.TimeseriesValuesBindingSource.DataSource = GetType(System.Data.DataTable)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_ExportValues, Me.ToolStripButton_showMarkers})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(384, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton_ExportValues
        '
        Me.ToolStripButton_ExportValues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ExportValues.Image = Global.BlueM.Wave.My.Resources.Resources.table_save
        Me.ToolStripButton_ExportValues.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ExportValues.Name = "ToolStripButton_ExportValues"
        Me.ToolStripButton_ExportValues.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_ExportValues.Text = "Export values"
        '
        'ToolStripButton_showMarkers
        '
        Me.ToolStripButton_showMarkers.CheckOnClick = True
        Me.ToolStripButton_showMarkers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_showMarkers.Image = Global.BlueM.Wave.My.Resources.Resources.text_list_numbers
        Me.ToolStripButton_showMarkers.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_showMarkers.Name = "ToolStripButton_showMarkers"
        Me.ToolStripButton_showMarkers.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_showMarkers.Text = "Show markers for selected rows in chart"
        '
        'Label_DisplayCount
        '
        Me.Label_DisplayCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_DisplayCount.AutoSize = True
        Me.Label_DisplayCount.Location = New System.Drawing.Point(10, 432)
        Me.Label_DisplayCount.Name = "Label_DisplayCount"
        Me.Label_DisplayCount.Size = New System.Drawing.Size(125, 13)
        Me.Label_DisplayCount.TabIndex = 4
        Me.Label_DisplayCount.Text = "Displaying X of Y records"
        '
        'Button_next
        '
        Me.Button_next.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_next.Image = Global.BlueM.Wave.My.Resources.Resources.control_fastforward
        Me.Button_next.Location = New System.Drawing.Point(328, 427)
        Me.Button_next.Name = "Button_next"
        Me.Button_next.Size = New System.Drawing.Size(22, 22)
        Me.Button_next.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.Button_next, "next 100 records")
        Me.Button_next.UseVisualStyleBackColor = True
        '
        'Button_previous
        '
        Me.Button_previous.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_previous.Image = Global.BlueM.Wave.My.Resources.Resources.control_rewind
        Me.Button_previous.Location = New System.Drawing.Point(227, 427)
        Me.Button_previous.Name = "Button_previous"
        Me.Button_previous.Size = New System.Drawing.Size(22, 22)
        Me.Button_previous.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.Button_previous, "previous 100 records")
        Me.Button_previous.UseVisualStyleBackColor = True
        '
        'NumericUpDown_StartRecord
        '
        Me.NumericUpDown_StartRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_StartRecord.Location = New System.Drawing.Point(250, 428)
        Me.NumericUpDown_StartRecord.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_StartRecord.Name = "NumericUpDown_StartRecord"
        Me.NumericUpDown_StartRecord.Size = New System.Drawing.Size(77, 20)
        Me.NumericUpDown_StartRecord.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.NumericUpDown_StartRecord, "start record")
        Me.NumericUpDown_StartRecord.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Button_first
        '
        Me.Button_first.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_first.Image = Global.BlueM.Wave.My.Resources.Resources.control_start
        Me.Button_first.Location = New System.Drawing.Point(205, 427)
        Me.Button_first.Name = "Button_first"
        Me.Button_first.Size = New System.Drawing.Size(22, 22)
        Me.Button_first.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.Button_first, "first records")
        Me.Button_first.UseVisualStyleBackColor = True
        '
        'Button_last
        '
        Me.Button_last.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_last.Image = Global.BlueM.Wave.My.Resources.Resources.control_end
        Me.Button_last.Location = New System.Drawing.Point(350, 427)
        Me.Button_last.Name = "Button_last"
        Me.Button_last.Size = New System.Drawing.Size(22, 22)
        Me.Button_last.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.Button_last, "last records")
        Me.Button_last.UseVisualStyleBackColor = True
        '
        'DateTimePicker_JumpDate
        '
        Me.DateTimePicker_JumpDate.CustomFormat = "dd.MM.yyyy HH:mm"
        Me.DateTimePicker_JumpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_JumpDate.Location = New System.Drawing.Point(62, 27)
        Me.DateTimePicker_JumpDate.Name = "DateTimePicker_JumpDate"
        Me.DateTimePicker_JumpDate.Size = New System.Drawing.Size(122, 20)
        Me.DateTimePicker_JumpDate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Jump to"
        '
        'TimeSeriesValuesDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 461)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePicker_JumpDate)
        Me.Controls.Add(Me.NumericUpDown_StartRecord)
        Me.Controls.Add(Me.Button_first)
        Me.Controls.Add(Me.Button_previous)
        Me.Controls.Add(Me.Button_last)
        Me.Controls.Add(Me.Button_next)
        Me.Controls.Add(Me.Label_DisplayCount)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "TimeSeriesValuesDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Time Series Values"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimeseriesValuesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.NumericUpDown_StartRecord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TimeseriesValuesBindingSource As BindingSource
    Friend WithEvents TimestampDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Label_DisplayCount As Label
    Friend WithEvents Button_next As Button
    Friend WithEvents Button_previous As Button
    Friend WithEvents NumericUpDown_StartRecord As NumericUpDown
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Button_first As Button
    Friend WithEvents Button_last As Button
    Friend WithEvents ToolStripButton_ExportValues As ToolStripButton
    Friend WithEvents ToolStripButton_showMarkers As ToolStripButton
    Friend WithEvents DateTimePicker_JumpDate As DateTimePicker
    Friend WithEvents Label1 As Label
End Class
