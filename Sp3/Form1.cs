using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Departamentos objDepto;
        Incendios objIncendios;
        TipoIncendio objTipoIncendios;
        Provincias objProvincias;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                objDepto = new Departamentos();
                objIncendios = new Incendios();
                objProvincias = new Provincias();
                objTipoIncendios = new TipoIncendio();
            }
            catch (Exception)
            {

                MessageBox.Show("Problemas con las tablas");
            }

            //creacion de tablas
            DataTable TablaProvincias = new DataTable();
            DataTable TablaDepartamentos  = new DataTable();
            DataTable TablaIncendios = new DataTable();
;
             TablaProvincias = objProvincias.getAll();
             TablaDepartamentos = objDepto.getAll();
            TablaIncendios = objTipoIncendios.getAll();

            //declaracion de nodos
            TreeNode Incendios;
            TreeNode Provincias;
            TreeNode Departamentos;

            Incendios = treeView1.Nodes.Add("Incendios");

            foreach (DataRow fProvincias in TablaProvincias.Rows)
            {
                Provincias = Incendios.Nodes.Add(fProvincias["Provincia"].ToString(),fProvincias["nombre"].ToString());
                Provincias.Tag=(fProvincias["Provincia"]);

                foreach (DataRow fDepartamento in TablaDepartamentos.Rows)
                {
                    if (Provincias.Tag.ToString() == fDepartamento["Provincia"].ToString())
                    {
                        Departamentos = Provincias.Nodes.Add(fDepartamento["nombre"].ToString());
                        Departamentos.Tag = (fDepartamento["Departamento"].ToString());
                    }

                }
            }
            

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //creacion de tablas
            DataTable TablaProvincias = new DataTable();
            DataTable TablaDepartamentos = new DataTable();
            DataTable TablaIncendio = new DataTable();
            DataTable TablaTipoIncendio = new DataTable();
            
            TablaProvincias = objProvincias.getAll();
            TablaDepartamentos = objDepto.getAll();
            TablaTipoIncendio = objTipoIncendios.getAll();
            TablaIncendio = objIncendios.getAll();

            Int32 Total = 0;
            Int32 cantidad = 0;
            int f = 0;
            dataGridView1.Rows.Clear();

            lblTotal.Text = "";
            foreach (DataRow fila in TablaTipoIncendio.Rows)
            {
                dataGridView1.Rows.Add(fila["Descripcion"]);

                foreach (DataRow filaIncendio in TablaIncendio.Rows)
                {
                    
                    if (fila["TipoIncendio"].ToString() == filaIncendio["TipoIncendio"].ToString())
                    {
                        Total = Total + Convert.ToInt32(filaIncendio["Cantidad"]);
                    }
                    cantidad = cantidad + Convert.ToInt32(filaIncendio["Cantidad"]);

                }

                dataGridView1.Rows[f].Cells[1].Value = Total;
                lblTotal.Text = cantidad.ToString();

                Total = 0;
                f++;
                cantidad = 0;
            }

        }

        private void cmdGrafico_Click(object sender, EventArgs e)
        {
            DataTable TablaProvincias = new DataTable();
            DataTable TablaDepartamentos = new DataTable();
            DataTable TablaIncendio = new DataTable();
            DataTable TablaTipoIncendio = new DataTable();
            TablaProvincias = objProvincias.getAll();
            TablaDepartamentos = objDepto.getAll();
            TablaTipoIncendio = objTipoIncendios.getAll();
            TablaIncendio = objIncendios.getAll();


            Series serie = new Series();

            chart1.Series.Clear();
            int cantidades = 0;
            foreach (DataRow filaTipo in TablaTipoIncendio.Rows)
            {
                serie = chart1.Series.Add(filaTipo["Descripcion"].ToString());


                foreach (DataRow filaIncendios in TablaIncendio.Rows)
                {
                    if (filaIncendios["TipoIncendio"].ToString() == filaTipo["TipoIncendio"].ToString())
                    {
                        cantidades = cantidades + Convert.ToInt32(filaIncendios["Cantidad"]);
                    }


                }
                serie.Points.AddXY(Convert.ToString(filaTipo["TipoIncendio"]), cantidades);
                cantidades = 0;

            }

           
        }
    }
}
