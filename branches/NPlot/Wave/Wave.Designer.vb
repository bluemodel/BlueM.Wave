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
        Dim ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Dim toolStripSeparator As System.Windows.Forms.ToolStripSeparator
        Dim toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
        Dim StatusStrip1 As System.Windows.Forms.StatusStrip
        Dim ToolStrip1 As System.Windows.Forms.ToolStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wave))
        Dim ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Me.ToolStripStatusLabel_Log = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripButton_Neu = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Öffnen = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Speichern = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Export = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Import = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_EnterSeries = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Cut = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Analysis = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_EditChart = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Übersicht = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Drucken = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Kopieren = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_ReRead = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSplitButton_Help = New System.Windows.Forms.ToolStripSplitButton
        Me.HilfeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.NPlot1 = New NPlot.Windows.PlotSurface2D
        ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        StatusStrip1 = New System.Windows.Forms.StatusStrip
        ToolStrip1 = New System.Windows.Forms.ToolStrip
        ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        StatusStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripSeparator1
        '
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New System.Drawing.Size(6, 34)
        '
        'toolStripSeparator
        '
        toolStripSeparator.Name = "toolStripSeparator"
        toolStripSeparator.Size = New System.Drawing.Size(6, 34)
        '
        'toolStripSeparator3
        '
        toolStripSeparator3.Name = "toolStripSeparator3"
        toolStripSeparator3.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripSeparator2
        '
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New System.Drawing.Size(6, 34)
        '
        'StatusStrip1
        '
        StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel_Log})
        StatusStrip1.Location = New System.Drawing.Point(0, 638)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No
        StatusStrip1.ShowItemToolTips = True
        StatusStrip1.Size = New System.Drawing.Size(945, 22)
        StatusStrip1.SizingGrip = False
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel_Log
        '
        Me.ToolStripStatusLabel_Log.ActiveLinkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel_Log.Image = Global.IHWB.Wave.My.Resources.Resources.script
        Me.ToolStripStatusLabel_Log.IsLink = True
        Me.ToolStripStatusLabel_Log.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ToolStripStatusLabel_Log.LinkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel_Log.Margin = New System.Windows.Forms.Padding(3, 3, 0, 2)
        Me.ToolStripStatusLabel_Log.Name = "ToolStripStatusLabel_Log"
        Me.ToolStripStatusLabel_Log.Size = New System.Drawing.Size(16, 17)
        Me.ToolStripStatusLabel_Log.ToolTipText = "Log anzeigen"
        Me.ToolStripStatusLabel_Log.VisitedLinkColor = System.Drawing.SystemColors.ControlDarkDark
        '
        'ToolStrip1
        '
        ToolStrip1.AutoSize = False
        ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Neu, Me.ToolStripButton_Öffnen, Me.ToolStripButton_Speichern, Me.ToolStripButton_Export, ToolStripSeparator4, Me.ToolStripButton_Import, Me.ToolStripButton_EnterSeries, ToolStripSeparator2, Me.ToolStripButton_Cut, Me.ToolStripButton_Analysis, ToolStripSeparator1, Me.ToolStripButton_EditChart, Me.ToolStripButton_Übersicht, toolStripSeparator, Me.ToolStripButton_Drucken, Me.ToolStripButton_Kopieren, toolStripSeparator3, Me.ToolStripButton_ReRead, Me.ToolStripSplitButton_Help})
        ToolStrip1.Location = New System.Drawing.Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New System.Drawing.Size(945, 34)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton_Neu
        '
        Me.ToolStripButton_Neu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Neu.Image = CType(resources.GetObject("ToolStripButton_Neu.Image"), System.Drawing.Image)
        Me.ToolStripButton_Neu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Neu.Name = "ToolStripButton_Neu"
        Me.ToolStripButton_Neu.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Neu.Text = "&Neu"
        '
        'ToolStripButton_Öffnen
        '
        Me.ToolStripButton_Öffnen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Öffnen.Image = CType(resources.GetObject("ToolStripButton_Öffnen.Image"), System.Drawing.Image)
        Me.ToolStripButton_Öffnen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Öffnen.Name = "ToolStripButton_Öffnen"
        Me.ToolStripButton_Öffnen.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Öffnen.Text = "TEN-Datei Öffnen"
        '
        'ToolStripButton_Speichern
        '
        Me.ToolStripButton_Speichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Speichern.Image = CType(resources.GetObject("ToolStripButton_Speichern.Image"), System.Drawing.Image)
        Me.ToolStripButton_Speichern.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Speichern.Name = "ToolStripButton_Speichern"
        Me.ToolStripButton_Speichern.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Speichern.Text = "&Speichern"
        '
        'ToolStripButton_Export
        '
        Me.ToolStripButton_Export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Export.Image = Global.IHWB.Wave.My.Resources.Resources.page_white_go
        Me.ToolStripButton_Export.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Export.Name = "ToolStripButton_Export"
        Me.ToolStripButton_Export.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Export.Text = "Zeitreihen exportieren"
        Me.ToolStripButton_Export.ToolTipText = "Zeitreihen exportieren"
        '
        'ToolStripSeparator4
        '
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_Import
        '
        Me.ToolStripButton_Import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Import.Image = Global.IHWB.Wave.My.Resources.Resources.chart_line_add
        Me.ToolStripButton_Import.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Import.Name = "ToolStripButton_Import"
        Me.ToolStripButton_Import.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Import.Text = "Aus Datei importieren"
        '
        'ToolStripButton_EnterSeries
        '
        Me.ToolStripButton_EnterSeries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_EnterSeries.Image = Global.IHWB.Wave.My.Resources.Resources.chart_line_edit
        Me.ToolStripButton_EnterSeries.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_EnterSeries.Name = "ToolStripButton_EnterSeries"
        Me.ToolStripButton_EnterSeries.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_EnterSeries.Text = "Zeitreihe eingeben"
        '
        'ToolStripButton_Cut
        '
        Me.ToolStripButton_Cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Cut.Image = Global.IHWB.Wave.My.Resources.Resources.cut
        Me.ToolStripButton_Cut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Cut.Name = "ToolStripButton_Cut"
        Me.ToolStripButton_Cut.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Cut.Text = "Zeitreihe zuschneiden"
        '
        'ToolStripButton_Analysis
        '
        Me.ToolStripButton_Analysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Analysis.Image = Global.IHWB.Wave.My.Resources.Resources.calculator
        Me.ToolStripButton_Analysis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Analysis.Name = "ToolStripButton_Analysis"
        Me.ToolStripButton_Analysis.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Analysis.Text = "Analyse"
        '
        'ToolStripButton_EditChart
        '
        Me.ToolStripButton_EditChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_EditChart.Image = Global.IHWB.Wave.My.Resources.Resources.chart_curve_edit
        Me.ToolStripButton_EditChart.ImageTransparentColor = System.Drawing.SystemColors.Control
        Me.ToolStripButton_EditChart.Name = "ToolStripButton_EditChart"
        Me.ToolStripButton_EditChart.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_EditChart.Text = "Diagramm bearbeiten"
        '
        'ToolStripButton_Übersicht
        '
        Me.ToolStripButton_Übersicht.Checked = True
        Me.ToolStripButton_Übersicht.CheckOnClick = True
        Me.ToolStripButton_Übersicht.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton_Übersicht.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Übersicht.Image = Global.IHWB.Wave.My.Resources.Resources.application_split
        Me.ToolStripButton_Übersicht.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Übersicht.Name = "ToolStripButton_Übersicht"
        Me.ToolStripButton_Übersicht.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Übersicht.Text = "Übersicht an/aus"
        Me.ToolStripButton_Übersicht.ToolTipText = "Übersicht an/aus"
        '
        'ToolStripButton_Drucken
        '
        Me.ToolStripButton_Drucken.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Drucken.Image = CType(resources.GetObject("ToolStripButton_Drucken.Image"), System.Drawing.Image)
        Me.ToolStripButton_Drucken.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Drucken.Name = "ToolStripButton_Drucken"
        Me.ToolStripButton_Drucken.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Drucken.Text = "&Drucken"
        '
        'ToolStripButton_Kopieren
        '
        Me.ToolStripButton_Kopieren.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Kopieren.Image = CType(resources.GetObject("ToolStripButton_Kopieren.Image"), System.Drawing.Image)
        Me.ToolStripButton_Kopieren.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Kopieren.Name = "ToolStripButton_Kopieren"
        Me.ToolStripButton_Kopieren.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Kopieren.Text = "Diagramm &Kopieren (PNG)"
        '
        'ToolStripButton_ReRead
        '
        Me.ToolStripButton_ReRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ReRead.Image = Global.IHWB.Wave.My.Resources.Resources.arrow_refresh
        Me.ToolStripButton_ReRead.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ReRead.Name = "ToolStripButton_ReRead"
        Me.ToolStripButton_ReRead.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ReRead.Text = "ReRead"
        Me.ToolStripButton_ReRead.ToolTipText = "Importierte Zeitreihen neu einlesen"
        '
        'ToolStripSplitButton_Help
        '
        Me.ToolStripSplitButton_Help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSplitButton_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripSplitButton_Help.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HilfeToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.ToolStripSplitButton_Help.Image = Global.IHWB.Wave.My.Resources.Resources.help
        Me.ToolStripSplitButton_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton_Help.Name = "ToolStripSplitButton_Help"
        Me.ToolStripSplitButton_Help.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ToolStripSplitButton_Help.Size = New System.Drawing.Size(32, 31)
        Me.ToolStripSplitButton_Help.Text = "Help"
        '
        'HilfeToolStripMenuItem
        '
        Me.HilfeToolStripMenuItem.Name = "HilfeToolStripMenuItem"
        Me.HilfeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.HilfeToolStripMenuItem.Text = "Hilfe"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(12, 64)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(195, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.NPlot1)
        Me.SplitContainer1.Panel2MinSize = 100
        Me.SplitContainer1.Size = New System.Drawing.Size(945, 605)
        Me.SplitContainer1.SplitterDistance = 162
        Me.SplitContainer1.TabIndex = 1
        '
        'TChart2
        '
        '
        '
        '
        Me.TChart2.Aspect.ZOffset = 0
        Me.TChart2.Cursor = System.Windows.Forms.Cursors.Default
        Me.TChart2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TChart2.Location = New System.Drawing.Point(0, 0)
        Me.TChart2.Name = "TChart2"
        Me.TChart2.Size = New System.Drawing.Size(941, 158)
        Me.TChart2.TabIndex = 0
        '
        'NPlot1
        '
        Me.NPlot1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NPlot1.AutoScaleAutoGeneratedAxes = False
        Me.NPlot1.AutoScaleTitle = False
        Me.NPlot1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.NPlot1.DateTimeToolTip = False
        Me.NPlot1.Legend = Nothing
        Me.NPlot1.LegendZOrder = -1
        Me.NPlot1.Location = New System.Drawing.Point(0, 0)
        Me.NPlot1.Name = "NPlot1"
        Me.NPlot1.RightMenu = Nothing
        Me.NPlot1.ShowCoordinates = True
        Me.NPlot1.Size = New System.Drawing.Size(941, 435)
        Me.NPlot1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None
        Me.NPlot1.TabIndex = 0
        Me.NPlot1.Text = "PlotSurface2D1"
        Me.NPlot1.Title = ""
        Me.NPlot1.TitleFont = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.NPlot1.XAxis1 = Nothing
        Me.NPlot1.XAxis2 = Nothing
        Me.NPlot1.YAxis1 = Nothing
        Me.NPlot1.YAxis2 = Nothing
        '
        'Wave
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(945, 660)
        Me.Controls.Add(ToolStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Wave"
        Me.Text = "Wave"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Public WithEvents TChart2 As Steema.TeeChart.TChart
    Friend WithEvents ToolStripButton_Neu As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Öffnen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Speichern As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Drucken As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Kopieren As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_EditChart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Übersicht As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Export As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripButton_Analysis As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripStatusLabel_Log As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripButton_Import As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_EnterSeries As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Cut As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton_ReRead As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSplitButton_Help As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents HilfeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NPlot1 As NPlot.Windows.PlotSurface2D

End Class
