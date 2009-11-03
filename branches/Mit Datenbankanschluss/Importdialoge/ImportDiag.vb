Imports System.IO

Partial Public Class ImportDiag
    Inherits System.Windows.Forms.Form

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private IsInitializing As Boolean

    Private datei As ZeitreihenDatei


#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByRef _dateiobjekt As ZeitreihenDatei)

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
        Call Me.datei.Zeitreihen_Infos_Lesen()

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
        Dim line, text As string

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

    End Sub

    'OK Button gedr�ckt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Ausgew�hlte Spalten
        Dim i As Integer
        If (Me.ListBox_YSpalten.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Y-Spalte ausw�hlen!", MsgBoxStyle.Exclamation, "Fehler")
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
    Private Sub inputChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Zeile�berschriften.TextChanged, TextBox_ZeileEinheiten.TextChanged, TextBox_ZeileDaten.TextChanged, CheckBox_Einheiten.CheckedChanged, ComboBox_Dezimaltrennzeichen.SelectedIndexChanged, RadioButton_Zeichengetrennt.CheckedChanged, ComboBox_Trennzeichen.SelectedIndexChanged, TextBox_Spaltenbreite.TextChanged, NumericUpDown_DatumsSpalte.ValueChanged

        If (Me.IsInitializing = True) Then
            Exit Sub
        Else
            'Eingaben speichern
            '------------------
            'Zeilennummern
            Me.datei.iZeileUeberschriften = Me.TextBox_Zeile�berschriften.Value
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

            'Datum
            Me.datei.XSpalte = Me.NumericUpDown_DatumsSpalte.Value - 1 'Immer eins weniger wie du ! 

            'Spalten neu auslesen
            Call Me.datei.Zeitreihen_Infos_Lesen()

            'Anzeige aktualisieren
            Call Me.aktualisieren()

        End If

    End Sub

    'Anzeige aktualisieren
    '*********************
    Private Sub aktualisieren()

        Dim i As Integer

        'Dezimaltrennzeichen
        Me.ComboBox_Dezimaltrennzeichen.SelectedItem = Me.datei.Dezimaltrennzeichen

        'Zeilennummern
        Me.TextBox_Zeile�berschriften.Text = Me.datei.iZeileUeberschriften
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

#End Region 'Methoden

    'Reihe in den YSpalten suchen
    '****************************
    Private Sub TextBox_YSpalte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Suche.TextChanged

        Me.ListBox_YSpalten.ClearSelected()
        Dim index As Integer = Me.ListBox_YSpalten.FindString(Me.TextBox_Suche.Text)
        If index <> -1 Then ListBox_YSpalten.SetSelected(index, True)
    End Sub

End Class