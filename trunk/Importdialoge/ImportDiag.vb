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
    Private Sub inputChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_ZeileÜberschriften.TextChanged, TextBox_ZeileEinheiten.TextChanged, TextBox_ZeileDaten.TextChanged, CheckBox_Einheiten.CheckedChanged, ComboBox_Dezimaltrennzeichen.SelectedIndexChanged, RadioButton_Zeichengetrennt.CheckedChanged, ComboBox_Trennzeichen.SelectedIndexChanged, TextBox_Spaltenbreite.TextChanged, NumericUpDown_DatumsSpalte.ValueChanged

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
                Me.StatusImage.Image = Global.IHWB.Wave.My.Resources.Resources.tick
                Me.StatusImage.Text = "OK"

            Catch ex As Exception
                'Bei Exception Status auf Fehler setzen
                Me.StatusImage.Image = Global.IHWB.Wave.My.Resources.Resources.fehler
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

    'Reihe in den YSpalten suchen
    '****************************
    Private Sub TextBox_YSpalte_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox_Suche.TextChanged

        Me.ListBox_YSpalten.ClearSelected()
        Dim index As Integer = Me.ListBox_YSpalten.FindString(Me.TextBox_Suche.Text)
        If index <> -1 Then ListBox_YSpalten.SetSelected(index, True)
    End Sub

#End Region 'Methoden


Private Sub Select_All(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SelectAll.Click

    Dim i As Long

    For i = 0 To Me.ListBox_YSpalten.Items.Count - 1
        Me.ListBox_YSpalten.SetSelected(i, True)
    Next


End Sub
End Class