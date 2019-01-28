<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConvertErrorValuesDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConvertErrorValuesDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.ComboBox_Series = New System.Windows.Forms.ComboBox
        Me.Label_Series = New System.Windows.Forms.Label
        Me.TextBox_errorvalues = New System.Windows.Forms.TextBox
        Me.Label_ErrorValues = New System.Windows.Forms.Label
        Me.Label_note = New System.Windows.Forms.Label
        Me.Label_Help = New System.Windows.Forms.Label
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(140, 71)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
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
        'ComboBox_Series
        '
        Me.ComboBox_Series.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Series.FormattingEnabled = True
        Me.ComboBox_Series.Location = New System.Drawing.Point(85, 10)
        Me.ComboBox_Series.Name = "ComboBox_Series"
        Me.ComboBox_Series.Size = New System.Drawing.Size(203, 21)
        Me.ComboBox_Series.TabIndex = 1
        '
        'Label_Series
        '
        Me.Label_Series.AutoSize = True
        Me.Label_Series.Location = New System.Drawing.Point(13, 13)
        Me.Label_Series.Name = "Label_Series"
        Me.Label_Series.Size = New System.Drawing.Size(39, 13)
        Me.Label_Series.TabIndex = 2
        Me.Label_Series.Text = "Series:"
        '
        'TextBox_errorvalues
        '
        Me.TextBox_errorvalues.Location = New System.Drawing.Point(85, 38)
        Me.TextBox_errorvalues.Name = "TextBox_errorvalues"
        Me.TextBox_errorvalues.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_errorvalues.TabIndex = 3
        Me.TextBox_errorvalues.Text = "-777,-9999.999"
        '
        'Label_ErrorValues
        '
        Me.Label_ErrorValues.AutoSize = True
        Me.Label_ErrorValues.Location = New System.Drawing.Point(13, 41)
        Me.Label_ErrorValues.Name = "Label_ErrorValues"
        Me.Label_ErrorValues.Size = New System.Drawing.Size(66, 13)
        Me.Label_ErrorValues.TabIndex = 4
        Me.Label_ErrorValues.Text = "Error values:"
        '
        'Label_note
        '
        Me.Label_note.AutoSize = True
        Me.Label_note.Location = New System.Drawing.Point(191, 41)
        Me.Label_note.Name = "Label_note"
        Me.Label_note.Size = New System.Drawing.Size(97, 13)
        Me.Label_note.TabIndex = 5
        Me.Label_note.Text = "(comma-separated)"
        '
        'Label_Help
        '
        Me.Label_Help.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Help.Location = New System.Drawing.Point(13, 74)
        Me.Label_Help.Name = "Label_Help"
        Me.Label_Help.Size = New System.Drawing.Size(121, 32)
        Me.Label_Help.TabIndex = 6
        Me.Label_Help.Text = "Converts error values to NaN"
        '
        'ConvertErrorValuesDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(298, 112)
        Me.Controls.Add(Me.Label_Help)
        Me.Controls.Add(Me.Label_note)
        Me.Controls.Add(Me.Label_ErrorValues)
        Me.Controls.Add(Me.TextBox_errorvalues)
        Me.Controls.Add(Me.Label_Series)
        Me.Controls.Add(Me.ComboBox_Series)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConvertErrorValuesDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Convert error values"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Series As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Series As System.Windows.Forms.Label
    Friend WithEvents TextBox_errorvalues As System.Windows.Forms.TextBox
    Friend WithEvents Label_ErrorValues As System.Windows.Forms.Label
    Friend WithEvents Label_note As System.Windows.Forms.Label
    Friend WithEvents Label_Help As System.Windows.Forms.Label

End Class
