Imports System.Windows.Forms

Friend Class HYDRO_AS_2D_Diag

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub dlgLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DateTimePicker_refDate.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class
