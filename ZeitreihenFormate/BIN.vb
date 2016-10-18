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
    Inherits Dateiformat

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
        Me.Datumsformat = Datumsformate("default") 'irrelevant weil binär
        Me.iZeileDaten = 0
        Me.UseEinheiten = False

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        ReDim Me.Spalten(1)

        Me.Spalten(0).Name = "Datetime"

        Me.Spalten(1).Name = "Values"
        Me.Spalten(1).Einheit = "-"

        Me.XSpalte = 0

    End Sub

    'BIN-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Try

            Dim NCount, i As Integer
            Dim msg As String
            Dim X() As DateTime
            Dim Y() As Single

            'Zeitreihe instanzieren (nur eine)
            ReDim Me.Zeitreihen(0)
            Me.Zeitreihen(0) = New Zeitreihe(IO.Path.GetFileName(Me.File))
            Me.Zeitreihen(0).Einheit = "-"

            'Einlesen
            '--------
            ReDim X(0)
            ReDim Y(0)
            Using fortran As New Sydro.SydroZre.Fortran()
                NCount = fortran.getZreBinValues(Me.File, Nothing, Nothing, False, X, Y)
                msg = fortran.ErrorMsg
            End Using

            If NCount < 0 Then
                Throw New Exception("Fehler " & NCount & ": " & msg)
            End If

            'Umwandeln in Zeitreihe
            For i = 0 To NCount - 1
                'Fehlwerte -9999.999 durch NaN ersetzen
                If Math.Abs(Y(i) + 9999.999) < 0.001 Then
                    Me.Zeitreihen(0).AddNode(X(i), Double.NaN)
                Else
                    Me.Zeitreihen(0).AddNode(X(i), Y(i))
                End If
            Next

            'Log 
            Call Log.AddLogEntry(NCount & " Stützstellen gelesen.")

        Catch ex As Exception
            'Fehler weiterschmeissen
            Throw ex

        End Try

    End Sub

#End Region 'Methoden

End Class
