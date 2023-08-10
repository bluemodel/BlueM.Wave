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
    <TestMethod()> Public Sub TestVolume_m3s_instantaneous()

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
    <TestMethod()> Public Sub TestVolume_m3s_blockright()

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
    <TestMethod()> Public Sub TestVolume_m3s_blockleft()

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
    ''' Volume for time series with unit mm/h, interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_mmh_blockright()

        Dim ts As New TimeSeries()
        ts.Unit = "mm/h"
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        Assert.AreEqual(144.0, ts.Volume)

    End Sub

    ''' <summary>
    ''' Volume for time series with unit mm/d, interpretation BlockRight
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_mmd_blockright()

        Dim ts As New TimeSeries()
        ts.Unit = "mm/d"
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        Assert.AreEqual(6.0, ts.Volume)

    End Sub

    ''' <summary>
    ''' Volume for time series with unsupported units
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_unsupported_units()

        Dim units As New List(Of String) From
            {
            "mm/m",
            "m",
            "mm",
            "-",
            "mm/y",
            "mm/month"
            }

        Dim ts As New TimeSeries()
        ts.Interpretation = TimeSeries.InterpretationEnum.BlockRight
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        For Each unit As String In units
            ts.Unit = unit
            Assert.AreEqual(Double.NaN, ts.Volume)
        Next
    End Sub

    ''' <summary>
    ''' Volume for time series with unsupported interpretations
    ''' </summary>
    <TestMethod()> Public Sub TestVolume_unsupported_interpretations()

        Dim interpretations As New List(Of TimeSeries.InterpretationEnum) From
            {
                TimeSeries.InterpretationEnum.Undefined,
                TimeSeries.InterpretationEnum.Cumulative,
                TimeSeries.InterpretationEnum.CumulativePerTimestep
            }

        Dim ts As New TimeSeries()
        ts.Unit = "mm/h"
        ts.AddNode(New DateTime(2000, 1, 1), 1.0)
        ts.AddNode(New DateTime(2000, 1, 2), 2.0)
        ts.AddNode(New DateTime(2000, 1, 3), 3.0)
        ts.AddNode(New DateTime(2000, 1, 4), 2.0)

        For Each interpretation As TimeSeries.InterpretationEnum In interpretations
            ts.Interpretation = interpretation
            Assert.AreEqual(Double.NaN, ts.Volume)
        Next

    End Sub

End Class