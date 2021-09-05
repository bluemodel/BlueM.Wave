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
Public MustInherit Class TimeSeriesCollectionBase

    Private _titles As List(Of String)
    Private _units As List(Of String)
    Private _nodes As SortedList(Of DateTime, List(Of Double))

    ''' <summary>
    ''' The time series collection's nodes
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Nodes As SortedList(Of DateTime, List(Of Double))
        Get
            Return Me._nodes
        End Get
    End Property

    ''' <summary>
    ''' The time series collection's dates
    ''' </summary>
    Public ReadOnly Property Dates() As IList(Of DateTime)
        Get
            Return Me._nodes.Keys
        End Get
    End Property

    ''' <summary>
    ''' The time series collection's titles
    ''' </summary>
    Public ReadOnly Property Titles() As IList(Of String)
        Get
            Return Me._titles
        End Get
    End Property

    ''' <summary>
    ''' The time series collection's units
    ''' </summary>
    Public ReadOnly Property Units() As IList(Of String)
        Get
            Return Me._units
        End Get
    End Property

    ''' <summary>
    ''' The length of the TimeSeriesCollection (number of timestamps)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Length As Integer
        Get
            Return Me.Nodes.Count
        End Get
    End Property

    ''' <summary>
    ''' The width of the TimeSeriesCollection (number of series)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Width As Integer
        Get
            Return Me.Titles.Count
        End Get
    End Property

    ''' <summary>
    ''' Returns the start date of the time series collection
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property StartDate As DateTime
        Get
            Return Me.Dates.First()
        End Get
    End Property

    ''' <summary>
    ''' Returns the end date of the time series collection
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EndDate As DateTime
        Get
            Return Me.Dates.Last()
        End Get
    End Property

    ''' <summary>
    ''' Returns a value given a timestamp and title
    ''' </summary>
    ''' <param name="t">timestamp</param>
    ''' <param name="title">title</param>
    ''' <returns></returns>
    Public ReadOnly Property Value(t As DateTime, ByVal title As String) As Double
        Get
            If Not Me.Titles.Contains(title) Then
                Throw New ArgumentException(String.Format("Series with title '{0}' not found!", title))
            End If
            Return Me.Value(t, Me.Titles.IndexOf(title))
        End Get
    End Property

    ''' <summary>
    ''' Returns a value given a timestamp and index
    ''' </summary>
    ''' <param name="t">timestamp</param>
    ''' <param name="index">index</param>
    ''' <returns></returns>
    Public ReadOnly Property Value(t As DateTime, ByVal index As Integer) As Double
        Get
            If index >= Me.Titles.Count Then
                Throw New ArgumentException(String.Format("Series index {0} out of range!", index))
            End If
            Return Me.Nodes(t)(index)
        End Get
    End Property

    ''' <summary>
    ''' Returns a list of values given a title
    ''' </summary>
    ''' <param name="title"></param>
    ''' <returns></returns>
    Public ReadOnly Property Values(ByVal title As String) As IList(Of Double)
        Get
            If Not Me.Titles.Contains(title) Then
                Throw New ArgumentException(String.Format("Series with title '{0}' not found!", title))
            End If
            Return Me.Values(Me.Titles.IndexOf(title))
        End Get
    End Property

    ''' <summary>
    ''' Returns a list of values given an index
    ''' </summary>
    ''' <param name="index">series index</param>
    ''' <returns></returns>
    Public ReadOnly Property Values(ByVal index As Integer) As IList(Of Double)
        Get
            If index >= Me.Titles.Count Then
                Throw New ArgumentException(String.Format("Series index {0} out of range!", index))
            End If
            Dim v As New List(Of Double)
            For Each t As DateTime In Me.Dates
                v.Add(Me.Value(t, index))
            Next
            Return v
        End Get
    End Property

    ''' <summary>
    ''' Returns a single TimeSeries given a title
    ''' </summary>
    ''' <param name="title"></param>
    ''' <returns></returns>
    ''' <remarks>Returned TimeSeries has no metadata except for title and unit</remarks>
    Public ReadOnly Property TimeSeries(ByVal title As String) As TimeSeries
        Get
            If Not Me.Titles.Contains(title) Then
                Throw New ArgumentException(String.Format("Series with title '{0}' not found!", title))
            End If
            Return Me.TimeSeries(Me.Titles.IndexOf(title))
        End Get
    End Property

    ''' <summary>
    ''' Returns a single TimeSeries given an index
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks>Returned TimeSeries has no metadata except for title and unit</remarks>
    Public ReadOnly Property TimeSeries(ByVal index As Integer) As TimeSeries
        Get
            If index >= Me.Titles.Count Then
                Throw New ArgumentException(String.Format("Series index {0} out of range!", index))
            End If
            Dim ts As New TimeSeries()
            ts.Title = Me.Titles(index)
            ts.Unit = Me.Units(index)
            For Each t As DateTime In Me.Dates
                ts.AddNode(t, Me.Value(t, index))
            Next
            Return ts
        End Get
    End Property

    ''' <summary>
    ''' Creates a new TimeSeriesCollection
    ''' </summary>
    Public Sub New()
        Me._nodes = New SortedList(Of Date, List(Of Double))
        Me._titles = New List(Of String)
        Me._units = New List(Of String)
    End Sub

    ''' <summary>
    ''' Adds a TimeSeries to the TimeSeriesCollection
    ''' </summary>
    ''' <param name="ts">TimeSeries to add</param>
    Public MustOverride Sub AddTimeseries(ByVal ts As TimeSeries)

    ''' <summary>
    ''' Adds new values
    ''' </summary>
    ''' <param name="t">Timestamp</param>
    ''' <param name="values">ParamArray of values</param>
    Public Sub AddValues(ByVal t As DateTime, ParamArray ByVal values As Double())
        If values.Length <> Me.Width Then
            Throw New ArgumentException(String.Format("Unexpected number of values given, expected {0}, got {1}", Me.Width, values.Length))
        End If
        Me.Nodes.Add(t, values.ToList)
    End Sub

End Class
