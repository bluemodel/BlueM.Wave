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
        Dim ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Me.ToolStripStatusLabel_Log = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripButton_New = New System.Windows.Forms.ToolStripButton
        Me.ToolStripDropDownButton_Open = New System.Windows.Forms.ToolStripDropDownButton
        Me.ToolStripMenuItem_ImportSeries = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_EnterSeries = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_LoadTEN = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_LoadTheme = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_ReloadFromFiles = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_RecentlyUsedFiles = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripDropDownButton_Save = New System.Windows.Forms.ToolStripDropDownButton
        Me.ToolStripMenuItem_SaveProjectFile = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_SaveChart = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_ExportSeries = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripButton_Copy = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Print = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_RenameSeries = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Cut = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Merge = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_ChangeTimestep = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_ShowNaNValues = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_ConvertErrorValues = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_Analysis = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Properties = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_EditChart = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_ToggleOverview = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_ToggleNavigation = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_NormalMode = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Zoom = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Pan = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton_ZoomPrevious = New System.Windows.Forms.ToolStripButton
        Me.ToolStripDropDownButton_Help = New System.Windows.Forms.ToolStripDropDownButton
        Me.HilfeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripDropDownButton_ZoomToSeries = New System.Windows.Forms.ToolStripDropDownButton
        Me.ToolStripButton_ZoomAll = New System.Windows.Forms.ToolStripButton
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.PanelNavigation = New System.Windows.Forms.Panel
        Me.Label_Navigate = New System.Windows.Forms.Label
        Me.Button_NavForward = New System.Windows.Forms.Button
        Me.Button_NavBack = New System.Windows.Forms.Button
        Me.NumericUpDown_NavMultiplier = New System.Windows.Forms.NumericUpDown
        Me.ComboBox_NavIncrement = New System.Windows.Forms.ComboBox
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.Panel_DisplayRange = New System.Windows.Forms.Panel
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
        ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        StatusStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PanelNavigation.SuspendLayout()
        CType(Me.NumericUpDown_NavMultiplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_DisplayRange.SuspendLayout()
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
        Me.ToolStripStatusLabel_Log.Image = Global.BlueM.Wave.My.Resources.Resources.script
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
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_New, Me.ToolStripDropDownButton_Open, Me.ToolStripDropDownButton_Save, ToolStripSeparator4, Me.ToolStripButton_Copy, Me.ToolStripButton_Print, ToolStripSeparator2, Me.ToolStripButton_RenameSeries, Me.ToolStripButton_Cut, Me.ToolStripButton_Merge, Me.ToolStripButton_ChangeTimestep, Me.ToolStripSeparator8, Me.ToolStripButton_ShowNaNValues, Me.ToolStripButton_ConvertErrorValues, Me.ToolStripSeparator7, Me.ToolStripButton_Analysis, Me.ToolStripButton_Properties, ToolStripSeparator1, Me.ToolStripButton_EditChart, Me.ToolStripButton_ToggleOverview, Me.ToolStripButton_ToggleNavigation, toolStripSeparator, Me.ToolStripButton_NormalMode, Me.ToolStripButton_Zoom, Me.ToolStripButton_Pan, Me.ToolStripSeparator3, Me.ToolStripButton_ZoomPrevious, Me.ToolStripDropDownButton_Help, Me.ToolStripDropDownButton_ZoomToSeries, Me.ToolStripButton_ZoomAll})
        ToolStrip1.Location = New System.Drawing.Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New System.Drawing.Size(944, 34)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton_New
        '
        Me.ToolStripButton_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_New.Image = CType(resources.GetObject("ToolStripButton_New.Image"), System.Drawing.Image)
        Me.ToolStripButton_New.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_New.Name = "ToolStripButton_New"
        Me.ToolStripButton_New.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_New.Text = "&New"
        '
        'ToolStripDropDownButton_Open
        '
        Me.ToolStripDropDownButton_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton_Open.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ImportSeries, Me.ToolStripMenuItem_EnterSeries, ToolStripSeparator6, Me.ToolStripMenuItem_LoadTEN, Me.ToolStripMenuItem_LoadTheme, ToolStripSeparator5, Me.ToolStripMenuItem_ReloadFromFiles, Me.ToolStripMenuItem_RecentlyUsedFiles})
        Me.ToolStripDropDownButton_Open.Image = CType(resources.GetObject("ToolStripDropDownButton_Open.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton_Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_Open.Name = "ToolStripDropDownButton_Open"
        Me.ToolStripDropDownButton_Open.Size = New System.Drawing.Size(29, 31)
        Me.ToolStripDropDownButton_Open.Text = "Open"
        '
        'ToolStripMenuItem_ImportSeries
        '
        Me.ToolStripMenuItem_ImportSeries.Image = Global.BlueM.Wave.My.Resources.Resources.chart_line_add
        Me.ToolStripMenuItem_ImportSeries.Name = "ToolStripMenuItem_ImportSeries"
        Me.ToolStripMenuItem_ImportSeries.Size = New System.Drawing.Size(180, 22)
        Me.ToolStripMenuItem_ImportSeries.Text = "Import time series"
        '
        'ToolStripMenuItem_EnterSeries
        '
        Me.ToolStripMenuItem_EnterSeries.Image = Global.BlueM.Wave.My.Resources.Resources.chart_line_edit
        Me.ToolStripMenuItem_EnterSeries.Name = "ToolStripMenuItem_EnterSeries"
        Me.ToolStripMenuItem_EnterSeries.Size = New System.Drawing.Size(180, 22)
        Me.ToolStripMenuItem_EnterSeries.Text = "Enter time series"
        '
        'ToolStripSeparator6
        '
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New System.Drawing.Size(177, 6)
        '
        'ToolStripMenuItem_LoadTEN
        '
        Me.ToolStripMenuItem_LoadTEN.Image = Global.BlueM.Wave.My.Resources.Resources.chart_curve
        Me.ToolStripMenuItem_LoadTEN.Name = "ToolStripMenuItem_LoadTEN"
        Me.ToolStripMenuItem_LoadTEN.Size = New System.Drawing.Size(180, 22)
        Me.ToolStripMenuItem_LoadTEN.Text = "Load chart (*.TEN)"
        '
        'ToolStripMenuItem_LoadTheme
        '
        Me.ToolStripMenuItem_LoadTheme.Image = Global.BlueM.Wave.My.Resources.Resources.color_wheel
        Me.ToolStripMenuItem_LoadTheme.Name = "ToolStripMenuItem_LoadTheme"
        Me.ToolStripMenuItem_LoadTheme.Size = New System.Drawing.Size(180, 22)
        Me.ToolStripMenuItem_LoadTheme.Text = "Load theme (*.XML)"
        '
        'ToolStripSeparator5
        '
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New System.Drawing.Size(177, 6)
        '
        'ToolStripMenuItem_ReloadFromFiles
        '
        Me.ToolStripMenuItem_ReloadFromFiles.Image = Global.BlueM.Wave.My.Resources.Resources.arrow_refresh
        Me.ToolStripMenuItem_ReloadFromFiles.Name = "ToolStripMenuItem_ReloadFromFiles"
        Me.ToolStripMenuItem_ReloadFromFiles.Size = New System.Drawing.Size(180, 22)
        Me.ToolStripMenuItem_ReloadFromFiles.Text = "Reload from files"
        '
        'ToolStripMenuItem_RecentlyUsedFiles
        '
        Me.ToolStripMenuItem_RecentlyUsedFiles.Name = "ToolStripMenuItem_RecentlyUsedFiles"
        Me.ToolStripMenuItem_RecentlyUsedFiles.Size = New System.Drawing.Size(180, 22)
        Me.ToolStripMenuItem_RecentlyUsedFiles.Text = "Recently used files"
        '
        'ToolStripDropDownButton_Save
        '
        Me.ToolStripDropDownButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton_Save.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_SaveProjectFile, Me.ToolStripMenuItem_SaveChart, Me.ToolStripMenuItem_ExportSeries})
        Me.ToolStripDropDownButton_Save.Image = CType(resources.GetObject("ToolStripDropDownButton_Save.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_Save.Name = "ToolStripDropDownButton_Save"
        Me.ToolStripDropDownButton_Save.Size = New System.Drawing.Size(29, 31)
        Me.ToolStripDropDownButton_Save.Text = "&Save"
        '
        'ToolStripMenuItem_SaveProjectFile
        '
        Me.ToolStripMenuItem_SaveProjectFile.Image = Global.BlueM.Wave.My.Resources.Resources.page_save
        Me.ToolStripMenuItem_SaveProjectFile.Name = "ToolStripMenuItem_SaveProjectFile"
        Me.ToolStripMenuItem_SaveProjectFile.Size = New System.Drawing.Size(166, 22)
        Me.ToolStripMenuItem_SaveProjectFile.Text = "Save project file"
        '
        'ToolStripMenuItem_SaveChart
        '
        Me.ToolStripMenuItem_SaveChart.Image = Global.BlueM.Wave.My.Resources.Resources.chart_pie
        Me.ToolStripMenuItem_SaveChart.Name = "ToolStripMenuItem_SaveChart"
        Me.ToolStripMenuItem_SaveChart.Size = New System.Drawing.Size(166, 22)
        Me.ToolStripMenuItem_SaveChart.Text = "Save chart"
        '
        'ToolStripMenuItem_ExportSeries
        '
        Me.ToolStripMenuItem_ExportSeries.Image = Global.BlueM.Wave.My.Resources.Resources.page_white_go
        Me.ToolStripMenuItem_ExportSeries.Name = "ToolStripMenuItem_ExportSeries"
        Me.ToolStripMenuItem_ExportSeries.Size = New System.Drawing.Size(166, 22)
        Me.ToolStripMenuItem_ExportSeries.Text = "Export time series"
        '
        'ToolStripSeparator4
        '
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_Copy
        '
        Me.ToolStripButton_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Copy.Image = CType(resources.GetObject("ToolStripButton_Copy.Image"), System.Drawing.Image)
        Me.ToolStripButton_Copy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Copy.Name = "ToolStripButton_Copy"
        Me.ToolStripButton_Copy.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Copy.Text = "&Copy chart to clipboard (PNG)"
        '
        'ToolStripButton_Print
        '
        Me.ToolStripButton_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Print.Image = CType(resources.GetObject("ToolStripButton_Print.Image"), System.Drawing.Image)
        Me.ToolStripButton_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Print.Name = "ToolStripButton_Print"
        Me.ToolStripButton_Print.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Print.Text = "&Print"
        '
        'ToolStripButton_RenameSeries
        '
        Me.ToolStripButton_RenameSeries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_RenameSeries.Image = CType(resources.GetObject("ToolStripButton_RenameSeries.Image"), System.Drawing.Image)
        Me.ToolStripButton_RenameSeries.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_RenameSeries.Name = "ToolStripButton_RenameSeries"
        Me.ToolStripButton_RenameSeries.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_RenameSeries.Text = "Rename Series"
        Me.ToolStripButton_RenameSeries.ToolTipText = "Rename time series"
        '
        'ToolStripButton_Cut
        '
        Me.ToolStripButton_Cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Cut.Image = Global.BlueM.Wave.My.Resources.Resources.cut
        Me.ToolStripButton_Cut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Cut.Name = "ToolStripButton_Cut"
        Me.ToolStripButton_Cut.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Cut.Text = "Cut time series"
        '
        'ToolStripButton_Merge
        '
        Me.ToolStripButton_Merge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Merge.Image = Global.BlueM.Wave.My.Resources.Resources.arrow_join_right
        Me.ToolStripButton_Merge.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Merge.Name = "ToolStripButton_Merge"
        Me.ToolStripButton_Merge.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Merge.Text = "Merge"
        Me.ToolStripButton_Merge.ToolTipText = "Merge time series"
        '
        'ToolStripButton_ChangeTimestep
        '
        Me.ToolStripButton_ChangeTimestep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ChangeTimestep.Image = Global.BlueM.Wave.My.Resources.Resources.chart_bar_edit
        Me.ToolStripButton_ChangeTimestep.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ChangeTimestep.Name = "ToolStripButton_ChangeTimestep"
        Me.ToolStripButton_ChangeTimestep.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ChangeTimestep.Text = "Change timestep"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_ShowNaNValues
        '
        Me.ToolStripButton_ShowNaNValues.CheckOnClick = True
        Me.ToolStripButton_ShowNaNValues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ShowNaNValues.Image = Global.BlueM.Wave.My.Resources.Resources.chart_line_error
        Me.ToolStripButton_ShowNaNValues.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ShowNaNValues.Name = "ToolStripButton_ShowNaNValues"
        Me.ToolStripButton_ShowNaNValues.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ShowNaNValues.Text = "Visualize NaN values for the currently active series"
        '
        'ToolStripButton_ConvertErrorValues
        '
        Me.ToolStripButton_ConvertErrorValues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ConvertErrorValues.Image = CType(resources.GetObject("ToolStripButton_ConvertErrorValues.Image"), System.Drawing.Image)
        Me.ToolStripButton_ConvertErrorValues.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ConvertErrorValues.Name = "ToolStripButton_ConvertErrorValues"
        Me.ToolStripButton_ConvertErrorValues.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ConvertErrorValues.Text = "Convert error values"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_Analysis
        '
        Me.ToolStripButton_Analysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Analysis.Image = Global.BlueM.Wave.My.Resources.Resources.calculator
        Me.ToolStripButton_Analysis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Analysis.Name = "ToolStripButton_Analysis"
        Me.ToolStripButton_Analysis.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Analysis.Text = "Perform an analysis"
        '
        'ToolStripButton_Properties
        '
        Me.ToolStripButton_Properties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Properties.Image = Global.BlueM.Wave.My.Resources.Resources.table
        Me.ToolStripButton_Properties.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Properties.Name = "ToolStripButton_Properties"
        Me.ToolStripButton_Properties.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Properties.Text = "Properties"
        Me.ToolStripButton_Properties.ToolTipText = "Show time series properties"
        '
        'ToolStripButton_EditChart
        '
        Me.ToolStripButton_EditChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_EditChart.Image = Global.BlueM.Wave.My.Resources.Resources.chart_curve_edit
        Me.ToolStripButton_EditChart.ImageTransparentColor = System.Drawing.SystemColors.Control
        Me.ToolStripButton_EditChart.Name = "ToolStripButton_EditChart"
        Me.ToolStripButton_EditChart.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_EditChart.Text = "Edit chart"
        '
        'ToolStripButton_ToggleOverview
        '
        Me.ToolStripButton_ToggleOverview.Checked = True
        Me.ToolStripButton_ToggleOverview.CheckOnClick = True
        Me.ToolStripButton_ToggleOverview.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton_ToggleOverview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ToggleOverview.Image = Global.BlueM.Wave.My.Resources.Resources.application_split
        Me.ToolStripButton_ToggleOverview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ToggleOverview.Name = "ToolStripButton_ToggleOverview"
        Me.ToolStripButton_ToggleOverview.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ToggleOverview.Text = "Toggle overview"
        Me.ToolStripButton_ToggleOverview.ToolTipText = "Toggle overview"
        '
        'ToolStripButton_ToggleNavigation
        '
        Me.ToolStripButton_ToggleNavigation.Checked = True
        Me.ToolStripButton_ToggleNavigation.CheckOnClick = True
        Me.ToolStripButton_ToggleNavigation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripButton_ToggleNavigation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ToggleNavigation.Image = CType(resources.GetObject("ToolStripButton_ToggleNavigation.Image"), System.Drawing.Image)
        Me.ToolStripButton_ToggleNavigation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ToggleNavigation.Name = "ToolStripButton_ToggleNavigation"
        Me.ToolStripButton_ToggleNavigation.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ToggleNavigation.Text = "Toggle navigation controls"
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
        Me.ToolStripButton_Zoom.Image = Global.BlueM.Wave.My.Resources.Resources.zoom
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
        Me.ToolStripButton_Pan.Image = Global.BlueM.Wave.My.Resources.Resources.pan
        Me.ToolStripButton_Pan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Pan.Name = "ToolStripButton_Pan"
        Me.ToolStripButton_Pan.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Pan.Text = "Pan"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_ZoomPrevious
        '
        Me.ToolStripButton_ZoomPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ZoomPrevious.Image = Global.BlueM.Wave.My.Resources.Resources.zoom_previous
        Me.ToolStripButton_ZoomPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ZoomPrevious.Name = "ToolStripButton_ZoomPrevious"
        Me.ToolStripButton_ZoomPrevious.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ZoomPrevious.Text = "Zoom previous"
        '
        'ToolStripDropDownButton_Help
        '
        Me.ToolStripDropDownButton_Help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripDropDownButton_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton_Help.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HilfeToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.ToolStripDropDownButton_Help.Image = Global.BlueM.Wave.My.Resources.Resources.help
        Me.ToolStripDropDownButton_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_Help.Name = "ToolStripDropDownButton_Help"
        Me.ToolStripDropDownButton_Help.Size = New System.Drawing.Size(29, 31)
        Me.ToolStripDropDownButton_Help.Text = "Help"
        '
        'HilfeToolStripMenuItem
        '
        Me.HilfeToolStripMenuItem.Name = "HilfeToolStripMenuItem"
        Me.HilfeToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.HilfeToolStripMenuItem.Text = "Help (wiki)"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Image = Global.BlueM.Wave.My.Resources.Resources.BlueM_icon
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ToolStripDropDownButton_ZoomToSeries
        '
        Me.ToolStripDropDownButton_ZoomToSeries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton_ZoomToSeries.Image = Global.BlueM.Wave.My.Resources.Resources.chart_magnify
        Me.ToolStripDropDownButton_ZoomToSeries.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_ZoomToSeries.Name = "ToolStripDropDownButton_ZoomToSeries"
        Me.ToolStripDropDownButton_ZoomToSeries.Size = New System.Drawing.Size(29, 31)
        Me.ToolStripDropDownButton_ZoomToSeries.Text = "Zoom to timeseries"
        '
        'ToolStripButton_ZoomAll
        '
        Me.ToolStripButton_ZoomAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ZoomAll.Image = CType(resources.GetObject("ToolStripButton_ZoomAll.Image"), System.Drawing.Image)
        Me.ToolStripButton_ZoomAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ZoomAll.Name = "ToolStripButton_ZoomAll"
        Me.ToolStripButton_ZoomAll.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ZoomAll.Text = "Zoom all"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel1)
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelNavigation, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TChart1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel_DisplayRange, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(940, 448)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'PanelNavigation
        '
        Me.PanelNavigation.Controls.Add(Me.Label_Navigate)
        Me.PanelNavigation.Controls.Add(Me.Button_NavForward)
        Me.PanelNavigation.Controls.Add(Me.Button_NavBack)
        Me.PanelNavigation.Controls.Add(Me.NumericUpDown_NavMultiplier)
        Me.PanelNavigation.Controls.Add(Me.ComboBox_NavIncrement)
        Me.PanelNavigation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelNavigation.Location = New System.Drawing.Point(1, 1)
        Me.PanelNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelNavigation.Name = "PanelNavigation"
        Me.PanelNavigation.Size = New System.Drawing.Size(938, 38)
        Me.PanelNavigation.TabIndex = 2
        '
        'Label_Navigate
        '
        Me.Label_Navigate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label_Navigate.AutoSize = True
        Me.Label_Navigate.Location = New System.Drawing.Point(376, 13)
        Me.Label_Navigate.Name = "Label_Navigate"
        Me.Label_Navigate.Size = New System.Drawing.Size(53, 13)
        Me.Label_Navigate.TabIndex = 6
        Me.Label_Navigate.Text = "Navigate:"
        '
        'Button_NavForward
        '
        Me.Button_NavForward.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_NavForward.Image = CType(resources.GetObject("Button_NavForward.Image"), System.Drawing.Image)
        Me.Button_NavForward.Location = New System.Drawing.Point(854, 8)
        Me.Button_NavForward.Name = "Button_NavForward"
        Me.Button_NavForward.Size = New System.Drawing.Size(75, 23)
        Me.Button_NavForward.TabIndex = 6
        Me.Button_NavForward.UseVisualStyleBackColor = True
        '
        'Button_NavBack
        '
        Me.Button_NavBack.Image = CType(resources.GetObject("Button_NavBack.Image"), System.Drawing.Image)
        Me.Button_NavBack.Location = New System.Drawing.Point(8, 8)
        Me.Button_NavBack.Name = "Button_NavBack"
        Me.Button_NavBack.Size = New System.Drawing.Size(75, 23)
        Me.Button_NavBack.TabIndex = 6
        Me.Button_NavBack.UseVisualStyleBackColor = True
        '
        'NumericUpDown_NavMultiplier
        '
        Me.NumericUpDown_NavMultiplier.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.NumericUpDown_NavMultiplier.Location = New System.Drawing.Point(435, 9)
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
        Me.ComboBox_NavIncrement.Location = New System.Drawing.Point(481, 9)
        Me.ComboBox_NavIncrement.Name = "ComboBox_NavIncrement"
        Me.ComboBox_NavIncrement.Size = New System.Drawing.Size(81, 21)
        Me.ComboBox_NavIncrement.TabIndex = 2
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
        Me.TChart1.Location = New System.Drawing.Point(1, 40)
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
        Me.TChart1.Size = New System.Drawing.Size(938, 370)
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
        'Panel_DisplayRange
        '
        Me.Panel_DisplayRange.Controls.Add(Me.Label_Display)
        Me.Panel_DisplayRange.Controls.Add(Me.NumericUpDown_DisplayRangeMultiplier)
        Me.Panel_DisplayRange.Controls.Add(Me.ComboBox_DisplayRangeUnit)
        Me.Panel_DisplayRange.Controls.Add(Me.DateTimePicker_NavEnd)
        Me.Panel_DisplayRange.Controls.Add(Me.DateTimePicker_NavStart)
        Me.Panel_DisplayRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_DisplayRange.Location = New System.Drawing.Point(1, 411)
        Me.Panel_DisplayRange.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_DisplayRange.Name = "Panel_DisplayRange"
        Me.Panel_DisplayRange.Size = New System.Drawing.Size(938, 36)
        Me.Panel_DisplayRange.TabIndex = 1
        '
        'Label_Display
        '
        Me.Label_Display.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label_Display.AutoSize = True
        Me.Label_Display.Location = New System.Drawing.Point(381, 12)
        Me.Label_Display.Name = "Label_Display"
        Me.Label_Display.Size = New System.Drawing.Size(44, 13)
        Me.Label_Display.TabIndex = 6
        Me.Label_Display.Text = "Display:"
        '
        'NumericUpDown_DisplayRangeMultiplier
        '
        Me.NumericUpDown_DisplayRangeMultiplier.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.NumericUpDown_DisplayRangeMultiplier.Location = New System.Drawing.Point(431, 8)
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
        Me.ComboBox_DisplayRangeUnit.Items.AddRange(New Object() {"", "Centuries", "Decades", "Years", "Months", "Weeks", "Days", "Hours", "Minutes", "Seconds"})
        Me.ComboBox_DisplayRangeUnit.Location = New System.Drawing.Point(477, 8)
        Me.ComboBox_DisplayRangeUnit.Name = "ComboBox_DisplayRangeUnit"
        Me.ComboBox_DisplayRangeUnit.Size = New System.Drawing.Size(81, 21)
        Me.ComboBox_DisplayRangeUnit.TabIndex = 2
        '
        'DateTimePicker_NavEnd
        '
        Me.DateTimePicker_NavEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker_NavEnd.CustomFormat = "dd.MM.yyyy HH:mm"
        Me.DateTimePicker_NavEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_NavEnd.Location = New System.Drawing.Point(819, 8)
        Me.DateTimePicker_NavEnd.Name = "DateTimePicker_NavEnd"
        Me.DateTimePicker_NavEnd.ShowUpDown = True
        Me.DateTimePicker_NavEnd.Size = New System.Drawing.Size(110, 20)
        Me.DateTimePicker_NavEnd.TabIndex = 1
        '
        'DateTimePicker_NavStart
        '
        Me.DateTimePicker_NavStart.CustomFormat = "dd.MM.yyyy HH:mm"
        Me.DateTimePicker_NavStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_NavStart.Location = New System.Drawing.Point(8, 8)
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
        Me.KeyPreview = True
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
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.PanelNavigation.ResumeLayout(False)
        Me.PanelNavigation.PerformLayout()
        CType(Me.NumericUpDown_NavMultiplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_DisplayRange.ResumeLayout(False)
        Me.Panel_DisplayRange.PerformLayout()
        CType(Me.NumericUpDown_DisplayRangeMultiplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TChart2 As Steema.TeeChart.TChart
    Friend WithEvents ToolStripButton_New As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton_Open As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripDropDownButton_Save As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripButton_Print As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Copy As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_EditChart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ToggleOverview As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripButton_Analysis As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripStatusLabel_Log As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripButton_Cut As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton_Help As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents HilfeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ImportSeries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_LoadTEN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_SaveChart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ExportSeries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ReloadFromFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_EnterSeries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_LoadTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton_Zoom As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Pan As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ZoomPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_NormalMode As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel_DisplayRange As System.Windows.Forms.Panel
    Friend WithEvents NumericUpDown_DisplayRangeMultiplier As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox_DisplayRangeUnit As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePicker_NavEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker_NavStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents PanelNavigation As System.Windows.Forms.Panel
    Friend WithEvents NumericUpDown_NavMultiplier As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox_NavIncrement As System.Windows.Forms.ComboBox
    Friend WithEvents Button_NavBack As System.Windows.Forms.Button
    Friend WithEvents Button_NavForward As System.Windows.Forms.Button
    Friend WithEvents Label_Navigate As System.Windows.Forms.Label
    Friend WithEvents Label_Display As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton_ZoomAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ToolStripButton_ToggleNavigation As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ConvertErrorValues As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_RenameSeries As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton_ZoomToSeries As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem_SaveProjectFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton_Properties As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripMenuItem_RecentlyUsedFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton_ChangeTimestep As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Merge As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ShowNaNValues As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator

End Class
