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
''' Factory zur Erzeugung von Datei-Instanzen
''' </summary>
Public Module Dateifactory

    Public Const FileExtASC As String = ".ASC"
    Public Const FileExtCSV As String = ".CSV"
    Public Const FileExtREG As String = ".REG"
    Public Const FileExtDAT As String = ".DAT"
    Public Const FileExtSMB As String = ".SMB"
    Public Const FileExtUVF As String = ".UVF"
    Public Const FileExtWEL As String = ".WEL"
    Public Const FileExtKWL As String = ".KWL"
    Public Const FileExtZRE As String = ".ZRE"
    Public Const FileExtTEN As String = ".TEN"
    Public Const FileExtOUT As String = ".OUT" 'SWMM binäre Ergebnisdatei
    Public Const FileExtTXT As String = ".TXT" 'SWMM Routingfiles
    Public Const FileExtBIN As String = ".BIN" 'SYDRO Binärformat
    Public Const FileExtZRXP As String = ".ZRX" 'ZRXP format
    Public Const FileExtWVP As String = ".WVP" 'Wave project file

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
        FileExt = Path.GetExtension(file).ToUpper()
        Select Case FileExt

            Case FileExtASC
                Dim isSSV As Boolean
                If (WEL_GISMO.verifyFormat(file, isSSV)) Then
                    ' GISMO result file in WEL format (separator is " ")
                    Datei = New WEL_GISMO(file, isSSV)
                Else
                    Datei = New ASC(file)
                End If

            Case FileExtDAT
                'Dateiformat prüfen:
                If (HYDRO_AS_2D.verifyFormat(file)) Then
                    'HYDRO-AS_2D Ergebnisdatei
                    Datei = New HYDRO_AS_2D(file)
                ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                    'Hystem-Extran Regenreihe
                    Datei = New HystemExtran_REG(file)
                Else
                    Throw New Exception("Dateiformat nicht erkannt: Es handelt es sich weder um eine HYDRO-AS-2D noch um eine Hystem-Regendatei")
                End If

            Case FileExtREG
                'Dateiformat prüfen:
                If (REG_SMUSI.verifyFormat(file)) Then
                    'SMUSI-Regenreihe
                    Datei = New REG_SMUSI(file)
                ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                    'Hystem-Extran-Regenreihe
                    Datei = New HystemExtran_REG(file)
                Else
                    Throw New Exception("Dateiformat nicht erkannt: Es handelt es sich weder um eine SMUSI- noch um eine Hystem-Regendatei")
                End If

            Case FileExtSMB
                Datei = New SMB(file)

            Case FileExtUVF
                If UVF.verifyFormat(file) Then
                    Datei = New UVF(file)
                Else
                    Throw New Exception("UVF-Datei liegt nicht im erwarteten Format vor!")
                End If


            Case FileExtWEL, FileExtKWL
                If (WEL.verifyFormat(file)) Then
                    'BlueM WEL file
                    Datei = New WEL(file)
                ElseIf (WEL_TALSIM.verifyFormat(file)) Then
                    'TASLIM WEL file
                    Datei = New WEL_TALSIM(file)
                ElseIf (HystemExtran_WEL.verifyFormat(file)) Then
                    'HYSTEM EXTRAN rain time series
                    Datei = New HystemExtran_WEL(file)
                Else
                    Throw New Exception("Unknown file format! Please check!")
                End If

            Case FileExtZRE
                Datei = New ZRE(file)

            Case FileExtCSV
                Dim isssv As Boolean
                If (WEL_GISMO.verifyFormat(file, isssv)) Then
                    ' GISMO result file in CSV format (separator is a ";")
                    Datei = New WEL_GISMO(file, isssv)
                Else
                    Datei = New CSV(file)
                End If

            Case FileExtOUT
                Datei = New SWMM_OUT(file)

            Case FileExtTXT
                'Dateiformat prüfen:
                If (SWMM_TXT.verifyFormat(file)) Then
                    'SWMM Datei
                    Datei = New SWMM_TXT(file)
                Else
                    'Textdateien können üblicherweise als CSV gelesen werden
                    Datei = New CSV(file)
                End If

            Case FileExtBIN
                Datei = New BIN(file)

            Case FileExtZRXP
                Datei = New ZRXP(file)

            Case Else
                'Wenn alle Stricke reissen, Import als CSV versuchen
                Datei = New CSV(file)

        End Select

        Return Datei

    End Function

End Module
