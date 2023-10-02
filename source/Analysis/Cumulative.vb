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
''' <summary>
''' Cumulative analysis
''' </summary>
Friend Class Cumulative
    Inherits Analysis

#Region "Properties"

    Public Overloads Shared Function Description() As String
        Return "For each selected time series, calculates a new time series that consists of the cumulative values of the original series. NaN values are ignored."
    End Function

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result test
    ''' </summary>
    Public Overrides ReadOnly Property hasResultText() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has result values
    ''' </summary>
    Public Overrides ReadOnly Property hasResultValues() As Boolean
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the analysis function has a result diagram
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

    Public Overrides ReadOnly Property hasResultTable() As Boolean
        Get
            Return False
        End Get
    End Property

#End Region 'Properties

#Region "Methoden"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ts_list">List of time series to process</param>
    Public Sub New(ByRef ts_list As List(Of TimeSeries))
        Call MyBase.New(ts_list)
    End Sub

    ''' <summary>
    ''' Process the analysis
    ''' Creates new result time series that contain the cumulative values of the original series
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim i As Integer
        Dim value, sum As Double
        Dim startdate As DateTime
        Dim ts, ts_cum As TimeSeries

        'show dialog
        Dim dlg As New CumulativeDialog(MyBase.InputTimeSeries)
        If dlg.ShowDialog() = DialogResult.OK Then

            'get start date from dialog
            startdate = CType(dlg.MaskedTextBox_Start.ValidateText(), DateTime)

            For Each ts In MyBase.InputTimeSeries

                'cut time series to startdate
                ts.Cut(startdate, ts.EndDate)

                'create new cumulative timeseries
                ts_cum = New TimeSeries($"{ts.Title} (cumulative)")
                ts_cum.Unit = ts.Unit
                ts_cum.Interpretation = TimeSeries.InterpretationEnum.Cumulative
                ts_cum.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)

                sum = 0.0
                For i = 0 To ts.Length - 1

                    'determine value depending on interpretation
                    Select Case ts.Interpretation
                        Case TimeSeries.InterpretationEnum.Instantaneous,
                             TimeSeries.InterpretationEnum.BlockLeft,
                             TimeSeries.InterpretationEnum.CumulativePerTimestep
                            value = ts.Values(i)
                        Case TimeSeries.InterpretationEnum.BlockRight
                            If i = 0 Then
                                value = 0
                            Else
                                value = ts.Values(i - 1)
                            End If
                        Case Else
                            Throw New NotImplementedException($"Analysis function Cumulative is currently not supported for time series with interpretation {[Enum].GetName(GetType(TimeSeries.InterpretationEnum), ts.Interpretation)}!")
                    End Select

                    If Double.IsNaN(value) Then
                        'omit NaN values
                        Continue For
                    End If

                    'cumulate
                    sum += value

                    'add new node
                    ts_cum.AddNode(ts.Dates(i), sum)
                Next

                'delete intermediate nodes where the cumulative value does not change
                Dim unnecessary_nodes As New List(Of Integer)
                For i = 1 To ts_cum.Length - 2
                    If ts_cum.Values(i - 1) = ts_cum.Values(i + 1) Then
                        unnecessary_nodes.Add(i)
                    End If
                Next
                unnecessary_nodes.Reverse()
                For Each i In unnecessary_nodes
                    ts_cum.Nodes.RemoveAt(i)
                Next

                'store result series
                MyBase.ResultSeries.Add(ts_cum)

            Next

        End If
    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()
        'nothing to do
    End Sub

#End Region 'Methoden

End Class
