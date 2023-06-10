# BlueM.Wave API
The BlueM.Wave API provides methods for reading time series from files, manipulating and displaying them.

To access the API, include a reference to Wave.exe (or to the Wave project if you have the source code) in your .NET project.

## Introduction

### TimeSeries
The class `BlueM.Wave.TimeSeries` represents a single time series and exposes methods for manipulating them:
```vb
'create a new time series
Dim ts As New BlueM.Wave.TimeSeries("my series")
ts.Unit = "m3/s"
ts.Interpretation = BlueM.Wave.TimeSeries.InterpretationEnum.BlockRight

'add some nodes to the time series
ts.AddNode(New DateTime(2000, 1, 1), 10)
ts.AddNode(New DateTime(2000, 1, 2), 20)
ts.AddNode(New DateTime(2000, 1, 3), 30)
ts.AddNode(New DateTime(2000, 1, 4), 15)

'print some information about the time series
Console.WriteLine("Length: " & ts.Length)
Console.WriteLine("Start date: " & ts.StartDate)
Console.WriteLine("End date: " & ts.EndDate)
Console.WriteLine("Average: " & ts.Average)

'manipulate the time series, e.g. by cutting it
ts.Cut(New DateTime(2000, 1, 2), New DateTime(2000, 1, 3))
```

### TimeSeriesFile
You can use the `BlueM.Wave.TimeSeriesFile` class' factory method `getInstance()` to read time series from any of the supported file formats:
```vb
Dim filepath as String = "path\to\file"
Dim tsfile as BlueM.Wave.TimeSeriesFile = BlueM.Wave.TimeSeriesFile.getInstance(filepath)

'check the time series contained in the file
For Each sInfo As BlueM.Wave.TimeSeriesInfo In tsfile.TimeSeriesInfos
	Console.WriteLine(sInfo.Name)
Next

'select some series for import
tsfile.selectAllSeries()

'read the selected time series from the file
tsfile.readFile()

'loop over all series read from the file and print some information about them
For Each ts As BlueM.Wave.TimeSeries In tsfile.TimeSeries.Values
	Console.WriteLine("Series title: " & ts.Title)
	Console.WriteLine("Extent: " & ts.StartDate & " - " & ts.EndDate)
	Console.WriteLine("Max value: " & ts.Maximum)
	Console.WriteLine("Min value: " & ts.Minimum)
Next

'get one particular time series
Dim ts As BlueM.Wave.TimeSeries = tsfile.getTimeSeries("seriesname or index")
```

The namespace `BlueM.Wave.Fileformats` contains special classes for reading from specific file formats, which all inherit from `TimeSeriesFile`.

### App
The class `BlueM.Wave.App` represents a fully fledged Wave app instance including it's windows. Instantiate a new Wave app instance as follows. This will show the main Wave window.
```vb
Dim app As New BlueM.Wave.App()
```

Then use the App's `Wave` property to access methods e.g. for importing time series:
```vb
app.Wave.Import_File()
app.Wave.Import_Series()
```
and use the `TimeSeries` field to access any already imported time series:
```vb
app.Wave.TimeSeries
```

You can also use the `BlueM.Wave.Wave` class on its own to import time series and then launch the app afterwards:
```vb
Dim wave1 as New BlueM.Wave.Wave()
wave1.Import_Series()
'instantiate the app with the existing Wave instance
Dim app As New BlueM.Wave.App(wave1)
```

## Examples
The repository contains a project `Wave.Examples` with some more examples of API usage.
