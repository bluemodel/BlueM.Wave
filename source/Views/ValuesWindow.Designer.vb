<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ValuesWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ValuesWindow))
        DataGridView1 = New DataGridView()
        ToolStrip1 = New ToolStrip()
        ToolStripButton_ExportValues = New ToolStripButton()
        ToolStripButton_showMarkers = New ToolStripButton()
        Label_DisplayCount = New Label()
        Button_next = New Button()
        Button_previous = New Button()
        NumericUpDown_StartRecord = New NumericUpDown()
        ToolTip1 = New ToolTip(components)
        Button_first = New Button()
        Button_last = New Button()
        MaskedTextBox_JumpDate = New MaskedTextBox()
        Button_Jump = New Button()
        Label_JumpTo = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        CType(NumericUpDown_StartRecord, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.CausesValidation = False
        DataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.Location = New Point(15, 61)
        DataGridView1.Margin = New Padding(4, 3, 4, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Control
        DataGridViewCellStyle1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.GrayText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridView1.Size = New Size(419, 426)
        DataGridView1.TabIndex = 3
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton_ExportValues, ToolStripButton_showMarkers})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(448, 25)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton_ExportValues
        ' 
        ToolStripButton_ExportValues.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ExportValues.Image = CType(resources.GetObject("ToolStripButton_ExportValues.Image"), Image)
        ToolStripButton_ExportValues.ImageTransparentColor = Color.Magenta
        ToolStripButton_ExportValues.Name = "ToolStripButton_ExportValues"
        ToolStripButton_ExportValues.Size = New Size(23, 22)
        ToolStripButton_ExportValues.Text = "Export values"
        ' 
        ' ToolStripButton_showMarkers
        ' 
        ToolStripButton_showMarkers.CheckOnClick = True
        ToolStripButton_showMarkers.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_showMarkers.Image = CType(resources.GetObject("ToolStripButton_showMarkers.Image"), Image)
        ToolStripButton_showMarkers.ImageTransparentColor = Color.Magenta
        ToolStripButton_showMarkers.Name = "ToolStripButton_showMarkers"
        ToolStripButton_showMarkers.Size = New Size(23, 22)
        ToolStripButton_showMarkers.Text = "Show markers for selected rows in chart"
        ' 
        ' Label_DisplayCount
        ' 
        Label_DisplayCount.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label_DisplayCount.AutoSize = True
        Label_DisplayCount.Location = New Point(12, 498)
        Label_DisplayCount.Margin = New Padding(4, 0, 4, 0)
        Label_DisplayCount.Name = "Label_DisplayCount"
        Label_DisplayCount.Size = New Size(138, 15)
        Label_DisplayCount.TabIndex = 4
        Label_DisplayCount.Text = "Displaying X of Y records"
        ' 
        ' Button_next
        ' 
        Button_next.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_next.Image = CType(resources.GetObject("Button_next.Image"), Image)
        Button_next.Location = New Point(383, 493)
        Button_next.Margin = New Padding(4, 3, 4, 3)
        Button_next.Name = "Button_next"
        Button_next.Size = New Size(26, 25)
        Button_next.TabIndex = 8
        ToolTip1.SetToolTip(Button_next, "next 100 records")
        Button_next.UseVisualStyleBackColor = True
        ' 
        ' Button_previous
        ' 
        Button_previous.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_previous.Image = CType(resources.GetObject("Button_previous.Image"), Image)
        Button_previous.Location = New Point(265, 493)
        Button_previous.Margin = New Padding(4, 3, 4, 3)
        Button_previous.Name = "Button_previous"
        Button_previous.Size = New Size(26, 25)
        Button_previous.TabIndex = 6
        ToolTip1.SetToolTip(Button_previous, "previous 100 records")
        Button_previous.UseVisualStyleBackColor = True
        ' 
        ' NumericUpDown_StartRecord
        ' 
        NumericUpDown_StartRecord.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        NumericUpDown_StartRecord.Location = New Point(292, 494)
        NumericUpDown_StartRecord.Margin = New Padding(4, 3, 4, 3)
        NumericUpDown_StartRecord.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_StartRecord.Name = "NumericUpDown_StartRecord"
        NumericUpDown_StartRecord.Size = New Size(90, 23)
        NumericUpDown_StartRecord.TabIndex = 7
        ToolTip1.SetToolTip(NumericUpDown_StartRecord, "start record")
        NumericUpDown_StartRecord.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Button_first
        ' 
        Button_first.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_first.Image = CType(resources.GetObject("Button_first.Image"), Image)
        Button_first.Location = New Point(239, 493)
        Button_first.Margin = New Padding(4, 3, 4, 3)
        Button_first.Name = "Button_first"
        Button_first.Size = New Size(26, 25)
        Button_first.TabIndex = 5
        ToolTip1.SetToolTip(Button_first, "first records")
        Button_first.UseVisualStyleBackColor = True
        ' 
        ' Button_last
        ' 
        Button_last.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_last.Image = CType(resources.GetObject("Button_last.Image"), Image)
        Button_last.Location = New Point(408, 493)
        Button_last.Margin = New Padding(4, 3, 4, 3)
        Button_last.Name = "Button_last"
        Button_last.Size = New Size(26, 25)
        Button_last.TabIndex = 9
        ToolTip1.SetToolTip(Button_last, "last records")
        Button_last.UseVisualStyleBackColor = True
        ' 
        ' MaskedTextBox_JumpDate
        ' 
        MaskedTextBox_JumpDate.Culture = New System.Globalization.CultureInfo("")
        MaskedTextBox_JumpDate.Location = New Point(76, 31)
        MaskedTextBox_JumpDate.Margin = New Padding(4, 3, 4, 3)
        MaskedTextBox_JumpDate.Name = "MaskedTextBox_JumpDate"
        MaskedTextBox_JumpDate.Size = New Size(116, 23)
        MaskedTextBox_JumpDate.TabIndex = 2
        MaskedTextBox_JumpDate.ValidatingType = GetType(Date)
        ' 
        ' Button_Jump
        ' 
        Button_Jump.Location = New Point(200, 29)
        Button_Jump.Margin = New Padding(4, 3, 4, 3)
        Button_Jump.Name = "Button_Jump"
        Button_Jump.Size = New Size(88, 27)
        Button_Jump.TabIndex = 10
        Button_Jump.Text = "Jump"
        Button_Jump.UseVisualStyleBackColor = True
        ' 
        ' Label_JumpTo
        ' 
        Label_JumpTo.AutoSize = True
        Label_JumpTo.Location = New Point(14, 35)
        Label_JumpTo.Margin = New Padding(4, 0, 4, 0)
        Label_JumpTo.Name = "Label_JumpTo"
        Label_JumpTo.Size = New Size(53, 15)
        Label_JumpTo.TabIndex = 11
        Label_JumpTo.Text = "Jump to:"
        ' 
        ' ValuesWindow
        ' 
        AcceptButton = Button_Jump
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(448, 532)
        Controls.Add(Label_JumpTo)
        Controls.Add(Button_Jump)
        Controls.Add(MaskedTextBox_JumpDate)
        Controls.Add(NumericUpDown_StartRecord)
        Controls.Add(Button_first)
        Controls.Add(Button_previous)
        Controls.Add(Button_last)
        Controls.Add(Button_next)
        Controls.Add(Label_DisplayCount)
        Controls.Add(ToolStrip1)
        Controls.Add(DataGridView1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimumSize = New Size(464, 225)
        Name = "ValuesWindow"
        StartPosition = FormStartPosition.CenterParent
        Text = "Time Series Values"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(NumericUpDown_StartRecord, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As ToolStrip
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
    Friend WithEvents MaskedTextBox_JumpDate As MaskedTextBox
    Friend WithEvents Button_Jump As Button
    Friend WithEvents Label_JumpTo As Label
End Class
