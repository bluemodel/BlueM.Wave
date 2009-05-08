Public Class Zeichen

    Private _char As Char

    Public Property Character() As Char
        Get
            Return _char
        End Get
        Set(ByVal value As Char)
            _char = value
        End Set
    End Property

    'Konstruktoren
    '*************
    Public Sub New(ByVal str As String)
        Character = Convert.ToChar(str)
    End Sub

    Public Sub New(ByVal chr As Char)
        Character = chr
    End Sub

    'Zeichenausgabe als String
    '*************************
    Public Overrides Function tostring() As String

        Dim output As String

        Select Case Character
            Case Chr(32)
                output = "Leerzeichen"
            Case Chr(9)
                output = "Tab"
            Case Else
                output = Convert.ToString(Character)
        End Select

        Return output

    End Function

End Class
