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
''' <summary>
''' A variable representing a time series that can be used in a mathematical expression for the Calculate analysis function
''' </summary>
Friend Class CalculatorVariable

    Private _varName As String
    Private _ts As TimeSeries

    ''' <summary>
    ''' The variable name
    ''' </summary>
    Public ReadOnly Property varName As String
        Get
            Return _varName
        End Get
    End Property

    ''' <summary>
    ''' The time series associated with the variable
    ''' </summary>
    Public ReadOnly Property ts As TimeSeries
        Get
            Return _ts
        End Get
    End Property

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="varName">the variable name</param>
    ''' <param name="ts">the time series</param>
    Public Sub New(varName As String, ts As TimeSeries)
        _varName = varName
        _ts = ts
    End Sub

    ''' <summary>
    ''' Returns a string representation of the variable consisting of variable name and time series title
    ''' </summary>
    Public Overrides Function ToString() As String
        Return String.Format("{0}: {1}", varName, ts.Title)
    End Function
End Class
