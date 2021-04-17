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
''' Make a time series equidistant with a user-specified timestep
''' </summary>
''' <remarks>http://wiki.bluemodel.org/index.php/Wave:ChangeTimestep</remarks>
Friend Class ChangeTimestep
    Inherits Analysis

#Region "Properties"

    ''' <summary>
    ''' Returns a text description of the analysis function
    ''' </summary>
    Public Overloads Shared Function Description() As String
        Return "Make a time series equidistant with a user-specified timestep"
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

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="seriesList">Collection von Zeitreihen</param>
    Public Sub New(ByRef seriesList As List(Of TimeSeries))

        Call MyBase.New(seriesList)

        If seriesList.Count <> 1 Then
            Throw New Exception("The ChangeTimestep analysis requires the selection of exactly 1 time series!")
        End If

    End Sub

    ''' <summary>
    ''' Analyse durchführen
    ''' </summary>
    Public Overrides Sub ProcessAnalysis()

        Dim ts, ts_new As TimeSeries
        Dim interpretation As TimeSeries.InterpretationEnum
        Dim timesteptype As TimeSeries.TimeStepTypeEnum
        Dim timestepinterval As Integer
        Dim startdate As DateTime

        ts = Me.mZeitreihen(0).Clone()

        'show the ChangeTimeStepDialog
        Dim dlg As New ChangeTimestepDialog(ts)
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then

            'read settings from dialog
            interpretation = dlg.ComboBox_Interpretation.SelectedItem
            timesteptype = dlg.ComboBox_TimestepType.SelectedItem
            timestepinterval = dlg.NumericUpDown_TimestepInterval.Value
            startdate = dlg.DateTimePicker_Start.Value

            'change timestep
            ts.Interpretation = interpretation
            ts_new = ts.ChangeTimestep(timesteptype, timestepinterval, startdate)

            'Store result series
            ts_new.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.AnalysisResult)
            Me.mResultSeries.Add(ts_new)

        End If

    End Sub

    ''' <summary>
    ''' Ergebnisse aufbereiten
    ''' </summary>
    Public Overrides Sub PrepareResults()
        'nothing to do
    End Sub

#End Region 'Methods

End Class
