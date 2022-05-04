# Requirements
* [.NET SDK for .NET 6](https://docs.microsoft.com/en-us/dotnet/core/sdk) needed for build.

# Usage
* Use *linprog vstup-XXX.txt vystup-XXX.mod -d -r* command.
* In general *linprog [-h --help] [inputFileName] [outputFileName] [-r --run] [-d --debug]*.
* If *outputFileName* is not specified, *output.mod* is used.

## Build
* Use *dotnet build* command.

## Options
* -h|--help - Display help informations.
* -r|--run - Run glpsol automatically.
* -d|--debug - Display debug informations during run.

# Program structure
* Program
    * Main(string[] args) : void - Entry point.
    * WriteHelpInfo() : void - Write help informations to output.
    * ReadWeightedDirectedGraph(string firstLine, StreamReader reader) : Graph - Read input to Graph object instance and return it.
    * WriteGlpkScript(Graph graph, StreamWriter writer) : void - Write data from Graph instance to .mod file using MathProg language.
    * RunGlpSol(string outputFile) : void - Automatically run *glpsol -m outputFile.mod* command.
* Graph
    * Directed graph object that stores dictionary of vertices and of their neighbors.
* Vertex 
    * Only holds its Id. 
* Edge
    * Oriented edge object that holds its first and second vertex.