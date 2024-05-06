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
''' Represents a date range with defined start and end dates
''' </summary>
Public Class DateRange

    Public start As Date
    Public [end] As Date

    Public Sub New(start As Date, [end] As Date)
        If [end] < start Then
            Throw New Exception($"End date must be greater or equal to start date!")
        End If
        Me.start = start
        Me.end = [end]
    End Sub

    Public ReadOnly Property Length As TimeSpan
        Get
            Return Me.end - Me.start
        End Get
    End Property

End Class
