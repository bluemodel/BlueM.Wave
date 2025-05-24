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
''' Abstract base class for all file formats
''' </summary>
Public MustInherit Class TimeSeriesFile

    ''' <summary>
    ''' FileTypes
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum FileTypes
        UNDETERMINED
        UNKNOWN
        ASC
        BIN
        CSV
        DFS0
        GISMO_WEL
        HYBNAT_BCS
        HYBNAT_WEL
        HYDRO_AS_DAT
        HYSTEM_WEL
        JAMS
        PRMS_OUT
        HYSTEM_REG
        SMUSI_REG
        SMB
        SWMM_TIMESERIES
        SWMM_INTERFACE
        SWMM_LID_REPORT
        SWMM_OUT
        SYDROSQLITE
        TEN
        UVF
        WBL
        WEL
        WVP
        ZRE
        ZRXP
    End Enum

    ''' <summary>
    ''' File extensions
    ''' </summary>
    ''' <remarks>
    ''' This is a replacement for an Enum of type String.
    ''' This should actually be a static class that cannot be instantiated nor inherited from,
    ''' which would be a Module in VB, but that cannot be nested, and would mess up the namespace.
    ''' </remarks>
    Public MustInherit Class FileExtensions
        Public Shared ReadOnly ASC As String = ".ASC"
        Public Shared ReadOnly BCS As String = ".BCS"   'BGS HYBNAT BCS format
        Public Shared ReadOnly BIN As String = ".BIN"   'SYDRO binary format
        Public Shared ReadOnly CSV As String = ".CSV"
        Public Shared ReadOnly DAT As String = ".DAT"
        Public Shared ReadOnly DB As String = ".DB"     'SYDRO SQLite format
        Public Shared ReadOnly DFS0 As String = ".DFS0" 'DHI MIKE Dfs0 file format
        Public Shared ReadOnly KWL As String = ".KWL"
        Public Shared ReadOnly OUT As String = ".OUT"   'SWMM binary result file or PRMS out file
        Public Shared ReadOnly REG As String = ".REG"
        Public Shared ReadOnly SMB As String = ".SMB"
        Public Shared ReadOnly TEN As String = ".TEN"
        Public Shared ReadOnly TXT As String = ".TXT"   'SWMM routing interface file, SWMM LID report file or generic text file
        Public Shared ReadOnly UVF As String = ".UVF"
        Public Shared ReadOnly WBL As String = ".WBL"   'SYDRO binary WEL format
        Public Shared ReadOnly WEL As String = ".WEL"
        Public Shared ReadOnly WVP As String = ".WVP"   'Wave project file
        Public Shared ReadOnly ZRE As String = ".ZRE"
        Public Shared ReadOnly ZRX As String = ".ZRX"   'ZRXP format
        Public Shared ReadOnly ZRXP As String = ".ZRXP" 'ZRXP format
    End Class

    ''' <summary>
    ''' FileFilter for file dialogs
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FileFilter As String =
        "All files (*.*)|*.*|" &
        "Text files (*.txt)|*.txt|" &
        "CSV files (*.csv)|*.csv|" &
        "DHI MIKE DFS0 files (*.dfs0)|*.dfs0|" &
        "HYBNAT files (*.bcs, *.wel)|*.bcs;*.wel|" &
        "HYDRO_AS-2D result files (*.dat)|*.dat|" &
        "Hystem-Extran files (*.dat, *.reg)|*.dat;*.reg|" &
        "JAMS timeseries result files (*.dat)|*.dat|" &
        "PRMS result files (*.dat, *.out)|*.dat;*.out|" &
        "SIMBA files (*.smb)|*.smb|" &
        "SMUSI files (*.asc. *.reg)|*.asc;*.reg|" &
        "SWMM files (*.dat, *.txt, *.out)|*.dat;*.txt;*.out|" &
        "SYDRO binary files (*.bin)|*.bin|" &
        "SYDRO binary wel files (*.wbl)|*.wbl|" &
        "SYDRO SQLite files (*.db)|*.db|" &
        "UVF files (*.uvf)|*.uvf|" &
        "WEL files (*.wel, *.kwl)|*.wel;*.kwl|" &
        "Wave project files (*.wvp)|*.wvp|" &
        "ZRE files (*.zre)|*.zre|" &
        "ZRXP files (*.zrx, *.zrxp)|*.zrx;*.zrxp"

#Region "Eigenschaften"

    Private _file As String
    Private _isColumnSeparated As Boolean = True
    Private _trennzeichen As Character = semicolon
    Private _dateFormat As String = Helpers.CurrentDateFormat
    Private _decimalSeparator As Character = period
    Private _iLineHeadings As Integer = 1
    Private _iLineUnits As Integer = 2
    Private _iLineData As Integer = 3
    Private _useUnits As Boolean = True
    Private _columnWidth As Integer = 16
    Private _columnOffset As Integer = 0
    Private _dateTimeColumnIndex As Integer = 0
    Private _seriesInfos As List(Of TimeSeriesInfo)
    Private _selectedSeries As List(Of TimeSeriesInfo)
    Private _metadata As Metadata
    Private _encoding As Text.Encoding
    Private _titleSuffix As String = ""

    ''' <summary>
    ''' Encoding to use for reading the file
    ''' </summary>
    ''' <remarks>Defaults to the system default (usually ISO-8859-1)</remarks>
    ''' <returns></returns>
    Public Property Encoding As Text.Encoding
        Get
            Return Me._encoding
        End Get
        Set(value As Text.Encoding)
            Me._encoding = value
        End Set
    End Property

    ''' <summary>
    ''' File metadata
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileMetadata() As Metadata
        Get
            Return Me._metadata
        End Get
    End Property

    ''' <summary>
    ''' Stores the TimeSeries read from the file.
    ''' The key corresponds to the column index stored as <seealso cref="TimeSeriesInfo.Index"/> in <seealso cref="TimeSeriesInfos"/>.
    ''' </summary>
    Public TimeSeries As Dictionary(Of Integer, TimeSeries)

#End Region 'Eigenschaften

#Region "Properties"

    ''' <summary>
    ''' Path to file
    ''' </summary>
    Public Property File() As String
        Get
            Return _file
        End Get
        Set(value As String)
            _file = value
        End Set
    End Property

    ''' <summary>
    ''' Indicates whether the file contains columns separated by a character (True, default) or is fixed width (False)
    ''' </summary>
    Public Property IsColumnSeparated() As Boolean
        Get
            Return _isColumnSeparated
        End Get
        Set(value As Boolean)
            _isColumnSeparated = value
        End Set
    End Property

    ''' <summary>
    ''' Separator (default is semicolon)
    ''' </summary>
    Public Property Separator() As Character
        Get
            Return _trennzeichen
        End Get
        Set(value As Character)
            _trennzeichen = value
        End Set
    End Property

    ''' <summary>
    ''' Date format (default is Helpers.CurrentDateFormat)
    ''' </summary>
    Public Property Dateformat() As String
        Get
            Return _dateFormat
        End Get
        Set(value As String)
            Me._dateFormat = value
        End Set
    End Property

    ''' <summary>
    ''' Decimal separator (default is period)
    ''' </summary>
    Public Property DecimalSeparator() As Character
        Get
            Return _decimalSeparator
        End Get
        Set(value As Character)
            _decimalSeparator = value
        End Set
    End Property

    ''' <summary>
    ''' Number of the line containing column headings
    ''' </summary>
    Public Property iLineHeadings() As Integer
        Get
            Return _iLineHeadings
        End Get
        Set(value As Integer)
            _iLineHeadings = value
        End Set
    End Property

    ''' <summary>
    ''' Number of the line containing units
    ''' </summary>
    Public Property iLineUnits() As Integer
        Get
            Return _iLineUnits
        End Get
        Set(value As Integer)
            _iLineUnits = value
        End Set
    End Property

    ''' <summary>
    ''' Number of the first line containing data
    ''' </summary>
    Public Property iLineData() As Integer
        Get
            Return _iLineData
        End Get
        Set(value As Integer)
            _iLineData = value
        End Set
    End Property

    ''' <summary>
    ''' Number of header lines
    ''' </summary>
    ''' <returns>iLineData - 1</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property nLinesHeader() As Integer
        Get
            Return _iLineData - 1
        End Get
    End Property

    ''' <summary>
    ''' Read units from the file? (default is True)
    ''' </summary>
    Public Property UseUnits() As Boolean
        Get
            Return _useUnits
        End Get
        Set(value As Boolean)
            _useUnits = value
        End Set
    End Property

    ''' <summary>
    ''' Column width (default is 16)
    ''' </summary>
    ''' <remarks>only relevant for fixed width files</remarks>
    Public Property ColumnWidth() As Integer
        Get
            Return _columnWidth
        End Get
        Set(value As Integer)
            _columnWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Number of characters before the first column (only relevant for fixed width columns)
    ''' </summary>
    Public Property ColumnOffset As Integer
        Get
            Return _columnOffset
        End Get
        Set(value As Integer)
            _columnOffset = value
        End Set
    End Property

    ''' <summary>
    ''' Index of the column containing the datetime values
    ''' </summary>
    Public Property DateTimeColumnIndex() As Integer
        Get
            Return _dateTimeColumnIndex
        End Get
        Set(value As Integer)
            _dateTimeColumnIndex = value
        End Set
    End Property

    ''' <summary>
    ''' List of TimeSeriesInfo of all series contained in a file
    ''' </summary>
    ''' <remarks></remarks>
    Public Property TimeSeriesInfos() As List(Of TimeSeriesInfo)
        Get
            Return _seriesInfos
        End Get
        Set(value As List(Of TimeSeriesInfo))
            _seriesInfos = value
        End Set
    End Property

    ''' <summary>
    ''' List of TimeSeriesInfo of series currently selected for import
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedSeries() As List(Of TimeSeriesInfo)
        Get
            Return _selectedSeries
        End Get
    End Property

    ''' <summary>
    ''' Suffix to be added to the timeseries titles when imported from this file
    ''' </summary>
    ''' <remarks>
    ''' This is useful for distinguishing between series with the same titles in different files.
    ''' For example, if you import two files with the same series names, you can set a suffix
    ''' for each file to differentiate them in the resulting timeseries.
    ''' </remarks>
    Public Property TitleSuffix As String
        Get
            Return _titleSuffix
        End Get
        Set(value As String)
            _titleSuffix = value
        End Set
    End Property

    ''' <summary>
    ''' Get a timeseries from the file using its index
    ''' </summary>
    ''' <param name="index">Index of the desired timeseries within the file</param>
    ''' <returns>The timeseries</returns>
    ''' <remarks>If the timeseries has not been imported yet, an import is initiated.
    ''' Throws an exception if the timeseries cannot be found in the file.</remarks>
    Public Overloads ReadOnly Property getTimeSeries(Optional index As Integer = 0) As TimeSeries
        Get
            If Me.TimeSeries.ContainsKey(index) Then
                Return Me.TimeSeries(index)
            Else
                Dim found As Boolean = Me.selectSeries(index)
                If Not found Then
                    Throw New Exception($"Series with index {index} not found in file!")
                End If
                'read the file (again)
                Me.TimeSeries.Clear()
                Call Me.readFile()
                Return Me.TimeSeries(index)
            End If
        End Get
    End Property


    ''' <summary>
    ''' Get a timeseries from the file using its title
    ''' </summary>
    ''' <param name="title">Title of the desired timeseries.</param>
    ''' <returns>The timeseries</returns>
    ''' <remarks>If the timeseries has not been imported yet, an import is initiated.
    ''' Throws an exception if the timeseries cannot be found in the file.</remarks>
    Public Overloads ReadOnly Property getTimeSeries(title As String) As TimeSeries
        Get
            Dim found As Boolean = False
            'Find the series using the given title
            For Each ts As TimeSeries In Me.TimeSeries.Values
                If title = ts.Title Then
                    Return ts
                    found = True
                End If
            Next
            If Not found Then
                'Timeseries was not found (perhaps not yet imported?)
                'Check whether a column with the given title exists
                For Each sInfo As TimeSeriesInfo In Me.TimeSeriesInfos
                    If (sInfo.Name = title) Then
                        'get the timeseries by its index
                        Return Me.getTimeSeries(sInfo.Index)
                    End If
                Next
            End If
            'Timeseries not found in file
            Throw New Exception($"The timeseries '{title}' could not be found in the file '{IO.Path.GetFileName(Me.File)}'!")
        End Get
    End Property

    ''' <summary>
    ''' Indicates whether the ImportDialog should be shown when importing this file format
    ''' </summary>
    ''' <returns>True if the ImportDialog should be shown when importing this file format</returns>
    Public MustOverride ReadOnly Property UseImportDialog() As Boolean

    ''' <summary>
    ''' Returns a list of format-specific metadata keys
    ''' </summary>
    ''' <remarks>Is an empty list by default, should be overridden by inheriting classes</remarks>
    Public Shared ReadOnly Property MetadataKeys() As List(Of String)
        Get
            Return New List(Of String)
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="FileName">Path to the file to be imported</param>
    ''' <param name="ReadAllNow">If True, select all series and read the file immediately</param>
    Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

        'Initialize data structures
        Me.TimeSeries = New Dictionary(Of Integer, TimeSeries)
        Me._seriesInfos = New List(Of TimeSeriesInfo)
        Me._selectedSeries = New List(Of TimeSeriesInfo)
        Me._metadata = New Metadata()

        'Store the filepath
        Me.File = FileName

        'set default properties
        Me.Encoding = Helpers.DefaultEncoding

    End Sub

    ''' <summary>
    ''' Reads information about the series contained in the file and stores it in <seealso cref="TimeSeriesInfos"/>
    ''' </summary>
    Public MustOverride Sub readSeriesInfo()

    ''' <summary>
    ''' Select all available series for import
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub selectAllSeries()

        Me.SelectedSeries.Clear()
        For Each sInfo As TimeSeriesInfo In Me.TimeSeriesInfos
            Me.SelectedSeries.Add(sInfo)
        Next

    End Sub

    ''' <summary>
    ''' Select a series for import by index
    ''' </summary>
    ''' <param name="index">index</param>
    ''' <returns>True if successful, False if index was not found</returns>
    ''' <remarks></remarks>
    Public Function selectSeries(index As Integer) As Boolean

        Dim i As Integer = 0
        For Each sInfo As TimeSeriesInfo In Me.TimeSeriesInfos
            If sInfo.Index = index Then
                Me.SelectedSeries.Add(sInfo)
                Return True
            End If
        Next
        'series not found in file
        Log.AddLogEntry(Log.levels.error, $"Series with index {index} not found in file!")
        Return False

    End Function

    ''' <summary>
    ''' Select a series for import by name
    ''' </summary>
    ''' <param name="seriesName">name of the series</param>
    ''' <returns>True if successful, False if series name was not found</returns>
    ''' <remarks></remarks>
    Public Function selectSeries(seriesName As String) As Boolean

        For Each sInfo As TimeSeriesInfo In Me.TimeSeriesInfos
            If sInfo.Name = seriesName Then
                Me.SelectedSeries.Add(sInfo)
                Return True
            End If
        Next
        'series not found in file
        Log.AddLogEntry(Log.levels.error, $"Series {seriesName} not found in file!")
        Return False

    End Function

    ''' <summary>
    ''' Reads the selected series (see <seealso cref="SelectedSeries"/>) from the file and stores them as timeseries in <seealso cref="TimeSeries"/>
    ''' </summary>
    Public MustOverride Sub readFile()

    ''' <summary>
    ''' Sets default metadata keys and values for a time series corresponding to the file format
    ''' </summary>
    ''' <remarks>Should be overloaded by inheriting classes that deal with metadata</remarks>
    Public Shared Sub setDefaultMetadata(ts As TimeSeries)
        'add default keys
        ts.Metadata.AddKeys(TimeSeriesFile.MetadataKeys)
        'no default values to set
    End Sub

#End Region 'Methods

    ''' <summary>
    ''' Returns the default file extension for a file type
    ''' </summary>
    ''' <param name="type">The file type</param>
    ''' <returns>The file extension as a string (including the leading ".")</returns>
    Public Shared Function getFileExtension(type As FileTypes) As String
        Select Case type
            Case FileTypes.ASC
                Return FileExtensions.ASC
            Case FileTypes.BIN
                Return FileExtensions.BIN
            Case FileTypes.CSV
                Return FileExtensions.CSV
            Case FileTypes.DFS0
                Return FileExtensions.DFS0
            Case FileTypes.GISMO_WEL
                Return FileExtensions.ASC
            Case FileTypes.HYBNAT_BCS
                Return FileExtensions.BCS
            Case FileTypes.HYBNAT_WEL
                Return FileExtensions.WEL
            Case FileTypes.HYDRO_AS_DAT
                Return FileExtensions.DAT
            Case FileTypes.HYSTEM_WEL
                Return FileExtensions.WEL
            Case FileTypes.PRMS_OUT
                Return FileExtensions.OUT
            Case FileTypes.HYSTEM_REG
                Return FileExtensions.REG
            Case FileTypes.SMUSI_REG
                Return FileExtensions.REG
            Case FileTypes.SMB
                Return FileExtensions.SMB
            Case FileTypes.SWMM_INTERFACE
                Return FileExtensions.TXT
            Case FileTypes.SWMM_LID_REPORT
                Return FileExtensions.TXT
            Case FileTypes.SWMM_TIMESERIES
                Return FileExtensions.DAT
            Case FileTypes.SWMM_OUT
                Return FileExtensions.OUT
            Case FileTypes.SYDROSQLITE
                Return FileExtensions.DB
            Case FileTypes.TEN
                Return FileExtensions.TEN
            Case FileTypes.UVF
                Return FileExtensions.UVF
            Case FileTypes.WBL
                Return FileExtensions.WBL
            Case FileTypes.WEL
                Return FileExtensions.WEL
            Case FileTypes.WVP
                Return FileExtensions.WVP
            Case FileTypes.ZRE
                Return FileExtensions.ZRE
            Case FileTypes.ZRXP
                Return FileExtensions.ZRX
            Case Else
                Throw New Exception($"File extension of file type {type} is undefined!")
        End Select
    End Function

    ''' <summary>
    ''' Returns whether a file type supports multiple series in one file
    ''' </summary>
    ''' <param name="type">The file type</param>
    ''' <returns>True if the file type supports multiple series</returns>
    Public Shared Function SupportsMultipleSeries(type As FileTypes) As Boolean
        Select Case type
            Case TimeSeriesFile.FileTypes.CSV,
                 TimeSeriesFile.FileTypes.DFS0,
                 TimeSeriesFile.FileTypes.SWMM_INTERFACE,
                 TimeSeriesFile.FileTypes.HYBNAT_BCS
                'TODO: ZRXP does actually also support multiple series, but for simplicity's sake, we assume that it doesn't
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Determines the file type of a file based on the file's extension and contents
    ''' </summary>
    ''' <param name="file">Path to the file</param>
    ''' <returns>the determined file type</returns>
    ''' <remarks>
    ''' Also checks whether the file exists and if the file format is as expected.
    ''' If the file is a WEL file, this function also checks whether the file is contained
    ''' within a WLZIP of the same name and if it is, extracts it.
    ''' </remarks>
    Public Shared Function getFileType(file As String) As FileTypes

        Dim fileName, fileExt As String
        Dim fileType As FileTypes

        fileName = IO.Path.GetFileName(file)
        fileExt = IO.Path.GetExtension(file).ToUpper()

        'Check whether the file exists
        If Not IO.File.Exists(file) Then
            'A WEL/WBL file may be zipped within a WLZIP file, so try extracting it from there
            If fileExt = FileExtensions.WEL Or fileExt = FileExtensions.WBL Then
                If Not Fileformats.WEL.extractFromWLZIP(file) Then
                    Throw New Exception($"File '{file}' not found!")
                End If
            Else
                Throw New Exception($"File '{file}' not found!")
            End If
        End If

        'set default
        fileType = FileTypes.UNKNOWN

        'Depending on file extension
        Select Case fileExt

            Case FileExtensions.ASC
                If Fileformats.GISMO_WEL.verifyFormat(file) Then
                    'GISMO result file in WEL format
                    Log.AddLogEntry(levels.info, $"Detected GISMO result format for file {fileName}.")
                    fileType = FileTypes.GISMO_WEL
                Else
                    'Assume SMUSI ASC format
                    Log.AddLogEntry(levels.info, $"Assuming SMUSI ASC format for file {fileName}.")
                    fileType = FileTypes.ASC
                End If

            Case FileExtensions.BCS
                'HYBNAT BCS file
                Log.AddLogEntry(levels.info, $"Assuming HYBNAT BCS format for file {fileName}.")
                fileType = FileTypes.HYBNAT_BCS

            Case FileExtensions.BIN
                'SYDRO binary file
                Log.AddLogEntry(levels.info, $"Assuming SYDRO binary format for file {fileName}.")
                fileType = FileTypes.BIN

            Case FileExtensions.CSV
                'check file format
                If Fileformats.GISMO_WEL.verifyFormat(file) Then
                    'GISMO result file in CSV format
                    Log.AddLogEntry(levels.info, $"Detected GISMO result format for file {fileName}.")
                    fileType = FileTypes.GISMO_WEL
                Else
                    Log.AddLogEntry(levels.info, $"Assuming CSV format for file {fileName}.")
                    fileType = FileTypes.CSV
                End If

            Case FileExtensions.DAT
                'Check file format
                If Fileformats.HystemExtran_REG.verifyFormat(file) Then
                    'Hystem-Extran rainfall file
                    Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {fileName}.")
                    fileType = FileTypes.HYSTEM_REG
                ElseIf Fileformats.PRMS.verifyFormat(file) Then
                    'PRMS result file
                    Log.AddLogEntry(levels.info, $"Detected PRMS result format for file {fileName}.")
                    fileType = FileTypes.PRMS_OUT
                ElseIf Fileformats.JAMS.verifyFormat(file) Then
                    'JAMS result file
                    Log.AddLogEntry(levels.info, $"Detected JAMS result format for file {fileName}.")
                    fileType = FileTypes.JAMS
                ElseIf Fileformats.SWMM_TIMESERIES.verifyFormat(file) Then
                    'SWMM time series format
                    Log.AddLogEntry(levels.info, $"Detected SWMM time series format for file {fileName}.")
                    fileType = FileTypes.SWMM_TIMESERIES
                ElseIf Fileformats.HYDRO_AS_2D.verifyFormat(file) Then
                    'HYDRO-AS_2D result file
                    Log.AddLogEntry(levels.info, $"Detected HYDRO_AS-2D result format for file {fileName}.")
                    fileType = FileTypes.HYDRO_AS_DAT
                End If

            Case FileExtensions.DB
                Log.AddLogEntry(levels.info, $"Assuming SYDRO SQLite format for file {fileName}.")
                fileType = FileTypes.SYDROSQLITE

            Case FileExtensions.DFS0
                Log.AddLogEntry(levels.info, $"Assuming DHI DFS0 format for file {fileName}.")
                fileType = FileTypes.DFS0

            Case FileExtensions.OUT
                If Fileformats.PRMS.verifyFormat(file) Then
                    'PRMS result format
                    Log.AddLogEntry(levels.info, $"Detected PRMS result format for file {fileName}.")
                    fileType = FileTypes.PRMS_OUT
                Else
                    'Assume SWMM5 binary output format
                    Log.AddLogEntry(levels.info, $"Assuming SWMM5 binary output format for file {fileName}.")
                    fileType = FileTypes.SWMM_OUT
                End If

            Case FileExtensions.REG
                'Check file format
                If Fileformats.SMUSI_REG.verifyFormat(file) Then
                    'SMUSI rainfall file
                    Log.AddLogEntry(levels.info, $"Detected SMUSI rainfall format for file {fileName}.")
                    fileType = FileTypes.SMUSI_REG
                ElseIf Fileformats.HystemExtran_REG.verifyFormat(file) Then
                    'Hystem-Extran rainfall file
                    Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {fileName}.")
                    fileType = FileTypes.HYSTEM_REG
                End If

            Case FileExtensions.SMB
                Log.AddLogEntry(levels.info, $"Assuming SIMBA format for file {fileName}.")
                fileType = FileTypes.SMB

            Case FileExtensions.TEN
                Log.AddLogEntry(levels.info, $"Assuming TeeChart native format for file {fileName}.")
                fileType = FileTypes.TEN

            Case FileExtensions.TXT
                'Check file format
                If Fileformats.SWMM_LID_REPORT.verifyFormat(file) Then
                    'SWMM LID report file
                    Log.AddLogEntry(levels.info, $"Detected SWMM LID report file format for file {fileName}.")
                    fileType = FileTypes.SWMM_LID_REPORT
                ElseIf Fileformats.SWMM_INTERFACE.verifyFormat(file) Then
                    'SWMM routing interface file
                    Log.AddLogEntry(levels.info, $"Detected SWMM routing interface file format for file {fileName}.")
                    fileType = FileTypes.SWMM_INTERFACE
                End If

            Case FileExtensions.UVF
                'Check file format
                If Fileformats.UVF.verifyFormat(file) Then
                    fileType = FileTypes.UVF
                End If

            Case FileExtensions.WBL
                'Check format
                If Fileformats.WBL.verifyFormat(file) Then
                    'SYDRO binary WEL file
                    Log.AddLogEntry(levels.info, $"Detected SYDRO binary WEL format for file {fileName}.")
                    fileType = FileTypes.WBL
                End If

            Case FileExtensions.WEL, FileExtensions.KWL
                'Check file format
                If Fileformats.WEL.verifyFormat(file) Then
                    'WEL file
                    Log.AddLogEntry(levels.info, $"Detected BlueM/Talsim WEL format for file {fileName}.")
                    fileType = FileTypes.WEL
                ElseIf Fileformats.HystemExtran_WEL.verifyFormat(file) Then
                    'Hystem-Extran rainfall file
                    Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {fileName}.")
                    fileType = FileTypes.HYSTEM_WEL
                ElseIf Fileformats.WBL.verifyFormat(file) Then
                    'SYDRO binary WEL file
                    Log.AddLogEntry(levels.info, $"Detected SYDRO binary WEL format for file {fileName}.")
                    fileType = FileTypes.WBL
                ElseIf Fileformats.HYBNAT_WEL.verifyFormat(file) Then
                    'HYBNAT WEL file
                    Log.AddLogEntry(levels.info, $"Detected HYBNAT WEL format for file {fileName}.")
                    fileType = FileTypes.HYBNAT_WEL
                End If

            Case FileExtensions.WVP
                Log.AddLogEntry(levels.info, $"Assuming Wave project file format for file {fileName}.")
                fileType = FileTypes.WVP

            Case FileExtensions.ZRE
                Log.AddLogEntry(levels.info, $"Assuming ZRE format for file {fileName}.")
                fileType = FileTypes.ZRE

            Case FileExtensions.ZRX, FileExtensions.ZRXP
                Log.AddLogEntry(levels.info, $"Assuming ZRXP format for file {fileName}.")
                fileType = FileTypes.ZRXP

            Case Else
                'Unknown filetype
                fileType = FileTypes.UNKNOWN

        End Select

        'Unknown filetype
        If fileType = FileTypes.UNKNOWN Then
            Log.AddLogEntry(levels.warning, $"Unable to determine file type of file {fileName}!")
        End If

        Return fileType

    End Function

    ''' <summary>
    ''' Factory method for creating a TimeSeriesFile instance based on the file type
    ''' </summary>
    ''' <param name="file">Path to the file</param>
    ''' <param name="fileType">Optional file type. If not provided, the type is determined using `getFileType()`</param>
    ''' <returns>A TimeSeriesFile instance representing the file</returns>
    Public Shared Function getInstance(file As String, Optional fileType As FileTypes = FileTypes.UNDETERMINED) As TimeSeriesFile

        Dim FileInstance As TimeSeriesFile

        'determine file type if not passed as argument
        If fileType = FileTypes.UNDETERMINED Then
            fileType = getFileType(file)
        End If

        'Depending on file type
        Select Case fileType

            Case FileTypes.UNKNOWN
                Log.AddLogEntry(levels.warning, $"File {IO.Path.GetFileName(file)} has an unknown file type, attempting to load as CSV.")
                FileInstance = New Fileformats.CSV(file)
            Case FileTypes.ASC
                FileInstance = New Fileformats.ASC(file)
            Case FileTypes.BIN
                FileInstance = New Fileformats.BIN(file)
            Case FileTypes.CSV
                FileInstance = New Fileformats.CSV(file)
            Case FileTypes.HYDRO_AS_DAT
                FileInstance = New Fileformats.HYDRO_AS_2D(file)
            Case FileTypes.DFS0
                FileInstance = New Fileformats.DFS0(file)
            Case FileTypes.GISMO_WEL
                FileInstance = New Fileformats.GISMO_WEL(file)
            Case FileTypes.HYBNAT_BCS
                FileInstance = New Fileformats.HYBNAT_BCS(file)
            Case FileTypes.HYBNAT_WEL
                FileInstance = New Fileformats.HYBNAT_WEL(file)
            Case FileTypes.HYSTEM_REG
                FileInstance = New Fileformats.HystemExtran_REG(file)
            Case FileTypes.HYSTEM_WEL
                FileInstance = New Fileformats.HystemExtran_WEL(file)
            Case FileTypes.JAMS
                FileInstance = New Fileformats.JAMS(file)
            Case FileTypes.PRMS_OUT
                FileInstance = New Fileformats.PRMS(file)
            Case FileTypes.SMB
                FileInstance = New Fileformats.SMB(file)
            Case FileTypes.SMUSI_REG
                FileInstance = New Fileformats.SMUSI_REG(file)
            Case FileTypes.SWMM_INTERFACE
                FileInstance = New Fileformats.SWMM_INTERFACE(file)
            Case FileTypes.SWMM_LID_REPORT
                FileInstance = New Fileformats.SWMM_LID_REPORT(file)
            Case FileTypes.SWMM_TIMESERIES
                FileInstance = New Fileformats.SWMM_TIMESERIES(file)
            Case FileTypes.SWMM_OUT
                FileInstance = New Fileformats.SWMM_OUT(file)
            Case FileTypes.SYDROSQLITE
                FileInstance = New Fileformats.SydroSQLite(file)
            Case FileTypes.TEN
                Throw New Exception("Native TeeChart files (TEN) must to be loaded using `Wave.Import_File()`!")
            Case FileTypes.UVF
                FileInstance = New Fileformats.UVF(file)
            Case FileTypes.WBL
                FileInstance = New Fileformats.WBL(file)
            Case FileTypes.WEL
                FileInstance = New Fileformats.WEL(file)
            Case FileTypes.WVP
                Throw New Exception("Wave project files (WVP) need to be loaded using `Wave.Import_File()` or `Wave.Load_WVP()`!")
            Case FileTypes.ZRE
                FileInstance = New Fileformats.ZRE(file)
            Case FileTypes.ZRXP
                FileInstance = New Fileformats.ZRXP(file)
            Case Else
                Throw New Exception($"Unknown file type {fileType}!")

        End Select

        Return FileInstance

    End Function

End Class