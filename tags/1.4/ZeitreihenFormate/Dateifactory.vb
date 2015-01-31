﻿'Copyright (c) 2011, ihwb, TU Darmstadt
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
''' Factory zur Erzeugung von Datei-Instanzen
''' </summary>
Public Module Dateifactory

    Public Const FileExtASC As String = ".ASC"
    Public Const FileExtCSV As String = ".CSV"
    Public Const FileExtREG As String = ".REG"
    Public Const FileExtDAT As String = ".DAT"
    Public Const FileExtRVA As String = ".RVA"
    Public Const FileExtSMB As String = ".SMB"
    Public Const FileExtWEL As String = ".WEL"
    Public Const FileExtKWL As String = ".KWL"
    Public Const FileExtZRE As String = ".ZRE"
    Public Const FileExtTEN As String = ".TEN"
    Public Const FileExtDTL As String = ".DTL" 'DWD-Daten: Temperatur und Luftfeuchte
    Public Const FileExtOUT As String = ".OUT" 'SWMM binäre Ergebnisdatei
    Public Const FileExtTXT As String = ".TXT" 'SWMM Routingfiles

    Public Const FileExtnetCDF As String = ".NC"

    ''' <summary>
    ''' Erzeugt eine zur Dateiendung passende Datei-Instanz
    ''' </summary>
    ''' <param name="file">Pfad zur Datei</param>
    ''' <returns>Eine Instanz der Datei</returns>
    Public Function getDateiInstanz(ByVal file As String) As Dateiformat

        Dim Datei As Dateiformat
        Dim FileExt As String

        'Prüfen, ob die Datei existiert
        If (Not System.IO.File.Exists(file)) Then
            Throw New Exception("Datei '" & file & "' nicht gefunden!")
        End If

        'Fallunterscheidung je nach Dateiendung
        FileExt = System.IO.Path.GetExtension(file).ToUpper()
        Select Case FileExt

            Case FileExtASC
                Datei = New ASC(file)

            Case FileExtDAT
                Datei = New HystemExtran_REG(file)

            Case FileExtREG
                'Dateiformat prüfen:
                If (SMUSI_REG.verifyFormat(file)) Then
                    'SMUSI-Regenreihe
                    Datei = New SMUSI_REG(file)
                ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                    'Hystem-Extran-Regenreihe
                    Datei = New HystemExtran_REG(file)
                Else
                    Throw New Exception("Es handelt es sich weder um eine SMUSI- noch um eine Hystem-Regendatei")
                End If

            Case FileExtRVA
                Datei = New RVA(file)

            Case FileExtSMB
                Datei = New SMB(file)

            Case FileExtWEL, FileExtKWL
                'Datei = New WEL(file)
                'Dateiformat prüfen:
                If (HystemExtran_WEL.verifyFormat(file)) Then
                    'Hystem-Extran-Regenreihe
                    Datei = New HystemExtran_WEL(file)
                Else
                    Datei = New WEL(file)
                End If

            Case FileExtZRE
                Datei = New ZRE(file)

            Case FileExtCSV
                Datei = New CSV(file)

            Case FileExtDTL
                Datei = New DWD_T_L(file)

            Case FileExtOUT
                Datei = New SWMM_OUT(file)

            Case FileExtTXT
                Datei = New SWMM_TXT(file)

            Case Else
                Throw New Exception("Die Dateiendung '" & FileExt & "' ist nicht bekannt!")

        End Select

        Return Datei

    End Function

End Module