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

Friend Class ImportDiag
    Inherits System.Windows.Forms.Form

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private IsInitializing As Boolean

    Private datei As FileFormatBase

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByRef _dateiobjekt As FileFormatBase)

        Call MyBase.New()

        IsInitializing = True

        Call InitializeComponent()

        Me.datei = _dateiobjekt

    End Sub

    'Form laden
    '**********
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Combobox Trennzeichen initialisieren
        Me.ComboBox_Separator.BeginUpdate()
        Me.ComboBox_Separator.Items.Add(Constants.semicolon)
        Me.ComboBox_Separator.Items.Add(Constants.comma)
        Me.ComboBox_Separator.Items.Add(Constants.period)
        Me.ComboBox_Separator.Items.Add(Constants.space)
        Me.ComboBox_Separator.Items.Add(Constants.tab)
        Me.ComboBox_Separator.EndUpdate()

        'Combobox Dezimaltrennzeichen initialisieren
        Me.ComboBox_DecimalMark.BeginUpdate()
        Me.ComboBox_DecimalMark.Items.Add(Constants.period)
        Me.ComboBox_DecimalMark.Items.Add(Constants.comma)
        Me.ComboBox_DecimalMark.EndUpdate()

        'Combobox Datumsformat initialisieren
        For Each datumsformat As String In Helpers.DateFormats.Values
            If Not ComboBox_Dateformat.Items.Contains(datumsformat) Then
                Me.ComboBox_Dateformat.Items.Add(datumsformat)
            End If
        Next
        Me.ComboBox_Dateformat.SelectedIndex = 0

        'Versuchen, die Spalten auszulesen (mit Standardeinstellungen)
        Call Me.datei.ReadColumns()

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
        Dim line, text As String

        'Dateiname anzeigen
        Me.Label_File.Text &= " " & Path.GetFileName(Me.datei.File)

        'Workaround for SWMM5 binary output files (*.OUT)
        If IO.Path.GetExtension(Me.datei.File).ToUpper() = FileFactory.FileExtOUT Then
            Me.TextBox_Preview.Text = Path.GetFileName(Me.datei.File) & " is a SWMM 5 binary output file." & eol & "Preview is not available!"
            'Disable all other fields
            Me.GroupBox_Dateformat.Enabled = False
            Me.GroupBox_Columns.Enabled = False
            Me.GroupBox_DecimalMark.Enabled = False
            Me.GroupBox_Settings.Enabled = False

            Exit Sub
        End If

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

            text &= line & Constants.eol

            'gucken, ob Dateiende
            If (StrRead.Peek = -1) Then Exit For
        Next

        If (StrRead.Peek <> -1) Then
            text &= "..."
        End If

        Me.TextBox_Preview.Text = text

        StrRead.Close()
        fs.Close()

    End Sub

    'OK Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Ausgewählte Spalten
        Dim i As Integer
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MsgBox("Please select at least one series!", MsgBoxStyle.Exclamation, "Wave")
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            ReDim Me.datei.SelectedColumns(Me.ListBox_Series.SelectedItems.Count - 1)
            For i = 0 To Me.ListBox_Series.SelectedItems.Count - 1
                Me.datei.SelectedColumns(i) = Me.ListBox_Series.SelectedItems(i)
            Next
        End If

    End Sub

    'Benutzereingabe verarbeiten
    '***************************
    Private Sub inputChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        NumericUpDown_LineTitles.TextChanged, NumericUpDown_LineUnits.TextChanged, NumericUpDown_LineData.TextChanged, CheckBox_Units.CheckedChanged, ComboBox_DecimalMark.SelectedIndexChanged, RadioButton_CharSeparated.CheckedChanged, ComboBox_Separator.SelectedIndexChanged, TextBox_ColumnWidth.TextChanged, NumericUpDown_ColumnDateTime.ValueChanged, ComboBox_Dateformat.SelectedIndexChanged, ComboBox_Dateformat.LostFocus

        If (Me.IsInitializing = True) Then
            Exit Sub
        Else

            'Eingaben speichern
            '------------------
            Try

                'Datenzeile muss nach Überschriften und Einheiten sein!
                Me.IsInitializing = True
                If (Me.NumericUpDown_LineData.Value <= Me.NumericUpDown_LineTitles.Value) Then
                    Me.NumericUpDown_LineData.Value = Me.NumericUpDown_LineTitles.Value + 1
                End If
                If (Me.CheckBox_Units.Checked _
                    And Me.NumericUpDown_LineData.Value <= Me.NumericUpDown_LineUnits.Value) Then
                    Me.NumericUpDown_LineData.Value = Me.NumericUpDown_LineUnits.Value + 1
                End If
                Me.IsInitializing = False

                'Zeilennummern
                Me.datei.iLineHeadings = Me.NumericUpDown_LineTitles.Value
                Me.datei.iLineData = Me.NumericUpDown_LineData.Value

                'Einheiten
                Me.datei.UseUnits = Me.CheckBox_Units.Checked
                If (Me.CheckBox_Units.Checked) Then
                    Me.datei.iLineUnits = Me.NumericUpDown_LineUnits.Value
                End If

                'Datumsformat
                Me.datei.Dateformat = Me.ComboBox_Dateformat.Text

                'Dezimaltrennzeichen
                Me.datei.DecimalSeparator = Me.ComboBox_DecimalMark.SelectedItem

                'Spalteneinstellungen
                If (Me.RadioButton_CharSeparated.Checked) Then
                    Me.datei.IsColumnSeparated = True
                    Me.datei.Separator = Me.ComboBox_Separator.SelectedItem
                Else
                    Me.datei.IsColumnSeparated = False
                    Me.datei.ColumnWidth = Convert.ToInt32(Me.TextBox_ColumnWidth.Text)
                End If

                'Datum
                Me.datei.DateTimeColumnIndex = Me.NumericUpDown_ColumnDateTime.Value - 1 'Immer eins weniger wie du ! 

                'Spalten neu auslesen
                Call Me.datei.ReadColumns()

                'Anzeige aktualisieren
                Call Me.aktualisieren()

                'Wenn alles glatt gelaufen ist:
                Me.StatusImage.Image = Global.BlueM.Wave.My.Resources.Resources.tick
                Me.StatusImage.Text = "OK"

            Catch ex As Exception
                'Bei Exception Status auf Fehler setzen
                Me.StatusImage.Image = Global.BlueM.Wave.My.Resources.Resources.fehler
                Me.StatusImage.Text = "Error"
            End Try

        End If

    End Sub

    'Anzeige aktualisieren
    '*********************
    Private Sub aktualisieren()

        Dim i As Integer

        'Dezimaltrennzeichen
        Me.ComboBox_DecimalMark.SelectedItem = Me.datei.DecimalSeparator

        'Zeilennummern
        Me.NumericUpDown_LineTitles.Text = Me.datei.iLineHeadings
        Me.NumericUpDown_LineData.Text = Me.datei.iLineData

        'Einheiten
        If (Me.datei.UseUnits) Then
            Me.CheckBox_Units.Checked = True
            Me.NumericUpDown_LineUnits.Enabled = True
        Else
            Me.CheckBox_Units.Checked = False
            Me.NumericUpDown_LineUnits.Enabled = False
        End If
        Me.NumericUpDown_LineUnits.Text = Me.datei.iLineUnits

        'Spaltenformat
        If (Me.datei.IsColumnSeparated) Then
            Me.RadioButton_CharSeparated.Checked = True
            Me.ComboBox_Separator.Enabled = True
            Me.TextBox_ColumnWidth.Enabled = False
        Else
            Me.RadioButton_FixedWidth.Checked = True
            Me.ComboBox_Separator.Enabled = False
            Me.TextBox_ColumnWidth.Enabled = True
        End If

        'Trennzeichen
        Me.ComboBox_Separator.SelectedItem = Me.datei.Separator

        'Datumsformat
        If Not ComboBox_Dateformat.Items.Contains(Me.datei.Dateformat) Then
            Me.ComboBox_Dateformat.Items.Add(Me.datei.Dateformat)
        End If
        Me.ComboBox_Dateformat.SelectedItem = Me.datei.Dateformat

        'Spaltenbreite
        Me.TextBox_ColumnWidth.Text = Me.datei.ColumnWidth

        'XSpalte
        Me.TextBox_ColumnDateTime.Text = Me.datei.Columns(Me.datei.DateTimeColumnIndex).Name

        'YSpalten
        Me.ListBox_Series.Items.Clear()
        Call Me.ListBox_Series.BeginUpdate()
        For i = 0 To datei.Columns.Length - 1
            If (i <> datei.DateTimeColumnIndex) Then
                Me.ListBox_Series.Items.Add(datei.Columns(i))
            End If
        Next
        Call Me.ListBox_Series.EndUpdate()

    End Sub

    ''' <summary>
    ''' Do a case-insensitive search for matching list items and select them
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextBox_Suche_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Search.TextChanged

        Dim search, itemname As String

        Me.ListBox_Series.ClearSelected()

        search = Me.TextBox_Search.Text.ToLower()

        If (search = "") Then Return

        For i As Integer = 0 To Me.ListBox_Series.Items.Count - 1
            itemname = Me.ListBox_Series.Items(i).ToString().ToLower()
            If (itemname.Contains(search)) Then
                Me.ListBox_Series.SetSelected(i, True)
            End If
        Next

    End Sub

    Private Sub Select_All(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SelectAll.Click

        Dim i As Long

        For i = 0 To Me.ListBox_Series.Items.Count - 1
            Me.ListBox_Series.SetSelected(i, True)
        Next

    End Sub

#End Region 'Methoden

End Class