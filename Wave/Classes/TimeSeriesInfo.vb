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
''' <summary>
''' Contains basic information about a series contained in a file
''' </summary>
''' <remarks></remarks>
Public Class TimeSeriesInfo

    ''' <summary>
    ''' Series name
    ''' </summary>
    Public Name As String

    ''' <summary>
    ''' Series unit
    ''' </summary>
    Public Unit As String

    ''' <summary>
    ''' Index of the series in the file (usually the column index)
    ''' </summary>
    Public Index As Integer

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()

    End Sub

    'TODO: The following properties are only needed by SWMM file formats
    Friend Objekt As String     'Bezeichnung des Objekts (z.B. "S101")
    Friend Type As String       '"FLOW" oder ein Stoffparameter (z.B. "CSB")
    Friend ObjType As String    '"Subcatchment", "Node" oder "Link"

    Public Overrides Function ToString() As String
        Return Me.Name
    End Function

End Class

