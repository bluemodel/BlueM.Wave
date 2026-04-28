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
        Button_OK = New Button()
        Button_Cancel = New Button()
        GroupBox_DisplayOptions = New GroupBox()
        CheckBox_PointsVisibility = New CheckBox()
        CheckBox_LineWidth = New CheckBox()
        CheckBox_LineStyle = New CheckBox()
        CheckBox_Color = New CheckBox()
        CheckBox_Title = New CheckBox()
        CheckBox_Unit = New CheckBox()
        CheckBox_Interpretation = New CheckBox()
        GroupBox_SeriesProperties = New GroupBox()
        TextBox_File = New TextBox()
        Button_Browse = New Button()
        Label1 = New Label()
        GroupBox_File = New GroupBox()
        GroupBox_Options = New GroupBox()
        CheckBox_RelativePaths = New CheckBox()
        SaveFileDialog1 = New SaveFileDialog()
        GroupBox_DisplayOptions.SuspendLayout()
        GroupBox_SeriesProperties.SuspendLayout()
        GroupBox_File.SuspendLayout()
        GroupBox_Options.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(163, 284)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 1
        Button_OK.Text = "Ok"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' Button_Cancel
        ' 
        Button_Cancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Cancel.DialogResult = DialogResult.Cancel
        Button_Cancel.Location = New Point(249, 284)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New Size(80, 27)
        Button_Cancel.TabIndex = 5
        Button_Cancel.Text = "Cancel"
        Button_Cancel.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_DisplayOptions
        ' 
        GroupBox_DisplayOptions.Controls.Add(CheckBox_PointsVisibility)
        GroupBox_DisplayOptions.Controls.Add(CheckBox_LineWidth)
        GroupBox_DisplayOptions.Controls.Add(CheckBox_LineStyle)
        GroupBox_DisplayOptions.Controls.Add(CheckBox_Color)
        GroupBox_DisplayOptions.Location = New Point(161, 48)
        GroupBox_DisplayOptions.Name = "GroupBox_DisplayOptions"
        GroupBox_DisplayOptions.Padding = New Padding(4, 3, 4, 3)
        GroupBox_DisplayOptions.Size = New Size(144, 138)
        GroupBox_DisplayOptions.TabIndex = 7
        GroupBox_DisplayOptions.TabStop = False
        GroupBox_DisplayOptions.Text = "Display options"
        ' 
        ' CheckBox_PointsVisibility
        ' 
        CheckBox_PointsVisibility.AutoSize = True
        CheckBox_PointsVisibility.Location = New Point(7, 102)
        CheckBox_PointsVisibility.Name = "CheckBox_PointsVisibility"
        CheckBox_PointsVisibility.Size = New Size(105, 19)
        CheckBox_PointsVisibility.TabIndex = 13
        CheckBox_PointsVisibility.Text = "Points visibility"
        CheckBox_PointsVisibility.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_LineWidth
        ' 
        CheckBox_LineWidth.AutoSize = True
        CheckBox_LineWidth.Location = New Point(7, 75)
        CheckBox_LineWidth.Name = "CheckBox_LineWidth"
        CheckBox_LineWidth.Size = New Size(81, 19)
        CheckBox_LineWidth.TabIndex = 12
        CheckBox_LineWidth.Text = "Line width"
        CheckBox_LineWidth.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_LineStyle
        ' 
        CheckBox_LineStyle.AutoSize = True
        CheckBox_LineStyle.Location = New Point(7, 48)
        CheckBox_LineStyle.Name = "CheckBox_LineStyle"
        CheckBox_LineStyle.Size = New Size(75, 19)
        CheckBox_LineStyle.TabIndex = 8
        CheckBox_LineStyle.Text = "Line style"
        CheckBox_LineStyle.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_Color
        ' 
        CheckBox_Color.AutoSize = True
        CheckBox_Color.Location = New Point(7, 22)
        CheckBox_Color.Name = "CheckBox_Color"
        CheckBox_Color.Size = New Size(78, 19)
        CheckBox_Color.TabIndex = 7
        CheckBox_Color.Text = "Line color"
        CheckBox_Color.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_Title
        ' 
        CheckBox_Title.AutoSize = True
        CheckBox_Title.Location = New Point(7, 22)
        CheckBox_Title.Name = "CheckBox_Title"
        CheckBox_Title.Size = New Size(48, 19)
        CheckBox_Title.TabIndex = 9
        CheckBox_Title.Text = "Title"
        CheckBox_Title.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_Unit
        ' 
        CheckBox_Unit.AutoSize = True
        CheckBox_Unit.Location = New Point(7, 48)
        CheckBox_Unit.Name = "CheckBox_Unit"
        CheckBox_Unit.Size = New Size(48, 19)
        CheckBox_Unit.TabIndex = 10
        CheckBox_Unit.Text = "Unit"
        CheckBox_Unit.UseVisualStyleBackColor = True
        ' 
        ' CheckBox_Interpretation
        ' 
        CheckBox_Interpretation.AutoSize = True
        CheckBox_Interpretation.Location = New Point(7, 75)
        CheckBox_Interpretation.Name = "CheckBox_Interpretation"
        CheckBox_Interpretation.Size = New Size(98, 19)
        CheckBox_Interpretation.TabIndex = 11
        CheckBox_Interpretation.Text = "Interpretation"
        CheckBox_Interpretation.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_SeriesProperties
        ' 
        GroupBox_SeriesProperties.Controls.Add(CheckBox_Title)
        GroupBox_SeriesProperties.Controls.Add(CheckBox_Unit)
        GroupBox_SeriesProperties.Controls.Add(CheckBox_Interpretation)
        GroupBox_SeriesProperties.Location = New Point(10, 48)
        GroupBox_SeriesProperties.Name = "GroupBox_SeriesProperties"
        GroupBox_SeriesProperties.Padding = New Padding(4, 3, 4, 3)
        GroupBox_SeriesProperties.Size = New Size(144, 138)
        GroupBox_SeriesProperties.TabIndex = 8
        GroupBox_SeriesProperties.TabStop = False
        GroupBox_SeriesProperties.Text = "Series properties"
        ' 
        ' TextBox_File
        ' 
        TextBox_File.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_File.Location = New Point(72, 17)
        TextBox_File.Name = "TextBox_File"
        TextBox_File.Size = New Size(188, 23)
        TextBox_File.TabIndex = 9
        ' 
        ' Button_Browse
        ' 
        Button_Browse.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_Browse.Location = New Point(268, 16)
        Button_Browse.Name = "Button_Browse"
        Button_Browse.Size = New Size(40, 27)
        Button_Browse.TabIndex = 10
        Button_Browse.Text = "..."
        Button_Browse.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(7, 22)
        Label1.Name = "Label1"
        Label1.Size = New Size(55, 15)
        Label1.TabIndex = 11
        Label1.Text = "File path:"
        ' 
        ' GroupBox_File
        ' 
        GroupBox_File.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_File.Controls.Add(Label1)
        GroupBox_File.Controls.Add(TextBox_File)
        GroupBox_File.Controls.Add(Button_Browse)
        GroupBox_File.Location = New Point(14, 14)
        GroupBox_File.Name = "GroupBox_File"
        GroupBox_File.Padding = New Padding(4, 3, 4, 3)
        GroupBox_File.Size = New Size(315, 53)
        GroupBox_File.TabIndex = 12
        GroupBox_File.TabStop = False
        ' 
        ' GroupBox_Options
        ' 
        GroupBox_Options.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox_Options.Controls.Add(CheckBox_RelativePaths)
        GroupBox_Options.Controls.Add(GroupBox_SeriesProperties)
        GroupBox_Options.Controls.Add(GroupBox_DisplayOptions)
        GroupBox_Options.Location = New Point(14, 74)
        GroupBox_Options.Name = "GroupBox_Options"
        GroupBox_Options.Padding = New Padding(4, 3, 4, 3)
        GroupBox_Options.Size = New Size(315, 197)
        GroupBox_Options.TabIndex = 13
        GroupBox_Options.TabStop = False
        GroupBox_Options.Text = "Options"
        ' 
        ' CheckBox_RelativePaths
        ' 
        CheckBox_RelativePaths.AutoSize = True
        CheckBox_RelativePaths.Location = New Point(18, 22)
        CheckBox_RelativePaths.Name = "CheckBox_RelativePaths"
        CheckBox_RelativePaths.Size = New Size(123, 19)
        CheckBox_RelativePaths.TabIndex = 9
        CheckBox_RelativePaths.Text = "Save relative paths"
        CheckBox_RelativePaths.UseVisualStyleBackColor = True
        ' 
        ' SaveProjectFileDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(343, 324)
        Controls.Add(GroupBox_Options)
        Controls.Add(GroupBox_File)
        Controls.Add(Button_OK)
        Controls.Add(Button_Cancel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(359, 363)
        Name = "SaveProjectFileDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Save project file"
        GroupBox_DisplayOptions.ResumeLayout(False)
        GroupBox_DisplayOptions.PerformLayout()
        GroupBox_SeriesProperties.ResumeLayout(False)
        GroupBox_SeriesProperties.PerformLayout()
        GroupBox_File.ResumeLayout(False)
        GroupBox_File.PerformLayout()
        GroupBox_Options.ResumeLayout(False)
        GroupBox_Options.PerformLayout()
        ResumeLayout(False)

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
    Friend WithEvents CheckBox_RelativePaths As CheckBox
End Class
