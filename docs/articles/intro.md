## Getting started
1. Download [Visual Studio Installer (Community Edition)](https://visualstudio.microsoft.com/de/downloads/)
1. Install Visual Studio with **.NET-desktop development tools**
1. Start Visual Studio and go to *Extras -> Options -> NuGet Package Manager -> Package Sources*
1. Add `https://api.nuget.org/v3/index.json` to package sources
1. Clone BlueM.Wave source code from https://github.com/bluemodel/BlueM.Wave
1. Copy a valid TeeChart Pro license file to `BlueM.Wave\source\My Project\TeeChart.licenses` (see below for instructions on using the evaluation version of TeeChart for testing purposes)
1. Open `Wave.sln` in Visual Studio
1. Set `Wave` as the startup project
1. Run project (*Debug -> Start Debugging*)

Optional setup steps:
* To run tests in `Wave.Tests`:
  * Clone BlueM.Datasets from https://github.com/bluemodel/BlueM.Datasets into the same parent directory as BlueM.Wave
  * Run Tests (*Test -> Run all Tests*), see below for details
* To build the installer project `Wave.Setup`:
  * Install the extension [Microsoft Visual Studio Installer Projects 2022](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2022InstallerProjects), see below for details

## TeeChart license
BlueM.Wave uses [TeeChart .NET](https://www.steema.com/product/net) for all charts. To compile a fully functional version of BlueM.Wave, you need a valid TeeChart .NET Pro license file located at `BlueM.Wave\source\My Project\TeeChart.licenses`.

For testing purposes, you can use the evaluation version of TeeChart by creating an empty text file at `BlueM.Wave\source\My Project\TeeChart.licenses` or by excluding the corresponding entry `My Project\TeeChart.licenses` from the Wave project in Visual Studio. This will allow you to compile, but any charts will be displayed with a watermark.

## Testing
The repository contains an assembly `Wave.Tests` for unit testing. 

Some of the tests use test data from the [BlueM.Datasets](https://github.com/bluemodel/BlueM.Datasets) repository, which needs to be checked out alongside BlueM.Wave in the same parent folder.

There are two ways to run tests:
* from within Visual Studio using the *Text Explorer* window (see also [this article](https://learn.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2022))
* from the command-line with the following command (see also [this arcticle](https://learn.microsoft.com/en-us/visualstudio/test/vstest-console-options?view=vs-2022)):
```bat
vstest.console.exe BlueM.Wave\tests\bin\x64\Debug\Wave.Tests.dll /Settings:BlueM.Wave\tests\tests.runsettings
```

To add new tests, follow the pattern of the existing ones and/or refer to the [MSTest framework docs](https://docs.microsoft.com/en-us/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022).

All tests defined in `Wave.Tests` are automatically run in a GitHub action whenever a push to the master branch or to a pull request occurs (see [workflow file](https://github.com/bluemodel/BlueM.Wave/blob/master/.github/workflows/run-tests.yml)).

## Debug log messages
Debug log messages can be created by calling 
```vbnet
Log.AddLogEntry(levels.debug, "message")
```
and are only visible in the log if the user setting `loggingLevel` is set to `debug`.

## API
The BlueM.Wave API provides methods for reading time series from files, manipulating and displaying them.

To access the API, include a reference to Wave.exe (or to the Wave project if you have the source code) in your .NET project.

The repository contains a project `Wave.Examples` with some examples of API usage.

See the [API documentation](../api/index.md)

## Building an installer
An installer (.msi) for BlueM.Wave can be built using the `Wave.Setup` project located at `setup\Wave.Setup.vdproj`. This is a Visual Studio Installer Project which requires a [Visual Studio extension](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2022InstallerProjects) to be installed (see [docs](https://aka.ms/vdproj-docs)).

## Releases
To create a new release:
* Change the `AssemblyVersion` in the file `source\My Project\AssemblyInfo.vb`.
* Update the changelog in the file `source\CHANGELOG.md`.
* In Visual Studio, select the `Wave.Setup` project and update its `Version` property to the new version number.
* Visual Studio will ask to change the `ProductCode`, answer Yes.
* Commit and push the changes to the master branch together with a new tag consisting of the version number (e.g. "2.4.3")

When a new tag is pushed to the master branch, this triggers a [workflow](https://github.com/bluemodel/BlueM.Wave/actions/workflows/release.yml) which builds Wave and the installer and creates a new draft release on GitHub. This draft release must be published manually.
