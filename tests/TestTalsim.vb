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
''' Tests Talsim-specific functionality
''' </summary>
<TestClass()>
Public Class TestTalsim

    <TestMethod()>
    <DataRow("TALSIM\Clipboard_Talsim_WLZIP.txt")>
    <DataRow("TALSIM\Clipboard_Talsim_Zeitreihe_BIN.txt")>
    <DataRow("TALSIM\Clipboard_Talsim_Zeitreihe_WEL.txt")>
    <DataRow("TALSIM\Clipboard_Talsim_Zeitreihen_multi.txt")>
    <DataRow("TALSIM\Clipboard_Talsim4_Zeitreihe_KTR.WEL.txt")>
    <DataRow("TALSIM\Clipboard_Talsim5_Zeitreihe_KTR.WEL.txt")>
    Public Sub TestTalsimClipboard(clipboardFile As String)

        Dim workdir = IO.Directory.GetCurrentDirectory()
        Try
            'set current directory to test assemply path to ensure any relative paths in the clipboard data are correct
            IO.Directory.SetCurrentDirectory(My.Application.Info.DirectoryPath())

            Dim clipboardData As String = IO.File.ReadAllText(IO.Path.Combine(TestData.getTestDataDir(), clipboardFile))

            Dim talsimclipboard As New Parsers.TalsimClipboard(clipboardData)
            Dim tsList As List(Of TimeSeries) = talsimclipboard.Process()

            'check that time series were created
            Assert.IsTrue(tsList.Count > 0)

        Catch ex As Exception
            Assert.Fail("Exception occurred: " & ex.Message)

        Finally
            'restore original working directory
            IO.Directory.SetCurrentDirectory(workdir)
        End Try

    End Sub

    ''' <summary>
    ''' Tests finding a WEL file within a WLZIP file and extracting it
    ''' </summary>
    <TestMethod()>
    Public Sub TestWLZIP()

        Dim file_wel As String = IO.Path.Combine(TestData.getTestDataDir(), "Talsim", "TALSIM.WEL")

        'delete existing file before testing
        If IO.File.Exists(file_wel) Then
            IO.File.Delete(file_wel)
        End If

        'attempt to extract from WLZIP
        Dim success As Boolean = Parsers.TalsimClipboard.extractFromWLZIP(file_wel)

        Assert.IsTrue(success)
        Assert.IsTrue(IO.File.Exists(file_wel))

        'clean up by deleting the extracted file
        IO.File.Delete(file_wel)

    End Sub


    ''' <summary>
    ''' Tests loading a WEL file within a WLZIP file when processing Talsim clipboard data
    ''' </summary>
    <TestMethod()>
    Public Sub TestWLZIPFromTalsimClipboard()

        Dim workdir = IO.Directory.GetCurrentDirectory()
        Try
            'set current directory to test assemply path to ensure any relative paths in the clipboard data are correct
            IO.Directory.SetCurrentDirectory(My.Application.Info.DirectoryPath())

            Dim file_clipboard As String = IO.Path.Combine(TestData.getTestDataDir(), "Talsim", "Clipboard_Talsim_WLZIP.txt")
            Dim file_wel As String = IO.Path.Combine(TestData.getTestDataDir(), "Talsim", "TALSIM.WEL")

            'delete existing file before testing
            If IO.File.Exists(file_wel) Then
                IO.File.Delete(file_wel)
            End If

            Dim clipboardData As String = IO.File.ReadAllText(file_clipboard)

            Dim talsimclipboard As New Parsers.TalsimClipboard(clipboardData)
            Dim tsList As List(Of TimeSeries) = talsimclipboard.Process()

            'check that time series were created
            Assert.IsTrue(tsList.Count > 0)

            'check that extracted file was deleted
            Assert.IsFalse(IO.File.Exists(file_wel))

        Catch ex As Exception
            Assert.Fail("Exception occurred: " & ex.Message)

        Finally
            'restore original working directory
            IO.Directory.SetCurrentDirectory(workdir)
        End Try

    End Sub

End Class