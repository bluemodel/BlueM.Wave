'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
'
Imports System.IO

Namespace Fileformats

    ''' <summary>
    ''' Class for SWMM5 time series (DAT) data format
    ''' </summary>
    Public Class SWMM_DAT_MASS
        Inherits TimeSeriesFile

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
            Set(value As Integer)
                _Zeitintervall = value
            End Set
        End Property

#End Region

#Region "Methoden"

        'Methoden
        '########

        'Konstruktor
        '***********
        Public Sub New(FileName As String, Optional ReadAllNow As Boolean = False)

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
            Dim sInfo As TimeSeriesInfo

            Me.TimeSeriesInfos.Clear()

            Try
                'Datei öffnen
                Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
                Dim StrRead As StreamReader = New StreamReader(FiStr, Me.Encoding)
                Dim StrReadSync = TextReader.Synchronized(StrRead)

                'Reihentitel steht in 1. Zeile:
                Zeile = StrReadSync.ReadLine.ToString()

                'store series info
                sInfo = New TimeSeriesInfo()
                sInfo.Name = Zeile.Trim()
                Me.TimeSeriesInfos.Add(sInfo)

                StrReadSync.Close()
                StrRead.Close()
                FiStr.Close()

            Catch ex As Exception
                MsgBox($"Unable to read file!{eol}{eol}Error: {ex.Message}", MsgBoxStyle.Critical)
            End Try

        End Sub

        'DAT-Datei einlesen
        '******************
        Public Overrides Sub readFile()
            Throw New NotImplementedException("Reading SWMM5 time series (DAT) files is not yet implemented!")
        End Sub

        ''' <summary>
        ''' Exportiert eine Zeitreihe als DAT-Datei
        ''' </summary>
        ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
        ''' <param name="File">Pfad zur anzulegenden Datei</param>
        ''' <param name="dt">Zeitschritt</param>
        Public Shared Sub Write_File(Reihe As TimeSeries, File As String, dt As Integer)

            Dim KontiReihe As TimeSeries

            'Äquidistante Zeitreihe erzeugen
            KontiReihe = Reihe.getKontiZRE2(dt)

            Dim strwrite As StreamWriter
            Dim iZeile, n As Integer
            strwrite = New StreamWriter(File)

            n = 0   'n = Anzahl der Zeitreihenwerte
            For iZeile = 0 To KontiReihe.Length - 1
                strwrite.Write(KontiReihe.Dates(n).ToString(DatumsformatSWMMDAT) & " ")
                strwrite.Write(KontiReihe.Values(n).ToString(Helpers.DefaultNumberFormat))
                n = n + 1
                strwrite.WriteLine()
            Next
            strwrite.Close()

        End Sub

#End Region


    End Class

End Namespace