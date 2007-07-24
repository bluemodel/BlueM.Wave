Imports System.IO

Public Class Wave

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

        'ZRE-Objekt instanzieren
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
    Public Sub Import_WEL(ByVal FileName As String, ByVal ParamArray spaltenSel() As String)

        Dim i As Integer

        'WEL-Objekt instanzieren
        Dim WEL As New WEL(FileName, spaltenSel)

        'Serien zeichnen
        '---------------
        For i = 0 To WEL.Zeitreihen.GetUpperBound(0)

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

    End Sub

    'TXT-Datei importieren
    '*********************
    Public Sub Import_TXT(FileName as String)

        'TXT-Objekt instanzieren
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

    'Zeitreihe importieren
    '*********************
    Public Sub Import_Zeitreihe(ByVal zre As Zeitreihe)

        Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        'X-Werte als Zeitdaten einstellen
        Line1.XValues.DateTime = True
        'Namen vergeben
        Line1.Title = zre.Title
        'Anzahl Serien bestimmen
        Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
        'Neue Serie hinzufügen
        Me.TChart1.Chart.Series(AnzahlSerien - 1).Add(zre.XWerte, zre.YWerte)

    End Sub

    'TEN-Datei importieren
    '*********************
    Public Sub Import_TEN(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TENImport.Click
        TChart1.Import.ShowImportDialog()
    End Sub

End Class
