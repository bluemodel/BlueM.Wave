<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AnnualStatistics_Dialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnnualStatistics_Dialog))
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.ComboBox_startMonth = New System.Windows.Forms.ComboBox()
        Me.Label_startMonth = New System.Windows.Forms.Label()
        Me.CheckBox_boundingbox = New System.Windows.Forms.CheckBox()
        Me.CheckBox_plottingPosition = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Button_OK
        '
        Me.Button_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button_OK.Location = New System.Drawing.Point(384, 144)
        Me.Button_OK.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(150, 44)
        Me.Button_OK.TabIndex = 3
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'ComboBox_startMonth
        '
        Me.ComboBox_startMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_startMonth.FormattingEnabled = True
        Me.ComboBox_startMonth.Location = New System.Drawing.Point(288, 12)
        Me.ComboBox_startMonth.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ComboBox_startMonth.Name = "ComboBox_startMonth"
        Me.ComboBox_startMonth.Size = New System.Drawing.Size(242, 33)
        Me.ComboBox_startMonth.TabIndex = 14
        '
        'Label_startMonth
        '
        Me.Label_startMonth.AutoSize = True
        Me.Label_startMonth.Location = New System.Drawing.Point(24, 17)
        Me.Label_startMonth.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label_startMonth.Name = "Label_startMonth"
        Me.Label_startMonth.Size = New System.Drawing.Size(257, 25)
        Me.Label_startMonth.TabIndex = 13
        Me.Label_startMonth.Text = "Start of hydrological year:"
        '
        'CheckBox_boundingbox
        '
        Me.CheckBox_boundingbox.AutoSize = True
        Me.CheckBox_boundingbox.Location = New System.Drawing.Point(29, 63)
        Me.CheckBox_boundingbox.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.CheckBox_boundingbox.Name = "CheckBox_boundingbox"
        Me.CheckBox_boundingbox.Size = New System.Drawing.Size(516, 29)
        Me.CheckBox_boundingbox.TabIndex = 15
        Me.CheckBox_boundingbox.Text = "Generate annual bounding boxes (max, avg, min)"
        Me.CheckBox_boundingbox.UseVisualStyleBackColor = True
        '
        'CheckBox_plottingPosition
        '
        Me.CheckBox_plottingPosition.AutoSize = True
        Me.CheckBox_plottingPosition.Location = New System.Drawing.Point(29, 101)
        Me.CheckBox_plottingPosition.Name = "CheckBox_plottingPosition"
        Me.CheckBox_plottingPosition.Size = New System.Drawing.Size(348, 29)
        Me.CheckBox_plottingPosition.TabIndex = 16
        Me.CheckBox_plottingPosition.Text = "Create plotting position diagram"
        Me.CheckBox_plottingPosition.UseVisualStyleBackColor = True
        '
        'AnnualStatistics_Dialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 212)
        Me.Controls.Add(Me.CheckBox_plottingPosition)
        Me.Controls.Add(Me.CheckBox_boundingbox)
        Me.Controls.Add(Me.ComboBox_startMonth)
        Me.Controls.Add(Me.Label_startMonth)
        Me.Controls.Add(Me.Button_OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AnnualStatistics_Dialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AnnualStatistics"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_startMonth As ComboBox
    Friend WithEvents Label_startMonth As Label
    Friend WithEvents CheckBox_boundingbox As CheckBox
    Friend WithEvents CheckBox_plottingPosition As CheckBox
End Class
