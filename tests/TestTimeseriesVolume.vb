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
''' Tests for calculating the volume of time series
''' </summary>
<TestClass()> Public Class TestTimeseriesVolume

    ''' <summary>
    ''' Volume for time series with unit m³/s, interpretation Instantaneous
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_1()

        Dim ts As New TimeSeries()
        ts.Unit = "m³/s"
        ts.Interpretation = TimeSeries.InterpretationEnum.Instantaneous
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        Assert.AreEqual(561_600.0, ts.Volume)

    End Sub

    ''' <summary>
    ''' Volume for time series with unit m³/s, interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_2()

        Dim ts As New TimeSeries()
        ts.Unit = "m³/s"
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        Assert.AreEqual(518_400.0, ts.Volume)

    End Sub

    ''' <summary>
    ''' Volume for time series with unit m³/s, interpretation BlockLeft
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_3()

        Dim ts As New TimeSeries()
        ts.Unit = "m³/s"
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockLeft
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        Assert.AreEqual(604_800.0, ts.Volume)

    End Sub

    ''' <summary>
    ''' Volume for time series with unit mm, interpretation CumulativePerTimestep
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_4()

        Dim ts As New TimeSeries()
        ts.Unit = "mm"
        ts.Interpretation = TimeSeries.InterpretationEnum.CumulativePerTimestep
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        Assert.AreEqual(8.0, ts.Volume)

    End Sub

    ''' <summary>
    ''' Volume for time series with unit mm/h, interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_5()

        Dim ts As New TimeSeries()
        ts.Unit = "mm/h"
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        Assert.AreEqual(144.0, ts.Volume)

    End Sub

End Class