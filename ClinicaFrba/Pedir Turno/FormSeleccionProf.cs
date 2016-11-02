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

namespace ClinicaFrba.Pedir_Turno
{
    public partial class FormSeleccionProf : Form
    {
        /*-------------------------------------------ATRIBUTOS-----------------------------------------------*/
        private SqlConnection conexion;
        private string tipo_busqueda;

        /*-------------------------------------------CONSTRUCTOR---------------------------------------------*/
        public FormSeleccionProf()
        {
            InitializeComponent();
            this.conexion = new SqlConnection(@Configuraciones.datosConexion);
            rellenar_scroll_especialidades();
        }

        /*-------------------------------------------ACEPTAR-------------------------------------------------*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (Info.SelectedRows.Count != 1)
            {
                MessageBox.Show("Seleccione la fila que contiene los datos requeridos");
            }
            else
            {
                this.iniciar_confirmacion_de_turno();
            }
        }

        private void iniciar_confirmacion_de_turno()
        {
            FormConfirmarTurno confirmacion = new FormConfirmarTurno();

            DataGridViewRow row = Info.SelectedRows[0];
            decimal id_profesional = decimal.Parse(row.Cells[0].Value.ToString()); // <-- la primera columna es la correspondiente al ID_Prof
            decimal especialidad = decimal.Parse(Especialidades_profesional.SelectedValue.ToString()); // <-- la cuarta columna es la correspondiente al ID_Espe


            string nombre_profesional = row.Cells[1].Value.ToString();
            string apellido_profesional = row.Cells[2].Value.ToString();
            string nombre_y_apellido = nombre_profesional + ' ' + apellido_profesional;
            confirmacion.obtener_datos(id_profesional, especialidad, nombre_y_apellido, Especialidades_profesional.Text);

           
            confirmacion.ShowDialog();
            if (confirmacion.DialogResult == DialogResult.OK)
            {
                this.Close();
            }
        }

        /*-------------------------------------------BUSCAR--------------------------------------------------*/
        private void Buscar_Click(object sender, EventArgs e)
        {

            if (Especialidades_profesional.SelectedItem != null)
            {
                this.buscar_profesionales();

            }
            else
            {
                MessageBox.Show("Complete la especialidad");
            }

        }

        private void buscar_profesionales()
        {

            conexion.Open();

            SqlCommand busqueda_profesional_especialidades = new SqlCommand("CHAMBA.PROFESIONALES_POR_ESPECIALIDAD", conexion);

            busqueda_profesional_especialidades.CommandType = CommandType.StoredProcedure;
            busqueda_profesional_especialidades.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = decimal.Parse(Especialidades_profesional.SelectedValue.ToString());
            busqueda_profesional_especialidades.Parameters.Add("@Nombre_profesional", SqlDbType.VarChar).Value = Nombre_profesional.Text;
            busqueda_profesional_especialidades.Parameters.Add("@Apellido_profesional", SqlDbType.VarChar).Value = Apellido_profesional.Text;


            SqlDataAdapter adapter = new SqlDataAdapter(busqueda_profesional_especialidades);
            rellenar_tabla_informacion(adapter);

            conexion.Close();
        }

        private void rellenar_tabla_informacion(SqlDataAdapter adapter)
        {
            DataTable table = new DataTable();
            adapter.Fill(table);
            Info.DataSource = table;
        }

        /*-------------------------------------------SCROLL ESPECIALIDADES------------------------------------*/
        private void rellenar_scroll_especialidades()
        {
            conexion.Open();

            String query = "SELECT Espe_Descripcion, Espe_Codigo FROM CHAMBA.Especialidades ORDER BY Espe_Descripcion";
            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            Especialidades_profesional.DataSource = tabla;
            Especialidades_profesional.ValueMember = "Espe_Codigo";
            Especialidades_profesional.DisplayMember = "Espe_Descripcion";

            conexion.Close();

        }

        /*-------------------------------------------LIMPIAR--------------------------------------------------*/
        private void Limpiar_Click(object sender, EventArgs e)
        {
            limpiar_campos_de_datos();
            limpiar_tabla_de_informacion();
        }

        private void limpiar_tabla_de_informacion()
        {
            Info.DataSource = null;
        }

        private void limpiar_campos_de_datos()
        {
            Nombre_profesional.Clear();
            Apellido_profesional.Clear();
        }
        /*-------------------------------------------NO RELEVANTES--------------------------------------------*/
        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /*-------------------------------------------CANCELAR---------------------------------------------*/
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSeleccionProf_Load(object sender, EventArgs e)
        {

        }
    }
}
