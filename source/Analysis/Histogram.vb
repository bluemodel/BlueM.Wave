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
Imports BlueM.Wave.AnnualStatistics
''' <summary>
''' Histogram and density estimation
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:Histogram</remarks>
Friend Class Histogram
    Inherits Analysis

    Private n_breaks As Integer
    Private n_bins As Integer
    Private breaks As Double()

    Private Structure histogramResults
        Dim title As String
        Dim frequency, cumfrequency As Integer()
        Dim amount As Integer
        Dim probability As Double()
        Dim PU As Double() ' probability of non-exceedance
    End Structure

    Private results As histogramResults()

    Public Overloads Shared Function Description() As String
        Return "Divides the entire range of values into a series of user-defined intervals (bins) and calculates the percentage of values falling into each interval." &
            "Also estimates a corresponding density function."
    End Function

    ''' <summary>
    ''' Flag, der anzeigt, ob die Analysefunktion einen Ergebnistext erzeugt
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
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

    Public Overrides ReadOnly Property hasResultTable() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="zeitreihen">zu analysierende Zeitreihen</param>
    Public Sub New(ByRef zeitreihen As List(Of TimeSeries))

        'Konstruktor der Basisklasse aufrufen!
        Call MyBase.New(zeitreihen)

        Dim einheit As String
        Dim histogramDlg As HistogramDialog
        Dim dlgResult As DialogResult

        'Check: series have to use the same unit
        If (zeitreihen.Count > 1) Then
            einheit = zeitreihen(1).Unit
            For Each zre As TimeSeries In zeitreihen
                If (zre.Unit <> einheit) Then
                    Throw New Exception("Please select only series with the same unit!")
                End If
            Next
        End If

        ReDim Me.results(zeitreihen.Count - 1)

        'Show HistogramDialog
        histogramDlg = New HistogramDialog(Me.InputTimeSeries)
        dlgResult = histogramDlg.ShowDialog()
        If dlgResult <> DialogResult.OK Then
            Throw New Exception("Cancelled by user")
        End If

        'store parameters
        Me.n_bins = histogramDlg.n_bins
        Me.n_breaks = Me.n_bins + 1
        Me.breaks = histogramDlg.breaks

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i, j, n As Integer

        'Analyse series
        '--------------
        n = 0
        For Each zre As TimeSeries In Me.InputTimeSeries

            With Me.results(n)

                'title
                .title = zre.Title

                'determine frequencies
                '---------------------
                ReDim .frequency(n_bins - 1)
                'loop through series values
                For i = 0 To zre.Length - 1
                    Dim value As Double
                    'assign to bin
                    value = zre.Values(i)
                    If value = Me.breaks(0) Then
                        'add to first bin
                        .frequency(0) += 1
                    Else
                        For j = 0 To n_breaks - 2
                            If value > Me.breaks(j) And value <= Me.breaks(j + 1) Then
                                'correct bin found, add to frequency
                                .frequency(j) += 1
                                Exit For
                            End If
                        Next
                    End If
                Next

                'Total amount
                .amount = 0
                For i = 0 To Me.n_bins - 1
                    .amount += .frequency(i)
                Next

                'Probability
                ReDim .probability(Me.n_bins - 1)
                For i = 0 To Me.n_bins - 1
                    .probability(i) = .frequency(i) / .amount * 100 '%
                Next

                'Cumulative frequency
                ReDim .cumfrequency(Me.n_bins - 1)
                .cumfrequency(0) = .frequency(0)
                For i = 1 To Me.n_bins - 1
                    .cumfrequency(i) = .cumfrequency(i - 1) + .frequency(i)
                Next

                'Probability of non-exceedance
                ReDim .PU(n_bins - 1)
                For i = 0 To Me.n_bins - 1
                    .PU(i) = .cumfrequency(i) / .amount * 100 '%
                Next
            End With
            n += 1
        Next


    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'result table
        Me.ResultTable = New DataTable($"Histogram: {String.Join(", ", Me.InputTimeSeries)}")

        Me.ResultTable.Columns.Add("From", GetType(Double))
        Me.ResultTable.Columns.Add("To", GetType(Double))
        For Each result As histogramResults In Me.results
            Me.ResultTable.Columns.Add($"{result.title}: frequency", GetType(Integer))
            Me.ResultTable.Columns.Add($"{result.title}: probability [%]", GetType(Double))
        Next

        For i As Integer = 0 To Me.n_bins - 1
            Dim row(Me.ResultTable.Columns.Count - 1) As Object
            row(0) = Me.breaks(i)
            row(1) = Me.breaks(i + 1)
            Dim cellindex As Integer = 2
            For Each result As histogramResults In Me.results
                row(cellindex) = result.frequency(i)
                row(cellindex + 1) = result.probability(i)
                cellindex += 2
            Next
            Me.ResultTable.Rows.Add(row)
        Next

        'Ergebniswerte
        '-------------
        'TODO: Ergebniswerte zurückgeben?

        'Ergebnisdiagramm
        '----------------

        'Diagramm formatieren
        Me.ResultChart = New Steema.TeeChart.Chart()
        Call Helpers.ChartSetDefaultFormat(Me.ResultChart)
        Me.ResultChart.Header.Text = "Histogram"

        'Achsen
        Me.ResultChart.Axes.Left.Title.Caption = "Probability [%]"
        Me.ResultChart.Axes.Left.Automatic = False
        Me.ResultChart.Axes.Left.Minimum = 0
        Me.ResultChart.Axes.Left.AutomaticMaximum = True
        Me.ResultChart.Axes.Left.MaximumOffset = 2

        Me.ResultChart.Axes.Right.Visible = True
        Me.ResultChart.Axes.Right.Title.Caption = "Probability of non-exceedance [%]"
        Me.ResultChart.Axes.Right.Title.Angle = 90
        Me.ResultChart.Axes.Right.Automatic = False
        Me.ResultChart.Axes.Right.Minimum = 0
        Me.ResultChart.Axes.Right.Maximum = 100
        Me.ResultChart.Axes.Right.Grid.Visible = False

        Me.ResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.ResultChart.Axes.Bottom.Title.Caption = $"Value [{Me.InputTimeSeries(0).Unit}]"

        'Serien
        For Each res As histogramResults In Me.results

            Dim serieP As New Steema.TeeChart.Styles.Histogram(Me.ResultChart)
            serieP.Title = $"{res.title} (P(x))"
            serieP.Marks.Visible = False

            For i As Integer = 0 To n_bins - 1
                serieP.Add((Me.breaks(i) + Me.breaks(i + 1)) / 2, res.probability(i), res.probability(i).ToString("F2") & "%")
            Next

            Dim seriePU As New Steema.TeeChart.Styles.Line(Me.ResultChart)
            seriePU.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
            seriePU.Title = $"{res.title} (PU(x))"
            seriePU.LinePen.Width = 2
            seriePU.Pointer.Visible = True
            seriePU.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
            seriePU.Pointer.HorizSize = 2
            seriePU.Pointer.VertSize = 2

            For i As Integer = 0 To Me.n_bins - 1
                seriePU.Add(Me.breaks(i + 1), res.PU(i), res.PU(i).ToString("F2") & "%")
            Next

        Next

        'Markstips
        Dim markstip As New Steema.TeeChart.Tools.MarksTip()
        markstip.Style = Steema.TeeChart.Styles.MarksStyles.Label
        'markstip.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        Me.ResultChart.Tools.Add(markstip)

    End Sub

End Class