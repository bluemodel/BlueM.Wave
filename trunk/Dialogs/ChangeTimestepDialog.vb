Imports System.Windows.Forms

Friend Class ChangeTimestepDialog

    Private zres As Dictionary(Of String, TimeSeries)
    Private isInitializing As Boolean

    Public Sub New(ByRef _zres As Dictionary(Of String, TimeSeries))
        Call InitializeComponent()

        'store dictionary of time series
        Me.zres = _zres

        'fill comboboxes
        Me.isInitializing = True
        For Each zre As TimeSeries In Me.zres.Values
            Me.ComboBox_Timeseries.Items.Add(zre.Title)
        Next
        Me.ComboBox_Timeseries.SelectedIndex = 0

        Me.ComboBox_Interpretation.Items.Add(TimeSeries.InterpretationEnum.Instantaneous)
        Me.ComboBox_Interpretation.Items.Add(TimeSeries.InterpretationEnum.BlockLeft)
        Me.ComboBox_Interpretation.Items.Add(TimeSeries.InterpretationEnum.BlockRight)
        Me.ComboBox_Interpretation.Items.Add(TimeSeries.InterpretationEnum.Cumulative)
        Me.ComboBox_Interpretation.Items.Add(TimeSeries.InterpretationEnum.CumulativePerTimestep)
        Me.ComboBox_Interpretation.SelectedIndex = 0

        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Year)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Month)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Week)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Day)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Hour)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Minute)
        Me.ComboBox_TimestepType.Items.Add(TimeSeries.TimeStepTypeEnum.Second)
        Me.ComboBox_TimestepType.SelectedIndex = 3
        Me.isInitializing = False

    End Sub

    ''' <summary>
    ''' Called when the selected timeseries has changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox_Timeseries_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Timeseries.SelectedIndexChanged

        'determine selected time series
        Dim zre As TimeSeries
        zre = Me.zres(Me.ComboBox_Timeseries.SelectedItem)

        'update the DateTimePicker
        Me.DateTimePicker_Start.Value = zre.StartDate

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
