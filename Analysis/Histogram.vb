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
''' Histogram and density estimation
''' </summary>
''' <remarks>http://wiki.bluemodel.org/index.php/Wave:Histogram</remarks>
Public Class Histogram
    Inherits Analysis

    Private Const n_bins As Integer = 100
    Dim bin_size As Double
    Dim bins As Double()

    Private Structure resultValues
        Dim title As String
        Dim frequency, cumfrequency As Integer()
        Dim amount As Integer
        Dim probability As Double()
        Dim PU As Double() ' probability of subceedance (Unterschreitungswahrscheinlichkeit)
    End Structure

    Private results As resultValues()

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
    Public Sub New(ByRef zeitreihen As List(Of Zeitreihe))

        'Konstruktor der Basisklasse aufrufen!
        Call MyBase.New(zeitreihen)

        Dim einheit As String

        'Prüfung: Zeitreihen müssen die gleiche Einheit besitzen
        If (zeitreihen.Count > 1) Then
            einheit = zeitreihen(1).Einheit
            For Each zre As Zeitreihe In zeitreihen
                If (zre.Einheit <> einheit) Then
                    Throw New Exception("Please select only series with the same unit!")
                End If
            Next
        End If

        ReDim Me.results(zeitreihen.Count - 1)

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i, j, n As Integer
        Dim min, max As Double

        'clean series
        For Each zre As Zeitreihe In Me.mZeitreihen
            zre = zre.getCleanZRE()
        Next

        'Determine min and max
        '---------------------
        min = Double.MaxValue
        max = Double.MinValue
        For Each zre As Zeitreihe In Me.mZeitreihen
            'Min und Max
            min = Math.Min(min, zre.Minimum)
            max = Math.Max(max, zre.Maximum)
        Next

        'Construct bins
        '--------------
        ReDim Me.bins(n_bins - 1)
        Me.bin_size = (max - min) / (n_bins)
        For i = 0 To n_bins - 1
            Me.bins(i) = min + i * Me.bin_size
        Next

        'Analyse series
        '--------------
        n = 0
        For Each zre As Zeitreihe In Me.mZeitreihen

            With Me.results(n)

                'title
                .title = zre.Title

                'determine frequencies
                '---------------------
                ReDim .frequency(n_bins - 1)
                'loop through series values
                For i = 0 To zre.Length - 1
                    'loop over bins
                    For j = 0 To n_bins - 1
                        If (zre.YWerte(i) >= Me.bins(j) And zre.YWerte(i) < (Me.bins(j) + Me.bin_size)) Then
                            'correct bin found, add to frequency
                            .frequency(j) += 1
                            Exit For
                        End If
                    Next
                Next

                'Total amount
                .amount = 0
                For i = 0 To n_bins - 1
                    .amount += .frequency(i)
                Next

                'Probability
                ReDim .probability(n_bins - 1)
                For i = 0 To n_bins - 1
                    .probability(i) = .frequency(i) / .amount * 100 '%
                Next

                'Cumulative frequency
                ReDim .cumfrequency(n_bins - 1)
                .cumfrequency(0) = .frequency(0)
                For i = 1 To n_bins - 1
                    .cumfrequency(i) = .cumfrequency(i - 1) + .frequency(i)
                Next

                'Probability of subceedance
                ReDim .PU(n_bins - 1)
                For i = 0 To n_bins - 1
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
        Me.mResultText = "Histogram has been calculated:" & eol
        Me.mResultText &= "The following bins were used:" & eol
        For i As Integer = 0 To n_bins - 1
            Me.mResultText &= "Bin " & i + 1 & ": " & Me.bins(i) & " - " & (Me.bins(i) + Me.bin_size) & eol
        Next
        Me.mResultText &= eol

        'Ergebniswerte
        '-------------
        'TODO: Ergebniswerte zurückgeben?

        'Ergebnisdiagramm
        '----------------

        'Diagramm formatieren
        Me.mResultChart = New Steema.TeeChart.Chart()
        Call Wave.formatChart(Me.mResultChart)
        Me.mResultChart.Header.Text = "Histogram"

        'Achsen
        Me.mResultChart.Axes.Left.Title.Caption = "Probability [%]"
        Me.mResultChart.Axes.Left.Automatic = False
        Me.mResultChart.Axes.Left.Minimum = 0
        Me.mResultChart.Axes.Left.AutomaticMaximum = True
        Me.mResultChart.Axes.Left.MaximumOffset = 2

        Me.mResultChart.Axes.Right.Visible = True
        Me.mResultChart.Axes.Right.Title.Caption = "Probability of subceedance [%]"
        Me.mResultChart.Axes.Right.Title.Angle = 90
        Me.mResultChart.Axes.Right.Automatic = False
        Me.mResultChart.Axes.Right.Minimum = 0
        Me.mResultChart.Axes.Right.Maximum = 100

        Me.mResultChart.Axes.Bottom.Labels.Style = Steema.TeeChart.AxisLabelStyle.Value
        Me.mResultChart.Axes.Bottom.Title.Caption = "Value [" & Me.mZeitreihen(0).Einheit & "]"

        'Serien
        For Each res As resultValues In Me.results

            Dim serieP As New Steema.TeeChart.Styles.Bar(Me.mResultChart)
            serieP.Title = res.title & " (P(x))"
            serieP.Marks.Visible = False

            For i As Integer = 0 To n_bins - 1
                serieP.Add(Me.bins(i) + Me.bin_size / 2, res.probability(i), "Bin " & (i + 1).ToString & ": " & res.probability(i).ToString("F2") & "%")
            Next

            Dim seriePU As New Steema.TeeChart.Styles.Line(Me.mResultChart)
            seriePU.VertAxis = Steema.TeeChart.Styles.VerticalAxis.Right
            seriePU.Title = res.title & " (PU(x))"
            seriePU.LinePen.Width = 2
            seriePU.Pointer.Visible = True
            seriePU.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle
            seriePU.Pointer.HorizSize = 2
            seriePU.Pointer.VertSize = 2

            For i As Integer = 0 To n_bins - 1
                seriePU.Add(Me.bins(i) + Me.bin_size / 2, res.PU(i), "Bin " & (i + 1).ToString & ": " & res.PU(i).ToString("F2") & "%")
            Next

        Next

        'Markstips
        Dim markstip As New Steema.TeeChart.Tools.MarksTip()
        markstip.Style = Steema.TeeChart.Styles.MarksStyles.Label
        'markstip.MouseAction = Steema.TeeChart.Tools.MarksTipMouseAction.Move
        Me.mResultChart.Tools.Add(markstip)

    End Sub

End Class