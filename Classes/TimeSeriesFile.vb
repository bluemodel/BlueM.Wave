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
''' <summary>
''' Abstract base class for all file formats
''' </summary>
Public MustInherit Class TimeSeriesFile

#Region "allgemeine Eigenschaften"
    Protected SpaltenOffset As Integer = 0          'Anzahl Zeichen bevor die erste Spalte anfängt (nur bei Spalten mit fester Breite)
#End Region

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
    Private _dateTimeColumnIndex As Integer = 0
    Private _seriesList As List(Of SeriesInfo)
    Private _selectedSeries As List(Of SeriesInfo)
    Private _nLinesperTimestamp As Integer = 1
    Private _metadata As Metadata
    Private _encoding As Text.Encoding

    ''' <summary>
    ''' Contains basic information about a series contained in a file
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure SeriesInfo
        Public Name As String
        Public Unit As String
        Public Index As Integer
        'TODO: The following properties are only needed by SWMM file formats
        Public Objekt As String     'Bezeichnung des Objekts (z.B. "S101")
        Public Type As String       '"FLOW" oder ein Stoffparameter (z.B. "CSB")
        Public ObjType As String    '"Subcatchment", "Node" oder "Link"
        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Structure

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
    ''' Stores the TimeSeries read from the file
    ''' The key corresponds to seriesInfo.index
    ''' </summary>
    Public FileTimeSeries As Dictionary(Of Integer, TimeSeries)

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
    ''' List of all series contained in a file
    ''' </summary>
    ''' <remarks></remarks>
    Public Property SeriesList() As List(Of SeriesInfo)
        Get
            Return _seriesList
        End Get
        Set(value As List(Of SeriesInfo))
            _seriesList = value
        End Set
    End Property

    ''' <summary>
    ''' List of series currently selected for import
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedSeries() As List(Of SeriesInfo)
        Get
            Return _selectedSeries
        End Get
    End Property

    ''' <summary>
    ''' Number of lines per timestamp (default is 1)
    ''' </summary>
    Public Property nLinesPerTimestamp() As Integer
        Get
            Return _nLinesperTimestamp
        End Get
        Set(value As Integer)
            _nLinesperTimestamp = value
        End Set
    End Property

    ''' <summary>
    ''' Get a timeseries from the file using its index
    ''' </summary>
    ''' <param name="index">Index of the desired timeseries within the file</param>
    ''' <returns>The timeseries</returns>
    ''' <remarks>If the timeseries has not been imported yet, an import is initiated. 
    ''' Throws an exception if the timeseries cannot be found in the file.</remarks>
    Public ReadOnly Property getTimeSeries(Optional index As Integer = 0) As TimeSeries
        Get
            If Me.FileTimeSeries.ContainsKey(index) Then
                Return Me.FileTimeSeries(index)
            Else
                Me.selectSeries(index)
                'read the file (again)
                Me.FileTimeSeries.Clear()
                Call Me.readFile()
                Return Me.FileTimeSeries(index)
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
    Public ReadOnly Property getTimeSeries(title As String) As TimeSeries
        Get
            Dim found As Boolean = False
            'Find the series using the given title
            For Each ts As TimeSeries In Me.FileTimeSeries.Values
                If title = ts.Title Then
                    Return ts
                    found = True
                End If
            Next
            If Not found Then
                'Timeseries was not found (perhaps not yet imported?)
                'Check whether a column with the given title exists
                For Each sInfo As SeriesInfo In Me.SeriesList
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
    ''' Flag indicating whether the ImportDialog should be used for this file
    ''' </summary>
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
        Me.FileTimeSeries = New Dictionary(Of Integer, TimeSeries)
        Me._seriesList = New List(Of SeriesInfo)
        Me._selectedSeries = New List(Of SeriesInfo)
        Me._metadata = New Metadata()

        'Store the filepath
        Me.File = FileName

        'set default properties
        Me.Encoding = Helpers.DefaultEncoding

    End Sub

    ''' <summary>
    ''' Reads information about the series contained in the file
    ''' </summary>
    Public MustOverride Sub readSeriesInfo()

    ''' <summary>
    ''' Select all available series for import
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub selectAllSeries()

        Me.SelectedSeries.Clear()
        For Each sInfo As SeriesInfo In Me.SeriesList
            Me.SelectedSeries.Add(sInfo)
        Next

    End Sub

    ''' <summary>
    ''' Select a series for import by column index
    ''' </summary>
    ''' <param name="colIndex">column index</param>
    ''' <returns>True if successful, False if column index was not found</returns>
    ''' <remarks></remarks>
    Public Function selectSeries(colIndex As Integer) As Boolean

        Dim i As Integer = 0
        For Each sInfo As SeriesInfo In Me.SeriesList
            If sInfo.Index = colIndex Then
                Me.SelectedSeries.Add(sInfo)
                Return True
            End If
        Next
        'series not found in file
        Log.AddLogEntry(Log.levels.error, $"Series with column index {colIndex} not found in file!")
        Return False

    End Function

    ''' <summary>
    ''' Select a series for import by name
    ''' </summary>
    ''' <param name="seriesName">name of the series</param>
    ''' <returns>True if successful, False if series name was not found</returns>
    ''' <remarks></remarks>
    Public Function selectSeries(seriesName As String) As Boolean

        For Each sInfo As SeriesInfo In Me.SeriesList
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
    ''' Reads the selected series (see `SelectedSeries`) from the file and stores them as timeseries in `FileTimeSeries`
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

End Class