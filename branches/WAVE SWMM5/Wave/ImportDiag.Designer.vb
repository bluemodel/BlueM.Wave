<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportDiag
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportDiag))
        Me.Label_Datei = New System.Windows.Forms.Label
        Me.TextBox_Vorschau = New System.Windows.Forms.RichTextBox
        Me.ComboBox_Trennzeichen = New System.Windows.Forms.ComboBox
        Me.Button_OK = New System.Windows.Forms.Button
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.ListBox_YSpalten = New System.Windows.Forms.ListBox
        Me.Label_Suche = New System.Windows.Forms.Label
        Me.Label_XSpalte = New System.Windows.Forms.Label
        Me.TextBox_XSpalte = New System.Windows.Forms.TextBox
        Me.RadioButton_Spaltenbreite = New System.Windows.Forms.RadioButton
        Me.RadioButton_Zeichengetrennt = New System.Windows.Forms.RadioButton
        Me.GroupBox_Spaltenmodus = New System.Windows.Forms.GroupBox
        Me.TextBox_Spaltenbreite = New System.Windows.Forms.TextBox
        Me.TextBox_ZeileÜberschriften = New System.Windows.Forms.NumericUpDown
        Me.Label_ZeileÜberschriften = New System.Windows.Forms.Label
        Me.ComboBox_Dezimaltrennzeichen = New System.Windows.Forms.ComboBox
        Me.GroupBox_Einstellungen = New System.Windows.Forms.GroupBox
        Me.CheckBox_Einheiten = New System.Windows.Forms.CheckBox
        Me.TextBox_ZeileEinheiten = New System.Windows.Forms.NumericUpDown
        Me.TextBox_ZeileDaten = New System.Windows.Forms.NumericUpDown
        Me.Label_ZeileDaten = New System.Windows.Forms.Label
        Me.GroupBox_Vorschau = New System.Windows.Forms.GroupBox
        Me.Label_Spaltenauswahl = New System.Windows.Forms.Label
        Me.NumericUpDown_DatumsSpalte = New System.Windows.Forms.NumericUpDown
        Me.TextBox_Suche = New System.Windows.Forms.TextBox
        Me.GroupBox_Dezimaltrennzeichen = New System.Windows.Forms.GroupBox
        Me.Label_Dezimaltrennzeichen = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.StatusImage = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox_Spaltenmodus.SuspendLayout()
        CType(Me.TextBox_ZeileÜberschriften, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Einstellungen.SuspendLayout()
        CType(Me.TextBox_ZeileEinheiten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox_ZeileDaten, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Vorschau.SuspendLayout()
        CType(Me.NumericUpDown_DatumsSpalte, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Dezimaltrennzeichen.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_Datei
        '
        Me.Label_Datei.AutoSize = True
        Me.Label_Datei.Location = New System.Drawing.Point(12, 19)
        Me.Label_Datei.Name = "Label_Datei"
        Me.Label_Datei.Size = New System.Drawing.Size(35, 13)
        Me.Label_Datei.TabIndex = 17
        Me.Label_Datei.Text = "Datei:"
        '
        'TextBox_Vorschau
        '
        Me.TextBox_Vorschau.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Vorschau.BackColor = System.Drawing.Color.White
        Me.TextBox_Vorschau.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Vorschau.Location = New System.Drawing.Point(12, 42)
        Me.TextBox_Vorschau.Name = "TextBox_Vorschau"
        Me.TextBox_Vorschau.ReadOnly = True
        Me.TextBox_Vorschau.Size = New System.Drawing.Size(226, 252)
        Me.TextBox_Vorschau.TabIndex = 16
        Me.TextBox_Vorschau.Text = ""
        Me.TextBox_Vorschau.WordWrap = False
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
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(229, 498)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 11
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(310, 498)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 10
        Me.Button_Cancel.Text = "Abbrechen"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'ListBox_YSpalten
        '
        Me.ListBox_YSpalten.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_YSpalten.FormattingEnabled = True
        Me.ListBox_YSpalten.Location = New System.Drawing.Point(247, 91)
        Me.ListBox_YSpalten.Name = "ListBox_YSpalten"
        Me.ListBox_YSpalten.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_YSpalten.Size = New System.Drawing.Size(121, 173)
        Me.ListBox_YSpalten.TabIndex = 18
        '
        'Label_Suche
        '
        Me.Label_Suche.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Suche.AutoSize = True
        Me.Label_Suche.Location = New System.Drawing.Point(244, 281)
        Me.Label_Suche.Name = "Label_Suche"
        Me.Label_Suche.Size = New System.Drawing.Size(41, 13)
        Me.Label_Suche.TabIndex = 17
        Me.Label_Suche.Text = "Suche:"
        '
        'Label_XSpalte
        '
        Me.Label_XSpalte.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_XSpalte.AutoSize = True
        Me.Label_XSpalte.Location = New System.Drawing.Point(244, 19)
        Me.Label_XSpalte.Name = "Label_XSpalte"
        Me.Label_XSpalte.Size = New System.Drawing.Size(106, 13)
        Me.Label_XSpalte.TabIndex = 17
        Me.Label_XSpalte.Text = "X-Spalte (Zeitachse):"
        '
        'TextBox_XSpalte
        '
        Me.TextBox_XSpalte.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_XSpalte.Location = New System.Drawing.Point(247, 42)
        Me.TextBox_XSpalte.Name = "TextBox_XSpalte"
        Me.TextBox_XSpalte.ReadOnly = True
        Me.TextBox_XSpalte.Size = New System.Drawing.Size(80, 20)
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
        Me.GroupBox_Spaltenmodus.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_Spaltenmodus.Name = "GroupBox_Spaltenmodus"
        Me.GroupBox_Spaltenmodus.Size = New System.Drawing.Size(239, 84)
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
        'TextBox_ZeileÜberschriften
        '
        Me.TextBox_ZeileÜberschriften.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_ZeileÜberschriften.Location = New System.Drawing.Point(15, 46)
        Me.TextBox_ZeileÜberschriften.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TextBox_ZeileÜberschriften.Name = "TextBox_ZeileÜberschriften"
        Me.TextBox_ZeileÜberschriften.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_ZeileÜberschriften.TabIndex = 24
        Me.TextBox_ZeileÜberschriften.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label_ZeileÜberschriften
        '
        Me.Label_ZeileÜberschriften.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_ZeileÜberschriften.AutoSize = True
        Me.Label_ZeileÜberschriften.Location = New System.Drawing.Point(12, 25)
        Me.Label_ZeileÜberschriften.Name = "Label_ZeileÜberschriften"
        Me.Label_ZeileÜberschriften.Size = New System.Drawing.Size(73, 13)
        Me.Label_ZeileÜberschriften.TabIndex = 23
        Me.Label_ZeileÜberschriften.Text = "Überschriften:"
        '
        'ComboBox_Dezimaltrennzeichen
        '
        Me.ComboBox_Dezimaltrennzeichen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Dezimaltrennzeichen.DropDownWidth = 50
        Me.ComboBox_Dezimaltrennzeichen.FormattingEnabled = True
        Me.ComboBox_Dezimaltrennzeichen.Location = New System.Drawing.Point(9, 51)
        Me.ComboBox_Dezimaltrennzeichen.Name = "ComboBox_Dezimaltrennzeichen"
        Me.ComboBox_Dezimaltrennzeichen.Size = New System.Drawing.Size(50, 21)
        Me.ComboBox_Dezimaltrennzeichen.TabIndex = 26
        '
        'GroupBox_Einstellungen
        '
        Me.GroupBox_Einstellungen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Einstellungen.Controls.Add(Me.CheckBox_Einheiten)
        Me.GroupBox_Einstellungen.Controls.Add(Me.TextBox_ZeileEinheiten)
        Me.GroupBox_Einstellungen.Controls.Add(Me.TextBox_ZeileDaten)
        Me.GroupBox_Einstellungen.Controls.Add(Me.TextBox_ZeileÜberschriften)
        Me.GroupBox_Einstellungen.Controls.Add(Me.Label_ZeileDaten)
        Me.GroupBox_Einstellungen.Controls.Add(Me.Label_ZeileÜberschriften)
        Me.GroupBox_Einstellungen.Location = New System.Drawing.Point(12, 102)
        Me.GroupBox_Einstellungen.Name = "GroupBox_Einstellungen"
        Me.GroupBox_Einstellungen.Size = New System.Drawing.Size(373, 79)
        Me.GroupBox_Einstellungen.TabIndex = 27
        Me.GroupBox_Einstellungen.TabStop = False
        Me.GroupBox_Einstellungen.Text = "Zeilennummern"
        '
        'CheckBox_Einheiten
        '
        Me.CheckBox_Einheiten.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_Einheiten.AutoSize = True
        Me.CheckBox_Einheiten.Location = New System.Drawing.Point(150, 24)
        Me.CheckBox_Einheiten.Name = "CheckBox_Einheiten"
        Me.CheckBox_Einheiten.Size = New System.Drawing.Size(73, 17)
        Me.CheckBox_Einheiten.TabIndex = 25
        Me.CheckBox_Einheiten.Text = "Einheiten:"
        Me.CheckBox_Einheiten.UseVisualStyleBackColor = True
        '
        'TextBox_ZeileEinheiten
        '
        Me.TextBox_ZeileEinheiten.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.TextBox_ZeileEinheiten.Location = New System.Drawing.Point(150, 46)
        Me.TextBox_ZeileEinheiten.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TextBox_ZeileEinheiten.Name = "TextBox_ZeileEinheiten"
        Me.TextBox_ZeileEinheiten.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_ZeileEinheiten.TabIndex = 24
        Me.TextBox_ZeileEinheiten.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TextBox_ZeileDaten
        '
        Me.TextBox_ZeileDaten.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_ZeileDaten.Location = New System.Drawing.Point(283, 46)
        Me.TextBox_ZeileDaten.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TextBox_ZeileDaten.Name = "TextBox_ZeileDaten"
        Me.TextBox_ZeileDaten.Size = New System.Drawing.Size(50, 20)
        Me.TextBox_ZeileDaten.TabIndex = 24
        Me.TextBox_ZeileDaten.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label_ZeileDaten
        '
        Me.Label_ZeileDaten.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_ZeileDaten.AutoSize = True
        Me.Label_ZeileDaten.Location = New System.Drawing.Point(280, 25)
        Me.Label_ZeileDaten.Name = "Label_ZeileDaten"
        Me.Label_ZeileDaten.Size = New System.Drawing.Size(87, 13)
        Me.Label_ZeileDaten.TabIndex = 23
        Me.Label_ZeileDaten.Text = "Erste Datenzeile:"
        '
        'GroupBox_Vorschau
        '
        Me.GroupBox_Vorschau.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Vorschau.Controls.Add(Me.Label_Spaltenauswahl)
        Me.GroupBox_Vorschau.Controls.Add(Me.NumericUpDown_DatumsSpalte)
        Me.GroupBox_Vorschau.Controls.Add(Me.TextBox_Suche)
        Me.GroupBox_Vorschau.Controls.Add(Me.Label_Datei)
        Me.GroupBox_Vorschau.Controls.Add(Me.TextBox_Vorschau)
        Me.GroupBox_Vorschau.Controls.Add(Me.Label_Suche)
        Me.GroupBox_Vorschau.Controls.Add(Me.TextBox_XSpalte)
        Me.GroupBox_Vorschau.Controls.Add(Me.Label_XSpalte)
        Me.GroupBox_Vorschau.Controls.Add(Me.ListBox_YSpalten)
        Me.GroupBox_Vorschau.Location = New System.Drawing.Point(12, 187)
        Me.GroupBox_Vorschau.Name = "GroupBox_Vorschau"
        Me.GroupBox_Vorschau.Size = New System.Drawing.Size(373, 305)
        Me.GroupBox_Vorschau.TabIndex = 28
        Me.GroupBox_Vorschau.TabStop = False
        Me.GroupBox_Vorschau.Text = "Vorschau"
        '
        'Label_Spaltenauswahl
        '
        Me.Label_Spaltenauswahl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Spaltenauswahl.AutoSize = True
        Me.Label_Spaltenauswahl.Location = New System.Drawing.Point(244, 73)
        Me.Label_Spaltenauswahl.Name = "Label_Spaltenauswahl"
        Me.Label_Spaltenauswahl.Size = New System.Drawing.Size(85, 13)
        Me.Label_Spaltenauswahl.TabIndex = 26
        Me.Label_Spaltenauswahl.Text = "Spaltenauswahl:"
        '
        'NumericUpDown_DatumsSpalte
        '
        Me.NumericUpDown_DatumsSpalte.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_DatumsSpalte.Location = New System.Drawing.Point(333, 42)
        Me.NumericUpDown_DatumsSpalte.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_DatumsSpalte.Name = "NumericUpDown_DatumsSpalte"
        Me.NumericUpDown_DatumsSpalte.Size = New System.Drawing.Size(34, 20)
        Me.NumericUpDown_DatumsSpalte.TabIndex = 25
        Me.NumericUpDown_DatumsSpalte.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TextBox_Suche
        '
        Me.TextBox_Suche.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Suche.Location = New System.Drawing.Point(291, 278)
        Me.TextBox_Suche.Name = "TextBox_Suche"
        Me.TextBox_Suche.Size = New System.Drawing.Size(77, 20)
        Me.TextBox_Suche.TabIndex = 23
        '
        'GroupBox_Dezimaltrennzeichen
        '
        Me.GroupBox_Dezimaltrennzeichen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Dezimaltrennzeichen.Controls.Add(Me.ComboBox_Dezimaltrennzeichen)
        Me.GroupBox_Dezimaltrennzeichen.Controls.Add(Me.Label_Dezimaltrennzeichen)
        Me.GroupBox_Dezimaltrennzeichen.Location = New System.Drawing.Point(259, 12)
        Me.GroupBox_Dezimaltrennzeichen.Name = "GroupBox_Dezimaltrennzeichen"
        Me.GroupBox_Dezimaltrennzeichen.Size = New System.Drawing.Size(126, 84)
        Me.GroupBox_Dezimaltrennzeichen.TabIndex = 29
        Me.GroupBox_Dezimaltrennzeichen.TabStop = False
        Me.GroupBox_Dezimaltrennzeichen.Text = "Dezimaltrennzeichen"
        '
        'Label_Dezimaltrennzeichen
        '
        Me.Label_Dezimaltrennzeichen.AutoSize = True
        Me.Label_Dezimaltrennzeichen.Location = New System.Drawing.Point(6, 26)
        Me.Label_Dezimaltrennzeichen.Name = "Label_Dezimaltrennzeichen"
        Me.Label_Dezimaltrennzeichen.Size = New System.Drawing.Size(108, 13)
        Me.Label_Dezimaltrennzeichen.TabIndex = 23
        Me.Label_Dezimaltrennzeichen.Text = "Dezimaltrennzeichen:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusImage})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 524)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(392, 22)
        Me.StatusStrip1.TabIndex = 30
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusImage
        '
        Me.StatusImage.Image = Global.IHWB.Wave.My.Resources.Resources.tick
        Me.StatusImage.Name = "StatusImage"
        Me.StatusImage.Size = New System.Drawing.Size(37, 17)
        Me.StatusImage.Text = "OK"
        '
        'ImportDiag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancel
        Me.ClientSize = New System.Drawing.Size(392, 546)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox_Dezimaltrennzeichen)
        Me.Controls.Add(Me.GroupBox_Vorschau)
        Me.Controls.Add(Me.GroupBox_Einstellungen)
        Me.Controls.Add(Me.GroupBox_Spaltenmodus)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 500)
        Me.Name = "ImportDiag"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Datei importieren"
        Me.GroupBox_Spaltenmodus.ResumeLayout(False)
        Me.GroupBox_Spaltenmodus.PerformLayout()
        CType(Me.TextBox_ZeileÜberschriften, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Einstellungen.ResumeLayout(False)
        Me.GroupBox_Einstellungen.PerformLayout()
        CType(Me.TextBox_ZeileEinheiten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox_ZeileDaten, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Vorschau.ResumeLayout(False)
        Me.GroupBox_Vorschau.PerformLayout()
        CType(Me.NumericUpDown_DatumsSpalte, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Dezimaltrennzeichen.ResumeLayout(False)
        Me.GroupBox_Dezimaltrennzeichen.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents GroupBox_Einstellungen As System.Windows.Forms.GroupBox
    Private WithEvents GroupBox_Spaltenmodus As System.Windows.Forms.GroupBox
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents GroupBox_Vorschau As System.Windows.Forms.GroupBox
    Private WithEvents GroupBox_Dezimaltrennzeichen As System.Windows.Forms.GroupBox
    Private WithEvents Label_Datei As System.Windows.Forms.Label
    Private WithEvents TextBox_Vorschau As System.Windows.Forms.RichTextBox
    Private WithEvents ComboBox_Trennzeichen As System.Windows.Forms.ComboBox
    Private WithEvents ListBox_YSpalten As System.Windows.Forms.ListBox
    Private WithEvents Label_Suche As System.Windows.Forms.Label
    Private WithEvents Label_XSpalte As System.Windows.Forms.Label
    Private WithEvents TextBox_XSpalte As System.Windows.Forms.TextBox
    Private WithEvents RadioButton_Spaltenbreite As System.Windows.Forms.RadioButton
    Private WithEvents RadioButton_Zeichengetrennt As System.Windows.Forms.RadioButton
    Private WithEvents TextBox_Spaltenbreite As System.Windows.Forms.TextBox
    Private WithEvents TextBox_ZeileÜberschriften As System.Windows.Forms.NumericUpDown
    Private WithEvents Label_ZeileÜberschriften As System.Windows.Forms.Label
    Private WithEvents ComboBox_Dezimaltrennzeichen As System.Windows.Forms.ComboBox
    Private WithEvents TextBox_ZeileEinheiten As System.Windows.Forms.NumericUpDown
    Private WithEvents TextBox_ZeileDaten As System.Windows.Forms.NumericUpDown
    Private WithEvents Label_ZeileDaten As System.Windows.Forms.Label
    Private WithEvents CheckBox_Einheiten As System.Windows.Forms.CheckBox
    Private WithEvents Label_Dezimaltrennzeichen As System.Windows.Forms.Label
    Private WithEvents TextBox_Suche As System.Windows.Forms.TextBox
    Private WithEvents Label_Spaltenauswahl As System.Windows.Forms.Label
    Private WithEvents NumericUpDown_DatumsSpalte As System.Windows.Forms.NumericUpDown
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusImage As System.Windows.Forms.ToolStripStatusLabel
End Class
