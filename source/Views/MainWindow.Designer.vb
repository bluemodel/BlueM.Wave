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
        components = New ComponentModel.Container()
        Dim ToolStripSeparator1 As ToolStripSeparator
        Dim toolStripSeparator As ToolStripSeparator
        Dim ToolStripSeparator2 As ToolStripSeparator
        Dim StatusStrip1 As StatusStrip
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Dim ToolStrip1 As ToolStrip
        Dim ToolStripSeparator6 As ToolStripSeparator
        Dim ToolStripSeparator5 As ToolStripSeparator
        Dim ToolStripSeparator4 As ToolStripSeparator
        Dim ToolStripSeparator3 As ToolStripSeparator
        Dim Margins2 As Steema.TeeChart.Margins = New Steema.TeeChart.Margins()
        Dim Margins3 As Steema.TeeChart.Margins = New Steema.TeeChart.Margins()
        ToolStripStatusLabel_Errors = New ToolStripStatusLabel()
        ToolStripStatusLabel_Warnings = New ToolStripStatusLabel()
        ToolStripStatusLabel_Log = New ToolStripStatusLabel()
        ToolStripButton_New = New ToolStripButton()
        ToolStripDropDownButton_Open = New ToolStripDropDownButton()
        ToolStripMenuItem_ImportSeries = New ToolStripMenuItem()
        ToolStripMenuItem_EnterSeries = New ToolStripMenuItem()
        ToolStripMenuItem_PasteFromClipboard = New ToolStripMenuItem()
        ToolStripMenuItem_LoadTEN = New ToolStripMenuItem()
        ToolStripMenuItem_ReloadFromFiles = New ToolStripMenuItem()
        ToolStripMenuItem_RecentlyUsedFiles = New ToolStripMenuItem()
        ToolStripDropDownButton_Save = New ToolStripDropDownButton()
        ToolStripMenuItem_SaveProjectFile = New ToolStripMenuItem()
        ToolStripMenuItem_SaveChart = New ToolStripMenuItem()
        ToolStripMenuItem_ExportSeries = New ToolStripMenuItem()
        ToolStripButton_Settings = New ToolStripButton()
        ToolStripSeparator12 = New ToolStripSeparator()
        ToolStripButton_Copy = New ToolStripButton()
        ToolStripButton_Print = New ToolStripButton()
        ToolStripButton_Cut = New ToolStripButton()
        ToolStripButton_Merge = New ToolStripButton()
        ToolStripSeparator8 = New ToolStripSeparator()
        ToolStripButton_ShowNaNValues = New ToolStripButton()
        ToolStripButton_ConvertErrorValues = New ToolStripButton()
        ToolStripButton_RemoveNaNValues = New ToolStripButton()
        ToolStripSeparator7 = New ToolStripSeparator()
        ToolStripButton_Analysis = New ToolStripButton()
        ToolStripButton_EditChart = New ToolStripButton()
        ToolStripDropDownButton_ColorPalette = New ToolStripDropDownButton()
        ToolStripMenuItem_ColorPaletteMaterial = New ToolStripMenuItem()
        ToolStripMenuItem_ColorPaletteDistinct = New ToolStripMenuItem()
        ToolStripMenuItem_ColorPaletteWheel = New ToolStripMenuItem()
        ToolStripMenuItem_ColorPaletteRandom = New ToolStripMenuItem()
        ToolStripSeparator9 = New ToolStripSeparator()
        ToolStripButton_Properties = New ToolStripButton()
        ToolStripButton_TimeseriesValues = New ToolStripButton()
        ToolStripSeparator10 = New ToolStripSeparator()
        ToolStripButton_AxisDialog = New ToolStripButton()
        ToolStripButton_AutoAdjustYAxes = New ToolStripButton()
        ToolStripSeparator13 = New ToolStripSeparator()
        ToolStripButton_AddMarkers = New ToolStripButton()
        ToolStripButton_RemoveMarkers = New ToolStripButton()
        ToolStripButton_ToggleOverview = New ToolStripButton()
        ToolStripButton_ToggleNavigation = New ToolStripButton()
        ToolStripButton_ZoomIn = New ToolStripButton()
        ToolStripButton_ZoomOut = New ToolStripButton()
        ToolStripButton_ZoomPrevious = New ToolStripButton()
        ToolStripDropDownButton_Help = New ToolStripDropDownButton()
        ToolStripMenuItem_Help = New ToolStripMenuItem()
        ToolStripMenuItem_Changelog = New ToolStripMenuItem()
        ToolStripMenuItem_CheckForUpdate = New ToolStripMenuItem()
        ToolStripMenuItem_About = New ToolStripMenuItem()
        ToolStripButton_ZoomNext = New ToolStripButton()
        ToolStripDropDownButton_ZoomToSeries = New ToolStripDropDownButton()
        ToolStripButton_ZoomAll = New ToolStripButton()
        ToolStripButton_UpdateNotification = New ToolStripButton()
        ToolStripSeparator11 = New ToolStripSeparator()
        ToolStripDropDownButton_ActivateAllSeries = New ToolStripDropDownButton()
        ToolStripMenuItem_ActivateAllSeries = New ToolStripMenuItem()
        ToolStripMenuItem_DeactivateAllSeries = New ToolStripMenuItem()
        OpenFileDialog1 = New OpenFileDialog()
        SplitContainer1 = New SplitContainer()
        TChart2 = New Steema.TeeChart.TChart()
        TableLayoutPanel1 = New TableLayoutPanel()
        PanelNavigation = New Panel()
        Button_NavEnd = New Button()
        Button_NavStart = New Button()
        Label_Navigate = New Label()
        Button_NavForward = New Button()
        Button_NavBack = New Button()
        NumericUpDown_NavMultiplier = New NumericUpDown()
        ComboBox_NavIncrement = New ComboBox()
        TChart1 = New Steema.TeeChart.TChart()
        Panel_DisplayRange = New Panel()
        Label_Display = New Label()
        NumericUpDown_DisplayRangeMultiplier = New NumericUpDown()
        ComboBox_DisplayRangeUnit = New ComboBox()
        MaskedTextBox_NavEnd = New MaskedTextBox()
        MaskedTextBox_NavStart = New MaskedTextBox()
        SaveFileDialog1 = New SaveFileDialog()
        ProgressBar1 = New ProgressBar()
        ToolTip1 = New ToolTip(components)
        ToolStripSeparator1 = New ToolStripSeparator()
        toolStripSeparator = New ToolStripSeparator()
        ToolStripSeparator2 = New ToolStripSeparator()
        StatusStrip1 = New StatusStrip()
        ToolStrip1 = New ToolStrip()
        ToolStripSeparator6 = New ToolStripSeparator()
        ToolStripSeparator5 = New ToolStripSeparator()
        ToolStripSeparator4 = New ToolStripSeparator()
        ToolStripSeparator3 = New ToolStripSeparator()
        StatusStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        PanelNavigation.SuspendLayout()
        CType(NumericUpDown_NavMultiplier, ComponentModel.ISupportInitialize).BeginInit()
        Panel_DisplayRange.SuspendLayout()
        CType(NumericUpDown_DisplayRangeMultiplier, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 39)
        ' 
        ' toolStripSeparator
        ' 
        toolStripSeparator.Name = "toolStripSeparator"
        toolStripSeparator.Size = New Size(6, 39)
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 39)
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel_Errors, ToolStripStatusLabel_Warnings, ToolStripStatusLabel_Log})
        StatusStrip1.Location = New Point(0, 741)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 16, 0)
        StatusStrip1.RightToLeft = RightToLeft.No
        StatusStrip1.ShowItemToolTips = True
        StatusStrip1.Size = New Size(1101, 22)
        StatusStrip1.SizingGrip = False
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel_Errors
        ' 
        ToolStripStatusLabel_Errors.Image = CType(resources.GetObject("ToolStripStatusLabel_Errors.Image"), Image)
        ToolStripStatusLabel_Errors.IsLink = True
        ToolStripStatusLabel_Errors.LinkBehavior = LinkBehavior.NeverUnderline
        ToolStripStatusLabel_Errors.LinkColor = SystemColors.ControlDarkDark
        ToolStripStatusLabel_Errors.Name = "ToolStripStatusLabel_Errors"
        ToolStripStatusLabel_Errors.Size = New Size(29, 17)
        ToolStripStatusLabel_Errors.Text = "0"
        ToolStripStatusLabel_Errors.ToolTipText = "Errors"
        ' 
        ' ToolStripStatusLabel_Warnings
        ' 
        ToolStripStatusLabel_Warnings.Image = CType(resources.GetObject("ToolStripStatusLabel_Warnings.Image"), Image)
        ToolStripStatusLabel_Warnings.IsLink = True
        ToolStripStatusLabel_Warnings.LinkBehavior = LinkBehavior.NeverUnderline
        ToolStripStatusLabel_Warnings.LinkColor = SystemColors.ControlDarkDark
        ToolStripStatusLabel_Warnings.Name = "ToolStripStatusLabel_Warnings"
        ToolStripStatusLabel_Warnings.Size = New Size(29, 17)
        ToolStripStatusLabel_Warnings.Text = "0"
        ToolStripStatusLabel_Warnings.ToolTipText = "Warnings"
        ' 
        ' ToolStripStatusLabel_Log
        ' 
        ToolStripStatusLabel_Log.ActiveLinkColor = SystemColors.ControlDarkDark
        ToolStripStatusLabel_Log.IsLink = True
        ToolStripStatusLabel_Log.LinkBehavior = LinkBehavior.NeverUnderline
        ToolStripStatusLabel_Log.LinkColor = SystemColors.ControlDarkDark
        ToolStripStatusLabel_Log.Margin = New Padding(3, 3, 0, 2)
        ToolStripStatusLabel_Log.Name = "ToolStripStatusLabel_Log"
        ToolStripStatusLabel_Log.Size = New Size(0, 17)
        ToolStripStatusLabel_Log.ToolTipText = "Show log"
        ToolStripStatusLabel_Log.VisitedLinkColor = SystemColors.ControlDarkDark
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.AutoSize = False
        ToolStrip1.BackColor = SystemColors.Control
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton_New, ToolStripDropDownButton_Open, ToolStripDropDownButton_Save, ToolStripSeparator4, ToolStripButton_Settings, ToolStripSeparator12, ToolStripButton_Copy, ToolStripButton_Print, ToolStripSeparator2, ToolStripButton_Cut, ToolStripButton_Merge, ToolStripSeparator8, ToolStripButton_ShowNaNValues, ToolStripButton_ConvertErrorValues, ToolStripButton_RemoveNaNValues, ToolStripSeparator7, ToolStripButton_Analysis, ToolStripSeparator1, ToolStripButton_EditChart, ToolStripDropDownButton_ColorPalette, ToolStripSeparator9, ToolStripButton_Properties, ToolStripButton_TimeseriesValues, ToolStripSeparator10, ToolStripButton_AxisDialog, ToolStripButton_AutoAdjustYAxes, ToolStripSeparator13, ToolStripButton_AddMarkers, ToolStripButton_RemoveMarkers, ToolStripSeparator3, ToolStripButton_ToggleOverview, ToolStripButton_ToggleNavigation, toolStripSeparator, ToolStripButton_ZoomIn, ToolStripButton_ZoomOut, ToolStripButton_ZoomPrevious, ToolStripDropDownButton_Help, ToolStripButton_ZoomNext, ToolStripDropDownButton_ZoomToSeries, ToolStripButton_ZoomAll, ToolStripButton_UpdateNotification, ToolStripSeparator11, ToolStripDropDownButton_ActivateAllSeries})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1101, 39)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton_New
        ' 
        ToolStripButton_New.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_New.Image = CType(resources.GetObject("ToolStripButton_New.Image"), Image)
        ToolStripButton_New.ImageTransparentColor = Color.Magenta
        ToolStripButton_New.Name = "ToolStripButton_New"
        ToolStripButton_New.Size = New Size(23, 36)
        ToolStripButton_New.Text = "&New"
        ' 
        ' ToolStripDropDownButton_Open
        ' 
        ToolStripDropDownButton_Open.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton_Open.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem_ImportSeries, ToolStripMenuItem_EnterSeries, ToolStripMenuItem_PasteFromClipboard, ToolStripSeparator6, ToolStripMenuItem_LoadTEN, ToolStripSeparator5, ToolStripMenuItem_ReloadFromFiles, ToolStripMenuItem_RecentlyUsedFiles})
        ToolStripDropDownButton_Open.Image = CType(resources.GetObject("ToolStripDropDownButton_Open.Image"), Image)
        ToolStripDropDownButton_Open.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton_Open.Name = "ToolStripDropDownButton_Open"
        ToolStripDropDownButton_Open.Size = New Size(29, 36)
        ToolStripDropDownButton_Open.Text = "Open"
        ' 
        ' ToolStripMenuItem_ImportSeries
        ' 
        ToolStripMenuItem_ImportSeries.Image = CType(resources.GetObject("ToolStripMenuItem_ImportSeries.Image"), Image)
        ToolStripMenuItem_ImportSeries.Name = "ToolStripMenuItem_ImportSeries"
        ToolStripMenuItem_ImportSeries.Size = New Size(231, 22)
        ToolStripMenuItem_ImportSeries.Text = "Import time series"
        ' 
        ' ToolStripMenuItem_EnterSeries
        ' 
        ToolStripMenuItem_EnterSeries.Image = CType(resources.GetObject("ToolStripMenuItem_EnterSeries.Image"), Image)
        ToolStripMenuItem_EnterSeries.Name = "ToolStripMenuItem_EnterSeries"
        ToolStripMenuItem_EnterSeries.Size = New Size(231, 22)
        ToolStripMenuItem_EnterSeries.Text = "Enter time series"
        ' 
        ' ToolStripMenuItem_PasteFromClipboard
        ' 
        ToolStripMenuItem_PasteFromClipboard.Image = CType(resources.GetObject("ToolStripMenuItem_PasteFromClipboard.Image"), Image)
        ToolStripMenuItem_PasteFromClipboard.Name = "ToolStripMenuItem_PasteFromClipboard"
        ToolStripMenuItem_PasteFromClipboard.Size = New Size(231, 22)
        ToolStripMenuItem_PasteFromClipboard.Text = "Paste from Clipboard (Ctrl+V)"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(228, 6)
        ' 
        ' ToolStripMenuItem_LoadTEN
        ' 
        ToolStripMenuItem_LoadTEN.Image = CType(resources.GetObject("ToolStripMenuItem_LoadTEN.Image"), Image)
        ToolStripMenuItem_LoadTEN.Name = "ToolStripMenuItem_LoadTEN"
        ToolStripMenuItem_LoadTEN.Size = New Size(231, 22)
        ToolStripMenuItem_LoadTEN.Text = "Load chart (*.TEN)"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(228, 6)
        ' 
        ' ToolStripMenuItem_ReloadFromFiles
        ' 
        ToolStripMenuItem_ReloadFromFiles.Image = CType(resources.GetObject("ToolStripMenuItem_ReloadFromFiles.Image"), Image)
        ToolStripMenuItem_ReloadFromFiles.Name = "ToolStripMenuItem_ReloadFromFiles"
        ToolStripMenuItem_ReloadFromFiles.Size = New Size(231, 22)
        ToolStripMenuItem_ReloadFromFiles.Text = "Reload from files"
        ' 
        ' ToolStripMenuItem_RecentlyUsedFiles
        ' 
        ToolStripMenuItem_RecentlyUsedFiles.Name = "ToolStripMenuItem_RecentlyUsedFiles"
        ToolStripMenuItem_RecentlyUsedFiles.Size = New Size(231, 22)
        ToolStripMenuItem_RecentlyUsedFiles.Text = "Recently used files"
        ' 
        ' ToolStripDropDownButton_Save
        ' 
        ToolStripDropDownButton_Save.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton_Save.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem_SaveProjectFile, ToolStripMenuItem_SaveChart, ToolStripMenuItem_ExportSeries})
        ToolStripDropDownButton_Save.Image = CType(resources.GetObject("ToolStripDropDownButton_Save.Image"), Image)
        ToolStripDropDownButton_Save.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton_Save.Name = "ToolStripDropDownButton_Save"
        ToolStripDropDownButton_Save.Size = New Size(29, 36)
        ToolStripDropDownButton_Save.Text = "&Save"
        ' 
        ' ToolStripMenuItem_SaveProjectFile
        ' 
        ToolStripMenuItem_SaveProjectFile.Image = CType(resources.GetObject("ToolStripMenuItem_SaveProjectFile.Image"), Image)
        ToolStripMenuItem_SaveProjectFile.Name = "ToolStripMenuItem_SaveProjectFile"
        ToolStripMenuItem_SaveProjectFile.Size = New Size(167, 22)
        ToolStripMenuItem_SaveProjectFile.Text = "Save project file"
        ' 
        ' ToolStripMenuItem_SaveChart
        ' 
        ToolStripMenuItem_SaveChart.Image = CType(resources.GetObject("ToolStripMenuItem_SaveChart.Image"), Image)
        ToolStripMenuItem_SaveChart.Name = "ToolStripMenuItem_SaveChart"
        ToolStripMenuItem_SaveChart.Size = New Size(167, 22)
        ToolStripMenuItem_SaveChart.Text = "Save chart"
        ' 
        ' ToolStripMenuItem_ExportSeries
        ' 
        ToolStripMenuItem_ExportSeries.Image = CType(resources.GetObject("ToolStripMenuItem_ExportSeries.Image"), Image)
        ToolStripMenuItem_ExportSeries.Name = "ToolStripMenuItem_ExportSeries"
        ToolStripMenuItem_ExportSeries.Size = New Size(167, 22)
        ToolStripMenuItem_ExportSeries.Text = "Export time series"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_Settings
        ' 
        ToolStripButton_Settings.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Settings.Image = CType(resources.GetObject("ToolStripButton_Settings.Image"), Image)
        ToolStripButton_Settings.ImageTransparentColor = Color.Magenta
        ToolStripButton_Settings.Name = "ToolStripButton_Settings"
        ToolStripButton_Settings.Size = New Size(23, 36)
        ToolStripButton_Settings.Text = "Settings"
        ' 
        ' ToolStripSeparator12
        ' 
        ToolStripSeparator12.Name = "ToolStripSeparator12"
        ToolStripSeparator12.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_Copy
        ' 
        ToolStripButton_Copy.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Copy.Image = CType(resources.GetObject("ToolStripButton_Copy.Image"), Image)
        ToolStripButton_Copy.ImageTransparentColor = Color.Magenta
        ToolStripButton_Copy.Name = "ToolStripButton_Copy"
        ToolStripButton_Copy.Size = New Size(23, 36)
        ToolStripButton_Copy.Text = "&Copy chart to clipboard (PNG)"
        ' 
        ' ToolStripButton_Print
        ' 
        ToolStripButton_Print.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Print.Image = CType(resources.GetObject("ToolStripButton_Print.Image"), Image)
        ToolStripButton_Print.ImageTransparentColor = Color.Magenta
        ToolStripButton_Print.Name = "ToolStripButton_Print"
        ToolStripButton_Print.Size = New Size(23, 36)
        ToolStripButton_Print.Text = "&Print"
        ' 
        ' ToolStripButton_Cut
        ' 
        ToolStripButton_Cut.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Cut.Image = CType(resources.GetObject("ToolStripButton_Cut.Image"), Image)
        ToolStripButton_Cut.ImageTransparentColor = Color.Magenta
        ToolStripButton_Cut.Name = "ToolStripButton_Cut"
        ToolStripButton_Cut.Size = New Size(23, 36)
        ToolStripButton_Cut.Text = "Cut time series"
        ' 
        ' ToolStripButton_Merge
        ' 
        ToolStripButton_Merge.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Merge.Image = CType(resources.GetObject("ToolStripButton_Merge.Image"), Image)
        ToolStripButton_Merge.ImageTransparentColor = Color.Magenta
        ToolStripButton_Merge.Name = "ToolStripButton_Merge"
        ToolStripButton_Merge.Size = New Size(23, 36)
        ToolStripButton_Merge.Text = "Merge"
        ToolStripButton_Merge.ToolTipText = "Merge time series"
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_ShowNaNValues
        ' 
        ToolStripButton_ShowNaNValues.CheckOnClick = True
        ToolStripButton_ShowNaNValues.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ShowNaNValues.Image = CType(resources.GetObject("ToolStripButton_ShowNaNValues.Image"), Image)
        ToolStripButton_ShowNaNValues.ImageTransparentColor = Color.Magenta
        ToolStripButton_ShowNaNValues.Name = "ToolStripButton_ShowNaNValues"
        ToolStripButton_ShowNaNValues.Size = New Size(23, 36)
        ToolStripButton_ShowNaNValues.Text = "Visualize NaN values for the currently active series"
        ' 
        ' ToolStripButton_ConvertErrorValues
        ' 
        ToolStripButton_ConvertErrorValues.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ConvertErrorValues.Image = CType(resources.GetObject("ToolStripButton_ConvertErrorValues.Image"), Image)
        ToolStripButton_ConvertErrorValues.ImageTransparentColor = Color.Magenta
        ToolStripButton_ConvertErrorValues.Name = "ToolStripButton_ConvertErrorValues"
        ToolStripButton_ConvertErrorValues.Size = New Size(23, 36)
        ToolStripButton_ConvertErrorValues.Text = "Convert error values"
        ' 
        ' ToolStripButton_RemoveNaNValues
        ' 
        ToolStripButton_RemoveNaNValues.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_RemoveNaNValues.Image = CType(resources.GetObject("ToolStripButton_RemoveNaNValues.Image"), Image)
        ToolStripButton_RemoveNaNValues.ImageTransparentColor = Color.Magenta
        ToolStripButton_RemoveNaNValues.Name = "ToolStripButton_RemoveNaNValues"
        ToolStripButton_RemoveNaNValues.Size = New Size(23, 36)
        ToolStripButton_RemoveNaNValues.Text = "Remove nodes with NaN values"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_Analysis
        ' 
        ToolStripButton_Analysis.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Analysis.Image = CType(resources.GetObject("ToolStripButton_Analysis.Image"), Image)
        ToolStripButton_Analysis.ImageTransparentColor = Color.Magenta
        ToolStripButton_Analysis.Name = "ToolStripButton_Analysis"
        ToolStripButton_Analysis.Size = New Size(23, 36)
        ToolStripButton_Analysis.Text = "Perform an analysis"
        ' 
        ' ToolStripButton_EditChart
        ' 
        ToolStripButton_EditChart.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_EditChart.Image = CType(resources.GetObject("ToolStripButton_EditChart.Image"), Image)
        ToolStripButton_EditChart.ImageTransparentColor = SystemColors.Control
        ToolStripButton_EditChart.Name = "ToolStripButton_EditChart"
        ToolStripButton_EditChart.Size = New Size(23, 36)
        ToolStripButton_EditChart.Text = "Edit chart"
        ' 
        ' ToolStripDropDownButton_ColorPalette
        ' 
        ToolStripDropDownButton_ColorPalette.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton_ColorPalette.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem_ColorPaletteMaterial, ToolStripMenuItem_ColorPaletteDistinct, ToolStripMenuItem_ColorPaletteWheel, ToolStripMenuItem_ColorPaletteRandom})
        ToolStripDropDownButton_ColorPalette.Image = CType(resources.GetObject("ToolStripDropDownButton_ColorPalette.Image"), Image)
        ToolStripDropDownButton_ColorPalette.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton_ColorPalette.Name = "ToolStripDropDownButton_ColorPalette"
        ToolStripDropDownButton_ColorPalette.Size = New Size(29, 36)
        ToolStripDropDownButton_ColorPalette.Text = "Set color palette"
        ' 
        ' ToolStripMenuItem_ColorPaletteMaterial
        ' 
        ToolStripMenuItem_ColorPaletteMaterial.Name = "ToolStripMenuItem_ColorPaletteMaterial"
        ToolStripMenuItem_ColorPaletteMaterial.Size = New Size(139, 22)
        ToolStripMenuItem_ColorPaletteMaterial.Text = "Material"
        ' 
        ' ToolStripMenuItem_ColorPaletteDistinct
        ' 
        ToolStripMenuItem_ColorPaletteDistinct.Name = "ToolStripMenuItem_ColorPaletteDistinct"
        ToolStripMenuItem_ColorPaletteDistinct.Size = New Size(139, 22)
        ToolStripMenuItem_ColorPaletteDistinct.Text = "Distinct"
        ' 
        ' ToolStripMenuItem_ColorPaletteWheel
        ' 
        ToolStripMenuItem_ColorPaletteWheel.Name = "ToolStripMenuItem_ColorPaletteWheel"
        ToolStripMenuItem_ColorPaletteWheel.Size = New Size(139, 22)
        ToolStripMenuItem_ColorPaletteWheel.Text = "Color Wheel"
        ' 
        ' ToolStripMenuItem_ColorPaletteRandom
        ' 
        ToolStripMenuItem_ColorPaletteRandom.Name = "ToolStripMenuItem_ColorPaletteRandom"
        ToolStripMenuItem_ColorPaletteRandom.Size = New Size(139, 22)
        ToolStripMenuItem_ColorPaletteRandom.Text = "Random"
        ' 
        ' ToolStripSeparator9
        ' 
        ToolStripSeparator9.Name = "ToolStripSeparator9"
        ToolStripSeparator9.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_Properties
        ' 
        ToolStripButton_Properties.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_Properties.Image = CType(resources.GetObject("ToolStripButton_Properties.Image"), Image)
        ToolStripButton_Properties.ImageTransparentColor = Color.Magenta
        ToolStripButton_Properties.Name = "ToolStripButton_Properties"
        ToolStripButton_Properties.Size = New Size(23, 36)
        ToolStripButton_Properties.Text = "Properties"
        ToolStripButton_Properties.ToolTipText = "Show time series properties"
        ' 
        ' ToolStripButton_TimeseriesValues
        ' 
        ToolStripButton_TimeseriesValues.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_TimeseriesValues.Image = CType(resources.GetObject("ToolStripButton_TimeseriesValues.Image"), Image)
        ToolStripButton_TimeseriesValues.ImageTransparentColor = Color.Magenta
        ToolStripButton_TimeseriesValues.Name = "ToolStripButton_TimeseriesValues"
        ToolStripButton_TimeseriesValues.Size = New Size(23, 36)
        ToolStripButton_TimeseriesValues.Text = "Values"
        ToolStripButton_TimeseriesValues.ToolTipText = "Show time series values"
        ' 
        ' ToolStripSeparator10
        ' 
        ToolStripSeparator10.Name = "ToolStripSeparator10"
        ToolStripSeparator10.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_AxisDialog
        ' 
        ToolStripButton_AxisDialog.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_AxisDialog.Image = CType(resources.GetObject("ToolStripButton_AxisDialog.Image"), Image)
        ToolStripButton_AxisDialog.ImageTransparentColor = Color.Magenta
        ToolStripButton_AxisDialog.Name = "ToolStripButton_AxisDialog"
        ToolStripButton_AxisDialog.Size = New Size(23, 36)
        ToolStripButton_AxisDialog.Text = "ToolStripButton_AxisDialog"
        ToolStripButton_AxisDialog.ToolTipText = "Manage Axes"
        ' 
        ' ToolStripButton_AutoAdjustYAxes
        ' 
        ToolStripButton_AutoAdjustYAxes.CheckOnClick = True
        ToolStripButton_AutoAdjustYAxes.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_AutoAdjustYAxes.Image = CType(resources.GetObject("ToolStripButton_AutoAdjustYAxes.Image"), Image)
        ToolStripButton_AutoAdjustYAxes.ImageTransparentColor = Color.Magenta
        ToolStripButton_AutoAdjustYAxes.Name = "ToolStripButton_AutoAdjustYAxes"
        ToolStripButton_AutoAdjustYAxes.Size = New Size(23, 36)
        ToolStripButton_AutoAdjustYAxes.Text = "Auto-adjust Y-axes to current view"
        ' 
        ' ToolStripSeparator13
        ' 
        ToolStripSeparator13.Name = "ToolStripSeparator13"
        ToolStripSeparator13.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_AddMarkers
        ' 
        ToolStripButton_AddMarkers.CheckOnClick = True
        ToolStripButton_AddMarkers.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_AddMarkers.Image = CType(resources.GetObject("ToolStripButton_AddMarkers.Image"), Image)
        ToolStripButton_AddMarkers.ImageTransparentColor = Color.Magenta
        ToolStripButton_AddMarkers.Name = "ToolStripButton_AddMarkers"
        ToolStripButton_AddMarkers.Size = New Size(23, 36)
        ToolStripButton_AddMarkers.Text = "Add markers"
        ToolStripButton_AddMarkers.TextImageRelation = TextImageRelation.ImageAboveText
        ToolStripButton_AddMarkers.ToolTipText = "Shows markers with series values at the current mouse position. " & vbCrLf & "Click in the chart to make individual markers persistent or to remove them again."
        ' 
        ' ToolStripButton_RemoveMarkers
        ' 
        ToolStripButton_RemoveMarkers.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_RemoveMarkers.Image = CType(resources.GetObject("ToolStripButton_RemoveMarkers.Image"), Image)
        ToolStripButton_RemoveMarkers.ImageTransparentColor = Color.Magenta
        ToolStripButton_RemoveMarkers.Name = "ToolStripButton_RemoveMarkers"
        ToolStripButton_RemoveMarkers.Size = New Size(23, 36)
        ToolStripButton_RemoveMarkers.Text = "Remove all markers"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 39)
        ' 
        ' ToolStripButton_ToggleOverview
        ' 
        ToolStripButton_ToggleOverview.Checked = True
        ToolStripButton_ToggleOverview.CheckOnClick = True
        ToolStripButton_ToggleOverview.CheckState = CheckState.Checked
        ToolStripButton_ToggleOverview.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ToggleOverview.Image = CType(resources.GetObject("ToolStripButton_ToggleOverview.Image"), Image)
        ToolStripButton_ToggleOverview.ImageTransparentColor = Color.Magenta
        ToolStripButton_ToggleOverview.Name = "ToolStripButton_ToggleOverview"
        ToolStripButton_ToggleOverview.Size = New Size(23, 36)
        ToolStripButton_ToggleOverview.Text = "Toggle overview"
        ToolStripButton_ToggleOverview.ToolTipText = "Toggle overview"
        ' 
        ' ToolStripButton_ToggleNavigation
        ' 
        ToolStripButton_ToggleNavigation.Checked = True
        ToolStripButton_ToggleNavigation.CheckOnClick = True
        ToolStripButton_ToggleNavigation.CheckState = CheckState.Checked
        ToolStripButton_ToggleNavigation.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ToggleNavigation.Image = CType(resources.GetObject("ToolStripButton_ToggleNavigation.Image"), Image)
        ToolStripButton_ToggleNavigation.ImageTransparentColor = Color.Magenta
        ToolStripButton_ToggleNavigation.Name = "ToolStripButton_ToggleNavigation"
        ToolStripButton_ToggleNavigation.Size = New Size(23, 36)
        ToolStripButton_ToggleNavigation.Text = "Toggle navigation controls"
        ' 
        ' ToolStripButton_ZoomIn
        ' 
        ToolStripButton_ZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ZoomIn.Image = CType(resources.GetObject("ToolStripButton_ZoomIn.Image"), Image)
        ToolStripButton_ZoomIn.ImageTransparentColor = Color.Magenta
        ToolStripButton_ZoomIn.Name = "ToolStripButton_ZoomIn"
        ToolStripButton_ZoomIn.Size = New Size(23, 36)
        ToolStripButton_ZoomIn.Text = "Zoom in"
        ' 
        ' ToolStripButton_ZoomOut
        ' 
        ToolStripButton_ZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ZoomOut.Image = CType(resources.GetObject("ToolStripButton_ZoomOut.Image"), Image)
        ToolStripButton_ZoomOut.ImageTransparentColor = Color.Magenta
        ToolStripButton_ZoomOut.Name = "ToolStripButton_ZoomOut"
        ToolStripButton_ZoomOut.Size = New Size(23, 36)
        ToolStripButton_ZoomOut.Text = "Zoom out"
        ' 
        ' ToolStripButton_ZoomPrevious
        ' 
        ToolStripButton_ZoomPrevious.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ZoomPrevious.Enabled = False
        ToolStripButton_ZoomPrevious.Image = CType(resources.GetObject("ToolStripButton_ZoomPrevious.Image"), Image)
        ToolStripButton_ZoomPrevious.ImageTransparentColor = Color.Magenta
        ToolStripButton_ZoomPrevious.Name = "ToolStripButton_ZoomPrevious"
        ToolStripButton_ZoomPrevious.Size = New Size(23, 36)
        ToolStripButton_ZoomPrevious.Text = "Zoom previous"
        ToolStripButton_ZoomPrevious.ToolTipText = "Zoom to previous extent"
        ' 
        ' ToolStripDropDownButton_Help
        ' 
        ToolStripDropDownButton_Help.Alignment = ToolStripItemAlignment.Right
        ToolStripDropDownButton_Help.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton_Help.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem_Help, ToolStripMenuItem_Changelog, ToolStripMenuItem_CheckForUpdate, ToolStripMenuItem_About})
        ToolStripDropDownButton_Help.Image = CType(resources.GetObject("ToolStripDropDownButton_Help.Image"), Image)
        ToolStripDropDownButton_Help.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton_Help.Name = "ToolStripDropDownButton_Help"
        ToolStripDropDownButton_Help.Size = New Size(29, 36)
        ToolStripDropDownButton_Help.Text = "Help"
        ' 
        ' ToolStripMenuItem_Help
        ' 
        ToolStripMenuItem_Help.Name = "ToolStripMenuItem_Help"
        ToolStripMenuItem_Help.Size = New Size(165, 22)
        ToolStripMenuItem_Help.Text = "Help"
        ' 
        ' ToolStripMenuItem_Changelog
        ' 
        ToolStripMenuItem_Changelog.Name = "ToolStripMenuItem_Changelog"
        ToolStripMenuItem_Changelog.Size = New Size(165, 22)
        ToolStripMenuItem_Changelog.Text = "Changelog"
        ' 
        ' ToolStripMenuItem_CheckForUpdate
        ' 
        ToolStripMenuItem_CheckForUpdate.Name = "ToolStripMenuItem_CheckForUpdate"
        ToolStripMenuItem_CheckForUpdate.Size = New Size(165, 22)
        ToolStripMenuItem_CheckForUpdate.Text = "Check for update"
        ' 
        ' ToolStripMenuItem_About
        ' 
        ToolStripMenuItem_About.Image = CType(resources.GetObject("ToolStripMenuItem_About.Image"), Image)
        ToolStripMenuItem_About.Name = "ToolStripMenuItem_About"
        ToolStripMenuItem_About.Size = New Size(165, 22)
        ToolStripMenuItem_About.Text = "About"
        ' 
        ' ToolStripButton_ZoomNext
        ' 
        ToolStripButton_ZoomNext.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ZoomNext.Enabled = False
        ToolStripButton_ZoomNext.Image = CType(resources.GetObject("ToolStripButton_ZoomNext.Image"), Image)
        ToolStripButton_ZoomNext.ImageTransparentColor = Color.Magenta
        ToolStripButton_ZoomNext.Name = "ToolStripButton_ZoomNext"
        ToolStripButton_ZoomNext.Size = New Size(23, 36)
        ToolStripButton_ZoomNext.Text = "Zoom next"
        ToolStripButton_ZoomNext.ToolTipText = "Zoom to next extent"
        ' 
        ' ToolStripDropDownButton_ZoomToSeries
        ' 
        ToolStripDropDownButton_ZoomToSeries.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton_ZoomToSeries.Image = CType(resources.GetObject("ToolStripDropDownButton_ZoomToSeries.Image"), Image)
        ToolStripDropDownButton_ZoomToSeries.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton_ZoomToSeries.Name = "ToolStripDropDownButton_ZoomToSeries"
        ToolStripDropDownButton_ZoomToSeries.Size = New Size(29, 36)
        ToolStripDropDownButton_ZoomToSeries.Text = "Zoom to timeseries"
        ' 
        ' ToolStripButton_ZoomAll
        ' 
        ToolStripButton_ZoomAll.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton_ZoomAll.Image = CType(resources.GetObject("ToolStripButton_ZoomAll.Image"), Image)
        ToolStripButton_ZoomAll.ImageTransparentColor = Color.Magenta
        ToolStripButton_ZoomAll.Name = "ToolStripButton_ZoomAll"
        ToolStripButton_ZoomAll.Size = New Size(23, 36)
        ToolStripButton_ZoomAll.Text = "Zoom all"
        ToolStripButton_ZoomAll.ToolTipText = "Zoom to full extent"
        ' 
        ' ToolStripButton_UpdateNotification
        ' 
        ToolStripButton_UpdateNotification.Alignment = ToolStripItemAlignment.Right
        ToolStripButton_UpdateNotification.Image = CType(resources.GetObject("ToolStripButton_UpdateNotification.Image"), Image)
        ToolStripButton_UpdateNotification.ImageTransparentColor = Color.Magenta
        ToolStripButton_UpdateNotification.Name = "ToolStripButton_UpdateNotification"
        ToolStripButton_UpdateNotification.Size = New Size(117, 36)
        ToolStripButton_UpdateNotification.Text = "Update available!"
        ToolStripButton_UpdateNotification.Visible = False
        ' 
        ' ToolStripSeparator11
        ' 
        ToolStripSeparator11.Name = "ToolStripSeparator11"
        ToolStripSeparator11.Size = New Size(6, 39)
        ' 
        ' ToolStripDropDownButton_ActivateAllSeries
        ' 
        ToolStripDropDownButton_ActivateAllSeries.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton_ActivateAllSeries.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem_ActivateAllSeries, ToolStripMenuItem_DeactivateAllSeries})
        ToolStripDropDownButton_ActivateAllSeries.Image = CType(resources.GetObject("ToolStripDropDownButton_ActivateAllSeries.Image"), Image)
        ToolStripDropDownButton_ActivateAllSeries.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton_ActivateAllSeries.Name = "ToolStripDropDownButton_ActivateAllSeries"
        ToolStripDropDownButton_ActivateAllSeries.Size = New Size(29, 36)
        ' 
        ' ToolStripMenuItem_ActivateAllSeries
        ' 
        ToolStripMenuItem_ActivateAllSeries.Image = CType(resources.GetObject("ToolStripMenuItem_ActivateAllSeries.Image"), Image)
        ToolStripMenuItem_ActivateAllSeries.Name = "ToolStripMenuItem_ActivateAllSeries"
        ToolStripMenuItem_ActivateAllSeries.Size = New Size(176, 22)
        ToolStripMenuItem_ActivateAllSeries.Text = "Activate all series"
        ' 
        ' ToolStripMenuItem_DeactivateAllSeries
        ' 
        ToolStripMenuItem_DeactivateAllSeries.Image = CType(resources.GetObject("ToolStripMenuItem_DeactivateAllSeries.Image"), Image)
        ToolStripMenuItem_DeactivateAllSeries.Name = "ToolStripMenuItem_DeactivateAllSeries"
        ToolStripMenuItem_DeactivateAllSeries.Size = New Size(176, 22)
        ToolStripMenuItem_DeactivateAllSeries.Text = "Deactivate all series"
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.Multiselect = True
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        SplitContainer1.BorderStyle = BorderStyle.Fixed3D
        SplitContainer1.Location = New Point(0, 39)
        SplitContainer1.Margin = New Padding(4, 3, 4, 3)
        SplitContainer1.Name = "SplitContainer1"
        SplitContainer1.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(TChart2)
        SplitContainer1.Panel1MinSize = 100
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(TableLayoutPanel1)
        SplitContainer1.Size = New Size(1101, 699)
        SplitContainer1.SplitterDistance = 161
        SplitContainer1.SplitterWidth = 5
        SplitContainer1.TabIndex = 1
        ' 
        ' TChart2
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
        TChart2.Axes.Bottom.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Bottom.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Labels.Brush.Color = Color.White
        TChart2.Axes.Bottom.Labels.Brush.Solid = True
        TChart2.Axes.Bottom.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.Axes.Bottom.Labels.Font.Brush.Solid = True
        TChart2.Axes.Bottom.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Bottom.Labels.Font.Size = 9
        TChart2.Axes.Bottom.Labels.Font.SizeFloat = 9F
        TChart2.Axes.Bottom.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Bottom.Labels.ImageBevel.Brush.Solid = True
        TChart2.Axes.Bottom.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Bottom.Labels.Shadow.Brush.Solid = True
        TChart2.Axes.Bottom.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Bottom.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Title.Brush.Color = Color.Silver
        TChart2.Axes.Bottom.Title.Brush.Solid = True
        TChart2.Axes.Bottom.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart2.Axes.Bottom.Title.Font.Brush.Solid = True
        TChart2.Axes.Bottom.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Bottom.Title.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Bottom.Title.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Bottom.Title.Font.Size = 11
        TChart2.Axes.Bottom.Title.Font.SizeFloat = 11F
        TChart2.Axes.Bottom.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Bottom.Title.ImageBevel.Brush.Solid = True
        TChart2.Axes.Bottom.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Bottom.Title.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Bottom.Title.Shadow.Brush.Solid = True
        TChart2.Axes.Bottom.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Depth.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Labels.Brush.Color = Color.White
        TChart2.Axes.Depth.Labels.Brush.Solid = True
        TChart2.Axes.Depth.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.Axes.Depth.Labels.Font.Brush.Solid = True
        TChart2.Axes.Depth.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Depth.Labels.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Depth.Labels.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Depth.Labels.Font.Size = 9
        TChart2.Axes.Depth.Labels.Font.SizeFloat = 9F
        TChart2.Axes.Depth.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Depth.Labels.ImageBevel.Brush.Solid = True
        TChart2.Axes.Depth.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Depth.Labels.Shadow.Brush.Solid = True
        TChart2.Axes.Depth.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Depth.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Title.Brush.Color = Color.Silver
        TChart2.Axes.Depth.Title.Brush.Solid = True
        TChart2.Axes.Depth.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart2.Axes.Depth.Title.Font.Brush.Solid = True
        TChart2.Axes.Depth.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Depth.Title.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Depth.Title.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Depth.Title.Font.Size = 11
        TChart2.Axes.Depth.Title.Font.SizeFloat = 11F
        TChart2.Axes.Depth.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Depth.Title.ImageBevel.Brush.Solid = True
        TChart2.Axes.Depth.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Depth.Title.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Depth.Title.Shadow.Brush.Solid = True
        TChart2.Axes.Depth.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.DepthTop.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Labels.Brush.Color = Color.White
        TChart2.Axes.DepthTop.Labels.Brush.Solid = True
        TChart2.Axes.DepthTop.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.Axes.DepthTop.Labels.Font.Brush.Solid = True
        TChart2.Axes.DepthTop.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = True
        TChart2.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = True
        TChart2.Axes.DepthTop.Labels.Font.Size = 9
        TChart2.Axes.DepthTop.Labels.Font.SizeFloat = 9F
        TChart2.Axes.DepthTop.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = True
        TChart2.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.DepthTop.Labels.Shadow.Brush.Solid = True
        TChart2.Axes.DepthTop.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.DepthTop.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Title.Brush.Color = Color.Silver
        TChart2.Axes.DepthTop.Title.Brush.Solid = True
        TChart2.Axes.DepthTop.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart2.Axes.DepthTop.Title.Font.Brush.Solid = True
        TChart2.Axes.DepthTop.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = True
        TChart2.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = True
        TChart2.Axes.DepthTop.Title.Font.Size = 11
        TChart2.Axes.DepthTop.Title.Font.SizeFloat = 11F
        TChart2.Axes.DepthTop.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.DepthTop.Title.ImageBevel.Brush.Solid = True
        TChart2.Axes.DepthTop.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.DepthTop.Title.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.DepthTop.Title.Shadow.Brush.Solid = True
        TChart2.Axes.DepthTop.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Left.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Labels.Brush.Color = Color.White
        TChart2.Axes.Left.Labels.Brush.Solid = True
        TChart2.Axes.Left.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.Axes.Left.Labels.Font.Brush.Solid = True
        TChart2.Axes.Left.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Left.Labels.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Left.Labels.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Left.Labels.Font.Size = 9
        TChart2.Axes.Left.Labels.Font.SizeFloat = 9F
        TChart2.Axes.Left.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Left.Labels.ImageBevel.Brush.Solid = True
        TChart2.Axes.Left.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Left.Labels.Shadow.Brush.Solid = True
        TChart2.Axes.Left.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Left.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Title.Brush.Color = Color.Silver
        TChart2.Axes.Left.Title.Brush.Solid = True
        TChart2.Axes.Left.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart2.Axes.Left.Title.Font.Brush.Solid = True
        TChart2.Axes.Left.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Left.Title.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Left.Title.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Left.Title.Font.Size = 11
        TChart2.Axes.Left.Title.Font.SizeFloat = 11F
        TChart2.Axes.Left.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Left.Title.ImageBevel.Brush.Solid = True
        TChart2.Axes.Left.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Left.Title.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Left.Title.Shadow.Brush.Solid = True
        TChart2.Axes.Left.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Right.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Labels.Brush.Color = Color.White
        TChart2.Axes.Right.Labels.Brush.Solid = True
        TChart2.Axes.Right.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.Axes.Right.Labels.Font.Brush.Solid = True
        TChart2.Axes.Right.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Right.Labels.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Right.Labels.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Right.Labels.Font.Size = 9
        TChart2.Axes.Right.Labels.Font.SizeFloat = 9F
        TChart2.Axes.Right.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Right.Labels.ImageBevel.Brush.Solid = True
        TChart2.Axes.Right.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Right.Labels.Shadow.Brush.Solid = True
        TChart2.Axes.Right.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Right.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Title.Brush.Color = Color.Silver
        TChart2.Axes.Right.Title.Brush.Solid = True
        TChart2.Axes.Right.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart2.Axes.Right.Title.Font.Brush.Solid = True
        TChart2.Axes.Right.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Right.Title.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Right.Title.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Right.Title.Font.Size = 11
        TChart2.Axes.Right.Title.Font.SizeFloat = 11F
        TChart2.Axes.Right.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Right.Title.ImageBevel.Brush.Solid = True
        TChart2.Axes.Right.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Right.Title.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Right.Title.Shadow.Brush.Solid = True
        TChart2.Axes.Right.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Top.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Labels.Brush.Color = Color.White
        TChart2.Axes.Top.Labels.Brush.Solid = True
        TChart2.Axes.Top.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.Axes.Top.Labels.Font.Brush.Solid = True
        TChart2.Axes.Top.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Top.Labels.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Top.Labels.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Top.Labels.Font.Size = 9
        TChart2.Axes.Top.Labels.Font.SizeFloat = 9F
        TChart2.Axes.Top.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Top.Labels.ImageBevel.Brush.Solid = True
        TChart2.Axes.Top.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Top.Labels.Shadow.Brush.Solid = True
        TChart2.Axes.Top.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Axes.Top.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Title.Brush.Color = Color.Silver
        TChart2.Axes.Top.Title.Brush.Solid = True
        TChart2.Axes.Top.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart2.Axes.Top.Title.Font.Brush.Solid = True
        TChart2.Axes.Top.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Top.Title.Font.Shadow.Brush.Solid = True
        TChart2.Axes.Top.Title.Font.Shadow.Brush.Visible = True
        TChart2.Axes.Top.Title.Font.Size = 11
        TChart2.Axes.Top.Title.Font.SizeFloat = 11F
        TChart2.Axes.Top.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Axes.Top.Title.ImageBevel.Brush.Solid = True
        TChart2.Axes.Top.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Axes.Top.Title.Shadow.Brush.Color = Color.DarkGray
        TChart2.Axes.Top.Title.Shadow.Brush.Solid = True
        TChart2.Axes.Top.Title.Shadow.Brush.Visible = True
        TChart2.Dock = DockStyle.Fill
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Footer.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Footer.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Footer.Brush.Color = Color.Silver
        TChart2.Footer.Brush.Solid = True
        TChart2.Footer.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Footer.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Footer.Font.Brush.Color = Color.Red
        TChart2.Footer.Font.Brush.Solid = True
        TChart2.Footer.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Footer.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Footer.Font.Shadow.Brush.Solid = True
        TChart2.Footer.Font.Shadow.Brush.Visible = True
        TChart2.Footer.Font.Size = 8
        TChart2.Footer.Font.SizeFloat = 8F
        TChart2.Footer.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Footer.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Footer.ImageBevel.Brush.Solid = True
        TChart2.Footer.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Footer.Shadow.Brush.Color = Color.DarkGray
        TChart2.Footer.Shadow.Brush.Solid = True
        TChart2.Footer.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Header.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Header.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Header.Brush.Color = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        TChart2.Header.Brush.Solid = True
        TChart2.Header.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Header.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Header.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.Header.Font.Brush.Solid = True
        TChart2.Header.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Header.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Header.Font.Shadow.Brush.Solid = True
        TChart2.Header.Font.Shadow.Brush.Visible = True
        TChart2.Header.Font.Size = 12
        TChart2.Header.Font.SizeFloat = 12F
        TChart2.Header.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Header.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Header.ImageBevel.Brush.Solid = True
        TChart2.Header.ImageBevel.Brush.Visible = True
        TChart2.Header.Lines = New String() {"Overview chart"}
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Header.Shadow.Brush.Color = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        TChart2.Header.Shadow.Brush.Solid = True
        TChart2.Header.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Legend.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Legend.Brush.Color = Color.White
        TChart2.Legend.Brush.Solid = True
        TChart2.Legend.Brush.Visible = True
        TChart2.Legend.CheckBoxes = False
        ' 
        ' 
        ' 
        TChart2.Legend.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.Legend.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart2.Legend.Font.Brush.Solid = True
        TChart2.Legend.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Legend.Font.Shadow.Brush.Solid = True
        TChart2.Legend.Font.Shadow.Brush.Visible = True
        TChart2.Legend.Font.Size = 9
        TChart2.Legend.Font.SizeFloat = 9F
        TChart2.Legend.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Legend.ImageBevel.Brush.Solid = True
        TChart2.Legend.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Shadow.Brush.Color = Color.FromArgb(CByte(0), CByte(0), CByte(0))
        TChart2.Legend.Shadow.Brush.Solid = True
        TChart2.Legend.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Symbol.Shadow.Brush.Color = Color.DarkGray
        TChart2.Legend.Symbol.Shadow.Brush.Solid = True
        TChart2.Legend.Symbol.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Legend.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Legend.Title.Brush.Color = Color.White
        TChart2.Legend.Title.Brush.Solid = True
        TChart2.Legend.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Legend.Title.Font.Bold = True
        ' 
        ' 
        ' 
        TChart2.Legend.Title.Font.Brush.Color = Color.Black
        TChart2.Legend.Title.Font.Brush.Solid = True
        TChart2.Legend.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.Legend.Title.Font.Shadow.Brush.Solid = True
        TChart2.Legend.Title.Font.Shadow.Brush.Visible = True
        TChart2.Legend.Title.Font.Size = 8
        TChart2.Legend.Title.Font.SizeFloat = 8F
        TChart2.Legend.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Bold
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Legend.Title.ImageBevel.Brush.Solid = True
        TChart2.Legend.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Legend.Title.Shadow.Brush.Color = Color.DarkGray
        TChart2.Legend.Title.Shadow.Brush.Solid = True
        TChart2.Legend.Title.Shadow.Brush.Visible = True
        TChart2.Location = New Point(0, 0)
        TChart2.Margin = New Padding(4, 3, 4, 3)
        TChart2.Name = "TChart2"
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Panel.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Panel.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Panel.Brush.Color = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Panel.Brush.Solid = True
        TChart2.Panel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Panel.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Panel.ImageBevel.Brush.Solid = True
        TChart2.Panel.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Panel.Shadow.Brush.Color = Color.DarkGray
        TChart2.Panel.Shadow.Brush.Solid = True
        TChart2.Panel.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.Printer.Landscape = True
        Margins2.Bottom = 100
        Margins2.Left = 100
        Margins2.Right = 100
        Margins2.Top = 100
        TChart2.Printer.Margins = Margins2
        TChart2.Size = New Size(1097, 157)
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubFooter.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.SubFooter.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.SubFooter.Brush.Color = Color.Silver
        TChart2.SubFooter.Brush.Solid = True
        TChart2.SubFooter.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.SubFooter.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.SubFooter.Font.Brush.Color = Color.Red
        TChart2.SubFooter.Font.Brush.Solid = True
        TChart2.SubFooter.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubFooter.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.SubFooter.Font.Shadow.Brush.Solid = True
        TChart2.SubFooter.Font.Shadow.Brush.Visible = True
        TChart2.SubFooter.Font.Size = 8
        TChart2.SubFooter.Font.SizeFloat = 8F
        TChart2.SubFooter.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubFooter.ImageBevel.Brush.Color = Color.LightGray
        TChart2.SubFooter.ImageBevel.Brush.Solid = True
        TChart2.SubFooter.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubFooter.Shadow.Brush.Color = Color.DarkGray
        TChart2.SubFooter.Shadow.Brush.Solid = True
        TChart2.SubFooter.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubHeader.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.SubHeader.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.SubHeader.Brush.Color = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        TChart2.SubHeader.Brush.Solid = True
        TChart2.SubHeader.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart2.SubHeader.Font.Bold = False
        ' 
        ' 
        ' 
        TChart2.SubHeader.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart2.SubHeader.Font.Brush.Solid = True
        TChart2.SubHeader.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubHeader.Font.Shadow.Brush.Color = Color.DarkGray
        TChart2.SubHeader.Font.Shadow.Brush.Solid = True
        TChart2.SubHeader.Font.Shadow.Brush.Visible = True
        TChart2.SubHeader.Font.Size = 12
        TChart2.SubHeader.Font.SizeFloat = 12F
        TChart2.SubHeader.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubHeader.ImageBevel.Brush.Color = Color.LightGray
        TChart2.SubHeader.ImageBevel.Brush.Solid = True
        TChart2.SubHeader.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.SubHeader.Shadow.Brush.Color = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        TChart2.SubHeader.Shadow.Brush.Solid = True
        TChart2.SubHeader.Shadow.Brush.Visible = True
        TChart2.TabIndex = 0
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Back.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Walls.Back.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Walls.Back.Brush.Color = Color.Silver
        TChart2.Walls.Back.Brush.Solid = True
        TChart2.Walls.Back.Brush.Visible = False
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Back.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Walls.Back.ImageBevel.Brush.Solid = True
        TChart2.Walls.Back.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Back.Shadow.Brush.Color = Color.DarkGray
        TChart2.Walls.Back.Shadow.Brush.Solid = True
        TChart2.Walls.Back.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Bottom.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Walls.Bottom.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Walls.Bottom.Brush.Color = Color.White
        TChart2.Walls.Bottom.Brush.Solid = True
        TChart2.Walls.Bottom.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Bottom.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Walls.Bottom.ImageBevel.Brush.Solid = True
        TChart2.Walls.Bottom.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Bottom.Shadow.Brush.Color = Color.DarkGray
        TChart2.Walls.Bottom.Shadow.Brush.Solid = True
        TChart2.Walls.Bottom.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Left.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Walls.Left.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Walls.Left.Brush.Color = Color.LightYellow
        TChart2.Walls.Left.Brush.Solid = True
        TChart2.Walls.Left.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Left.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Walls.Left.ImageBevel.Brush.Solid = True
        TChart2.Walls.Left.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Left.Shadow.Brush.Color = Color.DarkGray
        TChart2.Walls.Left.Shadow.Brush.Solid = True
        TChart2.Walls.Left.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Right.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart2.Walls.Right.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart2.Walls.Right.Brush.Color = Color.LightYellow
        TChart2.Walls.Right.Brush.Solid = True
        TChart2.Walls.Right.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Right.ImageBevel.Brush.Color = Color.LightGray
        TChart2.Walls.Right.ImageBevel.Brush.Solid = True
        TChart2.Walls.Right.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Walls.Right.Shadow.Brush.Color = Color.DarkGray
        TChart2.Walls.Right.Shadow.Brush.Solid = True
        TChart2.Walls.Right.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart2.Zoom.Brush.Color = Color.LightBlue
        TChart2.Zoom.Brush.Solid = True
        TChart2.Zoom.Brush.Visible = True
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(PanelNavigation, 0, 0)
        TableLayoutPanel1.Controls.Add(TChart1, 0, 1)
        TableLayoutPanel1.Controls.Add(Panel_DisplayRange, 0, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Margin = New Padding(0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 44F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 42F))
        TableLayoutPanel1.Size = New Size(1097, 529)
        TableLayoutPanel1.TabIndex = 3
        ' 
        ' PanelNavigation
        ' 
        PanelNavigation.Controls.Add(Button_NavEnd)
        PanelNavigation.Controls.Add(Button_NavStart)
        PanelNavigation.Controls.Add(Label_Navigate)
        PanelNavigation.Controls.Add(Button_NavForward)
        PanelNavigation.Controls.Add(Button_NavBack)
        PanelNavigation.Controls.Add(NumericUpDown_NavMultiplier)
        PanelNavigation.Controls.Add(ComboBox_NavIncrement)
        PanelNavigation.Dock = DockStyle.Fill
        PanelNavigation.Location = New Point(1, 1)
        PanelNavigation.Margin = New Padding(0)
        PanelNavigation.Name = "PanelNavigation"
        PanelNavigation.Size = New Size(1095, 44)
        PanelNavigation.TabIndex = 0
        ' 
        ' Button_NavEnd
        ' 
        Button_NavEnd.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_NavEnd.Image = CType(resources.GetObject("Button_NavEnd.Image"), Image)
        Button_NavEnd.Location = New Point(1059, 9)
        Button_NavEnd.Name = "Button_NavEnd"
        Button_NavEnd.Size = New Size(27, 27)
        Button_NavEnd.TabIndex = 6
        ToolTip1.SetToolTip(Button_NavEnd, "Navigate to end")
        Button_NavEnd.UseVisualStyleBackColor = True
        ' 
        ' Button_NavStart
        ' 
        Button_NavStart.Image = CType(resources.GetObject("Button_NavStart.Image"), Image)
        Button_NavStart.Location = New Point(9, 9)
        Button_NavStart.Name = "Button_NavStart"
        Button_NavStart.Size = New Size(27, 27)
        Button_NavStart.TabIndex = 0
        ToolTip1.SetToolTip(Button_NavStart, "Navigate to start")
        Button_NavStart.UseVisualStyleBackColor = True
        ' 
        ' Label_Navigate
        ' 
        Label_Navigate.Anchor = AnchorStyles.Top
        Label_Navigate.AutoSize = True
        Label_Navigate.Location = New Point(444, 15)
        Label_Navigate.Name = "Label_Navigate"
        Label_Navigate.Size = New Size(57, 15)
        Label_Navigate.TabIndex = 2
        Label_Navigate.Text = "Navigate:"
        ' 
        ' Button_NavForward
        ' 
        Button_NavForward.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button_NavForward.Image = CType(resources.GetObject("Button_NavForward.Image"), Image)
        Button_NavForward.Location = New Point(973, 9)
        Button_NavForward.Name = "Button_NavForward"
        Button_NavForward.Size = New Size(80, 27)
        Button_NavForward.TabIndex = 5
        ToolTip1.SetToolTip(Button_NavForward, "Navigate forwards")
        Button_NavForward.UseVisualStyleBackColor = True
        ' 
        ' Button_NavBack
        ' 
        Button_NavBack.Image = CType(resources.GetObject("Button_NavBack.Image"), Image)
        Button_NavBack.Location = New Point(42, 10)
        Button_NavBack.Name = "Button_NavBack"
        Button_NavBack.Size = New Size(80, 27)
        Button_NavBack.TabIndex = 1
        ToolTip1.SetToolTip(Button_NavBack, "Navigate backwards")
        Button_NavBack.UseVisualStyleBackColor = True
        ' 
        ' NumericUpDown_NavMultiplier
        ' 
        NumericUpDown_NavMultiplier.Anchor = AnchorStyles.Top
        NumericUpDown_NavMultiplier.Location = New Point(507, 10)
        NumericUpDown_NavMultiplier.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_NavMultiplier.Name = "NumericUpDown_NavMultiplier"
        NumericUpDown_NavMultiplier.Size = New Size(47, 23)
        NumericUpDown_NavMultiplier.TabIndex = 3
        NumericUpDown_NavMultiplier.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' ComboBox_NavIncrement
        ' 
        ComboBox_NavIncrement.Anchor = AnchorStyles.Top
        ComboBox_NavIncrement.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_NavIncrement.FormattingEnabled = True
        ComboBox_NavIncrement.Items.AddRange(New Object() {"Centuries", "Decades", "Years", "Months", "Weeks", "Days", "Hours", "Minutes", "Seconds"})
        ComboBox_NavIncrement.Location = New Point(560, 9)
        ComboBox_NavIncrement.Name = "ComboBox_NavIncrement"
        ComboBox_NavIncrement.Size = New Size(94, 23)
        ComboBox_NavIncrement.TabIndex = 4
        ' 
        ' TChart1
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
        TChart1.Axes.Bottom.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Bottom.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Labels.Brush.Color = Color.White
        TChart1.Axes.Bottom.Labels.Brush.Solid = True
        TChart1.Axes.Bottom.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.Axes.Bottom.Labels.Font.Brush.Solid = True
        TChart1.Axes.Bottom.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Bottom.Labels.Font.Size = 9
        TChart1.Axes.Bottom.Labels.Font.SizeFloat = 9F
        TChart1.Axes.Bottom.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Bottom.Labels.ImageBevel.Brush.Solid = True
        TChart1.Axes.Bottom.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Bottom.Labels.Shadow.Brush.Solid = True
        TChart1.Axes.Bottom.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Bottom.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Title.Brush.Color = Color.Silver
        TChart1.Axes.Bottom.Title.Brush.Solid = True
        TChart1.Axes.Bottom.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart1.Axes.Bottom.Title.Font.Brush.Solid = True
        TChart1.Axes.Bottom.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Bottom.Title.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Bottom.Title.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Bottom.Title.Font.Size = 11
        TChart1.Axes.Bottom.Title.Font.SizeFloat = 11F
        TChart1.Axes.Bottom.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Bottom.Title.ImageBevel.Brush.Solid = True
        TChart1.Axes.Bottom.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Bottom.Title.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Bottom.Title.Shadow.Brush.Solid = True
        TChart1.Axes.Bottom.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Depth.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Labels.Brush.Color = Color.White
        TChart1.Axes.Depth.Labels.Brush.Solid = True
        TChart1.Axes.Depth.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.Axes.Depth.Labels.Font.Brush.Solid = True
        TChart1.Axes.Depth.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Depth.Labels.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Depth.Labels.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Depth.Labels.Font.Size = 9
        TChart1.Axes.Depth.Labels.Font.SizeFloat = 9F
        TChart1.Axes.Depth.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Depth.Labels.ImageBevel.Brush.Solid = True
        TChart1.Axes.Depth.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Depth.Labels.Shadow.Brush.Solid = True
        TChart1.Axes.Depth.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Depth.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Title.Brush.Color = Color.Silver
        TChart1.Axes.Depth.Title.Brush.Solid = True
        TChart1.Axes.Depth.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart1.Axes.Depth.Title.Font.Brush.Solid = True
        TChart1.Axes.Depth.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Depth.Title.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Depth.Title.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Depth.Title.Font.Size = 11
        TChart1.Axes.Depth.Title.Font.SizeFloat = 11F
        TChart1.Axes.Depth.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Depth.Title.ImageBevel.Brush.Solid = True
        TChart1.Axes.Depth.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Depth.Title.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Depth.Title.Shadow.Brush.Solid = True
        TChart1.Axes.Depth.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.DepthTop.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Labels.Brush.Color = Color.White
        TChart1.Axes.DepthTop.Labels.Brush.Solid = True
        TChart1.Axes.DepthTop.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.Axes.DepthTop.Labels.Font.Brush.Solid = True
        TChart1.Axes.DepthTop.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = True
        TChart1.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = True
        TChart1.Axes.DepthTop.Labels.Font.Size = 9
        TChart1.Axes.DepthTop.Labels.Font.SizeFloat = 9F
        TChart1.Axes.DepthTop.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = True
        TChart1.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.DepthTop.Labels.Shadow.Brush.Solid = True
        TChart1.Axes.DepthTop.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.DepthTop.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Title.Brush.Color = Color.Silver
        TChart1.Axes.DepthTop.Title.Brush.Solid = True
        TChart1.Axes.DepthTop.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart1.Axes.DepthTop.Title.Font.Brush.Solid = True
        TChart1.Axes.DepthTop.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = True
        TChart1.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = True
        TChart1.Axes.DepthTop.Title.Font.Size = 11
        TChart1.Axes.DepthTop.Title.Font.SizeFloat = 11F
        TChart1.Axes.DepthTop.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.DepthTop.Title.ImageBevel.Brush.Solid = True
        TChart1.Axes.DepthTop.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.DepthTop.Title.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.DepthTop.Title.Shadow.Brush.Solid = True
        TChart1.Axes.DepthTop.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Left.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Labels.Brush.Color = Color.White
        TChart1.Axes.Left.Labels.Brush.Solid = True
        TChart1.Axes.Left.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.Axes.Left.Labels.Font.Brush.Solid = True
        TChart1.Axes.Left.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Left.Labels.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Left.Labels.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Left.Labels.Font.Size = 9
        TChart1.Axes.Left.Labels.Font.SizeFloat = 9F
        TChart1.Axes.Left.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Left.Labels.ImageBevel.Brush.Solid = True
        TChart1.Axes.Left.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Left.Labels.Shadow.Brush.Solid = True
        TChart1.Axes.Left.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Left.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Title.Brush.Color = Color.Silver
        TChart1.Axes.Left.Title.Brush.Solid = True
        TChart1.Axes.Left.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart1.Axes.Left.Title.Font.Brush.Solid = True
        TChart1.Axes.Left.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Left.Title.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Left.Title.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Left.Title.Font.Size = 11
        TChart1.Axes.Left.Title.Font.SizeFloat = 11F
        TChart1.Axes.Left.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Left.Title.ImageBevel.Brush.Solid = True
        TChart1.Axes.Left.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Left.Title.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Left.Title.Shadow.Brush.Solid = True
        TChart1.Axes.Left.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Right.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Labels.Brush.Color = Color.White
        TChart1.Axes.Right.Labels.Brush.Solid = True
        TChart1.Axes.Right.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.Axes.Right.Labels.Font.Brush.Solid = True
        TChart1.Axes.Right.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Right.Labels.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Right.Labels.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Right.Labels.Font.Size = 9
        TChart1.Axes.Right.Labels.Font.SizeFloat = 9F
        TChart1.Axes.Right.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Right.Labels.ImageBevel.Brush.Solid = True
        TChart1.Axes.Right.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Right.Labels.Shadow.Brush.Solid = True
        TChart1.Axes.Right.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Right.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Title.Brush.Color = Color.Silver
        TChart1.Axes.Right.Title.Brush.Solid = True
        TChart1.Axes.Right.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart1.Axes.Right.Title.Font.Brush.Solid = True
        TChart1.Axes.Right.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Right.Title.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Right.Title.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Right.Title.Font.Size = 11
        TChart1.Axes.Right.Title.Font.SizeFloat = 11F
        TChart1.Axes.Right.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Right.Title.ImageBevel.Brush.Solid = True
        TChart1.Axes.Right.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Right.Title.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Right.Title.Shadow.Brush.Solid = True
        TChart1.Axes.Right.Title.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Labels.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Top.Labels.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Labels.Brush.Color = Color.White
        TChart1.Axes.Top.Labels.Brush.Solid = True
        TChart1.Axes.Top.Labels.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Labels.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Labels.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.Axes.Top.Labels.Font.Brush.Solid = True
        TChart1.Axes.Top.Labels.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Labels.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Top.Labels.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Top.Labels.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Top.Labels.Font.Size = 9
        TChart1.Axes.Top.Labels.Font.SizeFloat = 9F
        TChart1.Axes.Top.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Labels.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Top.Labels.ImageBevel.Brush.Solid = True
        TChart1.Axes.Top.Labels.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Labels.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Top.Labels.Shadow.Brush.Solid = True
        TChart1.Axes.Top.Labels.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Axes.Top.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Title.Brush.Color = Color.Silver
        TChart1.Axes.Top.Title.Brush.Solid = True
        TChart1.Axes.Top.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Title.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Title.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart1.Axes.Top.Title.Font.Brush.Solid = True
        TChart1.Axes.Top.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Top.Title.Font.Shadow.Brush.Solid = True
        TChart1.Axes.Top.Title.Font.Shadow.Brush.Visible = True
        TChart1.Axes.Top.Title.Font.Size = 11
        TChart1.Axes.Top.Title.Font.SizeFloat = 11F
        TChart1.Axes.Top.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Axes.Top.Title.ImageBevel.Brush.Solid = True
        TChart1.Axes.Top.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Axes.Top.Title.Shadow.Brush.Color = Color.DarkGray
        TChart1.Axes.Top.Title.Shadow.Brush.Solid = True
        TChart1.Axes.Top.Title.Shadow.Brush.Visible = True
        TChart1.Dock = DockStyle.Fill
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Footer.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Footer.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Footer.Brush.Color = Color.Silver
        TChart1.Footer.Brush.Solid = True
        TChart1.Footer.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Footer.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Footer.Font.Brush.Color = Color.Red
        TChart1.Footer.Font.Brush.Solid = True
        TChart1.Footer.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Footer.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Footer.Font.Shadow.Brush.Solid = True
        TChart1.Footer.Font.Shadow.Brush.Visible = True
        TChart1.Footer.Font.Size = 8
        TChart1.Footer.Font.SizeFloat = 8F
        TChart1.Footer.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Footer.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Footer.ImageBevel.Brush.Solid = True
        TChart1.Footer.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Footer.Shadow.Brush.Color = Color.DarkGray
        TChart1.Footer.Shadow.Brush.Solid = True
        TChart1.Footer.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Header.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Header.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Header.Brush.Color = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        TChart1.Header.Brush.Solid = True
        TChart1.Header.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Header.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Header.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.Header.Font.Brush.Solid = True
        TChart1.Header.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Header.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Header.Font.Shadow.Brush.Solid = True
        TChart1.Header.Font.Shadow.Brush.Visible = True
        TChart1.Header.Font.Size = 12
        TChart1.Header.Font.SizeFloat = 12F
        TChart1.Header.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Header.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Header.ImageBevel.Brush.Solid = True
        TChart1.Header.ImageBevel.Brush.Visible = True
        TChart1.Header.Lines = New String() {"Main chart"}
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Header.Shadow.Brush.Color = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        TChart1.Header.Shadow.Brush.Solid = True
        TChart1.Header.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Legend.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Legend.Brush.Color = Color.White
        TChart1.Legend.Brush.Solid = True
        TChart1.Legend.Brush.Visible = True
        TChart1.Legend.CheckBoxes = False
        ' 
        ' 
        ' 
        TChart1.Legend.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.Legend.Font.Brush.Color = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        TChart1.Legend.Font.Brush.Solid = True
        TChart1.Legend.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Legend.Font.Shadow.Brush.Solid = True
        TChart1.Legend.Font.Shadow.Brush.Visible = True
        TChart1.Legend.Font.Size = 9
        TChart1.Legend.Font.SizeFloat = 9F
        TChart1.Legend.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Legend.ImageBevel.Brush.Solid = True
        TChart1.Legend.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Shadow.Brush.Color = Color.FromArgb(CByte(0), CByte(0), CByte(0))
        TChart1.Legend.Shadow.Brush.Solid = True
        TChart1.Legend.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Symbol.Shadow.Brush.Color = Color.DarkGray
        TChart1.Legend.Symbol.Shadow.Brush.Solid = True
        TChart1.Legend.Symbol.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Title.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Legend.Title.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Legend.Title.Brush.Color = Color.White
        TChart1.Legend.Title.Brush.Solid = True
        TChart1.Legend.Title.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Legend.Title.Font.Bold = True
        ' 
        ' 
        ' 
        TChart1.Legend.Title.Font.Brush.Color = Color.Black
        TChart1.Legend.Title.Font.Brush.Solid = True
        TChart1.Legend.Title.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Title.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.Legend.Title.Font.Shadow.Brush.Solid = True
        TChart1.Legend.Title.Font.Shadow.Brush.Visible = True
        TChart1.Legend.Title.Font.Size = 8
        TChart1.Legend.Title.Font.SizeFloat = 8F
        TChart1.Legend.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Bold
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Title.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Legend.Title.ImageBevel.Brush.Solid = True
        TChart1.Legend.Title.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Legend.Title.Shadow.Brush.Color = Color.DarkGray
        TChart1.Legend.Title.Shadow.Brush.Solid = True
        TChart1.Legend.Title.Shadow.Brush.Visible = True
        TChart1.Location = New Point(1, 46)
        TChart1.Margin = New Padding(0)
        TChart1.Name = "TChart1"
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Panel.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Panel.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Panel.Brush.Color = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Panel.Brush.Solid = True
        TChart1.Panel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Panel.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Panel.ImageBevel.Brush.Solid = True
        TChart1.Panel.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Panel.Shadow.Brush.Color = Color.DarkGray
        TChart1.Panel.Shadow.Brush.Solid = True
        TChart1.Panel.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.Printer.Landscape = True
        Margins3.Bottom = 10
        Margins3.Left = 10
        Margins3.Right = 10
        Margins3.Top = 10
        TChart1.Printer.Margins = Margins3
        TChart1.Size = New Size(1095, 439)
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubFooter.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.SubFooter.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.SubFooter.Brush.Color = Color.Silver
        TChart1.SubFooter.Brush.Solid = True
        TChart1.SubFooter.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.SubFooter.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.SubFooter.Font.Brush.Color = Color.Red
        TChart1.SubFooter.Font.Brush.Solid = True
        TChart1.SubFooter.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubFooter.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.SubFooter.Font.Shadow.Brush.Solid = True
        TChart1.SubFooter.Font.Shadow.Brush.Visible = True
        TChart1.SubFooter.Font.Size = 8
        TChart1.SubFooter.Font.SizeFloat = 8F
        TChart1.SubFooter.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubFooter.ImageBevel.Brush.Color = Color.LightGray
        TChart1.SubFooter.ImageBevel.Brush.Solid = True
        TChart1.SubFooter.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubFooter.Shadow.Brush.Color = Color.DarkGray
        TChart1.SubFooter.Shadow.Brush.Solid = True
        TChart1.SubFooter.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubHeader.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.SubHeader.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.SubHeader.Brush.Color = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        TChart1.SubHeader.Brush.Solid = True
        TChart1.SubHeader.Brush.Visible = True
        ' 
        ' 
        ' 
        TChart1.SubHeader.Font.Bold = False
        ' 
        ' 
        ' 
        TChart1.SubHeader.Font.Brush.Color = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        TChart1.SubHeader.Font.Brush.Solid = True
        TChart1.SubHeader.Font.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubHeader.Font.Shadow.Brush.Color = Color.DarkGray
        TChart1.SubHeader.Font.Shadow.Brush.Solid = True
        TChart1.SubHeader.Font.Shadow.Brush.Visible = True
        TChart1.SubHeader.Font.Size = 12
        TChart1.SubHeader.Font.SizeFloat = 12F
        TChart1.SubHeader.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubHeader.ImageBevel.Brush.Color = Color.LightGray
        TChart1.SubHeader.ImageBevel.Brush.Solid = True
        TChart1.SubHeader.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.SubHeader.Shadow.Brush.Color = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        TChart1.SubHeader.Shadow.Brush.Solid = True
        TChart1.SubHeader.Shadow.Brush.Visible = True
        TChart1.TabIndex = 1
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Back.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Walls.Back.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Walls.Back.Brush.Color = Color.Silver
        TChart1.Walls.Back.Brush.Solid = True
        TChart1.Walls.Back.Brush.Visible = False
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Back.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Walls.Back.ImageBevel.Brush.Solid = True
        TChart1.Walls.Back.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Back.Shadow.Brush.Color = Color.DarkGray
        TChart1.Walls.Back.Shadow.Brush.Solid = True
        TChart1.Walls.Back.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Bottom.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Walls.Bottom.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Walls.Bottom.Brush.Color = Color.White
        TChart1.Walls.Bottom.Brush.Solid = True
        TChart1.Walls.Bottom.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Bottom.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Walls.Bottom.ImageBevel.Brush.Solid = True
        TChart1.Walls.Bottom.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Bottom.Shadow.Brush.Color = Color.DarkGray
        TChart1.Walls.Bottom.Shadow.Brush.Solid = True
        TChart1.Walls.Bottom.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Left.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Walls.Left.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Walls.Left.Brush.Color = Color.LightYellow
        TChart1.Walls.Left.Brush.Solid = True
        TChart1.Walls.Left.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Left.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Walls.Left.ImageBevel.Brush.Solid = True
        TChart1.Walls.Left.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Left.Shadow.Brush.Color = Color.DarkGray
        TChart1.Walls.Left.Shadow.Brush.Solid = True
        TChart1.Walls.Left.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Right.Bevel.ColorOne = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        TChart1.Walls.Right.Bevel.ColorTwo = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        ' 
        ' 
        ' 
        TChart1.Walls.Right.Brush.Color = Color.LightYellow
        TChart1.Walls.Right.Brush.Solid = True
        TChart1.Walls.Right.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Right.ImageBevel.Brush.Color = Color.LightGray
        TChart1.Walls.Right.ImageBevel.Brush.Solid = True
        TChart1.Walls.Right.ImageBevel.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Walls.Right.Shadow.Brush.Color = Color.DarkGray
        TChart1.Walls.Right.Shadow.Brush.Solid = True
        TChart1.Walls.Right.Shadow.Brush.Visible = True
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        TChart1.Zoom.Brush.Color = Color.LightBlue
        TChart1.Zoom.Brush.Solid = True
        TChart1.Zoom.Brush.Visible = True
        ' 
        ' Panel_DisplayRange
        ' 
        Panel_DisplayRange.Controls.Add(Label_Display)
        Panel_DisplayRange.Controls.Add(NumericUpDown_DisplayRangeMultiplier)
        Panel_DisplayRange.Controls.Add(ComboBox_DisplayRangeUnit)
        Panel_DisplayRange.Controls.Add(MaskedTextBox_NavEnd)
        Panel_DisplayRange.Controls.Add(MaskedTextBox_NavStart)
        Panel_DisplayRange.Dock = DockStyle.Fill
        Panel_DisplayRange.Location = New Point(1, 486)
        Panel_DisplayRange.Margin = New Padding(0)
        Panel_DisplayRange.Name = "Panel_DisplayRange"
        Panel_DisplayRange.Size = New Size(1095, 42)
        Panel_DisplayRange.TabIndex = 2
        ' 
        ' Label_Display
        ' 
        Label_Display.Anchor = AnchorStyles.Top
        Label_Display.AutoSize = True
        Label_Display.Location = New Point(453, 11)
        Label_Display.Name = "Label_Display"
        Label_Display.Size = New Size(48, 15)
        Label_Display.TabIndex = 1
        Label_Display.Text = "Display:"
        ' 
        ' NumericUpDown_DisplayRangeMultiplier
        ' 
        NumericUpDown_DisplayRangeMultiplier.Anchor = AnchorStyles.Top
        NumericUpDown_DisplayRangeMultiplier.Location = New Point(507, 9)
        NumericUpDown_DisplayRangeMultiplier.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown_DisplayRangeMultiplier.Name = "NumericUpDown_DisplayRangeMultiplier"
        NumericUpDown_DisplayRangeMultiplier.Size = New Size(47, 23)
        NumericUpDown_DisplayRangeMultiplier.TabIndex = 2
        NumericUpDown_DisplayRangeMultiplier.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' ComboBox_DisplayRangeUnit
        ' 
        ComboBox_DisplayRangeUnit.Anchor = AnchorStyles.Top
        ComboBox_DisplayRangeUnit.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox_DisplayRangeUnit.FormattingEnabled = True
        ComboBox_DisplayRangeUnit.Items.AddRange(New Object() {"", "Centuries", "Decades", "Years", "Months", "Weeks", "Days", "Hours", "Minutes", "Seconds"})
        ComboBox_DisplayRangeUnit.Location = New Point(560, 8)
        ComboBox_DisplayRangeUnit.Name = "ComboBox_DisplayRangeUnit"
        ComboBox_DisplayRangeUnit.Size = New Size(94, 23)
        ComboBox_DisplayRangeUnit.TabIndex = 3
        ' 
        ' MaskedTextBox_NavEnd
        ' 
        MaskedTextBox_NavEnd.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        MaskedTextBox_NavEnd.Culture = New System.Globalization.CultureInfo("")
        MaskedTextBox_NavEnd.Location = New Point(970, 8)
        MaskedTextBox_NavEnd.Name = "MaskedTextBox_NavEnd"
        MaskedTextBox_NavEnd.Size = New Size(116, 23)
        MaskedTextBox_NavEnd.TabIndex = 4
        MaskedTextBox_NavEnd.ValidatingType = GetType(Date)
        ' 
        ' MaskedTextBox_NavStart
        ' 
        MaskedTextBox_NavStart.Culture = New System.Globalization.CultureInfo("")
        MaskedTextBox_NavStart.Location = New Point(9, 9)
        MaskedTextBox_NavStart.Name = "MaskedTextBox_NavStart"
        MaskedTextBox_NavStart.Size = New Size(116, 23)
        MaskedTextBox_NavStart.TabIndex = 0
        MaskedTextBox_NavStart.ValidatingType = GetType(Date)
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ProgressBar1.Enabled = False
        ProgressBar1.Location = New Point(971, 740)
        ProgressBar1.Margin = New Padding(4, 3, 4, 3)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(126, 18)
        ProgressBar1.Step = 1
        ProgressBar1.TabIndex = 2
        ' 
        ' MainWindow
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        ClientSize = New Size(1101, 763)
        Controls.Add(ProgressBar1)
        Controls.Add(ToolStrip1)
        Controls.Add(SplitContainer1)
        Controls.Add(StatusStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        KeyPreview = True
        Margin = New Padding(4, 3, 4, 3)
        MinimumSize = New Size(581, 467)
        Name = "MainWindow"
        Text = "BlueM.Wave"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        PanelNavigation.ResumeLayout(False)
        PanelNavigation.PerformLayout()
        CType(NumericUpDown_NavMultiplier, ComponentModel.ISupportInitialize).EndInit()
        Panel_DisplayRange.ResumeLayout(False)
        Panel_DisplayRange.PerformLayout()
        CType(NumericUpDown_DisplayRangeMultiplier, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
    Friend WithEvents ToolStripButton_AddMarkers As ToolStripButton
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents ToolStripButton_RemoveMarkers As ToolStripButton
End Class
