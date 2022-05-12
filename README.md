# Intro
Porgram for generating MathProg scripts for glpk solver.

# Requirements
* [.NET SDK for .NET 6](https://docs.microsoft.com/en-us/dotnet/core/sdk) needed for build.

# Usage
* Use *linprog vstup-XXX.txt vystup-XXX.mod -d -r* command.
* In general *linprog [-h --help] [inputFileName] [outputFileName] [-r --run] [-d --debug]*.
* If *outputFileName* is not specified, *output.mod* is used.

## Build
* Use *dotnet build* command.
* Program builds to *./bin/Debug/net6.0/* directory

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

# Output
* set Edges := {(v1,v2,weight),...}; - List of edges.
* var r{(i, j, w) in Edges}, >= 0, <=1, integer; - Variable r(i,j,w) for each edge in {0,1} indicating, whether edge shoud be removed or not.
* minimize obj: sum{(i, j, w) in Edges} r[i, j, w]*w; - Minimizing objective function.
* c0: r[v1,v2,w1] + r[v2,v3,w2] + r[v3,v1,w3] >= 1; - Condition for each cycle of lenth three of four. Means that at least one edge must be removed (r[v1,v2,w] set to 1).
