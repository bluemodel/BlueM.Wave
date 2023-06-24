<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportDiag
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
        Dim Label_Format As System.Windows.Forms.Label
        Dim Label_Series As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExportDiag))
        Me.ListBox_Series = New System.Windows.Forms.ListBox()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.ComboBox_Format = New System.Windows.Forms.ComboBox()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_SelectAll = New System.Windows.Forms.Button()
        Label_Format = New System.Windows.Forms.Label()
        Label_Series = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label_Format
        '
        Label_Format.AutoSize = True
        Label_Format.Location = New System.Drawing.Point(12, 12)
        Label_Format.Name = "Label_Format"
        Label_Format.Size = New System.Drawing.Size(42, 13)
        Label_Format.TabIndex = 3
        Label_Format.Text = "Format:"
        '
        'Label_Series
        '
        Label_Series.AutoSize = True
        Label_Series.Location = New System.Drawing.Point(12, 46)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New System.Drawing.Size(39, 13)
        Label_Series.TabIndex = 4
        Label_Series.Text = "Series:"
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
        Me.ListBox_Series.Size = New System.Drawing.Size(170, 108)
        Me.ListBox_Series.TabIndex = 0
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(26, 186)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 1
        Me.Button_OK.Text = "Ok"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'ComboBox_Format
        '
        Me.ComboBox_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Format.FormattingEnabled = True
        Me.ComboBox_Format.Location = New System.Drawing.Point(60, 9)
        Me.ComboBox_Format.Name = "ComboBox_Format"
        Me.ComboBox_Format.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_Format.TabIndex = 2
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(107, 186)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 5
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_SelectAll
        '
        Me.Button_SelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectAll.Location = New System.Drawing.Point(121, 36)
        Me.Button_SelectAll.Name = "Button_SelectAll"
        Me.Button_SelectAll.Size = New System.Drawing.Size(61, 23)
        Me.Button_SelectAll.TabIndex = 5
        Me.Button_SelectAll.Text = "Select All"
        Me.Button_SelectAll.UseVisualStyleBackColor = True
        '
        'ExportDiag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(194, 221)
        Me.Controls.Add(Label_Format)
        Me.Controls.Add(Me.ComboBox_Format)
        Me.Controls.Add(Label_Series)
        Me.Controls.Add(Me.Button_SelectAll)
        Me.Controls.Add(Me.ListBox_Series)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(210, 260)
        Me.Name = "ExportDiag"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export time series"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox_Series As System.Windows.Forms.ListBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Format As System.Windows.Forms.ComboBox
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents Button_SelectAll As System.Windows.Forms.Button
End Class
