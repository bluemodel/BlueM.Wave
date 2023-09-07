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
Imports System.IO

Namespace Fileformats

    ''' <summary>
    ''' Class for reading the SWMM5 Routing Interface File format (*.txt)
    ''' The format is described in the SWMM format description 
    ''' </summary>
    ''' <remarks>See https://wiki.bluemodel.org/index.php/SWMM_file_formats </remarks>
    Public Class SWMM_INTERFACE
        Inherits TimeSeriesFile

#Region "Eigenschaften"

        Const DatumsformatSWMM_TXT As String = "yyyy  MM dd  HH mm"
        Const iZeileReportTimeStep As Integer = 3
        Const iZeileAnzConstituents As Integer = 4
        Private Shared AnzConstituents As Integer
        Private Shared Constituents() As Constituent
        Private AnzNodes As Integer
        Private _Zeitintervall As Integer
        Private _noConstituents As Integer

        Private Structure seriesMetadata
            Dim Node As String
            Dim Variable As String
        End Structure

        Private seriesMetadataIndex As Dictionary(Of Integer, seriesMetadata)

        Public Structure Constituent
            Dim Type As String
            Dim Unit As String
            Dim Index As Integer
        End Structure

        Private Structure Node
            Dim Bez As String
            Dim Index As Integer
        End Structure

        Public Property Zeitintervall() As Integer
            Get
                Return _Zeitintervall
            End Get
            Set(value As Integer)
                _Zeitintervall = value
            End Set
        End Property

        ''' <summary>
        ''' Returns a list of SWMM interface file specific metadata keys
        ''' </summary>
        Public Overloads Shared ReadOnly Property MetadataKeys() As List(Of String)
            Get
                Dim keys As New List(Of String) From {
                    "Node",
                    "Variable"
                }
                Return keys
            End Get
        End Property


#End Region

#Region "Properties"

        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property



#End Region 'Properties

#Region "Methoden"

        ''' <summary>
        ''' Konstruktor
        ''' </summary>
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.iLineHeadings = 2
            Me.UseUnits = True
            Me.IsColumnSeparated = False
            Me.ColumnOffset = 0
            Me.DecimalSeparator = Constants.period

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Datei komplett einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Spaltenköpfe auslesen
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Dim i, j As Integer
            Dim Zeile As String = ""
            Dim ZeileSpalten As String = ""
            Dim ZeileEinheiten As String = ""
            Dim strArray() As String
            Dim Constituents() As Constituent
            'dim AnzConstituents As Integer
            Dim Nodes() As Node
            Dim index As Long
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()
            Me.seriesMetadataIndex = New Dictionary(Of Integer, seriesMetadata)

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Zeile mit Reporting Time Step finden
            For i = 1 To iZeileReportTimeStep
                Zeile = StrReadSync.ReadLine.ToString()
            Next
            strArray = Zeile.Split(New Char() {space.ToChar}, StringSplitOptions.RemoveEmptyEntries)
            Me.Zeitintervall = Convert.ToSingle(strArray(0))

            'Zeile mit der Anzahl der Constituents finden
            For i = 1 To iZeileAnzConstituents - iZeileReportTimeStep
                Zeile = StrReadSync.ReadLine.ToString()
            Next
            'Anzahl der Constituents zu einem Knoten
            strArray = Zeile.Split(New Char() {space.ToChar}, StringSplitOptions.RemoveEmptyEntries)
            AnzConstituents = Convert.ToSingle(strArray(0))

            ReDim Constituents(AnzConstituents - 1)
            'Inflows und Einheit einlesen
            For i = 0 To AnzConstituents - 1
                Zeile = StrReadSync.ReadLine.ToString()
                strArray = Zeile.Split(New Char() {space.ToChar}, StringSplitOptions.RemoveEmptyEntries)
                Constituents(i).Type = strArray(0)
                Constituents(i).Unit = strArray(1)
                Constituents(i).Index = i
            Next

            'Anzahl der Zuflussknoten ermitteln
            'entspricht der Anzahl der Zeilen pro Zeitschritt
            Zeile = StrReadSync.ReadLine.ToString()
            strArray = Zeile.Split(New Char() {space.ToChar}, StringSplitOptions.RemoveEmptyEntries)
            AnzNodes = Convert.ToInt32(strArray(0))
            ReDim Nodes(AnzNodes - 1)
            For i = 0 To AnzNodes - 1
                Zeile = StrReadSync.ReadLine.ToString()
                Nodes(i).Bez = Trim(Zeile)
                Nodes(i).Index = i
            Next

            'Anzahl der Zeitreihen (Spalten) ermitteln
            Dim anzSpalten As Integer
            anzSpalten = AnzConstituents * AnzNodes

            'iZeileDaten kann erst jetzt gesetzt werden, wenn AnzZeilen_dT bekannt ist
            Me.iLineData = iZeileAnzConstituents + AnzConstituents + AnzNodes + 3

            'Spaltenköpfe (Zuflussknoten) und Indizes einlesen
            index = 1
            For i = 0 To AnzNodes - 1
                For j = 0 To AnzConstituents - 1

                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = $"{Nodes(i).Bez} {Constituents(j).Type}"
                    sInfo.Unit = Constituents(j).Unit
                    sInfo.Index = index
                    Me.TimeSeriesInfos.Add(sInfo)

                    'store metadata for later
                    Dim metadata As seriesMetadata
                    metadata.Node = Nodes(i).Bez
                    metadata.Variable = Constituents(j).Type
                    Me.seriesMetadataIndex.Add(index, metadata)

                    index = index + 1
                Next
            Next

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Zeitreihen einlesen
        ''' </summary>
        Public Overrides Sub readFile()

            Dim iZeile, i As Integer, j As Integer
            Dim Zeile As String
            Dim datum As DateTime
            Dim Werte() As String
            Dim tmpArray() As String
            Dim IDWerte As Long
            Dim ts As TimeSeries

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Zeitreihen instanzieren
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                ts = New TimeSeries(sInfo.Name)
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            'Einheiten?
            If (Me.UseUnits = False) Then
                MsgBox("Beim Einlesen eines SWMM-Interface-Files müssen immer die Einheiten gesetzt sein!")
                Exit Sub
            End If

            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                Me.TimeSeries(sInfo.Index).Unit = sInfo.Unit
                'add metadata
                Me.TimeSeries(sInfo.Index).Metadata.AddKeys(SWMM_INTERFACE.MetadataKeys)
                Me.TimeSeries(sInfo.Index).Metadata("Node") = Me.seriesMetadataIndex(sInfo.Index).Node
                Me.TimeSeries(sInfo.Index).Metadata("Variable") = Me.seriesMetadataIndex(sInfo.Index).Variable
            Next

            'Einlesen
            '--------
            ReDim Werte(AnzConstituents * AnzNodes)
            'Header
            For iZeile = 1 To Me.iLineData - 1
                Zeile = StrReadSync.ReadLine.ToString()
            Next

            'Daten
            'Schema: Alle Werte in Array Werte schreiben, dann aber über SpaltenSel.Index nur die ausgewählten in Zeitreihe importieren
            Do
                IDWerte = 1
                For i = 0 To AnzNodes - 1
                    Zeile = StrReadSync.ReadLine.ToString()
                    tmpArray = Zeile.Split(New Char() {space.ToChar}, StringSplitOptions.RemoveEmptyEntries)
                    If i = 0 Then
                        datum = New System.DateTime(tmpArray(1), tmpArray(2), tmpArray(3), tmpArray(4), tmpArray(5), tmpArray(6), 0, New System.Globalization.GregorianCalendar())
                    End If
                    For j = 0 To AnzConstituents - 1
                        Werte(IDWerte) = tmpArray(tmpArray.Length - AnzConstituents + j)
                        IDWerte = IDWerte + 1
                    Next
                Next
                For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                    Me.TimeSeries(sInfo.Index).AddNode(datum, StringToDouble(Werte(sInfo.Index)))
                Next

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        End Sub

        ''' <summary>
        ''' Sets default metadata values for a time series corresponding to the SWMM interface file format
        ''' </summary>
        Public Overloads Shared Sub setDefaultMetadata(ts As TimeSeries)
            'Make sure all required keys exist
            ts.Metadata.AddKeys(SWMM_INTERFACE.MetadataKeys)
            'Set default values
            If ts.Metadata("Node") = "" Then ts.Metadata("Node") = ts.Title
            If ts.Metadata("Variable") = "" Then ts.Metadata("Variable") = "FLOW"
        End Sub

        ''' <summary>
        ''' Exportiert eine Zeitreihe als TXT-Datei
        ''' </summary>
        ''' <param name="Reihen">Die zu exportierenden Zeitreihen</param>
        ''' <param name="File">Pfad zur anzulegenden Datei</param>
        Public Shared Sub Write_File(ByRef Reihen As List(Of TimeSeries), File As String)

            Dim strwrite As StreamWriter
            Dim i, j, k As Integer
            Dim LenReihe As Long
            Dim KonFaktor As Integer


            'SWMM5 Interface File
            'RTC-Demo 
            ' 60 - reporting time step in sec
            ' 1    - number of constituents as listed below:
            'FLOW LPS
            ' 5    - number of nodes as listed below:
            'S101
            'Node          Year Mon Day Hr  Min Sec         FLOW
            'S101          2001 6   10  0   0   0          0.000

            strwrite = New StreamWriter(File, False, Helpers.DefaultEncoding)

            Dim dt As Integer

            'the first line contains the keyword "SWMM5" (without the quotes)
            strwrite.WriteLine("SWMM5 Interface File")

            'a line of text that describes the file (can be blank)
            strwrite.WriteLine("BlueM.Wave export")

            'the time step used for all inflow records (integer seconds)
            'TODO: this assumes equidistant time series!
            dt = DateDiff(DateInterval.Minute, Reihen(0).Dates(0), Reihen(0).Dates(1))
            dt = dt * 60
            strwrite.WriteLine($"{dt,5} - reporting time step in sec")

            'the number of variables stored in the file, where the first variable must always be flow rate
            Dim variables As New HashSet(Of String)
            For Each ts As TimeSeries In Reihen
                variables.Add(ts.Metadata("Variable"))
            Next
            strwrite.WriteLine($"{variables.Count} - number of constituents as listed below:")

            'the name and units of each variable (one per line), where flow rate is the first variable listed and is always named FLOW
            'TODO: make sure FLOW is first
            'TODO: use actual unit instead of always "LPS"
            For Each variable As String In variables
                strwrite.WriteLine($"{variable}   LPS")
            Next

            'the number of nodes with recorded inflow data
            Dim nodes As New HashSet(Of String)
            For Each ts As TimeSeries In Reihen
                nodes.Add(ts.Metadata("Node"))
            Next
            strwrite.WriteLine($"{nodes.Count} - number of nodes as listed below:")

            'the name of each node (one per line)
            For Each node As String In nodes
                strwrite.WriteLine(node)
            Next

            'TODO: Hier muss noch geprüft werden, ob für alle Nodes auch alle Variables existieren

            'a line of text that provides column headings for the data to follow (can be blank)
            strwrite.WriteLine($"Node          Year Mon Day Hr  Min Sec         {String.Join("  ", variables)}")

            'for each node at each time step, a line with:
            ' the name of the node
            ' the date (year, month, and day separated by spaces)
            ' the time of day (hours, minutes, and seconds separated by spaces)
            ' the flow rate followed by the concentration of each quality constituent
            'Time periods with no values at any node can be skipped

            'TODO: Bei mehreren Variablen muss im Moment die richtige Reihenfolge vorab gegeben sein!

            'SWMM-Reihen immer in l/s
            KonFaktor = FakConv(Reihen(0).Unit)

            LenReihe = Reihen(0).Length
            For i = 0 To LenReihe - 1
                For j = 0 To nodes.Count - 1
                    strwrite.Write(nodes(j).PadRight(12))
                    strwrite.Write("   ")
                    strwrite.Write(Reihen(j).Dates(i).ToString(DatumsformatSWMM_TXT))
                    strwrite.Write(" 00   ")
                    strwrite.Write((Reihen(j * variables.Count).Values(i) * KonFaktor).ToString(DefaultNumberFormat).PadLeft(14))
                    If variables.Count > 1 Then
                        For k = 1 To variables.Count - 1
                            If k < variables.Count - 1 Then
                                strwrite.Write("   ")
                                strwrite.Write((Reihen(j * k + 1).Values(i) * KonFaktor).ToString(DefaultNumberFormat).PadLeft(10))
                            ElseIf k = variables.Count - 1 Then
                                strwrite.Write("   ")
                                strwrite.WriteLine((Reihen(j * variables.Count + 1).Values(i) * KonFaktor).ToString(DefaultNumberFormat).PadLeft(10))
                            End If
                        Next
                    Else
                        strwrite.WriteLine()
                    End If
                Next
            Next

            strwrite.Close()

        End Sub

        ''' <summary>
        ''' Prüft, ob es sich um ein Routing Interface File für SWMM handelt
        ''' </summary>
        ''' <param name="file">Pfad zur Datei</param>
        ''' <returns></returns>
        Public Shared Function verifyFormat(file As String) As Boolean

            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim Zeile As String = ""

            '1. Zeile einlesen
            Zeile = StrRead.ReadLine.ToString().Trim()

            StrRead.Close()
            FiStr.Close()

            If Zeile.StartsWith("SWMM5") And Not Zeile.StartsWith("SWMM5 LID Report File") Then
                'Es ist ein SWMM-Interface-File
                Return True
            Else
                Return False
            End If

        End Function

        Public Shared Function nEqualStrings(strArrayIn() As String) As Integer

            Dim Names() As String
            Dim i, j As Integer
            Dim strExists As Boolean

            ReDim Preserve Names(0)
            Names(0) = strArrayIn(0)
            nEqualStrings = 1

            For i = 0 To strArrayIn.Length - 1
                strExists = False
                For j = 0 To Names.Length - 1
                    If strArrayIn(i) = Names(j) Then
                        strExists = True
                        Exit For
                    End If
                Next
                If strExists = False Then
                    nEqualStrings = nEqualStrings + 1
                    ReDim Preserve Names(Names.Length)
                    Names(Names.Length - 1) = strArrayIn(i)
                End If
            Next

        End Function

        Public Shared Function FakConv(UnitIn As String) As Integer

            Select Case UnitIn
                Case "m3/s"
                    Return 1000
                Case Else
                    Return 1
            End Select
        End Function

#End Region 'Methoden

    End Class

End Namespace