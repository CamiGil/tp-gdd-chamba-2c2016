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

namespace ClinicaFrba.Cancelar_Atencion
{
    public partial class FormCrearCancelaciónPaciente : Form
    {
        SqlConnection conexion;
        public FormCrearCancelaciónPaciente()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.FormClosed += Configuraciones.validarCierreVentana;
        }

        private void label1_Click(object sender, EventArgs e)
        {

            conexion.Open();

            consultarFecha();

            conexion.Close();

        }

        private void consultarFecha()
        {
            String query = "SELECT Turn_Fecha FROM CHAMBA.Turnos t JOIN CHAMBA.Pacientes P ON "+
                           "(p.Paci_Numero = t.Turn_Paciente) "
                           +" WHERE pr.Paci_Numero = "+ Configuraciones.usuario;

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            comboBox2.DataSource = tabla;
            comboBox2.DisplayMember = "Turn_Fecha";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormCrearCancelaciónPaciente_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
