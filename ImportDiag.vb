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

        'Versuchen, die Spalten auszulesen (mit Standardeinstellungen)
        Call Me.datei.SpaltenAuslesen()

        'Anzeige aktualisieren
        Call Me.aktualisieren()

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

    'Benutzereingabe verarbeiten
    '***************************
    Private Sub inputChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_ZeileÜberschriften.TextChanged, TextBox_ZeileEinheiten.TextChanged, TextBox_ZeileDaten.TextChanged, CheckBox_Einheiten.CheckedChanged, ComboBox_Dezimaltrennzeichen.SelectedIndexChanged, RadioButton_Zeichengetrennt.CheckedChanged, ComboBox_Trennzeichen.SelectedIndexChanged, TextBox_Spaltenbreite.TextChanged

        If (Me.IsInitializing = True) Then
            Exit Sub
        Else
            'Eingaben speichern
            '------------------
            'Zeilennummern
            Me.datei.iZeileUeberschriften = Me.TextBox_ZeileÜberschriften.Value
            Me.datei.iZeileDaten = Me.TextBox_ZeileDaten.Value

            'Einheiten
            Me.datei.UseEinheiten = Me.CheckBox_Einheiten.Checked
            If (Me.CheckBox_Einheiten.Checked) Then
                Me.datei.iZeileEinheiten = Me.TextBox_ZeileEinheiten.Value
            End If

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

            'Spalten neu auslesen
            Call Me.datei.SpaltenAuslesen()

            'Anzeige aktualisieren
            Call Me.aktualisieren()

        End If

    End Sub

    'Anzeige aktualisieren
    '*********************
    Private Sub aktualisieren()

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
        Me.RadioButton_Zeichengetrennt.Checked = Me.datei.Zeichengetrennt
        If (Me.datei.Zeichengetrennt) Then
            Me.RadioButton_Zeichengetrennt.Checked = True
            Me.ComboBox_Trennzeichen.Enabled = True
            Me.TextBox_Spaltenbreite.Enabled = False
        Else
            Me.RadioButton_Zeichengetrennt.Checked = False
            Me.ComboBox_Trennzeichen.Enabled = False
            Me.TextBox_Spaltenbreite.Enabled = True
        End If

        'Trennzeichen
        Me.ComboBox_Trennzeichen.SelectedItem = Me.datei.Trennzeichen

        'Spaltenbreite
        Me.TextBox_Spaltenbreite.Text = Me.datei.Spaltenbreite

        'XSpalte
        Me.TextBox_XSpalte.Text = Me.datei.XSpalte

        'YSpalten
        Me.ListBox_YSpalten.Items.Clear()
        Me.ListBox_YSpalten.Items.AddRange(Me.datei.YSpalten)

    End Sub

#End Region 'Methoden

    'Reihe in den YSpalten suchen
    '****************************
    Private Sub TextBox_YSpalte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_YSpalte.TextChanged

        Me.ListBox_YSpalten.ClearSelected()
        Dim index As Integer = Me.ListBox_YSpalten.FindString(Me.TextBox_YSpalte.Text)
        If index <> -1 Then ListBox_YSpalten.SetSelected(index, True)
    End Sub

End Class