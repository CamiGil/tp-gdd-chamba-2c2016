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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormCrearCancelaciónPaciente_Load(object sender, EventArgs e)
        {

            conexion.Open();
            
            SqlCommand cargar = new SqlCommand("CHAMBA.TurnosCancelablesPorPaciente", conexion);
            cargar.CommandType = CommandType.StoredProcedure;
            cargar.Parameters.Add("@Paciente", SqlDbType.Decimal).Value = Configuraciones.usuario;
            cargar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Configuraciones.fecha;
            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable table = new DataTable();
            adapter.Fill(table);

            comboBox2.DataSource = table;
            comboBox2.ValueMember = "Turn_Numero";
            comboBox2.DisplayMember = "Agen_Fecha";


            conexion.Close();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                if (textBox1.Text != "")
                {
                    if (comboBox3.Text != "")
                    {
                        conexion.Open();
                        SqlCommand cargar = new SqlCommand("CHAMBA.PacienteCancelaTurno", conexion);
                        cargar.CommandType = CommandType.StoredProcedure;
                        cargar.Parameters.Add("@Tipo", SqlDbType.Int).Value = comboBox3.SelectedIndex;
                        cargar.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = textBox1.Text;
                        cargar.Parameters.Add("@Turno", SqlDbType.Decimal).Value = decimal.Parse(comboBox2.SelectedValue.ToString());
                        cargar.ExecuteNonQuery();
                        conexion.Close();
                        MessageBox.Show("Datos guardados exitosamente");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Seleccione el tipo");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un motivo");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un turno");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
