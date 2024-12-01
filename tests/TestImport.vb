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
''' Tests for reading different time series formats
''' </summary>
<TestClass()>
Public Class TestImport

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
        Dim success As Boolean = Fileformats.WEL.extractFromWLZIP(file_wel)

        Assert.IsTrue(success)
        Assert.IsTrue(IO.File.Exists(file_wel))

    End Sub

End Class