Imports System.Globalization

Module Konstanten

    'Zeilenumbruch
    Public Const eol As String = Chr(13) & Chr(10)

    'Not a Number
    Public Const NaN As Double = -999

    'Zeitreihenformate
    Public Enum Formate As Integer
        ZRE = 1
        WEL = 2
        CSV = 3
        ASC = 4
    End Enum

    'Zahlenformatanweisung
    Public ReadOnly Property FortranProvider() As NumberFormatInfo
        Get
            'Provider einrichten
            FortranProvider = New NumberFormatInfo()
            FortranProvider.NumberDecimalSeparator = "."
            FortranProvider.NumberGroupSeparator = ""
            FortranProvider.NumberGroupSizes = New Integer() {3}
        End Get
    End Property

    'Umwandlung von String zu Double
    '*******************************
    Public Function StringToDouble(ByVal str As String) As Double

        Dim wert As Double

        Try
            wert = Convert.ToDouble(str, Konstanten.FortranProvider)
        Catch ex As Exception
            wert = Konstanten.NaN
        End Try

        Return wert

    End Function

End Module
