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
''' Analysis function for analyzing the timestep between nodes
''' </summary>
Friend Class TimeStepAnalysis
    Inherits Analysis

    ''' <summary>
    ''' Time units
    ''' </summary>
    Friend Enum TimeUnitEnum As Short
        Years = 1
        Months = 2
        Days = 3
        Hours = 4
        Minutes = 5
        Seconds = 6
    End Enum

    ''' <summary>
    ''' Selected time unit
    ''' </summary>
    Private TimeUnit As TimeUnitEnum

#Region "Properties"

    Public Overrides ReadOnly Property Description As String
        Get
            Return "Calculates the the timestep between nodes and returns a new timeseries of timestep sizes in the user-specified unit."
        End Get
    End Property

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
    ''' Name of the selected time unit
    ''' </summary>
    Private ReadOnly Property TimeUnitName() As String
        Get
            Return Me.TimeUnit.ToString("g")
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ts_list">Collection of TimeSeries</param>
    Public Sub New(ByRef ts_list As List(Of TimeSeries))

        Call MyBase.New(ts_list)

        Dim dlg As New TimestepAnalysisDialog()
        Dim dlg_result As DialogResult = dlg.ShowDialog()

        If Not dlg_result = DialogResult.OK Then
            Throw New Exception("User abort")
        End If

        Me.TimeUnit = dlg.ComboBox_TimestepType.SelectedItem

    End Sub

    ''' <summary>
    ''' Process Analysis
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim timestamp, timestamp_prev As DateTime
        Dim dt As TimeSpan
        Dim dt_val As Double
        Dim result_ts As TimeSeries

        'Loop over time series
        For Each ts As TimeSeries In MyBase.InputTimeSeries

            'create a new result time series
            result_ts = New TimeSeries($"{ts.Title} (dt {Me.TimeUnitName})")
            result_ts.Unit = Me.TimeUnitName
            result_ts.Interpretation = TimeSeries.InterpretationEnum.BlockLeft
            result_ts.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)

            'Loop over timestamps
            For i As Integer = 0 To ts.Length - 1

                timestamp = ts.Dates(i)

                If i = 0 Then
                    'dt of first node is always NaN
                    result_ts.AddNode(timestamp, Double.NaN)
                Else
                    'calculate dt
                    dt = timestamp - timestamp_prev
                    'convert to user-specified unit
                    Select Case Me.TimeUnit
                        'TODO: Implement conversion to Years and Months and activate corresponding options in TimestepAnalysisDialog
                        'Case TimeUnit.Years
                        'Case TimeUnit.Months
                        Case TimeUnitEnum.Days
                            dt_val = dt.TotalDays
                        Case TimeUnitEnum.Hours
                            dt_val = dt.TotalHours
                        Case TimeUnitEnum.Minutes
                            dt_val = dt.TotalMinutes
                        Case TimeUnitEnum.Seconds
                            dt_val = dt.TotalSeconds
                        Case Else
                            Throw New Exception($"Conversion of timestep to time unit {Me.TimeUnitName} is not yet implemented!")
                    End Select
                    'store dt value as new node in result series
                    result_ts.AddNode(timestamp, dt_val)
                End If
                timestamp_prev = timestamp
            Next
            'save result time series
            Me.ResultSeries.Add(result_ts)
        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()
        'TODO
    End Sub

#End Region 'Methods

End Class
