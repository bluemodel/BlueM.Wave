Public Class AnalysisDialog

    '*************************************************************
    'Dialog zur Auswahl von Zeitreihe(n) und einer Analysefunktion
    '*************************************************************

    'Konstruktor
    '***********
    Public Sub New(ByVal zeitreihen As Collection)

        Call InitializeComponent()

        'Liste der Formate
        Me.ComboBox_Funktion.DataSource = System.Enum.GetValues(GetType(AnalysisFactory.AnalysisFunctions))

        'Zeitreihen in Listbox eintragen
        For Each item As Object In zeitreihen
            Me.ListBox_Zeitreihen.Items.Add(item)
        Next

    End Sub

    'Ausgew�hlte Analysefunktion
    '***************************
    Friend ReadOnly Property selectedAnalysisFunction() As AnalysisFactory.AnalysisFunctions
        Get
            Return Me.ComboBox_Funktion.SelectedItem
        End Get
    End Property

    'Ausgew�hlte Zeitreihen
    '**********************
    Friend ReadOnly Property selectedZeitreihen() As Collection
        Get
            Dim zeitreihen As New Collection()
            For Each item As Object In Me.ListBox_Zeitreihen.SelectedItems
                zeitreihen.Add(CType(item, Zeitreihe), CType(item, Zeitreihe).Title)
            Next
            Return zeitreihen
        End Get
    End Property

    'Analysefunktion ver�ndert
    '*************************
    Private Sub ComboBox_Funktion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Funktion.SelectedIndexChanged

        'Umschalten zwischen Einfach- und Mehrfachauswahl von Zeitreihen:
        '----------------------------------------------------------------
        Select Case ComboBox_Funktion.SelectedItem

            '   Analysefunktionen f�r einzelne Zeitreihen
            Case AnalysisFactory.AnalysisFunctions.Monatsauswertung
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.One

                'Analysefunktionen f�r mehrere Zeitreihen
            Case AnalysisFactory.AnalysisFunctions.Nash_Sutcliffe
                Me.ListBox_Zeitreihen.SelectionMode = SelectionMode.MultiExtended

            Case Else
                'Noch nicht implementiert
                MsgBox("Noch nicht implementiert!", MsgBoxStyle.Exclamation, "Wave")
                'Standardauswahl setzen:
                ComboBox_Funktion.SelectedItem = AnalysisFactory.AnalysisFunctions.Monatsauswertung
        End Select

    End Sub

    'OK-Button gedr�ckt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click
        'Eingabekontrolle
        If (Me.ListBox_Zeitreihen.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Zeitreihe ausw�hlen!", MsgBoxStyle.Exclamation, "Wave")
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub


End Class