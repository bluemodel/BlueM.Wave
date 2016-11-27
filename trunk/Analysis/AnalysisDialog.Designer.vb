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
        Dim Label_Series As System.Windows.Forms.Label
        Dim Label_Analysis As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnalysisDialog))
        Me.Button_Cancel = New System.Windows.Forms.Button
        Me.Button_OK = New System.Windows.Forms.Button
        Me.ListBox_Series = New System.Windows.Forms.ListBox
        Me.ComboBox_Analysis = New System.Windows.Forms.ComboBox
        Label_Series = New System.Windows.Forms.Label
        Label_Analysis = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label_Series
        '
        Label_Series.AutoSize = True
        Label_Series.Location = New System.Drawing.Point(12, 46)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New System.Drawing.Size(39, 13)
        Label_Series.TabIndex = 8
        Label_Series.Text = "Series:"
        '
        'Label_Analysis
        '
        Label_Analysis.AutoSize = True
        Label_Analysis.Location = New System.Drawing.Point(12, 15)
        Label_Analysis.Name = "Label_Analysis"
        Label_Analysis.Size = New System.Drawing.Size(48, 13)
        Label_Analysis.TabIndex = 8
        Label_Analysis.Text = "Analysis:"
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(207, 237)
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
        Me.Button_OK.Location = New System.Drawing.Point(126, 237)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 7
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'ListBox_Series
        '
        Me.ListBox_Series.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_Series.FormattingEnabled = True
        Me.ListBox_Series.Location = New System.Drawing.Point(12, 62)
        Me.ListBox_Series.Name = "ListBox_Series"
        Me.ListBox_Series.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_Series.Size = New System.Drawing.Size(272, 160)
        Me.ListBox_Series.TabIndex = 6
        '
        'ComboBox_Analysis
        '
        Me.ComboBox_Analysis.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Analysis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Analysis.FormattingEnabled = True
        Me.ComboBox_Analysis.Location = New System.Drawing.Point(69, 12)
        Me.ComboBox_Analysis.Name = "ComboBox_Analysis"
        Me.ComboBox_Analysis.Size = New System.Drawing.Size(213, 21)
        Me.ComboBox_Analysis.TabIndex = 10
        '
        'AnalysisDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 272)
        Me.Controls.Add(Label_Analysis)
        Me.Controls.Add(Me.ComboBox_Analysis)
        Me.Controls.Add(Label_Series)
        Me.Controls.Add(Me.ListBox_Series)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "AnalysisDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Analysis"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents ListBox_Series As System.Windows.Forms.ListBox
    Private WithEvents ComboBox_Analysis As System.Windows.Forms.ComboBox
End Class
