Imports System.IO

Public Class Zeitreihendarstellung

    'Eigenschaften
    '#############
    Private TxtEinstellungen As TxtEinstellungen

    'Methoden
    '########

    'Textdatei als Zeitreihe importieren
    '***********************************
    Private Sub Zeitreihenimport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TxtImport.Click

        Dim DialogResult As DialogResult

        'Datei-öffnen Dialog anzeigen
        DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (DialogResult = Windows.Forms.DialogResult.OK) Then

                'Einstellungen-Dialog initialisieren
                TxtEinstellungen = New TxtEinstellungen()
                TxtEinstellungen.File = Me.OpenFileDialog1.FileName

                'Einstellungen-Dialog anzeigen
                DialogResult = Me.TxtEinstellungen.ShowDialog()
                If (DialogResult = Windows.Forms.DialogResult.OK) Then

                    Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
                    Me.TextSource1.Series = Line1

                    'X-Werte als Zeitdaten einstellen
                    Line1.XValues.DateTime = True
                    
                    'Einstellungen übergeben
                    '-----------------------
                    'Trennzeichen einstellen
                    Me.TextSource1.Separator = Me.TxtEinstellungen.Trennzeichen.Character
                    'Dezimaltrennzeichen einstellen
                    Me.TextSource1.DecimalSeparator = Me.TxtEinstellungen.Dezimaltrennzeichen.Character
                    'Anzahl Kopfzeilen einstellen
                    Me.TextSource1.HeaderLines = Me.TxtEinstellungen.AnzKopfzeilen
                    'Datei einlesen
                    Me.TextSource1.LoadFromFile(Me.TxtEinstellungen.File)

                    'Serie zeichnen
                    '--------------
                    'Anzahl Serien bestimmen
                    Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
                    'Neue Serie hinzufügen
                    Me.TChart1.Chart.Series(AnzahlSerien - 1).DataSource = Me.TextSource1.Series

                End If

        End If

    End Sub

    'TEN-Datei importieren
    '*********************
    Private Sub TENImport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TENImport.Click
        TChart1.Import.ShowImportDialog()
    End Sub

End Class
