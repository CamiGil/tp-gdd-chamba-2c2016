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
            if (Info.SelectedRows == null)
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
            string id_profesional = row.Cells[0].Value.ToString(); // <-- la primera columna es la correspondiente al ID_Prof
            string especialidad = row.Cells[3].Value.ToString(); // <-- la cuarta columna es la correspondiente al ID_Espe

            if (tipo_busqueda == "solo_especialidad")
            {
                string nombre_profesional = row.Cells[1].Value.ToString();
                string apellido_profesional = row.Cells[2].Value.ToString();
                string nombre_y_apellido = nombre_profesional + ' ' + apellido_profesional;
                confirmacion.obtener_datos(id_profesional, especialidad,nombre_y_apellido,Especialidades_profesional.SelectedItem.ToString());
                
            }
            else
            {
                string nombre_y_apellido = Nombre_profesional.Text.ToString() + ' '+ Apellido_profesional.Text.ToString();
                if (tipo_busqueda == "con_datos_y_especialidad")
                {
                    confirmacion.obtener_datos(id_profesional, especialidad, nombre_y_apellido, Especialidades_profesional.SelectedItem.ToString());
                }
                else
                {
                    string especialidad_descipcion = row.Cells[4].Value.ToString();
                    confirmacion.obtener_datos(id_profesional, especialidad, nombre_y_apellido, especialidad_descipcion);
                }
                
            }

            confirmacion.ShowDialog();
            if (confirmacion.DialogResult == DialogResult.OK)
            {
                this.Close();
            }            
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

           if (Especialidades_profesional.SelectedItem == null)
           {
               if (validarCamposVacios())
               {
                   this.buscar_profesional_sin_refinamiento();
               }
               else { MessageBox.Show("Rellene los campos de nombre y apellido"); }
           }
           else
           {
               if (validarCamposVacios())
               {
                   this.buscar_profesional_refinado_por_tipo();
               }
               else
               {
                   this.buscar_profesionales_solo_por_especialidad();
               }
           }
          }

        private void buscar_profesionales_solo_por_especialidad()
        {
            this.tipo_busqueda = "solo_especialidad";

            conexion.Open();

            SqlCommand busqueda_profesional_especialidades = new SqlCommand("CHAMBA.PROFESIONALES_POR_ESPECIALIDAD", conexion);

            busqueda_profesional_especialidades.CommandType = CommandType.StoredProcedure;
            busqueda_profesional_especialidades.Parameters.Add("@Especialidad_descripcion", SqlDbType.VarChar).Value = Especialidades_profesional.SelectedItem.ToString();

            SqlDataAdapter adapter = new SqlDataAdapter(busqueda_profesional_especialidades);
            rellenar_tabla_informacion(adapter);

            conexion.Close();
        }

        private void buscar_profesional_refinado_por_tipo()
        {
            this.tipo_busqueda = "con_datos_y_especialidad";
            conexion.Open();

            SqlCommand busqueda_profesional_especialidades = new SqlCommand("CHAMBA.BUSQUEDA_PROFESIONAL_REFINADO_POR_TIPO", conexion);

            busqueda_profesional_especialidades.CommandType = CommandType.StoredProcedure;
            busqueda_profesional_especialidades.Parameters.Add("@Nombre_profesional", SqlDbType.VarChar).Value = Nombre_profesional.Text.ToString();
            busqueda_profesional_especialidades.Parameters.Add("@Apellido_profesional", SqlDbType.VarChar).Value = Apellido_profesional.Text.ToString();
            busqueda_profesional_especialidades.Parameters.Add("@Especialidad", SqlDbType.VarChar).Value = Especialidades_profesional.SelectedItem.ToString();

            //busqueda_profesional_especialidades.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(busqueda_profesional_especialidades);

            rellenar_tabla_informacion(adapter);
            conexion.Close();
        }

        private void buscar_profesional_sin_refinamiento()
        {
            this.tipo_busqueda = "solo_datos";

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

        /*-------------------------------------------SCROLL ESPECIALIDADES------------------------------------*/
        private void rellenar_scroll_especialidades()
        {
            conexion.Open();

            String query = "SELECT e.Espe_Descripcion as 'Tipo' FROM CHAMBA.Especialidades e ORDER BY e.Espe_Descripcion";
            SqlCommand listar = new SqlCommand(query, conexion);

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = listar;
            adapter.Fill(tabla);



            int indeeex = 0;
            while (indeeex < tabla.Rows.Count)
            {
                Especialidades_profesional.Items.Add(tabla.Rows[indeeex]["Tipo"].ToString());
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
