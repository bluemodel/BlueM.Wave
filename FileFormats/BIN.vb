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
''' Klasse für das SYDRO Binärformat
''' </summary>
''' <remarks>Greift zurück auf SydroZreNet.dll, welche wiederum auf SydroZreI.dll zurückgreift</remarks>
Public Class BIN
    Inherits FileFormatBase

    ''' <summary>
    ''' Error value
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ErrorValue As Double = -9999.999

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
        Me.Dateformat = Helpers.DefaultDateFormat 'irrelevant weil binär
        Me.iLineData = 0
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

        Dim sInfo As New SeriesInfo()
        
        Me.SeriesList.Clear()
        
        sInfo.Name = IO.Path.GetFileName(Me.File)
        sInfo.Unit = "-"
        Me.SeriesList.Add(sInfo)

    End Sub

    'BIN-Datei einlesen
    '******************
    Public Overrides Sub readFile()

        Try

            Dim NCount, i, errorcount As Integer
            Dim msg As String
            Dim X() As DateTime
            Dim Y() As Single
            Dim timestamp As DateTime
            Dim value As Double
            Dim sInfo As SeriesInfo
            Dim ts As TimeSeries

            'Zeitreihe instanzieren (nur eine)
            sInfo = Me.SeriesList(0)
            ts = New TimeSeries(sInfo.Name)
            ts.Unit = sInfo.Unit

            'Einlesen
            '--------
            ReDim X(0)
            ReDim Y(0)
            Using fortran As New Sydro.SydroZre.Fortran()
                NCount = fortran.getZreBinValues(Me.File, Nothing, Nothing, False, X, Y)
                msg = fortran.ErrorMsg
            End Using

            If NCount < 0 Then
                Throw New Exception("ERROR: " & NCount & ": " & msg)
            End If

            'Umwandeln in Zeitreihe
            errorcount = 0
            For i = 0 To NCount - 1
                timestamp = X(i)
                value = Y(i)
                'convert error values to NaN
                If Math.Abs(value - BIN.ErrorValue) < 0.0001 Then
                    value = Double.NaN
                    errorcount += 1
                End If
                ts.AddNode(timestamp, value)
            Next

            'Log 
            Call Log.AddLogEntry("Read " & NCount & " nodes.")
            If errorcount > 0 Then
                Log.AddLogEntry("The file contained " & errorcount & " error values (" & BIN.ErrorValue & "), which were converted to NaN!")
            End If

			'store time series
            Me.TimeSeriesCollection.Add(ts.Title, ts)

        Catch ex As Exception
            'Fehler weiterschmeissen
            Throw ex

        End Try

    End Sub

    ''' <summary>
    ''' Write a time series to a BIN file
    ''' </summary>
    ''' <param name="zre">the timeseries to write</param>
    ''' <param name="file">path to the file</param>
    ''' <remarks></remarks>
    Public Shared Sub Write_File(ByRef zre As TimeSeries, ByVal file As String)

        Dim isOK As Boolean
        Dim iResp As Integer
        Dim msg As String
        Dim dates() As Date
        Dim values() As Single
        Dim i As Integer

        'convert timeseries nodes to arrays of dates and values
        ReDim dates(zre.Length - 1)
        ReDim values(zre.Length - 1)

        i = 0
        For Each kvp As KeyValuePair(Of DateTime, Double) In zre.Nodes
            dates(i) = kvp.Key
            'convert NaN values to the BIN error value
            If Double.IsNaN(kvp.Value) Then
                values(i) = BIN.ErrorValue
            Else
                values(i) = kvp.Value
            End If
            i += 1
        Next

        'write values to file
        Using fortran As New Sydro.SydroZre.Fortran()
            isOK = fortran.setFileName(file)
            iResp = fortran.setZreBinValues(file, False, fortran.DateToDouble(dates), values)
            msg = fortran.ErrorMsg
        End Using

        If msg.Trim().Length > 0 Then
            Log.AddLogEntry("SydroZre: " & msg)
        End If

        If iResp <> zre.Length Then
            Throw New Exception("Number of values written to file (" & iResp & ") does not correspond to length of time series (" & zre.Length & ")!")
        End If

    End Sub

#End Region 'Methoden

End Class
