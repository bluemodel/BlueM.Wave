'Copyright (c) 2011, ihwb, TU Darmstadt
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

Public Class ImportFromDatabaseDialog

    Private mSelectedResultIDs As New List(Of Long)

    Public ReadOnly Property SelectedResultIDs() As List(Of Long)
        Get
            Return mSelectedResultIDs
        End Get
    End Property




    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (DataGridView_Auswahl.SelectedRows.Count = 0) Then
            DataGridView_Auswahl.Focus()
            Return
        End If

        If (Not FillSelectedResults()) Then
            'Fehler also schlissen
            Me.Close()
        End If

      Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function FillSelectedResults() As Boolean
        FillSelectedResults = False
        Try
            mSelectedResultIDs.Clear()

            For Each row As DataGridViewRow In DataGridView_Auswahl.SelectedRows()
                'in 0 steht die SimresultID
                mSelectedResultIDs.Add(Int32.Parse(row.Cells(0).Value))
            Next

        Catch ex As Exception
            FillSelectedResults = False

        End Try

    End Function



Private Sub ImportFromDatabaseDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'TODO: This line of code loads data into the 'BlueMOrbDataSet.AllInfosAboutSimResults' table. You can move, or remove it, as needed.
Me.AllInfosAboutSimResultsTableAdapter.Fill(Me.BlueMOrbDataSet.AllInfosAboutSimResults)

End Sub


End Class
