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
        ''' Creates a FileFormatBase-inherited instance based on the file type
        ''' </summary>
        ''' <param name="file">Path to the file</param>
        ''' <param name="fileType">Optional file type. If not provided, the type is determined using `FileFormats.getFileType()`</param>
        ''' <returns>A FileFormatBase-inherited instance of the file</returns>
        Public Function getFileInstance(file As String, Optional fileType As FileTypes = FileTypes.UNDETERMINED) As FileFormatBase

            Dim FileInstance As FileFormatBase

            'determine file type if not passed as argument
            If fileType = FileTypes.UNDETERMINED Then
                fileType = FileFormats.getFileType(file)
            End If

            'Depending on file type
            Select Case fileType

                Case FileTypes.UNKNOWN
                    Log.AddLogEntry(levels.warning, $"File {IO.Path.GetFileName(file)} has an unknown file type, attempting to load as CSV.")
                    FileInstance = New CSV(file)
                Case FileTypes.ASC
                    FileInstance = New ASC(file)
                Case FileTypes.BIN
                    FileInstance = New BIN(file)
                Case FileTypes.CSV
                    FileInstance = New CSV(file)
                Case FileTypes.HYDRO_AS_DAT
                    FileInstance = New HYDRO_AS_2D(file)
                Case FileTypes.DFS0
                    FileInstance = New DFS0(file)
                Case FileTypes.GISMO_WEL
                    FileInstance = New GISMO_WEL(file)
                Case FileTypes.HYSTEM_REG
                    FileInstance = New HystemExtran_REG(file)
                Case FileTypes.HYSTEM_WEL
                    FileInstance = New HystemExtran_WEL(file)
                Case FileTypes.PRMS_OUT
                    FileInstance = New PRMS(file)
                Case FileTypes.SMB
                    FileInstance = New SMB(file)
                Case FileTypes.SMUSI_REG
                    FileInstance = New SMUSI_REG(file)
                Case FileTypes.SWMM_DAT_MASS
                    Throw New NotImplementedException("Reading files of type DAT_SWMM_MASS is not yet implemented!")
                Case FileTypes.SWMM_DAT_TIME
                    Throw New NotImplementedException("Reading files of type DAT_SWMM_TIME is not yet implemented!")
                Case FileTypes.SWMM_INTERFACE
                    FileInstance = New SWMM_INTERFACE(file)
                Case FileTypes.SWMM_LID_REPORT
                    FileInstance = New SWMM_LID_REPORT(file)
                Case FileTypes.SWMM_OUT
                    FileInstance = New SWMM_OUT(file)
                Case FileTypes.SYDROSQLITE
                    FileInstance = New SydroSQLite(file)
                Case FileTypes.TEN
                    Throw New Exception("Native TeeChart files (TEN) must to be loaded using `Wave.Import_File()`!")
                Case FileTypes.UVF
                    FileInstance = New UVF(file)
                Case FileTypes.WBL
                    FileInstance = New WBL(file)
                Case FileTypes.WEL
                    FileInstance = New WEL(file)
                Case FileTypes.WVP
                    Throw New Exception("Wave project files (WVP) need to be loaded using `Wave.Import_File()` or `Wave.Load_WVP()`!")
                Case FileTypes.ZRE
                    FileInstance = New ZRE(file)
                Case FileTypes.ZRXP
                    FileInstance = New ZRXP(file)
                Case Else
                    Throw New Exception($"Unknown file type {fileType}!")

            End Select

            Return FileInstance

        End Function

    End Module

End Namespace