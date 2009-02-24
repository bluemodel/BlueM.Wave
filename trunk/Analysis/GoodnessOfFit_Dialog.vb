Public Class GoodnessOfFit_Dialog

    Public Sub New(ByVal zre1 As String, ByVal zre2 As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.RadioButton1.Text = zre1
        Me.RadioButton2.Text = zre2

    End Sub

    Public Function getNrGemesseneReihe() As Integer

        If (Me.RadioButton1.Checked) Then
            Return 1
        Else
            Return 2
        End If

    End Function


End Class