Imports System.IO

Public Class Wave

    '*******************************************************************************
    '*******************************************************************************
    '**** Klasse Wave                                                           ****
    '****                                                                       ****
    '**** Tool zur Darstellung von Zeitreihen in TeeChart                       ****
    '****                                                                       ****
    '**** Autoren: Michael Bach, Felix Froehlich,                               ****
    '****          Dirk Muschalla, Christoph Hübner                             ****
    '****                                                                       ****
    '**** Fachgebiet Ingenieurhydrologie und Wasserbewirtschaftung              ****
    '**** TU Darmstadt                                                          ****
    '*******************************************************************************
    '*******************************************************************************

    'Eigenschaften
    '#############
    Private WithEvents colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private Const HelpURL As String = "http://130.83.196.154/BlueM/wiki/index.php/Wave"

    'Methoden
    '########

#Region "Form behavior"

    'Konstruktor
    '***********
    Public Sub New()

        ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
        InitializeComponent()

        'Charts einrichten
        '-----------------
        'Übersicht darf nicht gescrolled oder gezoomt werden
        Me.TChart2.Zoom.Allow = False
        Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        'Achsen
        Me.TChart1.Axes.Bottom.Automatic = False
        Me.TChart2.Axes.Bottom.Automatic = False

        'Übersichtslegende
        Me.TChart2.Legend.CheckBoxes = True

        'ColorBand einrichten
        Call Me.ColorBandEinrichten()

    End Sub

    'Form wird geladen
    '*****************
    Private Sub Wave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Übergabeparameter verarbeiten
        '-----------------------------
        For Each param As String In My.Application.CommandLineArgs

            'Dateien öffnen
            If (File.Exists(param)) Then
                Call Me.Import_File(param)
            End If

        Next

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
        Call Me.ResizeCharts()
    End Sub

    'Splitter Resize
    '***************
    Private Sub SplitContainer1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        Call Me.ResizeCharts()
    End Sub

#End Region 'Form behavior

#Region "Chart behvior"

    'Größe von Charts anpassen
    '*************************
    Private Sub ResizeCharts()
        Me.TChart2.Width = Me.SplitContainer1.Panel1.Width - 5
        Me.TChart2.Height = Me.SplitContainer1.Panel1.Height - 5
        Me.TChart1.Width = Me.SplitContainer1.Panel2.Width - 5
        Me.TChart1.Height = Me.SplitContainer1.Panel2.Height - 5
    End Sub

    'ColorBand einrichten
    '********************
    Private Sub ColorBandEinrichten()
        colorBand1 = New Steema.TeeChart.Tools.ColorBand
        Me.TChart2.Tools.Add(colorBand1)
        colorBand1.Axis = Me.TChart2.Axes.Bottom
        colorBand1.Brush.Color = Color.Coral
        colorBand1.Brush.Transparency = 50
        colorBand1.ResizeEnd = True
        colorBand1.ResizeStart = True
        colorBand1.EndLinePen.Visible = False
        colorBand1.StartLinePen.Visible = False
    End Sub

    'Cursor für TChart1
    '******************
    Private Sub TChart1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseDown
        Select Case e.Button
            Case Windows.Forms.MouseButtons.Right
                Cursor = Cursors.SizeAll
            Case Windows.Forms.MouseButtons.Left
                Cursor = Cursors.Cross
        End Select
    End Sub

    Private Sub TChart1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseUp
        Cursor = Cursors.Default
    End Sub

    'TChart1 Scrolled, Zoomed, ZoomUndone
    '************************************
    Private Sub TChart1Scrolled(ByVal sender As Object, ByVal e As System.EventArgs) Handles TChart1.Scroll, TChart1.Zoomed, TChart1.UndoneZoom
        Me.colorBand1.Start = Me.TChart1.Axes.Bottom.Minimum
        Me.colorBand1.End = Me.TChart1.Axes.Bottom.Maximum
    End Sub

    'ColorBand Resized
    '*****************
    Private Sub TChart2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart2.MouseUp
        Me.TChart1.Axes.Bottom.Minimum = Me.colorBand1.Start
        Me.TChart1.Axes.Bottom.Maximum = Me.colorBand1.End
    End Sub

    'TChart2 DoubleClick
    '*******************
    Private Sub TChart2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TChart2.DoubleClick
        Me.TChart2.ShowEditor()
    End Sub

#End Region 'Chart behavior'

#Region "UI"

    'Neu
    '***
    Private Sub Neu(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NeuToolStripButton.Click
        Me.TChart1.Clear()
        Me.TChart2.Clear()
        Call ColorBandEinrichten()
    End Sub

    'Öffnen
    '******
    Private Sub Öffnen(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÖffnenToolStripButton.Click
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Call Me.Import_File(Me.OpenFileDialog1.FileName)
        End If
    End Sub

    'Editieren
    '*********
    Private Sub Editieren(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripButton.Click
        Call Me.TChart1.ShowEditor()
    End Sub

    'Speichern
    '*********
    Private Sub Speichern(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeichernToolStripButton.Click
        Call Me.TChart2.Export.ShowExportDialog()
    End Sub

    'Drucken
    '*******
    Private Sub Drucken(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DruckenToolStripButton.Click
        Call Me.TChart1.Printer.Preview()
    End Sub

    'Kopieren (als PNG)
    '******************
    Private Sub Kopieren(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KopierenToolStripButton.Click
        Call Me.TChart1.Export.Image.PNG.CopyToClipboard()
    End Sub

    'Hilfe
    '*****
    Private Sub Hilfe(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HilfeToolStripButton.Click
        Call Process.Start(HelpURL)
    End Sub

    'Übersicht an/aus
    '****************
    Private Sub Übersicht_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÜbersichtToolStripButton.Click

        If (ÜbersichtToolStripButton.Checked) Then
            Me.SplitContainer1.Panel1Collapsed = False
            Call Me.ResizeCharts()
        Else
            Me.SplitContainer1.Panel1Collapsed = True
            Call Me.ResizeCharts()
        End If

    End Sub

#End Region 'UI

#Region "Funktionalität"

    'Datei importieren
    '*****************
    Friend Sub Import_File(ByVal file As String)

        Select Case Path.GetExtension(file).ToUpper()

            Case ".TEN"
                Call Me.Import_TEN(file)

            Case ".ZRE"
                Call Me.Import_ZRE(file)

            Case ".WEL", ".KWL"
                Call Me.Import_WEL(file)

            Case ".ASC"
                Call Me.Import_ASC(file)

            Case Else
                Call Me.Import_CSV(file)

        End Select

    End Sub

    'TEN-Datei importieren
    '*********************
    Public Sub Import_TEN(ByVal FileName As String)

        Dim res As DialogResult

        'Warnen, wenn bereits Serien vorhanden (Chart wird komplett überschrieben!)
        If (TChart1.Series.Count() > 0) Then
            res = MsgBox("Die vorhandenen Serien werden überschrieben!" & Chr(13) & Chr(10) & "Fortfahren?", MsgBoxStyle.OkCancel)
            If (Not res = Windows.Forms.DialogResult.OK) Then Exit Sub
        End If

        'Übersicht zurücksetzen und ausblenden (wird bei TEN nicht genutzt)
        Me.TChart2.Clear()
        Me.ÜbersichtToolStripButton.Checked = False

        'TEN-Datei importieren
        Call TChart1.Import.Template.Load(FileName)

    End Sub

    'ZRE-Datei importieren
    '*********************
    Public Sub Import_ZRE(ByVal FileName As String)

        'ZRE-Objekt instanzieren
        Dim ZRE As New ZRE(FileName)

        'Sofort Spalten auslesen (bei ZRE kein ImportDialog!)
        Call ZRE.SpaltenAuslesen()

        'ZRE einlesen
        Call ZRE.Read_File()

        'Serie zeichnen
        '--------------
        Call Me.Display_Series(ZRE.Zeitreihen(0))

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
    Public Sub Import_ASC(ByVal FileName As String)

        Dim i As Integer

        'ASC-Objekt instanzieren
        Dim ASC As New ASC(FileName)

        'Import-Dialog anzeigen
        Call Me.showImportDialog(ASC)

        'Serien zeichnen
        '---------------
        If (Not IsNothing(ASC.Zeitreihen)) Then

            For i = 0 To ASC.Zeitreihen.GetUpperBound(0)
                Call Me.Display_Series(ASC.Zeitreihen(i))
            Next

        End If

    End Sub

    'CSV-Datei importieren
    '*********************
    Public Sub Import_CSV(ByVal FileName As String)

        Dim i As Integer

        'CSV-Objekt instanzieren
        Dim CSV As New CSV(FileName)

        'Import-Dialog anzeigen
        Call Me.showImportDialog(CSV)

        'Serien zeichnen
        '---------------
        If (Not IsNothing(CSV.Zeitreihen)) Then

            For i = 0 To CSV.Zeitreihen.GetUpperBound(0)
                Call Me.Display_Series(CSV.Zeitreihen(i))
            Next

        End If

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

    'Serie im Chart anzeigen
    '***********************
    Public Sub Display_Series(ByVal zre As Zeitreihe)

        Dim i As Integer

        'Linien instanzieren
        Dim Line1 As New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        Dim Line2 As New Steema.TeeChart.Styles.Line(Me.TChart2.Chart)

        'X-Werte als Zeitdaten einstellen
        Line1.XValues.DateTime = True
        Line2.XValues.DateTime = True

        'Namen vergeben
        Line1.Title = zre.Title
        Line2.Title = zre.Title

        'Anzahl Serien bestimmen
        Dim AnzahlSerien As Integer = Me.TChart1.Series.Count

        'Neue Serie hinzufügen
        Me.TChart1.Chart.Series(AnzahlSerien - 1).Add(zre.XWerte, zre.YWerte)
        Me.TChart2.Chart.Series(AnzahlSerien - 1).Add(zre.XWerte, zre.YWerte)

        'Anzeigebereich anpassen
        '-----------------------
        'kleinsten und größten X-Wert bestimmen
        Dim Xmin, Xmax, tmpXmin, tmpXmax As DateTime
        Xmin = Date.FromOADate(Me.TChart1.Series(0).MinXValue)
        Xmax = Date.FromOADate(Me.TChart1.Series(0).MaxXValue)
        For i = 1 To AnzahlSerien - 1
            tmpXmin = Date.FromOADate(Me.TChart1.Series(i).MinXValue)
            tmpXmax = Date.FromOADate(Me.TChart1.Series(i).MaxXValue)
            If (tmpXmin < Xmin) Then Xmin = tmpXmin
            If (tmpXmax > Xmax) Then Xmax = tmpXmax
        Next

        'Übersicht neu skalieren
        Me.TChart2.Axes.Bottom.Minimum = Xmin.ToOADate()
        Me.TChart2.Axes.Bottom.Maximum = Xmax.ToOADate()

        'Auswahlbereich skalieren (nur bei der ersten hinzugefügten Serie)
        If (AnzahlSerien = 1) Then
            colorBand1.Start = Me.TChart2.Axes.Bottom.Minimum
            colorBand1.End = Me.TChart2.Axes.Bottom.Maximum
        End If

        'Hauptdiagramm neu skalieren
        Me.TChart1.Axes.Bottom.Minimum = colorBand1.Start
        Me.TChart1.Axes.Bottom.Maximum = colorBand1.End

    End Sub

#End Region 'Funktionalität

End Class
