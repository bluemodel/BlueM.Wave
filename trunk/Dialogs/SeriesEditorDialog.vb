'Copyright (c) BlueM Dev Group
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
    Private Sub Paste(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Paste.Click

        Dim CSVSeparator As String
        Dim ClipboardContents As IDataObject
        Dim line, row() As String

        'aktuell verwendetes Listentrennzeichen bestimmen
        CSVSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator

        'Inhalt der Zwischenablage einlesen
        ClipboardContents = Clipboard.GetDataObject()

        'Prüfen, ob ClipboardContents im CSV-Format vorliegen oder konvertiert werden können
        If (Not ClipboardContents.GetDataPresent(DataFormats.CommaSeparatedValue, True)) Then
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
    ''' Checks the entered dates for parseability and ascendingness
    ''' </summary>
    ''' <returns>Boolean success</returns>
    ''' <remarks></remarks>
    Private Function check_dates() As Boolean

        Dim str As String = ""
        Dim i As Integer
        Dim cell As DataGridViewCell
        Dim t0, t1 As DateTime

        Try

            Me.Cursor = Cursors.WaitCursor
            Me.Enabled = False

            t0 = New DateTime(0)

            'Alle Einträge durchlaufen
            For i = 0 To Me.DataGridView1.RowCount - 2 '(letzte Zeile nicht prüfen)

                'Spalte Datum
                '------------
                cell = Me.DataGridView1.Rows(i).Cells(0)
                str = cell.Value
                'Datumsformat prüfen
                Try
                    t1 = DateTime.Parse(str)
                Catch ex As Exception
                    cell.ErrorText = "Date format not recognized!"
                    Throw New Exception("The date '" & str & "' can not be parsed!")
                End Try

                'Prüfen, ob Datum aufsteigend
                If (t1 <= t0) Then
                    cell.ErrorText = "Date not ascending!"
                    Throw New Exception("The date '" & t1 & "' is not ascending!")
                End If

                cell.ErrorText = String.Empty
                t0 = t1

            Next

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Exclamation)

        Finally

            Me.Cursor = Cursors.Default
            Me.Enabled = True

        End Try

        Return True

    End Function

    ''' <summary>
    ''' OK button clicked. Checks all entries and creates the time series object. If successful, the dialog is closed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

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
        If (Not Me.check_dates()) Then
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
    Private Sub DataGridView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If (e.Control And e.KeyCode = Keys.V) Then
            Call Me.Paste(sender, New EventArgs())
        End If
    End Sub
End Class