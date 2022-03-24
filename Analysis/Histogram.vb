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
''' Histogram and density estimation
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:Histogram</remarks>
Friend Class Histogram
    Inherits Analysis

    Private n_breaks As Integer
    Private n_bins As Integer
    Private breaks As Double()

    Private Structure resultValues
        Dim title As String
        Dim frequency, cumfrequency As Integer()
        Dim amount As Integer
        Dim probability As Double()
        Dim PU As Double() ' probability of non-exceedance
    End Structure

    Private results As resultValues()

    Public Overloads Shared Function Description() As String
        Return "Divides the entire range of values into a series of user-defined intervals (bins) and calculates the percentage of values falling into each interval." &
            "Also estimates a corresponding density function."
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
        histogramDlg = New HistogramDialog(Me.mZeitreihen)
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
        For Each zre As TimeSeries In Me.mZeitreihen

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

        'Ergebnistext
        '------------
        Const formatstring As String = "F4"
        Me.mResultText = "Histogram has been calculated." & eol
        Me.mResultText &= "Result data:" & eol
        For Each result As resultValues In Me.results
            Me.mResultText &= result.title & eol
            Me.mResultText &= String.Join(Helpers.CurrentListSeparator, "from", "to", "frequency", "probability") & eol
            For i As Integer = 0 To Me.n_bins - 1
                Me.mResultText &= String.Join(Helpers.CurrentListSeparator,
                    Me.breaks(i).ToString(formatstring),
                    Me.breaks(i + 1).ToString(formatstring),
                    result.frequency(i),
                    result.probability(i).ToString(formatstring)) & eol
            Next
        Next

        'Ergebniswerte
        '-------------
        'TODO: Ergebniswerte zurückgeben?

        'Ergebnisdiagramm
        '----------------

        'Diagramm formatieren
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Helpers.FormatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Histogram"

        'Achsen
        Me.mResultChart.Axes.Left.Title.Caption = "Probability [%]"
        Me.mResultChart.Axes.Left.Automatic = False
        Me.mResultChart.Axes.Left.Minimum = 0
        Me.mResultChart.Axes.Left.AutomaticMaximum = True
        Me.mResultChart.Axes.Left.MaximumOffset = 2

        Me.mResultChart.Axes.Right.Visible = True
        Me.mResultChart.Axes.Right.Title.Caption = "Probability of non-exceedance [%]"
        Me.mResultChart.Axes.Right.Title.Angle = 90
        Me.mResultChart.Axes.Right.Automatic = False
        Me.mResultChart.Axes.Right.Minimum = 0
        Me.mResultChart.Axes.Right.Maximum = 100
        Me.mResultChart.Axes.Right.Grid.Visible = False

        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Bottom.Title.Caption = $"Value [{Me.mZeitreihen(0).Unit}]"

        'Serien
        For Each res As resultValues In Me.results

            Dim serieP As New Steema.TeeChart.Styles.Histogram(Me.mResultChart)
            serieP.Title = $"{res.title} (P(x))"
            serieP.Marks.Visible = False

            For i As Integer = 0 To n_bins - 1
                serieP.Add((Me.breaks(i) + Me.breaks(i + 1)) / 2, res.probability(i), res.probability(i).ToString("F2") & "%")
            Next

            Dim seriePU As New Steema.TeeChart.Styles.Line(Me.mResultChart)
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
        Me.mResultChart.Tools.Add(markstip)

    End Sub

End Class