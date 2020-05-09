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
Imports System.IO

''' <summary>
''' Class for the SYDRO SQLite format
''' </summary>
''' <remarks>
''' Currently only supports time series of class DefaultFlag and FlaggedTimeseries
''' Has the following dependencies:
''' SydroSQLiteNet.dll
''' SydroZreNet.dll
''' System.Data.SQLite
''' SydroCommon.dll
''' SydroDomain.dll
''' sydrodomain.ini
''' log4net.dll
''' </remarks>
Public Class SQLite
    Inherits FileFormatBase

    ''' <summary>
    ''' Specifies whether the import dialog should be shown during import
    ''' </summary>
    ''' <remarks>Returns True if the class is FlaggedTimeseries, otherwise False</remarks>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Select Me.ts_class
                Case Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Flagged, _
                    Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Forecast
                    Return True
                Case Else
                    Return False
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Customer used for making calls to SydroSQLiteNet
    ''' </summary>
    ''' <remarks>Is required, but its value is irrelevant</remarks>
    Const customer As String = "Unknown"

    ''' <summary>
    ''' User name used for making calls to SydroSQLiteNet
    ''' </summary>
    ''' <remarks>Is required, but its value is irrelevant</remarks>
    Const user As String = "Wave"

    ''' <summary>
    ''' Time series ID
    ''' </summary>
    ''' <remarks>Is determined by the filename but has to correspond to the ID in the database</remarks>
    Private id As Integer

    ''' <summary>
    ''' Time series interpretation
    ''' </summary>
    Private interpretation As TimeSeries.InterpretationEnum

    ''' <summary>
    ''' Time series class (e.g. DefaultFlag, FlaggedTimeSeries, etc.)
    ''' </summary>
    Private ts_class As Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags

    ''' <summary>
    ''' Mapping of ColumnInfo.Index to Flag
    ''' </summary>
    ''' <remarks></remarks>
    Private flag_mapping As Dictionary(Of Integer, Integer)

    ''' <summary>
    ''' Path to the file, used for calls to SydroSQLiteNet
    ''' </summary>
    Private ReadOnly Property path() As String
        Get
            Return IO.Path.GetDirectoryName(Me.File) & "\"
        End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="ReadAllNow"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.Dateformat = Helpers.DefaultDateFormat 'irrelevant weil binär
        Me.iLineData = 0
        Me.UseUnits = False

        Me.flag_mapping = New Dictionary(Of Integer, Integer)

        Call Me.readSeriesInfo()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllSeries()
            Call Me.readFile()
        End If

    End Sub

    ''' <summary>
    ''' Reads information about the time series stored in the file
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub readSeriesInfo()

        Dim sql As Sydro.SydroZre.SydroSQLNet
        Dim ts_atts As Sydro.SydroZre.SydroTimeSeriesAttributes
        Dim att_string As String
        Dim att_pairs() As String
        Dim key, value As String
        Dim ts_name As String
        Dim sInfo As SeriesInfo

        Me.SeriesList.Clear()

        'Determine time series ID from the filename
        If Not Integer.TryParse(IO.Path.GetFileNameWithoutExtension(Me.File), Me.id) Then
            Throw New Exception("Unable to determine time series ID from filename!")
        End If

        'load sql file
        sql = New Sydro.SydroZre.SydroSQLNet(SQLite.customer, SQLite.user, Me.id, Me.path)
        If sql.HasError Then
            Throw New Exception("Error while reading SQLite database: " & sql.ErrorMsg)
        End If

        'read and parse attributes and store as FileMetadata
        ts_atts = sql.TimeSeriesAttributes()
        If ts_atts.HasError Then
            Throw New Exception("Error while reading time series attributes: " & ts_atts.ResultMsg)
        End If
        att_string = ts_atts.KeyValueString
        att_pairs = att_string.Split(ts_atts.KeyValuePairsSplit)
        For Each att_pair As String In att_pairs
            key = att_pair.Split(ts_atts.KeyValueSplit)(0)
            value = att_pair.Split(ts_atts.KeyValueSplit)(1)
            If Not Me.FileMetadata.ContainsKey(key) Then
                'TODO: this is because ReadSeriesInfo() may be called multiple times
                Me.FileMetadata.Add(key, value)
            End If
        Next
        'store the name
        ts_name = Me.FileMetadata(ts_atts.MetaShortNameField)
        If ts_name.Trim() = "" Then
            'if name is empty, use the ID
            ts_name = Me.id
        End If
        'store the interpretation
        'TODO: this only works because TimeSeries.InterpretationEnum currently uses the same values as sydrodomain!
        Me.interpretation = Me.FileMetadata(ts_atts.MetaInterpretationField)
        'store the unit
        'TODO: We can only get the UnitId from the attributes
        'therefore the unit is only read later when actually reading the series

        'Determine TSClass
        Me.ts_class = Sydro.SydroZre.SydroSQLNet.TimeSeriesClass(SQLite.user, Me.id, Me.path)

        'read and store series info
        Me.flag_mapping = New Dictionary(Of Integer, Integer)
        Select Case Me.ts_class
            Case Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Unknown
                Throw New Exception("Time series class is unknown!")

            Case Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.DefaultFlag
                '"Normal" time series has only one series
                sInfo = New SeriesInfo()
                sInfo.Name = ts_name
                sInfo.Index = 1
                Me.SeriesList.Add(sInfo)
                Me.flag_mapping.Add(1, 0)

            Case Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Flagged, _
                Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Forecast
                'Read flags and add them as individual columns
                Dim flagstring As String
                Dim flag_id As Integer
                Dim flag_desc As String
                flagstring = Sydro.SydroZre.SydroSQLNet.AttributeFlags(SQLite.user, Me.id, Me.path)
                Dim index As Integer = 1
                For Each flag As String In flagstring.Split("#")
                    flag_id = flag.Split(",")(0)
                    flag_desc = flag.Split(",")(2)
                    sInfo = New SeriesInfo()
                    sInfo.Name = ts_name & "/" & flag_id & ": " & flag_desc
                    sInfo.Index = index
                    Me.SeriesList.Add(sInfo)
                    Me.flag_mapping.Add(index, flag_id)
                    index += 1
                Next

            Case Else
                Throw New Exception("Time series of class " & ts_class & " are not yet supported!")
        End Select

    End Sub


    ''' <summary>
    ''' Reads the time series from the file
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub readFile()

        Dim ts As TimeSeries
        Dim ts_list As List(Of TimeSeries)
        Dim sql As Sydro.SydroZre.SydroSQLNet
        Dim sydro_ts As Sydro.SydroZre.SydroTimeSeries
        Dim flag_id As Integer
        Dim dateArr() As Date
        Dim sngArr() As Single

        'load sql file
        sql = New Sydro.SydroZre.SydroSQLNet(SQLite.customer, SQLite.user, Me.id, Me.path)
        If sql.HasError Then
            Throw New Exception("Error while reading SQLite database: " & sql.ErrorMsg)
        End If

        ts_list = New List(Of TimeSeries)

        Select Case Me.ts_class
            Case Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Unknown
                Throw New Exception("Time series class is unknown!")

            Case Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.DefaultFlag, _
                Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Flagged
                'Loop over selected series
                For Each sInfo As SeriesInfo In Me.SelectedSeries
                    flag_id = Me.flag_mapping(sInfo.Index)
                    'retrieve time series from sqlite
                    sydro_ts = sql.TimeSeries(flag_id, New Date(0), New Date(0))
                    Log.AddLogEntry("SydroSQLiteNet: " & sydro_ts.ResultMsg)
                    'get time series values
                    ReDim dateArr(0)
                    ReDim sngArr(0)
                    sydro_ts.TimeSeriesStringToArray(dateArr, sngArr)
                    'check whether the series actually contains data
                    If dateArr.Length = 0 Then
                        MsgBox("Time series " & sInfo.Name & " is empty!", MsgBoxStyle.Exclamation)
                        Continue For
                    End If
                    'store as new time series
                    ts = New TimeSeries(sInfo.Name)
                    ts.Unit = sydro_ts.Metadata.Unit 'use unit from sydro_ts because colinfo doesn't have it
                    For j As Integer = 0 To dateArr.Count - 1
                        ts.AddNode(dateArr(j), sngArr(j))
                    Next
                    'store interpretation
                    ts.Interpretation = Me.interpretation
                    'copy FileMetadata to TimeSeriesMetadata
                    ts.Metadata = Me.FileMetadata

                    'store timeseries
                    Me.TimeSeriesCollection.Add(ts.Title, ts)
                Next

            Case Sydro.SydroZre.SydroSQLNet.EnumTimeseriesClassFlags.Forecast
                Dim unit As String
                Dim T0Arr As DateTime()
                Dim fcLengthArr(), flagArr() As Short
                Dim fc_ts As Sydro.SydroZre.SydroSQLFCTimeSeries
                Dim ts_dict As Dictionary(Of DateTime, TimeSeries)
                'get unit
                sydro_ts = sql.TimeSeries(0, New Date(0), New Date(0))
                unit = sydro_ts.Metadata.Unit
                'instantiate ForecastTimeseries
                fc_ts = New Sydro.SydroZre.SydroSQLFCTimeSeries(SQLite.user, Me.id, Me.path)
                'Loop over selected series
                For Each sInfo As SeriesInfo In Me.SelectedSeries
                    ReDim T0Arr(0)
                    ReDim dateArr(0)
                    ReDim fcLengthArr(0)
                    ReDim flagArr(0)
                    ReDim sngArr(0)

                    flag_id = Me.flag_mapping(sInfo.Index)
                    'get T0 dates
                    Dim ts_T0 As Sydro.SydroZre.SydroTimeSeries
                    ts_T0 = fc_ts.getTimeSeriesFCDates(flag_id)
                    ts_T0.TimeSeriesStringToArray(T0Arr, sngArr)
                    'create a dict to store individual time series for each T0
                    ts_dict = New Dictionary(Of DateTime, TimeSeries)
                    For Each T0 As DateTime In T0Arr
                        'retrieve time series for flag from sqlite
                        fc_ts.getValues(flag_id, T0, New Date(0), New Date(0), T0Arr, dateArr, fcLengthArr, sngArr, flagArr)
                        'check whether the series actually contains data
                        If dateArr.Length = 0 Then
                            MsgBox("Time series " & sInfo.Name & " is empty!", MsgBoxStyle.Exclamation)
                            Continue For
                        End If
                        ts = New TimeSeries(T0 & ": " & sInfo.Name)
                        'store unit
                        ts.Unit = unit
                        'store interpretation
                        ts.Interpretation = Me.interpretation
                        'copy FileMetadata to TimeSeriesMetadata
                        ts.Metadata = Me.FileMetadata
                        'store values
                        For j As Integer = 0 To dateArr.Count - 1
                            ts.AddNode(dateArr(j), sngArr(j))
                        Next

                        ts_dict.Add(T0, ts)
                    Next

                    'store timeseries
                    For Each ts In ts_dict.Values
                        Me.TimeSeriesCollection.Add(ts.Title, ts)
                    Next
                Next

            Case Else
                Throw New Exception("Time series of class " & ts_class & " are not yet supported!")

        End Select

    End Sub

End Class
