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
Imports System.Globalization

''' <summary>
''' Class representing a time series
''' </summary>
''' <remarks>A time series consists of a list of nodes (date, value) and associated metadata</remarks>
Public Class TimeSeries

    ''' <summary>
    ''' Interpretations
    ''' </summary>
    Public Enum InterpretationEnum As Integer
        Instantaneous = 1
        BlockRight = 2
        BlockLeft = 3
        Cumulative = 4
        CumulativePerTimestep = 5
        Undefined = 99
    End Enum

    ''' <summary>
    ''' Timestep types
    ''' </summary>
    Public Enum TimeStepTypeEnum As Short
        Year = 1
        Month = 2
        Week = 3
        Day = 4
        Hour = 5
        Minute = 6
        Second = 7
    End Enum

#Region "Members"

    Private _id As Integer
    Private _title As String
    Private _nodes As SortedList(Of DateTime, Double)
    Private _nodesCleaned As SortedList(Of DateTime, Double)
    Private _unit As String
    Private _metadata As Metadata
    Private _Objekt As String
    Private _Type As String
    Private _Interpretation As InterpretationEnum
    Private _DataSource As TimeSeriesDataSource
    Private _displayOptions As TimeSeriesDisplayOptions

#End Region 'Members

#Region "Properties"

    ''' <summary>
    ''' Unique Id
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Id() As Integer
        Get
            Return Me._id
        End Get
    End Property

    ''' <summary>
    ''' Title of the time series
    ''' </summary>
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(value As String)
            _title = value
        End Set
    End Property

    ''' <summary>
    ''' The time series' interpretation
    ''' </summary>
    Public Property Interpretation() As InterpretationEnum
        Get
            Return Me._Interpretation
        End Get
        Set(value As InterpretationEnum)
            Me._Interpretation = value
        End Set
    End Property

    ''' <summary>
    ''' The time series' nodes
    ''' </summary>
    Public Overloads ReadOnly Property Nodes() As SortedList(Of DateTime, Double)
        Get
            Return _nodes
        End Get
    End Property

    ''' <summary>
    ''' Returns the value at timestamp t
    ''' </summary>
    Public Overloads ReadOnly Property Nodes(t As DateTime) As Double
        Get
            Return _nodes(t)
        End Get
    End Property

    ''' <summary>
    ''' The time series' nodes within a defined time period (inclusively)
    ''' </summary>
    Public Overloads ReadOnly Property Nodes(startdate As DateTime, enddate As DateTime) As SortedList(Of DateTime, Double)
        Get
            Dim nodelist As New SortedList(Of DateTime, Double)
            For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
                If node.Key < startdate Then
                    Continue For
                ElseIf node.Key > enddate Then
                    Exit For
                End If
                nodelist.Add(node.Key, node.Value)
            Next
            Return nodelist
        End Get
    End Property

    ''' <summary>
    ''' The time series' nodes without NaN and Infinity values
    ''' </summary>
    ''' <remarks>The cleaned nodes are cached and only created once per instance</remarks>
    Public Overloads ReadOnly Property NodesClean() As SortedList(Of DateTime, Double)
        Get
            If IsNothing(Me._nodesCleaned) Then
                Me._nodesCleaned = New SortedList(Of DateTime, Double)
                For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
                    If Not (Double.IsNaN(node.Value) Or Double.IsInfinity(node.Value)) Then
                        Me._nodesCleaned.Add(node.Key, node.Value)
                    End If
                Next
            End If
            Return Me._nodesCleaned
        End Get
    End Property

    ''' <summary>
    ''' The time series' nodes without NaN and Infinity values within a defined time period (inclusively)
    ''' </summary>
    Public Overloads ReadOnly Property NodesClean(startdate As DateTime, enddate As DateTime) As SortedList(Of DateTime, Double)
        Get
            Dim nodelist As New SortedList(Of DateTime, Double)
            For Each node As KeyValuePair(Of DateTime, Double) In Me.NodesClean
                If node.Key < startdate Then
                    Continue For
                ElseIf node.Key > enddate Then
                    Exit For
                End If
                nodelist.Add(node.Key, node.Value)
            Next
            Return nodelist
        End Get
    End Property

    ''' <summary>
    ''' The time series' dates
    ''' </summary>
    Public ReadOnly Property Dates() As IList(Of DateTime)
        Get
            Return Me._nodes.Keys
        End Get
    End Property

    ''' <summary>
    ''' The time series' values
    ''' </summary>
    Public ReadOnly Property Values() As IList(Of Double)
        Get
            Return Me._nodes.Values
        End Get
    End Property

    ''' <summary>
    ''' The time series' metadata
    ''' </summary>
    Public Property Metadata() As Metadata
        Get
            Return Me._metadata
        End Get
        Set(value As Metadata)
            Me._metadata = value
        End Set
    End Property

    ''' <summary>
    ''' The time series' metadata formatted as a single string
    ''' </summary>
    ''' <remarks>Empty entries are omitted from the string</remarks>
    Public ReadOnly Property MetadataText() As String
        Get
            Dim text As String = ""
            For Each kvp As KeyValuePair(Of String, String) In Me._metadata
                If kvp.Value <> "" Then
                    'omit empty entries
                    text &= $"{kvp.Key}: {kvp.Value}, "
                End If
            Next
            Return text
        End Get
    End Property

    ''' <summary>
    ''' The time series' length (number of nodes)
    ''' </summary>
    Public ReadOnly Property Length() As Integer
        Get
            Return Me._nodes.Count
        End Get
    End Property

    ''' <summary>
    ''' The number of NaN and Infinity nodes
    ''' </summary>
    Public ReadOnly Property NaNCount As Integer
        Get
            Return Me.Nodes.Count - Me.NodesClean.Count
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of time periods (range, count) consisting of NaN nodes
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NaNPeriods As List(Of (range As DateRange, count As Integer))
        Get
            NaNPeriods = New List(Of (range As DateRange, count As Integer))

            If Me.NaNCount > 0 Then

                Dim isNanPeriod As Boolean = False

                Dim start As DateTime = Nothing
                Dim [end] As DateTime = Nothing
                Dim count As Integer

                'if the first value is NaN, start a NaN period
                If Double.IsNaN(Me.FirstValue) Then
                    isNanPeriod = True
                    start = Me.StartDate
                End If

                'loop through all nodes
                count = 0
                For i = 0 To Me.Length - 1
                    If Not isNanPeriod Then
                        If Double.IsNaN(Me.Values(i)) Then
                            'start of NaN period
                            isNanPeriod = True
                            start = Me.Dates(i)
                            count = 1
                        End If
                    Else
                        If Double.IsNaN(Me.Values(i)) Then
                            count += 1
                        Else
                            'end of NaN period
                            isNanPeriod = False
                            [end] = Me.Dates(i - 1)
                            'store NaN period
                            NaNPeriods.Add((New DateRange(start, [end]), count))
                        End If
                    End If
                Next

                'if the last value is NaN, add a last NaN period
                If Double.IsNaN(Me.LastValue) Then
                    NaNPeriods.Add((New DateRange(start, Me.EndDate), count))
                End If

            End If

            Return NaNPeriods
        End Get
    End Property

    ''' <summary>
    ''' The unit of the the time series' values
    ''' </summary>
    Public Property Unit() As String
        Get
            Return _unit
        End Get
        Set(value As String)
            If (value.Trim() = "") Then value = "-"
            _unit = value
        End Set
    End Property

    ''' <summary>
    ''' The original datasource of the time series
    ''' </summary>
    ''' <returns></returns>
    Public Property DataSource As TimeSeriesDataSource
        Get
            Return _DataSource
        End Get
        Set(value As TimeSeriesDataSource)
            _DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Options for displaying the time series
    ''' </summary>
    ''' <returns></returns>
    Public Property DisplayOptions As TimeSeriesDisplayOptions
        Get
            Return _displayOptions
        End Get
        Set(value As TimeSeriesDisplayOptions)
            _displayOptions = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the start date of the time series
    ''' </summary>
    Public ReadOnly Property StartDate() As DateTime
        Get
            Return Me.Dates.First()
        End Get
    End Property

    ''' <summary>
    ''' Returns the end date of the time series
    ''' </summary>
    Public ReadOnly Property EndDate() As DateTime
        Get
            Return Me.Dates.Last()
        End Get
    End Property

    ''' <summary>
    ''' Returns the maximum value of the time series
    ''' </summary>
    Public Overloads ReadOnly Property Maximum() As Double
        Get
            If Me.NodesClean.Count > 0 Then
                Return Me.NodesClean().Values.Max
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the maximum value of the time series within a defined time period (inclusively)
    ''' </summary>
    Public Overloads ReadOnly Property Maximum(startdate As DateTime, enddate As DateTime) As Double
        Get
            If Me.NodesClean(startdate, enddate).Count > 0 Then
                Return Me.NodesClean(startdate, enddate).Values.Max
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the node with the maximum value of the time series
    ''' </summary>
    Public Overloads ReadOnly Property MaximumNode() As KeyValuePair(Of DateTime, Double)
        Get
            Dim nodes As SortedList(Of DateTime, Double) = Me.NodesClean()
            If nodes.Count > 0 Then
                Dim maxValue = nodes.Values.Max
                Dim maxIndex = nodes.IndexOfValue(maxValue)
                Dim maxDate As DateTime = nodes.Keys(maxIndex)
                Return New KeyValuePair(Of DateTime, Double)(maxDate, maxValue)
            Else
                Return New KeyValuePair(Of DateTime, Double)(Me.StartDate, Double.NaN)
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the node with the maximum value of the time series within a defined time period (inclusively)
    ''' </summary>
    Public Overloads ReadOnly Property MaximumNode(startdate As DateTime, enddate As DateTime) As KeyValuePair(Of DateTime, Double)
        Get
            Dim nodes As SortedList(Of DateTime, Double) = Me.NodesClean(startdate, enddate)
            If nodes.Count > 0 Then
                Dim maxValue = nodes.Values.Max
                Dim maxIndex = nodes.IndexOfValue(maxValue)
                Dim maxDate As DateTime = nodes.Keys(maxIndex)
                Return New KeyValuePair(Of DateTime, Double)(maxDate, maxValue)
            Else
                Return New KeyValuePair(Of DateTime, Double)(startdate, Double.NaN)
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the minimum value of the time series
    ''' </summary>
    Public Overloads ReadOnly Property Minimum() As Double
        Get
            If Me.NodesClean.Count > 0 Then
                Return Me.NodesClean().Values.Min
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the minimum value of the time series within a defined time period (inclusively)
    ''' </summary>
    Public Overloads ReadOnly Property Minimum(startdate As DateTime, enddate As DateTime) As Double
        Get
            If Me.NodesClean(startdate, enddate).Count > 0 Then
                Return Me.NodesClean(startdate, enddate).Values.Min
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the average value of the time series
    ''' </summary>
    ''' <remarks>Simple average calculation without regard for time</remarks>
    Public ReadOnly Property Average() As Double
        Get
            Dim avg As Double
            avg = 0
            For Each value As Double In Me.NodesClean.Values
                avg += value
            Next
            avg = avg / Me.NodesClean.Count
            Return avg
        End Get
    End Property

    ''' <summary>
    ''' Returns the first value of the time series
    ''' </summary>
    Public ReadOnly Property FirstValue() As Double
        Get
            Return Me.Values.First()
        End Get
    End Property

    ''' <summary>
    ''' Returns the last value of the time series
    ''' </summary>
    Public ReadOnly Property LastValue() As Double
        Get
            Return Me.Values.Last()
        End Get
    End Property

    ''' <summary>
    ''' Returns the sum of the time series' values
    ''' </summary>
    Public ReadOnly Property Sum() As Double
        Get
            Dim _sum As Double
            _sum = 0.0
            For Each value As Double In Me.NodesClean.Values
                _sum += value
            Next
            Return _sum
        End Get
    End Property

    ''' <summary>
    ''' Returns the volume of the time series (values integrated over time)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' Only works for time-based units that end with "/s", "/min", "/h" or "/d"
    ''' and interpretations Instantaneous, BlockLeft and BlockRight. 
    ''' Otherwise returns NaN.
    ''' </remarks>
    Public ReadOnly Property Volume() As Double
        Get
            Dim v1, v2, partial_volume, total_volume As Double
            Dim t1, t2 As DateTime

            If Me.NodesClean.Count = 0 Then
                Return Double.NaN
            End If

            If Me.Interpretation = InterpretationEnum.CumulativePerTimestep _
                Or Me.Interpretation = InterpretationEnum.Cumulative _
                Or Me.Interpretation = InterpretationEnum.Undefined Then
                Return Double.NaN
            End If

            'determine timestep type of unit
            Dim unitTimesteptype As TimeStepTypeEnum
            If Me.Unit.ToLower.EndsWith("/s") Then
                unitTimesteptype = TimeStepTypeEnum.Second
            ElseIf Me.Unit.ToLower.EndsWith("/min") Then
                unitTimesteptype = TimeStepTypeEnum.Minute
            ElseIf Me.Unit.ToLower.EndsWith("/h") Then
                unitTimesteptype = TimeStepTypeEnum.Hour
            ElseIf Me.Unit.ToLower.EndsWith("/d") Then
                unitTimesteptype = TimeStepTypeEnum.Day
            Else
                'not a unit supported for volume calculation
                Return Double.NaN
            End If

            'Integrate volume between each node and cumulate
            total_volume = 0.0
            For i As Integer = 1 To Me.Length - 1
                t1 = Me.Nodes.Keys(i - 1)
                v1 = Me.Nodes.Values(i - 1)
                t2 = Me.Nodes.Keys(i)
                v2 = Me.Nodes.Values(i)
                partial_volume = TimeSeries.IntegrateVolume(t1, v1, t2, v2, Me.Interpretation, unitTimesteptype)
                If Not Double.IsNaN(partial_volume) Then
                    total_volume += partial_volume
                End If
            Next

            Return total_volume
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Default Constructor
    ''' </summary>
    Public Sub New()
        Call Me.New("-")
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="title">Title of the time series</param>
    Public Sub New(title As String)
        Me._id = TimeSeries.getUniqueID()
        Me._metadata = New Metadata()
        Me._title = title
        Me._unit = "-"
        Me._Interpretation = InterpretationEnum.Undefined
        Me.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.Undefined)
        Me.DisplayOptions = New TimeSeriesDisplayOptions()
        Me._nodes = New SortedList(Of DateTime, Double)
    End Sub

    ''' <summary>
    ''' Returns the time series' title
    ''' </summary>
    Public Overrides Function ToString() As String
        Return Me.Title
    End Function

    ''' <summary>
    ''' Clones a time series
    ''' </summary>
    ''' <param name="preserveId">if True, the Id is preserved, otherwise a new unique Id is assigned (default)</param>
    Public Function Clone(Optional preserveId As Boolean = False) As TimeSeries
        Dim target As New TimeSeries(Me.Title)
        If preserveId Then
            target._id = Me.Id
        End If
        target.Unit = Me.Unit
        target._nodes = New SortedList(Of DateTime, Double)(Me._nodes)
        target.Metadata = Me.Metadata.Copy()
        target._Interpretation = Me.Interpretation
        target.DataSource = Me.DataSource.Copy()
        target.DisplayOptions = Me.DisplayOptions.Copy()
        Return target
    End Function

    ''' <summary>
    ''' Adds a node to the time series
    ''' </summary>
    ''' <param name="_date">Date</param>
    ''' <param name="_value">Value</param>
    ''' <remarks>If the given date already exists, the new node is discarded and a warning is written to the log</remarks>
    Public Sub AddNode(_date As DateTime, _value As Double)
        If (Me.Nodes.ContainsKey(_date)) Then
            Log.AddLogEntry(Log.levels.warning, $"Duplicate data point at {_date.ToString(Helpers.CurrentDateFormat)}: Value of {_value.ToString(Helpers.DefaultNumberFormat)} will be discarded. Existing value: {Me.Nodes(_date).ToString(Helpers.DefaultNumberFormat)}")
            Exit Sub
        End If
        Me._nodes.Add(_date, _value)
    End Sub

    ''' <summary>
    ''' Updates the value of an existing node
    ''' </summary>
    ''' <param name="_date">Date</param>
    ''' <param name="_value">Value</param>
    Public Sub UpdateNode(_date As DateTime, _value As Double)
        If Not Me.Nodes.ContainsKey(_date) Then
            Throw New Exception($"Unable to update node, no existing node for date {_date} found!")
        End If
        Me._nodes(_date) = _value
    End Sub

    ''' <summary>
    ''' Cut a time series using start and end dates
    ''' </summary>
    ''' <param name="_start">start date</param>
    ''' <param name="_end">end date</param>
    ''' <param name="titleSuffix">suffix to be added to the title of time series after cutting (default: " (cut)")</param>
    ''' <remarks>
    ''' Removes all nodes before the start date and after the end date.
    ''' If there is no node with the exact start date, the closest node before the start date is also kept.
    ''' If there is no node with the exact end date, the closest node after the end date is also kept.
    ''' </remarks>
    Public Overloads Sub Cut(_start As DateTime, _end As DateTime, Optional titleSuffix As String = " (cut)")

        Dim i, lengthOld, lengthNew As Integer
        Dim iStart, iEnd As Integer
        Dim newNodes As SortedList(Of DateTime, Double)

        If _start >= _end Then
            Throw New ArgumentException("Unable to cut time series: start date is on or after end date!")
        End If

        If (Me.StartDate < _start Or Me.EndDate > _end) Then

            Me.Title &= titleSuffix

            lengthOld = Me.Length

            'find the start
            iStart = Me._nodes.IndexOfKey(_start)

            If (iStart = -1) Then
                'no node with the exact start date found
                'use the closest node before the start instead
                For i = 1 To Me.Length - 1
                    If (Me.Dates(i) > _start) Then
                        'new start date
                        _start = Me.Dates(i - 1)
                        Exit For
                    End If
                Next
            End If

            'find the end
            iEnd = Me._nodes.IndexOfKey(_end)

            If (iEnd = -1) Then
                'no node with the exact end date found
                'use the closest node after the end instead
                For i = Me.Length - 2 To 0 Step -1
                    If (Me.Dates(i) < _end) Then
                        'new end date
                        _end = Me.Dates(i + 1)
                        Exit For
                    End If
                Next
            End If

            'copy nodes within start and end to a new list
            newNodes = New SortedList(Of DateTime, Double)
            For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
                If (node.Key >= _start And node.Key <= _end) Then
                    newNodes.Add(node.Key, node.Value)
                End If
            Next

            lengthNew = newNodes.Count

            Me._nodes = newNodes

            'trim excess capacity
            Call Me.Nodes.TrimExcess()

            'invalidate NodesCleaned
            Me._nodesCleaned = Nothing

            'Log 
            Call Log.AddLogEntry(Log.levels.info, $"{Me.Title}: cut from {lengthOld} to {lengthNew} data points.")

        End If

    End Sub

    ''' <summary>
    ''' Cut the time series to the timespan of another time series
    ''' </summary>
    ''' <param name="series2">the time series to whose timespan the time series should be cut</param>
    ''' <param name="titleSuffix">suffix to be added to the title of time series after cutting (default: " (cut)")</param>
    ''' <remarks></remarks>
    Public Overloads Sub Cut(series2 As TimeSeries, Optional titleSuffix As String = " (cut)")

        If (Me.StartDate < series2.StartDate Or Me.EndDate > series2.EndDate) Then
            Call Me.Cut(series2.StartDate, series2.EndDate, titleSuffix)
        End If

    End Sub

    ''' <summary>
    ''' Appends a time series
    ''' </summary>
    ''' <param name="series2">series to be appended</param>
    ''' <remarks>in the case of an overlap between the two series, 
    ''' the nodes of the appended series within the overlap are discarded</remarks>
    Public Sub Append(series2 As TimeSeries)

        Dim startDate, endDate As DateTime

        If series2.EndDate <= Me.EndDate And series2.StartDate >= Me.StartDate Then
            'series2 does not extend beyond this series, so nothing to do
            Log.AddLogEntry(Log.levels.warning, $"Series '{series2.Title}' does not extend beyond series '{Me.Title}' so nothing can be appended!")
            Return
        End If

        'make copies of start and end date because they update dynamically
        startDate = Me.StartDate
        endDate = Me.EndDate

        'copy all nodes from series2 outside the range of this series
        For Each node As KeyValuePair(Of DateTime, Double) In series2.Nodes
            If node.Key < startDate Or node.Key > endDate Then
                Me.AddNode(node.Key, node.Value)
            End If
        Next

    End Sub


    ''' <summary>
    ''' Returns the index of the node with the specified timestamp
    ''' </summary>
    ''' <param name="timestamp">The timestamp to look for</param>
    ''' <param name="direction">The direction to search if the exact timestamp is not found (default: Before)</param>
    ''' <returns>The index of the node with the specified timestamp, or the index of the date closest to the timestamp in the given direction</returns>
    Private Function IndexOfDate(timestamp As DateTime, Optional direction As TimeDirection = TimeDirection.Before) As Integer

        If timestamp < Me.StartDate Then
            Return 0
        End If
        If timestamp > Me.EndDate Then
            Return Me.Length - 1
        End If
        If Me._nodes.ContainsKey(timestamp) Then
            Return Me._nodes.IndexOfKey(timestamp)
        End If

        'return index of the date closest to the timestamp
        Dim t_index As Integer
        Dim t As DateTime
        Select Case direction
            Case TimeDirection.Before
                'start at the end and go backwards
                t_index = Me.Length - 1
                t = Me.Dates(t_index)
                While t >= timestamp And t_index > 0
                    t_index -= 1
                    t = Me.Dates(t_index)
                End While
            Case TimeDirection.After
                'start at the beginning and go forwards
                t_index = 0
                t = Me.Dates(t_index)
                While t <= timestamp And t_index < Me.Length - 1
                    t_index += 1
                    t = Me.Dates(t_index)
                End While
        End Select

        Return t_index

    End Function

    ''' <summary>
    ''' Returns the date of the node closest to the specified timestamp
    ''' </summary>
    ''' <param name="timestamp">The timestamp to look for</param>
    ''' <param name="direction">The direction to search if the exact timestamp is not found (default: Before)</param>
    ''' <returns>The date of the node closest to the specified timestamp in the given direction</returns>
    Public Function ClosestDate(timestamp As DateTime, Optional direction As TimeDirection = TimeDirection.Before) As DateTime
        Dim index As Integer = Me.IndexOfDate(timestamp, direction)
        Return Me.Dates(index)
    End Function

    ''' <summary>
    ''' Splits a time series into individual series for each hydrological year
    ''' </summary>
    ''' <param name="startMonth">the month with which the hydrological year starts. Default = 11 (November)</param>
    ''' <returns>A dictionary of time series, the key represents the calendar year in which each hydrological year starts</returns>
    ''' <remarks></remarks>
    Public Function SplitHydroYears(Optional startMonth As Integer = 11) As Dictionary(Of Integer, TimeSeries)

        Dim ts As TimeSeries
        Dim year, year_start, year_end As Integer
        Dim tsDict As New Dictionary(Of Integer, TimeSeries)

        If startMonth < 1 Or startMonth > 12 Then
            Throw New ArgumentException("startMonth must be an integer representing the month between 1 and 12")
        End If

        'determine first and last hydrological year
        If Me.StartDate < New DateTime(Me.StartDate.Year, startMonth, 1) Then
            year_start = Me.StartDate.Year - 1
        Else
            year_start = Me.StartDate.Year
        End If

        If Me.EndDate >= New DateTime(Me.EndDate.Year, startMonth, 1) Then
            year_end = Me.EndDate.Year
        Else
            year_end = Me.EndDate.Year - 1
        End If

        'cut the series into years
        For year = year_start To year_end
            ts = Me.Clone()
            Dim t_start, t_end As DateTime
            t_start = New DateTime(year, startMonth, 1)
            'Do not go beyond the year 9999
            If year < 9999 Then
                t_end = New DateTime(year + 1, startMonth, 1)
            Else
                t_end = Me.EndDate
            End If
            If ts.Interpretation = InterpretationEnum.BlockRight Then
                'if interpretation is block right, use the last date before the end of the year
                If Me.Dates.Contains(t_end) Then
                    t_end = ts.Dates(ts.IndexOfDate(t_end) - 1)
                Else
                    t_end = ts.Dates(ts.IndexOfDate(t_end, TimeDirection.Before))
                End If
            End If
            ts.Cut(t_start, t_end)
            ts.Title &= $" ({year})"
            tsDict.Add(year, ts)
        Next

        Return tsDict

    End Function

    ''' <summary>
    ''' Returns a new, equidistant time series with the specified timestep
    ''' </summary>
    ''' <param name="timesteptype">The type of timestep interval</param>
    ''' <param name="timestepinterval">The number of intervals that make up each timestep</param>
    ''' <param name="startdate">Start date for the new time series</param>
    ''' <param name="outputInterpretation">Optional interpretation to use for the output timeseries. 
    ''' If not defined, the original interpretation is preserved, if possible</param>
    ''' <remarks>
    ''' Output interpretation Instantaneous is not implemented,
    ''' because it would require considering a time interval 0.5 * dt to the left and right of the output timestamps
    ''' which causes issues with timestep intervals of type Month (and also headaches).
    ''' Time steps that are not fully within the time series are not included in the result time series.
    ''' Time steps within which at least one node contain NaN values also get the value NaN.
    ''' 
    ''' TODO:
    '''     Support more interpretations
    '''     Allow handling NaN values (e.g. as 0)?
    ''' </remarks>
    ''' <returns>The new time series</returns>
    Public Function ChangeTimestep(timesteptype As TimeStepTypeEnum,
                                   timestepinterval As Integer,
                                   startdate As DateTime,
                                   Optional outputInterpretation As TimeSeries.InterpretationEnum = InterpretationEnum.Undefined) As TimeSeries

        Dim i, i_start As Integer
        Dim dt As TimeSpan
        Dim ts As TimeSeries
        Dim t, t_start, t_end As DateTime
        Dim value, value_intp, value_intp2, volume, cumval As Double
        Dim timestep_full, node_processed As Boolean

        'check input interpretation
        Select Case Me.Interpretation
            Case InterpretationEnum.Instantaneous,
                 InterpretationEnum.BlockLeft,
                 InterpretationEnum.BlockRight,
                 InterpretationEnum.CumulativePerTimestep
                'everything OK
            Case Else
                Throw New NotImplementedException($"Changing the timestep of a time series with interpretation {[Enum].GetName(GetType(InterpretationEnum), Me.Interpretation)} is currently not implemented!")
        End Select

        'set output interpretation to input interpretation if undefined
        If outputInterpretation = InterpretationEnum.Undefined Then
            outputInterpretation = Me.Interpretation
        End If

        'check output interpretation
        Select Case outputInterpretation
            Case InterpretationEnum.BlockLeft,
                 InterpretationEnum.BlockRight,
                 InterpretationEnum.CumulativePerTimestep
                'everything OK
            Case Else
                Throw New NotImplementedException($"Changing the timestep of a time series to an output interpretation {[Enum].GetName(GetType(InterpretationEnum), outputInterpretation)} is currently not implemented!")
        End Select

        'create a new timeseries
        ts = New TimeSeries(Me.Title)
        ts.Unit = Me.Unit
        ts.Interpretation = outputInterpretation
        ts.Metadata = Me.Metadata
        'append timestep description to title
        Dim timesteptypeString As String = [Enum].GetName(GetType(TimeStepTypeEnum), timesteptype).ToLower()
        If timestepinterval > 1 Then
            timesteptypeString &= "s"
        End If
        ts.Title &= $" ({timestepinterval} {timesteptypeString})"

        'find first timestep that is fully within the old time series
        t_start = startdate
        Do While True

            t_end = TimeSeries.AddTimeInterval(t_start, timesteptype, timestepinterval)

            If t_start >= Me.StartDate Then
                Exit Do
            Else
                'advance timestep
                t_start = t_end
            End If
        Loop

        'Find first node of the timeseries to begin with
        For i = 0 To Me.Length - 1
            If Me.Dates(i) >= t_start Then
                Select Case Me.Interpretation
                    Case InterpretationEnum.BlockLeft
                        i_start = i
                    Case InterpretationEnum.Instantaneous,
                         InterpretationEnum.BlockRight,
                         InterpretationEnum.CumulativePerTimestep
                        i_start = Math.Max(i - 1, 0)
                End Select
                Exit For
            End If
        Next

        'if output interpretation is CumulativePerTimestep, we need to add an initial node with value 0 at the start date
        If outputInterpretation = InterpretationEnum.CumulativePerTimestep Then
            ts.AddNode(startdate, 0.0)
        End If

        volume = 0.0
        cumval = 0.0
        timestep_full = False

        'Loop over nodes
        For i = i_start To Me.Length - 2

            'abort once a time step is not fully within the time series anymore
            If t_end > Me.EndDate Then
                Exit For
            End If

            node_processed = False
            Do Until node_processed

                If Me.Dates(i) >= t_start Then
                    If Me.Dates(i + 1) > t_end Then
                        'add partial value between Me.Dates(i) and t_end
                        value_intp = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_end, Me.Interpretation)
                        volume += TimeSeries.IntegrateVolume(Me.Dates(i), Me.Values(i), t_end, value_intp, Me.Interpretation)
                        cumval += value_intp
                    Else
                        'add whole value
                        volume += TimeSeries.IntegrateVolume(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), Me.Interpretation)
                        cumval += Me.Values(i + 1)

                    End If
                Else
                    If Me.Dates(i + 1) >= t_end Then
                        'add partial value between t_start and t_end
                        value_intp = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_start, Me.Interpretation)
                        value_intp2 = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_end, Me.Interpretation)
                        volume += TimeSeries.IntegrateVolume(t_start, value_intp, t_end, value_intp2, Me.Interpretation)
                        cumval += value_intp2 - value_intp
                    Else
                        'add partial value between t_start and Me.Dates(i + 1)
                        value_intp = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_start, Me.Interpretation)
                        volume += TimeSeries.IntegrateVolume(t_start, value_intp, Me.Dates(i + 1), Me.Values(i + 1), Me.Interpretation)
                        cumval += Me.Values(i + 1) - value_intp
                    End If
                End If

                If Me.Dates(i + 1) <= t_end Then
                    node_processed = True
                End If

                If Me.Dates(i + 1) >= t_end Then
                    timestep_full = True
                End If

                If timestep_full Then
                    'set final value depending on interpretation
                    dt = t_end - t_start
                    Select Case outputInterpretation
                        Case InterpretationEnum.Instantaneous,
                             InterpretationEnum.BlockLeft,
                             InterpretationEnum.BlockRight
                            value = volume / dt.TotalSeconds
                        Case InterpretationEnum.CumulativePerTimestep
                            value = cumval
                    End Select
                    'set timestamp depending on interpretation
                    Select Case outputInterpretation
                        Case InterpretationEnum.Instantaneous,
                             InterpretationEnum.BlockRight
                            t = t_start
                        Case InterpretationEnum.BlockLeft,
                             InterpretationEnum.CumulativePerTimestep
                            t = t_end
                    End Select
                    'store new node
                    ts.AddNode(t, value)
                    'advance to next timestep
                    t_start = t_end
                    t_end = TimeSeries.AddTimeInterval(t_start, timesteptype, timestepinterval)
                    volume = 0.0
                    cumval = 0.0
                    timestep_full = False
                End If
            Loop

        Next

        Return ts

    End Function

    ''' <summary>
    ''' Calculate a metric from the time series' values
    ''' </summary>
    ''' <param name="WertTyp">MaxWert, MinWert, Average, AnfWert, EndWert, Summe</param>
    ''' <returns>the calculated metric</returns>
    ''' <remarks>Obsolete, kept for backwards compatibility with BlueM.Opt</remarks>
    Public Function getWert(WertTyp As String) As Double
        Dim Wert As Double

        Select Case WertTyp

            Case "MaxWert"
                Wert = Me.Maximum

            Case "MinWert"
                Wert = Me.Minimum

            Case "Average"
                Wert = Me.Average

            Case "AnfWert"
                Wert = Me.FirstValue

            Case "EndWert"
                Wert = Me.LastValue

            Case "Summe"
                Wert = Me.Sum

            Case Else
                Throw New Exception($"Der Werttyp '{WertTyp}' wird nicht unterstützt!")

        End Select

        Return Wert

    End Function

    ''' <summary>
    ''' Creates a copy of the time series in which all nodes with specified error values are converted to NaN
    ''' </summary>
    ''' <param name="errorvalues">array of error values to ignore</param>
    ''' <returns>the cleaned time series</returns>
    ''' <remarks>a tolerance of 0.0001 is used to compare series values to errorvalues</remarks>
    Public Function convertErrorValues(ParamArray errorvalues() As Double) As TimeSeries

        Const tolerance As Double = 0.0001
        Dim isErrorvalue As Boolean
        Dim errorCount As Integer
        Dim tsConverted As TimeSeries

        'Instantiate a new series
        tsConverted = New TimeSeries(Me.Title)

        'copy metadata
        tsConverted.Unit = Me.Unit
        tsConverted.Interpretation = Me.Interpretation
        tsConverted.Metadata = Me.Metadata

        Log.AddLogEntry(Log.levels.info, $"Converting error values from series {Me.Title}...")

        errorCount = 0
        For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
            'Check if is error value
            isErrorvalue = False
            For Each errvalue As Double In errorvalues
                If Math.Abs(node.Value - errvalue) < tolerance Then
                    isErrorvalue = True
                    Exit For
                End If
            Next
            If isErrorvalue Then
                'convert the node to NaN
                tsConverted.AddNode(node.Key, Double.NaN)
                errorCount += 1
                Call Log.AddLogEntry(Log.levels.info, $"Converting node at {node.Key} with value {node.Value} to NaN")
            Else
                'copy the node
                tsConverted.AddNode(node.Key, node.Value)
            End If
        Next

        'Log
        If errorCount > 0 Then
            Call Log.AddLogEntry(Log.levels.info, $"{Me.Title}: {errorCount} nodes were coverted to NaN!")
        End If

        Return tsConverted

    End Function

    ''' <summary>
    ''' Returns a copy of the time series without the nodes having NaN and Infinity values
    ''' </summary>
    ''' <returns>the cleaned time series</returns>
    Public Function removeNaNValues() As TimeSeries

        Dim nanCount As Integer
        Dim tsCleaned As TimeSeries

        'if no NaN nodes exist, return a clone
        If Me.NaNCount = 0 Then
            Return Me.Clone()
        End If

        'store number of NaN nodes
        nanCount = Me.NaNCount

        'Instantiate a new series
        tsCleaned = New TimeSeries(Me.Title)

        'copy metadata
        tsCleaned.Unit = Me.Unit
        tsCleaned.Interpretation = Me.Interpretation
        tsCleaned.Metadata = Me.Metadata.Copy()
        tsCleaned.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)

        'copy clean nodes
        tsCleaned._nodes = New SortedList(Of DateTime, Double)(Me.NodesClean)

        'Log
        If nanCount > 0 Then
            Call Log.AddLogEntry(Log.levels.info, $"{Me.Title}: {nanCount} NaN and Infinity nodes were removed!")
        End If

        Return tsCleaned

    End Function

#End Region 'Methods

#Region "Shared Members"

    ''' <summary>
    ''' Shared global ID which is incremented for each new Timeseries instance
    ''' </summary>
    Private Shared _globalId As Integer = 0

    ''' <summary>
    ''' Returns a new unique ID
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property getUniqueID As Integer
        Get
            TimeSeries._globalId += 1
            Return TimeSeries._globalId
        End Get
    End Property

    ''' <summary>
    ''' Returns a new DateTime offset from the given base DateTime by the specified interval
    ''' </summary>
    ''' <param name="t">Base DateTime from which to offset</param>
    ''' <param name="timesteptype">The type of interval to offset</param>
    ''' <param name="timestepinterval">The number of intervals to offset (can be negative in order to subtract)</param>
    ''' <returns>The offset DateTime</returns>
    ''' <remarks>
    ''' Does not check for exceeding DateTime.MaxValue or going below DateTime.MinValue.
    ''' Handles leap days and varying month lengths correctly when offsetting by years or months.
    ''' </remarks>
    Public Shared Function AddTimeInterval(t As DateTime, timesteptype As TimeStepTypeEnum, timestepinterval As Integer) As DateTime

        'TODO: handle not going above DateTime.MaxValue #68

        If timestepinterval = 0 Then
            Return t
        End If

        Select Case timesteptype
            Case TimeStepTypeEnum.Second
                t += TimeSpan.FromSeconds(timestepinterval)
            Case TimeStepTypeEnum.Minute
                t += TimeSpan.FromMinutes(timestepinterval)
            Case TimeStepTypeEnum.Hour
                t += TimeSpan.FromHours(timestepinterval)
            Case TimeStepTypeEnum.Day
                t += TimeSpan.FromDays(timestepinterval)
            Case TimeStepTypeEnum.Week
                t += TimeSpan.FromDays(7 * timestepinterval)
            Case TimeStepTypeEnum.Month
                t = t.AddMonths(timestepinterval)
            Case TimeStepTypeEnum.Year
                t = t.AddYears(timestepinterval)
            Case Else
                Throw New NotImplementedException($"TimeStepType {timesteptype} not implemented!")
        End Select

        Return t

    End Function

    ''' <summary>
    ''' Interpolates a value between two nodes while respecting the interpretation
    ''' </summary>
    ''' <param name="t1">First timestamp</param>
    ''' <param name="v1">First value</param>
    ''' <param name="t2">Second timestamp</param>
    ''' <param name="v2">Second value</param>
    ''' <param name="t">Timestamp at which to interpolate</param>
    ''' <param name="interpretation">Interpretation to use</param>
    ''' <returns>The interpolated value</returns>
    Public Shared Function InterpolateValue(t1 As DateTime, v1 As Double, t2 As DateTime, v2 As Double, t As DateTime, interpretation As InterpretationEnum) As Double

        Dim dt_total, dt_part As TimeSpan
        Dim value As Double

        If t < t1 Or t > t2 Then
            Throw New Exception("Timestamp to interpolate is not within range!")
        End If

        dt_total = t2 - t1
        dt_part = t - t1

        Select Case interpretation
            Case InterpretationEnum.Instantaneous
                value = (v2 - v1) / dt_total.TotalSeconds * dt_part.TotalSeconds + v1
            Case InterpretationEnum.BlockRight
                value = v1
            Case InterpretationEnum.BlockLeft
                value = v2
            Case InterpretationEnum.Cumulative
                value = (v2 - v1) / dt_total.TotalSeconds * dt_part.TotalSeconds + v1
            Case InterpretationEnum.CumulativePerTimestep
                value = v2 / dt_total.TotalSeconds * dt_part.TotalSeconds
            Case Else
                Throw New NotImplementedException($"Interpolation between nodes with interpretation {[Enum].GetName(GetType(InterpretationEnum), interpretation)} is currently not implemented!")
        End Select

        Return value

    End Function

    ''' <summary>
    ''' Integrates the volume between two nodes while respecting interpretation and unit
    ''' </summary>
    ''' <param name="t1">First timestamp</param>
    ''' <param name="v1">First value</param>
    ''' <param name="t2">Second timestamp</param>
    ''' <param name="v2">Second value</param>
    ''' <param name="interpretation">Interpretation to use</param>
    ''' <param name="unitTimeStepType">TimeStepType of the unit, default is <see cref="TimeStepTypeEnum.Second">per second</see></param>
    ''' <returns>The volume</returns>
    Public Shared Function IntegrateVolume(t1 As DateTime, v1 As Double, t2 As DateTime, v2 As Double, interpretation As InterpretationEnum, Optional unitTimeStepType As TimeStepTypeEnum = TimeStepTypeEnum.Second) As Double

        Dim value, volume As Double
        Dim dt As TimeSpan = t2 - t1

        'determine applicable value depending on interpretation
        Select Case interpretation
            Case InterpretationEnum.Instantaneous
                value = (v1 + v2) / 2
            Case InterpretationEnum.BlockRight
                value = v1
            Case InterpretationEnum.BlockLeft,
                 InterpretationEnum.CumulativePerTimestep
                value = v2
            Case InterpretationEnum.Cumulative
                value = v2 - v1
            Case Else
                Throw New NotImplementedException($"Integration between nodes with interpretation {[Enum].GetName(GetType(InterpretationEnum), interpretation)} is currently not implemented!")
        End Select

        If interpretation = InterpretationEnum.CumulativePerTimestep Then
            'value is by definition already the sum/volume, no need to consider the time span
            volume = value
        Else
            'mutiply value by time span to get volume
            Select Case unitTimeStepType
                Case TimeStepTypeEnum.Second
                    volume = value * dt.TotalSeconds
                Case TimeStepTypeEnum.Minute
                    volume = value * dt.TotalMinutes
                Case TimeStepTypeEnum.Hour
                    volume = value * dt.TotalHours
                Case TimeStepTypeEnum.Day
                    volume = value * dt.TotalDays
                Case Else
                    Throw New NotImplementedException($"Integration between nodes with unit time step type {[Enum].GetName(GetType(TimeStepTypeEnum), unitTimeStepType)} is currently not implemented!")
            End Select
        End If

        Return volume

    End Function

    ''' <summary>
    ''' Synchronizes two timeseries in-place by only keeping the common timestamps
    ''' </summary>
    ''' <param name="ts1">First timeseries</param>
    ''' <param name="ts2">Second timeseries</param>
    Public Shared Sub Synchronize(ByRef ts1 As TimeSeries, ByRef ts2 As TimeSeries)

        Dim t_common, t_diff As HashSet(Of DateTime)

        t_diff = ts1.Dates.ToHashSet()
        t_diff.SymmetricExceptWith(ts2.Dates)

        If t_diff.Count = 0 Then
            'nothing to do
            Exit Sub
        End If

        t_common = ts1.Dates.Intersect(ts2.Dates).ToHashSet()

        'switch depending on whether there are more common or more different timestamps
        If t_diff.Count < t_common.Count Then
            'remove the different timestamps from the node lists
            For Each t As DateTime In t_diff
                If ts1.Dates.Contains(t) Then
                    ts1.Nodes.Remove(t)
                End If
                If ts2.Dates.Contains(t) Then
                    ts2.Nodes.Remove(t)
                End If
            Next
        Else
            'create new node lists with only the common timestamps
            Dim ts1_nodes As New SortedList(Of DateTime, Double)()
            Dim ts2_nodes As New SortedList(Of DateTime, Double)()
            For Each t As DateTime In t_common
                ts1_nodes.Add(t, ts1.Nodes(t))
                ts2_nodes.Add(t, ts2.Nodes(t))
            Next
            ts1._nodes = ts1_nodes
            ts2._nodes = ts2_nodes
        End If

        ts1._nodesCleaned = Nothing
        ts2._nodesCleaned = Nothing

    End Sub

    ''' <summary>
    ''' Returns a new time series with all timestamps shifted by the specified interval
    ''' </summary>
    ''' <param name="timestepType">The type of interval to offset</param>
    ''' <param name="timestepInterval">The number of intervals to offset (can be negative in order to subtract)</param>
    ''' <returns>The shifted timeseries</returns>
    ''' <remarks>
    ''' Shifting by months may cause the loss of data points due to varying month lengths.
    ''' Shifting by years may cause the loss of data points due to leap days.
    ''' </remarks>
    Public Function ShiftTime(timestepInterval As Integer, timestepType As TimeStepTypeEnum) As TimeSeries

        Dim ts As TimeSeries = Me.Clone()
        ts.Nodes.Clear()

        Dim calendar As Calendar = CultureInfo.CurrentCulture.Calendar
        Dim t_new As DateTime
        For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
            If timestepType = TimeStepTypeEnum.Year Then
                'skip leap day if target year is not a leap year
                Dim targetYear As Integer = node.Key.Year + timestepInterval
                If calendar.IsLeapDay(node.Key.Year, node.Key.Month, node.Key.Day) AndAlso Not calendar.IsLeapYear(targetYear) Then
                    Log.AddLogEntry(Log.levels.warning, $"Skipping timestamp on leap day {node.Key} when shifting time series '{Me.Title}' by years!")
                    Continue For
                End If
            ElseIf timestepType = TimeStepTypeEnum.Month Then
                'skip day 29, 30, 31 if target month does not have that many days
                Dim targetYear As Integer = node.Key.AddMonths(timestepInterval).Year
                Dim targetMonth As Integer = node.Key.AddMonths(timestepInterval).Month
                Dim daysInTargetMonth As Integer = DateTime.DaysInMonth(targetYear, targetMonth)
                If node.Key.Day > daysInTargetMonth Then
                    Log.AddLogEntry(Log.levels.warning, $"Skipping timestamp on end of month {node.Key} when shifting time series '{Me.Title}' by months!")
                    Continue For
                End If
            End If
            t_new = TimeSeries.AddTimeInterval(node.Key, timestepType, timestepInterval)
            ts.AddNode(t_new, node.Value)
        Next

        'append timeshift description to title
        Dim timesteptypeString As String = [Enum].GetName(GetType(TimeStepTypeEnum), timestepType).ToLower()
        Dim timestepIntervalString As String = timestepInterval.ToString()
        If timestepInterval > 0 Then
            timestepIntervalString = "+" & timestepIntervalString
        End If
        If timestepInterval > 1 Then
            timesteptypeString &= "s"
        End If
        ts.Title &= $" ({timestepIntervalString} {timesteptypeString})"

        Return ts
    End Function

#End Region

End Class
