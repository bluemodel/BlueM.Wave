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
''' Log
''' </summary>
Public Module Log

    Private logWindow As LogWindow

    Public logMessages As List(Of KeyValuePair(Of levels, String))

    ''' <summary>
    ''' Logging level, value set here may be overwritten by application settings
    ''' </summary>
    Friend level As levels = levels.info

    ''' <summary>
    ''' Log levels
    ''' </summary>
    Public Enum levels As Short
        debug
        info
        warning
        [error]
    End Enum

    ''' <summary>
    ''' Is triggered after a log message was added
    ''' </summary>
    Public Event LogMsgAdded(level As Log.levels, msg As String)

    Sub New()
        'attempt to read loggingLevel from application settings
        Try
            Log.level = [Enum].Parse(GetType(levels), My.Settings.loggingLevel)
        Catch ex As Exception
            'set default logging level to info
            Log.level = levels.info
        End Try
        Log.logMessages = New List(Of KeyValuePair(Of levels, String))
    End Sub

    ''' <summary>
    ''' Adds a log entry
    ''' </summary>
    ''' <param name="level">log level</param>
    ''' <param name="msg">message</param>
    Public Sub AddLogEntry(level As levels, msg As String)

        If level >= Log.level Then
            Log.logMessages.Add(New KeyValuePair(Of levels, String)(level, msg))
            If (msg.Contains(Constants.eol)) Then
                'Wenn Eintrag mehrzeilig, dann formatieren
                msg = Constants.eol & "  " & msg.Replace(Constants.eol, Constants.eol & "  ")
            End If

            If Not IsNothing(logWindow) Then
                Log.logWindow.AddLogEntry(level, msg)
            End If

            RaiseEvent LogMsgAdded(level, msg)
        End If

    End Sub

    ''' <summary>
    ''' Log zurücksetzen (allen Text löschen)
    ''' </summary>
    Public Sub ClearLog()

        Log.logMessages.Clear()

        If Not IsNothing(Log.logWindow) Then
            Log.logWindow.ClearLog()
        End If

        RaiseEvent LogMsgAdded(Log.levels.info, "")
    End Sub

    Public Sub HideLogWindow()
        If Not IsNothing(Log.logWindow) Then
            Log.logWindow.Hide()
        End If
    End Sub

    Public Sub ShowLogWindow()

        If IsNothing(logWindow) Then
            logWindow = New LogWindow()
            'if this is the first time the window is shown, add any already existing messages
            For Each msg As KeyValuePair(Of levels, String) In logMessages
                logWindow.AddLogEntry(msg.Key, msg.Value)
            Next
        End If
        logWindow.Show()
        logWindow.WindowState = FormWindowState.Normal
        logWindow.BringToFront()
    End Sub

End Module