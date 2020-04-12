using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        private Graph _graph;

        public Form1()
        {
            InitializeComponent();
            startValue1();
        }

        private void createNewGraph()
        {
            pictureBox1.Image = null;
            try
            {
                int v = Convert.ToInt32(v_count.Text);
                int e = Convert.ToInt32(e_count.Text);
                dataGridView1.DataSource = Matrix.crateCleanTable(v, e);
                errorLabel.Text = "";
            }
            catch
            {
                errorLabel.Text = "Помилка при читанні даних";
            }
        }

        private void startValue1()
        {
            _graph = GraphItem.createTestGraph();
            dataGridView1.DataSource = Matrix.createIncidenceMatrix(_graph);
            v_count.Text = _graph.VertexCount.ToString();
            e_count.Text = _graph.EdgeCount.ToString();
            errorLabel.Text = "";
            richTextBox1.Text = "";
            vizGraph();
        }

        private void startValue2()
        {
            _graph = GraphItem.createTestGraph1();
            dataGridView1.DataSource = Matrix.createIncidenceMatrix(_graph);
            v_count.Text = _graph.VertexCount.ToString();
            e_count.Text = _graph.EdgeCount.ToString();
            errorLabel.Text = "";
            richTextBox1.Text = "";
            vizGraph();
        }

        private void updata()
        {
            try
            {
                _graph = Matrix.createGraphByIncidenceMatrix(getTable(dataGridView1));
                vizGraph();
                errorLabel.Text = "";
                richTextBox1.Text = "";
            }
            catch
            {
                errorLabel.Text = "Матриця інцидентності заповнена невірно.";
            }

        }

        private DataTable getTable(DataGridView dgv)
        {
            var dt = ((DataTable)dgv.DataSource).Copy();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (!column.Visible)
                {
                    dt.Columns.Remove(column.Name);
                }
            }
            return dt;
        }
        private void vizGraph()
        {
            dataGridView1.DataSource = Matrix.createIncidenceMatrix(_graph);
            pictureBox1.Image = GraphVisualization.getImage(_graph);
        }

        private void postman()
        {
            lab2.Postman p = new lab2.Postman(_graph);
            pictureBox1.Image = GraphVisualization.getImage(p.WorkGraph);
            String str = "Ціна шляху = " + p.Weigth.ToString() + "\n\n";
            for (int i = 0; i< p.CycleVertex.Count; i++)
            {
                str += p.CycleVertex[i].Name + "->";
            }
            str += p.CycleVertex[0].Name + "\n\n";
            for (int i = 0; i < p.CycleEdges.Count-1; i++)
            {
                str += p.CycleEdges[i].Name + "->";
            }
            str += p.CycleEdges[p.CycleEdges.Count - 1].Name + "\n";
            richTextBox1.Text = str;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            createNewGraph();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            startValue1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            postman();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            startValue2();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            startValue1();
        }
    }
}
