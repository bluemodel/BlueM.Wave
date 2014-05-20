<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnalysisDialog
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
        Dim Label_Zeitreihen As System.Windows.Forms.Label
        Dim Label_Analyse As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnalysisDialog))
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.Button_OK = New System.Windows.Forms.Button
        Me.ListBox_Zeitreihen = New System.Windows.Forms.ListBox
        Me.ComboBox_Funktion = New System.Windows.Forms.ComboBox
        Label_Zeitreihen = New System.Windows.Forms.Label
        Label_Analyse = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label_Zeitreihen
        '
        Label_Zeitreihen.AutoSize = True
        Label_Zeitreihen.Location = New System.Drawing.Point(12, 46)
        Label_Zeitreihen.Name = "Label_Zeitreihen"
        Label_Zeitreihen.Size = New System.Drawing.Size(63, 13)
        Label_Zeitreihen.TabIndex = 8
        Label_Zeitreihen.Text = "Zeitreihe(n):"
        '
        'Label_Analyse
        '
        Label_Analyse.AutoSize = True
        Label_Analyse.Location = New System.Drawing.Point(12, 15)
        Label_Analyse.Name = "Label_Analyse"
        Label_Analyse.Size = New System.Drawing.Size(51, 13)
        Label_Analyse.TabIndex = 8
        Label_Analyse.Text = "Funktion:"
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(126, 237)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 9
        Me.Button_Cancel.Text = "Abbrechen"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(207, 237)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 7
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'ListBox_Zeitreihen
        '
        Me.ListBox_Zeitreihen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Zeitreihen.FormattingEnabled = True
        Me.ListBox_Zeitreihen.Location = New System.Drawing.Point(12, 62)
        Me.ListBox_Zeitreihen.Name = "ListBox_Zeitreihen"
        Me.ListBox_Zeitreihen.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_Zeitreihen.Size = New System.Drawing.Size(272, 160)
        Me.ListBox_Zeitreihen.TabIndex = 6
        '
        'ComboBox_Funktion
        '
        Me.ComboBox_Funktion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Funktion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Funktion.FormattingEnabled = True
        Me.ComboBox_Funktion.Location = New System.Drawing.Point(69, 12)
        Me.ComboBox_Funktion.Name = "ComboBox_Funktion"
        Me.ComboBox_Funktion.Size = New System.Drawing.Size(213, 21)
        Me.ComboBox_Funktion.TabIndex = 10
        '
        'AnalysisDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 272)
        Me.Controls.Add(Me.ComboBox_Funktion)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Label_Analyse)
        Me.Controls.Add(Label_Zeitreihen)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.ListBox_Zeitreihen)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "AnalysisDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Analyse"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents ListBox_Zeitreihen As System.Windows.Forms.ListBox
    Private WithEvents ComboBox_Funktion As System.Windows.Forms.ComboBox
End Class
