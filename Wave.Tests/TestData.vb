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
''' Module to generate and read test data
''' </summary>
Module TestData

    ''' <summary>
    ''' Returns the absolute path to the test data directory BlueM.Datasets\Wave
    ''' which is expected to be in the same directory as BlueM.Wave
    ''' </summary>
    ''' <returns></returns>
    Private Function getTestDataDir() As String
        Try
            Dim appdir As String = My.Application.Info.DirectoryPath() 'e.g. BlueM.Wave\Wave.Tests\bin\x64\Debug
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
    ''' Returns the time series contained in the file BlueM.Datasets\Wave\BIN\abfluss_1.bin
    ''' </summary>
    ''' <returns></returns>
    Friend Function getTestTimeSeries_BIN1() As TimeSeries
        Dim binfile As String = IO.Path.Combine(getTestDataDir, "BIN", "abfluss_1.bin")
        Dim BIN As New Fileformats.BIN(binfile, ReadAllNow:=True)
        Dim ts As TimeSeries = BIN.getTimeSeries(0)
        Return ts
    End Function

End Module
