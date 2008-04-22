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
        Me.CheckedListBox_Zeitreihen = New System.Windows.Forms.ListBox
        Me.Button_OK = New System.Windows.Forms.Button
        Me.ComboBox_Format = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'CheckedListBox_Zeitreihen
        '
        Me.CheckedListBox_Zeitreihen.FormattingEnabled = True
        Me.CheckedListBox_Zeitreihen.Location = New System.Drawing.Point(26, 89)
        Me.CheckedListBox_Zeitreihen.Name = "CheckedListBox_Zeitreihen"
        Me.CheckedListBox_Zeitreihen.Size = New System.Drawing.Size(243, 109)
        Me.CheckedListBox_Zeitreihen.TabIndex = 0
        '
        'Button_OK
        '
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(49, 226)
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
        Me.ComboBox_Format.Location = New System.Drawing.Point(26, 13)
        Me.ComboBox_Format.Name = "ComboBox_Format"
        Me.ComboBox_Format.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_Format.TabIndex = 2
        '
        'ExportDiag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ComboBox_Format)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.CheckedListBox_Zeitreihen)
        Me.Name = "ExportDiag"
        Me.Text = "ExportDiag"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CheckedListBox_Zeitreihen As System.Windows.Forms.ListBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Format As System.Windows.Forms.ComboBox
End Class
