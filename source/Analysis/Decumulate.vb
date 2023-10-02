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
''' Decumulates time series
''' </summary>
Friend Class Decumulate
    Inherits Analysis

    Public Overloads Shared Function Description() As String
        Return "For each selected time series, calculates a new time series that consists of the decumulated values of the original series. NaN values are ignored."
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result test
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has result values
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result diagram
    ''' </summary>
    Public Overrides ReadOnly Property hasResultChart() As Boolean
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
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property hasResultTable() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ts_list">List of time series to process</param>
    Public Sub New(ByRef ts_list As List(Of TimeSeries))
        Call MyBase.New(ts_list)
    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' Creates new result time series that contain the decumulated values of the original series
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i As Integer
        Dim value, last_value As Double
        Dim ts, ts_decum As TimeSeries

        For Each ts In MyBase.InputTimeSeries

            If Not ts.Interpretation = TimeSeries.InterpretationEnum.Cumulative Then
                Log.AddLogEntry(levels.error, $"Time series {ts.Title} with interpretation {[Enum].GetName(GetType(TimeSeries.InterpretationEnum), ts.Interpretation)} cannot be deculumated!")
                Continue For
            End If

            'create new decumulated timeseries
            ts_decum = New TimeSeries($"{ts.Title} (decumulated)")
            ts_decum.Unit = ts.Unit
            ts_decum.Interpretation = TimeSeries.InterpretationEnum.CumulativePerTimestep
            ts_decum.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)

            'copy first node unchanged
            ts_decum.AddNode(ts.StartDate, ts.FirstValue)

            'loop over remaining nodes
            last_value = ts.FirstValue
            For i = 1 To ts.Length - 1

                If Double.IsNaN(ts.Values(i)) Then
                    'omit NaN values
                    Continue For
                End If

                'decumulate
                value = ts.Values(i) - last_value

                'add new node
                ts_decum.AddNode(ts.Dates(i), value)

                'store last value
                last_value = ts.Values(i)
            Next

            'store result series
            MyBase.ResultSeries.Add(ts_decum)

        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()
        'nothing to do
    End Sub

End Class
