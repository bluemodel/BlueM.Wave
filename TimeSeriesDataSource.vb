'Copyright (c) BlueM Dev Group
'Website: http://bluemodel.org
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
    ''' Returns a custom string representation of the datasource
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function ToString() As String
        Select Case _origin
            Case OriginEnum.FileImport
                Return String.Format("File: ""{0}"", Title: ""{1}""", _filepath, _title)
            Case Else
                Return [Enum].GetName(GetType(OriginEnum), _origin)
        End Select
    End Function
End Class
