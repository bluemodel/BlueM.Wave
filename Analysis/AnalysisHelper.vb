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
    ''' <remarks>NaN-Werte in einer Zeitreihe führen dazu, dass die zugehörige Stützstelle ebenfalls gelöscht wird</remarks>
    Public Function getConcurrentValues(ByRef zre1 As Zeitreihe, ByRef zre2 As Zeitreihe) As Double(,)

        Dim values(,) As Double
        Dim zre1_temp, zre2_temp As Zeitreihe
        Dim i, j, n As Integer
        Dim found As Boolean

        'Zeitreihen aufeinander zuschneiden
        Call zre1.Cut(zre2)
        Call zre2.Cut(zre1)

        'Neue temporäre Zeitreihen instanzieren
        zre1_temp = New Zeitreihe(zre1.Title & " (bereinigt)")
        zre1_temp.Einheit = zre1.Einheit
        zre2_temp = New Zeitreihe(zre2.Title & " (bereinigt)")
        zre2_temp.Einheit = zre2.Einheit

        'ERSTE gemeinsame Stützstelle finden
        zre1_temp.Length = 1
        zre2_temp.Length = 1
        found = False
        j = 0
        For i = 0 To zre1.Length - 1
            'Ende von zre2 abfangen
            If (j > zre2.Length - 1) Then
                Exit For
            End If
            'NaN-Werte in zre1 überspringen
            If (zre1.YWerte(i) <> NaN) Then
                'Korrepondierenden Wert in zre2 suchen
                Do Until (zre2.XWerte(j) > zre1.XWerte(i))

                    If (zre2.XWerte(j) = zre1.XWerte(i)) Then
                        'Übereinstimmung gefunden!

                        'NaN-Werte in zre2 überspringen
                        If (zre2.YWerte(j) <> NaN) Then
                            'Stützstelle kopieren
                            zre1_temp.XWerte(0) = zre1.XWerte(i)
                            zre2_temp.XWerte(0) = zre2.XWerte(j)
                            zre1_temp.YWerte(0) = zre1.YWerte(i)
                            zre2_temp.YWerte(0) = zre2.YWerte(j)
                            found = True
                        End If
                    End If

                    'zre2 eins weiter setzen
                    j += 1

                    'Ende von zre2 abfangen
                    If (j > zre2.Length - 1) Then
                        Exit Do
                    End If
                Loop
            End If
            If (found) Then Exit For
        Next

        If (Not found) Then Throw New Exception("Es konnte keine gemeinsame Stützstelle gefunden werden!")

        'WEITERE gemeinsame Stützstellen finden
        n = 1
        Do Until (i > (zre1.Length - 1) Or j > (zre2.Length - 1))

            'zre1 eins weiter setzen
            i += 1

            'NaN-Werte in zre1 überspringen
            If (zre1.YWerte(i) <> NaN) Then

                'Korrepondierenden Wert in zre2 suchen
                Do Until (zre2.XWerte(j) > zre1.XWerte(i))

                    If (zre2.XWerte(j) = zre1.XWerte(i)) Then
                        'Übereinstimmung gefunden!

                        'NaN-Werte in zre2 überspringen
                        If (zre2.YWerte(j) <> NaN) Then
                            n += 1
                            'Stützstelle kopieren
                            zre1_temp.Length = n
                            zre2_temp.Length = n
                            zre1_temp.XWerte(n - 1) = zre1.XWerte(i)
                            zre2_temp.XWerte(n - 1) = zre2.XWerte(j)
                            zre1_temp.YWerte(n - 1) = zre1.YWerte(i)
                            zre2_temp.YWerte(n - 1) = zre2.YWerte(j)
                        End If
                    End If

                    'zre2 eins weiter setzen
                    j += 1

                    'Ende von zre2 abfangen
                    If (j > zre2.Length - 1) Then
                        Exit Do
                    End If
                Loop
            End If
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
