using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Text;


namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        private Graph _graph;

        public Form1()
        {
            InitializeComponent();
            startValue();         
        }

        private void startValue()
        {
            _graph = GraphItem.createTestGraph();
            dataGridView1.DataSource = Matrix.createIncidenceMatrix(_graph);
            v_count.Text = _graph.VertexCount.ToString();
            e_count.Text = _graph.EdgeCount.ToString();
            dataGridView2.DataSource = null;
            pictureBox2.Image = null;
            errorLabel.Text = "";
            vizGraph();
        }

        private void vizGraph()
        {
            dataGridView1.DataSource = Matrix.createIncidenceMatrix(_graph);
            pictureBox1.Image = GraphVisualization.getImage(_graph);         
        }

        private void runPrim()
        {
            Prim prim = new Prim();
            Graph primG = prim.usePrim(_graph);
            dataGridView2.DataSource = Matrix.createIncidenceMatrix(primG);
            pictureBox2.Image = GraphVisualization.getImage(primG);
        }

        private void updata()
        {
            dataGridView2.DataSource = null;
            pictureBox2.Image = null;
            try
            {
                _graph = Matrix.createGraphByIncidenceMatrix(getTable(dataGridView1));
                vizGraph();
                errorLabel.Text = "";
            }
            catch
            {
                errorLabel.Text = "Матриця інцидентності заповнена невірно.";
            }
            
        }

        private void createNewGraph()
        {
            dataGridView2.DataSource = null;
            pictureBox2.Image = null;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            updata();        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            startValue();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runPrim();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            createNewGraph();
        }
    }

    

    
     
}
