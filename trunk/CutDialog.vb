Imports System.Windows.Forms

Public Class CutDialog

    ''' <summary>
    ''' Dialog zum Zuschneiden einer Zeitreihe
    ''' </summary>
    ''' <remarks></remarks>

    Private IsInitializing As Boolean
    Private Const Datumsformat As String = "dd.MM.yyyy HH:mm"
    Private colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private Anfang, Ende As DateTime
    Private zreOrig As Zeitreihe
    Public zreCut As Zeitreihe

    'Konstruktor
    '***********
    Public Sub New()

        Me.IsInitializing = True

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.IsInitializing = False

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'Zeitreihenauswahl verändert
    '***************************
    Private Sub ComboBox_Zeitreihen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Zeitreihen.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Dim i As Integer
        Dim serie As Steema.TeeChart.Styles.Series

        'Ausgewählte Zeitreihe
        Me.zreOrig = CType(Me.ComboBox_Zeitreihen.SelectedItem, Zeitreihe)

        Me.Anfang = Me.zreOrig.Anfangsdatum
        Me.Ende = Me.zreOrig.Enddatum

        'Zeitreihe in Chart anzeigen
        Call Me.TChart1.Chart.Series.Clear()
        serie = New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        serie.Title = Me.zreOrig.Title
        For i = 0 To Me.zreOrig.Length - 1
            serie.Add(Me.zreOrig.XWerte(i), Me.zreOrig.YWerte(i))
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
        Me.Label_Anfangsdatum.Text = Me.Anfang.ToString(Datumsformat)
        Me.Label_Enddatum.Text = Me.Ende.ToString(Datumsformat)

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
        If (Me.ComboBox_Zeitreihen.SelectedIndex = 0) Then
            MsgBox("Bitte zuerst eine Zeitreihe auswählen!", MsgBoxStyle.Exclamation)
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
        Me.zreCut.Title &= " (cut)"
        Me.zreCut.Cut(Me.Anfang, Me.Ende)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

End Class
