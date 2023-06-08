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
''' Write description!
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:TestAnalysis</remarks>
Friend Class TestAnalysis
    Inherits Analysis

    'fields for storing stuff
    Private timeseries_input As TimeSeries  'Time series to be analyzed
    Private title As String                 'Title of the time series
    Private length As Integer               'Count of the time series values
    Private timeseries_output As TimeSeries 'Result time series

    ''' <summary>
    ''' Returns the description of the analysis function
    ''' </summary>
    Public Overloads Shared Function Description() As String
        Return "Write description!"
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function produces a result text
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function produces result values
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function produces a result chart
    ''' </summary>
    Public Overrides ReadOnly Property hasResultChart() As Boolean
        Get
            Return True
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

        'Check expected count of time series
        If (timeseries.Count <> 1) Then
            Throw New Exception("The TestAnalysis requires the selection of exactly one time series!")
        End If

    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        'Get time series from list (`InputTimeSeries` defined in base class)
        timeseries_input = InputTimeSeries.Item(0)

        'Get values from input time series
        title = timeseries_input.Title
        length = timeseries_input.Length
        '...
        '...

        'Implement analysis here ...

        'Optional: display a progress bar
        MyBase.AnalysisProgressStart(100)
        '...
        MyBase.AnalysisProgressUpdate(50)
        '...
        MyBase.AnalysisProgressFinish()

        'store result for later
        timeseries_output = timeseries_input.Clone()

    End Sub

    ''' <summary>
    ''' Prepare the results of the analysis
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Result text (will be shown in WaveLog)
        MyBase.ResultText = "Success!"

        'Result values (will be shown in WaveLog)
        MyBase.ResultValues.Add($"Count values of time series '{title}'", length)

        'Result chart (will be shown in separate window)
        MyBase.ResultChart = New Steema.TeeChart.Chart()
        'Fill and format chart
        '...
        '...

        'List of result series (will be loaded in main window)
        MyBase.ResultSeries = New List(Of TimeSeries) From {timeseries_output}

    End Sub

End Class