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
Imports System.Text.RegularExpressions

''' <summary>
''' Wrapper around a Steema.TeeChart.Axis instance
''' exposing selected properties
''' </summary>
Friend Class AxisWrapper

    Private _name As String
    Private _TAxis As Steema.TeeChart.Axis

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="_name">One of "Left", "Right", "Custom 0", etc.</param>
    ''' <param name="_axis">The axis instance to wrap around</param>
    Public Sub New(_name As String, ByRef _axis As Steema.TeeChart.Axis)
        Me._name = _name
        Me._TAxis = _axis
    End Sub

    ''' <summary>
    ''' Axis Name, e.g. "Left", "Right", "Custom 0", etc.
    ''' </summary>
    Public ReadOnly Property Name As String
        Get
            Return Me._name
        End Get
    End Property

    ''' <summary>
    ''' Axis Title as displayed in the chart
    ''' </summary>
    ''' <returns></returns>
    Public Property Title As String
        Get
            Return Me._TAxis.Title.Text
        End Get
        Set(value As String)
            Me._TAxis.Title.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Axis Unit, internally stored in the Tag property
    ''' </summary>
    ''' <returns></returns>
    Public Property Unit As String
        Get
            Return Me._TAxis.Tag
        End Get
        Set(value As String)
            Me._TAxis.Tag = value
        End Set
    End Property

    ''' <summary>
    ''' Attempts to extract a unit enclosed in square or round brackets from a text
    ''' </summary>
    ''' <param name="text">Text from which to extract the unit</param>
    ''' <returns>The extracted unit or if unsuccessful the original text</returns>
    Public Shared Function parseUnit(text As String) As String

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
