Imports System.Windows.Forms

Public Class PropertiesDialog

    Public Sub New(ByVal series As Dictionary(Of String, TimeSeries))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'Update the BindingSource of the DataGridview
        Me.TimeSeriesBindingSource.Clear()
        For Each kvp As KeyValuePair(Of String, TimeSeries) In series
            Me.TimeSeriesBindingSource.Add(kvp.Value)
        Next
    End Sub

End Class
