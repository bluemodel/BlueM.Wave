BlueM.Wave Release Notes
========================

Version 2.12.2
--------------
NEW:
* Added support for reading GINA GBL files in hydraulic scenario format #212

CHANGED:
* When performing analyses based on years (GoodnessOfFit, AnnualStatistics) with series with interpretation block right, 
  the end date of each year is now more exactly defined as the last timestamp before the start of the next year

FIXED:
* GoodnessOfFit analysis: major performance improvement when processing series with NaN values
* Fixed first year missing from result of GoodnessOfFit analysis #210
* Fixed series reordering in TeeChart editor no longer working

Version 2.12.1
--------------
NEW:
* New toolbar buttons for interactively adding and removing markers to display series values at selected timestamps #209

FIXED:
* Fixed duplicate entries in zoom history when zooming by left-click dragging in the main chart
* Handle nonexistent files in the recently used files menu

Version 2.11.0
--------------
NEW:
* New analysis function TimeShift for shifting time series by a specified time interval #206
* New dialog for editing metadata of multiple series at once when exporting #202
* Added support for reading files in GINA CSV WEL format #203
* Added a user setting for showing/hiding the overview chart on startup #38

CHANGED:
* When series are removed, any unused custom axes are now also removed

FIXED:
* Fixed series not being removed from the overview chart when they are deleted using the TeeChart editor #205
* Fixed Y axes not auto-adjusting when series are activated or deactivated
* Fixed series having the same color when the color palette is exhausted, the color palette now repeats instead

API-CHANGES:
* New public function `TimeSeries.ShiftTime()` for shifting all timestamps by a specified time interval
* `TimeSeries.AddTimeInterval()` now correctly handles varying month lengths and leap days when offsetting by months or years

Version 2.10.0
--------------
NEW:
* Added support for reading and writing files in Delft-FEWS PI timeseries XML format #200

CHANGED:
* Updated about box

Version 2.9.2
-------------
NEW:
* Added support for reading files in GINA binary WEL format (GBL) #197

FIXED:
* Fixed crash when series containing values of Infinity are displayed in the chart. Such values are now treated as NaN by the chart. #199
* Fixed decimal numbers not being interpreted correctly when evaluating formulas in the Calculator analysis function if the system's decimal separator is not "." #198

Version 2.9.1
-------------
NEW:
* Added a user setting for the default font size of the chart #195

CHANGED:
* Changing chart user settings now has an immediate effect on the chart

FIXED:
* Fixed order of series specified in a WVP file not being preserved on import #194

Version 2.9.0
-------------
NEW:
* Added user settings and a dialog for changing them
* Added a user setting for the default series line width #192
* Added support for an optional series title suffix when importing series from a file #191

CHANGED:
* The list of recently used files in the Open menu is now persistent across sessions
* Changed the loggingLevel application setting to a user setting with a togglable option for showing debug messages

Version 2.8.3
-------------
NEW:
* Added support for wildcards when selecting series to import #173

FIXED:
* The Import series dialog is now always centered and focussed when importing from files via drag and drop

Version 2.8.2
-------------
FIXED:
* Analysis Comparison: if not enough coincident data points are available, a relevant error message is displayed
* WEL format: handle incomplete line with units
* Removed DotNetZip dependency and use .NET's IO.Compression instead #179

Version 2.8.1
-------------
NEW:
* Analysis AnnualStatistics: now also outputs the sum of values per year

FIXED:
* Analysis ChangeTimestep: fixed failure to execute with some time series with interpretation CumulativePerTimestep
* Analysis ChangeTimestep: fixed missing first node when output interpretation is CumulativePerTimestep

Version 2.8.0
-------------
NEW:
* Added support for reading HYBNAT WEL and BCS files #130
* Added support for exporting to HYBNAT BCS format #130

CHANGED:
* The select series dialog is now always displayed on top

FIXED:
* Analysis GoodnessOfFit: fixed volume error calculation if one of the compared time series contains NaN values #151
* Talsim clipboard content no longer requires the entry 'Einheit' to be present for WEL files
* Togglable toolbar buttons are now reset when the New button is clicked

API-CHANGES:
* New property `TimeSeries.NaNPeriods()` for retrieving a list of time periods consisting of NaN nodes
* New method `TimeSeries.UpdateNode()` for updating the value of an existing node
* New class `DateRange` representing a date range with defined start and end dates

Version 2.7.1
-------------
API-CHANGES:
* Allow using the API for reading time series files without requiring a reference to System.Windows.Forms

Version 2.7.0
-------------
NEW:
* New analysis function AnnualRecurrenceProbability which calculates the annual recurrence probility and return period of annual maxima using plotting position method

API-CHANGES:
* New property `TimeSeries.MaximumNode()` for retrieving the node (date and value) with the maximum value of a time series, optionally within a defined time period

FIXED:
* Improved handling of errors and edge cases when importing from Talsim clipboard
* WBL files can now also be contained in WLZIP files

CHANGED:
* Updated DHI.DFS to v22.0.3

Version 2.6.1
-------------
NEW:
* Added an error text in the Time Series Properties window when a time series' volume is NaN 
* Added a warning log message when executing the Goodness Of Fit analysis with time series whose volume is NaN

FIXED:
* Fixed superfluous metadata in export to ZRXP format #126
* Added a check for title uniqueness when a title is edited in the Time Series properties window
* Disabled the ability to sort columns in the Time Series Values window

Version 2.6.0
-------------
NEW:
* Added a new, simpler, series selection dialog for known file types
* Added the ability to reorder series from within the Time Series Properties window
* Wave project file (WVP):
  * Added support for additional series options in WVP files #117
  * When saving to WVP, a new dialog allows specifying which series options to save
  * When saving to WVP, file paths can now optionally be saved as relative
  * WVP files are now always read and written using UTF-8 encoding

FIXED:
* Improved performance and usability of Time Series Values window

CHANGED:
* Upgrade TeeChart to v4.2023.4.18

Version 2.5.0
-------------
NEW:
* Allow multiple files to be selected when importing files using the Import time series menu item 
* Added navigation buttons for navigating to the start and end of the currently active series
* New analysis function Decumulate
* Added support for importing SWMM time series file format 
* Added support for reading and writing REXCHANGE header values for ZRXP files #123

FIXED:
* Analysis function Accumulate now respects the input interpretation
* Time Series Properties and Values windows now restore on toolbar button press if previously minimized
* Importing from SWMM binary output format:
  * Read and store metadata
  * Include the element type in series names, making them unique #63
  * Fixed wrong units for "System" variables
  * Always use the "official" SWMM notation for units
* Importing from SWMM routing interface format:
  * Read and store metadata
* Exporting to SWMM routing interface format: 
  * Allow editing of series metadata before export
  * General improvements and additional checks
* Exporting to SWMM time series file format #118:
  * Renamed SWMM_DAT_MASS and SWMM_DAT_TIME export formats to SWMM_TIMESERIES
  * Removed automatic transformation of time series to 5 min equidistant timestep before export
  * Fixed output date format to use "/" as date separator
  * Added comment header line

CHANGED:
* Analysis function Cumulative renamed to Accumulate
* Added progress bar to Calculator analysis function
* Upgrade to System.Data.SQLite v1.0.118.0 and SQLite 3.42.0

API-CHANGES:
* Removed property `TimeSeriesFile.nLinesPerTimestamp`
* Removed the properties `TimeSeries.Objekt` and `TimeSeries.Type`
* New public method `Fileformats.WVP.Write_File()` for writing a Wave project file

Version 2.4.5
-------------
NEW:
* Added support for reading JAMS/J2000 timeseries result files

CHANGED:
* Unknown file formats will now always be attempted to be imported as CSV

FIXED:
* Fixed crash when attempting to verify HYSTEM-EXTRAN REG format on a different DAT file

Version 2.4.4
-------------
FIXED:
* Improved calculation of time series volume #112:
  * more supported units: "/s", "/min", "/h" and "/d" 
  * proper consideration of interpretation
  * any NaN nodes now cause the adjacent time steps (depending on the interpretation) to not contribute to the volume
* Fixed issue with deleting series or editing series properties in the properties window, 
  for series with extreme dates that cannot be entirely displayed in the chart #68

Version 2.4.3
-------------
FIXED:
* Fixed reading WBL files where the series indices provided in the accompanying WELINFO file are not 0-based

CHANGED:
* The update check and the link to the download page now point to GitHub releases

Version 2.4.2
-------------
FIXED:
* Analysis function GoodnessOfFit: fixed NaN result values causing the result chart to crash #105

CHANGED:
* Updated DHI.DFS to v21.0.0

Version 2.4.1
-------------
NEW:
* Added a button for deleting time series to the properties window
* Allow exporting multiple time series at the same time #104

CHANGED:
* Analysis result table: first column is now always frozen #99
* Analysis function GoodnessOfFit: slight improvement to scaling of volume error for display in radar chart #98

FIXED:
* Detect empty time series before attempting to import them
* Some fixes to reading and writing HYSTEM-EXTRAN REG format #106
* Mousewheel scroll in the overview chart now centers the zoom on the mouse #95

Version 2.4.0
-------------
NEW:
* New BlueM.Wave installer package #89:
  * Installs and uninstalls Wave in Windows
  * Registers the file types WVP and TEN to open in Wave
* Added support for using Windows "Open with" functionality for any file type
* Mouse wheel in charts now zooms #95
* The following analysis functions now display tabular results in a dedicated window #99
  * AnnualStatistics
  * GoodnessOfFit
  * Histogram
  * MonthlyStatistics

CHANGED:
* Analysis function GoodnessOfFit: 
  * More than two time series can now be analyzed at once
  * Results are now displayed as a radar chart #98
  * More warnings in the log if not all data is usable
* Show version numbers in the update check messages

FIXED:
* Improved performance when panning the main chart with the right mouse button

Version 2.3.1
-------------
CHANGED:
* When importing series from clipboard CSV data, their datasource is now stored as "Clipboard"
* When saving a project file, emit prominent warnings and errors about series that can not be saved
* When saving a chart, the native TeeChart format (*.TEN) is now selected by default

Version 2.3.0
-------------
NEW:
* Added support for ZRXP files containing ensemble forecast time series #92

Version 2.2.0
-------------
FIXED:
* Analysis function GoodnessOfFit: fixed calculation of logarithmic Nash-Sutcliffe efficiency:
  * Use the average of the logarithmic observed values instead of the logarithm of the average observed value in the equation
  * A small epsilon value of 1% of the average observed values is now added to all values before the logarithmic transform in order to deal with zero values
    See Pushpalatha et al. (2012) DOI:10.1016/j.jhydrol.2011.11.055

Version 2.1.1
-------------
FIXED:
* Significantly improved the speed of reading SYDRO binary WEL files (*.WBL)
* Improved handling of errors occurring while reading time series file metadata
* Added an error message when ZRE file metadata is incomplete

Version 2.1.0
-------------
NEW:
* Analysis function Cumulative: a start date can now be specified #87
* Analysis function AnnualStatistics: now optionally generates annual bounding box series (max, avg, min) #88

Version 2.0.0
-------------
CHANGED:
* License changed to LGPLv3, see files COPYING and COPYING.LESSER

API-CHANGES:
* The Wave window can no longer be shown using `Wave.Show()`. 
  Instead, a new `BlueM.Wave.App` instance must be instantiated, see API docs for details #84

Version 1.15.1
--------------
FIXED:
* Fixed failed check for parseable dates in Enter time series dialog not aborting the import process
* Errors encountered while reading certain file formats were not being handled correctly
* Fixed the Wave window not showing under some circumstances when using the CLI #83

Version 1.15.0
--------------
CHANGED:
* This document is now named CHANGELOG.md

FIXED:
* Fixed some issues with TEN import

API-CHANGES:
* Renamed class `FileFormatBase` to `TimeSeriesFile`
* Renamed property `TimeSeriesFile.FileTimeSeries` to `TimeSeriesFile.TimeSeries`
* Removed modules `FileFactory` and `FileFormats` and integrated functionality into class `TimeSeriesFile`, e.g.
  * `FileFactory.getFileInstance()` -> `TimeSeriesFile.getInstance()`,
  * `FileFormats.getFileType()` -> `TimeSeriesFile.getFileType()`, 
  * `FileFormats.FileTypes` -> `TimeSeriesFile.FileTypes`
* Renamed structure `SeriesInfo` to class `TimeSeriesInfo`
* Renamed property `TimeSeriesFile.SeriesList` to `TimeSeriesFile.TimeSeriesInfos`
* Encapsulated file extension constants within a new class `TimeSeriesFile.FileExtensions`

Version 1.14.2
--------------
FIXED:
* Fixed series being deleted in the Properties window not being propagated to the chart after a series was deleted in TeeChart
* Fixed the order in which time series are displayed not being consistent everywhere #79

API-CHANGES:
* Renamed the internal storage of time series to `Wave.TimeSeries` and changed its type to `TimeSeriesCollection`
* `Wave.Import_Files()` now accepts an enumerable of file paths as parameter

Version 1.14.1
--------------
FIXED:
* Fixed TEN files not being recognized as native TeeChart format

Version 1.14.0
--------------
API-CHANGES:
* Fixed broken application state when showing the Wave window using the API #77
* Allow multiple separate app instances to exist at the same time
* New alternative method for launching an app instance: `BlueM.Wave.App.Launch()`
* Introduced the namespace `BlueM.Wave.Fileformats` for all objects related to file formats
* Added a new public Enum `TimeSeriesFile.FileTypes` for file types
* Added a new method `FileFormats.getFileType()` for determining a file's type
* The method `FileFactory.getFileInstance()` now has an optional parameter for specifying the file type

Version 1.13.6
--------------
NEW:
* Added experimental support for reading SYDRO binary WEL files (*.WBL). Requires an accompanying *.WELINFO file for metadata info.
* Added ISO date format "yyyy-MM-dd HH:mm:ss" to list of date format presets in import dialog

FIXED: 
* Fixed empty time series being added after a failed import attempt (CSV)
* Fixed detection of files with endings such as .CHLO.WEL within WLZIP files (TALSIM clipboard)
* Fixed duplicate "(cut)" in series titles after cutting a time series

CHANGED:
* Added more information about detected or assumed file formats to the log
* Upgraded MathNet.Numerics to v5.0.0
* Upgraded DHI.DFS to v20.1.0
* Upgraded TeeChart Pro to 4.2022.8.23

API-CHANGES:
* Attempting to call `Timeseries.Cut()` with a start date on or after the end date now throws an ArgumentException

Version 1.13.5
--------------
CHANGED:
* Allow input values of up to 100000 for "First data line" in the import dialog

Version 1.13.4
--------------
NEW:
* Analysis function ChangeTimestep: added the possibility to specify the desired output interpretation #46

API-CHANGES:
* TimeSeries.ChangeTimestep now has an additional optional parameter for the output interpretation #46

Version 1.13.3
--------------
NEW: 
* Analysis function ChangeTimestep: added support for input interpretation BlockLeft #46
* Added a test project for unit testing

FIXED:
* Analysis function ChangeTimestep: fixed some cases of wrong results #46

Version 1.13.2
--------------
FIXED:
* Fixed some issues when reading TEN files

Version 1.13.1
--------------
NEW:
* Added a workaround for reading non-standard timestamps with an hour of 24 in ZRXP files

Version 1.13.0
--------------
NEW:
* Added new analysis functions Autocorrelation and LinearRegression
* Added a progress bar in the status bar for lengthy processes (currently only used for Autocorrelation analysis)

Version 1.12.3
--------------
FIXED:
* Fixed analyses inadvertently changing the original time series
* Analysis function GoodnessOfFit: fixed CSV formatted output in log

Version 1.12.2
--------------
FIXED: 
* Significantly improved performance when reading multiple series from a DFS0 file
* Analysis functions: fixed error when snychronizing two time series with more different than common timestamps
* The date time format used for displaying and entering dates and times now more consistently follows the user's Windows settings

Version 1.12.1
--------------
FIXED:
* Fixed detection of files with endings such as .BOF.WEL and .CWR.WEL within WLZIP files (TALSIM)

CHANGED:
* Upgraded to TeeChart Pro 4.2022.1.10

Version 1.12.0
--------------
NEW:
* Added the ability to export to DFS0 format #73
* Added the ability to convert to DFS0 using the CLI #73
* Added buttons for activating / deactivating all series at once #72

CHANGED:
* Internal code restructuring to MVC architecture #75
* Upgraded to Math.NET Numerics v4.15.0 from NuGet #74

Version 1.11.0
--------------
NEW:
* Added a button for selecting a color palette
* Analysis function GoodnessOfFit: added Kling-Gupta efficiency parameter #52
* Analysis function ChangeTimestep: the initial setting for the interpretation is now taken from the timeseries' properties
* Added support for importing SWMM5 LID report files #61

CHANGED:
* Default line width of series is now 2
* File import dialog: 
  * There is now a small delay (1s) between user input and the start of the search
  * The number of available and currently selected series is now displayed
* CSV format: extra spaces around titles and units are now removed during import
* Upgraded to TeeChart Pro 4.2021.11.2
* Switched from Ionic.Zip to DotNetZip

FIXED:
* Custom axes now become invisible automatically when not used by any active series #69
* Changing the title of a series in the TeeChart Editor no longer causes a desync with the internally stored time series #12
* Regression: Fixed automatic renaming of duplicate time series titles

REMOVED:
* Removed "Load Theme" menu option

NOTE: Due to a migration to a different issue tracking system, issue numbers have changed. Old numbers are denoted by BUG, new numbers by #

Version 1.10.6
--------------
This version fixes a number of issues in order to be able to support the full date range between 01.01.0001 and 31.12.9999 (BUG 749)

CHANGED:
* Import, analysis and export functions can now use the full date range between 01.01.0001 and 31.12.9999
* Chart display, panning and zooming is restricted to the displayable date range between 01.01.0100 and 31.12.9000
* Replaced DateTimePickers with MaskedTextBoxes for date time input and display
* Cut dialog: removed preview chart

FIXED:
* Improved performance when importing series with dates before 01.01.0100 and after 31.12.9000
* Analysis function "AnnualStatistics": fixed an error occurring with series ending in the year 9999

KNOWN ISSUES:
* Analysis Function "ChangeTimestep": Error when the last new timestep ends in year > 9999

Version 1.10.5
--------------
NEW:
* CLI: Added a new optional argument "-of" for specifiying the output format for conversions. 
  Currently supported output formats are CSV (default) and BIN.
  See documentation: https://wiki.bluemodel.org/index.php/Wave:CLI

Version 1.10.4
--------------
CHANGED:
* All analysis function now use the OS list separator for result output in the log
* Analysis function "MonthlyStatistics": 
  * added an option for selecting the start month for the chart
  * added more columns to the result output in the log
  * result output in the log is now "NaN" instead of 0 for months with no values
* Analysis function "GoodnessOfFit":
  * added an option for selecting the start month for the hydrological year
  * the "Description" of hydrological years in the result output now corresponds to the calendar year in which the hydrological year starts
* Analysis function "AnnualStatistics":
  * added an option for selecting the start month for the hydrological year

FIXED:
* Fixed TimeSeries properties Min, Max and Volume for series consisting only of NaN values
* Fixed an uncaught exception when attempting to add a log message without a Wave window instance

API-CHANGES:
* Class TimeSeries: new overloaded properties Nodes and NodesClean for retrieving nodes within a defined time period
* TimeSeries.SplitHydroYears() now has an optional parameter for specifiying the start month of the hydrological year (default: November)

Version 1.10.3
--------------
NEW:
* Added a command-line interface (CLI) with commands for importing and converting time series files to CSV
  See documentation: https://wiki.bluemodel.org/index.php/Wave:CLI

CHANGED:
* Export to CSV:
  * NaN values are now exported as "NaN"
  * When exporting multiple time series with timestamps not common to all series, non-existant nodes are exported as empty entries
  * Performance improvements 

API-CHANGES:
* New public class WVP for parsing and processing Wave project files (*.wvp)
* New property TimeSeries.NaNCount
* Removed obsolete TimeSeries methods getKontiZRE, getKontiZRE2 and getKontiZRE3 from public API (BUG 717)

Version 1.10.2
--------------
NEW:
* Added support for reading the DHI MIKE Dfs0 file format

Version 1.10.1
--------------
NEW:
* Analysis function "ChangeTimestep": added an option for ignoring NaN values

FIXED:
* Prevent OverflowException when displaying time series beginning with a NaN value as a step function in the overview chart (BUG 748)
* Prevent DateTimePicker in TimeSeriesValues window from attempting to display a date outside of the supported range
* Fix counter display in TimeSeriesValues window
* Ensure that nodes outside of the supported date range are properly removed when importing a series and improve logging thereof (BUG 749)

Version 1.10.0
--------------
NEW:
* Time Series Values: a new window for displaying time series values as a table (experimental)
  * optionally displays markers in main chart for currently selected rows in table

Version 1.9.7
-------------
CHANGED:
* Analysis function "GoodnessOfFit": volumes, volume error and average values are now calculated based on only the timestamps common to both timeseries

API-CHANGES:
* New method Timeseries.Synchronize() for synchronizing two timeseries by only keeping the common timestamps

Version 1.9.6
-------------
FIXED:
* Prevent errors when trying to add log messages without a log window

Version 1.9.5
-------------
NEW:
* Analysis function "Comparison" now also calculates the linear correlation coefficient
* Added the file extension ".ZRXP" (in addition to ".ZRX") for recognizing the ZRXP file format

CHANGED:
* Zooming by dragging the mouse in the main chart now requires at least 5 horizontal pixels in order to cause a zoom

FIXED:
* Fixed wording in Histogram analysis (non-exceedance instead of subceedance)

Version 1.9.4
-------------
NEW:
* Added a "Calculator" analysis function for performing mathematical operations on time series (BUG 722)

CHANGED:
* Vertical axes now have a maximum offset of 5 by default

FIXED:
* Fixed wrong update notification and wrong information in the about box when Wave is executed from another application
* Improved handling and display of time series datasource information
* Fixed detection of a single NaN value at the end of a time series when visualizing NaN values
* When auto-adjustment of Y-axes to current view is switched on, custom vertical axes are now also auto-adjusted
* Switching the visualization of NaN values off no longer removes user-defined color bands
* Removing NaN values from series now also updates the series displayed in the chart
* Series titles containing ":" are now correctly enclosed in quotes when saving WVP files

Version 1.9.3
-------------
FIXED:
* Wave project files (WVP): allow specification of "tab" as separator and use of quotes for series names containing ":"
* API: fixed error when calling FileFormatBase.getTimeSeries() more than once

Version 1.9.2
-------------
NEW:
* Added buttons for zooming in and out a fixed amount
* Added a button for zooming to the next extent in the zoom history

CHANGED:
* Delete temporary files created when importing clipboard data after import is complete

FIXED:
* Fixed inconsistent zoom behavior when loading a TEN file saved with versions before 1.9.1
* Fixed zoom rectangle in main chart not showing after visualizing and then hiding NaN values
* Fixed inconsistencies in zoom history

Version 1.9.1
-------------
CHANGED:
* NaN values:
  * NaN values are now displayed as gaps in the chart
  * NaN values are now saved and loaded in TEN files (BUG 725)
* Zoom/pan behavior:
  * Changed the zoom/pan behavior in both the overview and main chart to zoom with left click, pan with right-click
  * Clicking and dragging towards the left in the main chart no longer zooms to the previous extent, but instead performs a normal zoom
  * Removed the buttons for changing the pan/zoom mode
* Improved the speed of displaying lines in the overview chart
* Removed the Time Series Statistics dialog
* Changes to the Time Series Properties dialog:
  * Added a button for showing/hiding statistics
  * Changed the background color of editable columns
  * The dropdown button for changing the interpretation now only appears for the currently active cell
* Automatically added axes now have their grid visibility switched off by default 

Version 1.9.0
-------------
NEW:
* Time Series Properties and Statistics are now two separate dialogs
* Every loaded time series now stores information about its original datasource, which is displayed in the properties dialog
* Log messages are now categorized in levels and colored to make warnings and errors more prominent (BUG 730)
* The logging level can be set in Wave.exe.config by setting loggingLevel to either debug, info, warning or error (default is info)
* Added error and warning counters to the status bar
* Added an update notification and a check for update help menu item

CHANGED:
* The Volume property of a time series is now only computed for time series with units ending with "/s", otherwise returning NaN.
  Before, the Sum property was used as a fallback for Volume.
* Updated to .NET Framework 4.8

FIXED:
* Fixed some errors when the chart contains series that are not of type line

API-CHANGES:
* New property TimeSeries.DataSource of type KeyValuePair(String fileName, String seriesName). 
  Is used by Wave for displaying the datasource in the properties dialog, filling the MRU files menu item and for reloading time series from files.

Version 1.8.6
-------------
CHANGED:
* Analysis functions Comparison and DoubleSumCurve: the result diagrams no longer contain tooltips of the points' timestamps

FIXED:
* Analysis functions Comparison and DoubleSumCurve: significantly increased the creation speed of the result diagrams

Version 1.8.5
-------------
API-CHANGES:
* CSV.Write_File() now accepts an optional CultureInfo parameter for setting the date time format, number format and the list separator

Version 1.8.4
-------------
CHANGED:
* When reading CSV files, remove any quotes around titles and units
* When writing CSV files, the date time format, number format and the list separator are now taken from the current Windows region settings

Version 1.8.3
-------------
NEW:
* Most recently loaded file is now displayed in main window title
* Any clipboard text content can now be imported as CSV (press Ctrl+V or use menu item)

CHANGED:
* Tab characters are now represented with a visual representation in the file preview of the file import dialog

Version 1.8.2
-------------
FIXED:
  * Fixed crashes with very small or large dates in time series (BUG 749)
    Timeseries are now cut upon import to minimum 01.01.0100 and maximum 31.12.9900
  * Fixed error when importing CSV files without units

KNOWN ISSUES:
  * DateTimePickers have a minimum date of 01.01.1753 and a maximum date of 31.12.9998
    and will display these even if timeseries / the view extends beyond that range (BUG 749)

Version 1.8.1
-------------
FIXED:
* Analysis Histogram: the nodes of the cumulative probability lines are now displayed at the upper end of each bin, 
  instead of at the middle of each bin

Version 1.8.0
-------------
NEW:
* Added support for different file encodings:
  * Default encoding for reading and writing files is taken from the operating system (usually ISO-8859-1)
  * File encoding can be autodetected from byte order marks or manually set in the Import dialog
* Added a button and dialog for managing axes
* The title, interpretation and unit of time series are now editable in the Properties dialog
* Series can now be deleted in the Properties dialog
* Added a menu item for opening the release notes
* Added support for reading BW_TMP.DAT files in HYDRO_AS-2D version 5 format (previous versions still supported)
* Added a button for removing nodes with NaN values from the currently loaded time series

CHANGED:
* Analysis result charts are now displayed in a dedicated window
* The Rename Series button and dialog have been removed, series titles can now be changed in the Properties dialog
* The Change Timestep button has been removed, a corresponding function is now located in the analysis dialog
* The analysis selection dialog now displays a description of the selected analysis function and a link to the wiki
* The Change Timestep function is now implemented natively
* Reading SYDRO SQLite time series files is now implemented natively
* Reading and writing SYDRO binary files is now implemented natively
* When loading PRMS files, the file import dialog is now displayed, allowing for the selection of time series

FIXED:
* HYSTEM-EXTRAN-REG-Format: 
  * Recognize different line types as defined by the character in column 20, 
  * Recognize -9999 as error values
* The Properties dialog now auto-updates when series are added or removed
* Updated SWMM.DllAdapter, now supports the new interface of SWMM OUT files
* Fixed unintended use of the system's locale settings when exporting files in formats CSV, UVF, ZRXP, SWMM_DAT_MASS and SWMM_DAT_TIME
* Fixed the interpretation of the output of the ChangeTimestep function
* When writing BIN files, NaN values are now properly converted to the error value -9999.999
* Date formats entered in the import dialog are now properly escaped if they contain special characters such as "/" or ":"
* Selected series are no longer deselected every time an input is changed in the import dialog
* Time series statistics Minimum, Maximum, Average, Sum and Volume (shown in the properties dialog) 
  are now calculated by ignoring NaN values. (Before, any NaN values would lead to undefined values)

API-CHANGES:
* TimeSeries instances now each have a unique Id within the session
* New method TimeSeries.ChangeTimestep()
* The following changes have been made to public methods and properties of FileFormatBase (which are inherited by all file formats):
  * Replaced TimeSeries (array) with FileTimeSeries (dictionary)
  * Replaced structure ColumnInfo with SeriesInfo (with slightly different properties)
  * Replaced Columns (array) with SeriesList (list of SeriesInfo objects)
  * Replaced selectedColumns (array) with SelectedSeries (list of SeriesInfo objects)
  * Renamed selectAllColumns() to selectAllSeries()
  * Renamed Read_Columns() to readSeriesInfo()
  * Renamed Read_File to readFile()

Version 1.7.1
-------------
NEW
* Added a new analysis function "TimestepAnalysis" which computes the time difference between
  each node of one or multiple time series and displays them as new time series
* Added support for reading SYDRO SQLite time series files (*.db) of class ForecastTimeseries

CHANGED:
* The "Monthly Statistics" analysis function now asks the user whether the time series' values 
  correspond to the current or previous month
* UI: Import series dialog improved with adjustable size of available series display

FIXED:
* Fixed not being able to open SYDRO SQLite time series files from a network path
* Time series with interpretation BlockLeft were not being displayed as stairs
* The output time series of the "Change timestep" function now have a defined interpretation
* Fixed exceptions occurring due to invalid min/max dates in the Change timestep dialog

Version 1.7.0
-------------
NEW
* Added support for reading SYDRO SQLite time series files (*.db) (only in x86 version)

FIXED:
* Analysis function "Cumulative" now ignores NaN values in time series 
  (before, the cumulative sum would become NaN as well)

Version 1.6.3
-------------
NEW
* Added support for reading PRMS statistic variables result files (statvar.dat)

CHANGED:
* Reworked the cut time series dialog. The initial start and end dates for the cut are now taken 
  from the currently displayed time period of the main chart.
* Unparseable values in time series files are no longer written to the log individually. Instead, 
  the total number of NaN values of each time series is written to the log when they are displayed 
  for the first time.

FIXED:
* Fixed unintended use of the system's locale settings when reading files in HYDRO_AS-2D and ZRXP format

Version 1.6.2
-------------
NEW:
* Added support for storing an "Interpretation" property for time series. 
  Interpretation is currently only known for series read from WEL files and the TALSIM clipboard,
  all other series have a default interpretation value of 99 (Undefined).
  Series with interpretation 2 (BlockRight), 3 (BlockLeft) and 5 (CumulativePerTimestep) 
  are displayed as (inverted) stairs in the chart, all others are displayed normally (linear interpolation between nodes).
* Added a button for auto-adjusting the Y-axes to the current viewport
  (currently only works for left and right, but not custom axes)
* Added support for reading Q_Strg.dat files in HYDRO_AS-2D version 5 format (previous versions still supported)
* Added read support for PRMS output files (annual and monthly summaries, as well as DPOUT)
* Added write support for ZRXP format
* Added write support for UVF format
* Added write support for SYDRO binary format (BIN) (only in x86 version)
* Added a button for visualizing NaN values of the currently active series
  This also causes the time periods consisting of NaN values to be written to the log
* Added metadata handling for time series, metadata is displayed in the series properties dialog
* Implemented reading and writing of metadata for file formats UVF and ZRXP
* Added a dialog for editing metadata before export
* Added a new analysis function "Cumulative" which converts a time series to cumulative values
* Added the possibility of specifying custom import settings in Wave project files (*.wvp) (BUG 720)
* Added a button and a dialog for merging time series (BUG 719)
* Added a dialog for specifying a reference date (beginning of simulation) when importing HYDRO_AS-2D files
* The Open File menu now includes a list of recently used files (not stored across sessions)
* Added a button for changing the time step of a time series (BUG 717) (only in x86 version)
* Added the ability to process more clipboard contents from TALSIM 
  (including BIN files and multiple files/series at once) (BUG 695)
* Added the ability to extract WEL files from WLZIP files of the same name

CHANGED:
* The "Enter time series" dialog has been improved, unparseable values are now converted to NaN 
  instead of causing an abort of the import.
* The "Remove error values" function has been changed to "Convert error values" and now converts 
  user-specified error values to NaN
* When importing time series from SMUSI REG format, the title is taken from the first line of the file, 
  before any comma

FIXED:
* The Reload from Files command now respects the previously made series selection for each file
* Imported files are no longer kept as file objects in memory (should lead to less memory usage)
* ZRXP format: the time part of the timestamp is now optional (00:00:00 assumed if missing)
* ZRXP format: values equal to the error value specified as "RINVAL" in the file header are 
  converted to NaN during import
* UVF format: fixed reading and writing of time series starting before 1900
* UVF format: values equal to -777 are recognized as error values and converted to NaN during import
* BIN format: values equal to -9999.999 are recognized as error values and converted to NaN during import
* Fixed export to SMUSI REG format. Time series must be equidistant with 5 minute time step.

API-CHANGES:
* The function TimeSeries.getCleanZRE() has been replaced with two separate functions:
  Timeseries.removeNaNValues() and Timeseries.convertErrorValues()

KNOWN ISSUES:
* When saving a chart to the native TEN format of TeeChart, any NaN values contained in the series are lost 
  (i.e. the nodes with the NaN values are omitted from the series).

Version 1.6.1
-------------
NEW:
* Added support for loading and saving Wave project files (*.wvp) (BUG 705)
* Added a button for displaying time series properties in a table view (BUG 711)
* Added the ability to load TALSIM simulation results using the clipboard (by pressing Ctrl+V) (BUG 695)
* Added an "AnnualStatistics" analysis function which calculates statistics such as
  min, max, avg and volume of a time series for each hydrological year
* The "Comparison" analysis function now fits a simple linear regression line to the data
  (using the Math.NET Numerics library)
* The analysis function Histogram now allows the user to specify the bins (BUG 397). 
  Also, the histogram result data is now written to the log.
* The analysis function MonthlyStatistics now writes the result data to the log
* Added a button for removing user-specified error values from series
* Added a button for renaming series
* Added a dropdown button for zooming to a specific series
* Added support for exporting series to CSV format (BUG 318)
* Added support for importing files in the ZRXP format (*.zrx)
* GoodnessOfFit (GoF) analysis function:
  * Added "Logarithmic Nash-Sutcliffe Efficiency" as a new GoF parameter
  * The GoF parameters can now optionally be calculated for individual hydrological years
* Added support for importing BW_TMP.DAT result files from HYDRO_AS-2D

CHANGED:
* Error values in time series such as e.g. -777 and -9999 are now imported and displayed as regular values.
  Previously, when importing from BIN and DWD-T-L format, these would be replaced with NaN.
  The new "Remove error values" function can be used to remove these error values, if desired. (BUG 693)
* Analysis GoodnessOfFit: Volume error is based on actual volume integrated over time 
  if the time series unit ends with "/s"
* Removed support for the DWD-T-L format
* If a time series contains duplicate nodes, the duplicate nodes are now simply discarded 
  and a warning is written to the log instead of aborting the import

FIXED:
* The decimal mark selector in the import series dialog now actually works (BUG 351)
* Comparison analysis function: changed the result series to a point series because a line series automatically 
  sorts itself by the x-values and thus connects the points in the wrong order (not time-based)

Version 1.6
-----------
NEW:
* Significantly improved the possibilities for viewing and navigating time series:
  - New toolbar buttons for zooming, panning, zoom undo and zoom all
  - Start and end date for display can be set using date/time pickers
  - Display range can be set to exact date/time ranges using a simple interface
  - Buttons for navigating in time using a user-specified time increment
  - All changes to the viewport are recorded in a history that can be undone

Version 1.5
-----------
NEW:
* Support for importing GISMO files
* Support for importing HYDRO-AS_2D files
* Support for importing UVF files
* Support for importing SYDRO binary files (x86 version only)
* Support for loading TeeChart themes (*.xml) 
* The date format can now be specified while importing time series (BUG 686)

CHANGED:
* Loading a TEN-file does not cause existing series to be deleted, 
  the series contained in the TEN-file can be imported optionally
* More options for cutting time series (cut all series at once and keep original series)
* Small improvements to layout and functionality
* Switched to .NET Framework 3.5

REMOVED:
* RVA files no longer supported

Version 1.4
-----------
Initial Release under the BSD-2 License, see LICENSE.txt for more information.

Version 1.3
-----------
NEW:
* Wave can now read time series from SWMM binary result files (*.OUT)

FIXED:
* Import of time series from TeeCharts native TEN files

UPDATE:
* Import of SMUSI REG files: Empty lines (dry weather periods) are improted correctly

CHANGED:
* Upgrade to TeeChart v4

Version 1.2
-----------
UPDATE:
* Internal storage of sampling points re-engineered

NEW:
* Analysis function Goodness of Fit with new indicators: Volume error, Correlation coefficient, Hydrologic deviation

Version 1.1
-----------
NEW:
* Analysis functions: Comparison, Double sum analysis, Goodness of Fit, Statistics
* Import formats for SMB and REG files
* Time series are assorted to different axes according to the time series units
* ReRead Files": Deletes all times series in current instance of BlueM.Wave and reimports the time series from it's respective files again (Bug 388)

FIXED:
* Cutting time series

Version 1.0
-----------
* Start of version control