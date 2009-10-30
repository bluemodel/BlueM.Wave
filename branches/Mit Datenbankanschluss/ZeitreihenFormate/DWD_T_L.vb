Imports System.IO

''' <summary>
''' Klasse für das DWD-Temperatur- und Luftfeuchte-Dateiformat
''' </summary>
''' <remarks>Format siehe http://130.83.196.220/bluem/wiki/index.php/DWD-T-L-Format</remarks>
Public Class DWD_T_L
    Inherits ZeitreihenDatei

    Const DatumsformatDWDTL As String = "yyyyMMdd"
    Const LenString_T As Integer = 4   'Länge des Strings eines Temperatur-Wertes 
    Const LenString_L As Integer = 3   'Länge des Sringes eines Luftfeuchte-Wertes
    Const iDim As Integer = 3          'Dezimalfaktor wird erstmal global auf 3 gesetzt
    Dim Stationsnummer As String


#Region "Eigenschaften"

    'Eigenschaften
    '#############

    'Private _Zeitintervall As Integer
    'Private _WerteProZeile As Integer
    'Private _DezFaktor As Integer

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

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileDaten = 1
        Me.UseEinheiten = True

        Call Me.Zeitreihen_Infos_Lesen()

        ' Dialogaufruf zur Eingabe der Stationsnummer
        ' TODO: Hier kann man eventuell zukünftig auch ein Array von Stationsnummern
        '       eingeben und dann alle auf einmal einlesen.
        Dim dialog As New DWD_Stationswahl

        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        Stationsnummer = dialog.Stationsnummer

        'If (ReadAllNow) Then
        '    'Direkt einlesen
        '    Call Me.selectAllSpalten()
        '    Call Me.Read_File()
        'End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub Zeitreihen_Infos_Lesen()

        Dim Zeile As String = ""

        'Try
            'Datei öffnen
            'Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            'Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            'Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Es gibt immer Temperatur und Luftfeuchte!
            ReDim Me.Spalten(1)

            '1. Spalte (X)
            Me.Spalten(0).Name = "Temperatur"
            Me.Spalten(0).Einheit = "°C"
            Me.Spalten(0).Index = 0

            '2. Spalte (Y)
            Me.Spalten(1).Name = "Luftfeuchte"
            Me.Spalten(1).Einheit = "%"
            Me.Spalten(1).Index = 1

            'Reihentitel steht in 1. Zeile:
            'Zeile = StrReadSync.ReadLine.ToString()
            'Me.Spalten(1).Name = Zeile.Substring(20, 30).Trim()

            'Einheit steht in 2. Zeile:
            'Zeile = StrReadSync.ReadLine.ToString()
            'Me.Spalten(1).Einheit = Zeile.Substring(68, 2)

            'Me.DezFaktor = Zeile.Substring(29, 1)

            'Zeitintervall setzen
            'Me.Zeitintervall = Convert.ToSingle(Zeile.Substring(23, 2).Trim)

            'StrReadSync.close()
            'StrRead.Close()
            'FiStr.Close()

        'Catch ex As Exception
        '    MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        'End Try

    End Sub

    'REG-Datei einlesen
    '******************
    Public Overrides Sub Zeitreihe_Einlesen()

        Dim i, j As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum, Zeilendatum As DateTime
        Dim Datenkollektivkennung As String
        
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren
        ReDim Me.Zeitreihen(1) 
        Me.Zeitreihen(0) = New Zeitreihe(Me.Spalten(0).Name)
        Me.Zeitreihen(0).Einheit = Me.Spalten(0).Einheit

        Me.Zeitreihen(1) = New Zeitreihe(Me.Spalten(1).Name)
        Me.Zeitreihen(1).Einheit = Me.Spalten(1).Einheit

        'Einlesen
        '--------
        j = 0

        Do
            j += 1
            Zeile = StrReadSync.ReadLine.ToString()

            If (Zeile.Length > 0 and Zeile.Substring(2,5)= Stationsnummer) Then

                'Datum erkennen
                '--------------
                Jahr = Zeile.Substring(7, 4)
                Monat = Zeile.Substring(11, 2)
                Tag = Zeile.Substring(13, 2)
                
                Datenkollektivkennung = Zeile.Substring(0,2)
                If Datenkollektivkennung = "TX" Then
                    'Erster Wert ist 01:50 MEZ, die fehlende Stunde wird in nachfolgender For-Schleife noch addiert
                    Stunde = "00"
                    Minute = "50"
                Else
                    'Erster Wert ist 01:30 MEZ, die fehlende Stunde wird in nachfolgender For-Schleife noch addiert
                    Stunde = "00"
                    Minute = "30"
                End If
                
                Zeilendatum = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())
                Datum = Zeilendatum
                
                'Datum und Wert zur Zeitreihe hinzufügen
                '---------------------------------------
                ' Es gibt 24 Temperaturwerte und 24 Luftfeuchtewerte, diese werden aber in unterschiedliche Zeitreihen abgelegt
                For i = 0 To 23 
                        Datum = Datum.AddMinutes(60)
                        
                        'Fehlerabfrage Temperatur
                        If StringToDouble(Zeile.Substring(19 + (LenString_T+1) * i, LenString_T)) = -999
                            Me.Zeitreihen(0).AddNode(Datum,Double.NaN)
                        Else
                            Me.Zeitreihen(0).AddNode(Datum, StringToDouble(Zeile.Substring(19 + (LenString_T+1) * i, LenString_T)) /10)
                        End If
                        
                        'Fehlerabfrage Luftfeuche
                        If StringToDouble(Zeile.Substring(139 + (LenString_L+1) * i, LenString_L)) = -99
                            Me.Zeitreihen(1).AddNode(Datum,Double.NaN)
                        Else
                            Me.Zeitreihen(1).AddNode(Datum, StringToDouble(Zeile.Substring(139 + (LenString_L+1) * i, LenString_L)) )
                        End If
                        
                        

                Next

            End If

        Loop Until StrReadSync.Peek() = -1
        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' TODO
    ''' Exportiert eine Zeitreihe als dtl-Datei
    ''' Nur zu Überprüfungszwecken
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    'Public Shared Sub Write_File(ByVal Reihe As Zeitreihe, ByVal File As String)

    '    Dim dt As Integer
    '    Dim KontiReihe As Zeitreihe

    '    'Zeitintervall aus ersten und zweiten Zeitschritt der Reihe ermitteln
    '    dt = DateDiff(DateInterval.Minute, Reihe.XWerte(0), Reihe.XWerte(1))

    '    'Äquidistante Zeitreihe erzeugen
    '    KontiReihe = Reihe.getKontiZRE(dt)

    '    Dim strwrite As StreamWriter
    '    Dim iZeile, j, n As Integer
    '    Const WerteproZeile As Integer = 12
    '    strwrite = New StreamWriter(File)
    '    Dim IntWert As Long

    '    '1. Zeile
    '    strwrite.WriteLine("TUD   0 0   0 1 0 0 Messstelle / Station                 0        0           0")

    '    '2. Zeile: 
    '    'Standard
    '    strwrite.Write("TUD   0 0   0 2 0 0 ")
    '    'Zeitintervall
    '    strwrite.Write(dt.ToString.PadLeft(5))
    '    'Dimension der Zehnerprotenz
    '    strwrite.Write((iDim * (-1)).ToString.PadLeft(5))
    '    'Anfangsdatum
    '    strwrite.Write(KontiReihe.Anfangsdatum.ToString(DatumsformatDWDTL))
    '    'Enddatum
    '    strwrite.Write(KontiReihe.Enddatum.ToString(DatumsformatDWDTL))
    '    'Anzahl der Kommentarzeilen nach Zeile 2, wird = 3 gesetzt
    '    strwrite.Write("    3")
    '    'Art der Daten, N = Niederschlag, Q = Abfluss
    '    strwrite.Write("N    ")
    '    'Einheit
    '    strwrite.WriteLine("MM / IB   ")

    '    '3. Zeile: 
    '    strwrite.WriteLine("TUD   0 0   0 3 0 0 Beginn         Kommentarzeile 1                        Ende")

    '    '4. Zeile: Anfangs- und Enddatum
    '    strwrite.WriteLine("TUD   0 0   0 4 0 0 Beginn         Kommentarzeile 2                        Ende")

    '    '5. Zeile: Werte
    '    strwrite.WriteLine("TUD   0 0   0 5 0 0 Beginn         Kommentarzeile 3                        Ende")

    '    n = 0   'n = Anzahl der Zeitreihenwerte
    '    For iZeile = 0 To (KontiReihe.Length / WerteproZeile) - 1
    '        strwrite.Write("TUD  ")
    '        strwrite.Write(KontiReihe.XWerte(n).ToString(DatumsformatDWDTL) & " ")
    '        For j = 1 To WerteproZeile
    '            IntWert = KontiReihe.YWerte(n) * 10 ^ (iDim)
    '            strwrite.Write(IntWert.ToString.PadLeft(5))
    '            n = n + 1
    '        Next
    '        strwrite.WriteLine()
    '    Next
    '    strwrite.WriteLine("TUD   0 09999 0 0 0E")
    '    strwrite.Close()

    'End Sub


#End Region 'Methoden

End Class


