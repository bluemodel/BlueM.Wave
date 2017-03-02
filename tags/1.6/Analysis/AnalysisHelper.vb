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
''' Modul, das oft genutzte Analysefunktion zur Verfügung stellt
''' </summary>
Public Module AnalysisHelper

    ''' <summary>
    ''' Löscht aus zwei Zeitreihen alle nicht-gemeinsamen Stützstellen und gibt zusätzlich die gemeinsamen Wertepaare als Array zurück
    ''' </summary>
    ''' <param name="zre1">Erste Zeitreihe</param>
    ''' <param name="zre2">Zweite Zeitreihe</param>
    ''' <returns>Alle gemeinsamen Wertepaare als zweidimensionales Array, 1. Dimension: Stützstelle, 2. Dimension: Wertepaar</returns>
    Public Function getConcurrentValues(ByRef zre1 As Zeitreihe, ByRef zre2 As Zeitreihe) As Double(,)

        Dim values(,) As Double
        Dim zre1_temp, zre2_temp As Zeitreihe
        Dim i, j As Integer
        Dim found As Boolean

        'Zeitreihen aufeinander zuschneiden
        Call zre1.Cut(zre2)
        Call zre2.Cut(zre1)

        'Neue temporäre Zeitreihen instanzieren
        zre1_temp = New Zeitreihe(zre1.Title)
        zre1_temp.Einheit = zre1.Einheit
        zre2_temp = New Zeitreihe(zre2.Title)
        zre2_temp.Einheit = zre2.Einheit

        'ERSTE gemeinsame Stützstelle finden
        found = False
        j = 0
        For i = 0 To zre1.Length - 1
            'Ende von zre2 abfangen
            If (j > zre2.Length - 1) Then
                Exit For
            End If

            'Korrespondierenden Wert in zre2 suchen
            Do Until (zre2.XWerte(j) > zre1.XWerte(i))

                If (zre2.XWerte(j) = zre1.XWerte(i)) Then
                    'Übereinstimmung gefunden!

                    'Stützstellen kopieren
                    zre1_temp.AddNode(zre1.XWerte(i), zre1.YWerte(i))
                    zre2_temp.AddNode(zre2.XWerte(j), zre2.YWerte(j))

                    found = True
                End If

                'zre2 eins weiter setzen
                j += 1

                'Ende von zre2 abfangen
                If (j > zre2.Length - 1) Then
                    Exit Do
                End If
            Loop
            If (found) Then Exit For
        Next

        If (Not found) Then Throw New Exception("No coincident data points found!")

        'WEITERE gemeinsame Stützstellen finden
        Do Until (i > (zre1.Length - 1) Or j > (zre2.Length - 1))

            'zre1 eins weiter setzen
            i += 1

            'Korrespondierenden Wert in zre2 suchen
            Do Until (zre2.XWerte(j) > zre1.XWerte(i))

                If (zre2.XWerte(j) = zre1.XWerte(i)) Then
                    'Übereinstimmung gefunden!

                    'Stützstellen kopieren
                    zre1_temp.AddNode(zre1.XWerte(i), zre1.YWerte(i))
                    zre2_temp.AddNode(zre2.XWerte(j), zre2.YWerte(j))
                End If

                'zre2 eins weiter setzen
                j += 1

                'Ende von zre2 abfangen
                If (j > zre2.Length - 1) Then
                    Exit Do
                End If
            Loop
        Loop

        'Temporäre Zeitreihen in Ursprungszeitreihen zurückkopieren
        zre1 = zre1_temp.Clone()
        zre2 = zre2_temp.Clone()

        'Wertepaare in Array packen
        ReDim values(zre1.Length - 1, 1)
        For i = 0 To values.GetUpperBound(0)
            values(i, 0) = zre1.YWerte(i)
            values(i, 1) = zre2.YWerte(i)
        Next

        Return values

    End Function

End Module
