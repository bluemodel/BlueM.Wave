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
Friend Class Autocorrelation_Dialog

    Private timeseries As TimeSeries

    Public ReadOnly Property lagSize()
        Get
            'Größe der Lags auslesen
            Return Me.spnGroesseLag.Value
        End Get
    End Property

    Public ReadOnly Property lagCount()
        Get
            'Anzahl der Lags auslesen
            Return Me.spnAnzahlLag.Value()
        End Get
    End Property

    Public Sub New(ts As TimeSeries)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.timeseries = ts

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        'Check max possible lag
        Dim maxlag As Integer = Me.timeseries.Length / lagSize - 1
        If lagSize * lagCount > Me.timeseries.Length Then
            MsgBox(
                $"The selected time series is too short or the largest lag is too long! " &
                $"Please select at most {maxlag} offsets with the currently set number of time steps!", MsgBoxStyle.Exclamation)
            Return
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
