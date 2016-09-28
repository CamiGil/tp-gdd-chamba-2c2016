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

namespace ClinicaFrba
{
    public partial class FormLogin : Form
    {

        SqlConnection conexion;

        public FormLogin()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            Configuraciones.formLogin = this;
        }


        private void btnIniciar_Click(object sender, EventArgs e)
        {

            lblResultado.Text = "Iniciando sesion...";
            modificarEstadoControles(false);
            if (validarCamposVacios())
            {

                conexion.Open();

                SqlCommand verificarLogin = new SqlCommand("CHAMBA.VerificarLogin", conexion);

                verificarLogin.CommandType = CommandType.StoredProcedure;
                verificarLogin.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = txtUsuario.Text;
                verificarLogin.Parameters.Add("@Clave", SqlDbType.VarChar).Value = txtClave.Text;
                verificarLogin.Parameters.Add("@MaxIntentos", SqlDbType.Int).Value = Configuraciones.cantMaxIntentosLogin;

                var resultado = verificarLogin.Parameters.Add("@Resultado", SqlDbType.Int);
                var idUsuario = verificarLogin.Parameters.Add("@Id", SqlDbType.Int);
                resultado.Direction = ParameterDirection.Output;
                idUsuario.Direction = ParameterDirection.Output;

                SqlDataReader data = verificarLogin.ExecuteReader();
                data.Close();

                switch (Convert.ToInt32(resultado.Value))
                {
                    /* 0: El usuario no existe
                     * 1: Intentos excedidos
                     * 2: Clave incorrecta
                     * 3: No hay roles disponibles
                     * 4: Login exitoso
                    */
                    case 0: case 2:
                        lblResultado.Text = "Los datos ingresados son incorrectos";
                        break;
                    case 3:
                        lblResultado.Text = "No tiene roles asignados";
                        break;
                    case 1:
                        lblResultado.Text = "Intentos fallidos excedidos";
                        break;
                    case 4:
                        Configuraciones.usuario = Convert.ToInt32(idUsuario.Value);
                        lblResultado.Text = "";
                        FormSeleccionarRol form = new FormSeleccionarRol();
                        form.Show();
                        this.Hide();
                        break;
                }
                conexion.Close();
            }else{
               lblResultado.Text = "Complete los datos solicitados";
            }
            limpiarDatosDeControles();
            modificarEstadoControles(true);
        }

        private void modificarEstadoControles(Boolean estado)
        {
            txtUsuario.Enabled = estado;
            txtClave.Enabled = estado;
            btnIniciar.Enabled = estado;
            txtUsuario.Focus();
        }

        private void limpiarDatosDeControles()
        {
            txtUsuario.Text = "";
            txtClave.Text = "";
        }

        private void establecerIntentosLogin(int intentos)
        {

        }

        private Boolean validarCamposVacios()
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text) || !string.IsNullOrEmpty(txtClave.Text))
            {
                return true;
            }
            return false;
        }

    }
}
