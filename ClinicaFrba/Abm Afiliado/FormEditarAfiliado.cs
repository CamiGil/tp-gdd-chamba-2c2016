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
        String afiliado;
        List<FormEditarAfiliado> afiliadosAsociados = new List<FormEditarAfiliado>();
        int proximoIdFamiliar = 2;
        FormCambioPlan planCambiado = null;

        public FormEditarAfiliado()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            conexion.Open();

            String query = "SELECT Plan_Codigo, Plan_Descripcion FROM CHAMBA.Planes";

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            conexion.Close();

            cboPlan.DataSource = tabla;
            cboPlan.DisplayMember = "Plan_Descripcion";
            cboPlan.ValueMember = "Plan_Codigo";

            dtpNacimiento.Text = Configuraciones.fecha;
        }

        private void FormEditarAfiliado_Load(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == "Conyuge" || this.Tag.ToString() == "Hijo")
            {
                this.btnConyuge.Visible = false;
                this.btnHijo.Visible = false;
            }

            if (this.Tag.ToString() == "Editar")
            {
                cboPlan.Enabled = false;
                btnCambiarPlan.Enabled = true;
            }
        }

        public void cargarDatos(String numeroAfiliado)
        {
            afiliado = numeroAfiliado;

            conexion.Open();

            SqlCommand proximoId = new SqlCommand("CHAMBA.ObtenerProximoIdFamiliar", conexion);
            proximoId.CommandType = CommandType.StoredProcedure;

            proximoId.Parameters.Add("@Afiliado", SqlDbType.VarChar).Value = obtenerNumeroAfiliadoSinIdFamilia(afiliado);
            var nuevoIdFamiliar = proximoId.Parameters.Add("@id", SqlDbType.Int);
            nuevoIdFamiliar.Direction = ParameterDirection.Output;
            SqlDataReader data = proximoId.ExecuteReader();
            data.Close();
            proximoIdFamiliar = int.Parse(nuevoIdFamiliar.Value.ToString());

            if (proximoIdFamiliar > 2)
            {
                this.btnConyuge.Visible = false;
            }

            if (obtenerIdFamilia(afiliado) != "01" && obtenerIdFamilia(afiliado) != "02")
            {
                this.btnHijo.Visible = false;
            }

            String query = "SELECT * FROM CHAMBA.Pacientes JOIN CHAMBA.Usuarios ON Paci_Usuario = Usua_Id JOIN CHAMBA.Planes ON Plan_Codigo = Paci_Plan WHERE Paci_Numero = '" + numeroAfiliado + "'";

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

            conexion.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (camposCompletos())
            {
                this.DialogResult = DialogResult.OK;
                if (this.Tag.ToString() != "Hijo" && this.Tag.ToString() != "Conyuge")
                {
                    guardarDatos();
                    MessageBox.Show("Datos guardados exitosamente");
                    this.Close();
                }
                
            }
        }

        public SqlCommand generarComandoSQL()
        {
            SqlCommand guardar;
            guardar = new SqlCommand();
            guardar.CommandType = CommandType.StoredProcedure;

            guardar.CommandText = "CHAMBA.ModificarAfiliado";

            guardar.Parameters.Add("@Afiliado", SqlDbType.VarChar).Value = afiliado;
            guardar.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txtNombre.Text;
            guardar.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = txtApellido.Text;
            guardar.Parameters.Add("@TipoDocumento", SqlDbType.Int).Value = cboTipoDocumento.SelectedIndex;
            guardar.Parameters.Add("@Documento", SqlDbType.Int).Value = txtDocumento.Text;
            guardar.Parameters.Add("@Domicilio", SqlDbType.VarChar).Value = txtDomicilio.Text;
            guardar.Parameters.Add("@Telefono", SqlDbType.Int).Value = txtTelefono.Text;
            guardar.Parameters.Add("@Email", SqlDbType.VarChar).Value = txtEmail.Text;
            guardar.Parameters.Add("@FechaNac", SqlDbType.DateTime).Value = dtpNacimiento.Text;
            guardar.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = cboSexo.Text;
            guardar.Parameters.Add("@EstadoCivil", SqlDbType.Int).Value = cboEstadoCivil.SelectedIndex;
            guardar.Parameters.Add("@CantHijos", SqlDbType.Int).Value = nudHijos.Value;
            guardar.Parameters.Add("@Plan", SqlDbType.Int).Value = cboPlan.SelectedValue;

            return guardar;
        }

        private void guardarDatos()
        {
            conexion.Open();


            if (this.Tag.ToString() == "Agregar")
            {
                SqlCommand nuevoIdPaciente = new SqlCommand("CHAMBA.ObtenerNuevoIdPaciente", conexion);
                nuevoIdPaciente.CommandType = CommandType.StoredProcedure;

                var nuevoId = nuevoIdPaciente.Parameters.Add("@id", SqlDbType.VarChar, 12);
                nuevoId.Direction = ParameterDirection.Output;
                SqlDataReader dataId = nuevoIdPaciente.ExecuteReader();
                dataId.Close();
                afiliado = nuevoId.Value.ToString();
            }

            SqlTransaction transaccion;

            transaccion = conexion.BeginTransaction("Transaccion");

            SqlCommand comando = generarComandoSQL();
            comando.Connection = conexion;
            comando.Transaction = transaccion;

            if (proximoIdFamiliar == 2) proximoIdFamiliar++;

            foreach (FormEditarAfiliado formAfiliado in afiliadosAsociados)
            {
                formAfiliado.afiliado = obtenerNumeroAfiliadoSinIdFamilia(afiliado);
                if (formAfiliado.Tag.ToString() == "Conyuge")
                {
                    formAfiliado.afiliado += "02";
                }
                else
                {
                    formAfiliado.afiliado += proximoIdFamiliar.ToString("0#");
                    proximoIdFamiliar++;
                }

                SqlCommand comandoFamiliares = formAfiliado.generarComandoSQL();
                comandoFamiliares.Connection = conexion;
                comandoFamiliares.Transaction = transaccion;
                comandoFamiliares.ExecuteNonQuery();

            }

            if (planCambiado != null)
            {
                SqlCommand comandoCambioPlan = planCambiado.generarComandoSQL();
                comandoCambioPlan.Connection = conexion;
                comandoCambioPlan.Transaction = transaccion;
                comandoCambioPlan.ExecuteNonQuery();
            }

            comando.ExecuteNonQuery();

            transaccion.Commit();
            conexion.Close();
        }

        private String obtenerNumeroAfiliadoSinIdFamilia(String cadena)
        {
            if (cadena.Length > 2)
                return cadena.Substring(0, cadena.Length - 2);
            return "";
        }

        private String obtenerIdFamilia(String cadena)
        {
            if (cadena.Length > 2)
                return cadena.Substring(cadena.Length - 2, 2);
            return "";
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
            else if (!esNumerico(txtDocumento.Text))
            {
                MessageBox.Show("El documento debe contener solo numeros");
            }
            else if (txtDomicilio.Text == "")
            {
                MessageBox.Show("Complete el domicilio");
            }
            else if (txtTelefono.Text == "")
            {
                MessageBox.Show("Complete el telefono");
            }
            else if (!esNumerico(txtTelefono.Text))
            {
                MessageBox.Show("El telefono debe contener solo numeros");
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

        private bool esNumerico(String cadena)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(cadena, @"^\d+$");
        }

        private void btnConyuge_Click(object sender, EventArgs e)
        {
            FormEditarAfiliado form= new FormEditarAfiliado();
            form.Tag = "Conyuge";
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                afiliadosAsociados.Add(form);
                btnConyuge.Enabled = false;
            }

        }

        private void btnHijo_Click(object sender, EventArgs e)
        {
            FormEditarAfiliado form = new FormEditarAfiliado();
            form.Tag = "Hijo";
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                afiliadosAsociados.Add(form);
            }
        }

        private void btnCambiarPlan_Click(object sender, EventArgs e)
        {
            FormCambioPlan form = new FormCambioPlan();
            form.afiliado = afiliado;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                planCambiado = form;
                cboPlan.Text = planCambiado.nuevoPlan;
            }
        }
    }
}
