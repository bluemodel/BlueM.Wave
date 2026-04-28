<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Autocorrelation_Dialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Autocorrelation_Dialog))
        Cancel_Button = New Button()
        OK_Button = New Button()
        spnAnzahlLag = New NumericUpDown()
        spnGroesseLag = New NumericUpDown()
        Label2 = New Label()
        Label1 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label7 = New Label()
        GroupBox1 = New GroupBox()
        CType(spnAnzahlLag, ComponentModel.ISupportInitialize).BeginInit()
        CType(spnGroesseLag, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(270, 206)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(184, 206)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' spnAnzahlLag
        ' 
        spnAnzahlLag.Location = New Point(141, 54)
        spnAnzahlLag.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        spnAnzahlLag.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        spnAnzahlLag.Name = "spnAnzahlLag"
        spnAnzahlLag.Size = New Size(79, 23)
        spnAnzahlLag.TabIndex = 9
        spnAnzahlLag.Value = New Decimal(New Integer() {300, 0, 0, 0})
        ' 
        ' spnGroesseLag
        ' 
        spnGroesseLag.Location = New Point(141, 25)
        spnGroesseLag.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        spnGroesseLag.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        spnGroesseLag.Name = "spnGroesseLag"
        spnGroesseLag.Size = New Size(79, 23)
        spnGroesseLag.TabIndex = 8
        spnGroesseLag.Value = New Decimal(New Integer() {5, 0, 0, 0})
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(7, 57)
        Label2.Name = "Label2"
        Label2.Size = New Size(106, 15)
        Label2.TabIndex = 6
        Label2.Text = "Number of offsets:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(7, 28)
        Label1.Name = "Label1"
        Label1.Size = New Size(125, 15)
        Label1.TabIndex = 5
        Label1.Text = "Number of time steps:"
        ' 
        ' Label4
        ' 
        Label4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label4.AutoEllipsis = True
        Label4.ForeColor = SystemColors.ControlDarkDark
        Label4.Location = New Point(14, 107)
        Label4.Name = "Label4"
        Label4.Size = New Size(334, 87)
        Label4.TabIndex = 10
        Label4.Text = "More offsets take longer to compute." & vbCrLf & vbCrLf & "NOTE: The time series should not contain a trend and be equidistant."
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = SystemColors.ControlText
        Label5.Location = New Point(227, 28)
        Label5.Name = "Label5"
        Label5.Size = New Size(44, 15)
        Label5.TabIndex = 13
        Label5.Text = "(1 - 10)"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(227, 57)
        Label7.Name = "Label7"
        Label7.Size = New Size(56, 15)
        Label7.TabIndex = 14
        Label7.Text = "(1 - 2000)"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(spnGroesseLag)
        GroupBox1.Controls.Add(spnAnzahlLag)
        GroupBox1.Location = New Point(14, 14)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(4, 3, 4, 3)
        GroupBox1.Size = New Size(334, 90)
        GroupBox1.TabIndex = 15
        GroupBox1.TabStop = False
        GroupBox1.Text = "Offsets (lag)"
        ' 
        ' Autocorrelation_Dialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(362, 245)
        Controls.Add(Cancel_Button)
        Controls.Add(OK_Button)
        Controls.Add(GroupBox1)
        Controls.Add(Label4)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Autocorrelation_Dialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Autocorrelation"
        CType(spnAnzahlLag, ComponentModel.ISupportInitialize).EndInit()
        CType(spnGroesseLag, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents spnAnzahlLag As System.Windows.Forms.NumericUpDown
    Friend WithEvents spnGroesseLag As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBox1 As GroupBox
End Class
