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
        Me.ToolStripStatusLabel_Messages = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripButton_Neu = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Öffnen = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Speichern = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Import = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_EnterSeries = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Export = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Analysis = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_EditChart = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Übersicht = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Drucken = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Kopieren = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Info = New System.Windows.Forms.ToolStripButton
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        StatusStrip1 = New System.Windows.Forms.StatusStrip
        ToolStrip1 = New System.Windows.Forms.ToolStrip
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
        StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel_Messages})
        StatusStrip1.Location = New System.Drawing.Point(0, 478)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        StatusStrip1.ShowItemToolTips = True
        StatusStrip1.Size = New System.Drawing.Size(941, 22)
        StatusStrip1.SizingGrip = False
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel_Messages
        '
        Me.ToolStripStatusLabel_Messages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripStatusLabel_Messages.Enabled = False
        Me.ToolStripStatusLabel_Messages.Image = Global.Wave.My.Resources.Resources.warning
        Me.ToolStripStatusLabel_Messages.Margin = New System.Windows.Forms.Padding(3, 3, 0, 2)
        Me.ToolStripStatusLabel_Messages.Name = "ToolStripStatusLabel_Messages"
        Me.ToolStripStatusLabel_Messages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ToolStripStatusLabel_Messages.Size = New System.Drawing.Size(16, 17)
        Me.ToolStripStatusLabel_Messages.Text = "Messages"
        Me.ToolStripStatusLabel_Messages.ToolTipText = "Messages"
        '
        'ToolStrip1
        '
        ToolStrip1.AutoSize = False
        ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Neu, Me.ToolStripButton_Öffnen, Me.ToolStripButton_Speichern, Me.ToolStripButton_Import, Me.ToolStripButton_EnterSeries, ToolStripSeparator2, Me.ToolStripButton_Export, Me.ToolStripButton_Analysis, ToolStripSeparator1, Me.ToolStripButton_EditChart, Me.ToolStripButton_Übersicht, toolStripSeparator, Me.ToolStripButton_Drucken, Me.ToolStripButton_Kopieren, toolStripSeparator3, Me.ToolStripButton_Info})
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
        'ToolStripButton_Import
        '
        Me.ToolStripButton_Import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Import.Image = Global.Wave.My.Resources.Resources.chart_line_add
        Me.ToolStripButton_Import.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Import.Name = "ToolStripButton_Import"
        Me.ToolStripButton_Import.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Import.Text = "Serie(n) importieren"
        '
        'ToolStripButton_EnterSeries
        '
        Me.ToolStripButton_EnterSeries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_EnterSeries.Image = Global.Wave.My.Resources.Resources.chart_line_edit
        Me.ToolStripButton_EnterSeries.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_EnterSeries.Name = "ToolStripButton_EnterSeries"
        Me.ToolStripButton_EnterSeries.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_EnterSeries.Text = "Zeitreihe eingeben"
        '
        'ToolStripButton_Export
        '
        Me.ToolStripButton_Export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Export.Image = Global.Wave.My.Resources.Resources.page_white_go
        Me.ToolStripButton_Export.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Export.Name = "ToolStripButton_Export"
        Me.ToolStripButton_Export.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Export.Text = "Exportieren"
        Me.ToolStripButton_Export.ToolTipText = "Exportieren"
        '
        'ToolStripButton_Analysis
        '
        Me.ToolStripButton_Analysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Analysis.Image = Global.Wave.My.Resources.Resources.calculator
        Me.ToolStripButton_Analysis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Analysis.Name = "ToolStripButton_Analysis"
        Me.ToolStripButton_Analysis.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Analysis.Text = "Analyse"
        '
        'ToolStripButton_EditChart
        '
        Me.ToolStripButton_EditChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_EditChart.Image = Global.Wave.My.Resources.Resources.pencil
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
        Me.ToolStripButton_Übersicht.Image = Global.Wave.My.Resources.Resources.application_split
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
        'ToolStripButton_Info
        '
        Me.ToolStripButton_Info.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton_Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Info.Image = CType(resources.GetObject("ToolStripButton_Info.Image"), System.Drawing.Image)
        Me.ToolStripButton_Info.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_Info.Name = "ToolStripButton_Info"
        Me.ToolStripButton_Info.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Info.Text = "&Info"
        '
        'TChart1
        '
        '
        '
        '
        Me.TChart1.Aspect.ZOffset = 0
        Me.TChart1.Location = New System.Drawing.Point(0, 0)
        Me.TChart1.Name = "TChart1"
        Me.TChart1.Size = New System.Drawing.Size(945, 190)
        Me.TChart1.TabIndex = 0
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
        Me.SplitContainer1.Panel2.Controls.Add(StatusStrip1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TChart1)
        Me.SplitContainer1.Panel2MinSize = 100
        Me.SplitContainer1.Size = New System.Drawing.Size(945, 626)
        Me.SplitContainer1.SplitterDistance = 118
        Me.SplitContainer1.TabIndex = 1
        '
        'TChart2
        '
        '
        '
        '
        Me.TChart2.Aspect.ZOffset = 0
        Me.TChart2.Cursor = System.Windows.Forms.Cursors.Default
        Me.TChart2.Location = New System.Drawing.Point(0, 0)
        Me.TChart2.Name = "TChart2"
        Me.TChart2.Size = New System.Drawing.Size(942, 106)
        Me.TChart2.TabIndex = 0
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
        Me.Controls.Add(ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Wave"
        Me.Text = "Wave"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Public WithEvents TChart2 As Steema.TeeChart.TChart
    Public WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents ToolStripButton_Neu As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Öffnen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Speichern As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Drucken As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Kopieren As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Info As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_EditChart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Übersicht As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Export As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripButton_Analysis As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripStatusLabel_Messages As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripButton_Import As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_EnterSeries As System.Windows.Forms.ToolStripButton

End Class
