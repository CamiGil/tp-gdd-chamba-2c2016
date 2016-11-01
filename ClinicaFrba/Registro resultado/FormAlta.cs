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

namespace ClinicaFrba.Registrar_Agenta_Medico
{
    public partial class FormAlta : Form
    {
        SqlConnection conexion;


        public FormAlta()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cboPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand guardarAtencion = new SqlCommand("CHAMBA.RegistrarAtencion", conexion);
            guardarAtencion.CommandType = CommandType.StoredProcedure;

            guardarAtencion.Parameters.Add("@IdTurno", SqlDbType.Decimal).Value = cboPlan.ValueMember;
            guardarAtencion.Parameters.Add("@Sintomas", SqlDbType.VarChar).Value = textBox1.Text;
            guardarAtencion.Parameters.Add("@Diagnostico", SqlDbType.VarChar).Value = textBox2.Text;

            conexion.Close();
        }

        private void FormAlta_Load(object sender, EventArgs e)
        {
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            conexion.Open();

            SqlCommand listarAfiliados = new SqlCommand("CHAMBA.PosiblesPacientes", conexion);
            listarAfiliados.CommandType = CommandType.StoredProcedure;

            listarAfiliados.Parameters.Add("@IdMedico", SqlDbType.Decimal).Value = Configuraciones.usuario;
            listarAfiliados.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Configuraciones.fecha;

            SqlDataAdapter adapter = new SqlDataAdapter(listarAfiliados);
            DataTable table = new DataTable();
            adapter.Fill(table);

            cboPlan.DataSource = table;
            cboPlan.DisplayMember = "nombreUsuario";
            cboPlan.ValueMember = "Turn_Numero";
            conexion.Close();
        }
    }
}
