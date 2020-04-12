using System;
using System.Drawing;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System.IO;
using System.Diagnostics;


/// <summary>
/// Містить опис графу, його вершин та ребер.
/// </summary>
namespace WindowsFormsApplication5
{
    class GraphItem
    {
        public static Graph createTestGraph()
        {
            Graph graph = new Graph();
            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);
            Vertex v3 = new Vertex(3);
            Vertex v4 = new Vertex(4);
            Vertex v5 = new Vertex(5);
            Vertex v6 = new Vertex(6);
            Vertex v6 = new Vertex(7);

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);
            graph.AddVertex(v6);
            graph.AddVertex(v7);

            graph.AddEdge(new Edge(v1, v2, 2));
            graph.AddEdge(new Edge(v2, v3, 4));
            graph.AddEdge(new Edge(v3, v4, 3));
            graph.AddEdge(new Edge(v4, v5, 2));
            graph.AddEdge(new Edge(v5, v3, 2));
            graph.AddEdge(new Edge(v4, v6, 6));
            graph.AddEdge(new Edge(v5, v6, 1));
            graph.AddEdge(new Edge(v4, v6, 4));
            graph.AddEdge(new Edge(v4, v7, 5));
            graph.AddEdge(new Edge(v6, v7, 4));
            graph.AddEdge(new Edge(v5, v7, 3));
            graph.AddEdge(new Edge(v2, v7, 4));
            graph.AddEdge(new Edge(v1, v7, 3));
            graph.AddEdge(new Edge(v3, v7, 2));

            return graph;
        }

        public static Graph createTestGraph1()
        {
            Graph graph = new Graph();
            Vertex v1 = new Vertex(1);
            Vertex v2 = new Vertex(2);
            Vertex v3 = new Vertex(3);
            Vertex v4 = new Vertex(4);
            Vertex v5 = new Vertex(5);
            Vertex v6 = new Vertex(6);
            Vertex v7 = new Vertex(7);

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);
            graph.AddVertex(v6);
            graph.AddVertex(v7);

            graph.AddEdge(new Edge(v1, v2, 2));
            graph.AddEdge(new Edge(v2, v3, 4));
            graph.AddEdge(new Edge(v3, v4, 3));
            graph.AddEdge(new Edge(v4, v5, 2));
            graph.AddEdge(new Edge(v5, v3, 2));
            graph.AddEdge(new Edge(v4, v6, 6));
            graph.AddEdge(new Edge(v5, v6, 1));
            graph.AddEdge(new Edge(v4, v6, 4));
            graph.AddEdge(new Edge(v4, v7, 5));
            graph.AddEdge(new Edge(v6, v7, 4));
            graph.AddEdge(new Edge(v5, v7, 3));
            graph.AddEdge(new Edge(v2, v7, 4));
            graph.AddEdge(new Edge(v1, v7, 3));
            graph.AddEdge(new Edge(v3, v7, 2));

            return graph;
        }

    }


    /// <summary>
    /// Опис вершини графу
    /// </summary>
    public class Vertex : IComparable<Vertex>
    {
        public Vertex(int id, String name = null)
        {
            ID = id;
            if (name == null)
            {
                Name = "v" + id.ToString();
            }
            else Name = name;
        }

        public int ID { get; set; }
        public String Name { get; set; }

        public int CompareTo(Vertex other)
        {
            return ID.CompareTo(other.ID);
        }

        public Vertex clone()
        {
            return new Vertex(this.ID, this.Name);
        }
    }

    /// <summary>
    /// Опис ребра графу, що з'єднює дві вершини Vertex
    /// </summary>
    public class Edge : IEdge<Vertex>, IComparable<Edge>
    {
        public Edge(Vertex s, Vertex t, int weight = 1, String name = null)
        {
            Source = s;
            Target = t;
            Weight = weight;
            if (name == null)
            {
                Name = "e" + s.ID.ToString() + "-" + t.ID.ToString();
            }
            else Name = name;
        }

        public Vertex Source { get; set; }
        public Vertex Target { get; set; }
        public String Name { get; set; }
        public int Weight { get; set; }

        public int CompareTo(Edge other)
        {
            return Name.CompareTo(other.Name);
        }

        
    }

    /// <summary>
    /// Граф, в якого вершини описуються класом Vertex, 
    /// а дуги за допомогою класу Edge
    /// </summary>
    public class Graph : AdjacencyGraph<Vertex, Edge>
    {
        public Graph clone()
        {
            Graph g = new Graph();
            g.AddVertexRange(this.Vertices);
            g.AddEdgeRange(this.Edges);
            return g;
        }
    }

}
