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
        REG = 5
    End Enum

    ''' <summary>
    ''' Wandelt einen String zu Double um
    ''' </summary>
    ''' <param name="str">umzuwandelnder String</param>
    ''' <returns>Double-Wert, gesetzt auf Konstanten.NaN (-999) falls Wert NaN, Infinity oder unlesbar</returns>
    ''' <remarks></remarks>
    Public Function StringToDouble(ByVal str As String) As Double

        Dim wert As Double
        Dim success As Boolean

        success = Double.TryParse(str, NumberStyles.Any, Konstanten.Zahlenformat, wert)

        If (Not success) Then
            'Wert ist unlesbar
            wert = Konstanten.NaN
            Call Log.AddLogEntry("Der Wert '" & str.Trim() & "' ist unlesbar und wurde durch " & Konstanten.NaN.ToString() & " ersetzt!")
        Else
            'BUG 395: NaN und Infinity abfangen
            If (Double.IsNaN(wert) _
                Or Double.IsInfinity(wert)) Then
                wert = Konstanten.NaN
                Call Log.AddLogEntry("Der Wert '" & str.Trim() & "' wurde durch " & Konstanten.NaN.ToString() & " ersetzt!")
            End If
        End If

        Return wert

    End Function

End Module
