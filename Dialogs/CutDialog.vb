'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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
''' Dialog for cutting time series
''' </summary>
''' <remarks></remarks>
Friend Class CutDialog

    Private IsInitializing As Boolean
    Private cutStart, cutEnd As DateTime
    Private zreOrig As List(Of TimeSeries)

    Public zreCut As List(Of TimeSeries)
    Public Const labelAlle As String = "- ALL -"

    'Konstruktor
    '***********
    Public Sub New(ByRef zeitreihen As List(Of TimeSeries), initialStart As DateTime, initialEnd As DateTime)

        Me.IsInitializing = True

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.zreCut = New List(Of TimeSeries)
        Me.zreOrig = zeitreihen

        Me.cutStart = initialStart
        Me.cutEnd = initialEnd

        'set CurrentCulture for MaskedTextBoxes
        Me.MaskedTextBox_cutStart.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_cutStart.FormatProvider = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_cutEnd.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_cutEnd.FormatProvider = Globalization.CultureInfo.CurrentCulture

        'Comboboxen füllen
        'Option zum Zuschneiden von allen Reihen
        Me.ComboBox_ZeitreiheCut.Items.Add(labelAlle)
        'Zeitreihen hinzufügen
        For Each zre As TimeSeries In Me.zreOrig
            Me.ComboBox_ZeitreiheCut.Items.Add(zre)
            Me.ComboBox_RefSeries.Items.Add(zre)
        Next

        'Update MaskedTextboxes
        Call Me.updateMaskedTextBoxes()

        Me.IsInitializing = False

    End Sub

    'Zuzuschneidende Zeitreihe wurde ausgewählt
    '******************************************
    Private Sub ComboBox_ZeitreiheCut_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox_ZeitreiheCut.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Dim zre As TimeSeries
        Dim earliestStart, latestEnd As DateTime

        earliestStart = DateTime.MaxValue
        latestEnd = DateTime.MinValue

        'Ausgewählte Zeitreihe
        If (Me.ComboBox_ZeitreiheCut.SelectedItem.ToString = labelAlle) Then

            'Anfangs- und Enddatum von allen Zeitreihen bestimmen
            For Each zre In Me.zreOrig
                If (zre.StartDate < earliestStart) Then earliestStart = zre.StartDate
                If (zre.EndDate > latestEnd) Then latestEnd = zre.EndDate
            Next

        Else

            'Eine einzige Zeitreihe wurde ausgewählt
            zre = CType(Me.ComboBox_ZeitreiheCut.SelectedItem, TimeSeries)

            earliestStart = zre.StartDate
            latestEnd = zre.EndDate

        End If

        Me.IsInitializing = True 'um eine Kettenreaktionen zu verhindern

        'Min und Max setzen
        Me.Label_StartDateTime.Text = "Min: " & earliestStart.ToString(Helpers.CurrentDateFormat)
        Me.Label_EndDateTime.Text = "Max: " & latestEnd.ToString(Helpers.CurrentDateFormat)

        Me.IsInitializing = False

        'Anzeige aktualisieren
        Call Me.updateMaskedTextBoxes()

    End Sub

    ''' <summary>
    ''' Handles KeyDown in start and end date textboxes
    ''' Resets the color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBoxKeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MaskedTextBox_cutStart.KeyDown, MaskedTextBox_cutEnd.KeyDown
        CType(sender, MaskedTextBox).ForeColor = DefaultForeColor
    End Sub

    ''' <summary>
    ''' Handles ValidationCompleted of start and end dates
    ''' Checks whether input is valid DateTime
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBoxValidationCompleted(sender As System.Object, e As TypeValidationEventArgs) Handles MaskedTextBox_cutStart.TypeValidationCompleted, MaskedTextBox_cutEnd.TypeValidationCompleted
        If Not e.IsValidInput Then
            e.Cancel = True
            CType(sender, MaskedTextBox).ForeColor = Color.Red
        End If
    End Sub

    ''' <summary>
    ''' Handles cut start date changed in form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBox_cutStart_Validated(sender As System.Object, e As System.EventArgs) Handles MaskedTextBox_cutStart.Validated

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.cutStart = Me.MaskedTextBox_cutStart.Text
    End Sub

    ''' <summary>
    ''' Handles cut end date changed in form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBox_cutEnd_Validated(sender As System.Object, e As System.EventArgs) Handles MaskedTextBox_cutEnd.Validated

        If (Me.IsInitializing) Then
            Exit Sub
        End If

        Me.cutEnd = Me.MaskedTextBox_cutEnd.Text

    End Sub

    ''' <summary>
    ''' Updates the MaskedTextBoxes displaying the currently set cut start and end dates
    ''' </summary>
    Private Sub updateMaskedTextBoxes()

        Me.IsInitializing = True 'um eine Kettenreaktion zu verhindern

        Me.MaskedTextBox_cutStart.Text = Me.cutStart
        Me.MaskedTextBox_cutEnd.Text = Me.cutEnd

        Me.IsInitializing = False

    End Sub

    ''' <summary>
    ''' Handles selection of a reference time series for cutting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ComboBox_ZeitreiheRef_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox_RefSeries.SelectedIndexChanged

        If (Me.IsInitializing) Then
            Exit Sub
        End If

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
                MsgBox("The two time series do not overlap!", MsgBoxStyle.Exclamation)
                'Abbrechen
                Exit Sub
            End If

            'Man kann nicht abschneiden, was nicht existiert
            If (zreRef.StartDate < zre.StartDate) Then
                answer = MsgBox($"The start date of this time series is earlier than the start of the time series that you want to cut. Nothing will be cut from the start.{eol}Continue?", MsgBoxStyle.OkCancel)
                If (answer = MsgBoxResult.Ok) Then
                    'Kleinstes mögliches Datum nehmen
                    tmp_anfang = zre.StartDate
                Else
                    'Abbrechen
                    Exit Sub
                End If
            End If
            If (zreRef.EndDate > zre.EndDate) Then
                answer = MsgBox($"The end date of this time series is later than the end of the time series that you want to cut. Nothing will be cut from the end. {eol}Continue?", MsgBoxStyle.OkCancel)
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
        Me.cutStart = tmp_anfang
        Me.cutEnd = tmp_ende

        'Anzeige aktualisieren
        Call Me.updateMaskedTextBoxes()

    End Sub

    'OK gedrückt
    '***********
    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click

        Dim zre, ts_cut As TimeSeries

        'Prüfung
        If (Me.ComboBox_ZeitreiheCut.SelectedIndex = -1) Then
            MsgBox("Please select a time series to be cut!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        If (Me.cutStart >= Me.cutEnd) Then
            MsgBox("The end must be later than the start!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        'Zeitreihe(n) zuschneiden
        If (Me.ComboBox_ZeitreiheCut.SelectedItem.ToString = labelAlle) Then
            For Each zre In Me.zreOrig
                ts_cut = zre.Clone()
                Call ts_cut.Cut(Me.cutStart, Me.cutEnd)
                Me.zreCut.Add(ts_cut)
            Next
        Else
            zre = Me.ComboBox_ZeitreiheCut.SelectedItem
            ts_cut = zre.Clone()
            Call ts_cut.Cut(Me.cutStart, Me.cutEnd)
            Me.zreCut.Add(ts_cut)
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Button_Cancel_Click(sender As System.Object, e As System.EventArgs) Handles Button_Cancel.Click
        Call Me.Close()
    End Sub

End Class
