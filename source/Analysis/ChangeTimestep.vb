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
''' Make a time series equidistant with a user-specified timestep
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:ChangeTimestep</remarks>
Friend Class ChangeTimestep
    Inherits Analysis

#Region "Properties"

    ''' <summary>
    ''' Returns a text description of the analysis function
    ''' </summary>
    Public Overloads Shared Function Description() As String
        Return "Make a time series equidistant with a user-specified timestep"
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

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="seriesList">Collection von Zeitreihen</param>
    Public Sub New(ByRef seriesList As List(Of TimeSeries))

        Call MyBase.New(seriesList)

        If seriesList.Count <> 1 Then
            Throw New Exception("The ChangeTimestep analysis requires the selection of exactly 1 time series!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim ts, ts_new As TimeSeries
        Dim inputInterpretation, outputInterpretation As TimeSeries.InterpretationEnum
        Dim timesteptype As TimeSeries.TimeStepTypeEnum
        Dim timestepinterval As Integer
        Dim startdate As DateTime
        Dim ignoreNaN As Boolean

        ts = Me.InputTimeSeries(0)

        'show the ChangeTimeStepDialog
        Dim dlg As New ChangeTimestepDialog(ts)
        If dlg.ShowDialog() <> DialogResult.OK Then
            Throw New AnalysisCancelledException("Analysis cancelled")
        End If

        'read settings from dialog
        inputInterpretation = dlg.ComboBox_InputInterpretation.SelectedItem
        outputInterpretation = dlg.ComboBox_OutputInterpretation.SelectedItem
        timesteptype = dlg.ComboBox_TimestepType.SelectedItem
        timestepinterval = dlg.NumericUpDown_TimestepInterval.Value
        startdate = CType(dlg.MaskedTextBox_Start.ValidateText(), DateTime)
        ignoreNaN = dlg.CheckBox_IgnoreNaN.Checked

        'if ignoreNaN is set, remove all NaN values from time series
        If ignoreNaN = True Then
            ts = ts.removeNaNValues()
        End If

        'change timestep
        ts.Interpretation = inputInterpretation
        ts_new = ts.ChangeTimestep(timesteptype, timestepinterval, startdate, outputInterpretation)

        'Store result series
        ts_new.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
        Me.ResultSeries.Add(ts_new)

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()
        'nothing to do
    End Sub

#End Region 'Methods

End Class
