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
Imports System.Windows.Forms

Public Class LogWindow

#Region "Form behavior"

    Private WithEvents myLog As Log

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.myLog = Log.getInstance()

    End Sub

    Private Sub LogChanged() Handles myLog.LogChanged
        'Textbox mit Logtext aktualisieren
        Me.TextBox_Log.Text = Log.Text
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

#End Region 'Form behavior

End Class
