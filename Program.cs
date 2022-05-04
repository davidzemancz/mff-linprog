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
                else{ 
                    // File names
                    string inputFile = firstArg;
                    string outputFile = args.Length > 1 ? args[1] : "./output.mod";

                    // Flags
                    debug = Array.Exists(args, arg => arg == "-d" || arg == "--debug");
                    bool runGlpk = Array.Exists(args, arg => arg == "-r" || arg == "--run");

                    // Reader file and write output
                    using(StreamReader reader = new(inputFile))
                    using(StreamWriter writer = new(outputFile)){
                        string firstLine = reader.ReadLine();
                        if (firstLine.StartsWith("WEIGHTED DIGRAPH")) {
                            Graph graph = ReadWeightedDirectedGraph(firstLine, reader);
                            WriteGlpkScript(graph, writer);
                        }
                        else if (firstLine.StartsWith("GRAPH")) {
                            throw new NotImplementedException();
                        }
                    }
                }

            } catch (Exception ex){
                Console.WriteLine($"[{ex.GetType().Name}] {ex.Message}");
                if (debug) Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Write help informations
        /// </summary>
        static void WriteHelpInfo(){
            Console.WriteLine("Usage");
            Console.WriteLine(" linprog [-h --help] [inputFileName] [outputFileName] [-r --run] [-d --debug]");
            Console.WriteLine("Options");
            Console.WriteLine(" -h|--help        Display help informations.");
            Console.WriteLine(" -r|--run         Run GLPK automatically.");
            Console.WriteLine(" -d|--debug       Display debug informations during run.");
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

        static void WriteGlpkScript(Graph graph, StreamWriter writer){
            List<Edge[]> cyclesOfLengthThreeAndFour = graph.FindCyclesOfLengthThreeAndFour();

            /*
                var x >= 0;
                var y, >= 3, <= 4;
                var z >= 0;
                maximize obj: 3*x + 5*y - 2*z;
                p1: 2*x + 3*z <= 5;
                p2: 2*z - 3*y <= 8;
                solve;
                end;
            */

            // Variables
            string objectiveFunction = "";
            foreach (KeyValuePair<Vertex, List<Edge>> kvp in graph.Neighbors){ 
                foreach (Edge edge in kvp.Value){
                    writer.WriteLine($"var r{edge.First.Id}_{edge.Second.Id} >= 0, <=1;");
                    objectiveFunction += $"r{edge.First.Id}_{edge.Second.Id}*{edge.Weight} + ";
                }
            }
            if (objectiveFunction.Length > 0) objectiveFunction = objectiveFunction.Remove(objectiveFunction.Length - 2, 2);

            // Objective function
            writer.WriteLine($"minimize obj: {objectiveFunction};");

            // Conditions
            int i = 0;
            foreach (Edge[] cycle in cyclesOfLengthThreeAndFour){
                writer.Write($"c{i++}: ");
                for (int j = 0; j < cycle.Length; j++)
                {
                    writer.Write($"r{cycle[j].First.Id}_{cycle[j].Second.Id} ");
                    if (j < cycle.Length - 1) writer.Write("+ ");
                }
                writer.Write($">= 1;");
                writer.WriteLine();
            }

            // Solve
            writer.WriteLine($"solve;");
            writer.WriteLine($"end;");

            /*
            foreach (List<Edge> cycle in cyclesOfLengthThreeAndFour){
                Console.WriteLine($"----- Cycle ({cycle.Count}) -----");
                foreach (Edge edge in cycle){
                    Console.WriteLine(edge.ToString());
                }
                Console.WriteLine("---------------------");
            }
            */
        }
    }
}