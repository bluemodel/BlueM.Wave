<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AnalysisResultChart
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
        Dim ToolStrip1 As System.Windows.Forms.ToolStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnalysisResultChart))
        Me.ToolStripButton_Copy = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_EditChart = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        ToolStrip1 = New System.Windows.Forms.ToolStrip()
        ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Copy, Me.ToolStripButton_EditChart})
        ToolStrip1.Location = New System.Drawing.Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New System.Drawing.Size(784, 25)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton_Copy
        '
        Me.ToolStripButton_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Copy.Image = CType(resources.GetObject("ToolStripButton_Copy.Image"), System.Drawing.Image)
        Me.ToolStripButton_Copy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Copy.Name = "ToolStripButton_Copy"
        Me.ToolStripButton_Copy.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_Copy.Text = "&Copy chart to clipboard (PNG)"
        '
        'ToolStripButton_EditChart
        '
        Me.ToolStripButton_EditChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_EditChart.Image = Global.BlueM.Wave.My.Resources.Resources.chart_curve_edit
        Me.ToolStripButton_EditChart.ImageTransparentColor = System.Drawing.SystemColors.Control
        Me.ToolStripButton_EditChart.Name = "ToolStripButton_EditChart"
        Me.ToolStripButton_EditChart.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton_EditChart.Text = "Edit chart"
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 425)
        Me.Panel1.TabIndex = 3
        '
        'AnalysisResultChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "AnalysisResultChart"
        Me.Text = "Analysis Result Chart"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripButton_Copy As ToolStripButton
    Friend WithEvents ToolStripButton_EditChart As ToolStripButton
    Friend WithEvents Panel1 As Panel
End Class
