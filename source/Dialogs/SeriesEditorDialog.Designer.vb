<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeriesEditorDialog
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
        Dim Label_Title As Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeriesEditorDialog))
        DataGridView1 = New DataGridView()
        ColumnDate = New DataGridViewTextBoxColumn()
        ColumnValue = New DataGridViewTextBoxColumn()
        Button_Paste = New Button()
        Button_OK = New Button()
        TextBox_Title = New TextBox()
        Label_Unit = New Label()
        TextBox_Unit = New TextBox()
        Label_Help = New Label()
        Label_Title = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label_Title
        ' 
        Label_Title.AutoSize = True
        Label_Title.Location = New Point(10, 17)
        Label_Title.Name = "Label_Title"
        Label_Title.Size = New Size(32, 15)
        Label_Title.TabIndex = 0
        Label_Title.Text = "Title:"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {ColumnDate, ColumnValue})
        DataGridView1.Location = New Point(14, 111)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(313, 223)
        DataGridView1.TabIndex = 6
        ' 
        ' ColumnDate
        ' 
        ColumnDate.HeaderText = "Date"
        ColumnDate.Name = "ColumnDate"
        ColumnDate.Width = 56
        ' 
        ' ColumnValue
        ' 
        ColumnValue.HeaderText = "Value"
        ColumnValue.Name = "ColumnValue"
        ColumnValue.Width = 60
        ' 
        ' Button_Paste
        ' 
        Button_Paste.Image = CType(resources.GetObject("Button_Paste.Image"), Image)
        Button_Paste.ImageAlign = ContentAlignment.MiddleLeft
        Button_Paste.Location = New Point(14, 75)
        Button_Paste.Name = "Button_Paste"
        Button_Paste.Size = New Size(80, 27)
        Button_Paste.TabIndex = 4
        Button_Paste.Text = "Paste"
        Button_Paste.TextAlign = ContentAlignment.MiddleRight
        Button_Paste.UseVisualStyleBackColor = True
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(248, 340)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 7
        Button_OK.Text = "OK"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' TextBox_Title
        ' 
        TextBox_Title.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_Title.Location = New Point(66, 14)
        TextBox_Title.Name = "TextBox_Title"
        TextBox_Title.Size = New Size(259, 23)
        TextBox_Title.TabIndex = 1
        ' 
        ' Label_Unit
        ' 
        Label_Unit.AutoSize = True
        Label_Unit.Location = New Point(10, 50)
        Label_Unit.Name = "Label_Unit"
        Label_Unit.Size = New Size(32, 15)
        Label_Unit.TabIndex = 2
        Label_Unit.Text = "Unit:"
        ' 
        ' TextBox_Unit
        ' 
        TextBox_Unit.Location = New Point(66, 46)
        TextBox_Unit.Name = "TextBox_Unit"
        TextBox_Unit.Size = New Size(116, 23)
        TextBox_Unit.TabIndex = 3
        TextBox_Unit.Text = "-"
        ' 
        ' Label_Help
        ' 
        Label_Help.ForeColor = SystemColors.ControlDarkDark
        Label_Help.Location = New Point(110, 75)
        Label_Help.Margin = New Padding(4, 0, 4, 0)
        Label_Help.Name = "Label_Help"
        Label_Help.Size = New Size(216, 30)
        Label_Help.TabIndex = 5
        Label_Help.Text = "accepts two adjacent columns (dates and values) from Excel"
        ' 
        ' SeriesEditorDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(340, 377)
        Controls.Add(Label_Title)
        Controls.Add(TextBox_Title)
        Controls.Add(Label_Unit)
        Controls.Add(TextBox_Unit)
        Controls.Add(Button_Paste)
        Controls.Add(Label_Help)
        Controls.Add(DataGridView1)
        Controls.Add(Button_OK)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "SeriesEditorDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Enter time series"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
