'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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
        'TestAnalysis       '<--------- Add new analysis functions to enumeration here
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
            'Case AnalysisFunctions.TestAnalysis         '<--------- Add case for new analysis descriptions here
            '    Return TestAnalysis.Description         '<--------- 
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

            'Case AnalysisFunctions.TestAnalysis                 '<--------- Add case for creation of new analysis instance here
            '    oAnalysis = New TestAnalysis(seriesList)        '<---------

            Case Else
                Throw New Exception("Analysis not found!")

        End Select

        Return oAnalysis

    End Function

End Module
