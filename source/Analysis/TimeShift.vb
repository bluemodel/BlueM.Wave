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
''' Shifts all timestamps of one or multiple time series by a fixed timespan
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:TimeShift</remarks>
Friend Class TimeShift
    Inherits Analysis

    ''' <summary>
    ''' Returns the description of the analysis function
    ''' </summary>
    Public Overloads Shared Function Description() As String
        Return "Shifts all timestamps by a fixed timespan"
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function produces a result text
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function produces result values
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function produces a result chart
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

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result table
    ''' that should be shown in a separate window
    ''' </summary>
    Public Overrides ReadOnly Property hasResultTable() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Class constructor
    ''' </summary>
    ''' <param name="timeseries">list of time series to be analyzed</param>
    Public Sub New(ByRef timeseries As List(Of TimeSeries))

        'Call constructor of base class
        Call MyBase.New(timeseries)

    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim dlg As New TimeShiftDialog()
        If dlg.ShowDialog() = DialogResult.Cancel Then
            'user cancelled
            Throw New Exception("User cancelled analysis")
        End If

        Dim timestepInterval As Integer = dlg.NumericUpDown_TimestepInterval.Value
        Dim timestepType As TimeSeries.TimeStepTypeEnum = dlg.ComboBox_TimestepType.SelectedItem

        MyBase.AnalysisProgressStart(Me.InputTimeSeries.Count)

        Dim i As Integer = 0
        For Each ts As TimeSeries In InputTimeSeries

            i += 1
            MyBase.AnalysisProgressUpdate(i)

            Dim ts_shifted As TimeSeries = ts.ShiftTime(timestepInterval, timestepType)

            MyBase.ResultSeries.Add(ts_shifted)
        Next

        MyBase.AnalysisProgressFinish()

    End Sub

    ''' <summary>
    ''' Prepare the results of the analysis
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'nothing to do here

    End Sub

End Class