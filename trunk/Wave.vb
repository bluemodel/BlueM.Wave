Imports System.IO

Public Class Wave

    'Eigenschaften
    '#############

    'Methoden
    '########

    'Datei öffnen Dialog anzeigen
    '****************************
    Private Sub Import_File_Dialog(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_TxtImport.Click

        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Import_File(Me.OpenFileDialog1.FileName)
        End If

    End Sub

    'Datei als Zeitreihe importieren
    '*******************************
    Private Sub Import_File(ByVal file As String)

        Select Case Path.GetExtension(file).ToUpper()

            Case ".ZRE"
                Import_ZRE(file)

            Case ".WEL", ".KWL"
                Import_WEL(file)

            Case Else
                Import_TXT(file)

        End Select

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
        If (Not IsNothing(WEL.Zeitreihen)) Then

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

        End If
        
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

    'Drag & Drop von Dateien verarbeiten
    '***********************************
    Private Sub Wave_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then

            Dim dateien() As String
            Dim i As Integer

            'Dateien einem Array zuweisen
            dateien = e.Data.GetData(DataFormats.FileDrop)

            'Die einzelnen Dateien importieren
            For i = 0 To dateien.GetUpperBound(0)
                Call Import_File(dateien(i))
            Next
        End If

    End Sub

    'Drag & Drop von Dateien zulassen
    '********************************
    Private Sub Wave_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
End Class
