Public Class Gegenueberstellung_Dialog
    Public ReadOnly Property xAchse() As String
        Get
            If Me.RadioButton_Reihe1.Checked Then
                Return Me.RadioButton_Reihe1.Text
            Else
                Return Me.RadioButton_Reihe2.Text
            End If
        End Get
        
    End Property

    Public Sub New(ByVal name1 As String, ByVal name2 As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Auswahl der Zeitreihe, die zur x-Achse werden soll
        Me.RadioButton_Reihe1.Text = name1
        Me.RadioButton_Reihe2.Text = name2
        Me.Label1.Text = "Bitte Zeitreihe wählen, die auf x-Achse kommen soll..."

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click
        If (Not RadioButton_Reihe1.Checked And Not RadioButton_Reihe2.Checked) Then
            MsgBox("Bitte eine Zeitreihe als x-Achse auswählen!")
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub
End Class