'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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
''' Plots the cumulative coincident values of two time series against each other and adds a 45° line to the resulting plot
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:Doppelsummenanalyse</remarks>
Friend Class DoubleSumCurve
    Inherits Analysis

    Private ts_1, ts_2 As TimeSeries
    Private summe1(), summe2() As Double

    Public Overloads Shared Function Description() As String
        Return "Plots the cumulative coincident values of two time series against each other and adds a 45° line to the resulting plot."
    End Function

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
    ''' Flag indicating whether the analysis function has result series
    ''' that should be added to the main diagram
    ''' </summary>
    Public Overrides ReadOnly Property hasResultSeries() As Boolean
        Get
            Return False
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

        Dim i, n As Integer

        'assign timeseries and remove NaN values
        Me.ts_1 = Me.mZeitreihen(0).removeNaNValues()
        Me.ts_2 = Me.mZeitreihen(1).removeNaNValues()

        'synchronize
        TimeSeries.Synchronize(Me.ts_1, Me.ts_2)

        n = Me.ts_1.Length

        ReDim Me.summe1(n - 1)
        ReDim Me.summe2(n - 1)

        'Erster Wert
        Me.summe1(0) = Me.ts_1.FirstValue
        Me.summe2(0) = Me.ts_2.FirstValue
        'Weitere Werte kumulativ aufsummieren
        For i = 1 To n - 1
            Me.summe1(i) = Me.summe1(i - 1) + Me.ts_1.Values(i)
            Me.summe2(i) = Me.summe2(i - 1) + Me.ts_2.Values(i)
        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Text:
        '-----
        Me.mResultText = $"The analysis is based on {Me.summe1.Length} coincident data points between {Me.ts_1.StartDate.ToString(Helpers.DefaultDateFormat)} and {Me.ts_1.EndDate.ToString(DefaultDateFormat)}"

        'Diagramm:
        '---------
        Dim doppelsumme, gerade As Steema.TeeChart.Styles.Line

        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = $"Double Sum Curve ({Me.ts_1.Title} / {Me.ts_2.Title})"
        Me.mResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Title.Caption = $"Sum {Me.ts_1.Title}"
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Left.Title.Caption = $"Sum {Me.ts_2.Title}"
        Me.mResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        doppelsumme = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        doppelsumme.Title = $"Double Sum Curve {Me.ts_1.Title} - {Me.ts_2.Title}"
        doppelsumme.Pointer.Visible = True
        doppelsumme.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        doppelsumme.Pointer.HorizSize = 2
        doppelsumme.Pointer.VertSize = 2

        gerade = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        gerade.Title = "45° line"
        gerade.Color = Color.DarkGray
        gerade.LinePen.Style = Drawing2D.DashStyle.Dash

        'Werte eintragen
        doppelsumme.Add(summe1, summe2)

        gerade.Add(0, 0)
        Dim maxwert As Double = Math.Min(Me.mResultChart.Axes.Bottom.MaxXValue, Me.mResultChart.Axes.Left.MaxYValue)
        gerade.Add(maxwert, maxwert)

    End Sub

End Class
