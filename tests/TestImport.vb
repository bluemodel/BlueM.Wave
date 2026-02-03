'BlueM.Wave
'Copyright (C) BlueM Dev Group
'<https://www.bluemodel.org>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
'
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

''' <summary>
''' Tests for reading different time series formats
''' </summary>
<TestClass()>
Public Class TestImport

    ''' <summary>
    ''' Tests finding a WEL file within a WLZIP file and extracting it
    ''' </summary>
    <TestMethod()>
    Public Sub TestWLZIP()

        Dim file_wel As String = IO.Path.Combine(TestData.getTestDataDir(), "Talsim", "TALSIM.WEL")

        'delete existing file before testing
        If IO.File.Exists(file_wel) Then
            IO.File.Delete(file_wel)
        End If

        'attempt to extract from WLZIP
        Dim success As Boolean = Fileformats.WEL.extractFromWLZIP(file_wel)

        Assert.IsTrue(success)
        Assert.IsTrue(IO.File.Exists(file_wel))

    End Sub


    ''' <summary>
    ''' Tests importing all supported time series file formats
    ''' </summary>
    ''' <param name="filepath">relative path to the file in the test data directory</param>
    ''' <remarks>
    ''' TODO: the following files cannot be tested yet because they are dependent on the user's region settings:
    ''' "CSV\DEMONA_PSI.csv"
    ''' "CSV\MergeTest.csv"
    ''' "CSV\UTF-8.csv"
    '''
    ''' TODO: the following files connot be tested yet because they require user dialog input:
    ''' "HYBNAT\BCS\aligned.BCS"
    ''' "HYBNAT\BCS\shrinked.BCS"
    ''' "HYBNAT\WEL\BEK1.WEL"
    ''' "HYBNAT\WEL\BEK2.WEL"
    ''' "HYBNAT\WEL\BEK3.WEL"
    ''' "HYBNAT\WEL\EIN1.WEL"
    ''' "HYBNAT\WEL\KFL1.WEL"
    ''' "HYBNAT\WEL\NFL1.WEL"
    ''' "HYBNAT\WEL\PEG1.WEL"
    ''' "HYBNAT\WEL\TRS1.WEL"
    ''' "HYBNAT\WEL\TRS2.WEL"
    ''' "HYBNAT\WEL\VER1.WEL"
    ''' "HYDRO_AS-2D\BW_TMP.DAT"
    ''' "HYDRO_AS-2D\HYDRO_AS-2D_v5\BW_TMP.DAT"
    ''' "HYDRO_AS-2D\HYDRO_AS-2D_v5\Q_Strg.dat"
    ''' "HYDRO_AS-2D\Pegel.dat"
    ''' "HYDRO_AS-2D\Q_Strg.dat"
	''' 
    ''' TODO: The following files cannot be tested because Wave does not support them (yet):
    ''' "SMUSI\F10_WEL.ASC"
    ''' "SMUSI\SMUSI_3.ASC"
    ''' </remarks>
    <TestMethod>
    <DataRow("BIN\TS_LM.m3s.10000a.bin")>
    <DataRow("BIN\abfluss_1.bin")>
    <DataRow("BIN\abfluss_2.bin")>
    <DataRow("DFS0\Rain_accumulated.dfs0")>
    <DataRow("DFS0\Rain_backwardStep.dfs0")>
    <DataRow("DFS0\Rain_forwardStep.dfs0")>
    <DataRow("DFS0\Rain_instantaneous.dfs0")>
    <DataRow("DFS0\Rain_stepaccumulated.dfs0")>
    <DataRow("DFS0\data_ndr_roese.dfs0")>
    <DataRow("FEWS_PI\test_fews_pi.xml")>
    <DataRow("GINA\gina_HNW.csv")>
    <DataRow("GINA\hdf5_hydraulic.h5")>
    <DataRow("GINA\hdf5_pollutant.h5")>
    <DataRow("GINA\sample_hydraulic.gbl")>
    <DataRow("GINA\sample_pollutant.gbl")>
    <DataRow("GISMO\AUS_WEL.asc")>
    <DataRow("GISMO\BEK_WEL.asc")>
    <DataRow("GISMO\EIN_WEL.asc")>
    <DataRow("GISMO\FKA_WEL.asc")>
    <DataRow("GISMO\GER_WELl.asc")>
    <DataRow("GISMO\RUE_WEL.asc")>
    <DataRow("HYSTEM-EXTRAN\HYSTEM-EXTRAN.DAT")>
    <DataRow("HYSTEM-EXTRAN\HYSTEM-EXTRAN.REG")>
    <DataRow("HYSTEM-EXTRAN\HYSTEM-EXTRAN.WEL")>
    <DataRow("HYSTEM-EXTRAN\HYSTEM_EXTRAN_N_mm_h_1976-2019_1.reg")>
    <DataRow("HYSTEM-EXTRAN\HYSTEM_EXTRAN_N_mm_h_1976-2019_2.reg")>
    <DataRow("JAMS\Pegel.dat")>
    <DataRow("JAMS\TimeLoop.dat")>
    <DataRow("PRMS\DPOUT_stori.out")>
    <DataRow("PRMS\annual_stori.out")>
    <DataRow("PRMS\monthly_stori.out")>
    <DataRow("PRMS\statvar.dat")>
    <DataRow("SIMBA\SIMBA.SMB")>
    <DataRow("SMUSI\N1013N_61-70.reg")>
    <DataRow("SMUSI\N74235_Mue_10aV2.reg")>
    <DataRow("SMUSI\REGEN475.REG")>
    <DataRow("SMUSI\rr03_575.reg")>
    <DataRow("SMUSI\smusi-reg-with-empty-line-between-header-and-data.reg")>
    <DataRow("SWMM\A128.out")>
    <DataRow("SWMM\LIDRep_BRC.txt")>
    <DataRow("SWMM\M180.out")>
    <DataRow("SWMM\M180Qzu_WQ.txt")>
    <DataRow("SWMM\Qzu_A128.txt")>
    <DataRow("SWMM\SWMM5_TimeSeriesFile.dat")>
    <DataRow("SWMM\Tst_SubC.out")>
    <DataRow("SWMM\Wissmarbach.out")>
    <DataRow("TALSIM\TALSIM.wel")>
    <DataRow("TALSIM\Talsim_states.KTR.WEL")>
    <DataRow("UVF\42007.0_q.uvf")>
    <DataRow("UVF\42960.0_q_28.uvf")>
    <DataRow("UVF\57033.0_w.uvf")>
    <DataRow("UVF\57211.0_w.uvf")>
    <DataRow("UVF\57540.0_q.uvf")>
    <DataRow("UVF\n_summenlinie.uvf")>
    <DataRow("WEL\BlueM_EFLWEL_csv.wel")>
    <DataRow("WEL\BlueM_EFLWEL_noncsv.wel")>
    <DataRow("WEL\DEMONA_PSI.wel")>
    <DataRow("WEL\OLEF_NA_ORG1000.wel")>
    <DataRow("ZRE\1982_DA.ZRE")>
    <DataRow("ZRE\Zufl_gem.zre")>
    <DataRow("ZRXP\Q.1440.zrx")>
    <DataRow("ZRXP\Q2.1440.zrx")>
    <DataRow("ZRXP\cmp_Z329_S109.zrx")>
    <DataRow("ZRXP\ensemble_forecast.zrxp")>
    <DataRow("ZRXP\multiple_timeseries.zrxp")>
    <DataRow("ZRXP\series_with_infinity.zrx")>
    Public Sub TestFileImport(filepath As String)
        filepath = IO.Path.Combine(TestData.getTestDataDir(), filepath)
        Try
            Dim file As TimeSeriesFile = TimeSeriesFile.getInstance(filepath)
            'read series info from file
            Call file.readSeriesInfo()
            'select first series and read it
            Dim sInfo As TimeSeriesInfo = file.TimeSeriesInfos.First()
            Dim ts As TimeSeries = file.getTimeSeries(sInfo.Index)
        Catch ex As Exception
            Assert.Fail($"Exception thrown when reading file {filepath}: " & ex.Message)
        End Try
    End Sub

End Class