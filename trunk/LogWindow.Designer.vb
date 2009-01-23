<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogWindow
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
        Dim ToolStrip1 As System.Windows.Forms.ToolStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogWindow))
        Me.ToolStripButton_New = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Save = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Copy = New System.Windows.Forms.ToolStripButton
        Me.TextBox_Log = New System.Windows.Forms.RichTextBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        ToolStrip1 = New System.Windows.Forms.ToolStrip
        ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_New, Me.ToolStripButton_Save, Me.ToolStripButton_Copy})
        ToolStrip1.Location = New System.Drawing.Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New System.Drawing.Size(435, 25)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton_New
        '
        Me.ToolStripButton_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_New.Image = CType(resources.GetObject("ToolStripButton_New.Image"), System.Drawing.Image)
        Me.ToolStripButton_New.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_New.Name = "ToolStripButton_New"
        Me.ToolStripButton_New.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_New.Text = "Log löschen"
        Me.ToolStripButton_New.ToolTipText = "Log löschen"
        '
        'ToolStripButton_Save
        '
        Me.ToolStripButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Save.Image = CType(resources.GetObject("ToolStripButton_Save.Image"), System.Drawing.Image)
        Me.ToolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Save.Name = "ToolStripButton_Save"
        Me.ToolStripButton_Save.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Save.Text = "Speichern"
        '
        'ToolStripButton_Copy
        '
        Me.ToolStripButton_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Copy.Enabled = False
        Me.ToolStripButton_Copy.Image = CType(resources.GetObject("ToolStripButton_Copy.Image"), System.Drawing.Image)
        Me.ToolStripButton_Copy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Copy.Name = "ToolStripButton_Copy"
        Me.ToolStripButton_Copy.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Copy.Text = "Kopieren"
        '
        'TextBox_Log
        '
        Me.TextBox_Log.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Log.BackColor = System.Drawing.Color.White
        Me.TextBox_Log.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Log.Location = New System.Drawing.Point(10, 30)
        Me.TextBox_Log.Name = "TextBox_Log"
        Me.TextBox_Log.ReadOnly = True
        Me.TextBox_Log.Size = New System.Drawing.Size(413, 275)
        Me.TextBox_Log.TabIndex = 0
        Me.TextBox_Log.Text = ""
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "txt"
        Me.SaveFileDialog1.FileName = "log.txt"
        Me.SaveFileDialog1.Filter = "Text-Dateien|*.txt"
        Me.SaveFileDialog1.Title = "Log speichern"
        '
        'LogWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(ToolStrip1)
        Me.Controls.Add(Me.TextBox_Log)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "LogWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WaveLog"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents TextBox_Log As System.Windows.Forms.RichTextBox
    Private WithEvents ToolStripButton_New As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton_Save As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton_Copy As System.Windows.Forms.ToolStripButton
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog

End Class
