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
''' Class for SWMM5 time series (DAT) data format
''' </summary>

Public Class SWMM_DAT_MASS
    Inherits FileFormatBase

    Const DatumsformatSWMMDAT As String = "MM/dd/yyyy HH:mm"
    Const iDim As Integer = 3        'Dezimalfaktor wird erstmal global auf 3 gesetzt

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _Zeitintervall As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Property Zeitintervall() As Integer
        Get
            Return _Zeitintervall
        End Get
        Set(ByVal value As Integer)
            _Zeitintervall = value
        End Set
    End Property

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iLineData = 2
        Me.UseUnits = False

        Call Me.readSeriesInfo()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllSeries()
            Call Me.readFile()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub readSeriesInfo()

        Dim Zeile As String = ""
        Dim sInfo As SeriesInfo

        Me.SeriesList.Clear()

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Reihentitel steht in 1. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()

            'store series info
            sInfo = New SeriesInfo()
            sInfo.Name = Zeile.Trim()
            Me.SeriesList.Add(sInfo)

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    'DAT-Datei einlesen
    '******************
    Public Overrides Sub readFile()

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als DAT-Datei
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    ''' <param name="dt">Zeitschritt</param>
    Public Shared Sub Write_File(ByVal Reihe As TimeSeries, ByVal File As String, ByVal dt As Integer)

        Dim KontiReihe As TimeSeries

        'Äquidistante Zeitreihe erzeugen
        KontiReihe = Reihe.getKontiZRE2(dt)

        Dim strwrite As StreamWriter
        Dim iZeile, n As Integer
        strwrite = New StreamWriter(File)

        n = 0   'n = Anzahl der Zeitreihenwerte
        For iZeile = 0 To KontiReihe.Length - 1
            strwrite.Write(KontiReihe.Dates(n).ToString(DatumsformatSWMMDAT) & " ")
            strwrite.Write(KontiReihe.Values(n).ToString())
            n = n + 1
            strwrite.WriteLine()
        Next
        strwrite.Close()

    End Sub

#End Region


End Class
