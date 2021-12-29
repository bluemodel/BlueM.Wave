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
    ''' FIXME: this should probably be an event of the Timeseries itself?
    ''' </summary>
    ''' <param name="id"></param>
    Friend Event SeriesPropertiesChanged(id As Integer)

    Friend Event SeriesRemoved(id As Integer)

    ''' <summary>
    ''' Internal collection of time series {id: TimeSeries, ...}
    ''' </summary>
    Public TimeSeriesDict As Dictionary(Of Integer, TimeSeries)

    ''' <summary>
    ''' The Log instance shared among all Wave instances
    ''' </summary>
    Friend WithEvents logInstance As Log

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

        'Sonderf�lle abfangen:
        '---------------------
        Select Case IO.Path.GetExtension(file).ToUpper()

            Case FileFactory.FileExtTEN
                '.TEN-Datei
                Call Me.Load_TEN(file)

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
                        'Ansonsten alle Spalten ausw�hlen
                        Call Datei.selectAllSeries()
                        ok = True
                    End If

                    If (ok) Then

                        'FIXME: Me.Cursor = Cursors.WaitCursor

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

                        'Update window title
                        'FIXME: Me.Text = "BlueM.Wave - " & file

                    Else
                        'Import abgebrochen
                        Log.AddLogEntry(Log.levels.error, "Import cancelled!")

                    End If

                Catch ex As Exception
                    MsgBox("Error during import:" & eol & ex.Message, MsgBoxStyle.Critical)
                    Call Log.AddLogEntry(Log.levels.error, "Error during import: " & ex.Message)

                Finally
                    'FIXME: Me.Cursor = Cursors.Default
                End Try

        End Select

    End Sub

    ''' <summary>
    ''' Load a Wave project file
    ''' </summary>
    ''' <param name="projectfile">Path to the Wave project file</param>
    ''' <remarks></remarks>
    Private Sub Load_WVP(projectfile As String)

        Try
            Dim tsList As List(Of TimeSeries)

            'FIXME: Me.Cursor = Cursors.WaitCursor

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

            'Update window title
            'FIXME: Me.Text = "BlueM.Wave - " & projectfile

            'FIXME: Call Me.UpdateChartExtents()

        Catch ex As Exception
            MsgBox("Error while loading project file:" & eol & ex.Message, MsgBoxStyle.Critical)
            Call Log.AddLogEntry(Log.levels.error, "Error while loading project file:" & eol & ex.Message)
        Finally
            'FIXME: Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' L�dt eine native Teechart-Datei (*.TEN)
    ''' </summary>
    ''' <param name="FileName">Pfad zur TEN-Datei</param>
    ''' <remarks></remarks>
    Friend Sub Load_TEN(FileName As String)

        'FIXME: Load_TEN()
        'Dim result As DialogResult
        'Dim i As Integer
        'Dim reihe As TimeSeries
        'Dim XMin, XMax As DateTime

        'Try

        '    'Log
        '    Call Log.AddLogEntry(Log.levels.info, $"Loading file '{FileName}' ...")

        '    'Bereits vorhandene Reihen merken
        '    Dim existingIds = New List(Of Integer)
        '    For Each id As Integer In Me.TimeSeriesDict.Keys
        '        existingIds.Add(id)
        '    Next

        '    'Zoom der X-Achse merken
        '    XMin = Me.ChartMinX
        '    XMax = Me.ChartMaxX
        '    If (XMin <> XMax) Then
        '        Me.selectionMade = True
        '    Else
        '        Me.selectionMade = False
        '    End If

        '    'TEN-Datei importieren
        '    'Diagramme werden hiermit komplett ersetzt!
        '    Call TChart1.Import.Template.Load(FileName)
        '    Call TChart2.Import.Template.Load(FileName)

        '    '�bersichtsdiagramm wieder als solches formatieren
        '    'TODO: als Funktion auslagern und auch bei Init_Charts() verwenden
        '    Call WaveView.FormatChart(Me.TChart2.Chart)
        '    Me.TChart2.Panel.Brush.Color = Color.FromArgb(239, 239, 239)
        '    Me.TChart2.Walls.Back.Color = Color.FromArgb(239, 239, 239)
        '    Me.TChart2.Header.Visible = False
        '    Me.TChart2.Legend.Visible = False
        '    Me.TChart2.Axes.Left.Labels.Font.Color = Color.FromArgb(100, 100, 100)
        '    Me.TChart2.Axes.Left.Labels.Font.Size = 8
        '    Me.TChart2.Axes.Bottom.Labels.Font.Color = Color.FromArgb(100, 100, 100)
        '    Me.TChart2.Axes.Bottom.Labels.Font.Size = 8
        '    Me.TChart2.Axes.Bottom.Automatic = False
        '    Me.TChart2.Axes.Bottom.Labels.DateTimeFormat = "dd.MM.yyyy"
        '    Me.TChart2.Axes.Bottom.Labels.Angle = 0

        '    'Hide axis titles
        '    Me.TChart2.Axes.Left.Title.Visible = False
        '    Me.TChart2.Axes.Right.Title.Visible = False
        '    For i = 0 To Me.TChart2.Axes.Custom.Count - 1
        '        Me.TChart2.Axes.Custom(i).Title.Visible = False
        '    Next
        '    'change type of series to FastLine
        '    For i = 0 To Me.TChart2.Series.Count - 1
        '        Steema.TeeChart.Styles.Series.ChangeType(Me.TChart2.Series(i), GetType(Steema.TeeChart.Styles.FastLine))
        '    Next

        '    'Abfrage f�r Reihenimport
        '    If (Me.TChart1.Series.Count() > 0) Then
        '        result = MsgBox("Also import time series?", MsgBoxStyle.YesNo)

        '        Select Case result

        '            Case Windows.Forms.DialogResult.Yes
        '                'Reihen aus TEN-Datei sollen importiert werden

        '                'Alle Reihen durchlaufen
        '                For Each series As Steema.TeeChart.Styles.Series In TChart1.Series

        '                    'Nur Zeitreihen behandeln
        '                    If (series.GetHorizAxis.IsDateTime) Then

        '                        'Zeitreihe aus dem importierten Diagramm nach intern �bertragen
        '                        Log.AddLogEntry(Log.levels.info, $"Importing series '{series.Title}' from TEN file...")
        '                        reihe = New TimeSeries(series.Title)
        '                        For i = 0 To series.Count - 1
        '                            reihe.AddNode(DateTime.FromOADate(series.XValues(i)), series.YValues(i))
        '                        Next
        '                        'Determine total number of NaN-values and write to log
        '                        If reihe.NaNCount > 0 Then
        '                            Log.AddLogEntry(Log.levels.warning, $"Series '{reihe.Title}' contains {reihe.NaNCount} NaN values!")
        '                        End If
        '                        'Get the series' unit from the axis title
        '                        Dim axistitle As String = ""
        '                        Select Case series.VertAxis
        '                            Case Steema.TeeChart.Styles.VerticalAxis.Left
        '                                axistitle = Me.TChart1.Axes.Left.Title.Text
        '                            Case Steema.TeeChart.Styles.VerticalAxis.Right
        '                                axistitle = Me.TChart1.Axes.Right.Title.Text
        '                            Case Steema.TeeChart.Styles.VerticalAxis.Custom
        '                                axistitle = series.CustomVertAxis.Title.Text
        '                        End Select
        '                        reihe.Unit = AxisWrapper.parseUnit(axistitle)

        '                        'Save datasource
        '                        reihe.DataSource = New TimeSeriesDataSource(FileName, series.Title)

        '                        'Store the series internally
        '                        Call Me.StoreTimeseries(reihe)

        '                        'update the title in case it was changed during storage
        '                        series.Title = reihe.Title

        '                        'Store the time series id in the Tag property
        '                        series.Tag = reihe.Id
        '                        'Store id as Tag in Chart2 as well
        '                        For Each series2 As Steema.TeeChart.Styles.Series In TChart2.Series
        '                            If series2.Title = series.Title Then
        '                                series2.Tag = reihe.Id
        '                                Exit For
        '                            End If
        '                        Next

        '                    End If
        '                Next

        '                'Update window title
        '                Me.Text = "BlueM.Wave - " & FileName

        '            Case Windows.Forms.DialogResult.No
        '                'Reihen aus TEN-Datei sollen nicht importiert werden

        '                'Alle Reihen aus den Diagrammen l�schen (wurden bei TEN-Import automatisch mit eingeladen)
        '                Me.TChart1.Series.RemoveAllSeries()
        '                Me.TChart2.Series.RemoveAllSeries()

        '        End Select

        '    End If

        '    'extract units from axis titles and store as tags
        '    Me.TChart1.Axes.Left.Tag = AxisWrapper.parseUnit(Me.TChart1.Axes.Left.TitleOrName)
        '    Me.TChart1.Axes.Right.Tag = AxisWrapper.parseUnit(Me.TChart1.Axes.Right.TitleOrName)
        '    For Each axis As Steema.TeeChart.Axis In Me.TChart1.Axes.Custom
        '        axis.Tag = AxisWrapper.parseUnit(axis.TitleOrName)
        '    Next

        '    'Die vor dem Laden bereits vorhandenen Zeitreihen wieder zu den Diagrammen hinzuf�gen (durch TEN-Import verloren)
        '    For Each id As Integer In existingIds
        '        Call Me.Display_Series(Me.TimeSeriesDict(id))
        '    Next

        '    'Vorherigen Zoom wiederherstellen
        '    If (Me.selectionMade) Then
        '        Me.ChartMinX = XMin
        '        Me.ChartMaxX = XMax
        '    End If

        '    'ColorBands neu einrichten (durch TEN-Import verloren)
        '    Call Me.Init_ColorBands()

        '    'Reset zoom and pan settings
        '    Me.TChart1.Zoom.Direction = Steema.TeeChart.ZoomDirections.None
        '    Me.TChart1.Panning.Allow = Steema.TeeChart.ScrollModes.None
        '    Me.TChart1.Panning.MouseButton = MouseButtons.Right
        '    Me.TChart2.Zoom.Direction = Steema.TeeChart.ZoomDirections.None
        '    Me.TChart2.Panning.Allow = Steema.TeeChart.ScrollModes.None

        '    'Charts aktualisieren
        '    Call Me.UpdateChartExtents()

        '    Call Me.ViewportChanged()

        '    'Re-assign the chart to the chartlistbox (#41)
        '    Me.ChartListBox1.Chart = Me.TChart1

        '    'Log
        '    Call Log.AddLogEntry(Log.levels.info, $"TEN file '{FileName}' loaded successfully!")

        'Catch ex As Exception
        '    MsgBox("Error while loading:" & eol & ex.Message, MsgBoxStyle.Critical)
        '    Call Log.AddLogEntry(Log.levels.error, "Error while loading:" & eol & ex.Message)
        'End Try

        ''Update AxisDialog
        'Call Me.UpdateAxisDialog()
    End Sub

    ''' <summary>
    ''' Attempts import of clipboard content
    ''' Detects TALSIM clipboard content or plain text
    ''' </summary>
    Friend Sub Import_Clipboard()

        Dim dlgres As DialogResult

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
                    'FIXME: Me.Cursor = Cursors.WaitCursor
                    Call Me.LoadFromClipboard_TALSIM(clipboardtext)
                    'FIXME: Me.Cursor = Cursors.Default
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
            'FIXME: Me.Cursor = Cursors.Default
            Log.AddLogEntry(Log.levels.error, ex.Message)
            MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
        End Try

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
    ''' Saves and then displays the time series
    ''' </summary>
    ''' <param name="ts">the time series</param>
    Public Sub Import_Series(ts As TimeSeries)

        'Store the time series
        Me.StoreTimeseries(ts)

        'Raise event
        RaiseEvent SeriesAdded(ts)

        'Serie in Diagrammen anzeigen
        'FIXME: Call Me.Display_Series(zre)
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
    ''' Zeitreihe(n) exportieren
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ExportZeitreihe()

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

        'FIXME: Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try

            Select Case exportDlg.ComboBox_Format.SelectedItem

                Case FileFormatBase.FileFormats.ZRE
                    Call ZRE.Write_File(zres(0), filename)

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

                Case FileFormatBase.FileFormats.CSV
                    Call CSV.Write_File(zres, filename)

                Case FileFormatBase.FileFormats.BIN
                    Call BIN.Write_File(zres(0), filename)

                Case FileFormatBase.FileFormats.UVF
                    Call UVF.Write_File(zres(0), filename)

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
        Finally
            'FIXME: Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Delete a TimeSeries
    ''' </summary>
    ''' <param name="id">TimeSeries Id</param>
    Friend Sub DeleteTimeSeries(id As Integer)

        'Delete internally
        Me.TimeSeriesDict.Remove(id)

        RaiseEvent SeriesRemoved(id)

        'FIXME: 'Update dialogs
        'Me.valuesDialog.Update(Me.TimeSeriesDict.Values.ToList)

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
    ''' Also adds the datasource to the MRU file list if the time series has a file datasource
    ''' </summary>
    ''' <param name="timeseries"></param>
    Private Sub StoreTimeseries(ByRef timeseries As TimeSeries)

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

        'FIXME: 'Update dialogs
        'Me.propDialog.Update(Me.TimeSeriesDict.Values.ToList)
        'Me.valuesDialog.Update(Me.TimeSeriesDict.Values.ToList)
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

    Friend Sub SeriesPropertiesChangedHandler(id As Integer)
        RaiseEvent SeriesPropertiesChanged(id)
    End Sub

End Class
