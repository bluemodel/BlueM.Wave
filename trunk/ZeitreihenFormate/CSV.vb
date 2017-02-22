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
''' Klasse für generisches Textformat
''' </summary>
Public Class CSV
    Inherits Dateiformat

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return True
        End Get
    End Property

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String)
        MyBase.New(FileName)

        'Voreinstellungen
        Me.Datumsformat = Konstanten.Datumsformate("default")
    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim i As Integer
        Dim Zeile As String = ""
        Dim ZeileSpalten As String = ""
        Dim ZeileEinheiten As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Spaltenüberschriften auslesen
            For i = 1 To Math.Max(Me.iZeileDaten, Me.iZeileUeberschriften + 1)
                Zeile = StrReadSync.ReadLine.ToString
                If (i = Me.iZeileUeberschriften) Then ZeileSpalten = Zeile
                If (i = Me.iZeileEinheiten) Then ZeileEinheiten = Zeile
            Next

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

            'Spaltennamen auslesen
            '---------------------
            Dim anzSpalten As Integer
            Dim Namen() As String
            Dim Einheiten() As String

            If (Me.Zeichengetrennt) Then
                'Zeichengetrennt
                Namen = ZeileSpalten.Split(New Char() {Me.Trennzeichen.Character})
                Einheiten = ZeileEinheiten.Split(New Char() {Me.Trennzeichen.Character})
                anzSpalten = Namen.Length
            Else
                'Spalten mit fester Breite
                anzSpalten = Math.Ceiling(ZeileSpalten.Length / Me.Spaltenbreite)
                ReDim Namen(anzSpalten - 1)
                ReDim Einheiten(anzSpalten - 1)
                For i = 0 To anzSpalten - 1
                    Namen(i) = ZeileSpalten.Substring(i * Me.Spaltenbreite, Math.Min(Me.Spaltenbreite, ZeileSpalten.Substring(i * Me.Spaltenbreite).Length))
                    Einheiten(i) = ZeileEinheiten.Substring(i * Me.Spaltenbreite, Math.Min(Me.Spaltenbreite, ZeileSpalten.Substring(i * Me.Spaltenbreite).Length))
                Next
            End If

            'Spalten abspeichern
            ReDim Me.Spalten(anzSpalten - 1)
            For i = 0 To Namen.Length - 1
                Me.Spalten(i).Name = Namen(i).Trim()
                Me.Spalten(i).Index = i
            Next

            For i = 0 To Einheiten.Length - 1
                Me.Spalten(i).Einheit = Einheiten(i).Trim()
            Next

            'TODO: gegebenes Datumsformat an dieser Stelle testen

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    'CSV-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

        Dim i As Integer
        Dim Zeile As String
        Dim ok As Boolean
        Dim numberformat As NumberFormatInfo
        Dim datum As DateTime
        Dim Werte() As String

        Try

            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Anzahl Zeitreihen bestimmen
            ReDim Me.Zeitreihen(Me.SpaltenSel.Length - 1)

            'Zeitreihen instanzieren
            For i = 0 To Me.SpaltenSel.Length - 1
                Me.Zeitreihen(i) = New Zeitreihe(Me.SpaltenSel(i).Name)
            Next

            'Einheiten übergeben
            If (Me.UseEinheiten) Then
                For i = 0 To Me.SpaltenSel.Length - 1
                    Me.Zeitreihen(i).Einheit = Me.SpaltenSel(i).Einheit
                Next
            End If

            'Use default number format by default
            numberformat = Konstanten.Zahlenformat.Clone()
            If Me.Dezimaltrennzeichen.Character = Chr(44) Then
                'change decimal separator to comma
                numberformat.NumberDecimalSeparator = ","
            End If

            'Einlesen
            '--------

            'Header
            For i = 0 To Me.nZeilenHeader - 1
                StrReadSync.ReadLine()
            Next

            'Daten
            Do
                Zeile = StrReadSync.ReadLine.ToString()

                If (Me.Zeichengetrennt) Then

                    'Zeichengetrennt
                    '---------------
                    Werte = Zeile.Split(New Char() {Me.Trennzeichen.Character})

                    If (Werte.Length > 0 And Zeile.Trim.Length > 1) Then
                        'Erste Spalte: Datum_Zeit
                        ok = DateTime.TryParseExact(Werte(Me.XSpalte).Trim(), Me.Datumsformat, Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                        If (Not ok) Then
                            Throw New Exception("Could not parse the date '" & Werte(Me.XSpalte) & "' using the given date format '" & Me.Datumsformat & "'! Please check the date format!")
                        End If
                        'Restliche Spalten: Werte
                        For i = 0 To Me.SpaltenSel.Length - 1
                            Me.Zeitreihen(i).AddNode(datum, StringToDouble(Werte(Me.SpaltenSel(i).Index), numberformat))
                        Next
                    End If

                Else
                    'Spalten mit fester Breite
                    '-------------------------
                    'Erste Spalte: Datum_Zeit
                    ok = DateTime.TryParseExact(Zeile.Substring(0, Me.Spaltenbreite), Me.Datumsformat, Konstanten.Zahlenformat, Globalization.DateTimeStyles.None, datum)
                    If (Not ok) Then
                        Throw New Exception("Could not parse the date '" & Zeile.Substring(0, Me.Spaltenbreite) & "' using the given date format '" & Me.Datumsformat & "'! Please check the date format!")
                    End If
                    'Restliche Spalten: Werte
                    For i = 0 To Me.SpaltenSel.Length - 1
                        Me.Zeitreihen(i).AddNode(datum, StringToDouble(Zeile.Substring(Me.SpaltenSel(i).Index * Me.Spaltenbreite + SpaltenOffset, Math.Min(Me.Spaltenbreite, Zeile.Substring(Me.SpaltenSel(i).Index * Me.Spaltenbreite + SpaltenOffset).Length)), numberformat))
                    Next
                End If

            Loop Until StrReadSync.Peek() = -1

            StrReadSync.Close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Unable to read file!" & eol & eol & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

#End Region 'Methoden

End Class