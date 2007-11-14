<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Wave
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wave))
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.NeuToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ÖffnenToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SpeichernToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.EditToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ÜbersichtToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.DruckenToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.KopierenToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.HilfeToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TChart1
        '
        '
        '
        '
        Me.TChart1.Aspect.ElevationFloat = 345
        Me.TChart1.Aspect.RotationFloat = 345
        Me.TChart1.Aspect.View3D = False
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Bottom.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Depth.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Depth.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.DepthTop.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Left.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Left.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Right.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Right.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Top.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Top.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Header.Lines = New String() {"TeeChart"}
        '
        '
        '
        Me.TChart1.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        '
        '
        '
        Me.TChart1.Legend.Shadow.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Title.Font.Bold = True
        '
        '
        '
        Me.TChart1.Legend.Title.Pen.Visible = False
        Me.TChart1.Location = New System.Drawing.Point(-2, -8)
        Me.TChart1.Name = "TChart1"
        Me.TChart1.Size = New System.Drawing.Size(945, 190)
        Me.TChart1.TabIndex = 4
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Back.AutoHide = False
        '
        '
        '
        Me.TChart1.Walls.Bottom.AutoHide = False
        '
        '
        '
        Me.TChart1.Walls.Left.AutoHide = False
        '
        '
        '
        Me.TChart1.Walls.Right.AutoHide = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "txt"
        Me.OpenFileDialog1.Filter = "Text-Dateien (*.txt)|*.txt|SMUSI-Dateien (*.asc)|*.asc|ZRE-Dateien (*.zre)|*.zre|" & _
            "CSV-Dateien (*.csv)|*.csv|WEL-Dateien (*.wel, *.kwl)|*.wel;*.kwl|TeeChart-Dateie" & _
            "n (*.ten)|*.ten|Alle Dateien (*.*)|*.*"
        Me.OpenFileDialog1.Title = "Datei importieren"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NeuToolStripButton, Me.ÖffnenToolStripButton, Me.SpeichernToolStripButton, Me.ToolStripSeparator1, Me.EditToolStripButton, Me.ÜbersichtToolStripButton, Me.toolStripSeparator, Me.DruckenToolStripButton, Me.KopierenToolStripButton, Me.toolStripSeparator3, Me.HilfeToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(945, 34)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'NeuToolStripButton
        '
        Me.NeuToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NeuToolStripButton.Image = CType(resources.GetObject("NeuToolStripButton.Image"), System.Drawing.Image)
        Me.NeuToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NeuToolStripButton.Name = "NeuToolStripButton"
        Me.NeuToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.NeuToolStripButton.Text = "&Neu"
        '
        'ÖffnenToolStripButton
        '
        Me.ÖffnenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ÖffnenToolStripButton.Image = CType(resources.GetObject("ÖffnenToolStripButton.Image"), System.Drawing.Image)
        Me.ÖffnenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ÖffnenToolStripButton.Name = "ÖffnenToolStripButton"
        Me.ÖffnenToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.ÖffnenToolStripButton.Text = "Ö&ffnen"
        '
        'SpeichernToolStripButton
        '
        Me.SpeichernToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SpeichernToolStripButton.Image = CType(resources.GetObject("SpeichernToolStripButton.Image"), System.Drawing.Image)
        Me.SpeichernToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SpeichernToolStripButton.Name = "SpeichernToolStripButton"
        Me.SpeichernToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.SpeichernToolStripButton.Text = "&Speichern"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 34)
        '
        'EditToolStripButton
        '
        Me.EditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.EditToolStripButton.Image = Global.Wave.My.Resources.Resources.pencil
        Me.EditToolStripButton.ImageTransparentColor = System.Drawing.SystemColors.Control
        Me.EditToolStripButton.Name = "EditToolStripButton"
        Me.EditToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.EditToolStripButton.Text = "Bearbeiten"
        '
        'ÜbersichtToolStripButton
        '
        Me.ÜbersichtToolStripButton.Checked = True
        Me.ÜbersichtToolStripButton.CheckOnClick = True
        Me.ÜbersichtToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ÜbersichtToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ÜbersichtToolStripButton.Image = Global.Wave.My.Resources.Resources.application_split
        Me.ÜbersichtToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ÜbersichtToolStripButton.Name = "ÜbersichtToolStripButton"
        Me.ÜbersichtToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.ÜbersichtToolStripButton.Text = "Übersicht"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 34)
        '
        'DruckenToolStripButton
        '
        Me.DruckenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DruckenToolStripButton.Image = CType(resources.GetObject("DruckenToolStripButton.Image"), System.Drawing.Image)
        Me.DruckenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DruckenToolStripButton.Name = "DruckenToolStripButton"
        Me.DruckenToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.DruckenToolStripButton.Text = "&Drucken"
        '
        'KopierenToolStripButton
        '
        Me.KopierenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.KopierenToolStripButton.Image = CType(resources.GetObject("KopierenToolStripButton.Image"), System.Drawing.Image)
        Me.KopierenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.KopierenToolStripButton.Name = "KopierenToolStripButton"
        Me.KopierenToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.KopierenToolStripButton.Text = "&Kopieren (PNG)"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 34)
        '
        'HilfeToolStripButton
        '
        Me.HilfeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HilfeToolStripButton.Image = CType(resources.GetObject("HilfeToolStripButton.Image"), System.Drawing.Image)
        Me.HilfeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HilfeToolStripButton.Name = "HilfeToolStripButton"
        Me.HilfeToolStripButton.Size = New System.Drawing.Size(23, 31)
        Me.HilfeToolStripButton.Text = "Hi&lfe"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(12, 64)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(195, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 34)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TChart2)
        Me.SplitContainer1.Panel1MinSize = 100
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TChart1)
        Me.SplitContainer1.Panel2MinSize = 100
        Me.SplitContainer1.Size = New System.Drawing.Size(945, 626)
        Me.SplitContainer1.SplitterDistance = 118
        Me.SplitContainer1.TabIndex = 6
        '
        'TChart2
        '
        '
        '
        '
        Me.TChart2.Aspect.ElevationFloat = 345
        Me.TChart2.Aspect.RotationFloat = 345
        Me.TChart2.Aspect.View3D = False
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Automatic = True
        '
        '
        '
        Me.TChart2.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart2.Axes.Bottom.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart2.Axes.Depth.Automatic = True
        '
        '
        '
        Me.TChart2.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart2.Axes.Depth.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Automatic = True
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart2.Axes.DepthTop.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart2.Axes.Left.Automatic = True
        '
        '
        '
        Me.TChart2.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart2.Axes.Left.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart2.Axes.Right.Automatic = True
        '
        '
        '
        Me.TChart2.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart2.Axes.Right.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart2.Axes.Top.Automatic = True
        '
        '
        '
        Me.TChart2.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart2.Axes.Top.Grid.ZPosition = 0
        Me.TChart2.Cursor = System.Windows.Forms.Cursors.Default
        '
        '
        '
        Me.TChart2.Header.Lines = New String() {"TeeChart"}
        Me.TChart2.Header.Visible = False
        '
        '
        '
        Me.TChart2.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        '
        '
        '
        Me.TChart2.Legend.Shadow.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Title.Font.Bold = True
        '
        '
        '
        Me.TChart2.Legend.Title.Pen.Visible = False
        Me.TChart2.Location = New System.Drawing.Point(-2, -2)
        Me.TChart2.Name = "TChart2"
        Me.TChart2.Size = New System.Drawing.Size(942, 106)
        Me.TChart2.TabIndex = 6
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Back.AutoHide = False
        '
        '
        '
        Me.TChart2.Walls.Bottom.AutoHide = False
        '
        '
        '
        Me.TChart2.Walls.Left.AutoHide = False
        '
        '
        '
        Me.TChart2.Walls.Right.AutoHide = False
        '
        'Wave
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(945, 660)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Wave"
        Me.Text = "Wave"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Public WithEvents TChart2 As Steema.TeeChart.TChart
    Public WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents NeuToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ÖffnenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SpeichernToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DruckenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents KopierenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HilfeToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ÜbersichtToolStripButton As System.Windows.Forms.ToolStripButton

End Class
