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
Imports System.IO
Imports System.Text.RegularExpressions

Friend Class ImportDiag
    Inherits System.Windows.Forms.Form

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private IsInitializing As Boolean

    Private datei As TimeSeriesFile
    Private WithEvents inputTimer As Timers.Timer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets and sets the date format string in the corresponding combobox while escaping and unescaping any special characters.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' see https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
    ''' </remarks>
    Private Property DateFormat() As String
        Get
            Dim format As String
            Dim m As MatchCollection
            Dim specialchars As List(Of String)

            format = Me.ComboBox_Dateformat.Text
            'Escape any unescaped special characters
            specialchars = New List(Of String)(New String() {"/", ":"})
            For Each c As String In specialchars
                m = Regex.Matches(format, "[^\\]" + c)
                If m.Count > 0 Then
                    format = format.Replace(c, "\" & c)
                End If
            Next
            Return format
        End Get
        Set(datumsformat As String)
            'unescape special characters
            datumsformat = datumsformat.Replace("\", "")
            'add to combobox
            ComboBox_Dateformat.BeginUpdate()
            If Not ComboBox_Dateformat.Items.Contains(datumsformat) Then
                ComboBox_Dateformat.Items.Add(datumsformat)
            End If
            'select it
            ComboBox_Dateformat.SelectedItem = datumsformat
            ComboBox_Dateformat.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Gets and sets the selected Encoding
    ''' </summary>
    ''' <returns></returns>
    Private Property selectedEncoding As System.Text.Encoding
        Get
            Return CType(Me.ComboBox_Encoding.SelectedItem, System.Text.EncodingInfo).GetEncoding()
        End Get
        Set(value As System.Text.Encoding)
            Me.ComboBox_Encoding.SelectedValue = value.CodePage
        End Set
    End Property

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByRef fileInstance As TimeSeriesFile)

        Call MyBase.New()

        IsInitializing = True

        Call InitializeComponent()

        Me.datei = fileInstance

        'add type name of file instance to dialog title
        Me.Text &= $" - file format: {Me.datei.GetType().Name}"

        'initialize input delay timer
        Me.inputTimer = New Timers.Timer(1000)
        Me.inputTimer.SynchronizingObject = Me
        Me.inputTimer.AutoReset = False

    End Sub

    'Form laden
    '**********
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Combobox Trennzeichen initialisieren
        Me.ComboBox_Separator.BeginUpdate()
        Me.ComboBox_Separator.Items.Add(Constants.semicolon)
        Me.ComboBox_Separator.Items.Add(Constants.comma)
        Me.ComboBox_Separator.Items.Add(Constants.period)
        Me.ComboBox_Separator.Items.Add(Constants.space)
        Me.ComboBox_Separator.Items.Add(Constants.tab)
        Me.ComboBox_Separator.EndUpdate()

        'Combobox Dezimaltrennzeichen initialisieren
        Me.ComboBox_DecimalSeparator.BeginUpdate()
        Me.ComboBox_DecimalSeparator.Items.Add(Constants.period)
        Me.ComboBox_DecimalSeparator.Items.Add(Constants.comma)
        Me.ComboBox_DecimalSeparator.EndUpdate()

        'Combobox Datumsformat f�llen
        For Each datumsformat As String In Helpers.DateFormats.Values
            Me.DateFormat = datumsformat
        Next
        Me.ComboBox_Dateformat.SelectedIndex = 0

        'Combobox encoding
        Me.ComboBox_Encoding.DataSource = System.Text.Encoding.GetEncodings()
        Me.ComboBox_Encoding.DisplayMember = "Name"
        Me.ComboBox_Encoding.ValueMember = "CodePage"
        Me.ComboBox_Encoding.SelectedValue = Me.datei.Encoding.CodePage

        'Versuchen, die Spalten auszulesen (mit Standardeinstellungen)
        Call Me.datei.readSeriesInfo()

        'Anzeige aktualisieren
        Call Me.aktualisieren()

        'Ende der Initialisierung
        IsInitializing = False

        'Datei als Vorschau anzeigen
        Call Me.VorschauAnzeigen()

    End Sub

    'Vorschau der Datei anzeigen
    '***************************
    Private Sub VorschauAnzeigen()

        Const anzZeilen As Integer = 50 'maximal 50 Zeilen anzeigen
        Const anzSpalten As Integer = 3500 'maximal 3500 Spalten anzeigen (bei mehr wird immer umgebrochen!)
        Dim line, text As String

        'Dateiname anzeigen
        Me.Label_File.Text = "File: " & Path.GetFileName(Me.datei.File)

        'Workaround for binary file formats
        If TypeOf Me.datei Is Fileformats.SWMM_OUT _
            Or TypeOf Me.datei Is Fileformats.SydroSQLite _
            Or TypeOf Me.datei Is Fileformats.DFS0 _
            Or TypeOf Me.datei Is Fileformats.WBL Then

            Me.TextBox_Preview.Text = $"{Path.GetFileName(Me.datei.File)} is a binary file.{eol}Preview is not available!"
            'Disable all other fields
            Me.GroupBox_Dateformat.Enabled = False
            Me.GroupBox_Columns.Enabled = False
            Me.GroupBox_DecimalMark.Enabled = False
            Me.GroupBox_Settings.Enabled = False
            Me.Label_Encoding.Enabled = False
            Me.ComboBox_Encoding.Enabled = False
            Me.Button_EncodingAutodetect.Enabled = False

            Exit Sub
        End If

        'Vorschau anzeigen
        Dim fs As New FileStream(Me.datei.File, FileMode.Open, FileAccess.Read)
        Dim StrRead As New StreamReader(fs, Me.selectedEncoding)

        text = ""

        For i As Integer = 1 To anzZeilen

            line = StrRead.ReadLine.ToString()

            'gucken, ob Zeile zu lang
            If (line.Length > anzSpalten) Then
                line = line.Substring(0, anzSpalten) & " ..."
            End If

            'replace tab characters with a visual representation
            line = line.Replace(vbTab, " � ")

            text &= line & Constants.eol

            'gucken, ob Dateiende
            If (StrRead.Peek = -1) Then Exit For
        Next

        If (StrRead.Peek <> -1) Then
            text &= "..."
        End If

        Me.TextBox_Preview.Text = text

        StrRead.Close()
        fs.Close()

    End Sub

    'OK Button gedr�ckt
    '******************
    Private Sub Button_OK_Click(sender As Object, e As EventArgs) Handles Button_OK.Click

        'Selected series
        If (Me.ListBox_Series.SelectedItems.Count < 1) Then
            MsgBox("Please select at least one series!", MsgBoxStyle.Exclamation)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        Else
            For Each sInfo As TimeSeriesInfo In Me.ListBox_Series.SelectedItems
                Me.datei.selectSeries(sInfo.Index)
            Next
        End If

    End Sub

    'Benutzereingabe verarbeiten
    '***************************
    Private Sub inputChanged(sender As Object, e As EventArgs) Handles _
        NumericUpDown_LineTitles.TextChanged,
        NumericUpDown_LineUnits.TextChanged,
        NumericUpDown_LineData.TextChanged,
        CheckBox_Units.CheckedChanged,
        ComboBox_DecimalSeparator.SelectedIndexChanged,
        RadioButton_CharSeparated.CheckedChanged,
        ComboBox_Separator.SelectedIndexChanged,
        TextBox_ColumnWidth.TextChanged,
        NumericUpDown_ColumnDateTime.ValueChanged,
        ComboBox_Dateformat.SelectedIndexChanged,
        ComboBox_Dateformat.LostFocus,
        ComboBox_Encoding.SelectedIndexChanged

        If (Me.IsInitializing = True) Then
            Exit Sub
        Else

            'Eingaben speichern
            '------------------
            Try

                'Datenzeile muss nach �berschriften und Einheiten sein!
                Me.IsInitializing = True
                If (Me.NumericUpDown_LineData.Value <= Me.NumericUpDown_LineTitles.Value) Then
                    Me.NumericUpDown_LineData.Value = Me.NumericUpDown_LineTitles.Value + 1
                End If
                If (Me.CheckBox_Units.Checked _
                    And Me.NumericUpDown_LineData.Value <= Me.NumericUpDown_LineUnits.Value) Then
                    Me.NumericUpDown_LineData.Value = Me.NumericUpDown_LineUnits.Value + 1
                End If
                Me.IsInitializing = False

                'Zeilennummern
                Me.datei.iLineHeadings = Me.NumericUpDown_LineTitles.Value
                Me.datei.iLineData = Me.NumericUpDown_LineData.Value

                'Einheiten
                Me.datei.UseUnits = Me.CheckBox_Units.Checked
                If (Me.CheckBox_Units.Checked) Then
                    Me.datei.iLineUnits = Me.NumericUpDown_LineUnits.Value
                End If

                'Datumsformat
                Me.datei.Dateformat = Me.DateFormat

                'Dezimaltrennzeichen
                Me.datei.DecimalSeparator = Me.ComboBox_DecimalSeparator.SelectedItem

                'Spalteneinstellungen
                If (Me.RadioButton_CharSeparated.Checked) Then
                    Me.datei.IsColumnSeparated = True
                    Me.datei.Separator = Me.ComboBox_Separator.SelectedItem
                Else
                    Me.datei.IsColumnSeparated = False
                    Me.datei.ColumnWidth = Convert.ToInt32(Me.TextBox_ColumnWidth.Text)
                End If

                'Datum
                Me.datei.DateTimeColumnIndex = Me.NumericUpDown_ColumnDateTime.Value - 1 'Immer eins weniger wie du ! 

                'Encoding
                Me.datei.Encoding = Me.selectedEncoding

                'Spalten neu auslesen
                Call Me.datei.readSeriesInfo()

                'Anzeige aktualisieren
                Call Me.aktualisieren()

                'Wenn alles glatt gelaufen ist:
                Me.StatusImage.Image = Global.BlueM.Wave.My.Resources.Resources.tick
                Me.StatusImage.Text = "OK"

            Catch ex As Exception
                'Bei Exception Status auf Fehler setzen
                Me.StatusImage.Image = Global.BlueM.Wave.My.Resources.Resources.warning
                Me.StatusImage.Text = "Error"
            End Try

        End If

    End Sub

    'Anzeige aktualisieren
    '*********************
    Private Sub aktualisieren()

        'Dezimaltrennzeichen
        Me.ComboBox_DecimalSeparator.SelectedItem = Me.datei.DecimalSeparator

        'Zeilennummern
        Me.NumericUpDown_LineTitles.Text = Me.datei.iLineHeadings
        Me.NumericUpDown_LineData.Text = Me.datei.iLineData

        'Einheiten
        If (Me.datei.UseUnits) Then
            Me.CheckBox_Units.Checked = True
            Me.NumericUpDown_LineUnits.Enabled = True
        Else
            Me.CheckBox_Units.Checked = False
            Me.NumericUpDown_LineUnits.Enabled = False
        End If
        Me.NumericUpDown_LineUnits.Text = Me.datei.iLineUnits

        'Spaltenformat
        If (Me.datei.IsColumnSeparated) Then
            Me.RadioButton_CharSeparated.Checked = True
            Me.ComboBox_Separator.Enabled = True
            Me.TextBox_ColumnWidth.Enabled = False
        Else
            Me.RadioButton_FixedWidth.Checked = True
            Me.ComboBox_Separator.Enabled = False
            Me.TextBox_ColumnWidth.Enabled = True
        End If

        'Trennzeichen
        Me.ComboBox_Separator.SelectedItem = Me.datei.Separator

        'Datumsformat
        Me.DateFormat = Me.datei.Dateformat

        'Spaltenbreite
        Me.TextBox_ColumnWidth.Text = Me.datei.ColumnWidth

        'XSpalte
        Me.NumericUpDown_ColumnDateTime.Value = Me.datei.DateTimeColumnIndex + 1

        'Available series
        Dim sInfo As TimeSeriesInfo
        Me.Label_Series.Text = $"Available series ({Me.datei.TimeSeriesInfos.Count}):"
        'remember currently selected series
        Dim selectedSeries As New List(Of String)
        For Each sInfo In Me.ListBox_Series.SelectedItems
            selectedSeries.Add(sInfo.Name)
        Next
        'update list box
        Me.ListBox_Series.Items.Clear()
        Me.ListBox_Series.BeginUpdate()
        For Each sInfo In Me.datei.TimeSeriesInfos
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

        'refresh preview
        Call Me.VorschauAnzeigen()

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
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub searchSeries(sender As Object, e As Timers.ElapsedEventArgs) Handles inputTimer.Elapsed

        Dim search, itemname As String

        search = Me.TextBox_Search.Text.ToLower()

        If (search = "") Then Return

        Me.IsInitializing = True
        Me.ListBox_Series.BeginUpdate()
        Me.ListBox_Series.ClearSelected()
        For i As Integer = 0 To Me.ListBox_Series.Items.Count - 1
            itemname = Me.ListBox_Series.Items(i).ToString().ToLower()
            If (itemname.Contains(search)) Then
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

    Private Sub PictureBox_DateFormatHelp_Click(sender As Object, e As EventArgs) Handles PictureBox_DateFormatHelp.Click
        Process.Start("https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings")
    End Sub

    Private Sub PictureBox_DateFormatHelp_MouseHover(sender As Object, e As EventArgs) Handles PictureBox_DateFormatHelp.MouseHover
        Cursor = Cursors.Hand
    End Sub

    Private Sub PictureBox_DateFormatHelp_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox_DateFormatHelp.MouseLeave
        Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Autodetect encoding from byte order marks
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button_EncodingAutodetect_Click(sender As Object, e As EventArgs) Handles Button_EncodingAutodetect.Click
        Dim enc As Text.Encoding
        'Autodetect file encoding
        Dim strreader As New StreamReader(Me.datei.File, detectEncodingFromByteOrderMarks:=True)
        strreader.ReadLine()
        enc = strreader.CurrentEncoding
        strreader.Close()
        'set detected encoding
        Me.ComboBox_Encoding.SelectedValue = enc.CodePage
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

#End Region 'Methoden

End Class