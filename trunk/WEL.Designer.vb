<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WEL
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
        Me.Label_Vorschau = New System.Windows.Forms.Label
        Me.RichTextBox_Vorschau = New System.Windows.Forms.RichTextBox
        Me.ComboBox_Dezimaltrennzeichen = New System.Windows.Forms.ComboBox
        Me.Label_Dezimaltrennzeichen = New System.Windows.Forms.Label
        Me.ComboBox_Trennzeichen = New System.Windows.Forms.ComboBox
        Me.Label_Trennzeichen = New System.Windows.Forms.Label
        Me.Button_OK = New System.Windows.Forms.Button
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.ListBox_YSpalten = New System.Windows.Forms.ListBox
        Me.Label_YSpalten = New System.Windows.Forms.Label
        Me.Label_XSpalte = New System.Windows.Forms.Label
        Me.TextBox_XSpalte = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label_Vorschau
        '
        Me.Label_Vorschau.AutoSize = True
        Me.Label_Vorschau.Location = New System.Drawing.Point(12, 53)
        Me.Label_Vorschau.Name = "Label_Vorschau"
        Me.Label_Vorschau.Size = New System.Drawing.Size(55, 13)
        Me.Label_Vorschau.TabIndex = 17
        Me.Label_Vorschau.Text = "Vorschau:"
        '
        'RichTextBox_Vorschau
        '
        Me.RichTextBox_Vorschau.BackColor = System.Drawing.Color.White
        Me.RichTextBox_Vorschau.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox_Vorschau.Location = New System.Drawing.Point(12, 79)
        Me.RichTextBox_Vorschau.Name = "RichTextBox_Vorschau"
        Me.RichTextBox_Vorschau.ReadOnly = True
        Me.RichTextBox_Vorschau.Size = New System.Drawing.Size(230, 186)
        Me.RichTextBox_Vorschau.TabIndex = 16
        Me.RichTextBox_Vorschau.Text = ""
        Me.RichTextBox_Vorschau.WordWrap = False
        '
        'ComboBox_Dezimaltrennzeichen
        '
        Me.ComboBox_Dezimaltrennzeichen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Dezimaltrennzeichen.DropDownWidth = 50
        Me.ComboBox_Dezimaltrennzeichen.FormattingEnabled = True
        Me.ComboBox_Dezimaltrennzeichen.Location = New System.Drawing.Point(348, 12)
        Me.ComboBox_Dezimaltrennzeichen.Name = "ComboBox_Dezimaltrennzeichen"
        Me.ComboBox_Dezimaltrennzeichen.Size = New System.Drawing.Size(50, 21)
        Me.ComboBox_Dezimaltrennzeichen.TabIndex = 15
        '
        'Label_Dezimaltrennzeichen
        '
        Me.Label_Dezimaltrennzeichen.AutoSize = True
        Me.Label_Dezimaltrennzeichen.Location = New System.Drawing.Point(234, 15)
        Me.Label_Dezimaltrennzeichen.Name = "Label_Dezimaltrennzeichen"
        Me.Label_Dezimaltrennzeichen.Size = New System.Drawing.Size(108, 13)
        Me.Label_Dezimaltrennzeichen.TabIndex = 14
        Me.Label_Dezimaltrennzeichen.Text = "Dezimaltrennzeichen:"
        '
        'ComboBox_Trennzeichen
        '
        Me.ComboBox_Trennzeichen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Trennzeichen.DropDownWidth = 100
        Me.ComboBox_Trennzeichen.FormattingEnabled = True
        Me.ComboBox_Trennzeichen.Location = New System.Drawing.Point(93, 12)
        Me.ComboBox_Trennzeichen.Name = "ComboBox_Trennzeichen"
        Me.ComboBox_Trennzeichen.Size = New System.Drawing.Size(100, 21)
        Me.ComboBox_Trennzeichen.TabIndex = 13
        '
        'Label_Trennzeichen
        '
        Me.Label_Trennzeichen.AutoSize = True
        Me.Label_Trennzeichen.Location = New System.Drawing.Point(12, 15)
        Me.Label_Trennzeichen.Name = "Label_Trennzeichen"
        Me.Label_Trennzeichen.Size = New System.Drawing.Size(75, 13)
        Me.Label_Trennzeichen.TabIndex = 12
        Me.Label_Trennzeichen.Text = "Trennzeichen:"
        '
        'Button_OK
        '
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(249, 283)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 11
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(330, 284)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 10
        Me.Button_Cancel.Text = "Abbrechen"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'ListBox_YSpalten
        '
        Me.ListBox_YSpalten.FormattingEnabled = True
        Me.ListBox_YSpalten.Location = New System.Drawing.Point(266, 131)
        Me.ListBox_YSpalten.Name = "ListBox_YSpalten"
        Me.ListBox_YSpalten.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_YSpalten.Size = New System.Drawing.Size(120, 134)
        Me.ListBox_YSpalten.TabIndex = 18
        '
        'Label_YSpalten
        '
        Me.Label_YSpalten.AutoSize = True
        Me.Label_YSpalten.Location = New System.Drawing.Point(263, 106)
        Me.Label_YSpalten.Name = "Label_YSpalten"
        Me.Label_YSpalten.Size = New System.Drawing.Size(56, 13)
        Me.Label_YSpalten.TabIndex = 17
        Me.Label_YSpalten.Text = "Y-Spalten:"
        '
        'Label_XSpalte
        '
        Me.Label_XSpalte.AutoSize = True
        Me.Label_XSpalte.Location = New System.Drawing.Point(263, 53)
        Me.Label_XSpalte.Name = "Label_XSpalte"
        Me.Label_XSpalte.Size = New System.Drawing.Size(50, 13)
        Me.Label_XSpalte.TabIndex = 17
        Me.Label_XSpalte.Text = "X-Spalte:"
        '
        'TextBox_XSpalte
        '
        Me.TextBox_XSpalte.Location = New System.Drawing.Point(266, 79)
        Me.TextBox_XSpalte.Name = "TextBox_XSpalte"
        Me.TextBox_XSpalte.ReadOnly = True
        Me.TextBox_XSpalte.Size = New System.Drawing.Size(120, 20)
        Me.TextBox_XSpalte.TabIndex = 19
        '
        'WEL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 315)
        Me.Controls.Add(Me.TextBox_XSpalte)
        Me.Controls.Add(Me.ListBox_YSpalten)
        Me.Controls.Add(Me.Label_XSpalte)
        Me.Controls.Add(Me.Label_YSpalten)
        Me.Controls.Add(Me.Label_Vorschau)
        Me.Controls.Add(Me.RichTextBox_Vorschau)
        Me.Controls.Add(Me.ComboBox_Dezimaltrennzeichen)
        Me.Controls.Add(Me.Label_Dezimaltrennzeichen)
        Me.Controls.Add(Me.ComboBox_Trennzeichen)
        Me.Controls.Add(Me.Label_Trennzeichen)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Name = "WEL"
        Me.Text = "Einstellungen für WEL-Datei"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label_Vorschau As System.Windows.Forms.Label
    Friend WithEvents RichTextBox_Vorschau As System.Windows.Forms.RichTextBox
    Friend WithEvents ComboBox_Dezimaltrennzeichen As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Dezimaltrennzeichen As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Trennzeichen As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Trennzeichen As System.Windows.Forms.Label
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents ListBox_YSpalten As System.Windows.Forms.ListBox
    Friend WithEvents Label_YSpalten As System.Windows.Forms.Label
    Friend WithEvents Label_XSpalte As System.Windows.Forms.Label
    Friend WithEvents TextBox_XSpalte As System.Windows.Forms.TextBox
End Class
