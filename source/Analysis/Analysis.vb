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
''' Abstract base class for analysis functions
''' </summary>
Friend MustInherit Class Analysis

#Region "Events"

    ''' <summary>
    ''' Is raised when a lengthy process starts
    ''' </summary>
    Friend Event AnalysisStarted(n_steps As Integer)

    ''' <summary>
    ''' Is raised when a lengthy process updates its status
    ''' </summary>
    Friend Event AnalysisUpdated(value As Integer)

    ''' <summary>
    ''' Is raised when a lengthy process finishes
    ''' </summary>
    Friend Event AnalysisFinished()

#End Region

#Region "Eigenschaften"

    ''' <summary>
    ''' List of input TimeSeries for analysis
    ''' </summary>
    Protected InputTimeSeries As List(Of TimeSeries)

    ''' <summary>
    ''' Result text
    ''' is shown in the Wave log if `hasResultText` is True
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Protected ResultText As String

    ''' <summary>
    ''' Result values
    ''' Are shown in the Wave log if `hasResultValues` is True
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Protected ResultValues As Dictionary(Of String, Double)

    ''' <summary>
    ''' Result chart
    ''' is shown in a separate window if `hasResultChart` is True
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Protected ResultChart As ScottPlot.Plot

    ''' <summary>
    ''' List of result series
    ''' are loaded in the main chart if `hasResultSeries` is True
    ''' </summary>
    ''' <remarks>optional</remarks>
    Protected ResultSeries As List(Of TimeSeries)

    ''' <summary>
    ''' Result table
    ''' is shown in a separate window if `hasResultTable` is True
    ''' </summary>
    ''' <remarks>optional</remarks>
    Protected ResultTable As DataTable

#End Region 'Eigenschaften

#Region "Properties"

    ''' <summary>
    ''' Returns a text description of the analysis function
    ''' Should be overloaded by inheriting analysis functions
    ''' </summary>
    Public Shared Function Description() As String
        Return "No description found"
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result text
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultText() As Boolean

    ''' <summary>
    ''' Flag indicating whether the analysis function has result values
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultValues() As Boolean

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result diagram
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultChart() As Boolean

    ''' <summary>
    ''' Flag indicating whether the analysis function has result series
    ''' that should be added to the main diagram
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultSeries() As Boolean

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result table
    ''' that should be shown in a separate window
    ''' </summary>
    Public MustOverride ReadOnly Property hasResultTable() As Boolean

    ''' <summary>
    ''' Analysis result text
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultText() As String
        Get
            Return Me.ResultText
        End Get
    End Property

    ''' <summary>
    ''' Analysis result values
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultValues() As Dictionary(Of String, Double)
        Get
            Return Me.ResultValues
        End Get
    End Property

    ''' <summary>
    ''' Analysis result chart
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultChart() As ScottPlot.Plot
        Get
            Return Me.ResultChart
        End Get
    End Property

    ''' <summary>
    ''' Analysis result series
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultSeries() As List(Of TimeSeries)
        Get
            Return Me.ResultSeries
        End Get
    End Property

    ''' <summary>
    ''' Analysis result table
    ''' </summary>
    ''' <remarks>Optional</remarks>
    Public ReadOnly Property getResultTable() As DataTable
        Get
            Return Me.ResultTable
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="inputseries">List of input TimeSeries</param>
    Public Sub New(inputseries As List(Of TimeSeries))

        'Zeitreihen 
        Me.InputTimeSeries = inputseries

        'Datenstrukturen initialisieren
        Me.ResultValues = New Dictionary(Of String, Double)
        Me.ResultSeries = New List(Of TimeSeries)
    End Sub

    ''' <summary>
    ''' Processes the analysis
    ''' </summary>
    Public MustOverride Sub ProcessAnalysis()

    ''' <summary>
    ''' Prepares analysis results
    ''' </summary>
    Public MustOverride Sub PrepareResults()

    ''' <summary>
    ''' Starts a lengthy analysis process
    ''' </summary>
    ''' <param name="n_steps">Number of expected steps</param>
    Protected Sub AnalysisProgressStart(n_steps As Integer)
        RaiseEvent AnalysisStarted(n_steps)
    End Sub

    ''' <summary>
    ''' Updates a lengthy analysis process
    ''' </summary>
    ''' <param name="value">Progress value</param>
    Protected Sub AnalysisProgressUpdate(value As Integer)
        RaiseEvent AnalysisUpdated(value)
    End Sub

    ''' <summary>
    ''' Finishes a lengthy analysis process
    ''' </summary>
    Protected Sub AnalysisProgressFinish()
        RaiseEvent AnalysisFinished()
    End Sub



#End Region 'Methoden

End Class
