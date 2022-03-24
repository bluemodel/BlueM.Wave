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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.CheckedListBox_AvailableSeries = New System.Windows.Forms.CheckedListBox
        Me.ListBox_SelectedSeries = New System.Windows.Forms.ListBox
        Me.Button_Up = New System.Windows.Forms.Button
        Me.Button_Down = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Button_SelectAll = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBox_MergedSeriesTitle = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(309, 283)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'CheckedListBox_AvailableSeries
        '
        Me.CheckedListBox_AvailableSeries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBox_AvailableSeries.CheckOnClick = True
        Me.CheckedListBox_AvailableSeries.FormattingEnabled = True
        Me.CheckedListBox_AvailableSeries.Location = New System.Drawing.Point(12, 26)
        Me.CheckedListBox_AvailableSeries.Name = "CheckedListBox_AvailableSeries"
        Me.CheckedListBox_AvailableSeries.Size = New System.Drawing.Size(214, 214)
        Me.CheckedListBox_AvailableSeries.TabIndex = 0
        Me.CheckedListBox_AvailableSeries.ThreeDCheckBoxes = True
        '
        'ListBox_SelectedSeries
        '
        Me.ListBox_SelectedSeries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox_SelectedSeries.FormattingEnabled = True
        Me.ListBox_SelectedSeries.Location = New System.Drawing.Point(6, 26)
        Me.ListBox_SelectedSeries.Name = "ListBox_SelectedSeries"
        Me.ListBox_SelectedSeries.Size = New System.Drawing.Size(183, 238)
        Me.ListBox_SelectedSeries.TabIndex = 0
        '
        'Button_Up
        '
        Me.Button_Up.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Up.Image = Global.BlueM.Wave.My.Resources.Resources.arrow_up
        Me.Button_Up.Location = New System.Drawing.Point(195, 26)
        Me.Button_Up.Name = "Button_Up"
        Me.Button_Up.Size = New System.Drawing.Size(23, 23)
        Me.Button_Up.TabIndex = 1
        Me.Button_Up.UseVisualStyleBackColor = True
        '
        'Button_Down
        '
        Me.Button_Down.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Down.Image = Global.BlueM.Wave.My.Resources.Resources.arrow_down
        Me.Button_Down.Location = New System.Drawing.Point(195, 56)
        Me.Button_Down.Name = "Button_Down"
        Me.Button_Down.Size = New System.Drawing.Size(23, 23)
        Me.Button_Down.TabIndex = 2
        Me.Button_Down.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Series to merge:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Priorities:"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button_SelectAll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CheckedListBox_AvailableSeries)
        Me.SplitContainer1.Panel1MinSize = 100
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListBox_SelectedSeries)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button_Down)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button_Up)
        Me.SplitContainer1.Panel2MinSize = 100
        Me.SplitContainer1.Size = New System.Drawing.Size(460, 277)
        Me.SplitContainer1.SplitterDistance = 230
        Me.SplitContainer1.TabIndex = 0
        '
        'Button_SelectAll
        '
        Me.Button_SelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_SelectAll.Location = New System.Drawing.Point(12, 247)
        Me.Button_SelectAll.Name = "Button_SelectAll"
        Me.Button_SelectAll.Size = New System.Drawing.Size(75, 23)
        Me.Button_SelectAll.TabIndex = 1
        Me.Button_SelectAll.Text = "Select all"
        Me.Button_SelectAll.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 291)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Title of merged series:"
        '
        'TextBox_MergedSeriesTitle
        '
        Me.TextBox_MergedSeriesTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_MergedSeriesTitle.Location = New System.Drawing.Point(126, 288)
        Me.TextBox_MergedSeriesTitle.Name = "TextBox_MergedSeriesTitle"
        Me.TextBox_MergedSeriesTitle.Size = New System.Drawing.Size(177, 20)
        Me.TextBox_MergedSeriesTitle.TabIndex = 1
        '
        'MergeSeriesDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(464, 321)
        Me.Controls.Add(Me.TextBox_MergedSeriesTitle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(480, 360)
        Me.Name = "MergeSeriesDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Merge Series"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
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
