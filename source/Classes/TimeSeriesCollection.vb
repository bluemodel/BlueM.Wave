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
    ''' Reorders a TimeSeries within the collection
    ''' </summary>
    ''' <param name="id">Id of the TimeSeries to reorder</param>
    ''' <param name="direction">Direction</param>
    Public Sub Reorder(id As Integer, direction As Direction)
        Dim ts As TimeSeries
        'find index of timeseries to reorder
        Dim index As Integer = 0
        For Each ts In _timeseriesdict.Values
            If ts.Id = id Then
                Exit For
            End If
            index += 1
        Next
        If (direction = Direction.Up And index = 0) Or
           (direction = Direction.Down And index = _timeseriesdict.Count - 1) Then
            'series is already at top/bottom
            Exit Sub
        End If
        'reorder series
        Dim offset As Integer
        If direction = Direction.Up Then
            offset = -1
        Else
            offset = 1
        End If
        ts = _timeseriesdict(index)
        Me._timeseriesdict.RemoveAt(index)
        Me._timeseriesdict.Insert(index + offset, ts.Id, ts)
    End Sub

End Class
