Namespace Fileformats
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class HYDRO_AS_2D_Diag
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HYDRO_AS_2D_Diag))
            Me.OK_Button = New System.Windows.Forms.Button
            Me.DateTimePicker_refDate = New System.Windows.Forms.DateTimePicker
            Me.Label1 = New System.Windows.Forms.Label
            Me.Label2 = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'OK_Button
            '
            Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.OK_Button.Location = New System.Drawing.Point(257, 46)
            Me.OK_Button.Name = "OK_Button"
            Me.OK_Button.Size = New System.Drawing.Size(67, 23)
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            '
            'DateTimePicker_refDate
            '
            Me.DateTimePicker_refDate.CustomFormat = Helpers.CurrentDateFormat
            Me.DateTimePicker_refDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.DateTimePicker_refDate.Location = New System.Drawing.Point(201, 12)
            Me.DateTimePicker_refDate.Name = "DateTimePicker_refDate"
            Me.DateTimePicker_refDate.Size = New System.Drawing.Size(123, 20)
            Me.DateTimePicker_refDate.TabIndex = 1
            Me.DateTimePicker_refDate.Value = New Date(2000, 1, 1, 0, 0, 0, 0)
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(12, 15)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(183, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Text = "Reference date for start of simulation:"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(14, 51)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(181, 13)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "(Simulation time is added to this date)"
            '
            'HYDRO_AS_2D_Diag
            '
            Me.AcceptButton = Me.OK_Button
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(336, 81)
            Me.ControlBox = False
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.OK_Button)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.DateTimePicker_refDate)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "HYDRO_AS_2D_Diag"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "HYDRO_AS-2D reference date"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents OK_Button As System.Windows.Forms.Button
        Friend WithEvents DateTimePicker_refDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label

    End Class
End Namespace