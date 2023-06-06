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
        Dim args As List(Of String) = Environment.GetCommandLineArgs().Skip(1).ToList()
        Dim files As New List(Of String)

        If args.Count > 0 Then
            If Not args.First().StartsWith("-") Then
                'don't run the CLI, instead assume that args are filenames to open in the app
                For Each file As String In args
                    files.Add(file)
                Next
            Else
                'run the CLI
                Dim showWave As Boolean = False
                Using cli As New CLI()
                    showWave = cli.Run(args, wave)
                End Using

                If Not showWave Then
                    Exit Sub
                End If
            End If
        End If

        'launch the app
        Dim app As New App(wave)
        'open any files that were passed as commandline arguments
        For Each file As String In files
            wave.Import_File(file)
        Next
        Application.Run(app)

    End Sub

End Module
