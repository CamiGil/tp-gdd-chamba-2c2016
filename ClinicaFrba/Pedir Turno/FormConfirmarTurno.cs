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
    public partial class FormConfirmarTurno : Form
    {
        /*-------------------------------------------ATRIBUTOS-----------------------------------------------*/
        private SqlConnection conexion;
        private decimal profesional;
        private string nombre_profesional;
        private string nombre_especialidad;
        private decimal especialidad;

        /*-------------------------------------------CONSTRUCTOR---------------------------------------------*/
        public FormConfirmarTurno()
        {
            InitializeComponent();
            this.conexion = new SqlConnection(@Configuraciones.datosConexion);
        }

        private void FormConfirmarTurno_Load(object sender, EventArgs e)
        {
            Calendario.TodayDate = Configuraciones.fecha;
            Calendario.MinDate = Configuraciones.fecha;
            Calendario.SelectionStart = Configuraciones.fecha;

            conexion.Open();

            SqlCommand busqueda_dias = new SqlCommand("CHAMBA.DIAS_DISPONIBLES_PROFESIONAL_POR_ESPECIALIDAD", conexion);

            busqueda_dias.CommandType = CommandType.StoredProcedure;
            busqueda_dias.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = this.profesional;
            busqueda_dias.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = this.especialidad;
            busqueda_dias.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = Configuraciones.fecha;

            DataTable tabla = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(busqueda_dias);
            adapter.Fill(tabla);

            int i;
            for (i = 0; i < tabla.Rows.Count; i++)
            {
                Calendario.AddBoldedDate((DateTime)tabla.Rows[i]["Agen_Fecha"]);
            }

            Calendario.UpdateBoldedDates();

            conexion.Close();

        }

        /*-------------------------------------------INICIALIES----------------------------------------------*/
        public void obtener_datos(decimal id_profesional, decimal id_especialidad, string nombre_profesional, string especialidad)
        {

            this.profesional = id_profesional;
            this.nombre_profesional = nombre_profesional;
            this.nombre_especialidad = especialidad;
            this.especialidad = id_especialidad;

            mostrar_datos_del_profesional();
        }

        private void mostrar_datos_del_profesional()
        {
            Nombre_del_profesional.Text = this.nombre_profesional;
            Especialidad_del_profesional.Text = this.nombre_especialidad;
        }

        /*-------------------------------------------CARGAR HORARIOS DISPONIBLES-------------------------------*/
        private void Horarios_disponibles_Click(object sender, EventArgs e)
        {
            SelectionRange fecha = Calendario.SelectionRange;
            string mes = fecha.Start.Month.ToString();
            string anio = fecha.Start.Year.ToString();
            string dia = fecha.Start.Day.ToString();
            cargar_horas_disponibles_del_dia(dia, mes, anio);
        }

        private void cargar_horas_disponibles_del_dia(string dia, string mes, string anio)
        {
            conexion.Open();

            SqlCommand busqueda_profesional_especialidades = new SqlCommand("CHAMBA.HORARIOS_DISPONIBLES_EN_AGENDA_PROFESIONAL", conexion);

            busqueda_profesional_especialidades.CommandType = CommandType.StoredProcedure;
            busqueda_profesional_especialidades.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = this.profesional;
            busqueda_profesional_especialidades.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = this.especialidad;
            busqueda_profesional_especialidades.Parameters.Add("@Dia", SqlDbType.Int).Value = int.Parse(dia);
            busqueda_profesional_especialidades.Parameters.Add("@Numero_mes", SqlDbType.Int).Value = int.Parse(mes);
            busqueda_profesional_especialidades.Parameters.Add("@Anio", SqlDbType.Int).Value = int.Parse(anio);

            Info.DataSource = null;

            SqlDataAdapter adapter = new SqlDataAdapter(busqueda_profesional_especialidades);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Info.DataSource = table;

            conexion.Close();
        }

        /*-------------------------------------------ACEPTAR--------------------------------------------------*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (Info.SelectedRows.Count > 0) { 

                DataGridViewRow row = Info.SelectedRows[0];

                /* OPERACION */
                conexion.Open();

                SqlCommand reserva = new SqlCommand("CHAMBA.RESERVA_DE_TURNO", conexion);

                reserva.CommandType = CommandType.StoredProcedure;
                reserva.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = Configuraciones.usuario;
                reserva.Parameters.Add("@Agenda_id", SqlDbType.Decimal).Value = decimal.Parse(row.Cells[1].Value.ToString());

                reserva.ExecuteNonQuery();

                conexion.Close();

                MessageBox.Show("Turno reservado con éxito");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }else{
                MessageBox.Show("Seleccione un horario");
            }
        }
       
        /*-------------------------------------------CANCELAR------------------------------------------------*/
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*-------------------------------------------NO RELEVANTES--------------------------------------------*/
        private void label1_Click(object sender, EventArgs e)
        {

        }        
    }
}
