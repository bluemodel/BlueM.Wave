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
Friend Class GoodnessOfFit_Dialog

    Public Sub New(seriesList As List(Of TimeSeries))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ComboBox_ObservedSeries.BeginUpdate()
        Me.ComboBox_ObservedSeries.Items.AddRange(seriesList.ToArray())
        Me.ComboBox_ObservedSeries.EndUpdate()
        Me.ComboBox_ObservedSeries.SelectedIndex = 0

        Me.ComboBox_startMonth.BeginUpdate()
        Me.ComboBox_startMonth.Items.AddRange(Helpers.CalendarMonths.ToArray)
        Me.ComboBox_startMonth.EndUpdate()
        Me.ComboBox_startMonth.SelectedIndex = 10 'November
    End Sub

    ''' <summary>
    ''' Returns the Timeseries selected as "observed"
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property seriesObserved() As TimeSeries
        Get
            Return Me.ComboBox_ObservedSeries.SelectedItem
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of Timeseries _not_ selected as "observed"
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property seriesSimulated() As List(Of TimeSeries)
        Get
            Dim ts_list As New List(Of TimeSeries)
            Dim index As Integer = 0
            For Each ts As TimeSeries In Me.ComboBox_ObservedSeries.Items
                If index <> Me.ComboBox_ObservedSeries.SelectedIndex Then
                    ts_list.Add(ts)
                End If
                index += 1
            Next
            Return ts_list
        End Get
    End Property

    Private Sub CheckBox_Annual_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Annual.CheckedChanged
        If CheckBox_Annual.Checked Then
            Me.Label_startMonth.Enabled = True
            Me.ComboBox_startMonth.Enabled = True
        Else
            Me.Label_startMonth.Enabled = False
            Me.ComboBox_startMonth.Enabled = False
        End If
    End Sub
End Class