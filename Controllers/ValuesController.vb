Friend Class ValuesController
    Inherits Controller

    Private Overloads ReadOnly Property View As TimeSeriesValuesDialog
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

    'Zeitreihen Export
    '*****************
    Private Sub ExportZeitreihe_Click(sender As System.Object, e As System.EventArgs)
        Call _model.ExportZeitreihe()
    End Sub

    Private Sub SelectedRowsChanged(timestamps As List(Of DateTime))
        Call _model.HighlightTimestampsHandler(timestamps)
    End Sub

End Class
