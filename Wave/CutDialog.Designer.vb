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
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CutDialog))
        Me.Button_OK = New System.Windows.Forms.Button
        Me.ComboBox_ZeitreiheCut = New System.Windows.Forms.ComboBox
        Me.DateTimePicker_Anfang = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker_Ende = New System.Windows.Forms.DateTimePicker
        Me.Label_Anfangsdatum = New System.Windows.Forms.Label
        Me.Label_Enddatum = New System.Windows.Forms.Label
        Me.DateTimePicker_AnfangZeit = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker_EndeZeit = New System.Windows.Forms.DateTimePicker
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage_DateTime = New System.Windows.Forms.TabPage
        Me.TabPage_ZRE = New System.Windows.Forms.TabPage
        Me.ComboBox_ZeitreiheRef = New System.Windows.Forms.ComboBox
        Me.Button_Cancel = New System.Windows.Forms.Button
        Label_Zeitreihe = New System.Windows.Forms.Label
        Label_Anfang = New System.Windows.Forms.Label
        Label_Ende = New System.Windows.Forms.Label
        Label1 = New System.Windows.Forms.Label
        Label2 = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage_DateTime.SuspendLayout()
        Me.TabPage_ZRE.SuspendLayout()
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
        Label_Anfang.Location = New System.Drawing.Point(15, 15)
        Label_Anfang.Name = "Label_Anfang"
        Label_Anfang.Size = New System.Drawing.Size(44, 13)
        Label_Anfang.TabIndex = 3
        Label_Anfang.Text = "Anfang:"
        '
        'Label_Ende
        '
        Label_Ende.AutoSize = True
        Label_Ende.Location = New System.Drawing.Point(294, 15)
        Label_Ende.Name = "Label_Ende"
        Label_Ende.Size = New System.Drawing.Size(35, 13)
        Label_Ende.TabIndex = 7
        Label_Ende.Text = "Ende:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(7, 7)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(176, 13)
        Label1.TabIndex = 0
        Label1.Text = "auf folgende Zeitreihe zuschneiden:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(12, 262)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(196, 13)
        Label2.TabIndex = 15
        Label2.Text = "Ausgewählte Zeitreihe zuschneiden auf:"
        '
        'Button_OK
        '
        Me.Button_OK.Location = New System.Drawing.Point(334, 381)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(67, 23)
        Me.Button_OK.TabIndex = 11
        Me.Button_OK.Text = "OK"
        '
        'ComboBox_ZeitreiheCut
        '
        Me.ComboBox_ZeitreiheCut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ZeitreiheCut.FormattingEnabled = True
        Me.ComboBox_ZeitreiheCut.Location = New System.Drawing.Point(70, 10)
        Me.ComboBox_ZeitreiheCut.Name = "ComboBox_ZeitreiheCut"
        Me.ComboBox_ZeitreiheCut.Size = New System.Drawing.Size(412, 21)
        Me.ComboBox_ZeitreiheCut.TabIndex = 1
        '
        'DateTimePicker_Anfang
        '
        Me.DateTimePicker_Anfang.CustomFormat = "dd.MM.yyyy"
        Me.DateTimePicker_Anfang.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_Anfang.Location = New System.Drawing.Point(15, 31)
        Me.DateTimePicker_Anfang.Name = "DateTimePicker_Anfang"
        Me.DateTimePicker_Anfang.Size = New System.Drawing.Size(82, 20)
        Me.DateTimePicker_Anfang.TabIndex = 5
        '
        'DateTimePicker_Ende
        '
        Me.DateTimePicker_Ende.CustomFormat = "dd.MM.yyyy"
        Me.DateTimePicker_Ende.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_Ende.Location = New System.Drawing.Point(294, 31)
        Me.DateTimePicker_Ende.Name = "DateTimePicker_Ende"
        Me.DateTimePicker_Ende.Size = New System.Drawing.Size(82, 20)
        Me.DateTimePicker_Ende.TabIndex = 9
        '
        'Label_Anfangsdatum
        '
        Me.Label_Anfangsdatum.AutoSize = True
        Me.Label_Anfangsdatum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Anfangsdatum.Location = New System.Drawing.Point(9, 232)
        Me.Label_Anfangsdatum.Name = "Label_Anfangsdatum"
        Me.Label_Anfangsdatum.Size = New System.Drawing.Size(101, 13)
        Me.Label_Anfangsdatum.TabIndex = 4
        Me.Label_Anfangsdatum.Text = "dd.MM.yyyy HH:mm"
        '
        'Label_Enddatum
        '
        Me.Label_Enddatum.AutoSize = True
        Me.Label_Enddatum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_Enddatum.Location = New System.Drawing.Point(381, 232)
        Me.Label_Enddatum.Name = "Label_Enddatum"
        Me.Label_Enddatum.Size = New System.Drawing.Size(101, 13)
        Me.Label_Enddatum.TabIndex = 8
        Me.Label_Enddatum.Text = "dd.MM.yyyy HH:mm"
        '
        'DateTimePicker_AnfangZeit
        '
        Me.DateTimePicker_AnfangZeit.CustomFormat = "HH:mm"
        Me.DateTimePicker_AnfangZeit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_AnfangZeit.Location = New System.Drawing.Point(103, 31)
        Me.DateTimePicker_AnfangZeit.Name = "DateTimePicker_AnfangZeit"
        Me.DateTimePicker_AnfangZeit.ShowUpDown = True
        Me.DateTimePicker_AnfangZeit.Size = New System.Drawing.Size(64, 20)
        Me.DateTimePicker_AnfangZeit.TabIndex = 6
        '
        'DateTimePicker_EndeZeit
        '
        Me.DateTimePicker_EndeZeit.CustomFormat = "HH:mm"
        Me.DateTimePicker_EndeZeit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_EndeZeit.Location = New System.Drawing.Point(382, 31)
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
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage_DateTime)
        Me.TabControl1.Controls.Add(Me.TabPage_ZRE)
        Me.TabControl1.Location = New System.Drawing.Point(12, 283)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(470, 92)
        Me.TabControl1.TabIndex = 14
        '
        'TabPage_DateTime
        '
        Me.TabPage_DateTime.Controls.Add(Me.DateTimePicker_EndeZeit)
        Me.TabPage_DateTime.Controls.Add(Label_Anfang)
        Me.TabPage_DateTime.Controls.Add(Me.DateTimePicker_Ende)
        Me.TabPage_DateTime.Controls.Add(Me.DateTimePicker_Anfang)
        Me.TabPage_DateTime.Controls.Add(Label_Ende)
        Me.TabPage_DateTime.Controls.Add(Me.DateTimePicker_AnfangZeit)
        Me.TabPage_DateTime.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_DateTime.Name = "TabPage_DateTime"
        Me.TabPage_DateTime.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_DateTime.Size = New System.Drawing.Size(462, 66)
        Me.TabPage_DateTime.TabIndex = 0
        Me.TabPage_DateTime.Text = "Datum/Zeit"
        Me.TabPage_DateTime.UseVisualStyleBackColor = True
        '
        'TabPage_ZRE
        '
        Me.TabPage_ZRE.Controls.Add(Label1)
        Me.TabPage_ZRE.Controls.Add(Me.ComboBox_ZeitreiheRef)
        Me.TabPage_ZRE.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_ZRE.Name = "TabPage_ZRE"
        Me.TabPage_ZRE.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_ZRE.Size = New System.Drawing.Size(462, 66)
        Me.TabPage_ZRE.TabIndex = 1
        Me.TabPage_ZRE.Text = "Zeitreihe"
        Me.TabPage_ZRE.UseVisualStyleBackColor = True
        '
        'ComboBox_ZeitreiheRef
        '
        Me.ComboBox_ZeitreiheRef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ZeitreiheRef.FormattingEnabled = True
        Me.ComboBox_ZeitreiheRef.Location = New System.Drawing.Point(10, 28)
        Me.ComboBox_ZeitreiheRef.Name = "ComboBox_ZeitreiheRef"
        Me.ComboBox_ZeitreiheRef.Size = New System.Drawing.Size(446, 21)
        Me.ComboBox_ZeitreiheRef.TabIndex = 1
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(407, 381)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 16
        Me.Button_Cancel.Text = "Abbrechen"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'CutDialog
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 413)
        Me.Controls.Add(Label_Zeitreihe)
        Me.Controls.Add(Me.ComboBox_ZeitreiheCut)
        Me.Controls.Add(Me.TChart1)
        Me.Controls.Add(Me.Label_Anfangsdatum)
        Me.Controls.Add(Me.Label_Enddatum)
        Me.Controls.Add(Label2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.Button_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CutDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Zeitreihe zuschneiden"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage_DateTime.ResumeLayout(False)
        Me.TabPage_DateTime.PerformLayout()
        Me.TabPage_ZRE.ResumeLayout(False)
        Me.TabPage_ZRE.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents TabPage_DateTime As System.Windows.Forms.TabPage
    Private WithEvents TabControl1 As System.Windows.Forms.TabControl
    Private WithEvents Label_Anfangsdatum As System.Windows.Forms.Label
    Private WithEvents Label_Enddatum As System.Windows.Forms.Label
    Private WithEvents TabPage_ZRE As System.Windows.Forms.TabPage
    Private WithEvents DateTimePicker_Anfang As System.Windows.Forms.DateTimePicker
    Private WithEvents DateTimePicker_Ende As System.Windows.Forms.DateTimePicker
    Private WithEvents DateTimePicker_AnfangZeit As System.Windows.Forms.DateTimePicker
    Private WithEvents DateTimePicker_EndeZeit As System.Windows.Forms.DateTimePicker
    Private WithEvents ComboBox_ZeitreiheCut As System.Windows.Forms.ComboBox
    Private WithEvents ComboBox_ZeitreiheRef As System.Windows.Forms.ComboBox
    Private WithEvents Button_Cancel As System.Windows.Forms.Button

End Class
