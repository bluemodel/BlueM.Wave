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
Imports System
Imports System.Windows.Forms
''' <summary>
''' TabControl with hidden tabs
''' Source: https://stackoverflow.com/questions/2340566/creating-wizards-for-windows-forms-in-c-sharp
''' </summary>
Friend Class WizardPages
    Inherits TabControl

    Protected Overrides Sub WndProc(ByRef m As Message)
        'Hide tabs by trapping the TCM_ADJUSTRECT message
        If (m.Msg = Convert.ToInt32("0x1328", 16) And Not DesignMode) Then
            m.Result = CType(1, IntPtr)
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Protected Overrides Sub OnKeyDown(ke As KeyEventArgs)
        'Block Ctrl+Tab And Ctrl+Shift+Tab hotkeys
        If (ke.Control And ke.KeyCode = Keys.Tab) Then
            Return
        End If
        MyBase.OnKeyDown(ke)
    End Sub
End Class