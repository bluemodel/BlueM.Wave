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
''' Module to generate and read test data
''' </summary>
Module TestData

    ''' <summary>
    ''' Returns the absolute path to the test data directory BlueM.Datasets\Wave
    ''' which is expected to be in the same directory as BlueM.Wave
    ''' </summary>
    ''' <returns></returns>
    Friend Function getTestDataDir() As String
        Try
            Dim appdir As String = My.Application.Info.DirectoryPath() 'e.g. BlueM.Wave\tests\bin\x64\Debug
            Dim testdatadir As String = appdir
            For i As Integer = 1 To 5
                testdatadir = IO.Directory.GetParent(testdatadir).FullName
            Next
            testdatadir = IO.Path.Combine(testdatadir, "BlueM.Datasets", "Wave")
            Return testdatadir
        Catch ex As Exception
            Throw New InternalTestFailureException(
                "Unable to locate directory with test data! " &
                "Expected directory: BlueM.Datasets\Wave in the same directory as BlueM.Wave! " &
                ex.Message
            )
        End Try
    End Function

    ''' <summary>
    ''' Returns a time series with a timestep of 15 minutes, 14 nodes, 
    ''' interpretation Undefined, no unit
    ''' </summary>
    ''' <returns></returns>
    Friend Function getTestTimeSeries_15min() As TimeSeries
        Dim ts As New TimeSeries("TestTimeSeries_15min")
        ts.Interpretation = TimeSeries.InterpretationEnum.Undefined
        ts.AddNode(New DateTime(2000, 1, 1, 0, 0, 0), 0.0)
        ts.AddNode(New DateTime(2000, 1, 1, 0, 15, 0), 2.0)
        ts.AddNode(New DateTime(2000, 1, 1, 0, 30, 0), 3.0)
        ts.AddNode(New DateTime(2000, 1, 1, 0, 45, 0), 4.0)
        ts.AddNode(New DateTime(2000, 1, 1, 1, 0, 0), 5.0)
        ts.AddNode(New DateTime(2000, 1, 1, 1, 15, 0), 3.0)
        ts.AddNode(New DateTime(2000, 1, 1, 1, 30, 0), 2.0)
        ts.AddNode(New DateTime(2000, 1, 1, 1, 45, 0), 4.0)
        ts.AddNode(New DateTime(2000, 1, 1, 2, 0, 0), 2.0)
        ts.AddNode(New DateTime(2000, 1, 1, 2, 15, 0), 2.0)
        ts.AddNode(New DateTime(2000, 1, 1, 2, 30, 0), 1.0)
        ts.AddNode(New DateTime(2000, 1, 1, 2, 45, 0), 6.0)
        ts.AddNode(New DateTime(2000, 1, 1, 3, 0, 0), 5.0)
        ts.AddNode(New DateTime(2000, 1, 1, 3, 15, 0), 7.0)
        Return ts
    End Function

    ''' <summary>
    ''' Returns a time series contained in the given file from BlueM.Datasets\Wave\
    ''' </summary>
    ''' <param name="relativePath">Path to the time series file, relative to BlueM.Datasets\Wave\</param>
    ''' <returns></returns>
    Friend Function getTestTimeSeries(relativePath As String) As TimeSeries
        Dim fullPath As String = IO.Path.Combine(getTestDataDir, relativePath)
        If Not IO.File.Exists(fullPath) Then
            Throw New Exception($"File {fullPath} not found!")
        End If
        Dim file As TimeSeriesFile = TimeSeriesFile.getInstance(fullPath)
        ' read the first time series from the file
        Dim ts As TimeSeries = file.getTimeSeries(0)
        Return ts
    End Function

End Module
