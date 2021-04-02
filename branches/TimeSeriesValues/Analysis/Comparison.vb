﻿'Copyright (c) BlueM Dev Group
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
Imports MathNet.Numerics
''' <summary>
''' Plots the concurrent values of two time series against each other and least-squares fits a line to the resulting points
''' </summary>
''' <remarks>http://wiki.bluemodel.org/index.php/Wave:Gegenueberstellung</remarks>
Friend Class Comparison
    Inherits Analysis

    Private datume As IList(Of DateTime)
    Private ergebnisreihe(,) As Double ' Ergebnis der Gegenueberstellung: y-Werte der Reihe(xnummer) werden x-Achsen-Werte, y-Werte der Reihe(ynummer) werden y-Achsen-Werte  
    Private xnummer As Integer ' Nummer mit der auf mZeitreihen(i) zugegriffen wird, xnummer = Zeitreihe soll auf x-Achse
    Private ynummer As Integer ' Nummer mit der auf mZeitreihen(i) zugegriffen wird, xnummer = Zeitreihe soll auf y-Achse

    Public Overloads Shared Function Description() As String
        Return "Plots the concurrent values of two time series against each other and least-squares fits a line to the resulting points."
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
            Return True
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
            Throw New Exception("The Comparison analysis requires the selection of exactly 2 time series!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i As Integer
        Dim reihe1, reihe2 As TimeSeries

        ' Dialogaufruf zur Auswahl der x-Achse
        Dim dialog As New Comparison_Dialog(Me.mZeitreihen(0).Title, Me.mZeitreihen(1).Title)

        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        ' Zuweisen der x-Achse
        Dim xachse As String
        xachse = dialog.xAchse
        If (xachse = Me.mZeitreihen(0).Title) Then
            xnummer = 0
            ynummer = 1
        Else
            xnummer = 1
            ynummer = 0
        End If

        'Reihen säubern und zuweisen
        reihe1 = Me.mZeitreihen(xnummer).removeNaNValues()
        reihe2 = Me.mZeitreihen(ynummer).removeNaNValues()

        'Nur gemeinsame Stützstellen nutzen
        Me.ergebnisreihe = AnalysisHelper.getConcurrentValues(reihe1, reihe2)

        'Datume übernehmen
        Me.datume = reihe1.Dates

        'Calculate linear regression
        Dim p As Tuple(Of Double, Double)
        Dim xvalues(), yvalues() As Double

        'store x and y values as separate arrays
        ReDim xvalues(Me.ergebnisreihe.GetUpperBound(0))
        ReDim yvalues(Me.ergebnisreihe.GetUpperBound(0))
        For i = 0 To Me.ergebnisreihe.GetUpperBound(0)
            xvalues(i) = Me.ergebnisreihe(i, 0)
            yvalues(i) = Me.ergebnisreihe(i, 1)
        Next

        p = Fit.Line(xvalues, yvalues)

        'Store result values
        Me.mResultValues.Add("alpha", p.Item1)
        Me.mResultValues.Add("beta", p.Item2)

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Text:
        '-----
        Me.mResultText = "The analysis is based on " & Me.ergebnisreihe.GetLength(0) & " coincident data points between " & Me.datume(0).ToString(Helpers.DefaultDateFormat) & " and " & Me.datume(Me.datume.Count - 1).ToString(Helpers.DefaultDateFormat)

        'Diagramm:
        '---------
        Dim i, length As Integer
        Dim series_points As Steema.TeeChart.Styles.Points
        Dim regression_line As Steema.TeeChart.Styles.Line

        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Comparison (" & Me.mZeitreihen(xnummer).Title & " / " & Me.mZeitreihen(ynummer).Title & ")"
        Me.mResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Title.Caption = Me.mZeitreihen(xnummer).Title & " [" & Me.mZeitreihen(xnummer).Unit & "]"
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Left.Title.Caption = Me.mZeitreihen(ynummer).Title & " [" & Me.mZeitreihen(ynummer).Unit & "]"
        Me.mResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        series_points = New Steema.TeeChart.Styles.Points(Me.mResultChart)
        series_points.Title = "Comparison " & Me.mZeitreihen(xnummer).Title & " - " & Me.mZeitreihen(ynummer).Title
        series_points.Pointer.Visible = True
        series_points.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        series_points.Pointer.HorizSize = 2
        series_points.Pointer.VertSize = 2

        regression_line = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        regression_line.Title = "Regression line"
        regression_line.LinePen.Width = 2
        regression_line.LinePen.Color = Color.Red

        'Plot points
        '-----------
        Dim x, y, x_min, x_max As Double
        Dim x_values, y_values As Double()

        x_min = Double.MaxValue
        x_max = Double.MinValue

        length = Me.ergebnisreihe.GetUpperBound(0) - 1
        ReDim x_values(length - 1)
        ReDim y_values(length - 1)

        'assign values to x and y arrays (faster)
        For i = 0 To length - 1
            x = ergebnisreihe(i, 0)
            y = ergebnisreihe(i, 1)
            x_values(i) = x
            y_values(i) = y
            If x < x_min Then x_min = x
            If x > x_max Then x_max = x
        Next

        series_points.Add(x_values, y_values)

        'Plot regression line
        '--------------------
        Dim alpha, beta As Double
        alpha = Me.mResultValues("alpha")
        beta = Me.mResultValues("beta")
        regression_line.Add(x_min, beta * x_min + alpha)
        regression_line.Add(x_max, beta * x_max + alpha)

        'Annotation
        '----------
        Dim anno As New Steema.TeeChart.Tools.Annotation(Me.mResultChart)
        anno.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom
        anno.Text = "Linear regression line: " & eol
        anno.Text &= "y = " + Str(beta) + " * x + " + Str(alpha)

    End Sub

End Class