'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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
Imports System.Text.RegularExpressions

''' <summary>
''' Wrapper around a Steema.TeeChart.Axis instance
''' exposing selected properties
''' </summary>
Public Class AxisWrapper

    Private _name As String
    Private _TAxis As Steema.TeeChart.Axis

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="_name">One of "Left", "Right", "Custom 0", etc.</param>
    ''' <param name="_axis">The axis instance to wrap around</param>
    Public Sub New(ByVal _name As String, ByRef _axis As Steema.TeeChart.Axis)
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
    Public Shared Function parseUnit(ByVal text As String) As String

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
