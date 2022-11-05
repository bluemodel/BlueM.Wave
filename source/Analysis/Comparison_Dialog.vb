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
Friend Class Comparison_Dialog

    Public Sub New(name1 As String, name2 As String)

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