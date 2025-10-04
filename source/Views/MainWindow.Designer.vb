<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
        Dim toolStripSeparator As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
        Dim StatusStrip1 As System.Windows.Forms.StatusStrip
        Dim ToolStrip1 As System.Windows.Forms.ToolStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Dim ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
        Dim ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
        Dim Margins1 As Steema.TeeChart.Margins = New Steema.TeeChart.Margins()
        Dim Margins2 As Steema.TeeChart.Margins = New Steema.TeeChart.Margins()
        Me.ToolStripStatusLabel_Errors = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel_Warnings = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel_Log = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripButton_New = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton_Open = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem_ImportSeries = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_EnterSeries = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_PasteFromClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_LoadTEN = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ReloadFromFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_RecentlyUsedFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripDropDownButton_Save = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem_SaveProjectFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_SaveChart = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ExportSeries = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton_Settings = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton_Copy = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_Print = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_Cut = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_Merge = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton_ShowNaNValues = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_ConvertErrorValues = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_RemoveNaNValues = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton_Analysis = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_EditChart = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton_ColorPalette = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem_ColorPaletteMaterial = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ColorPaletteDistinct = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ColorPaletteWheel = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_ColorPaletteRandom = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton_Properties = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_TimeseriesValues = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton_AxisDialog = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_AutoAdjustYAxes = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_Crosshair = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_ToggleOverview = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_ToggleNavigation = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_ZoomIn = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_ZoomOut = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_ZoomPrevious = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton_Help = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem_Help = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Changelog = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_CheckForUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_About = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton_ZoomNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton_ZoomToSeries = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripButton_ZoomAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_UpdateNotification = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripDropDownButton_ActivateAllSeries = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem_ActivateAllSeries = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_DeactivateAllSeries = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TChart2 = New Steema.TeeChart.TChart()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelNavigation = New System.Windows.Forms.Panel()
        Me.Button_NavEnd = New System.Windows.Forms.Button()
        Me.Button_NavStart = New System.Windows.Forms.Button()
        Me.Label_Navigate = New System.Windows.Forms.Label()
        Me.Button_NavForward = New System.Windows.Forms.Button()
        Me.Button_NavBack = New System.Windows.Forms.Button()
        Me.NumericUpDown_NavMultiplier = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox_NavIncrement = New System.Windows.Forms.ComboBox()
        Me.TChart1 = New Steema.TeeChart.TChart()
        Me.Panel_DisplayRange = New System.Windows.Forms.Panel()
        Me.Label_Display = New System.Windows.Forms.Label()
        Me.NumericUpDown_DisplayRangeMultiplier = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox_DisplayRangeUnit = New System.Windows.Forms.ComboBox()
        Me.MaskedTextBox_NavEnd = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox_NavStart = New System.Windows.Forms.MaskedTextBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        StatusStrip1 = New System.Windows.Forms.StatusStrip()
        ToolStrip1 = New System.Windows.Forms.ToolStrip()
        ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        StatusStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel_Errors, Me.ToolStripStatusLabel_Warnings, Me.ToolStripStatusLabel_Log})
        StatusStrip1.Location = New System.Drawing.Point(0, 639)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No
        StatusStrip1.ShowItemToolTips = True
        StatusStrip1.Size = New System.Drawing.Size(944, 22)
        StatusStrip1.SizingGrip = False
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel_Errors
        '
        Me.ToolStripStatusLabel_Errors.Image = Global.BlueM.Wave.My.Resources.Resources.cancel_inactive
        Me.ToolStripStatusLabel_Errors.IsLink = True
        Me.ToolStripStatusLabel_Errors.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ToolStripStatusLabel_Errors.LinkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel_Errors.Name = "ToolStripStatusLabel_Errors"
        Me.ToolStripStatusLabel_Errors.Size = New System.Drawing.Size(29, 17)
        Me.ToolStripStatusLabel_Errors.Text = "0"
        Me.ToolStripStatusLabel_Errors.ToolTipText = "Errors"
        '
        'ToolStripStatusLabel_Warnings
        '
        Me.ToolStripStatusLabel_Warnings.Image = Global.BlueM.Wave.My.Resources.Resources.warning_inactive
        Me.ToolStripStatusLabel_Warnings.IsLink = True
        Me.ToolStripStatusLabel_Warnings.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ToolStripStatusLabel_Warnings.LinkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel_Warnings.Name = "ToolStripStatusLabel_Warnings"
        Me.ToolStripStatusLabel_Warnings.Size = New System.Drawing.Size(29, 17)
        Me.ToolStripStatusLabel_Warnings.Text = "0"
        Me.ToolStripStatusLabel_Warnings.ToolTipText = "Warnings"
        '
        'ToolStripStatusLabel_Log
        '
        Me.ToolStripStatusLabel_Log.ActiveLinkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel_Log.IsLink = True
        Me.ToolStripStatusLabel_Log.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ToolStripStatusLabel_Log.LinkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripStatusLabel_Log.Margin = New System.Windows.Forms.Padding(3, 3, 0, 2)
        Me.ToolStripStatusLabel_Log.Name = "ToolStripStatusLabel_Log"
        Me.ToolStripStatusLabel_Log.Size = New System.Drawing.Size(0, 17)
        Me.ToolStripStatusLabel_Log.ToolTipText = "Show log"
        Me.ToolStripStatusLabel_Log.VisitedLinkColor = System.Drawing.SystemColors.ControlDarkDark
        '
        'ToolStrip1
        '
        ToolStrip1.AutoSize = False
        ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_New, Me.ToolStripDropDownButton_Open, Me.ToolStripDropDownButton_Save, ToolStripSeparator4, Me.ToolStripButton_Settings, Me.ToolStripSeparator12, Me.ToolStripButton_Copy, Me.ToolStripButton_Print, ToolStripSeparator2, Me.ToolStripButton_Cut, Me.ToolStripButton_Merge, Me.ToolStripSeparator8, Me.ToolStripButton_ShowNaNValues, Me.ToolStripButton_ConvertErrorValues, Me.ToolStripButton_RemoveNaNValues, Me.ToolStripSeparator7, Me.ToolStripButton_Analysis, ToolStripSeparator1, Me.ToolStripButton_EditChart, Me.ToolStripDropDownButton_ColorPalette, Me.ToolStripSeparator9, Me.ToolStripButton_Properties, Me.ToolStripButton_TimeseriesValues, Me.ToolStripSeparator10, Me.ToolStripButton_AxisDialog, Me.ToolStripButton_AutoAdjustYAxes, Me.ToolStripButton_Crosshair, ToolStripSeparator3, Me.ToolStripButton_ToggleOverview, Me.ToolStripButton_ToggleNavigation, toolStripSeparator, Me.ToolStripButton_ZoomIn, Me.ToolStripButton_ZoomOut, Me.ToolStripButton_ZoomPrevious, Me.ToolStripDropDownButton_Help, Me.ToolStripButton_ZoomNext, Me.ToolStripDropDownButton_ZoomToSeries, Me.ToolStripButton_ZoomAll, Me.ToolStripButton_UpdateNotification, Me.ToolStripSeparator11, Me.ToolStripDropDownButton_ActivateAllSeries})
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
        Me.ToolStripDropDownButton_Open.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ImportSeries, Me.ToolStripMenuItem_EnterSeries, Me.ToolStripMenuItem_PasteFromClipboard, ToolStripSeparator6, Me.ToolStripMenuItem_LoadTEN, ToolStripSeparator5, Me.ToolStripMenuItem_ReloadFromFiles, Me.ToolStripMenuItem_RecentlyUsedFiles})
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
        Me.ToolStripMenuItem_ImportSeries.Size = New System.Drawing.Size(231, 22)
        Me.ToolStripMenuItem_ImportSeries.Text = "Import time series"
        '
        'ToolStripMenuItem_EnterSeries
        '
        Me.ToolStripMenuItem_EnterSeries.Image = Global.BlueM.Wave.My.Resources.Resources.chart_line_edit
        Me.ToolStripMenuItem_EnterSeries.Name = "ToolStripMenuItem_EnterSeries"
        Me.ToolStripMenuItem_EnterSeries.Size = New System.Drawing.Size(231, 22)
        Me.ToolStripMenuItem_EnterSeries.Text = "Enter time series"
        '
        'ToolStripMenuItem_PasteFromClipboard
        '
        Me.ToolStripMenuItem_PasteFromClipboard.Image = Global.BlueM.Wave.My.Resources.Resources.page_paste
        Me.ToolStripMenuItem_PasteFromClipboard.Name = "ToolStripMenuItem_PasteFromClipboard"
        Me.ToolStripMenuItem_PasteFromClipboard.Size = New System.Drawing.Size(231, 22)
        Me.ToolStripMenuItem_PasteFromClipboard.Text = "Paste from Clipboard (Ctrl+V)"
        '
        'ToolStripSeparator6
        '
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New System.Drawing.Size(228, 6)
        '
        'ToolStripMenuItem_LoadTEN
        '
        Me.ToolStripMenuItem_LoadTEN.Image = Global.BlueM.Wave.My.Resources.Resources.chart_curve
        Me.ToolStripMenuItem_LoadTEN.Name = "ToolStripMenuItem_LoadTEN"
        Me.ToolStripMenuItem_LoadTEN.Size = New System.Drawing.Size(231, 22)
        Me.ToolStripMenuItem_LoadTEN.Text = "Load chart (*.TEN)"
        '
        'ToolStripSeparator5
        '
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New System.Drawing.Size(228, 6)
        '
        'ToolStripMenuItem_ReloadFromFiles
        '
        Me.ToolStripMenuItem_ReloadFromFiles.Image = Global.BlueM.Wave.My.Resources.Resources.arrow_refresh
        Me.ToolStripMenuItem_ReloadFromFiles.Name = "ToolStripMenuItem_ReloadFromFiles"
        Me.ToolStripMenuItem_ReloadFromFiles.Size = New System.Drawing.Size(231, 22)
        Me.ToolStripMenuItem_ReloadFromFiles.Text = "Reload from files"
        '
        'ToolStripMenuItem_RecentlyUsedFiles
        '
        Me.ToolStripMenuItem_RecentlyUsedFiles.Name = "ToolStripMenuItem_RecentlyUsedFiles"
        Me.ToolStripMenuItem_RecentlyUsedFiles.Size = New System.Drawing.Size(231, 22)
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
        Me.ToolStripMenuItem_SaveProjectFile.Image = Global.BlueM.Wave.My.Resources.Resources.chart_curve_link
        Me.ToolStripMenuItem_SaveProjectFile.Name = "ToolStripMenuItem_SaveProjectFile"
        Me.ToolStripMenuItem_SaveProjectFile.Size = New System.Drawing.Size(167, 22)
        Me.ToolStripMenuItem_SaveProjectFile.Text = "Save project file"
        '
        'ToolStripMenuItem_SaveChart
        '
        Me.ToolStripMenuItem_SaveChart.Image = Global.BlueM.Wave.My.Resources.Resources.chart_pie
        Me.ToolStripMenuItem_SaveChart.Name = "ToolStripMenuItem_SaveChart"
        Me.ToolStripMenuItem_SaveChart.Size = New System.Drawing.Size(167, 22)
        Me.ToolStripMenuItem_SaveChart.Text = "Save chart"
        '
        'ToolStripMenuItem_ExportSeries
        '
        Me.ToolStripMenuItem_ExportSeries.Image = Global.BlueM.Wave.My.Resources.Resources.page_white_go
        Me.ToolStripMenuItem_ExportSeries.Name = "ToolStripMenuItem_ExportSeries"
        Me.ToolStripMenuItem_ExportSeries.Size = New System.Drawing.Size(167, 22)
        Me.ToolStripMenuItem_ExportSeries.Text = "Export time series"
        '
        'ToolStripSeparator4
        '
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_Settings
        '
        Me.ToolStripButton_Settings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Settings.Image = Global.BlueM.Wave.My.Resources.Resources.cog
        Me.ToolStripButton_Settings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Settings.Name = "ToolStripButton_Settings"
        Me.ToolStripButton_Settings.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Settings.Text = "Settings"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 34)
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
        Me.ToolStripButton_ConvertErrorValues.Image = Global.BlueM.Wave.My.Resources.Resources.error_convert
        Me.ToolStripButton_ConvertErrorValues.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ConvertErrorValues.Name = "ToolStripButton_ConvertErrorValues"
        Me.ToolStripButton_ConvertErrorValues.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ConvertErrorValues.Text = "Convert error values"
        '
        'ToolStripButton_RemoveNaNValues
        '
        Me.ToolStripButton_RemoveNaNValues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_RemoveNaNValues.Image = Global.BlueM.Wave.My.Resources.Resources.error_delete
        Me.ToolStripButton_RemoveNaNValues.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_RemoveNaNValues.Name = "ToolStripButton_RemoveNaNValues"
        Me.ToolStripButton_RemoveNaNValues.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_RemoveNaNValues.Text = "Remove nodes with NaN values"
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
        'ToolStripButton_EditChart
        '
        Me.ToolStripButton_EditChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_EditChart.Image = Global.BlueM.Wave.My.Resources.Resources.chart_curve_edit
        Me.ToolStripButton_EditChart.ImageTransparentColor = System.Drawing.SystemColors.Control
        Me.ToolStripButton_EditChart.Name = "ToolStripButton_EditChart"
        Me.ToolStripButton_EditChart.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_EditChart.Text = "Edit chart"
        '
        'ToolStripDropDownButton_ColorPalette
        '
        Me.ToolStripDropDownButton_ColorPalette.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton_ColorPalette.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ColorPaletteMaterial, Me.ToolStripMenuItem_ColorPaletteDistinct, Me.ToolStripMenuItem_ColorPaletteWheel, Me.ToolStripMenuItem_ColorPaletteRandom})
        Me.ToolStripDropDownButton_ColorPalette.Image = Global.BlueM.Wave.My.Resources.Resources.color_wheel
        Me.ToolStripDropDownButton_ColorPalette.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_ColorPalette.Name = "ToolStripDropDownButton_ColorPalette"
        Me.ToolStripDropDownButton_ColorPalette.Size = New System.Drawing.Size(29, 31)
        Me.ToolStripDropDownButton_ColorPalette.Text = "Set color palette"
        '
        'ToolStripMenuItem_ColorPaletteMaterial
        '
        Me.ToolStripMenuItem_ColorPaletteMaterial.Name = "ToolStripMenuItem_ColorPaletteMaterial"
        Me.ToolStripMenuItem_ColorPaletteMaterial.Size = New System.Drawing.Size(139, 22)
        Me.ToolStripMenuItem_ColorPaletteMaterial.Text = "Material"
        '
        'ToolStripMenuItem_ColorPaletteDistinct
        '
        Me.ToolStripMenuItem_ColorPaletteDistinct.Name = "ToolStripMenuItem_ColorPaletteDistinct"
        Me.ToolStripMenuItem_ColorPaletteDistinct.Size = New System.Drawing.Size(139, 22)
        Me.ToolStripMenuItem_ColorPaletteDistinct.Text = "Distinct"
        '
        'ToolStripMenuItem_ColorPaletteWheel
        '
        Me.ToolStripMenuItem_ColorPaletteWheel.Name = "ToolStripMenuItem_ColorPaletteWheel"
        Me.ToolStripMenuItem_ColorPaletteWheel.Size = New System.Drawing.Size(139, 22)
        Me.ToolStripMenuItem_ColorPaletteWheel.Text = "Color Wheel"
        '
        'ToolStripMenuItem_ColorPaletteRandom
        '
        Me.ToolStripMenuItem_ColorPaletteRandom.Name = "ToolStripMenuItem_ColorPaletteRandom"
        Me.ToolStripMenuItem_ColorPaletteRandom.Size = New System.Drawing.Size(139, 22)
        Me.ToolStripMenuItem_ColorPaletteRandom.Text = "Random"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_Properties
        '
        Me.ToolStripButton_Properties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Properties.Image = Global.BlueM.Wave.My.Resources.Resources.table_edit
        Me.ToolStripButton_Properties.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Properties.Name = "ToolStripButton_Properties"
        Me.ToolStripButton_Properties.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Properties.Text = "Properties"
        Me.ToolStripButton_Properties.ToolTipText = "Show time series properties"
        '
        'ToolStripButton_TimeseriesValues
        '
        Me.ToolStripButton_TimeseriesValues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_TimeseriesValues.Image = Global.BlueM.Wave.My.Resources.Resources.table
        Me.ToolStripButton_TimeseriesValues.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_TimeseriesValues.Name = "ToolStripButton_TimeseriesValues"
        Me.ToolStripButton_TimeseriesValues.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_TimeseriesValues.Text = "Values"
        Me.ToolStripButton_TimeseriesValues.ToolTipText = "Show time series values"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripButton_AxisDialog
        '
        Me.ToolStripButton_AxisDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_AxisDialog.Image = Global.BlueM.Wave.My.Resources.Resources.shape_align_bottom
        Me.ToolStripButton_AxisDialog.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_AxisDialog.Name = "ToolStripButton_AxisDialog"
        Me.ToolStripButton_AxisDialog.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_AxisDialog.Text = "ToolStripButton_AxisDialog"
        Me.ToolStripButton_AxisDialog.ToolTipText = "Manage Axes"
        '
        'ToolStripButton_AutoAdjustYAxes
        '
        Me.ToolStripButton_AutoAdjustYAxes.CheckOnClick = True
        Me.ToolStripButton_AutoAdjustYAxes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_AutoAdjustYAxes.Image = Global.BlueM.Wave.My.Resources.Resources.arrow_up_down
        Me.ToolStripButton_AutoAdjustYAxes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_AutoAdjustYAxes.Name = "ToolStripButton_AutoAdjustYAxes"
        Me.ToolStripButton_AutoAdjustYAxes.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_AutoAdjustYAxes.Text = "Auto-adjust Y-axes to current view"
        '
        'ToolStripButton_Crosshair
        '
        Me.ToolStripButton_Crosshair.CheckOnClick = True
        Me.ToolStripButton_Crosshair.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Crosshair.Image = Global.BlueM.Wave.My.Resources.Resources.crosshair
        Me.ToolStripButton_Crosshair.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Crosshair.Name = "ToolStripButton_Crosshair"
        Me.ToolStripButton_Crosshair.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Crosshair.Text = "Crosshair"
        Me.ToolStripButton_Crosshair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New System.Drawing.Size(6, 34)
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
        'ToolStripButton_ZoomIn
        '
        Me.ToolStripButton_ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ZoomIn.Image = Global.BlueM.Wave.My.Resources.Resources.zoom_in
        Me.ToolStripButton_ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ZoomIn.Name = "ToolStripButton_ZoomIn"
        Me.ToolStripButton_ZoomIn.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ZoomIn.Text = "Zoom in"
        '
        'ToolStripButton_ZoomOut
        '
        Me.ToolStripButton_ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ZoomOut.Image = Global.BlueM.Wave.My.Resources.Resources.zoom_out
        Me.ToolStripButton_ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ZoomOut.Name = "ToolStripButton_ZoomOut"
        Me.ToolStripButton_ZoomOut.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ZoomOut.Text = "Zoom out"
        '
        'ToolStripButton_ZoomPrevious
        '
        Me.ToolStripButton_ZoomPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ZoomPrevious.Enabled = False
        Me.ToolStripButton_ZoomPrevious.Image = Global.BlueM.Wave.My.Resources.Resources.zoom_previous
        Me.ToolStripButton_ZoomPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ZoomPrevious.Name = "ToolStripButton_ZoomPrevious"
        Me.ToolStripButton_ZoomPrevious.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ZoomPrevious.Text = "Zoom previous"
        Me.ToolStripButton_ZoomPrevious.ToolTipText = "Zoom to previous extent"
        '
        'ToolStripDropDownButton_Help
        '
        Me.ToolStripDropDownButton_Help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripDropDownButton_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton_Help.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_Help, Me.ToolStripMenuItem_Changelog, Me.ToolStripMenuItem_CheckForUpdate, Me.ToolStripMenuItem_About})
        Me.ToolStripDropDownButton_Help.Image = Global.BlueM.Wave.My.Resources.Resources.help
        Me.ToolStripDropDownButton_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_Help.Name = "ToolStripDropDownButton_Help"
        Me.ToolStripDropDownButton_Help.Size = New System.Drawing.Size(29, 31)
        Me.ToolStripDropDownButton_Help.Text = "Help"
        '
        'ToolStripMenuItem_Help
        '
        Me.ToolStripMenuItem_Help.Name = "ToolStripMenuItem_Help"
        Me.ToolStripMenuItem_Help.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem_Help.Text = "Help"
        '
        'ToolStripMenuItem_Changelog
        '
        Me.ToolStripMenuItem_Changelog.Name = "ToolStripMenuItem_Changelog"
        Me.ToolStripMenuItem_Changelog.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem_Changelog.Text = "Changelog"
        '
        'ToolStripMenuItem_CheckForUpdate
        '
        Me.ToolStripMenuItem_CheckForUpdate.Name = "ToolStripMenuItem_CheckForUpdate"
        Me.ToolStripMenuItem_CheckForUpdate.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem_CheckForUpdate.Text = "Check for update"
        '
        'ToolStripMenuItem_About
        '
        Me.ToolStripMenuItem_About.Image = Global.BlueM.Wave.My.Resources.Resources.BlueM_icon
        Me.ToolStripMenuItem_About.Name = "ToolStripMenuItem_About"
        Me.ToolStripMenuItem_About.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem_About.Text = "About"
        '
        'ToolStripButton_ZoomNext
        '
        Me.ToolStripButton_ZoomNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_ZoomNext.Enabled = False
        Me.ToolStripButton_ZoomNext.Image = CType(resources.GetObject("ToolStripButton_ZoomNext.Image"), System.Drawing.Image)
        Me.ToolStripButton_ZoomNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_ZoomNext.Name = "ToolStripButton_ZoomNext"
        Me.ToolStripButton_ZoomNext.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_ZoomNext.Text = "Zoom next"
        Me.ToolStripButton_ZoomNext.ToolTipText = "Zoom to next extent"
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
        Me.ToolStripButton_ZoomAll.ToolTipText = "Zoom to full extent"
        '
        'ToolStripButton_UpdateNotification
        '
        Me.ToolStripButton_UpdateNotification.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton_UpdateNotification.Image = Global.BlueM.Wave.My.Resources.Resources.application_get
        Me.ToolStripButton_UpdateNotification.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_UpdateNotification.Name = "ToolStripButton_UpdateNotification"
        Me.ToolStripButton_UpdateNotification.Size = New System.Drawing.Size(117, 31)
        Me.ToolStripButton_UpdateNotification.Text = "Update available!"
        Me.ToolStripButton_UpdateNotification.Visible = False
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 34)
        '
        'ToolStripDropDownButton_ActivateAllSeries
        '
        Me.ToolStripDropDownButton_ActivateAllSeries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton_ActivateAllSeries.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_ActivateAllSeries, Me.ToolStripMenuItem_DeactivateAllSeries})
        Me.ToolStripDropDownButton_ActivateAllSeries.Image = Global.BlueM.Wave.My.Resources.Resources.accept_split
        Me.ToolStripDropDownButton_ActivateAllSeries.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton_ActivateAllSeries.Name = "ToolStripDropDownButton_ActivateAllSeries"
        Me.ToolStripDropDownButton_ActivateAllSeries.Size = New System.Drawing.Size(29, 31)
        '
        'ToolStripMenuItem_ActivateAllSeries
        '
        Me.ToolStripMenuItem_ActivateAllSeries.Image = Global.BlueM.Wave.My.Resources.Resources.accept
        Me.ToolStripMenuItem_ActivateAllSeries.Name = "ToolStripMenuItem_ActivateAllSeries"
        Me.ToolStripMenuItem_ActivateAllSeries.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem_ActivateAllSeries.Text = "Activate all series"
        '
        'ToolStripMenuItem_DeactivateAllSeries
        '
        Me.ToolStripMenuItem_DeactivateAllSeries.Image = Global.BlueM.Wave.My.Resources.Resources.accept_grayscale
        Me.ToolStripMenuItem_DeactivateAllSeries.Name = "ToolStripMenuItem_DeactivateAllSeries"
        Me.ToolStripMenuItem_DeactivateAllSeries.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem_DeactivateAllSeries.Text = "Deactivate all series"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Multiselect = True
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
        Me.SplitContainer1.SplitterDistance = 140
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
        Me.TChart2.Axes.Bottom.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Bottom.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Axes.Bottom.Labels.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Bottom.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Bottom.Labels.Font.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Bottom.Labels.Font.Size = 9
        Me.TChart2.Axes.Bottom.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Bottom.Labels.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Bottom.Labels.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Angle = 0
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Bottom.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Bottom.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Axes.Bottom.Title.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart2.Axes.Bottom.Title.Font.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Bottom.Title.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Title.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Bottom.Title.Font.Size = 11
        Me.TChart2.Axes.Bottom.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Bottom.Title.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Bottom.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Bottom.Title.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Bottom.Title.Shadow.Brush.Visible = True
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
        Me.TChart2.Axes.Depth.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Depth.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Axes.Depth.Labels.Brush.Solid = True
        Me.TChart2.Axes.Depth.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Depth.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Depth.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Depth.Labels.Font.Brush.Solid = True
        Me.TChart2.Axes.Depth.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Depth.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Depth.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Depth.Labels.Font.Size = 9
        Me.TChart2.Axes.Depth.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Depth.Labels.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Depth.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Depth.Labels.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Depth.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Angle = 0
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Depth.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Depth.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Axes.Depth.Title.Brush.Solid = True
        Me.TChart2.Axes.Depth.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart2.Axes.Depth.Title.Font.Brush.Solid = True
        Me.TChart2.Axes.Depth.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Depth.Title.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Depth.Title.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Depth.Title.Font.Size = 11
        Me.TChart2.Axes.Depth.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Depth.Title.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Depth.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Depth.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Depth.Title.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Depth.Title.Shadow.Brush.Visible = True
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
        Me.TChart2.Axes.DepthTop.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Axes.DepthTop.Labels.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Labels.Font.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.DepthTop.Labels.Font.Size = 9
        Me.TChart2.Axes.DepthTop.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.DepthTop.Labels.Shadow.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Angle = 0
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Axes.DepthTop.Title.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart2.Axes.DepthTop.Title.Font.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.DepthTop.Title.Font.Size = 11
        Me.TChart2.Axes.DepthTop.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.DepthTop.Title.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.DepthTop.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.DepthTop.Title.Shadow.Brush.Solid = True
        Me.TChart2.Axes.DepthTop.Title.Shadow.Brush.Visible = True
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
        Me.TChart2.Axes.Left.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Left.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Axes.Left.Labels.Brush.Solid = True
        Me.TChart2.Axes.Left.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Left.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Left.Labels.Font.Brush.Solid = True
        Me.TChart2.Axes.Left.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Left.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Left.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Left.Labels.Font.Size = 9
        Me.TChart2.Axes.Left.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Left.Labels.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Left.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Left.Labels.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Left.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Angle = 90
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Left.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Left.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Axes.Left.Title.Brush.Solid = True
        Me.TChart2.Axes.Left.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart2.Axes.Left.Title.Font.Brush.Solid = True
        Me.TChart2.Axes.Left.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Left.Title.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Left.Title.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Left.Title.Font.Size = 11
        Me.TChart2.Axes.Left.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Left.Title.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Left.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Left.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Left.Title.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Left.Title.Shadow.Brush.Visible = True
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
        Me.TChart2.Axes.Right.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Right.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Axes.Right.Labels.Brush.Solid = True
        Me.TChart2.Axes.Right.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Right.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Right.Labels.Font.Brush.Solid = True
        Me.TChart2.Axes.Right.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Right.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Right.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Right.Labels.Font.Size = 9
        Me.TChart2.Axes.Right.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Right.Labels.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Right.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Right.Labels.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Right.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Angle = 270
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Right.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Right.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Axes.Right.Title.Brush.Solid = True
        Me.TChart2.Axes.Right.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart2.Axes.Right.Title.Font.Brush.Solid = True
        Me.TChart2.Axes.Right.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Right.Title.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Right.Title.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Right.Title.Font.Size = 11
        Me.TChart2.Axes.Right.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Right.Title.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Right.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Right.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Right.Title.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Right.Title.Shadow.Brush.Visible = True
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
        Me.TChart2.Axes.Top.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Top.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Axes.Top.Labels.Brush.Solid = True
        Me.TChart2.Axes.Top.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Top.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Top.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Top.Labels.Font.Brush.Solid = True
        Me.TChart2.Axes.Top.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Top.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Top.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Top.Labels.Font.Size = 9
        Me.TChart2.Axes.Top.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Top.Labels.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Top.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Top.Labels.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Top.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Angle = 0
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Axes.Top.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Axes.Top.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Axes.Top.Title.Brush.Solid = True
        Me.TChart2.Axes.Top.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Font.Bold = False
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart2.Axes.Top.Title.Font.Brush.Solid = True
        Me.TChart2.Axes.Top.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Top.Title.Font.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Top.Title.Font.Shadow.Brush.Visible = True
        Me.TChart2.Axes.Top.Title.Font.Size = 11
        Me.TChart2.Axes.Top.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Axes.Top.Title.ImageBevel.Brush.Solid = True
        Me.TChart2.Axes.Top.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Axes.Top.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Axes.Top.Title.Shadow.Brush.Solid = True
        Me.TChart2.Axes.Top.Title.Shadow.Brush.Visible = True
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
        Me.TChart2.Footer.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Footer.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Footer.Brush.Solid = True
        Me.TChart2.Footer.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Footer.Font.Bold = False
        '
        '
        '
        Me.TChart2.Footer.Font.Brush.Color = System.Drawing.Color.Red
        Me.TChart2.Footer.Font.Brush.Solid = True
        Me.TChart2.Footer.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Footer.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Footer.Font.Shadow.Brush.Solid = True
        Me.TChart2.Footer.Font.Shadow.Brush.Visible = True
        Me.TChart2.Footer.Font.Size = 8
        Me.TChart2.Footer.Font.SizeFloat = 8.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Footer.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Footer.ImageBevel.Brush.Solid = True
        Me.TChart2.Footer.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Footer.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Footer.Shadow.Brush.Solid = True
        Me.TChart2.Footer.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Header.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Header.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Header.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Header.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TChart2.Header.Brush.Solid = True
        Me.TChart2.Header.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Header.Font.Bold = False
        '
        '
        '
        Me.TChart2.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Header.Font.Brush.Solid = True
        Me.TChart2.Header.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Header.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Header.Font.Shadow.Brush.Solid = True
        Me.TChart2.Header.Font.Shadow.Brush.Visible = True
        Me.TChart2.Header.Font.Size = 12
        Me.TChart2.Header.Font.SizeFloat = 12.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Header.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Header.ImageBevel.Brush.Solid = True
        Me.TChart2.Header.ImageBevel.Brush.Visible = True
        Me.TChart2.Header.Lines = New String() {"Overview chart"}
        '
        '
        '
        '
        '
        '
        Me.TChart2.Header.Shadow.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.TChart2.Header.Shadow.Brush.Solid = True
        Me.TChart2.Header.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Legend.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Legend.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Legend.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Legend.Brush.Solid = True
        Me.TChart2.Legend.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Legend.Font.Bold = False
        '
        '
        '
        Me.TChart2.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart2.Legend.Font.Brush.Solid = True
        Me.TChart2.Legend.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Legend.Font.Shadow.Brush.Solid = True
        Me.TChart2.Legend.Font.Shadow.Brush.Visible = True
        Me.TChart2.Legend.Font.Size = 9
        Me.TChart2.Legend.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Legend.ImageBevel.Brush.Solid = True
        Me.TChart2.Legend.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TChart2.Legend.Shadow.Brush.Solid = True
        Me.TChart2.Legend.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Symbol.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Legend.Symbol.Shadow.Brush.Solid = True
        Me.TChart2.Legend.Symbol.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Legend.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Legend.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Legend.Title.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Legend.Title.Brush.Solid = True
        Me.TChart2.Legend.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Legend.Title.Font.Bold = True
        '
        '
        '
        Me.TChart2.Legend.Title.Font.Brush.Color = System.Drawing.Color.Black
        Me.TChart2.Legend.Title.Font.Brush.Solid = True
        Me.TChart2.Legend.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Legend.Title.Font.Shadow.Brush.Solid = True
        Me.TChart2.Legend.Title.Font.Shadow.Brush.Visible = True
        Me.TChart2.Legend.Title.Font.Size = 8
        Me.TChart2.Legend.Title.Font.SizeFloat = 8.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Legend.Title.ImageBevel.Brush.Solid = True
        Me.TChart2.Legend.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Legend.Title.Shadow.Brush.Solid = True
        Me.TChart2.Legend.Title.Shadow.Brush.Visible = True
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
        Me.TChart2.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Panel.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Panel.Brush.Solid = True
        Me.TChart2.Panel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Panel.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Panel.ImageBevel.Brush.Solid = True
        Me.TChart2.Panel.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Panel.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Panel.Shadow.Brush.Solid = True
        Me.TChart2.Panel.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart2.Printer.Landscape = True
        Margins1.Bottom = 100
        Margins1.Left = 100
        Margins1.Right = 100
        Margins1.Top = 100
        Me.TChart2.Printer.Margins = Margins1
        Me.TChart2.Size = New System.Drawing.Size(940, 136)
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubFooter.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.SubFooter.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.SubFooter.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.SubFooter.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.SubFooter.Brush.Solid = True
        Me.TChart2.SubFooter.Brush.Visible = True
        '
        '
        '
        Me.TChart2.SubFooter.Font.Bold = False
        '
        '
        '
        Me.TChart2.SubFooter.Font.Brush.Color = System.Drawing.Color.Red
        Me.TChart2.SubFooter.Font.Brush.Solid = True
        Me.TChart2.SubFooter.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubFooter.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.SubFooter.Font.Shadow.Brush.Solid = True
        Me.TChart2.SubFooter.Font.Shadow.Brush.Visible = True
        Me.TChart2.SubFooter.Font.Size = 8
        Me.TChart2.SubFooter.Font.SizeFloat = 8.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubFooter.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.SubFooter.ImageBevel.Brush.Solid = True
        Me.TChart2.SubFooter.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubFooter.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.SubFooter.Shadow.Brush.Solid = True
        Me.TChart2.SubFooter.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubHeader.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.SubHeader.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.SubHeader.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.SubHeader.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TChart2.SubHeader.Brush.Solid = True
        Me.TChart2.SubHeader.Brush.Visible = True
        '
        '
        '
        Me.TChart2.SubHeader.Font.Bold = False
        '
        '
        '
        Me.TChart2.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.SubHeader.Font.Brush.Solid = True
        Me.TChart2.SubHeader.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubHeader.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.SubHeader.Font.Shadow.Brush.Solid = True
        Me.TChart2.SubHeader.Font.Shadow.Brush.Visible = True
        Me.TChart2.SubHeader.Font.Size = 12
        Me.TChart2.SubHeader.Font.SizeFloat = 12.0!
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubHeader.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.SubHeader.ImageBevel.Brush.Solid = True
        Me.TChart2.SubHeader.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.SubHeader.Shadow.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.TChart2.SubHeader.Shadow.Brush.Solid = True
        Me.TChart2.SubHeader.Shadow.Brush.Visible = True
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
        Me.TChart2.Walls.Back.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Walls.Back.Brush.Color = System.Drawing.Color.Silver
        Me.TChart2.Walls.Back.Brush.Solid = True
        Me.TChart2.Walls.Back.Brush.Visible = False
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Back.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Walls.Back.ImageBevel.Brush.Solid = True
        Me.TChart2.Walls.Back.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Back.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Walls.Back.Shadow.Brush.Solid = True
        Me.TChart2.Walls.Back.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Bottom.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Walls.Bottom.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Walls.Bottom.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Walls.Bottom.Brush.Color = System.Drawing.Color.White
        Me.TChart2.Walls.Bottom.Brush.Solid = True
        Me.TChart2.Walls.Bottom.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Bottom.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Walls.Bottom.ImageBevel.Brush.Solid = True
        Me.TChart2.Walls.Bottom.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Bottom.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Walls.Bottom.Shadow.Brush.Solid = True
        Me.TChart2.Walls.Bottom.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Left.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Walls.Left.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Walls.Left.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Walls.Left.Brush.Color = System.Drawing.Color.LightYellow
        Me.TChart2.Walls.Left.Brush.Solid = True
        Me.TChart2.Walls.Left.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Left.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Walls.Left.ImageBevel.Brush.Solid = True
        Me.TChart2.Walls.Left.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Left.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Walls.Left.Shadow.Brush.Solid = True
        Me.TChart2.Walls.Left.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Right.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart2.Walls.Right.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart2.Walls.Right.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart2.Walls.Right.Brush.Color = System.Drawing.Color.LightYellow
        Me.TChart2.Walls.Right.Brush.Solid = True
        Me.TChart2.Walls.Right.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Right.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart2.Walls.Right.ImageBevel.Brush.Solid = True
        Me.TChart2.Walls.Right.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Walls.Right.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart2.Walls.Right.Shadow.Brush.Solid = True
        Me.TChart2.Walls.Right.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart2.Zoom.Brush.Color = System.Drawing.Color.LightBlue
        Me.TChart2.Zoom.Brush.Solid = True
        Me.TChart2.Zoom.Brush.Visible = True
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(940, 458)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'PanelNavigation
        '
        Me.PanelNavigation.Controls.Add(Me.Button_NavEnd)
        Me.PanelNavigation.Controls.Add(Me.Button_NavStart)
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
        Me.PanelNavigation.TabIndex = 0
        '
        'Button_NavEnd
        '
        Me.Button_NavEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_NavEnd.Image = Global.BlueM.Wave.My.Resources.Resources.control_end
        Me.Button_NavEnd.Location = New System.Drawing.Point(906, 8)
        Me.Button_NavEnd.Name = "Button_NavEnd"
        Me.Button_NavEnd.Size = New System.Drawing.Size(23, 23)
        Me.Button_NavEnd.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.Button_NavEnd, "Navigate to end")
        Me.Button_NavEnd.UseVisualStyleBackColor = True
        '
        'Button_NavStart
        '
        Me.Button_NavStart.Image = Global.BlueM.Wave.My.Resources.Resources.control_start
        Me.Button_NavStart.Location = New System.Drawing.Point(8, 8)
        Me.Button_NavStart.Name = "Button_NavStart"
        Me.Button_NavStart.Size = New System.Drawing.Size(23, 23)
        Me.Button_NavStart.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.Button_NavStart, "Navigate to start")
        Me.Button_NavStart.UseVisualStyleBackColor = True
        '
        'Label_Navigate
        '
        Me.Label_Navigate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label_Navigate.AutoSize = True
        Me.Label_Navigate.Location = New System.Drawing.Point(376, 13)
        Me.Label_Navigate.Name = "Label_Navigate"
        Me.Label_Navigate.Size = New System.Drawing.Size(53, 13)
        Me.Label_Navigate.TabIndex = 2
        Me.Label_Navigate.Text = "Navigate:"
        '
        'Button_NavForward
        '
        Me.Button_NavForward.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_NavForward.Image = CType(resources.GetObject("Button_NavForward.Image"), System.Drawing.Image)
        Me.Button_NavForward.Location = New System.Drawing.Point(831, 8)
        Me.Button_NavForward.Name = "Button_NavForward"
        Me.Button_NavForward.Size = New System.Drawing.Size(69, 23)
        Me.Button_NavForward.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.Button_NavForward, "Navigate forwards")
        Me.Button_NavForward.UseVisualStyleBackColor = True
        '
        'Button_NavBack
        '
        Me.Button_NavBack.Image = CType(resources.GetObject("Button_NavBack.Image"), System.Drawing.Image)
        Me.Button_NavBack.Location = New System.Drawing.Point(37, 8)
        Me.Button_NavBack.Name = "Button_NavBack"
        Me.Button_NavBack.Size = New System.Drawing.Size(69, 23)
        Me.Button_NavBack.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.Button_NavBack, "Navigate backwards")
        Me.Button_NavBack.UseVisualStyleBackColor = True
        '
        'NumericUpDown_NavMultiplier
        '
        Me.NumericUpDown_NavMultiplier.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.NumericUpDown_NavMultiplier.Location = New System.Drawing.Point(435, 9)
        Me.NumericUpDown_NavMultiplier.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_NavMultiplier.Name = "NumericUpDown_NavMultiplier"
        Me.NumericUpDown_NavMultiplier.Size = New System.Drawing.Size(40, 20)
        Me.NumericUpDown_NavMultiplier.TabIndex = 3
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
        Me.ComboBox_NavIncrement.TabIndex = 4
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
        Me.TChart1.Axes.Bottom.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Bottom.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Axes.Bottom.Labels.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Bottom.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Bottom.Labels.Font.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Bottom.Labels.Font.Size = 9
        Me.TChart1.Axes.Bottom.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Bottom.Labels.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Bottom.Labels.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Angle = 0
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Bottom.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Bottom.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Axes.Bottom.Title.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart1.Axes.Bottom.Title.Font.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Bottom.Title.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Title.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Bottom.Title.Font.Size = 11
        Me.TChart1.Axes.Bottom.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Bottom.Title.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Bottom.Title.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Bottom.Title.Shadow.Brush.Visible = True
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
        Me.TChart1.Axes.Depth.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Depth.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Axes.Depth.Labels.Brush.Solid = True
        Me.TChart1.Axes.Depth.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Depth.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Depth.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Depth.Labels.Font.Brush.Solid = True
        Me.TChart1.Axes.Depth.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Depth.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Depth.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Depth.Labels.Font.Size = 9
        Me.TChart1.Axes.Depth.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Depth.Labels.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Depth.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Depth.Labels.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Depth.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Angle = 0
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Depth.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Depth.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Axes.Depth.Title.Brush.Solid = True
        Me.TChart1.Axes.Depth.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart1.Axes.Depth.Title.Font.Brush.Solid = True
        Me.TChart1.Axes.Depth.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Depth.Title.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Depth.Title.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Depth.Title.Font.Size = 11
        Me.TChart1.Axes.Depth.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Depth.Title.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Depth.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Depth.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Depth.Title.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Depth.Title.Shadow.Brush.Visible = True
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
        Me.TChart1.Axes.DepthTop.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Axes.DepthTop.Labels.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Labels.Font.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.DepthTop.Labels.Font.Size = 9
        Me.TChart1.Axes.DepthTop.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.DepthTop.Labels.Shadow.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Angle = 0
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Axes.DepthTop.Title.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart1.Axes.DepthTop.Title.Font.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.DepthTop.Title.Font.Size = 11
        Me.TChart1.Axes.DepthTop.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.DepthTop.Title.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.DepthTop.Title.Shadow.Brush.Solid = True
        Me.TChart1.Axes.DepthTop.Title.Shadow.Brush.Visible = True
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
        Me.TChart1.Axes.Left.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Left.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Axes.Left.Labels.Brush.Solid = True
        Me.TChart1.Axes.Left.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Left.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Left.Labels.Font.Brush.Solid = True
        Me.TChart1.Axes.Left.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Left.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Left.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Left.Labels.Font.Size = 9
        Me.TChart1.Axes.Left.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Left.Labels.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Left.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Left.Labels.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Left.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Angle = 90
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Left.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Left.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Axes.Left.Title.Brush.Solid = True
        Me.TChart1.Axes.Left.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart1.Axes.Left.Title.Font.Brush.Solid = True
        Me.TChart1.Axes.Left.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Left.Title.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Left.Title.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Left.Title.Font.Size = 11
        Me.TChart1.Axes.Left.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Left.Title.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Left.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Left.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Left.Title.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Left.Title.Shadow.Brush.Visible = True
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
        Me.TChart1.Axes.Right.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Right.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Axes.Right.Labels.Brush.Solid = True
        Me.TChart1.Axes.Right.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Right.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Right.Labels.Font.Brush.Solid = True
        Me.TChart1.Axes.Right.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Right.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Right.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Right.Labels.Font.Size = 9
        Me.TChart1.Axes.Right.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Right.Labels.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Right.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Right.Labels.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Right.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Angle = 270
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Right.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Right.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Axes.Right.Title.Brush.Solid = True
        Me.TChart1.Axes.Right.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart1.Axes.Right.Title.Font.Brush.Solid = True
        Me.TChart1.Axes.Right.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Right.Title.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Right.Title.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Right.Title.Font.Size = 11
        Me.TChart1.Axes.Right.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Right.Title.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Right.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Right.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Right.Title.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Right.Title.Shadow.Brush.Visible = True
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
        Me.TChart1.Axes.Top.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Top.Labels.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Axes.Top.Labels.Brush.Solid = True
        Me.TChart1.Axes.Top.Labels.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Top.Labels.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Top.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Top.Labels.Font.Brush.Solid = True
        Me.TChart1.Axes.Top.Labels.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Top.Labels.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Top.Labels.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Top.Labels.Font.Size = 9
        Me.TChart1.Axes.Top.Labels.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Top.Labels.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Top.Labels.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Top.Labels.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Top.Labels.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Angle = 0
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Axes.Top.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Axes.Top.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Axes.Top.Title.Brush.Solid = True
        Me.TChart1.Axes.Top.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Font.Bold = False
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart1.Axes.Top.Title.Font.Brush.Solid = True
        Me.TChart1.Axes.Top.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Top.Title.Font.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Top.Title.Font.Shadow.Brush.Visible = True
        Me.TChart1.Axes.Top.Title.Font.Size = 11
        Me.TChart1.Axes.Top.Title.Font.SizeFloat = 11.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Axes.Top.Title.ImageBevel.Brush.Solid = True
        Me.TChart1.Axes.Top.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Top.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Axes.Top.Title.Shadow.Brush.Solid = True
        Me.TChart1.Axes.Top.Title.Shadow.Brush.Visible = True
        Me.TChart1.Dock = System.Windows.Forms.DockStyle.Fill
        '
        '
        '
        '
        '
        '
        Me.TChart1.Footer.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Footer.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Footer.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Footer.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Footer.Brush.Solid = True
        Me.TChart1.Footer.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Footer.Font.Bold = False
        '
        '
        '
        Me.TChart1.Footer.Font.Brush.Color = System.Drawing.Color.Red
        Me.TChart1.Footer.Font.Brush.Solid = True
        Me.TChart1.Footer.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Footer.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Footer.Font.Shadow.Brush.Solid = True
        Me.TChart1.Footer.Font.Shadow.Brush.Visible = True
        Me.TChart1.Footer.Font.Size = 8
        Me.TChart1.Footer.Font.SizeFloat = 8.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Footer.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Footer.ImageBevel.Brush.Solid = True
        Me.TChart1.Footer.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Footer.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Footer.Shadow.Brush.Solid = True
        Me.TChart1.Footer.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Header.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Header.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Header.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Header.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TChart1.Header.Brush.Solid = True
        Me.TChart1.Header.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Header.Font.Bold = False
        '
        '
        '
        Me.TChart1.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Header.Font.Brush.Solid = True
        Me.TChart1.Header.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Header.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Header.Font.Shadow.Brush.Solid = True
        Me.TChart1.Header.Font.Shadow.Brush.Visible = True
        Me.TChart1.Header.Font.Size = 12
        Me.TChart1.Header.Font.SizeFloat = 12.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Header.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Header.ImageBevel.Brush.Solid = True
        Me.TChart1.Header.ImageBevel.Brush.Visible = True
        Me.TChart1.Header.Lines = New String() {"Main chart"}
        '
        '
        '
        '
        '
        '
        Me.TChart1.Header.Shadow.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.TChart1.Header.Shadow.Brush.Solid = True
        Me.TChart1.Header.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Legend.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Legend.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Legend.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Legend.Brush.Solid = True
        Me.TChart1.Legend.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Legend.Font.Bold = False
        '
        '
        '
        Me.TChart1.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TChart1.Legend.Font.Brush.Solid = True
        Me.TChart1.Legend.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Legend.Font.Shadow.Brush.Solid = True
        Me.TChart1.Legend.Font.Shadow.Brush.Visible = True
        Me.TChart1.Legend.Font.Size = 9
        Me.TChart1.Legend.Font.SizeFloat = 9.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Legend.ImageBevel.Brush.Solid = True
        Me.TChart1.Legend.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TChart1.Legend.Shadow.Brush.Solid = True
        Me.TChart1.Legend.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Symbol.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Legend.Symbol.Shadow.Brush.Solid = True
        Me.TChart1.Legend.Symbol.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Title.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Legend.Title.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Legend.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Legend.Title.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Legend.Title.Brush.Solid = True
        Me.TChart1.Legend.Title.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Legend.Title.Font.Bold = True
        '
        '
        '
        Me.TChart1.Legend.Title.Font.Brush.Color = System.Drawing.Color.Black
        Me.TChart1.Legend.Title.Font.Brush.Solid = True
        Me.TChart1.Legend.Title.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Legend.Title.Font.Shadow.Brush.Solid = True
        Me.TChart1.Legend.Title.Font.Shadow.Brush.Visible = True
        Me.TChart1.Legend.Title.Font.Size = 8
        Me.TChart1.Legend.Title.Font.SizeFloat = 8.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Legend.Title.ImageBevel.Brush.Solid = True
        Me.TChart1.Legend.Title.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Legend.Title.Shadow.Brush.Solid = True
        Me.TChart1.Legend.Title.Shadow.Brush.Visible = True
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
        Me.TChart1.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Panel.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Panel.Brush.Solid = True
        Me.TChart1.Panel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Panel.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Panel.ImageBevel.Brush.Solid = True
        Me.TChart1.Panel.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Panel.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Panel.Shadow.Brush.Solid = True
        Me.TChart1.Panel.Shadow.Brush.Visible = True
        '
        '
        '
        Me.TChart1.Printer.Landscape = True
        Margins2.Bottom = 10
        Margins2.Left = 10
        Margins2.Right = 10
        Margins2.Top = 10
        Me.TChart1.Printer.Margins = Margins2
        Me.TChart1.Size = New System.Drawing.Size(938, 380)
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubFooter.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.SubFooter.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.SubFooter.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.SubFooter.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.SubFooter.Brush.Solid = True
        Me.TChart1.SubFooter.Brush.Visible = True
        '
        '
        '
        Me.TChart1.SubFooter.Font.Bold = False
        '
        '
        '
        Me.TChart1.SubFooter.Font.Brush.Color = System.Drawing.Color.Red
        Me.TChart1.SubFooter.Font.Brush.Solid = True
        Me.TChart1.SubFooter.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubFooter.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.SubFooter.Font.Shadow.Brush.Solid = True
        Me.TChart1.SubFooter.Font.Shadow.Brush.Visible = True
        Me.TChart1.SubFooter.Font.Size = 8
        Me.TChart1.SubFooter.Font.SizeFloat = 8.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubFooter.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.SubFooter.ImageBevel.Brush.Solid = True
        Me.TChart1.SubFooter.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubFooter.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.SubFooter.Shadow.Brush.Solid = True
        Me.TChart1.SubFooter.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubHeader.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.SubHeader.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.SubHeader.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.SubHeader.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TChart1.SubHeader.Brush.Solid = True
        Me.TChart1.SubHeader.Brush.Visible = True
        '
        '
        '
        Me.TChart1.SubHeader.Font.Bold = False
        '
        '
        '
        Me.TChart1.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.SubHeader.Font.Brush.Solid = True
        Me.TChart1.SubHeader.Font.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubHeader.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.SubHeader.Font.Shadow.Brush.Solid = True
        Me.TChart1.SubHeader.Font.Shadow.Brush.Visible = True
        Me.TChart1.SubHeader.Font.Size = 12
        Me.TChart1.SubHeader.Font.SizeFloat = 12.0!
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubHeader.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.SubHeader.ImageBevel.Brush.Solid = True
        Me.TChart1.SubHeader.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.SubHeader.Shadow.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.TChart1.SubHeader.Shadow.Brush.Solid = True
        Me.TChart1.SubHeader.Shadow.Brush.Visible = True
        Me.TChart1.TabIndex = 1
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
        Me.TChart1.Walls.Back.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Walls.Back.Brush.Color = System.Drawing.Color.Silver
        Me.TChart1.Walls.Back.Brush.Solid = True
        Me.TChart1.Walls.Back.Brush.Visible = False
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Back.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Walls.Back.ImageBevel.Brush.Solid = True
        Me.TChart1.Walls.Back.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Back.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Walls.Back.Shadow.Brush.Solid = True
        Me.TChart1.Walls.Back.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Bottom.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Walls.Bottom.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Walls.Bottom.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Walls.Bottom.Brush.Color = System.Drawing.Color.White
        Me.TChart1.Walls.Bottom.Brush.Solid = True
        Me.TChart1.Walls.Bottom.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Bottom.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Walls.Bottom.ImageBevel.Brush.Solid = True
        Me.TChart1.Walls.Bottom.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Bottom.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Walls.Bottom.Shadow.Brush.Solid = True
        Me.TChart1.Walls.Bottom.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Left.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Walls.Left.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Walls.Left.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Walls.Left.Brush.Color = System.Drawing.Color.LightYellow
        Me.TChart1.Walls.Left.Brush.Solid = True
        Me.TChart1.Walls.Left.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Left.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Walls.Left.ImageBevel.Brush.Solid = True
        Me.TChart1.Walls.Left.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Left.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Walls.Left.Shadow.Brush.Solid = True
        Me.TChart1.Walls.Left.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Right.Bevel.ColorOne = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TChart1.Walls.Right.Bevel.ColorTwo = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TChart1.Walls.Right.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None
        '
        '
        '
        Me.TChart1.Walls.Right.Brush.Color = System.Drawing.Color.LightYellow
        Me.TChart1.Walls.Right.Brush.Solid = True
        Me.TChart1.Walls.Right.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Right.ImageBevel.Brush.Color = System.Drawing.Color.LightGray
        Me.TChart1.Walls.Right.ImageBevel.Brush.Solid = True
        Me.TChart1.Walls.Right.ImageBevel.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Right.Shadow.Brush.Color = System.Drawing.Color.DarkGray
        Me.TChart1.Walls.Right.Shadow.Brush.Solid = True
        Me.TChart1.Walls.Right.Shadow.Brush.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Zoom.Brush.Color = System.Drawing.Color.LightBlue
        Me.TChart1.Zoom.Brush.Solid = True
        Me.TChart1.Zoom.Brush.Visible = True
        '
        'Panel_DisplayRange
        '
        Me.Panel_DisplayRange.Controls.Add(Me.Label_Display)
        Me.Panel_DisplayRange.Controls.Add(Me.NumericUpDown_DisplayRangeMultiplier)
        Me.Panel_DisplayRange.Controls.Add(Me.ComboBox_DisplayRangeUnit)
        Me.Panel_DisplayRange.Controls.Add(Me.MaskedTextBox_NavEnd)
        Me.Panel_DisplayRange.Controls.Add(Me.MaskedTextBox_NavStart)
        Me.Panel_DisplayRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_DisplayRange.Location = New System.Drawing.Point(1, 421)
        Me.Panel_DisplayRange.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_DisplayRange.Name = "Panel_DisplayRange"
        Me.Panel_DisplayRange.Size = New System.Drawing.Size(938, 36)
        Me.Panel_DisplayRange.TabIndex = 2
        '
        'Label_Display
        '
        Me.Label_Display.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label_Display.AutoSize = True
        Me.Label_Display.Location = New System.Drawing.Point(381, 12)
        Me.Label_Display.Name = "Label_Display"
        Me.Label_Display.Size = New System.Drawing.Size(44, 13)
        Me.Label_Display.TabIndex = 1
        Me.Label_Display.Text = "Display:"
        '
        'NumericUpDown_DisplayRangeMultiplier
        '
        Me.NumericUpDown_DisplayRangeMultiplier.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.NumericUpDown_DisplayRangeMultiplier.Location = New System.Drawing.Point(431, 8)
        Me.NumericUpDown_DisplayRangeMultiplier.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_DisplayRangeMultiplier.Name = "NumericUpDown_DisplayRangeMultiplier"
        Me.NumericUpDown_DisplayRangeMultiplier.Size = New System.Drawing.Size(40, 20)
        Me.NumericUpDown_DisplayRangeMultiplier.TabIndex = 2
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
        Me.ComboBox_DisplayRangeUnit.TabIndex = 3
        '
        'MaskedTextBox_NavEnd
        '
        Me.MaskedTextBox_NavEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MaskedTextBox_NavEnd.Culture = New System.Globalization.CultureInfo("")
        Me.MaskedTextBox_NavEnd.Location = New System.Drawing.Point(829, 8)
        Me.MaskedTextBox_NavEnd.Name = "MaskedTextBox_NavEnd"
        Me.MaskedTextBox_NavEnd.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox_NavEnd.TabIndex = 4
        Me.MaskedTextBox_NavEnd.ValidatingType = GetType(Date)
        '
        'MaskedTextBox_NavStart
        '
        Me.MaskedTextBox_NavStart.Culture = New System.Globalization.CultureInfo("")
        Me.MaskedTextBox_NavStart.Location = New System.Drawing.Point(8, 8)
        Me.MaskedTextBox_NavStart.Name = "MaskedTextBox_NavStart"
        Me.MaskedTextBox_NavStart.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox_NavStart.TabIndex = 0
        Me.MaskedTextBox_NavStart.ValidatingType = GetType(Date)
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Enabled = False
        Me.ProgressBar1.Location = New System.Drawing.Point(832, 641)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(108, 16)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.TabIndex = 2
        '
        'MainWindow
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(944, 661)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(ToolStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(500, 410)
        Me.Name = "MainWindow"
        Me.Text = "BlueM.Wave"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
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
    Friend WithEvents ToolStripMenuItem_Help As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_About As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ImportSeries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_LoadTEN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_SaveChart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ExportSeries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ReloadFromFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_EnterSeries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton_ZoomPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel_DisplayRange As System.Windows.Forms.Panel
    Friend WithEvents NumericUpDown_DisplayRangeMultiplier As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox_DisplayRangeUnit As System.Windows.Forms.ComboBox
    Friend WithEvents MaskedTextBox_NavEnd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBox_NavStart As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents PanelNavigation As System.Windows.Forms.Panel
    Friend WithEvents NumericUpDown_NavMultiplier As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBox_NavIncrement As System.Windows.Forms.ComboBox
    Friend WithEvents Button_NavBack As System.Windows.Forms.Button
    Friend WithEvents Button_NavForward As System.Windows.Forms.Button
    Friend WithEvents Label_Navigate As System.Windows.Forms.Label
    Friend WithEvents Label_Display As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton_ZoomAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ToolStripButton_ToggleNavigation As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ConvertErrorValues As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton_ZoomToSeries As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem_SaveProjectFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton_Properties As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripMenuItem_RecentlyUsedFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_Merge As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_ShowNaNValues As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton_AutoAdjustYAxes As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_RemoveNaNValues As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripMenuItem_Changelog As ToolStripMenuItem
    Friend WithEvents ToolStripButton_AxisDialog As ToolStripButton
    Friend WithEvents ToolStripMenuItem_PasteFromClipboard As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As ToolStripSeparator
    Friend WithEvents ToolStripStatusLabel_Errors As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel_Warnings As ToolStripStatusLabel
    Friend WithEvents ToolStripButton_UpdateNotification As ToolStripButton
    Friend WithEvents ToolStripMenuItem_CheckForUpdate As ToolStripMenuItem
    Friend WithEvents ToolStripButton_ZoomIn As ToolStripButton
    Friend WithEvents ToolStripButton_ZoomOut As ToolStripButton
    Friend WithEvents ToolStripButton_ZoomNext As ToolStripButton
    Friend WithEvents ToolStripButton_TimeseriesValues As ToolStripButton
    Friend WithEvents ToolStripDropDownButton_ColorPalette As ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem_ColorPaletteMaterial As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ColorPaletteDistinct As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ColorPaletteWheel As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_ColorPaletteRandom As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ToolStripDropDownButton_ActivateAllSeries As ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem_ActivateAllSeries As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_DeactivateAllSeries As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Button_NavStart As Button
    Friend WithEvents Button_NavEnd As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ToolStripButton_Settings As ToolStripButton
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ToolStripButton_Crosshair As ToolStripButton
End Class
