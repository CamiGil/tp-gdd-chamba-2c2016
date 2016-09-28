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
        int afiliado = 0;
        DataGridViewSelectedRowCollection seleccionado;

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

            dtpNacimiento.Text = Configuraciones.fecha;
        }

        private void FormEditarAfiliado_Load(object sender, EventArgs e)
        {

        }

        public void cargarDatos(int numeroAfiliado)
        {
            afiliado = numeroAfiliado;
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
            if (camposCompletos())
            {
                if (afiliado == 0)
                {
                    MessageBox.Show("Alta sin funcionamiento");
                    return;
                }

                conexion.Open();
                SqlCommand guardar;
                guardar = new SqlCommand("CHAMBA.ModificarAfiliado", conexion);  
                guardar.CommandType = CommandType.StoredProcedure;
                guardar.Parameters.Add("@Afiliado", SqlDbType.Int).Value = afiliado;
                guardar.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txtNombre.Text;
                guardar.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = txtApellido.Text;
                guardar.Parameters.Add("@TipoDocumento", SqlDbType.Int).Value = cboTipoDocumento.SelectedIndex;
                guardar.Parameters.Add("@Documento", SqlDbType.Int).Value = txtDocumento.Text;
                guardar.Parameters.Add("@Domicilio", SqlDbType.VarChar).Value = txtDomicilio.Text;
                guardar.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = txtTelefono.Text;
                guardar.Parameters.Add("@Email", SqlDbType.VarChar).Value = txtEmail.Text;
                guardar.Parameters.Add("@FechaNac", SqlDbType.DateTime).Value = dtpNacimiento.Text;
                guardar.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = cboSexo.Text;
                guardar.Parameters.Add("@EstadoCivil", SqlDbType.Int).Value = cboEstadoCivil.SelectedIndex;
                guardar.Parameters.Add("@CantHijos", SqlDbType.Int).Value = nudHijos.Value;
                guardar.Parameters.Add("@Plan", SqlDbType.Int).Value = cboPlan.SelectedValue;
                guardar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Configuraciones.fecha;
                guardar.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Datos guardados exitosamente");
                this.Close();
            }
        }

        private bool camposCompletos()
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Complete el nombre");
            }
            else if(txtApellido.Text == "")
            {
                MessageBox.Show("Complete el apellido");
            }
            else if (cboTipoDocumento.Text == "")
            {
                MessageBox.Show("Complete el tipo de documento");
            }
            else if (txtDocumento.Text == "")
            {
                MessageBox.Show("Complete el documento");
            }
            else if (txtDomicilio.Text == "")
            {
                MessageBox.Show("Complete el domicilio");
            }
            else if (txtTelefono.Text == "")
            {
                MessageBox.Show("Complete el telefono");
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Complete el email");
            }
            else if (cboSexo.Text == "")
            {
                MessageBox.Show("Complete el sexo");
            }
            else if (cboEstadoCivil.Text == "")
            {
                MessageBox.Show("Complete el estado civil");
            }
            else if (cboPlan.Text == "")
            {
                MessageBox.Show("Complete el plan medico");
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}
