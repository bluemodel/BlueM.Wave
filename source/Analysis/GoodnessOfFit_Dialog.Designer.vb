<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GoodnessOfFit_Dialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GoodnessOfFit_Dialog))
        Button_OK = New Button()
        CheckBox_Annual = New CheckBox()
        ComboBox_startMonth = New ComboBox()
        Label_startMonth = New Label()
        GroupBox1 = New GroupBox()
        GroupBox2 = New GroupBox()
        ComboBox_ObservedSeries = New ComboBox()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(295, 180)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 3
        Button_OK.Text = "OK"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_Annual
        ' 
        CheckBox_Annual.AutoSize = True
        CheckBox_Annual.Location = New Point(7, 22)
        CheckBox_Annual.Name = "CheckBox_Annual"
        CheckBox_Annual.Size = New Size(309, 19)
        CheckBox_Annual.TabIndex = 4
        CheckBox_Annual.Text = "Calculate parameters for individual hydrological years"
        CheckBox_Annual.UseVisualStyleBackColor = True
        ' 
        ' ComboBox_startMonth
        ' 
        ComboBox_startMonth.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_startMonth.Enabled = False
        ComboBox_startMonth.FormattingEnabled = True
        ComboBox_startMonth.Location = New Point(161, 47)
        ComboBox_startMonth.Name = "ComboBox_startMonth"
        ComboBox_startMonth.Size = New Size(118, 23)
        ComboBox_startMonth.TabIndex = 14
        ' 
        ' Label_startMonth
        ' 
        Label_startMonth.AutoSize = True
        Label_startMonth.Enabled = False
        Label_startMonth.Location = New Point(6, 51)
        Label_startMonth.Margin = New Padding(4, 0, 4, 0)
        Label_startMonth.Name = "Label_startMonth"
        Label_startMonth.Size = New Size(142, 15)
        Label_startMonth.TabIndex = 13
        Label_startMonth.Text = "Start of hydrological year:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckBox_Annual)
        GroupBox1.Controls.Add(ComboBox_startMonth)
        GroupBox1.Controls.Add(Label_startMonth)
        GroupBox1.Location = New Point(14, 82)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(361, 84)
        GroupBox1.TabIndex = 15
        GroupBox1.TabStop = False
        GroupBox1.Text = "Hydrological years"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(ComboBox_ObservedSeries)
        GroupBox2.Location = New Point(14, 14)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(361, 61)
        GroupBox2.TabIndex = 16
        GroupBox2.TabStop = False
        GroupBox2.Text = "Series with ""observed"" values"
        ' 
        ' ComboBox_ObservedSeries
        ' 
        ComboBox_ObservedSeries.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_ObservedSeries.FormattingEnabled = True
        ComboBox_ObservedSeries.Location = New Point(8, 22)
        ComboBox_ObservedSeries.Name = "ComboBox_ObservedSeries"
        ComboBox_ObservedSeries.Size = New Size(344, 23)
        ComboBox_ObservedSeries.TabIndex = 3
        ' 
        ' GoodnessOfFit_Dialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(387, 219)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(Button_OK)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "GoodnessOfFit_Dialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Goodness of Fit"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents CheckBox_Annual As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox_startMonth As ComboBox
    Friend WithEvents Label_startMonth As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ComboBox_ObservedSeries As ComboBox
End Class
