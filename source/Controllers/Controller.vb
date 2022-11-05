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
''' <summary>
''' Abstract Controller class
''' </summary>
Friend MustInherit Class Controller

    Protected WithEvents _view As IView
    Protected WithEvents _model As Wave

    Public Overridable ReadOnly Property View As IView
        Get
            Return _view
        End Get
    End Property

    Public ReadOnly Property Model As Wave
        Get
            Return _model
        End Get
    End Property

    Public Sub New(view As IView, model As Wave)
        _view = view
        _model = model
    End Sub

    Public MustOverride Sub ShowView()

End Class
