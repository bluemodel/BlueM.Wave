''' <summary>
''' Log
''' </summary>
''' <remarks>implementiert als Singleton-Klasse</remarks>
Public Class Log

    Private Shared myInstance As Log
    Private myLastMessage As String
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

    ''' <summary>
    ''' Die letzte Meldung im Log
    ''' </summary>
    Public Shared ReadOnly Property LastMessage() As String
        Get
            Return Log.getInstance.myLastMessage
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

        If (msg.Contains(Konstanten.eol)) Then
            'Wenn Eintrag mehrzeilig, dann formatieren
            msg = Konstanten.eol & "  " & msg.Replace(Konstanten.eol, Konstanten.eol & "  ")
        Else
            'Ansonsten als Letzte Meldung speichern
            Log.getInstance.myLastMessage = msg
        End If

        'Meldung zu Text hinzufügen
        Log.getInstance.myText &= "* " & DateTime.Now.ToString(Konstanten.Datumsformat) & ": " & msg & Konstanten.eol

        RaiseEvent LogChanged()

    End Sub

    ''' <summary>
    ''' Log zurücksetzen (allen Text löschen)
    ''' </summary>
    Public Shared Sub ClearLog()

        Log.getInstance.myText = ""
        Log.getInstance.myLastMessage = ""

        RaiseEvent LogChanged()

    End Sub

End Class