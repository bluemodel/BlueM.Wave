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
        Dim Label_Series As System.Windows.Forms.Label
        Dim Label_Start As System.Windows.Forms.Label
        Dim Label_End As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CutDialog))
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.ComboBox_ZeitreiheCut = New System.Windows.Forms.ComboBox()
        Me.Label_StartDateTime = New System.Windows.Forms.Label()
        Me.Label_EndDateTime = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage_DateTime = New System.Windows.Forms.TabPage()
        Me.MaskedTextBox_cutEnd = New System.Windows.Forms.MaskedTextBox()
        Me.MaskedTextBox_cutStart = New System.Windows.Forms.MaskedTextBox()
        Me.TabPage_ZRE = New System.Windows.Forms.TabPage()
        Me.ComboBox_RefSeries = New System.Windows.Forms.ComboBox()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.CheckBox_keepUncutSeries = New System.Windows.Forms.CheckBox()
        Label_Series = New System.Windows.Forms.Label()
        Label_Start = New System.Windows.Forms.Label()
        Label_End = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage_DateTime.SuspendLayout()
        Me.TabPage_ZRE.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_Series
        '
        Label_Series.AutoSize = True
        Label_Series.Location = New System.Drawing.Point(12, 13)
        Label_Series.Name = "Label_Series"
        Label_Series.Size = New System.Drawing.Size(39, 13)
        Label_Series.TabIndex = 0
        Label_Series.Text = "Series:"
        '
        'Label_Start
        '
        Label_Start.AutoSize = True
        Label_Start.Location = New System.Drawing.Point(6, 15)
        Label_Start.Name = "Label_Start"
        Label_Start.Size = New System.Drawing.Size(32, 13)
        Label_Start.TabIndex = 0
        Label_Start.Text = "Start:"
        '
        'Label_End
        '
        Label_End.AutoSize = True
        Label_End.Location = New System.Drawing.Point(333, 15)
        Label_End.Name = "Label_End"
        Label_End.Size = New System.Drawing.Size(29, 13)
        Label_End.TabIndex = 2
        Label_End.Text = "End:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(7, 7)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(205, 13)
        Label1.TabIndex = 0
        Label1.Text = "Cut to the timespan of the following series:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(15, 73)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(111, 13)
        Label2.TabIndex = 4
        Label2.Text = "Cut selected series to:"
        '
        'Button_OK
        '
        Me.Button_OK.Location = New System.Drawing.Point(337, 192)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(67, 23)
        Me.Button_OK.TabIndex = 7
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
        'Label_StartDateTime
        '
        Me.Label_StartDateTime.AutoSize = True
        Me.Label_StartDateTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_StartDateTime.Location = New System.Drawing.Point(12, 43)
        Me.Label_StartDateTime.Name = "Label_StartDateTime"
        Me.Label_StartDateTime.Size = New System.Drawing.Size(124, 13)
        Me.Label_StartDateTime.TabIndex = 2
        Me.Label_StartDateTime.Text = "Min: dd.MM.yyyy HH:mm"
        '
        'Label_EndDateTime
        '
        Me.Label_EndDateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_EndDateTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label_EndDateTime.Location = New System.Drawing.Point(349, 43)
        Me.Label_EndDateTime.Name = "Label_EndDateTime"
        Me.Label_EndDateTime.Size = New System.Drawing.Size(136, 13)
        Me.Label_EndDateTime.TabIndex = 3
        Me.Label_EndDateTime.Text = "Max: dd.MM.yyyy HH:mm"
        Me.Label_EndDateTime.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage_DateTime)
        Me.TabControl1.Controls.Add(Me.TabPage_ZRE)
        Me.TabControl1.Location = New System.Drawing.Point(15, 94)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(470, 92)
        Me.TabControl1.TabIndex = 5
        '
        'TabPage_DateTime
        '
        Me.TabPage_DateTime.Controls.Add(Me.MaskedTextBox_cutEnd)
        Me.TabPage_DateTime.Controls.Add(Me.MaskedTextBox_cutStart)
        Me.TabPage_DateTime.Controls.Add(Label_Start)
        Me.TabPage_DateTime.Controls.Add(Label_End)
        Me.TabPage_DateTime.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_DateTime.Name = "TabPage_DateTime"
        Me.TabPage_DateTime.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_DateTime.Size = New System.Drawing.Size(462, 66)
        Me.TabPage_DateTime.TabIndex = 0
        Me.TabPage_DateTime.Text = "Date/time"
        Me.TabPage_DateTime.UseVisualStyleBackColor = True
        '
        'MaskedTextBox_cutEnd
        '
        Me.MaskedTextBox_cutEnd.Culture = New System.Globalization.CultureInfo("")
        Me.MaskedTextBox_cutEnd.Location = New System.Drawing.Point(336, 31)
        Me.MaskedTextBox_cutEnd.Mask = "00/00/0000 00:00"
        Me.MaskedTextBox_cutEnd.Name = "MaskedTextBox_cutEnd"
        Me.MaskedTextBox_cutEnd.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox_cutEnd.TabIndex = 3
        Me.MaskedTextBox_cutEnd.ValidatingType = GetType(DateTime)
        '
        'MaskedTextBox_cutStart
        '
        Me.MaskedTextBox_cutStart.Culture = New System.Globalization.CultureInfo("")
        Me.MaskedTextBox_cutStart.Location = New System.Drawing.Point(7, 31)
        Me.MaskedTextBox_cutStart.Mask = "00/00/0000 00:00"
        Me.MaskedTextBox_cutStart.Name = "MaskedTextBox_cutStart"
        Me.MaskedTextBox_cutStart.Size = New System.Drawing.Size(100, 20)
        Me.MaskedTextBox_cutStart.TabIndex = 1
        Me.MaskedTextBox_cutStart.ValidatingType = GetType(DateTime)
        '
        'TabPage_ZRE
        '
        Me.TabPage_ZRE.Controls.Add(Label1)
        Me.TabPage_ZRE.Controls.Add(Me.ComboBox_RefSeries)
        Me.TabPage_ZRE.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_ZRE.Name = "TabPage_ZRE"
        Me.TabPage_ZRE.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_ZRE.Size = New System.Drawing.Size(462, 66)
        Me.TabPage_ZRE.TabIndex = 1
        Me.TabPage_ZRE.Text = "Series"
        Me.TabPage_ZRE.UseVisualStyleBackColor = True
        '
        'ComboBox_RefSeries
        '
        Me.ComboBox_RefSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_RefSeries.FormattingEnabled = True
        Me.ComboBox_RefSeries.Location = New System.Drawing.Point(10, 28)
        Me.ComboBox_RefSeries.Name = "ComboBox_RefSeries"
        Me.ComboBox_RefSeries.Size = New System.Drawing.Size(446, 21)
        Me.ComboBox_RefSeries.TabIndex = 1
        '
        'Button_Cancel
        '
        Me.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancel.Location = New System.Drawing.Point(410, 192)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Button_Cancel.TabIndex = 8
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'CheckBox_keepUncutSeries
        '
        Me.CheckBox_keepUncutSeries.AutoSize = True
        Me.CheckBox_keepUncutSeries.Location = New System.Drawing.Point(19, 196)
        Me.CheckBox_keepUncutSeries.Name = "CheckBox_keepUncutSeries"
        Me.CheckBox_keepUncutSeries.Size = New System.Drawing.Size(111, 17)
        Me.CheckBox_keepUncutSeries.TabIndex = 6
        Me.CheckBox_keepUncutSeries.Text = "Keep uncut series"
        Me.CheckBox_keepUncutSeries.UseVisualStyleBackColor = True
        '
        'CutDialog
        '
        Me.AcceptButton = Me.Button_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 223)
        Me.Controls.Add(Me.CheckBox_keepUncutSeries)
        Me.Controls.Add(Label_Series)
        Me.Controls.Add(Me.ComboBox_ZeitreiheCut)
        Me.Controls.Add(Me.Label_StartDateTime)
        Me.Controls.Add(Me.Label_EndDateTime)
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
        Me.Text = "Cut time series"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage_DateTime.ResumeLayout(False)
        Me.TabPage_DateTime.PerformLayout()
        Me.TabPage_ZRE.ResumeLayout(False)
        Me.TabPage_ZRE.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents TabPage_DateTime As System.Windows.Forms.TabPage
    Private WithEvents TabControl1 As System.Windows.Forms.TabControl
    Private WithEvents Label_StartDateTime As System.Windows.Forms.Label
    Private WithEvents Label_EndDateTime As System.Windows.Forms.Label
    Private WithEvents TabPage_ZRE As System.Windows.Forms.TabPage
    Private WithEvents ComboBox_RefSeries As System.Windows.Forms.ComboBox
    Private WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents CheckBox_keepUncutSeries As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox_ZeitreiheCut As System.Windows.Forms.ComboBox
    Friend WithEvents MaskedTextBox_cutStart As MaskedTextBox
    Friend WithEvents MaskedTextBox_cutEnd As MaskedTextBox
End Class
