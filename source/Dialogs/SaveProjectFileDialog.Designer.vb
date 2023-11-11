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
        Me.GroupBox_DisplayOptions = New System.Windows.Forms.GroupBox()
        Me.CheckBox_PointsVisibility = New System.Windows.Forms.CheckBox()
        Me.CheckBox_LineWidth = New System.Windows.Forms.CheckBox()
        Me.CheckBox_LineStyle = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Color = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Title = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Unit = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Interpretation = New System.Windows.Forms.CheckBox()
        Me.GroupBox_SeriesProperties = New System.Windows.Forms.GroupBox()
        Me.TextBox_File = New System.Windows.Forms.TextBox()
        Me.Button_Browse = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox_File = New System.Windows.Forms.GroupBox()
        Me.GroupBox_Options = New System.Windows.Forms.GroupBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox_DisplayOptions.SuspendLayout()
        Me.GroupBox_SeriesProperties.SuspendLayout()
        Me.GroupBox_File.SuspendLayout()
        Me.GroupBox_Options.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(316, 316)
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
        Me.Button_Cancel.Location = New System.Drawing.Point(397, 316)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 5
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'GroupBox_DisplayOptions
        '
        Me.GroupBox_DisplayOptions.Controls.Add(Me.CheckBox_PointsVisibility)
        Me.GroupBox_DisplayOptions.Controls.Add(Me.CheckBox_LineWidth)
        Me.GroupBox_DisplayOptions.Controls.Add(Me.CheckBox_LineStyle)
        Me.GroupBox_DisplayOptions.Controls.Add(Me.CheckBox_Color)
        Me.GroupBox_DisplayOptions.Location = New System.Drawing.Point(9, 114)
        Me.GroupBox_DisplayOptions.Name = "GroupBox_DisplayOptions"
        Me.GroupBox_DisplayOptions.Size = New System.Drawing.Size(200, 116)
        Me.GroupBox_DisplayOptions.TabIndex = 7
        Me.GroupBox_DisplayOptions.TabStop = False
        Me.GroupBox_DisplayOptions.Text = "Display options"
        '
        'CheckBox_PointsVisibility
        '
        Me.CheckBox_PointsVisibility.AutoSize = True
        Me.CheckBox_PointsVisibility.Location = New System.Drawing.Point(6, 88)
        Me.CheckBox_PointsVisibility.Name = "CheckBox_PointsVisibility"
        Me.CheckBox_PointsVisibility.Size = New System.Drawing.Size(93, 17)
        Me.CheckBox_PointsVisibility.TabIndex = 13
        Me.CheckBox_PointsVisibility.Text = "Points visibility"
        Me.CheckBox_PointsVisibility.UseVisualStyleBackColor = True
        '
        'CheckBox_LineWidth
        '
        Me.CheckBox_LineWidth.AutoSize = True
        Me.CheckBox_LineWidth.Location = New System.Drawing.Point(6, 65)
        Me.CheckBox_LineWidth.Name = "CheckBox_LineWidth"
        Me.CheckBox_LineWidth.Size = New System.Drawing.Size(74, 17)
        Me.CheckBox_LineWidth.TabIndex = 12
        Me.CheckBox_LineWidth.Text = "Line width"
        Me.CheckBox_LineWidth.UseVisualStyleBackColor = True
        '
        'CheckBox_LineStyle
        '
        Me.CheckBox_LineStyle.AutoSize = True
        Me.CheckBox_LineStyle.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox_LineStyle.Name = "CheckBox_LineStyle"
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
        'GroupBox_SeriesProperties
        '
        Me.GroupBox_SeriesProperties.Controls.Add(Me.CheckBox_Title)
        Me.GroupBox_SeriesProperties.Controls.Add(Me.CheckBox_Unit)
        Me.GroupBox_SeriesProperties.Controls.Add(Me.CheckBox_Interpretation)
        Me.GroupBox_SeriesProperties.Location = New System.Drawing.Point(9, 19)
        Me.GroupBox_SeriesProperties.Name = "GroupBox_SeriesProperties"
        Me.GroupBox_SeriesProperties.Size = New System.Drawing.Size(200, 89)
        Me.GroupBox_SeriesProperties.TabIndex = 8
        Me.GroupBox_SeriesProperties.TabStop = False
        Me.GroupBox_SeriesProperties.Text = "Series properties"
        '
        'TextBox_File
        '
        Me.TextBox_File.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_File.Location = New System.Drawing.Point(62, 15)
        Me.TextBox_File.Name = "TextBox_File"
        Me.TextBox_File.Size = New System.Drawing.Size(352, 20)
        Me.TextBox_File.TabIndex = 9
        '
        'Button_Browse
        '
        Me.Button_Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Browse.Location = New System.Drawing.Point(420, 14)
        Me.Button_Browse.Name = "Button_Browse"
        Me.Button_Browse.Size = New System.Drawing.Size(34, 23)
        Me.Button_Browse.TabIndex = 10
        Me.Button_Browse.Text = "..."
        Me.Button_Browse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "File path:"
        '
        'GroupBox_File
        '
        Me.GroupBox_File.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_File.Controls.Add(Me.Label1)
        Me.GroupBox_File.Controls.Add(Me.TextBox_File)
        Me.GroupBox_File.Controls.Add(Me.Button_Browse)
        Me.GroupBox_File.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_File.Name = "GroupBox_File"
        Me.GroupBox_File.Size = New System.Drawing.Size(460, 46)
        Me.GroupBox_File.TabIndex = 12
        Me.GroupBox_File.TabStop = False
        '
        'GroupBox_Options
        '
        Me.GroupBox_Options.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Options.Controls.Add(Me.GroupBox_SeriesProperties)
        Me.GroupBox_Options.Controls.Add(Me.GroupBox_DisplayOptions)
        Me.GroupBox_Options.Location = New System.Drawing.Point(12, 64)
        Me.GroupBox_Options.Name = "GroupBox_Options"
        Me.GroupBox_Options.Size = New System.Drawing.Size(460, 241)
        Me.GroupBox_Options.TabIndex = 13
        Me.GroupBox_Options.TabStop = False
        Me.GroupBox_Options.Text = "Options"
        '
        'SaveProjectFileDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 351)
        Me.Controls.Add(Me.GroupBox_Options)
        Me.Controls.Add(Me.GroupBox_File)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(260, 390)
        Me.Name = "SaveProjectFileDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Save project file"
        Me.GroupBox_DisplayOptions.ResumeLayout(False)
        Me.GroupBox_DisplayOptions.PerformLayout()
        Me.GroupBox_SeriesProperties.ResumeLayout(False)
        Me.GroupBox_SeriesProperties.PerformLayout()
        Me.GroupBox_File.ResumeLayout(False)
        Me.GroupBox_File.PerformLayout()
        Me.GroupBox_Options.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox_DisplayOptions As GroupBox
    Friend WithEvents CheckBox_PointsVisibility As CheckBox
    Friend WithEvents CheckBox_LineWidth As CheckBox
    Friend WithEvents CheckBox_LineStyle As CheckBox
    Friend WithEvents CheckBox_Color As CheckBox
    Friend WithEvents CheckBox_Title As CheckBox
    Friend WithEvents CheckBox_Unit As CheckBox
    Friend WithEvents CheckBox_Interpretation As CheckBox
    Friend WithEvents GroupBox_SeriesProperties As GroupBox
    Friend WithEvents TextBox_File As TextBox
    Friend WithEvents Button_Browse As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox_File As GroupBox
    Friend WithEvents GroupBox_Options As GroupBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class
