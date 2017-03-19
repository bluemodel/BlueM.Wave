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
''' Doppelsummenanalyse zweier Zeitreihen
''' </summary>
''' <remarks>http://wiki.bluemodel.org/index.php/Wave:Doppelsummenanalyse</remarks>
Public Class DoubleSumCurve
    Inherits Analysis

    Dim summe1(), summe2() As Double
    Dim datume As IList(Of DateTime)

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion einen Ergebnistext erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion Ergebniswerte erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion ein Ergebnisdiagramm erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultChart() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">zu analysierende Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As List(Of TimeSeries))

        Call MyBase.New(zeitreihen)

        'Prüfung: genau 2 Zeitreihen erlaubt
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("The Double Sum Curve analysis requires the selection of exactly 2 time series!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i As Integer
        Dim zre1, zre2 As TimeSeries
        Dim values(,) As Double

        zre1 = Me.mZeitreihen(0).getCleanZRE()
        zre2 = Me.mZeitreihen(1).getCleanZRE()

        'nur gemeinsame Stützstellen nutzen
        values = AnalysisHelper.getConcurrentValues(zre1, zre2)

        ReDim Me.summe1(values.GetUpperBound(0))
        ReDim Me.summe2(values.GetUpperBound(0))

        'Erster Wert
        Me.summe1(0) = values(0, 0)
        Me.summe2(0) = values(0, 1)

        'Weitere Werte kumulativ aufsummieren
        For i = 1 To values.GetUpperBound(0)
            Me.summe1(i) = values(i, 0) + summe1(i - 1)
            Me.summe2(i) = values(i, 1) + summe2(i - 1)
        Next

        'Datume übernehmen (werden später für Punkte-Labels im Diagramm gebraucht)
        datume = zre1.Dates

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Text:
        '-----
        Me.mResultText = "Double Sum Curve:" & eol _
                        & eol _
                        & "The analysis is based on " & Me.summe1.Length & " coincident data points between " & Me.datume(0).ToString(Datumsformate("default")) & " and " & Me.datume(Me.datume.Count - 1).ToString(Datumsformate("default")) & eol _
                        & eol

        'Diagramm:
        '---------
        Dim i As Integer
        Dim doppelsumme, gerade As Steema.TeeChart.Styles.Line

        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Double Sum Curve (" & Me.mZeitreihen(0).Title & " / " & Me.mZeitreihen(1).Title & ")"
        Me.mResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Title.Caption = "Sum " & Me.mZeitreihen(0).Title
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Left.Title.Caption = "Sum " & Me.mZeitreihen(1).Title
        Me.mResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        doppelsumme = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        doppelsumme.Title = "Double Sum Curve " & Me.mZeitreihen(0).Title & " - " & Me.mZeitreihen(1).Title
        doppelsumme.Pointer.Visible = True
        doppelsumme.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        doppelsumme.Pointer.HorizSize = 2
        doppelsumme.Pointer.VertSize = 2

        gerade = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        gerade.Title = "45° line"
        gerade.Color = Color.DarkGray
        gerade.linepen.Style = Drawing2D.DashStyle.Dash

        'Werte eintragen
        For i = 0 To summe1.Length - 1
            doppelsumme.Add(summe1(i), summe2(i), datume(i).ToString(Konstanten.Datumsformate("default")))
        Next

        gerade.Add(0, 0)
        Dim maxwert As Double = Math.Min(Me.mResultChart.Axes.Bottom.MaxXValue, Me.mResultChart.Axes.Left.MaxYValue)
        gerade.Add(maxwert, maxwert)

        'Markstips
        '---------
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.mResultChart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        markstips.Style = Steema.TeeChart.Styles.MarksStyles.Label
        markstips.Series = doppelsumme
        doppelsumme.Cursor = Cursors.Help

    End Sub

End Class
