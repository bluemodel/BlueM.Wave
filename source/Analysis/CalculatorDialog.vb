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

Friend Class CalculatorDialog

    Public Sub New(tsVariables As List(Of CalculatorVariable))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'Add variables to listbox
        For Each tsvar As CalculatorVariable In tsVariables
            Me.ListBox_Variables.Items.Add(tsvar)
        Next

        'Add unique units to combobox
        Dim units As New HashSet(Of String)
        For Each tsvar As CalculatorVariable In tsVariables
            units.Add(tsvar.ts.Unit)
        Next
        Me.ComboBox_Unit.Items.AddRange(units.ToArray())
        Me.ComboBox_Unit.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Listbox doubleclicked
    ''' Adds the currently selected time series as a variable to the formula at the current cursor position
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ListBox_Variables_DoubleClick(sender As Object, e As EventArgs) Handles ListBox_Variables.DoubleClick

        Dim varName As String = CType(Me.ListBox_Variables.SelectedItem, CalculatorVariable).varName

        Dim originalFormula As String = Me.TextBox_Formula.Text
        Dim newFormula As String

        Dim oldCursorPos As Integer = Me.TextBox_Formula.SelectionStart
        Dim newCursorPos As Integer

        Select Case oldCursorPos
            Case 0
                'add variable to beginning
                newFormula = $"{varName} {originalFormula}"
                newCursorPos = 2

            Case Me.TextBox_Formula.Text.Length - 1
                'add variable to end
                newFormula = $"{originalFormula} {varName}"
                newCursorPos = newFormula.Length - 1

            Case Else
                'add variable inbetween
                newFormula = $"{originalFormula.Substring(0, oldCursorPos)} {varName} {originalFormula.Substring(oldCursorPos)}"
                newCursorPos = oldCursorPos + 3

        End Select

        'remove any superfluous spaces
        newFormula = newFormula.Replace("  ", " ")

        'set new formula text
        Me.TextBox_Formula.Text = newFormula
        Me.TextBox_Formula.SelectionStart = newCursorPos
        Me.TextBox_Formula.SelectionLength = 0
        Me.TextBox_Formula.Focus()

        'unselect listbox
        Me.ListBox_Variables.SelectedIndex = -1
    End Sub

    Private Sub LinkLabel_Help_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel_Help.LinkClicked
        Process.Start("https://wiki.bluemodel.org/index.php/Wave:Calculator")
    End Sub

    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click

        Try
            'Test formula parsing
            Dim parser As New MathParserNet.Parser()
            'register custom functions
            parser.RegisterCustomDoubleFunction("MAX", AddressOf Calculator.Max)
            parser.RegisterCustomDoubleFunction("MIN", AddressOf Calculator.Min)
            'create dummy variables
            For Each tsVariable As CalculatorVariable In Me.ListBox_Variables.Items
                parser.AddVariable(tsVariable.varName, 1.0)
            Next
            'evaluate formula
            Dim result As Double = parser.SimplifyDouble(Me.TextBox_Formula.Text)
            'clean up
            parser.RemoveAllVariables()
            parser.UnregisterAllCustomFunctions()
        Catch ex As Exception
            MsgBox("Error while parsing formula: " & eol & ex.Message, MsgBoxStyle.Critical)
            Return
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As System.Object, e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
