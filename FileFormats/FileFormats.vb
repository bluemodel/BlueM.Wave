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
Namespace Fileformats

    Public Module FileFormats

        ''' <summary>
        ''' FileTypes
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum FileTypes
            UNDETERMINED
            UNKNOWN
            ASC
            BIN
            CSV
            DFS0
            GISMO_WEL
            HYDRO_AS_DAT
            HYSTEM_WEL
            PRMS_OUT
            HYSTEM_REG
            SMUSI_REG
            SMB
            SWMM_DAT_MASS
            SWMM_DAT_TIME
            SWMM_INTERFACE
            SWMM_LID_REPORT
            SWMM_OUT
            SYDROSQLITE
            TEN
            UVF
            WBL
            WEL
            WVP
            ZRE
            ZRXP
        End Enum

        Public Const FileExtASC As String = ".ASC"
        Public Const FileExtBIN As String = ".BIN"   'SYDRO binary format
        Public Const FileExtCSV As String = ".CSV"
        Public Const FileExtDAT As String = ".DAT"
        Public Const FileExtDFS0 As String = ".DFS0" 'DHI MIKE Dfs0 file format
        Public Const FileExtKWL As String = ".KWL"
        Public Const FileExtOUT As String = ".OUT"   'SWMM binary result file or PRMS out file
        Public Const FileExtREG As String = ".REG"
        Public Const FileExtSMB As String = ".SMB"
        Public Const FileExtSQLITE As String = ".DB" 'SYDRO SQLite format
        Public Const FileExtTEN As String = ".TEN"
        Public Const FileExtTXT As String = ".TXT"   'SWMM interface routing file, SWMM LID report file or generic text file 
        Public Const FileExtUVF As String = ".UVF"
        Public Const FileExtWBL As String = ".WBL"   'SYDRO binary WEL format
        Public Const FileExtWEL As String = ".WEL"
        Public Const FileExtWVP As String = ".WVP"   'Wave project file
        Public Const FileExtZRE As String = ".ZRE"
        Public Const FileExtZRX As String = ".ZRX"   'ZRXP format
        Public Const FileExtZRXP As String = ".ZRXP" 'ZRXP format

        ''' <summary>
        ''' FileFilter for file dialogs
        ''' </summary>
        ''' <remarks></remarks>
        Friend Const FileFilter As String =
            "All files (*.*)|*.*|" &
            "Text files (*.txt)|*.txt|" &
            "CSV files (*.csv)|*.csv|" &
            "DHI MIKE DFS0 files (*.dfs0)|*.dfs0|" &
            "HYDRO_AS-2D result files (*.dat)|*.dat|" &
            "Hystem-Extran files (*.dat, *.reg)|*.dat;*.reg|" &
            "PRMS result files (*.dat, *.out)|*.dat;*.out|" &
            "SIMBA files (*.smb)|*.smb|" &
            "SMUSI files (*.asc. *.reg)|*.asc;*.reg|" &
            "SWMM files (*.txt, *.out)|*.txt;*.out|" &
            "SYDRO binary files (*.bin)|*.bin|" &
            "SYDRO binary wel files (*.wbl)|*.wbl|" &
            "SYDRO SQLite files (*.db)|*.db|" &
            "UVF files (*.uvf)|*.uvf|" &
            "WEL files (*.wel, *.kwl)|*.wel;*.kwl|" &
            "Wave project files (*.wvp)|*.wvp|" &
            "ZRE files (*.zre)|*.zre|" &
            "ZRXP files (*.zrx, *.zrxp)|*.zrx;*.zrxp"

        ''' <summary>
        ''' Determines the file type of a file based on the file's extension and contents
        ''' </summary>
        ''' <param name="file">Path to the file</param>
        ''' <returns>the determined file type</returns>
        ''' <remarks>
        ''' Also checks whether the file exists and if the file format is as expected.
        ''' If the file is a WEL file, this function also checks whether the file is contained
        ''' within a WLZIP of the same name and if it is, extracts it.
        ''' </remarks>
        Public Function getFileType(file As String) As FileTypes

            Dim fileExt As String
            Dim fileType As FileTypes

            fileExt = IO.Path.GetExtension(file).ToUpper()

            'Check whether the file exists
            If Not IO.File.Exists(file) Then
                'A WEL file may be zipped within a WLZIP file, so try extracting it from there
                If fileExt = FileExtWEL Then
                    If Not WEL.extractFromWLZIP(file) Then
                        Throw New Exception($"ERROR: File '{file}' not found!")
                    End If
                Else
                    Throw New Exception($"ERROR: File '{file}' not found!")
                End If
            End If

            'Depending on file extension
            Select Case fileExt

                Case FileExtASC
                    If (GISMO_WEL.verifyFormat(file)) Then
                        'GISMO result file in WEL format
                        Log.AddLogEntry(levels.info, $"Detected GISMO result format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.GISMO_WEL
                    Else
                        'Assume SMUSI ASC format
                        Log.AddLogEntry(levels.info, $"Assuming SMUSI ASC format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.ASC
                    End If

                Case FileExtBIN
                    'SYDRO binary file
                    Log.AddLogEntry(levels.info, $"Assuming SYDRO binary format for file {IO.Path.GetFileName(file)}.")
                    fileType = FileTypes.BIN

                Case FileExtCSV
                    'check file format
                    If GISMO_WEL.verifyFormat(file) Then
                        'GISMO result file in CSV format
                        Log.AddLogEntry(levels.info, $"Detected GISMO result format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.GISMO_WEL
                    Else
                        Log.AddLogEntry(levels.info, $"Assuming CSV format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.CSV
                    End If

                Case FileExtDAT
                    'Check file format
                    If (HYDRO_AS_2D.verifyFormat(file)) Then
                        'HYDRO-AS_2D result file
                        Log.AddLogEntry(levels.info, $"Detected HYDRO_AS-2D result format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.HYDRO_AS_DAT
                    ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                        'Hystem-Extran rainfall file
                        Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.HYSTEM_REG
                    ElseIf PRMS.verifyFormat(file) Then
                        'PRMS result file
                        Log.AddLogEntry(levels.info, $"Detected PRMS result format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.PRMS_OUT
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unknown format!")
                    End If

                Case FileExtDFS0
                    Log.AddLogEntry(levels.info, $"Assuming DHI DFS0 format for file {IO.Path.GetFileName(file)}.")
                    fileType = FileTypes.DFS0

                Case FileExtOUT
                    If PRMS.verifyFormat(file) Then
                        'PRMS result format
                        Log.AddLogEntry(levels.info, $"Detected PRMS result format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.PRMS_OUT
                    Else
                        'Assume SWMM5 binary output format
                        Log.AddLogEntry(levels.info, $"Assuming SWMM5 binary output format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.SWMM_OUT
                    End If

                Case FileExtREG
                    'Check file format
                    If (SMUSI_REG.verifyFormat(file)) Then
                        'SMUSI rainfall file
                        Log.AddLogEntry(levels.info, $"Detected SMUSI rainfall format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.SMUSI_REG
                    ElseIf (HystemExtran_REG.verifyFormat(file)) Then
                        'Hystem-Extran rainfall file
                        Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.HYSTEM_REG
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unknown format!")
                    End If

                Case FileExtSMB
                    Log.AddLogEntry(levels.info, $"Assuming SIMBA format for file {IO.Path.GetFileName(file)}.")
                    fileType = FileTypes.SMB

                Case FileExtSQLITE
                    Log.AddLogEntry(levels.info, $"Assuming SYDRO SQLite format for file {IO.Path.GetFileName(file)}.")
                    fileType = FileTypes.SYDROSQLITE

                Case FileExtTXT
                    'Check file format
                    If SWMM_LID_REPORT.verifyFormat(file) Then
                        'SWMM LID report file
                        Log.AddLogEntry(levels.info, $"Detected SWMM LID report file format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.SWMM_LID_REPORT
                    ElseIf SWMM_INTERFACE.verifyFormat(file) Then
                        'SWMM routing interface file
                        Log.AddLogEntry(levels.info, $"Detected SWMM routing interface file format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.SWMM_INTERFACE
                    Else
                        'Other text files can usually be read as CSV files
                        Log.AddLogEntry(levels.info, $"Assuming CSV format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.CSV
                    End If

                Case FileExtUVF
                    'Check file format
                    If UVF.verifyFormat(file) Then
                        fileType = FileTypes.UVF
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unexpected format!")
                    End If

                Case FileExtWBL
                    'Check format
                    If WBL.verifyFormat(file) Then
                        'SYDRO binary WEL file
                        Log.AddLogEntry(levels.info, $"Detected SYDRO binary WEL format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.WBL
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unexpected format!")
                    End If

                Case FileExtWEL, FileExtKWL
                    'Check file format
                    If (WEL.verifyFormat(file)) Then
                        'WEL file
                        Log.AddLogEntry(levels.info, $"Detected BlueM/Talsim WEL format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.WEL
                    ElseIf (HystemExtran_WEL.verifyFormat(file)) Then
                        'Hystem-Extran rainfall file
                        Log.AddLogEntry(levels.info, $"Detected Hystem-Extran rainfall format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.HYSTEM_WEL
                    ElseIf (WBL.verifyFormat(file)) Then
                        'SYDRO binary WEL file
                        Log.AddLogEntry(levels.info, $"Detected SYDRO binary WEL format for file {IO.Path.GetFileName(file)}.")
                        fileType = FileTypes.WBL
                    Else
                        Throw New Exception($"File {IO.Path.GetFileName(file)} has an unknown format!")
                    End If

                Case FileExtWVP
                    Log.AddLogEntry(levels.info, $"Assuming Wave project file format for file {IO.Path.GetFileName(file)}.")
                    fileType = FileTypes.WVP

                Case FileExtZRE
                    Log.AddLogEntry(levels.info, $"Assuming ZRE format for file {IO.Path.GetFileName(file)}.")
                    fileType = FileTypes.ZRE

                Case FileExtZRX, FileExtZRXP
                    Log.AddLogEntry(levels.info, $"Assuming ZRXP format for file {IO.Path.GetFileName(file)}.")
                    fileType = FileTypes.ZRXP

                Case Else
                    'Unknown filetype
                    Log.AddLogEntry(levels.warning, $"Unable to determine file type of file {IO.Path.GetFileName(file)}!")
                    fileType = FileTypes.UNKNOWN

            End Select

            Return fileType

        End Function

    End Module

End Namespace
