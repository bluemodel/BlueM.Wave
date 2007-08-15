<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TXT
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
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.Button_OK = New System.Windows.Forms.Button
        Me.Label_Trennzeichen = New System.Windows.Forms.Label
        Me.ComboBox_Trennzeichen = New System.Windows.Forms.ComboBox
        Me.Label_Dezimaltrennzeichen = New System.Windows.Forms.Label
        Me.ComboBox_Dezimaltrennzeichen = New System.Windows.Forms.ComboBox
        Me.Label_AnzKopfzeilen = New System.Windows.Forms.Label
        Me.TextBox_AnzKopfzeilen = New System.Windows.Forms.MaskedTextBox
        Me.RichTextBox_Vorschau = New System.Windows.Forms.RichTextBox
        Me.Label_Datei = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(368, 321)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 0
        Me.Button_Cancel.Text = "Abbrechen"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(287, 320)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 1
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Label_Trennzeichen
        '
        Me.Label_Trennzeichen.AutoSize = True
        Me.Label_Trennzeichen.Location = New System.Drawing.Point(13, 13)
        Me.Label_Trennzeichen.Name = "Label_Trennzeichen"
        Me.Label_Trennzeichen.Size = New System.Drawing.Size(75, 13)
        Me.Label_Trennzeichen.TabIndex = 2
        Me.Label_Trennzeichen.Text = "Trennzeichen:"
        '
        'ComboBox_Trennzeichen
        '
        Me.ComboBox_Trennzeichen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Trennzeichen.DropDownWidth = 100
        Me.ComboBox_Trennzeichen.FormattingEnabled = True
        Me.ComboBox_Trennzeichen.Location = New System.Drawing.Point(116, 10)
        Me.ComboBox_Trennzeichen.Name = "ComboBox_Trennzeichen"
        Me.ComboBox_Trennzeichen.Size = New System.Drawing.Size(100, 21)
        Me.ComboBox_Trennzeichen.TabIndex = 3
        '
        'Label_Dezimaltrennzeichen
        '
        Me.Label_Dezimaltrennzeichen.AutoSize = True
        Me.Label_Dezimaltrennzeichen.Location = New System.Drawing.Point(232, 13)
        Me.Label_Dezimaltrennzeichen.Name = "Label_Dezimaltrennzeichen"
        Me.Label_Dezimaltrennzeichen.Size = New System.Drawing.Size(108, 13)
        Me.Label_Dezimaltrennzeichen.TabIndex = 4
        Me.Label_Dezimaltrennzeichen.Text = "Dezimaltrennzeichen:"
        '
        'ComboBox_Dezimaltrennzeichen
        '
        Me.ComboBox_Dezimaltrennzeichen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Dezimaltrennzeichen.DropDownWidth = 50
        Me.ComboBox_Dezimaltrennzeichen.FormattingEnabled = True
        Me.ComboBox_Dezimaltrennzeichen.Location = New System.Drawing.Point(349, 10)
        Me.ComboBox_Dezimaltrennzeichen.Name = "ComboBox_Dezimaltrennzeichen"
        Me.ComboBox_Dezimaltrennzeichen.Size = New System.Drawing.Size(50, 21)
        Me.ComboBox_Dezimaltrennzeichen.TabIndex = 5
        '
        'Label_AnzKopfzeilen
        '
        Me.Label_AnzKopfzeilen.AutoSize = True
        Me.Label_AnzKopfzeilen.Location = New System.Drawing.Point(13, 47)
        Me.Label_AnzKopfzeilen.Name = "Label_AnzKopfzeilen"
        Me.Label_AnzKopfzeilen.Size = New System.Drawing.Size(94, 13)
        Me.Label_AnzKopfzeilen.TabIndex = 6
        Me.Label_AnzKopfzeilen.Text = "Anzahl Kopfzeilen:"
        '
        'TextBox_AnzKopfzeilen
        '
        Me.TextBox_AnzKopfzeilen.Location = New System.Drawing.Point(116, 44)
        Me.TextBox_AnzKopfzeilen.Mask = "0#"
        Me.TextBox_AnzKopfzeilen.Name = "TextBox_AnzKopfzeilen"
        Me.TextBox_AnzKopfzeilen.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.TextBox_AnzKopfzeilen.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_AnzKopfzeilen.TabIndex = 7
        '
        'RichTextBox_Vorschau
        '
        Me.RichTextBox_Vorschau.BackColor = System.Drawing.Color.White
        Me.RichTextBox_Vorschau.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox_Vorschau.Location = New System.Drawing.Point(16, 112)
        Me.RichTextBox_Vorschau.Name = "RichTextBox_Vorschau"
        Me.RichTextBox_Vorschau.ReadOnly = True
        Me.RichTextBox_Vorschau.Size = New System.Drawing.Size(230, 181)
        Me.RichTextBox_Vorschau.TabIndex = 8
        Me.RichTextBox_Vorschau.Text = ""
        Me.RichTextBox_Vorschau.WordWrap = False
        '
        'Label_Datei
        '
        Me.Label_Datei.AutoSize = True
        Me.Label_Datei.Location = New System.Drawing.Point(13, 86)
        Me.Label_Datei.Name = "Label_Datei"
        Me.Label_Datei.Size = New System.Drawing.Size(35, 13)
        Me.Label_Datei.TabIndex = 9
        Me.Label_Datei.Text = "Datei:"
        '
        'TXT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 356)
        Me.Controls.Add(Me.Label_Datei)
        Me.Controls.Add(Me.RichTextBox_Vorschau)
        Me.Controls.Add(Me.TextBox_AnzKopfzeilen)
        Me.Controls.Add(Me.Label_AnzKopfzeilen)
        Me.Controls.Add(Me.ComboBox_Dezimaltrennzeichen)
        Me.Controls.Add(Me.Label_Dezimaltrennzeichen)
        Me.Controls.Add(Me.ComboBox_Trennzeichen)
        Me.Controls.Add(Me.Label_Trennzeichen)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Name = "TXT"
        Me.Text = "Einstellungen für Textdatei"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents Label_Trennzeichen As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Trennzeichen As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Dezimaltrennzeichen As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Dezimaltrennzeichen As System.Windows.Forms.ComboBox
    Friend WithEvents Label_AnzKopfzeilen As System.Windows.Forms.Label
    Friend WithEvents TextBox_AnzKopfzeilen As System.Windows.Forms.MaskedTextBox
    Friend WithEvents RichTextBox_Vorschau As System.Windows.Forms.RichTextBox
    Friend WithEvents Label_Datei As System.Windows.Forms.Label
End Class
