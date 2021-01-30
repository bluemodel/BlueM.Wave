'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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
    ''' Log levels
    ''' </summary>
    Public Enum levels As Short
        debug
        info
        warning
        [error]
    End Enum

    ''' <summary>
    ''' Wird ausgelöst, wenn sich der Log Text verändert hat
    ''' </summary>
    Public Shared Event LogMsgAdded(level As Log.levels, msg As String)

    Private Sub New()
        'nix
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
    ''' Einen Log-Eintrag hinzufügen
    ''' </summary>
    ''' <param name="msg">Eintrag</param>
    Public Shared Sub AddLogEntry(ByVal level As levels, ByVal msg As String)

        If (msg.Contains(Constants.eol)) Then
            'Wenn Eintrag mehrzeilig, dann formatieren
            msg = Constants.eol & "  " & msg.Replace(Constants.eol, Constants.eol & "  ")
        End If

        Log.logWindow.AddLogEntry(level, msg)

        RaiseEvent LogMsgAdded(level, msg)

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