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