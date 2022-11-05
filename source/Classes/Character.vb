'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
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
    Public Sub New(str As String)
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
    Public Sub New(chr As Char)
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
        Set(value As Char)
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
