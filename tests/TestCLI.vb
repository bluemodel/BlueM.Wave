'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABiLITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
'
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class TestCLI

    ''' <summary>
    ''' Tests that the Wave CLI can convert a ZRX file to BIN format
    ''' </summary>
    <TestMethod>
    Public Sub TestConvertZRXToBIN()
        Dim inputFile As String = IO.Path.Combine(TestData.GetTestDataDir(), "ZRXP", "Q.1440.zrx")
        Dim outputDirectory As String = IO.Path.GetDirectoryName(inputFile)
        Dim executable As String = IO.Path.ChangeExtension(GetType(Wave).Assembly.Location, ".exe")

        Assert.IsTrue(IO.File.Exists(inputFile), $"Input file not found: {inputFile}")
        Assert.IsTrue(IO.File.Exists(executable), $"Wave executable not found: {executable}")

        Dim inputZRX As New Fileformats.ZRXP(inputFile, ReadAllNow:=True)
        Dim tsZRX As TimeSeries = inputZRX.TimeSeries.Values.Single()

        Dim startInfo As New ProcessStartInfo(executable) With {
            .UseShellExecute = False,
            .CreateNoWindow = True
        }
        startInfo.ArgumentList.Add("-convert")
        startInfo.ArgumentList.Add("-of")
        startInfo.ArgumentList.Add("BIN")
        startInfo.ArgumentList.Add(inputFile)
        startInfo.ArgumentList.Add(outputDirectory)

        Using process As Process = Process.Start(startInfo)
            Assert.IsNotNull(process, "Failed to start the Wave CLI process.")
            Assert.IsTrue(process.WaitForExit(30000), "The Wave CLI conversion did not finish within 30 seconds.")
            Assert.AreEqual(0, process.ExitCode, "The Wave CLI process returned a failure exit code.")
        End Using

        Dim outputFile As String = IO.Path.Combine(outputDirectory, $"{tsZRX.Title}.bin")
        Assert.IsTrue(IO.File.Exists(outputFile), $"Expected BIN output file not found: {outputFile}")

        Dim outputBIN As New Fileformats.BIN(outputFile, ReadAllNow:=True)
        Dim tsBIN As TimeSeries = outputBIN.TimeSeries.Values.Single()
        Assert.AreEqual(tsZRX.Length, tsBIN.Length, $"Converted BIN file has unexpected node count: {tsBIN.Length}")

    End Sub

End Class
