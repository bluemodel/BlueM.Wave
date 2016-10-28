'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
'
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
'
Imports System.Globalization

Module Konstanten

    'Zeilenumbruch
    Public Const eol As String = Chr(13) & Chr(10)

    'Datumsformate
    Public ReadOnly Property Datumsformate() As System.Collections.Generic.Dictionary(Of String, String)
        Get
            Dim dict As New System.Collections.Generic.Dictionary(Of String, String)
            dict.Add("default", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO1", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO2", "yyyyMMdd HH:mm")
            dict.Add("SMUSI", "dd MM yyyy   HH")
            dict.Add("UVF", "yyyyMMddHHmm") 'eigentlich nur zweistellige Jahreszahl, aber das Jahrhundert wird beim Einlesen trotzdem bestimmt
            dict.Add("WEL", "dd.MM.yyyy HH:mm")
            dict.Add("ZRE", "yyyyMMdd HH:mm")
            Return dict
        End Get
    End Property

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
        REG_HYSTEM = 5
        REG_SMUSI = 6
        DAT_SWMM_MASS = 7
        DAT_SWMM_TIME = 8
        TXT = 9
        DAT_HYDRO_AS = 10
    End Enum

    ''' <summary>
    ''' Wandelt einen String zu Double um
    ''' </summary>
    ''' <param name="str">umzuwandelnder String</param>
    ''' <returns>Double-Wert, gesetzt auf NaN falls Wert unlesbar. NaN und Infinity werden erkannt und übernommen</returns>
    ''' <remarks></remarks>
    Public Function StringToDouble(ByVal str As String) As Double

        Dim wert As Double
        Dim success As Boolean

        success = Double.TryParse(str, NumberStyles.Any, Konstanten.Zahlenformat, wert)

        If (Not success) Then
            'Wert ist unlesbar
            wert = Double.NaN
            Call Log.AddLogEntry("Der Wert '" & str.Trim() & "' ist unlesbar und wurde durch NaN ersetzt!")
        End If

        Return wert

    End Function

End Module
