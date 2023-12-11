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
    ''' Class for HYBNAT BCS files
    ''' </summary>
    Public Class HYBNAT_BCS
        Inherits TimeSeriesFile

        ''' <summary>
        ''' The unit of the time series
        ''' </summary>
        ''' <remarks> Is fixed for each type of file</remarks>
        Private _unit As String

        ''' <summary>
        ''' Referencedate for the beginning of the simulation
        ''' </summary>
        ''' <remarks>default: 01.01.2000 00:00:00</remarks>
        Public refDate As DateTime

        ''' <summary>
        ''' Set if the import dialog should be used
        ''' </summary>
        ''' <value></value>
        ''' <returns>True</returns>
        ''' <remarks></remarks>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Instanciates a new HYBNAT BCS file
        ''' </summary>
        ''' <param name="file">Path to file</param>
        ''' <remarks></remarks>
        Public Sub New(file As String)

            Call MyBase.New(file)

            'Default settings
            iLineHeadings = 1
            iLineData = 2
            UseUnits = False
            _unit = "m³/s"
            IsColumnSeparated = True
            Separator = Constants.semicolon
            DateTimeColumnIndex = 0
            refDate = New DateTime(2000, 1, 1, 0, 0, 0)

            Call readSeriesInfo()

        End Sub

        ''' <summary>
        ''' Checks if the file is a HYBNAT BCS file
        ''' </summary>
        ''' <param name="file">Path to file</param>
        ''' <returns>Boolean</returns>
        ''' <remarks>Check is based on file extension and line 1 (must start with "time;")</remarks>
        Public Shared Function verifyFormat(file As String) As Boolean
            'Check if file name ends with ".bcs"
            Dim filename As String = Path.GetFileName(file).ToLower()
            If Not filename.EndsWith(".bcs") Then Return False

            'Open file
            Dim stream As New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim reader As New StreamReader(stream, System.Text.Encoding.Default)
            Dim syncReader = TextReader.Synchronized(reader)

            'Check if first line starts with "time;"
            If Not syncReader.ReadLine.ToString.Trim().StartsWith("time;") Then Return False

            'Close file
            syncReader.Close()
            reader.Close()
            stream.Close()

            'File is valid
            Return True
        End Function

        ''' <summary>
        ''' Read number of columns and their names
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readSeriesInfo()
            'Clear series infos
            TimeSeriesInfos.Clear()

            'Open file
            Dim stream As New FileStream(File, FileMode.Open, IO.FileAccess.Read)
            Dim reader As New StreamReader(stream, System.Text.Encoding.Default)
            Dim syncReader = TextReader.Synchronized(reader)

            'Read first line for column names
            Dim columnNames() As String = syncReader.ReadLine.Split(Separator.ToChar)

            'Close file
            syncReader.Close()
            reader.Close()
            stream.Close()

            'Store series info
            For i = 0 To columnNames.Count - 1
                'Overjump time column and empty columns
                If i = DateTimeColumnIndex Then Continue For
                If columnNames(i).Trim() = "" Then Continue For

                'Add series info
                Dim seriesInfo = New TimeSeriesInfo With {
                    .Name = columnNames(i).Trim(),
                    .Unit = _unit,
                    .Index = i + 1
                }
                TimeSeriesInfos.Add(seriesInfo)
            Next
        End Sub

        ''' <summary>
        ''' Reads the time series from the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readFile()
            'Show dialog for setting the reference date
            Dim dlg As New ReferenceDateDialog()
            dlg.ShowDialog()
            refDate = dlg.DateTimePicker_refDate.Value

            'Instantiate time series
            For Each seriesInfo As TimeSeriesInfo In SelectedSeries
                Dim timeSeries = New TimeSeries(seriesInfo.Name) With {
                    .Unit = seriesInfo.Unit,
                    .DataSource = New TimeSeriesDataSource(File, seriesInfo.Name),
                    .Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.Instantaneous
                }
                MyBase.TimeSeries.Add(seriesInfo.Index, timeSeries)
            Next

            'Open file and overjump line with headings
            Dim stream As New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim reader As New StreamReader(stream, Me.Encoding)
            Dim syncReader = TextReader.Synchronized(reader)
            syncReader.ReadLine()

            'Read data until end of file
            Do
                'Get values and datetime (calculate from reference date and seconds)
                Dim values = syncReader.ReadLine.Split(Separator.ToChar)
                Dim datetime = refDate + New TimeSpan(0, 0, Helpers.StringToDouble(values(DateTimeColumnIndex)))

                'Add nodes to time series
                For Each seriesInfo As TimeSeriesInfo In SelectedSeries
                    TimeSeries(seriesInfo.Index).AddNode(datetime, Helpers.StringToDouble(values(seriesInfo.Index - 1)))
                Next
            Loop Until syncReader.Peek() = -1

            'Close file
            syncReader.Close()
            reader.Close()
            stream.Close()
        End Sub

    End Class

End Namespace