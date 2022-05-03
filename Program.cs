using System;
using System.IO;

namespace Linprog{
    class Program{
        
        /// <summary>
        /// Entry-point
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args){
            try {
                if (args.Length < 1) { throw new Exception("At least one argument required"); }
                string firstArg = args[0].Trim().ToLower();
                firstArg = "/home/david/mffuk/linprog/mff-linprog/data/ukolPrakticky/uloha2_1/vstup-000.txt";
                if (firstArg == "-h" || firstArg == "--help"){ // help
                    WriteHelpInfo();
                }
                else{ // input filename
                    StreamReader reader = new StreamReader(firstArg);
                    string firstLine = reader.ReadLine();
                    if (firstLine.StartsWith("WEIGHTED DIGRAPH")) {
                        ReadWeightedDirectedGraph(firstLine, reader);
                    }
                    else if (firstLine.StartsWith("GRAPH")) {
                        throw new NotImplementedException();
                    }
                }

            } catch (Exception ex){
                Console.WriteLine($"[{ex.GetType().Name}] {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Write help informations
        /// </summary>
        static void WriteHelpInfo(){
            // TODO
        }

        /// <summary>
        /// Read directed graph from StreamReader
        /// </summary>
        /// <param name="firstLine">First line of file; format (WEIGHTED DIGRAPH 4 6:)</param>
        /// <param name="reader">Stream reader</param>
        static void ReadWeightedDirectedGraph(string firstLine, StreamReader reader){
            // Get graph params
            string[] lineArr = firstLine.Remove(firstLine.Length - 1, 1).Split(' ');
            int verticesCount = int.Parse(lineArr[2]);
            int edgesCount = int.Parse(lineArr[3]);

            // Read edges (format '0 --> 1 ( 4)')
            string line;
            while ((line = reader.ReadLine()) != null){
                lineArr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int vertex1 = int.Parse(lineArr[0]);
                int vertex2 = int.Parse(lineArr[2]);
                int weight = int.Parse(lineArr[4].Remove(lineArr[4].Length - 1, 1));

                Console.WriteLine($"Edge {vertex1}-{vertex2}; weight {weight}");
            }
        }
    }
}