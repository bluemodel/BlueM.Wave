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

    ''' <summary>
    ''' Tests that the Wave app starts and shows the main window successfully
    ''' </summary>
    <STATestMethod>
    Public Sub TestAppStarts()
        Dim app As New App()
        Dim started As Boolean = False
        Dim startupTimer As New System.Windows.Forms.Timer With {.Interval = 100}
        Dim stopwatch As System.Diagnostics.Stopwatch = System.Diagnostics.Stopwatch.StartNew()

        AddHandler startupTimer.Tick,
            Sub(sender As Object, e As EventArgs)
                If app.MainForm.Visible Then
                    started = True
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

        Assert.IsTrue(started, "The app's main window did not become visible before the timeout.")
    End Sub

    ''' <summary>
    ''' Tests that a startup file passed to the Wave app is imported after the main window starts
    ''' </summary>
    <STATestMethod>
    Public Sub TestStartupFileIsImportedAfterMainWindowStarts()
        Dim startupFile As String = IO.Path.Combine(TestData.GetTestDataDir(), "UVF", "42007.0_q.uvf")
        Assert.IsTrue(IO.File.Exists(startupFile), $"Input startupFile not found: {startupFile}")

        Dim wave As New Wave()
        Dim app As New App(wave, New List(Of String) From {startupFile})
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
        Assert.AreEqual(IO.Path.GetFullPath(startupFile), IO.Path.GetFullPath(wave.TimeSeries.Values.Single().DataSource.FilePath))
        Assert.IsTrue(wave.TimeSeries.Values.Single().Nodes.Count > 0, "The imported time series contains no data.")
    End Sub

    ''' <summary>
    ''' Tests that a TEN startup file passed to the Wave app is imported after the main window starts
    ''' </summary>
    <STATestMethod>
    Public Sub TestStartupTENFileIsLoaded()
        Dim tenFile As String = IO.Path.Combine(TestData.GetTestDataDir(), "TEN", "DEMONA_PSI.json.ten")
        Assert.IsTrue(IO.File.Exists(tenFile), $"Input tenFile not found: {tenFile}")
        Dim wave As New Wave()
        Dim app As New App(wave, New List(Of String) From {tenFile})
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

        Assert.IsTrue(imported, "The TEN startup file's time series were not imported before the timeout.")
        Assert.IsTrue(wave.TimeSeries.Count > 0)
        Assert.IsTrue(wave.TimeSeries.Values.Any(Function(ts) IO.Path.GetFullPath(ts.DataSource.FilePath).Equals(IO.Path.GetFullPath(tenFile), StringComparison.OrdinalIgnoreCase)))
        Assert.IsTrue(wave.TimeSeries.Values.All(Function(ts) ts.Nodes.Count > 0), "A time series imported from the TEN file contains no data.")
    End Sub

End Class
