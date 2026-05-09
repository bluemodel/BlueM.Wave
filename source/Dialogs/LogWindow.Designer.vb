<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogWindow
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Dim ToolStrip1 As ToolStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogWindow))
        ToolStripButton_Clear = New ToolStripButton()
        ToolStripButton_Save = New ToolStripButton()
        ToolStripButton_Copy = New ToolStripButton()
        TextBox_Log = New RichTextBox()
        SaveFileDialog1 = New SaveFileDialog()
        ToolStrip1 = New ToolStrip()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton_Clear, ToolStripButton_Save, ToolStripButton_Copy})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1125, 25)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton_Clear
        ' 
        ToolStripButton_Clear.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Clear.Image = CType(resources.GetObject("ToolStripButton_Clear.Image"), Image)
        ToolStripButton_Clear.ImageTransparentColor = Color.Magenta
        ToolStripButton_Clear.Name = "ToolStripButton_Clear"
        ToolStripButton_Clear.Size = New Size(23, 22)
        ToolStripButton_Clear.Text = "Clear"
        ToolStripButton_Clear.ToolTipText = "Clear log"
        ' 
        ' ToolStripButton_Save
        ' 
        ToolStripButton_Save.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Save.Image = CType(resources.GetObject("ToolStripButton_Save.Image"), Image)
        ToolStripButton_Save.ImageTransparentColor = Color.Magenta
        ToolStripButton_Save.Name = "ToolStripButton_Save"
        ToolStripButton_Save.Size = New Size(23, 22)
        ToolStripButton_Save.Text = "Save"
        ' 
        ' ToolStripButton_Copy
        ' 
        ToolStripButton_Copy.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Copy.Image = CType(resources.GetObject("ToolStripButton_Copy.Image"), Image)
        ToolStripButton_Copy.ImageTransparentColor = Color.Magenta
        ToolStripButton_Copy.Name = "ToolStripButton_Copy"
        ToolStripButton_Copy.Size = New Size(23, 22)
        ToolStripButton_Copy.Text = "Copy"
        ' 
        ' TextBox_Log
        ' 
        TextBox_Log.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_Log.Font = New Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TextBox_Log.Location = New Point(12, 35)
        TextBox_Log.Margin = New Padding(4, 3, 4, 3)
        TextBox_Log.Name = "TextBox_Log"
        TextBox_Log.ReadOnly = True
        TextBox_Log.Size = New Size(1098, 520)
        TextBox_Log.TabIndex = 0
        TextBox_Log.Text = ""
        TextBox_Log.WordWrap = False
        ' 
        ' SaveFileDialog1
        ' 
        SaveFileDialog1.DefaultExt = "txt"
        SaveFileDialog1.FileName = "log.txt"
        SaveFileDialog1.Filter = "Text-Dateien|*.txt"
        SaveFileDialog1.Title = "Log speichern"
        ' 
        ' LogWindow
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1125, 567)
        Controls.Add(ToolStrip1)
        Controls.Add(TextBox_Log)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimumSize = New Size(534, 432)
        Name = "LogWindow"
        StartPosition = FormStartPosition.CenterParent
        Text = "WaveLog"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Private WithEvents TextBox_Log As System.Windows.Forms.RichTextBox
    Private WithEvents ToolStripButton_Clear As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton_Save As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton_Copy As System.Windows.Forms.ToolStripButton
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog

End Class
