<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CalculatorDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CalculatorDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TextBox_Title = New System.Windows.Forms.TextBox()
        Me.TextBox_Formula = New System.Windows.Forms.TextBox()
        Me.ListBox_Variables = New System.Windows.Forms.ListBox()
        Me.Label_Formula = New System.Windows.Forms.Label()
        Me.Label_Variables = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox_Unit = New System.Windows.Forms.ComboBox()
        Me.LinkLabel_Help = New System.Windows.Forms.LinkLabel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(276, 259)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'TextBox_Title
        '
        Me.TextBox_Title.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Title.Location = New System.Drawing.Point(12, 233)
        Me.TextBox_Title.Name = "TextBox_Title"
        Me.TextBox_Title.Size = New System.Drawing.Size(301, 20)
        Me.TextBox_Title.TabIndex = 5
        Me.TextBox_Title.Text = "Calculation result"
        '
        'TextBox_Formula
        '
        Me.TextBox_Formula.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Formula.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Formula.Location = New System.Drawing.Point(12, 25)
        Me.TextBox_Formula.Name = "TextBox_Formula"
        Me.TextBox_Formula.Size = New System.Drawing.Size(407, 23)
        Me.TextBox_Formula.TabIndex = 1
        '
        'ListBox_Variables
        '
        Me.ListBox_Variables.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Variables.FormattingEnabled = True
        Me.ListBox_Variables.Location = New System.Drawing.Point(12, 67)
        Me.ListBox_Variables.Name = "ListBox_Variables"
        Me.ListBox_Variables.Size = New System.Drawing.Size(407, 147)
        Me.ListBox_Variables.TabIndex = 3
        '
        'Label_Formula
        '
        Me.Label_Formula.AutoSize = True
        Me.Label_Formula.Location = New System.Drawing.Point(10, 9)
        Me.Label_Formula.Name = "Label_Formula"
        Me.Label_Formula.Size = New System.Drawing.Size(47, 13)
        Me.Label_Formula.TabIndex = 0
        Me.Label_Formula.Text = "Formula:"
        '
        'Label_Variables
        '
        Me.Label_Variables.AutoSize = True
        Me.Label_Variables.Location = New System.Drawing.Point(10, 51)
        Me.Label_Variables.Name = "Label_Variables"
        Me.Label_Variables.Size = New System.Drawing.Size(53, 13)
        Me.Label_Variables.TabIndex = 2
        Me.Label_Variables.Text = "Variables:"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 217)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Result series title:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(316, 217)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Unit:"
        '
        'ComboBox_Unit
        '
        Me.ComboBox_Unit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Unit.FormattingEnabled = True
        Me.ComboBox_Unit.Location = New System.Drawing.Point(319, 232)
        Me.ComboBox_Unit.Name = "ComboBox_Unit"
        Me.ComboBox_Unit.Size = New System.Drawing.Size(100, 21)
        Me.ComboBox_Unit.TabIndex = 7
        '
        'LinkLabel_Help
        '
        Me.LinkLabel_Help.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_Help.AutoSize = True
        Me.LinkLabel_Help.Location = New System.Drawing.Point(10, 267)
        Me.LinkLabel_Help.Name = "LinkLabel_Help"
        Me.LinkLabel_Help.Size = New System.Drawing.Size(29, 13)
        Me.LinkLabel_Help.TabIndex = 9
        Me.LinkLabel_Help.TabStop = True
        Me.LinkLabel_Help.Text = "Help"
        '
        'CalculatorDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(434, 297)
        Me.Controls.Add(Me.LinkLabel_Help)
        Me.Controls.Add(Me.ComboBox_Unit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label_Variables)
        Me.Controls.Add(Me.Label_Formula)
        Me.Controls.Add(Me.ListBox_Variables)
        Me.Controls.Add(Me.TextBox_Title)
        Me.Controls.Add(Me.TextBox_Formula)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 336)
        Me.Name = "CalculatorDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Calculator"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
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
