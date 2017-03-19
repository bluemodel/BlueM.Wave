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
Imports System.IO

''' <summary>
''' Klasse für das TXT-Dateiformat von SWMM5
''' Format ist festgeschrieben im SWMM-Anwenderhandbuch
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/TXT-Format</remarks>
Public Class SWMM_TXT
    Inherits Dateiformat
    
#Region "Eigenschaften"

    Const DatumsformatSWMM_TXT As String = "yyyy  MM dd  HH mm"
    Const iZeileReportTimeStep As Integer = 3
    Const iZeileAnzConstituents As Integer = 4
    Private Shared AnzConstituents As Integer
    private Shared Constituents() As Constituent
    Private AnzNodes As Integer
    Private _Zeitintervall As Integer
    Private _noConstituents As Integer
    

    Structure Constituent
        Dim Type As String
        Dim Unit As String
        Dim Index As Integer
    End Structure

    Structure Nodes
        Dim Bez As String
        Dim Index As Integer
    End Structure

    Public Property Zeitintervall() As Integer
        Get
            Return _Zeitintervall
        End Get
        Set(ByVal value As Integer)
            _Zeitintervall = value
        End Set
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
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        SpaltenOffset = 0

        'Voreinstellungen
        Me.iZeileUeberschriften = 2
        Me.UseEinheiten = True
        Me.Zeichengetrennt = False
        Me.Dezimaltrennzeichen = Me.punkt

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Datei komplett einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If


    End Sub

    ''' <summary>
    ''' Spaltenköpfe auslesen
    ''' </summary>
    Public Overrides Sub SpaltenAuslesen()

        Dim i, j As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""
        Dim strArray() As String
        Dim Constituents() As Constituent
        'dim AnzConstituents As Integer
        Dim Nodes() As Nodes
        Dim IDSpalte As Long

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync As TextReader = TextReader.Synchronized(StrRead)

            'Zeile mit Reporting Time Step finden
            For i = 1 To iZeileReportTimeStep
                Zeile = StrReadSync.ReadLine.ToString()
            Next
            strArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            Me.Zeitintervall = Convert.ToSingle(strArray(0))

            'Zeile mit der Anzahl der Constituents finden
            For i = 1 To iZeileAnzConstituents - iZeileReportTimeStep
                Zeile = StrReadSync.ReadLine.ToString()
            Next
            'Anzahl der Constituents zu einem Knoten
            strArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            AnzConstituents = Convert.ToSingle(strArray(0))

            ReDim Constituents(AnzConstituents - 1)
            'Inflows und Einheit einlesen
            For i = 0 To AnzConstituents - 1
                Zeile = StrReadSync.ReadLine.ToString()
                strArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                Constituents(i).Type = strArray(0)
                Constituents(i).Unit = strArray(1)
                Constituents(i).Index = i
            Next

            'Anzahl der Zuflussknoten ermitteln
            'entspricht der Anzahl der Zeilen pro Zeitschritt
            Zeile = StrReadSync.ReadLine.ToString()
            strArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
            Me.nZeilen = Convert.ToSingle(strArray(0))
            AnzNodes = Me.nZeilen
            ReDim Nodes(AnzNodes - 1)
            For i = 0 To AnzNodes - 1
                Zeile = StrReadSync.ReadLine.ToString()
                Nodes(i).Bez = Trim(Zeile)
                Nodes(i).Index = i
            Next

            'Anzahl der Zeitreihen (Spalten) ermitteln
            Dim anzSpalten As Integer
            anzSpalten = AnzConstituents * AnzNodes
            ReDim Me.Spalten(anzSpalten)

            'iZeileDaten kann erst jetzt gesetzt werden, wenn AnzZeilen_dT bekannt ist
            Me.iZeileDaten = iZeileAnzConstituents + AnzConstituents + AnzNodes + 3

            'Index 0 für Datum belegen
            Me.Spalten(0).Name = "Date"
            Me.Spalten(0).Index = 0
            Me.Spalten(0).Einheit = "-"


            'Spaltenköpfe (Zuflussknoten) und Indizes einlesen
            IDSpalte = 1
            For i = 0 To AnzNodes - 1
                For j = 0 To AnzConstituents - 1
                    Me.Spalten(IDSpalte).Name = Nodes(i).Bez & " " & Constituents(j).Type
                    Me.Spalten(IDSpalte).Objekt = Nodes(i).Bez
                    Me.Spalten(IDSpalte).Type = Constituents(j).Type
                    Me.Spalten(IDSpalte).Einheit = Constituents(j).Unit
                    Me.Spalten(IDSpalte).Index = IDSpalte
                    IDSpalte = IDSpalte + 1
                Next
            Next


            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte die Datei '" & Path.GetFileName(Me.File) & "' nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    ''' <summary>
    ''' Zeitreihen einlesen
    ''' </summary>
    Public Overrides Sub Read_File()

        Dim iZeile, i As Integer, j As Integer
        Dim Zeile As String
        Dim datum As DateTime
        Dim Werte() As String
        Dim tmpArray() As String
        Dim IDWerte As Long
        'Dim AnzConstituents As Integer
        Dim AllConstituents() As String
        Dim AllNodes() As String


        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Anzahl Zeitreihen bestimmen
        ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)

        'Zeitreihen instanzieren
        For i = 0 To Me.SpaltenSel.Length - 1
            Me.Zeitreihen(i) = New TimeSeries(Me.SpaltenSel(i).Name)
        Next

        'Einheiten?
        If (Me.UseEinheiten = False) Then
            MsgBox("Beim Einlesen eines SWMM-Interface-Files müssen immer die Einheiten gesetzt sein!")
            Exit Sub
        End If

        ReDim AllConstituents(Me.SpaltenSel.Length - 1)
        ReDim AllNodes(Me.SpaltenSel.Length - 1)
        'Alle ausgewählten Spalten durchlaufen
        For i = 0 To Me.SpaltenSel.Length - 1
            Me.Zeitreihen(i).Unit = Me.SpaltenSel(i).Einheit
            Me.Zeitreihen(i).Objekt = Me.SpaltenSel(i).Objekt
            AllConstituents(i) = Me.SpaltenSel(i).Type
            Me.Zeitreihen(i).Type = Me.SpaltenSel(i).Type
            AllNodes(i) = Me.SpaltenSel(i).Objekt
        Next


        'Einlesen
        '--------
        'AnzConstituents = nEqualStrings(AllConstituents)
        'AnzNodes = nEqualStrings(AllNodes)
        ReDim Werte(AnzConstituents * AnzNodes)
        'Header
        For iZeile = 1 To Me.iZeileDaten - 1
            Zeile = StrReadSync.ReadLine.ToString()
        Next

        'Daten
        'Schema: Alle Werte in Array Werte schreiben, dann aber über SpaltenSel.Index nur die ausgewählten in Zeitreihe importieren
        Do
            IDWerte = 1
            For i = 0 To AnzNodes - 1
                Zeile = StrReadSync.ReadLine.ToString()
                tmpArray = Zeile.Split(New Char() {leerzeichen.Character}, StringSplitOptions.RemoveEmptyEntries)
                If i = 0 Then
                    datum = New System.DateTime(tmpArray(1), tmpArray(2), tmpArray(3), tmpArray(4), tmpArray(5), tmpArray(6), 0, New System.Globalization.GregorianCalendar())
                End If
                For j = 0 To AnzConstituents - 1
                    Werte(IDWerte) = tmpArray(tmpArray.Length - AnzConstituents + j)
                    IDWerte = IDWerte + 1
                Next
            Next
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index)))
            Next

        Loop Until StrReadSync.Peek() = -1

        StrReadSync.Close()
        StrRead.Close()
        FiStr.Close()

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als TXT-Datei
    ''' </summary>
    ''' <param name="Reihen">Die zu exportierenden Zeitreihen</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    Public Shared Sub Write_File(ByRef Reihen As List(Of TimeSeries), ByVal File As String)

        Dim strwrite As StreamWriter
        Dim i, j, k As Integer
        Dim AllConstituents() As Constituent
        Dim UniqueConstituents(0) As Constituent
        'Dim AnzConstituents As Integer
        Dim AllNodes() As String
        Dim UniqueNodes(0) As String
        Dim AnzOutNodes As Integer
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


        strwrite = New StreamWriter(File, False, System.Text.Encoding.GetEncoding("iso8859-1"))

        Dim dt As Integer
        'Zeitintervall aus ersten und zweiten Zeitschritt der Reihe ermitteln
        dt = DateDiff(DateInterval.Minute, Reihen(0).Dates(0), Reihen(0).Dates(1))
        dt = dt * 60

        '1. Zeile
        strwrite.WriteLine("SWMM5 Interface File")
        '2. Zeile: 
        strwrite.WriteLine("WAVE-Export")
        'Zeitintervall
        strwrite.Write(dt.ToString.PadLeft(5))
        strwrite.WriteLine(" - reporting time step in sec")

        'Erstmal alle Constituents (FLOW, COD,...) ermitteln, da diese im Textkopf angegeben werden müssen
        ReDim AllConstituents(Reihen.Count - 1)
        For i = 0 To Reihen.Count - 1
            AllConstituents(i).Type = Reihen(i).Type
            AllConstituents(i).Unit = Reihen(i).Unit
        Next
        GetUniqueConstituents(AllConstituents, UniqueConstituents)

        strwrite.Write(UniqueConstituents.Length)
        strwrite.WriteLine(" - number of constituents as listed below:")

        For i = 0 To UniqueConstituents.Length - 1
            strwrite.Write(UniqueConstituents(i).Type)
            strwrite.Write("   ")
            strwrite.WriteLine("LPS")
        Next

        'Alle Zuflussknoten ermitteln, da diese im Textkopf angegeben werden müsen
        ReDim AllNodes(Reihen.Count - 1)
        For i = 0 To Reihen.Count - 1
            AllNodes(i) = Reihen(i).Objekt
        Next
        GetUniqueNodes(AllNodes, UniqueNodes)
        AnzOutNodes = UniqueNodes.Length

        strwrite.Write(AnzOutNodes)
        strwrite.WriteLine(" - number of nodes as listed below:")

        For i = 0 To AnzOutNodes - 1
            strwrite.WriteLine(UniqueNodes(i))
        Next

        'TO DO: Hier muss noch geprüft werden, ob für alle Objects (Zuflussknoten) auch alle Constituents existieren

        'Schreiben der Überschrift, d.h. Datum und Constituents
        'Datum
        strwrite.Write("Node          Year Mon Day Hr  Min Sec         ")
        'Constituents
        For i = 0 To UniqueConstituents.Length - 1
            If i < UniqueConstituents.Length - 1 Then
                strwrite.Write(UniqueConstituents(i).Type)
                strwrite.Write("   ")
            ElseIf i = UniqueConstituents.Length - 1 Then
                strwrite.WriteLine(UniqueConstituents(i).Type)
            End If
        Next

        'SWMM-Reihen immer in l/s
        KonFaktor = FakConv(Reihen(0).Unit)

        'Zeitreihen rausschreiben
        'TO DO: Bei mehreren Constituents muss im Moment die richtige Reihenfolge vorab gegeben sein
        'd.h. eigentlich muss beim rausschreiben noch Objekt und Constituent geprüft werden
        LenReihe = Reihen(0).Length
        For i = 0 To LenReihe - 1
            For j = 0 To AnzOutNodes - 1
                strwrite.Write(UniqueNodes(j).PadRight(12))
                strwrite.Write("   ")
                strwrite.Write(Reihen(j).Dates(i).ToString(DatumsformatSWMM_TXT))
                strwrite.Write(" 00   ")
                strwrite.Write((Reihen(j * UniqueConstituents.Length).Values(i) * KonFaktor).ToString(Zahlenformat).PadLeft(14))
                If UniqueConstituents.Length > 1 Then
                    For k = 1 To UniqueConstituents.Length - 1
                        If k < UniqueConstituents.Length - 1 Then
                            strwrite.Write("   ")
                            strwrite.Write((Reihen(j * k + 1).Values(i) * KonFaktor).ToString(Zahlenformat).PadLeft(10))
                        ElseIf k = UniqueConstituents.Length - 1 Then
                            strwrite.Write("   ")
                            strwrite.WriteLine((Reihen(j * UniqueConstituents.Length + 1).Values(i) * KonFaktor).ToString(Zahlenformat).PadLeft(10))
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
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim Zeile As String = ""

        '3 Zeilen einlesen
        Zeile = StrRead.ReadLine.ToString()
        Zeile = Trim(Zeile)

        StrRead.Close()
        FiStr.Close()

        If Zeile.StartsWith("SWMM5") Then
            'Es ist ein SWMM-Interface-File
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function nEqualStrings(ByVal strArrayIn() As String) As Integer

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

    Public Shared Sub GetUniqueConstituents(ByVal AllConstIn() As Constituent, ByRef ConstOut() As Constituent)

        Dim i, j As Integer
        Dim ExistsOutArray As Boolean

        ConstOut(0).Type = AllConstIn(0).Type
        ConstOut(0).Unit = AllConstIn(0).Unit

        For i = 0 To AllConstIn.Length - 1
            ExistsOutArray = False
            For j = 0 To ConstOut.Length - 1
                If AllConstIn(i).Type = ConstOut(j).Type Then
                    ExistsOutArray = True
                    Exit For
                End If
            Next
            If ExistsOutArray = False Then
                ReDim Preserve ConstOut(ConstOut.Length)
                ConstOut(ConstOut.Length - 1).Type = AllConstIn(i).Type
                ConstOut(ConstOut.Length - 1).Unit = AllConstIn(i).Unit
            End If
        Next

    End Sub

    Public Shared Sub GetUniqueNodes(ByVal AllNodesIn() As String, ByRef NodesOut() As String)

        Dim i, j As Integer
        Dim ExistsOutArray As Boolean

        NodesOut(0) = AllNodesIn(0)

        For i = 0 To AllNodesIn.Length - 1
            ExistsOutArray = False
            For j = 0 To NodesOut.Length - 1
                If AllNodesIn(i) = NodesOut(j) Then
                    ExistsOutArray = True
                    Exit For
                End If
            Next
            If ExistsOutArray = False Then
                ReDim Preserve NodesOut(NodesOut.Length)
                NodesOut(NodesOut.Length - 1) = AllNodesIn(i)
            End If
        Next

    End Sub

    Public Shared Function FakConv(ByVal UnitIn As String) As Integer

        Select Case UnitIn
            Case "m3/s"
                Return 1000
            Case Else
                Return 1
        End Select
    End Function

#End Region 'Methoden

End Class