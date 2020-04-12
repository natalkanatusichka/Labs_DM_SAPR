using System.Collections.Generic;

namespace WindowsFormsApplication5
{
    /// <summary>
    /// Алгоритм прима працює, якщо:
    ///     -- граф зв'язний
    ///     -- у графі відсутні петлі
    /// </summary>
    class Prim
    {
        private IncidenceMatrix _m;

        /// <summary>
        /// Створює максимальний кістяк, за допомогою алгоритма Прима
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public Graph usePrim(Graph g)
        {
            //Читаємо з графу g всі вершини та ребра
            List<Title_Vertex> allV = new List<Title_Vertex>();
            foreach (Vertex v in g.Vertices)
            {
                allV.Add(new Title_Vertex(v));
            }
            List<Title_Edge> allE = new List<Title_Edge>();
            foreach (Edge e in g.Edges)
            {
                allE.Add(new Title_Edge(e));
            }
            //Створюємо матрицю інцидентності графа
            _m = new IncidenceMatrix(allE.ToArray(), allV.ToArray());
            primAlgoritm();
            return createGraph(_m);                      
        }

        private void primAlgoritm()
        {
            //Позначаємо в матриці інцидентності першу вершину як використану
            _m._v[0].state = true;

            while (!isEnd()) iteration();
        }

        /// <summary>
        /// Перевіряє, чи всі вершини використані
        /// </summary>
        /// <returns></returns>
        private bool isEnd()
        {
            bool r = true;
            foreach (Title_Vertex v in _m._v)
            {
                if (!v.state)
                {
                    r = false;
                    break;
                }
            }
            return r;
        }

        private void iteration()
        {
            //Шукаємо найважче з ребер, що прилягають до використаних вершин
            int maxE = getMaxEdge();

            //Видаляємо розглянуте ребро з матриці інцидентності (_c)
            TwoNumber n = serachConectedVertises(_m._v, _m._e[maxE]);
            _m._c[n._n1, maxE] = 0;
            _m._c[n._n2, maxE] = 0;
            //Якщо використана тільки одна з вершин, 
            //додаємо позначаємо ребро як використане
            if (!(_m._v[n._n1].state &&  _m._v[n._n2].state))
            {
                //Позначаємо ребро як використане
                _m._e[maxE].state = true;
                //Позначимо обидві вершини як використані
                _m._v[n._n1].state = true;
                _m._v[n._n2].state = true;
            }
        }

        /// <summary>
        /// Шукає найважче ребро, що прилягає хоч до однієї
        /// використаної вершини, і повертає його номер в масиві
        /// </summary>
        /// <returns></returns>
        private int getMaxEdge()
        {
            int max = int.MinValue;
            int maxe = -1;
            for (int j = 0; j < _m._v.Length; j++)
            {
                //якщо вершина використана
                if (_m._v[j].state)
                {
                    for (int i = 0; i < _m._e.Length; i++)
                    {
                        //якщо і-те ребро прилягає до вершини
                        if (_m._c[j, i] == 1)
                        {
                            //перевіряємо чи воно найважче
                            if (_m._e[i].weight > max)
                            {
                                max = _m._e[i].weight;
                                maxe = i;
                            }
                        }
                    }
                }
            }

            return maxe;
        }

        /// <summary>
        /// Шукає номера вершин в масиві, що з'єднані вказаним ребром
        /// </summary>
        /// <param name="_v"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private  TwoNumber serachConectedVertises(Title_Vertex[] _v, Title_Edge e)
        {
            //Знаходимо вершини, які з'єднюються ребром
            TwoNumber number = new TwoNumber();
            for (int j = 0, t = 0; j < _v.Length; j++)
            {
                if (_v[j].vertex.CompareTo(e.edge.Source) == 0)
                {
                    number._n1 = j;
                    t++;
                }
                else if (_v[j].vertex.CompareTo(e.edge.Target) == 0)
                {
                    number._n2 = j;
                    t++;
                }
            }

            return number;
        }
        
        /// <summary>
        /// Створює граф з вказаної матриці інцидентності
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private Graph createGraph(IncidenceMatrix m)
        {
            Graph g = new Graph();
            foreach(Title_Vertex v in m._v)
            {
                g.AddVertex(v.vertex);
            }
            foreach(Title_Edge e in m._e)
            {
                if (e.state) g.AddEdge(e.edge);
            }
            return g;
        }
        
    }

    class TwoNumber
    {
        public int _n1 = -1;
        public int _n2 = -1;
    }

    class Title_Edge
    {
        public Edge edge;
        public int weight;
        public bool state;

        public Title_Edge(Edge e)
        {
            edge = e;
            weight = e.Weight;
            state = false;
        }
    }

    class Title_Vertex
    {
        public Vertex vertex;
        public bool state;

        public Title_Vertex(Vertex v)
        {
            vertex = v;
            state = false;
        }
    }

    class IncidenceMatrix
    {
        public Title_Edge[] _e;
        public Title_Vertex[] _v;
        public int[,] _c;

        public IncidenceMatrix(Title_Edge[] e, Title_Vertex[] v)
        {
            _e = e;
            _v = v;
            _c = new int[_v.Length, _e.Length];
            generate();
        }

        /// <summary>
        /// Заповнює значення матриці інцидентності
        /// </summary>
        private void generate()
        {    
            for (int i = 0; i < _e.Length; i++)
            {
                for (int j = 0,  t = 0; t!=2; j++)
                {
                    if ( _v[j].vertex.CompareTo(_e[i].edge.Source) == 0)
                    {
                        _c[j, i] = 1;
                        t++;
                    }
                    else if (_v[j].vertex.CompareTo(_e[i].edge.Target) == 0)
                    {
                        _c[j, i] = 1;
                        t++;
                    }
                } 
            }
        }
    }
}
