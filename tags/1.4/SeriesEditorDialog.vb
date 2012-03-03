'Copyright (c) 2011, ihwb, TU Darmstadt
'All rights reserved.
'
'Released under the BSD-2-Clause License:
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list 
'  of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list 
'  of conditions and the following disclaimer in the documentation and/or other materials 
'  provided with the distribution.
'
'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
'OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
'SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
'SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
'OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
'HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
'TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
'EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'--------------------------------------------------------------------------------------------
'
'Dialog zum Eingeben einer Zeitreihe
'###################################

Public Class SeriesEditorDialog

    Private IsInitializing As Boolean
    Private mZeitreihe As Zeitreihe

    'Titel
    '*****
    Private ReadOnly Property Title() As String
        Get
            Return Me.TextBox_Title.Text
        End Get
    End Property

    'Einheit
    '*******
    Private ReadOnly Property Einheit() As String
        Get
            If (Not Me.TextBox_Einheit.Text.Trim() = "")
                Return Me.TextBox_Einheit.Text
            Else
                Return "-"
            End If
        End Get
    End Property

    'Zeitreihe
    '*********
    Public ReadOnly Property Zeitreihe() As Zeitreihe
        Get
            Return Me.mZeitreihe
        End Get
    End Property

    'Konstruktor
    '***********
    Public Sub New()

        Me.IsInitializing = True

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.IsInitializing = False

    End Sub

    'Werte aus Zwischenablage einfügen
    '*********************************
    Private Sub Paste(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Paste.Click

        Dim CSVSeparator As String
        Dim ClipboardContents As IDataObject
        Dim line, row() As String

        'aktuell verwendetes Listentrennzeichen bestimmen
        CSVSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator

        'Inhalt der Zwischenablage einlesen
        ClipboardContents = Clipboard.GetDataObject()

        'Prüfen, ob ClipboardContents im CSV-Format vorliegen oder konvertiert werden können
        If (Not ClipboardContents.GetDataPresent(DataFormats.CommaSeparatedValue, True)) Then
            MsgBox("Der Inhalt der Zwischenablage kann nicht verarbeitet werden!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'ClipboardContents zu CSV konvertieren und in einen StreamReader packen
        Dim sr As New System.IO.StreamReader(CType(ClipboardContents.GetData(DataFormats.CommaSeparatedValue), System.IO.MemoryStream))

        'Alle Zeilen verarbeiten und zu DataGridView hinzufügen
        Do Until sr.Peek = -1
            line = sr.ReadLine()
            If (line.Length > 0 And line.Contains(CSVSeparator)) Then
                row = line.Split(CSVSeparator, 2, StringSplitOptions.RemoveEmptyEntries)
                Me.DataGridView1.Rows.Add(row)
            End If
        Loop

    End Sub

    'Alle Eingaben überprüfen
    '************************
    Private Function Prüfen() As Boolean

        Dim str As String = ""
        Dim i As Integer
        Dim cell As DataGridViewCell
        Dim t0, t1 As DateTime
        Dim d As Double

        t0 = New DateTime(0)

        'Alle Einträge durchlaufen
        For i = 0 To Me.DataGridView1.RowCount - 2 '(letzte Zeile nicht prüfen)


            'Spalte Datum
            '------------
            cell = Me.DataGridView1.Rows(i).Cells(0)
            str = cell.Value
            'Datumsformat prüfen
            Try
                t1 = DateTime.Parse(str)
            Catch ex As Exception
                MsgBox("Das Datum '" & str & "' kann nicht verarbeitet werden!", MsgBoxStyle.Critical)
                cell.ErrorText = "Datumsformat nicht verarbeitbar!"
                Return False
            End Try

            'Prüfen, ob Datum aufsteigend
            If (t1 <= t0) Then
                MsgBox("Das Datum ist bei '" & t1 & "' nicht aufsteigend!", MsgBoxStyle.Critical)
                cell.ErrorText = "Datum nicht aufsteigend!"
                Return False
            End If
            cell.ErrorText = String.Empty
            t0 = t1

            'Spalte Wert
            '-----------
            cell = Me.DataGridView1.Rows(i).Cells(1)
            str = cell.Value
            'Wert prüfen
            Try
                d = Convert.ToDouble(str)
            Catch ex As Exception
                MsgBox("Der Wert '" & str & "' kann nicht verarbeitet werden!", MsgBoxStyle.Critical)
                cell.ErrorText = "Wert nicht verarbeitbar!"
                Return False
            End Try
            cell.ErrorText = String.Empty

        Next

        Return True

    End Function

    'On the fly - Überprüfung
    '************************
    Private Sub DataGridView1_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        If (Me.IsInitializing) Then Exit Sub

        Dim cell As DataGridViewCell
        Dim str As String = ""
        Dim d As Double
        Dim t As DateTime

        'letzte Zeile nicht prüfen
        If (e.RowIndex < Me.DataGridView1.RowCount - 1) Then

            cell = Me.DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)

            If (e.ColumnIndex = 0) Then
                'Datumsformat prüfen
                '-------------------
                str = cell.Value
                Try
                    t = DateTime.Parse(str)
                    cell.ErrorText = String.Empty
                Catch ex As Exception
                    MsgBox("Das Datum '" & str & "' kann nicht verarbeitet werden!", MsgBoxStyle.Critical)
                    cell.ErrorText = "Datumsformat nicht verarbeitbar!"
                End Try

            ElseIf (e.ColumnIndex = 1) Then
                'Wertformat prüfen
                '-----------------
                str = cell.Value
                Try
                    d = Convert.ToDouble(str)
                    cell.ErrorText = String.Empty
                Catch ex As Exception
                    MsgBox("Der Wert '" & str & "' kann nicht verarbeitet werden!", MsgBoxStyle.Critical)
                    cell.ErrorText = "Wert nicht verarbeitbar!"
                End Try
            End If

        End If

    End Sub

    'OK gedrückt
    '***********
    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click

        Dim i As Integer
        Dim datum As DateTime
        Dim wert As Double

        'Titel prüfen
        If (Me.TextBox_Title.Text = String.Empty) Then
            MsgBox("Bitte einen Titel eingeben!", MsgBoxStyle.Exclamation)
            Me.TextBox_Title.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        End If

        'Eingabe prüfen
        If (Not Me.Prüfen()) Then
            Me.DialogResult = DialogResult.None
            Exit Sub
        End If

        'Zeitreihe instanzieren
        Me.mZeitreihe = New Zeitreihe(Me.Title)

        'Einheit schreiben
        Me.Zeitreihe.Einheit = Me.Einheit

        For i = 0 To Me.DataGridView1.RowCount - 2 '(letzte Zeile nicht mitnehmen)
            datum = DateTime.Parse(Me.DataGridView1.Rows(i).Cells(0).Value)
            wert = Convert.ToDouble(Me.DataGridView1.Rows(i).Cells(1).Value)
            Me.mZeitreihe.AddNode(datum, wert)
        Next

        Me.Close()

    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If (e.Control And e.KeyCode = Keys.V) Then
            Call Me.Paste(sender, New EventArgs())
        End If
    End Sub
End Class