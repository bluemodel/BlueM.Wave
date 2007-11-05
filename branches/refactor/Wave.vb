Imports System.IO

Public Class Wave

    'Eigenschaften
    '#############
    Private WithEvents colorBand1 As Steema.TeeChart.Tools.ColorBand

    'Methoden
    '########

#Region "UI"

    'Form wird geladen
    '*****************
    Private Sub Wave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Übergabeparameter verarbeiten
        For Each param As String In My.Application.CommandLineArgs

            'Dateien öffnen
            If (File.Exists(param)) Then
                Call Me.Import_File(param)
            End If

        Next

    End Sub

    'Datei öffnen Dialog anzeigen
    '****************************
    Private Sub Import_File_Dialog(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_OpenFile.Click, MenuItem_OpenFile.Click

        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Import_File(Me.OpenFileDialog1.FileName)
        End If

    End Sub

    'TEN-Datei importieren
    '*********************
    Public Sub Import_TEN(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_OpenTEN.Click, MenuItem_OpenTEN.Click
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

	'Form Resize
	'***********
    Private Sub Wave_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.TChart2.Width = Me.SplitContainer1.Panel1.Width
        Me.TChart2.Height = Me.SplitContainer1.Panel1.Height
        Me.TChart1.Width = Me.SplitContainer1.Panel2.Width
        Me.TChart1.Height = Me.SplitContainer1.Panel2.Height
    End Sub

	'Splitter Resize
	'***************
    Private Sub SplitContainer1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        Me.TChart2.Width = Me.SplitContainer1.Panel1.Width
        Me.TChart2.Height = Me.SplitContainer1.Panel1.Height
        Me.TChart1.Width = Me.SplitContainer1.Panel2.Width
        Me.TChart1.Height = Me.SplitContainer1.Panel2.Height
    End Sub

	'Ausschnitt aktualisieren
	'************************
    Private Sub TChart2_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart2.MouseUp
        Me.TChart1.Axes.Bottom.Minimum = Me.colorBand1.Start
        Me.TChart1.Axes.Bottom.Maximum = Me.colorBand1.End
    End Sub

    'Form schliessen
    '***************
    Private Sub MenuItem_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem_Exit.Click
        Me.Close()
    End Sub

#End Region 'UI

#Region "Funktionalität"

    'Datei als Zeitreihe importieren
    '*******************************
    Friend Sub Import_File(ByVal file As String)

        Select Case Path.GetExtension(file).ToUpper()

            Case ".ZRE"
                Call Me.Import_ZRE(file)

            Case ".WEL", ".KWL"
                Call Me.Import_WEL(file)

            Case ".ASC"
                Call Me.Import_ASC(file)

            Case Else
                Call Me.Import_TXT(file)

        End Select

    End Sub

    'ZRE-Datei importieren
    '*********************
    Public Sub Import_ZRE(ByVal FileName As String)

        'ZRE-Objekt instanzieren
        Dim ZRE As New ZRE(FileName)

        'ZRE einlesen
        Call ZRE.Read_File()

        'Serie zeichnen
        '--------------
        Call Me.Display_Series(ZRE.Zeitreihen(0))

    End Sub

    'Import-Formular anzeigen
    '*********************
    Private Sub showImportDialog(ByRef _datei As Dateiformat)

        _datei.ImportDiag = New ImportDiag(_datei)

        Dim DiagResult As DialogResult

        'Dialog anzeigen
        DiagResult = _datei.ImportDiag.ShowDialog()

        If (DiagResult = Windows.Forms.DialogResult.OK) Then
            'Datei einlesen
            Call _datei.Read_File()
        Else
            Exit Sub
        End If
    End Sub

    'WEL-Datei importieren
    '*********************
    Public Sub Import_WEL(ByVal FileName As String)

        Dim i As Integer

        'WEL-Objekt instanzieren
        Dim WEL As New WEL(FileName)

        'Import-Dialog anzeigen
        Call Me.showImportDialog(WEL)

        'Serien zeichnen
        '---------------
        If (Not IsNothing(WEL.Zeitreihen)) Then

            For i = 0 To WEL.Zeitreihen.GetUpperBound(0)
                Call Me.Display_Series(WEL.Zeitreihen(i))
            Next

        End If

    End Sub

    'ASC-Datei importieren (SMUSI)
    '*****************************
    Public Sub Import_ASC(ByVal FileName As String, ByVal ParamArray spaltenSel() As String)

        Dim i As Integer
        Dim Linie1 As Steema.TeeChart.Styles.Line
        Dim Linie2 As Steema.TeeChart.Styles.Line

        'WEL-Objekt instanzieren
        Dim ASC As New ASC(FileName, spaltenSel)

        'Serien zeichnen
        '---------------
        If (Not IsNothing(ASC.Zeitreihen)) Then

            Me.TChart1.Panel.Brush.Color = Color.White
            Me.TChart2.Panel.Brush.Color = Color.White
            Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.Vertical
            Me.TChart2.Zoom.Active = False

            For i = 0 To ASC.Zeitreihen.GetUpperBound(0)
                'Neue Serien Instanzieren
                Linie1 = New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
                Linie2 = New Steema.TeeChart.Styles.Line(Me.TChart2.Chart)
                'X-Werte als Zeitdaten einstellen
                Linie1.XValues.DateTime = True
                Linie2.XValues.DateTime = True
                'Namen vergeben
                Linie1.Title = ASC.Zeitreihen(i).Title
                Linie2.Title = ASC.Zeitreihen(i).Title
                'Anzahl Serien bestimmen
                Dim AnzahlSerien As Integer = Me.TChart1.Series.Count
                'Neue Serie hinzufügen
                Me.TChart1.Chart.Series(AnzahlSerien - 1).Add(ASC.Zeitreihen(i).XWerte, ASC.Zeitreihen(i).YWerte)
                Me.TChart2.Chart.Series(AnzahlSerien - 1).Add(ASC.Zeitreihen(i).XWerte, ASC.Zeitreihen(i).YWerte)
                Me.TChart1.Axes.Bottom.Automatic = False
                Me.TChart1.Axes.Bottom.Minimum = DateTime.Parse(ASC.Zeitreihen(i).XWerte(0)).ToOADate
                Me.TChart1.Axes.Bottom.Maximum = DateTime.Parse(ASC.Zeitreihen(i).XWerte(0)).AddDays(7).ToOADate
                Me.TChart2.Axes.Bottom.Automatic = False
                Me.TChart2.Axes.Bottom.Minimum = DateTime.Parse(ASC.Zeitreihen(i).XWerte(0)).ToOADate
                Me.TChart2.Axes.Bottom.Maximum = DateTime.Parse(ASC.Zeitreihen(i).XWerte(ASC.Zeitreihen(i).XWerte.GetUpperBound(0))).ToOADate
                'ColorBand einrichtenASC.Zeitreihen(i).XWerte
                If (IsNothing(colorBand1)) Then
                    colorBand1 = New Steema.TeeChart.Tools.ColorBand
                    Me.TChart2.Tools.Add(colorBand1)
                    colorBand1.Axis = Me.TChart2.Axes.Bottom
                    colorBand1.Brush.Color = Color.Coral
                    colorBand1.End = Me.TChart1.Axes.Bottom.Maximum
                    colorBand1.ResizeEnd = True
                    colorBand1.ResizeStart = True
                    colorBand1.Start = Me.TChart1.Axes.Bottom.Minimum
                    colorBand1.Brush.Transparency = 50
                End If
            Next

        End If

    End Sub

    'TXT-Datei importieren
    '*********************
    Public Sub Import_TXT(ByVal FileName As String)

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

    'Serie im Chart anzeigen
    '***********************
    Public Sub Display_Series(ByVal zre As Zeitreihe)

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

#End Region 'Funktionalität

End Class
