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
Module Constants

    Public Const eol As String = Chr(13) & Chr(10) 'line break
    Public semicolon As Character = New Character(";")
    Public comma As Character = New Character(",")
    Public period As Character = New Character(".")
    Public space As Character = New Character(" ")
    Public tab As Character = New Character(Chr(9))
    'minimum OADate
    Public minOADate As New DateTime(100, 1, 1)
    'maxmimum OADate is supposed to be 9999/12/31 23:59:59 but using that date causes OverflowExceptions from within TeeChart
    Public maxOADate As New DateTime(9000, 12, 31, 23, 59, 59)

    Public Const urlHelp As String = "https://wiki.bluemodel.org/index.php/Wave"
    Public Const urlUpdateCheck As String = "https://api.github.com/repos/bluemodel/BlueM.Wave/releases/latest"
    Public Const urlDownload As String = "https://github.com/bluemodel/BlueM.Wave/releases/latest"

End Module
