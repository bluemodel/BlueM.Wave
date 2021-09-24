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
Imports System.Windows.Forms

Friend Class LogWindow

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.AddLogEntry(Log.levels.info, "Logging level is set to " & Log.level.ToString())
    End Sub

    Friend Sub AddLogEntry(level As Log.levels, msg As String)

        Dim start, length As Integer

        start = Me.TextBox_Log.TextLength

        'format log entry
        msg = String.Format("* {0} {1}: {2}", DateTime.Now.ToString(Helpers.DefaultDateFormat), level.ToString.ToUpper(), msg) & eol

        length = msg.Length

        'append text
        Me.TextBox_Log.AppendText(msg)

        'select new entry
        Me.TextBox_Log.Select(start, length)

        Dim currentFont As Font = Me.TextBox_Log.Font

        Dim boldFont As New Font(
            currentFont.FontFamily,
            currentFont.Size,
            FontStyle.Bold)

        'apply formatting
        Select Case level
            Case Log.levels.debug
                Me.TextBox_Log.SelectionColor = Color.DarkGreen
                Me.TextBox_Log.SelectionFont = boldFont
            Case Log.levels.warning
                Me.TextBox_Log.SelectionColor = Color.DarkOrange
                Me.TextBox_Log.SelectionFont = boldFont
            Case Log.levels.error
                Me.TextBox_Log.SelectionColor = Color.Red
                Me.TextBox_Log.SelectionFont = boldFont
            Case Else
                Me.TextBox_Log.SelectionColor = Color.Black
                Me.TextBox_Log.SelectionFont = currentFont
        End Select

        'unselect and reset formatting
        Me.TextBox_Log.Select(Me.TextBox_Log.Text.Length, 0)
        Me.TextBox_Log.SelectionColor = Color.Black
        Me.TextBox_Log.SelectionFont = currentFont

        'scroll to end
        Me.TextBox_Log.SelectionStart = Me.TextBox_Log.Text.Length
        Me.TextBox_Log.ScrollToCaret()

        Call Application.DoEvents()
    End Sub

    Friend Sub ClearLog()
        Me.TextBox_Log.Text = ""
        Call Application.DoEvents()
    End Sub

    'Form schließen
    '**************
    Private Sub LogWindow_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        'verhindern, dass das Formular komplett gelöscht wird
        e.Cancel = True

        'Formular verstecken
        Call Me.Hide()

    End Sub

    Private Sub ToolStripButton_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Clear.Click
        Call Log.ClearLog()
    End Sub

    Private Sub ToolStripButton_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_Save.Click
        If (Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK) Then
            Me.TextBox_Log.SaveFile(Me.SaveFileDialog1.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub

End Class
