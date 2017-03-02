﻿'Copyright (c) BlueM Dev Group
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
''' Dialog for setting the break values for the histogram analysis
''' </summary>
''' <remarks></remarks>
Public Class HistogramDialog

    Private min, max As Double
    Friend n_bins As Integer
    Friend breaks As Double()

    Public Sub New(ByRef zres As List(Of Zeitreihe))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.DataGridView_breaks.Columns(0).ValueType = GetType(Double)
        Me.DataGridView_breaks.Columns(0).DefaultCellStyle.Format = "F"
        Me.DataGridView_breaks.Columns(1).ValueType = GetType(Double)
        Me.DataGridView_breaks.Columns(1).DefaultCellStyle.Format = "F"

        'get min and max from series
        Me.min = Double.MaxValue
        Me.max = Double.MinValue
        For Each zre As Zeitreihe In zres
            Me.min = Math.Min(Me.min, zre.getCleanZRE().Minimum)
            Me.max = Math.Max(Me.max, zre.getCleanZRE().Maximum)
        Next
        Me.Label_minmax.Text = String.Format("Value range: {0:F} - {1:F}", min, max)

        'set default number of breaks
        Me.n_bins = 10
        Me.NumericUpDown_n_bins.Value = Me.n_bins

        'update breaks
        Call Me.recalculateBreaks()

    End Sub

    ''' <summary>
    ''' recalculates the breaks based on the current number of bins 
    ''' and displays them in the datagridview
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub recalculateBreaks()

        Dim i As Integer
        Dim n_breaks As Integer
        Dim bin_size As Double

        Me.DataGridView_breaks.Rows.Clear()
        'recalculate breaks
        n_breaks = Me.n_bins + 1
        ReDim Me.breaks(n_breaks - 1)
        bin_size = (Me.max - Me.min) / (Me.n_bins)
        For i = 0 To n_breaks - 1
            Me.breaks(i) = Me.min + i * bin_size
        Next

        'update DataGridView
        For i = 0 To Me.n_bins - 1
            Me.DataGridView_breaks.Rows.Add(Me.breaks(i), Me.breaks(i + 1))
        Next

    End Sub

    ''' <summary>
    ''' sorts the datagridview and updates the fromValue column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sortBreaks()
        'sort the grid
        Me.DataGridView_breaks.Sort(Me.DataGridView_breaks.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
        'update fromValue column
        Me.DataGridView_breaks.Rows(0).Cells(0).Value = Me.min
        For i As Integer = 1 To Me.DataGridView_breaks.Rows.Count - 2
            Me.DataGridView_breaks.Rows(i).Cells(0).Value = Me.DataGridView_breaks.Rows(i - 1).Cells(1).Value
        Next
    End Sub

    ''' <summary>
    ''' OK button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim n_breaks As Integer

        'read breaks from DataGridView
        Me.n_bins = Me.DataGridView_breaks.Rows.Count - 1
        n_breaks = Me.n_bins + 1
        ReDim Me.breaks(n_breaks - 1)
        Me.breaks(0) = Me.DataGridView_breaks.Rows(0).Cells(0).Value
        For i As Integer = 1 To n_breaks - 1
            Me.breaks(i) = Me.DataGridView_breaks.Rows(i - 1).Cells(1).Value
        Next

        'check if max is covered
        If Me.breaks(n_breaks - 1) < Me.max Then
            MsgBox("The breaks do not cover the entire value range!" & eol & "Please add a break that covers the maximum value of " & Me.max.ToString("F"), MsgBoxStyle.Exclamation, "Wave")
            Exit Sub
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Cancel button clicked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "Events"

    Private Sub Button_calcBreaks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_recalculate.Click
        Me.n_bins = Me.NumericUpDown_n_bins.Value
        Call Me.recalculateBreaks()
    End Sub

    ''' <summary>
    ''' Occurs when the value of a cell changes
    ''' </summary>
    Private Sub DataGridView_bins_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView_breaks.CellValueChanged
        If e.RowIndex = -1 Then
            'occurs during initialization
            Exit Sub
        End If
        If String.IsNullOrEmpty(Me.DataGridView_breaks.Rows(e.RowIndex).Cells(1).Value.ToString) Then
            'delete rows with empty cells
            Me.DataGridView_breaks.Rows.RemoveAt(e.RowIndex)
        End If
        Call Me.sortBreaks()
    End Sub

    Private Sub DataGridView_bins_UserAddedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView_breaks.UserAddedRow
        'update NumericUpDown_nBins
        Me.NumericUpDown_n_bins.Value = Me.DataGridView_breaks.Rows.Count - 1
    End Sub

    Private Sub DataGridView_bins_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView_breaks.UserDeletedRow
        'update NumericUpDown_nBins
        Me.NumericUpDown_n_bins.Value = Math.Max(Me.DataGridView_breaks.Rows.Count - 1, 0)
    End Sub

    '''' <summary>
    '''' Occurs when a cell loses input focus, enabling content validation
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub DataGridView_bins_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView_breaks.CellValidating
    '    If String.IsNullOrEmpty(e.FormattedValue.ToString()) Then
    '        Me.DataGridView_breaks.Rows(e.RowIndex).ErrorText = "Value must not be empty"
    '        e.Cancel = True
    '    End If
    'End Sub

    Private Sub DataGridView_bins_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView_breaks.DataError
        MsgBox("The entered value is not valid!", MsgBoxStyle.Critical, "Wave")
    End Sub

#End Region 'Events

End Class
