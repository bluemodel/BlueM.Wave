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
        Label_Title = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_Title
        '
        Label_Title.AutoSize = True
        Label_Title.Location = New System.Drawing.Point(12, 15)
        Label_Title.Name = "Label_Title"
        Label_Title.Size = New System.Drawing.Size(30, 13)
        Label_Title.TabIndex = 0
        Label_Title.Text = "Titel:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnDate, Me.ColumnValue})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 67)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(268, 184)
        Me.DataGridView1.TabIndex = 3
        '
        'ColumnDate
        '
        Me.ColumnDate.HeaderText = "Datum"
        Me.ColumnDate.Name = "ColumnDate"
        Me.ColumnDate.Width = 61
        '
        'ColumnValue
        '
        Me.ColumnValue.HeaderText = "Wert"
        Me.ColumnValue.Name = "ColumnValue"
        Me.ColumnValue.Width = 53
        '
        'Button_Paste
        '
        Me.Button_Paste.Image = Global.Wave.My.Resources.Resources.page_paste
        Me.Button_Paste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_Paste.Location = New System.Drawing.Point(12, 38)
        Me.Button_Paste.Name = "Button_Paste"
        Me.Button_Paste.Size = New System.Drawing.Size(75, 23)
        Me.Button_Paste.TabIndex = 2
        Me.Button_Paste.Text = "Einfügen"
        Me.Button_Paste.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Paste.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(205, 257)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 4
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'TextBox_Title
        '
        Me.TextBox_Title.Location = New System.Drawing.Point(58, 12)
        Me.TextBox_Title.Name = "TextBox_Title"
        Me.TextBox_Title.Size = New System.Drawing.Size(222, 20)
        Me.TextBox_Title.TabIndex = 1
        '
        'SeriesEditorDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 289)
        Me.Controls.Add(Me.TextBox_Title)
        Me.Controls.Add(Label_Title)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Paste)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SeriesEditorDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Zeitreihe eingeben"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ColumnDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button_Paste As System.Windows.Forms.Button
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents TextBox_Title As System.Windows.Forms.TextBox
End Class
