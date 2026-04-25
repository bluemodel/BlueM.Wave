Namespace Fileformats
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ReferenceDateDialog
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReferenceDateDialog))
            OK_Button = New Button()
            DateTimePicker_refDate = New DateTimePicker()
            Label1 = New Label()
            Label2 = New Label()
            SuspendLayout()
            ' 
            ' OK_Button
            ' 
            OK_Button.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            OK_Button.Location = New Point(300, 53)
            OK_Button.Margin = New Padding(4, 3, 4, 3)
            OK_Button.Name = "OK_Button"
            OK_Button.Size = New Size(78, 27)
            OK_Button.TabIndex = 0
            OK_Button.Text = "OK"
            ' 
            ' DateTimePicker_refDate
            ' 
            DateTimePicker_refDate.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            DateTimePicker_refDate.CustomFormat = "dd.MM.yyyy HH:mm"
            DateTimePicker_refDate.Format = DateTimePickerFormat.Custom
            DateTimePicker_refDate.Location = New Point(234, 14)
            DateTimePicker_refDate.Margin = New Padding(4, 3, 4, 3)
            DateTimePicker_refDate.Name = "DateTimePicker_refDate"
            DateTimePicker_refDate.Size = New Size(143, 23)
            DateTimePicker_refDate.TabIndex = 1
            DateTimePicker_refDate.Value = New Date(2000, 1, 1, 0, 0, 0, 0)
            ' 
            ' Label1
            ' 
            Label1.AutoSize = True
            Label1.Location = New Point(14, 17)
            Label1.Margin = New Padding(4, 0, 4, 0)
            Label1.Name = "Label1"
            Label1.Size = New Size(205, 15)
            Label1.TabIndex = 2
            Label1.Text = "Reference date for start of simulation:"
            ' 
            ' Label2
            ' 
            Label2.AutoSize = True
            Label2.Location = New Point(16, 59)
            Label2.Margin = New Padding(4, 0, 4, 0)
            Label2.Name = "Label2"
            Label2.Size = New Size(208, 15)
            Label2.TabIndex = 3
            Label2.Text = "(Simulation time is added to this date)"
            ' 
            ' ReferenceDateDialog
            ' 
            AcceptButton = OK_Button
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(392, 93)
            ControlBox = False
            Controls.Add(Label2)
            Controls.Add(OK_Button)
            Controls.Add(Label1)
            Controls.Add(DateTimePicker_refDate)
            FormBorderStyle = FormBorderStyle.FixedDialog
            Icon = CType(resources.GetObject("$this.Icon"), Icon)
            Margin = New Padding(4, 3, 4, 3)
            MaximizeBox = False
            MinimizeBox = False
            Name = "ReferenceDateDialog"
            ShowInTaskbar = False
            StartPosition = FormStartPosition.CenterParent
            Text = "Reference Date"
            ResumeLayout(False)
            PerformLayout()

        End Sub
        Friend WithEvents OK_Button As System.Windows.Forms.Button
        Friend WithEvents DateTimePicker_refDate As System.Windows.Forms.DateTimePicker
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label

    End Class
End Namespace