<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MergeSeriesDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MergeSeriesDialog))
        OK_Button = New Button()
        Cancel_Button = New Button()
        CheckedListBox_AvailableSeries = New CheckedListBox()
        ListBox_SelectedSeries = New ListBox()
        Button_Up = New Button()
        Button_Down = New Button()
        Label1 = New Label()
        Label2 = New Label()
        SplitContainer1 = New SplitContainer()
        Button_SelectAll = New Button()
        Label3 = New Label()
        TextBox_MergedSeriesTitle = New TextBox()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        OK_Button.Location = New Point(369, 330)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(80, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' Cancel_Button
        ' 
        Cancel_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Cancel_Button.DialogResult = DialogResult.Cancel
        Cancel_Button.Location = New Point(457, 330)
        Cancel_Button.Name = "Cancel_Button"
        Cancel_Button.Size = New Size(80, 27)
        Cancel_Button.TabIndex = 1
        Cancel_Button.Text = "Cancel"
        ' 
        ' CheckedListBox_AvailableSeries
        ' 
        CheckedListBox_AvailableSeries.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        CheckedListBox_AvailableSeries.CheckOnClick = True
        CheckedListBox_AvailableSeries.FormattingEnabled = True
        CheckedListBox_AvailableSeries.Location = New Point(14, 30)
        CheckedListBox_AvailableSeries.Name = "CheckedListBox_AvailableSeries"
        CheckedListBox_AvailableSeries.Size = New Size(249, 238)
        CheckedListBox_AvailableSeries.TabIndex = 0
        CheckedListBox_AvailableSeries.ThreeDCheckBoxes = True
        ' 
        ' ListBox_SelectedSeries
        ' 
        ListBox_SelectedSeries.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListBox_SelectedSeries.FormattingEnabled = True
        ListBox_SelectedSeries.Location = New Point(7, 30)
        ListBox_SelectedSeries.Name = "ListBox_SelectedSeries"
        ListBox_SelectedSeries.Size = New Size(212, 274)
        ListBox_SelectedSeries.TabIndex = 0
        ' 
        ' Button_Up
        ' 
        Button_Up.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_Up.Image = CType(resources.GetObject("Button_Up.Image"), Image)
        Button_Up.Location = New Point(226, 30)
        Button_Up.Name = "Button_Up"
        Button_Up.Size = New Size(27, 27)
        Button_Up.TabIndex = 1
        Button_Up.UseVisualStyleBackColor = True
        ' 
        ' Button_Down
        ' 
        Button_Down.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_Down.Image = CType(resources.GetObject("Button_Down.Image"), Image)
        Button_Down.Location = New Point(226, 65)
        Button_Down.Name = "Button_Down"
        Button_Down.Size = New Size(27, 27)
        Button_Down.TabIndex = 2
        Button_Down.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(15, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(91, 15)
        Label1.TabIndex = 5
        Label1.Text = "Series to merge:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(4, 10)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 15)
        Label2.TabIndex = 5
        Label2.Text = "Priorities:"
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        SplitContainer1.Location = New Point(0, 0)
        SplitContainer1.Margin = New Padding(0)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(Button_SelectAll)
        SplitContainer1.Panel1.Controls.Add(Label1)
        SplitContainer1.Panel1.Controls.Add(CheckedListBox_AvailableSeries)
        SplitContainer1.Panel1MinSize = 100
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(Label2)
        SplitContainer1.Panel2.Controls.Add(ListBox_SelectedSeries)
        SplitContainer1.Panel2.Controls.Add(Button_Down)
        SplitContainer1.Panel2.Controls.Add(Button_Up)
        SplitContainer1.Panel2MinSize = 100
        SplitContainer1.Size = New Size(537, 320)
        SplitContainer1.SplitterDistance = 268
        SplitContainer1.SplitterWidth = 5
        SplitContainer1.TabIndex = 0
        ' 
        ' Button_SelectAll
        ' 
        Button_SelectAll.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button_SelectAll.Location = New Point(14, 285)
        Button_SelectAll.Name = "Button_SelectAll"
        Button_SelectAll.Size = New Size(80, 27)
        Button_SelectAll.TabIndex = 1
        Button_SelectAll.Text = "Select all"
        Button_SelectAll.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label3.AutoSize = True
        Label3.Location = New Point(12, 336)
        Label3.Name = "Label3"
        Label3.Size = New Size(122, 15)
        Label3.TabIndex = 7
        Label3.Text = "Title of merged series:"
        ' 
        ' TextBox_MergedSeriesTitle
        ' 
        TextBox_MergedSeriesTitle.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox_MergedSeriesTitle.Location = New Point(140, 333)
        TextBox_MergedSeriesTitle.Name = "TextBox_MergedSeriesTitle"
        TextBox_MergedSeriesTitle.Size = New Size(206, 23)
        TextBox_MergedSeriesTitle.TabIndex = 1
        ' 
        ' MergeSeriesDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Cancel_Button
        ClientSize = New Size(541, 370)
        Controls.Add(OK_Button)
        Controls.Add(TextBox_MergedSeriesTitle)
        Controls.Add(Label3)
        Controls.Add(Cancel_Button)
        Controls.Add(SplitContainer1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(557, 409)
        Name = "MergeSeriesDialog"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Merge Series"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel1.PerformLayout()
        SplitContainer1.Panel2.ResumeLayout(False)
        SplitContainer1.Panel2.PerformLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox_AvailableSeries As System.Windows.Forms.CheckedListBox
    Friend WithEvents ListBox_SelectedSeries As System.Windows.Forms.ListBox
    Friend WithEvents Button_Up As System.Windows.Forms.Button
    Friend WithEvents Button_Down As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button_SelectAll As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox_MergedSeriesTitle As System.Windows.Forms.TextBox

End Class
