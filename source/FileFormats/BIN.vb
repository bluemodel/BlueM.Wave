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
    ''' Klasse für das SYDRO Binärformat
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BIN
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Error value
        ''' </summary>
        Public Const ErrorValue As Double = -9999.999

        ''' <summary>
        ''' Reference date for real dates
        ''' </summary>
        Private Shared ReadOnly refdate As DateTime = New DateTime(1601, 1, 1)

        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return False
            End Get
        End Property

#Region "Methoden"

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.Dateformat = Helpers.CurrentDateFormat 'irrelevant weil binär
            Me.iLineData = 0
            Me.UseUnits = False

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Reads series info
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Dim sInfo As New TimeSeriesInfo()

            Me.TimeSeriesInfos.Clear()

            sInfo.Name = IO.Path.GetFileName(Me.File)
            sInfo.Unit = "-"
            sInfo.Index = 0
            Me.TimeSeriesInfos.Add(sInfo)

        End Sub


        ''' <summary>
        ''' Reads the file
        ''' </summary>
        Public Overrides Sub readFile()
            Dim rdate As Double
            Dim timestamp As DateTime
            Dim value As Single
            Dim errorcount As Integer
            Dim sInfo As TimeSeriesInfo
            Dim ts As TimeSeries

            'Zeitreihe instanzieren (nur eine)
            sInfo = Me.TimeSeriesInfos(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit
            ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)

            Using reader As New IO.BinaryReader(IO.File.OpenRead(File), Text.ASCIIEncoding.ASCII)
                'skip header
                reader.ReadBytes(12)
                'read values
                errorcount = 0
                Do
                    rdate = reader.ReadDouble()
                    value = reader.ReadSingle()

                    'convert real date to DateTime
                    timestamp = rDateToDate(rdate)
                    'convert error values to NaN
                    If Math.Abs(value - BIN.ErrorValue) < 0.0001 Then
                        value = Double.NaN
                        errorcount += 1
                    End If

                    ts.AddNode(timestamp, value)
                Loop Until reader.PeekChar < 0
            End Using

            'store time series
            Me.TimeSeries.Add(sInfo.Index, ts)

            'Log 
            Call Log.AddLogEntry(Log.levels.info, $"Read {ts.Length} nodes.")
            If errorcount > 0 Then
                Log.AddLogEntry(Log.levels.warning, $"The file contained {errorcount} error values ({BIN.ErrorValue}), which were converted to NaN!")
            End If

        End Sub

        ''' <summary>
        ''' Converts a real (double) date value to a DateTime object
        ''' </summary>
        ''' <param name="rDate">the real date to convert</param>
        ''' <returns>DateTime object</returns>
        ''' <remarks>
        ''' Assumes the real date is number of hours since 01.01.1601
        ''' Rounds to the nearest second
        ''' </remarks>
        Friend Shared Function rDateToDate(rDate As Double) As DateTime
            Dim timestamp As DateTime
            Dim hours, minutes, seconds As Integer
            hours = Math.Floor(rDate)
            minutes = Math.Floor((rDate - hours) * 60.0)
            seconds = Math.Round((rDate - hours - (minutes / 60.0)) * 3600.0, 0)
            timestamp = refdate + New TimeSpan(days:=0, hours, minutes, seconds, milliseconds:=0)
            Return timestamp
        End Function

        ''' <summary>
        ''' Convert a DateTime object to a real (double) date value
        ''' </summary>
        ''' <param name="timestamp"></param>
        ''' <returns>real (double) date</returns>
        ''' <remarks>real (double date) is hours since 01.01.1601</remarks>
        Private Shared Function dateToRDate(timestamp As DateTime) As Double
            Dim rDate As Double
            rDate = (timestamp - refdate).TotalHours
            Return rDate
        End Function

        ''' <summary>
        ''' Write a time series to a BIN file
        ''' </summary>
        ''' <param name="ts">the timeseries to write</param>
        ''' <param name="file">path to the file</param>
        Public Shared Sub Write_File(ByRef ts As TimeSeries, file As String)

            Dim header() As Int32
            Dim rdate As Double
            Dim value As Single

            Using writer As New IO.BinaryWriter(IO.File.Create(file))

                'write header
                ReDim header(2)
                header = {3319, 0, 0}
                For Each entry As Int32 In header
                    writer.Write(entry)
                Next

                'write values
                For Each node As KeyValuePair(Of DateTime, Double) In ts.Nodes
                    'convert DateTime to rDate
                    rdate = dateToRDate(node.Key)
                    'convert error values
                    If Double.IsNaN(node.Value) Then
                        value = BIN.ErrorValue
                    Else
                        value = node.Value
                    End If
                    writer.Write(rdate)
                    writer.Write(value)
                Next
            End Using

        End Sub

#End Region 'Methoden

    End Class

End Namespace