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

        private void calculateFlow()
        {
            Flow f = new Flow(_graph);
            int r = f.setMaxFlow();
            richTextBox1.Text = "Flow = " + r.ToString();
            pictureBox1.Image = GraphVisualization.getImage(_graph);
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
            calculateFlow();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            startValue1();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            createNewGraph();
        }
    }
}
