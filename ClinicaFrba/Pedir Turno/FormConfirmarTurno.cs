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
        private decimal afiliado;
        private string bono;

        /*-------------------------------------------CONSTRUCTOR---------------------------------------------*/
        public FormConfirmarTurno()
        {
            InitializeComponent();
            this.conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.FormClosed += Configuraciones.validarCierreVentana;

        }

        /*-------------------------------------------INICIALIES----------------------------------------------*/
        public void obtener_datos(string afiliado, string numero_bono,string id_profesional, string id_especialidad, string nombre_profesional, string especialidad)
        {

            this.profesional = decimal.Parse(id_profesional);
            this.nombre_profesional = nombre_profesional;
            this.nombre_especialidad = especialidad;
            this.especialidad = decimal.Parse(id_especialidad);
            this.afiliado = decimal.Parse(afiliado);
            this.bono = numero_bono;

            mostrar_datos_del_profesional();
            mostrar_dias_disponibles_iniciales();
        }

        private void mostrar_datos_del_profesional()
        {
            Nombre_del_profesional.Text = this.nombre_profesional;
            Especialidad_del_profesional.Text = this.nombre_especialidad;
        }

        private void mostrar_dias_disponibles_iniciales()
        {
            SelectionRange fecha = Calendario.SelectionRange;
            string mes = fecha.Start.Month.ToString();
            string anio = fecha.Start.Year.ToString();
            cargar_dias_disponibles_del_mes(mes,anio);
        }

        private void cargar_dias_disponibles_del_mes(string mes, string anio)
        {
            conexion.Open();

            SqlCommand busqueda_profesional_especialidades = new SqlCommand("CHAMBA.DIAS_DISPONIBLES_PROFESIONAL_POR_ESPECIALDIAD", conexion);

            busqueda_profesional_especialidades.CommandType = CommandType.StoredProcedure;
            busqueda_profesional_especialidades.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = this.profesional;
            busqueda_profesional_especialidades.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = this.especialidad;
            busqueda_profesional_especialidades.Parameters.Add("@Numero_mes", SqlDbType.Int).Value = int.Parse(mes);
            busqueda_profesional_especialidades.Parameters.Add("@Anio", SqlDbType.Int).Value = int.Parse(anio);

            SqlDataAdapter adapter = new SqlDataAdapter(busqueda_profesional_especialidades);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Info.DataSource = table;

            conexion.Close();
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

            DataGridViewRow row = Info.SelectedRows[0];
            string id_agenda= row.Cells[2].Value.ToString(); // <-- la tercera columna es la correspondiente al ID_Agenda

            /* OPERACION */
            conexion.Open();

            SqlCommand reserva = new SqlCommand("CHAMBA.RESERVA_DE_TURNO", conexion);

            reserva.CommandType = CommandType.StoredProcedure;
            reserva.Parameters.Add("@Afiliado", SqlDbType.Decimal).Value = this.afiliado;
            reserva.Parameters.Add("@Profesional", SqlDbType.Decimal).Value = this.profesional;
            reserva.Parameters.Add("@Especialidad", SqlDbType.Decimal).Value = this.especialidad;
            reserva.Parameters.Add("@Agenda_id", SqlDbType.Decimal).Value = decimal.Parse(id_agenda);

            reserva.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Turno reservado con éxito");
            this.Hide();
        }
       
        /*-------------------------------------------CANCELAR------------------------------------------------*/
        private void button2_Click(object sender, EventArgs e)
        {

        }

        /*-------------------------------------------NO RELEVANTES--------------------------------------------*/
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormConfirmarTurno_Load(object sender, EventArgs e)
        {

        }
        
        /*-------------------------------------------DÍAS DE ATENCION EN EL MES--------------------------------*/
        private void DiasDeAtencion_Click(object sender, EventArgs e)
        {
            SelectionRange fecha = Calendario.SelectionRange;
            string mes = fecha.Start.Month.ToString();
            string anio = fecha.Start.Year.ToString();
            this.cargar_dias_disponibles_del_mes(mes, anio);
        }
      
    }
}
