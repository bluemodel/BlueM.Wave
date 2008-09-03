''' <summary>
''' Fabrik zum Erzeugen von Analyse-Objekten
''' </summary>
Module AnalysisFactory

    ''' <summary>
    ''' Liste der Analysefunktionen
    ''' </summary>
    Public Enum AnalysisFunctions
        Monatsauswertung = 1
        Doppelsummenalyse = 2
    End Enum

    ''' <summary>
    ''' Fabrikmethode zur Erzeugung einer Analyse
    ''' </summary>
    ''' <param name="analysisfunction">Das zu erzeugende Analyse-Objekt</param>
    ''' <param name="zeitreihen">Collection von zu analysierenden Zeitreihen</param>
    ''' <returns>Das Analyse-Objekt</returns>
    Public Function CreateAnalysis(ByVal analysisfunction As AnalysisFunctions, ByVal zeitreihen As Collection) As Analysis

        Dim oAnalysis As Analysis

        Select Case analysisfunction

            Case AnalysisFunctions.Monatsauswertung
                oAnalysis = New Monatsauswertung(zeitreihen)

            Case AnalysisFunctions.Doppelsummenalyse
                oAnalysis = New Doppelsummenanalyse(zeitreihen)

            Case Else
                Throw New Exception("Analysefunktion nicht gefunden!")

        End Select

        Return oAnalysis

    End Function

End Module
