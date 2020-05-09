'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
'
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
Imports System.IO

''' <summary>
''' Klasse für das Simba-Dateiformat (*.SMB)
''' </summary>
''' <remarks>Format siehe http://wiki.bluemodel.org/index.php/SMB-Format</remarks>
Public Class SMB
    Inherits FileFormatBase

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property


#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iLineData = 2
        Me.UseUnits = True

        Call Me.ReadColumns()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllColumns()
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub ReadColumns()

        Dim Zeile As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Es gibt immer 2 Spalten
            ReDim Me.Columns(1)

            '1. Spalte (X)
            Me.Columns(0).Name = "Datum_Zeit"
            Me.Columns(0).Index = 0

            '2. Spalte (Y)

            'Reihentitel steht in 1. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            Me.Columns(1).Name = Zeile.Substring(15).Trim()
            'Annahme, dass SMB-Dateien Regenreihen sind, daher Einheit mm fest verdrahtet
            Me.Columns(1).Einheit = "mm"
            Me.Columns(1).Index = 1

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'SMB-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i, j As Integer
        Dim Zeile As String
        Dim Stunde, Minute, Tag, Monat, Jahr As Integer
        Dim Datum As DateTime
        Dim Anfangsdatum As DateTime
        Dim tmpWert As String

        Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
        Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
        Dim StrReadSync = TextReader.Synchronized(StrRead)

        'Zeitreihe instanzieren
        ReDim Me.TimeSeries(0) 'bei SMB gibt es nur eine Zeitreihe
        Me.TimeSeries(0) = New TimeSeries(Me.SelectedColumns(0).Name)
        Me.TimeSeries(0).Unit = Me.SelectedColumns(0).Einheit

        j = 1

        'Anfangsdatum einlesen
        Zeile = StrReadSync.ReadLine.ToString()
        Tag = Zeile.Substring(0, 2)
        Monat = Zeile.Substring(2, 2)
        Jahr = Zeile.Substring(4, 4)
        Stunde = Zeile.Substring(8, 2)
        Minute = Zeile.Substring(10, 2)

        Anfangsdatum = New System.DateTime(Jahr, Monat, Tag, Stunde, Minute, 0, New System.Globalization.GregorianCalendar())

        'Einlesen
        '--------
        Do
            Zeile = StrReadSync.ReadLine.ToString()
            j += 1
            If (j > Me.nLinesHeader And Zeile.Length > 0) Then

                'Datum erkennen
                '--------------
                For i = 0 To Zeile.Length
                    tmpWert = Zeile.Substring(i, 2)
                    If tmpWert = "  " Then
                        Minute = Zeile.Substring(0, i)
                        Exit For
                    End If
                Next
                'Minute = Zeile.Substring(0, 3)
                Datum = Anfangsdatum.AddMinutes(Minute)

                'Datum und Wert zur Zeitreihe hinzufügen
                '---------------------------------------
                Me.TimeSeries(0).AddNode(Datum, StringToDouble(Zeile.Substring(i + 2)))

            End If
        Loop Until StrReadSync.Peek() = -1

        StrReadSync.close()
        StrRead.Close()
        FiStr.Close()

    End Sub

#End Region 'Methoden

End Class
