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
Public Class ExportDiag

    'Formatauswahl verändert
    '***********************
    Private Sub ComboBox_Format_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Format.SelectedIndexChanged

        Select Case ComboBox_Format.SelectedItem

            Case Konstanten.Dateiformate.ZRE
                Me.ListBox_Series.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.REG_HYSTEM
                Me.ListBox_Series.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.REG_SMUSI
                Me.ListBox_Series.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.DAT_SWMM_MASS
                Me.ListBox_Series.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.DAT_SWMM_TIME
                Me.ListBox_Series.SelectionMode = SelectionMode.One

            Case Konstanten.Dateiformate.TXT
                Me.ListBox_Series.SelectionMode = SelectionMode.MultiExtended

            Case Else
                Me.ListBox_Series.SelectionMode = SelectionMode.MultiExtended
                'Noch nicht implementiert
                MsgBox("Noch nicht implementiert!", MsgBoxStyle.Exclamation)
                ComboBox_Format.SelectedItem = Konstanten.Dateiformate.ZRE
        End Select

    End Sub

    'OK-Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click
        'Eingabekontrolle
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Zeitreihe auswählen!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub


    Private Sub Button_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SelectAll.Click

        Dim i As Long

        Select Case ComboBox_Format.SelectedItem
            Case Konstanten.Dateiformate.TXT
                For i = 0 To Me.ListBox_Series.Items.Count - 1
                    Me.ListBox_Series.SetSelected(i, True)
                Next
            Case Else
                MsgBox("Bei diesem Format ist keine mehrfachauswahl möglich")
        End Select

    End Sub

End Class