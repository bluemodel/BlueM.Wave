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
''' Class representing time series metadata
''' </summary>
''' <remarks>Acts as a Dictionary(Of String, String)</remarks>
Public Class Metadata
    Inherits Dictionary(Of String, String)

    ''' <summary>
    ''' Adds a list of keys with empty values
    ''' </summary>
    ''' <param name="keys">A list of keys to add</param>
    ''' <remarks>Values of keys that are already present are not overwritten</remarks>
    Public Sub AddKeys(keys As List(Of String))
        For Each key As String In keys
            If Not Me.ContainsKey(key) Then
                Me.Add(key, "")
            End If
        Next
    End Sub

End Class
