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

namespace ClinicaFrba.Cancelar_Atencion
{
    public partial class FormCrearCancelaciónProfesional : Form
    {
        SqlConnection conexion;
        public FormCrearCancelaciónProfesional()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Ingrese un motivo");
                }
                else
                {
                    conexion.Open();

                    SqlCommand guardar;
                    guardar = new SqlCommand();
                    guardar.CommandType = CommandType.StoredProcedure;

                    guardar.CommandText = "ProfesionalCancelaTurno";
                    guardar.Parameters.Add("@FechaInicial", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    guardar.Parameters.Add("@FechaFinal", SqlDbType.DateTime).Value = dtpDesde.Value;
                    guardar.Parameters.Add("@Tipo", SqlDbType.Decimal).Value = decimal.Parse(comboBox3.SelectedValue.ToString());
                    guardar.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = Configuraciones.usuario;
                    conexion.Close();
                    MessageBox.Show("Datos guardados exitosamente");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Seleccione el tipo");
            }
        }

        private void Calendario_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void FormCrearCancelaciónProfesional_Load(object sender, EventArgs e)
        {
            dtpDesde.MinDate = Configuraciones.fecha;
            dtpDesde.Value = Configuraciones.fecha;
            dateTimePicker1.MinDate = Configuraciones.fecha;
            dateTimePicker1.Value = Configuraciones.fecha;
        }
    }
}
