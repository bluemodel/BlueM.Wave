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
''' Performs mathematical operations on time series
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:Calculator</remarks>
Friend Class Calculator
    Inherits Analysis

#Region "Properties"

    ''' <summary>
    ''' Returns a text description of the analysis function
    ''' </summary>
    Public Overloads Shared Function Description() As String
        Return "Performs mathematical operations on time series and returns a new time series as the result."
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result test
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return True
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
    ''' The mathematical expression used for the calculation
    ''' </summary>
    Private expression As String

    ''' <summary>
    ''' List of variables
    ''' </summary>
    Private tsVariables As List(Of CalculatorVariable)

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="seriesList">list of TimeSeries</param>
    Public Sub New(ByRef seriesList As List(Of TimeSeries))

        Call MyBase.New(seriesList)

        If seriesList.Count > 26 Then
            ' only 26 variable names are available
            Throw New Exception("The Calculator analysis only supports up to 26 time series!")
        End If

        'assign variable names to time series and store as dictionary
        Me.tsVariables = New List(Of CalculatorVariable)
        Dim varNames As New List(Of String) From
            {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        Dim i As Integer = 0
        For Each ts As TimeSeries In Me.InputTimeSeries
            tsVariables.Add(New CalculatorVariable(varNames(i), ts))
            i += 1
        Next

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        'show the ChangeTimeStepDialog
        Dim dlg As New CalculatorDialog(Me.tsVariables)
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then

            'read settings from dialog
            Me.expression = dlg.TextBox_Formula.Text
            Dim title As String = dlg.TextBox_Title.Text
            Dim unit As String = dlg.ComboBox_Unit.Text

            'initialize parser and add custom functions
            Dim parser As New MathParserNet.Parser()
            parser.RegisterCustomDoubleFunction("MAX", AddressOf Calculator.Max)
            parser.RegisterCustomDoubleFunction("MIN", AddressOf Calculator.Min)

            Dim value As Double
            Dim ts_result As New TimeSeries()

            'collect unique timestamps
            Dim timestamps As New HashSet(Of DateTime)
            For Each ts As TimeSeries In Me.InputTimeSeries
                timestamps.UnionWith(ts.Dates)
            Next

            'initialize progress bar
            MyBase.AnalysisProgressStart(timestamps.Count)

            'loop over timestamps
            Dim i As Integer = 1
            For Each t As DateTime In timestamps
                'create variables for this timestamp
                For Each tsVariable As CalculatorVariable In tsVariables
                    value = Double.NaN
                    If tsVariable.ts.Dates.Contains(t) Then
                        value = tsVariable.ts.Nodes(t)
                    End If
                    parser.AddVariable(tsVariable.varName, value)
                Next
                'evaluate formula
                value = parser.SimplifyDouble(Me.expression)
                'store as new node in result series
                ts_result.AddNode(t, value)
                'remove variables
                parser.RemoveAllVariables()
                'update progressbar
                MyBase.AnalysisProgressUpdate(i)
                i += 1
            Next

            'clean up
            parser.UnregisterAllCustomFunctions()

            'Store result series
            ts_result.Title = title
            ts_result.Unit = unit
            ts_result.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
            Me.ResultSeries.Add(ts_result)

            'finish progress bar
            MyBase.AnalysisProgressFinish()

        End If

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()
        Me.ResultText = "Calculator analysis:" & eol
        Me.ResultText &= $"Formula: {Me.expression}" & eol
        Me.ResultText &= "Variables: " & eol
        For Each tsvariable As CalculatorVariable In Me.tsVariables
            Me.ResultText &= $"{tsvariable.varName}: {tsvariable.ts.Title}" & eol
        Next
        Me.ResultText &= $"Result series: {Me.ResultSeries.First.Title}"

    End Sub

#End Region 'Methods

#Region "Custom functions"

    Friend Shared Function Max(val1 As Double, val2 As Double) As Double
        Return Math.Max(val1, val2)
    End Function

    Friend Shared Function Min(val1 As Double, val2 As Double) As Double
        Return Math.Min(val1, val2)
    End Function

#End Region 'Custom functions

End Class
