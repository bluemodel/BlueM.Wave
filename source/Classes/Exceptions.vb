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

Imports System.Runtime.Serialization

''' <summary>
''' Thrown when a general, internal exception occurs
''' </summary>
<Serializable>
Friend Class InternalException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

<Serializable>
Friend Class AnalysisCancelledException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

''' <summary>
''' Thrown when the input parameters for an analysis function are invalid
''' </summary>
<Serializable>
Friend Class AnalysisInvalidInputException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

''' <summary>
''' Thrown when an error occurs during the execution of an analysis function
''' </summary>
<Serializable>
Friend Class AnalysisFailedException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

''' <summary>
''' Thrown when a time series with a specified name or index cannot be found in a time series collection
''' </summary>
<Serializable>
Public Class SeriesNotFoundException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

''' <summary>
''' Thrown when an error occurs while reading a time series file
''' </summary>
<Serializable>
Public Class TimeSeriesFileReadingException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

''' <summary>
''' Thrown when an error occurs while writing a time series file
''' </summary>
<Serializable>
Public Class TimeSeriesFileWritingException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class

''' <summary>
''' Thrown when an error occurs while parsing file import instructions
''' </summary>
<Serializable>
Public Class ParserException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class
