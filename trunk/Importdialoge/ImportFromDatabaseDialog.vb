Imports System.Windows.Forms

Public Class ImportFromDatabaseDialog

    Private mSelectedResultIDs As New List(Of Long)

    Public ReadOnly Property SelectedResultIDs() As List(Of Long)
        Get
            Return mSelectedResultIDs
        End Get
    End Property




    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If (DataGridView_Auswahl.SelectedRows.Count = 0) Then
            DataGridView_Auswahl.Focus()
            Return
        End If

        If (Not FillSelectedResults()) Then
            'Fehler also schlissen
            Me.Close()
        End If

      Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function FillSelectedResults() As Boolean
        FillSelectedResults = False
        Try
            mSelectedResultIDs.Clear()

            For Each row As DataGridViewRow In DataGridView_Auswahl.SelectedRows()
                'in 0 steht die SimresultID
                mSelectedResultIDs.Add(Int32.Parse(row.Cells(0).Value))
            Next

        Catch ex As Exception
            FillSelectedResults = False

        End Try

    End Function



Private Sub ImportFromDatabaseDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'TODO: This line of code loads data into the 'BlueMOrbDataSet.AllInfosAboutSimResults' table. You can move, or remove it, as needed.
Me.AllInfosAboutSimResultsTableAdapter.Fill(Me.BlueMOrbDataSet.AllInfosAboutSimResults)

End Sub


End Class
