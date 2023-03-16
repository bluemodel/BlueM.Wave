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
''' Stores information about the datasource of a time series
''' </summary>
Public Class TimeSeriesDataSource

    ''' <summary>
    ''' Origin types
    ''' </summary>
    Public Enum OriginEnum
        FileImport
        AnalysisResult
        ManuallyEntered
        Undefined
    End Enum

    Private _origin As OriginEnum
    Private _filepath As String
    Private _title As String

    ''' <summary>
    ''' Creates a new TimeSeriesDataSource instance with origin type FileImport
    ''' </summary>
    ''' <param name="filepath">path to the file from which the series was imported</param>
    ''' <param name="title">title of the series in the file</param>
    Public Sub New(filepath As String, title As String)
        _origin = OriginEnum.FileImport
        _filepath = filepath
        _title = title
    End Sub

    ''' <summary>
    ''' Creates a new TimeSeriesDataSource instance of a specific origin type (used for origins other than FileImport)
    ''' </summary>
    ''' <param name="origin">The origin type</param>
    Public Sub New(origin As OriginEnum)
        _origin = origin
        _filepath = ""
        _title = ""
    End Sub

    ''' <summary>
    ''' The origin type of the time series
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Origin As OriginEnum
        Get
            Return _origin
        End Get
    End Property

    ''' <summary>
    ''' The path to the file from which the series was imported
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FilePath As String
        Get
            Return _filepath
        End Get
    End Property

    ''' <summary>
    ''' The title of the series in the file from which it was imported
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Title As String
        Get
            Return _title
        End Get
    End Property

    ''' <summary>
    ''' Returns a copy of the datasource
    ''' </summary>
    ''' <returns></returns>
    Public Function Copy() As TimeSeriesDataSource
        Dim _copy As New TimeSeriesDataSource(Me.Origin)
        _copy._filepath = Me.FilePath
        _copy._title = Me.Title
        Return _copy
    End Function

    ''' <summary>
    ''' Returns a custom string representation of the datasource
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function ToString() As String
        Select Case _origin
            Case OriginEnum.FileImport
                Return $"File: ""{_filepath}"", Title: ""{_title}"""
            Case Else
                Return [Enum].GetName(GetType(OriginEnum), _origin)
        End Select
    End Function
End Class
