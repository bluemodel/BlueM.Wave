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

        'Set a default regex match timeout to prevent potential DoS attacks from untrusted input
        AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromMilliseconds(100))

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        'load user settings
        My.Settings.Reload()
        If My.Settings.isNewVersion Then
            'upgrade settings from previous version
            Try
                My.Settings.Upgrade()
                My.Settings.isNewVersion = False
                Log.AddLogEntry(Log.Levels.debug, "User settings upgraded from previous version.")
            Catch ex As System.Configuration.ConfigurationErrorsException
                Log.AddLogEntry(Log.Levels.error, "Error upgrading user settings: " & ex.Message)
            End Try
        End If

        'set color mode (depends on user settings)
        Application.SetColorMode(Helpers.GetCurrentColorMode)

        Dim wave As New Wave()
        Dim args As List(Of String) = Environment.GetCommandLineArgs().Skip(1).ToList()
        Dim files As New List(Of String)

        If args.Count > 0 Then
            If Not args.First().StartsWith("-"c) Then
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
                    Return
                End If
            End If
        End If

        'import any files that were passed as commandline arguments
        Dim importTasks As New List(Of Task)
        For Each file As String In files
            importTasks.Add(wave.Import_File(file))
        Next
        Task.WaitAll(importTasks)

        'launch the app
        Dim app As New App(wave)
        Application.Run(app)

    End Sub

End Module
