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
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
'
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class TestApp

    <STATestMethod>
    Public Sub TestStartupFileIsImportedAfterMainWindowStarts()
        Dim file As String = IO.Path.Combine(TestData.GetTestDataDir(), "UVF", "42007.0_q.uvf")
        Dim wave As New Wave()
        Dim app As New App(wave, New List(Of String) From {file})
        Dim imported As Boolean = False
        Dim startupTimer As New System.Windows.Forms.Timer With {.Interval = 100}
        Dim stopwatch As System.Diagnostics.Stopwatch = System.Diagnostics.Stopwatch.StartNew()

        AddHandler startupTimer.Tick,
            Sub(sender As Object, e As EventArgs)
                If wave.TimeSeries.Count > 0 Then
                    imported = True
                    startupTimer.Stop()
                    app.MainForm.Close()
                ElseIf stopwatch.Elapsed >= TimeSpan.FromSeconds(15) Then
                    startupTimer.Stop()
                    app.MainForm.Close()
                End If
            End Sub

        startupTimer.Start()
        System.Windows.Forms.Application.Run(app)
        startupTimer.Dispose()

        Assert.IsTrue(imported, "The startup file was not imported before the timeout.")
        Assert.AreEqual(1, wave.TimeSeries.Count)
        Assert.AreEqual(IO.Path.GetFullPath(file), IO.Path.GetFullPath(wave.TimeSeries.Values.Single().DataSource.FilePath))
        Assert.IsTrue(wave.TimeSeries.Values.Single().Nodes.Count > 0, "The imported time series contains no data.")
    End Sub

End Class
