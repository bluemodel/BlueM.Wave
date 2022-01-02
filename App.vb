Module App

    'Model
    Private _wave As Wave

    'Controllers
    Private _waveController As Controller
    Private _propController As Controller
    Private _valuesController As Controller

    <STAThread()>
    Public Sub Main()
        ' Starts the application.

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        'Application.Run(New Wave())

        _wave = New Wave()
        _waveController = New WaveController(New MainWindow(), _wave)

        If Environment.GetCommandLineArgs().Count > 1 Then
            'run the CLI
            Dim showWave As Boolean = CLI.Run(Environment.GetCommandLineArgs().Skip(1).ToList, _wave)
            If Not showWave Then
                Exit Sub
            End If
        End If

        _waveController.ShowView()
    End Sub

    Public Sub showPropertiesWindow()
        If IsNothing(_propController) Then
            _propController = New PropertiesController(New PropertiesWindow(), _wave)
        End If
        _propController.ShowView()
    End Sub

    Public Sub showValuesWindow()
        If IsNothing(_valuesController) Then
            _valuesController = New ValuesController(New ValuesWindow(), _wave)
        End If
        _valuesController.ShowView()
    End Sub

End Module
