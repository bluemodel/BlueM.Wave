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
Imports System.Xml
Imports System.Xml.Serialization

Namespace Fileformats

    ''' <summary>
    ''' Class for the Delft-FEWS published interface timeseries format
    ''' Format description: https://publicwiki.deltares.nl/spaces/FEWSDOC/pages/8683960/Delft-Fews+Published+Interface+timeseries+Format+PI+Import
    ''' XML schema: https://fews.wldelft.nl/schemas/version1.0/pi-schemas/pi_timeseries.xsd
    ''' </summary>
    Public Class FEWS_PI
        Inherits TimeSeriesFile

        ''' <summary>
        ''' Specifies whether to use the file import dialog
        ''' </summary>
        ''' <value></value>
        ''' <returns>True</returns>
        ''' <remarks></remarks>
        Public Overrides ReadOnly Property UseImportDialog() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Instantiates a new FEWS_PI object
        ''' </summary>
        ''' <param name="file">path to the file</param>
        ''' <remarks></remarks>
        Public Sub New(file As String, Optional ReadAllNow As Boolean = False)

            Call MyBase.New(file)

            'settings
            Me.Dateformat = Helpers.DateFormats("ISO")
            Me.UseUnits = True

            'set default metadata keys
            Me.FileMetadata.AddKeys(FEWS_PI.MetadataKeys)

            Call Me.readSeriesInfo()

            If (ReadAllNow) Then
                'read immediately
                Call Me.selectAllSeries()
                Call Me.readFile()
            End If

        End Sub

        ''' <summary>
        ''' Checks whether a file conforms with the Delft-FEWS PI-format
        ''' </summary>
        ''' <param name="file">path to file</param>
        ''' <returns>Boolean</returns>
        ''' <remarks></remarks>
        Public Shared Function verifyFormat(file As String) As Boolean
            Try
                'attempt to deserialize XML
                Dim serializer As New XmlSerializer(GetType(XMLTimeSeries))
                Dim xmlroot As XMLTimeSeries
                Using xmlreader As New XmlTextReader(file)
                    xmlroot = CType(serializer.Deserialize(xmlreader), XMLTimeSeries)
                End Using
                Return True
            Catch ex As Exception
                Log.AddLogEntry(levels.debug, $"Failed to verify XML file as Delft-FEWS PI format due to error {ex}")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Reads the metadata from the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readSeriesInfo()

            Me.TimeSeriesInfos.Clear()

            Try
                'deserialize XML
                Dim serializer As New XmlSerializer(GetType(XMLTimeSeries))
                Dim tsRoot As XMLTimeSeries
                Using xmlreader As New XmlTextReader(Me.File)
                    tsRoot = CType(serializer.Deserialize(xmlreader), XMLTimeSeries)
                End Using
                'store time series info
                Dim index As Integer = 0
                For Each series As XMLSeries In tsRoot.series
                    Dim tsInfo As New TimeSeriesInfo
                    tsInfo.Index = index
                    tsInfo.Name = $"{series.header.locationId}.{series.header.parameterId}"
                    tsInfo.Unit = series.header.units
                    Me.TimeSeriesInfos.Add(tsInfo)
                    index += 1
                Next
            Catch ex As Exception
                Throw New Exception("Error while reading Delft-FEWS PI XML file!", ex)
            End Try

        End Sub

        ''' <summary>
        ''' reads the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub readFile()

            Me.TimeSeries.Clear()

            Try
                'deserialize XML
                Dim serializer As New XmlSerializer(GetType(XMLTimeSeries))
                Dim xmlroot As XMLTimeSeries
                Using xmlreader As New XmlTextReader(Me.File)
                    xmlroot = CType(serializer.Deserialize(xmlreader), XMLTimeSeries)
                End Using

                'read selected series
                Dim index As Integer = 0
                For Each sInfo As TimeSeriesInfo In Me.SelectedSeries
                    Dim series As XMLSeries = xmlroot.series(sInfo.Index)
                    Dim ts As New TimeSeries()
                    ts.Title = $"{series.header.locationId}.{series.header.parameterId}"
                    ts.Unit = series.header.units
                    ts.DataSource = New TimeSeriesDataSource(Me.File, ts.Title)
                    'set interpretation
                    Select Case series.header.type
                        Case "instantaneous"
                            ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.Instantaneous
                        Case "accumulative"
                            ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.CumulativePerTimestep
                        Case "mean"
                            ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight
                    End Select
                    'store FEWS PI specific metadata
                    ts.Metadata.Add("type", series.header.type)
                    ts.Metadata.Add("locationId", series.header.locationId)
                    ts.Metadata.Add("parameterId", series.header.parameterId)
                    ts.Metadata.Add("timeStep", series.header.timeStep.unit)
                    ts.Metadata.Add("multiplier", series.header.timeStep.multiplier)
                    ts.Metadata.Add("missVal", series.header.missVal.ToString(Globalization.CultureInfo.InvariantCulture))
                    ts.Metadata.Add("stationName", series.header.stationName)
                    ts.Metadata.Add("units", series.header.units)
                    'store events as nodes
                    Dim errorvalue As Double = series.header.missVal
                    For Each xmlevent As XMLEvent In series.events
                        If xmlevent.value = errorvalue Then
                            ts.AddNode(xmlevent.dt, Double.NaN)
                        Else
                            ts.AddNode(xmlevent.dt, xmlevent.value)
                        End If
                    Next
                    Me.TimeSeries.Add(index, ts)
                    index += 1
                Next
            Catch ex As Exception
                Throw New Exception("Error while reading Delft-FEWS PI XML file!", ex)
            End Try

        End Sub

        ''' <summary>
        ''' Returns a list of Delft-FEWS PI-specific metadata keys
        ''' </summary>
        Public Overloads Shared ReadOnly Property MetadataKeys() As List(Of String)
            Get
                Dim keys As New List(Of String) From {
                "type",
                "locationId",
                "parameterId",
                "timeStep",
                "multiplier",
                "missVal",
                "stationName",
                "units"
            }
                Return keys
            End Get
        End Property

        ''' <summary>
        ''' Sets default metadata values for a time series corresponding to the Delft-FEWS PI timeseries format
        ''' </summary>
        Public Overloads Shared Sub setDefaultMetadata(ts As TimeSeries)
            'Make sure all required keys exist
            ts.Metadata.AddKeys(FEWS_PI.MetadataKeys)
            'Set default values
            If ts.Metadata("type") = "" Then
                Select Case ts.Interpretation
                    Case BlueM.Wave.TimeSeries.InterpretationEnum.Instantaneous
                        ts.Metadata("type") = "instantaneous"
                    Case BlueM.Wave.TimeSeries.InterpretationEnum.CumulativePerTimestep
                        ts.Metadata("type") = "accumulative"
                    Case BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight
                        ts.Metadata("type") = "mean"
                    Case Else
                        ts.Metadata("type") = "unknown"
                End Select
            End If
            If ts.Metadata("timeStep") = "" Then ts.Metadata("timeStep") = "nonequidistant"
            If ts.Metadata("multiplier") = "" Then ts.Metadata("multiplier") = "1"
            If ts.Metadata("missVal") = "" Then ts.Metadata("missVal") = "-999.0"
            If ts.Metadata("units") = "" Then ts.Metadata("units") = ts.Unit
        End Sub

        ''' <summary>
        ''' Parses a date and time string into a DateTime object
        ''' </summary>
        ''' <param name="dateString">date string</param>
        ''' <param name="timeString">time string</param>
        ''' <returns>DateTime object</returns>
        Private Shared Function parseDateTime(dateString As String, timeString As String) As DateTime
            Dim success As Boolean
            Dim timestamp As DateTime
            success = DateTime.TryParseExact($"{dateString} {timeString}", Helpers.DateFormats("ISO"), Helpers.DefaultNumberFormat, Globalization.DateTimeStyles.None, timestamp)
            If Not success Then
                Throw New Exception($"Unable to parse timestamp {dateString} {timeString}!")
            End If
            Return timestamp
        End Function

        ''' <summary>
        ''' Write one or multiple series to an XML file in Delft-FEWS PI timeseries format
        ''' </summary>
        ''' <param name="tsList">time series to write to file</param>
        ''' <param name="file">path to the xml file</param>
        ''' <remarks></remarks>
        Public Shared Sub Write_File(ByRef tsList As List(Of TimeSeries), file As String)
            Dim xmlserializer As New XmlSerializer(GetType(XMLTimeSeries))
            Dim xmlroot As New XMLTimeSeries With {
                .version = "1.2",
                .timeZone = "0.0",
                .series = New List(Of XMLSeries)
            }
            For Each ts As TimeSeries In tsList
                Dim xmlseries As New XMLSeries
                Dim xmlheader As New XMLHeader With {
                    .type = ts.Metadata("type"),
                    .locationId = ts.Metadata("locationId"),
                    .parameterId = ts.Metadata("parameterId"),
                    .timeStep = New XMLTimeStep With {
                        .unit = ts.Metadata("timeStep"),
                        .multiplier = ts.Metadata("multiplier")
                    },
                    .startDate = New XMLDate With {
                        .date = ts.StartDate.ToString("yyyy-MM-dd"),
                        .time = ts.StartDate.ToString("HH:mm:ss")
                    },
                    .endDate = New XMLDate With {
                        .date = ts.EndDate.ToString("yyyy-MM-dd"),
                        .time = ts.EndDate.ToString("HH:mm:ss")
                    },
                    .missVal = Double.Parse(ts.Metadata("missVal"), Globalization.CultureInfo.InvariantCulture),
                    .stationName = ts.Metadata("stationName"),
                    .units = ts.Metadata("units")
                }
                xmlseries.header = xmlheader
                xmlseries.events = New List(Of XMLEvent)
                For Each node As KeyValuePair(Of DateTime, Double) In ts.Nodes
                    Dim xmlevent As New XMLEvent With {
                        .date = node.Key.ToString("yyyy-MM-dd"),
                        .time = node.Key.ToString("HH:mm:ss"),
                        .value = If(Double.IsNaN(node.Value), xmlheader.missVal, node.Value),
                        .flag = 2 'Completed/Reliable: Original value was missing. Value has been filled in through interpolation, transformation(e.g.stage discharge) Or a model.
                    }
                    xmlseries.events.Add(xmlevent)
                Next
                xmlroot.series.Add(xmlseries)
            Next
            Dim xmlwriter As New XmlTextWriter(file, System.Text.Encoding.UTF8) With {
                .Formatting = Formatting.Indented,
                .IndentChar = ControlChars.Tab,
                .Indentation = 1
            }
            xmlserializer.Serialize(xmlwriter, xmlroot)
            xmlwriter.Close()
        End Sub

        ''' <summary>
        ''' Object representation of the Delft-FEWS PI timeseries XML structure
        ''' </summary>
        <XmlRoot("TimeSeries", Namespace:="http://www.wldelft.nl/fews/PI")>
        Public Class XMLTimeSeries
            <XmlNamespaceDeclarations>
            Public xmlns As New XmlSerializerNamespaces(
                {New XmlQualifiedName("", "http://www.wldelft.nl/fews/PI"),
                 New XmlQualifiedName("xsi", "http://www.w3.org/2001/XMLSchema-instance")}
            )

            <XmlAttribute(AttributeName:="schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")>
            Public Property schemaLocation As String = "http://www.wldelft.nl/fews/PI http://fews.wldelft.nl/schemas/version1.0/pi-schemas/pi_timeseries.xsd"

            <XmlAttribute("version")>
            Public version As String

            <XmlElement("timeZone")>
            Public timeZone As String

            <XmlElement("series")>
            Public series As List(Of XMLSeries)
        End Class

        Public Class XMLSeries
            <XmlElement("header")>
            Public header As XMLHeader

            <XmlElement("event")>
            Public events As List(Of XMLEvent)
        End Class

        Public Class XMLHeader
            <XmlElement("type")>
            Public type As String

            <XmlElement("locationId")>
            Public locationId As String

            <XmlElement("parameterId")>
            Public parameterId As String

            <XmlElement("timeStep")>
            Public timeStep As XMLTimeStep

            <XmlElement("startDate")>
            Public startDate As XMLDate

            <XmlElement("endDate")>
            Public endDate As XMLDate

            <XmlElement("missVal")>
            Public missVal As Double

            <XmlElement("stationName")>
            Public stationName As String

            <XmlElement("units")>
            Public units As String
        End Class

        Public Class XMLTimeStep
            <XmlAttribute("unit")>
            Public unit As String

            <XmlAttribute("multiplier")>
            Public multiplier As Integer
        End Class

        Public Class XMLDate
            <XmlAttribute("date")>
            Public [date] As String

            <XmlAttribute("time")>
            Public time As String

            <XmlIgnore>
            ReadOnly Property dt As DateTime
                Get
                    Return parseDateTime(Me.date, Me.time)
                End Get
            End Property
        End Class

        Public Class XMLEvent
            <XmlAttribute("date")>
            Public [date] As String

            <XmlAttribute("time")>
            Public time As String

            <XmlAttribute("value")>
            Public value As Double

            <XmlAttribute("flag")>
            Public flag As Integer

            <XmlIgnore>
            ReadOnly Property dt As DateTime
                Get
                    Return parseDateTime(Me.date, Me.time)
                End Get
            End Property

        End Class

    End Class

End Namespace

