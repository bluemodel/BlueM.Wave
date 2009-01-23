Public Class Zeitreihe

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _title As String
    Private _nodes As SortedList(Of DateTime, Double)
    Private _Einheit As String
   
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

    ''' <summary>
    ''' Die St�tzstellen der Zeitreihe
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
    ''' Die L�nge (Anzahl St�tzstellen) der Zeitreihe
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
         Return Me._nodes.Keys(Me._nodes.Count -1)
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
        Me._nodes = New SortedList(Of DateTime, Double)
    End Sub

   ''' <summary>
   ''' Konstruktor
   ''' </summary>
   ''' <param name="title">Titel der Zeitreihe</param>
    Public Sub New(ByVal title As String)
        Me._title = title
        Me._Einheit = "-"
        Me._nodes = New SortedList(Of DateTime, Double)
    End Sub

   ''' <summary>
   ''' Gibt den Zeitreihen-Titel zur�ck
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
        target._nodes = New SortedList(Of DateTime, Double)(Me._nodes)
        Return target
    End Function

    ''' <summary>
    ''' F�gt der Zeitreihe eine St�tzstelle hinzu
    ''' </summary>
    ''' <param name="datum">Datum</param>
    ''' <param name="wert">Wert</param>
    ''' <remarks>Das Datum der St�tzstelle darf in der Zeireihe noch nicht vorhanden sein</remarks>
    Public Sub AddNode(ByVal datum As DateTime, ByVal wert As Double)
        If (Me.Nodes.ContainsKey(datum)) Then
            Throw New Exception("Fehler: Die Zeitreihe enth�lt bereits eine St�tzstelle am " & datum.ToString(Konstanten.Datumsformat) & "!")
        End If
        Me._nodes.Add(datum, wert)
    End Sub

    ''' <summary>
    ''' Zeitreihe zuschneiden
    ''' </summary>
    ''' <param name="anfang">Anfangsdatum</param>
    ''' <param name="ende">Enddatum</param>
    ''' <remarks>Wenn Anfangs- und/oder Enddatum nicht genau als St�tzstellen vorliegen, wird an den n�chstau�eren St�tzstellen abgeschnitten</remarks>
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
                'Anfang liegt nicht exakt als St�tzstelle vor:
                'davorliegende St�tzstelle nehmen
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

            'Ende liegt nicht exakt als St�tzstelle vor:
            'dahinterliegende St�tzstelle nehmen
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

            '�berfl�ssige Kapazit�t entfernen
            Call Me.Nodes.TrimExcess()

            'Log 
            Call Log.AddLogEntry("Zeitreihe '" & Me.Title & "' von " & lengthOld.ToString() & " auf " & lengthNew.ToString() & " St�tzstellen gek�rzt.")

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
    Public Function getWert(ByVal WertTyp As String) As Double

        Dim Wert As Double

        Select Case WertTyp

            Case "MaxWert"
                Wert = Double.MinValue
                For Each value As Double In Me.YWerte
                    If (value > Wert) Then
                        Wert = value
                    End If
                Next

            Case "MinWert"
                Wert = Double.MaxValue
                For Each value As Double In Me.YWerte
                    If (value < Wert) Then
                        Wert = value
                    End If
                Next

            Case "Average"
                Wert = 0
                For Each value As Double In Me.YWerte
                    Wert += value
                Next
                Wert = Wert / Me.Length

            Case "AnfWert"
                Wert = Me.YWerte(0)

            Case "EndWert"
                Wert = Me.YWerte(Me.Length - 1)

            Case "Summe"
                Wert = 0
                For Each value As Double In Me.YWerte
                    Wert += value
                Next

            Case Else
                Throw New Exception("Der Werttyp '" & WertTyp & "' wird nicht unterst�tzt!")

        End Select

        Return Wert

    End Function

    ''' <summary>
    ''' Erstellt eine neue �quidistante Zeitreihe
    ''' </summary>
    ''' <param name="Soll_dT">Sollzeitschritt (in Minuten)</param>      
    Public Function MakeKontiZeitreihe(ByVal Soll_dT As Integer) As KontiZeitreihe

        Dim i As Integer
        Dim intloop As Integer
        Dim Ist_dT As Integer
        Dim AnzZusWerte As Integer
        Dim SumZusWerte As Long

        Dim OutZR As New KontiZeitreihe("Konti_" & Me.Title)
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
    ''' S�ubert die Zeitreihe, in dem alle St�tzstellen vom Wert NaN (-999) entfernt werden
    ''' </summary>
    Public Sub Clean()

        Dim keysToBeRemoved As New List(Of DateTime)()

        If (Me.YWerte.Contains(Konstanten.NaN)) Then

            Me.Title &= " (clean)"

            'NaN-St�tzstellen finden
            For Each node As KeyValuePair(Of DateTime, Double) In Me.Nodes
                If (node.Value = Konstanten.NaN) Then
                    keysToBeRemoved.Add(node.Key)
                End If
            Next

            'NaN-St�tzstellen entfernen
            For Each key As DateTime In keysToBeRemoved
                Me.Nodes.Remove(key)
            Next

            '�berfl�ssige Kapazit�t entfernen
            Call Me.Nodes.TrimExcess()

            'Log
            Call Log.AddLogEntry("... " & Me.Title & ": " & keysToBeRemoved.Count.ToString() & " NaN-Werte (" & Konstanten.NaN.ToString() & ") wurden bereinigt.")

        End If

    End Sub

#End Region 'Methoden

End Class