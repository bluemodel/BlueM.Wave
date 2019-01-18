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

        Me.ComboBox_Interpretation.Items.Add(Sydro.SydroZre.Fortran.InterpretationEnum.LinearInterpolation)
        Me.ComboBox_Interpretation.Items.Add(Sydro.SydroZre.Fortran.InterpretationEnum.BlockLinks)
        Me.ComboBox_Interpretation.Items.Add(Sydro.SydroZre.Fortran.InterpretationEnum.BlockRechts)
        Me.ComboBox_Interpretation.Items.Add(Sydro.SydroZre.Fortran.InterpretationEnum.Summenlinie)
        Me.ComboBox_Interpretation.Items.Add(Sydro.SydroZre.Fortran.InterpretationEnum.SummeProDt)
        Me.ComboBox_Interpretation.SelectedIndex = 0

        Me.ComboBox_TimestepType.Items.Add(Sydro.SydroZre.Fortran.TimeStepTypeEnum.Year) 'BUG: TimeStepType Year does currently not work properly in SydoZreI.dll!
        Me.ComboBox_TimestepType.Items.Add(Sydro.SydroZre.Fortran.TimeStepTypeEnum.Month)
        Me.ComboBox_TimestepType.Items.Add(Sydro.SydroZre.Fortran.TimeStepTypeEnum.Week)
        Me.ComboBox_TimestepType.Items.Add(Sydro.SydroZre.Fortran.TimeStepTypeEnum.Day)
        Me.ComboBox_TimestepType.Items.Add(Sydro.SydroZre.Fortran.TimeStepTypeEnum.Hour)
        Me.ComboBox_TimestepType.Items.Add(Sydro.SydroZre.Fortran.TimeStepTypeEnum.Minute)
        Me.ComboBox_TimestepType.Items.Add(Sydro.SydroZre.Fortran.TimeStepTypeEnum.Second)
        Me.ComboBox_TimestepType.SelectedIndex = 0
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

        'update the DateTimePickers
        Me.DateTimePicker_Start.MinDate = zre.StartDate
        Me.DateTimePicker_Start.MaxDate = zre.EndDate
        Me.DateTimePicker_Start.Value = zre.StartDate

        Me.DateTimePicker_End.MinDate = zre.StartDate
        Me.DateTimePicker_End.MaxDate = zre.EndDate
        Me.DateTimePicker_End.Value = zre.EndDate

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
