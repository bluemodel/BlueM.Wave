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

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

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

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
