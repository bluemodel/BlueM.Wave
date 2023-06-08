# Adding a new analysis function

To add a new analysis function in Wave
1. write a new analysis function class that inherits from `Analysis`
1. add the new analysis function to the `AnalysisFactory`

## 1. Create a new analysis class

The new analysis class must inherit from `Analysis`. It receives a list of input time series and can produce text, values, a chart, a table and/or a list of result time series.

Example `Analysis\TestAnalysis.vb`:
```vb
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
            Return True
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

        'Result table (will be shown in separate window)
        MyBase.ResultTable = New DataTable("Test analysis result")
        'add columns and rows as needed
        '...
        '...

        'List of result series (will be loaded in main window)
        MyBase.ResultSeries = New List(Of TimeSeries) From {timeseries_output}

    End Sub

End Class
```

## 2. Add the new analysis function to the `AnalysisFactory`

Add the new analysis function to the AnalysisFactory (file `Analysis\AnalysisFactory.vb`). The locations to edit are highlighted below with `<------------`.

```vb
''' <summary>
''' Factory for creating analysis instances
''' </summary>
Friend Module AnalysisFactory

    ''' <summary>
    ''' List of analysis functions
    ''' </summary>
    Public Enum AnalysisFunctions
        AnnualStatistics
        Autocorrelation
        Calculator
        ChangeTimestep
        Comparison
        Cumulative
        DoubleSumCurve
        GoodnessOfFit
        Histogram
        LinearRegression
        MonthlyStatistics
        TimestepAnalysis
        TestAnalysis       '<--------- Add new analysis functions to enumeration here
    End Enum

    ''' <summary>
    ''' Returns a text description of the analysis function
    ''' </summary>
    ''' <param name="analysisfunction"></param>
    ''' <returns></returns>
    Public Function getAnalysisDescription(analysisfunction As AnalysisFunctions) As String
        Select Case analysisfunction
            Case AnalysisFunctions.AnnualStatistics
                Return AnnualStatistics.Description
            Case AnalysisFunctions.Autocorrelation
                Return Autocorrelation.Description
            Case AnalysisFunctions.Calculator
                Return Calculator.Description
            Case AnalysisFunctions.ChangeTimestep
                Return ChangeTimestep.Description
            Case AnalysisFunctions.Comparison
                Return Comparison.Description
            Case AnalysisFunctions.Cumulative
                Return Cumulative.Description
            Case AnalysisFunctions.DoubleSumCurve
                Return DoubleSumCurve.Description
            Case AnalysisFunctions.GoodnessOfFit
                Return GoodnessOfFit.Description
            Case AnalysisFunctions.Histogram
                Return Histogram.Description
            Case AnalysisFunctions.LinearRegression
                Return LinearRegression.Description
            Case AnalysisFunctions.MonthlyStatistics
                Return MonthlyStatistics.Description
            Case AnalysisFunctions.TimestepAnalysis
                Return TimeStepAnalysis.Description
            Case AnalysisFunctions.TestAnalysis         '<--------- Add case for new analysis descriptions here
                Return TestAnalysis.Description         '<--------- 
            Case Else
                Return "Description not found"
        End Select
    End Function

    ''' <summary>
    ''' Factory method for creating an analysis instance
    ''' </summary>
    ''' <param name="analysisfunction">the type of analysis instance to create</param>
    ''' <param name="seriesList">list of input TimeSeries</param>
    ''' <returns>the analysis instance</returns>
    Public Function CreateAnalysis(analysisfunction As AnalysisFunctions, seriesList As List(Of TimeSeries)) As Analysis

        Dim oAnalysis As Analysis

        Select Case analysisfunction

            Case AnalysisFunctions.AnnualStatistics
                oAnalysis = New AnnualStatistics(seriesList)

            Case AnalysisFunctions.Autocorrelation
                oAnalysis = New Autocorrelation(seriesList)

            Case AnalysisFunctions.Calculator
                oAnalysis = New Calculator(seriesList)

            Case AnalysisFunctions.ChangeTimestep
                oAnalysis = New ChangeTimestep(seriesList)

            Case AnalysisFunctions.Comparison
                oAnalysis = New Comparison(seriesList)

            Case AnalysisFunctions.Cumulative
                oAnalysis = New Cumulative(seriesList)

            Case AnalysisFunctions.DoubleSumCurve
                oAnalysis = New DoubleSumCurve(seriesList)

            Case AnalysisFunctions.GoodnessOfFit
                oAnalysis = New GoodnessOfFit(seriesList)

            Case AnalysisFunctions.Histogram
                oAnalysis = New Histogram(seriesList)

            Case AnalysisFunctions.LinearRegression
                oAnalysis = New LinearRegression(seriesList)

            Case AnalysisFunctions.MonthlyStatistics
                oAnalysis = New MonthlyStatistics(seriesList)

            Case AnalysisFunctions.TimestepAnalysis
                oAnalysis = New TimeStepAnalysis(seriesList)

            Case AnalysisFunctions.TestAnalysis                 '<--------- Add case for creation of new analysis instance here
                oAnalysis = New TestAnalysis(seriesList)        '<---------

            Case Else
                Throw New Exception("Analysis not found!")

        End Select

        Return oAnalysis

    End Function

End Module
```