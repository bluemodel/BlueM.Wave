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
''' Tests for the function TimeSeries.SplitHydroYears
''' </summary>
<TestClass()> Public Class TestSplitHydroYears

    ''' <summary>
    ''' Time series starting on 01.11.
    ''' </summary>
    <TestMethod()> Public Sub TestSplitHydroYears1()

        Dim ts As New TimeSeries("testinput")

        Dim t As New DateTime(2000, 11, 1)
        For i As Integer = 0 To 1000
            ts.AddNode(t, 1.0)
            t = t.AddDays(1)
        Next

        Dim hydroyears As Dictionary(Of Integer, TimeSeries)
        hydroyears = ts.SplitHydroYears()

        Assert.IsTrue(hydroyears.ContainsKey(2000))
        Assert.AreEqual(New DateTime(2000, 11, 1), hydroyears(2000).StartDate)

    End Sub

    ''' <summary>
    ''' Time series starting on 02.11.
    ''' </summary>
    <TestMethod()> Public Sub TestSplitHydroYears2()

        Dim ts As New TimeSeries("testinput")

        Dim t As New DateTime(2000, 11, 2)
        For i As Integer = 0 To 1000
            ts.AddNode(t, 1.0)
            t = t.AddDays(1)
        Next

        Dim hydroyears As Dictionary(Of Integer, TimeSeries)
        hydroyears = ts.SplitHydroYears()

        Assert.IsTrue(hydroyears.ContainsKey(2000))
        Assert.AreEqual(New DateTime(2000, 11, 2), hydroyears(2000).StartDate)

    End Sub

    ''' <summary>
    ''' Time series starting on 31.10. with block right interpretation
    ''' </summary>
    <TestMethod()> Public Sub TestSplitHydroYears3()

        Dim ts As New TimeSeries("testinput")
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight

        Dim t As New DateTime(2000, 10, 31)
        For i As Integer = 0 To 1000
            ts.AddNode(t, 1.0)
            t = t.AddDays(1)
        Next

        Dim hydroyears As Dictionary(Of Integer, TimeSeries)
        hydroyears = ts.SplitHydroYears()

        Assert.IsTrue(hydroyears.ContainsKey(1999))
        Assert.AreEqual(1, hydroyears(1999).Length)
        Assert.IsTrue(hydroyears.ContainsKey(2000))
        Assert.AreEqual(New DateTime(2000, 11, 1), hydroyears(2000).StartDate)

    End Sub
    ''' <summary>
    ''' Time series starting on 31.10. with no interpretation
    ''' </summary>
    <TestMethod()> Public Sub TestSplitHydroYears31()

        Dim ts As New TimeSeries("testinput")

        Dim t As New DateTime(2000, 10, 31)
        For i As Integer = 0 To 1000
            ts.AddNode(t, 1.0)
            t = t.AddDays(1)
        Next

        Dim hydroyears As Dictionary(Of Integer, TimeSeries)
        hydroyears = ts.SplitHydroYears()

        Assert.IsTrue(hydroyears.ContainsKey(1999))
        Assert.AreEqual(2, hydroyears(1999).Length)
        Assert.IsTrue(hydroyears.ContainsKey(2000))
        Assert.AreEqual(New DateTime(2000, 11, 1), hydroyears(2000).StartDate)

    End Sub

    ''' <summary>
    ''' Time series with hourly data
    ''' </summary>
    <TestMethod()> Public Sub TestSplitHydroYears4()

        Dim ts As New TimeSeries("testinput")
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight

        Dim t As New DateTime(2000, 10, 31)
        For i As Integer = 0 To 1000 * 24
            ts.AddNode(t, 1.0)
            t = t.AddHours(1)
        Next

        Dim hydroyears As Dictionary(Of Integer, TimeSeries)
        hydroyears = ts.SplitHydroYears()

        Assert.IsTrue(hydroyears.ContainsKey(1999))
        Assert.AreEqual(ts.StartDate, hydroyears(1999).StartDate)
        Assert.AreEqual(New DateTime(2000, 10, 31, 23, 0, 0), hydroyears(1999).EndDate)
        Assert.IsTrue(hydroyears.ContainsKey(2000))
        Assert.AreEqual(New DateTime(2000, 11, 1), hydroyears(2000).StartDate)

    End Sub

    ''' <summary>
    ''' Time series without node at hydro year split
    ''' </summary>
    <TestMethod()> Public Sub TestSplitHydroYears5()

        Dim ts As New TimeSeries("testinput")
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight

        Dim t As New DateTime(2000, 10, 31, 12, 0, 0)
        For i As Integer = 0 To 1000
            ts.AddNode(t, 1.0)
            t = t.AddDays(1)
        Next

        Dim hydroyears As Dictionary(Of Integer, TimeSeries)
        hydroyears = ts.SplitHydroYears()

        Assert.IsTrue(hydroyears.ContainsKey(1999))
        Assert.AreEqual(ts.StartDate, hydroyears(1999).StartDate)
        Assert.AreEqual(New DateTime(2000, 10, 31, 12, 0, 0), hydroyears(1999).EndDate)
        Assert.IsTrue(hydroyears.ContainsKey(2000))
        Assert.AreEqual(New DateTime(2000, 10, 31, 12, 0, 0), hydroyears(2000).StartDate) ' start date equals end date of previous hydro year
        Assert.AreEqual(New DateTime(2001, 10, 31, 12, 0, 0), hydroyears(2000).EndDate)

    End Sub

End Class