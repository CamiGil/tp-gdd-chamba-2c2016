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
    public partial class FormAfiliados : Form
    {
        SqlConnection conexion;
        public FormAfiliados()
        {
            InitializeComponent();
            conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormAfiliados_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarAfiliados();
        }

        private void cargarAfiliados()
        {
            conexion.Open();
            SqlCommand cargar = new SqlCommand("CHAMBA.CargarAfiliados", conexion);
            cargar.CommandType = CommandType.StoredProcedure;
            cargar.Parameters.Add("@Afiliado", SqlDbType.VarChar).Value = txtAfiliado.Text;
            cargar.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txtNombre.Text;
            cargar.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = txtApellido.Text;
            cargar.Parameters.Add("@Documento", SqlDbType.VarChar).Value = txtDni.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            conexion.Close();

            habilitarBotones();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtAfiliado.Text = "";
            txtDni.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            habilitarBotones();
        }

        private void habilitarBotones()
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FormEditarAfiliado form = new FormEditarAfiliado();
            form.cargarDatos(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            form.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Eliminar afiliado " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            FormEditarAfiliado form = new FormEditarAfiliado();
            form.ShowDialog();
        }
    }
}
