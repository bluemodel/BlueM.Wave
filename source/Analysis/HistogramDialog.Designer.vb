<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HistogramDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HistogramDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        DataGridView_breaks = New DataGridView()
        valueFrom = New DataGridViewTextBoxColumn()
        valueTo = New DataGridViewTextBoxColumn()
        NumericUpDown_n_bins = New NumericUpDown()
        Label_n_bins = New Label()
        Button_recalculate = New Button()
        Label_minmax = New Label()
        Button_paste = New Button()
        ToolTip1 = New ToolTip(components)
        CType(DataGridView_breaks, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown_n_bins, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(177, 469)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(263, 469)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' DataGridView_breaks
        ' 
        DataGridView_breaks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView_breaks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView_breaks.Columns.AddRange(New DataGridViewColumn() {valueFrom, valueTo})
        DataGridView_breaks.Location = New Point(19, 68)
        DataGridView_breaks.Name = "DataGridView_breaks"
        DataGridView_breaks.Size = New Size(324, 381)
        DataGridView_breaks.TabIndex = 1
        ' 
        ' valueFrom
        ' 
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        valueFrom.DefaultCellStyle = DataGridViewCellStyle1
        valueFrom.HeaderText = "From"
        valueFrom.Name = "valueFrom"
        valueFrom.ReadOnly = True
        valueFrom.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' valueTo
        ' 
        valueTo.HeaderText = "To"
        valueTo.Name = "valueTo"
        valueTo.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' NumericUpDown_n_bins
        ' 
        NumericUpDown_n_bins.Location = New Point(90, 38)
        NumericUpDown_n_bins.Name = "NumericUpDown_n_bins"
        NumericUpDown_n_bins.Size = New Size(65, 23)
        NumericUpDown_n_bins.TabIndex = 2
        ' 
        ' Label_n_bins
        ' 
        Label_n_bins.AutoSize = True
        Label_n_bins.Location = New Point(15, 40)
        Label_n_bins.Name = "Label_n_bins"
        Label_n_bins.Size = New Size(65, 15)
        Label_n_bins.TabIndex = 3
        Label_n_bins.Text = "No of bins:"
        ' 
        ' Button_recalculate
        ' 
        Button_recalculate.Location = New Point(161, 35)
        Button_recalculate.Name = "Button_recalculate"
        Button_recalculate.Size = New Size(88, 27)
        Button_recalculate.TabIndex = 4
        Button_recalculate.Text = "Recalculate"
        ToolTip1.SetToolTip(Button_recalculate, "Recalculate bins of equal width")
        Button_recalculate.UseVisualStyleBackColor = True
        ' 
        ' Label_minmax
        ' 
        Label_minmax.AutoSize = True
        Label_minmax.Location = New Point(15, 15)
        Label_minmax.Name = "Label_minmax"
        Label_minmax.Size = New Size(71, 15)
        Label_minmax.TabIndex = 5
        Label_minmax.Text = "Value range:"
        ' 
        ' Button_paste
        ' 
        Button_paste.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_paste.Image = CType(resources.GetObject("Button_paste.Image"), Image)
        Button_paste.Location = New Point(255, 34)
        Button_paste.Name = "Button_paste"
        Button_paste.Size = New Size(88, 27)
        Button_paste.TabIndex = 6
        Button_paste.Text = "Paste"
        Button_paste.TextImageRelation = TextImageRelation.ImageBeforeText
        ToolTip1.SetToolTip(Button_paste, "Paste breaks as a column from Excel or as whitespace-separated text")
        Button_paste.UseVisualStyleBackColor = True
        ' 
        ' HistogramDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(355, 508)
        Controls.Add(OK_Button)
        Controls.Add(Cancel_Button)
        Controls.Add(Button_paste)
        Controls.Add(Label_minmax)
        Controls.Add(Button_recalculate)
        Controls.Add(Label_n_bins)
        Controls.Add(NumericUpDown_n_bins)
        Controls.Add(DataGridView_breaks)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(371, 265)
        Name = "HistogramDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Histogram"
        CType(DataGridView_breaks, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown_n_bins, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
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
