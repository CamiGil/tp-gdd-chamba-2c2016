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
        decimal afiliado;
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

            dtpNacimiento.Value = Configuraciones.fecha;
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

        public void cargarDatos(decimal numeroAfiliado)
        {
            afiliado = numeroAfiliado;

            conexion.Open();

            SqlCommand tieneConyuge = new SqlCommand("CHAMBA.TieneConyuge", conexion);
            tieneConyuge.CommandType = CommandType.StoredProcedure;

            tieneConyuge.Parameters.Add("@Afiliado", SqlDbType.VarChar).Value = obtenerNumeroAfiliadoSinIdFamilia(afiliado);
            var existeConyuge = tieneConyuge.Parameters.Add("@Existe", SqlDbType.Decimal);
            existeConyuge.Direction = ParameterDirection.Output;
            SqlDataReader data = tieneConyuge.ExecuteReader();
            data.Close();
            int deshabilitarConyuge = int.Parse(existeConyuge.Value.ToString());

            SqlCommand proximoId = new SqlCommand("CHAMBA.ObtenerProximoIdFamiliar", conexion);
            proximoId.CommandType = CommandType.StoredProcedure;

            proximoId.Parameters.Add("@Afiliado", SqlDbType.VarChar).Value = obtenerNumeroAfiliadoSinIdFamilia(afiliado);
            var nuevoIdFamiliar = proximoId.Parameters.Add("@id", SqlDbType.Decimal);
            nuevoIdFamiliar.Direction = ParameterDirection.Output;
            data = proximoId.ExecuteReader();
            data.Close();
            proximoIdFamiliar = int.Parse(nuevoIdFamiliar.Value.ToString());

            if (deshabilitarConyuge==1)
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

                if (existeEmail())
                {
                    MessageBox.Show("El email ya se encuentra en uso por otro usuario");
                }
                else
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
        }

        private bool existeEmail()
        {
            conexion.Open();
            String query = "SELECT Usua_Id FROM CHAMBA.Usuarios JOIN CHAMBA.Pacientes ON Paci_Usuario = Usua_Id WHERE Usua_Mail = '" + txtEmail.Text + "' AND Paci_Numero <> '"+ afiliado +"'";

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);
            conexion.Close();
            if (tabla.Rows.Count > 0)
            {
                return true;
            }

            return false;

        }

        public SqlCommand generarComandoSQL()
        {
            SqlCommand guardar;
            guardar = new SqlCommand();
            guardar.CommandType = CommandType.StoredProcedure;

            guardar.CommandText = "CHAMBA.ModificarAfiliado";

            guardar.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = afiliado;
            guardar.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txtNombre.Text;
            guardar.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = txtApellido.Text;
            guardar.Parameters.Add("@TipoDocumento", SqlDbType.Int).Value = cboTipoDocumento.SelectedIndex;
            guardar.Parameters.Add("@Documento", SqlDbType.Decimal).Value = txtDocumento.Text;
            guardar.Parameters.Add("@Domicilio", SqlDbType.VarChar).Value = txtDomicilio.Text;
            guardar.Parameters.Add("@Telefono", SqlDbType.Decimal).Value = txtTelefono.Text;
            guardar.Parameters.Add("@Email", SqlDbType.VarChar).Value = txtEmail.Text;
            guardar.Parameters.Add("@FechaNac", SqlDbType.DateTime).Value = dtpNacimiento.Text;
            guardar.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = cboSexo.Text;
            guardar.Parameters.Add("@EstadoCivil", SqlDbType.Int).Value = cboEstadoCivil.SelectedIndex;
            guardar.Parameters.Add("@CantHijos", SqlDbType.Int).Value = nudHijos.Value;
            guardar.Parameters.Add("@Plan", SqlDbType.Decimal).Value = cboPlan.SelectedValue;

            return guardar;
        }

        private void guardarDatos()
        {
            conexion.Open();


            if (this.Tag.ToString() == "Agregar")
            {
                SqlCommand nuevoIdPaciente = new SqlCommand("CHAMBA.ObtenerNuevoIdPaciente", conexion);
                nuevoIdPaciente.CommandType = CommandType.StoredProcedure;

                var nuevoId = nuevoIdPaciente.Parameters.Add("@id", SqlDbType.Decimal);
                nuevoId.Direction = ParameterDirection.Output;
                SqlDataReader dataId = nuevoIdPaciente.ExecuteReader();
                dataId.Close();
                afiliado = decimal.Parse(nuevoId.Value.ToString());
            }

            SqlTransaction transaccion;

            transaccion = conexion.BeginTransaction("Transaccion");

            SqlCommand comando = generarComandoSQL();
            comando.Connection = conexion;
            comando.Transaction = transaccion;

            foreach (FormEditarAfiliado formAfiliado in afiliadosAsociados)
            {
                formAfiliado.afiliado = obtenerNumeroAfiliadoSinIdFamilia(afiliado);
                if (formAfiliado.Tag.ToString() == "Conyuge")
                {
                    formAfiliado.afiliado = decimal.Parse(formAfiliado.afiliado.ToString() + "02");
                }
                else
                {
                    formAfiliado.afiliado = decimal.Parse(formAfiliado.afiliado.ToString() +  proximoIdFamiliar.ToString("0#"));
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

        private decimal obtenerNumeroAfiliadoSinIdFamilia(decimal id)
        {
            if (id.ToString().Length > 2)
                return decimal.Parse(id.ToString().Substring(0, id.ToString().Length - 2));
            return 0;
        }

        private String obtenerIdFamilia(decimal id)
        {
            if (id.ToString().Length > 2)
                return id.ToString().Substring(id.ToString().Length - 2, 2);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
