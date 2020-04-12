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

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);

            graph.AddEdge(new Edge(v1, v2, 5));
            graph.AddEdge(new Edge(v1, v3, 8));
            graph.AddEdge(new Edge(v2, v4, 3));
            graph.AddEdge(new Edge(v2, v5));
            graph.AddEdge(new Edge(v3, v2, 4));
            graph.AddEdge(new Edge(v3, v5, 7));
            graph.AddEdge(new Edge(v1, v5, 4));

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
    public class Graph : AdjacencyGraph<Vertex, Edge> { }

}
