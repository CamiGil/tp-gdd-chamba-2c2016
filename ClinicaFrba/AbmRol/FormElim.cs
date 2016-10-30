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

namespace ClinicaFrba.AbmRol
{
    public partial class FormElim : Form
    {
        SqlConnection conexion;
        SqlCommand cargarRoles, idRol, inhabilitar, inhabilitarPorUsuario;
        SqlDataReader data;
      
        public FormElim()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
            conexion.Open();

            cargarRoles = new SqlCommand("CHAMBA.CargarRolesHabilitados", conexion);
            cargarRoles.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cargarRoles);
            DataTable table = new DataTable();
            conexion.Close();
            adapter.Fill(table);
            comboBox2.DataSource = table;
            comboBox2.DisplayMember = "Rol_Nombre";
        }


        private void FormElim_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
   
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar el rol seleccionado?", "Eliminar Rol", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                string nombre = comboBox2.Text.ToString();
                conexion.Open();
                idRol = new SqlCommand("CHAMBA.ObtenerRolId", conexion);
                idRol.CommandType = CommandType.StoredProcedure;
                idRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                var resultado = idRol.Parameters.Add("@Valor", SqlDbType.Decimal);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = idRol.ExecuteReader();

                var id = resultado.Value;
                decimal rol = decimal.Parse(id.ToString());
                data.Close();
                //inhabilitar rol

                inhabilitar = new SqlCommand("CHAMBA.InhabilitarRol", conexion);
                inhabilitar.CommandType = CommandType.StoredProcedure;
                inhabilitar.Parameters.Add("@id", SqlDbType.Decimal).Value = rol;
                inhabilitar.ExecuteNonQuery();

                inhabilitarPorUsuario = new SqlCommand("CHAMBA.InhabilitarRolPorUsuario", conexion);
                inhabilitarPorUsuario.CommandType = CommandType.StoredProcedure;
                inhabilitarPorUsuario.Parameters.Add("@id", SqlDbType.Decimal).Value = rol;
                inhabilitarPorUsuario.ExecuteNonQuery();
                conexion.Close();

                String mensaje = "El rol se ha eliminado exitosamente";
                String caption = "Rol eliminado";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

                this.Close();
            }
        }
    }
}
