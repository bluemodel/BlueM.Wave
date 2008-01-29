Imports System.IO

Public Class RVA
    Inherits Dateiformat

    '***************************************************************************************
    '***************************************************************************************
    '**** Klasse RVA                                                                    ****
    '****                                                                               ****
    '**** Liest eine rva-Datei ein, wie sie erstellt wird durch die Konsolenversion von ****
    '**** "IHA Software" v7.03 von The Nature Conservancy                               ****
    '**** http://www.nature.org/initiatives/freshwater/conservationtools/art17004.html  ****
    '****                                                                               ****
    '**** Autoren: Felix Froehlich,                                                     ****
    '****                                                                               ****
    '**** Fachgebiet Ingenieurhydrologie und Wasserbewirtschaftung                      ****
    '**** TU Darmstadt                                                                  ****
    '***************************************************************************************
    '***************************************************************************************

#Region "Properties"

    'Struktur für RVA-Ergebnisse
    '---------------------------
    Public Structure IHAParam                           'Struktur für RVA Ergebnis eines Parameters
        Public PName As String                          'Parametername
        Public HAMiddle As Double                       'Hydrologic Alteration (HA) - Middle RVA Category
        Public ReadOnly Property fx_HA() As Double      'normalverteilter Funktionswert des HA-Werts
            Get
                Return fx(HAMiddle)
            End Get
        End Property
    End Structure

    Public Structure IHAParamGroup                      'Struktur für RVA Ergebnis einer Parametergruppe
        Public No As Short                              'Gruppennummer
        Public GName As String                          'Gruppennname
        Public Avg_fx_HA As Double                      'Mittelwert der fx(HA)-Werte der Gruppe
        Public IHAParams() As IHAParam                  'Liste der Paremeter
    End Structure

    Public Structure Struct_RVAValues                   'Struktur für alle RVA Ergebnisse zusammen
        Public GAvg_fx_HA As Double                     'Mittelwert der fx(HA)-Werte über alle Gruppen
        Public IHAParamGroups() As IHAParamGroup        'Liste der Parametergruppen
    End Structure

    Public RVAValues As Struct_RVAValues

#End Region

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)

        Call MyBase.New(FileName)

        'IHA-Parametergruppen definieren
        '-------------------------------
        With Me.RVAValues

            ReDim .IHAParamGroups(4)

            'Parameter group #1
            .IHAParamGroups(0).No = 1
            .IHAParamGroups(0).GName = "Quantity"
            ReDim .IHAParamGroups(0).IHAParams(11)

            'Parameter group #2
            .IHAParamGroups(1).No = 2
            .IHAParamGroups(1).GName = "Extremes"
            ReDim .IHAParamGroups(1).IHAParams(11)

            'Parameter group #3
            .IHAParamGroups(2).No = 3
            .IHAParamGroups(2).GName = "Timing"
            ReDim .IHAParamGroups(2).IHAParams(1)

            'Parameter group #4
            .IHAParamGroups(3).No = 4
            .IHAParamGroups(3).GName = "Frequency"
            ReDim .IHAParamGroups(3).IHAParams(3)

            'Parameter group #5
            .IHAParamGroups(4).No = 5
            .IHAParamGroups(4).GName = "Rate"
            ReDim .IHAParamGroups(4).IHAParams(2)

        End With

        Call Me.Read_File()

    End Sub

    Public Overrides Sub Read_File()

        'RVA-Datei öffnen und einlesen
        '-----------------------------
        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

        Dim i, j As Integer
        Dim Zeile As String
        Dim Psum, Gsum As Double                        'Summen der fx_HA Werte

        'Schleife über Parametergruppen
        '------------------------------
        Gsum = 0
        For i = 0 To Me.RVAValues.IHAParamGroups.GetUpperBound(0)

            'Ergebnisse einer Parametergruppe einlesen
            '-----------------------------------------
            With Me.RVAValues.IHAParamGroups(i)

                Do
                    Zeile = StrReadSync.ReadLine.ToString
                    If (Zeile.Contains("Parameter Group #" & .No)) Then

                        Psum = 0
                        'Schleife über Parameter
                        For j = 0 To .IHAParams.GetUpperBound(0)
                            Zeile = StrReadSync.ReadLine.ToString
                            .IHAParams(j).PName = Zeile.Substring(0, 20).Trim
                            .IHAParams(j).HAMiddle = Convert.ToDouble(Zeile.Substring(171, 14).Trim)
                            Psum += .IHAParams(j).fx_HA
                        Next

                        Exit Do
                    End If
                Loop Until StrReadSync.Peek() = -1

                'Mittelwert für eine Parametergruppe berechnen
                '---------------------------------------------
                .Avg_fx_HA = Psum / .IHAParams.GetLength(0)
                Gsum += .Avg_fx_HA

            End With

        Next 'Ende Schleife über Parametergruppen

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

        'Mittelwert aller Parametergruppen berechnen
        '-------------------------------------------
        Me.RVAValues.GAvg_fx_HA = Gsum / Me.RVAValues.IHAParamGroups.GetLength(0)

    End Sub

    'Transformiert einen HA-Wert in einen normalverteilten Funktionswert
    '*******************************************************************
    Private Shared Function fx(ByVal HA As Double) As Double

        'Parameter für Normalverteilung mit f(0) ~= 1
        '[EXCEL:] 1/(std*WURZEL(2*PI()))*EXP(-1/2*((X-avg)/std)^2)
        Dim std As Double = 0.398942423706863                   'Standardabweichung
        Dim avg As Double = 0                                   'Erwartungswert

        fx = 1 / (std * Math.Sqrt(2 * Math.PI)) * Math.Exp(-1 / 2 * ((HA - avg) / std) ^ 2)

        Return fx

    End Function

    Public Overrides Sub SpaltenAuslesen()
		'nix
    End Sub
End Class
