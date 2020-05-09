<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeriesEditorDialog
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
        Dim Label_Title As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeriesEditorDialog))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ColumnDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Button_Paste = New System.Windows.Forms.Button
        Me.Button_OK = New System.Windows.Forms.Button
        Me.TextBox_Title = New System.Windows.Forms.TextBox
        Me.Label_Unit = New System.Windows.Forms.Label
        Me.TextBox_Unit = New System.Windows.Forms.TextBox
        Me.Label_Help = New System.Windows.Forms.Label
        Label_Title = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_Title
        '
        Label_Title.AutoSize = True
        Label_Title.Location = New System.Drawing.Point(9, 15)
        Label_Title.Name = "Label_Title"
        Label_Title.Size = New System.Drawing.Size(30, 13)
        Label_Title.TabIndex = 0
        Label_Title.Text = "Title:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnDate, Me.ColumnValue})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 96)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(268, 193)
        Me.DataGridView1.TabIndex = 6
        '
        'ColumnDate
        '
        Me.ColumnDate.HeaderText = "Date"
        Me.ColumnDate.Name = "ColumnDate"
        Me.ColumnDate.Width = 55
        '
        'ColumnValue
        '
        Me.ColumnValue.HeaderText = "Value"
        Me.ColumnValue.Name = "ColumnValue"
        Me.ColumnValue.Width = 59
        '
        'Button_Paste
        '
        Me.Button_Paste.Image = Global.BlueM.Wave.My.Resources.Resources.page_paste
        Me.Button_Paste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_Paste.Location = New System.Drawing.Point(12, 66)
        Me.Button_Paste.Name = "Button_Paste"
        Me.Button_Paste.Size = New System.Drawing.Size(75, 24)
        Me.Button_Paste.TabIndex = 4
        Me.Button_Paste.Text = "Paste"
        Me.Button_Paste.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Paste.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(205, 295)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 7
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'TextBox_Title
        '
        Me.TextBox_Title.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Title.Location = New System.Drawing.Point(57, 12)
        Me.TextBox_Title.Name = "TextBox_Title"
        Me.TextBox_Title.Size = New System.Drawing.Size(223, 20)
        Me.TextBox_Title.TabIndex = 1
        '
        'Label_Unit
        '
        Me.Label_Unit.AutoSize = True
        Me.Label_Unit.Location = New System.Drawing.Point(9, 43)
        Me.Label_Unit.Name = "Label_Unit"
        Me.Label_Unit.Size = New System.Drawing.Size(29, 13)
        Me.Label_Unit.TabIndex = 2
        Me.Label_Unit.Text = "Unit:"
        '
        'TextBox_Unit
        '
        Me.TextBox_Unit.Location = New System.Drawing.Point(57, 40)
        Me.TextBox_Unit.Name = "TextBox_Unit"
        Me.TextBox_Unit.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_Unit.TabIndex = 3
        Me.TextBox_Unit.Text = "-"
        '
        'Label_Help
        '
        Me.Label_Help.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Help.Location = New System.Drawing.Point(94, 65)
        Me.Label_Help.Name = "Label_Help"
        Me.Label_Help.Size = New System.Drawing.Size(185, 26)
        Me.Label_Help.TabIndex = 5
        Me.Label_Help.Text = "accepts two adjacent columns (dates and values) from Excel"
        '
        'SeriesEditorDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 327)
        Me.Controls.Add(Label_Title)
        Me.Controls.Add(Me.TextBox_Title)
        Me.Controls.Add(Me.Label_Unit)
        Me.Controls.Add(Me.TextBox_Unit)
        Me.Controls.Add(Me.Button_Paste)
        Me.Controls.Add(Me.Label_Help)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button_OK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SeriesEditorDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Enter time series"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ColumnDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents Label_Help As System.Windows.Forms.Label
    Private WithEvents TextBox_Unit As System.Windows.Forms.TextBox
    Private WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Private WithEvents Button_Paste As System.Windows.Forms.Button
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents TextBox_Title As System.Windows.Forms.TextBox
    Private WithEvents Label_Unit As System.Windows.Forms.Label
End Class
