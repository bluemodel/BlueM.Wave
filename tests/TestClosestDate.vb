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
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System

''' <summary>
''' Tests for the function TimeSeries.ClosestDate
''' </summary>
<TestClass>
Public Class TestClosestDate

    Private Function CreateSampleTimeSeries() As TimeSeries
        Dim ts As New TimeSeries("Test Series")
        ts.AddNode(New DateTime(2020, 1, 1, 0, 0, 0), 1.0)
        ts.AddNode(New DateTime(2020, 1, 2, 0, 0, 0), 2.0)
        ts.AddNode(New DateTime(2020, 1, 3, 0, 0, 0), 3.0)
        ts.AddNode(New DateTime(2020, 1, 4, 0, 0, 0), 4.0)
        Return ts
    End Function

    <TestMethod>
    Public Sub ClosestDate_ExactMatch_ReturnsSameDate()
        Dim ts = CreateSampleTimeSeries()
        Dim target As New DateTime(2020, 1, 2, 0, 0, 0)
        Dim result = ts.ClosestDate(target)
        Assert.AreEqual(target, result)
    End Sub

    <TestMethod>
    Public Sub ClosestDate_BeforeDirection_ReturnsPreviousDate()
        Dim ts = CreateSampleTimeSeries()
        Dim target As New DateTime(2020, 1, 2, 12, 0, 0)
        Dim result = ts.ClosestDate(target, TimeDirection.Before)
        Assert.AreEqual(New DateTime(2020, 1, 2, 0, 0, 0), result)
    End Sub

    <TestMethod>
    Public Sub ClosestDate_AfterDirection_ReturnsNextDate()
        Dim ts = CreateSampleTimeSeries()
        Dim target As New DateTime(2020, 1, 2, 12, 0, 0)
        Dim result = ts.ClosestDate(target, TimeDirection.After)
        Assert.AreEqual(New DateTime(2020, 1, 3, 0, 0, 0), result)
    End Sub

    <TestMethod>
    Public Sub ClosestDate_BeforeFirstDate_ReturnsFirstDate()
        Dim ts = CreateSampleTimeSeries()
        Dim target As New DateTime(2019, 12, 31, 0, 0, 0)
        Dim result = ts.ClosestDate(target, TimeDirection.Before)
        Assert.AreEqual(New DateTime(2020, 1, 1, 0, 0, 0), result)
    End Sub

    <TestMethod>
    Public Sub ClosestDate_AfterLastDate_ReturnsLastDate()
        Dim ts = CreateSampleTimeSeries()
        Dim target As New DateTime(2020, 1, 5, 0, 0, 0)
        Dim result = ts.ClosestDate(target, TimeDirection.After)
        Assert.AreEqual(New DateTime(2020, 1, 4, 0, 0, 0), result)
    End Sub

End Class