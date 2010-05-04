Imports System.IO

''' <summary>
''' Klasse für das WEL-Dateiformat von Hystem-Extran
''' Bei WEL-Dateien in Hystem handelt es sich immer um Zuflussdaten
''' Format ist festgeschrieben im HystemExtran-Anwenderhandbuch
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/WEL-Format</remarks>
Public Class HystemExtran_WEL
    Inherits Dateiformat
    
    

#Region "Eigenschaften"

    Const maxSpalten_dT As Integer = 8
    Const HExt_welEinheit As String = "m3/s"
    Private AnzSpalten_dT() As Integer

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
        Me.iZeileUeberschriften = 5
        Me.UseEinheiten = True
        Me.Zeichengetrennt = False
        Me.Spaltenbreite = 10
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
        Dim iZeileAnzSpalten As Integer = 4

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Zeile mit der Anzahl der Zeireihen finden
            For i = 1 To Me.iZeileUeberschriften - 1
                Zeile = StrReadSync.ReadLine.ToString()
                If (i = iZeileAnzSpalten) Then ZeileSpalten = Zeile
            Next

            'Anzahl der Zeitreihen auslesen
            Dim anzSpalten As Integer
            anzSpalten = Convert.ToSingle(Right(ZeileSpalten, 5))
            ReDim Me.Spalten(anzSpalten)

            'Index 0 für Datum belegen
            Me.Spalten(0).Name = "Date"
            Me.Spalten(0).Index = 0
            Me.Spalten(0).Einheit = "-"
            
            'Anzahl der Zeilen und Spalten pro Zeitschritt ermitteln
            Me.nZeilen = Math.Ceiling(anzSpalten / maxSpalten_dT)
            ReDim AnzSpalten_dT(Me.nZeilen - 1)
            If Me.nZeilen = 1 Then
                AnzSpalten_dT(Me.nZeilen - 1) = anzSpalten
            ElseIf Me.nZeilen > 1 Then
                For i = 0 To Me.nZeilen - 2
                    AnzSpalten_dT(i) = maxSpalten_dT
                Next
                AnzSpalten_dT(Me.nZeilen - 1) = anzSpalten - (maxSpalten_dT * (Me.nZeilen - 1))
            End If

            'Spaltenköpfe (Zuflussknoten) und Indizes einlesen
            Dim index As Integer
            index = 1
            For i = 0 To Me.nZeilen - 1
                Zeile = StrReadSync.ReadLine.ToString()
                For j = 0 To AnzSpalten_dT(i) - 1
                    Me.Spalten(index).Name = Zeile.Substring((j * Me.Spaltenbreite) + SpaltenOffset, Me.Spaltenbreite)
                    Me.Spalten(index).Einheit = HExt_welEinheit
                    Me.Spalten(index).Index = index
                    Me.Spalten(index).Objekt = trim(Me.Spalten(index).Name)
                    Me.Spalten(index).Type = "FLOW"
                    index = index + 1
                Next
            Next

            'iZeileDaten kann erst jetzt gesetzt werden, wenn AnzZeilen_dT bekannt ist
            Me.iZeileDaten = iZeileUeberschriften + Me.nZeilen
            
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

        Dim iZeile, i As Integer
        Dim Zeile As String
        Dim WerteString As String
        Dim datum As DateTime

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
                Me.Zeitreihen(i).Objekt = Me.SpaltenSel(i).Objekt
                Me.Zeitreihen(i).Type = Me.SpaltenSel(i).Type
            Next
        End If

        'Einlesen
        '--------

        'Header
        For iZeile = 1 To Me.iZeileDaten - 1
            Zeile = StrReadSync.ReadLine.ToString()
        Next

        'Daten
        Do
            Zeile = StrReadSync.ReadLine.ToString()
            if zeile.Substring(0,5) = "*****" then
                Exit Do
            End If
            'Erste Zeile: Datum_Zeit
            datum = New System.DateTime(Zeile.Substring(6 + SpaltenOffset, 4), Zeile.Substring(4 + SpaltenOffset, 2), Zeile.Substring(2 + SpaltenOffset, 2), Zeile.Substring(14 + SpaltenOffset, 2), Zeile.Substring(16 + SpaltenOffset, 2), 0, New System.Globalization.GregorianCalendar())
            'Restliche Zeilen pro Zeitschritt: Werte
            'Alle ausgewählten Spalten durchlaufen
            'Alle Abflusswerte einlesen
            WerteString = ""
            For i = 0 To Me.nZeilen - 1
                WerteString = WerteString + StrReadSync.ReadLine.ToString()
            Next
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i).AddNode(datum, StringToDouble(WerteString.Substring(((Me.SpaltenSel(i).Index - 1) * me.Spaltenbreite) + SpaltenOffset, Me.Spaltenbreite)))
            Next


        Loop Until StrReadSync.Peek() = -1

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um eine WEL-Datei für Hystem-Extran handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        '3 Zeilen einlesen
        Zeile = StrRead.ReadLine.ToString()
        Zeile = StrRead.ReadLine.ToString()
        Zeile = StrRead.ReadLine.ToString()
        
        StrRead.Close()
        FiStr.Close()

        If (Zeile.Length = 40) and (left(Zeile,2) = "  ") and (Zeile.Substring(10,4)  = "    ") Then
            'Es ist eine Extran-Regenreihe!
            Return True
        Else
            Return False
        End If

    End Function
#End Region 'Methoden

End Class