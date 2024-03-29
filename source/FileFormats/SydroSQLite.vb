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

Namespace Fileformats

    ''' <summary>
    ''' Class for the SYDRO SQLite format
    ''' </summary>
    Public Class SydroSQLite
        Inherits TimeSeriesFile

        Private Enum TimeseriesClassEnum As Short
            Unknown = -1
            DefaultFlag = 0
            Flagged = 1
            Forecast = 2
            Index = 3
        End Enum

        ''' <summary>
        ''' Specifies whether the import dialog should be shown during import
        ''' </summary>
        ''' <remarks>Returns True if the class is FlaggedTimeseries, otherwise False</remarks>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Select Case Me.ts_class
                    Case TimeseriesClassEnum.Flagged,
                    TimeseriesClassEnum.Forecast
                        Return True
                    Case Else
                        Return False
                End Select
            End Get
        End Property

        ''' <summary>
        ''' Time series interpretation
        ''' </summary>
        Private interpretation As TimeSeries.InterpretationEnum

        ''' <summary>
        ''' Time series class (e.g. DefaultFlag, FlaggedTimeSeries, etc.)
        ''' </summary>
        Private ts_class As TimeseriesClassEnum

        ''' <summary>
        ''' Time series unit
        ''' </summary>
        Private unit As String

        ''' <summary>
        ''' Mapping of SeriesInfo.Index to Flag
        ''' </summary>
        ''' <remarks>Used for FlaggedTimeseries</remarks>
        Private flag_mapping As Dictionary(Of Integer, Integer)

        ''' <summary>
        ''' Mapping of SeriesInfo.Index to Flag and T0
        ''' </summary>
        ''' <remarks>Used for FCTimeseries</remarks>
        Private flag_T0_mapping As Dictionary(Of Integer, (flag_id As Integer, T0 As DateTime))

        ''' <summary>
        ''' Converts a unit id to its corresponding name
        ''' </summary>
        ''' <param name="unitId">unit id</param>
        ''' <returns>unit name</returns>
        ''' <remarks>
        ''' obtained from sydrodomain.ini as of 2019-11-08
        ''' </remarks>
        Private Shared Function getUnitName(unitId As Integer) As String
            Dim units As New Dictionary(Of Integer, String) From {
            {29, "?"},
            {83, "leer"},
            {79, "number"},
            {82, "class"},
            {13, "mm/m"},
            {21, "Prozent"},
            {30, "h/d"},
            {32, "0/00"},
            {33, "leer"},
            {80, "ml/l"},
            {5, "mm"},
            {9, "m"},
            {15, "cm"},
            {34, "km"},
            {35, "hm"},
            {42, "dm"},
            {66, "l/ha"},
            {67, "l/km2"},
            {68, "l/m2"},
            {69, "mNN"},
            {36, "m2"},
            {37, "km2"},
            {38, "ha"},
            {39, "cm2"},
            {40, "mm2"},
            {41, "dm2"},
            {78, "m3/m"},
            {6, "m3"},
            {7, "Tsd.m3"},
            {14, "Mio.m3"},
            {18, "l"},
            {43, "hl"},
            {44, "km3"},
            {45, "cm3"},
            {46, "mm3"},
            {2, "m/s"},
            {3, "mm/d"},
            {4, "mm/h"},
            {16, "mm/min"},
            {20, "l/s/km2"},
            {51, "m/h"},
            {52, "m/d"},
            {56, "l/s/ha"},
            {62, "m/s2"},
            {1, "m3/s"},
            {12, "l/s"},
            {8, "m3/d"},
            {57, "m3/h"},
            {60, "l/h"},
            {61, "l/d"},
            {10, "oC"},
            {47, "kg"},
            {48, "g"},
            {49, "mg"},
            {50, "t"},
            {24, "s"},
            {25, "min"},
            {26, "h"},
            {28, "d"},
            {63, "w"},
            {64, "mon"},
            {65, "a"},
            {27, "Datum"},
            {72, "A"},
            {73, "bar"},
            {74, "pa"},
            {75, "hpa"},
            {76, "Grad"},
            {77, "Rad"},
            {70, "kg/m3"},
            {71, "mg/l"},
            {81, "g/l"},
            {84, "l/kg"},
            {85, "ml/g"},
            {31, "Grad_C"},
            {86, "kg/s"},
            {87, "y/n"}
        }

            If units.ContainsKey(unitId) Then
                Return units(unitId)
            Else
                Log.AddLogEntry(Log.levels.warning, "Unrecognized UnitID " & unitId)
                Return "-"
            End If
        End Function

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="FileName"></param>
        ''' <param name="ReadAllNow"></param>
        ''' <remarks></remarks>
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.Dateformat = "yyyy-MM-dd HH:mm" 'only used for DB queries
            Me.iLineData = 0
            Me.UseUnits = True

            Me.flag_mapping = New Dictionary(Of Integer, Integer)
            Me.flag_T0_mapping = New Dictionary(Of Integer, (flag_id As Integer, T0 As DateTime))

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' The database connection object
        ''' </summary>
        Private _connection As Data.SQLite.SQLiteConnection

        ''' <summary>
        ''' Connects to the database if not already connected and returns the connection object
        ''' </summary>
        ''' <returns></returns>
        Private ReadOnly Property connection As Data.SQLite.SQLiteConnection
            Get
                If IsNothing(Me._connection) Then
                    Dim connStringBuilder As Data.SQLite.SQLiteConnectionStringBuilder
                    Dim connString As String

                    connStringBuilder = New Data.SQLite.SQLiteConnectionStringBuilder()
                    connStringBuilder.DataSource = Me.File
                    connStringBuilder.ReadOnly = True
                    connStringBuilder.DateTimeFormat = Data.SQLite.SQLiteDateFormats.Default
                    connString = connStringBuilder.ConnectionString
                    Me._connection = New Data.SQLite.SQLiteConnection(connString, parseViaFramework:=True)
                End If
                Return Me._connection
            End Get
        End Property


        ''' <summary>
        ''' Reads information about the time series stored in the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readSeriesInfo()

            Dim key, value As String
            Dim ts_name As String
            Dim sInfo As TimeSeriesInfo

            'Connect to db
            Me.connection.Open()

            'read metadata from DB (convert all values to strings)
            Dim command As New Data.SQLite.SQLiteCommand(Me.connection)
            Dim dataReader As Data.SQLite.SQLiteDataReader
            command.CommandText = "SELECT * FROM Metadata LIMIT 1;" 'sometimes there may erroneously be more than one record
            dataReader = command.ExecuteReader()
            dataReader.Read()

            Me.FileMetadata.Clear()
            For i As Integer = 0 To dataReader.FieldCount - 1
                key = dataReader.GetName(i)
                value = dataReader.GetValue(i)
                Me.FileMetadata.Add(key, value)
            Next
            dataReader.Close()

            'extract significant metadata

            'series name
            If Me.FileMetadata.ContainsKey("Shortname") AndAlso Me.FileMetadata("Shortname").Trim() <> "" Then
                ts_name = Me.FileMetadata("Shortname").Trim()
            Else
                'use file name as name
                ts_name = IO.Path.GetFileName(Me.File)
            End If

            'time series class
            If Me.FileMetadata.ContainsKey("TimeSeriesClass") Then
                Me.ts_class = Convert.ToInt32(Me.FileMetadata("TimeSeriesClass"))
            End If

            'interpretation
            If Me.FileMetadata.ContainsKey("InterpretationId") Then
                'TODO: this only works because TimeSeries.InterpretationEnum currently uses the same values as sydrodomain!
                Me.interpretation = Convert.ToInt32(Me.FileMetadata("InterpretationId"))
            End If

            'unit
            If Me.FileMetadata.ContainsKey("UnitId") Then
                Me.unit = SydroSQLite.getUnitName(Convert.ToInt32(Me.FileMetadata("UnitId")))
            Else
                Me.unit = "-"
            End If

            'read flags from DB
            Dim flags As Dictionary(Of Integer, String) ' {id: description, ...}
            command.CommandText = "SELECT * FROM AttributeFlag;"
            dataReader = command.ExecuteReader()
            flags = New Dictionary(Of Integer, String)
            Do While dataReader.Read()
                flags.Add(dataReader.GetInt32(0), dataReader.GetString(2))
            Loop

            'read and store series info
            Me.TimeSeriesInfos.Clear()
            Me.flag_mapping = New Dictionary(Of Integer, Integer)
            Me.flag_T0_mapping = New Dictionary(Of Integer, (flag_id As Integer, T0 As DateTime))
            Select Case Me.ts_class
                Case TimeseriesClassEnum.Unknown
                    Throw New Exception("Time series class is unknown!")

                Case TimeseriesClassEnum.DefaultFlag
                    '"Normal" time series has only one series
                    sInfo = New TimeSeriesInfo()
                    sInfo.Name = ts_name
                    sInfo.Index = 0
                    Me.TimeSeriesInfos.Add(sInfo)
                    Me.flag_mapping.Add(0, 0)

                Case TimeseriesClassEnum.Flagged
                    'Add each flag as an individual series
                    Dim flag_id As Integer
                    Dim flag_desc As String
                    Dim index As Integer = 0
                    For Each flag As KeyValuePair(Of Integer, String) In flags
                        flag_id = flag.Key
                        flag_desc = flag.Value
                        sInfo = New TimeSeriesInfo()
                        sInfo.Name = $"{ts_name}/{flag_id}: {flag_desc}"
                        sInfo.Index = index
                        Me.TimeSeriesInfos.Add(sInfo)
                        Me.flag_mapping.Add(index, flag_id)
                        index += 1
                    Next

                Case TimeseriesClassEnum.Forecast
                    'Read flag and T0 combinations and add them as individual series
                    Dim flag_id As Integer
                    Dim flag_desc As String
                    Dim index As Integer = 0
                    Dim T0_list As List(Of DateTime)

                    For Each flag As KeyValuePair(Of Integer, String) In flags
                        flag_id = flag.Key
                        flag_desc = flag.Value
                        'get T0-dates from db
                        T0_list = New List(Of DateTime)
                        command = New Data.SQLite.SQLiteCommand(Me.connection)
                        command.CommandText = "SELECT DISTINCT(rDate) FROM TimeseriesValues WHERE attribFlag = @flag ORDER BY rDate;"
                        command.Parameters.Add("@flag", Data.DbType.Int32)
                        command.Parameters("@flag").Value = flag_id
                        command.Prepare()
                        dataReader = command.ExecuteReader()
                        Do While dataReader.Read()
                            T0_list.Add(dataReader.GetDateTime(0))
                        Loop
                        dataReader.Close()
                        For Each T0 As DateTime In T0_list
                            sInfo = New TimeSeriesInfo()
                            sInfo.Name = $"{ts_name}/{flag_id}: {flag_desc} - {T0.ToString(Helpers.CurrentDateFormat)}"
                            sInfo.Index = index
                            Me.TimeSeriesInfos.Add(sInfo)
                            Me.flag_T0_mapping.Add(index, (flag_id, T0))
                            index += 1
                        Next
                    Next

                Case Else
                    Throw New Exception($"Time series of class {ts_class} are not yet supported!")
            End Select

            'disconnect db
            Me.connection.Close()

        End Sub


        ''' <summary>
        ''' Reads the time series from the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readFile()

            Dim ts As TimeSeries
            Dim sInfo As TimeSeriesInfo
            Dim flag_id As Integer
            Dim timestamp As DateTime
            Dim value As Double

            connection.Open()
            Dim command As Data.SQLite.SQLiteCommand
            Dim datareader As Data.SQLite.SQLiteDataReader

            Select Case Me.ts_class
                Case TimeseriesClassEnum.Unknown
                    Throw New Exception("Time series class is unknown!")

                Case TimeseriesClassEnum.DefaultFlag

                    'Single time series
                    sInfo = Me.TimeSeriesInfos().First()
                    ts = New TimeSeries(sInfo.Name)
                    ts.Unit = Me.unit
                    ts.Interpretation = Me.interpretation
                    ts.Metadata = Me.FileMetadata
                    ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

                    'retrieve time series from db
                    command = New Data.SQLite.SQLiteCommand(Me.connection)
                    command.CommandText = "SELECT rDate, rValue FROM TimeseriesValues ORDER BY rDate;"
                    datareader = command.ExecuteReader()
                    Do While datareader.Read()
                        timestamp = datareader.GetDateTime(0)
                        If datareader.IsDBNull(1) Then
                            value = Double.NaN
                        Else
                            value = datareader.GetDouble(1)
                        End If
                        ts.AddNode(timestamp, value)
                    Loop
                    datareader.Close()

                    'store timeseries
                    Me.TimeSeries.Add(sInfo.Index, ts)

                Case TimeseriesClassEnum.Flagged

                    'Loop over selected series
                    For Each sInfo In Me.SelectedSeries

                        ts = New TimeSeries(sInfo.Name)
                        ts.Unit = Me.unit
                        ts.Interpretation = Me.interpretation
                        ts.Metadata = Me.FileMetadata
                        ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

                        'retrieve time series from db
                        flag_id = Me.flag_mapping(sInfo.Index)
                        command = New Data.SQLite.SQLiteCommand(Me.connection)
                        command.CommandText = "SELECT rDate, rValue FROM TimeseriesValues WHERE attribFlag = @flag ORDER BY rDate;"
                        command.Parameters.Add("@flag", Data.DbType.Int32)
                        command.Parameters("@flag").Value = flag_id
                        command.Prepare()

                        datareader = command.ExecuteReader()
                        Do While datareader.Read()
                            timestamp = datareader.GetDateTime(0)
                            If datareader.IsDBNull(1) Then
                                value = Double.NaN
                            Else
                                value = datareader.GetDouble(1)
                            End If
                            ts.AddNode(timestamp, value)
                        Loop
                        datareader.Close()

                        'store timeseries
                        Me.TimeSeries.Add(sInfo.Index, ts)
                    Next

                Case TimeseriesClassEnum.Forecast

                    Dim T0 As DateTime

                    'Loop over selected series
                    For Each sInfo In Me.SelectedSeries

                        ts = New TimeSeries(sInfo.Name)
                        ts.Unit = Me.unit
                        ts.Interpretation = Me.interpretation
                        ts.Metadata = Me.FileMetadata
                        ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

                        'retrieve time series from db
                        flag_id = Me.flag_T0_mapping(sInfo.Index).flag_id
                        T0 = Me.flag_T0_mapping(sInfo.Index).T0

                        command = New Data.SQLite.SQLiteCommand(Me.connection)
                        command.CommandText = "SELECT T1, rValue FROM TimeseriesValues WHERE (attribFlag = @flag AND rDate = @T0) ORDER BY T1;"
                        command.Parameters.Add("@flag", Data.DbType.Int32)
                        command.Parameters.Add("@T0", Data.DbType.DateTime2)
                        command.Parameters("@flag").Value = flag_id
                        command.Parameters("@T0").Value = T0.ToString(Me.Dateformat) 'string conversion should theoretically not be necessary here
                        command.Prepare()

                        datareader = command.ExecuteReader()
                        Do While datareader.Read()
                            timestamp = datareader.GetDateTime(0)
                            If datareader.IsDBNull(1) Then
                                value = Double.NaN
                            Else
                                value = datareader.GetDouble(1)
                            End If
                            ts.AddNode(timestamp, value)
                        Loop
                        datareader.Close()

                        'store time series
                        Me.TimeSeries.Add(sInfo.Index, ts)

                    Next

                Case Else
                    Throw New Exception($"Time series of class {ts_class} are not yet supported!")

            End Select

            connection.Close()

        End Sub

    End Class

End Namespace