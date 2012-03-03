<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportDiag
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
Dim Label_Format As System.Windows.Forms.Label
Dim Label_Zeitreihen As System.Windows.Forms.Label
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExportDiag))
Me.ListBox_Zeitreihen = New System.Windows.Forms.ListBox
Me.Button_OK = New System.Windows.Forms.Button
Me.ComboBox_Format = New System.Windows.Forms.ComboBox
Me.Button_Cancel = New System.Windows.Forms.Button
Me.Button_SelectAll = New System.Windows.Forms.Button
Label_Format = New System.Windows.Forms.Label
Label_Zeitreihen = New System.Windows.Forms.Label
Me.SuspendLayout()
'
'Label_Format
'
Label_Format.AutoSize = True
Label_Format.Location = New System.Drawing.Point(12, 12)
Label_Format.Name = "Label_Format"
Label_Format.Size = New System.Drawing.Size(42, 13)
Label_Format.TabIndex = 3
Label_Format.Text = "Format:"
'
'Label_Zeitreihen
'
Label_Zeitreihen.AutoSize = True
Label_Zeitreihen.Location = New System.Drawing.Point(12, 46)
Label_Zeitreihen.Name = "Label_Zeitreihen"
Label_Zeitreihen.Size = New System.Drawing.Size(63, 13)
Label_Zeitreihen.TabIndex = 4
Label_Zeitreihen.Text = "Zeitreihe(n):"
'
'ListBox_Zeitreihen
'
Me.ListBox_Zeitreihen.FormattingEnabled = True
Me.ListBox_Zeitreihen.Location = New System.Drawing.Point(12, 62)
Me.ListBox_Zeitreihen.Name = "ListBox_Zeitreihen"
Me.ListBox_Zeitreihen.Size = New System.Drawing.Size(169, 108)
Me.ListBox_Zeitreihen.TabIndex = 0
'
'Button_OK
'
Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
Me.Button_OK.Location = New System.Drawing.Point(106, 186)
Me.Button_OK.Name = "Button_OK"
Me.Button_OK.Size = New System.Drawing.Size(75, 23)
Me.Button_OK.TabIndex = 1
Me.Button_OK.Text = "Ok"
Me.Button_OK.UseVisualStyleBackColor = True
'
'ComboBox_Format
'
Me.ComboBox_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.ComboBox_Format.FormattingEnabled = True
Me.ComboBox_Format.Location = New System.Drawing.Point(60, 9)
Me.ComboBox_Format.Name = "ComboBox_Format"
Me.ComboBox_Format.Size = New System.Drawing.Size(121, 21)
Me.ComboBox_Format.TabIndex = 2
'
'Button_Cancel
'
Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.Button_Cancel.Location = New System.Drawing.Point(25, 186)
Me.Button_Cancel.Name = "Button_Cancel"
Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
Me.Button_Cancel.TabIndex = 5
Me.Button_Cancel.Text = "Abbrechen"
Me.Button_Cancel.UseVisualStyleBackColor = True
'
'Button_SelectAll
'
Me.Button_SelectAll.Location = New System.Drawing.Point(120, 36)
Me.Button_SelectAll.Name = "Button_SelectAll"
Me.Button_SelectAll.Size = New System.Drawing.Size(61, 23)
Me.Button_SelectAll.TabIndex = 5
Me.Button_SelectAll.Text = "Select All"
Me.Button_SelectAll.UseVisualStyleBackColor = True
'
'ExportDiag
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(193, 221)
Me.Controls.Add(Me.Button_SelectAll)
Me.Controls.Add(Me.Button_Cancel)
Me.Controls.Add(Label_Zeitreihen)
Me.Controls.Add(Label_Format)
Me.Controls.Add(Me.ComboBox_Format)
Me.Controls.Add(Me.Button_OK)
Me.Controls.Add(Me.ListBox_Zeitreihen)
Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "ExportDiag"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
Me.Text = "Zeitreihen exportieren"
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents ListBox_Zeitreihen As System.Windows.Forms.ListBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Format As System.Windows.Forms.ComboBox
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents Button_SelectAll As System.Windows.Forms.Button
End Class
