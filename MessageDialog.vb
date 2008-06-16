Imports System.Windows.Forms

Public Class MessageDialog

    'Message hinzufügen
    '******************
    Public Sub AddMessage(ByVal msg As String)

        If (Me.TextBox_Messages.Text.Length = 0) Then
            Me.TextBox_Messages.Text = msg
        Else
            Me.TextBox_Messages.Text &= eol & eol & "------------------" & eol & msg
        End If

    End Sub

    'Messages zurücksetzen
    '*********************
    Public Sub ClearMessages()
        Me.TextBox_Messages.Clear()
    End Sub

    'Form schließen
    '**************
    Private Sub MessageDialog_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        'verhindern, dass das Formular komplett gelöscht wird
        e.Cancel = True

        'Formular verstecken
        Call Me.Hide()

    End Sub

End Class
