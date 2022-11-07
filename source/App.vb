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
        MyBase.New()
        _wave = wave
        _waveController = New WaveController(New MainWindow(), _wave)
        _propController = New PropertiesController(New PropertiesWindow(), _wave)
        _valuesController = New ValuesController(New ValuesWindow(), _wave)

        MyBase.MainForm = _waveController.View

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

End Class
