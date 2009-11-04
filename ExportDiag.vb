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

            Case Konstanten.Dateiformate.DAT_SWMM_MASS
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.DAT_SWMM_TIME
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.TXT
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.MultiExtended

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


Private Sub Button_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SelectAll.Click

    Dim i As Long

    Select Case ComboBox_Format.SelectedItem
        Case Konstanten.Dateiformate.TXT
            For i = 0 To Me.ListBox_Zeitreihen.Items.Count - 1
                Me.ListBox_Zeitreihen.SetSelected(i, True)
            Next
        Case Else
            MsgBox("Bei diesem Format ist keine mehrfachauswahl möglich")
    End Select

End Sub

End Class