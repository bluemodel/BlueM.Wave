Public Class TXT

    'oft verwendete Zeichen (quasi Konstanten)
    '-----------------------------------------
    Private semikolon As Zeichen = New Zeichen(";")
    Private komma As Zeichen = New Zeichen(",")
    Private punkt As Zeichen = New Zeichen(".")
    Private leerzeichen As Zeichen = New Zeichen(" ")
    Private tab As Zeichen = New Zeichen(Chr(9))

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _file As String                     'Pfad zur Datei
    Private _trennzeichen As zeichen            'Spaltentrennzeichen
    Private _dezimaltrennzeichen As zeichen     'Dezimaltrennzeichen
    Private _anzKopfzeilen As Integer           'Anzahl Kopfzeilen

#End Region 'Eigenschaften

#Region "Properties"

    'Properties
    '##########
    Public Property File() As String
        Get
            Return _file
        End Get
        Set(ByVal value As String)
            _file = value
        End Set
    End Property

    Public Property Trennzeichen() As zeichen
        Get
            Return _trennzeichen
        End Get
        Set(ByVal value As zeichen)
            _trennzeichen = value
            Me.ComboBox_Trennzeichen.SelectedItem = _trennzeichen
        End Set
    End Property

    Public Property Dezimaltrennzeichen() As zeichen
        Get
            Return _dezimaltrennzeichen
        End Get
        Set(ByVal value As zeichen)
            _dezimaltrennzeichen = value
            Me.ComboBox_Dezimaltrennzeichen.SelectedItem = _dezimaltrennzeichen
        End Set
    End Property

    Public Property AnzKopfzeilen() As Integer
        Get
            Return _anzKopfzeilen
        End Get
        Set(ByVal value As Integer)
            _anzKopfzeilen = value
            Me.TextBox_AnzKopfzeilen.Text = _anzKopfzeilen.ToString()
        End Set
    End Property

#End Region 'Properties

#Region "Methoden"

    'Methoden
    '########

    'Form laden
    '**********
    Private Sub TXT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Combobox Trennzeichen initialisieren
        Me.ComboBox_Trennzeichen.BeginUpdate()
        Me.ComboBox_Trennzeichen.Items.Add(Me.semikolon)
        Me.ComboBox_Trennzeichen.Items.Add(Me.komma)
        Me.ComboBox_Trennzeichen.Items.Add(Me.punkt)
        Me.ComboBox_Trennzeichen.Items.Add(Me.leerzeichen)
        Me.ComboBox_Trennzeichen.Items.Add(Me.tab)
        Me.ComboBox_Trennzeichen.EndUpdate()

        'Combobox Dezimaltrennzeichen initialisieren
        Me.ComboBox_Dezimaltrennzeichen.BeginUpdate()
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.komma)
        Me.ComboBox_Dezimaltrennzeichen.Items.Add(Me.punkt)
        Me.ComboBox_Dezimaltrennzeichen.EndUpdate()

        'Standard-Trennzeichen setzen
        Me.Trennzeichen = Me.semikolon

        'Standard-Dezimaltrennzeichen setzen
        Me.Dezimaltrennzeichen = Me.punkt

        'Standard-Anzahl Kopfzeilen setzen
        Me.AnzKopfzeilen = 1

        'Datei als Vorschau anzeigen
        Me.RichTextBox_Vorschau.LoadFile(Me.File, RichTextBoxStreamType.PlainText)

    End Sub

    'OK Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        'Einstellungen übernehmen
        '------------------------

        'Trenn- und Dezimaltrennzeichen
        Me.Trennzeichen = Me.ComboBox_Trennzeichen.SelectedItem
        Me.Dezimaltrennzeichen = Me.ComboBox_Dezimaltrennzeichen.SelectedItem

        'Anzahl Kopfzeilen

        If (Me.TextBox_AnzKopfzeilen.Text < 0) Then
            MsgBox("Bitte eine Zahl für die Anzahl der Kopfzeilen eingeben!", MsgBoxStyle.Exclamation, "Fehler")
            Me.TextBox_AnzKopfzeilen.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            Me.AnzKopfzeilen = Convert.ToInt32(TextBox_AnzKopfzeilen.Text)
        End If

    End Sub

#End Region 'Methoden

End Class