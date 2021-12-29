Imports System.Text.RegularExpressions

Friend Class AxisController
    Inherits WinformsMvc.Controller

    Private WithEvents _axisView As AxisDialog
    Private _model As WaveModel

    Public Sub New()
        _axisView = New AxisDialog()

        AddHandler _axisView.AxisDeleted, AddressOf axisDeleted
    End Sub

    Public Overrides ReadOnly Property View As WinformsMvc.IView
        Get
            Return _axisView
        End Get
    End Property

    Public Overrides Function Loadable() As Boolean
        Return True
    End Function

    Public Overrides Sub SetModel(ByRef model As Object)
        _model = model
    End Sub

    ''' <summary>
    ''' Handles axis deleted in the AxisDialog
    ''' </summary>
    ''' <param name="axisname"></param>
    Private Sub axisDeleted(axisname As String)
        Dim axisnumber As Integer
        Dim m As Match = Regex.Match(axisname, "Custom (\d+)")
        If m.Success Then
            axisnumber = Integer.Parse(m.Groups(1).Value)
            'Delete axis from chart
            Me.TChart1.Axes.Custom.RemoveAt(axisnumber)
            Me.TChart1.Refresh()
            'update axis dialog
            Call Me.UpdateAxisDialog()
        End If
    End Sub

    ''' <summary>
    ''' Handles axis unit changed in the AxisDialog
    ''' </summary>
    ''' <remarks>Reassigns all series to their appropriate axis</remarks>
    Private Sub AxisUnitChanged() Handles _axisView.AxisUnitChanged

        For Each series As Steema.TeeChart.Styles.Series In Me.TChart1.Series
            assignSeriesToAxis(series, Me.TimeSeriesDict(series.Tag).Unit)
        Next

        'deactivate unused custom axes
        Dim unitUsed As Boolean
        For Each axis As Steema.TeeChart.Axis In Me.TChart1.Axes.Custom
            unitUsed = False
            For Each ts As TimeSeries In Me.TimeSeriesDict.Values
                If ts.Unit = axis.Tag Then
                    unitUsed = True
                    Exit For
                End If
            Next
            If Not unitUsed Then
                axis.Visible = False
            End If
        Next

    End Sub

    ''' <summary>
    ''' Update AxisDialog
    ''' </summary>
    Private Sub UpdateAxisDialog()
        'Wrap Left, Right and Custom axes
        Dim axisList As New List(Of AxisWrapper)
        axisList.Add(New AxisWrapper("Left", Me.TChart1.Axes.Left))
        axisList.Add(New AxisWrapper("Right", Me.TChart1.Axes.Right))
        For i As Integer = 0 To Me.TChart1.Axes.Custom.Count - 1
            axisList.Add(New AxisWrapper("Custom " & i, Me.TChart1.Axes.Custom(i)))
        Next

        Me.axisDialog.Update(axisList)
    End Sub

End Class
