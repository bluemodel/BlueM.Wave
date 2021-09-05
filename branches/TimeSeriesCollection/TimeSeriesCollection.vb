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
''' <summary>
''' A collection of TimeSeries
''' </summary>
Public Class TimeSeriesCollection
    Inherits TimeSeriesCollectionBase

    ''' <summary>
    ''' Creates a new TimeSeriesCollection
    ''' </summary>
    ''' <param name="ts_array">one or multiple TimeSeries</param>
    ''' <remarks>Non-common timestamps are filled with NaN values</remarks>
    Public Sub New(ByVal ParamArray ts_array() As TimeSeries)

        Call MyBase.New()

        'store titles and units
        For Each ts As TimeSeries In ts_array
            Me.Titles.Add(ts.Title)
            Me.Units.Add(ts.Unit)
        Next

        'union of all timestamps
        Dim t_union As HashSet(Of DateTime) = ts_array(0).Dates.ToHashSet()
        For i As Integer = 1 To ts_array.Length - 1
            t_union.UnionWith(ts_array(i).Dates)
        Next

        'store values
        Dim v As List(Of Double)
        For Each t As DateTime In t_union
            v = New List(Of Double)
            For Each ts As TimeSeries In ts_array
                If ts.Dates.Contains(t) Then
                    v.Add(ts.Nodes(t))
                Else
                    v.Add(Double.NaN)
                End If
            Next
            Me.Nodes.Add(t, v)
        Next
    End Sub

    ''' <summary>
    ''' Adds a TimeSeries to the TimeSeriesCollection
    ''' </summary>
    ''' <param name="ts">TimeSeries to add</param>
    ''' <remarks>Non-common timestamps are filled with NaN values</remarks>
    Public Overrides Sub AddTimeseries(ByVal ts As TimeSeries)

        Dim t_common As HashSet(Of DateTime) = ts.Dates.ToHashSet()
        t_common.IntersectWith(Me.Dates)

        Dim t_new As HashSet(Of DateTime) = ts.Dates.ToHashSet()
        t_new.ExceptWith(Me.Dates)

        Dim t_old As HashSet(Of DateTime) = Me.Dates.ToHashSet()
        t_old.ExceptWith(ts.Dates)

        'add NaN values to existing nodes not in new timeseries
        For Each t As DateTime In t_old
            Me.Nodes(t).Add(Double.NaN)
        Next

        'add values to common nodes
        For Each t As DateTime In t_common
            Me.Nodes(t).Add(ts.Nodes(t))
        Next

        'create new nodes with NaN values for existing series
        For Each t As DateTime In t_new
            Dim v As New List(Of Double)
            For i As Integer = 0 To Me.Width - 1
                v.Add(Double.NaN)
            Next
            v.Add(ts.Nodes(t))
            Me.Nodes.Add(t, v)
        Next

        'add title and unit
        Me.Titles.Add(ts.Title)
        Me.Units.Add(ts.Unit)
    End Sub

End Class
