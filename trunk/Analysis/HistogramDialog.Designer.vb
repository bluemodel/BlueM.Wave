<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HistogramDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HistogramDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.DataGridView_breaks = New System.Windows.Forms.DataGridView
        Me.valueFrom = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.valueTo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NumericUpDown_n_bins = New System.Windows.Forms.NumericUpDown
        Me.Label_n_bins = New System.Windows.Forms.Label
        Me.Button_recalculate = New System.Windows.Forms.Button
        Me.Label_minmax = New System.Windows.Forms.Label
        Me.Button_paste = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView_breaks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_n_bins, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(146, 399)
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
        'DataGridView_breaks
        '
        Me.DataGridView_breaks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView_breaks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_breaks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.valueFrom, Me.valueTo})
        Me.DataGridView_breaks.Location = New System.Drawing.Point(16, 59)
        Me.DataGridView_breaks.Name = "DataGridView_breaks"
        Me.DataGridView_breaks.Size = New System.Drawing.Size(275, 330)
        Me.DataGridView_breaks.TabIndex = 1
        '
        'valueFrom
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.valueFrom.DefaultCellStyle = DataGridViewCellStyle1
        Me.valueFrom.HeaderText = "From"
        Me.valueFrom.Name = "valueFrom"
        Me.valueFrom.ReadOnly = True
        Me.valueFrom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'valueTo
        '
        Me.valueTo.HeaderText = "To"
        Me.valueTo.Name = "valueTo"
        Me.valueTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'NumericUpDown_n_bins
        '
        Me.NumericUpDown_n_bins.Location = New System.Drawing.Point(77, 33)
        Me.NumericUpDown_n_bins.Name = "NumericUpDown_n_bins"
        Me.NumericUpDown_n_bins.Size = New System.Drawing.Size(54, 20)
        Me.NumericUpDown_n_bins.TabIndex = 2
        '
        'Label_n_bins
        '
        Me.Label_n_bins.AutoSize = True
        Me.Label_n_bins.Location = New System.Drawing.Point(13, 35)
        Me.Label_n_bins.Name = "Label_n_bins"
        Me.Label_n_bins.Size = New System.Drawing.Size(58, 13)
        Me.Label_n_bins.TabIndex = 3
        Me.Label_n_bins.Text = "No of bins:"
        '
        'Button_recalculate
        '
        Me.Button_recalculate.Location = New System.Drawing.Point(137, 30)
        Me.Button_recalculate.Name = "Button_recalculate"
        Me.Button_recalculate.Size = New System.Drawing.Size(75, 23)
        Me.Button_recalculate.TabIndex = 4
        Me.Button_recalculate.Text = "Recalculate"
        Me.ToolTip1.SetToolTip(Me.Button_recalculate, "Recalculate bins of equal width")
        Me.Button_recalculate.UseVisualStyleBackColor = True
        '
        'Label_minmax
        '
        Me.Label_minmax.AutoSize = True
        Me.Label_minmax.Location = New System.Drawing.Point(13, 13)
        Me.Label_minmax.Name = "Label_minmax"
        Me.Label_minmax.Size = New System.Drawing.Size(67, 13)
        Me.Label_minmax.TabIndex = 5
        Me.Label_minmax.Text = "Value range:"
        '
        'Button_paste
        '
        Me.Button_paste.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_paste.Image = Global.BlueM.Wave.My.Resources.Resources.page_paste
        Me.Button_paste.Location = New System.Drawing.Point(217, 30)
        Me.Button_paste.Name = "Button_paste"
        Me.Button_paste.Size = New System.Drawing.Size(75, 23)
        Me.Button_paste.TabIndex = 6
        Me.Button_paste.Text = "Paste"
        Me.Button_paste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.Button_paste, "Paste breaks as a column from Excel or as whitespace-separated text")
        Me.Button_paste.UseVisualStyleBackColor = True
        '
        'HistogramDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(304, 440)
        Me.Controls.Add(Me.Button_paste)
        Me.Controls.Add(Me.Label_minmax)
        Me.Controls.Add(Me.Button_recalculate)
        Me.Controls.Add(Me.Label_n_bins)
        Me.Controls.Add(Me.NumericUpDown_n_bins)
        Me.Controls.Add(Me.DataGridView_breaks)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(320, 235)
        Me.Name = "HistogramDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Histogram"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView_breaks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_n_bins, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents DataGridView_breaks As System.Windows.Forms.DataGridView
    Friend WithEvents NumericUpDown_n_bins As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label_n_bins As System.Windows.Forms.Label
    Friend WithEvents Button_recalculate As System.Windows.Forms.Button
    Friend WithEvents Label_minmax As System.Windows.Forms.Label
    Friend WithEvents valueFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents valueTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button_paste As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
