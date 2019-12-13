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
''' Analysis function for analyzing the timestep between nodes
''' </summary>
Friend Class TimeStepAnalysis
    Inherits Analysis

    ''' <summary>
    ''' Time units
    ''' </summary>
    Friend Enum TimeUnit As Short
        Years = 1
        Months = 2
        Days = 3
        Hours = 4
        Minutes = 5
        Seconds = 6
    End Enum

    ''' <summary>
    ''' Selected time unit
    ''' </summary>
    Private time_unit As TimeUnit

#Region "Properties"

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

    ''' <summary>
    ''' Name of the selected time unit
    ''' </summary>
    Private ReadOnly Property TimeUnitName() As String
        Get
            Return System.Enum.GetName(GetType(TimeUnit), Me.time_unit)
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ts_list">Collection of TimeSeries</param>
    Public Sub New(ByRef ts_list As List(Of TimeSeries))

        Call MyBase.New(ts_list)

        Dim dlg As New TimestepAnalysisDialog()
        Dim dlg_result As DialogResult = dlg.ShowDialog()

        If Not dlg_result = DialogResult.OK Then
            Throw New Exception("User abort")
        End If

        Me.time_unit = dlg.ComboBox_TimestepType.SelectedItem

    End Sub

    ''' <summary>
    ''' Process Analysis
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim timestamp, timestamp_prev As DateTime
        Dim dt As TimeSpan
        Dim dt_val As Double
        Dim result_ts As TimeSeries

        'Loop over time series
        For Each ts As TimeSeries In MyBase.mZeitreihen

            'create a new result time series
            result_ts = New TimeSeries(ts.Title & " (dt " & Me.TimeUnitName & ")")
            result_ts.Unit = Me.TimeUnitName
            result_ts.Interpretation = TimeSeries.InterpretationType.BlockLeft

            'Loop over timestamps
            For i As Integer = 0 To ts.Length - 1

                timestamp = ts.Dates(i)

                If i = 0 Then
                    'dt of first node is always NaN
                    result_ts.AddNode(timestamp, Double.NaN)
                Else
                    'calculate dt
                    dt = timestamp - timestamp_prev
                    'convert to user-specified unit
                    Select Case Me.time_unit
                        'TODO: Implement conversion to Years and Months and activate corresponding options in TimestepAnalysisDialog
                        'Case TimeUnit.Years
                        'Case TimeUnit.Months
                        Case TimeUnit.Days
                            dt_val = dt.TotalDays
                        Case TimeUnit.Hours
                            dt_val = dt.TotalHours
                        Case TimeUnit.Minutes
                            dt_val = dt.TotalMinutes
                        Case TimeUnit.Seconds
                            dt_val = dt.TotalSeconds
                        Case Else
                            Throw New Exception("Conversion of timestep to time unit " & Me.TimeUnitName & " is not yet implemented!")
                    End Select
                    'store dt value as new node in result series
                    result_ts.AddNode(timestamp, dt_val)
                End If
                timestamp_prev = timestamp
            Next
            'save result time series
            Me.mResultSeries.Add(result_ts.Title, result_ts)
        Next

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()
        'TODO
    End Sub

#End Region 'Methods

End Class
