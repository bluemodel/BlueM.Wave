Public Class ExportDiag

    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

    End Sub

    Private Sub ComboBox_Format_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Format.SelectedIndexChanged
        If ComboBox_Format.SelectedItem <> Konstanten.formate.ZRE Then
            MsgBox("Das geht noch nicht!", MsgBoxStyle.Information, "Nicht implementiert")
            ComboBox_Format.SelectedItem = Konstanten.formate.ZRE
        End If
    End Sub

    Private Sub CheckedListBox_Zeitreihen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckedListBox_Zeitreihen.SelectedIndexChanged

    End Sub
End Class