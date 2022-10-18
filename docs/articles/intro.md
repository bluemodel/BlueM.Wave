## Getting started
1. Download [Visual Studio Installer (Community Edition)](https://visualstudio.microsoft.com/de/downloads/)
1. Install Visual Studio with **.NET-desktop development tools**
1. Start Visual Studio and go to *Extras -> Options -> NuGet Package Manager -> Package Sources*
1. Add `https://api.nuget.org/v3/index.json` to package sources
1. Clone BlueM.Wave source code from https://github.com/bluemodel/BlueM.Wave
1. Clone BlueM.Datasets from https://github.com/bluemodel/BlueM.Datasets
1. To use TeeChart Pro trial version, create an empty file `BlueM.Wave\Wave\My Project\TeeChart.licenses` or remove the corresponding entry from the project in Visual Studio
1. Open `Wave.sln` in Visual Studio
1. Run Tests (*Test -> Run all Tests*)
1. Run Project (*Debug -> Start Debugging*)

## TeeChart license
BlueM.Wave uses [TeeChart .NET](https://www.steema.com/product/net) for all charts. To compile a fully functional version of BlueM.Wave, you need a TeeChart .NET Pro license.

For testing purposes, you can use the evaluation version of TeeChart creating an empty file `BlueM.Wave\Wave\My Project\TeeChart.licenses` or removing the corresponding entry from the project in Visual Studio. This will allow you to compile, but any charts will be displayed with a watermark.

## Testing
The repository contains an assembly `Wave.Tests` for unit testing. Tests can be run from within Visual Studio. To add new tests, follow the pattern of the existing ones and/or refer to the [MSTest framework docs](https://docs.microsoft.com/en-us/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022).

## Debug log messages
Debug log messages can be created with `Log.AddLogEntry(levels.debug, "message")` and are only visible in the log if the application setting `loggingLevel` is set to `debug`.

To set the `loggingLevel` to `debug`, edit the section `applicationSettings` in the file `Wave.exe.config` after building to be as follows:
```xml
    <applicationSettings>
        <BlueM.Wave.My.MySettings>
            <setting name="loggingLevel" serializeAs="String">
                <value>debug</value>
            </setting>
        </BlueM.Wave.My.MySettings>
    </applicationSettings>
```

## API
The BlueM.Wave API provides methods for reading time series from files, manipulating and displaying them.

To access the API, include a reference to Wave.exe (or to the Wave project if you have the source code) in your .NET project.

The repository contains a project `Wave.Examples` with some examples of API usage.

See the [API documentation](../api/index.md)