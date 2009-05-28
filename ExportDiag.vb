Public Class ExportDiag

    'Formatauswahl verändert
    '***********************
    Private Sub ComboBox_Format_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Format.SelectedIndexChanged

        Select Case ComboBox_Format.SelectedItem

            Case Konstanten.Dateiformate.ZRE
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.REG_HYSTEM
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.REG_SMUSI
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.One

            Case Else
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.MultiExtended
                'Noch nicht implementiert
                MsgBox("Noch nicht implementiert!", MsgBoxStyle.Exclamation)
                ComboBox_Format.SelectedItem = Konstanten.Dateiformate.ZRE
        End Select

    End Sub

    'OK-Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click
        'Eingabekontrolle
        If (Me.ListBox_Zeitreihen.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Zeitreihe auswählen!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub
End Class