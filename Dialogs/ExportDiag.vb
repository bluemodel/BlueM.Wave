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
Friend Class ExportDiag

    Public Sub New(ByRef timeseries As TimeSeriesCollection)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'add formats to combobox
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.CSV)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.BIN)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.DFS0)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.HYSTEM_REG)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.SMUSI_REG)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.SWMM_DAT_MASS)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.SWMM_DAT_TIME)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.SWMM_INTERFACE)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.UVF)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.ZRE)
        Me.ComboBox_Format.Items.Add(Fileformats.FileTypes.ZRXP)
        Me.ComboBox_Format.SelectedIndex = 0

        'Add series to listbox
        For Each zre As TimeSeries In timeseries.Values
            Me.ListBox_Series.Items.Add(zre)
        Next

    End Sub

    ''' <summary>
    ''' Selected format changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox_Format_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox_Format.SelectedIndexChanged

        Select Case ComboBox_Format.SelectedItem

            Case Fileformats.FileTypes.ZRE,
                 Fileformats.FileTypes.HYSTEM_REG,
                 Fileformats.FileTypes.SMUSI_REG,
                 Fileformats.FileTypes.SWMM_DAT_MASS,
                 Fileformats.FileTypes.SWMM_DAT_TIME,
                 Fileformats.FileTypes.BIN,
                 Fileformats.FileTypes.UVF,
                 Fileformats.FileTypes.ZRXP
                'Allow selection of a single series
                Me.ListBox_Series.SelectionMode = SelectionMode.One
                Me.Button_SelectAll.Enabled = False

            Case Fileformats.FileTypes.SWMM_INTERFACE,
                 Fileformats.FileTypes.CSV,
                 Fileformats.FileTypes.DFS0
                'Allow selection of multiple series
                Me.ListBox_Series.SelectionMode = SelectionMode.MultiExtended
                Me.Button_SelectAll.Enabled = True

            Case Else
                'not yet implemented
                MsgBox("Not yet implemented!", MsgBoxStyle.Exclamation)
                ComboBox_Format.SelectedIndex = 0
        End Select

    End Sub

    ''' <summary>
    ''' OK button pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_OK_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click

        'validate inputs
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MsgBox("Please select at least one series!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Button 'select all' pressed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_SelectAll_Click(sender As System.Object, e As System.EventArgs) Handles Button_SelectAll.Click

        Dim i As Long
        For i = 0 To Me.ListBox_Series.Items.Count - 1
            Me.ListBox_Series.SetSelected(i, True)
        Next

    End Sub

End Class