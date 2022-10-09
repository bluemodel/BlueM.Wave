'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
'
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
'
''' <summary>
''' Class representing an instance of the Wave app
''' </summary>
Friend Class AppInstance
    'Model
    Private _wave As Wave

    'Controllers
    Private WithEvents _waveController As WaveController
    Private _propController As Controller
    Private _valuesController As Controller

    ''' <summary>
    ''' Instantiates a new AppInstance with the passed Wave instance as the model
    ''' </summary>
    ''' <param name="wave">the Wave instance to use as the model</param>
    Public Sub New(wave As Wave)
        _wave = wave
        _waveController = New WaveController(New MainWindow(), _wave)
        _propController = New PropertiesController(New PropertiesWindow(), _wave)
        _valuesController = New ValuesController(New ValuesWindow(), _wave)
    End Sub

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
