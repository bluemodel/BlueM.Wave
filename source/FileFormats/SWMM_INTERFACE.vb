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
    ''' Class for reading and writing the SWMM5 routing interface file format
    ''' </summary>
    ''' <remarks>See https://wiki.bluemodel.org/index.php/SWMM_file_formats </remarks>
    Public Class SWMM_INTERFACE
        Inherits TimeSeriesFile

#Region "Eigenschaften"

        Const DatumsformatSWMM_TXT As String = "yyyy MM  dd  HH mm  ss"
        Const iZeileReportTimeStep As Integer = 3
        Const iZeileAnzConstituents As Integer = 4
        Private Shared AnzConstituents As Integer
        Private Shared Constituents() As Constituent
        Private AnzNodes As Integer
        Private _Zeitintervall As Integer
        Private _noConstituents As Integer

        ''' <summary>
        ''' Structure for storing SWMM series information
        ''' </summary>
        Private Structure SWMMSeriesInfo
            Dim Node As String
            Dim Variable As String
        End Structure

        ''' <summary>
        ''' Dictionary containing all SWMM series infos
        ''' Key is series index
        ''' </summary>
        Private swmmInfos As Dictionary(Of Integer, SWMMSeriesInfo)

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
        ''' Returns a list of SWMM routing interface file specific metadata keys
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
            Me.swmmInfos = New Dictionary(Of Integer, SWMMSeriesInfo)

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

                    'store SWMM info
                    Dim swmmInfo As SWMMSeriesInfo
                    swmmInfo.Node = Nodes(i).Bez
                    swmmInfo.Variable = Constituents(j).Type
                    Me.swmmInfos.Add(index, swmmInfo)

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
                Me.TimeSeries(sInfo.Index).Metadata("Node") = Me.swmmInfos(sInfo.Index).Node
                Me.TimeSeries(sInfo.Index).Metadata("Variable") = Me.swmmInfos(sInfo.Index).Variable
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
        ''' Sets default metadata values for a time series corresponding to the SWMM routing interface file format
        ''' </summary>
        Public Overloads Shared Sub setDefaultMetadata(ts As TimeSeries)
            'Make sure all required keys exist
            ts.Metadata.AddKeys(SWMM_INTERFACE.MetadataKeys)
            'Set default values
            If ts.Metadata("Node") = "" Then
                'use series title by default
                ts.Metadata("Node") = ts.Title
                'check for existing metadata from a SWMM binary output file and reuse if possible
                If ts.Metadata.ContainsKey("Type") And ts.Metadata.ContainsKey("Name") Then
                    'if Type is "Node", assign "Name" to "Node"
                    If ts.Metadata("Type") = "Node" Then
                        ts.Metadata("Node") = ts.Metadata("Name")
                    End If
                End If
            End If
            If ts.Metadata("Variable") = "" Then ts.Metadata("Variable") = "FLOW"
        End Sub

        ''' <summary>
        ''' Exports a list of time series to SWMM routing interface text format
        ''' </summary>
        ''' <param name="seriesList">list of time series to export</param>
        ''' <param name="file">path to file to export to</param>
        Public Shared Sub Write_File(ByRef seriesList As List(Of TimeSeries), file As String)

            Dim strwrite As StreamWriter
            Dim i, j, k As Integer
            Dim dt As Integer
            Dim LenReihe As Long
            Dim KonFaktor As Integer

            'check for required metadata
            For Each ts As TimeSeries In seriesList
                If Not ts.Metadata.ContainsKey("Node") Then
                    Throw New Exception($"Series {ts.Title} is missing a required metadata entry 'Node'!")
                End If
                If Not ts.Metadata.ContainsKey("Variable") Then
                    Throw New Exception($"Series {ts.Title} is missing a required metadata entry 'Variable'!")
                End If
            Next

            'sort series by node and then variable with FLOW first
            seriesList.Sort(Function(ts1 As TimeSeries, ts2 As TimeSeries)
                                If ts1.Metadata("Node") = ts2.Metadata("Node") Then
                                    If ts1.Metadata("Variable") = "FLOW" Then
                                        Return -1
                                    ElseIf ts2.Metadata("Variable") = "FLOW" Then
                                        Return 1
                                    Else
                                        Return ts1.Metadata("Variable").CompareTo(ts2.Metadata("Variable"))
                                    End If
                                Else
                                    Return ts1.Metadata("Node").CompareTo(ts2.Metadata("Node"))
                                End If
                            End Function)

            'Determine unique nodes and variables
            Dim nodes As New List(Of String)
            Dim variables As New List(Of String)
            For Each ts As TimeSeries In seriesList
                If Not nodes.Contains(ts.Metadata("Node")) Then
                    'node names must not contain spaces
                    nodes.Add(ts.Metadata("Node").Replace(" ", "_"))
                End If
                If Not variables.Contains(ts.Metadata("Variable")) Then
                    variables.Add(ts.Metadata("Variable"))
                End If
            Next

            'check that "FLOW" is among the variables
            If Not variables.Contains("FLOW") Then
                Throw New Exception($"SWMM routing interface text format requires a variable named 'FLOW'!")
            End If

            'determine units for variables
            Dim units As New List(Of String)
            For Each variable As String In variables
                'find the first time series containing the variable and use its unit
                'TODO: this assumes that all series with the same variable also have the same unit!
                For Each ts As TimeSeries In seriesList
                    If ts.Metadata("Variable") = variable Then
                        If variable = "FLOW" Then
                            'FLOW must always be in l/s
                            units.Add("LPS")
                            'determine conversion factor
                            KonFaktor = FlowConversionFactor(ts.Unit)
                        Else
                            units.Add(ts.Unit)
                        End If
                        Exit For
                    End If
                Next
            Next

            'determine time step in seconds
            'TODO: this assumes that all time series are equidistant and have the same timestep!
            dt = DateDiff(DateInterval.Second, seriesList(0).Dates(0), seriesList(0).Dates(1))

            'open file for writing
            strwrite = New StreamWriter(file, False, Helpers.DefaultEncoding)

            'the first line contains the keyword "SWMM5" (without the quotes)
            strwrite.WriteLine("SWMM5 routing interface file")

            'a line of text that describes the file (can be blank)
            strwrite.WriteLine("BlueM.Wave export")

            'the time step used for all inflow records (integer seconds)
            strwrite.WriteLine($"{dt,-5} - reporting time step in sec")

            'the number of variables stored in the file, where the first variable must always be flow rate
            strwrite.WriteLine($"{variables.Count,-5} - number of constituents as listed below:")

            'the name and units of each variable (one per line), where flow rate is the first variable listed and is always named FLOW
            For i = 0 To variables.Count - 1
                strwrite.WriteLine($"{variables(i)}   {units(i)}")
            Next

            'the number of nodes with recorded inflow data
            strwrite.WriteLine($"{nodes.Count,-5} - number of nodes as listed below:")

            'the name of each node (one per line)
            For Each node As String In nodes
                strwrite.WriteLine(node)
            Next

            'a line of text that provides column headings for the data to follow (can be blank)
            strwrite.Write($"Node          Year Mon Day Hr Min Sec  ")
            For Each variable As String In variables
                strwrite.Write($"{variable,-10}  ")
            Next
            strwrite.WriteLine()

            'for each node at each time step, a line with:
            ' the name of the node
            ' the date (year, month, and day separated by spaces)
            ' the time of day (hours, minutes, and seconds separated by spaces)
            ' the flow rate followed by the concentration of each quality constituent
            'Time periods with no values at any node can be skipped
            LenReihe = seriesList(0).Length
            For i = 0 To LenReihe - 1
                For j = 0 To nodes.Count - 1
                    strwrite.Write(nodes(j).PadRight(11))
                    strwrite.Write("   ")
                    strwrite.Write(seriesList(j).Dates(i).ToString(DatumsformatSWMM_TXT))
                    strwrite.Write("   ")
                    strwrite.Write((seriesList(j * variables.Count).Values(i) * KonFaktor).ToString(DefaultNumberFormat).PadRight(10))
                    If variables.Count > 1 Then
                        For k = 1 To variables.Count - 1
                            If k < variables.Count - 1 Then
                                strwrite.Write("  ")
                                strwrite.Write((seriesList(j * k + 1).Values(i) * KonFaktor).ToString(DefaultNumberFormat).PadRight(10))
                            ElseIf k = variables.Count - 1 Then
                                strwrite.Write("  ")
                                strwrite.WriteLine(seriesList(j * variables.Count + 1).Values(i).ToString(DefaultNumberFormat).PadRight(10))
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

        ''' <summary>
        ''' Returns a conversion factor for converting a flow unit to LPS
        ''' </summary>
        ''' <param name="unit">the flow unit to convert</param>
        ''' <returns></returns>
        Public Shared Function FlowConversionFactor(unit As String) As Integer

            Select Case unit
                Case "m3/s", "m³/s", "CMS"
                    Return 1000
                Case "l/s", "LPS"
                    Return 1
                Case Else
                    Throw New Exception($"Unable to determine conversion factor for converting unit {unit} to LPS!")
            End Select

        End Function

#End Region 'Methoden

    End Class

End Namespace