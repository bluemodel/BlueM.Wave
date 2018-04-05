'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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
''' Fabrik zum Erzeugen von Analyse-Objekten
''' </summary>
Module AnalysisFactory

    ''' <summary>
    ''' Liste der Analysefunktionen
    ''' </summary>
    Public Enum AnalysisFunctions
        MonthlyStatistics = 1
        DoubleSumCurve = 2
        GoodnessOfFit = 3
        Histogram = 4
        Comparison = 5
        AnnualStatistics = 6
    End Enum

    ''' <summary>
    ''' Fabrikmethode zur Erzeugung eines Analyse-Objekts
    ''' </summary>
    ''' <param name="analysisfunction">Typ des zu erzeugenden Analyse-Objekts</param>
    ''' <param name="zeitreihen">Collection von zu analysierenden Zeitreihen</param>
    ''' <returns>Das Analyse-Objekt</returns>
    Public Function CreateAnalysis(ByVal analysisfunction As AnalysisFunctions, ByVal zeitreihen As List(Of TimeSeries)) As Analysis

        Dim oAnalysis As Analysis

        Select Case analysisfunction

            Case AnalysisFunctions.MonthlyStatistics
                oAnalysis = New MonthlyStatistics(zeitreihen)

            Case AnalysisFunctions.DoubleSumCurve
                oAnalysis = New DoubleSumCurve(zeitreihen)

            Case AnalysisFunctions.GoodnessOfFit
                oAnalysis = New GoodnessOfFit(zeitreihen)

            Case AnalysisFunctions.Histogram
                oAnalysis = New Histogram(zeitreihen)

            Case AnalysisFunctions.Comparison
                oAnalysis = New Comparison(zeitreihen)

            Case AnalysisFunctions.AnnualStatistics
                oAnalysis = New AnnualStatistics(zeitreihen)

            Case Else
                Throw New Exception("Analysis not found!")

        End Select

        Return oAnalysis

    End Function

End Module
