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
    public partial class FormSeleccionarRol : Form
    {
        SqlConnection conexion;

        public FormSeleccionarRol()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormSeleccionarRol_Load(object sender, EventArgs e)
        {
            conexion.Open();
            String query = "SELECT Rol_Id, Rol_Nombre FROM CHAMBA.Roles JOIN CHAMBA.Rol_X_Usuario ON Rol_Id = Rol_X_Usua_Rol WHERE Rol_Estado = 1 AND Rol_X_Usua_Usuario = '" + Configuraciones.usuario + "'";

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            cboRol.DataSource = tabla;
            cboRol.DisplayMember = "Rol_Nombre";
            cboRol.ValueMember = "Rol_Id";

            conexion.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Configuraciones.rol = Convert.ToInt32(cboRol.SelectedValue);
            FormMain form = new FormMain();
            form.Show();
            this.Close();
        }
    }
}
