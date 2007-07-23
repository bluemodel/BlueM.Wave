Imports System.IO

Public Class Zeitreihendarstellung

    'Eigenschaften
    '#############

    'Methoden
    '########

    'Textdatei als Zeitreihe importieren
    '***********************************
    Private Sub Import_File(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TxtImport.Click

        Dim DialogResult As DialogResult

        'Datei-öffnen Dialog anzeigen
        DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (DialogResult = Windows.Forms.DialogResult.OK) Then

            Select Case Path.GetExtension(Me.OpenFileDialog1.FileName).ToUpper()

                Case ".ZRE"
                    Import_ZRE(Me.OpenFileDialog1.FileName)

                Case ".WEL"
                    Import_WEL(Me.OpenFileDialog1.FileName)

                Case Else
                    Import_TXT(Me.OpenFileDialog1.FileName)

            End Select

        End If

    End Sub

    'ZRE-Datei importieren
    '*********************
    Public Sub Import_ZRE(ByVal FileName As String)

        'ZRE-Objekt intialisieren
        Dim ZRE As New ZRE(FileName)
        'Serie zeichnen
        '--------------
        Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        'Namen vergeben
        Line1.Title = ZRE.Zeitreihe.Title
        'X-Werte als Zeitdaten einstellen
        Line1.XValues.DateTime = True
        'Anzahl Serien bestimmen
        Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
        'Neue Serie hinzufügen
        Me.TChart1.Chart.Series(AnzahlSerien - 1).Add(ZRE.Zeitreihe.XWerte, ZRE.Zeitreihe.YWerte)

    End Sub

    'WEL-Datei importieren
    '*********************
    Public Sub Import_WEL(ByVal FileName As String)
        
        Dim i as Integer
        
        'WEL-Dialog initialisieren
        Dim WEL As New WEL()
        WEL.File = FileName

        If (WEL.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            For i = 0 To WEL.Zeitreihen.GetUpperBound(0)
                'Serie zeichnen
                '--------------
                Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
                'X-Werte als Zeitdaten einstellen
                Line1.XValues.DateTime = True
                'Namen vergeben
                Line1.Title = WEL.Zeitreihen(i).Title
                'Anzahl Serien bestimmen
                Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
                'Neue Serie hinzufügen
                Me.TChart1.Chart.Series(AnzahlSerien - 1).Add(WEL.Zeitreihen(i).XWerte, WEL.Zeitreihen(i).YWerte)
            Next
        End If

    End Sub

    'TXT-Datei importieren
    '*********************
    Public Sub Import_TXT(FileName as String)

        'TXT-Dialog initialisieren
        Dim TXT As New TXT()
        TXT.File = FileName

        'Einstellungen-Dialog anzeigen
        If (TXT.ShowDialog() = Windows.Forms.DialogResult.OK) Then

            Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
            'Namen vergeben
            Line1.Title = Path.GetFileNameWithoutExtension(TXT.File)
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
    End Sub

    'TEN-Datei importieren
    '*********************
    Public Sub Import_TEN(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TENImport.Click
        TChart1.Import.ShowImportDialog()
    End Sub

End Class
