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
''' Factory for creating analysis instances
''' </summary>
Friend Module AnalysisFactory

    ''' <summary>
    ''' List of analysis functions
    ''' </summary>
    Public Enum AnalysisFunctions
        Accumulate
        AnnualRecurrenceProbability
        AnnualStatistics
        Autocorrelation
        Calculator
        ChangeTimestep
        Comparison
        Decumulate
        DoubleSumCurve
        GoodnessOfFit
        Histogram
        LinearRegression
        MonthlyStatistics
        TimeShift
        TimestepAnalysis
        'TestAnalysis    'EDIT THIS
    End Enum

    ''' <summary>
    ''' Returns a text description of the analysis function
    ''' </summary>
    ''' <param name="analysisfunction"></param>
    ''' <returns></returns>
    Public Function getAnalysisDescription(analysisfunction As AnalysisFunctions) As String
        Select Case analysisfunction
            Case AnalysisFunctions.Accumulate
                Return Accumulate.Description
            Case AnalysisFunctions.AnnualRecurrenceProbability
                Return AnnualRecurrenceProbability.Description
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
            Case AnalysisFunctions.Decumulate
                Return Decumulate.Description
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
            Case AnalysisFunctions.TimeShift
                Return TimeShift.Description
            Case AnalysisFunctions.TimestepAnalysis
                Return TimeStepAnalysis.Description
                'Case AnalysisFunctions.TestAnalysis    'EDIT THIS
                '    Return TestAnalysis.Description    'EDIT THIS
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

            Case AnalysisFunctions.Accumulate
                oAnalysis = New Accumulate(seriesList)

            Case AnalysisFunctions.AnnualRecurrenceProbability
                oAnalysis = New AnnualRecurrenceProbability(seriesList)

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

            Case AnalysisFunctions.Decumulate
                oAnalysis = New Decumulate(seriesList)

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

            Case AnalysisFunctions.TimeShift
                oAnalysis = New TimeShift(seriesList)

            Case AnalysisFunctions.TimestepAnalysis
                oAnalysis = New TimeStepAnalysis(seriesList)

            'Case AnalysisFunctions.TestAnalysis             'EDIT THIS
            '    oAnalysis = New TestAnalysis(seriesList)    'EDIT THIS

            Case Else
                Throw New Exception("Analysis not found!")

        End Select

        Return oAnalysis

    End Function

End Module
