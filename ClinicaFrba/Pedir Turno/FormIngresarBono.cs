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
        /*-------------------------------------------ATRIBUTOS-----------------------------------------------*/
        private SqlConnection conexion;

        /*-------------------------------------------CONSTRUCTOR---------------------------------------------*/
        public FormIngresarBono()
        {
            InitializeComponent();
            this.conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.FormClosed += Configuraciones.validarCierreVentana;
        }

        /*-------------------------------------------ACEPTAR-------------------------------------------------*/
        private void button1_Click(object sender, EventArgs e)
        {
            modificarEstadoControles(false);
            if (validarCamposVacios())
            {
                conexion.Open();

                SqlCommand verificarBono = new SqlCommand("CHAMBA.VerificarBono", conexion);

                verificarBono.CommandType = CommandType.StoredProcedure;
                verificarBono.Parameters.Add("@numero_bono", SqlDbType.Decimal).Value = numBono.Text.ToString();
                verificarBono.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = numAfiliado.Text.ToString();

                var resultado = verificarBono.Parameters.Add("@Resultado", SqlDbType.Int);
                resultado.Direction = ParameterDirection.Output;

                SqlDataReader data = verificarBono.ExecuteReader();
                data.Close();

                if (resultado.Value.ToString() == "0")
                {
                    MessageBox.Show("El bono ingresado no corresponde al afiliado en cuestión");
                }
                else
                {
                    this.iniciar_seleccion_del_profesional();
                }

                conexion.Close();

            }
            else
            {
                MessageBox.Show("Rellene todos los campos para poder continuar");
            }

        }

        private void iniciar_seleccion_del_profesional()
        {
            FormSeleccionProf seleccion_de_profesional = new FormSeleccionProf();
            seleccion_de_profesional.obtener_datos(numAfiliado.Text.ToString(), numBono.Text.ToString());
            seleccion_de_profesional.Show();
            this.Hide();
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

        /*-------------------------------------------CANCELAR-------------------------------------------------*/
        private void botonCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
