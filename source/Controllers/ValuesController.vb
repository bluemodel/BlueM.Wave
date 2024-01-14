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
Friend Class ValuesController
    Inherits Controller

    Private Overloads ReadOnly Property View As ValuesWindow
        Get
            Return _view
        End Get
    End Property

    Public Sub New(view As IView, ByRef model As Wave)
        Call MyBase.New(view, model)

        _view.SetController(Me)

        'subscribe to events

        'view events
        AddHandler Me.View.ToolStripButton_ExportValues.Click, AddressOf ExportZeitreihe_Click
        AddHandler Me.View.SelectedRowsChanged, AddressOf SelectedRowsChanged

        'model events
        AddHandler _model.SeriesAdded, AddressOf UpdateView
        AddHandler _model.SeriesPropertiesChanged, AddressOf UpdateView
        AddHandler _model.SeriesRemoved, AddressOf UpdateView
        AddHandler _model.SeriesReordered, AddressOf UpdateView
        AddHandler _model.SeriesCleared, AddressOf UpdateView
    End Sub

    Public Overrides Sub ShowView()
        If IsNothing(_view) Then
            _view = New ValuesWindow()
            View.Update(_model.TimeSeries.ToList)
        End If
        View.WindowState = FormWindowState.Normal
        View.Show()
        View.BringToFront()
    End Sub

    Private Sub UpdateView()
        View.Update(_model.TimeSeries.ToList)
    End Sub

    'Zeitreihen Export
    '*****************
    Private Sub ExportZeitreihe_Click(sender As System.Object, e As System.EventArgs)
        Call _model.ExportTimeseries()
    End Sub

    Private Sub SelectedRowsChanged(timestamps As List(Of DateTime))
        Call _model.HighlightTimestampsHandler(timestamps)
    End Sub

End Class
