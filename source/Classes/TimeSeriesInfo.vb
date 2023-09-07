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
''' Contains basic information about a series contained in a file
''' </summary>
''' <remarks></remarks>
Public Class TimeSeriesInfo

    ''' <summary>
    ''' Series name
    ''' </summary>
    Public Name As String

    ''' <summary>
    ''' Series unit
    ''' </summary>
    Public Unit As String

    ''' <summary>
    ''' Index of the series in the file (usually the column index)
    ''' </summary>
    Public Index As Integer

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()

    End Sub

    Public Overrides Function ToString() As String
        Return Me.Name
    End Function

End Class

