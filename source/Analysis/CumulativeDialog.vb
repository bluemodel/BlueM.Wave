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

Friend Class CumulativeDialog

    Public Sub New(ByRef ts_list As List(Of TimeSeries))

        Call InitializeComponent()

        'set CurrentCulture for MaskedTextBox
        Me.MaskedTextBox_Start.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_Start.FormatProvider = Globalization.CultureInfo.CurrentCulture

        'set initial start date to latest common start date
        Dim startdates As New List(Of DateTime)
        For Each ts As TimeSeries In ts_list
            startdates.Add(ts.StartDate)
        Next
        Dim startdate As DateTime = startdates.Max
        Me.MaskedTextBox_Start.Text = startdate
    End Sub

    ''' <summary>
    ''' Handles KeyDown in start date textbox
    ''' Resets the color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBoxStartKeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MaskedTextBox_Start.KeyDown
        CType(sender, MaskedTextBox).ForeColor = DefaultForeColor
    End Sub

    ''' <summary>
    ''' Handles ValidationCompleted of start date textbox
    ''' Checks whether input is valid DateTime
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MaskedTextBoxStartValidationCompleted(sender As System.Object, e As TypeValidationEventArgs) Handles MaskedTextBox_Start.TypeValidationCompleted
        If Not e.IsValidInput Then
            e.Cancel = True
            CType(sender, MaskedTextBox).ForeColor = Color.Red
        End If
    End Sub

    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
