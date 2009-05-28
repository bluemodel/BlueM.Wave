Imports System.IO
''' <summary>
''' Factory zur Erzeugung von Datei-Instanzen
''' </summary>
Public Module Dateifactory

    Public Const FileExtASC As String = ".ASC"
    Public Const FileExtCSV As String = ".CSV"
    Public Const FileExtREG As String = ".REG"
    Public Const FileExtDAT As String = ".DAT"
    Public Const FileExtRVA As String = ".RVA"
    Public Const FileExtSMB As String = ".SMB"
    Public Const FileExtWEL As String = ".WEL"
    Public Const FileExtKWL As String = ".KWL"
    Public Const FileExtZRE As String = ".ZRE"
    Public Const FileExtTEN As String = ".TEN"
    Public Const FileExtDTL As String = ".DTL" 'DWD-Daten: Temperatur und Luftfeuchte
    'Public Const FileExtSREG As String = ".SREG" 'SMUSI-REGEN

    ''' <summary>
    ''' Erzeugt eine zur Dateiendung passende Datei-Instanz
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns>Eine Instanz der Datei</returns>
    ''' <remarks></remarks>
    Public Function getDateiInstanz(ByVal file As String) As Dateiformat

        Dim Datei As Dateiformat
        Dim FileExt As String

        'Prüfen, ob die Datei existiert
        If (Not System.IO.File.Exists(file)) Then
            Throw New Exception("Datei '" & file & "' nicht gefunden!")
        End If

        'Fallunterscheidung je nach Dateiendung
        FileExt = System.IO.Path.GetExtension(file).ToUpper()
        Select Case FileExt

            Case FileExtASC
                Datei = New ASC(file)

            Case FileExtDAT
                Datei = New REG(file) 'Hystem-Extran

            
            Case FileExtREG
                'Abfrage ob SMUSI oder HYSTEM
                Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
                Dim StrReadSync = TextReader.Synchronized(StrRead)
                Dim Zeile As String = ""

                '2 Zeilen einlesen
                Zeile = StrReadSync.ReadLine.ToString()
                Zeile = StrReadSync.ReadLine.ToString()

                StrReadSync.close()
                StrRead.Close()
                FiStr.Close()
                
                If Zeile.Substring(0,4) = "hN =" Then
                    'SMUSI-Regenreihe
                    Datei = New SMUSI_REG(file)
                Else
                    'Hystem-Regenreihen
                    Datei = New REG(file)
                End If
            'Case FileExtSREG
                

            Case FileExtRVA
                Datei = New RVA(file)

            Case FileExtSMB
                Datei = New SMB(file)

            Case FileExtWEL, FileExtKWL
                Datei = New WEL(file)

            Case FileExtZRE
                Datei = New ZRE(file)

            Case FileExtCSV
                Datei = New CSV(file)

            Case FileExtDTL
                Datei = New DWD_T_L(file)

            Case Else
                Throw New Exception("Die Dateiendung '" & FileExt & "' ist nicht bekannt!")

        End Select

        Return Datei

    End Function

End Module

