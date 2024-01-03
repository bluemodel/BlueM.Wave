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
''' Wrapper for a Y axis
''' </summary>
Friend Class AxisWrapper

    Public Axis As ScottPlot.Renderable.Axis
    Private _min As Double
    Private _max As Double
    Private _autoMin As Boolean
    Private _autoMax As Boolean

    Public Sub New(axis As ScottPlot.Renderable.Axis)
        Me.Axis = axis
        Me._autoMin = True
        Me._autoMax = True
    End Sub

    Public ReadOnly Property AxisIndex As Integer
        Get
            Return Me.Axis.AxisIndex
        End Get
    End Property

    Public Property Unit As String
        Get
            Return Me.Axis.AxisLabel.Label
        End Get
        Set(value As String)
            Me.Axis.AxisLabel.Label = value
        End Set
    End Property

    Public Property AutoMin As Boolean
        Get
            Return Me._autoMin
        End Get
        Set(value As Boolean)
            Me._autoMin = value
            Call Me.setLimits()
        End Set
    End Property

    Public Property AutoMax As Boolean
        Get
            Return Me._autoMax
        End Get
        Set(value As Boolean)
            Me._autoMax = value
            Call Me.setLimits()
        End Set
    End Property

    Public Property Max As Double
        Get
            Return Me.Axis.Dims.Max
        End Get
        Set(value As Double)
            Me.Axis.Dims.SetAxis(min:=Nothing, max:=value)
            Me._autoMax = False
            'Call Me.setLimits()
        End Set
    End Property

    Public Property Min As Double
        Get
            Return Me.Axis.Dims.Min
        End Get
        Set(value As Double)
            Me.Axis.Dims.SetAxis(min:=value, max:=Nothing)
            Me._autoMin = False
            'Call Me.setLimits()
        End Set
    End Property

    Public Sub setLimits()
        If Me._autoMin And Me._autoMax Then
            Me.Axis.Dims.ResetLimits()
            'Me._axis.Dims.SetAxis(min:=Nothing, max:=Nothing)
        ElseIf Me._autoMin Then
            Dim max As Double = Me.Max
            Me.Axis.Dims.ResetLimits()
            Me.Axis.Dims.SetAxis(min:=Nothing, max:=max)
        ElseIf Me._autoMax Then
            Dim min As Double = Me.Min
            Me.Axis.Dims.ResetLimits()
            Me.Axis.Dims.SetAxis(min:=min, max:=Nothing)
        Else
            Me.Axis.Dims.SetAxis(Me.Min, Me.Max)
        End If
    End Sub

End Class

