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
Public MustInherit Class ZeitreihenExtern


    Public Enum ZREQuellenTyp As Integer
        ZRDatei = 0
        ZRDatabase = 1
        ZRnetCDF = 2
    End Enum


    Public MustOverride ReadOnly Property Typ() As ZREQuellenTyp


    ''' <summary>
    ''' Array der in der Datei enhaltenen Zeitreihen
    ''' </summary>
    Public Zeitreihen() As Zeitreihe

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public MustOverride ReadOnly Property UseImportDialog() As Boolean



    ''' <summary>
    ''' Liest die ausgewählten Spalten (siehe SpaltenSel) ein und legt sie im Array Zeitreihen ab.
    ''' </summary>
    Public MustOverride Sub Zeitreihe_Einlesen()

    ''' <summary>
    ''' Alle vorhandenen Spalten für den Import auswählen
    ''' </summary>
    ''' <remarks>Die X-Spalte wird nicht mit ausgewählt</remarks>
    Public MustOverride Sub selectAll_Zeitreihen()



End Class
