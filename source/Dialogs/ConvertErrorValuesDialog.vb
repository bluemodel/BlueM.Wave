'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
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

    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click
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

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
