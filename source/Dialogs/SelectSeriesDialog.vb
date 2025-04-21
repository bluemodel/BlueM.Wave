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
Imports System.Text.RegularExpressions
Friend Class SelectSeriesDialog
    Inherits System.Windows.Forms.Form

    Private IsInitializing As Boolean

    Private tsFile As TimeSeriesFile
    Private WithEvents inputTimer As Timers.Timer

    Public Sub New(ByRef fileInstance As TimeSeriesFile)

        Call MyBase.New()

        IsInitializing = True

        Call InitializeComponent()

        Me.tsFile = fileInstance

        'Set labels
        Me.Label_FileName.Text = IO.Path.GetFileName(Me.tsFile.File)
        Me.Label_FileType.Text = Me.tsFile.GetType().Name

        'initialize input delay timer
        Me.inputTimer = New Timers.Timer(1000)
        Me.inputTimer.SynchronizingObject = Me
        Me.inputTimer.AutoReset = False

        'set intial focus
        Me.TextBox_Search.Focus()

    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Versuchen, die Spalten auszulesen (mit Standardeinstellungen)
        Call Me.tsFile.readSeriesInfo()

        'Anzeige aktualisieren
        Call Me.RefreshDisplay()

        'Ende der Initialisierung
        IsInitializing = False

    End Sub

    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click

        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MsgBox("Please select at least one series!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            For Each sInfo As TimeSeriesInfo In Me.ListBox_Series.SelectedItems
                Me.tsFile.selectSeries(sInfo.Index)
            Next
        End If

    End Sub

    Private Sub RefreshDisplay()

        'Available series
        Dim sInfo As TimeSeriesInfo
        Me.Label_Series.Text = $"Available series ({Me.tsFile.TimeSeriesInfos.Count}):"
        'remember currently selected series
        Dim selectedSeries As New List(Of String)
        For Each sInfo In Me.ListBox_Series.SelectedItems
            selectedSeries.Add(sInfo.Name)
        Next
        'update list box
        Me.ListBox_Series.Items.Clear()
        Me.ListBox_Series.BeginUpdate()
        For Each sInfo In Me.tsFile.TimeSeriesInfos
            Me.ListBox_Series.Items.Add(sInfo)
        Next
        Me.ListBox_Series.EndUpdate()
        'reselect any previously selected items
        For Each sName As String In selectedSeries
            For i As Integer = 0 To Me.ListBox_Series.Items.Count - 1
                sInfo = Me.ListBox_Series.Items(i)
                If sInfo.Name = sName Then
                    Me.ListBox_Series.SetSelected(i, True)
                    Continue For
                End If
            Next
        Next

    End Sub

    ''' <summary>
    ''' Handles text changed in the search text box by resetting the input timer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox_Search_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Search.TextChanged

        'reset the input timer
        Me.inputTimer.Stop()
        Me.inputTimer.Start()

    End Sub

    ''' <summary>
    ''' Do a case-insensitive search for matching list items and select them
    ''' Supports wildcards '*' and '?' in the search string
    ''' '*' matches any number of characters, '?' matches a single character
    ''' The search string is converted to a regex pattern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub searchSeries(sender As Object, e As Timers.ElapsedEventArgs) Handles inputTimer.Elapsed

        Dim search, itemname As String

        search = Me.TextBox_Search.Text

        If (search = "") Then Return

        'convert search string to regex pattern
        Dim pattern As String = Regex.Escape(search).Replace("\ ", " ").Replace("\?", ".").Replace("\*", ".*")

        Log.AddLogEntry(Log.levels.debug, "Searching for series matching pattern: " & pattern)

        Me.IsInitializing = True
        Me.ListBox_Series.BeginUpdate()
        Me.ListBox_Series.ClearSelected()
        For i As Integer = 0 To Me.ListBox_Series.Items.Count - 1
            itemname = Me.ListBox_Series.Items(i).ToString()
            If Regex.IsMatch(itemname, pattern, RegexOptions.IgnoreCase) Then
                Me.ListBox_Series.SetSelected(i, True)
            End If
        Next
        Me.ListBox_Series.EndUpdate()
        Me.IsInitializing = False
        Call ListBox_Series_SelectedIndexChanged(ListBox_Series, New EventArgs())

    End Sub

    ''' <summary>
    ''' Handles Select all button clicked by selecting all series
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Select_All(sender As Object, e As EventArgs) Handles Button_SelectAll.Click

        Me.IsInitializing = True
        Me.ListBox_Series.BeginUpdate()
        For i As Integer = 0 To Me.ListBox_Series.Items.Count - 1
            Me.ListBox_Series.SetSelected(i, True)
        Next
        Me.ListBox_Series.EndUpdate()
        Me.IsInitializing = False
        Call ListBox_Series_SelectedIndexChanged(ListBox_Series, New EventArgs())

    End Sub

    ''' <summary>
    ''' Handles selected series changed by updating the displayed number of selected series
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ListBox_Series_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox_Series.SelectedIndexChanged
        If Not Me.IsInitializing Then
            Me.Label_Selected.Text = $"{ListBox_Series.SelectedIndices.Count} selected"
        End If
    End Sub

    Private Sub ImportDiag_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.inputTimer.Dispose()
    End Sub

End Class