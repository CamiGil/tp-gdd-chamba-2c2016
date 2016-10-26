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
        private string nombre_profesional;
        private string apellido_profesional;
        private string tipo_especialidad;
        private string nombre_especialidad;
        private string afiliado;
        private string bono;

        /*-------------------------------------------CONSTRUCTOR---------------------------------------------*/
        public FormConfirmarTurno()
        {
            InitializeComponent();
            this.conexion = new SqlConnection(@Configuraciones.datosConexion);
            this.FormClosed += Configuraciones.validarCierreVentana;

            mostrar_datos_del_profesional();
            mostrar_dias_disponibles_iniciales();
        }

        /*-------------------------------------------INICIALIES----------------------------------------------*/
        public void obtener_datos(string afiliado, string numero_bono,string nombre, string apellido, string tipo_especialidad, string especialidad)
        {
            this.nombre_especialidad = nombre;
            this.apellido_profesional = apellido;
            this.tipo_especialidad = tipo_especialidad;
            this.nombre_especialidad = especialidad;
            this.afiliado = afiliado;
            this.bono = numero_bono;
        }

        private void mostrar_datos_del_profesional()
        {
            Nombre_del_profesional.Text = this.nombre_profesional + this.apellido_profesional;
            Especialidad_del_profesional.Text = this.tipo_especialidad + ',' + this.nombre_especialidad;
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
            busqueda_profesional_especialidades.Parameters.Add("@Nombre_profesional", SqlDbType.VarChar).Value = this.Nombre_del_profesional;
            busqueda_profesional_especialidades.Parameters.Add("@Apellido_profesional", SqlDbType.VarChar).Value = this.apellido_profesional;
            busqueda_profesional_especialidades.Parameters.Add("@Tipo_especialidad", SqlDbType.VarChar).Value = this.tipo_especialidad;
            busqueda_profesional_especialidades.Parameters.Add("@Especialidad", SqlDbType.VarChar).Value = this.Especialidad_del_profesional;
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
            busqueda_profesional_especialidades.Parameters.Add("@Nombre_profesional", SqlDbType.VarChar).Value = this.Nombre_del_profesional;
            busqueda_profesional_especialidades.Parameters.Add("@Apellido_profesional", SqlDbType.VarChar).Value = this.apellido_profesional;
            busqueda_profesional_especialidades.Parameters.Add("@Tipo_especialidad", SqlDbType.VarChar).Value = this.tipo_especialidad;
            busqueda_profesional_especialidades.Parameters.Add("@Especialidad", SqlDbType.VarChar).Value = this.Especialidad_del_profesional;
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

            /* OBTENCION DE DIA, MES Y AÑO SELECCIONADO */
            SelectionRange fecha = Calendario.SelectionRange;
            string mes = fecha.Start.Month.ToString();
            string anio = fecha.Start.Year.ToString();
            string dia = fecha.Start.Day.ToString();

            /* OBTENCION DE HORA ESPECIFICA SELECCIONADO */
            DataGridViewRow row= Info.SelectedRows[0];
            string hora = row.Cells[1].ToString();
            string minutos = row.Cells[2].ToString();

            /* OPERACION */
            conexion.Open();

            SqlCommand reserva = new SqlCommand("CHAMBA.RESERVA_DE_TURNO", conexion);

            reserva.CommandType = CommandType.StoredProcedure;
            reserva.Parameters.Add("@Nombre_profesional", SqlDbType.VarChar).Value = this.Nombre_del_profesional;
            reserva.Parameters.Add("@Apellido_profesional", SqlDbType.VarChar).Value = this.apellido_profesional;
            reserva.Parameters.Add("@Tipo_especialidad", SqlDbType.VarChar).Value = this.tipo_especialidad;
            reserva.Parameters.Add("@Especialidad", SqlDbType.VarChar).Value = this.Especialidad_del_profesional;
            reserva.Parameters.Add("@Dia", SqlDbType.Int).Value = int.Parse(dia);
            reserva.Parameters.Add("@Numero_mes", SqlDbType.Int).Value = int.Parse(mes);
            reserva.Parameters.Add("@Anio", SqlDbType.Int).Value = int.Parse(anio);
            reserva.Parameters.Add("@Hora", SqlDbType.Int).Value = int.Parse(hora);
            reserva.Parameters.Add("@Minuto", SqlDbType.Int).Value = int.Parse(minutos);

            reserva.ExecuteNonQuery();
            conexion.Close();
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
      
    }
}
