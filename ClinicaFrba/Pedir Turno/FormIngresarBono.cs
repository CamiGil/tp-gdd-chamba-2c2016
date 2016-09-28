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

namespace ClinicaFrba.Pedir_Turno
{
    public partial class FormIngresarBono : Form
    {
        SqlConnection conexion;

        public FormIngresarBono()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.FormClosed += Configuraciones.validarCierreVentana;
        }

        private void modificarEstadoControles(Boolean estado)
        {
            numAfiliado.Enabled = estado;
            numBono.Enabled = estado;
        }

        private Boolean validarCamposVacios()
        {
            if (!string.IsNullOrEmpty(numAfiliado.Text) || !string.IsNullOrEmpty(numBono.Text))
            {
                return true;
            }
            return false;
        }

        //BOTÓN ACEPTAR
        private void button1_Click(object sender, EventArgs e)
        {
            modificarEstadoControles(false);
            if (validarCamposVacios())
            {
                conexion.Open();

            }
        }
        private void botonCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
