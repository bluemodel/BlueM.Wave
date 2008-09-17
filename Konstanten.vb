Imports System.Globalization

Module Konstanten

    'Zeilenumbruch
    Public Const eol As String = Chr(13) & Chr(10)

    'Not a Number
    Public Const NaN As Double = -999

    'Datumsformat
    Public Const Datumsformat As String = "dd.MM.yyyy HH:mm"

    'Zahlenformatanweisung
    Public ReadOnly Property Zahlenformat() As NumberFormatInfo
        Get
            'NumberFormatInfo einrichten
            Zahlenformat = New NumberFormatInfo()
            Zahlenformat.NumberDecimalSeparator = "."
            Zahlenformat.NumberGroupSeparator = ""
            Zahlenformat.NumberGroupSizes = New Integer() {3}
        End Get
    End Property

    'Zeitreihenformate
    Public Enum Dateiformate As Integer
        ZRE = 1
        WEL = 2
        CSV = 3
        ASC = 4
    End Enum

    ''' <summary>
    ''' Wandelt einen String zu Double um
    ''' </summary>
    ''' <param name="str">umzuwandelnder String</param>
    ''' <returns>Double-Wert, NaN falls String nicht umwandelbar</returns>
    ''' <remarks></remarks>
    Public Function StringToDouble(ByVal str As String) As Double

        Dim wert As Double

        Try
            wert = Convert.ToDouble(str, Konstanten.Zahlenformat)
        Catch ex As Exception
            wert = Konstanten.NaN
            Call Wave.Log.AddLogEntry("Der Wert '" & str & "' konnte nicht gelesen werden und wurde durch NaN (" & Konstanten.NaN.ToString() & ") ersetzt!")
        End Try

        Return wert

    End Function

End Module
