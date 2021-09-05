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
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property Objekt() As String
        Get
            Return _Objekt
        End Get
        Set(ByVal value As String)
            _Objekt = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    ''' <summary>
    ''' The time series' interpretation
    ''' </summary>
    Public Property Interpretation() As InterpretationEnum
        Get
            Return Me._Interpretation
        End Get
        Set(ByVal value As InterpretationEnum)
            Me._Interpretation = value
        End Set
    End Property

    ''' <summary>
    ''' The time series' nodes
    ''' </summary>
    Public ReadOnly Property Nodes() As SortedList(Of DateTime, Double)
        Get
            Return _nodes
        End Get
    End Property

    ''' <summary>
    ''' The time series' nodes without NaN and Infinity values
    ''' </summary>
    ''' <remarks>The cleaned nodes are cached and only created once per instance</remarks>
    Public ReadOnly Property NodesClean() As SortedList(Of DateTime, Double)
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
        Set(ByVal value As Metadata)
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
                    text &= kvp.Key & ": " & kvp.Value & ", "
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
    ''' The unit of the the time series' values
    ''' </summary>
    Public Property Unit() As String
        Get
            Return _unit
        End Get
        Set(ByVal value As String)
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
            Dim max As Double
            max = Double.MinValue
            For Each value As Double In Me.NodesClean.Values
                If (value > max) Then
                    max = value
                End If
            Next
            Return max
        End Get
    End Property

    ''' <summary>
    ''' Returns the maximum value of the time series within a defined time period
    ''' </summary>
    Public Overloads ReadOnly Property Maximum(ByVal startdate As DateTime, ByVal enddate As DateTime) As Double
        Get
            Dim max As Double
            max = Double.MinValue
            For Each kvp As KeyValuePair(Of DateTime, Double) In Me.NodesClean
                If kvp.Key < startdate Then
                    Continue For
                ElseIf kvp.Key > enddate Then
                    Exit For
                End If
                If (kvp.Value > max) Then
                    max = kvp.Value
                End If
            Next
            Return max
        End Get
    End Property

    ''' <summary>
    ''' Returns the minimum value of the time series
    ''' </summary>
    Public Overloads ReadOnly Property Minimum() As Double
        Get
            Dim min As Double
            min = Double.MaxValue
            For Each value As Double In Me.NodesClean.Values
                If (value < min) Then
                    min = value
                End If
            Next
            Return min
        End Get
    End Property

    ''' <summary>
    ''' Returns the minimum value of the time series within a defined time period
    ''' </summary>
    Public Overloads ReadOnly Property Minimum(ByVal startdate As DateTime, ByVal enddate As DateTime) As Double
        Get
            Dim min As Double
            min = Double.MaxValue
            For Each kvp As KeyValuePair(Of DateTime, Double) In Me.NodesClean
                If kvp.Key < startdate Then
                    Continue For
                ElseIf kvp.Key > enddate Then
                    Exit For
                End If
                If (kvp.Value < min) Then
                    min = kvp.Value
                End If
            Next
            Return min
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
    ''' <remarks>If the unit is per second (ends with "/s"), values are integrated over time using a linear interpretation. 
    ''' Otherwise, NaN is returned.</remarks>
    Public ReadOnly Property Volume() As Double
        Get
            Dim v0, v1, vol As Double
            Dim t0, t1 As DateTime
            Dim dt As TimeSpan

            If Me.Unit.ToLower.EndsWith("/s") Then
                Log.AddLogEntry(Log.levels.debug, "Calculating volume by integrating over time for series " & Me.Title)
                t0 = Me.NodesClean.First().Key
                v0 = Me.NodesClean.First().Value
                vol = 0.0
                For Each node As KeyValuePair(Of Date, Double) In Me.NodesClean
                    t1 = node.Key
                    v1 = node.Value
                    If t1 > t0 Then 'start at the second node
                        dt = t1 - t0
                        vol += (v0 + v1) / 2 * dt.TotalSeconds 'linear interpretation!
                    End If
                    t0 = t1
                    v0 = v1
                Next
            Else
                vol = Double.NaN
            End If

            Return vol
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
    Public Sub New(ByVal title As String)
        Me._id = TimeSeries.getUniqueID()
        Me._metadata = New Metadata()
        Me._title = title
        Me._unit = "-"
        Me._Objekt = "-"
        Me._Type = "-"
        Me._Interpretation = InterpretationEnum.Undefined
        Me.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.Undefined)
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
    Public Function Clone() As TimeSeries
        Dim target As New TimeSeries(Me.Title)
        target.Unit = Me.Unit
        target.Objekt = Me.Objekt
        target.Type = Me.Type
        target._nodes = New SortedList(Of DateTime, Double)(Me._nodes)
        target.Metadata = Me.Metadata
        target._Interpretation = Me.Interpretation
        Return target
    End Function

    ''' <summary>
    ''' Adds a node to the time series
    ''' </summary>
    ''' <param name="_date">Date</param>
    ''' <param name="_value">Value</param>
    ''' <remarks>If the given date already exists, the new node is discarded and a warning is written to the log</remarks>
    Public Sub AddNode(ByVal _date As DateTime, ByVal _value As Double)
        If (Me.Nodes.ContainsKey(_date)) Then
            Log.AddLogEntry(Log.levels.warning, "Duplicate data point at " & _date.ToString(Helpers.DefaultDateFormat) &
                            ": Value of " & _value.ToString() & " will be discarded. Existing value: " & Me.Nodes(_date).ToString())
            Exit Sub
        End If
        Me._nodes.Add(_date, _value)
    End Sub

    ''' <summary>
    ''' Cut a time series using start and end dates
    ''' </summary>
    ''' <param name="_start">start date</param>
    ''' <param name="_end">end date</param>
    ''' <remarks>Removes all nodes before the start date and after the end date</remarks>
    Public Overloads Sub Cut(ByVal _start As DateTime, ByVal _end As DateTime)

        Dim i, lengthOld, lengthNew As Integer
        Dim iStart, iEnd As Integer
        Dim newNodes As SortedList(Of DateTime, Double)

        If (Me.StartDate < _start Or Me.EndDate > _end) Then

            Me.Title &= " (cut)"

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
            Call Log.AddLogEntry(Log.levels.info, Me.Title & ": cut from " & lengthOld.ToString() & " to " & lengthNew.ToString() & " data points.")

        End If

    End Sub

    ''' <summary>
    ''' Cut the time series to the timespan of another time series
    ''' </summary>
    ''' <param name="series2">the time series to whose timespan the time series should be cut</param>
    ''' <remarks></remarks>
    Public Overloads Sub Cut(ByVal series2 As TimeSeries)

        If (Me.StartDate < series2.StartDate Or Me.EndDate > series2.EndDate) Then
            Call Me.Cut(series2.StartDate, series2.EndDate)
        End If

    End Sub

    ''' <summary>
    ''' Appends a time series
    ''' </summary>
    ''' <param name="series2">series to be appended</param>
    ''' <remarks>in the case of an overlap between the two series, 
    ''' the nodes of the appended series within the overlap are discarded</remarks>
    Public Sub Append(ByVal series2 As TimeSeries)

        Dim startDate, endDate As DateTime

        If series2.EndDate <= Me.EndDate And series2.StartDate >= Me.StartDate Then
            'series2 does not extend beyond this series, so nothing to do
            Log.AddLogEntry(Log.levels.warning, "Series '" & series2.Title & "' does not extend beyond series '" & Me.Title & "' so nothing can be appended!")
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
    ''' Splits a time series into individual series for each hydrological years
    ''' </summary>
    ''' <returns>A dictionary of time series, the key represents the year</returns>
    ''' <remarks>The start of the hydrological year is defined as November 1st of the previous year</remarks>
    Public Function SplitHydroYears() As Dictionary(Of Integer, TimeSeries)

        Dim ts As TimeSeries
        Dim year, year_start, year_end As Integer
        Dim tsDict As New Dictionary(Of Integer, TimeSeries)

        'determine first and last hydrological year
        If Me.StartDate < New DateTime(Me.StartDate.Year, 11, 1) Then
            year_start = Me.StartDate.Year
        Else
            year_start = Me.StartDate.Year + 1
        End If

        If Me.EndDate > New DateTime(Me.EndDate.Year, 11, 1) Then
            year_end = Me.EndDate.Year + 1
        Else
            year_end = Me.EndDate.Year
        End If

        'cut the series
        For year = year_start To year_end
            ts = Me.Clone()
            ts.Cut(New DateTime(year - 1, 11, 1), New DateTime(year, 10, 31, 23, 59, 59))
            ts.Title &= " (" & year.ToString() & ")"
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
    ''' <remarks>
    ''' Time steps that are not fully within the time series are not included in the result time series.
    ''' Time steps within which at least one node contain NaN values also get the value NaN.
    ''' Interpretation of output time series is kept except for Instantaneous, where output is BlockRight
    '''     because keeping the interpretation would require a time step 0.5 * dt to the left and right of the output timestamps
    '''     which causes issues with timestep intervals of type Month.
    ''' TODO:
    '''     Support more interpretations
    '''     Allow handling NaN values (e.g. as 0)?
    ''' </remarks>
    ''' <returns>The new time series</returns>
    Public Function ChangeTimestep(timesteptype As TimeStepTypeEnum, timestepinterval As Integer, startdate As DateTime) As TimeSeries

        Dim i, i_start As Integer
        Dim dt_new, dt_old, dt_part As TimeSpan
        Dim ts As TimeSeries
        Dim t, t_start, t_end As DateTime
        Dim value, value_intp, value_intp2, volume, sum As Double
        Dim timestep_full, node_processed As Boolean

        Select Case Me.Interpretation
            Case InterpretationEnum.Instantaneous,
                 InterpretationEnum.BlockRight,
                 InterpretationEnum.CumulativePerTimestep
                'everything OK
            Case Else
                Throw New NotImplementedException(String.Format("Changing the timestep of a time series with interpretation {0} is currently not implemented!", [Enum].GetName(GetType(InterpretationEnum), Me.Interpretation)))
        End Select

        'create a new timeseries
        ts = New TimeSeries(Me.Title)
        ts.Unit = Me.Unit
        ts.Interpretation = Me.Interpretation
        ts.Metadata = Me.Metadata
        ts.Title &= " (" & timestepinterval & " " & [Enum].GetName(GetType(TimeStepTypeEnum), timesteptype) & ")"

        'find first timestep that is fully within the time series
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
                    Case InterpretationEnum.BlockLeft,
                     InterpretationEnum.Cumulative,
                     InterpretationEnum.CumulativePerTimestep
                        i_start = i
                    Case InterpretationEnum.Instantaneous,
                     InterpretationEnum.BlockRight
                        i_start = Math.Max(i - 1, 0)
                End Select
                Exit For
            End If
        Next

        volume = 0.0
        sum = 0.0
        timestep_full = False

        'Loop over nodes
        For i = i_start To Me.Length - 2

            'abort once a time step is not fully within the time series anymore
            If t_end > Me.EndDate Then
                Exit For
            End If

            dt_old = Me.Dates(i + 1) - Me.Dates(i)

            node_processed = False
            Do Until node_processed

                dt_new = t_end - t_start

                If Me.Dates(i) >= t_start Then
                    If Me.Dates(i + 1) >= t_end Then
                        'add partial value before t_end
                        dt_part = t_end - Me.Dates(i)
                        value_intp = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_end, Me.Interpretation)
                        volume += (Me.Values(i) + value_intp) / 2 * dt_part.TotalSeconds
                        sum += value_intp

                        timestep_full = True
                    Else
                        'add whole value
                        volume += (Me.Values(i) + Me.Values(i + 1)) / 2 * dt_old.TotalSeconds
                        sum += Me.Values(i + 1)

                    End If
                Else
                    If Me.Dates(i + 1) >= t_end Then
                        'add partial value between t_start and t_end
                        value_intp = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_start, Me.Interpretation)
                        value_intp2 = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_end, Me.Interpretation)
                        volume += (value_intp + value_intp2) / 2 * dt_new.TotalSeconds
                        sum += value_intp2 - value_intp

                        timestep_full = True
                    Else
                        'add partial value after t_start
                        dt_part = Me.Dates(i + 1) - t_start
                        value_intp = TimeSeries.InterpolateValue(Me.Dates(i), Me.Values(i), Me.Dates(i + 1), Me.Values(i + 1), t_start, Me.Interpretation)
                        volume += (Me.Values(i) + value_intp) / 2 * dt_part.TotalSeconds
                        sum += value_intp

                    End If
                End If

                If Me.Dates(i + 1) <= t_end Then
                    node_processed = True
                End If

                If timestep_full Then
                    'set final value depending on interpretation
                    Select Case Me.Interpretation
                        Case InterpretationEnum.Instantaneous,
                             InterpretationEnum.BlockRight
                            value = volume / dt_new.TotalSeconds
                        Case InterpretationEnum.CumulativePerTimestep
                            value = sum
                        Case InterpretationEnum.BlockLeft,
                             InterpretationEnum.Cumulative
                            'TODO
                    End Select
                    'set timestamp depending on interpretation
                    Select Case Me.Interpretation
                        Case InterpretationEnum.Instantaneous,
                             InterpretationEnum.BlockRight
                            t = t_start
                        Case InterpretationEnum.BlockLeft,
                             InterpretationEnum.Cumulative,
                             InterpretationEnum.CumulativePerTimestep
                            t = t_end
                    End Select
                    ts.AddNode(t, value)
                    volume = 0.0
                    sum = 0.0
                    'advance to next timestep
                    t_start = t_end
                    t_end = TimeSeries.AddTimeInterval(t_start, timesteptype, timestepinterval)
                    timestep_full = False
                End If
            Loop

        Next

        'exception, see method remarks
        If Me.Interpretation = InterpretationEnum.Instantaneous Then
            ts.Interpretation = InterpretationEnum.BlockRight
        End If

        Return ts

    End Function

    ''' <summary>
    ''' Calculate a metric from the time series' values
    ''' </summary>
    ''' <param name="WertTyp">MaxWert, MinWert, Average, AnfWert, EndWert, Summe</param>
    ''' <returns>the calculated metric</returns>
    ''' <remarks>Obsolete, kept for backwards compatibility with BlueM.Opt</remarks>
    Public Function getWert(ByVal WertTyp As String) As Double
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
                Throw New Exception("Der Werttyp '" & WertTyp & "' wird nicht unterstützt!")

        End Select

        Return Wert

    End Function

    ''' <summary>
    ''' Erstellt eine neue äquidistante Zeitreihe, neue Stützstellen kriegen den Wert 0
    ''' </summary>
    ''' <param name="Soll_dT">Sollzeitschritt (in Minuten)</param>      
    Public Function getKontiZRE(ByVal Soll_dT As Integer) As TimeSeries

        Dim i As Integer
        Dim intloop As Integer
        Dim Ist_dT As Integer
        Dim AnzZusWerte As Integer
        Dim SumZusWerte As Long

        Dim OutZR As New TimeSeries("Konti_" & Me.Title)
        OutZR.Unit = Me.Unit

        SumZusWerte = 0
        For i = 0 To Me.Length - 2

            AnzZusWerte = 0
            Ist_dT = DateDiff(DateInterval.Minute, Me.Dates(i), Me.Dates(i + 1))

            If (Ist_dT - Soll_dT > 0) Then
                AnzZusWerte = (Ist_dT / Soll_dT) - 1
                SumZusWerte = SumZusWerte + AnzZusWerte
                OutZR.AddNode(Me.Dates(i), Me.Values(i))
                For intloop = 1 To AnzZusWerte
                    OutZR.AddNode(Dates(i).AddMinutes(intloop * Soll_dT), 0.0)
                Next
            Else
                OutZR.AddNode(Me.Dates(i), Me.Values(i))
            End If
        Next

        'letzten Wert schreiben
        OutZR.AddNode(Me.Dates(i), Me.Values(i))

        Return OutZR

    End Function

    ''' <summary>
    ''' Erstellt eine neue äquidistante Zeitreihe, neue Stützstellen kriegen aus original Zeitreihe konvertierten Wert, geignet für Massenbezogenen Zeitreihen
    ''' </summary>
    ''' <param name="Soll_dT">Sollzeitschritt (in Minuten)</param>      
    Public Function getKontiZRE2(ByVal Soll_dT As Integer) As TimeSeries

        Dim i, j As Integer
        Dim intloop As Integer
        Dim n_dT As Integer
        Dim NewNodes As Integer
        Dim newValue As Double
        Dim sumValues As Double

        Dim TempZR As New TimeSeries("Temp_" & Me.Title)
        Dim OutZR As New TimeSeries("Konti_" & Me.Title)
        OutZR.Unit = Me.Unit

        'Zuerst wird eine Zeitreihe auf der Basis von Minutenwerten erstellt (kleinst mögliche Einheit)
        For i = 0 To Me.Length - 2

            NewNodes = 0
            n_dT = DateDiff(DateInterval.Minute, Me.Dates(i), Me.Dates(i + 1))
            NewNodes = n_dT

            If NewNodes > 1 Then
                newValue = Me.Values(i) / NewNodes
                TempZR.AddNode(Me.Dates(i), newValue)
                For intloop = 1 To NewNodes - 1
                    TempZR.AddNode(Dates(i).AddMinutes(intloop), newValue)
                Next
            Else
                TempZR.AddNode(Me.Dates(i), Me.Values(i))
            End If
        Next

        TempZR.AddNode(Me.Dates(i), Me.Values(i))


        'Zeitreihe mit neuer Schrittweite wird generiert

        'Die Zeitreihe sollte mindestens einen Sollzeitschritt umfassen
        If TempZR.Length < Soll_dT Then
            Throw New Exception("Die Zeitreihe umfasst nicht genug Werte für die definierte Zeitschrittlänge")
        End If
        'Abarbeiten aller Werte die innerhalb ganzer Sollzeitschritte liegen.
        i = 0
        Do While i <= TempZR.Length - Soll_dT
            j = 0
            sumValues = 0
            For j = 0 To Soll_dT - 1
                sumValues += TempZR.Values(i + j)
            Next
            OutZR.AddNode(TempZR.Dates(i), sumValues)
            i += Soll_dT
        Loop

        'Aufaddieren des Rests
        If i Mod 5 <> 0 Then
            sumValues = 0
            For j = i To TempZR.Length - 1
                sumValues += TempZR.Values(j)
            Next

            'letzten Wert schreiben
            OutZR.AddNode(TempZR.Dates(i), sumValues)

        End If

        Return OutZR


    End Function

    ''' <summary>
    ''' Erstellt eine neue äquidistante Zeitreihe, neue Stützstellen kriegen aus original Zeitreihe konvertierten Wert, geignet für zeitabhängige Zeitreihen
    ''' </summary>
    ''' <param name="Soll_dT">Sollzeitschritt (in Minuten)</param>      
    Public Function getKontiZRE3(ByVal Soll_dT As Integer) As TimeSeries

        Dim i As Integer
        Dim intloop As Integer
        Dim n_dT As Integer
        Dim NewNodes As Integer
        Dim newValue As Double

        Dim TempZR As New TimeSeries("Temp_" & Me.Title)
        Dim OutZR As New TimeSeries("Konti_" & Me.Title)
        OutZR.Unit = Me.Unit

        'Zuerst wird eine Zeitreihe auf der Basis von Minutenwerten erstellt (kleinst mögliche Einheit)
        For i = 0 To Me.Length - 2

            NewNodes = 0
            n_dT = DateDiff(DateInterval.Minute, Me.Dates(i), Me.Dates(i + 1))
            NewNodes = n_dT

            If NewNodes > 1 Then
                newValue = Me.Values(i)
                TempZR.AddNode(Me.Dates(i), newValue)
                For intloop = 1 To NewNodes - 1
                    TempZR.AddNode(Dates(i).AddMinutes(intloop), newValue)
                Next
            Else
                TempZR.AddNode(Me.Dates(i), Me.Values(i))
            End If
        Next

        TempZR.AddNode(Me.Dates(i), Me.Values(i))


        'Zeitreihe mit neuer Schrittweite wird generiert

        'Die Zeitreihe sollte mindestens einen Sollzeitschritt umfassen
        If TempZR.Length < Soll_dT Then
            Throw New Exception("Die Zeitreihe umfasst nicht genug Werte für die definierte Zeitschrittlänge")
        End If
        'Abarbeiten aller Werte die innerhalb ganzer Sollzeitschritte liegen.
        i = 0
        Do While i <= TempZR.Length - Soll_dT
            'j = 0
            'sumValues = 0
            'For j = 0 To Soll_dT - 1
            '    sumValues += TempZR.YWerte(i + j)
            'Next
            'newValue = sumValues / Soll_dT
            'OutZR.AddNode(TempZR.XWerte(i), newValue)
            'i += Soll_dT
            OutZR.AddNode(TempZR.Dates(i), TempZR.Values(i))
            i += Soll_dT
        Loop

        'Aufaddieren des Rests
        If i Mod 5 <> 0 Then
            'sumValues = 0
            'For j = i To TempZR.Length - 1
            '    sumValues += TempZR.YWerte(j)
            'Next
            'newValue = sumValues / Soll_dT
            ''letzten Wert schreiben
            'OutZR.AddNode(TempZR.XWerte(i), sumValues)
            OutZR.AddNode(TempZR.Dates(i), TempZR.Values(i))
        End If

        Return OutZR


    End Function

    ''' <summary>
    ''' Creates a copy of the time series in which all nodes with specified error values are converted to NaN
    ''' </summary>
    ''' <param name="errorvalues">array of error values to ignore</param>
    ''' <returns>the cleaned time series</returns>
    ''' <remarks>a tolerance of 0.0001 is used to compare series values to errorvalues</remarks>
    Public Function convertErrorValues(ByVal ParamArray errorvalues() As Double) As TimeSeries

        Const tolerance As Double = 0.0001
        Dim isErrorvalue As Boolean
        Dim errorCount As Integer
        Dim tsConverted As TimeSeries

        'Instantiate a new series
        tsConverted = New TimeSeries(Me.Title)

        'copy metadata
        tsConverted.Unit = Me.Unit
        tsConverted.Interpretation = Me.Interpretation
        tsConverted.Objekt = Me.Objekt
        tsConverted.Type = Me.Type
        tsConverted.Metadata = Me.Metadata

        Log.AddLogEntry(Log.levels.info, String.Format("Converting error values from series {0}...", Me.Title))

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
                Call Log.AddLogEntry(Log.levels.info, String.Format("Converting node at {0} with value {1} to NaN", node.Key, node.Value))
            Else
                'copy the node
                tsConverted.AddNode(node.Key, node.Value)
            End If
        Next

        'Log
        If errorCount > 0 Then
            Call Log.AddLogEntry(Log.levels.info, Me.Title & ": " & errorCount.ToString() & " nodes were coverted to NaN!")
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

        Log.AddLogEntry(Log.levels.info, String.Format("Removing NaN values from series {0}...", Me.Title))

        'Instantiate a new series
        tsCleaned = New TimeSeries(Me.Title)

        'copy metadata
        tsCleaned.Unit = Me.Unit
        tsCleaned.Interpretation = Me.Interpretation
        tsCleaned.Objekt = Me.Objekt
        tsCleaned.Type = Me.Type
        tsCleaned.Metadata = Me.Metadata

        'copy clean nodes
        tsCleaned._nodes = Me.NodesClean

        'Log
        nanCount = Me.Length - tsCleaned.Length
        If nanCount > 0 Then
            Call Log.AddLogEntry(Log.levels.info, Me.Title & ": " & nanCount.ToString() & " nodes were removed!")
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
    ''' <param name="timestepinterval">The number of intervals to offset (can be negative in order to subract)</param>
    ''' <returns>The offset DateTime</returns>
    Public Shared Function AddTimeInterval(t As DateTime, timesteptype As TimeStepTypeEnum, timestepinterval As Integer) As DateTime

        Dim n As Integer

        If timestepinterval > 0 Then
            'add time interval
            For n = 0 To timestepinterval - 1
                Select Case timesteptype
                    Case TimeStepTypeEnum.Second
                        t += TimeSpan.FromSeconds(1)
                    Case TimeStepTypeEnum.Minute
                        t += TimeSpan.FromMinutes(1)
                    Case TimeStepTypeEnum.Hour
                        t += TimeSpan.FromHours(1)
                    Case TimeStepTypeEnum.Day
                        t += TimeSpan.FromDays(1)
                    Case TimeStepTypeEnum.Week
                        t += TimeSpan.FromDays(7)
                    Case TimeStepTypeEnum.Month
                        t += TimeSpan.FromDays(DateTime.DaysInMonth(t.Year, t.Month))
                    Case TimeStepTypeEnum.Year
                        t = New Date(t.Year + 1, t.Month, t.Day, t.Hour, t.Minute, t.Second)
                    Case Else
                        Throw New NotImplementedException("TimeStepType " & timesteptype & " not implemented!")
                End Select
            Next
        Else
            'subtract time interval
            For n = 0 To Math.Abs(timestepinterval) - 1
                Select Case timesteptype
                    Case TimeStepTypeEnum.Second
                        t -= TimeSpan.FromSeconds(1)
                    Case TimeStepTypeEnum.Minute
                        t -= TimeSpan.FromMinutes(1)
                    Case TimeStepTypeEnum.Hour
                        t -= TimeSpan.FromHours(1)
                    Case TimeStepTypeEnum.Day
                        t -= TimeSpan.FromDays(1)
                    Case TimeStepTypeEnum.Week
                        t -= TimeSpan.FromDays(7)
                    Case TimeStepTypeEnum.Month
                        t -= TimeSpan.FromDays(DateTime.DaysInMonth(t.Year, t.Month - 1))
                    Case TimeStepTypeEnum.Year
                        t = New Date(t.Year - 1, t.Month, t.Day, t.Hour, t.Minute, t.Second)
                    Case Else
                        Throw New NotImplementedException("TimeStepType " & timesteptype & " not implemented!")
                End Select
            Next
        End If

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
                Throw New NotImplementedException(String.Format("Interpolation between nodes with interpretation {0} is currently not implemented!", [Enum].GetName(GetType(InterpretationEnum), interpretation)))
        End Select

        Return value

    End Function

    ''' <summary>
    ''' Synchronizes two timeseries in-place by only keeping the common timestamnps
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

        t_common = ts1.Dates.ToHashSet()
        t_common.Intersect(ts2.Dates)

        'switch depending on whether there are more common or more different nodes
        If t_diff.Count < t_common.Count Then
            'remove the different nodes
            For Each t As DateTime In t_diff
                If ts1.Dates.Contains(t) Then
                    ts1.Nodes.Remove(t)
                End If
                If ts2.Dates.Contains(t) Then
                    ts2.Nodes.Remove(t)
                End If
            Next
        Else
            'clear all nodes and re-add only the common nodes
            Dim ts1_nodes As SortedList(Of DateTime, Double) = ts1.Nodes
            Dim ts2_nodes As SortedList(Of DateTime, Double) = ts2.Nodes
            ts1.Nodes.Clear()
            ts2.Nodes.Clear()
            For Each t As DateTime In t_common
                ts1.AddNode(t, ts1_nodes(t))
                ts2.AddNode(t, ts2_nodes(t))
            Next
        End If

        ts1._nodesCleaned = Nothing
        ts2._nodesCleaned = Nothing

    End Sub

#End Region

End Class
