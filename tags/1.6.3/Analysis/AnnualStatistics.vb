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
''' <summary>
''' Calculates annual statistics (min, max, avg, vol) based on hydrological years
''' </summary>
''' <remarks></remarks>
Friend Class AnnualStatistics
    Inherits Analysis

    Public Structure struct_stat
        Public startDate As Date
        Public endDate As Date
        Public len As Long
        Public min As Double
        Public max As Double
        Public avg As Double
        Public vol As Double
    End Structure

    Private stats As Dictionary(Of String, struct_stat)

    Public Overrides ReadOnly Property hasResultChart() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has result series
    ''' that should be added to the main diagram
    ''' </summary>
    Public Overrides ReadOnly Property hasResultSeries() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Sub New(ByRef series As List(Of TimeSeries))
        MyBase.New(series)
        'Check: expects exactly one series
        If (series.Count <> 1) Then
            Throw New Exception("The Statistics analysis requires the selection of exactly 1 time series!")
        End If
        stats = New Dictionary(Of String, struct_stat)
    End Sub

    Private Function calculateStats(ByRef series As TimeSeries) As struct_stat
        Dim stats As struct_stat
        stats.startDate = series.StartDate
        stats.endDate = series.EndDate
        stats.len = series.Length
        stats.min = series.Minimum
        stats.max = series.Maximum
        stats.avg = series.Average
        stats.vol = series.Volume
        Return stats
    End Function

    Public Overrides Sub ProcessAnalysis()
        Dim hyoseries As Dictionary(Of Integer, TimeSeries)
        Dim year As Integer
        Dim series As TimeSeries

        'stats for entire series
        Me.stats.Add("Entire series", calculateStats(Me.mZeitreihen(0)))

        'stats for hydrological years
        hyoseries = Me.mZeitreihen(0).SplitHydroYears()
        For Each kvp As KeyValuePair(Of Integer, TimeSeries) In hyoseries
            year = kvp.Key
            series = kvp.Value
            Me.stats.Add(year.ToString, calculateStats(series))
        Next
    End Sub


    Public Overrides Sub PrepareResults()
        Dim stat As struct_stat
        Dim values() As String

        Const formatstring As String = "F4"

        Me.mResultText = "Statistics analysis:" & eol _
                         & eol _
                         & "Time series: " & Me.mZeitreihen(0).Title & eol _
                         & eol
        'output results in CSV format
        Me.mResultText &= "Results:" & eol
        Me.mResultText &= "Description;Start;End;Length;Min;Max;Avg;Volume" & eol
        For Each kvp As KeyValuePair(Of String, struct_stat) In Me.stats
            stat = kvp.Value
            ReDim values(7)
            values(0) = kvp.Key
            values(1) = stat.startDate.ToString(Helpers.DefaultDateFormat)
            values(2) = stat.endDate.ToString(Helpers.DefaultDateFormat)
            values(3) = stat.len.ToString(formatstring)
            values(4) = stat.min.ToString(formatstring)
            values(5) = stat.max.ToString(formatstring)
            values(6) = stat.avg.ToString(formatstring)
            values(7) = stat.vol.ToString(formatstring)
            Me.mResultText &= Join(values, ";") & eol
        Next
    End Sub

End Class
