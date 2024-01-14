Public Class Examples

    ''' <summary>
    ''' Path to the time series file
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FilePath As String
        Get
            Return Me.TextBox_File.Text
        End Get
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Attempt to determine the path to an example time series file from BlueM.Datasets
        Try
            Dim appdir As String = My.Application.Info.DirectoryPath() 'e.g. BlueM.Wave\Wave.Examples\bin\x64\Debug
            Dim testdatadir As String = appdir
            'move up 5 levels
            For i As Integer = 1 To 5
                testdatadir = IO.Directory.GetParent(testdatadir).FullName
            Next
            testdatadir = IO.Path.Combine(testdatadir, "BlueM.Datasets", "Wave")
            Dim filePath As String = IO.Path.Combine(testdatadir, "WEL", "DEMONA_PSI.wel")
            If Not IO.File.Exists(filePath) Then
                Throw New Exception($"File not found: {filePath}")
            End If
            Me.TextBox_File.Text = filePath
        Catch ex As Exception
            MsgBox("Unable to locate test file! " &
                   "Expected directory: BlueM.Datasets\Wave in the same directory as BlueM.Wave! " &
                   ex.Message,
                   MsgBoxStyle.Exclamation
            )
        End Try

        'Add button click handlers
        AddHandler Button1.Click, AddressOf Example1
        AddHandler Button2.Click, AddressOf Example2
        AddHandler Button3.Click, AddressOf Example3
        AddHandler Button4.Click, AddressOf Example4
        AddHandler Button5.Click, AddressOf Example5
        AddHandler Button6.Click, AddressOf Example6
        AddHandler Button7.Click, AddressOf Example7

    End Sub

    Private Sub Example1()

        Dim Wave1 As New BlueM.Wave.Wave()

        'import a time series file
        Wave1.Import_File(Me.FilePath) 'depending on the file format, this may display an import dialog

        'instantiate and show the Wave app
        Dim app As New BlueM.Wave.App(Wave1)

    End Sub

    Private Sub Example2()

        Dim Wave1 As New BlueM.Wave.Wave()
        Dim myFile As BlueM.Wave.TimeSeriesFile
        Dim ts1, ts2, ts3 As BlueM.Wave.TimeSeries

        'get a file instance
        myFile = BlueM.Wave.TimeSeriesFile.getInstance(Me.FilePath)

        'get a time series by name (this will automatically attempt to read the series from the file if not already done)
        ts1 = myFile.getTimeSeries("AA  _ETA")

        'get a second time series
        ts2 = myFile.getTimeSeries("AA  _ETP")

        'get a third time series by column index
        ts3 = myFile.getTimeSeries(83)

        'import the time series in Wave
        Wave1.Import_Series(ts1)
        Wave1.Import_Series(ts2)
        Wave1.Import_Series(ts3)

        'instantiate and show the Wave app
        Dim app As New BlueM.Wave.App(Wave1)

    End Sub

    Private Sub Example3()

        Dim Wave1 As New BlueM.Wave.Wave()
        Dim myFile As BlueM.Wave.TimeSeriesFile
        Dim ts As BlueM.Wave.TimeSeries

        'get a file instance
        myFile = BlueM.Wave.TimeSeriesFile.getInstance(Me.FilePath)

        'check the time series contained in the file
        For Each sInfo As BlueM.Wave.TimeSeriesInfo In myFile.TimeSeriesInfos
            Console.WriteLine(sInfo.Name)
        Next

        'select all series for import
        myFile.selectAllSeries()

        'read the selected time series from the file
        myFile.readFile()

        'loop over all series read from the file and print some information about them
        For Each ts In myFile.TimeSeries.Values
            Console.WriteLine("Series title: " & ts.Title)
            Console.WriteLine("Extent: " & ts.StartDate & " - " & ts.EndDate)
            Console.WriteLine("Max value: " & ts.Maximum)
            Console.WriteLine("Min value: " & ts.Minimum)
        Next

        'get one particular time series
        ts = myFile.getTimeSeries("S1  _1AB")

        'import the series in Wave
        Wave1.Import_Series(ts)

        'instantiate and show the Wave app
        Dim app As New BlueM.Wave.App(Wave1)

    End Sub

    Private Sub Example4()

        Dim Wave1 As New BlueM.Wave.Wave()

        'create a new time series
        Dim ts As New BlueM.Wave.TimeSeries("my series")
        ts.Unit = "m3/s"
        ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight

        'add some nodes to the time series
        ts.AddNode(New DateTime(2000, 1, 1), 10)
        ts.AddNode(New DateTime(2000, 1, 2), 20)
        ts.AddNode(New DateTime(2000, 1, 3), 30)
        ts.AddNode(New DateTime(2000, 1, 4), 15)

        'print some information about the time series
        Console.WriteLine("Length: " & ts.Length)
        Console.WriteLine("Start date: " & ts.StartDate)
        Console.WriteLine("End date: " & ts.EndDate)
        Console.WriteLine("Average: " & ts.Average)

        'set some custom display options
        ts.DisplayOptions.Color = Color.Red
        ts.DisplayOptions.LineStyle = ScottPlot.LineStyle.Dash
        ts.DisplayOptions.LineWidth = 1
        ts.DisplayOptions.ShowPoints = True

        'import the series in Wave
        Wave1.Import_Series(ts)

        'instantiate and show the Wave app
        Dim app As New BlueM.Wave.App(Wave1)

    End Sub

    Private Sub Example5()

        Dim myFile As BlueM.Wave.TimeSeriesFile
        Dim ts As BlueM.Wave.TimeSeries

        'get a file instance
        myFile = BlueM.Wave.TimeSeriesFile.getInstance(Me.FilePath)

        'get a specific time series from the file
        ts = myFile.getTimeSeries("S1  _1AB")

        'cut the time series
        ts.Cut(New DateTime(1959, 1, 1), New DateTime(1959, 2, 1))

        'loop over the nodes of the time series and print them
        Dim d As DateTime
        Dim v As Double
        For Each node As KeyValuePair(Of DateTime, Double) In ts.Nodes
            d = node.Key
            v = node.Value
            Console.WriteLine($"{d}: {v}")
        Next

    End Sub

    Private Sub Example6()

        Dim Wave1 As New BlueM.Wave.Wave()

        Dim myFile As BlueM.Wave.TimeSeriesFile
        Dim ts1, ts2 As BlueM.Wave.TimeSeries

        'get a file instance
        myFile = BlueM.Wave.TimeSeriesFile.getInstance(Me.FilePath)

        'get a specific time series from the file
        ts1 = myFile.getTimeSeries("S1  _1AB")

        'import the series in Wave
        Wave1.Import_Series(ts1)

        'instantiate and show the Wave app
        Dim app As New BlueM.Wave.App(Wave1)

        'get a second time series from the file
        ts2 = myFile.getTimeSeries("AA  _ETA")

        'display the second time series in Wave
        Wave1.Import_Series(ts2)
    End Sub

    Private Sub Example7()

        Dim myFile As BlueM.Wave.TimeSeriesFile
        Dim ts1 As BlueM.Wave.TimeSeries

        'instantiate and show the Wave app
        Dim app As New BlueM.Wave.App()

        'get a file instance
        myFile = BlueM.Wave.TimeSeriesFile.getInstance(Me.FilePath)

        'get a specific time series from the file
        ts1 = myFile.getTimeSeries("S1  _1AB")

        'import the series in Wave
        app.Wave.Import_Series(ts1)

    End Sub

End Class
