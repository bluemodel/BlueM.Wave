Imports System.Windows.Forms
Imports System.Collections.Generic


Public Class ImportFromDatabaseDialog

    'ElementsetID, Dimension_ID
    Public Structure TypeSelResultID
        Public ElemID As Int32
        Public DimensionID As Int32
    End Structure

    Private mSelectedResultIDs As New List(Of TypeSelResultID)

    Public ReadOnly Property SelectedResultIDs() As List(Of TypeSelResultID)
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
            Dim mSelIDs As TypeSelResultID
            mSelectedResultIDs.Clear()

            For Each row As DataGridViewRow In DataGridView_Auswahl.SelectedRows()
                mSelIDs = New TypeSelResultID
                mSelIDs.ElemID = Int32.Parse(row.Cells("ELEMENTSET_ID").Value)
                mSelIDs.DimensionID = Int32.Parse(row.Cells("DIMENSION_ID").Value)
                mSelectedResultIDs.Add(mSelIDs)
            Next

        Catch ex As Exception
            FillSelectedResults = False

        End Try

    End Function


Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

End Sub


Private Sub ImportFromDatabaseDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'TODO: This line of code loads data into the 'DIRCDataSet.ALLbutValues' table. You can move, or remove it, as needed.
Me.ALLbutValuesTableAdapter.Fill(Me.DIRCDataSet.ALLbutValues)

End Sub

Private Sub DataGridView1_CellContentClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView_Auswahl.CellContentClick

End Sub


Private Sub FillByElementSetIDandDimensionIDToolStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)

End Sub
End Class
