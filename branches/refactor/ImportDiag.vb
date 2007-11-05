Imports System.IO

Partial Public Class ImportDiag
    Inherits System.Windows.Forms.Form

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private IsInitializing As Boolean

    Private datei As Dateiformat


#End Region

#Region "Properties"

    'Properties
    '##########

#End Region 'Properties

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

        Call Me.updateDisplay()

        'Datei als Vorschau anzeigen
        Me.Label_Datei.Text += " " & Path.GetFileName(Me.datei.File)
        Me.RichTextBox_Vorschau.LoadFile(Me.datei.File, RichTextBoxStreamType.PlainText)

        'Ende der Initialisierung
        IsInitializing = False

    End Sub


    'Überprüfung, ob eine Spalte ausgewählt ist
    '******************************************
    Private Function isSelected(ByVal spalte As String) As Boolean

        isSelected = False
        Dim i As Integer

        For i = 0 To Me.datei.SpaltenSel.GetUpperBound(0)
            If (Me.datei.SpaltenSel(i) = spalte) Then
                Return True
            End If
        Next

    End Function

    'OK Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Einstellungen übernehmen
        '------------------------
        'Trennzeichen
        Me.datei.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem

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

        'Datei einlesen
        Call Me.datei.Read_File()

    End Sub

    'Wenn Spaltenart geändert wird, Spalten neu auslesen
    '***************************************************
    Private Sub RadioButton_Spalten_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton_Spaltenbreite.CheckedChanged

        'If (Me.IsInitializing = True) Then
        '    Exit Sub
        'Else
        If (Me.RadioButton_Zeichengetrennt.Checked) Then
            Me.datei.Zeichengetrennt = True
        Else
            Me.datei.Zeichengetrennt = False
        End If

        Call Me.datei.SpaltenAuslesen()

        Call Me.updateDisplay()
        'End If

    End Sub

    'Wenn Trennzeichen geändert wird, Spalten neu auslesen
    '*****************************************************
    Private Sub ComboBox_Trennzeichen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Trennzeichen.SelectedIndexChanged

        'If (Me.IsInitializing = True) Then
        '    Exit Sub
        'Else
        Me.datei.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
        Call Me.datei.SpaltenAuslesen()

        Call Me.updateDisplay()
        'End If

    End Sub

    'Wenn Spaltenbreite geändert wird, Spalten neu auslesen
    '******************************************************
    Private Sub TextBox_Spaltenbreite_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Spaltenbreite.TextChanged

        'If (Me.IsInitializing = True) Then
        '    Exit Sub
        'Else
        If (Convert.ToInt16(Me.TextBox_Spaltenbreite.Text) < 1) Then
            MsgBox("Bitte eine Zahl größer 0 für die Spaltenbreite angeben!", MsgBoxStyle.Exclamation, "Fehler")
            Me.TextBox_Spaltenbreite.Focus()
            Exit Sub
        End If

        Me.datei.Spaltenbreite = Convert.ToInt16(Me.TextBox_Spaltenbreite.Text)

        Call Me.datei.SpaltenAuslesen()

        Call Me.updateDisplay()
        'End If

    End Sub

    'Anzeige aktualisieren
    '*********************
    Private Sub updateDisplay()

        'Einstellungen anzeigen
        Me.RadioButton_Zeichengetrennt.Checked = Me.datei.Zeichengetrennt
        Me.ComboBox_Trennzeichen.SelectedItem = Me.datei.Trennzeichen
        Me.TextBox_Spaltenbreite.Text = Me.datei.Spaltenbreite

        'XSpalte
        Me.TextBox_XSpalte.Text = Me.datei.XSpalte
        'YSpalten
        Me.ListBox_YSpalten.Items.Clear()
        Me.ListBox_YSpalten.Items.AddRange(Me.datei.YSpalten)

    End Sub

#End Region 'Methoden

End Class