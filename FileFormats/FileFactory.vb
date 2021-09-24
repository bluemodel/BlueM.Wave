'Copyright (c) BlueM Dev Group
'Website: https://bluemodel.org
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
''' Factory for creating FileFormat instances
''' </summary>
Public Module FileFactory

    Public Const FileExtASC As String = ".ASC"
    Public Const FileExtCSV As String = ".CSV"
    Public Const FileExtREG As String = ".REG"
    Public Const FileExtDAT As String = ".DAT"
    Public Const FileExtDFS0 As String = ".DFS0" 'DHI MIKE Dfs0 file format
    Public Const FileExtSMB As String = ".SMB"
    Public Const FileExtUVF As String = ".UVF"
    Public Const FileExtWEL As String = ".WEL"
    Public Const FileExtKWL As String = ".KWL"
    Public Const FileExtZRE As String = ".ZRE"
    Public Const FileExtTEN As String = ".TEN"
    Public Const FileExtOUT As String = ".OUT"   'SWMM binary result file or PRMS out file
    Public Const FileExtTXT As String = ".TXT"   'SWMM routing file
    Public Const FileExtBIN As String = ".BIN"   'SYDRO binary format
    Public Const FileExtSQLITE As String = ".DB" 'SYDRO SQLite format
    Public Const FileExtZRX As String = ".ZRX"   'ZRXP format
    Public Const FileExtZRXP As String = ".ZRXP" 'ZRXP format
    Public Const FileExtWVP As String = ".WVP"   'Wave project file

    ''' <summary>
    ''' Obsolete: Only Maintained for backwards-compatibility
    ''' Use getFileInstance() instead
    ''' </summary>
    Public Function getDateiInstanz(ByVal file As String) As FileFormatBase
        Return getFileInstance(file)
    End Function

    ''' <summary>
    ''' Creates a FileFormatBase-inherited instance based on the file extension
    ''' </summary>
    ''' <param name="file">Path to the file</param>
    ''' <returns>A FileFormatBase-inherited instance of the file</returns>
    ''' <remarks>
    ''' Also checks whether the file exists and if the file format is as expected.
    ''' If the file is a WEL file, this function also checks whether the file is contained
    ''' within a WLZIP of the same name and if it is, extracts it.
    ''' </remarks>
    Public Function getFileInstance(ByVal file As String) As FileFormatBase

        Dim FileInstance As FileFormatBase
        Dim FileExt As String

        FileExt = Path.GetExtension(file).ToUpper()

        'Check whether the file exists
        If (Not System.IO.File.Exists(file)) Then
            If FileExt = FileExtWEL Then
                'A WEL file may be zipped within a WLZIP file, so try extracting it from there
                If Not WEL.extractFromWLZIP(file) Then
                    Throw New Exception($"ERROR: File '{file}' not found!")
                End If
            Else
                Throw New Exception($"ERROR: File '{file}' not found!")
            End If
        End If

        'Depending on file extension
        Select Case FileExt

            Case FileExtASC
                Dim isSSV As Boolean
                If (WEL_GISMO.verifyFormat(file, isSSV)) Then
                    'GISMO result file in WEL format (separator is " ")
                    FileInstance = New WEL_GISMO(file, isSSV)
                Else
                    FileInstance = New ASC(file)
                End If

            Case FileExtDAT
                'Check file format
                If (HYDRO_AS_2D.verifyFormat(file)) Then
                    'HYDRO-AS_2D result file
                    FileInstance = New HYDRO_AS_2D(file)
                ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                    'Hystem-Extran rainfall file
                    FileInstance = New HystemExtran_REG(file)
                ElseIf PRMS.verifyFormat(file) Then
                    'PRMS result file
                    FileInstance = New PRMS(file)
                Else
                    Throw New Exception("ERROR: File format not recognized! The file is neither a HYDRO_AS-2D file nor a Hystem-Extran rainfall file!")
                End If

            Case FileExtDFS0
                FileInstance = New DFS0(file)

            Case FileExtREG
                'Check file format
                If (REG_SMUSI.verifyFormat(file)) Then
                    'SMUSI rainfall file
                    FileInstance = New REG_SMUSI(file)
                ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                    'Hystem-Extran rainfall file
                    FileInstance = New HystemExtran_REG(file)
                Else
                    Throw New Exception("ERROR: File format not recognized! The file is neither a SMUSI nor a Hystem-Extran rainfall file!")
                End If

            Case FileExtSMB
                FileInstance = New SMB(file)

            Case FileExtUVF
                'Check file format
                If UVF.verifyFormat(file) Then
                    FileInstance = New UVF(file)
                Else
                    Throw New Exception("ERROR: UVF file has an unexpected format!")
                End If


            Case FileExtWEL, FileExtKWL
                'Check file format
                If (WEL.verifyFormat(file)) Then
                    'WEL file
                    FileInstance = New WEL(file)
                ElseIf (HystemExtran_WEL.verifyFormat(file)) Then
                    'Hystem-Extran rainfall file
                    FileInstance = New HystemExtran_WEL(file)
                Else
                    Throw New Exception("ERROR: WEL file has an unexpected format!")
                End If

            Case FileExtZRE
                FileInstance = New ZRE(file)

            Case FileExtCSV
                'check file format
                Dim isssv As Boolean
                If (WEL_GISMO.verifyFormat(file, isssv)) Then
                    'GISMO result file in CSV format (separator is a ";")
                    FileInstance = New WEL_GISMO(file, isssv)
                Else
                    FileInstance = New CSV(file)
                End If

            Case FileExtOUT
                If PRMS.verifyFormat(file) Then
                    FileInstance = New PRMS(file)
                Else
                    FileInstance = New SWMM_OUT(file)
                End If

            Case FileExtTXT
                'Check file format
                If (SWMM_TXT.verifyFormat(file)) Then
                    'SWMM file
                    FileInstance = New SWMM_TXT(file)
                Else
                    'Other text files can usually be read as CSV files
                    FileInstance = New CSV(file)
                End If

            Case FileExtBIN
                FileInstance = New BIN(file)

            Case FileExtSQLITE
                FileInstance = New SydroSQLite(file)

            Case FileExtZRX, FileExtZRXP
                FileInstance = New ZRXP(file)

            Case Else
                'If all else fails, attempt to read as CSV
                FileInstance = New CSV(file)

        End Select

        Return FileInstance

    End Function

End Module
