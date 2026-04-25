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
        Dim Label_Format As Label
        Dim Label_Series As Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExportDiag))
        ListBox_Series = New ListBox()
        Button_OK = New Button()
        ComboBox_Format = New ComboBox()
        Button_Cancel = New Button()
        Button_SelectAll = New Button()
        Label_Format = New Label()
        Label_Series = New Label()
        SuspendLayout()
        ' 
        ' Label_Format
        ' 
        Label_Format.AutoSize = True
        Label_Format.Location = New Point(14, 14)
        Label_Format.Name = "Label_Format"
        Label_Format.Size = New Size(48, 15)
        Label_Format.TabIndex = 3
        Label_Format.Text = "Format:"
        ' 
        ' Label_Series
        ' 
        Label_Series.AutoSize = True
        Label_Series.Location = New Point(14, 53)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New Size(40, 15)
        Label_Series.TabIndex = 4
        Label_Series.Text = "Series:"
        ' 
        ' ListBox_Series
        ' 
        ListBox_Series.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox_Series.FormattingEnabled = True
        ListBox_Series.Location = New Point(14, 72)
        ListBox_Series.Name = "ListBox_Series"
        ListBox_Series.SelectionMode = SelectionMode.MultiExtended
        ListBox_Series.Size = New Size(198, 124)
        ListBox_Series.TabIndex = 0
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(46, 215)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 1
        Button_OK.Text = "Ok"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' ComboBox_Format
        ' 
        ComboBox_Format.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_Format.FormattingEnabled = True
        ComboBox_Format.Location = New Point(70, 10)
        ComboBox_Format.Name = "ComboBox_Format"
        ComboBox_Format.Size = New Size(140, 23)
        ComboBox_Format.TabIndex = 2
        ' 
        ' Button_Cancel
        ' 
        Button_Cancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Cancel.DialogResult = DialogResult.Cancel
        Button_Cancel.Location = New Point(132, 215)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New Size(80, 27)
        Button_Cancel.TabIndex = 5
        Button_Cancel.Text = "Cancel"
        Button_Cancel.UseVisualStyleBackColor = True
        ' 
        ' Button_SelectAll
        ' 
        Button_SelectAll.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_SelectAll.Location = New Point(132, 39)
        Button_SelectAll.Name = "Button_SelectAll"
        Button_SelectAll.Size = New Size(80, 27)
        Button_SelectAll.TabIndex = 5
        Button_SelectAll.Text = "Select All"
        Button_SelectAll.UseVisualStyleBackColor = True
        ' 
        ' ExportDiag
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(226, 255)
        Controls.Add(Label_Format)
        Controls.Add(ComboBox_Format)
        Controls.Add(Label_Series)
        Controls.Add(Button_SelectAll)
        Controls.Add(ListBox_Series)
        Controls.Add(Button_OK)
        Controls.Add(Button_Cancel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(242, 294)
        Name = "ExportDiag"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Export time series"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ListBox_Series As System.Windows.Forms.ListBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Format As System.Windows.Forms.ComboBox
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents Button_SelectAll As System.Windows.Forms.Button
End Class
