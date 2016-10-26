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
        private string afiliado;
        private string bono;
        private SqlConnection conexion;

        /*-------------------------------------------CONSTRUCTOR---------------------------------------------*/
        public FormSeleccionProf()
        {
            InitializeComponent();
            this.conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.FormClosed += Configuraciones.validarCierreVentana;
            rellenar_scroll_especialidades();
        }

        /*-------------------------------------------ACEPTAR-------------------------------------------------*/
        private void button1_Click(object sender, EventArgs e)
        {
            modificar_estado_labels(false);
            if (validar_labels_vacios())
            {
                MessageBox.Show("Rellene todos los campos");
            }
            else
            {
                this.iniciar_confirmacion_de_turno();
            }
        }

        private void iniciar_confirmacion_de_turno()
        {
            FormConfirmarTurno confirmacion = new FormConfirmarTurno();

            DataGridViewRow row= Info.SelectedRows[0];
            string especialidad = row.Cells[3].ToString(); // <-- la tercera columna es la correspondiente a la especialidad

            if(Especialidades_profesional.SelectedItem == null)
            {
                string tipo_especialidad = row.Cells[2].ToString(); // <-- la segunda columna es la correspondiente al tipo de especialidad
                confirmacion.obtener_datos(this.afiliado,this.bono,Nombre_profesional.Text.ToString(), Apellido_profesional.Text.ToString(), tipo_especialidad, especialidad);
            }
            else
            {
                confirmacion.obtener_datos(this.afiliado,this.bono,Nombre_profesional.Text.ToString(),Apellido_profesional.Text.ToString(),Especialidades_profesional.SelectedItem.ToString(),especialidad);
            }
            confirmacion.Show();
            this.Hide();
        }

        private void modificar_estado_labels(Boolean estado)
        {
            Nombre_profesional.Enabled = estado;
            Apellido_profesional.Enabled = estado;
        }

        private bool validar_labels_vacios()
        {

            if (!string.IsNullOrEmpty(Nombre_profesional.Text) || !string.IsNullOrEmpty(Apellido_profesional.Text) || Info.SelectedRows == null)
            {
                return true;
            }
            return false;

        }

        /*-------------------------------------------BUSCAR--------------------------------------------------*/
        private void Buscar_Click(object sender, EventArgs e)
        {
            this.modificarEstadoControles(false);
            if (validarCamposVacios())
            {
                if (Especialidades_profesional.SelectedItem == null)
                {
                    this.buscar_profesional_sin_refinamiento();
                }
                else
                {
                    this.buscar_profesional_refinado_por_tipo();
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos para poder continuar");
            }
        }

        private void buscar_profesional_refinado_por_tipo()
        {
            conexion.Open();

            SqlCommand busqueda_profesional_especialidades = new SqlCommand("CHAMBA.BUSQUEDA_PROFESIONAL_REFINADO_POR_TIPO", conexion);

            busqueda_profesional_especialidades.CommandType = CommandType.StoredProcedure;
            busqueda_profesional_especialidades.Parameters.Add("@Nombre_profesional", SqlDbType.VarChar).Value = Nombre_profesional.Text.ToString();
            busqueda_profesional_especialidades.Parameters.Add("@Apellido_profesional", SqlDbType.VarChar).Value = Apellido_profesional.Text.ToString();
            busqueda_profesional_especialidades.Parameters.Add("@Tipo_Especialidad", SqlDbType.VarChar).Value = Especialidades_profesional.SelectedItem.ToString();

            busqueda_profesional_especialidades.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(busqueda_profesional_especialidades);

            rellenar_tabla_informacion(adapter);

            conexion.Close();
        }

        private void buscar_profesional_sin_refinamiento()
        {
            conexion.Open();

            SqlCommand busqueda_profesional_especialidades = new SqlCommand("CHAMBA.BUSQUEDA_PROFESIONAL_ESPECIALIDADES", conexion);

            busqueda_profesional_especialidades.CommandType = CommandType.StoredProcedure;
            busqueda_profesional_especialidades.Parameters.Add("@Nombre_profesional", SqlDbType.VarChar).Value = Nombre_profesional.Text.ToString();
            busqueda_profesional_especialidades.Parameters.Add("@Apellido_profesional", SqlDbType.VarChar).Value = Apellido_profesional.Text.ToString();

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

        private Boolean validarCamposVacios()
        {
            if (!string.IsNullOrEmpty(Nombre_profesional.Text) || !string.IsNullOrEmpty(Apellido_profesional.Text))
            {
                return true;
            }
            return false;
        }

        private void modificarEstadoControles(Boolean estado)
        {
            Nombre_profesional.Enabled = estado;
            Apellido_profesional.Enabled = estado;
        }

        /*-------------------------------------------SCROLL ESPECIALIDADES------------------------------------*/
        private void rellenar_scroll_especialidades()
        {
            conexion.Open();

            String query = "Select DISTINCT(e.Tipo_Espe_Descripcion) from CHAMBA.Tipo_Especialidad e";
            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);

            int indeeex = 0;
            while (indeeex < tabla.Rows.Count)
            {
                Especialidades_profesional.Items.Add(tabla.Rows[indeeex]["tipo"].ToString());
                indeeex++;
            }

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
        public void obtener_datos(string numero_afiliado, string numero_bono)
        {
            this.afiliado = numero_afiliado;
            this.bono = numero_bono;
        }

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

        }

       
    }
}
