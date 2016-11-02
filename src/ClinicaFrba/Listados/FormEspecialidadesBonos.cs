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
    public partial class FormEspecialidadesBonos : Form
    {
        SqlConnection conexion;
        public FormEspecialidadesBonos()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormEspecialidadesBonos_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboAño.Text != "" && cboMes.Text != "")
            {
                conexion.Open();
                SqlCommand cargar = new SqlCommand("CHAMBA.EspecialidadesBonos", conexion);
                cargar.CommandType = CommandType.StoredProcedure;
                cargar.Parameters.Add("@Semestre", SqlDbType.Int).Value = cboSemestre.Text;
                cargar.Parameters.Add("@Mes", SqlDbType.Int).Value = cboMes.SelectedValue;
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

        private void cboSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<int, string> meses = new Dictionary<int, string>();
            if (cboSemestre.Text == "1")
            {

                meses.Add(1, "Enero");
                meses.Add(2, "Febrero");
                meses.Add(3, "Marzo");
                meses.Add(4, "Abril");
                meses.Add(5, "Mayo");
                meses.Add(6, "Junio");

            }
            else
            {
                meses.Add(7, "Julio");
                meses.Add(8, "Agosto");
                meses.Add(9, "Septiembre");
                meses.Add(10, "Octubre");
                meses.Add(11, "Noviembre");
                meses.Add(12, "Diciembre");
            }
            meses.Add(0, "Todos");
            cboMes.DataSource = new BindingSource(meses, null);
            cboMes.DisplayMember = "Value";
            cboMes.ValueMember = "Key";
        }
    }
}
