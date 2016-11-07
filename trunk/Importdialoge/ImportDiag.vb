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
Imports System.IO

Partial Public Class ImportDiag
    Inherits System.Windows.Forms.Form

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private IsInitializing As Boolean

    Private datei As Dateiformat

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByRef _dateiobjekt As Dateiformat)

        Call MyBase.New()

        IsInitializing = True

        Call InitializeComponent()

        Me.datei = _dateiobjekt

    End Sub

    'Form laden
    '**********
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Combobox Trennzeichen initialisieren
        Me.ComboBox_Trennzeichen.BeginUpdate()
        Me.ComboBox_Trennzeichen.Items.Add(Me.datei.semikolon)
        Me.ComboBox_Trennzeichen.Items.Add(Me.datei.komma)
        Me.ComboBox_Trennzeichen.Items.Add(Me.datei.punkt)
        Me.ComboBox_Trennzeichen.Items.Add(Me.datei.leerzeichen)
        Me.ComboBox_Trennzeichen.Items.Add(Me.datei.tab)
        Me.ComboBox_Trennzeichen.EndUpdate()

        'Combobox Dezimaltrennzeichen initialisieren
        Me.ComboBox_Dezimaltrennzeichen.BeginUpdate()
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.datei.punkt)
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.datei.komma)
        Me.ComboBox_Dezimaltrennzeichen.EndUpdate()

        'Combobox Datumsformat initialisieren
        For Each datumsformat As String In Konstanten.Datumsformate.Values
            If Not ComboBox_Datumsformat.Items.Contains(datumsformat) Then
                Me.ComboBox_Datumsformat.Items.Add(datumsformat)
            End If
        Next
        Me.ComboBox_Datumsformat.SelectedIndex = 0

        'Versuchen, die Spalten auszulesen (mit Standardeinstellungen)
        Call Me.datei.SpaltenAuslesen()

        'Anzeige aktualisieren
        Call Me.aktualisieren()

        'Ende der Initialisierung
        IsInitializing = False

        'Datei als Vorschau anzeigen
        Call Me.VorschauAnzeigen()

    End Sub

    'Vorschau der Datei anzeigen
    '***************************
    Private Sub VorschauAnzeigen()

        Const anzZeilen As Integer = 50 'maximal 50 Zeilen anzeigen
        Const anzSpalten As Integer = 3500 'maximal 3500 Spalten anzeigen (bei mehr wird immer umgebrochen!)
        Dim line, text, FileExt As String

        'Dateiname anzeigen
        Me.Label_Datei.Text &= " " & Path.GetFileName(Me.datei.File)

        'Vorschau anzeigen
        Dim fs As New FileStream(Me.datei.File, FileMode.Open, FileAccess.Read)
        Dim StrRead As New StreamReader(fs, System.Text.Encoding.GetEncoding("iso8859-1"))

        text = ""

        For i As Integer = 1 To anzZeilen

            line = StrRead.ReadLine.ToString()

            'gucken, ob Zeile zu lang
            If (line.Length > anzSpalten) Then
                line = line.Substring(0, anzSpalten) & " ..."
            End If

            text &= line & Konstanten.eol

            'gucken, ob Dateiende
            If (StrRead.Peek = -1) Then Exit For
        Next

        If (StrRead.Peek <> -1) Then
            text &= "..."
        End If

        Me.TextBox_Vorschau.Text = text

        StrRead.Close()
        fs.Close()

        FileExt = System.IO.Path.GetExtension(Me.datei.File).ToUpper()
        If FileExt = ".OUT" Then
            Me.TextBox_Vorschau.Text = Path.GetFileName(Me.datei.File) & " is a" & vbCrLf & "binary file." & vbCrLf & "Preview is not available!"
            Me.NumericUpDown_DatumsSpalte.Enabled = False
            Me.GroupBox_Spaltenmodus.Enabled = False
            Me.GroupBox_Dezimaltrennzeichen.Enabled = False
            Me.GroupBox_Einstellungen.Enabled = False
        End If

    End Sub

    'OK Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Ausgewählte Spalten
        Dim i As Integer
        If (Me.ListBox_YSpalten.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Y-Spalte auswählen!", MsgBoxStyle.Exclamation, "Fehler")
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            ReDim Me.datei.SpaltenSel(Me.ListBox_YSpalten.SelectedItems.Count - 1)
            For i = 0 To Me.ListBox_YSpalten.SelectedItems.Count - 1
                Me.datei.SpaltenSel(i) = Me.ListBox_YSpalten.SelectedItems(i)
            Next
        End If

    End Sub

    'Benutzereingabe verarbeiten
    '***************************
    Private Sub inputChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        TextBox_ZeileÜberschriften.TextChanged, TextBox_ZeileEinheiten.TextChanged, TextBox_ZeileDaten.TextChanged, CheckBox_Einheiten.CheckedChanged, ComboBox_Dezimaltrennzeichen.SelectedIndexChanged, RadioButton_Zeichengetrennt.CheckedChanged, ComboBox_Trennzeichen.SelectedIndexChanged, TextBox_Spaltenbreite.TextChanged, NumericUpDown_DatumsSpalte.ValueChanged, ComboBox_Datumsformat.SelectedIndexChanged, ComboBox_Datumsformat.LostFocus

        If (Me.IsInitializing = True) Then
            Exit Sub
        Else

            'Eingaben speichern
            '------------------
            Try

                'Datenzeile muss nach Überschriften und Einheiten sein!
                Me.IsInitializing = True
                If (Me.TextBox_ZeileDaten.Value <= Me.TextBox_ZeileÜberschriften.Value) Then
                    Me.TextBox_ZeileDaten.Value = Me.TextBox_ZeileÜberschriften.Value + 1
                End If
                If (Me.CheckBox_Einheiten.Checked _
                    And Me.TextBox_ZeileDaten.Value <= Me.TextBox_ZeileEinheiten.Value) Then
                    Me.TextBox_ZeileDaten.Value = Me.TextBox_ZeileEinheiten.Value + 1
                End If
                Me.IsInitializing = False

                'Zeilennummern
                Me.datei.iZeileUeberschriften = Me.TextBox_ZeileÜberschriften.Value
                Me.datei.iZeileDaten = Me.TextBox_ZeileDaten.Value

                'Einheiten
                Me.datei.UseEinheiten = Me.CheckBox_Einheiten.Checked
                If (Me.CheckBox_Einheiten.Checked) Then
                    Me.datei.iZeileEinheiten = Me.TextBox_ZeileEinheiten.Value
                End If

                'Datumsformat
                Me.datei.Datumsformat = Me.ComboBox_Datumsformat.Text

                'Dezimaltrennzeichen
                Me.datei.Dezimaltrennzeichen = Me.ComboBox_Dezimaltrennzeichen.SelectedItem

                'Spalteneinstellungen
                If (Me.RadioButton_Zeichengetrennt.Checked) Then
                    Me.datei.Zeichengetrennt = True
                    Me.datei.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
                Else
                    Me.datei.Zeichengetrennt = False
                    Me.datei.Spaltenbreite = Convert.ToInt32(Me.TextBox_Spaltenbreite.Text)
                End If

                'Datum
                Me.datei.XSpalte = Me.NumericUpDown_DatumsSpalte.Value - 1 'Immer eins weniger wie du ! 

                'Spalten neu auslesen
                Call Me.datei.SpaltenAuslesen()

                'Anzeige aktualisieren
                Call Me.aktualisieren()

                'Wenn alles glatt gelaufen ist:
                Me.StatusImage.Image = Global.BlueM.Wave.My.Resources.Resources.tick
                Me.StatusImage.Text = "OK"

            Catch ex As Exception
                'Bei Exception Status auf Fehler setzen
                Me.StatusImage.Image = Global.BlueM.Wave.My.Resources.Resources.fehler
                Me.StatusImage.Text = "Fehler"
            End Try

        End If

    End Sub

    'Anzeige aktualisieren
    '*********************
    Private Sub aktualisieren()

        Dim i As Integer

        'Dezimaltrennzeichen
        Me.ComboBox_Dezimaltrennzeichen.SelectedItem = Me.datei.Dezimaltrennzeichen

        'Zeilennummern
        Me.TextBox_ZeileÜberschriften.Text = Me.datei.iZeileUeberschriften
        Me.TextBox_ZeileDaten.Text = Me.datei.iZeileDaten

        'Einheiten
        If (Me.datei.UseEinheiten) Then
            Me.CheckBox_Einheiten.Checked = True
            Me.TextBox_ZeileEinheiten.Enabled = True
        Else
            Me.CheckBox_Einheiten.Checked = False
            Me.TextBox_ZeileEinheiten.Enabled = False
        End If
        Me.TextBox_ZeileEinheiten.Text = Me.datei.iZeileEinheiten

        'Spaltenformat
        If (Me.datei.Zeichengetrennt) Then
            Me.RadioButton_Zeichengetrennt.Checked = True
            Me.ComboBox_Trennzeichen.Enabled = True
            Me.TextBox_Spaltenbreite.Enabled = False
        Else
            Me.RadioButton_Spaltenbreite.Checked = True
            Me.ComboBox_Trennzeichen.Enabled = False
            Me.TextBox_Spaltenbreite.Enabled = True
        End If

        'Trennzeichen
        Me.ComboBox_Trennzeichen.SelectedItem = Me.datei.Trennzeichen

        'Datumsformat
        If Not ComboBox_Datumsformat.Items.Contains(Me.datei.Datumsformat) Then
            Me.ComboBox_Datumsformat.Items.Add(Me.datei.Datumsformat)
        End If
        Me.ComboBox_Datumsformat.SelectedItem = Me.datei.Datumsformat

        'Spaltenbreite
        Me.TextBox_Spaltenbreite.Text = Me.datei.Spaltenbreite

        'XSpalte
        Me.TextBox_XSpalte.Text = Me.datei.Spalten(Me.datei.XSpalte).Name

        'YSpalten
        Me.ListBox_YSpalten.Items.Clear()
        Call Me.ListBox_YSpalten.BeginUpdate()
        For i = 0 To datei.Spalten.Length - 1
            If (i <> datei.XSpalte) Then
                Me.ListBox_YSpalten.Items.Add(datei.Spalten(i))
            End If
        Next
        Call Me.ListBox_YSpalten.EndUpdate()

    End Sub

    ''' <summary>
    ''' Do a case-insensitive search for matching list items and select them
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextBox_Suche_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Suche.TextChanged

        Dim search, itemname As String

        Me.ListBox_YSpalten.ClearSelected()

        search = Me.TextBox_Suche.Text.ToLower()

        If (search = "") Then Return

        For i As Integer = 0 To Me.ListBox_YSpalten.Items.Count - 1
            itemname = Me.ListBox_YSpalten.Items(i).ToString().ToLower()
            If (itemname.Contains(search)) Then
                Me.ListBox_YSpalten.SetSelected(i, True)
            End If
        Next

    End Sub

    Private Sub Select_All(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SelectAll.Click

        Dim i As Long

        For i = 0 To Me.ListBox_YSpalten.Items.Count - 1
            Me.ListBox_YSpalten.SetSelected(i, True)
        Next

    End Sub

#End Region 'Methoden

End Class