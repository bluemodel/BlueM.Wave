Imports System.IO

''' <summary>
''' Klasse für das REG-Dateiformat (Hystem-Extran-Regendaten)
''' </summary>
''' <remarks>Format siehe http://130.83.196.154/BlueM/wiki/index.php/REG-Format</remarks>
Public Class SMUSI_REG
    Inherits Dateiformat

    Const DatumsformatSMUSI_REG As String = "dd MM yyyy   HH"
    Const LenString As Integer = 5   'Länge des Strings eines Wertes in der reg/dat-Datei
    Const iDim As Integer = 3        'Dezimalfaktor wird erstmal global auf 3 gesetzt

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _Zeitintervall As Integer
    Private _WerteProZeile As Integer
    Private _DezFaktor As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Property Zeitintervall() As Integer
        Get
            Return _Zeitintervall
        End Get
        Set(ByVal value As Integer)
            _Zeitintervall = value
        End Set
    End Property

    Public Property DezFaktor() As Integer
        Get
            Return _DezFaktor
        End Get
        Set(ByVal value As Integer)
            _DezFaktor = value
        End Set
    End Property

    Public Property WerteProZeile(ByVal dt As Integer) As Integer
        Get
            Select Case dt
                Case 5
                    Return 12
            End Select
        End Get

        Set(ByVal value As Integer)
            _WerteProZeile = 12
        End Set
    End Property

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileDaten = 4
        Me.UseEinheiten = True

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim Zeile As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Es gibt immer 2 Spalten (Datum + Werte)!
            ReDim Me.Spalten(1)

            '1. Spalte (X)
            Me.Spalten(0).Name = "Datum_Zeit"
            Me.Spalten(0).Index = 0

            '2. Spalte (Y)
            Me.Spalten(1).Index = 1

            'Reihentitel steht in 1. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            Me.Spalten(1).Name = Zeile.Substring(0, 4).Trim()

            'Einheit steht in 2. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            Me.Spalten(1).Einheit = "mm"

            Me.DezFaktor = 3

            'Zeitintervall auslesen
            Me.Zeitintervall = 5

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'SMUSI_REG-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i, j As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum, Zeilendatum As DateTime
        Dim Wert As Double

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren
        ReDim Me.Zeitreihen(0) 'bei REG gibt es nur eine Zeitreihe
        Me.Zeitreihen(0) = New Zeitreihe(Me.SpaltenSel(0).Name)
        Me.Zeitreihen(0).Einheit = Me.SpaltenSel(0).Einheit

        'Einlesen
        '--------
        j = 0

        Do
            j += 1
            Zeile = StrReadSync.ReadLine.ToString()

            'If Zeile.Substring(5) = " 0 09999 0 0 0E" Then Exit Do

            If (j > Me.nZeilenHeader And Zeile.Trim.Length > 0) Then

                'Datum erkennen
                '--------------
                Jahr = Zeile.Substring(11, 4)
                Monat = Zeile.Substring(8, 2 )
                Tag = Zeile.Substring(5, 2)
                Stunde = Zeile.Substring(18, 2)
                Minute = 0
                Zeilendatum = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())
                'Datum und Wert zur Zeitreihe hinzufügen
                '---------------------------------------
                'alle bis auf den letzten Wert einlesen
                'beim letzten Wert besteht die Möglichkeit, dass nicht alle Zeichen belegt sind
                For i = 0 To 12 - 1
                    Datum = Zeilendatum.AddMinutes(i * 5)
                    Wert = StringToDouble(Zeile.Substring(20 + LenString * i, LenString)) /1000
                    Me.Zeitreihen(0).AddNode(Datum, Wert)
                Next

            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als SMUSI-REG-Datei
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    Public Shared Sub Write_File(ByVal Reihe As Zeitreihe, ByVal File As String)

        Dim dt As Integer
        Dim KontiReihe As Zeitreihe
        Dim Spanne As TimeSpan
        Dim Divisor As Integer
        Dim hn_A_Mittel As Integer
        
        'Zeitintervall aus ersten und zweiten Zeitschritt der Reihe ermitteln
        dt = DateDiff(DateInterval.Minute, Reihe.XWerte(0), Reihe.XWerte(1))

        'Äquidistante Zeitreihe erzeugen
        KontiReihe = Reihe.getKontiZRE(dt)

        Dim strwrite As StreamWriter
        Dim iZeile, j, n As Integer
        Const WerteproZeile As Integer = 12
        strwrite = New StreamWriter(File)
        Dim IntWert As Long
        Dim Summe As Integer

        Summe = 0 ' Jahresniederschlagshöhe

        '1. Zeile
        'strwrite.WriteLine("KONVertierte REG-Reihe")

        '2. Zeile: 
        'strwrite.WriteLine("hN =???? mm/a")
        
        '3. Zeile: 
        'strwrite.WriteLine("================================================================================")

        'Wertezeilen...
        n = 0   'n = Anzahl der Zeitreihenwerte
        For iZeile = 0 To (KontiReihe.Length / WerteproZeile) - 1
            strwrite.Write("KONV ")
            strwrite.Write(KontiReihe.XWerte(n).ToString(DatumsformatSMUSI_REG))
            For j = 1 To WerteproZeile
                IntWert = KontiReihe.YWerte(n) * 1000
                Summe = Summe + IntWert
                strwrite.Write(IntWert.ToString.PadLeft(5))
                n = n + 1
            Next
            strwrite.WriteLine()
                    
        Next
        strwrite.Close()

       'Header anpassen (Summe und Betrachungszeitraum)
        Dim FiStr As FileStream = New FileStream(File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim alles As String

        'Mittlere Jahresniederschlagshöhe berechnen
        Spanne = KontiReihe.Enddatum - KontiReihe.Anfangsdatum
        Divisor = Spanne.TotalDays /365
        hn_A_Mittel = Summe/1000 *1/Divisor

        'Komplette Datei einlesen
        alles = StrRead.ReadToEnd()
        StrRead.Close

        strwrite = New StreamWriter(File)
        strwrite.WriteLine("KONVertierte REG-Reihe")
        strwrite.Write("hN =")
        strwrite.Write(hn_A_Mittel)
        strwrite.Write(" mm/a")
        strwrite.WriteLine()
        strwrite.WriteLine("================================================================================")

        strwrite.Write(alles)
        strwrite.Close
    End Sub


#End Region 'Methoden

End Class
