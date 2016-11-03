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
        Validacion v = new Validacion();

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
                btnHistorialPlanes.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnEditar.Enabled = false;
                btnHistorialPlanes.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FormEditarAfiliado form = new FormEditarAfiliado();
            form.Tag = "Editar";
            form.cargarDatos(decimal.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                cargarAfiliados();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Está seguro que desea eliminar al afiliado", "Hospital", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                conexion.Open();
                SqlCommand cargar = new SqlCommand("CHAMBA.EliminarAfiliado", conexion);
                cargar.CommandType = CommandType.StoredProcedure;
                cargar.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                cargar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Configuraciones.fecha;
                cargar.ExecuteNonQuery();
                conexion.Close();                
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                MessageBox.Show("Afiliado eliminado exitosamente");
            }
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            FormEditarAfiliado form = new FormEditarAfiliado();
            form.Tag = "Agregar";
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                cargarAfiliados();
        }

        private void btnHistorialPlanes_Click(object sender, EventArgs e)
        {
            FormHistorialCambiosPlan form = new FormHistorialCambiosPlan();
            form.cargarDatos(decimal.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            form.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloLetras(e);
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloLetras(e);
        }

        private void txtAfiliado_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtAfiliado_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloNumeros(e);
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            v.soloNumeros(e);
        }

    }
}
