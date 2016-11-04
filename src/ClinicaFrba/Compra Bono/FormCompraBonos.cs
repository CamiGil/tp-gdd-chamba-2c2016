using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaFrba.Compra_Bono
{
    public partial class FormCompraBonos : Form
    {

        SqlConnection conexion;
        Validacion v = new Validacion();
        public FormCompraBonos()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private bool esNumerico(String cadena)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(cadena, @"^\d+$");
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if(txtAfiliado.Text != "" && esNumerico(txtAfiliado.Text))
            {
                conexion.Open();
                String query = "SELECT COUNT(*) FROM CHAMBA.Pacientes WHERE Paci_Numero = " + txtAfiliado.Text + " AND Paci_Fecha_Baja IS NULL";

                SqlCommand listar = new SqlCommand(query, conexion);

                DataTable tabla = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = listar;
                adapter.Fill(tabla);
                conexion.Close();

                if (int.Parse(tabla.Rows[0][0].ToString()) != 0){
                    FormConfirmar form = new FormConfirmar();
                    form.obtenerDatos(decimal.Parse(txtAfiliado.Text), nudCantidad.Value);
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("El afiliado no existe o no se encuentra activo");
                }
            }
            else
            {
                MessageBox.Show("Ingrese el numero de afiliado");
            }
        }

        private void FormCompraBonos_Load(object sender, EventArgs e)
        {

            conexion.Open();

            SqlCommand verificarPrivilegios = new SqlCommand("CHAMBA.EsAfiliado", conexion);

            verificarPrivilegios.CommandType = CommandType.StoredProcedure;
            verificarPrivilegios.Parameters.Add("@Usuario", SqlDbType.Decimal).Value = Configuraciones.usuario;
            verificarPrivilegios.Parameters.Add("@Rol", SqlDbType.Decimal).Value = Configuraciones.rol;

            var resultado = verificarPrivilegios.Parameters.Add("@Resultado", SqlDbType.Int);
            resultado.Direction = ParameterDirection.Output;

            SqlDataReader data = verificarPrivilegios.ExecuteReader();
            data.Close();

            if (int.Parse(resultado.Value.ToString()) == 1) // Es afiliado
            {
                txtAfiliado.Enabled = false;
                txtAfiliado.Text = obtenerNumeroAfiliado();
            }

            conexion.Close();
        }

        private String obtenerNumeroAfiliado()
        {
            String query = "SELECT Paci_Numero FROM CHAMBA.Pacientes WHERE Paci_Usuario = " + Configuraciones.usuario;

            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            return tabla.Rows[0]["Paci_Numero"].ToString();
        }

        private void txtAfiliado_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloNumeros(e);
        }
    }
}
