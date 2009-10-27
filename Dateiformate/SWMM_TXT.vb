Imports System.IO

''' <summary>
''' Klasse für das TXT-Dateiformat von SWMM5
''' Format ist festgeschrieben im SWMM-Anwenderhandbuch
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/TXT-Format</remarks>
Public Class SWMM_TXT
    Inherits Dateiformat
    
#Region "Eigenschaften"

    Const iZeileAnzConstituents As Integer = 4
    Private AnzConstituents As Integer
    Private AnzNodes As Integer

    Structure Constituents
        Dim Type As String
        Dim Unit As String
        Dim Index As Integer
    End Structure

    Structure Nodes
        Dim Bez As String
        Dim Index As Integer
    End Structure

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        SpaltenOffset = 0

        'Voreinstellungen
        Me.iZeileUeberschriften = 2
        Me.UseEinheiten = True
        Me.Zeichengetrennt = False
        Me.Dezimaltrennzeichen = Me.punkt

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If


    End Sub

    ''' <summary>
    ''' Spaltenköpfe auslesen
    ''' </summary>
    Public Overrides Sub SpaltenAuslesen()

        Dim i, j As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""
        Dim strArray() As String
        Dim Constituents() As Constituents
        Dim Nodes() As Nodes
        Dim IDSpalte As Long

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Zeile mit der Anzahl der Constituents finden
            For i = 1 To iZeileAnzConstituents
                Zeile = StrReadSync.ReadLine.ToString()
            Next
            'Anzahl der Constituents zu einem Knoten
            strArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            AnzConstituents = Convert.ToSingle(strArray(0))

            ReDim Constituents(AnzConstituents - 1)
            'Inflows und Einheit einlesen
            For i = 0 To AnzConstituents - 1
                Zeile = StrReadSync.ReadLine.ToString()
                strArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                Constituents(i).Type = strArray(0)
                Constituents(i).Unit = strArray(1)
                Constituents(i).Index = i
            Next

            'Anzahl der Zuflussknoten ermitteln
            'entspricht der Anzahl der Zeilen pro Zeitschritt
            Zeile = StrReadSync.ReadLine.ToString()
            strArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            Me.nZeilen = Convert.ToSingle(strArray(0))
            AnzNodes = Me.nZeilen
            ReDim Nodes(AnzNodes - 1)
            For i = 0 To AnzNodes - 1
                Zeile = StrReadSync.ReadLine.ToString()
                Nodes(i).Bez = Trim(Zeile)
                Nodes(i).Index = i
            Next

            'Anzahl der Zeitreihen (Spalten) ermitteln
            Dim anzSpalten As Integer
            anzSpalten = AnzConstituents * AnzNodes
            ReDim Me.Spalten(anzSpalten)

            'iZeileDaten kann erst jetzt gesetzt werden, wenn AnzZeilen_dT bekannt ist
            Me.iZeileDaten = iZeileAnzConstituents + AnzConstituents + AnzNodes + 3

            'Index 0 für Datum belegen
            Me.Spalten(0).Name = "Date"
            Me.Spalten(0).Index = 0
            Me.Spalten(0).Einheit = "-"


            'Spaltenköpfe (Zuflussknoten) und Indizes einlesen
            IDSpalte = 1
            For i = 0 To AnzNodes - 1
                For j = 0 To AnzConstituents - 1
                    Me.Spalten(IDSpalte).Name = Nodes(i).Bez & " " & Constituents(j).Type
                    Me.Spalten(IDSpalte).Einheit = Constituents(j).Unit
                    Me.Spalten(IDSpalte).Index = IDSpalte
                    IDSpalte = IDSpalte + 1
                Next
            Next
            

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte die Datei '" & Path.GetFileName(Me.File) & "' nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    ''' <summary>
    ''' Zeitreihen einlesen
    ''' </summary>
    Public Overrides Sub Read_File()

        Dim iZeile, i As Integer, j As Integer
        Dim Zeile As String
        Dim datum As DateTime
        Dim Werte() As String
        Dim tmpArray() As String
        Dim IDWerte As Long

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Anzahl Zeitreihen bestimmen
        ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)

        'Zeitreihen instanzieren
        For i = 0 To Me.SpaltenSel.Length - 1
            Me.Zeitreihen(i) = New Zeitreihe(Me.SpaltenSel(i).Name)
        Next

        'Einheiten?
        If (Me.UseEinheiten) Then
            'Alle ausgewählten Spalten durchlaufen
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i).Einheit = Me.SpaltenSel(i).Einheit
            Next
        End If

        'Einlesen
        '--------
        ReDim Werte(AnzConstituents * AnzNodes)
        'Header
        For iZeile = 1 To Me.iZeileDaten - 1
            Zeile = StrReadSync.ReadLine.ToString()
        Next

        'Daten
        Do
            IDWerte = 1
            For i = 0 To AnzNodes - 1
                Zeile = StrReadSync.ReadLine.ToString()
                tmpArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                If i = 0 Then
                    datum = New System.DateTime(tmpArray(1), tmpArray(2), tmpArray(3), tmpArray(4), tmpArray(5), tmpArray(6), 0, New System.Globalization.GregorianCalendar())
                End If
                For j = 0 To AnzConstituents - 1
                    Werte(IDWerte) = tmpArray(tmpArray.Length - AnzConstituents + j)
                    IDWerte = IDWerte + 1
                Next
            Next
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index)))
            Next


        Loop Until StrReadSync.Peek() = -1

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um ein Routing Interface File für SWMM handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        '3 Zeilen einlesen
        Zeile = StrRead.ReadLine.ToString()
        Zeile = Trim(Zeile)

        StrRead.Close()
        FiStr.Close()

        If Zeile.StartsWith("SWMM5") Then
            'Es ist ein SWMM-Interface-File
            Return True
        Else
            Return False
        End If
        
    End Function
#End Region 'Methoden

End Class