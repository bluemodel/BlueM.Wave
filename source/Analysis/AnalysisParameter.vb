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
Friend Class AnalysisParameter

    Public Enum ParameterTypeEnum
        Timeseries
        [Integer]
        [Date]
        [Boolean]
    End Enum

    Public Enum ParameterAmountEnum
        [Single]
        Multiple
    End Enum

    Public ParameterType As ParameterTypeEnum
    Public ParameterAmount As ParameterAmountEnum
    Public Description As String
    Public DefaultValue As Object
    Public MinValue As Object
    Public MaxValue As Object
    Private _Value As Object

    Public Property Value As Object
        Get
            Return _Value
        End Get
        Set
            _Value = Value
        End Set
    End Property

    Public Sub New(description As String, type As ParameterTypeEnum, amount As ParameterAmountEnum, Optional def As Object = Nothing, Optional min As Object = Nothing, Optional max As Object = Nothing)

        Me.Description = description
        Me.ParameterType = type
        Me.ParameterAmount = amount
        Me.DefaultValue = def
        Me.MinValue = min
        Me.MaxValue = max

        Me.Value = Nothing

    End Sub

End Class
