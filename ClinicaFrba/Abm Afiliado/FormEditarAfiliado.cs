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

namespace ClinicaFrba.Abm_Afiliado
{
    public partial class FormEditarAfiliado : Form
    {

        SqlConnection conexion;
        public FormEditarAfiliado()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);

            String query = "SELECT Plan_Codigo, Plan_Descripcion FROM CHAMBA.Planes";

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            cboPlan.DataSource = tabla;
            cboPlan.DisplayMember = "Plan_Descripcion";
            cboPlan.ValueMember = "Plan_Codigo";
        }

        private void FormEditarAfiliado_Load(object sender, EventArgs e)
        {

        }

        public void cargarDatos(int numeroAfiliado)
        {
            String query = "SELECT * FROM CHAMBA.Pacientes JOIN CHAMBA.Usuarios ON Paci_Usuario = Usua_Id JOIN CHAMBA.Planes ON Plan_Codigo = Paci_Plan WHERE Paci_Numero = " + numeroAfiliado;

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            cboTipoDocumento.Enabled = false;
            txtDocumento.Enabled = false;
            dtpNacimiento.Enabled = false;

            txtNombre.Text = tabla.Rows[0]["Usua_Nombre"].ToString();
            txtApellido.Text = tabla.Rows[0]["Usua_Apellido"].ToString();
            cboTipoDocumento.SelectedIndex = int.Parse(tabla.Rows[0]["Usua_TipoDNI"].ToString());
            txtDocumento.Text = tabla.Rows[0]["Usua_DNI"].ToString();
            txtDomicilio.Text = tabla.Rows[0]["Usua_Direccion"].ToString();
            txtTelefono.Text = tabla.Rows[0]["Usua_Telefono"].ToString();
            txtEmail.Text = tabla.Rows[0]["Usua_Mail"].ToString();
            dtpNacimiento.Text = tabla.Rows[0]["Usua_Fecha_Nac"].ToString();
            cboSexo.Text = tabla.Rows[0]["Usua_Sexo"].ToString();
            cboEstadoCivil.SelectedIndex = int.Parse(tabla.Rows[0]["Paci_Estado_Civil"].ToString());
            nudHijos.Value = int.Parse(tabla.Rows[0]["Paci_Cant_Hijos"].ToString());
            cboPlan.Text = tabla.Rows[0]["Plan_Descripcion"].ToString();

            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GUADAR DATOS");
        }
    }
}
