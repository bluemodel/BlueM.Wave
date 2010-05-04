Imports System.IO

''' <summary>
''' Klasse für das SMUSI REG-Dateiformat (SMUSI-Regendateien)
''' </summary>
''' <remarks>Format siehe http://130.83.196.220/bluem/wiki/index.php/SMUSI_REG-Format</remarks>
Public Class SMUSI_REG
    Inherits Dateiformat

    Const DatumsformatSMUSI_REG As String = "dd MM yyyy   HH"
    Const WerteproZeile As Integer = 12
    Const LenWert As Integer = 5
    Const LenZeilenanfang As Integer = 20
    Const dt_min As Integer = 5

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Zeitintervall von SMUSI-Regenreihen
    ''' </summary>
    ''' <remarks>5 Minuten</remarks>
    Private ReadOnly Property Zeitintervall() As TimeSpan
        Get
            Return New TimeSpan(0, SMUSI_REG.dt_min, 0)
        End Get
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

        Dim Zeile As String
        Dim i As Integer

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

            'Reihentitel steht in 1. Spalte bei den Werten:
            Zeile = ""
            For i = 0 To Me.nZeilenHeader
                Zeile = StrReadSync.ReadLine.ToString()
            Next
            Me.Spalten(1).Name = Zeile.Substring(0, 4).Trim()

            'Einheit ist immer mm
            Me.Spalten(1).Einheit = "mm"

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
        Dim leerzeile As Boolean
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim DatumCurrent, DatumZeile, DatumTmp As DateTime
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
        leerzeile = False

        Do
            j += 1
            Zeile = StrReadSync.ReadLine.ToString()

            'If Zeile.Substring(5) = " 0 09999 0 0 0E" Then Exit Do

            If (j > Me.nZeilenHeader) Then

                If (Zeile.Trim.Length < 1) Then
                    'Leere Zeile
                    '-----------
                    leerzeile = True
                Else
                    'Zeile mit Werten
                    '----------------
                    'Zeilendatum erkennen
                    Jahr = Zeile.Substring(11, 4)
                    Monat = Zeile.Substring(8, 2)
                    Tag = Zeile.Substring(5, 2)
                    Stunde = Zeile.Substring(18, 2)
                    Minute = 0
                    DatumZeile = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())

                    If (leerzeile) Then
                        'Bei vorheriger leeren Zeile: 0-Stelle 5 min nach letztem Datum einfügen
                        DatumTmp = DatumCurrent.Add(Me.Zeitintervall)
                        If (Not DatumTmp = DatumZeile) Then
                            Me.Zeitreihen(0).AddNode(DatumTmp, 0)
                        End If
                    End If

                    '12 x Datum und Wert zur Zeitreihe hinzufügen
                    '---------------------------------------
                    For i = 0 To SMUSI_REG.WerteproZeile - 1
                        DatumCurrent = DatumZeile.AddMinutes(i * SMUSI_REG.dt_min)
                        Wert = StringToDouble(Zeile.Substring(LenZeilenanfang + LenWert * i, LenWert)) / 1000
                        Me.Zeitreihen(0).AddNode(DatumCurrent, Wert)
                    Next

                    If (leerzeile) Then
                        'Bei vorheriger leeren Zeile: 0-Stelle 5 min vor Zeilendatum einfügen
                        DatumTmp = DatumZeile.Subtract(Me.Zeitintervall)
                        If (Not Zeitreihen(0).Nodes.ContainsKey(DatumTmp)) Then
                            Me.Zeitreihen(0).AddNode(DatumTmp, 0)
                        End If
                    End If

                    leerzeile = False
                End If
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
        strwrite = New StreamWriter(File)
        Dim IntWert As Long
        Dim Summe As Integer

        Summe = 0 ' Jahresniederschlagshöhe

        'Anfangsdatum und Enddatum der zu exporierenden Zeitreihe bestimmen
        'Es müssen 12 Stundenwerte in der Zeitreihe vorliegen, sonst wird
        'abgeschniten

        'ExportStartDatum
        Dim Datum, ExportStartDatum, ExportEndDatum As DateTime
        Dim iDatum As Integer
        iDatum = 0
        Datum = KontiReihe.Anfangsdatum
        Do While Datum.Minute > 0
            iDatum = iDatum + 1
            Datum = KontiReihe.XWerte(iDatum)
        Loop
        ExportStartDatum = Datum

        'ExportEndDatum
        Datum = KontiReihe.Enddatum
        Dim Endstunde As Integer
        If Datum.Minute <> 55 Then
            Endstunde = Datum.Hour - 1
        Else
            Endstunde = Datum.Hour
        End If
        ExportEndDatum = New System.DateTime(Datum.Year, Datum.Month, Datum.Day, Endstunde, 55, 0, New System.Globalization.GregorianCalendar())
        Dim iDatumEnd As Integer
        iDatumEnd = KontiReihe.Length - 1
        Datum = KontiReihe.Enddatum
        Do While Datum <> ExportEndDatum
            iDatumEnd = iDatumEnd - 1
            Datum = KontiReihe.XWerte(iDatumEnd)
        Loop

        Dim AnzahlZeilen As Integer
        AnzahlZeilen = (iDatumEnd - iDatum) / WerteproZeile

        'Wertezeilen...
        n = iDatum   'Ausgabe beginnt bei ersten vollen Stunde in der Zeitreihe
        For iZeile = 0 To AnzahlZeilen - 1
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
        Divisor = Math.Max(Spanne.TotalDays / 365, 1)
        hn_A_Mittel = Summe / 1000 * 1 / Divisor

        'Komplette Datei einlesen
        alles = StrRead.ReadToEnd()
        StrRead.Close()
        FiStr.Close()

        strwrite = New StreamWriter(File)
        strwrite.WriteLine("KONVertierte REG-Reihe")
        strwrite.Write("hN =")
        strwrite.Write(hn_A_Mittel)
        strwrite.Write(" mm/a")
        strwrite.WriteLine()
        strwrite.WriteLine("================================================================================")

        strwrite.Write(alles)
        strwrite.Close()

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um eine SMUSI-REG-Datei handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns></returns>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        '2 Zeilen einlesen
        Zeile = StrRead.ReadLine.ToString()
        Zeile = StrRead.ReadLine.ToString()

        StrRead.Close()
        FiStr.Close()

        If (Zeile.Substring(0, 4) = "hN =") Then
            'Es ist eine SMUSI-Regenreihe!
            Return True
        Else
            Return False
        End If

    End Function

#End Region 'Methoden

End Class
