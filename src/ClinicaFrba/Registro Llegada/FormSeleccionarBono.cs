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
        decimal turno;
        public FormSeleccionarBono()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        public void cargarBonos(decimal turno)
        {
            conexion.Open();

            this.turno = turno;

            SqlCommand cargar = new SqlCommand("CHAMBA.CargarBonosParaTurno", conexion);
            cargar.CommandType = CommandType.StoredProcedure;
            cargar.Parameters.Add("Turno", SqlDbType.Decimal).Value = turno;
            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            conexion.Close();            
            

            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show("El usuario no cuenta con bonos disponibles");
                this.Close();
            }
            else
            {
                cboBono.DataSource = tabla;
                cboBono.DisplayMember = "Bono_Numero";
                cboBono.ValueMember = "Bono_Numero";
                this.ShowDialog();
            }

 
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cboBono.Text != "")
            {
                conexion.Open();

                SqlCommand cargar = new SqlCommand("CHAMBA.RegistrarLlegada", conexion);
                cargar.CommandType = CommandType.StoredProcedure;
                cargar.Parameters.Add("Turno", SqlDbType.Decimal).Value = turno;
                cargar.Parameters.Add("Bono", SqlDbType.Decimal).Value = cboBono.SelectedValue;
                cargar.Parameters.Add("Fecha", SqlDbType.DateTime).Value = Configuraciones.fecha;
                SqlDataAdapter adapter = new SqlDataAdapter(cargar);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                conexion.Close();

                MessageBox.Show("Turno confirmado exitosamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un bono");
            }
        }

        private void FormSeleccionarBono_Load(object sender, EventArgs e)
        {

        }
    }
}
