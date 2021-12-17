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
''' <summary>
''' Class for listening to TeeChart Events
''' </summary>
Friend Class ChartEventListener
    Implements Steema.TeeChart.ITeeEventListener

    Private chart As Steema.TeeChart.Chart
    Private tsDict As Dictionary(Of Integer, TimeSeries)

    Public Sub New(chart As Steema.TeeChart.Chart, ByRef tsDict As Dictionary(Of Integer, TimeSeries))
        Me.chart = chart
        Me.tsDict = tsDict
    End Sub

    ''' <summary>
    ''' Handles TeeChart Events
    ''' </summary>
    ''' <param name="e"></param>
    Public Sub TeeEvent(e As Steema.TeeChart.TeeEvent) Implements Steema.TeeChart.ITeeEventListener.TeeEvent
        Try
            If TypeOf e Is Steema.TeeChart.Styles.SeriesEvent Then
                Dim seriesEvent As Steema.TeeChart.Styles.SeriesEvent = CType(e, Steema.TeeChart.Styles.SeriesEvent)
                Select Case seriesEvent.Event
                    Case Steema.TeeChart.Styles.SeriesEventStyle.ChangeActive
                        'series visibility has been changed. check whether custom axes should be made invisible

                        'collect units of all active series
                        Dim activeUnits As New HashSet(Of String)
                        For Each series As Steema.TeeChart.Styles.Series In Me.chart.Series
                            If series.Active Then
                                activeUnits.Add(series.GetVertAxis.Tag)
                            End If
                        Next
                        'set visibility of custom axes accordingly
                        For Each axis As Steema.TeeChart.Axis In chart.Axes.Custom
                            If activeUnits.Contains(axis.Tag) Then
                                axis.Visible = True
                            Else
                                axis.Visible = False
                            End If
                        Next

                    Case Steema.TeeChart.Styles.SeriesEventStyle.ChangeTitle
                        'series title changed, update title in time series dictionary
                        If Me.tsDict.ContainsKey(seriesEvent.Series.Tag) Then
                            Me.tsDict(seriesEvent.Series.Tag).Title = seriesEvent.Series.Title
                        End If
                End Select
            End If
        Catch ex As Exception
            Log.AddLogEntry(Log.levels.debug, ex.Message)
        End Try
    End Sub
End Class
