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
        Dim ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Me.ToolStripStatusLabel_Log = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripButton_Neu = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSplitButton_Oeffnen = New System.Windows.Forms.ToolStripSplitButton
        Me.ToolStripMenuItem_ZeitreihenImportieren = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_ZeitreiheEingeben = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_Refresh = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_TENLaden = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_ThemeLaden = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSplitButton_Speichern = New System.Windows.Forms.ToolStripSplitButton
        Me.ToolStripMenuItem_ExportDiagramm = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_ExportZeitreihe = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripButton_Kopieren = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Drucken = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Cut = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Analysis = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Convert = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_EditChart = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Übersicht = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_NormalMode = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Zoom = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Pan = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_ZoomPrevious = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSplitButton_Help = New System.Windows.Forms.ToolStripSplitButton
        Me.HilfeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label_Navigate = New System.Windows.Forms.Label
        Me.Button_NavRight = New System.Windows.Forms.Button
        Me.Button_NavLeft = New System.Windows.Forms.Button
        Me.NumericUpDown_NavMultiplier = New System.Windows.Forms.NumericUpDown
        Me.ComboBox_NavIncrement = New System.Windows.Forms.ComboBox
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.Panel_Navigation = New System.Windows.Forms.Panel
        Me.Label_Display = New System.Windows.Forms.Label
        Me.NumericUpDown_DisplayRangeMultiplier = New System.Windows.Forms.NumericUpDown
        Me.ComboBox_DisplayRangeUnit = New System.Windows.Forms.ComboBox
        Me.DateTimePicker_NavEnd = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker_NavStart = New System.Windows.Forms.DateTimePicker
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        StatusStrip1 = New System.Windows.Forms.StatusStrip
        ToolStrip1 = New System.Windows.Forms.ToolStrip
        ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        StatusStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDown_NavMultiplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_Navigation.SuspendLayout()
        CType(Me.NumericUpDown_DisplayRangeMultiplier, System.ComponentModel.ISupportInitialize).BeginInit()
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
        StatusStrip1.Location = New System.Drawing.Point(0, 639)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No
        StatusStrip1.ShowItemToolTips = True
        StatusStrip1.Size = New System.Drawing.Size(944, 22)
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
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Neu, Me.ToolStripSplitButton_Oeffnen, Me.ToolStripSplitButton_Speichern, ToolStripSeparator4, Me.ToolStripButton_Kopieren, Me.ToolStripButton_Drucken, ToolStripSeparator2, Me.ToolStripButton_Cut, Me.ToolStripButton_Analysis, Me.ToolStripButton_Convert, ToolStripSeparator1, Me.ToolStripButton_EditChart, Me.ToolStripButton_Übersicht, toolStripSeparator, Me.ToolStripButton_NormalMode, Me.ToolStripButton_Zoom, Me.ToolStripButton_Pan, Me.ToolStripButton_ZoomPrevious, Me.ToolStripSplitButton_Help})
        ToolStrip1.Location = New System.Drawing.Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New System.Drawing.Size(944, 34)
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
        Me.ToolStripSplitButton_Oeffnen.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ZeitreihenImportieren, Me.ToolStripMenuItem_ZeitreiheEingeben, ToolStripSeparator5, Me.ToolStripMenuItem_Refresh, ToolStripSeparator6, Me.ToolStripMenuItem_TENLaden, Me.ToolStripMenuItem_ThemeLaden})
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
        Me.ToolStripMenuItem_ZeitreihenImportieren.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem_ZeitreihenImportieren.Text = "Zeitreihe(n) importieren"
        '
        'ToolStripMenuItem_ZeitreiheEingeben
        '
        Me.ToolStripMenuItem_ZeitreiheEingeben.Image = Global.IHWB.Wave.My.Resources.Resources.chart_line_edit
        Me.ToolStripMenuItem_ZeitreiheEingeben.Name = "ToolStripMenuItem_ZeitreiheEingeben"
        Me.ToolStripMenuItem_ZeitreiheEingeben.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem_ZeitreiheEingeben.Text = "Zeitreihe eingeben"
        '
        'ToolStripSeparator5
        '
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New System.Drawing.Size(200, 6)
        '
        'ToolStripMenuItem_Refresh
        '
        Me.ToolStripMenuItem_Refresh.Image = Global.IHWB.Wave.My.Resources.Resources.arrow_refresh
        Me.ToolStripMenuItem_Refresh.Name = "ToolStripMenuItem_Refresh"
        Me.ToolStripMenuItem_Refresh.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem_Refresh.Text = "Dateien neu einlesen"
        '
        'ToolStripSeparator6
        '
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New System.Drawing.Size(200, 6)
        '
        'ToolStripMenuItem_TENLaden
        '
        Me.ToolStripMenuItem_TENLaden.Image = Global.IHWB.Wave.My.Resources.Resources.chart_curve
        Me.ToolStripMenuItem_TENLaden.Name = "ToolStripMenuItem_TENLaden"
        Me.ToolStripMenuItem_TENLaden.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem_TENLaden.Text = "Diagramm laden (*.TEN)"
        '
        'ToolStripMenuItem_ThemeLaden
        '
        Me.ToolStripMenuItem_ThemeLaden.Image = Global.IHWB.Wave.My.Resources.Resources.color_wheel
        Me.ToolStripMenuItem_ThemeLaden.Name = "ToolStripMenuItem_ThemeLaden"
        Me.ToolStripMenuItem_ThemeLaden.Size = New System.Drawing.Size(203, 22)
        Me.ToolStripMenuItem_ThemeLaden.Text = "Theme laden (*.XML)"
        '
        'ToolStripSplitButton_Speichern
        '
        Me.ToolStripSplitButton_Speichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripSplitButton_Speichern.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ExportDiagramm, Me.ToolStripMenuItem_ExportZeitreihe})
        Me.ToolStripSplitButton_Speichern.Image = CType(resources.GetObject("ToolStripSplitButton_Speichern.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton_Speichern.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton_Speichern.Name = "ToolStripSplitButton_Speichern"
        Me.ToolStripSplitButton_Speichern.Size = New System.Drawing.Size(32, 31)
        Me.ToolStripSplitButton_Speichern.Text = "&Speichern"
        '
        'ToolStripMenuItem_ExportDiagramm
        '
        Me.ToolStripMenuItem_ExportDiagramm.Image = Global.IHWB.Wave.My.Resources.Resources.chart_pie
        Me.ToolStripMenuItem_ExportDiagramm.Name = "ToolStripMenuItem_ExportDiagramm"
        Me.ToolStripMenuItem_ExportDiagramm.Size = New System.Drawing.Size(197, 22)
        Me.ToolStripMenuItem_ExportDiagramm.Text = "Diagramm exportieren"
        '
        'ToolStripMenuItem_ExportZeitreihe
        '
        Me.ToolStripMenuItem_ExportZeitreihe.Image = Global.IHWB.Wave.My.Resources.Resources.page_white_go
        Me.ToolStripMenuItem_ExportZeitreihe.Name = "ToolStripMenuItem_ExportZeitreihe"
        Me.ToolStripMenuItem_ExportZeitreihe.Size = New System.Drawing.Size(197, 22)
        Me.ToolStripMenuItem_ExportZeitreihe.Text = "Zeitreihe(n) exportieren"
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
        'ToolStripButton_NormalMode
        '
        Me.ToolStripButton_NormalMode.Checked = True
        Me.ToolStripButton_NormalMode.CheckOnClick = True
        Me.ToolStripButton_NormalMode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton_NormalMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_NormalMode.Image = CType(resources.GetObject("ToolStripButton_NormalMode.Image"), System.Drawing.Image)
        Me.ToolStripButton_NormalMode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_NormalMode.Name = "ToolStripButton_NormalMode"
        Me.ToolStripButton_NormalMode.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_NormalMode.Text = "Normal mode"
        Me.ToolStripButton_NormalMode.ToolTipText = "Normal mode (zoom with left mouse, pan with right mouse)"
        '
        'ToolStripButton_Zoom
        '
        Me.ToolStripButton_Zoom.CheckOnClick = True
        Me.ToolStripButton_Zoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Zoom.Image = Global.IHWB.Wave.My.Resources.Resources.zoom
        Me.ToolStripButton_Zoom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Zoom.Name = "ToolStripButton_Zoom"
        Me.ToolStripButton_Zoom.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Zoom.Text = "Zoom"
        Me.ToolStripButton_Zoom.ToolTipText = "Zoom (drag right to zoom, drag left to unzoom)"
        '
        'ToolStripButton_Pan
        '
        Me.ToolStripButton_Pan.CheckOnClick = True
        Me.ToolStripButton_Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Pan.Image = Global.IHWB.Wave.My.Resources.Resources.pan
        Me.ToolStripButton_Pan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Pan.Name = "ToolStripButton_Pan"
        Me.ToolStripButton_Pan.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Pan.Text = "Pan"
        '
        'ToolStripButton_ZoomPrevious
        '
        Me.ToolStripButton_ZoomPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ZoomPrevious.Image = Global.IHWB.Wave.My.Resources.Resources.zoom_previous
        Me.ToolStripButton_ZoomPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ZoomPrevious.Name = "ToolStripButton_ZoomPrevious"
        Me.ToolStripButton_ZoomPrevious.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ZoomPrevious.Text = "Zoom previous"
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
        Me.HilfeToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.HilfeToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Image = Global.IHWB.Wave.My.Resources.Resources.BlueM_icon
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem.Text = "About"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TChart1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel_Navigation)
        Me.SplitContainer1.Size = New System.Drawing.Size(944, 606)
        Me.SplitContainer1.SplitterDistance = 150
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
        Me.TChart2.Size = New System.Drawing.Size(940, 146)
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
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label_Navigate)
        Me.Panel1.Controls.Add(Me.Button_NavRight)
        Me.Panel1.Controls.Add(Me.Button_NavLeft)
        Me.Panel1.Controls.Add(Me.NumericUpDown_NavMultiplier)
        Me.Panel1.Controls.Add(Me.ComboBox_NavIncrement)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(940, 50)
        Me.Panel1.TabIndex = 2
        '
        'Label_Navigate
        '
        Me.Label_Navigate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label_Navigate.AutoSize = True
        Me.Label_Navigate.Location = New System.Drawing.Point(377, 12)
        Me.Label_Navigate.Name = "Label_Navigate"
        Me.Label_Navigate.Size = New System.Drawing.Size(53, 13)
        Me.Label_Navigate.TabIndex = 6
        Me.Label_Navigate.Text = "Navigate:"
        '
        'Button_NavRight
        '
        Me.Button_NavRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_NavRight.Location = New System.Drawing.Point(855, 9)
        Me.Button_NavRight.Name = "Button_NavRight"
        Me.Button_NavRight.Size = New System.Drawing.Size(75, 23)
        Me.Button_NavRight.TabIndex = 6
        Me.Button_NavRight.Text = "-->"
        Me.Button_NavRight.UseVisualStyleBackColor = True
        '
        'Button_NavLeft
        '
        Me.Button_NavLeft.Location = New System.Drawing.Point(11, 9)
        Me.Button_NavLeft.Name = "Button_NavLeft"
        Me.Button_NavLeft.Size = New System.Drawing.Size(75, 23)
        Me.Button_NavLeft.TabIndex = 6
        Me.Button_NavLeft.Text = "<---"
        Me.Button_NavLeft.UseVisualStyleBackColor = True
        '
        'NumericUpDown_NavMultiplier
        '
        Me.NumericUpDown_NavMultiplier.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.NumericUpDown_NavMultiplier.Location = New System.Drawing.Point(436, 10)
        Me.NumericUpDown_NavMultiplier.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_NavMultiplier.Name = "NumericUpDown_NavMultiplier"
        Me.NumericUpDown_NavMultiplier.Size = New System.Drawing.Size(40, 20)
        Me.NumericUpDown_NavMultiplier.TabIndex = 5
        Me.NumericUpDown_NavMultiplier.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ComboBox_NavIncrement
        '
        Me.ComboBox_NavIncrement.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBox_NavIncrement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_NavIncrement.FormattingEnabled = True
        Me.ComboBox_NavIncrement.Items.AddRange(New Object() {"Centuries", "Decades", "Years", "Months", "Weeks", "Days", "Hours", "Minutes", "Seconds"})
        Me.ComboBox_NavIncrement.Location = New System.Drawing.Point(482, 10)
        Me.ComboBox_NavIncrement.Name = "ComboBox_NavIncrement"
        Me.ComboBox_NavIncrement.Size = New System.Drawing.Size(81, 21)
        Me.ComboBox_NavIncrement.TabIndex = 2
        '
        'TChart1
        '
        Me.TChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.TChart1.Location = New System.Drawing.Point(0, 50)
        Me.TChart1.Margin = New System.Windows.Forms.Padding(0)
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
        Me.TChart1.Size = New System.Drawing.Size(940, 350)
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
        'Panel_Navigation
        '
        Me.Panel_Navigation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Navigation.Controls.Add(Me.Label_Display)
        Me.Panel_Navigation.Controls.Add(Me.NumericUpDown_DisplayRangeMultiplier)
        Me.Panel_Navigation.Controls.Add(Me.ComboBox_DisplayRangeUnit)
        Me.Panel_Navigation.Controls.Add(Me.DateTimePicker_NavEnd)
        Me.Panel_Navigation.Controls.Add(Me.DateTimePicker_NavStart)
        Me.Panel_Navigation.Location = New System.Drawing.Point(0, 400)
        Me.Panel_Navigation.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Navigation.Name = "Panel_Navigation"
        Me.Panel_Navigation.Size = New System.Drawing.Size(940, 50)
        Me.Panel_Navigation.TabIndex = 1
        '
        'Label_Display
        '
        Me.Label_Display.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label_Display.AutoSize = True
        Me.Label_Display.Location = New System.Drawing.Point(382, 18)
        Me.Label_Display.Name = "Label_Display"
        Me.Label_Display.Size = New System.Drawing.Size(44, 13)
        Me.Label_Display.TabIndex = 6
        Me.Label_Display.Text = "Display:"
        '
        'NumericUpDown_DisplayRangeMultiplier
        '
        Me.NumericUpDown_DisplayRangeMultiplier.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.NumericUpDown_DisplayRangeMultiplier.Location = New System.Drawing.Point(432, 15)
        Me.NumericUpDown_DisplayRangeMultiplier.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_DisplayRangeMultiplier.Name = "NumericUpDown_DisplayRangeMultiplier"
        Me.NumericUpDown_DisplayRangeMultiplier.Size = New System.Drawing.Size(40, 20)
        Me.NumericUpDown_DisplayRangeMultiplier.TabIndex = 5
        Me.NumericUpDown_DisplayRangeMultiplier.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ComboBox_DisplayRangeUnit
        '
        Me.ComboBox_DisplayRangeUnit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBox_DisplayRangeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_DisplayRangeUnit.FormattingEnabled = True
        Me.ComboBox_DisplayRangeUnit.Items.AddRange(New Object() {"Centuries", "Decades", "Years", "Months", "Weeks", "Days", "Hours", "Minutes", "Seconds"})
        Me.ComboBox_DisplayRangeUnit.Location = New System.Drawing.Point(478, 15)
        Me.ComboBox_DisplayRangeUnit.Name = "ComboBox_DisplayRangeUnit"
        Me.ComboBox_DisplayRangeUnit.Size = New System.Drawing.Size(81, 21)
        Me.ComboBox_DisplayRangeUnit.TabIndex = 2
        '
        'DateTimePicker_NavEnd
        '
        Me.DateTimePicker_NavEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker_NavEnd.CustomFormat = "dd.MM.yyyy HH:mm"
        Me.DateTimePicker_NavEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_NavEnd.Location = New System.Drawing.Point(820, 15)
        Me.DateTimePicker_NavEnd.Name = "DateTimePicker_NavEnd"
        Me.DateTimePicker_NavEnd.ShowUpDown = True
        Me.DateTimePicker_NavEnd.Size = New System.Drawing.Size(110, 20)
        Me.DateTimePicker_NavEnd.TabIndex = 1
        '
        'DateTimePicker_NavStart
        '
        Me.DateTimePicker_NavStart.CustomFormat = "dd.MM.yyyy HH:mm"
        Me.DateTimePicker_NavStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_NavStart.Location = New System.Drawing.Point(10, 15)
        Me.DateTimePicker_NavStart.Name = "DateTimePicker_NavStart"
        Me.DateTimePicker_NavStart.ShowUpDown = True
        Me.DateTimePicker_NavStart.Size = New System.Drawing.Size(110, 20)
        Me.DateTimePicker_NavStart.TabIndex = 0
        '
        'Wave
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(944, 661)
        Me.Controls.Add(ToolStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(500, 410)
        Me.Name = "Wave"
        Me.Text = "BlueM.Wave"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.NumericUpDown_NavMultiplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_Navigation.ResumeLayout(False)
        Me.Panel_Navigation.PerformLayout()
        CType(Me.NumericUpDown_DisplayRangeMultiplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TChart2 As Steema.TeeChart.TChart
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
    Friend WithEvents ToolStripMenuItem_TENLaden As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ExportDiagramm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ExportZeitreihe As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Refresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ZeitreiheEingeben As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ThemeLaden As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton_Zoom As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Pan As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ZoomPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_NormalMode As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel_Navigation As System.Windows.Forms.Panel
    Friend WithEvents NumericUpDown_DisplayRangeMultiplier As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox_DisplayRangeUnit As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePicker_NavEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker_NavStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents NumericUpDown_NavMultiplier As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox_NavIncrement As System.Windows.Forms.ComboBox
    Friend WithEvents Button_NavLeft As System.Windows.Forms.Button
    Friend WithEvents Button_NavRight As System.Windows.Forms.Button
    Friend WithEvents Label_Navigate As System.Windows.Forms.Label
    Friend WithEvents Label_Display As System.Windows.Forms.Label

End Class
