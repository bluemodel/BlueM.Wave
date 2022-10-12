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
''' Collection of TimeSeries
''' Implemented as an ordered dictionary with Ids as keys and TimeSeries as values
''' </summary>
Public Class TimeSeriesCollection
    Implements IDictionary(Of Integer, TimeSeries)

    Private _timeseriesdict As Specialized.OrderedDictionary

    Public Sub New()
        _timeseriesdict = New Specialized.OrderedDictionary()
    End Sub

    ''' <summary>
    ''' Gets or sets the TimeSeries with the specified Id
    ''' </summary>
    ''' <param name="Id">The Id of the TimeSeries to get or set</param>
    ''' <returns>The TimeSeries with the specified Id</returns>
    Default Public Property Item(Id As Integer) As TimeSeries Implements IDictionary(Of Integer, TimeSeries).Item
        Get
            If Me.ContainsId(Id) Then
                For index As Integer = 0 To _timeseriesdict.Count - 1
                    If _timeseriesdict.Keys(index) = Id Then
                        Return _timeseriesdict(index)
                    End If
                Next
            End If
            Throw New KeyNotFoundException($"TimeSeries with id {Id} not found!")
        End Get
        Set(value As TimeSeries)
            If Me.ContainsId(Id) Then
                For index As Integer = 0 To Me.Count - 1
                    If Me.Ids(index) = Id Then
                        _timeseriesdict(index) = value
                    End If
                Next
            Else
                Throw New KeyNotFoundException($"TimeSeries with id {Id} not found!")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets an ICollection(Of Integer) containing the Ids in the TimeSeriesCollection
    ''' </summary>
    ''' <returns>An ICollection(Of Integer) containing the Ids in the TimeSeriesCollection</returns>
    Public ReadOnly Property Ids As ICollection(Of Integer) Implements IDictionary(Of Integer, TimeSeries).Keys
        Get
            Return _timeseriesdict.Keys.Cast(Of Integer).ToList()
        End Get
    End Property

    ''' <summary>
    ''' Gets an ICollection(Of TimeSeries) containing the TimeSeries in the TimeSeriesCollection
    ''' </summary>
    ''' <returns>An ICollection(Of TimeSeries) containing the TimeSeries in the TimeSeriesCollection</returns>
    Public ReadOnly Property Values As ICollection(Of TimeSeries) Implements IDictionary(Of Integer, TimeSeries).Values
        Get
            Return _timeseriesdict.Values.Cast(Of TimeSeries).ToList()
        End Get
    End Property

    ''' <summary>
    ''' Gets a List(Of TimeSeries) containing the TimeSeries in the TimeSeriesCollection
    ''' </summary>
    ''' <returns>A List(Of TimeSeries) containing the TimeSeries in the TimeSeriesCollection</returns>
    Public ReadOnly Property ToList As List(Of TimeSeries)
        Get
            Return _timeseriesdict.Values.Cast(Of TimeSeries).ToList()
        End Get
    End Property

    ''' <summary>
    ''' Gets the number of TimeSeries contained in the TimeSeriesCollection
    ''' </summary>
    ''' <returns>The number of TimeSeries contained in the TimeSeriesCollection</returns>
    Public ReadOnly Property Count As Integer Implements ICollection(Of KeyValuePair(Of Integer, TimeSeries)).Count
        Get
            Return _timeseriesdict.Count
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the TimeSeriesCollection is read-only
    ''' </summary>
    ''' <returns>True if the TimeSeriesCollection is read-only; otherwise, False</returns>
    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of KeyValuePair(Of Integer, TimeSeries)).IsReadOnly
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Adds a TimeSeries to the TimeSeriesCollection
    ''' </summary>
    ''' <param name="ts">The TimeSeries to add</param>
    Public Sub Add(ts As TimeSeries)
        Me.Add(ts.Id, ts)
    End Sub

    ''' <summary>
    ''' Adds a TimeSeries with the provided Id to the TimeSeriesCollection
    ''' </summary>
    ''' <param name="Id">The Id of the Timeseries to add</param>
    ''' <param name="ts">The TimeSeries to add</param>
    Public Sub Add(Id As Integer, ts As TimeSeries) Implements IDictionary(Of Integer, TimeSeries).Add
        _timeseriesdict.Add(Id, ts)
    End Sub

    Public Sub Add(item As KeyValuePair(Of Integer, TimeSeries)) Implements ICollection(Of KeyValuePair(Of Integer, TimeSeries)).Add
        _timeseriesdict.Add(item.Key, item.Value)
    End Sub

    ''' <summary>
    ''' Revomes all TimeSeries from the TimeSeriesCollection
    ''' </summary>
    Public Sub Clear() Implements ICollection(Of KeyValuePair(Of Integer, TimeSeries)).Clear
        _timeseriesdict.Clear()
    End Sub

    Public Sub CopyTo(array() As KeyValuePair(Of Integer, TimeSeries), arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of Integer, TimeSeries)).CopyTo
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Determines whether the TimeSeriesCollection contains a TimeSeries with the specified Id
    ''' </summary>
    ''' <param name="Id">The Id to locate in the TimeSeriesCollection</param>
    ''' <returns>True if the TimeSeriesCollection contains a TimeSeries with the Id; otherwise, False</returns>
    Public Function ContainsId(Id As Integer) As Boolean Implements IDictionary(Of Integer, TimeSeries).ContainsKey
        Return _timeseriesdict.Contains(Id)
    End Function

    ''' <summary>
    ''' Removes the TimeSeries with the specified Id from the TimeSeriesCollection
    ''' </summary>
    ''' <param name="Id">The Id of the TimneSeries to remove</param>
    ''' <returns>True if the TimeSeries is successfully removed; otherwise, False</returns>
    Public Function Remove(Id As Integer) As Boolean Implements IDictionary(Of Integer, TimeSeries).Remove
        If Me.ContainsId(Id) Then
            _timeseriesdict.Remove(Id)
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Remove(item As KeyValuePair(Of Integer, TimeSeries)) As Boolean Implements ICollection(Of KeyValuePair(Of Integer, TimeSeries)).Remove
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' Gets the TimeSeries associated with the specified Id
    ''' </summary>
    ''' <param name="Id">The Id of the TimeSeries to get</param>
    ''' <param name="ts">
    ''' When this method returns, the TimeSeries associated with the specified Id, if the Id is found; 
    ''' otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.
    ''' </param>
    ''' <returns>True if the TimeSeriesCollection contains a TimeSeries with the specified Id; otherwise False</returns>
    Public Function TryGetTimeSeries(Id As Integer, ByRef ts As TimeSeries) As Boolean Implements IDictionary(Of Integer, TimeSeries).TryGetValue
        If Me.ContainsId(Id) Then
            ts = Me.Item(Id)
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Contains(item As KeyValuePair(Of Integer, TimeSeries)) As Boolean Implements ICollection(Of KeyValuePair(Of Integer, TimeSeries)).Contains
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' Returns an enumerator that iterates through the TimeSeriesCollection
    ''' </summary>
    ''' <returns>An enumerator that can be used to iterate through the TimeSeriesCollection</returns>
    Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of Integer, TimeSeries)) Implements IEnumerable(Of KeyValuePair(Of Integer, TimeSeries)).GetEnumerator
        Return _timeseriesdict.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Me.GetEnumerator()
    End Function

    ''' <summary>
    ''' Reorders the TimeSeries contained in the TimeSeriesCollection according to the specified list of Ids
    ''' </summary>
    ''' <param name="ids">List of Ids in the new order</param>
    Public Sub Reorder(ids As List(Of Integer))
        Dim newdict As New Specialized.OrderedDictionary()
        For Each id As Integer In ids
            newdict.Add(id, Me.Item(id))
        Next
        _timeseriesdict = newdict
    End Sub

End Class
