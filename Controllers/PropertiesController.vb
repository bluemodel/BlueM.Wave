Friend Class PropertiesController
    Inherits Controller

    Private Overloads ReadOnly Property View As PropertiesDialog
        Get
            Return _view
        End Get
    End Property

    Public Sub New(view As IView, ByRef model As Wave)
        Call MyBase.New(view, model)

        _view.SetController(Me)

        'view events
        AddHandler Me.View.SeriesPropertyChanged, AddressOf SeriesPropertiesViewChanged
        AddHandler Me.View.SeriesDeleted, AddressOf SeriesDeleted

        'model events
        AddHandler _model.SeriesAdded, AddressOf UpdateView
        AddHandler _model.SeriesPropertiesChanged, AddressOf UpdateView
        AddHandler _model.SeriesRemoved, AddressOf UpdateView
        AddHandler _model.SeriesCleared, AddressOf UpdateView
    End Sub

    Public Overrides Sub ShowView()
        If IsNothing(_view) Then
            _view = New PropertiesDialog()
        End If
        View.Update(_model.TimeSeriesDict.Values.ToList)
        View.Show()
        View.BringToFront()
    End Sub

    Private Sub UpdateView()
        View.Update(_model.TimeSeriesDict.Values.ToList)
    End Sub

    ''' <summary>
    ''' Handles time series properties changed in the PropertiesDialog
    ''' </summary>
    ''' <param name="id">Id of the time series whose properties have changed</param>
    ''' <remarks>Handles changed interpretation, title and unit</remarks>
    Private Sub SeriesPropertiesViewChanged(id As Integer)
        _model.SeriesPropertiesChangedHandler(id)
    End Sub

    Private Sub SeriesDeleted(id As Integer)
        _model.RemoveTimeSeries(id)
    End Sub

End Class
