using System;
using System.Collections.Generic;

namespace Linprog{
    class Graph{
        public Dictionary<Vertex, List<Edge>> Neighbors { get; set; } = new();
        public Dictionary<int, Vertex> Vertices { get; set; } = new();
        public void AddEdge(Edge edge){
            // Add vertex
            if (Vertices.ContainsKey(edge.First.Id)){
                edge.First = Vertices[edge.First.Id];
            } else{
                Vertices.Add(edge.First.Id, edge.First);
            }
            if (Vertices.ContainsKey(edge.Second.Id)){
                edge.Second = Vertices[edge.Second.Id];
            } else{
                Vertices.Add(edge.Second.Id, edge.Second);
            }

            // Add vertex's neighbors
            if (Neighbors.ContainsKey(edge.First)){
                Neighbors[edge.First].Add(edge);
            } else{
                Neighbors.Add(edge.First, new() { edge });
            }
        }

         public List<Edge[]> FindCyclesOfLengthThreeAndFour(){
            List<Edge[]> cycles = new();
            
            foreach (KeyValuePair<Vertex, List<Edge>> kvp in Neighbors){ 
                Vertex vertex = kvp.Key;
                foreach (Edge edge in kvp.Value){
                    if (Neighbors.ContainsKey(edge.Second)){
                        foreach (Edge edge2 in Neighbors[edge.Second]){ 
                            if (Neighbors.ContainsKey(edge2.Second)){
                                foreach (Edge edge3 in Neighbors[edge2.Second]){
                                    if (edge3.Second == vertex){ // Cycle of length three
                                        cycles.Add(new [] { edge, edge2, edge3 });
                                    }
                                    if (Neighbors.ContainsKey(edge3.Second)){
                                        foreach (Edge edge4 in Neighbors[edge3.Second]){
                                            if (edge4.Second == vertex){ // Cycle of length four
                                                cycles.Add(new [] { edge, edge2, edge3, edge4 });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return cycles;
        }

        public override string ToString(){
            string ret = "";
            foreach (KeyValuePair<Vertex, List<Edge>> kvp in Neighbors){
                foreach (Edge edge in kvp.Value){
                    ret += edge.ToString() + Environment.NewLine;
                }
            }
            return ret;
        }
    }

    class Edge{
        public Vertex First { get; set; }
        public Vertex Second { get; set; }
        public int Weight { get; set; }

        public Edge(Vertex first, Vertex second){
            First = first;
            Second = second;
        }

        public Edge(Vertex first, Vertex second, int weight) : this(first, second){
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{First} -- > {Second} ({Weight})";
        }
    }

    class Vertex{
        public int Id { get; set; }
        public Vertex(int id){
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}