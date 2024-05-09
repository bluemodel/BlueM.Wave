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
''' Class representing a calendar month
''' </summary>
Public Class Month
    ''' <summary>
    ''' Number of the month (1 to 12)
    ''' </summary>
    Public number As Integer
    ''' <summary>
    ''' Name of the month
    ''' </summary>
    Public name As String
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="number">Number of the month (1 to 12)</param>
    ''' <param name="name">Name of the month</param>
    Public Sub New(number As Integer, name As String)
        If number < 1 Or number > 12 Then
            Throw New ArgumentOutOfRangeException("Month number must be between 1 and 12!")
        End If
        Me.number = number
        Me.name = name
    End Sub
    ''' <summary>
    ''' Returns the name of the month
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function ToString() As String
        Return Me.name
    End Function
End Class

