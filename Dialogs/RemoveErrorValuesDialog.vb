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

''' <summary>
''' Dialog for removing user-specified error values from series
''' </summary>
''' <remarks></remarks>
Friend Class RemoveErrorValuesDialog

    Public zreOrig As Dictionary(Of String, TimeSeries)
    Public zreClean As Dictionary(Of String, TimeSeries)
    Public Const labelAlle As String = "- ALL -"

    Public Sub New(ByRef zeitreihen As Dictionary(Of String, TimeSeries))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.zreOrig = zeitreihen

        'populate combobox
        Me.ComboBox_Series.Items.Add(labelAlle)
        For Each zre As TimeSeries In zeitreihen.Values
            Me.ComboBox_Series.Items.Add(zre)
        Next

        Me.ComboBox_Series.SelectedIndex = 0

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Abort if no series is selected
        If Me.ComboBox_Series.SelectedIndex = -1 Then
            MsgBox("Please select a series!", MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Dim i As Integer
        Dim zre, zre_clean As TimeSeries
        Dim errorstrings() As String
        Dim errorvalue, errorvalues() As Double

        'read error values from text box
        errorstrings = Me.TextBox_errorvalues.Text.Split(",")
        ReDim errorvalues(errorstrings.GetUpperBound(0))
        i = 0
        For Each str As String In errorstrings
            If Not Double.TryParse(str, errorvalue) Then
                MsgBox(String.Format("Could not parse the value '{0}'!", str), MsgBoxStyle.Exclamation, "Wave")
                Exit Sub
            Else
                errorvalues(i) = errorvalue
                i += 1
            End If
        Next

        Me.zreClean = New Dictionary(Of String, TimeSeries)

        If Me.ComboBox_Series.SelectedItem.ToString = labelAlle Then
            'clean all series
            For Each zre In Me.zreOrig.Values
                zre_clean = zre.getCleanZRE(errorvalues)
                zre_clean.Title = zre_clean.Title & " (clean)"
                Me.zreClean.Add(zre_clean.Title, zre_clean)
            Next
        Else
            'clean only the selected series
            zre = Me.ComboBox_Series.SelectedItem
            zre_clean = zre.getCleanZRE(errorvalues)
            zre_clean.Title = zre_clean.Title & " (clean)"
            Me.zreClean.Add(zre_clean.Title, zre_clean)
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
