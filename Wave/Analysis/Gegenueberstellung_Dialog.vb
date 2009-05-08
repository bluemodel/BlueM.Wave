Public Class Gegenueberstellung_Dialog

    Public Sub New(ByVal name1 As String, ByVal name2 As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Auswahl der Zeitreihe, die zur x-Achse werden soll
        Me.RadioButton_Reihe1.Text = name1
        Me.RadioButton_Reihe2.Text = name2

    End Sub

    Public ReadOnly Property xAchse() As String
        Get
            If Me.RadioButton_Reihe1.Checked Then
                Return Me.RadioButton_Reihe1.Text
            Else
                Return Me.RadioButton_Reihe2.Text
            End If
        End Get
        
    End Property

End Class