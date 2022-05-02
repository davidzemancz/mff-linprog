using System;
using System.IO;

namespace Linprog {
    class Program {
        static void Main(string[] args) {
            try {
                if (args.Length < 1) { throw new Exception("At least one argument required"); }
                string firstArg = args[0].Trim().ToLower();
                if (firstArg == "-h" || firstArg == "--help") { // help
                    WriteHelpInfo();
                }
                else { // input filename
                    StreamReader streamReader = new StreamReader(firstArg);
                    string line = streamReader.ReadLine();
                    if (line.StartsWith("DIGRAPH")) {
                        while ((line = streamReader.ReadLine()) != null) {
                            
                        }
                    }
                    else if (line.StartsWith("WEIGHTED DIGRAPH")) {
                    }
                }

            } catch(Exception ex) {
                Console.WriteLine($"[{ex.GetType().Name}] {ex.Message}");
            }
        }

        static void WriteHelpInfo() {

        }
    }
}