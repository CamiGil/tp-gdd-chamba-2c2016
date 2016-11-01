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
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Decimal> numeros = new List<Decimal>();
            numeros.Add(0);
            numeros.Add(1);
            comboBox3.DataSource = numeros;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
                conexion.Open();
                conexion = new SqlConnection(@Configuraciones.datosConexion);
                SqlCommand guardar;
                guardar = new SqlCommand();
                guardar.CommandType = CommandType.StoredProcedure;

                guardar.CommandText = "ProfesionalCancelaTurno";
                guardar.Parameters.Add("@FechaInicial", SqlDbType.DateTime).Value = dateTimePicker1.Value.GetDateTimeFormats();
                guardar.Parameters.Add("@FechaFinal", SqlDbType.DateTime).Value = dtpDesde.Value.GetDateTimeFormats();
                guardar.Parameters.Add("@Tipo", SqlDbType.Decimal).Value = comboBox3.DataSource;
                guardar.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = Configuraciones.usuario;
                conexion.Close();
        }

        private void Calendario_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }
}
