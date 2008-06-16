Public NotInheritable Class AboutDialog

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Legen Sie den Titel des Formulars fest.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("Info über {0}", ApplicationTitle)
        ' Initialisieren Sie den gesamten Text, der im Infofeld angezeigt wird.
        ' TODO: Passen Sie die Assemblyinformationen der Anwendung im Bereich "Anwendung" des Dialogfelds für die 
        '    Projekteigenschaften (im Menü "Projekt") an.
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description
    End Sub

    Private Sub Link_Clicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles TextBoxDescription.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
