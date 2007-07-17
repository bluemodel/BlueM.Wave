Public Class Zeitreihendarstellung

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Zeitreihenimport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZR_Import.Click
        Dim DialogResult As DialogResult = OpenFileDialog1.ShowDialog()
        If DialogResult = Windows.Forms.DialogResult.OK Then
            Dim Line1 As New Steema.TeeChart.Styles.Line(TChart1.Chart)
            TextSource1.Series = Line1
            'Line1.XValues.DateTime = False
            Line1.XValues.DateTime = True
            'Me.TextSource1.DecimalSeparator = Global.Microsoft.VisualBasic.ChrW(46)
            TextSource1.LoadFromFile(OpenFileDialog1.FileName)
            Dim AnzahlSerien As Integer = TChart1.Series.Count
            TChart1.Chart.Series(AnzahlSerien - 1).DataSource = TextSource1.Series

        End If
    End Sub

    Private Sub TENImport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TEN_Import.Click
        TChart1.Import.ShowImportDialog()
    End Sub

End Class
