''' <summary>
''' Log
''' </summary>
''' <remarks>implementiert als Singleton-Klasse</remarks>
Public Class Log

    Private Shared myInstance As Log
    Private myText As String

    ''' <summary>
    ''' Wird ausgelöst, wenn sich der Log Text verändert hat
    ''' </summary>
    Public Shared Event LogChanged()

    ''' <summary>
    ''' Der Log Text
    ''' </summary>
    Public Shared ReadOnly Property Text() As String
        Get
            Return Log.getInstance.myText
        End Get
    End Property

    Private Sub New()
        'nix
    End Sub

    ''' <summary>
    ''' Gibt die (einzige) Instanz des Logs zurück
    ''' </summary>
    Public Shared Function getInstance() As Log
        If (IsNothing(myInstance)) Then
            myInstance = New Log()
        End If
        Return myInstance
    End Function

    ''' <summary>
    ''' Einen Log-Eintrag hinzufügen
    ''' </summary>
    ''' <param name="msg">Eintrag</param>
    Public Shared Sub AddLogEntry(ByVal msg As String)

        'Wenn Eintrag mehrzeilig, dann formatieren
        If (msg.Contains(Konstanten.eol)) Then msg = Konstanten.eol & "  " & msg.Replace(Konstanten.eol, Konstanten.eol & "  ")

        'Text hinzufügen
        Log.getInstance.myText &= "* " & DateTime.Now.ToString(Konstanten.Datumsformat) & ": " & msg & Konstanten.eol

        RaiseEvent LogChanged()

    End Sub

    ''' <summary>
    ''' Log zurücksetzen (allen Text löschen)
    ''' </summary>
    Public Shared Sub ClearLog()

        Log.getInstance.myText = ""

        RaiseEvent LogChanged()

    End Sub

End Class