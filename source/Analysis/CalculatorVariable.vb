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
''' A variable representing a time series that can be used in a mathematical expression for the Calculate analysis function
''' </summary>
Friend Class CalculatorVariable

    Private _varName As String
    Private _ts As TimeSeries

    ''' <summary>
    ''' The variable name
    ''' </summary>
    Public ReadOnly Property varName As String
        Get
            Return _varName
        End Get
    End Property

    ''' <summary>
    ''' The time series associated with the variable
    ''' </summary>
    Public ReadOnly Property ts As TimeSeries
        Get
            Return _ts
        End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="varName">the variable name</param>
    ''' <param name="ts">the time series</param>
    Public Sub New(varName As String, ts As TimeSeries)
        _varName = varName
        _ts = ts
    End Sub

    ''' <summary>
    ''' Returns a string representation of the variable consisting of variable name and time series title
    ''' </summary>
    Public Overrides Function ToString() As String
        Return $"{varName}: {ts.Title}"
    End Function
End Class
