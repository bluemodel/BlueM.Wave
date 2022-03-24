
''' <summary>
''' Class representing a calendar month
''' </summary>
Public Class Month
    ''' <summary>
    ''' Number of the month (1 to 12)
    ''' </summary>
    Public number As Integer
    ''' <summary>
    ''' Name of the month
    ''' </summary>
    Public name As String
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="number">Number of the month (1 to 12)</param>
    ''' <param name="name">Name of the month</param>
    Public Sub New(number As Integer, name As String)
        If number < 1 Or number > 12 Then
            Throw New ArgumentOutOfRangeException("Month number must be between 1 and 12!")
        End If
        Me.number = number
        Me.name = name
    End Sub
    ''' <summary>
    ''' Returns the name of the month
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function ToString() As String
        Return Me.name
    End Function
End Class

