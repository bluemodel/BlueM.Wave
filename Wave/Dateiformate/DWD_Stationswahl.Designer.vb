<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DWD_Stationswahl
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox_Stationsnummer = New System.Windows.Forms.TextBox
        Me.Button_OK = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(186, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bitte DWD-Stationsnummer eingeben:"
        '
        'TextBox_Stationsnummer
        '
        Me.TextBox_Stationsnummer.Location = New System.Drawing.Point(12, 35)
        Me.TextBox_Stationsnummer.Name = "TextBox_Stationsnummer"
        Me.TextBox_Stationsnummer.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_Stationsnummer.TabIndex = 1
        '
        'Button_OK
        '
        Me.Button_OK.Location = New System.Drawing.Point(201, 35)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(75, 23)
        Me.Button_OK.TabIndex = 2
        Me.Button_OK.Text = "OK"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'DWD_Stationswahl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(288, 79)
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.TextBox_Stationsnummer)
        Me.Controls.Add(Me.Label1)
        Me.Name = "DWD_Stationswahl"
        Me.Text = "DWD-Stationswahl"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_Stationsnummer As System.Windows.Forms.TextBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button
End Class
