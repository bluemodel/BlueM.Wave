Imports IHWB.Wave.BlueMOrbDataSet
Imports IHWB.Wave.BlueMOrbDataSetTableAdapters

Public Class ZeitreihenDatabase
    Inherits ZeitreihenExtern

    Private ds As BlueMOrbDataSet
    Private tSelectedResults As SelectedResultsTableAdapter


    Private mSelectedResultIDs As New List(Of Long)


    Public Sub AddZRE(ByVal SIMResultID As Int32)
        mSelectedResultIDs.Add(SIMResultID)
    End Sub


    Public Overrides Sub selectAll_Zeitreihen()
        'Macht keinen Sinn
    End Sub

    Public Overrides ReadOnly Property Typ() As ZeitreihenExtern.ZREQuellenTyp
        Get
            Return ZREQuellenTyp.ZRDatabase
        End Get
    End Property

    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub Zeitreihe_Einlesen()
        ReDim Zeitreihen(mSelectedResultIDs.Count - 1)
        Dim index As Integer
        ds = New BlueMOrbDataSet()
        tSelectedResults = New SelectedResultsTableAdapter()
        tSelectedResults.ClearBeforeFill = true

        For Each SimresultID As Long In mSelectedResultIDs
            einzelne_Zeitreihe_Einlesen(Zeitreihen(index), SimresultID)
            index = index + 1
        Next
    End Sub

    Private Sub einzelne_Zeitreihe_Einlesen(ByRef ZRE As Zeitreihe, ByVal SimResultID As Long)
        'Tableadapter aus DB holen
        ds.EnforceConstraints = False
        tSelectedResults.FillBySimResultID(ds.SelectedResults, SimResultID)

        'ZRE setzen
        ZRE = New Zeitreihe()
        If (ds.SelectedResults.Count = 0) Then
            'Hier gibt es nicht mehr zu tun :(
            Return
        End If
        'Titel und Einheit aus dem Erstem Element
        ZRE.Title = ds.SelectedResults(0).ELEMENTSET_NAME + "-" + ds.SelectedResults(0).DIMENSION_NAME
        ZRE.Einheit = ds.SelectedResults(0).DIMENSION_EINHEIT

        For Each row As BlueMOrbDataSet.SelectedResultsRow In ds.SelectedResults
            ZRE.AddNode(row.RESULT_TIME, row.RESULT_VALUE)
        Next

    End Sub



End Class
