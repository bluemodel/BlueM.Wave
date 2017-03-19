'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
'
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
'
Imports System.Windows.Forms

''' <summary>
''' Dialog zum Zuschneiden einer Zeitreihe
''' </summary>
''' <remarks></remarks>
Public Class CutDialog

    Private IsInitializing As Boolean
    Private colorBand1 As Steema.TeeChart.Tools.ColorBand
    Private Anfang, Ende As DateTime
    Private zreOrig As Dictionary(Of String, TimeSeries)
    Private serie_cut As Steema.TeeChart.Styles.Line
    Private serie_ref As Steema.TeeChart.Styles.Line

    Public zreCut As Dictionary(Of String, TimeSeries)
    Public Const labelAlle As String = "- ALL -"

    'Konstruktor
    '***********
    Public Sub New(ByRef zeitreihen As Dictionary(Of String, TimeSeries))

        Me.IsInitializing = True

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.zreCut = New Dictionary(Of String, TimeSeries)
        Me.zreOrig = zeitreihen

        'Comboboxen füllen
        'Option zum Zuschneiden von allen Reihen
        Me.ComboBox_ZeitreiheCut.Items.Add(labelAlle)
        'Zeitreihen hinzufügen
        For Each zre As TimeSeries In Me.zreOrig.Values
            Me.ComboBox_ZeitreiheCut.Items.Add(zre)
            Me.ComboBox_RefSeries.Items.Add(zre)
        Next

        'Diagramm formatieren
        Me.TChart1.Zoom.Allow = False
        Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.None

        Me.IsInitializing = False

    End Sub

    'Zuzuschneidende Zeitreihe wurde ausgewählt
    '******************************************
    Private Sub ComboBox_ZeitreiheCut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_ZeitreiheCut.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Dim i As Integer
        Dim zre As TimeSeries

        'Ausgewählte Zeitreihe
        If (Me.ComboBox_ZeitreiheCut.SelectedItem.ToString = labelAlle) Then

            'Anfangs- und Enddatum von allen Zeitreihen bestimmen
            Me.Anfang = DateTime.MaxValue
            Me.Ende = DateTime.MinValue
            For Each zre In Me.zreOrig.Values
                If (zre.StartDate < Me.Anfang) Then Me.Anfang = zre.StartDate
                If (zre.EndDate > Me.Ende) Then Me.Ende = zre.EndDate
            Next

            'Alle Zeitreihen in Chart anzeigen
            Call Me.TChart1.Chart.Series.Clear()
            For Each zre In Me.zreOrig.Values
                Dim series As New Steema.TeeChart.Styles.Line()
                For i = 0 To zre.Length - 1
                    series.Add(zre.Dates(i), zre.Values(i))
                    series.Title = zre.Title
                Next
                Me.TChart1.Chart.Series.Add(series)
            Next

        Else

            'Eine einzige Zeitreihe wurde ausgewählt
            zre = CType(Me.ComboBox_ZeitreiheCut.SelectedItem, TimeSeries)

            Me.Anfang = zre.StartDate
            Me.Ende = zre.EndDate

            'Zeitreihe in Chart anzeigen
            Call Me.TChart1.Chart.Series.Clear()
            serie_cut = New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
            serie_cut.Title = zre.Title
            For i = 0 To zre.Length - 1
                serie_cut.Add(zre.Dates(i), zre.Values(i))
            Next

        End If

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
        Me.Label_StartDateTime.Text = Me.Anfang.ToString(Konstanten.Datumsformate("default"))
        Me.Label_EndDateTime.Text = Me.Ende.ToString(Konstanten.Datumsformate("default"))

        Me.IsInitializing = True 'um eine Kettenreaktionen zu verhindern

        'DateTimePicker zurücksetzen
        Me.DateTimePicker_StartDate.MinDate = DateTimePicker.MinimumDateTime
        Me.DateTimePicker_StartDate.MaxDate = DateTimePicker.MaximumDateTime

        'Min und Max setzen
        Me.DateTimePicker_StartDate.MinDate = Me.Anfang.Date
        Me.DateTimePicker_StartDate.MaxDate = Me.Ende.Date

        Me.DateTimePicker_EndDate.MinDate = Me.Anfang.Date
        Me.DateTimePicker_EndDate.MaxDate = Me.Ende.Date

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
    Private Sub DateTimePicker_Anfang_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_StartDate.Leave, DateTimePicker_StartTime.Leave

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.Anfang = Me.DateTimePicker_StartDate.Value.Date
        Me.Anfang = Me.Anfang.Add(Me.DateTimePicker_StartTime.Value.TimeOfDay)

        Call Me.updateColorband()
        Call Me.updateDateTimePickers()

    End Sub

    'Enddatum verändert
    '******************
    Private Sub DateTimePicker_Ende_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker_EndDate.Leave, DateTimePicker_EndTime.Leave

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.Ende = Me.DateTimePicker_EndDate.Value.Date
        Me.Ende = Me.Ende.Add(Me.DateTimePicker_EndTime.Value.TimeOfDay)

        Call Me.updateColorband()

    End Sub

    'DateTimePicker aktualisieren
    '****************************
    Private Sub updateDateTimePickers()

        Me.IsInitializing = True 'um eine Kettenreaktion zu verhindern

        Me.DateTimePicker_StartDate.Value = Me.Anfang.Date
        Me.DateTimePicker_StartTime.Value = Me.Anfang
        Me.DateTimePicker_EndDate.Value = Me.Ende.Date
        Me.DateTimePicker_EndTime.Value = Me.Ende

        Me.IsInitializing = False

    End Sub

    'Referenz-Zeitreihe wurde ausgewählt 
    '***********************************
    Private Sub ComboBox_ZeitreiheRef_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_RefSeries.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Dim i As Integer
        Dim zre, zreRef As TimeSeries
        Dim tmp_anfang, tmp_ende As DateTime
        Dim answer As MsgBoxResult

        zreRef = Me.ComboBox_RefSeries.SelectedItem

        tmp_anfang = zreRef.StartDate
        tmp_ende = zreRef.EndDate

        If (Me.ComboBox_ZeitreiheCut.SelectedItem.ToString <> labelAlle) Then

            'Kontrolle ob Referenzreihe zu schneidende Zeitreihe abdeckt
            zre = Me.ComboBox_ZeitreiheCut.SelectedItem
            If (zreRef.EndDate < zre.StartDate Or zreRef.StartDate > zre.EndDate) Then
                MsgBox("Die beiden Zeitreihen decken keinen gemeinsamen Zeitraum ab!", MsgBoxStyle.Exclamation)
                'Abbrechen
                Exit Sub
            End If

            'Man kann nicht abschneiden, was nicht existiert
            If (zreRef.StartDate < zre.StartDate) Then
                answer = MsgBox("Das Anfangsdatum dieser Zeitreihe liegt vor dem Anfangsdatum der zu schneidenden Zeitreihe! " & eol & "Fortfahren?", MsgBoxStyle.OkCancel)
                If (answer = MsgBoxResult.Ok) Then
                    'Kleinstes mögliches Datum nehmen
                    tmp_anfang = zre.StartDate
                Else
                    'Abbrechen
                    Exit Sub
                End If
            End If
            If (zreRef.EndDate > zre.EndDate) Then
                answer = MsgBox("Das Enddatum dieser Zeitreihe liegt hinter dem Enddatum der zu schneidenden Zeitreihe! " & eol & "Fortfahren?", MsgBoxStyle.OkCancel)
                If (answer = MsgBoxResult.Ok) Then
                    'Größtes mögliches Datum für Ende nehmen
                    tmp_ende = zre.EndDate
                Else
                    'Abbrechen
                    Exit Sub
                End If
            End If

        End If

        'Neuen Anfang und Ende setzen
        Me.Anfang = tmp_anfang
        Me.Ende = tmp_ende

        'Referenzserie anzeigen
        Me.serie_ref = New Steema.TeeChart.Styles.Line(Me.TChart1.Chart)
        Me.serie_ref.Color = Color.Gray
        For i = 0 To zreRef.Length - 1
            Me.serie_ref.Add(zreRef.Dates(i), zreRef.Values(i))
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

        Dim newtitle As String
        Dim zre As TimeSeries

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

        'Zeitreihe(n) zuschneiden
        If (Me.ComboBox_ZeitreiheCut.SelectedItem.ToString = labelAlle) Then
            For Each zre In Me.zreOrig.Values
                newtitle = zre.Title & " (cut)"
                Me.zreCut.Add(newtitle, zre.Clone())
                Call Me.zreCut(newtitle).Cut(Me.Anfang, Me.Ende)
            Next
        Else
            zre = Me.ComboBox_ZeitreiheCut.SelectedItem
            newtitle = zre.Title & " (cut)"
            Me.zreCut.Add(newtitle, zre.Clone())
            Call Me.zreCut(newtitle).Cut(Me.Anfang, Me.Ende)
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        Call Me.Close()
    End Sub

End Class
