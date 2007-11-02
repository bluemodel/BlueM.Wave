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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wave))
        Me.TextSource1 = New Steema.TeeChart.Data.TextSource
        Me.Commander1 = New Steema.TeeChart.Commander
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.DateiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem_OpenFile = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem_OpenTEN = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.MenuItem_Exit = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton_OpenFile = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_OpenTEN = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextSource1
        '
        Me.TextSource1.DecimalSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.TextSource1.Fields.AddRange(New Steema.TeeChart.Data.TextField() {New Steema.TeeChart.Data.TextField(0, "x"), New Steema.TeeChart.Data.TextField(1, "y")})
        Me.TextSource1.HeaderLines = 1
        Me.TextSource1.Separator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.TextSource1.Series = Nothing
        '
        'Commander1
        '
        Me.Commander1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.Commander1.AutoSize = False
        Me.Commander1.ButtonSize = New System.Drawing.Size(32, 32)
        Me.Commander1.Chart = Me.TChart1
        Me.Commander1.Divider = False
        Me.Commander1.Dock = System.Windows.Forms.DockStyle.None
        Me.Commander1.DropDownArrows = True
        Me.Commander1.LabelValues = True
        Me.Commander1.Location = New System.Drawing.Point(88, 24)
        Me.Commander1.Name = "Commander1"
        Me.Commander1.ShowToolTips = True
        Me.Commander1.Size = New System.Drawing.Size(345, 34)
        Me.Commander1.TabIndex = 2
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "txt"
        Me.OpenFileDialog1.Filter = "Text-Dateien (*.txt)|*.txt|SMUSI-Dateien (*.asc)|*.asc|ZRE-Dateien (*.zre)|*.zre|" & _
            "CSV-Dateien (*.csv)|*.csv|WEL-Dateien (*.wel, *.kwl)|*.wel;*.kwl|Alle Dateien (*" & _
            ".*)|*.*"
        Me.OpenFileDialog1.Title = "Textdatei öffnen"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DateiToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(945, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'DateiToolStripMenuItem
        '
        Me.DateiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItem_OpenFile, Me.MenuItem_OpenTEN, Me.ToolStripSeparator2, Me.MenuItem_Exit})
        Me.DateiToolStripMenuItem.Name = "DateiToolStripMenuItem"
        Me.DateiToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.DateiToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.DateiToolStripMenuItem.Text = "Datei"
        '
        'MenuItem_OpenFile
        '
        Me.MenuItem_OpenFile.Image = Global.Wave.My.Resources.Resources.chart_line_add
        Me.MenuItem_OpenFile.Name = "MenuItem_OpenFile"
        Me.MenuItem_OpenFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.MenuItem_OpenFile.Size = New System.Drawing.Size(221, 22)
        Me.MenuItem_OpenFile.Text = "Textdatei öffnen..."
        '
        'MenuItem_OpenTEN
        '
        Me.MenuItem_OpenTEN.Image = Global.Wave.My.Resources.Resources.icon_teechart
        Me.MenuItem_OpenTEN.Name = "MenuItem_OpenTEN"
        Me.MenuItem_OpenTEN.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.MenuItem_OpenTEN.Size = New System.Drawing.Size(221, 22)
        Me.MenuItem_OpenTEN.Text = "TEN-Datei öffnen..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(218, 6)
        '
        'MenuItem_Exit
        '
        Me.MenuItem_Exit.Name = "MenuItem_Exit"
        Me.MenuItem_Exit.Size = New System.Drawing.Size(221, 22)
        Me.MenuItem_Exit.Text = "Schließen"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_OpenFile, Me.ToolStripButton_OpenTEN, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(945, 34)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton_OpenFile
        '
        Me.ToolStripButton_OpenFile.AutoSize = False
        Me.ToolStripButton_OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_OpenFile.Image = Global.Wave.My.Resources.Resources.chart_line_add
        Me.ToolStripButton_OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_OpenFile.Name = "ToolStripButton_OpenFile"
        Me.ToolStripButton_OpenFile.Size = New System.Drawing.Size(32, 32)
        Me.ToolStripButton_OpenFile.Text = "Textdatei öffnen"
        '
        'ToolStripButton_OpenTEN
        '
        Me.ToolStripButton_OpenTEN.AutoSize = False
        Me.ToolStripButton_OpenTEN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_OpenTEN.Image = Global.Wave.My.Resources.Resources.icon_teechart
        Me.ToolStripButton_OpenTEN.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_OpenTEN.Name = "ToolStripButton_OpenTEN"
        Me.ToolStripButton_OpenTEN.Size = New System.Drawing.Size(32, 32)
        Me.ToolStripButton_OpenTEN.Text = "TEN-Datei öffnen"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 34)
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
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 58)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(945, 602)
        Me.SplitContainer1.SplitterDistance = 114
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
        Me.TChart1.Location = New System.Drawing.Point(-2, -2)
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
        'Wave
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(945, 660)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Commander1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Wave"
        Me.Text = "Wave"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextSource1 As Steema.TeeChart.Data.TextSource
    Friend WithEvents Commander1 As Steema.TeeChart.Commander
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents DateiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItem_OpenFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItem_OpenTEN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton_OpenFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_OpenTEN As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuItem_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Public WithEvents TChart2 As Steema.TeeChart.TChart
    Public WithEvents TChart1 As Steema.TeeChart.TChart

End Class
