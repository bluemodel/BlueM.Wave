## File formats

Each supported file format is represented by its own class that inherits from [`TimeSeriesFile`](xref:BlueM.Wave.TimeSeriesFile).

Reading files always happens in steps:
1. `.readSeriesInfo()` reads time series metadata from the file
2. One or multiple time series are selected for import (e.g. by the user) 
3. `.readFile()` reads the time series data for the selected series from the file

There are some shortcuts:
* `.getTimeSeries()` retrieves one time series and if necessary, selects and reads it from the file first.

### Adding a new file format
* Create a new class in the namespace [`FileFormats`](xref:BlueM.Wave.Fileformats) that inherits from [`TimeSeriesFile`](xref:BlueM.Wave.TimeSeriesFile).
* Implement a constructor that calls `.readSeriesInfo()`
* Implement `.readSeriesInfo()` and store the information in `.TimeSeriesInfos` as a list of [`TimeSeriesInfo`](xref:BlueM.Wave.TimeSeriesInfo). Each `TimeSeriesInfo` must hold all the information required to uniquely identify and read a series from the file.
* If the file format can hold multiple time series and you want the user to be able to select which series to import, make `.UseImportDialog()` return `True`.
* Implement `.readFile()` to read those series that were selected for import (those stored in `.SelectedSeries`) from the file. Store the time series in `.Timeseries` as a dictionary with series index as key and [`TimeSeries`](xref:BlueM.Wave.TimeSeries) as values.
* If the file format's file extension is not unique, implement `.verifyFormat()` for verifying whether a file has this format.

### Registering the file format
The new file format must be registered
* Add a new entry to the Enum [`TimeSeriesFile.FileTypes`](xref:BlueM.Wave.TimeSeriesFile.FileTypes).
* Add the file format's file extension to [`TimeSeriesFile.FileExtensions`](xref:BlueM.Wave.TimeSeriesFile.FileExtensions) and to `TimeSeriesFile.FileFilter` if it is not yet included.
* Add a case for handling the file format's extension to the [`TimeSeriesFile.getFileType()`](xref:BlueM.Wave.TimeSeriesFile#BlueM_Wave_TimeSeriesFile_getFileType_System_String_) method. If other file formats use the same extension, use calls to `.verifyFormat()` to distinguish between them.

### Implementing file writing
If you want the file format to also be exportable from Wave:
* Implement `.writeFile()` to write a list of time series to a file.
* If the file format supports multiple series per file, add the format in [`TimeSeriesFile.SupportsMultipleSeries()`](xref:BlueM.Wave.TimeSeriesFile#BlueM_Wave_TimeSeriesFile_SupportsMultipleSeries_BlueM_Wave_TimeSeriesFile_FileTypes_).
* Add a case for the file format to `Wave.ExportTimeseries()`.
 * Optional: add metadata handling by implementing `.MetadataKeys()` and `.setDefaultMetadata()` and adding corresponding cases to `Wave.ExportTimeseries()`.