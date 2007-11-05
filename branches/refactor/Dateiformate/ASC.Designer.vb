<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ASC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ASC))
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.Button_OK = New System.Windows.Forms.Button
        Me.Label_YSpalten = New System.Windows.Forms.Label
        Me.Label_XSpalte = New System.Windows.Forms.Label
        Me.ListBox_YSpalten = New System.Windows.Forms.ListBox
        Me.TextBox_XSpalte = New System.Windows.Forms.TextBox
        Me.RichTextBox_Vorschau = New System.Windows.Forms.RichTextBox
        Me.Label_Datei = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(311, 305)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 10
        Me.Button_Cancel.Text = "Abbrechen"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(230, 305)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 11
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Label_YSpalten
        '
        Me.Label_YSpalten.AutoSize = True
        Me.Label_YSpalten.Location = New System.Drawing.Point(263, 62)
        Me.Label_YSpalten.Name = "Label_YSpalten"
        Me.Label_YSpalten.Size = New System.Drawing.Size(56, 13)
        Me.Label_YSpalten.TabIndex = 17
        Me.Label_YSpalten.Text = "Y-Spalten:"
        '
        'Label_XSpalte
        '
        Me.Label_XSpalte.AutoSize = True
        Me.Label_XSpalte.Location = New System.Drawing.Point(263, 9)
        Me.Label_XSpalte.Name = "Label_XSpalte"
        Me.Label_XSpalte.Size = New System.Drawing.Size(50, 13)
        Me.Label_XSpalte.TabIndex = 17
        Me.Label_XSpalte.Text = "X-Spalte:"
        '
        'ListBox_YSpalten
        '
        Me.ListBox_YSpalten.FormattingEnabled = True
        Me.ListBox_YSpalten.Location = New System.Drawing.Point(266, 87)
        Me.ListBox_YSpalten.Name = "ListBox_YSpalten"
        Me.ListBox_YSpalten.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_YSpalten.Size = New System.Drawing.Size(120, 212)
        Me.ListBox_YSpalten.TabIndex = 18
        '
        'TextBox_XSpalte
        '
        Me.TextBox_XSpalte.Location = New System.Drawing.Point(266, 35)
        Me.TextBox_XSpalte.Name = "TextBox_XSpalte"
        Me.TextBox_XSpalte.ReadOnly = True
        Me.TextBox_XSpalte.Size = New System.Drawing.Size(120, 20)
        Me.TextBox_XSpalte.TabIndex = 19
        '
        'RichTextBox_Vorschau
        '
        Me.RichTextBox_Vorschau.BackColor = System.Drawing.Color.White
        Me.RichTextBox_Vorschau.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox_Vorschau.Location = New System.Drawing.Point(12, 35)
        Me.RichTextBox_Vorschau.Name = "RichTextBox_Vorschau"
        Me.RichTextBox_Vorschau.ReadOnly = True
        Me.RichTextBox_Vorschau.Size = New System.Drawing.Size(230, 264)
        Me.RichTextBox_Vorschau.TabIndex = 16
        Me.RichTextBox_Vorschau.Text = ""
        Me.RichTextBox_Vorschau.WordWrap = False
        '
        'Label_Datei
        '
        Me.Label_Datei.AutoSize = True
        Me.Label_Datei.Location = New System.Drawing.Point(12, 9)
        Me.Label_Datei.Name = "Label_Datei"
        Me.Label_Datei.Size = New System.Drawing.Size(35, 13)
        Me.Label_Datei.TabIndex = 17
        Me.Label_Datei.Text = "Datei:"
        '
        'ASC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(396, 336)
        Me.Controls.Add(Me.TextBox_XSpalte)
        Me.Controls.Add(Me.ListBox_YSpalten)
        Me.Controls.Add(Me.Label_XSpalte)
        Me.Controls.Add(Me.Label_YSpalten)
        Me.Controls.Add(Me.Label_Datei)
        Me.Controls.Add(Me.RichTextBox_Vorschau)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ASC"
        Me.Text = "ASC-Datei: Spalten auswählen"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents Label_YSpalten As System.Windows.Forms.Label
    Friend WithEvents Label_XSpalte As System.Windows.Forms.Label
    Friend WithEvents ListBox_YSpalten As System.Windows.Forms.ListBox
    Friend WithEvents TextBox_XSpalte As System.Windows.Forms.TextBox
    Friend WithEvents RichTextBox_Vorschau As System.Windows.Forms.RichTextBox
    Friend WithEvents Label_Datei As System.Windows.Forms.Label
End Class
