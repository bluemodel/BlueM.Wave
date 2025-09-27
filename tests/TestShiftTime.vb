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
''' Tests for TimeSeries.AddTimeInterval and TimeSeries.ShiftTime
''' </summary>
<TestClass()> Public Class TestShiftTime

    ''' <summary>
    ''' Test AddTimeInterval for all TimeStepTypeEnum values
    ''' </summary>
    <TestMethod()> Public Sub TestAddTimeInterval_AllTypes()
        Dim baseDate As DateTime = New DateTime(2020, 1, 1, 0, 0, 0)
        Assert.AreEqual(New DateTime(2020, 1, 1, 0, 0, 1), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Second, 1))
        Assert.AreEqual(New DateTime(2020, 1, 1, 0, 1, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Minute, 1))
        Assert.AreEqual(New DateTime(2020, 1, 1, 1, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Hour, 1))
        Assert.AreEqual(New DateTime(2020, 1, 2, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Day, 1))
        Assert.AreEqual(New DateTime(2020, 1, 8, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Week, 1))
        Assert.AreEqual(New DateTime(2020, 2, 1, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Month, 1))
        Assert.AreEqual(New DateTime(2021, 1, 1, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Year, 1))
    End Sub

    ''' <summary>
    ''' Test AddTimeInterval with negative intervals
    ''' </summary>
    <TestMethod()> Public Sub TestAddTimeInterval_Negative()
        Dim baseDate As DateTime = New DateTime(2020, 1, 15, 12, 0, 0)
        Assert.AreEqual(New DateTime(2020, 1, 15, 11, 59, 59), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Second, -1))
        Assert.AreEqual(New DateTime(2020, 1, 15, 11, 59, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Minute, -1))
        Assert.AreEqual(New DateTime(2020, 1, 15, 11, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Hour, -1))
        Assert.AreEqual(New DateTime(2020, 1, 14, 12, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Day, -1))
        Assert.AreEqual(New DateTime(2020, 1, 8, 12, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Week, -1))
        Assert.AreEqual(New DateTime(2019, 12, 15, 12, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Month, -1))
        Assert.AreEqual(New DateTime(2019, 1, 15, 12, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Year, -1))
    End Sub

    ''' <summary>
    ''' Test AddTimeInterval handling of varying month lengths
    ''' </summary>
    <TestMethod()> Public Sub TestAddTimeInterval_Months()
        Dim baseDate As DateTime = New DateTime(2020, 1, 31, 0, 0, 0)
        Assert.AreEqual(New DateTime(2020, 2, 29, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Month, 1))
        Assert.AreEqual(New DateTime(2020, 3, 31, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Month, 2))
        Assert.AreEqual(New DateTime(2020, 4, 30, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Month, 3))
    End Sub

    ''' <summary>
    ''' Test AddTimeInterval handling of leap days
    ''' </summary>
    <TestMethod()> Public Sub TestAddTimeInterval_LeapYears()
        Dim baseDate As DateTime = New DateTime(2020, 2, 29, 0, 0, 0)
        Assert.AreEqual(New DateTime(2021, 2, 28, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Year, 1))
        Assert.AreEqual(New DateTime(2024, 2, 29, 0, 0, 0), TimeSeries.AddTimeInterval(baseDate, TimeSeries.TimeStepTypeEnum.Year, 4))
    End Sub

    ''' <summary>
    ''' Test ShiftTime with positive interval
    ''' </summary>
    <TestMethod()> Public Sub TestShiftTime_PositiveInterval()
        Dim ts As New TimeSeries("TestSeries")
        ts.AddNode(New DateTime(2020, 1, 1), 1.0)
        ts.AddNode(New DateTime(2020, 1, 2), 2.0)
        ts.AddNode(New DateTime(2020, 1, 3), 3.0)
        ts.AddNode(New DateTime(2020, 1, 31), 31.0)
        ts.AddNode(New DateTime(2020, 12, 31), 99.0)

        Dim shifted As TimeSeries = ts.ShiftTime(1, TimeSeries.TimeStepTypeEnum.Day)
        Assert.AreEqual(5, shifted.Length)
        Assert.AreEqual(1.0, shifted.Nodes(New DateTime(2020, 1, 2)))
        Assert.AreEqual(2.0, shifted.Nodes(New DateTime(2020, 1, 3)))
        Assert.AreEqual(3.0, shifted.Nodes(New DateTime(2020, 1, 4)))
        Assert.AreEqual(31.0, shifted.Nodes(New DateTime(2020, 2, 1)))
        Assert.AreEqual(99.0, shifted.Nodes(New DateTime(2021, 1, 1)))
    End Sub

    ''' <summary>
    ''' Test ShiftTime with negative interval
    ''' </summary>
    <TestMethod()> Public Sub TestShiftTime_NegativeInterval()
        Dim ts As New TimeSeries("TestSeries")
        ts.AddNode(New DateTime(2020, 1, 1), 1.0)
        ts.AddNode(New DateTime(2020, 5, 1), 5.0)
        ts.AddNode(New DateTime(2020, 5, 2), 5.2)
        ts.AddNode(New DateTime(2020, 6, 1), 6.0)
        ts.AddNode(New DateTime(2020, 7, 1), 7.0)

        Dim shifted As TimeSeries = ts.ShiftTime(-1, TimeSeries.TimeStepTypeEnum.Day)
        Assert.AreEqual(5, shifted.Length)
        Assert.AreEqual(1.0, shifted.Nodes(New DateTime(2019, 12, 31)))
        Assert.AreEqual(5.0, shifted.Nodes(New DateTime(2020, 4, 30)))
        Assert.AreEqual(5.2, shifted.Nodes(New DateTime(2020, 5, 1)))
        Assert.AreEqual(6.0, shifted.Nodes(New DateTime(2020, 5, 31)))
        Assert.AreEqual(7.0, shifted.Nodes(New DateTime(2020, 6, 30)))
    End Sub

    ''' <summary>
    ''' Test ShiftTime with a leap day in the time series
    ''' </summary>
    <TestMethod()> Public Sub TestShiftTime_LeapDayHandling()
        ' Arrange
        Dim ts As New TimeSeries("LeapDayTest")
        ts.AddNode(New DateTime(2020, 2, 29), 100.0) ' 2020 is a leap year
        ts.AddNode(New DateTime(2020, 3, 1), 200.0)

        ' Act
        Dim shifted1 As TimeSeries = ts.ShiftTime(1, TimeSeries.TimeStepTypeEnum.Year) ' Shift by 1 year (2021 is not a leap year)

        ' Assert
        Assert.AreEqual(1, shifted1.Length, "Leap day should be missing after shift of 1 year.")
        Assert.AreEqual(200.0, shifted1.Nodes(New DateTime(2021, 3, 1)))

        ' Act
        Dim shifted4 As TimeSeries = ts.ShiftTime(4, TimeSeries.TimeStepTypeEnum.Year) ' Shift by 4 years (2024 is a leap year)

        ' Assert
        Assert.AreEqual(2, shifted4.Length, "Leap day should be preserved after shift of 4 years.")
        Assert.AreEqual(100.0, shifted4.Nodes(New DateTime(2024, 2, 29)))
        Assert.AreEqual(200.0, shifted4.Nodes(New DateTime(2024, 3, 1)))
    End Sub

    ''' <summary>
    ''' Test ShiftTime with an interval of months that would cause duplicate keys
    ''' </summary>
    <TestMethod()> Public Sub TestShiftTime_Months()
        Dim ts As New TimeSeries("TestSeries")
        ts.AddNode(New DateTime(2021, 1, 28), 28.0)
        ts.AddNode(New DateTime(2021, 1, 29), 29.0)
        ts.AddNode(New DateTime(2021, 1, 30), 30.0)
        ts.AddNode(New DateTime(2021, 1, 31), 31.0)

        Dim shifted As TimeSeries = ts.ShiftTime(1, TimeSeries.TimeStepTypeEnum.Month)

        'February 29, 30 and 31 should have been skipped because those dates do not exist
        Assert.AreEqual(1, shifted.Length)
        Assert.AreEqual(28.0, shifted.Nodes(New DateTime(2021, 2, 28)))

    End Sub

End Class