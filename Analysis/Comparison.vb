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
Imports MathNet.Numerics
''' <summary>
''' Plots the coincident values of two time series against each other, least-squares fits a line to the resulting points and calculates the linear correlation coefficient.
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:Gegenueberstellung</remarks>
Friend Class Comparison
    Inherits Analysis

    Private ts_x, ts_y As TimeSeries

    Public Overloads Shared Function Description() As String
        Return "Plots the coincident values of two time series against each other, least-squares fits a line to the resulting points and calculates the linear correlation coefficient."
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

        ' Dialogaufruf zur Auswahl der x-Achse
        Dim dialog As New Comparison_Dialog(Me.mZeitreihen(0).Title, Me.mZeitreihen(1).Title)

        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        ' Zuweisen der x-Achse
        Dim xachse As String
        xachse = dialog.xAchse
        If (xachse = Me.mZeitreihen(0).Title) Then
            Me.ts_x = Me.mZeitreihen(0)
            Me.ts_y = Me.mZeitreihen(1)
        Else
            Me.ts_x = Me.mZeitreihen(1)
            Me.ts_y = Me.mZeitreihen(0)
        End If

        'Remove NaN values
        Me.ts_x = Me.ts_x.removeNaNValues()
        Me.ts_y = Me.ts_y.removeNaNValues()

        'Synchronize
        TimeSeries.Synchronize(Me.ts_x, Me.ts_y)

        Dim x_values As Double()
        Dim y_values As Double()
        x_values = Me.ts_x.Values.ToArray()
        y_values = Me.ts_y.Values.ToArray()

        'Calculate linear regression
        Dim p As Tuple(Of Double, Double)
        p = Fit.Line(x_values, y_values)

        'Calculate correlation coefficient
        Dim r As Double
        r = MathNet.Numerics.GoodnessOfFit.R(y_values, x_values)

        'Store result values
        Me.mResultValues.Add("Linear regression intercept", p.Item1)
        Me.mResultValues.Add("Linear regression slope", p.Item2)
        Me.mResultValues.Add("Correlation coefficient", r)

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        Dim x_values As Double() = Me.ts_x.Values.ToArray()
        Dim y_values As Double() = Me.ts_y.Values.ToArray()
        Dim x_title As String = Me.ts_x.Title
        Dim y_title As String = Me.ts_y.Title
        Dim x_unit As String = Me.ts_x.Unit
        Dim y_unit As String = Me.ts_y.Unit

        'Text:
        '-----
        Me.mResultText = $"The analysis is based on {Me.ts_x.Length} coincident data points between {Me.ts_x.StartDate.ToString(Helpers.DefaultDateFormat)} and {Me.ts_x.EndDate.ToString(Helpers.DefaultDateFormat)}"

        'Diagramm:
        '---------
        Dim series_points As Steema.TeeChart.Styles.Points
        Dim regression_line As Steema.TeeChart.Styles.Line

        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.FormatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = $"Comparison ({x_title} / {y_title})"
        Me.mResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.mResultChart.Axes.Bottom.Title.Caption = $"{x_title}  [{x_unit}]"
        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Left.Title.Caption = $"{y_title}  [{y_unit}]"
        Me.mResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        series_points = New Steema.TeeChart.Styles.Points(Me.mResultChart)
        series_points.Title = $"Comparison {x_title} - {y_title}"
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
        series_points.Add(x_values, y_values)

        'Plot regression line
        '--------------------
        Dim intercept, slope As Double
        Dim x_min, x_max As Double
        x_min = x_values.Min()
        x_max = x_values.Max()
        intercept = Me.mResultValues("Linear regression intercept")
        slope = Me.mResultValues("Linear regression slope")
        regression_line.Add(x_min, slope * x_min + intercept)
        regression_line.Add(x_max, slope * x_max + intercept)

        'Annotation
        '----------
        Dim anno As New Steema.TeeChart.Tools.Annotation(Me.mResultChart)
        anno.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom
        anno.Text = $"Correlation coefficient: {Me.mResultValues("Correlation coefficient").ToString(DefaultNumberFormat)}" & eol
        anno.Text &= "Linear regression line: " & eol
        anno.Text &= $"y = {slope.ToString(DefaultNumberFormat)} * x + {intercept.ToString(DefaultNumberFormat)}"

    End Sub

End Class