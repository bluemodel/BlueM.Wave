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
Imports System.Globalization

''' <summary>
''' Klasse für UF Dateiformat
''' Formatbeschreibung: http://aquaplan.de/public_papers/imex/sectionUVF.html
''' </summary>
Public Class UVF
    Inherits Dateiformat

    ''' <summary>
    ''' Ob der Importdialog genutzt werden soll
    ''' </summary>
    ''' <value></value>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    ' UVF-spezifische Metadaten
    Private _defArt As String
    Private _jahrhundert As Integer
    Private _ort As String
    Private _lage_X As Double
    Private _lage_Y As Double
    Private _lage_Z As Double

    ''' <summary>
    ''' Instanziert ein neues UVF Objekt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal file As String, Optional ByVal ReadAllNow As Boolean = False)

        Call MyBase.New(file)

        'Voreinstellungen
        Me.Datumsformat = Konstanten.Datumsformate("UVF")
        Me.UseEinheiten = True

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If

    End Sub

    ''' <summary>
    ''' Prüft, ob es sich um eine UVF-Datei handelt
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns>Boolean</returns>
    ''' <remarks>Prüfung erfolgt anhand der Zeile *Z</remarks>
    Public Shared Function verifyFormat(ByVal file As String) As Boolean

        Dim i As Integer
        Dim Zeile As String
        Dim headerFound As Boolean = False

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Do
                Zeile = StrReadSync.ReadLine.ToString()
                i += 1
                If Zeile.StartsWith("$") Then Continue Do ' Kommentarzeile
                If Zeile.ToLower.StartsWith("*z") Then      ' Hier fängt der Header an
                    headerFound = True
                    Exit Do
                End If

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If Not headerFound Then
                Throw New Exception("Die Datei enthält keine Header-Zeile, die mit '*Z' beginnt!")
            End If

            Return True

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Liest die Metadaten der in der Datei enthaltenen Zeitreihe aus
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String
        Dim headerFound As Boolean = False

        ReDim Me.Spalten(1) ' Jede UVF-Datei enthält nur eine Zeitreihe

        'X-Spalte konfigurieren
        Me.Spalten(0).Name = "Zeit"
        Me.Spalten(0).Einheit = ""
        Me.Spalten(0).Index = 0

        'Y-Spalte konfigurieren
        Me.Spalten(1).Index = 1

        'Header einlesen
        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            Do
                Zeile = StrReadSync.ReadLine.ToString()
                i += 1
                If Zeile.StartsWith("$") Then Continue Do ' Kommentarzeile
                If Zeile.ToLower.StartsWith("*z") Then      ' Hier fängt der Header an
                    headerFound = True
                    iZeileUeberschriften = i + 1
                    iZeileEinheiten = i + 1
                    iZeileDaten = i + 4
                    Continue Do
                End If
                If i = iZeileUeberschriften Then
                    'Zeitreihenname einlesen
                    Me.Spalten(1).Name = Zeile.Substring(0, 15).Trim()
                    'Einheit einlesen
                    Me.Spalten(1).Einheit = Zeile.Substring(15, 15).Trim()
                    'DefArt oder Anfangsjahrhundert einlesen, falls vorhanden
                    Me._jahrhundert = 0
                    If Zeile.Length > 30 Then
                        If Zeile.Substring(30, 1) = "I" Or _
                           Zeile.Substring(30, 1) = "K" Or _
                           Zeile.Substring(30, 1) = "M" Then
                            Me._defArt = Zeile.Substring(30, 1)
                        ElseIf Zeile.Substring(30, 4) = "1900" Or _
                               Zeile.Substring(30, 4) = "2000" Then
                            'Anfangsjahrhundert ist angegeben
                            Me._jahrhundert = Zeile.Substring(30, 4)
                        End If
                    End If
                    'Anfangsjahrhundert auf 1900 setzen, falls nicht angegeben
                    If Me._jahrhundert = 0 Then
                        Me._jahrhundert = 1900
                        Log.AddLogEntry("UVF: Anfangsjahrhundert ist nicht angegeben, es wird 1900 angenommen.")
                    End If
                    Continue Do
                End If
                If i = iZeileUeberschriften + 1 Then
                    'Ort und Lage einlesen
                    Try
                        Me._ort = Zeile.Substring(0, Math.Min(Zeile.Length, 15)).Trim()
                        If Me._ort <> "" Then
                            'append Ort to series title
                            Me.Spalten(1).Name &= " - " & Me._ort
                        End If
                        Me._lage_X = Zeile.Substring(15, 10)
                        Me._lage_Y = Zeile.Substring(25, 10)
                        Me._lage_Z = Zeile.Substring(35)
                        Exit Do
                    Catch ex As Exception
                        'do nothing
                    End Try
                End If

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            If Not headerFound Then
                Throw New Exception("Die Datei enthält keine Header-Zeile, die mit '*Z' beginnt!")
            End If

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub

    ''' <summary>
    ''' Liest die Datei ein
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Read_File()

        Dim i, year, year_prev As Integer
        Dim Zeile As String
        Dim datumstring, datumstringExt As String
        Dim datum As DateTime
        Dim ok As Boolean
        Dim wert As Double

        Try
            'Zeitreihe instanzieren
            ReDim Me.Zeitreihen(0)
            Me.Zeitreihen(0) = New Zeitreihe(Me.Spalten(1).Name)
            Me.Zeitreihen(0).Einheit = Me.Spalten(1).Einheit

            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Einlesen
            '--------

            'Header
            For i = 0 To Me.nZeilenHeader - 1
                StrReadSync.ReadLine()
            Next

            'Daten
            year_prev = Integer.Parse(Me._jahrhundert.ToString.Substring(2)) 'Aus Anfangsjahrhundert
            Do
                Zeile = StrReadSync.ReadLine.ToString()
                'Datum lesen
                datumstring = Zeile.Substring(0, 10)
                year = Integer.Parse(datumstring.Substring(0, 2))
                'Jahrhundert bestimmen
                If year - year_prev < 0 Then
                    'neues Jahrhundert
                    Me._jahrhundert += 100
                End If
                year_prev = year
                'Jahrhundert voranstellen
                datumstringExt = Me._jahrhundert.ToString.Substring(0, 2) & datumstring
                'parse it
                ok = DateTime.TryParseExact(datumstringExt, Me.Datumsformat, Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                If (Not ok) Then
                    Throw New Exception("Kann das Datum '" & datumstring & "' mit dem gegebenen Datumsformat '" & Me.Datumsformat & "' nicht parsen! Bitte Datumsformat anpassen!")
                End If
                'Wert lesen
                wert = Konstanten.StringToDouble(Zeile.Substring(10))
                'Stützstelle abspeichern
                Me.Zeitreihen(0).AddNode(datum, wert)

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub


End Class
