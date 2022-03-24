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

        If Environment.GetCommandLineArgs().Count > 1 Then
            'run the CLI
            Dim showWave As Boolean = CLI.Run(Environment.GetCommandLineArgs().Skip(1).ToList, _wave)
            If Not showWave Then
                Exit Sub
            End If
        End If

        showMainWindow(_wave)

    End Sub

    Public Sub showMainWindow(model As Wave)
        If IsNothing(_waveController) Then
            _waveController = New WaveController(New MainWindow(), model)
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
