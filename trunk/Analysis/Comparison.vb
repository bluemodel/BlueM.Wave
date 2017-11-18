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
Imports MathNet.Numerics
''' <summary>
''' Gegenüberstellung/Vergleich zweier Zeitreihen
''' </summary>
''' <remarks>http://wiki.bluemodel.org/index.php/Wave:Gegenueberstellung</remarks>
Public Class Comparison
    Inherits Analysis

    Dim datume As IList(Of DateTime)
    Dim ergebnisreihe(,) As Double ' Ergebnis der Gegenueberstellung: y-Werte der Reihe(xnummer) werden x-Achsen-Werte, y-Werte der Reihe(ynummer) werden y-Achsen-Werte  
    Dim xnummer As Integer ' Nummer mit der auf mZeitreihen(i) zugegriffen wird, xnummer = Zeitreihe soll auf x-Achse
    Dim ynummer As Integer ' Nummer mit der auf mZeitreihen(i) zugegriffen wird, xnummer = Zeitreihe soll auf y-Achse

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
        Dim values(,), xvalues(), yvalues() As Double

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
        reihe1 = Me.mZeitreihen(xnummer).getCleanZRE()
        reihe2 = Me.mZeitreihen(ynummer).getCleanZRE()

        'Nur gemeinsame Stützstellen nutzen
        values = AnalysisHelper.getConcurrentValues(reihe1, reihe2)

        ' Ergebnisreihe allokieren
        ReDim Me.ergebnisreihe(values.GetUpperBound(0), 1)
        ReDim xvalues(values.GetUpperBound(0))
        ReDim yvalues(values.GetUpperBound(0))

        ' x- und y-Werte der Ergebnisreihe zuweisen
        For i = 0 To values.GetUpperBound(0)
            ergebnisreihe(i, 0) = values(i, 0)
            ergebnisreihe(i, 1) = values(i, 1)
            xvalues(i) = values(i, 0)
            yvalues(i) = values(i, 1)
        Next

        'Datume übernehmen (werden später für Punkte-Labels im Diagramm gebraucht)
        datume = reihe1.Dates

        'Calculate linear regression
        Dim p As Tuple(Of Double, Double)
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
        Dim i, ende As Integer
        Dim gegenueberstellung_linie, regression_line As Steema.TeeChart.Styles.Line

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
        gegenueberstellung_linie = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        gegenueberstellung_linie.Title = "Comparison " & Me.mZeitreihen(xnummer).Title & " - " & Me.mZeitreihen(ynummer).Title
        gegenueberstellung_linie.Pointer.Visible = True
        gegenueberstellung_linie.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
        gegenueberstellung_linie.Pointer.HorizSize = 2
        gegenueberstellung_linie.Pointer.VertSize = 2

        regression_line = New Steema.TeeChart.Styles.Line(Me.mResultChart)
        regression_line.Title = "Regression line"
        regression_line.LinePen.Width = 2
        regression_line.LinePen.Color = Color.Red

        'assign values to series
        Dim alpha, beta, x, y As Double
        alpha = Me.mResultValues("alpha")
        beta = Me.mResultValues("beta")

        ende = (ergebnisreihe.Length / 2 - 2)
        For i = 0 To ende
            x = ergebnisreihe(i, 0)
            y = ergebnisreihe(i, 1)
            gegenueberstellung_linie.Add(x, y, datume(i).ToString(Helpers.DefaultDateFormat))
            regression_line.Add(x, beta * x + alpha)
        Next

        'Markstips
        '---------
        Dim markstips As New Steema.TeeChart.Tools.MarksTip(Me.mResultChart)
        markstips.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        markstips.Style = Steema.TeeChart.Styles.MarksStyles.Label
        markstips.Series = gegenueberstellung_linie
        gegenueberstellung_linie.Cursor = Cursors.Help

        'Annotation
        '----------
        Dim anno As New Steema.TeeChart.Tools.Annotation(Me.mResultChart)
        anno.Position = Steema.TeeChart.Tools.AnnotationPositions.RightBottom
        anno.Text = "Linear regression line: " & eol
        anno.Text &= "y = " + Str(beta) + " * x + " + Str(alpha)

    End Sub

End Class