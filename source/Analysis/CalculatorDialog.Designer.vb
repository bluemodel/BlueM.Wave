<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CalculatorDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CalculatorDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        TextBox_Title = New TextBox()
        TextBox_Formula = New TextBox()
        ListBox_Variables = New ListBox()
        Label_Formula = New Label()
        Label_Variables = New Label()
        Label1 = New Label()
        Label2 = New Label()
        ComboBox_Unit = New ComboBox()
        LinkLabel_Help = New LinkLabel()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(328, 304)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(414, 304)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' TextBox_Title
        ' 
        TextBox_Title.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_Title.Location = New Point(14, 269)
        TextBox_Title.Name = "TextBox_Title"
        TextBox_Title.Size = New Size(358, 23)
        TextBox_Title.TabIndex = 5
        TextBox_Title.Text = "Calculation result"
        ' 
        ' TextBox_Formula
        ' 
        TextBox_Formula.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_Formula.Font = New Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TextBox_Formula.Location = New Point(14, 29)
        TextBox_Formula.Name = "TextBox_Formula"
        TextBox_Formula.Size = New Size(480, 23)
        TextBox_Formula.TabIndex = 1
        ' 
        ' ListBox_Variables
        ' 
        ListBox_Variables.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox_Variables.FormattingEnabled = True
        ListBox_Variables.Location = New Point(14, 77)
        ListBox_Variables.Name = "ListBox_Variables"
        ListBox_Variables.Size = New Size(480, 169)
        ListBox_Variables.TabIndex = 3
        ' 
        ' Label_Formula
        ' 
        Label_Formula.AutoSize = True
        Label_Formula.Location = New Point(12, 10)
        Label_Formula.Name = "Label_Formula"
        Label_Formula.Size = New Size(54, 15)
        Label_Formula.TabIndex = 0
        Label_Formula.Text = "Formula:"
        ' 
        ' Label_Variables
        ' 
        Label_Variables.AutoSize = True
        Label_Variables.Location = New Point(12, 59)
        Label_Variables.Name = "Label_Variables"
        Label_Variables.Size = New Size(56, 15)
        Label_Variables.TabIndex = 2
        Label_Variables.Text = "Variables:"
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label1.AutoSize = True
        Label1.Location = New Point(12, 250)
        Label1.Name = "Label1"
        Label1.Size = New Size(97, 15)
        Label1.TabIndex = 4
        Label1.Text = "Result series title:"
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label2.AutoSize = True
        Label2.Location = New Point(369, 250)
        Label2.Name = "Label2"
        Label2.Size = New Size(32, 15)
        Label2.TabIndex = 6
        Label2.Text = "Unit:"
        ' 
        ' ComboBox_Unit
        ' 
        ComboBox_Unit.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ComboBox_Unit.FormattingEnabled = True
        ComboBox_Unit.Location = New Point(378, 268)
        ComboBox_Unit.Name = "ComboBox_Unit"
        ComboBox_Unit.Size = New Size(116, 23)
        ComboBox_Unit.TabIndex = 7
        ' 
        ' LinkLabel_Help
        ' 
        LinkLabel_Help.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LinkLabel_Help.AutoSize = True
        LinkLabel_Help.Location = New Point(12, 308)
        LinkLabel_Help.Name = "LinkLabel_Help"
        LinkLabel_Help.Size = New Size(32, 15)
        LinkLabel_Help.TabIndex = 9
        LinkLabel_Help.TabStop = True
        LinkLabel_Help.Text = "Help"
        ' 
        ' CalculatorDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(506, 343)
        Controls.Add(OK_Button)
        Controls.Add(LinkLabel_Help)
        Controls.Add(Cancel_Button)
        Controls.Add(ComboBox_Unit)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Label_Variables)
        Controls.Add(Label_Formula)
        Controls.Add(ListBox_Variables)
        Controls.Add(TextBox_Title)
        Controls.Add(TextBox_Formula)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(522, 382)
        Name = "CalculatorDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Calculator"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TextBox_Title As TextBox
    Friend WithEvents TextBox_Formula As TextBox
    Friend WithEvents ListBox_Variables As ListBox
    Friend WithEvents Label_Formula As Label
    Friend WithEvents Label_Variables As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox_Unit As ComboBox
    Friend WithEvents LinkLabel_Help As LinkLabel
End Class
