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
        Public HALow As Double                          'Hydrologic Alteration (HA) - Low RVA Category
        Public HAMiddle As Double                       'Hydrologic Alteration (HA) - Middle RVA Category
        Public HAHigh As Double                         'Hydrologic Alteration (HA) - High RVA Category
    End Structure

    Public Structure IHAParamGroup                      'Struktur für RVA Ergebnis einer Parametergruppe
        Public No As Short                              'Gruppennummer
        Public GName As String                          'Gruppennname
        Public IHAParams() As IHAParam                  'Liste der Parameter
        Public ReadOnly Property NParams() As Integer   'Anzahl Parameter in der Gruppe
            Get
                Return Me.IHAParams.Length()
            End Get
        End Property
    End Structure

    Public Structure Struct_RVAValues                   'Struktur für alle RVA Ergebnisse zusammen
        Public Title As String                          'Titel
        Public IHAParamGroups() As IHAParamGroup        'Liste der Parametergruppen
        Public ReadOnly Property NGroups() As Integer   'Anzahl der Parametergruppen
            Get
                Return Me.IHAParamGroups.Length()
            End Get
        End Property
        Public Shadows ReadOnly Property ToString() As String
            Get
                Return Me.Title
            End Get
        End Property
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
        Dim Werte() As String

        'Datei bis zu RVA-Ergebnissen vorspulen
        '--------------------------------------
        Do
            Zeile = StrReadSync.ReadLine.ToString()
        Loop Until (Zeile.Trim = "Assessment of Hydrologic Alteration")

        'Schleife über Parametergruppen
        '------------------------------
        For i = 0 To Me.RVAValues.IHAParamGroups.GetUpperBound(0)

            'Ergebnisse einer Parametergruppe einlesen
            '-----------------------------------------
            With Me.RVAValues.IHAParamGroups(i)

                Do
                    Zeile = StrReadSync.ReadLine.ToString
                    If (Zeile.Trim = "Parameter Group #" & .No) Then

                        'Schleife über Parameter
                        For j = 0 To .IHAParams.GetUpperBound(0)
                            Zeile = StrReadSync.ReadLine.ToString()
                            'Name einlesen
                            .IHAParams(j).PName = Zeile.Substring(0, 22).Trim()
                            'Werte einlesen
                            Werte = Zeile.Substring(22).Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                            .IHAParams(j).HAMiddle = StringToDouble(Werte(2))
                            .IHAParams(j).HAHigh = StringToDouble(Werte(5))
                            .IHAParams(j).HALow = StringToDouble(Werte(8))
                        Next

                        Exit Do
                    End If
                Loop Until StrReadSync.Peek() = -1

            End With

        Next 'Ende Schleife über Parametergruppen

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    Public Overrides Sub SpaltenAuslesen()
        'nix
    End Sub
End Class
