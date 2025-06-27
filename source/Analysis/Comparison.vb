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

    Public Overrides ReadOnly Property hasResultTable() As Boolean
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
        Dim dialog As New Comparison_Dialog(Me.InputTimeSeries(0).Title, Me.InputTimeSeries(1).Title)

        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        ' Zuweisen der x-Achse
        Dim xachse As String
        xachse = dialog.xAchse
        If (xachse = Me.InputTimeSeries(0).Title) Then
            Me.ts_x = Me.InputTimeSeries(0)
            Me.ts_y = Me.InputTimeSeries(1)
        Else
            Me.ts_x = Me.InputTimeSeries(1)
            Me.ts_y = Me.InputTimeSeries(0)
        End If

        'Remove NaN values
        Me.ts_x = Me.ts_x.removeNaNValues()
        Me.ts_y = Me.ts_y.removeNaNValues()

        'Synchronize
        TimeSeries.Synchronize(Me.ts_x, Me.ts_y)

        'Check if enough data is present
        If Me.ts_x.Length < 2 Then
            Throw New Exception("Not enough coincident data points available!")
        End If

        Dim x_values As Double()
        Dim y_values As Double()
        x_values = Me.ts_x.Values.ToArray()
        y_values = Me.ts_y.Values.ToArray()

        'Calculate linear regression
        Dim p As (A As Double, B As Double)
        p = Fit.Line(x_values, y_values)

        'Calculate correlation coefficient
        Dim r As Double
        r = MathNet.Numerics.GoodnessOfFit.R(y_values, x_values)

        'Store result values
        Me.ResultValues.Add("Linear regression intercept", p.A)
        Me.ResultValues.Add("Linear regression slope", p.B)
        Me.ResultValues.Add("Correlation coefficient", r)

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
        Me.ResultText = $"The analysis is based on {Me.ts_x.Length} coincident data points between {Me.ts_x.StartDate.ToString(Helpers.CurrentDateFormat)} and {Me.ts_x.EndDate.ToString(Helpers.CurrentDateFormat)}"

        'Diagramm:
        '---------
        Dim series_points As Steema.TeeChart.Styles.Points
        Dim regression_line As Steema.TeeChart.Styles.Line

        Me.ResultChart = New Steema.TeeChart.Chart()
        Call Helpers.ChartSetDefaultFormat(Me.ResultChart)
        Me.ResultChart.Header.Text = $"Comparison ({x_title} / {y_title})"
        Me.ResultChart.Legend.Visible = False

        'Achsen
        '------
        Me.ResultChart.Axes.Bottom.Title.Caption = $"{x_title}  [{x_unit}]"
        Me.ResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.ResultChart.Axes.Left.Title.Caption = $"{y_title}  [{y_unit}]"
        Me.ResultChart.Axes.Left.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value

        'Reihen
        '------
        series_points = New Steema.TeeChart.Styles.Points(Me.ResultChart)
        series_points.Title = $"Comparison {x_title} - {y_title}"
        series_points.Pointer.Visible = True
        series_points.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        series_points.Pointer.HorizSize = 2
        series_points.Pointer.VertSize = 2

        regression_line = New Steema.TeeChart.Styles.Line(Me.ResultChart)
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
        intercept = Me.ResultValues("Linear regression intercept")
        slope = Me.ResultValues("Linear regression slope")
        regression_line.Add(x_min, slope * x_min + intercept)
        regression_line.Add(x_max, slope * x_max + intercept)

        'Annotation
        '----------
        Dim anno As New Steema.TeeChart.Tools.Annotation(Me.ResultChart)
        anno.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom
        anno.Text = $"Correlation coefficient: {Me.ResultValues("Correlation coefficient").ToString(DefaultNumberFormat)}" & eol
        anno.Text &= "Linear regression line: " & eol
        anno.Text &= $"y = {slope.ToString(DefaultNumberFormat)} * x + {intercept.ToString(DefaultNumberFormat)}"

    End Sub

End Class