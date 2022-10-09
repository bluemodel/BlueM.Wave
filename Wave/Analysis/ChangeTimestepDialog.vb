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
