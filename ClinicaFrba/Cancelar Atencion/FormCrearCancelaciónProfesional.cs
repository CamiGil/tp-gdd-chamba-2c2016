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
            if(textBox2.Text== "")
                MessageBox.Show("Complete la fecha inicial");
            else if (textBox3.Text == "")
                MessageBox.Show("Complete la fecha final");
            else
            {

                SqlCommand guardar;
                guardar = new SqlCommand();
                guardar.CommandType = CommandType.StoredProcedure;

                guardar.CommandText = "ProfesionalCancelaTurno";
                guardar.Parameters.Add("@FechaInicial", SqlDbType.DateTime).Value = textBox2.Text;
                guardar.Parameters.Add("@FechaFinal", SqlDbType.DateTime).Value = textBox3.Text;
                guardar.Parameters.Add("@Tipo", SqlDbType.Decimal).Value = comboBox3.DataSource;
                guardar.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = Configuraciones.usuario;
            }
        }
    }
}
