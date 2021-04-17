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
''' <remarks>implementiert als Singleton-Klasse</remarks>
Friend Class Log

    Private Shared myInstance As Log
    Private Shared logWindow As LogWindow

    ''' <summary>
    ''' Logging level, value set here may be overwritten by application settings
    ''' </summary>
    Friend Shared level As levels = levels.info

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
    Public Shared Event LogMsgAdded(level As Log.levels, msg As String)

    Private Sub New()
        'attempt to reading loggingLevel from application settings
        Try
            Log.level = [Enum].Parse(GetType(levels), My.Settings.loggingLevel)
        Catch ex As Exception
            'set default logging level to info
            Log.level = levels.info
        End Try
    End Sub

    ''' <summary>
    ''' Gibt die (einzige) Instanz des Logs zurück
    ''' </summary>
    Public Shared Function getInstance() As Log
        If (IsNothing(myInstance)) Then
            myInstance = New Log()
            'instantiate log window
            If IsNothing(logWindow) Then
                logWindow = New LogWindow()
            End If
        End If
        Return myInstance
    End Function

    ''' <summary>
    ''' Adds a log entry
    ''' </summary>
    ''' <param name="level">log level</param>
    ''' <param name="msg">message</param>
    Public Shared Sub AddLogEntry(ByVal level As levels, ByVal msg As String)

        If level >= Log.level Then
            If (msg.Contains(Constants.eol)) Then
                'Wenn Eintrag mehrzeilig, dann formatieren
                msg = Constants.eol & "  " & msg.Replace(Constants.eol, Constants.eol & "  ")
            End If

            Log.logWindow.AddLogEntry(level, msg)

            RaiseEvent LogMsgAdded(level, msg)
        End If

    End Sub

    ''' <summary>
    ''' Log zurücksetzen (allen Text löschen)
    ''' </summary>
    Public Shared Sub ClearLog()
        Log.logWindow.ClearLog()
        RaiseEvent LogMsgAdded(Log.levels.info, "")
    End Sub

    Public Shared Sub HideLogWindow()
        Log.logWindow.Hide()
    End Sub

    Public Shared Sub ShowLogWindow()
        Log.logWindow.Show()
        Log.logWindow.WindowState = FormWindowState.Normal
        Log.logWindow.BringToFront()
    End Sub


End Class