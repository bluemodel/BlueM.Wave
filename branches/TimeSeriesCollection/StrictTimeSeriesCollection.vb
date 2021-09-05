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
''' A collection of TimeSeries with only common timestamps
''' </summary>
Public Class StrictTimeSeriesCollection
    Inherits TimeSeriesCollectionBase

    ''' <summary>
    ''' Creates a new TimeSeriesCollection with only the common timestamps
    ''' </summary>
    ''' <param name="ts_array">one or multiple TimeSeries</param>
    ''' <remarks>Non-common timestamps are discarded</remarks>
    Public Sub New(ByVal ParamArray ts_array() As TimeSeries)

        Call MyBase.New()

        'store titles and units
        For Each ts As TimeSeries In ts_array
            Me.Titles.Add(ts.Title)
            Me.Units.Add(ts.Unit)
        Next

        'find common timestamps
        Dim t_common As HashSet(Of DateTime) = ts_array(0).Dates.ToHashSet()
        For i As Integer = 1 To ts_array.Length - 1
            t_common.IntersectWith(ts_array(i).Dates)
        Next

        'store values for common timestamps only
        Dim v As List(Of Double)
        For Each t As DateTime In t_common
            v = New List(Of Double)
            For Each ts As TimeSeries In ts_array
                v.Add(ts.Nodes(t))
            Next
            Me.Nodes.Add(t, v)
        Next
    End Sub

    ''' <summary>
    ''' Adds a TimeSeries to the StrictTimeSeriesCollection, keeping only the timestamps common to all
    ''' </summary>
    ''' <param name="ts">TimeSeries to add</param>
    ''' <remarks>Any existing timestamps not contained in the TimeSeries being added are also removed</remarks>
    Public Overrides Sub AddTimeseries(ByVal ts As TimeSeries)

        Dim t_common As HashSet(Of DateTime) = Me.Dates.ToHashSet()
        t_common.IntersectWith(ts.Dates)

        Dim t_diff As HashSet(Of DateTime)
        t_diff = Me.Dates.ToHashSet()
        t_diff.SymmetricExceptWith(ts.Dates)

        'remove different nodes
        For Each t As DateTime In t_diff
            Me.Nodes.Remove(t)
        Next

        'add new values for common nodes
        For Each t As DateTime In t_common
            Me.Nodes(t).Add(ts.Nodes(t))
        Next

        'add title and unit
        Me.Titles.Add(ts.Title)
        Me.Units.Add(ts.Unit)
    End Sub

End Class
