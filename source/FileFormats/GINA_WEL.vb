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
Imports System.IO

Namespace Fileformats

    ''' <summary>
    ''' Class for the GINA WEL file format
    ''' It is essentially a CSV format, this class only contains a method for detecting the format
    ''' and overrides the CSV import settings in the constructor
    ''' </summary>
    ''' <remarks>Format siehe https://wiki.bluemodel.org/index.php/GINA-WEL-Format</remarks>
    Public Class GINA_WEL
        Inherits CSV

        'Constructor
        Public Sub New(FileName As String)

            MyBase.New(FileName)

            ' Presettings
            Me.Dateformat = DateFormats("default")
            Me.DecimalSeparator = Constants.period
            Me.IsColumnSeparated = True
            Me.Separator = Constants.semicolon
            Me.UseUnits = True

            ' Index of header rows
            Me.iLineHeadings = 5
            Me.iLineUnits = 6
            Me.iLineData = 7

            Call Me.readSeriesInfo()

        End Sub

        ''' <summary>
        ''' Checks if the file is a GINA WEL result file
        ''' </summary>
        ''' <param name="file">file path</param>
        ''' <returns>True if the file is a GINA WEL result file</returns>
        Public Shared Function verifyFormat(file As String) As Boolean

            ' open file
            Dim FiStr As FileStream = New FileStream(file, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, detectEncodingFromByteOrderMarks:=True)
            Dim lines As New List(Of String)

            ' read four lines
            For i As Integer = 1 To 4
                lines.Add(StrRead.ReadLine().Trim())
            Next

            ' close file
            StrRead.Close()
            FiStr.Close()

            ' check first line
            If Not (lines(0).Contains("Gew.Typ")) Then
                Return False
            End If

            ' check second line
            If Not (lines(1).Contains("Grundlast")) Then
                Return False
            End If

            ' check third line
            If Not (lines(2).Contains("AEO")) Then
                Return False
            End If

            ' check fourth line
            If Not lines(3).Contains("AEOpnat") Then
                Return False
            End If

            Return True

        End Function

    End Class

End Namespace