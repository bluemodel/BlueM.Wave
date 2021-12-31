Module App

    'Model
    Private _wave As Wave

    'Controllers
    Private _waveController As Controller
    Private _propController As Controller

    'Dialogs
    Private _valuesDialog As TimeSeriesValuesDialog

    <STAThread()>
    Public Sub Main()
        ' Starts the application.

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        'Application.Run(New Wave())

        _wave = New Wave()

        If Environment.GetCommandLineArgs().Count > 1 Then
            'run the CLI
            Dim showWave As Boolean = CLI.Run(Environment.GetCommandLineArgs().Skip(1).ToList)
            If Not showWave Then
                Exit Sub
            End If
        End If

        If IsNothing(_valuesDialog) Then
            _valuesDialog = New TimeSeriesValuesDialog()
        End If

        _waveController = New WaveController(New MainWindow(), _wave)
        _waveController.ShowView()
    End Sub

    Public Sub showPropDialog()
        If IsNothing(_propController) Then
            _propController = New PropertiesController(New PropertiesDialog(), _wave)
        End If
        _propController.ShowView()
    End Sub

End Module
