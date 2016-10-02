using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class FormHistorialCambiosPlan : Form
    {
        SqlConnection conexion;
        public FormHistorialCambiosPlan()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormHistorialCambiosPlan_Load(object sender, EventArgs e)
        {

        }

        public void cargarDatos(String afiliado)
        {
            conexion.Open();
            SqlCommand cargar = new SqlCommand("CHAMBA.HistorialCambiosPlan", conexion);
            cargar.CommandType = CommandType.StoredProcedure;
            cargar.Parameters.Add("@Afiliado", SqlDbType.VarChar).Value = afiliado;
            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            conexion.Close();
        }
    }
}
