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
Public Module Constants

    Friend Const eol As String = Chr(13) & Chr(10) 'line break
    Friend semicolon As Character = New Character(";")
    Friend comma As Character = New Character(",")
    Friend period As Character = New Character(".")
    Friend space As Character = New Character(" ")
    Friend tab As Character = New Character(Chr(9))
    'minimum OADate
    Friend minOADate As New DateTime(100, 1, 1)
    'maxmimum OADate is supposed to be 9999/12/31 23:59:59 but using that date causes OverflowExceptions from within TeeChart
    Friend maxOADate As New DateTime(9000, 12, 31, 23, 59, 59)

    Friend Const urlHelp As String = "https://wiki.bluemodel.org/index.php/Wave"
    Friend Const urlUpdateCheck As String = "https://api.github.com/repos/bluemodel/BlueM.Wave/releases/latest"
    Friend Const urlDownload As String = "https://github.com/bluemodel/BlueM.Wave/releases/latest"

    Public Enum Direction
        Up
        Down
    End Enum

End Module
