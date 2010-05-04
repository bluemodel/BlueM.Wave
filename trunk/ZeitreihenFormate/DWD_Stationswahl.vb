Public Class DWD_Stationswahl

        Public ReadOnly Property Stationsnummer As String
        Get
            Return   Me.TextBox_Stationsnummer.Text
        End Get
        
    End Property

Private Sub Button_OK_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles Button_OK.Click
        If Me.TextBox_Stationsnummer.Text <> "" Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
            MsgBox("Bitte einen fünfstelligen Wert eingeben!")

        End If
End Sub
End Class