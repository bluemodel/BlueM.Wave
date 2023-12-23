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
Friend Class PropertiesController
    Inherits Controller

    Private Overloads ReadOnly Property View As PropertiesWindow
        Get
            Return _view
        End Get
    End Property

    Public Sub New(view As IView, ByRef model As Wave)
        Call MyBase.New(view, model)

        _view.SetController(Me)

        'view events
        AddHandler Me.View.SeriesPropertyChanged, AddressOf SeriesPropertiesViewChanged
        AddHandler Me.View.SeriesDeleted, AddressOf SeriesDeletedHandler

        'model events
        AddHandler _model.SeriesAdded, AddressOf UpdateView
        AddHandler _model.SeriesPropertiesChanged, AddressOf UpdateView
        AddHandler _model.SeriesRemoved, AddressOf UpdateView
        AddHandler _model.SeriesCleared, AddressOf UpdateView
        AddHandler _model.SeriesReordered, AddressOf UpdateView
    End Sub

    Public Overrides Sub ShowView()
        If IsNothing(_view) Then
            _view = New PropertiesWindow()
        End If
        View.Update(_model.TimeSeries.ToList)
        View.WindowState = FormWindowState.Normal
        View.Show()
        View.BringToFront()
    End Sub

    Private Sub UpdateView()
        View.Update(_model.TimeSeries.ToList)
    End Sub

    ''' <summary>
    ''' Handles time series properties changed in the PropertiesDialog
    ''' </summary>
    ''' <param name="id">Id of the time series whose properties have changed</param>
    ''' <remarks>Handles changed interpretation, title and unit</remarks>
    Private Sub SeriesPropertiesViewChanged(id As Integer)
        _model.SeriesPropertiesChangedHandler(id)
    End Sub

    ''' <summary>
    ''' Deletes series deleted in the view from the model
    ''' </summary>
    ''' <param name="ids">List of time series ids to delete</param>
    Private Sub SeriesDeletedHandler(ids As List(Of Integer))
        For Each id As Integer In ids
            _model.RemoveTimeSeries(id)
        Next
    End Sub

End Class
