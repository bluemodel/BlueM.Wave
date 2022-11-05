'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
'
Friend Class LinearRegression_Dialog

    Public Sub New(ByVal zeitreihe1 As String, ByVal zeitreihe2 As String)
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Radiobuttons die Titel der Zeitreihen zuweisen
        Me.RadioButton1.Text = zeitreihe1
        Me.RadioButton2.Text = zeitreihe2
    End Sub

    Public Function getNrLueckenhafteReihe() As Integer
        'Gewählte Zeitreihe an Analyse zurückgeben
        If (Me.RadioButton1.Checked) Then
            Return 1
        Else
            Return 2
        End If
    End Function

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
