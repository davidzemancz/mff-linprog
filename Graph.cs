using System;
using System.Collections.Generic;

namespace Linprog{
    class Graph{
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public void AddEdge(Edge edge){
            Edges.Add(edge);
        }

        public override string ToString(){
            string ret = "";
            foreach (Edge edge in Edges){
                ret += edge.ToString() + Environment.NewLine;
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