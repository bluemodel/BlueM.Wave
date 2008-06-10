Module AnalysisFactory

    'Liste der Analysefunktionen
    '***************************
    Public Enum AnalysisFunctions
        Monatsauswertung = 1
        Nash_Sutcliffe = 2
    End Enum

    'Fabrikmethode zur Erzeugung einer Analyse
    '*****************************************
    Public Function CreateAnalysis(ByVal analysisfunction As AnalysisFunctions, ByVal zeitreihen As Collection) As Analysis

        Dim oAnalysis As Analysis

        Select Case analysisfunction

            Case AnalysisFunctions.Monatsauswertung
                oAnalysis = New Monatsauswertung(zeitreihen)

                'Case AnalysisFunctions.Nash_Sutcliffe
                'oAnalysis = New NashSutcliffe()

            Case Else
                Throw New Exception("Analysefunktion nicht gefunden!")

        End Select

        Return oAnalysis

    End Function

End Module
