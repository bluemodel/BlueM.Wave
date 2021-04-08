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
''' <summary>
''' Helper Class for Characters
''' </summary>
''' <remarks></remarks>
Public Class Character

    Private _char As Char

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="str">String to convert (only the first character is used)
    ''' Special strings such as " ", "space", "tab", "point", "comma" and "semicolon" are also possible
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal str As String)
        Select Case str.Trim().ToLower()
            Case "", "space"
                _char = Chr(32)
            Case "tab"
                _char = Chr(9)
            Case "point"
                _char = Chr(46)
            Case "comma"
                _char = Chr(44)
            Case "semicolon"
                _char = Chr(59)
            Case Else
                _char = Convert.ToChar(str)
        End Select
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="chr">Character</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal chr As Char)
        ToChar = chr
    End Sub

    ''' <summary>
    ''' Returns a Char instance
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ToChar() As Char
        Get
            Return _char
        End Get
        Set(ByVal value As Char)
            _char = value
        End Set
    End Property

    ''' <summary>
    ''' Returns a plaintext string representation (e.g. "comma")
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String

        Dim output As String

        Select Case Me.ToChar
            Case Chr(32)
                output = "space"
            Case Chr(9)
                output = "tab"
            Case Chr(46)
                output = "point"
            Case Chr(44)
                output = "comma"
            Case Chr(59)
                output = "semicolon"
            Case Else
                output = Convert.ToString(Me.ToChar)
        End Select

        Return output

    End Function

End Class
