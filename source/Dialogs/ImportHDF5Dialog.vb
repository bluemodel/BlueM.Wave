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
Imports System.Text.RegularExpressions

''' <summary>
''' Custom import dialog for HDF5 files with two-panel selection:
''' Left panel: Select base elements (e.g., T_Dru0161, T_Dru0800)
''' Right panel: Select variable columns (e.g., Q_zu, Q_ab, Pges_zu, AFS_ab)
''' </summary>
Friend Class ImportHDF5Dialog
    Inherits Form

    Private tsFile As Fileformats.GINA_HDF5

    'Data structures for grouping
    'Key: base element name (e.g., "T_Dru0161"), Value: list of series info for that element
    Private elementSeriesMap As New Dictionary(Of String, List(Of TimeSeriesInfo))
    'Key: variable name (e.g., "Q_zu"), Value: unit
    Private variableUnitsMap As New Dictionary(Of String, String)
    'Sorted list of unique base element names
    Private elementNames As New List(Of String)
    'Sorted list of unique variable names
    Private variableNames As New List(Of String)

    Public Sub New(ByRef fileInstance As Fileformats.GINA_HDF5)
        Me.tsFile = fileInstance
        Call InitializeComponent()
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'File name label
        Label_FileName.Text = IO.Path.GetFileName(Me.tsFile.File)

        'Read series info from file
        Call Me.tsFile.readSeriesInfo()

        'Clear existing data
        elementSeriesMap.Clear()
        variableUnitsMap.Clear()
        elementNames.Clear()
        variableNames.Clear()

        'Process each series to extract element names and variable names
        For Each sInfo As TimeSeriesInfo In Me.tsFile.TimeSeriesInfos
            'Series name format: "DatasetName_ColumnName" (e.g., "T_Dru0161_Q_zu")
            'Parse to get: dataset name and column name
            Dim lastUnderscore As Integer = sInfo.Name.LastIndexOf("_"c)
            If lastUnderscore <= 0 Then Continue For

            Dim datasetName As String = sInfo.Name.Substring(0, lastUnderscore)
            Dim columnName As String = sInfo.Name.Substring(lastUnderscore + 1)

            'Extract base element name from dataset name
            Dim baseElement As String = ExtractBaseElementName(datasetName)
            Dim measurementType As String = ExtractMeasurementType(datasetName, baseElement)

            'Build variable name: measurementType_columnName (e.g., "Q_zu") or just columnName if no measurement type
            Dim variableName As String
            If measurementType.Length > 0 Then
                variableName = $"{measurementType}_{columnName}"
            Else
                variableName = columnName
            End If

            'Add to element series map
            If Not elementSeriesMap.ContainsKey(baseElement) Then
                elementSeriesMap.Add(baseElement, New List(Of TimeSeriesInfo))
                elementNames.Add(baseElement)
            End If
            elementSeriesMap(baseElement).Add(sInfo)

            'Add to variable units map (store unit for display)
            If Not variableUnitsMap.ContainsKey(variableName) Then
                variableUnitsMap.Add(variableName, sInfo.Unit)
                variableNames.Add(variableName)
            End If
        Next

        'Sort element names
        elementNames.Sort()
        variableNames.Sort()

        'Populate elements list
        ListBox_Elements.BeginUpdate()
        ListBox_Elements.Items.Clear()
        For Each elementName In elementNames
            ListBox_Elements.Items.Add(elementName)
        Next
        ListBox_Elements.EndUpdate()

        'Populate variables list with units
        CheckedListBox_Variables.BeginUpdate()
        CheckedListBox_Variables.Items.Clear()
        For Each varName In variableNames
            Dim unit As String = variableUnitsMap(varName)
            Dim displayText As String = If(unit <> "-" AndAlso unit <> "", $"{varName} [{unit}]", varName)
            CheckedListBox_Variables.Items.Add(displayText, True)  'Check all by default
        Next
        CheckedListBox_Variables.EndUpdate()

        'Update labels
        Label_Elements.Text = $"Elements ({elementNames.Count}):"
        Label_Variables.Text = $"Variables ({variableNames.Count}):"

    End Sub


    ''' <summary>
    ''' Extracts the base element name from a dataset name
    ''' Pattern: Element names like T_Dru0161, T_Sch0821, T_Wfg0014 end with digits
    ''' Dataset names may have additional suffixes like _Q, _Pges, _AFS
    ''' </summary>
    Private Function ExtractBaseElementName(datasetName As String) As String
        'Pattern: Match element names that end with digits (e.g., T_Dru0161)
        'The base element is everything up to and including the numeric suffix
        Dim match As Match = Regex.Match(datasetName, "^(.+?\d+)")
        If match.Success Then
            Return match.Groups(1).Value
        Else
            'If no pattern match, use the full name up to the last underscore
            Dim lastUnderscore As Integer = datasetName.LastIndexOf("_"c)
            If lastUnderscore > 0 Then
                Return datasetName.Substring(0, lastUnderscore)
            Else
                Return datasetName
            End If
        End If
    End Function

    ''' <summary>
    ''' Extracts the measurement type from a dataset name (e.g., "Q" from "T_Dru0161_Q")
    ''' </summary>
    Private Function ExtractMeasurementType(datasetName As String, baseElementName As String) As String
        If datasetName.Length > baseElementName.Length AndAlso datasetName.StartsWith(baseElementName) Then
            Dim suffix As String = datasetName.Substring(baseElementName.Length)
            If suffix.StartsWith("_") Then
                Return suffix.Substring(1)
            Else
                Return suffix
            End If
        Else
            Return ""
        End If
    End Function

    Private Sub Button_SelectAllElements_Click(sender As Object, e As EventArgs) Handles Button_SelectAllElements.Click
        For i As Integer = 0 To ListBox_Elements.Items.Count - 1
            ListBox_Elements.SetSelected(i, True)
        Next
    End Sub

    Private Sub Button_SelectNoneElements_Click(sender As Object, e As EventArgs) Handles Button_SelectNoneElements.Click
        ListBox_Elements.ClearSelected()
    End Sub

    Private Sub Button_SelectAllVariables_Click(sender As Object, e As EventArgs) Handles Button_SelectAllVariables.Click
        For i As Integer = 0 To CheckedListBox_Variables.Items.Count - 1
            CheckedListBox_Variables.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub Button_SelectNoneVariables_Click(sender As Object, e As EventArgs) Handles Button_SelectNoneVariables.Click
        For i As Integer = 0 To CheckedListBox_Variables.Items.Count - 1
            CheckedListBox_Variables.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click
        'Validate selection
        If ListBox_Elements.SelectedItems.Count < 1 Then
            MsgBox("Please select at least one element!", MsgBoxStyle.Exclamation)
            Me.DialogResult = DialogResult.None
            Exit Sub
        End If

        If CheckedListBox_Variables.CheckedItems.Count < 1 Then
            MsgBox("Please select at least one variable!", MsgBoxStyle.Exclamation)
            Me.DialogResult = DialogResult.None
            Exit Sub
        End If

        'Build list of selected variable names (without unit suffix)
        Dim selectedVariables As New List(Of String)
        For Each item In CheckedListBox_Variables.CheckedItems
            Dim displayText As String = item.ToString()
            'Remove unit suffix if present: "Q_zu [l/s]" -> "Q_zu"
            Dim bracketPos As Integer = displayText.IndexOf(" [")
            If bracketPos > 0 Then
                selectedVariables.Add(displayText.Substring(0, bracketPos))
            Else
                selectedVariables.Add(displayText)
            End If
        Next

        'Build set of selected element names
        Dim selectedElements As New HashSet(Of String)
        For Each elementName As String In ListBox_Elements.SelectedItems
            selectedElements.Add(elementName)
        Next

        'Select series that match both selected elements AND selected variables
        For Each sInfo As TimeSeriesInfo In Me.tsFile.TimeSeriesInfos
            'Parse series name
            Dim lastUnderscore As Integer = sInfo.Name.LastIndexOf("_"c)
            If lastUnderscore <= 0 Then Continue For

            Dim datasetName As String = sInfo.Name.Substring(0, lastUnderscore)
            Dim columnName As String = sInfo.Name.Substring(lastUnderscore + 1)

            Dim baseElement As String = ExtractBaseElementName(datasetName)
            Dim measurementType As String = ExtractMeasurementType(datasetName, baseElement)

            Dim variableName As String
            If measurementType.Length > 0 Then
                variableName = $"{measurementType}_{columnName}"
            Else
                variableName = columnName
            End If

            'Check if this series should be selected
            If selectedElements.Contains(baseElement) AndAlso selectedVariables.Contains(variableName) Then
                Me.tsFile.selectSeries(sInfo.Index)
            End If
        Next

        Me.tsFile.TitleSuffix = Me.TextBox_TitleSuffix.Text.TrimEnd()
    End Sub

End Class
