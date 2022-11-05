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

Friend Class ChangeTimestepDialog

    Public Sub New(ByRef ts As TimeSeries)

        Call InitializeComponent()

        'fill comboboxes
        Me.ComboBox_InputInterpretation.Items.Add(TimeSeries.InterpretationEnum.Instantaneous)
        Me.ComboBox_InputInterpretation.Items.Add(TimeSeries.InterpretationEnum.BlockRight)
        Me.ComboBox_InputInterpretation.Items.Add(TimeSeries.InterpretationEnum.BlockLeft)
        Me.ComboBox_InputInterpretation.Items.Add(TimeSeries.InterpretationEnum.Cumulative)
        Me.ComboBox_InputInterpretation.Items.Add(TimeSeries.InterpretationEnum.CumulativePerTimestep)

        'Me.ComboBox_OutputInterpretation.Items.Add(TimeSeries.InterpretationEnum.Instantaneous) 'not supported
        Me.ComboBox_OutputInterpretation.Items.Add(TimeSeries.InterpretationEnum.BlockRight)
        Me.ComboBox_OutputInterpretation.Items.Add(TimeSeries.InterpretationEnum.BlockLeft)
        Me.ComboBox_OutputInterpretation.Items.Add(TimeSeries.InterpretationEnum.Cumulative)
        Me.ComboBox_OutputInterpretation.Items.Add(TimeSeries.InterpretationEnum.CumulativePerTimestep)

        'set initial selection according to the timeseries' interpretation
        Select Case ts.Interpretation
            Case TimeSeries.InterpretationEnum.BlockRight,
                 TimeSeries.InterpretationEnum.BlockLeft,
                 TimeSeries.InterpretationEnum.Cumulative,
                 TimeSeries.InterpretationEnum.CumulativePerTimestep
                Me.ComboBox_InputInterpretation.SelectedItem = ts.Interpretation
                Me.ComboBox_OutputInterpretation.SelectedItem = ts.Interpretation
            Case TimeSeries.InterpretationEnum.Instantaneous
                'if input is instantaneous, suggest BlockRight as output
                Me.ComboBox_InputInterpretation.SelectedItem = ts.Interpretation
                Me.ComboBox_OutputInterpretation.SelectedItem = TimeSeries.InterpretationEnum.BlockRight
            Case Else
                'if input is undefined, suggest BlockRight for both input and output
                Me.ComboBox_InputInterpretation.SelectedItem = TimeSeries.InterpretationEnum.BlockRight
                Me.ComboBox_OutputInterpretation.SelectedItem = TimeSeries.InterpretationEnum.BlockRight
        End Select

        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Year)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Month)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Week)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Day)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Hour)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Minute)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Second)
        Me.ComboBox_TimestepType.SelectedIndex = 3

        'set CurrentCulture for MaskedTextBoxes
        Me.MaskedTextBox_Start.Culture = Globalization.CultureInfo.CurrentCulture
        Me.MaskedTextBox_Start.FormatProvider = Globalization.CultureInfo.CurrentCulture

        'set the initial start date
        Me.MaskedTextBox_Start.Text = ts.StartDate
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
