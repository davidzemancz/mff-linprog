# Requirements
* [.NET SDK for .NET 6](https://docs.microsoft.com/en-us/dotnet/core/sdk) for build

# Build
* Use *dotnet build* command.

# Run
* Use *linprog vstup-XXX.txt vystup-XXX.mod -d -r* command.

# Usage
* *linprog [-h --help] [inputFileName] [outputFileName] [-r --run] [-d --debug]*.
* If *outputFileName* is not specified, output.mod is used.

## Options
* -h|--help - Display help informations.
* -r|--run - Run glpsol automatically.
* -d|--debug - Display debug informations during run.