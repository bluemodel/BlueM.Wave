Public MustInherit Class ZeitreihenExtern
    

    public enum ZREQuellenTyp as integer
        ZRDatei = 0
        ZRDatabase = 1
        ZRnetCDF = 2
    End Enum


    Public mustoverride ReadOnly Property Typ() As ZREQuellenTyp


    ''' <summary>
    ''' Array der in der Datei enhaltenen Zeitreihen
    ''' </summary>
    Public Zeitreihen() As Zeitreihe

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public MustOverride ReadOnly Property UseImportDialog() As Boolean



    ''' <summary>
    ''' Liest die ausgewählten Spalten (siehe SpaltenSel) ein und legt sie im Array Zeitreihen ab.
    ''' </summary>
    Public MustOverride Sub Zeitreihe_Einlesen()

    ''' <summary>
    ''' Alle vorhandenen Spalten für den Import auswählen
    ''' </summary>
    ''' <remarks>Die X-Spalte wird nicht mit ausgewählt</remarks>
    Public MustOverride Sub selectAll_Zeitreihen()



End Class
