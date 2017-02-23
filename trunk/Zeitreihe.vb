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
Public Class Zeitreihe

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _title As String
    Private _nodes As SortedList(Of DateTime, Double)
    Private _Einheit As String
    Private _Objekt As String
    Private _Type As String

#End Region 'Eigenschaften

#Region "Properties"

    'Properties
    '##########

    ''' <summary>
    ''' Titel der Zeitreihe
    ''' </summary>
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property Objekt() As String
        Get
            Return _Objekt
        End Get
        Set(ByVal value As String)
            _Objekt = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    ''' <summary>
    ''' Die Stützstellen der Zeitreihe
    ''' </summary>
    Public ReadOnly Property Nodes() As SortedList(Of DateTime, Double)
        Get
            Return _nodes
        End Get
    End Property

    ''' <summary>
    ''' Die X-Werte der Zeitreihe als IList von DateTime
    ''' </summary>
    Public ReadOnly Property XWerte() As IList(Of DateTime)
        Get
            Return Me._nodes.Keys
        End Get
    End Property

    ''' <summary>
    ''' Die Y-Werte der Zeitreihe als IList von Double
    ''' </summary>
    Public ReadOnly Property YWerte() As IList(Of Double)
        Get
            Return Me._nodes.Values
        End Get
    End Property

    ''' <summary>
    ''' Die Länge (Anzahl Stützstellen) der Zeitreihe
    ''' </summary>
    Public ReadOnly Property Length() As Integer
        Get
            Return Me._nodes.Count
        End Get
    End Property

    ''' <summary>
    ''' Einheit der Zeitreihe
    ''' </summary>
    Public Property Einheit() As String
        Get
            Return _Einheit
        End Get
        Set(ByVal value As String)
            If (value.Trim() = "") Then value = "-"
            _Einheit = value
        End Set
    End Property

    ''' <summary>
    ''' Anfangsdatum
    ''' </summary>
    Public ReadOnly Property Anfangsdatum() As DateTime
        Get
            Return Me._nodes.Keys(0)
        End Get

    End Property

    ''' <summary>
    ''' Enddatum
    ''' </summary>
    Public ReadOnly Property Enddatum() As DateTime
        Get
            Return Me._nodes.Keys(Me._nodes.Count - 1)
        End Get

    End Property

    ''' <summary>
    ''' Returns the maximum value of the time series
    ''' </summary>
    Public ReadOnly Property Maximum() As Double
        Get
            Dim max As Double
            max = Double.MinValue
            For Each value As Double In Me.YWerte
                If (value > max) Then
                    max = value
                End If
            Next
            Return max
        End Get
    End Property

    ''' <summary>
    ''' Returns the minimum value of the time series
    ''' </summary>
    Public ReadOnly Property Minimum() As Double
        Get
            Dim min As Double
            min = Double.MaxValue
            For Each value As Double In Me.YWerte
                If (value < min) Then
                    min = value
                End If
            Next
            Return min
        End Get
    End Property

    ''' <summary>
    ''' Returns the average value of the time series
    ''' </summary>
    ''' <remarks>Simple average calculation without regard for time</remarks>
    Public ReadOnly Property Average() As Double
        Get
            Dim avg As Double
            avg = 0
            For Each value As Double In Me.YWerte
                avg += value
            Next
            avg = avg / Me.Length
            Return avg
        End Get
    End Property

    ''' <summary>
    ''' Returns the first value of the time series
    ''' </summary>
    Public ReadOnly Property FirstValue() As Double
        Get
            Return Me.YWerte(0)
        End Get
    End Property

    ''' <summary>
    ''' Returns the last value of the time series
    ''' </summary>
    Public ReadOnly Property LastValue() As Double
        Get
            Return Me.YWerte(Me.Length - 1)
        End Get
    End Property

    ''' <summary>
    ''' Returns the sum of the time series' values
    ''' </summary>
    Public ReadOnly Property Sum() As Double
        Get
            Dim _sum As Double
            _sum = 0.0
            For Each value As Double In Me.YWerte
                _sum += value
            Next
            Return _sum
        End Get
    End Property

    ''' <summary>
    ''' Returns the volume of the time series (values integrated over time)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>If the unit is per second (ends with "/s"), values are integrated over time using a linear interpretation. 
    ''' Otherwise, a simple summation is performed.</remarks>
    Public ReadOnly Property Volume() As Double
        Get
            Dim v0, v1, vol As Double
            Dim t0, t1 As DateTime
            Dim dt As TimeSpan

            vol = 0.0
            If Me.Einheit.ToLower.EndsWith("/s") Then
                Log.AddLogEntry(Me.Title & ": calculating volume by integrating over time.")
                t0 = Me.Anfangsdatum
                v0 = Me.Nodes(t0)
                For Each node As KeyValuePair(Of Date, Double) In Me.Nodes
                    t1 = node.Key
                    v1 = node.Value
                    If t1 > t0 Then 'start at the second node
                        dt = t1 - t0
                        vol += (v0 + v1) / 2 * dt.TotalSeconds 'linear interpretation!
                    End If
                    t0 = t1
                    v0 = v1
                Next
            Else
                'simple sum
                Log.AddLogEntry(Me.Title & ": calculating volume by simple summation.")
                vol = Me.Sum
            End If

            Return vol
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"

    'Methoden
    '########

    ''' <summary>
    ''' Default Konstruktor
    ''' </summary>
    Public Sub New()
        Me._title = "-"
        Me._Einheit = "-"
        Me._Objekt = "-"
        Me._Type = "-"
        Me._nodes = New SortedList(Of DateTime, Double)
    End Sub

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="title">Titel der Zeitreihe</param>
    Public Sub New(ByVal title As String)
        Me._title = title
        Me._Einheit = "-"
        Me._Objekt = "-"
        Me._Type = "-"
        Me._nodes = New SortedList(Of DateTime, Double)
    End Sub

    ''' <summary>
    ''' Gibt den Zeitreihen-Titel zurück
    ''' </summary>
    Public Overrides Function ToString() As String
        Return Me.Title
    End Function

    ''' <summary>
    ''' Zeitreihe kopieren
    ''' </summary>
    Public Function Clone() As Zeitreihe
        Dim target As New Zeitreihe(Me.Title)
        target.Einheit = Me.Einheit
        target.Objekt = Me.Objekt
        target.Type = Me.Type
        target._nodes = New SortedList(Of DateTime, Double)(Me._nodes)
        Return target
    End Function

    ''' <summary>
    ''' Fügt der Zeitreihe eine Stützstelle hinzu
    ''' </summary>
    ''' <param name="datum">Datum</param>
    ''' <param name="wert">Wert</param>
    ''' <remarks>Das Datum der Stützstelle darf in der Zeitreihe noch nicht vorhanden sein</remarks>
    Public Sub AddNode(ByVal datum As DateTime, ByVal wert As Double)
        If (Me.Nodes.ContainsKey(datum)) Then
            Throw New Exception("Error: duplicate data point on " & datum.ToString(Konstanten.Datumsformate("default")) & "!")
        End If
        Me._nodes.Add(datum, wert)
    End Sub

    ''' <summary>
    ''' Zeitreihe zuschneiden
    ''' </summary>
    ''' <param name="anfang">Anfangsdatum</param>
    ''' <param name="ende">Enddatum</param>
    ''' <remarks>Wenn Anfangs- und/oder Enddatum nicht genau als Stützstellen vorliegen, wird an den nächstaußeren Stützstellen abgeschnitten</remarks>
    Public Overloads Sub Cut(ByVal anfang As DateTime, ByVal ende As DateTime)

        Dim i, lengthOld, lengthNew As Integer
        Dim iAnfang, iEnde As Integer
        Dim newNodes As SortedList(Of DateTime, Double)

        If (Me.Anfangsdatum < anfang Or Me.Enddatum > ende) Then

            Me.Title &= " (cut)"

            lengthOld = Me.Length

            'Anfang finden
            iAnfang = Me._nodes.IndexOfKey(anfang)

            If (iAnfang = -1) Then
                'Anfang liegt nicht exakt als Stützstelle vor:
                'davorliegende Stützstelle nehmen
                For i = 1 To Me.Length - 1
                    If (Me.XWerte(i) > anfang) Then
                        iAnfang = i - 1
                        'neuer Anfang:
                        anfang = Me.XWerte(i - 1)
                        Exit For
                    End If
                Next
            End If

            'Ende finden
            iEnde = Me._nodes.IndexOfKey(ende)

            'Ende liegt nicht exakt als Stützstelle vor:
            'dahinterliegende Stützstelle nehmen
            If (iEnde = -1) Then
                For i = Me.Length - 2 To 0 Step -1
                    If (Me.XWerte(i) < ende) Then
                        iEnde = i + 1
                        'neues Ende:
                        ende = Me.XWerte(i + 1)
                        Exit For
                    End If
                Next
            End If

            newNodes = New SortedList(Of DateTime, Double)

            For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
                If (node.Key >= anfang And node.Key <= ende) Then
                    newNodes.Add(node.Key, node.Value)
                End If
            Next

            lengthNew = newNodes.Count

            Me._nodes = newNodes

            'Überflüssige Kapazität entfernen
            Call Me.Nodes.TrimExcess()

            'Log 
            Call Log.AddLogEntry(Me.Title & ": cut from " & lengthOld.ToString() & " to " & lengthNew.ToString() & " data points.")

        End If

    End Sub

    ''' <summary>
    ''' Schneidet die Zeitreihe auf den Zeitraum einer 2. Zeitreihe zu
    ''' </summary>
    ''' <param name="zre2">die 2. Zeitreihe</param>
    ''' <remarks></remarks>
    Public Overloads Sub Cut(ByVal zre2 As Zeitreihe)

        If (Me.Anfangsdatum < zre2.Anfangsdatum Or Me.Enddatum > zre2.Enddatum) Then
            Call Me.Cut(zre2.Anfangsdatum, zre2.Enddatum)
        End If

    End Sub

    ''' <summary>
    ''' Einen Wert aus einer Zeitreihe berechnen
    ''' </summary>
    ''' <param name="WertTyp">MaxWert, MinWert, Average, AnfWert, EndWert, Summe</param>
    ''' <returns>der berechnete Wert</returns>
    ''' <remarks>Obsolete, kept for backwards compatibility with BlueM.Opt</remarks>
    Public Function getWert(ByVal WertTyp As String) As Double
        Dim Wert As Double

        Select Case WertTyp

            Case "MaxWert"
                Wert = Me.Maximum

            Case "MinWert"
                Wert = Me.Minimum

            Case "Average"
                Wert = Me.Average

            Case "AnfWert"
                Wert = Me.FirstValue

            Case "EndWert"
                Wert = Me.LastValue

            Case "Summe"
                Wert = Me.Sum

            Case Else
                Throw New Exception("Der Werttyp '" & WertTyp & "' wird nicht unterstützt!")

        End Select

    End Function

    ''' <summary>
    ''' Erstellt eine neue äquidistante Zeitreihe, neue Stützstellen kriegen den Wert 0
    ''' </summary>
    ''' <param name="Soll_dT">Sollzeitschritt (in Minuten)</param>      
    Public Function getKontiZRE(ByVal Soll_dT As Integer) As Zeitreihe

        Dim i As Integer
        Dim intloop As Integer
        Dim Ist_dT As Integer
        Dim AnzZusWerte As Integer
        Dim SumZusWerte As Long

        Dim OutZR As New Zeitreihe("Konti_" & Me.Title)
        OutZR.Einheit = Me.Einheit

        SumZusWerte = 0
        For i = 0 To Me.Length - 2

            AnzZusWerte = 0
            Ist_dT = DateDiff(DateInterval.Minute, Me.XWerte(i), Me.XWerte(i + 1))

            If (Ist_dT - Soll_dT > 0) Then
                AnzZusWerte = (Ist_dT / Soll_dT) - 1
                SumZusWerte = SumZusWerte + AnzZusWerte
                OutZR.AddNode(Me.XWerte(i), Me.YWerte(i))
                For intloop = 1 To AnzZusWerte
                    OutZR.AddNode(XWerte(i).AddMinutes(intloop * Soll_dT), 0.0)
                Next
            Else
                OutZR.AddNode(Me.XWerte(i), Me.YWerte(i))
            End If
        Next

        'letzten Wert schreiben
        OutZR.AddNode(Me.XWerte(i), Me.YWerte(i))

        Return OutZR

    End Function

    ''' <summary>
    ''' Erstellt eine neue äquidistante Zeitreihe, neue Stützstellen kriegen aus original Zeitreihe konvertierten Wert, geignet für Massenbezogenen Zeitreihen
    ''' </summary>
    ''' <param name="Soll_dT">Sollzeitschritt (in Minuten)</param>      
    Public Function getKontiZRE2(ByVal Soll_dT As Integer) As Zeitreihe

        Dim i, j As Integer
        Dim intloop As Integer
        Dim n_dT As Integer
        Dim NewNodes As Integer
        Dim newValue As Double
        Dim sumValues As Double

        Dim TempZR As New Zeitreihe("Temp_" & Me.Title)
        Dim OutZR As New Zeitreihe("Konti_" & Me.Title)
        OutZR.Einheit = Me.Einheit

        'Zuerst wird eine Zeitreihe auf der Basis von Minutenwerten erstellt (kleinst mögliche Einheit)
        For i = 0 To Me.Length - 2

            NewNodes = 0
            n_dT = DateDiff(DateInterval.Minute, Me.XWerte(i), Me.XWerte(i + 1))
            NewNodes = n_dT

            If NewNodes > 1 Then
                newValue = Me.YWerte(i) / NewNodes
                TempZR.AddNode(Me.XWerte(i), newValue)
                For intloop = 1 To NewNodes - 1
                    TempZR.AddNode(XWerte(i).AddMinutes(intloop), newValue)
                Next
            Else
                TempZR.AddNode(Me.XWerte(i), Me.YWerte(i))
            End If
        Next

        TempZR.AddNode(Me.XWerte(i), Me.YWerte(i))


        'Zeitreihe mit neuer Schrittweite wird generiert

        'Die Zeitreihe sollte mindestens einen Sollzeitschritt umfassen
        If TempZR.Length < Soll_dT Then
            Throw New Exception("Die Zeitreihe umfasst nicht genug Werte für die definierte Zeitschrittlänge")
        End If
        'Abarbeiten aller Werte die innerhalb ganzer Sollzeitschritte liegen.
        i = 0
        Do While i <= TempZR.Length - Soll_dT
            j = 0
            sumValues = 0
            For j = 0 To Soll_dT - 1
                sumValues += TempZR.YWerte(i + j)
            Next
            OutZR.AddNode(TempZR.XWerte(i), sumValues)
            i += Soll_dT
        Loop

        'Aufaddieren des Rests
        If i Mod 5 <> 0 Then
            sumValues = 0
            For j = i To TempZR.Length - 1
                sumValues += TempZR.YWerte(j)
            Next

            'letzten Wert schreiben
            OutZR.AddNode(TempZR.XWerte(i), sumValues)

        End If

        Return OutZR


    End Function

    ''' <summary>
    ''' Erstellt eine neue äquidistante Zeitreihe, neue Stützstellen kriegen aus original Zeitreihe konvertierten Wert, geignet für zeitabhängige Zeitreihen
    ''' </summary>
    ''' <param name="Soll_dT">Sollzeitschritt (in Minuten)</param>      
    Public Function getKontiZRE3(ByVal Soll_dT As Integer) As Zeitreihe

        Dim i As Integer
        Dim intloop As Integer
        Dim n_dT As Integer
        Dim NewNodes As Integer
        Dim newValue As Double

        Dim TempZR As New Zeitreihe("Temp_" & Me.Title)
        Dim OutZR As New Zeitreihe("Konti_" & Me.Title)
        OutZR.Einheit = Me.Einheit

        'Zuerst wird eine Zeitreihe auf der Basis von Minutenwerten erstellt (kleinst mögliche Einheit)
        For i = 0 To Me.Length - 2

            NewNodes = 0
            n_dT = DateDiff(DateInterval.Minute, Me.XWerte(i), Me.XWerte(i + 1))
            NewNodes = n_dT

            If NewNodes > 1 Then
                newValue = Me.YWerte(i)
                TempZR.AddNode(Me.XWerte(i), newValue)
                For intloop = 1 To NewNodes - 1
                    TempZR.AddNode(XWerte(i).AddMinutes(intloop), newValue)
                Next
            Else
                TempZR.AddNode(Me.XWerte(i), Me.YWerte(i))
            End If
        Next

        TempZR.AddNode(Me.XWerte(i), Me.YWerte(i))


        'Zeitreihe mit neuer Schrittweite wird generiert

        'Die Zeitreihe sollte mindestens einen Sollzeitschritt umfassen
        If TempZR.Length < Soll_dT Then
            Throw New Exception("Die Zeitreihe umfasst nicht genug Werte für die definierte Zeitschrittlänge")
        End If
        'Abarbeiten aller Werte die innerhalb ganzer Sollzeitschritte liegen.
        i = 0
        Do While i <= TempZR.Length - Soll_dT
            'j = 0
            'sumValues = 0
            'For j = 0 To Soll_dT - 1
            '    sumValues += TempZR.YWerte(i + j)
            'Next
            'newValue = sumValues / Soll_dT
            'OutZR.AddNode(TempZR.XWerte(i), newValue)
            'i += Soll_dT
            OutZR.AddNode(TempZR.XWerte(i), TempZR.YWerte(i))
            i += Soll_dT
        Loop

        'Aufaddieren des Rests
        If i Mod 5 <> 0 Then
            'sumValues = 0
            'For j = i To TempZR.Length - 1
            '    sumValues += TempZR.YWerte(j)
            'Next
            'newValue = sumValues / Soll_dT
            ''letzten Wert schreiben
            'OutZR.AddNode(TempZR.XWerte(i), sumValues)
            OutZR.AddNode(TempZR.XWerte(i), TempZR.YWerte(i))
        End If

        Return OutZR


    End Function

    ''' <summary>
    ''' Creates a copy of the time series from which all nodes with values of NaN or +-Infinity have been removed.
    ''' </summary>
    ''' <param name="errorvalues">optional additional error values to ignore</param>
    ''' <returns>the cleaned time series</returns>
    ''' <remarks>a tolerance of 0.0001 is used to compare series values to errorvalues</remarks>
    Public Function getCleanZRE(ByVal ParamArray errorvalues() As Double) As Zeitreihe

        Const tolerance As Double = 0.0001
        Dim isErrorvalue As Boolean
        Dim errorCount As Integer
        Dim newnodes As SortedList(Of DateTime, Double)
        Dim cleanZRE As Zeitreihe

        'Instantiate a new series
        cleanZRE = New Zeitreihe(Me.Title)
        cleanZRE.Einheit = Me.Einheit
        cleanZRE.Objekt = Me.Objekt
        cleanZRE.Type = Me.Type

        Log.AddLogEntry(String.Format("Removing error values from series {0}...", Me.Title))

        newnodes = New SortedList(Of DateTime, Double)()
        errorCount = 0

        For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
            'Check if is error value
            isErrorvalue = False
            If (Double.IsNaN(node.Value) Or _
                Double.IsInfinity(node.Value)) Then
                isErrorvalue = True
            Else
                For Each errvalue As Double In errorvalues
                    If Math.Abs(node.Value - errvalue) < tolerance Then
                        isErrorvalue = True
                        Exit For
                    End If
                Next
            End If

            If isErrorvalue Then
                'skip the node
                errorCount += 1
                Call Log.AddLogEntry(String.Format("Removing node at {0} with value {1}", node.Key, node.Value))
            Else
                'copy the node
                newnodes.Add(node.Key, node.Value)
            End If
        Next

        'Log
        Call Log.AddLogEntry(Me.Title & ": " & errorCount.ToString() & " nodes were removed!")

        'assign nodes to clean series
        cleanZRE._nodes = newnodes

        Return cleanZRE

    End Function

#End Region 'Methoden

End Class
