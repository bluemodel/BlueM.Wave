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
''' Base class for all file formats
''' </summary>
Public MustInherit Class FileFormatBase

    ''' <summary>
    ''' FileFormats
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum FileFormats As Integer
        ZRE = 1
        WEL = 2
        CSV = 3
        ASC = 4
        REG_HYSTEM = 5
        REG_SMUSI = 6
        DAT_SWMM_MASS = 7
        DAT_SWMM_TIME = 8
        TXT_SWMM = 9
        DAT_HYDRO_AS = 10
        UVF = 11
        ZRXP = 12
        WVP = 13
        BIN = 14
    End Enum

    ''' <summary>
    ''' FileFilter for file dialogs
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FileFilter As String = _
            "All files (*.*)|*.*|" & _
            "Text files (*.txt)|*.txt|" & _
            "CSV files (*.csv)|*.csv|" & _
            "ZRE files (*.zre)|*.zre|" & _
            "WEL files (*.wel, *.kwl)|*.wel;*.kwl|" & _
            "UVF files (*.uvf)|*.uvf|" & _
            "SMUSI files (*.asc)|*.asc|" & _
            "SIMBA files (*.smb)|*.smb|" & _
            "Hystem Extran files (*.dat)|*.dat|" & _
            "SWMM files (*.out)|*.out|" & _
            "HYDRO_AS-2D files (*.dat)|*.dat|" & _
            "SYDRO binary files (*.bin)|*.bin|" & _
            "ZRXP files (*.zrx)|*.zrx|" & _
            "Wave project files (*wvp)|*.wvp"

#Region "allgemeine Eigenschaften"
    Protected SpaltenOffset As Integer = 0          'Anzahl Zeichen bevor die erste Spalte anfängt (nur bei Spalten mit fester Breite)
#End Region

#Region "Eigenschaften"

    Private _file As String
    Private _isColumnSeparated As Boolean = True
    Private _trennzeichen As Character = semicolon
    Private _dateFormat As String = Helpers.DefaultDateFormat
    Private _decimalSeparator As Character = period
    Private _iLineHeadings As Integer = 1
    Private _iLineUnits As Integer = 2
    Private _iLineData As Integer = 3
    Private _useUnits As Boolean = True
    Private _columnWidth As Integer = 16
    Private _dateTimeColumnIndex As Integer = 0
    Private _columns() As ColumnInfo
    Private _selectedColumns() As ColumnInfo
    Private _nLinesperTimestamp As Integer = 1
    Private _metadata As Dictionary(Of String, String)

    ''' <summary>
    ''' Contains information about a column in a file
    ''' </summary>
    ''' <remarks>Can represent either values or timestamps</remarks>
    Public Structure ColumnInfo
        Public Name As String     'Gesamte Bezeichnung der Reihe (z.B. "S101 FLOW")
        Public Einheit As String  '"LPS", "CMS", "MG/L"
        Public Index As Integer
        Public Objekt As String   'Bezeichnung des Objekts (z.B. "S101")
        Public Type As String     '"FLOW" oder ein Stoffparameter (z.B. "CSB")
        Public ObjType As String  '"Subcatchment", "Node" oder "Link"
        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Structure

    ''' <summary>
    ''' File metadata
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Metadata() As Dictionary(Of String, String)
        Get
            Return Me._metadata
        End Get
    End Property

    ''' <summary>
    ''' Array stores the TimeSeries read from the file
    ''' </summary>
    Public TimeSeries() As TimeSeries

    ''' <summary>
    ''' Instance of the ImportDialog
    ''' </summary>
    Friend ImportDiag As ImportDiag

#End Region 'Eigenschaften

#Region "Properties"

    ''' <summary>
    ''' Path to file
    ''' </summary>
    Public Property File() As String
        Get
            Return _file
        End Get
        Set(ByVal value As String)
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
        Set(ByVal value As Boolean)
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
        Set(ByVal value As Character)
            _trennzeichen = value
        End Set
    End Property

    ''' <summary>
    ''' Date format (default is Helpers.DefaultDateFormat)
    ''' </summary>
    Public Property Dateformat() As String
        Get
            Return _dateFormat
        End Get
        Set(ByVal value As String)
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
        Set(ByVal value As Character)
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
        Set(ByVal value As Integer)
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
        Set(ByVal value As Integer)
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
        Set(ByVal value As Integer)
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
        Set(ByVal value As Boolean)
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
        Set(ByVal value As Integer)
            _columnWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Index of the column containing the datetime values
    ''' </summary>
    Public Property DateTimeColumnIndex() As Integer
        Get
            Return _dateTimeColumnIndex
        End Get
        Set(ByVal value As Integer)
            _dateTimeColumnIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Array of all columns contained in file
    ''' </summary>
    ''' <remarks>includes the datetime column!</remarks>
    Public Property Columns() As ColumnInfo()
        Get
            Return _columns
        End Get
        Set(ByVal value As ColumnInfo())
            _columns = value
        End Set
    End Property

    ''' <summary>
    ''' Array of columns selected for import
    ''' </summary>
    Public Property SelectedColumns() As ColumnInfo()
        Get
            Return _selectedColumns
        End Get
        Set(ByVal value As ColumnInfo())
            _selectedColumns = value
        End Set
    End Property

    ''' <summary>
    ''' List of series names selected for import
    ''' </summary>
    Public ReadOnly Property SelectedSeries() As List(Of String)
        Get
            Dim seriesList As New List(Of String)
            For Each col As ColumnInfo In Me._selectedColumns
                seriesList.Add(col.Name)
            Next
            Return seriesList
        End Get
    End Property

    ''' <summary>
    ''' Number of lines per timestamp (default is 1)
    ''' </summary>
    Public Property nLinesPerTimestamp() As Integer
        Get
            Return _nLinesperTimestamp
        End Get
        Set(ByVal value As Integer)
            _nLinesperTimestamp = value
        End Set
    End Property

    ''' <summary>
    ''' Get a timeseries from the file using its title
    ''' </summary>
    ''' <param name="title">Title of the desired timeseries. Use an empty string for getting the first timeseries in the file.</param>
    ''' <returns>The timeseries</returns>
    ''' <remarks>If the timeseries has not been imported yet, an import is initiated. 
    ''' Throws an exception if the timeseries cannot be found in the file.</remarks>
    Public ReadOnly Property getTimeSeries(ByVal title As String) As TimeSeries
        Get
            'If an empty title is given, return the first timeseries
            If (title.Length = 0) Then Return Me.getTimeSeries(0)

            'Find the series using the given title
            For i As Integer = 0 To Me.TimeSeries.GetUpperBound(0)
                If (Me.TimeSeries(i).Title = title) Then
                    'return the timeseries
                    Return Me.TimeSeries(i)
                End If
            Next

            'Timeseries was not found (perhaps not yet imported?)
            'Check whether a column with the given title exists
            For Each column As ColumnInfo In Me.Columns
                If (column.Name = title) Then
                    'select the column for import
                    ReDim Me.SelectedColumns(-1)
                    Me.selectColumn(column)
                    'read the file (again)
                    Call Me.Read_File()
                    'return the timeseries
                    Return Me.getTimeSeries(title)
                End If
            Next
            'Timeseries does not exist in file
            Throw New Exception("The timeseries '" & title & "' could not be found in the file '" & IO.Path.GetFileName(Me.File) & "'!")
        End Get
    End Property

    ''' <summary>
    ''' Get a timeseries from the file using its index
    ''' </summary>
    ''' <param name="index">0-based index of the timeseries (0: 1st timeseries column)</param>
    ''' <returns>The timeseries</returns>
    ''' <remarks>If the timeseries has not been imported yet, an import is initiated. 
    ''' Throws an exception if the timeseries cannot be found in the file.</remarks>
    Public ReadOnly Property getTimeSeries(ByVal index As Integer) As TimeSeries
        Get
            If (index <= Me.TimeSeries.GetUpperBound(0)) Then
                'return the timeseries
                Return Me.TimeSeries(index)
            End If

            'Timeseries has not been imported yet
            For Each column As ColumnInfo In Me.Columns
                If (column.Index <> Me.DateTimeColumnIndex And column.Index - 1 = index) Then
                    'select the column for import
                    ReDim Me.SelectedColumns(-1)
                    Me.selectColumn(column)
                    'read the file (again)
                    Call Me.Read_File()
                    'return the timeseries
                    Return Me.getTimeSeries(index)
                End If
            Next
            'Timeseries does not exist in file
            Throw New Exception("The timeseries '" & index.ToString() & "' could not be found in the file '" & IO.Path.GetFileName(Me.File) & "'!")
        End Get
    End Property

    ''' <summary>
    ''' Flag indicating whether the ImportDialog should be used for this file
    ''' </summary>
    Public MustOverride ReadOnly Property UseImportDialog() As Boolean

    ''' <summary>
    ''' Returns a list of format-specific metadata keys
    ''' </summary>
    ''' <remarks>Should be overridden by inheriting classes</remarks>
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
    Public Sub New(ByVal FileName As String)

        'Initialize data structures
        ReDim Me.TimeSeries(-1)
        ReDim Me.Columns(-1)
        ReDim Me.SelectedColumns(-1)
        Me._metadata = New Dictionary(Of String, String)

        'Store the filepath
        Me.File = FileName

    End Sub

    ''' <summary>
    ''' Reads information about the columns (series) contained in the file
    ''' </summary>
    Public MustOverride Sub ReadColumns()

    ''' <summary>
    ''' Select all available columns for import
    ''' </summary>
    ''' <remarks>The datetime column is not selected</remarks>
    Public Sub selectAllColumns()

        Dim i, n As Integer

        ReDim Me.SelectedColumns(Me.Columns.Length - 2) 'leave out the datetime column

        n = 0
        For i = 0 To Me.Columns.Length - 1
            If (Me.Columns(i).Index <> Me.DateTimeColumnIndex) Then
                Me.SelectedColumns(n) = Me.Columns(i)
                n += 1
            End If
        Next

    End Sub

    ''' <summary>
    ''' Select a column for import
    ''' </summary>
    ''' <param name="column">ColumnInfo object</param>
    ''' <remarks></remarks>
    Public Sub selectColumn(ByVal column As ColumnInfo)
        Dim n As Integer
        n = Me.SelectedColumns.Length
        ReDim Preserve Me.SelectedColumns(n)
        Me.SelectedColumns(n) = column
    End Sub

    ''' <summary>
    ''' Select a series for import by name
    ''' </summary>
    ''' <param name="seriesName">name of the series</param>
    ''' <returns>True if successful, False if series name was not found</returns>
    ''' <remarks></remarks>
    Public Function selectSeries(ByVal seriesName As String) As Boolean
        'search for series in file
        For Each column As FileFormatBase.ColumnInfo In Me.Columns
            If column.Name = seriesName Then
                'select the column for import
                Call Me.selectColumn(column)
                Return True
            End If
        Next
        'series not found in file
        Log.AddLogEntry("ERROR: Series " & seriesName & " not found in file!")
        Return False
    End Function

    ''' <summary>
    ''' Reads the selected columns (see SelectedColumns) from the file and stores them as timeseries in the TimeSeries array
    ''' </summary>
    Public MustOverride Sub Read_File()

    ''' <summary>
    ''' Sets default metadata values for a time series corresponding to the file format
    ''' </summary>
    ''' <remarks>Should be overloaded by inheriting classes that deal with metadata</remarks>
    Public Shared Sub setDefaultMetadata(ByVal ts As TimeSeries)
        For Each key As String In FileFormatBase.MetadataKeys
            If Not ts.Metadata.ContainsKey(key) Then
                ts.Metadata.Add(key, "")
            End If
        Next
    End Sub

#End Region 'Methods

End Class
