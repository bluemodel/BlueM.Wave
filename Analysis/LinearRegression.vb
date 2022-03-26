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
''' Fills gaps in one time series by applying a linear regression relationship with a second time series.
''' </summary>
''' <remarks>https://wiki.bluemodel.org/index.php/Wave:LinearRegression</remarks>

Friend Class LinearRegression
    Inherits Analysis

    Private ts_ref As TimeSeries             'reference time series
    Private ts_gaps As TimeSeries            'time series with gaps
    Private ts_filled As TimeSeries          'filled time series
    Private a, b As Double                   'regression coefficients
    Private r As Double                      'correlation coefficient
    Private n_gaps As Integer = 0            'counter for filled gaps
    Private neueDatume As List(Of Date)      'list of new dates
    Private neueWerte As List(Of Double)     'list of new values

    Public Overloads Shared Function Description() As String
        Return "Fills gaps (NaN values and missing timestamps) in one time series by applying a linear regression relationship with a second time series."
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
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has result series
    ''' that should be added to the main diagram
    ''' </summary>
    Public Overrides ReadOnly Property hasResultSeries() As Boolean
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

        'Prüfung: Anzahl erwarteter Zeitreihen
        If (zeitreihen.Count <> 2) Then
            Throw New Exception("The LinearRegression analysis requires the selection of exactly 2 time series!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        'Auswahl-Dialog öffnen
        Dim dialog As New LinearRegression_Dialog(Me.mZeitreihen(0).Title, Me.mZeitreihen(1).Title)
        If (dialog.ShowDialog() <> DialogResult.OK) Then
            Throw New Exception("User abort")
        End If

        'Zeitreihen zuweisen
        If (dialog.getNrLueckenhafteReihe = 1) Then
            Me.ts_gaps = Me.mZeitreihen(0)
            Me.ts_ref = Me.mZeitreihen(1)
        Else
            Me.ts_gaps = Me.mZeitreihen(1)
            Me.ts_ref = Me.mZeitreihen(0)
        End If

        'Calculate correlation and linear regression
        Dim ts_x As TimeSeries = Me.ts_ref.Clone()
        Dim ts_y As TimeSeries = Me.ts_gaps.Clone()

        'Remove NaN values
        ts_x = ts_x.removeNaNValues()
        ts_y = ts_y.removeNaNValues()

        'Synchronize
        TimeSeries.Synchronize(ts_x, ts_y)

        'convert to value array
        Dim x_values As Double()
        Dim y_values As Double()
        x_values = ts_x.Values.ToArray()
        y_values = ts_y.Values.ToArray()

        'Calculate correlation coefficient
        r = MathNet.Numerics.GoodnessOfFit.R(y_values, x_values)

        'Fehlermeldung bei zu niedriger Korrelation
        If (r < 0.7) Then
            Log.AddLogEntry(levels.warning,
                "The correlation coefficient is smaller than 0.7!" & eol &
                "There is no good linear relationship between the two selected time series!" & eol &
                "Filling gaps using linear regression is not recommended!")
        End If

        'Calculate linear regression
        Dim p As Tuple(Of Double, Double)
        p = MathNet.Numerics.Fit.Line(x_values, y_values)
        a = p.Item1
        b = p.Item2

        'Listen instanzieren für neue Wertepaare
        neueDatume = New List(Of DateTime)
        neueWerte = New List(Of Double)

        'Copy all non-NaN nodes to a new time series
        Me.ts_filled = Me.ts_gaps.removeNaNValues()
        Me.ts_filled.Title = Me.ts_gaps.Title & " (filled)"
        Me.ts_filled.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)

        'Find nodes present in ts_ref but not contained in ts_filled
        Dim t_missing As HashSet(Of DateTime)
        t_missing = ts_ref.Dates.ToHashSet()
        t_missing.ExceptWith(ts_filled.Dates.ToHashSet())

        'Loop over missing timestamps
        For Each t As DateTime In t_missing

            'calculate new value using regression equation y = a + b * x
            Dim v As Double = a + b * ts_ref.Nodes(t)

            'Wertepaare speichern für Textausgabe
            neueDatume.Add(t)
            neueWerte.Add(v)

            'add new node
            ts_filled.AddNode(t, a + b * ts_ref.Nodes(t))

            'Zähler der gefüllten Lücken erhöhen
            n_gaps += 1

        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()

        'Aufgefüllte Werte als Text
        Dim neueWerteText As String = ""
        For i As Integer = 0 To neueDatume.Count - 1
            neueWerteText &= neueDatume(i) & ":   " & neueWerte(i) & eol
        Next

        'Ergebnistext
        Me.mResultText =
            $"{Me.n_gaps} gaps were filled in time series '{Me.ts_gaps.Title}'." & eol &
            $"Linear regression line: yi = {Me.a} + {Me.b} * xi" & eol &
            $"Correlation coefficient: r = {Me.r}" & eol &
            $"Filled nodes:" & eol &
            neueWerteText

        'Ergebniswerte:
        Me.mResultValues.Add("Correlation coefficient", Me.r)

        'Result series
        Me.mResultSeries = New List(Of TimeSeries) From {Me.ts_filled}

    End Sub
End Class
