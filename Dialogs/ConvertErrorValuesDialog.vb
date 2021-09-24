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

''' <summary>
''' Dialog for converting user-specified error values in series to NaN
''' </summary>
''' <remarks></remarks>
Friend Class ConvertErrorValuesDialog

    Public tsOriginal As List(Of TimeSeries)
    Public tsConverted As List(Of TimeSeries)
    Public Const labelAlle As String = "- ALL -"

    Public Sub New(ByRef seriesList As List(Of TimeSeries))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.tsOriginal = seriesList

        'populate combobox
        Me.ComboBox_Series.Items.Add(labelAlle)
        For Each ts As TimeSeries In seriesList
            Me.ComboBox_Series.Items.Add(ts)
        Next

        Me.ComboBox_Series.SelectedIndex = 0

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Abort if no series is selected
        If Me.ComboBox_Series.SelectedIndex = -1 Then
            MsgBox("Please select a series!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim i As Integer
        Dim ts, ts_new As TimeSeries
        Dim errorstrings() As String
        Dim errorvalue, errorvalues() As Double

        'read error values from text box
        errorstrings = Me.TextBox_errorvalues.Text.Split(",")
        ReDim errorvalues(errorstrings.GetUpperBound(0))
        i = 0
        For Each str As String In errorstrings
            If Not Double.TryParse(str, errorvalue) Then
                MsgBox($"Could not parse the value '{str}'!", MsgBoxStyle.Exclamation)
                Exit Sub
            Else
                errorvalues(i) = errorvalue
                i += 1
            End If
        Next

        Me.tsConverted = New List(Of TimeSeries)

        If Me.ComboBox_Series.SelectedItem.ToString = labelAlle Then
            'clean all series
            For Each ts In Me.tsOriginal
                ts_new = ts.convertErrorValues(errorvalues)
                ts_new.Title &= " (clean)"
                Me.tsConverted.Add(ts_new)
            Next
        Else
            'clean only the selected series
            ts = Me.ComboBox_Series.SelectedItem
            ts_new = ts.convertErrorValues(errorvalues)
            ts_new.Title &= " (clean)"
            Me.tsConverted.Add(ts_new)
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
