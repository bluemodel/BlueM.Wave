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
Friend NotInheritable Class AboutBox

    Private Sub AboutBox_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'get AssemblyInfo from the executing assembly rather than from My.Application because the latter may be an application other than Wave
        Dim info As ApplicationServices.AssemblyInfo = New ApplicationServices.AssemblyInfo(Reflection.Assembly.GetExecutingAssembly)
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If info.Title <> "" Then
            ApplicationTitle = info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(info.AssemblyName)
        End If
        Me.Text = $"About {ApplicationTitle}"
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = info.ProductName
        Me.LabelVersion.Text = $"Version {info.Version}"
        Me.LabelCopyright.Text = info.Copyright
        Me.LabelCompanyName.Text = info.CompanyName
        Me.TextBox_Description.Text = info.Description
    End Sub

    Private Sub OKButton_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub LinkLabel1_Click(sender As System.Object, e As System.EventArgs) Handles LinkLabel1.Click
        Process.Start(Me.LinkLabel1.Text)
    End Sub

End Class
