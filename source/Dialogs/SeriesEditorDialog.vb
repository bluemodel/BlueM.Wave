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
''' <summary>
''' Dialog for entering a time series (or copy/paste from Excel)
''' </summary>
''' <remarks></remarks>
Friend Class SeriesEditorDialog

    Private IsInitializing As Boolean
    Private mZeitreihe As TimeSeries

    ''' <summary>
    ''' Series title as entered by the user
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property series_Title() As String
        Get
            Return Me.TextBox_Title.Text
        End Get
    End Property

    ''' <summary>
    ''' Series unit as entered by the user
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Defaults to "-" if left empty</remarks>
    Private ReadOnly Property series_Unit() As String
        Get
            If (Not Me.TextBox_Unit.Text.Trim() = "") Then
                Return Me.TextBox_Unit.Text
            Else
                Return "-"
            End If
        End Get
    End Property

    ''' <summary>
    ''' The final timeseries object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Zeitreihe() As TimeSeries
        Get
            Return Me.mZeitreihe
        End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        Me.IsInitializing = True

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.IsInitializing = False

    End Sub


    ''' <summary>
    ''' Processes a paste command
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Paste(sender As System.Object, e As System.EventArgs) Handles Button_Paste.Click

        Dim CSVSeparator As String
        Dim ClipboardContents As IDataObject
        Dim line, row() As String

        'aktuell verwendetes Listentrennzeichen bestimmen
        CSVSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator

        'Inhalt der Zwischenablage einlesen
        ClipboardContents = Clipboard.GetDataObject()

        'Prüfen, ob ClipboardContents im CSV-Format vorliegen oder konvertiert werden können
        If Not ClipboardContents.GetDataPresent(DataFormats.CommaSeparatedValue, True) Then
            MsgBox("Unable to process the clipboard contents!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Me.Enabled = False

        'ClipboardContents zu CSV konvertieren und in einen StreamReader packen
        Dim sr As New System.IO.StreamReader(CType(ClipboardContents.GetData(DataFormats.CommaSeparatedValue), System.IO.MemoryStream))

        'Alle Zeilen verarbeiten und zu DataGridView hinzufügen
        Do Until sr.Peek = -1
            line = sr.ReadLine()
            If (line.Length > 0 And line.Contains(CSVSeparator)) Then
                row = line.Split(CSVSeparator, 2, StringSplitOptions.RemoveEmptyEntries)
                Me.DataGridView1.Rows.Add(row)
            End If
        Loop

        Me.Cursor = Cursors.Default
        Me.Enabled = True

    End Sub

    ''' <summary>
    ''' Checks the entered dates for parseability
    ''' </summary>
    ''' <returns>Boolean success</returns>
    ''' <remarks></remarks>
    Private Function CheckDates() As Boolean

        Dim str As String
        Dim i As Integer
        Dim cell As DataGridViewCell
        Dim t As DateTime

        Dim isOK As Boolean = False

        Try

            Me.Cursor = Cursors.WaitCursor
            Me.Enabled = False

            'Loop through all rows except for the last
            For i = 0 To Me.DataGridView1.RowCount - 2

                'Date column
                cell = Me.DataGridView1.Rows(i).Cells(0)
                str = cell.Value
                'Check whether the date is parseable
                Try
                    t = DateTime.Parse(str)
                Catch ex As Exception
                    cell.ErrorText = "Date format not recognized!"
                    Throw New Exception($"The date '{str}' can not be parsed!")
                End Try

                cell.ErrorText = String.Empty

            Next

            isOK = True

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            isOK = False

        Finally

            Me.Cursor = Cursors.Default
            Me.Enabled = True

        End Try

        Return isOK

    End Function

    ''' <summary>
    ''' OK button clicked. Checks all entries and creates the time series object. If successful, the dialog is closed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_OK_Click(sender As System.Object, e As System.EventArgs) Handles Button_OK.Click

        Dim i As Integer
        Dim timestamp As DateTime
        Dim value As Double

        'check the title
        If (Me.TextBox_Title.Text = String.Empty) Then
            MsgBox("Please enter a title for the series!", MsgBoxStyle.Exclamation)
            Me.TextBox_Title.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        End If

        'check the dates
        If Not Me.CheckDates() Then
            Me.DialogResult = DialogResult.None
            Exit Sub
        End If

        'create a new time series object
        Me.mZeitreihe = New TimeSeries(Me.series_Title)
        Me.mZeitreihe.DataSource = New TimeSeriesDataSource(TimeSeriesDataSource.OriginEnum.ManuallyEntered)

        'store the unit
        Me.mZeitreihe.Unit = Me.series_Unit

        'add the nodes
        Me.Cursor = Cursors.WaitCursor
        Me.Enabled = False
        For i = 0 To Me.DataGridView1.RowCount - 2 '(letzte Zeile nicht mitnehmen)
            timestamp = DateTime.Parse(Me.DataGridView1.Rows(i).Cells(0).Value)
            value = Helpers.StringToDouble(Me.DataGridView1.Rows(i).Cells(1).Value)
            Me.mZeitreihe.AddNode(timestamp, value)
        Next
        Me.Cursor = Cursors.Default
        Me.Enabled = True

        Me.Close()

    End Sub

    ''' <summary>
    ''' Handles Ctrl+V key event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If (e.Control And e.KeyCode = Keys.V) Then
            Call Me.Paste(sender, New EventArgs())
        End If
    End Sub
End Class