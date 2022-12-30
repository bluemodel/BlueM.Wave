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
    ''' Class for the SYDRO binary WEL format
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WBL
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
        ''' </summary>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Possible data types of values
        ''' </summary>
        Private Enum DataTypes As Integer
            [Integer] = 1
            [Single] = 2
            [Double] = 3
            [Boolean] = 4
        End Enum

        ''' <summary>
        ''' Lengths in bytes for different data types
        ''' </summary>
        Private Shared ReadOnly DataTypeLengths As New Dictionary(Of DataTypes, Integer) From {
        {DataTypes.Integer, 4},
        {DataTypes.Single, 4},
        {DataTypes.Double, 8},
        {DataTypes.Boolean, 1}
    }

        ''' <summary>
        ''' The data type of values in this file instance
        ''' </summary>
        Private DataType As DataTypes

        ''' <summary>
        ''' The length in bytes of the values in this file instance
        ''' </summary>
        ''' <returns></returns>
        Private ReadOnly Property DataTypeLength As Integer
            Get
                Return WBL.DataTypeLengths(Me.DataType)
            End Get
        End Property

        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

            MyBase.New(FileName)

            'Voreinstellungen
            Me.Dateformat = Helpers.CurrentDateFormat 'irrelevant because binary
            Me.iLineData = 0
            Me.UseUnits = True

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'Direkt einlesen
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Reads series info and datatype from accompanying *.WELINFO file
        ''' </summary>
        Public Overrides Sub readSeriesInfo()

            Me.TimeSeriesInfos.Clear()

            'find a *.WELINFO file with the same name in the same directory
            Dim file_welinfo As String = IO.Path.Combine(IO.Path.GetDirectoryName(Me.File), IO.Path.GetFileNameWithoutExtension(Me.File) & ".WELINFO")
            If Not IO.File.Exists(file_welinfo) Then
                Throw New Exception($"Required metadata file {IO.Path.GetFileName(file_welinfo)} not found!")
            End If

            Log.AddLogEntry(levels.info, $"Reading metadata from file {IO.Path.GetFileName(file_welinfo)}...")

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(file_welinfo, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Dim line As String
            Dim isData As Boolean = False

            Do
                line = StrReadSync.ReadLine.ToString

                If Not isData And line.StartsWith("Datentyp=") Then
                    Me.DataType = Integer.Parse(line.Split("=").Last.Trim)
                    Continue Do
                End If

                If line.StartsWith("[Elemente]") Then
                    isData = True
                    Continue Do
                End If

                If isData Then
                    Dim sInfo As New TimeSeriesInfo()
                    sInfo.Name = line.Split(";")(0).Trim()
                    sInfo.Unit = line.Split(";")(3).Trim()
                    sInfo.Index = Integer.Parse(line.Split(";")(4).Trim())
                    Me.TimeSeriesInfos.Add(sInfo)
                End If
            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If Me.TimeSeriesInfos.Count = 0 Then
                Throw New Exception($"Unable to read series info from metadata file {IO.Path.GetFileName(file_welinfo)}!")
            End If

            If Me.DataType = 0 Then
                Log.AddLogEntry(levels.warning, $"Unable to determine data type from metadata file {IO.Path.GetFileName(file_welinfo)}. Assuming Single.")
                Me.DataType = DataTypes.Single 'set default data type to Single
            Else
                Log.AddLogEntry(levels.debug, $"Data type read from metadata file: {Me.DataType}")
            End If

            Log.AddLogEntry(levels.debug, $"Number of series read from metadata file: {Me.TimeSeriesInfos.Count}")

        End Sub

        ''' <summary>
        ''' Reads the file
        ''' </summary>
        Public Overrides Sub readFile()

            Dim rdate As Double
            Dim timestamp As DateTime
            Dim value As Single
            Dim errorcount As Integer

            'instantiate time series
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                Dim ts As New TimeSeries(sInfo.Name)
                ts.Unit = sInfo.Unit
                ts.DataSource = New TimeSeriesDataSource(Me.File, sInfo.Name)
                ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight
                Me.TimeSeries.Add(sInfo.Index, ts)
            Next

            'get a sorted list of selected indices
            Dim selectedIndices As New List(Of Integer)
            For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                selectedIndices.Add(sInfo.Index)
            Next
            selectedIndices.Sort()

            Using reader As New IO.BinaryReader(IO.File.OpenRead(File), Text.ASCIIEncoding.ASCII)
                'skip header (same length as a single record consisting of 8 bytes date and x bytes for each value depending on the data type
                reader.ReadBytes(8 + Me.DataTypeLength * Me.TimeSeriesInfos.Count)
                'read values
                errorcount = 0
                Do
                    'read date
                    rdate = reader.ReadDouble()
                    'convert real date to DateTime
                    timestamp = BIN.rDateToDate(rdate)

                    'read values of selected indices
                    Dim position As Integer = 0
                    For Each index As Integer In selectedIndices

                        'skip ahead to current index
                        reader.ReadBytes(Me.DataTypeLength * (index - position))

                        'read value depending on data type
                        Select Case Me.DataType
                            Case DataTypes.Integer
                                value = Convert.ToDouble(reader.ReadInt32())
                            Case DataTypes.Single
                                value = Convert.ToDouble(reader.ReadSingle())
                            Case DataTypes.Double
                                value = reader.ReadDouble()
                            Case DataTypes.Boolean
                                value = Convert.ToDouble(reader.ReadBoolean())
                        End Select

                        'convert error values to NaN
                        If Math.Abs(value - BIN.ErrorValue) < 0.0001 Then
                            value = Double.NaN
                            errorcount += 1
                        End If

                        'add node to time series
                        Me.TimeSeries(index).AddNode(timestamp, value)

                        'update position
                        position = index + 1
                    Next

                    'skip ahead to next timestamp
                    reader.ReadBytes(Me.DataTypeLength * (Me.TimeSeriesInfos.Count - position))

                Loop Until reader.PeekChar < 0
            End Using

            'Log 
            Call Log.AddLogEntry(Log.levels.info, $"Read {Me.TimeSeries.Count} time series with {Me.TimeSeries.First.Value.Length} nodes each.")
            If errorcount > 0 Then
                Log.AddLogEntry(Log.levels.warning, $"The file contained {errorcount} error values ({BIN.ErrorValue}), which were converted to NaN!")
            End If

        End Sub

        ''' <summary>
        ''' Checks whether a file is a SYDRO binary file
        ''' </summary>
        ''' <param name="file">path to the file to check</param>
        ''' <returns>True if verification was successful</returns>
        ''' <remarks>Adapted from Fortran routine FILE_GETRECL (formerly ZRE_GETRECL)</remarks>
        Public Shared Function verifyFormat(file As String)

            Const unformattedbits As Byte = &HF4
            Const formattedbits As Byte = &HF8

            Dim firstByte As Byte

            Using reader As New IO.BinaryReader(IO.File.OpenRead(file), Text.ASCIIEncoding.ASCII)
                'read first byte
                firstByte = reader.ReadByte()

                If ((firstByte And unformattedbits) <> unformattedbits And
                (firstByte And formattedbits) <> formattedbits) Then
                    Return False
                Else
                    Return True
                End If
            End Using

        End Function

    End Class

End Namespace