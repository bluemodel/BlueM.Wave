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
Friend Module Main

    <STAThread()>
    Public Sub Main()
        ' Starts the application.

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim wave As New Wave()

        If Environment.GetCommandLineArgs().Count > 1 Then
            'run the CLI
            Dim showWave As Boolean = False
            Using cli As New CLI()
                showWave = cli.Run(Environment.GetCommandLineArgs().Skip(1).ToList, wave)
            End Using

            If Not showWave Then
                Exit Sub
            End If
        End If

        'launch the app
        Call Launch(wave)

    End Sub

    ''' <summary>
    ''' Launches a new instance of the Wave app
    ''' </summary>
    ''' <param name="wave">the Wave model instance to associate with the app</param>
    Public Sub Launch(wave As Wave)
        Dim instance As New AppInstance(wave)
        instance.showMainWindow()
    End Sub

End Module
