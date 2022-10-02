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

Namespace Fileformats

    ''' <summary>
    ''' Factory for creating FileFormat instances
    ''' </summary>
    Public Module FileFactory

        ''' <summary>
        ''' Obsolete: Only Maintained for backwards-compatibility
        ''' Use getFileInstance() instead
        ''' </summary>
        Public Function getDateiInstanz(file As String) As FileFormatBase
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
        Public Function getFileInstance(file As String) As FileFormatBase

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
                        Log.AddLogEntry(levels.info, $"Detected GISMO result format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New WEL_GISMO(file, isSSV)
                    Else
                        'Assume SMUSI ASC format
                        Log.AddLogEntry(levels.info, $"Assuming SMUSI ASC format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New ASC(file)
                    End If

                Case FileExtDAT
                    'Check file format
                    If (HYDRO_AS_2D.verifyFormat(file)) Then
                        'HYDRO-AS_2D result file
                        Log.AddLogEntry(levels.info, $"Detected HYDRO_AS-2D result format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New HYDRO_AS_2D(file)
                    ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                        'Hystem-Extran rainfall file
                        Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New HystemExtran_REG(file)
                    ElseIf PRMS.verifyFormat(file) Then
                        'PRMS result file
                        Log.AddLogEntry(levels.info, $"Detected PRMS result format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New PRMS(file)
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unknown format!")
                    End If

                Case FileExtDFS0
                    FileInstance = New DFS0(file)

                Case FileExtREG
                    'Check file format
                    If (REG_SMUSI.verifyFormat(file)) Then
                        'SMUSI rainfall file
                        Log.AddLogEntry(levels.info, $"Detected SMUSI rainfall format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New REG_SMUSI(file)
                    ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                        'Hystem-Extran rainfall file
                        Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New HystemExtran_REG(file)
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unknown format!")
                    End If

                Case FileExtSMB
                    FileInstance = New SMB(file)

                Case FileExtUVF
                    'UVF format
                    If Not UVF.verifyFormat(file) Then
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unexpected format!")
                    End If
                    FileInstance = New UVF(file)

                Case FileExtWEL, FileExtKWL
                    'Check file format
                    If (WEL.verifyFormat(file)) Then
                        'WEL file
                        Log.AddLogEntry(levels.info, $"Detected BlueM/Talsim WEL format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New WEL(file)
                    ElseIf (HystemExtran_WEL.verifyFormat(file)) Then
                        'Hystem-Extran rainfall file
                        Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New HystemExtran_WEL(file)
                    ElseIf (WBL.verifyFormat(file)) Then
                        'SYDRO binary WEL file
                        Log.AddLogEntry(levels.info, $"Detected SYDRO binary WEL format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New WBL(file)
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unknown format!")
                    End If

                Case FileExtZRE
                    FileInstance = New ZRE(file)

                Case FileExtCSV
                    'check file format
                    Dim isssv As Boolean
                    If (WEL_GISMO.verifyFormat(file, isssv)) Then
                        'GISMO result file in CSV format (separator is a ";")
                        Log.AddLogEntry(levels.info, $"Detected GISMO result format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New WEL_GISMO(file, isssv)
                    Else
                        FileInstance = New CSV(file)
                    End If

                Case FileExtOUT
                    If PRMS.verifyFormat(file) Then
                        'PRMS result format
                        Log.AddLogEntry(levels.info, $"Detected PRMS result format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New PRMS(file)
                    Else
                        'Assume SWMM5 binary output format
                        Log.AddLogEntry(levels.info, $"Assuming SWMM5 binary output format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New SWMM_OUT(file)
                    End If

                Case FileExtTXT
                    'Check file format
                    If SWMM_LID_REPORT.verifyFormat(file) Then
                        'SWMM LID report file
                        Log.AddLogEntry(levels.info, $"Detected SWMM LID report file format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New SWMM_LID_REPORT(file)
                    ElseIf SWMM_INTERFACE.verifyFormat(file) Then
                        'SWMM routing interface file
                        Log.AddLogEntry(levels.info, $"Detected SWMM routing interface file format for file {IO.Path.GetFileName(file)}.")
                        FileInstance = New SWMM_INTERFACE(file)
                    Else
                        'Other text files can usually be read as CSV files
                        FileInstance = New CSV(file)
                    End If

                Case FileExtBIN
                    'SYDRO binary file
                    FileInstance = New BIN(file)

                Case FileExtWBL
                    'SYDRO binary WEL file
                    If Not WBL.verifyFormat(file) Then
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unexpected format!")
                    End If
                    FileInstance = New WBL(file)

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

End Namespace