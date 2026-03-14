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

''' <summary>
''' Wave Model
''' </summary>
Public Class Wave

    ''' <summary>
    ''' Is raised when a new time series has been added
    ''' </summary>
    ''' <param name="ts"></param>
    Friend Event SeriesAdded(ts As TimeSeries)

    ''' <summary>
    ''' Is raised when a time series has its properties changed
    ''' </summary>
    ''' <param name="id"></param>
    Friend Event SeriesPropertiesChanged(id As Integer)

    ''' <summary>
    ''' Is raised when a time series is removed
    ''' </summary>
    ''' <param name="id"></param>
    Friend Event SeriesRemoved(id As Integer)

    ''' <summary>
    ''' Is raised when all time series are removed
    ''' </summary>
    Friend Event SeriesCleared()

    ''' <summary>
    ''' Is raised when all the time series are reordered
    ''' </summary>
    Friend Event SeriesAllReordered()

    ''' <summary>
    ''' Is raised when a single time series is reordered
    ''' </summary>
    Friend Event SeriesReordered(id As Integer, direction As Integer)

    ''' <summary>
    ''' Is raised when timestamps should be highlighted
    ''' </summary>
    ''' <param name="timestamps"></param>
    Friend Event HighlightTimestamps(timestamps As List(Of DateTime))

    ''' <summary>
    ''' Is raised after a file was imported
    ''' </summary>
    ''' <param name="file"></param>
    Friend Event FileImported(file As String)

    ''' <summary>
    ''' Is raised when a TEN file is being loaded
    ''' </summary>
    ''' <param name="file"></param>
    Friend Event TENFileLoading(file As String)

    ''' <summary>
    ''' Is raised when the busy state of the model changes
    ''' </summary>
    ''' <param name="isBusy"></param>
    Friend Event IsBusyChanged(isBusy As Boolean)

    ''' <summary>
    ''' Internal collection of time series
    ''' </summary>
    Public TimeSeries As TimeSeriesCollection

    Public Sub New()
        'Kollektionen einrichten
        '-----------------------
        Me.TimeSeries = New TimeSeriesCollection()
    End Sub

    ''' <summary>
    ''' Import series from multiple files
    ''' </summary>
    ''' <param name="files">An enumerable of file paths</param>
    ''' <remarks></remarks>
    Public Sub Import_Files(files As IEnumerable(Of String))
        For Each file As String In files
            Call Me.Import_File(file)
        Next
    End Sub

    ''' <summary>
    ''' Import a file
    ''' </summary>
    ''' <param name="file">file path</param>
    Public Sub Import_File(file As String)

        Dim fileInstance As TimeSeriesFile
        Dim ok As Boolean

        RaiseEvent IsBusyChanged(True)

        'Determine file type
        Dim fileType As TimeSeriesFile.FileTypes = TimeSeriesFile.getFileType(file)

        Select Case fileType

            'some edge cases:
            Case TimeSeriesFile.FileTypes.TEN
                '.TEN file has to be loaded by the controller/view
                RaiseEvent TENFileLoading(file)

            Case TimeSeriesFile.FileTypes.WVP
                'Wave project file
                Call Me.Load_WVP(file)

                'normal files:
            Case Else

                Try
                    'Log
                    Call Log.AddLogEntry(Log.levels.info, $"Importing file '{file}' ...")

                    'Datei-Instanz erzeugen
                    fileInstance = TimeSeriesFile.getInstance(file, fileType)

                    If (fileInstance.UseImportDialog) Then
                        'Falls Importdialog erforderlich, diesen anzeigen
                        ok = Me.ShowImportDialog(fileInstance)
                        Call Application.DoEvents()
                    Else
                        'Ansonsten alle Spalten auswählen
                        Call fileInstance.selectAllSeries()
                        ok = True
                    End If

                    If (ok) Then

                        'Datei einlesen
                        Call fileInstance.readFile()

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, $"File '{file}' imported successfully!")

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, "Loading series in chart...")

                        'Import all time series into the chart
                        For Each ts As TimeSeries In fileInstance.TimeSeries.Values
                            If fileInstance.TitleSuffix.Length > 0 Then
                                'append title suffix if set
                                ts.Title &= fileInstance.TitleSuffix
                            End If
                            'Import the series
                            Call Me.Import_Series(ts)
                        Next

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, "Successfully loaded series in chart!")

                        RaiseEvent FileImported(file)

                    Else
                        'Import abgebrochen
                        Log.AddLogEntry(Log.levels.error, "Import cancelled!")
                    End If

                Catch ex As Exception
                    MsgBox("Error during import:" & eol & ex.Message, MsgBoxStyle.Critical)
                    Call Log.AddLogEntry(Log.levels.error, "Error during import: " & ex.Message)
                End Try

        End Select

        RaiseEvent IsBusyChanged(False)

    End Sub

    ''' <summary>
    ''' Load a Wave project file
    ''' </summary>
    ''' <param name="projectfile">Path to the Wave project file</param>
    ''' <remarks></remarks>
    Private Sub Load_WVP(projectfile As String)

        Try
            Call Log.AddLogEntry(Log.levels.info, $"Loading Wave project file '{projectfile}'...")

            Dim wvp As New Parsers.WVP(projectfile)
            Dim tsList As List(Of TimeSeries) = wvp.Process()

            Call Log.AddLogEntry(Log.levels.info, $"Imported {tsList.Count} timeseries")

            'import the series
            Call Log.AddLogEntry(Log.levels.info, "Loading series in chart...")
            For Each ts As TimeSeries In tsList
                Call Me.Import_Series(ts)
            Next

            'Log
            Call Log.AddLogEntry(Log.levels.info, $"Project file '{projectfile}' loaded successfully!")

            RaiseEvent FileImported(projectfile)

        Catch ex As Exception
            MsgBox("Error while loading project file:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry(Log.levels.error, "Error while loading project file:" & eol & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Attempts import of clipboard content
    ''' Detects TALSIM clipboard content or plain text
    ''' </summary>
    Friend Sub Import_Clipboard()

        Dim dlgres As DialogResult

        RaiseEvent IsBusyChanged(True)

        Try
            'Check data format
            If Clipboard.ContainsText(TextDataFormat.Text) Then

                Dim clipboardtext As String
                clipboardtext = Clipboard.GetText(TextDataFormat.Text)

                If Parsers.TalsimClipboard.verifyFormat(clipboardtext) Then
                    'ask the user for confirmation
                    dlgres = MessageBox.Show($"TALSIM clipboard content detected!{eol}Load series in Wave?", "Load from clipboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dlgres = Windows.Forms.DialogResult.Yes Then
                        Call Me.LoadFromClipboard_TALSIM(clipboardtext)
                    End If
                Else
                    'ask the user whether to attempt CSV import
                    dlgres = MessageBox.Show("Attempt to load clipboard text content in Wave as CSV data?", "Load from clipboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dlgres = Windows.Forms.DialogResult.Yes Then
                        'save as temp text file and then load file
                        Dim tmpfile As String = IO.Path.GetTempFileName()
                        Using writer As New IO.StreamWriter(tmpfile, False, Helpers.DefaultEncoding)
                            writer.Write(clipboardtext)
                        End Using
                        Call Me.Import_File(tmpfile)
                        'delete temp file after import
                        IO.File.Delete(tmpfile)
                    End If
                End If
            Else
                MessageBox.Show("No usable clipboard content detected!", "Load from clipboard", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Log.AddLogEntry(Log.levels.error, ex.Message)
            MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        RaiseEvent IsBusyChanged(False)

    End Sub

    ''' <summary>
    ''' Load a timeseries from a file using information from a TALSIM clipboard entry
    ''' </summary>
    ''' <param name="clipboardtext">text content of the clipboard</param>
    ''' <remarks></remarks>
    Public Sub LoadFromClipboard_TALSIM(clipboardtext As String)

        Try
            Call Log.AddLogEntry(Log.levels.info, $"Parsing Talsim clipboard content...")

            Dim wvp As New Parsers.TalsimClipboard(clipboardtext)
            Dim tsList As List(Of TimeSeries) = wvp.Process()

            Call Log.AddLogEntry(Log.levels.info, $"Imported {tsList.Count} timeseries")

            'import the series
            Call Log.AddLogEntry(Log.levels.info, "Loading series in chart...")
            For Each ts As TimeSeries In tsList
                Call Me.Import_Series(ts)
            Next

            'Log
            Call Log.AddLogEntry(Log.levels.info, $"Talsim clipboard content parsed successfully!")

        Catch ex As Exception
            MsgBox("Error while processing Talsim clipboard content:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry(Log.levels.error, "Error while processing Talsim clipboard content:" & eol & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Import a time series
    ''' </summary>
    ''' <param name="ts">the time series</param>
    Public Sub Import_Series(ByRef ts As TimeSeries)

        If ts.Length = 0 Then
            Throw New Exception($"Time series '{ts.Title}' has length 0 and cannot be loaded in Wave!")
        End If

        'Store the time series
        Me.AddTimeSeries(ts)

    End Sub

    ''' <summary>
    ''' Reorders the internally stored time series according to the specified list of Ids
    ''' </summary>
    ''' <param name="ids">List of Ids in the new order</param>
    Friend Sub Reorder_Series(ids As List(Of Integer))
        Me.TimeSeries.Reorder(ids)
        RaiseEvent SeriesAllReordered()
    End Sub

    ''' <summary>
    ''' Initiates timeseries export
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ExportTimeseries()

        Dim exportDlg As ExportDiag
        Dim dlgResult As DialogResult
        Dim folder As String = ""
        Dim filename As String = ""
        Dim tsList As List(Of TimeSeries)

        'Abort if no time series loaded
        If (Me.TimeSeries.Count < 1) Then
            MsgBox("No time series available for export!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Show Export dialog
        exportDlg = New ExportDiag(Me.TimeSeries)
        dlgResult = exportDlg.ShowDialog()

        If dlgResult <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Dim fileType As TimeSeriesFile.FileTypes = exportDlg.ComboBox_Format.SelectedItem

        'get selected series
        tsList = New List(Of TimeSeries)
        For Each ts As TimeSeries In exportDlg.ListBox_Series.SelectedItems
            tsList.Add(ts.Clone()) 'clone them because we may alter their metadata before saving
        Next

        'export to one or multiple files?
        Dim multifileExport As Boolean
        If tsList.Count = 1 Then
            'a single series will always be exported to a single file
            multifileExport = False
        Else
            'if multiple series were selected, it depends on the file type
            If TimeSeriesFile.SupportsMultipleSeries(fileType) Then
                'export all series to a single file
                multifileExport = False
            Else
                'export each series to an individual file
                multifileExport = True
            End If
        End If

        'prepare metadata according to file format
        Dim keys As List(Of String)
        'get a list of required metadata keys depending on file type being exported
        Select Case fileType
            Case TimeSeriesFile.FileTypes.FEWS_PI
                keys = Fileformats.FEWS_PI.MetadataKeys
            Case TimeSeriesFile.FileTypes.SWMM_INTERFACE
                keys = Fileformats.SWMM_INTERFACE.MetadataKeys
            Case TimeSeriesFile.FileTypes.UVF
                keys = Fileformats.UVF.MetadataKeys
            Case TimeSeriesFile.FileTypes.ZRXP
                keys = Fileformats.ZRXP.MetadataKeys
            Case Else
                keys = TimeSeriesFile.MetadataKeys 'empty list
        End Select
        'add missing keys to each series
        For Each ts As TimeSeries In tsList
            If keys.Count > 0 Then
                'add additional keys as necessary
                For Each key As String In keys
                    If Not ts.Metadata.ContainsKey(key) Then
                        'add a new key with an empty value
                        ts.Metadata.Add(key, "")
                    End If
                Next
                'set default metadata values
                Select Case fileType
                    Case TimeSeriesFile.FileTypes.FEWS_PI
                        Fileformats.FEWS_PI.setDefaultMetadata(ts)
                    Case TimeSeriesFile.FileTypes.SWMM_INTERFACE
                        Fileformats.SWMM_INTERFACE.setDefaultMetadata(ts)
                    Case TimeSeriesFile.FileTypes.UVF
                        Fileformats.UVF.setDefaultMetadata(ts)
                    Case TimeSeriesFile.FileTypes.ZRXP
                        Fileformats.ZRXP.setDefaultMetadata(ts)
                    Case Else
                        TimeSeriesFile.setDefaultMetadata(ts)
                End Select
            End If
        Next

        'let user edit metadata
        If keys.Count > 0 Then
            If tsList.Count = 1 Then
                'show dialog for editing metadata of a single series
                Dim dlg As New MetadataDialog(tsList.First, fileType, keys)
                dlgResult = dlg.ShowDialog()
                If Not dlgResult = Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
                'update metadata of series
                tsList.First.Metadata = dlg.getMetadata()
            Else
                'show dialog for editing metadata of multiple series
                Dim dlg As New MultiMetadataDialog(tsList, fileType, keys)
                dlgResult = dlg.ShowDialog()
                If Not dlgResult = Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
                'update metadata of series
                For Each ts As TimeSeries In tsList
                    ts.Metadata = dlg.getMetadata(ts.Title)
                Next
            End If
        End If

        'determine default file name for each series
        Dim fileExt As String = TimeSeriesFile.getFileExtension(fileType).ToLower()
        Dim defaultFileNames As New List(Of String)
        For Each ts As TimeSeries In tsList
            Dim name As String = ts.Title
            'replace invalid chars in title
            For Each c As Char In IO.Path.GetInvalidFileNameChars()
                name = name.Replace(c, "_")
            Next
            defaultFileNames.Add(name & fileExt)
        Next

        'prepare and show save dialog
        If multifileExport Then
            'let the user select a folder
            'use the OpenFileDialog instead of the FolderBrowserDialog for better usability
            Dim dlg As New OpenFileDialog()
            dlg.Title = "Export to folder..."
            dlg.ValidateNames = False
            dlg.CheckFileExists = False
            dlg.CheckPathExists = True
            dlg.FileName = "Select folder"

            'Show dialog
            dlgResult = dlg.ShowDialog()
            If dlgResult <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            folder = IO.Path.GetDirectoryName(dlg.FileName)

            Log.AddLogEntry(Log.levels.info, $"Exporting time series to folder {folder}...")

        Else
            'let the user select a filename
            Dim SaveFileDialog1 As New SaveFileDialog()
            SaveFileDialog1.Title = "Export to file..."
            SaveFileDialog1.AddExtension = True
            SaveFileDialog1.OverwritePrompt = False
            SaveFileDialog1.SupportMultiDottedExtensions = True
            'suggest the default filename of the first series
            SaveFileDialog1.FileName = defaultFileNames(0)

            Select Case fileType
                Case TimeSeriesFile.FileTypes.ASC
                    SaveFileDialog1.DefaultExt = "asc"
                    SaveFileDialog1.Filter = "ASC files (*.asc)|*.asc"
                Case TimeSeriesFile.FileTypes.BIN
                    SaveFileDialog1.DefaultExt = "bin"
                    SaveFileDialog1.Filter = "SYDRO binary files (*.bin)|*.bin"
                Case TimeSeriesFile.FileTypes.CSV
                    SaveFileDialog1.DefaultExt = "csv"
                    SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv"
                Case TimeSeriesFile.FileTypes.DFS0
                    SaveFileDialog1.DefaultExt = "dfs0"
                    SaveFileDialog1.Filter = "DFS0 files (*.dfs0)|*.dfs0"
                Case TimeSeriesFile.FileTypes.FEWS_PI
                    SaveFileDialog1.DefaultExt = "xml"
                    SaveFileDialog1.Filter = "Delf-FEWS PI timeseries files (*.xml)|*.xml"
                Case TimeSeriesFile.FileTypes.WEL
                    SaveFileDialog1.DefaultExt = "wel"
                    SaveFileDialog1.Filter = "WEL files (*.wel)|*.wel"
                Case TimeSeriesFile.FileTypes.ZRE
                    SaveFileDialog1.DefaultExt = "zre"
                    SaveFileDialog1.Filter = "ZRE files (*.zre)|*.zre"
                Case TimeSeriesFile.FileTypes.HYBNAT_BCS
                    SaveFileDialog1.DefaultExt = "bcs"
                    SaveFileDialog1.Filter = "HYBNAT BCS files (*.bcs)|*.bcs"
                Case TimeSeriesFile.FileTypes.HYSTEM_REG
                    SaveFileDialog1.DefaultExt = "reg"
                    SaveFileDialog1.Filter = "HYSTEM REG files (*.reg)|*.reg"
                Case TimeSeriesFile.FileTypes.SMUSI_REG
                    SaveFileDialog1.DefaultExt = "reg"
                    SaveFileDialog1.Filter = "SMUSI REG files (*.reg)|*.reg"
                Case TimeSeriesFile.FileTypes.SWMM_INTERFACE
                    SaveFileDialog1.DefaultExt = "txt"
                    SaveFileDialog1.Filter = "SWMM routing interface files (*.txt)|*.txt"
                Case TimeSeriesFile.FileTypes.SWMM_TIMESERIES
                    SaveFileDialog1.DefaultExt = "dat"
                    SaveFileDialog1.Filter = "SWMM time series files (*.dat)|*.dat"
                Case TimeSeriesFile.FileTypes.UVF
                    SaveFileDialog1.DefaultExt = "uvf"
                    SaveFileDialog1.Filter = "UVF files (*.uvf)|*.uvf"
                Case TimeSeriesFile.FileTypes.ZRXP
                    SaveFileDialog1.DefaultExt = "zrx"
                    SaveFileDialog1.Filter = "ZRXP files (*.zrx)|*.zrx"
            End Select
            SaveFileDialog1.Filter &= "|All files (*.*)|*.*"
            SaveFileDialog1.FilterIndex = 1

            'Show Save dialog
            dlgResult = SaveFileDialog1.ShowDialog()
            If dlgResult <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            filename = SaveFileDialog1.FileName

        End If

        RaiseEvent IsBusyChanged(True)

        Try
            If TimeSeriesFile.SupportsMultipleSeries(fileType) Then
                'export all series to a single file

                'check for existing files and ask to overwrite
                If IO.File.Exists(filename) Then
                    Dim resp As MsgBoxResult = MsgBox($"Overwrite existing file {IO.Path.GetFileName(filename)}?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation)
                    If resp = MsgBoxResult.No Then
                        'abort
                        Exit Sub
                    End If
                End If

                Log.AddLogEntry(Log.levels.info, $"Exporting {tsList.Count} time series to file {filename}...")

                Select Case fileType

                    Case TimeSeriesFile.FileTypes.CSV
                        Call Fileformats.CSV.writeFile(tsList, filename)

                    Case TimeSeriesFile.FileTypes.DFS0
                        Call Fileformats.DFS0.writeFile(tsList, filename)

                    Case TimeSeriesFile.FileTypes.FEWS_PI
                        Call Fileformats.FEWS_PI.writeFile(tsList, filename)

                    Case TimeSeriesFile.FileTypes.SWMM_INTERFACE
                        Call Fileformats.SWMM_INTERFACE.writeFile(tsList, filename)

                    Case TimeSeriesFile.FileTypes.HYBNAT_BCS
                        Call Fileformats.HYBNAT_BCS.writeFile(tsList, filename)

                    Case Else
                        Throw New Exception($"Export to file type {fileType} not yet implemented!")

                End Select

            Else
                'export each series to a separate file
                For i As Integer = 0 To tsList.Count - 1

                    Dim ts As TimeSeries = tsList(i)

                    If multifileExport Then
                        'use default file name
                        filename = IO.Path.Combine(folder, defaultFileNames(i))
                    End If

                    'check for existing files and ask to overwrite
                    If IO.File.Exists(filename) Then
                        Dim resp As MsgBoxResult = MsgBox($"Overwrite existing file {IO.Path.GetFileName(filename)}?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation)
                        If resp = MsgBoxResult.No Then
                            'skip this file
                            Continue For
                        End If
                    End If

                    Log.AddLogEntry(Log.levels.info, $"Exporting time series '{ts.Title}' to file {filename}...")

                    Select Case fileType

                        Case TimeSeriesFile.FileTypes.BIN
                            Call Fileformats.BIN.writeFile(ts, filename)

                        Case TimeSeriesFile.FileTypes.HYSTEM_REG
                            Call Fileformats.HystemExtran_REG.writeFile(ts, filename)

                        Case TimeSeriesFile.FileTypes.SMUSI_REG
                            Call Fileformats.SMUSI_REG.writeFile(ts, filename)

                        Case TimeSeriesFile.FileTypes.SWMM_TIMESERIES
                            Call Fileformats.SWMM_TIMESERIES.writeFile(ts, filename)

                        Case TimeSeriesFile.FileTypes.UVF
                            Call Fileformats.UVF.writeFile(ts, filename)

                        Case TimeSeriesFile.FileTypes.ZRE
                            Call Fileformats.ZRE.writeFile(ts, filename)

                        Case TimeSeriesFile.FileTypes.ZRXP
                            Call Fileformats.ZRXP.writeFile(ts, filename)

                        Case Else
                            Throw New Exception($"Export to file type {fileType} not yet implemented!")
                    End Select

                Next

            End If

            MsgBox("Time series exported successfully!", MsgBoxStyle.Information)
            Log.AddLogEntry(Log.levels.info, "Time series exported successfully!")

        Catch ex As Exception
            Log.AddLogEntry(Log.levels.error, "Error during export: " & ex.Message)
            MsgBox("Error during export: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        RaiseEvent IsBusyChanged(False)

    End Sub

    ''' <summary>
    ''' Delete a TimeSeries
    ''' </summary>
    ''' <param name="id">TimeSeries Id</param>
    Friend Sub RemoveTimeSeries(id As Integer)

        'Delete internally
        Me.TimeSeries.Remove(id)

        RaiseEvent SeriesRemoved(id)

    End Sub

    Friend Sub RemoveAllTimeSeries()
        Me.TimeSeries.Clear()

        RaiseEvent SeriesCleared()
    End Sub

    ''' <summary>
    ''' Shows the import dialog for series selection
    ''' </summary>
    ''' <param name="tsFile">File instance from which to import</param>
    Friend Function ShowImportDialog(ByRef tsFile As TimeSeriesFile) As Boolean

        Dim dialog As Form
        Dim dialogResult As DialogResult

        If tsFile.GetType() Is GetType(Fileformats.CSV) Then
            ' show the import CSV dialog for exact CSV instances (but not for derived classes)
            dialog = New ImportCSVDialog(tsFile)
        ElseIf tsFile.GetType() Is GetType(Fileformats.GINA_HDF5) Then
            ' show the HDF5 import dialog with two-panel selection
            dialog = New ImportHDF5Dialog(CType(tsFile, Fileformats.GINA_HDF5))
        Else
            dialog = New SelectSeriesDialog(tsFile)
        End If

        ' Ensure the dialog is focused and centered
        dialog.StartPosition = FormStartPosition.CenterParent
        dialog.Focus()

        ' show the dialog and wait for user input
        dialogResult = dialog.ShowDialog()

        If dialogResult = Windows.Forms.DialogResult.OK Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Adds a time series to the internal storage
    ''' Renames the title if it is not unique
    ''' Raises the SeriesAdded event
    ''' </summary>
    ''' <param name="timeseries"></param>
    Private Sub AddTimeSeries(ByRef timeseries As TimeSeries)

        Dim duplicateFound As Boolean
        Dim pattern As String = "(?<name>.*)\s\(\d+\)$"
        Dim match As Match
        Dim n As Integer = 1

        'Umbenennen, falls Titel schon vergeben
        'Format: "Titel (n)"
        Do While True
            duplicateFound = False
            For Each ts As TimeSeries In Me.TimeSeries.Values
                If timeseries.Title = ts.Title Then
                    duplicateFound = True
                End If
            Next

            If duplicateFound Then
                match = Regex.Match(timeseries.Title, pattern)
                If (match.Success) Then
                    n += 1
                    timeseries.Title = Regex.Replace(timeseries.Title, pattern, $"${{name}} ({n})")
                Else
                    timeseries.Title &= " (1)"
                End If
            Else
                Exit Do
            End If
        Loop

        Me.TimeSeries.Add(timeseries)

        'Raise event
        RaiseEvent SeriesAdded(timeseries)

    End Sub

    Friend Sub HighlightTimestampsHandler(timestamps As List(Of DateTime))
        RaiseEvent HighlightTimestamps(timestamps)
    End Sub

    Friend Sub SeriesPropertiesChangedHandler(id As Integer)
        RaiseEvent SeriesPropertiesChanged(id)
    End Sub

    ''' <summary>
    ''' Reorders a TimeSeries
    ''' </summary>
    ''' <param name="id">Id of the TimeSeries to reorder</param>
    ''' <param name="direction">Direction</param>
    Friend Sub SeriesReorder(id As Integer, direction As Direction)
        Me.TimeSeries.Reorder(id, direction)
        'raise event for views to handle
        RaiseEvent SeriesReordered(id, direction)
    End Sub

End Class
