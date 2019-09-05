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

Public Module Helpers

    ''' <summary>
    ''' Default NumberFormat
    ''' </summary>
    ''' <returns>NumberFormatInfo instance with decimal separator "." and no NumberGroupSeparator</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DefaultNumberFormat() As NumberFormatInfo
        Get
            'NumberFormatInfo einrichten
            DefaultNumberFormat = New NumberFormatInfo()
            DefaultNumberFormat.NumberDecimalSeparator = "."
            DefaultNumberFormat.NumberGroupSeparator = ""
            DefaultNumberFormat.NumberGroupSizes = New Integer() {3}
        End Get
    End Property

    ''' <summary>
    ''' Default DateFormat
    ''' </summary>
    ''' <returns>DateFormats("default")</returns>
    ''' <remarks>dd.MM.yyyy HH:mm</remarks>
    Public ReadOnly Property DefaultDateFormat() As String
        Get
            Return DateFormats("default")
        End Get
    End Property

    ''' <summary>
    ''' Date formats
    ''' </summary>
    ''' <returns>Dictionary of available DateFormats</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DateFormats() As Dictionary(Of String, String)
        Get
            Dim dict As New Dictionary(Of String, String)
            dict.Add("default", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO1", "dd.MM.yyyy HH:mm")
            dict.Add("GISMO2", "yyyyMMdd HH:mm")
            dict.Add("SMUSI", "dd MM yyyy   HH")
            dict.Add("UVF", "yyyyMMddHHmm") 'eigentlich nur zweistellige Jahreszahl, aber das Jahrhundert wird beim Einlesen trotzdem bestimmt
            dict.Add("WEL", "dd.MM.yyyy HH:mm")
            dict.Add("ZRE", "yyyyMMdd HH:mm")
            dict.Add("ZRXP", "yyyyMMddHHmmss")
            Return dict
        End Get
    End Property

    ''' <summary>
    ''' Converts a string to a double
    ''' </summary>
    ''' <param name="str">string to be converted</param>
    ''' <param name="format">optional NumberFormatInfo object to use for the conversion. If not provided, the DefaultNumberInfo is used.</param>
    ''' <returns>Double value, set to NaN if the string was not parseable. NaN and +-Infinity in the input string are recognized and converted to the correspoing Double value.</returns>
    ''' <remarks></remarks>
    Public Function StringToDouble(ByVal str As String, Optional ByVal format As NumberFormatInfo = Nothing) As Double

        Dim value As Double
        Dim success As Boolean

        If format Is Nothing Then
            'use default number format
            format = DefaultNumberFormat
        End If

        success = Double.TryParse(str, NumberStyles.Any, format, value)

        If (Not success) Then
            'string could not be parsed
            value = Double.NaN
            Call Log.AddLogEntry("The value '" & str.Trim() & "' could not be parsed and was converted to NaN!")
        End If

        Return value

    End Function

    ''' <summary>
    ''' Determines whether Wave is running as a 64bit process
    ''' </summary>
    ''' <returns>True if 64bit</returns>
    ''' <remarks></remarks>
    Public Function is64BitProcess() As Boolean

        'once we upgrade to .NET Framework 4, we can use the following:
        'Return Environment.Is64BitProcess()

        'source: https://stackoverflow.com/a/397695
        If (IntPtr.Size = 8) Then
            '64 bit process
            Return True
        ElseIf (IntPtr.Size = 4) Then
            '32 bit process
            Return False
        End If

    End Function

End Module
