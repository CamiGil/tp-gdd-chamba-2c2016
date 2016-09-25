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
            if (txtUsuario.Text != "" && txtClave.Text != "")
            {
                
                conexion.Open();
                String query = "SELECT Usua_Id, Usua_Clave, Usua_Intentos FROM CHAMBA.Usuarios where Usua_Usuario = '" + txtUsuario.Text + "'";

                SqlCommand listar = new SqlCommand(query, conexion);

                DataTable tabla = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = listar;
                adapter.Fill(tabla);

                if (tabla.Rows.Count == 1)
                {
                    int intentos = Convert.ToInt32(tabla.Rows[0]["Usua_Intentos"]);

                    if (intentos > Configuraciones.cantMaxIntentosLogin)
                    {
                        lblResultado.Text = "Intentos fallidos excedidos";
                    }
                    else
                    {
                        if (txtClave.Text == tabla.Rows[0]["Usua_Clave"].ToString())
                        {
                            Configuraciones.usuario = Convert.ToInt32(tabla.Rows[0]["Usua_Id"]);
                            lblResultado.Text = "";
                            establecerIntentosLogin(0);
                            FormSeleccionarRol form = new FormSeleccionarRol();
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            lblResultado.Text = "La clave es incorrecta";
                        }
                    }
                    establecerIntentosLogin(intentos++);
                }else{
                    lblResultado.Text = "El usuario no existe";
                }
                
                conexion.Close();
            }
            else
            {
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
    }
}
