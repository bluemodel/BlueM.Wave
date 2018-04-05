'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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

#Region "Members"

    Private _title As String
    Private _nodes As SortedList(Of DateTime, Double)
    Private _nodesCleaned As SortedList(Of DateTime, Double)
    Private _unit As String
    Private _Objekt As String
    Private _Type As String

#End Region 'Members

#Region "Properties"

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
    ''' The time series' nodes
    ''' </summary>
    Public ReadOnly Property Nodes() As SortedList(Of DateTime, Double)
        Get
            Return _nodes
        End Get
    End Property

    ''' <summary>
    ''' The time series' nodes, cleaned by omitting NaN and Infinity values
    ''' </summary>
    Private Property NodesCleaned() As SortedList(Of DateTime, Double)
        Get
            Return Me._nodesCleaned
        End Get
        Set(ByVal value As SortedList(Of DateTime, Double))
            Me._nodesCleaned = value
        End Set
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
    ''' The time series ' values
    ''' </summary>
    Public ReadOnly Property Values() As IList(Of Double)
        Get
            Return Me._nodes.Values
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
    ''' Returns the start date of the time series
    ''' </summary>
    Public ReadOnly Property StartDate() As DateTime
        Get
            Return Me._nodes.Keys(0)
        End Get
    End Property

    ''' <summary>
    ''' Returns the end date of the time series
    ''' </summary>
    Public ReadOnly Property EndDate() As DateTime
        Get
            Return Me._nodes.Keys(Me._nodes.Count - 1)
        End Get
    End Property

    ''' <summary>
    ''' Returns the maximum value of the time series
    ''' </summary>
    Public ReadOnly Property Maximum() As Double
        Get
            Dim max As Double
            max = Double.MinValue
            For Each value As Double In Me.Values
                If (value > max) Then
                    max = value
                End If
            Next
            Return max
        End Get
    End Property

    ''' <summary>
    ''' Returns the minimum value of the time series
    ''' </summary>
    Public ReadOnly Property Minimum() As Double
        Get
            Dim min As Double
            min = Double.MaxValue
            For Each value As Double In Me.Values
                If (value < min) Then
                    min = value
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
            For Each value As Double In Me.Values
                avg += value
            Next
            avg = avg / Me.Length
            Return avg
        End Get
    End Property

    ''' <summary>
    ''' Returns the first value of the time series
    ''' </summary>
    Public ReadOnly Property FirstValue() As Double
        Get
            Return Me.Values(0)
        End Get
    End Property

    ''' <summary>
    ''' Returns the last value of the time series
    ''' </summary>
    Public ReadOnly Property LastValue() As Double
        Get
            Return Me.Values(Me.Length - 1)
        End Get
    End Property

    ''' <summary>
    ''' Returns the sum of the time series' values
    ''' </summary>
    Public ReadOnly Property Sum() As Double
        Get
            Dim _sum As Double
            _sum = 0.0
            For Each value As Double In Me.Values
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
    ''' Otherwise, a simple summation is performed.</remarks>
    Public ReadOnly Property Volume() As Double
        Get
            Dim v0, v1, vol As Double
            Dim t0, t1 As DateTime
            Dim dt As TimeSpan

            vol = 0.0
            If Me.Unit.ToLower.EndsWith("/s") Then
                Log.AddLogEntry(Me.Title & ": calculating volume by integrating over time.")
                t0 = Me.StartDate
                v0 = Me.Nodes(t0)
                For Each node As KeyValuePair(Of Date, Double) In Me.Nodes
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
                'simple sum
                Log.AddLogEntry(Me.Title & ": calculating volume by simple summation.")
                vol = Me.Sum
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
    ''' <param name="title">Title of the times series</param>
    Public Sub New(ByVal title As String)
        Me._title = title
        Me._unit = "-"
        Me._Objekt = "-"
        Me._Type = "-"
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
            Log.AddLogEntry("WARNING: Duplicate data point at " & _date.ToString(Helpers.DefaultDateFormat) & _
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

            'Log 
            Call Log.AddLogEntry(Me.Title & ": cut from " & lengthOld.ToString() & " to " & lengthNew.ToString() & " data points.")

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
    ''' Creates a copy of the time series from which all nodes with values of NaN or +-Infinity have been removed.
    ''' </summary>
    ''' <param name="errorvalues">optional additional error values to ignore</param>
    ''' <returns>the cleaned time series</returns>
    ''' <remarks>a tolerance of 0.0001 is used to compare series values to errorvalues</remarks>
    Public Function getCleanZRE(ByVal ParamArray errorvalues() As Double) As TimeSeries

        Const tolerance As Double = 0.0001
        Dim isErrorvalue As Boolean
        Dim errorCount As Integer
        Dim cleanZRE As TimeSeries

        'Instantiate a new series
        cleanZRE = New TimeSeries(Me.Title)
        cleanZRE.Unit = Me.Unit
        cleanZRE.Objekt = Me.Objekt
        cleanZRE.Type = Me.Type

        'only get the cleaned nodes once
        'but if additional error values are provided, force a new clean
        If IsNothing(Me.NodesCleaned) Or Not IsNothing(errorvalues) Then
            Log.AddLogEntry(String.Format("Removing error values from series {0}...", Me.Title))

            Me.NodesCleaned = New SortedList(Of DateTime, Double)
            errorCount = 0

            For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
                'Check if is error value
                isErrorvalue = False
                If (Double.IsNaN(node.Value) Or _
                    Double.IsInfinity(node.Value)) Then
                    isErrorvalue = True
                Else
                    For Each errvalue As Double In errorvalues
                        If Math.Abs(node.Value - errvalue) < tolerance Then
                            isErrorvalue = True
                            Exit For
                        End If
                    Next
                End If

                If isErrorvalue Then
                    'skip the node
                    errorCount += 1
                    Call Log.AddLogEntry(String.Format("Removing node at {0} with value {1}", node.Key, node.Value))
                Else
                    'copy the node
                    NodesCleaned.Add(node.Key, node.Value)
                End If
            Next

            'Log
            If errorCount > 0 Then
                Call Log.AddLogEntry(Me.Title & ": " & errorCount.ToString() & " nodes were removed!")
            End If

        End If

        'assign clean nodes to new series
        cleanZRE._nodes = NodesCleaned

        Return cleanZRE

    End Function

#End Region 'Methods

End Class
