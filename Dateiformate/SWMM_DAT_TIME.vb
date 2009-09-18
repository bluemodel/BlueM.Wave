﻿Imports System.IO
''' <summary>
''' Class for SWMM5 time series (DAT) data format
''' </summary>

Public Class SWMM_DAT_TIME
    Inherits Dateiformat

    Const DatumsformatSWMMDAT As String = "MM/dd/yyyy HH:mm"
    Const iDim As Integer = 3        'Dezimalfaktor wird erstmal global auf 3 gesetzt

#Region "Eigenschaften"

    'Eigenschaften
    '#############

    Private _Zeitintervall As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gibt an, ob beim Import des Dateiformats der Importdialog angezeigt werden soll
    ''' </summary>
    Public Overrides ReadOnly Property UseImportDialog() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Property Zeitintervall() As Integer
        Get
            Return _Zeitintervall
        End Get
        Set(ByVal value As Integer)
            _Zeitintervall = value
        End Set
    End Property

#End Region

#Region "Methoden"

    'Methoden
    '########

    'Konstruktor
    '***********
    Public Sub New(ByVal FileName As String, Optional ByVal ReadAllNow As Boolean = False)

        MyBase.New(FileName)

        'Voreinstellungen
        Me.iZeileDaten = 2
        Me.UseEinheiten = False

        Call Me.SpaltenAuslesen()

        If (ReadAllNow) Then
            'Direkt einlesen
            Call Me.selectAllSpalten()
            Call Me.Read_File()
        End If

    End Sub

    'Spalten auslesen
    '****************
    Public Overrides Sub SpaltenAuslesen()

        Dim Zeile As String = ""

        Try
            'Datei öffnen
            Dim FiStr As FileStream = New FileStream(Me.File, FileMode.Open, IO.FileAccess.Read)
            Dim StrRead As StreamReader = New StreamReader(FiStr, System.Text.Encoding.GetEncoding("iso8859-1"))
            Dim StrReadSync = TextReader.Synchronized(StrRead)

            'Es gibt immer 2 Spalten!
            ReDim Me.Spalten(1)

            '1. Spalte (X)
            Me.Spalten(0).Name = "Datum_Zeit"
            Me.Spalten(0).Index = 0

            '2. Spalte (Y)
            Me.Spalten(1).Index = 1

            'Reihentitel steht in 1. Zeile:
            Zeile = StrReadSync.ReadLine.ToString()
            Me.Spalten(1).Name = Zeile.Trim()

            StrReadSync.close()
            StrRead.Close()
            FiStr.Close()

        Catch ex As Exception
            MsgBox("Konnte Datei nicht einlesen!" & eol & eol & "Fehler: " & ex.Message, MsgBoxStyle.Critical, "Fehler")
        End Try

    End Sub
    'DAT-Datei einlesen
    '******************
    Public Overrides Sub Read_File()

    End Sub

    ''' <summary>
    ''' Exportiert eine Zeitreihe als DAT-Datei
    ''' </summary>
    ''' <param name="Reihe">Die zu exportierende Zeitreihe</param>
    ''' <param name="File">Pfad zur anzulegenden Datei</param>
    ''' <param name="dt">Zeitschritt</param>
    Public Shared Sub Write_File(ByVal Reihe As Zeitreihe, ByVal File As String, ByVal dt As Integer)

        Dim KontiReihe As Zeitreihe

        'Äquidistante Zeitreihe erzeugen
        KontiReihe = Reihe.getKontiZRE3(dt)

        Dim strwrite As StreamWriter
        Dim iZeile, n As Integer
        strwrite = New StreamWriter(File)

        n = 0   'n = Anzahl der Zeitreihenwerte
        For iZeile = 0 To KontiReihe.Length - 1
            strwrite.Write(KontiReihe.XWerte(n).ToString(DatumsformatSWMMDAT) & " ")
            strwrite.Write(KontiReihe.YWerte(n).ToString())
            n = n + 1
            strwrite.WriteLine()
        Next
        strwrite.Close()

    End Sub

#End Region


End Class

