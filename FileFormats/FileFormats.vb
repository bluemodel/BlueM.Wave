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
            ASC
            BIN
            CSV
            DAT_HYDRO_AS
            DAT_SWMM_MASS
            DAT_SWMM_TIME
            DFS0
            PRMS_OUT
            REG_HYSTEM
            REG_SMUSI
            SWMM_INTERFACE
            SWMM_LID_REPORT
            UVF
            WBL
            WEL
            WVP
            ZRE
            ZRXP
        End Enum

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
        Public Const FileExtTXT As String = ".TXT"   'SWMM interface routing file, SWMM LID report file or generic text file 
        Public Const FileExtBIN As String = ".BIN"   'SYDRO binary format
        Public Const FileExtSQLITE As String = ".DB" 'SYDRO SQLite format
        Public Const FileExtWBL As String = ".WBL"   'SYDRO binary WEL format
        Public Const FileExtZRX As String = ".ZRX"   'ZRXP format
        Public Const FileExtZRXP As String = ".ZRXP" 'ZRXP format
        Public Const FileExtWVP As String = ".WVP"   'Wave project file

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

    End Module

End Namespace
