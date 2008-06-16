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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wave))
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton_Neu = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Öffnen = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Speichern = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Export = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Analysis = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Edit = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Übersicht = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Drucken = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Kopieren = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_Info = New System.Windows.Forms.ToolStripButton
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TChart2 = New Steema.TeeChart.TChart
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStrip1.SuspendLayout()
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
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "txt"
        Me.OpenFileDialog1.Title = "Datei importieren"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_Neu, Me.ToolStripButton_Öffnen, Me.ToolStripButton_Speichern, ToolStripSeparator2, Me.ToolStripButton_Export, Me.ToolStripButton_Analysis, ToolStripSeparator1, Me.ToolStripButton_Edit, Me.ToolStripButton_Übersicht, toolStripSeparator, Me.ToolStripButton_Drucken, Me.ToolStripButton_Kopieren, toolStripSeparator3, Me.ToolStripButton_Info})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(945, 34)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
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
        Me.ToolStripButton_Öffnen.Text = "Ö&ffnen"
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
        'ToolStripButton_Edit
        '
        Me.ToolStripButton_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_Edit.Image = Global.Wave.My.Resources.Resources.pencil
        Me.ToolStripButton_Edit.ImageTransparentColor = System.Drawing.SystemColors.Control
        Me.ToolStripButton_Edit.Name = "ToolStripButton_Edit"
        Me.ToolStripButton_Edit.Size = New System.Drawing.Size(23, 31)
        Me.ToolStripButton_Edit.Text = "Bearbeiten"
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
    Friend WithEvents ToolStripButton_Neu As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Öffnen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Speichern As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Drucken As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Kopieren As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Info As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Edit As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Übersicht As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_Export As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripButton_Analysis As System.Windows.Forms.ToolStripButton

End Class
