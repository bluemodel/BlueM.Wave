<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Wave
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TChart1 = New Steema.TeeChart.TChart
        Me.Button_TxtImport = New System.Windows.Forms.Button
        Me.TextSource1 = New Steema.TeeChart.Data.TextSource
        Me.Commander1 = New Steema.TeeChart.Commander
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Button_TENImport = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TChart1
        '
        '
        '
        '
        Me.TChart1.Aspect.ElevationFloat = 345
        Me.TChart1.Aspect.RotationFloat = 345
        Me.TChart1.Aspect.View3D = False
        '
        '
        '
        '
        '
        '
        Me.TChart1.Axes.Bottom.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Bottom.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Bottom.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Depth.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Depth.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Depth.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.DepthTop.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.DepthTop.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Left.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Left.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Left.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Right.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Right.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Right.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Axes.Top.Automatic = True
        '
        '
        '
        Me.TChart1.Axes.Top.Grid.Style = System.Drawing.Drawing2D.DashStyle.Dash
        Me.TChart1.Axes.Top.Grid.ZPosition = 0
        '
        '
        '
        Me.TChart1.Header.Lines = New String() {"TeeChart"}
        '
        '
        '
        Me.TChart1.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Series
        '
        '
        '
        Me.TChart1.Legend.Shadow.Visible = True
        '
        '
        '
        '
        '
        '
        Me.TChart1.Legend.Title.Font.Bold = True
        '
        '
        '
        Me.TChart1.Legend.Title.Pen.Visible = False
        Me.TChart1.Location = New System.Drawing.Point(12, 72)
        Me.TChart1.Name = "TChart1"
        Me.TChart1.Size = New System.Drawing.Size(1139, 812)
        Me.TChart1.TabIndex = 0
        '
        '
        '
        '
        '
        '
        Me.TChart1.Walls.Back.AutoHide = False
        '
        '
        '
        Me.TChart1.Walls.Bottom.AutoHide = False
        '
        '
        '
        Me.TChart1.Walls.Left.AutoHide = False
        '
        '
        '
        Me.TChart1.Walls.Right.AutoHide = False
        '
        'Button_TxtImport
        '
        Me.Button_TxtImport.Location = New System.Drawing.Point(12, 43)
        Me.Button_TxtImport.Name = "Button_TxtImport"
        Me.Button_TxtImport.Size = New System.Drawing.Size(150, 23)
        Me.Button_TxtImport.TabIndex = 1
        Me.Button_TxtImport.Text = "Zeitreihe importieren"
        Me.Button_TxtImport.UseVisualStyleBackColor = True
        '
        'TextSource1
        '
        Me.TextSource1.DecimalSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.TextSource1.Fields.AddRange(New Steema.TeeChart.Data.TextField() {New Steema.TeeChart.Data.TextField(0, "x"), New Steema.TeeChart.Data.TextField(1, "y")})
        Me.TextSource1.HeaderLines = 1
        Me.TextSource1.Separator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.TextSource1.Series = Nothing
        '
        'Commander1
        '
        Me.Commander1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.Commander1.Chart = Me.TChart1
        Me.Commander1.DropDownArrows = True
        Me.Commander1.LabelValues = True
        Me.Commander1.Location = New System.Drawing.Point(0, 0)
        Me.Commander1.Name = "Commander1"
        Me.Commander1.ShowToolTips = True
        Me.Commander1.Size = New System.Drawing.Size(1181, 37)
        Me.Commander1.TabIndex = 2
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "txt"
        Me.OpenFileDialog1.Filter = "Text-Dateien (*.txt)|*.txt|ZRE-Dateien (*.zre)|*.zre|CSV-Dateien (*.csv)|*.csv|WEL-Dateien (*.wel)|*.wel|Alle Dateien (*.*)|*.*"
        Me.OpenFileDialog1.Title = "Textdatei öffnen"
        '
        'Button_TENImport
        '
        Me.Button_TENImport.Location = New System.Drawing.Point(168, 43)
        Me.Button_TENImport.Name = "Button_TENImport"
        Me.Button_TENImport.Size = New System.Drawing.Size(150, 23)
        Me.Button_TENImport.TabIndex = 3
        Me.Button_TENImport.Text = "TEN importieren"
        Me.Button_TENImport.UseVisualStyleBackColor = True
        '
        'Zeitreihendarstellung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1181, 1002)
        Me.Controls.Add(Me.Button_TENImport)
        Me.Controls.Add(Me.Commander1)
        Me.Controls.Add(Me.Button_TxtImport)
        Me.Controls.Add(Me.TChart1)
        Me.Name = "Zeitreihendarstellung"
        Me.Text = "Zeitreihendarstellung"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TChart1 As Steema.TeeChart.TChart
    Friend WithEvents Button_TxtImport As System.Windows.Forms.Button
    Friend WithEvents TextSource1 As Steema.TeeChart.Data.TextSource
    Friend WithEvents Commander1 As Steema.TeeChart.Commander
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button_TENImport As System.Windows.Forms.Button

End Class
