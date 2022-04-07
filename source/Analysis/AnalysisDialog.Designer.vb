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
        Dim Label_Analysis As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnalysisDialog))
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_Next = New System.Windows.Forms.Button()
        Me.Label_AnalaysisDescription = New System.Windows.Forms.Label()
        Me.LinkLabel_Helplink = New System.Windows.Forms.LinkLabel()
        Me.ListBox_AnalysisFunctions = New System.Windows.Forms.ListBox()
        Me.WizardPages1 = New BlueM.Wave.WizardPages()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_AnalysisFunction = New System.Windows.Forms.Label()
        Me.Button_Previous = New System.Windows.Forms.Button()
        Me.Button_Execute = New System.Windows.Forms.Button()
        Label_Analysis = New System.Windows.Forms.Label()
        Me.WizardPages1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_Analysis
        '
        Label_Analysis.AutoSize = True
        Label_Analysis.Location = New System.Drawing.Point(6, 3)
        Label_Analysis.Name = "Label_Analysis"
        Label_Analysis.Size = New System.Drawing.Size(136, 13)
        Label_Analysis.TabIndex = 8
        Label_Analysis.Text = "Select an analysis function:"
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(357, 308)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 9
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_Next
        '
        Me.Button_Next.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Next.Location = New System.Drawing.Point(519, 308)
        Me.Button_Next.Name = "Button_Next"
        Me.Button_Next.Size = New System.Drawing.Size(75, 23)
        Me.Button_Next.TabIndex = 7
        Me.Button_Next.Text = "Next >"
        Me.Button_Next.UseVisualStyleBackColor = True
        '
        'Label_AnalaysisDescription
        '
        Me.Label_AnalaysisDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_AnalaysisDescription.Location = New System.Drawing.Point(233, 19)
        Me.Label_AnalaysisDescription.Name = "Label_AnalaysisDescription"
        Me.Label_AnalaysisDescription.Size = New System.Drawing.Size(428, 216)
        Me.Label_AnalaysisDescription.TabIndex = 11
        Me.Label_AnalaysisDescription.Text = "Select an analysis function"
        '
        'LinkLabel_Helplink
        '
        Me.LinkLabel_Helplink.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel_Helplink.Location = New System.Drawing.Point(233, 251)
        Me.LinkLabel_Helplink.Name = "LinkLabel_Helplink"
        Me.LinkLabel_Helplink.Size = New System.Drawing.Size(428, 19)
        Me.LinkLabel_Helplink.TabIndex = 12
        Me.LinkLabel_Helplink.TabStop = True
        Me.LinkLabel_Helplink.Text = "Open wiki page"
        '
        'ListBox_AnalysisFunctions
        '
        Me.ListBox_AnalysisFunctions.FormattingEnabled = True
        Me.ListBox_AnalysisFunctions.Location = New System.Drawing.Point(6, 19)
        Me.ListBox_AnalysisFunctions.Name = "ListBox_AnalysisFunctions"
        Me.ListBox_AnalysisFunctions.Size = New System.Drawing.Size(221, 251)
        Me.ListBox_AnalysisFunctions.TabIndex = 14
        '
        'WizardPages1
        '
        Me.WizardPages1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WizardPages1.Controls.Add(Me.TabPage3)
        Me.WizardPages1.Controls.Add(Me.TabPage4)
        Me.WizardPages1.Location = New System.Drawing.Point(0, 0)
        Me.WizardPages1.Margin = New System.Windows.Forms.Padding(0)
        Me.WizardPages1.Name = "WizardPages1"
        Me.WizardPages1.SelectedIndex = 0
        Me.WizardPages1.Size = New System.Drawing.Size(675, 305)
        Me.WizardPages1.TabIndex = 16
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Label_Analysis)
        Me.TabPage3.Controls.Add(Me.ListBox_AnalysisFunctions)
        Me.TabPage3.Controls.Add(Me.Label_AnalaysisDescription)
        Me.TabPage3.Controls.Add(Me.LinkLabel_Helplink)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(667, 279)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Step1"
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage4.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage4.Controls.Add(Me.Label_AnalysisFunction)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(667, 279)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Step2"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(6, 23)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(656, 26)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'Label_AnalysisFunction
        '
        Me.Label_AnalysisFunction.AutoSize = True
        Me.Label_AnalysisFunction.Location = New System.Drawing.Point(11, 7)
        Me.Label_AnalysisFunction.Name = "Label_AnalysisFunction"
        Me.Label_AnalysisFunction.Size = New System.Drawing.Size(86, 13)
        Me.Label_AnalysisFunction.TabIndex = 9
        Me.Label_AnalysisFunction.Text = "Analysis function"
        '
        'Button_Previous
        '
        Me.Button_Previous.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Previous.Enabled = False
        Me.Button_Previous.Location = New System.Drawing.Point(438, 308)
        Me.Button_Previous.Name = "Button_Previous"
        Me.Button_Previous.Size = New System.Drawing.Size(75, 23)
        Me.Button_Previous.TabIndex = 17
        Me.Button_Previous.Text = "< Previous"
        Me.Button_Previous.UseVisualStyleBackColor = True
        '
        'Button_Execute
        '
        Me.Button_Execute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Execute.Location = New System.Drawing.Point(600, 308)
        Me.Button_Execute.Name = "Button_Execute"
        Me.Button_Execute.Size = New System.Drawing.Size(75, 23)
        Me.Button_Execute.TabIndex = 18
        Me.Button_Execute.Text = "Execute"
        Me.Button_Execute.UseVisualStyleBackColor = True
        '
        'AnalysisDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 343)
        Me.Controls.Add(Me.Button_Execute)
        Me.Controls.Add(Me.Button_Previous)
        Me.Controls.Add(Me.WizardPages1)
        Me.Controls.Add(Me.Button_Next)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "AnalysisDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Analysis"
        Me.WizardPages1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Private WithEvents Button_Next As System.Windows.Forms.Button
    Friend WithEvents Label_AnalaysisDescription As Label
    Friend WithEvents LinkLabel_Helplink As LinkLabel
    Friend WithEvents ListBox_AnalysisFunctions As ListBox
    Friend WithEvents WizardPages1 As WizardPages
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Private WithEvents Button_Previous As Button
    Friend WithEvents Label_AnalysisFunction As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Private WithEvents Button_Execute As Button
End Class
