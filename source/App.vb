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
''' <summary>
''' Class representing an instance of the Wave app
''' </summary>
Public Class App
    Inherits ApplicationContext

    'Model
    Private _wave As Wave
    'List of files to import at startup (e.g. from commandline arguments)
    Private _startupFiles As List(Of String)

    'Controllers
    Private WithEvents _waveController As WaveController
    Private _propController As Controller
    Private _valuesController As Controller

    ''' <summary>
    ''' Instantiates a new Wave app and shows the main Wave window
    ''' </summary>
    Public Sub New()
        Call Me.New(New Wave())
    End Sub

    ''' <summary>
    ''' Instantiates a new Wave app with the passed Wave instance as the model and shows the main Wave window
    ''' </summary>
    ''' <param name="wave">the Wave instance to use as the model</param>
    Public Sub New(wave As Wave)
        Me.New(wave, New List(Of String)())
    End Sub

    ''' <summary>
    ''' Instantiates a new Wave app with startup files to import after the main window is shown
    ''' </summary>
    ''' <param name="wave">the Wave instance to use as the model</param>
    ''' <param name="startupFiles">file paths to import at startup</param>
    Public Sub New(wave As Wave, startupFiles As List(Of String))
        MyBase.New()
        _wave = wave
        _startupFiles = startupFiles
        _waveController = New WaveController(New MainWindow(), _wave)
        _propController = New PropertiesController(New PropertiesWindow(), _wave)
        _valuesController = New ValuesController(New ValuesWindow(), _wave)

        MyBase.MainForm = _waveController.View

        If _startupFiles IsNot Nothing AndAlso _startupFiles.Count > 0 Then
            AddHandler MyBase.MainForm.Shown, AddressOf MainWindow_Shown
        End If

        showMainWindow()
    End Sub

    ''' <summary>
    ''' The Wave instance associated with this app instance
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Wave As Wave
        Get
            Return _wave
        End Get
    End Property

    ''' <summary>
    ''' Shows the main Wave window
    ''' </summary>
    Friend Sub showMainWindow()
        _waveController.ShowView()
    End Sub

    ''' <summary>
    ''' Shows the Timeseries Properties window
    ''' </summary>
    Friend Sub showPropertiesWindow() Handles _waveController.Properties_Clicked
        _propController.ShowView()
    End Sub

    ''' <summary>
    ''' Shows the Timeseries Values window
    ''' </summary>
    Friend Sub showValuesWindow() Handles _waveController.TimeseriesValues_Clicked
        _valuesController.ShowView()
    End Sub

    ''' <summary>
    ''' Handles the Shown event of the main window
    ''' Removes the event handler and starts importing files passed as commandline arguments asynchronously
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MainWindow_Shown(sender As Object, e As EventArgs)
        RemoveHandler MyBase.MainForm.Shown, AddressOf MainWindow_Shown
        'Import files passed as commandline arguments after the main window is shown
        MyBase.MainForm.BeginInvoke(New Action(AddressOf ImportStartupFiles))
    End Sub

    ''' <summary>
    ''' Imports files passed as commandline arguments
    ''' This is done asynchronously to avoid blocking the UI thread during startup
    ''' </summary>
    Private Async Sub ImportStartupFiles()
        For Each file As String In _startupFiles
            Log.AddLogEntry(Log.Levels.debug, "Importing file from commandline argument: " & file)
            Await _wave.Import_File(file)
        Next
    End Sub

End Class
