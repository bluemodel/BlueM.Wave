Imports System.IO

Public Class Zeitreihendarstellung

    'Eigenschaften
    '#############

    'Methoden
    '########

    'Textdatei als Zeitreihe importieren
    '***********************************
    Private Sub Zeitreihenimport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TxtImport.Click

        Dim DialogResult As DialogResult
        Dim i As Integer

        'Datei-öffnen Dialog anzeigen
        DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (DialogResult = Windows.Forms.DialogResult.OK) Then

            Select case Path.GetExtension(Me.OpenFileDialog1.FileName).ToUpper()

                Case ".ZRE"
                    'ZRE-Objekt intialisieren
                    Dim ZRE as New ZRE(Me.OpenFileDialog1.FileName)
                    'Serie zeichnen
                    '--------------
                    Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
                    'Namen vergeben
                    Line1.Title = Path.GetFileNameWithoutExtension(Me.OpenFileDialog1.FileName)
                    'X-Werte als Zeitdaten einstellen
                    Line1.XValues.DateTime = True
                    'Anzahl Serien bestimmen
                    Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
                    'Neue Serie hinzufügen
                    Me.TChart1.Chart.Series(AnzahlSerien - 1).Add(ZRE.XWerte, ZRE.YWerte)

                Case ".WEL"
                    'WEL-Dialog initialisieren
                    Dim WEL As New WEL()
                    WEL.File = Me.OpenFileDialog1.FileName

                    DialogResult = WEL.ShowDialog()
                    If (DialogResult = Windows.Forms.DialogResult.OK) Then
                        For i = 0 To WEL.Spalten.GetUpperBound(0) - 1
                            'Serie zeichnen
                            '--------------
                            Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
                            'X-Werte als Zeitdaten einstellen
                            Line1.XValues.DateTime = True
                            'Namen vergeben
                            Line1.Title = WEL.Spalten(i + 1) '1. Spalte ist Datum_Zeit!
                            'Anzahl Serien bestimmen
                            Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
                            'Neue Serie hinzufügen
                            Me.TChart1.Chart.Series(AnzahlSerien - 1).Add(WEL.XWerte, WEL.AllYWerte(i).YWerte)
                        Next
                    End If


                Case Else
                    'TXT-Dialog initialisieren
                    Dim TXT As New TXT()
                    TXT.File = Me.OpenFileDialog1.FileName

                    'Einstellungen-Dialog anzeigen
                    DialogResult = TXT.ShowDialog()
                    If (DialogResult = Windows.Forms.DialogResult.OK) Then

                        Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
                        'Namen vergeben
                        Line1.Title = Path.GetFileNameWithoutExtension(Me.OpenFileDialog1.FileName)
                        'X-Werte als Zeitdaten einstellen
                        Line1.XValues.DateTime = True

                        'An TextSource übergeben
                        '-----------------------
                        Me.TextSource1.Series = Line1
                        'Trennzeichen einstellen
                        Me.TextSource1.Separator = TXT.Trennzeichen.Character
                        'Dezimaltrennzeichen einstellen
                        Me.TextSource1.DecimalSeparator = TXT.Dezimaltrennzeichen.Character
                        'Anzahl Kopfzeilen einstellen
                        Me.TextSource1.HeaderLines = TXT.AnzKopfzeilen
                        'Datei einlesen
                        Me.TextSource1.LoadFromFile(TXT.File)

                        'Serie zeichnen
                        '--------------
                        'Anzahl Serien bestimmen
                        Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
                        'Neue Serie hinzufügen
                        Me.TChart1.Chart.Series(AnzahlSerien - 1).DataSource = Me.TextSource1.Series

                    End If

            End Select

        End If

    End Sub

    'TEN-Datei importieren
    '*********************
    Private Sub TENImport(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TENImport.Click
        TChart1.Import.ShowImportDialog()
    End Sub

End Class
