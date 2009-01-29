Imports System.IO

''' <summary>
''' Klasse für RVA-Dateiformat
''' </summary>
''' <remarks>
''' RVA-Dateien werden generiert von der Konsolenversion von "IHA Software" v7.03 
''' von The Nature Conservancy, siehe http://www.nature.org/initiatives/freshwater/conservationtools/art17004.html
''' </remarks>
Public Class RVA
    Inherits Dateiformat

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Struktur für RVA-Ergebnisse eines IHA-Parameters
    ''' </summary>
    Public Structure IHAParam
        ''' <summary>
        ''' Parametername
        ''' </summary>
        Public PName As String
        ''' <summary>
        ''' Hydrologic Alteration (HA) - Low RVA Category
        ''' </summary>
        Public HALow As Double
        ''' <summary>
        ''' Hydrologic Alteration (HA) - Middle RVA Category
        ''' </summary>
        Public HAMiddle As Double
        ''' <summary>
        ''' Hydrologic Alteration (HA) - High RVA Category
        ''' </summary>
        ''' <remarks></remarks>
        Public HAHigh As Double
    End Structure

    ''' <summary>
    ''' Struktur für RVA Ergebnis einer ganzen Parametergruppe
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure IHAParamGroup
        ''' <summary>
        ''' Gruppennummer
        ''' </summary>
        Public No As Short
        ''' <summary>
        ''' Gruppennname
        ''' </summary>
        Public GName As String
        ''' <summary>
        ''' Liste der Parameter in der Gruppe
        ''' </summary>
        Public IHAParams() As IHAParam
        ''' <summary>
        ''' Anzahl Parameter in der Gruppe
        ''' </summary>
        Public ReadOnly Property NParams() As Integer
            Get
                Return Me.IHAParams.Length()
            End Get
        End Property
    End Structure

    ''' <summary>
    ''' Struktur für alle RVA Ergebnisse zusammen
    ''' </summary>
    Public Structure Struct_RVAValues
        ''' <summary>
        ''' Titel
        ''' </summary>
        Public Title As String
        ''' <summary>
        ''' Liste der Parametergruppen
        ''' </summary>
        Public IHAParamGroups() As IHAParamGroup
        ''' <summary>
        ''' Anzahl der Parametergruppen
        ''' </summary>
        Public ReadOnly Property NGroups() As Integer
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

    ''' <summary>
    ''' Das RVA Ergebnis
    ''' </summary>
    Public RVAValues As Struct_RVAValues

#End Region

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

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

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.Read_File()
        End If

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
