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
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

''' <summary>
''' Tests for the analysis function ChangeTimestep
''' </summary>
<TestClass()> Public Class TestChangeTimestep

    ''' <summary>
    ''' Change timestep from 15min to 1h, interpretation BlockLeft
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_1h_BlockLeft()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockLeft

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Hour, 1, ts.StartDate)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockLeft, ts_new.Interpretation)
        Assert.AreEqual(3, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 1, 0, 0), ts_new.StartDate)
        Assert.AreEqual(3.5, ts_new.Nodes(New DateTime(2000, 1, 1, 1, 0, 0)))
        Assert.AreEqual(2.75, ts_new.Nodes(New DateTime(2000, 1, 1, 2, 0, 0)))
        Assert.AreEqual(3.5, ts_new.Nodes(New DateTime(2000, 1, 1, 3, 0, 0)))
        Assert.AreEqual(New DateTime(2000, 1, 1, 3, 0, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 1h, input interpretation BlockLeft, output interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_1h_BlockLeft_to_BlockRight()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockLeft

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Hour, 1, ts.StartDate, outputInterpretation:=TimeSeries.InterpretationEnum.BlockRight)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockRight, ts_new.Interpretation)
        Assert.AreEqual(3, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 0, 0, 0), ts_new.StartDate)
        Assert.AreEqual(3.5, ts_new.Nodes(New DateTime(2000, 1, 1, 0, 0, 0)))
        Assert.AreEqual(2.75, ts_new.Nodes(New DateTime(2000, 1, 1, 1, 0, 0)))
        Assert.AreEqual(3.5, ts_new.Nodes(New DateTime(2000, 1, 1, 2, 0, 0)))
        Assert.AreEqual(New DateTime(2000, 1, 1, 2, 0, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 1h, interpretation CumulativePerTimestep
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_1h_CumulativePerTimestep()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.CumulativePerTimestep

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Hour, 1, ts.StartDate)

        Assert.AreEqual(TimeSeries.InterpretationEnum.CumulativePerTimestep, ts_new.Interpretation)
        Assert.AreEqual(3, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 1, 0, 0), ts_new.StartDate)
        Assert.AreEqual(14.0, ts_new.Nodes(New DateTime(2000, 1, 1, 1, 0, 0)))
        Assert.AreEqual(11.0, ts_new.Nodes(New DateTime(2000, 1, 1, 2, 0, 0)))
        Assert.AreEqual(14.0, ts_new.Nodes(New DateTime(2000, 1, 1, 3, 0, 0)))
        Assert.AreEqual(New DateTime(2000, 1, 1, 3, 0, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 1h, interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_1h_BlockRight()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Hour, 1, ts.StartDate)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockRight, ts_new.Interpretation)
        Assert.AreEqual(3, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 0, 0, 0), ts_new.StartDate)
        Assert.AreEqual(2.25, ts_new.Nodes(New DateTime(2000, 1, 1, 0, 0, 0)))
        Assert.AreEqual(3.5, ts_new.Nodes(New DateTime(2000, 1, 1, 1, 0, 0)))
        Assert.AreEqual(2.75, ts_new.Nodes(New DateTime(2000, 1, 1, 2, 0, 0)))
        Assert.AreEqual(New DateTime(2000, 1, 1, 2, 0, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 1h, input interpretation Instantaneous, output interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_1h_Instantaneous()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.Instantaneous

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Hour, 1, ts.StartDate, outputInterpretation:=TimeSeries.InterpretationEnum.BlockRight)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockRight, ts_new.Interpretation)
        Assert.AreEqual(3, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 0, 0, 0), ts_new.StartDate)
        Assert.AreEqual(2.875, ts_new.Nodes(New DateTime(2000, 1, 1, 0, 0, 0)))
        Assert.AreEqual(3.125, ts_new.Nodes(New DateTime(2000, 1, 1, 1, 0, 0)))
        Assert.AreEqual(3.125, ts_new.Nodes(New DateTime(2000, 1, 1, 2, 0, 0)))
        Assert.AreEqual(New DateTime(2000, 1, 1, 2, 0, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 10min, interpretation BlockLeft
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_10min_BlockLeft()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockLeft

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Minute, 10, ts.StartDate)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockLeft, ts_new.Interpretation)
        Assert.AreEqual(19, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 0, 10, 0), ts_new.StartDate)
        Dim expected_values As New List(Of Double) From {
            2.0,
            2.5,
            3.0,
            4.0,
            4.5,
            5.0,
            3.0,
            2.5,
            2.0,
            4.0,
            3.0,
            2.0,
            2.0,
            1.5,
            1.0,
            6.0,
            5.5,
            5.0,
            7.0
        }
        For i As Integer = 0 To expected_values.Count - 1
            Assert.AreEqual(expected_values(i), ts_new.Values(i))
        Next
        Assert.AreEqual(New DateTime(2000, 1, 1, 3, 10, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 10min, interpretation CumulativePerTimestep
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_10min_CumulativePerTimestep()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.CumulativePerTimestep

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Minute, 10, ts.StartDate)

        Assert.AreEqual(TimeSeries.InterpretationEnum.CumulativePerTimestep, ts_new.Interpretation)
        Assert.AreEqual(19, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 0, 10, 0), ts_new.StartDate)
        Dim expected_values As New List(Of Double) From {
            1.333,
            1.667,
            2.0,
            2.667,
            3.0,
            3.333,
            2.0,
            1.667,
            1.333,
            2.667,
            2.0,
            1.333,
            1.333,
            1.0,
            0.667,
            4.0,
            3.667,
            3.333,
            4.667
        }
        For i As Integer = 0 To expected_values.Count - 1
            Assert.AreEqual(expected_values(i), ts_new.Values(i), 0.001)
        Next
        Assert.AreEqual(New DateTime(2000, 1, 1, 3, 10, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 10min, interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_10min_BlockRight()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Minute, 10, ts.StartDate)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockRight, ts_new.Interpretation)
        Assert.AreEqual(19, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 0, 0, 0), ts_new.StartDate)
        Dim expected_values As New List(Of Double) From {
            0.0,
            1.0,
            2.0,
            3.0,
            3.5,
            4.0,
            5.0,
            4.0,
            3.0,
            2.0,
            3.0,
            4.0,
            2.0,
            2.0,
            2.0,
            1.0,
            3.5,
            6.0,
            5.0
        }
        For i As Integer = 0 To expected_values.Count - 1
            Assert.AreEqual(expected_values(i), ts_new.Values(i))
        Next
        Assert.AreEqual(New DateTime(2000, 1, 1, 3, 0, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 10min, input interpretation Instantaneous, output interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_10min_Instantaneous()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries_15min()
        ts.Interpretation = TimeSeries.InterpretationEnum.Instantaneous

        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Minute, 10, ts.StartDate, outputInterpretation:=TimeSeries.InterpretationEnum.BlockRight)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockRight, ts_new.Interpretation)
        Assert.AreEqual(19, ts_new.Length)
        Assert.AreEqual(New DateTime(2000, 1, 1, 0, 0, 0), ts_new.StartDate)
        Dim expected_values As New List(Of Double) From {
            0.667,
            1.917,
            2.667,
            3.333,
            4.0,
            4.667,
            4.333,
            3.083,
            2.333,
            2.667,
            3.667,
            2.667,
            2.0,
            1.917,
            1.333,
            2.667,
            5.5,
            5.333,
            5.667
        }
        For i As Integer = 0 To expected_values.Count - 1
            Assert.AreEqual(expected_values(i), ts_new.Values(i), 0.001)
        Next
        Assert.AreEqual(New DateTime(2000, 1, 1, 3, 0, 0), ts_new.EndDate)

    End Sub

    ''' <summary>
    ''' Change timestep from 15min to 1 month, interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestChangeTimestep_15min_1month_BlockRight()

        Dim ts, ts_new As TimeSeries

        ts = getTestTimeSeries("BIN\abfluss_1.bin")
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight

        Dim startdate As DateTime = New DateTime(ts.StartDate.Year, ts.StartDate.Month, 1)
        ts_new = ts.ChangeTimestep(TimeSeries.TimeStepTypeEnum.Month, 1, startdate)

        Assert.AreEqual(TimeSeries.InterpretationEnum.BlockRight, ts_new.Interpretation)
        Assert.AreEqual(50, ts_new.Length)
        Assert.AreEqual(New DateTime(2012, 3, 1), ts_new.StartDate)
        Assert.AreEqual(New DateTime(2016, 4, 1), ts_new.EndDate)
        Assert.AreEqual(0.735, ts_new.FirstValue, 0.001)
        Assert.AreEqual(1.234, ts_new.LastValue, 0.001)
        Assert.AreEqual(0.083, ts_new.Minimum, 0.001)
        Assert.AreEqual(3.458, ts_new.Maximum, 0.001)
        Assert.AreEqual(41.908, ts_new.Sum, 0.001)
        Assert.AreEqual(0.975, ts_new.Average, 0.001)
        Assert.AreEqual(7, ts_new.NaNCount)

    End Sub

End Class