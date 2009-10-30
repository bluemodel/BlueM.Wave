''' <summary>
''' Factory zur Erzeugung von Datei-Instanzen
''' </summary>
Public Module ZeitreihenExtern_factory

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


    Public Function CreateZRExtern(ByVal Typ As ZeitreihenExtern.ZREQuellenTyp, ByVal str As String) As ZeitreihenExtern
        Select Case Typ
            Case ZeitreihenExtern.ZREQuellenTyp.ZRDatei
                Return CreateZRExtern(str)
            Case ZeitreihenExtern.ZREQuellenTyp.ZRDatabase
                Return New ZeitreihenDatabase()
            Case Else
                Return Nothing
        End Select
    End Function



    ''' <summary>
    ''' Erzeugt eine zur Dateiendung passende Datei-Instanz
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns>Eine Instanz der Datei</returns>
    ''' <remarks></remarks>
    Public Function CreateZRExtern(ByVal file As String) As ZeitreihenDatei

        Dim Datei As ZeitreihenDatei
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

            Case FileExtREG, FileExtDAT
                Datei = New REG(file)

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
