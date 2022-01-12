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
    ''' Internal collection of time series {id: TimeSeries, ...}
    ''' </summary>
    Public TimeSeriesDict As Dictionary(Of Integer, TimeSeries)

    Public Sub New()
        'Kollektionen einrichten
        '-----------------------
        Me.TimeSeriesDict = New Dictionary(Of Integer, TimeSeries)()
    End Sub

    ''' <summary>
    ''' Import series from multiple files
    ''' </summary>
    ''' <param name="files">array of file paths</param>
    ''' <remarks></remarks>
    Public Sub Import_Files(files() As String)
        For Each file As String In files
            Call Me.Import_File(file)
        Next
    End Sub

    ''' <summary>
    ''' Import a file
    ''' </summary>
    ''' <param name="file">file path</param>
    Public Sub Import_File(file As String)

        Dim Datei As FileFormatBase
        Dim ok As Boolean

        RaiseEvent IsBusyChanged(True)

        'Sonderfälle abfangen:
        '---------------------
        Select Case IO.Path.GetExtension(file).ToUpper()

            Case FileFactory.FileExtTEN
                '.TEN-Datei
                'has to be loaded by the controller/view
                RaiseEvent TENFileLoading(file)

            Case FileFactory.FileExtWVP
                'Wave project file
                Call Me.Load_WVP(file)

            Case Else

                'Normalfall:
                '-----------

                Try
                    'Log
                    Call Log.AddLogEntry(Log.levels.info, $"Importing file '{file}' ...")

                    'Datei-Instanz erzeugen
                    Datei = FileFactory.getFileInstance(file)

                    If (Datei.UseImportDialog) Then
                        'Falls Importdialog erforderlich, diesen anzeigen
                        ok = Me.ShowImportDialog(Datei)
                        Call Application.DoEvents()
                    Else
                        'Ansonsten alle Spalten auswählen
                        Call Datei.selectAllSeries()
                        ok = True
                    End If

                    If (ok) Then

                        'Datei einlesen
                        Call Datei.readFile()

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, $"File '{file}' imported successfully!")

                        'Log
                        Call Log.AddLogEntry(Log.levels.info, "Loading series in chart...")

                        'Import all time series into the chart
                        For Each ts As TimeSeries In Datei.FileTimeSeries.Values
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
            Dim tsList As List(Of TimeSeries)

            Call Log.AddLogEntry(Log.levels.info, $"Loading Wave project file '{projectfile}'...")

            Dim wvp As New WVP(projectfile)
            tsList = wvp.Process()

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

                If clipboardtext.Contains("SydroTyp=SydroErgZre") Or
                   clipboardtext.Contains("SydroTyp=SydroBinZre") Then
                    'it's a clipboard entry from TALSIM!

                    'ask the user for confirmation
                    dlgres = MessageBox.Show($"TALSIM clipboard content detected!{eol}Load series in Wave?", "Load from clipboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If Not dlgres = Windows.Forms.DialogResult.Yes Then
                        Exit Sub
                    End If
                    Call Me.LoadFromClipboard_TALSIM(clipboardtext)
                Else
                    'ask the user whether to attempt plain text import
                    dlgres = MessageBox.Show("Attempt to load clipboard text content in Wave as CSV data?", "Load from clipboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If Not dlgres = Windows.Forms.DialogResult.Yes Then
                        Exit Sub
                    End If
                    'save as temp text file and then load file
                    Dim tmpfile As String = IO.Path.GetTempFileName()
                    Using writer As New IO.StreamWriter(tmpfile, False, Helpers.DefaultEncoding)
                        writer.Write(clipboardtext)
                    End Using
                    Call Me.Import_File(tmpfile)
                    'delete temp file after import
                    IO.File.Delete(tmpfile)
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
    Private Sub LoadFromClipboard_TALSIM(clipboardtext As String)

        'Examples:

        '[SETTINGS]
        'Count = 1
        '[Zeitreihe1]
        'SydroTyp=SydroErgZre
        'ZRFormat=4
        'ID=362
        'Extension=.WEL
        'Kennung=S000
        'KennungLang={S000} {1AB, HYO} Ablauf_1
        'Zustand=1AB
        'Datei=D:\Talsim-NG\customers\WVER\projectData\felix\dataBase\Felix_data\00000362.WEL
        'GeaendertAm=
        'Modell=TALSIM
        'Herkunft=simuliert
        'Interpretation=2
        'SimVariante=Test Langzeit/HWMerkmal_HWGK_v02
        'Simulation=Test Langzeit
        'Einheit=m3/s
        'EndZeitreihe

        '[SETTINGS]
        'Count=2
        '[Zeitreihe1]
        'SydroTyp=SydroBinZre
        'ZRFormat=99
        'ID=1041
        'Extension=.BIN
        'Kennung=Sce10, E038, C38 (TA_Tekeze TK 04B)
        'KennungLang=Sce10, E038, C38 (TA_Tekeze TK 04B), m3/s
        'Datei=C:\Talsim-NG\customers\Nile\projectData\hubert\dataBase\hubert_zre\00001041.BIN
        'Einheit=m3/s
        'Modell=TALSIM
        'Interpretation=1
        'EndZeitreihe
        '[Zeitreihe2]
        'SydroTyp=SydroBinZre
        'ZRFormat=99
        'ID=1042
        'Extension=.BIN
        'Kennung=Sce10, E039, C39 (TA_TK5)
        'KennungLang=Sce10, E039, C39 (TA_TK5), m3/s
        'Datei=C:\Talsim-NG\customers\Nile\projectData\hubert\dataBase\hubert_zre\00001042.BIN
        'Einheit=m3/s
        'Modell=TALSIM
        'Interpretation=1
        'EndZeitreihe

        'parse clipboard contents
        Dim m As Match
        Dim i_series As Integer
        Dim parts() As String
        Dim zreblock As Boolean
        Dim data As New List(Of Dictionary(Of String, String)) '[{zreparams1},{zreparams2},...]
        Dim file, name As String
        Dim fileobj As FileFormatBase
        Dim ts As TimeSeries

        zreblock = False
        For Each line As String In clipboardtext.Split(eol)
            line = line.Trim()

            m = Regex.Match(line, "\[Zeitreihe(\d+)\]")
            If m.Success Then
                i_series = m.Groups(1).Value
                data.Add(New Dictionary(Of String, String))
                zreblock = True
            End If

            If zreblock Then
                If line.Contains("=") Then
                    parts = line.Split("=")
                    data(i_series - 1).Add(parts(0), parts(1))
                ElseIf line = "EndZeitreihe" Then
                    zreblock = False
                    Continue For
                End If
            End If

        Next

        'initiate loading of series
        For Each params As Dictionary(Of String, String) In data

            file = params("Datei")

            Select Case params("ZRFormat")
                Case "4" 'WEL file

                    'build series name
                    If params("Kennung") = "ZPG" Then
                        'handle control groups
                        name = "KGRP_" & params("Zustand")
                    Else
                        name = params("Kennung").PadRight(4, " ") & "_" & params("Zustand")
                    End If

                    'read file
                    Log.AddLogEntry(Log.levels.info, $"Loading file {file} ...")
                    fileobj = FileFactory.getFileInstance(file)

                    'read series from file
                    ts = fileobj.getTimeSeries(name)

                    'set interpretation
                    ts.Interpretation = params("Interpretation")

                    'import series
                    Call Me.Import_Series(ts)

                Case "99" 'BIN file

                    name = params("Kennung")

                    'read file
                    Log.AddLogEntry(Log.levels.info, $"Loading file {file} ...")
                    fileobj = FileFactory.getFileInstance(file)

                    'read series from file
                    fileobj.readFile()
                    ts = fileobj.FileTimeSeries.First.Value

                    'add metadata
                    ts.Title = name
                    ts.Unit = params("Einheit")

                    'set interpretation
                    ts.Interpretation = params("Interpretation")

                    'import series
                    Call Me.Import_Series(ts)

            End Select

        Next

    End Sub

    ''' <summary>
    ''' Import a time series
    ''' </summary>
    ''' <param name="ts">the time series</param>
    Public Sub Import_Series(ts As TimeSeries)

        'Store the time series
        Me.AddTimeSeries(ts)

    End Sub

    Friend Sub SaveProjectFile(projectfile)

        'collect datasources
        Dim datasources As New Dictionary(Of String, List(Of String)) '{file: [title, ...], ...}
        Dim file, title As String
        For Each ts As TimeSeries In Me.TimeSeriesDict.Values
            If ts.DataSource.Origin = TimeSeriesDataSource.OriginEnum.FileImport Then
                file = ts.DataSource.FilePath
                title = ts.DataSource.Title
                If Not datasources.ContainsKey(file) Then
                    datasources.Add(file, New List(Of String))
                End If
                datasources(file).Add(title)
            Else
                Log.AddLogEntry(Log.levels.warning, $"Series '{ts.Title}' does not originate from a file import and could not be saved to the project file!")
            End If
        Next

        'write the project file
        Dim fs As New IO.FileStream(projectfile, IO.FileMode.Create, IO.FileAccess.Write)
        Dim strwrite As New IO.StreamWriter(fs, Helpers.DefaultEncoding)

        strwrite.WriteLine("# Wave project file")

        For Each file In datasources.Keys
            'TODO: write relative paths to the project file?
            strwrite.WriteLine("file=" & file)
            For Each title In datasources(file)
                'TODO: if a series was renamed, write the new title to the project file
                If title.Contains(":") Then
                    'enclose titles containing ":" in quotes
                    title = $"""{title}"""
                End If
                strwrite.WriteLine("    series=" & title)
            Next
        Next

        strwrite.Close()
        fs.Close()

        Log.AddLogEntry(Log.levels.info, $"Wave project file {projectfile} saved.")

    End Sub

    ''' <summary>
    ''' Initiates timeseries export
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ExportTimeseries()

        Dim exportDlg As ExportDiag
        Dim dlgResult As DialogResult
        Dim filename As String
        Dim zres As List(Of TimeSeries)

        'Abort if no time series loaded
        If (Me.TimeSeriesDict.Count < 1) Then
            MsgBox("No time series available for export!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Show Export dialog
        exportDlg = New ExportDiag(Me.TimeSeriesDict)
        dlgResult = exportDlg.ShowDialog()

        If dlgResult <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        'get copies of the selected series
        zres = New List(Of TimeSeries)
        For Each ts As TimeSeries In exportDlg.ListBox_Series.SelectedItems
            zres.Add(ts.Clone())
        Next

        'prepare metadata according to file format
        Dim keys As List(Of String)
        Dim metadata_old As Metadata
        For Each ts As TimeSeries In zres
            'get a list of metadata keys
            Select Case exportDlg.ComboBox_Format.SelectedItem
                Case FileFormatBase.FileFormats.UVF
                    keys = UVF.MetadataKeys
                Case FileFormatBase.FileFormats.ZRXP
                    keys = ZRXP.MetadataKeys
                Case Else
                    keys = FileFormatBase.MetadataKeys 'empty list
            End Select
            If keys.Count > 0 Then
                'create a copy of the existing metadata
                metadata_old = ts.Metadata
                'create new metadata keys
                ts.Metadata = New Metadata()
                For Each key As String In keys
                    If metadata_old.Keys.Contains(key) Then
                        'copy old metadata value with the same key
                        ts.Metadata.Add(key, metadata_old(key))
                    Else
                        'add a new key with an empty value
                        ts.Metadata.Add(key, "")
                    End If
                Next
                'set default metadata values
                Select Case exportDlg.ComboBox_Format.SelectedItem
                    Case FileFormatBase.FileFormats.UVF
                        UVF.setDefaultMetadata(ts)
                    Case FileFormatBase.FileFormats.ZRXP
                        ZRXP.setDefaultMetadata(ts)
                    Case Else
                        FileFormatBase.setDefaultMetadata(ts)
                End Select
                'show dialog for editing metadata
                Dim dlg As New MetadataDialog(ts.Metadata)
                dlgResult = dlg.ShowDialog()
                If Not dlgResult = Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
                'update metadata of series
                ts.Metadata = dlg.Metadata
            End If
        Next

        'Prepare Save dialog
        Dim SaveFileDialog1 As New SaveFileDialog()
        SaveFileDialog1.Title = "Save as..."
        SaveFileDialog1.AddExtension = True
        SaveFileDialog1.OverwritePrompt = True
        Select Case exportDlg.ComboBox_Format.SelectedItem
            Case FileFormatBase.FileFormats.ASC
                SaveFileDialog1.DefaultExt = "asc"
                SaveFileDialog1.Filter = "ASC files (*.asc)|*.asc"
            Case FileFormatBase.FileFormats.BIN
                SaveFileDialog1.DefaultExt = "bin"
                SaveFileDialog1.Filter = "SYDRO binary files (*.bin)|*.bin"
            Case FileFormatBase.FileFormats.CSV
                SaveFileDialog1.DefaultExt = "csv"
                SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv"
            Case FileFormatBase.FileFormats.DFS0
                SaveFileDialog1.DefaultExt = "dfs0"
                SaveFileDialog1.Filter = "DFS0 files (*.dfs0)|*.dfs0"
            Case FileFormatBase.FileFormats.WEL
                SaveFileDialog1.DefaultExt = "wel"
                SaveFileDialog1.Filter = "WEL files (*.wel)|*.wel"
            Case FileFormatBase.FileFormats.ZRE
                SaveFileDialog1.DefaultExt = "zre"
                SaveFileDialog1.Filter = "ZRE files (*.zre)|*.zre"
            Case FileFormatBase.FileFormats.REG_HYSTEM
                SaveFileDialog1.DefaultExt = "reg"
                SaveFileDialog1.Filter = "HYSTEM REG files (*.reg)|*.reg"
            Case FileFormatBase.FileFormats.REG_SMUSI
                SaveFileDialog1.DefaultExt = "reg"
                SaveFileDialog1.Filter = "SMUSI REG files (*.reg)|*.reg"
            Case FileFormatBase.FileFormats.DAT_SWMM_MASS, FileFormatBase.FileFormats.DAT_SWMM_TIME
                SaveFileDialog1.DefaultExt = "dat"
                SaveFileDialog1.Filter = "SWMM DAT files (*.dat)|*.dat"
            Case FileFormatBase.FileFormats.SWMM_INTERFACE
                SaveFileDialog1.DefaultExt = "txt"
                SaveFileDialog1.Filter = "SWMM Interface files (*.txt)|*.txt"
            Case FileFormatBase.FileFormats.UVF
                SaveFileDialog1.DefaultExt = "uvf"
                SaveFileDialog1.Filter = "UVF files (*.uvf)|*.uvf"
            Case FileFormatBase.FileFormats.ZRXP
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

        'Export series
        Log.AddLogEntry(Log.levels.info, $"Exporting time series to file {SaveFileDialog1.FileName}...")

        RaiseEvent IsBusyChanged(True)

        Try

            Select Case exportDlg.ComboBox_Format.SelectedItem

                Case FileFormatBase.FileFormats.BIN
                    Call BIN.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.CSV
                    Call CSV.Write_File(zres, filename)

                Case FileFormatBase.FileFormats.DFS0
                    Call DFS0.Write_File(zres, filename)

                Case FileFormatBase.FileFormats.REG_HYSTEM
                    Call HystemExtran_REG.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.REG_SMUSI
                    Call REG_SMUSI.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.DAT_SWMM_MASS
                    Call SWMM_DAT_MASS.Write_File(zres(0), filename, 5) 'TODO: Zeitschritt ist noch nicht dynamisch definiert

                Case FileFormatBase.FileFormats.DAT_SWMM_TIME
                    Call SWMM_DAT_TIME.Write_File(zres(0), filename, 5) 'TODO: Zeitschritt ist noch nicht dynamisch definiert

                Case FileFormatBase.FileFormats.SWMM_INTERFACE
                    Call SWMM_INTERFACE.Write_File(zres, filename)

                Case FileFormatBase.FileFormats.UVF
                    Call UVF.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.ZRE
                    Call ZRE.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.ZRXP
                    Call ZRXP.Write_File(zres(0), filename)

                Case Else
                    MsgBox("Not yet implemented!", MsgBoxStyle.Exclamation)
            End Select

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
        Me.TimeSeriesDict.Remove(id)

        RaiseEvent SeriesRemoved(id)

    End Sub

    Friend Sub RemoveAllTimeSeries()
        Me.TimeSeriesDict.Clear()

        RaiseEvent SeriesCleared()
    End Sub

    ''' <summary>
    ''' Zeigt den Importdialog an und liest im Anschluss die Datei mit den eingegebenen Einstellungen ein
    ''' </summary>
    ''' <param name="Datei">Instanz der Datei, die importiert werden soll</param>
    Friend Function ShowImportDialog(ByRef Datei As FileFormatBase) As Boolean

        Datei.ImportDiag = New ImportDiag(Datei)

        Dim DiagResult As DialogResult

        'Dialog anzeigen
        DiagResult = Datei.ImportDiag.ShowDialog()

        If (DiagResult = Windows.Forms.DialogResult.OK) Then
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
            For Each ts As TimeSeries In Me.TimeSeriesDict.Values
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

        Me.TimeSeriesDict.Add(timeseries.Id, timeseries)

        'Raise event
        RaiseEvent SeriesAdded(timeseries)

    End Sub

    ''' <summary>
    ''' Checks for a newer version on the server
    ''' </summary>
    ''' <returns>True if a newer version is available</returns>
    Friend Async Function CheckForUpdate() As Threading.Tasks.Task(Of Boolean)


        'get current version (only consider major, minor and build numbers, omitting the auto-generated revision number)
        Dim v As Version = Reflection.Assembly.GetExecutingAssembly.GetName().Version()
        Dim currentVersion As New Version($"{v.Major}.{v.Minor}.{v.Build}")

        'retrieve latest version number from server
        Dim client As New Net.Http.HttpClient()
        Dim s As String = Await client.GetStringAsync(urlUpdateCheck)
        Dim latestVersion As New Version(s)
#If Not DEBUG Then
        'TODO: Logging is not thread-safe and causes an exception in debug mode!
        Log.AddLogEntry(Log.levels.debug, "CheckUpdate: Latest version on server: " & latestVersion.ToString())
#End If

        'compare versions
        If currentVersion < latestVersion Then
            Return True
        Else
            Return False
        End If

    End Function

    Friend Sub HighlightTimestampsHandler(timestamps As List(Of DateTime))
        RaiseEvent HighlightTimestamps(timestamps)
    End Sub


    Friend Sub SeriesPropertiesChangedHandler(id As Integer)
        RaiseEvent SeriesPropertiesChanged(id)
    End Sub

    ''' <summary>
    ''' Displays the main window
    ''' </summary>
    Public Sub Show()
        App.showMainWindow(Me)
    End Sub

End Class
