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
    public partial class FormCambioPlan : Form
    {

        SqlConnection conexion;
        public decimal afiliado;
        public String nuevoPlan;

        public FormCambioPlan()
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
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRazon.Text != "")
            {
                nuevoPlan = cboPlan.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Complete la razon");
            }
        }

        public SqlCommand generarComandoSQL()
        {
            SqlCommand guardar;
            guardar = new SqlCommand();
            guardar.CommandType = CommandType.StoredProcedure;

            guardar.CommandText = "CHAMBA.ModificarPlan";

            guardar.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = afiliado;
            guardar.Parameters.Add("@NuevoPlan", SqlDbType.Decimal).Value = cboPlan.SelectedValue;            
            guardar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Configuraciones.fecha;
            guardar.Parameters.Add("@Razon", SqlDbType.VarChar).Value = txtRazon.Text;

            return guardar;
        }
    }
}
