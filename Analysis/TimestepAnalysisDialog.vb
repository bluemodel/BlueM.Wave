Imports System.Windows.Forms

Partial Friend Class TimestepAnalysisDialog

    Private zres As Dictionary(Of String, TimeSeries)
    Private isInitializing As Boolean

    Public Sub New()
        Call InitializeComponent()

        'fill comboboxes
        Me.isInitializing = True
        'Me.ComboBox_TimestepType.Items.Add(TimeStepAnalysis.TimeUnit.Years)
        'Me.ComboBox_TimestepType.Items.Add(TimeStepAnalysis.TimeUnit.Months)
        Me.ComboBox_TimestepType.Items.Add(TimeStepAnalysis.TimeUnitEnum.Days)
        Me.ComboBox_TimestepType.Items.Add(TimeStepAnalysis.TimeUnitEnum.Hours)
        Me.ComboBox_TimestepType.Items.Add(TimeStepAnalysis.TimeUnitEnum.Minutes)
        Me.ComboBox_TimestepType.Items.Add(TimeStepAnalysis.TimeUnitEnum.Seconds)
        Me.ComboBox_TimestepType.SelectedItem = TimeStepAnalysis.TimeUnitEnum.Minutes
        Me.isInitializing = False

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
