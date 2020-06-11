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
Imports System.Text.RegularExpressions

''' <summary>
''' Class for axis instances
''' </summary>
Friend Class Axis

    Private _number As Integer
    Private _title As String
    Private _unit As String

    Public TAxis As Steema.TeeChart.Axis

    ''' <summary>
    ''' Axis number
    ''' 1: Left Axis
    ''' 2: Right Axis
    ''' >= 3: Custom Axis
    ''' </summary>
    Public Property Number As Integer
        Get
            Return _number
        End Get
        Set(value As Integer)
            _number = value
        End Set
    End Property

    ''' <summary>
    ''' Axis display title
    ''' </summary>
    ''' <returns></returns>
    Public Property Title As String
        Get
            Return _title
        End Get
        Set(value As String)
            _title = value
        End Set
    End Property

    ''' <summary>
    ''' Axis unit
    ''' </summary>
    ''' <returns></returns>
    Public Property Unit As String
        Get
            Return _unit
        End Get
        Set(value As String)
            _unit = value
        End Set
    End Property

    ''' <summary>
    ''' Attempts to extract a unit enclosed in square or round brackets from a text
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns>The detected unit or if unsuccessful the original text</returns>
    Public Shared Function parseUnit(ByVal text As String) As String

        Dim m As Match
        Dim unit As String

        m = Regex.Match(text, ".*[\[\(](.+?)[\]\)].*")
        If m.Success Then
            unit = m.Groups(1).Value
        Else
            unit = text
        End If

        Return unit

    End Function
End Class
