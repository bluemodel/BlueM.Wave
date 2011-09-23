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
        Dim ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
        Dim StatusStrip1 As System.Windows.Forms.StatusStrip
        Dim ToolStrip1 As System.Windows.Forms.ToolStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wave))
        Dim ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Me.ToolStripStatusLabel_Log = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripButton_Neu = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSplitButton_Oeffnen = New System.Windows.Forms.ToolStripSplitButton
        Me.ToolStripMenuItem_ZeitreihenImportieren = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_ZeitreiheEingeben = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem_Refresh = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem_Oeffnen = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSplitButton_Speichern = New System.Windows.Forms.ToolStripSplitButton
        Me.ToolStripMenuItem_Speichern = New System.Windows.Forms.ToolStripMenuItem
        Me.BlueMFormatExportierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripButton_Kopieren = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Drucken = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Cut = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Analysis = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Convert = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_EditChart = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Übersicht = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSplitButton_Help = New System.Windows.Forms.ToolStripSplitButton
        Me.HilfeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
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
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Neu, Me.ToolStripSplitButton_Oeffnen, Me.ToolStripSplitButton_Speichern, ToolStripSeparator4, Me.ToolStripButton_Kopieren, Me.ToolStripButton_Drucken, ToolStripSeparator2, Me.ToolStripButton_Cut, Me.ToolStripButton_Analysis, Me.ToolStripButton_Convert, ToolStripSeparator1, Me.ToolStripButton_EditChart, Me.ToolStripButton_Übersicht, toolStripSeparator, Me.ToolStripSplitButton_Help})
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
        'ToolStripSplitButton_Oeffnen
        '
        Me.ToolStripSplitButton_Oeffnen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripSplitButton_Oeffnen.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ZeitreihenImportieren, Me.ToolStripMenuItem_ZeitreiheEingeben, Me.ToolStripSeparator5, Me.ToolStripMenuItem_Refresh, Me.ToolStripSeparator6, Me.ToolStripMenuItem_Oeffnen})
        Me.ToolStripSplitButton_Oeffnen.Image = CType(resources.GetObject("ToolStripSplitButton_Oeffnen.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton_Oeffnen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton_Oeffnen.Name = "ToolStripSplitButton_Oeffnen"
        Me.ToolStripSplitButton_Oeffnen.Size = New System.Drawing.Size(32, 31)
        Me.ToolStripSplitButton_Oeffnen.Text = "Öffnen"
        '
        'ToolStripMenuItem_ZeitreihenImportieren
        '
        Me.ToolStripMenuItem_ZeitreihenImportieren.Image = Global.IHWB.Wave.My.Resources.Resources.chart_line_add
        Me.ToolStripMenuItem_ZeitreihenImportieren.Name = "ToolStripMenuItem_ZeitreihenImportieren"
        Me.ToolStripMenuItem_ZeitreihenImportieren.Size = New System.Drawing.Size(209, 22)
        Me.ToolStripMenuItem_ZeitreihenImportieren.Text = "Zeitreihe(n) importieren"
        '
        'ToolStripMenuItem_ZeitreiheEingeben
        '
        Me.ToolStripMenuItem_ZeitreiheEingeben.Image = Global.IHWB.Wave.My.Resources.Resources.chart_line_edit
        Me.ToolStripMenuItem_ZeitreiheEingeben.Name = "ToolStripMenuItem_ZeitreiheEingeben"
        Me.ToolStripMenuItem_ZeitreiheEingeben.Size = New System.Drawing.Size(209, 22)
        Me.ToolStripMenuItem_ZeitreiheEingeben.Text = "Zeitreihe eingeben"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(206, 6)
        '
        'ToolStripMenuItem_Refresh
        '
        Me.ToolStripMenuItem_Refresh.Image = Global.IHWB.Wave.My.Resources.Resources.arrow_refresh
        Me.ToolStripMenuItem_Refresh.Name = "ToolStripMenuItem_Refresh"
        Me.ToolStripMenuItem_Refresh.Size = New System.Drawing.Size(209, 22)
        Me.ToolStripMenuItem_Refresh.Text = "Dateien neu einlesen"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(206, 6)
        '
        'ToolStripMenuItem_Oeffnen
        '
        Me.ToolStripMenuItem_Oeffnen.Image = Global.IHWB.Wave.My.Resources.Resources.chart_curve
        Me.ToolStripMenuItem_Oeffnen.Name = "ToolStripMenuItem_Oeffnen"
        Me.ToolStripMenuItem_Oeffnen.Size = New System.Drawing.Size(209, 22)
        Me.ToolStripMenuItem_Oeffnen.Text = "Diagramm öffnen (*.TEN)"
        '
        'ToolStripSplitButton_Speichern
        '
        Me.ToolStripSplitButton_Speichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripSplitButton_Speichern.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_Speichern, Me.BlueMFormatExportierenToolStripMenuItem})
        Me.ToolStripSplitButton_Speichern.Image = CType(resources.GetObject("ToolStripSplitButton_Speichern.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton_Speichern.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton_Speichern.Name = "ToolStripSplitButton_Speichern"
        Me.ToolStripSplitButton_Speichern.Size = New System.Drawing.Size(32, 31)
        Me.ToolStripSplitButton_Speichern.Text = "&Speichern"
        '
        'ToolStripMenuItem_Speichern
        '
        Me.ToolStripMenuItem_Speichern.Image = Global.IHWB.Wave.My.Resources.Resources.chart_pie
        Me.ToolStripMenuItem_Speichern.Name = "ToolStripMenuItem_Speichern"
        Me.ToolStripMenuItem_Speichern.Size = New System.Drawing.Size(197, 22)
        Me.ToolStripMenuItem_Speichern.Text = "Diagramm exportieren"
        '
        'BlueMFormatExportierenToolStripMenuItem
        '
        Me.BlueMFormatExportierenToolStripMenuItem.Image = Global.IHWB.Wave.My.Resources.Resources.page_white_go
        Me.BlueMFormatExportierenToolStripMenuItem.Name = "BlueMFormatExportierenToolStripMenuItem"
        Me.BlueMFormatExportierenToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.BlueMFormatExportierenToolStripMenuItem.Text = "Zeitreihe(n) exportieren"
        '
        'ToolStripSeparator4
        '
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_Kopieren
        '
        Me.ToolStripButton_Kopieren.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Kopieren.Image = CType(resources.GetObject("ToolStripButton_Kopieren.Image"), System.Drawing.Image)
        Me.ToolStripButton_Kopieren.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Kopieren.Name = "ToolStripButton_Kopieren"
        Me.ToolStripButton_Kopieren.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Kopieren.Text = "Diagramm in die Zwis&chenablage kopieren (PNG)"
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
        Me.ToolStripButton_Analysis.Text = "Zeitreihen analysieren"
        '
        'ToolStripButton_Convert
        '
        Me.ToolStripButton_Convert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Convert.Image = Global.IHWB.Wave.My.Resources.Resources.script
        Me.ToolStripButton_Convert.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Convert.Name = "ToolStripButton_Convert"
        Me.ToolStripButton_Convert.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Convert.Text = "Zeitreihenformate konvertieren"
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
        Me.HilfeToolStripMenuItem.Text = "(Hilfe (Wiki"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Image = Global.IHWB.Wave.My.Resources.Resources.BlueM_icon
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AboutToolStripMenuItem.Text = "About Wave"
        '
        'TChart1
        '
        '
        '
        '
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Bottom.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Bottom.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Bottom.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Bottom.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Bottom.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Bottom.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Depth.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Depth.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Depth.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Depth.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Depth.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Depth.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.DepthTop.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.DepthTop.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Left.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Left.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Left.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Left.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Left.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Left.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Right.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Right.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Right.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Right.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Right.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Right.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Top.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Top.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Top.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Top.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Top.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Axes.Top.Title.Bevel.StringColorTwo = "FF808080"
        Me.TChart1.Dock = System.Windows.Forms.DockStyle.Fill
        '
        '
        '
        '
        '
        '
        Me.TChart1.Footer.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Footer.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Footer.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Footer.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Header.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Header.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Header.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Header.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Legend.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Legend.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Legend.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Legend.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Legend.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Legend.Title.Bevel.StringColorTwo = "FF808080"
        Me.TChart1.Location = New System.Drawing.Point(0, 0)
        Me.TChart1.Name = "TChart1"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Panel.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Panel.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Panel.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Panel.Bevel.StringColorTwo = "FF808080"
        Me.TChart1.Size = New System.Drawing.Size(941, 435)
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubFooter.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.SubFooter.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.SubFooter.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.SubFooter.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubHeader.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.SubHeader.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.SubHeader.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.SubHeader.Bevel.StringColorTwo = "FF808080"
        Me.TChart1.TabIndex = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Back.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Walls.Back.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Walls.Back.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Walls.Back.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Bottom.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Walls.Bottom.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Walls.Bottom.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Walls.Bottom.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Left.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Walls.Left.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Walls.Left.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Walls.Left.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Right.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Walls.Right.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Walls.Right.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart1.Walls.Right.Bevel.StringColorTwo = "FF808080"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.TChart1)
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
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Bottom.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Bottom.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Bottom.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Bottom.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Bottom.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Bottom.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Depth.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Depth.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Depth.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Depth.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Depth.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Depth.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.DepthTop.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.DepthTop.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Left.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Left.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Left.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Left.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Left.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Left.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Right.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Right.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Right.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Right.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Right.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Right.Title.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Labels.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Top.Labels.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Top.Labels.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Top.Labels.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Top.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Top.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Axes.Top.Title.Bevel.StringColorTwo = "FF808080"
        Me.TChart2.Cursor = System.Windows.Forms.Cursors.Default
        Me.TChart2.Dock = System.Windows.Forms.DockStyle.Fill
        '
        '
        '
        '
        '
        '
        Me.TChart2.Footer.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Footer.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Footer.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Footer.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Header.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Header.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Header.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Header.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Legend.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Legend.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Legend.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Legend.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Legend.Title.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Legend.Title.Bevel.StringColorTwo = "FF808080"
        Me.TChart2.Location = New System.Drawing.Point(0, 0)
        Me.TChart2.Name = "TChart2"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Panel.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Panel.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Panel.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Panel.Bevel.StringColorTwo = "FF808080"
        Me.TChart2.Size = New System.Drawing.Size(941, 158)
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubFooter.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.SubFooter.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.SubFooter.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.SubFooter.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubHeader.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.SubHeader.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.SubHeader.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.SubHeader.Bevel.StringColorTwo = "FF808080"
        Me.TChart2.TabIndex = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Back.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Walls.Back.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Walls.Back.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Walls.Back.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Bottom.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Walls.Bottom.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Walls.Bottom.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Walls.Bottom.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Left.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Walls.Left.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Walls.Left.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Walls.Left.Bevel.StringColorTwo = "FF808080"
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Right.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Walls.Right.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Walls.Right.Bevel.StringColorOne = "FFFFFFFF"
        Me.TChart2.Walls.Right.Bevel.StringColorTwo = "FF808080"
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
    Friend WithEvents TChart2 As Steema.TeeChart.TChart
    Friend WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents ToolStripButton_Neu As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSplitButton_Oeffnen As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ToolStripSplitButton_Speichern As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ToolStripButton_Drucken As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Kopieren As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_EditChart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Übersicht As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripButton_Analysis As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripStatusLabel_Log As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripButton_Cut As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSplitButton_Help As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents HilfeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton_Convert As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripMenuItem_ZeitreihenImportieren As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Oeffnen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem_Speichern As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BlueMFormatExportierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ZeitreiheEingeben As System.Windows.Forms.ToolStripMenuItem

End Class
