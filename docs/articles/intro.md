## Getting started
1. Download [Visual Studio Installer (Community Edition)](https://visualstudio.microsoft.com/de/downloads/)
1. Install Visual Studio with **.NET-desktop development tools**
1. Optionally install the extension [Microsoft Visual Studio Installer Projects 2022](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2022InstallerProjects) required for building the installer
1. Start Visual Studio and go to *Extras -> Options -> NuGet Package Manager -> Package Sources*
1. Add `https://api.nuget.org/v3/index.json` to package sources
1. Clone BlueM.Wave source code from https://github.com/bluemodel/BlueM.Wave
1. Clone BlueM.Datasets from https://github.com/bluemodel/BlueM.Datasets
1. Copy a valid TeeChart Pro license file to `BlueM.Wave\source\My Project\TeeChart.licenses` (see below for instructions on using the evaluation version of TeeChart for testing purposes)
1. Open `Wave.sln` in Visual Studio
1. Run Tests (*Test -> Run all Tests*)
1. Run Project (*Debug -> Start Debugging*)

## TeeChart license
BlueM.Wave uses [TeeChart .NET](https://www.steema.com/product/net) for all charts. To compile a fully functional version of BlueM.Wave, you need a valid TeeChart .NET Pro license file located at `BlueM.Wave\source\My Project\TeeChart.licenses`.

For testing purposes, you can use the evaluation version of TeeChart by creating an empty text file at `BlueM.Wave\source\My Project\TeeChart.licenses` or by excluding the corresponding entry `My Project\TeeChart.licenses` from the Wave project in Visual Studio. This will allow you to compile, but any charts will be displayed with a watermark.

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

## Building an installer
An installer (.msi) for BlueM.Wave can be built using the `Wave.Setup` project located at `setup\Wave.Setup.vdproj` which is a [Visual Studio Installer Project](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2022InstallerProjects) (see [docs](https://aka.ms/vdproj-docs)).

## Releases
To create a new release:
* Change the `AssemblyVersion` in the file `source\My Project\AssemblyInfo.vb`.
* Update the changelog in the file `source\CHANGELOG.md`.
* In Visual Studio, select the `Wave.Setup` project and update its `Version` property to the new version number.
* Visual Studio will ask to change the `ProductCode`, answer Yes.
* Commit and push the changes to the master branch together with a new tag consisting of the version number (e.g. "2.4.3")

When a new tag is pushed to the master branch, this triggers a [workflow](https://github.com/bluemodel/BlueM.Wave/actions/workflows/release.yml) which builds Wave and the installer and creates a new draft release on GitHub. This draft release must be published manually.
