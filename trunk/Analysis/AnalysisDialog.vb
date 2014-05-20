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
Public Class AnalysisDialog

    '*************************************************************
    'Dialog zur Auswahl von Zeitreihe(n) und einer Analysefunktion
    '*************************************************************

    'Konstruktor
    '***********
    Public Sub New(ByVal zeitreihen As Dictionary(Of String, Zeitreihe))

        Call InitializeComponent()

        'Liste der Analysefunktionen
        Me.ComboBox_Funktion.DataSource = System.Enum.GetValues(GetType(AnalysisFactory.AnalysisFunctions))

        'Zeitreihen in Listbox eintragen
        For Each zre As Zeitreihe In zeitreihen.Values
            Me.ListBox_Zeitreihen.Items.Add(zre)
        Next

    End Sub

    'Ausgewählte Analysefunktion
    '***************************
    Friend ReadOnly Property selectedAnalysisFunction() As AnalysisFactory.AnalysisFunctions
        Get
            Return Me.ComboBox_Funktion.SelectedItem
        End Get
    End Property

    'Ausgewählte Zeitreihen
    '**********************
    Friend ReadOnly Property selectedZeitreihen() As List(Of Zeitreihe)
        Get
            Dim zeitreihen As New List(Of Zeitreihe)()
            For Each item As Object In Me.ListBox_Zeitreihen.SelectedItems
                zeitreihen.Add(CType(item, Zeitreihe))
            Next
            Return zeitreihen
        End Get
    End Property

    'OK-Button gedrückt
    '******************
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click
        'Eingabekontrolle
        If (Me.ListBox_Zeitreihen.SelectedItems.Count < 1) Then
            MsgBox("Bitte mindestens eine Zeitreihe auswählen!", MsgBoxStyle.Exclamation, "Wave")
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub


    Private Sub AnalysisDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class