Imports System.Windows.Forms

Public Class CutDialog

    ''' <summary>
    ''' Dialog zum Zuschneiden einer Zeitreihe
    ''' </summary>
    ''' <remarks></remarks>

    Private IsInitializing As Boolean
    Private colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private Anfang, Ende As DateTime
    Private zreOrig As Zeitreihe
    Private serie_cut As Steema.TeeChart.Styles.Line
    Private serie_ref As Steema.TeeChart.Styles.Line
    Public zreCut As Zeitreihe

    'Konstruktor
    '***********
    Public Sub New(ByRef zeitreihen As Dictionary(Of String, Zeitreihe))

        Me.IsInitializing = True

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.IsInitializing = False

        ' Add any initialization after the InitializeComponent() call.

        'Zeitreihen zu Listboxen hinzufügen
        For Each zre As Zeitreihe In zeitreihen.Values
            Me.ComboBox_ZeitreiheCut.Items.Add(zre)
            Me.ComboBox_ZeitreiheRef.Items.Add(zre)
        Next

    End Sub

    'Zuzuschneidende Zeitreihe wurde ausgewählt
    '******************************************
    Private Sub ComboBox_ZeitreiheCut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_ZeitreiheCut.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Dim i As Integer

        'Ausgewählte Zeitreihe
        Me.zreOrig = CType(Me.ComboBox_ZeitreiheCut.SelectedItem, Zeitreihe)

        Me.Anfang = Me.zreOrig.Anfangsdatum
        Me.Ende = Me.zreOrig.Enddatum

        'Zeitreihe in Chart anzeigen
        Call Me.TChart1.Chart.Series.Clear()
        serie_cut = New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        serie_cut.Title = Me.zreOrig.Title
        For i = 0 To Me.zreOrig.Length - 1
            serie_cut.Add(Me.zreOrig.XWerte(i), Me.zreOrig.YWerte(i))
        Next

        'ColorBand einrichten
        Call Me.TChart1.Tools.Clear()
        Me.colorBand1 = New Steema.TeeChart.Tools.ColorBand()
        Me.TChart1.Tools.Add(colorBand1)
        colorBand1.Axis = Me.TChart1.Axes.Bottom
        colorBand1.Brush.Color = Color.Coral
        colorBand1.Brush.Transparency = 50
        colorBand1.ResizeEnd = True
        colorBand1.ResizeStart = True
        colorBand1.EndLinePen.Visible = False
        colorBand1.StartLinePen.Visible = False

        'Original Anfangs- und Enddatum anzeigen
        Me.Label_Anfangsdatum.Text = Me.Anfang.ToString(Konstanten.Datumsformat)
        Me.Label_Enddatum.Text = Me.Ende.ToString(Konstanten.Datumsformat)

        Me.IsInitializing = True 'um eine Kettenreaktionen zu verhindern

        'DateTimePicker zurücksetzen
        Me.DateTimePicker_Anfang.MinDate = DateTimePicker.MinimumDateTime
        Me.DateTimePicker_Anfang.MaxDate = DateTimePicker.MaximumDateTime

        'Min und Max setzen
        Me.DateTimePicker_Anfang.MinDate = Me.Anfang.Date
        Me.DateTimePicker_Anfang.MaxDate = Me.Ende.Date

        Me.DateTimePicker_Ende.MinDate = Me.Anfang.Date
        Me.DateTimePicker_Ende.MaxDate = Me.Ende.Date

        Me.IsInitializing = False

        'Anzeige aktualisieren
        Call Me.updateColorband()
        Call Me.updateDateTimePickers()

    End Sub

    Private Sub TChart1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TChart1.DoubleClick
        Call Me.TChart1.ShowEditor()
    End Sub

    'ColorBand verändert
    '*******************
    Private Sub TChart1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TChart1.MouseUp

        Me.Anfang = DateTime.FromOADate(Me.colorBand1.Start)
        Me.Ende = DateTime.FromOADate(Me.colorBand1.End)

        Call Me.updateDateTimePickers()

    End Sub

    'Anfangsdatum verändert
    '**********************
    Private Sub DateTimePicker_Anfang_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_Anfang.ValueChanged, DateTimePicker_AnfangZeit.ValueChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.Anfang = Me.DateTimePicker_Anfang.Value.Date
        Me.Anfang = Me.Anfang.Add(Me.DateTimePicker_AnfangZeit.Value.TimeOfDay)

        Call Me.updateColorband()
        Call Me.updateDateTimePickers()

    End Sub

    'Enddatum verändert
    '******************
    Private Sub DateTimePicker_Ende_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_Ende.ValueChanged, DateTimePicker_EndeZeit.ValueChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.Ende = Me.DateTimePicker_Ende.Value.Date
        Me.Ende = Me.Ende.Add(Me.DateTimePicker_EndeZeit.Value.TimeOfDay)

        Call Me.updateColorband()

    End Sub

    'DateTimePicker aktualisieren
    '****************************
    Private Sub updateDateTimePickers()

        Me.IsInitializing = True 'um eine Kettenreaktion zu verhindern

        Me.DateTimePicker_Anfang.Value = Me.Anfang.Date
        Me.DateTimePicker_AnfangZeit.Value = Me.Anfang
        Me.DateTimePicker_Ende.Value = Me.Ende.Date
        Me.DateTimePicker_EndeZeit.Value = Me.Ende

        Me.IsInitializing = False

    End Sub

    'Referenz-Zeitreihe wurde ausgewählt 
    '***********************************
    Private Sub ComboBox_ZeitreiheRef_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_ZeitreiheRef.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Dim i As Integer
        Dim zreRef As Zeitreihe
        Dim tmp_anfang, tmp_ende As DateTime
        Dim answer As MsgBoxResult

        zreRef = Me.ComboBox_ZeitreiheRef.SelectedItem

        'Kontrolle ob Referenzreihe zu schneidende Zeitreihe abdeckt
        If (zreRef.Enddatum < Me.zreOrig.Anfangsdatum Or zreRef.Anfangsdatum > Me.zreOrig.Enddatum) Then
            MsgBox("Die beiden Zeitreihen decken keinen gemeinsamen Zeitraum ab!", MsgBoxStyle.Exclamation)
            'Abbrechen
            Exit Sub
        End If

        tmp_anfang = zreRef.Anfangsdatum
        tmp_ende = zreRef.Enddatum

       'Man kann nicht abschneiden, was nicht existiert
        If (zreRef.Anfangsdatum < Me.zreOrig.Anfangsdatum) Then
            answer = MsgBox("Das Anfangsdatum dieser Zeitreihe liegt vor dem Anfangsdatum der zu schneidenden Zeitreihe! " & eol & "Fortfahren?", MsgBoxStyle.OkCancel)
            If (answer = MsgBoxResult.OK) Then
                'Kleinstes mögliches Datum nehmen
                tmp_anfang = Me.zreOrig.Anfangsdatum
            Else
                'Abbrechen
                Exit Sub
            End If
        End If
        If (zreRef.Enddatum > Me.zreOrig.Enddatum) Then
            answer = MsgBox("Das Enddatum dieser Zeitreihe liegt hinter dem Enddatum der zu schneidenden Zeitreihe! " & eol & "Fortfahren?", MsgBoxStyle.OkCancel)
            If (answer = MsgBoxResult.OK) Then
                'Größtes mögliches Datum für Ende nehmen
                tmp_ende = Me.zreOrig.Enddatum
            Else
                'Abbrechen
                Exit Sub
            End If
        End If

        'Anfang und Ende setzen
        Me.Anfang = tmp_anfang
        Me.Ende = tmp_ende

        'Serie anzeigen
        Me.serie_ref = New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        Me.serie_ref.Color = Color.Gray
        For i = 0 To zreRef.Length - 1
            Me.serie_ref.Add(zreRef.XWerte(i), zreRef.YWerte(i))
        Next

        'Anzeige aktualisieren
        Call Me.updateColorband()
        Call Me.updateDateTimePickers()

    End Sub

    'Colorband aktualisieren
    '***********************
    Private Sub updateColorband()

        Me.colorBand1.Start = Me.Anfang.ToOADate()
        Me.colorBand1.End = Me.Ende.ToOADate()

    End Sub

    'OK gedrückt
    '***********
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Prüfung
        If (Me.ComboBox_ZeitreiheCut.SelectedIndex = -1) Then
            MsgBox("Bitte zuerst eine zuzuschneidende Zeitreihe auswählen!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        If (Me.Anfang >= Me.Ende) Then
            MsgBox("Der Anfang muss vor dem Ende liegen!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        'Zeitreihe zuschneiden
        Me.zreCut = Me.zreOrig.Clone()
        Call Me.zreCut.Cut(Me.Anfang, Me.Ende)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        Call Me.Close()
    End Sub

End Class
