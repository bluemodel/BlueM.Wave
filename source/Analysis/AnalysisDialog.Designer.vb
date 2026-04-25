<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnalysisDialog
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
        Dim Label_Series As Label
        Dim Label_Analysis As Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnalysisDialog))
        Button_Cancel = New Button()
        Button_OK = New Button()
        ListBox_Series = New ListBox()
        ComboBox_Analysis = New ComboBox()
        Label_AnalaysisDescription = New Label()
        LinkLabel_Helplink = New LinkLabel()
        Label_Series = New Label()
        Label_Analysis = New Label()
        SuspendLayout()
        ' 
        ' Label_Series
        ' 
        Label_Series.AutoSize = True
        Label_Series.Location = New Point(14, 138)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New Size(40, 15)
        Label_Series.TabIndex = 8
        Label_Series.Text = "Series:"
        ' 
        ' Label_Analysis
        ' 
        Label_Analysis.AutoSize = True
        Label_Analysis.Location = New Point(14, 17)
        Label_Analysis.Name = "Label_Analysis"
        Label_Analysis.Size = New Size(53, 15)
        Label_Analysis.TabIndex = 8
        Label_Analysis.Text = "Analysis:"
        ' 
        ' Button_Cancel
        ' 
        Button_Cancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_Cancel.DialogResult = DialogResult.Cancel
        Button_Cancel.Location = New Point(251, 357)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New Size(80, 27)
        Button_Cancel.TabIndex = 9
        Button_Cancel.Text = "Cancel"
        Button_Cancel.UseVisualStyleBackColor = True
        ' 
        ' Button_OK
        ' 
        Button_OK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button_OK.DialogResult = DialogResult.OK
        Button_OK.Location = New Point(165, 357)
        Button_OK.Name = "Button_OK"
        Button_OK.Size = New Size(80, 27)
        Button_OK.TabIndex = 7
        Button_OK.Text = "OK"
        Button_OK.UseVisualStyleBackColor = True
        ' 
        ' ListBox_Series
        ' 
        ListBox_Series.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox_Series.FormattingEnabled = True
        ListBox_Series.Location = New Point(14, 162)
        ListBox_Series.Name = "ListBox_Series"
        ListBox_Series.SelectionMode = SelectionMode.MultiExtended
        ListBox_Series.Size = New Size(317, 184)
        ListBox_Series.TabIndex = 6
        ' 
        ' ComboBox_Analysis
        ' 
        ComboBox_Analysis.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox_Analysis.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_Analysis.FormattingEnabled = True
        ComboBox_Analysis.Location = New Point(80, 14)
        ComboBox_Analysis.Name = "ComboBox_Analysis"
        ComboBox_Analysis.Size = New Size(248, 23)
        ComboBox_Analysis.TabIndex = 10
        ' 
        ' Label_AnalaysisDescription
        ' 
        Label_AnalaysisDescription.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label_AnalaysisDescription.Location = New Point(14, 51)
        Label_AnalaysisDescription.Name = "Label_AnalaysisDescription"
        Label_AnalaysisDescription.Size = New Size(317, 59)
        Label_AnalaysisDescription.TabIndex = 11
        Label_AnalaysisDescription.Text = "Select an analysis method"
        ' 
        ' LinkLabel_Helplink
        ' 
        LinkLabel_Helplink.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LinkLabel_Helplink.Location = New Point(14, 113)
        LinkLabel_Helplink.Name = "LinkLabel_Helplink"
        LinkLabel_Helplink.Size = New Size(317, 18)
        LinkLabel_Helplink.TabIndex = 12
        LinkLabel_Helplink.TabStop = True
        LinkLabel_Helplink.Text = "Open wiki page"
        ' 
        ' AnalysisDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(343, 396)
        Controls.Add(LinkLabel_Helplink)
        Controls.Add(Label_AnalaysisDescription)
        Controls.Add(Label_Analysis)
        Controls.Add(ComboBox_Analysis)
        Controls.Add(Label_Series)
        Controls.Add(ListBox_Series)
        Controls.Add(Button_OK)
        Controls.Add(Button_Cancel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(347, 340)
        Name = "AnalysisDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Analysis"
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Private WithEvents ListBox_Series As System.Windows.Forms.ListBox
    Private WithEvents ComboBox_Analysis As System.Windows.Forms.ComboBox
    Friend WithEvents Label_AnalaysisDescription As Label
    Friend WithEvents LinkLabel_Helplink As LinkLabel
End Class
