Friend Class ValuesController
    Implements IController

    Private WithEvents _view As TimeSeriesValuesDialog
    Private _model As Wave

    Public Sub New(view As IView, ByRef model As Wave)
        _view = view
        _model = model

        'subscribe to events
        AddHandler _view.Button_ExportValues_Clicked, AddressOf ExportZeitreihe_Click
    End Sub

    'Zeitreihen Export
    '*****************
    Private Sub ExportZeitreihe_Click(sender As System.Object, e As System.EventArgs)
        Call _model.ExportZeitreihe()
    End Sub

End Class
