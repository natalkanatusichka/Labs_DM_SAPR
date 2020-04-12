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
            Vertex v1 = new Vertex(1, "S");
            Vertex v2 = new Vertex(2);
            Vertex v3 = new Vertex(3);
            Vertex v4 = new Vertex(4);
            Vertex v5 = new Vertex(5);
            Vertex v6 = new Vertex(6, "T");

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);
            graph.AddVertex(v6);

            graph.AddEdge(new Edge(v1, v2, 7));
            graph.AddEdge(new Edge(v1, v4, 14));
            graph.AddEdge(new Edge(v2, v3, 15));
            graph.AddEdge(new Edge(v2, v5, 10));
            graph.AddEdge(new Edge(v4, v3, 2));
            graph.AddEdge(new Edge(v4, v6, 6));
            graph.AddEdge(new Edge(v5, v6, 9));

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

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);
            graph.AddVertex(v6);

            graph.AddEdge(new Edge(v1, v2, 7));
            graph.AddEdge(new Edge(v1, v4, 14));
            graph.AddEdge(new Edge(v2, v3, 15));
            graph.AddEdge(new Edge(v2, v5, 10));
            graph.AddEdge(new Edge(v4, v3, 2));
            graph.AddEdge(new Edge(v4, v6, 6));
            graph.AddEdge(new Edge(v5, v6, 9));
            graph.AddEdge(new Edge(v2, v4, 3));

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
            Flow = 0;
            if (name == null)
            {
                Name = "e" + s.ID.ToString() + "-" + t.ID.ToString();
            }
            else Name = name;
        }

        public Vertex Source { get; set; }
        public Vertex Target { get; set; }
        public String Name { get; set; }
        /// <summary>
        /// Пропускна здатність
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Потік
        /// </summary>
        public int Flow { get; set; }

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
