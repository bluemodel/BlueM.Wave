<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SaveProjectFileDialog
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SaveProjectFileDialog))
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_PointsVisibility = New System.Windows.Forms.CheckBox()
        Me.CheckBox_LineWidth = New System.Windows.Forms.CheckBox()
        Me.CheckBox_LineStyle = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Color = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Title = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Unit = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Interpretation = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox_File = New System.Windows.Forms.TextBox()
        Me.Button_Browse = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(287, 316)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 1
        Me.Button_OK.Text = "Ok"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(368, 316)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 5
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBox_PointsVisibility)
        Me.GroupBox1.Controls.Add(Me.CheckBox_LineWidth)
        Me.GroupBox1.Controls.Add(Me.CheckBox_LineStyle)
        Me.GroupBox1.Controls.Add(Me.CheckBox_Color)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 114)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 116)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Display options"
        '
        'CheckBox_Points
        '
        Me.CheckBox_PointsVisibility.AutoSize = True
        Me.CheckBox_PointsVisibility.Location = New System.Drawing.Point(6, 88)
        Me.CheckBox_PointsVisibility.Name = "CheckBox_Points"
        Me.CheckBox_PointsVisibility.Size = New System.Drawing.Size(93, 17)
        Me.CheckBox_PointsVisibility.TabIndex = 13
        Me.CheckBox_PointsVisibility.Text = "Points visibility"
        Me.CheckBox_PointsVisibility.UseVisualStyleBackColor = True
        '
        'CheckBox_Width
        '
        Me.CheckBox_LineWidth.AutoSize = True
        Me.CheckBox_LineWidth.Location = New System.Drawing.Point(6, 65)
        Me.CheckBox_LineWidth.Name = "CheckBox_Width"
        Me.CheckBox_LineWidth.Size = New System.Drawing.Size(74, 17)
        Me.CheckBox_LineWidth.TabIndex = 12
        Me.CheckBox_LineWidth.Text = "Line width"
        Me.CheckBox_LineWidth.UseVisualStyleBackColor = True
        '
        'CheckBox_Style
        '
        Me.CheckBox_LineStyle.AutoSize = True
        Me.CheckBox_LineStyle.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox_LineStyle.Name = "CheckBox_Style"
        Me.CheckBox_LineStyle.Size = New System.Drawing.Size(70, 17)
        Me.CheckBox_LineStyle.TabIndex = 8
        Me.CheckBox_LineStyle.Text = "Line style"
        Me.CheckBox_LineStyle.UseVisualStyleBackColor = True
        '
        'CheckBox_Color
        '
        Me.CheckBox_Color.AutoSize = True
        Me.CheckBox_Color.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_Color.Name = "CheckBox_Color"
        Me.CheckBox_Color.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox_Color.TabIndex = 7
        Me.CheckBox_Color.Text = "Line color"
        Me.CheckBox_Color.UseVisualStyleBackColor = True
        '
        'CheckBox_Title
        '
        Me.CheckBox_Title.AutoSize = True
        Me.CheckBox_Title.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_Title.Name = "CheckBox_Title"
        Me.CheckBox_Title.Size = New System.Drawing.Size(46, 17)
        Me.CheckBox_Title.TabIndex = 9
        Me.CheckBox_Title.Text = "Title"
        Me.CheckBox_Title.UseVisualStyleBackColor = True
        '
        'CheckBox_Unit
        '
        Me.CheckBox_Unit.AutoSize = True
        Me.CheckBox_Unit.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox_Unit.Name = "CheckBox_Unit"
        Me.CheckBox_Unit.Size = New System.Drawing.Size(45, 17)
        Me.CheckBox_Unit.TabIndex = 10
        Me.CheckBox_Unit.Text = "Unit"
        Me.CheckBox_Unit.UseVisualStyleBackColor = True
        '
        'CheckBox_Interpretation
        '
        Me.CheckBox_Interpretation.AutoSize = True
        Me.CheckBox_Interpretation.Location = New System.Drawing.Point(6, 65)
        Me.CheckBox_Interpretation.Name = "CheckBox_Interpretation"
        Me.CheckBox_Interpretation.Size = New System.Drawing.Size(88, 17)
        Me.CheckBox_Interpretation.TabIndex = 11
        Me.CheckBox_Interpretation.Text = "Interpretation"
        Me.CheckBox_Interpretation.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CheckBox_Title)
        Me.GroupBox2.Controls.Add(Me.CheckBox_Unit)
        Me.GroupBox2.Controls.Add(Me.CheckBox_Interpretation)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 89)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Series properties"
        '
        'TextBox1
        '
        Me.TextBox_File.Location = New System.Drawing.Point(62, 13)
        Me.TextBox_File.Name = "TextBox1"
        Me.TextBox_File.Size = New System.Drawing.Size(323, 20)
        Me.TextBox_File.TabIndex = 9
        '
        'Button1
        '
        Me.Button_Browse.Location = New System.Drawing.Point(391, 11)
        Me.Button_Browse.Name = "Button1"
        Me.Button_Browse.Size = New System.Drawing.Size(34, 23)
        Me.Button_Browse.TabIndex = 10
        Me.Button_Browse.Text = "..."
        Me.Button_Browse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "File path:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.TextBox_File)
        Me.GroupBox3.Controls.Add(Me.Button_Browse)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(431, 46)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox2)
        Me.GroupBox4.Controls.Add(Me.GroupBox1)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 64)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(431, 241)
        Me.GroupBox4.TabIndex = 13
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Options"
        '
        'SaveProjectFileDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 351)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(210, 260)
        Me.Name = "SaveProjectFileDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Save project file"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CheckBox_PointsVisibility As CheckBox
    Friend WithEvents CheckBox_LineWidth As CheckBox
    Friend WithEvents CheckBox_LineStyle As CheckBox
    Friend WithEvents CheckBox_Color As CheckBox
    Friend WithEvents CheckBox_Title As CheckBox
    Friend WithEvents CheckBox_Unit As CheckBox
    Friend WithEvents CheckBox_Interpretation As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TextBox_File As TextBox
    Friend WithEvents Button_Browse As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
