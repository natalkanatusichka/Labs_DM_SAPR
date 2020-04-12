﻿using System;
using System.Drawing;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApplication5
{
    class GraphVisualization
    {
        /// <summary>
        /// Перетворює вказаний граф в рисунок
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static Image getImage(Graph graph)
        {
            String fileName = "graph";
            File.Delete(@".\" + fileName + ".jpg");
            var graphViz = new GraphvizAlgorithm<Vertex, Edge>(graph);
            graphViz.GraphFormat.Ratio = GraphvizRatioMode.Auto;
            //graphViz.GraphFormat.NodeSeparation = 0.75;
            //graphViz.GraphFormat.RankSeparation = 15;
            //graphViz.GraphFormat.Size = new GraphvizSizeF(10, 10);
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            graphViz.Generate(new FileDotEngine(), @".\" + fileName);

            try
            {
                var f = File.Open(@".\" + fileName + ".jpg", FileMode.Open);
                Image bmp = Bitmap.FromStream(f);
                f.Close();
                return bmp;
            }
            catch { return null; }
        }

        /// <summary>
        /// Налаштування відображення вершини
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FormatVertex(object sender, FormatVertexEventArgs<Vertex> e)
        {
            e.VertexFormatter.Label = e.Vertex.Name;
        }

        private static void FormatEdge(object sender, FormatEdgeEventArgs<Vertex, Edge> e)
        {
            e.EdgeFormatter.HeadArrow = new GraphvizArrow(GraphvizArrowShape.None);
            e.EdgeFormatter.Label = new GraphvizEdgeLabel();
            e.EdgeFormatter.Label.Value = e.Edge.Weight.ToString();
        }
    }

    public sealed class FileDotEngine : IDotEngine
    {
        public string Run(GraphvizImageType imageType, string dot, string outputFileName)
        {
            string output = outputFileName;
            File.WriteAllText(output, dot);

            var startInfo = new ProcessStartInfo
            {
                FileName = @"E:\Study\4_course\1_semester\Help\Дискретні моделі в САПР\Lab1\Lab_1\Lab_1\WindowsFormsApplication5\bin\Debug\WindowsFormsApplication5.exe",  // Путь к приложению
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = string.Format(@"{0} -Tjpg -O", output)
            };

            // assumes dot.exe is on the path:
            Process p = Process.Start(startInfo);
            p.WaitForExit();
            return output;
        }
    }
}
