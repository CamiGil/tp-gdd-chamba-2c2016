using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Listados
{
    public partial class FormProfesionalesHoras : Form
    {
        SqlConnection conexion;
        public FormProfesionalesHoras()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormProfesionalesHoras_Load(object sender, EventArgs e)
        {
            SqlCommand cargar = new SqlCommand("SELECT Espe_Descripcion, Espe_Codigo FROM CHAMBA.Especialidades ORDER BY Espe_Descripcion", conexion);
            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable table = new DataTable();
            adapter.Fill(table);
            cboEspecialidad.DataSource = table;
            cboEspecialidad.DisplayMember = "Espe_Descripcion";
            cboEspecialidad.ValueMember = "Espe_Codigo";

            conexion.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboAño.Text != "" && cboEspecialidad.Text != "" && cboSemestre.Text != "")
            {
                conexion.Open();
                SqlCommand cargar = new SqlCommand("CHAMBA.ProfesionalesHoras", conexion);
                cargar.CommandType = CommandType.StoredProcedure;
                cargar.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = cboEspecialidad.SelectedValue;
                cargar.Parameters.Add("@Semestre", SqlDbType.Int).Value = cboSemestre.Text;
                cargar.Parameters.Add("@Año", SqlDbType.VarChar).Value = cboAño.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cargar);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                conexion.Close();
            }
            else
            {
                MessageBox.Show("Complete los criterios de busqueda");

            }
        }
    }
}
