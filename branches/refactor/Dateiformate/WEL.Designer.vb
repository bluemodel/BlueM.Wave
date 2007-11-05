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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WEL))
        Me.Label_Datei = New System.Windows.Forms.Label
        Me.RichTextBox_Vorschau = New System.Windows.Forms.RichTextBox
        Me.ComboBox_Trennzeichen = New System.Windows.Forms.ComboBox
        Me.Button_OK = New System.Windows.Forms.Button
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.ListBox_YSpalten = New System.Windows.Forms.ListBox
        Me.Label_YSpalten = New System.Windows.Forms.Label
        Me.Label_XSpalte = New System.Windows.Forms.Label
        Me.TextBox_XSpalte = New System.Windows.Forms.TextBox
        Me.RadioButton_Spaltenbreite = New System.Windows.Forms.RadioButton
        Me.RadioButton_Zeichengetrennt = New System.Windows.Forms.RadioButton
        Me.GroupBox_Spaltenmodus = New System.Windows.Forms.GroupBox
        Me.TextBox_Spaltenbreite = New System.Windows.Forms.TextBox
        Me.GroupBox_Spaltenmodus.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_Datei
        '
        Me.Label_Datei.AutoSize = True
        Me.Label_Datei.Location = New System.Drawing.Point(12, 115)
        Me.Label_Datei.Name = "Label_Datei"
        Me.Label_Datei.Size = New System.Drawing.Size(35, 13)
        Me.Label_Datei.TabIndex = 17
        Me.Label_Datei.Text = "Datei:"
        '
        'RichTextBox_Vorschau
        '
        Me.RichTextBox_Vorschau.BackColor = System.Drawing.Color.White
        Me.RichTextBox_Vorschau.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox_Vorschau.Location = New System.Drawing.Point(12, 141)
        Me.RichTextBox_Vorschau.Name = "RichTextBox_Vorschau"
        Me.RichTextBox_Vorschau.ReadOnly = True
        Me.RichTextBox_Vorschau.Size = New System.Drawing.Size(230, 186)
        Me.RichTextBox_Vorschau.TabIndex = 16
        Me.RichTextBox_Vorschau.Text = ""
        Me.RichTextBox_Vorschau.WordWrap = False
        '
        'ComboBox_Trennzeichen
        '
        Me.ComboBox_Trennzeichen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Trennzeichen.DropDownWidth = 100
        Me.ComboBox_Trennzeichen.FormattingEnabled = True
        Me.ComboBox_Trennzeichen.Location = New System.Drawing.Point(124, 18)
        Me.ComboBox_Trennzeichen.Name = "ComboBox_Trennzeichen"
        Me.ComboBox_Trennzeichen.Size = New System.Drawing.Size(97, 21)
        Me.ComboBox_Trennzeichen.TabIndex = 13
        '
        'Button_OK
        '
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(230, 342)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 11
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(311, 342)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 10
        Me.Button_Cancel.Text = "Abbrechen"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'ListBox_YSpalten
        '
        Me.ListBox_YSpalten.FormattingEnabled = True
        Me.ListBox_YSpalten.Location = New System.Drawing.Point(266, 193)
        Me.ListBox_YSpalten.Name = "ListBox_YSpalten"
        Me.ListBox_YSpalten.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_YSpalten.Size = New System.Drawing.Size(120, 134)
        Me.ListBox_YSpalten.TabIndex = 18
        '
        'Label_YSpalten
        '
        Me.Label_YSpalten.AutoSize = True
        Me.Label_YSpalten.Location = New System.Drawing.Point(263, 168)
        Me.Label_YSpalten.Name = "Label_YSpalten"
        Me.Label_YSpalten.Size = New System.Drawing.Size(56, 13)
        Me.Label_YSpalten.TabIndex = 17
        Me.Label_YSpalten.Text = "Y-Spalten:"
        '
        'Label_XSpalte
        '
        Me.Label_XSpalte.AutoSize = True
        Me.Label_XSpalte.Location = New System.Drawing.Point(263, 115)
        Me.Label_XSpalte.Name = "Label_XSpalte"
        Me.Label_XSpalte.Size = New System.Drawing.Size(50, 13)
        Me.Label_XSpalte.TabIndex = 17
        Me.Label_XSpalte.Text = "X-Spalte:"
        '
        'TextBox_XSpalte
        '
        Me.TextBox_XSpalte.Location = New System.Drawing.Point(266, 141)
        Me.TextBox_XSpalte.Name = "TextBox_XSpalte"
        Me.TextBox_XSpalte.ReadOnly = True
        Me.TextBox_XSpalte.Size = New System.Drawing.Size(120, 20)
        Me.TextBox_XSpalte.TabIndex = 19
        '
        'RadioButton_Spaltenbreite
        '
        Me.RadioButton_Spaltenbreite.AutoSize = True
        Me.RadioButton_Spaltenbreite.Location = New System.Drawing.Point(15, 53)
        Me.RadioButton_Spaltenbreite.Name = "RadioButton_Spaltenbreite"
        Me.RadioButton_Spaltenbreite.Size = New System.Drawing.Size(78, 17)
        Me.RadioButton_Spaltenbreite.TabIndex = 20
        Me.RadioButton_Spaltenbreite.TabStop = True
        Me.RadioButton_Spaltenbreite.Text = "feste Breite"
        Me.RadioButton_Spaltenbreite.UseVisualStyleBackColor = True
        '
        'RadioButton_Zeichengetrennt
        '
        Me.RadioButton_Zeichengetrennt.AutoSize = True
        Me.RadioButton_Zeichengetrennt.Location = New System.Drawing.Point(15, 19)
        Me.RadioButton_Zeichengetrennt.Name = "RadioButton_Zeichengetrennt"
        Me.RadioButton_Zeichengetrennt.Size = New System.Drawing.Size(101, 17)
        Me.RadioButton_Zeichengetrennt.TabIndex = 21
        Me.RadioButton_Zeichengetrennt.TabStop = True
        Me.RadioButton_Zeichengetrennt.Text = "zeichengetrennt"
        Me.RadioButton_Zeichengetrennt.UseVisualStyleBackColor = True
        '
        'GroupBox_Spaltenmodus
        '
        Me.GroupBox_Spaltenmodus.Controls.Add(Me.TextBox_Spaltenbreite)
        Me.GroupBox_Spaltenmodus.Controls.Add(Me.RadioButton_Zeichengetrennt)
        Me.GroupBox_Spaltenmodus.Controls.Add(Me.ComboBox_Trennzeichen)
        Me.GroupBox_Spaltenmodus.Controls.Add(Me.RadioButton_Spaltenbreite)
        Me.GroupBox_Spaltenmodus.Location = New System.Drawing.Point(12, 13)
        Me.GroupBox_Spaltenmodus.Name = "GroupBox_Spaltenmodus"
        Me.GroupBox_Spaltenmodus.Size = New System.Drawing.Size(374, 89)
        Me.GroupBox_Spaltenmodus.TabIndex = 22
        Me.GroupBox_Spaltenmodus.TabStop = False
        Me.GroupBox_Spaltenmodus.Text = "Spalten"
        '
        'TextBox_Spaltenbreite
        '
        Me.TextBox_Spaltenbreite.Location = New System.Drawing.Point(124, 52)
        Me.TextBox_Spaltenbreite.Name = "TextBox_Spaltenbreite"
        Me.TextBox_Spaltenbreite.Size = New System.Drawing.Size(97, 20)
        Me.TextBox_Spaltenbreite.TabIndex = 22
        '
        'WEL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 375)
        Me.Controls.Add(Me.GroupBox_Spaltenmodus)
        Me.Controls.Add(Me.TextBox_XSpalte)
        Me.Controls.Add(Me.ListBox_YSpalten)
        Me.Controls.Add(Me.Label_XSpalte)
        Me.Controls.Add(Me.Label_YSpalten)
        Me.Controls.Add(Me.Label_Datei)
        Me.Controls.Add(Me.RichTextBox_Vorschau)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WEL"
        Me.Text = "WEL-Datei: Spalten auswählen"
        Me.GroupBox_Spaltenmodus.ResumeLayout(False)
        Me.GroupBox_Spaltenmodus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label_Datei As System.Windows.Forms.Label
    Friend WithEvents RichTextBox_Vorschau As System.Windows.Forms.RichTextBox
    Friend WithEvents ComboBox_Trennzeichen As System.Windows.Forms.ComboBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents ListBox_YSpalten As System.Windows.Forms.ListBox
    Friend WithEvents Label_YSpalten As System.Windows.Forms.Label
    Friend WithEvents Label_XSpalte As System.Windows.Forms.Label
    Friend WithEvents TextBox_XSpalte As System.Windows.Forms.TextBox
    Friend WithEvents RadioButton_Spaltenbreite As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Zeichengetrennt As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox_Spaltenmodus As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox_Spaltenbreite As System.Windows.Forms.TextBox
End Class
