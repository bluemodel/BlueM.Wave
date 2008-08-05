<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CutDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Label_Zeitreihe As System.Windows.Forms.Label
        Dim Label_Anfang As System.Windows.Forms.Label
        Dim Label_Ende As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CutDialog))
        Me.Button_OK = New System.Windows.Forms.Button
        Me.ComboBox_Zeitreihen = New System.Windows.Forms.ComboBox
        Me.DateTimePicker_Anfang = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker_Ende = New System.Windows.Forms.DateTimePicker
        Me.Label_Anfangsdatum = New System.Windows.Forms.Label
        Me.Label_Enddatum = New System.Windows.Forms.Label
        Me.DateTimePicker_AnfangZeit = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker_EndeZeit = New System.Windows.Forms.DateTimePicker
        Me.TChart1 = New Steema.TeeChart.TChart
        Label_Zeitreihe = New System.Windows.Forms.Label
        Label_Anfang = New System.Windows.Forms.Label
        Label_Ende = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label_Zeitreihe
        '
        Label_Zeitreihe.AutoSize = True
        Label_Zeitreihe.Location = New System.Drawing.Point(12, 13)
        Label_Zeitreihe.Name = "Label_Zeitreihe"
        Label_Zeitreihe.Size = New System.Drawing.Size(51, 13)
        Label_Zeitreihe.TabIndex = 0
        Label_Zeitreihe.Text = "Zeitreihe:"
        '
        'Label_Anfang
        '
        Label_Anfang.AutoSize = True
        Label_Anfang.Location = New System.Drawing.Point(12, 241)
        Label_Anfang.Name = "Label_Anfang"
        Label_Anfang.Size = New System.Drawing.Size(44, 13)
        Label_Anfang.TabIndex = 3
        Label_Anfang.Text = "Anfang:"
        '
        'Label_Ende
        '
        Label_Ende.AutoSize = True
        Label_Ende.Location = New System.Drawing.Point(330, 241)
        Label_Ende.Name = "Label_Ende"
        Label_Ende.Size = New System.Drawing.Size(35, 13)
        Label_Ende.TabIndex = 7
        Label_Ende.Text = "Ende:"
        '
        'Button_OK
        '
        Me.Button_OK.Location = New System.Drawing.Point(415, 283)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(67, 23)
        Me.Button_OK.TabIndex = 11
        Me.Button_OK.Text = "OK"
        '
        'ComboBox_Zeitreihen
        '
        Me.ComboBox_Zeitreihen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Zeitreihen.FormattingEnabled = True
        Me.ComboBox_Zeitreihen.Location = New System.Drawing.Point(70, 10)
        Me.ComboBox_Zeitreihen.Name = "ComboBox_Zeitreihen"
        Me.ComboBox_Zeitreihen.Size = New System.Drawing.Size(412, 21)
        Me.ComboBox_Zeitreihen.TabIndex = 1
        '
        'DateTimePicker_Anfang
        '
        Me.DateTimePicker_Anfang.CustomFormat = "dd.MM.yyyy"
        Me.DateTimePicker_Anfang.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_Anfang.Location = New System.Drawing.Point(12, 257)
        Me.DateTimePicker_Anfang.Name = "DateTimePicker_Anfang"
        Me.DateTimePicker_Anfang.Size = New System.Drawing.Size(82, 20)
        Me.DateTimePicker_Anfang.TabIndex = 5
        '
        'DateTimePicker_Ende
        '
        Me.DateTimePicker_Ende.CustomFormat = "dd.MM.yyyy"
        Me.DateTimePicker_Ende.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_Ende.Location = New System.Drawing.Point(330, 257)
        Me.DateTimePicker_Ende.Name = "DateTimePicker_Ende"
        Me.DateTimePicker_Ende.Size = New System.Drawing.Size(82, 20)
        Me.DateTimePicker_Ende.TabIndex = 9
        '
        'Label_Anfangsdatum
        '
        Me.Label_Anfangsdatum.AutoSize = True
        Me.Label_Anfangsdatum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Anfangsdatum.Location = New System.Drawing.Point(63, 241)
        Me.Label_Anfangsdatum.Name = "Label_Anfangsdatum"
        Me.Label_Anfangsdatum.Size = New System.Drawing.Size(101, 13)
        Me.Label_Anfangsdatum.TabIndex = 4
        Me.Label_Anfangsdatum.Text = "dd.MM.yyyy HH:mm"
        '
        'Label_Enddatum
        '
        Me.Label_Enddatum.AutoSize = True
        Me.Label_Enddatum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Enddatum.Location = New System.Drawing.Point(371, 241)
        Me.Label_Enddatum.Name = "Label_Enddatum"
        Me.Label_Enddatum.Size = New System.Drawing.Size(101, 13)
        Me.Label_Enddatum.TabIndex = 8
        Me.Label_Enddatum.Text = "dd.MM.yyyy HH:mm"
        '
        'DateTimePicker_AnfangZeit
        '
        Me.DateTimePicker_AnfangZeit.CustomFormat = "HH:mm"
        Me.DateTimePicker_AnfangZeit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_AnfangZeit.Location = New System.Drawing.Point(100, 257)
        Me.DateTimePicker_AnfangZeit.Name = "DateTimePicker_AnfangZeit"
        Me.DateTimePicker_AnfangZeit.ShowUpDown = True
        Me.DateTimePicker_AnfangZeit.Size = New System.Drawing.Size(64, 20)
        Me.DateTimePicker_AnfangZeit.TabIndex = 6
        '
        'DateTimePicker_EndeZeit
        '
        Me.DateTimePicker_EndeZeit.CustomFormat = "HH:mm"
        Me.DateTimePicker_EndeZeit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_EndeZeit.Location = New System.Drawing.Point(418, 257)
        Me.DateTimePicker_EndeZeit.Name = "DateTimePicker_EndeZeit"
        Me.DateTimePicker_EndeZeit.ShowUpDown = True
        Me.DateTimePicker_EndeZeit.Size = New System.Drawing.Size(64, 20)
        Me.DateTimePicker_EndeZeit.TabIndex = 10
        '
        'TChart1
        '
        '
        '
        '
        Me.TChart1.Aspect.View3D = False
        Me.TChart1.Aspect.ZOffset = 0
        Me.TChart1.Cursor = System.Windows.Forms.Cursors.Default
        '
        '
        '
        Me.TChart1.Header.Visible = False
        '
        '
        '
        Me.TChart1.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        Me.TChart1.Legend.Visible = False
        Me.TChart1.Location = New System.Drawing.Point(12, 37)
        Me.TChart1.Name = "TChart1"
        '
        '
        '
        Me.TChart1.Panel.MarginBottom = 3
        Me.TChart1.Panel.MarginTop = 3
        '
        '
        '
        Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal
        Me.TChart1.Size = New System.Drawing.Size(470, 192)
        Me.TChart1.TabIndex = 2
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Back.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        Me.TChart1.Walls.Back.Brush.Gradient.Visible = False
        '
        '
        '
        Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Horizontal
        '
        'CutDialog
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 317)
        Me.Controls.Add(Me.TChart1)
        Me.Controls.Add(Me.Label_Enddatum)
        Me.Controls.Add(Me.Label_Anfangsdatum)
        Me.Controls.Add(Label_Ende)
        Me.Controls.Add(Label_Anfang)
        Me.Controls.Add(Me.DateTimePicker_Ende)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.DateTimePicker_EndeZeit)
        Me.Controls.Add(Me.DateTimePicker_AnfangZeit)
        Me.Controls.Add(Me.DateTimePicker_Anfang)
        Me.Controls.Add(Me.ComboBox_Zeitreihen)
        Me.Controls.Add(Label_Zeitreihe)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CutDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Zeitreihe zuschneiden"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Zeitreihen As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePicker_Anfang As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker_Ende As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label_Anfangsdatum As System.Windows.Forms.Label
    Friend WithEvents Label_Enddatum As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker_AnfangZeit As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker_EndeZeit As System.Windows.Forms.DateTimePicker
    Friend WithEvents TChart1 As Steema.TeeChart.TChart

End Class
