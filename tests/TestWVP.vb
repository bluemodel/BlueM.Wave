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
''' Tests for reading and writing WVP files
''' </summary>
<TestClass()> Public Class TestWVP

    ''' <summary>
    ''' Tests parsing WVP files
    ''' </summary>
    <TestMethod()> Public Sub TestWVP_Parse()

        Dim filesWVP As New List(Of String) From {
            IO.Path.Combine(TestData.getTestDataDir(), "WVP", "test_all_filetypes.wvp"),
            IO.Path.Combine(TestData.getTestDataDir(), "WVP", "test_displayoptions.wvp")
        }

        For Each file As String In filesWVP
            Dim wvp As New Fileformats.WVP(file)
        Next

    End Sub

    ''' <summary>
    ''' Tests reading WVP series options
    ''' </summary>
    <TestMethod()> Public Sub TestWVP_ReadOptions()

        Dim fileWVP As String = IO.Path.Combine(TestData.getTestDataDir(), "WVP", "test_displayoptions.wvp")

        Dim wvp As New Fileformats.WVP(fileWVP)
        Dim tsList As List(Of TimeSeries) = wvp.Process()

        Assert.AreEqual("AA  _1AB", tsList(0).Title)
        Assert.AreEqual("AB  _1AB custom title", tsList(1).Title)
        Assert.AreEqual("AC  _1AB custom title", tsList(2).Title)
        With tsList(3)
            Assert.AreEqual("AD  _1AB ,= custom title", .Title)
            Assert.AreEqual("m³/s", .Unit)
            Assert.AreEqual(TimeSeries.InterpretationEnum.BlockLeft, .Interpretation)
            Assert.AreEqual("Red", .DisplayOptions.Color.Name)
            Assert.AreEqual(4, .DisplayOptions.LineWidth)
            Assert.AreEqual(Drawing.Drawing2D.DashStyle.Dash, .DisplayOptions.LineStyle)
            Assert.AreEqual(True, .DisplayOptions.ShowPoints)
        End With

    End Sub

    ''' <summary>
    ''' Tests writing WVP series options
    ''' </summary>
    <TestMethod()> Public Sub TestWVP_WriteOptions()

        'read time series using a WVP file
        Dim fileWVP As String = IO.Path.Combine(TestData.getTestDataDir(), "WVP", "test_displayoptions.wvp")
        Dim wvp As New Fileformats.WVP(fileWVP)
        Dim tsList As List(Of TimeSeries) = wvp.Process()

        'write the time series to a WVP file
        Dim file As String = IO.Path.Combine(TestData.getTestDataDir(), "WVP", "test_displayoptions_export.wvp")
        Call Fileformats.WVP.Write_File(tsList, file)

        'check the file contents of the written WVP file
        'NOTE: because the series were never loaded in the chart, colors that were not set in the input wvp
        'will have the default value of Color.Empty (0)
        Dim fileContents As String = New IO.StreamReader(file, Text.Encoding.UTF8).ReadToEnd()
        Dim lines As String() = fileContents.Split({"\r\n", "\r", "\n"}, StringSplitOptions.None)

        Dim iLine As Integer = 0
        For Each line As String In lines
            iLine += 1
            Select Case iLine
                Case 1
                    Assert.IsTrue(line.StartsWith("#"))
                Case 2
                    Assert.IsTrue(line.StartsWith("file="))
                    Assert.IsTrue(line.EndsWith("WEL\DEMONA_PSI.wel"))
                Case 3
                    Assert.AreEqual("    series=AA  _1AB: title=""AA  _1AB"", unit=""m3/s"", interpretation=BlockRight, color=0, linewidth=2, linestyle=Solid, showpoints=False", line)
                Case 4
                    Assert.AreEqual("    series=AB  _1AB: title=""AB  _1AB custom title"", unit=""m3/s"", interpretation=BlockRight, color=0, linewidth=2, linestyle=Solid, showpoints=False", line)
                Case 5
                    Assert.AreEqual("    series=AC  _1AB: title=""AC  _1AB custom title"", unit=""m3/s"", interpretation=BlockRight, color=0, linewidth=2, linestyle=Solid, showpoints=False", line)
                Case 6
                    Assert.AreEqual("    series=AD  _1AB: title=""AD  _1AB ,= custom title"", unit=""m³/s"", interpretation=BlockLeft, color=Red, linewidth=4, linestyle=Dash, showpoints=True", line)
            End Select
        Next

    End Sub

End Class