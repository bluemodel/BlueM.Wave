<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Autocorrelation_Dialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Autocorrelation_Dialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.spnAnzahlLag = New System.Windows.Forms.NumericUpDown()
        Me.spnGroesseLag = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.spnAnzahlLag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spnGroesseLag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(165, 181)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'spnAnzahlLag
        '
        Me.spnAnzahlLag.Location = New System.Drawing.Point(169, 60)
        Me.spnAnzahlLag.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.spnAnzahlLag.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spnAnzahlLag.Name = "spnAnzahlLag"
        Me.spnAnzahlLag.Size = New System.Drawing.Size(120, 20)
        Me.spnAnzahlLag.TabIndex = 9
        Me.spnAnzahlLag.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'spnGroesseLag
        '
        Me.spnGroesseLag.Location = New System.Drawing.Point(169, 35)
        Me.spnGroesseLag.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.spnGroesseLag.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.spnGroesseLag.Name = "spnGroesseLag"
        Me.spnGroesseLag.Size = New System.Drawing.Size(120, 20)
        Me.spnGroesseLag.TabIndex = 8
        Me.spnGroesseLag.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Lag settings:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Lag count (1 - 2000):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Lag size (1 - 10 time units):"
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.Location = New System.Drawing.Point(12, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(299, 39)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "More lags take longer to compute."
        '
        'Label6
        '
        Me.Label6.AutoEllipsis = True
        Me.Label6.Location = New System.Drawing.Point(12, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(296, 38)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "NOTE: The time series should not contain a trend"
        '
        'Autocorrelation_Dialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(323, 222)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.spnAnzahlLag)
        Me.Controls.Add(Me.spnGroesseLag)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Autocorrelation_Dialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Autocorrelation"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.spnAnzahlLag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spnGroesseLag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents spnAnzahlLag As System.Windows.Forms.NumericUpDown
    Friend WithEvents spnGroesseLag As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
