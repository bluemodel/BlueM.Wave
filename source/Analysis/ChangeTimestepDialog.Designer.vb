<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeTimestepDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangeTimestepDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        Label2 = New Label()
        ComboBox_InputInterpretation = New ComboBox()
        Label3 = New Label()
        ComboBox_TimestepType = New ComboBox()
        NumericUpDown_TimestepInterval = New NumericUpDown()
        MaskedTextBox_Start = New MaskedTextBox()
        Label4 = New Label()
        CheckBox_IgnoreNaN = New CheckBox()
        Label_IgnoreNaN = New Label()
        ComboBox_OutputInterpretation = New ComboBox()
        Label1 = New Label()
        CType(NumericUpDown_TimestepInterval, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(293, 162)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(379, 162)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(14, 10)
        Label2.Name = "Label2"
        Label2.Size = New Size(113, 15)
        Label2.TabIndex = 0
        Label2.Text = "Input interpretation:"
        ' 
        ' ComboBox_InputInterpretation
        ' 
        ComboBox_InputInterpretation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_InputInterpretation.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_InputInterpretation.FormattingEnabled = True
        ComboBox_InputInterpretation.Location = New Point(145, 7)
        ComboBox_InputInterpretation.Name = "ComboBox_InputInterpretation"
        ComboBox_InputInterpretation.Size = New Size(314, 23)
        ComboBox_InputInterpretation.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(14, 72)
        Label3.Name = "Label3"
        Label3.Size = New Size(100, 15)
        Label3.TabIndex = 2
        Label3.Text = "Timestep interval:"
        ' 
        ' ComboBox_TimestepType
        ' 
        ComboBox_TimestepType.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_TimestepType.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_TimestepType.FormattingEnabled = True
        ComboBox_TimestepType.Location = New Point(214, 69)
        ComboBox_TimestepType.Name = "ComboBox_TimestepType"
        ComboBox_TimestepType.Size = New Size(245, 23)
        ComboBox_TimestepType.TabIndex = 4
        ' 
        ' NumericUpDown_TimestepInterval
        ' 
        NumericUpDown_TimestepInterval.Location = New Point(145, 69)
        NumericUpDown_TimestepInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_TimestepInterval.Name = "NumericUpDown_TimestepInterval"
        NumericUpDown_TimestepInterval.Size = New Size(63, 23)
        NumericUpDown_TimestepInterval.TabIndex = 3
        NumericUpDown_TimestepInterval.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' MaskedTextBox_Start
        ' 
        MaskedTextBox_Start.Culture = New System.Globalization.CultureInfo("")
        MaskedTextBox_Start.Location = New Point(145, 100)
        MaskedTextBox_Start.Name = "MaskedTextBox_Start"
        MaskedTextBox_Start.Size = New Size(116, 23)
        MaskedTextBox_Start.TabIndex = 6
        MaskedTextBox_Start.ValidatingType = GetType(Date)
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(14, 104)
        Label4.Name = "Label4"
        Label4.Size = New Size(34, 15)
        Label4.TabIndex = 5
        Label4.Text = "Start:"
        ' 
        ' CheckBox_IgnoreNaN
        ' 
        CheckBox_IgnoreNaN.AutoSize = True
        CheckBox_IgnoreNaN.Location = New Point(145, 132)
        CheckBox_IgnoreNaN.Name = "CheckBox_IgnoreNaN"
        CheckBox_IgnoreNaN.Size = New Size(15, 14)
        CheckBox_IgnoreNaN.TabIndex = 8
        CheckBox_IgnoreNaN.UseVisualStyleBackColor = True
        ' 
        ' Label_IgnoreNaN
        ' 
        Label_IgnoreNaN.AutoSize = True
        Label_IgnoreNaN.Location = New Point(14, 133)
        Label_IgnoreNaN.Name = "Label_IgnoreNaN"
        Label_IgnoreNaN.Size = New Size(71, 15)
        Label_IgnoreNaN.TabIndex = 7
        Label_IgnoreNaN.Text = "Ignore NaN:"
        ' 
        ' ComboBox_OutputInterpretation
        ' 
        ComboBox_OutputInterpretation.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_OutputInterpretation.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_OutputInterpretation.FormattingEnabled = True
        ComboBox_OutputInterpretation.Location = New Point(145, 38)
        ComboBox_OutputInterpretation.Name = "ComboBox_OutputInterpretation"
        ComboBox_OutputInterpretation.Size = New Size(314, 23)
        ComboBox_OutputInterpretation.TabIndex = 10
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 42)
        Label1.Name = "Label1"
        Label1.Size = New Size(123, 15)
        Label1.TabIndex = 9
        Label1.Text = "Output interpretation:"
        ' 
        ' ChangeTimestepDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(471, 201)
        Controls.Add(OK_Button)
        Controls.Add(Label2)
        Controls.Add(Cancel_Button)
        Controls.Add(ComboBox_InputInterpretation)
        Controls.Add(Label1)
        Controls.Add(ComboBox_OutputInterpretation)
        Controls.Add(Label3)
        Controls.Add(NumericUpDown_TimestepInterval)
        Controls.Add(ComboBox_TimestepType)
        Controls.Add(Label4)
        Controls.Add(MaskedTextBox_Start)
        Controls.Add(Label_IgnoreNaN)
        Controls.Add(CheckBox_IgnoreNaN)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(487, 190)
        Name = "ChangeTimestepDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Change Timestep"
        CType(NumericUpDown_TimestepInterval, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_InputInterpretation As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_TimestepType As System.Windows.Forms.ComboBox
    Friend WithEvents NumericUpDown_TimestepInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents MaskedTextBox_Start As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_IgnoreNaN As CheckBox
    Friend WithEvents Label_IgnoreNaN As Label
    Friend WithEvents ComboBox_OutputInterpretation As ComboBox
    Friend WithEvents Label1 As Label
End Class
