using System;
using System.Collections.Generic;
using System.IO;

namespace Linprog{
    class Program{
        
        /// <summary>
        /// Entry-point
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args){
            bool debug = false;

            try {
                if (args.Length < 1) { throw new Exception("At least one argument required"); }
                string firstArg = args[0];
                if (firstArg == "-h" || firstArg == "--help"){ // help
                    WriteHelpInfo();
                }
                else{ // input filename
                    string inputFile = firstArg;
                    string outputFile = args.Length > 1 ? args[1] : "";

                    // TODO
                    debug = Array.Exists(args, arg => arg == "-d" || arg == "--debug");
                    bool runGlpk = Array.Exists(args, arg => arg == "-r" || arg == "--run");

                    StreamReader reader = new(firstArg);
                    StreamWriter writer = new(firstArg);
                    string firstLine = reader.ReadLine();
                    if (firstLine.StartsWith("WEIGHTED DIGRAPH")) {
                        Graph graph = ReadWeightedDirectedGraph(firstLine, reader);
                        WriteGlpkScript(graph);
                    }
                    else if (firstLine.StartsWith("GRAPH")) {
                        throw new NotImplementedException();
                    }
                }

            } catch (Exception ex){
                Console.WriteLine($"[{ex.GetType().Name}] {ex.Message}");
                if(debug) Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Write help informations
        /// </summary>
        static void WriteHelpInfo(){
            // TODO
            Console.WriteLine("Usage");
            Console.WriteLine(" linprog [-h --help] [inputFileName] [outputFileName] [-r --run] [-d --debug]");
        }

        /// <summary>
        /// Read directed graph from StreamReader
        /// </summary>
        /// <param name="firstLine">First line of file; format (WEIGHTED DIGRAPH 4 6:)</param>
        /// <param name="reader">Stream reader</param>
        /// <returns>Graph</returns>
        static Graph ReadWeightedDirectedGraph(string firstLine, StreamReader reader){
            Graph graph = new();
            
            // Get graph params
            string[] lineArr = firstLine.Remove(firstLine.Length - 1, 1).Split(' ');
            int verticesCount = int.Parse(lineArr[2]);
            int edgesCount = int.Parse(lineArr[3]);

            // Read edges (format '0 --> 1 ( 4)')
            string line;
            while ((line = reader.ReadLine()) != null){
                lineArr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int first = int.Parse(lineArr[0]);
                int second = int.Parse(lineArr[2]);
                int weight = int.Parse(lineArr[4].Remove(lineArr[4].Length - 1, 1));

                graph.AddEdge(new Edge(new(first), new(second), weight));
            }

            return graph;
        }

        static void WriteGlpkScript(Graph graph){
            
        }
    }
}