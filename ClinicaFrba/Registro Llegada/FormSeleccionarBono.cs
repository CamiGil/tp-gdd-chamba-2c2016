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

namespace ClinicaFrba.Registro_Llegada
{
    public partial class FormSeleccionarBono : Form
    {
        SqlConnection conexion;
        public FormSeleccionarBono()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        public void cargarBonos(decimal Turno)
        {
            conexion.Open();

            SqlCommand cargar = new SqlCommand("CHAMBA.CargarBonosParaTurno", conexion);
            cargar.CommandType = CommandType.StoredProcedure;
            cargar.Parameters.Add("Turno", SqlDbType.Decimal).Value = Turno;
            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            conexion.Close();

            cboBono.DataSource = tabla;
            cboBono.DisplayMember = "Bono_Numero";
 
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }
    }
}
